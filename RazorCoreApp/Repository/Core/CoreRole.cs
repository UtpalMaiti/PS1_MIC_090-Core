using Microsoft.AspNetCore.Identity;

namespace PS1_MIC_090_Core.Repository.Core
{
    public class CoreRole : IdentityRole<long>
    {
        public CoreRole()
        {
        }

        public CoreRole(string roleName) : base(roleName)
        {
        }

        public override long Id { get => base.Id; set => base.Id = value; }
        public override string? Name { get => base.Name; set => base.Name = value; }
        public override string? NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; }
        public override string? ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
