using HWTechnicalTest.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using System.Collections.Generic;
using HWTechnicalTest.Interfaces;
using System.Linq.Expressions;

namespace HWTechnicalTest.FTApi
{
    public class FTAPIClient
    {
        const int MIN_API_CALL_INTERVAL = 3000; // 3 second
        const int API_SEARCH_COUNT = 50;

        private FTAccessToken _token;

        private DateTime? _lastApiCall = null;

        private readonly IOptions<FTApiSettings> _settings;
        private readonly IDBOffersService _dbService;
        private System.Runtime.Caching.MemoryCache _memoryCache = System.Runtime.Caching.MemoryCache.Default;

        public FTAPIClient(IOptions<FTApiSettings> settings, IDBOffersService dbService)
        {
            _settings = settings;
            _dbService = dbService;
        }

        /// <summary>
        /// Get a token from France Travail API
        /// </summary>
        /// <returns></returns>
        private async Task GetToken()
        {

            var dict = new Dictionary<string, string>();
            dict.Add("grant_type", _settings.Value.LoginGrantType);
            dict.Add("client_id", _settings.Value.ClientId);
            dict.Add("client_secret", _settings.Value.ClientSecret);
            dict.Add("scope", _settings.Value.LoginScope);

            using HttpClient client = new HttpClient();

            using FormUrlEncodedContent content = new(dict);

            using HttpResponseMessage response = await client.PostAsync(_settings.Value.LoginUrl, content);

            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            _token = JsonSerializer.Deserialize<FTAccessToken>(jsonResponse);

            // if got a token, then set the expiration date
            if (_token != null)
                _token.expires_at = DateTime.Now.AddSeconds(_token.expires_in);
        }

        /// <summary>
        /// Get offers from France Travail API
        /// </summary>
        /// <param name="apiParameters">search parameters</param>
        /// <returns></returns>
        public async Task<List<JobOffer>> GetOffers(FTApiParameters apiParameters)
        {
            await PreventApiFlooding();

            // if token is null or expired, then get a new one
            if (_token == null || _token.Expired)
                await GetToken();

            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token?.access_token}");

            Console.WriteLine($"{_settings.Value.ApiUrl}{apiParameters.ToString()}");
            try
            {
                using HttpResponseMessage response = await client.GetAsync($"{_settings.Value.ApiUrl}{apiParameters.ToString()}");

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                FTApiSearchResponse apiResponse = JsonSerializer.Deserialize<FTApiSearchResponse>(json);

                _lastApiCall = DateTime.Now;

                return apiResponse.Resultats;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }


        }

        /// <summary>
        /// For each location, get the offers from France Travail API and add them to the db
        /// To get only the newest offers, we call the api sorted by publication date desc and stop when we reach the most recent offer in the db
        /// </summary>
        /// <returns></returns>
        public async Task CreateNewOffers()
        {
            // TODO: use offers creation date to optimize the detection of new offers

            foreach (var location in _settings.Value.LocationsInsee)
            {
                try
                {
                    int offset = 0;

                    //JobOffer mostRecent = await _dbService.GetLocationMostRecentAsync(location);

                    bool done = false;
                    while (!done) // continue until we reach the most recent offer
                    {
                        FTApiParameters api_parameters = new FTApiParameters
                        {
                            Range = new FTRangeParameters
                            {
                                Count = API_SEARCH_COUNT,
                                Offset = offset
                            },
                            DaysPublishSince = _settings.Value.DaysPublishSince,
                            Sort = 1,
                            INSEELocation = location,
                            //MaxCreationDate = mostRecent?.DateCreation
                        };

                        var offers = await GetOffers(api_parameters);
                        if (offers == null)
                            break;

                        foreach (var o in offers)
                        {
                            if (!await OfferAlreadySynced(o.Id)) // add it to the db if non existent
                            {
                                await _dbService.CreateAsync(o);
                                AddToCache(o.Id); // add offer id to cache
                            }
                        }

                        offset += API_SEARCH_COUNT;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        /// <summary>
        /// Check if an offer is already in the db, check into memory cache first
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<bool> OfferAlreadySynced(string id)
        {
            if (_memoryCache.Contains(id)) return true;

            var exists = await _dbService.Exists(id);

            if(exists) AddToCache(id);

            return exists;
        }

        /// <summary>
        /// Add Offer id to memory cache
        /// </summary>
        /// <param name="id"></param>
        private void AddToCache(string id)
        {
            if (_memoryCache.Contains(id)) return;
            _memoryCache.Add(id, true, DateTimeOffset.Now.AddHours(1));
        }

        private async Task PreventApiFlooding()
        {
            if (!_lastApiCall.HasValue) // if first call, then return
                return;

            await Task.Delay(MIN_API_CALL_INTERVAL - (DateTime.Now - _lastApiCall.Value).Milliseconds);
        }
    }
}
