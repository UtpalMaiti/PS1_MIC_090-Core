using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PS1_MIC_090_Core.Repository.Domain
{
    public class UserRoleMapping
    {
       
        [Key]
        public int UserRoleMappingId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        // Navigation Reference
        public virtual required User User { get; set; }
        public virtual required Role Role { get; set; }
    }
}
