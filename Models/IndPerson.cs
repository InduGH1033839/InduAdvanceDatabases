using System.Text.Json.Serialization;

namespace Hospital_Management_System_Indu.Models
{
    public class IndPerson
    {
        public int IndID { get; set; }
        public string IndName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string MobileNo { get; set; }
        public string MailAddr { get; set; }
        [JsonIgnore]
        public ICollection<VisitNote> VisitNotes { get; set; }
    }
}
