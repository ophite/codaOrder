using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication3.Models;
using WebMatrix.WebData;

namespace WebApplication3.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login loginData, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(loginData.UserName, loginData.Password))
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("GetDocumentsPost", "JournalSale_DocumentsController");
                }
            }

            ModelState.AddModelError("", "User name or password is invalid");
            return View(loginData);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register registerData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(registerData.UserName, registerData.Password);
                    return RedirectToAction("Index", "JournalSale_DocumentsController");
                }
                catch (MembershipCreateUserException ex)
                {
                    ModelState.AddModelError("", "Sorry username already exist");
                    return View(registerData);
                }
            }

            return View(registerData);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "JournalSale_DocumentsController");
        }
    }
}