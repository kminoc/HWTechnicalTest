using HWTechnicalTest.Interfaces;

namespace HWTechnicalTest.Services
{
    public class DBOffersServiceFactory
    {
        private readonly IEnumerable<IDBOffersService> _dbServices;
        public DBOffersServiceFactory(IEnumerable<IDBOffersService> dbServices)
        {
            _dbServices = dbServices;
        }
        public IDBOffersService GetDBService(string db_type)
        {
            return _dbServices.FirstOrDefault(e => e.DBType.Equals(db_type))
                ?? throw new NotSupportedException();
        }


    }
}
