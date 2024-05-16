using HWTechnicalTest.FTApi;
using Microsoft.Extensions.Options;

namespace HWTechnicalTest.Services
{
    public class OffersUpdateBackgroundService: BackgroundService
    {
        const int interval = 5;

        private readonly FTAPIClient _ftApiClient;

        public OffersUpdateBackgroundService(FTAPIClient ftApiClient)
        {
            _ftApiClient = ftApiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {           
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                   await _ftApiClient.CreateNewOffers();
                }
                catch (Exception ex)
                {
                    //TODO: Log the exception
                }
                finally
                {
                    // Wait [interval] minutes before next call
                    await Task.Delay(TimeSpan.FromMinutes(interval), stoppingToken);
                }
            }
        }
    }
}
