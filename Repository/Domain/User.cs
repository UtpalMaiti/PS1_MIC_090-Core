using System.ComponentModel.DataAnnotations;

namespace PS1_MIC_090_Core.Repository.Domain
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string EmailId { get; set; }
    }
}
