using iOrder.Entity;
using iOrder.Entity.Repositories;
using iOrder.Helpers;
using iOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace iOrder.Controllers
{
    public partial class AccountController : Controller
    {
        #region Properties

        private IUow _uow;
        private IAuthenticationProvider _authProvider;

        #endregion
        #region Methods

        public AccountController(IUow uow, IAuthenticationProvider authProvider)
        {
            this._uow = uow;
            this._authProvider = authProvider;
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
                if (_authProvider.Login(loginData))
                {
                    //bool rememberMe = true;
                    //FormsAuthentication.SetAuthCookie(loginData.UserName, rememberMe);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction(MVC.Document.ActionNames.Index, MVC.Document.Name);
                }

                ModelState.AddModelError("", ConstantDocument.ErrorLogin);
                return View(loginData);
            }

            return View(loginData);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult Logout()
        {
            _authProvider.SignOut();
            return RedirectToAction(MVC.Account.ActionNames.Login, MVC.Account.Name);
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
                    _authProvider.Register(registerData);
                    return RedirectToAction(MVC.Account.ActionNames.Login, MVC.Account.Name);
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
                    _authProvider.Register(registerData);
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
                if (!_authProvider.ChangePassword(chgPwdData))
                {
                    ModelState.AddModelError("", ConstantDocument.ErrorChangePassword);
                    return View(MVC.Account.ActionNames.ChangePassword, chgPwdData);
                }

                return RedirectToAction(MVC.Document.ActionNames.Index, MVC.Document.Name);
            }

            return View(chgPwdData);
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
                    ModelState.AddModelError("", ConstantDocument.ErrorSendEmail);
                    return View(model);
                }
                string url = Url.Action(MVC.Account.ActionNames.ConfirmPassword, MVC.Account.Name);
                _authProvider.SendEmail(userProfile, url);

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
        public virtual ActionResult ConfirmPassword(RestorePasswordParam model)
        {
            if (ModelState.IsValid)
            {
                bool result = _authProvider.ResetPassword(model);
                if (result)
                    return RedirectToAction(MVC.Account.ActionNames.Login, MVC.Account.Name);

                ModelState.AddModelError("", ConstantDocument.ErrorResetPassword);
            }

            return View(MVC.Account.ActionNames.ConfirmPassword, model);
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