using System.ComponentModel.DataAnnotations;

namespace PS1_MIC_090_Core.Repository.Domain
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Suffix { get; set; }
        public DateTime DOB { get; set; }
        public string? Gender { get; set; }
        public string? ApplicationReference { get; set; }
        public int ApplicationStatusId { get; set; }
        public int UserId { get; set; }

    }
}
