using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Hospital_Management_System_Indu.Models
{
    public class HealthEntry
    {
        [BsonId]
        public ObjectId EntryID { get; set; }

        public int RefIndID { get; set; }
        public string CaseTitle { get; set; }
        public List<string> Symptoms { get; set; }
        public List<string> PrescribedDrugs { get; set; }
        public List<string> ScanDocs { get; set; }
        public DateTime LoggedOn { get; set; }
    }

}
