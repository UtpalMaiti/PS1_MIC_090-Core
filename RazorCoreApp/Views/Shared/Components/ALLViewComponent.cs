using Microsoft.AspNetCore.Mvc;

namespace PS1_MIC_090_Core.Views.Shared.Components
{
    public abstract class ALLViewComponent : ViewComponent
    {
        protected ALLViewComponent()
        {
           
        }
        public int GetCurrentUserId()
        {
            int userid = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            if (userid < 1)
            {
                var cUserid = HttpContext.Request.Cookies
                    .FirstOrDefault(x => x.Key != null && x.Key.Equals("UserId"));
                userid = Convert.ToInt32(cUserid.Value);
                return userid;

            }
            return userid;
        }

        public void SetCurrentUserId(int userId, bool isRemember = false)
        {
            HttpContext.Session.SetInt32("UserId", userId);

            CookieOptions cookie = new CookieOptions();
            cookie.CreateCookieHeader("UserId", userId.ToString());
            if (isRemember)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
            }
            HttpContext.Response.Cookies.Append("UserId", userId.ToString(), cookie);
        }

    }
}
