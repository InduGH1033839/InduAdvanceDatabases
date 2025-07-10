using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Hospital_Management_System_Indu.Models
{
    public class LogRecord
    {
        [BsonId]
        public ObjectId LogID { get; set; }

        public string LogMessage { get; set; }
        public DateTime ActionTime { get; set; }
    }

}
