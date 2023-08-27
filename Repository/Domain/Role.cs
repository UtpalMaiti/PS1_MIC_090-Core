using System.ComponentModel.DataAnnotations;

namespace PS1_MIC_090_Core.Repository.Domain
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        public required string RoleName { get; set; }
    }
}
