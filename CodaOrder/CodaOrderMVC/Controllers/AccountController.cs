using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication3.Entity;
using WebApplication3.Entity.Repositories;
using WebApplication3.Helpers;
using WebApplication3.Models;
using WebMatrix.WebData;

namespace WebApplication3.Controllers
{
    public partial class AccountController : Controller
    {
        #region Properties

        private IUow _uow;

        #endregion
        #region Methods

        public AccountController(IUow uow)
        {
            this._uow = uow;
        }

        #endregion
        #region Login

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

                    return RedirectToAction(MVC.Document.ActionNames.Index, MVC.Document.Name);
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
                    return RedirectToAction(MVC.Document.ActionNames.Index, MVC.Document.Name);
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
                    return RedirectToAction(MVC.Account.ActionNames.AddNewUser, MVC.Account.Name);
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

                return RedirectToAction(MVC.Document.ActionNames.Index, MVC.Document.Name);
            }

            return View(chgPwdData);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction(MVC.Document.ActionNames.Index, MVC.Document.Name);
        }

        #endregion
        #region Restore password

        [HttpGet]
        public virtual ActionResult RestorePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult RestorePassword(RestorePassword model)
        {
            if (ModelState.IsValid)
            {
                UserProfile userProfile = _uow.AccountRepository.GetUserByEmail(model.Email);
                if (userProfile == null || string.IsNullOrEmpty(userProfile.EmailAddress))
                {
                    ModelState.AddModelError("Email", "User with such email do not exist!");
                    return View(model);
                }

                string securityToken = WebSecurity.GeneratePasswordResetToken(userProfile.UserName);
                string body = "http://localhost:35133/" + Url.Action(MVC.Account.ActionNames.ConfirmPassword, MVC.Account.Name) + "?token=" + securityToken;
                Tools.SendEmail(userProfile.EmailAddress, body);

                return RedirectToAction(MVC.Account.ActionNames.ShowRestorePasswordResult, MVC.Account.Name);
            }

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult ConfirmPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmPassword(RestorePasswordParam model)
        {
            if (ModelState.IsValid)
            {
                bool result = WebSecurity.ResetPassword(model.token, model.Password);
                if (result)
                    return RedirectToAction(MVC.Account.ActionNames.Login, MVC.Account.Name);

                ModelState.AddModelError("", "Can't reset password for you. Contact administrator");
            }

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult ShowRestorePasswordResult()
        {
            return View();
        }

        #endregion
        #region Menu

        [ChildActionOnly]
        public virtual PartialViewResult TopMenuLinks()
        {
            return PartialView();
        }

        [HttpGet]
        [ChildActionOnly]
        public virtual PartialViewResult LeftMenuLinks()
        {
            return PartialView();
        }

        #endregion
        #region User profile

        [Authorize]
        public virtual PartialViewResult UserProfile()
        {
            return PartialView();
        }

        [Authorize]
        [HttpGet]
        public virtual JsonResult CodaUserProfileInfo()
        {
            return Json(_uow.AccountRepository.GetCodaUserProfile(this.User.Identity.Name).Result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public virtual JsonResult CodaUserProfileInfo(CodaUserProfile userProfile)
        {
            SqlResult result = new SqlResult();
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg[ConstantDocument.IsResponseError] = result.IsError.ToString();

            if (result.IsError)
                msg[ConstantDocument.ResponseErrorMessage] = result.Message.Message;

            return Json(msg);
        }

        #endregion
    }
}