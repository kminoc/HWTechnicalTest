using HWTechnicalTest.FTApi;
using Microsoft.Extensions.Options;

namespace HWTechnicalTest.Services
{
    public class OffersUpdateBackgroundService: BackgroundService
    {
        const int interval = 5;

        private readonly FTAPIClient _ftApiClient;
        private readonly ILogger<OffersUpdateBackgroundService> _logger;

        public OffersUpdateBackgroundService(ILogger<OffersUpdateBackgroundService> logger,FTAPIClient ftApiClient)
        {
            _ftApiClient = ftApiClient;
            _logger = logger;
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
                    _logger.LogError(ex.Message);
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
