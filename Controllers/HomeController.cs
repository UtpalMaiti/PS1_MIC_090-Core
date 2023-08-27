using AutoMapper;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PS1_MIC_090_Core.Models;
using PS1_MIC_090_Core.Models.Constants;
using PS1_MIC_090_Core.Repository;
using PS1_MIC_090_Core.Repository.Contracts;
using PS1_MIC_090_Core.Repository.Domain;

using System.Diagnostics;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using PS1_MIC_090_Core.Models.Utilities;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using static System.Net.WebRequestMethods;

namespace PS1_MIC_090_Core.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper mapper;
        private readonly IRepository<User> users;
        private readonly IRepository<NameSuffix> nameSuffixs;
        private readonly IRepository<Role> roles;
        private readonly IRepository<Relation> relations;
        private readonly IRepository<UserRoleMapping> userRoleMappings;
        private readonly IUserRepository userRepository;

        public string? ReturnUrl { get; private set; }
        public string? ErrorMessage { get; private set; }

        public HomeController(
            ILogger<HomeController> logger, 
            IMapper mapper,
            IRepository<User> users,
            IRepository<NameSuffix> nameSuffixs,
            IRepository<Role> roles,
            IRepository<Relation> relations,
            IRepository<UserRoleMapping> userRoleMappings,
            IUserRepository userRepository
            )
        {
            this._logger = logger;
            this.mapper = mapper;
            this.users = users;
            this.nameSuffixs = nameSuffixs;
            this.roles = roles;
            this.relations = relations;
            this.userRoleMappings = userRoleMappings;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                int userid = GetCurrentUserId();

                if (userid < 1)
                {
                    return RedirectToAction("LogOn", "Home");
                }

                var mapping = (await this.userRoleMappings.GetAll())
                    .FirstOrDefault(x => x.UserId == userid);

                if (mapping != null)
                {
                    switch (mapping.RoleId)
                    {
                        case 1:
                            ViewBag.hasCreateApplication = true;
                            ViewBag.hasSearchApplication = true;
                            break;
                        case 2:
                            ViewBag.hasCreateApplication = true;
                            ViewBag.hasSearchApplication = false;
                            break;
                        default:
                            ViewBag.hasCreateApplication = true;
                            ViewBag.hasSearchApplication = false;
                            break;
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, nameof(HomeController), nameof(Index));
                throw;
            }

        }
        public ActionResult About()
        {

            //[FromQuery] -Gets values from the query string.
            //[FromRoute] - Gets values from route data.
            //[FromForm] -Gets values from posted form fields.
            //[FromBody] - Gets values from the request body.
            //[FromHeader] - Gets values from HTTP headers.

            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Logon()
        {

            int userid = GetCurrentUserId();

            if (userid > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Logon(LogOnViewModel model, string? returnUrl = null)
        {
            ReturnUrl = returnUrl;

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var hashPassword = Hasher.GenerateHashPassword(model.Password);



                var user = (await this.users.GetAll())
                  .FirstOrDefault(x => x.UserName.Equals(model.UserName) && x.Password.Equals(model.Password));
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                }
                if (user != null)
                {
                    SetCurrentUserId(user.UserId, model.isRememberMe);


                    // Use Input.Email and Input.Password to authenticate the user
                    // with your custom authentication logic.
                    //
                    // For demonstration purposes, the sample validates the user
                    // on the email address maria.rodriguez@contoso.com with 
                    // any password that passes model validation.
                   

                    var claims = new List<Claim>
        {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Role, "Administrator"),
                        new Claim(ClaimTypes.Upn, "Administrator"),
                        new Claim(ClaimTypes.Dsa, "Administrator"),
                        new Claim(ClaimTypes.CookiePath, "Administrator"),
                        new Claim(ClaimTypes.Role, "Administrator"),
                         new Claim("LastChanged", "False")
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        // Refreshing the authentication session should be allowed.

                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        IsPersistent = true,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        IssuedUtc = DateTimeOffset.UtcNow,
                        // The time at which the authentication ticket was issued.

                        RedirectUri = ReturnUrl
                        // The full path or absolute URI to be used as an http 
                        // redirect response value.
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    //HttpContext.User.AddIdentity(claimsIdentity);
                    //HttpContext.User.

                 var aa=   await HttpContext.AuthenticateAsync(
                     CookieAuthenticationDefaults.AuthenticationScheme);

                  await HttpContext.ChallengeAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, 
                        authProperties!);

                  var ttGetTokenAsync=  await HttpContext.GetTokenAsync(
                          CookieAuthenticationDefaults.AuthenticationScheme,
                          "GetTokenAsync");

                  

                    var i = HttpContext.User;



                    return RedirectToAction(nameof(Index));
                    //return LocalRedirect(Url.GetLocalUrl(returnUrl));
                }

                return View();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, nameof(HomeController), nameof(Logon));
                throw;
            }
           
        }
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User() 
            { 
                UserName = model.UserName, 
                EmailId = model.Email, 
                Password = model.Password, 
                UserId = 0 
            };
            user= await this.users.Add(user);
          
            return  RedirectToAction(nameof(Logon));
        }
        public async Task<ActionResult> Herder()
        {
            HeaderViewModel model = new HeaderViewModel();
            int userid = GetCurrentUserId();

            var user = (await this.users.GetAll())
                .FirstOrDefault(x => x.UserId == userid);
            
            if (user != null)
            {
                model.UserName = user.UserName;
                model.Email = user.EmailId;

                var mapping =  (await this.userRoleMappings.GetAll())
                    .FirstOrDefault(x => x.UserId == userid);
                if (mapping != null)
                {
                    var role = (await this.roles.GetAll())
                  .FirstOrDefault(x => x.RoleId == mapping.RoleId);

                    if (role != null)
                    {
                        model.RoleName = role.RoleName;
                    }
                }
            }
            model.CompanyName = "Company Logo";

            return PartialView(@"~/Views/Shared/_Herder.cshtml", model);
        }

        public ActionResult Search()
        {
            return View();
        }
        //[ChildActionOnly]
        public ActionResult ConfirmationScreen()
        {
            return View();
        }
        //[NonAction]
        public ActionResult RelationshipInfoScreen()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> HouseholdInfo()
        {
            CreateApplicationViewModel model = new CreateApplicationViewModel(new List<SelectListItem> ());
            model.UserId = GetCurrentUserId();
            model.DOB = DateTime.Now.ToUniversalTime();


            try
            {
                model.Suffixs = (await this.nameSuffixs.
                    GetAll()).
                    Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Suffix,
                        Value = x.SuffixId.ToString(),
                        //Group= new SelectListGroup()
                        //{
                        //    Name=nameof( x.Suffix),
                        //}
                    })
                    .ToList();
            }
            catch (Exception)
            {

                throw;
            }


            return View(model);
        }
        public async Task<ActionResult> LogOut(string? returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            ReturnUrl = returnUrl;

            SetCurrentUserId(0);
            base.UserLogOut();
            return RedirectToAction(nameof(Logon));
        }
        public JsonResult ValidateName(string userName)
        {
            if (!string.IsNullOrEmpty(userName) && userName.StartsWith("Pr"))
            {
                return Json("Employee Name is already existing");
            }
            else
            {
                return Json(true);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}