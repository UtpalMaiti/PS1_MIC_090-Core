using System.ComponentModel;

namespace PS1_MIC_090_Core.Models
{
    public class SearchViewModel
    {
        [DisplayName("FirstName")]
        public string? FirstName { get; set; }
        [DisplayName("LastName")]
        public string? LastName { get; set; }
        [DisplayName("DOB")]
        public DateTime DOB { get; set; }
        [DisplayName("ApplicationID")]
        public string? ApplicationId { get; set; }
        [DisplayName("ApplicationStatus")]
        public required string ApplicationStatus { get; set; }
    }
}
