using Hospital_Management_System_Indu.Models;
using MongoDB.Driver;

namespace Hospital_Management_System_Indu.DbContextBoth
{
    public class MongoLink
    {
        private readonly IMongoDatabase _db;

        public MongoLink(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoConn"));
            _db = client.GetDatabase("HealthSys");
        }

        public IMongoCollection<HealthEntry> HealthEntries =>
            _db.GetCollection<HealthEntry>("Tbl_HealthEntries");

        public IMongoCollection<LogRecord> Logs =>
            _db.GetCollection<LogRecord>("Tbl_CaseLogs");
    }

}
