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
    public partial class AccountController : Controller
    {
        [HttpGet]
        public virtual ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(Login loginData, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(loginData.UserName, loginData.Password))
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Documents");
                }

                ModelState.AddModelError("", "User name or password is invalid");
                return View(loginData);
            }

            return View(loginData);
        }

        [HttpGet]
        public virtual ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Register(Register registerData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(registerData.UserName, registerData.Password);
                    return RedirectToAction("Index", "Documents");
                }
                catch (MembershipCreateUserException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(registerData);
                }
            }

            return View(registerData);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public virtual ActionResult AddNewUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult AddNewUser(Register registerData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(registerData.UserName, registerData.Password);
                    return RedirectToAction("AddNewUser", "Action");
                }
                catch (MembershipCreateUserException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(registerData);
                }
            }

            return View(registerData);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ChangePassword(ChangePassword chgPwdData)
        {
            if (ModelState.IsValid)
            {
                if (!WebSecurity.ChangePassword(chgPwdData.UserName, chgPwdData.PasswordOld, chgPwdData.PasswordNew))
                {
                    ModelState.AddModelError("", "Check your old password and user");
                    return View(chgPwdData);
                }

                return RedirectToAction("Index", "Documents");
            }

            return View(chgPwdData);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Documents");
        }

        [ChildActionOnly]
        public virtual ActionResult PageLinks()
        {
            return View();
        }
    }
}