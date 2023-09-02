using Microsoft.AspNetCore.Identity;

namespace PS1_MIC_090_Core.Repository.Core
{
    public class CoreUserLogin : IdentityUserLogin<long>
    {
        public CoreUserLogin()
        {
        }

        public override string LoginProvider { get => base.LoginProvider; set => base.LoginProvider = value; }
        public override string ProviderKey { get => base.ProviderKey; set => base.ProviderKey = value; }
        public override string? ProviderDisplayName { get => base.ProviderDisplayName; set => base.ProviderDisplayName = value; }
        public override long UserId { get => base.UserId; set => base.UserId = value; }

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
