using System.ComponentModel.DataAnnotations;

namespace PS1_MIC_090_Core.Repository.Domain
{
    public class ApplicationStatus
    {
        [Key]
        public int ApplicationStatusId { get; set; }
        public required string Status { get; set; }
    }
}
