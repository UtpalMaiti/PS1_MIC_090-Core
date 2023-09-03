using BlazorApp.Server.Data;
using BlazorApp.Server.Repository.Core;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.Controllers
{
    public class HomeAppController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<AspNetUser> userManager;
        private readonly SignInManager<AspNetUser> signInManager;

        public HomeAppController(
            ApplicationDbContext context,
            UserManager<AspNetUser> userManager,
            SignInManager<AspNetUser> signInManager,
            IUserStore<AspNetUser> userStore,
            ILogger<HomeAppController> logger,
            IEmailSender emailSender
            )
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            
            return View();
        }
        [AllowAnonymous]
        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
