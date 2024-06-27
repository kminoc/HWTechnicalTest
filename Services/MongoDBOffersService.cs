using HWTechnicalTest.Interfaces;
using HWTechnicalTest.Model;
using HWTechnicalTest.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HWTechnicalTest.Services
{
    public class MongoDBOffersService:IDBOffersService
    {
        private readonly IMongoCollection<JobOffer> _offersCollection;
        public MongoDBOffersService(IOptions<MongoDBSettings> DBSettings)
        {
            var mongoClient = new MongoClient(DBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(DBSettings.Value.DatabaseName);

            _offersCollection = mongoDatabase.GetCollection<JobOffer>(DBSettings.Value.CollectionName);
        }
        public string DBType => "MONGODB";
        
        public async Task<List<JobOffer>> GetAsync(int offset,int page_size) =>        
           await _offersCollection.Find(_ => true).SortBy(o=>o.DateCreation).Skip(offset).Limit(page_size).ToListAsync();
        
        public async Task<JobOffer?> GetAsync(string id)=>        
            await _offersCollection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        
        public async Task CreateAsync(JobOffer newOffer) =>        
            await _offersCollection.InsertOneAsync(newOffer);
        
        public async Task UpdateAsync(string id, JobOffer updatedOffer) =>        
            await _offersCollection.ReplaceOneAsync(x => x.Id.Equals(id), updatedOffer);
       
        public async Task RemoveAsync(string id) =>        
            await _offersCollection.DeleteOneAsync(x => x.Id == id);        

        public async Task<JobOffer> GetLocationMostRecentAsync(string locationINSEE) =>     
            await _offersCollection.Find(o => o.LieuTravail!=null && o.LieuTravail.Commune.Equals(locationINSEE)).SortByDescending(o=>o.DateCreation).FirstOrDefaultAsync();

        public async Task<bool> Exists(string id) =>        
            await _offersCollection.Find(x => x.Id.Equals(id)).AnyAsync();

        public async Task<long> Count() => 
            await _offersCollection.CountDocumentsAsync(_ => true);

    }
}
