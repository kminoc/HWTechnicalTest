namespace HWTechnicalTest.Interfaces
{
    public interface IDBOffersService
    {
        public string DBType { get; }
        public Task<List<JobOffer>> GetAsync();
        public Task<JobOffer?> GetAsync(string id);
        public Task CreateAsync(JobOffer newOffer);
        public Task UpdateAsync(string id, JobOffer updatedOffer);
        public Task RemoveAsync(string id);
        public Task<JobOffer> GetLocationMostRecentAsync(string locationINSEE);
        public Task<bool> Exists(string id);
        public Task<long> Count();
    }
}
