using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication3.Entity.Repositories;
using WebApplication3.Helpers;
using WebMatrix.WebData;

namespace WebApplication3.Models
{
    public interface IAuthenticationProvider
    {
        void SignOut();
        bool Login(Login loginData);
        string Register(Register registerData);
        bool ChangePassword(ChangePassword registerData);
        string GeneratePasswordResetToken(string userName);
        string SendEmail(UserProfile userProfile, string returnUrl);
        bool ResetPassword(RestorePasswordParam restorePasswordParamData);
    }

    public class FormsAuthWrapper : IAuthenticationProvider
    {
        public bool ResetPassword(RestorePasswordParam restorePasswordParamData)
        {
            return WebSecurity.ResetPassword(restorePasswordParamData.token, restorePasswordParamData.Password);
        }

        public string SendEmail(UserProfile userProfile, string returnUrl)
        {
            string securityToken = GeneratePasswordResetToken(userProfile.UserName);
            string body = ConstantDocument.Url + returnUrl + "?token=" + securityToken;
            Tools.SendEmail(userProfile.EmailAddress, body);

            return body;
        }

        public string GeneratePasswordResetToken(string userName)
        {
            string securityToken = WebSecurity.GeneratePasswordResetToken(userName);
            return securityToken;
        }

        public bool ChangePassword(ChangePassword changeData)
        {
            return WebSecurity.ChangePassword(changeData.UserName, changeData.PasswordOld, changeData.PasswordNew);
        }

        public string Register(Register registerData)
        {
            return WebSecurity.CreateUserAndAccount(registerData.UserName, registerData.Password);
        }

        public bool Login(Login loginData)
        {
            //return System.Web.Security.Membership.ValidateUser(loginData.UserName, loginData.Password);
            return WebSecurity.Login(loginData.UserName, loginData.Password);
        }

        public void SignOut()
        {
            WebSecurity.Logout();
            //FormsAuthentication.SignOut();
        }
    }
}