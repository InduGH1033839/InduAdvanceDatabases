using System.Text.Json.Serialization;

namespace Hospital_Management_System_Indu.Models
{
    public class VisitNote
    {

        public int VisitID { get; set; }
        public int IndID { get; set; }
        public string Consultant { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitSummary { get; set; }
        [JsonIgnore]
        public IndPerson Person { get; set; }

    }
}
