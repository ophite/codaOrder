using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace WebApplication3.Models
{
    public interface IAuthenticationProvider
    {
        void SignOut();
        bool Login(Login loginData);
        string Register(Register registerData);
    }

    public class FormsAuthWrapper : IAuthenticationProvider
    {
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