using Microsoft.AspNetCore.Identity;

namespace PS1_MIC_090_Core.Repository.Core
{
    public class CoreUserRole : IdentityUserRole<long>
    {
        public override long UserId { get => base.UserId; set => base.UserId = value; }
        public override long RoleId { get => base.RoleId; set => base.RoleId = value; }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
