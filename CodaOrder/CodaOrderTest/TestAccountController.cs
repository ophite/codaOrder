// system
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
// app
using WebApplication3.Controllers;
using WebApplication3.Entity;
using WebApplication3.Infrastructure;
using WebApplication3.Models;
using WebApplication3;
using WebApplication3.Helpers;
// custom
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Moq;
using Ninject;
using MvcRouteTester;
using MvcRouteTester.Common;
// current app
using CodaOrderTest.MockContext;
using Assert = NUnit.Framework.Assert;
using WebApplication3.Entity.Interfaces;

namespace CodaOrderTest
{
    [TestClass]
    public class AccountControllerTest
    {
        #region Properties

        private Mock<IUow> _uowMock;
        private Mock<IAccountRepository> _accountRepositoryMock;
        private Mock<IAuthenticationProvider> _authMock;

        private AccountController _accountController;
        private RouteCollection _routes;

        private Login _loginData;
        private Register _registerData;
        private ChangePassword _changePasswordData;
        private RestorePassword _restorePasswordData;
        private RestorePasswordParam _restorePasswordParamData;
        private UserProfile _userProfileData;
        private string _securityToken = "1111kkkk";

        #endregion
        #region Login/Logout

        [TestMethod]
        public void LogOut()
        {
            // act
            _authMock.Setup(x => x.SignOut());
            var result = _accountController.Logout() as RedirectToRouteResult;
            // assert
            Assert.NotNull(result);
            Assert.AreEqual(MVC.Account.ActionNames.Login, result.RouteValues["Action"]);
            Assert.AreEqual(MVC.Account.Name, result.RouteValues["Controller"]);
            _authMock.Verify(x => x.SignOut(), Times.Once);
        }

        [TestMethod]
        public void Login()
        {
            // act 
            ViewResult result = _accountController.Login() as ViewResult;
            // assert
            Assert.IsNotNull(result, "Should return view result from Login");
        }

        [TestMethod]
        public void LoginPost()
        {
            // act 
            _authMock.Setup(x => x.Login(_loginData)).Returns(true);
            var result = _accountController.Login(_loginData, null) as RedirectToRouteResult;
            // assert
            Assert.AreEqual(MVC.Document.ActionNames.Index, result.RouteValues["Action"]);
            Assert.AreEqual(MVC.Document.Name, result.RouteValues["Controller"]);
            _authMock.Verify(x => x.Login(It.Is<Login>(o => o.UserName == "coda admin")));
            _authMock.Verify(x => x.Login(It.Is<Login>(o => o.Password == "2222")));
        }

        [TestMethod]
        public void LoginPostWithReturnUrlUsing()
        {
            // arrange
            string urlLogin = "/" + MVC.Account.Name + "/" + MVC.Account.ActionNames.Login;
            RouteAssert.GeneratesActionUrl(_routes, urlLogin, MVC.Account.ActionNames.Login, MVC.Account.Name);
            _accountController.Url = Tools.MockUrlHelper(urlLogin, _routes);
            string returnUrl = "/" + MVC.Account.Name + "/" + MVC.Account.ActionNames.UserProfile;
            // act
            _authMock.Setup(x => x.Login(_loginData)).Returns(true);
            var result = _accountController.Login(_loginData, returnUrl) as RedirectResult;
            // assert
            Assert.AreEqual(returnUrl, result.Url);
            _authMock.Verify(x => x.Login(It.Is<Login>(o => o.UserName == _loginData.UserName)));
            _authMock.Verify(x => x.Login(It.Is<Login>(o => o.Password == _loginData.Password)));
        }

        [TestMethod]
        public void LoginPostInvalid()
        {
            // act
            _authMock.Setup(x => x.Login(_loginData)).Returns(false);
            ViewResult result = _accountController.Login(_loginData, null) as ViewResult;
            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ConstantDocument.ErrorLogin, result.ViewData.ModelState[""].Errors.First().ErrorMessage);
        }

        #endregion
        #region Routes

        public void TestRoutes()
        {
            // arrange
            string urlLogin = "/" + MVC.Account.Name + "/" + MVC.Account.ActionNames.Login;
            string urlDocument = "/" + MVC.Document.Name + "/" + MVC.Document.ActionNames.Index;
            // assert
            RouteAssert.HasRoute(_routes, urlLogin);
            RouteAssert.GeneratesActionUrl(_routes, urlLogin, MVC.Account.ActionNames.Login, MVC.Account.Name);
            RouteAssert.HasRoute(_routes, urlDocument);
            RouteAssert.GeneratesActionUrl(_routes, urlDocument, MVC.Document.ActionNames.Index, MVC.Document.Name);
        }

        #endregion
        #region Register

        [TestMethod]
        public void Register()
        {
            // act
            var result = _accountController.Register() as ViewResult;
            // assert
            Assert.IsNotNull(result, "Should return view result from register");
        }

        [TestMethod]
        public void RegisterPost()
        {
            // act
            _authMock.Setup(x => x.Register(_registerData)).Returns(string.Empty);
            var result = _accountController.Register(_registerData) as RedirectToRouteResult;
            // assert
            Assert.AreEqual(MVC.Account.ActionNames.Login, result.RouteValues["Action"]);
            Assert.AreEqual(MVC.Account.Name, result.RouteValues["Controller"]);
            _authMock.Verify(i => i.Register(It.Is<Register>(o => o.UserName == _registerData.UserName)));
            _authMock.Verify(i => i.Register(It.Is<Register>(o => o.Password == o.PasswordConfirm && o.Password == _registerData.Password)));
        }

        [TestMethod]
        public void RegisterPostErrorModelState()
        {
            // arrange
            FormCollection fc = new FormCollection()
            {
                {"Password", "1"}
            };
            // act
            ModelStateDictionary modelState = Tools.MockTryUpdateModel<Register>(_registerData, fc);
            // assert
            Assert.IsFalse(modelState.IsValid);
            Assert.IsTrue(modelState.Values.SelectMany(i => i.Errors).Any(i => i.ErrorMessage.Equals("Password must be equal")));
            Assert.IsTrue(modelState.Values.SelectMany(i => i.Errors).Any(i => i.ErrorMessage.Contains("The field Password must be a string with a minimum length of")));
        }

        [TestMethod]
        public void RegisterPostErrorValidation()
        {
            // arrange
            _registerData.Password = "1";
            // act
            var result = _accountController.Register(_registerData) as RedirectToRouteResult;
            var resultValidation = Tools.MockValidateModel(_registerData);
            // assert
            Assert.IsTrue(resultValidation.Any(i => i.ErrorMessage.Equals("Password must be equal")));
            Assert.IsTrue(resultValidation.Any(i => i.ErrorMessage.Contains("The field Password must be a string with a minimum length of")));
            Assert.AreEqual(MVC.Account.Name, result.RouteValues["Controller"]);
            Assert.AreEqual(MVC.Account.ActionNames.Login, result.RouteValues["Action"]);
            _authMock.Verify(i => i.Register(It.Is<Register>(o => o.UserName == _registerData.UserName)), Times.Once);
            _authMock.Verify(i => i.Register(It.Is<Register>(o => o.Password == _registerData.Password && o.PasswordConfirm == _registerData.PasswordConfirm)), Times.Once);
        }

        #endregion
        #region AddNewUser

        [TestMethod]
        public void AddNewUser()
        {
            // act
            var result = _accountController.AddNewUser() as ActionResult;
            // assert
            Assert.IsNotNull(result, "Should return view on add new user");
        }

        [TestMethod]
        public void AddNewUserPost()
        {
            // act 
            var result = _accountController.AddNewUser(_registerData) as RedirectToRouteResult;
            // assert
            Assert.AreEqual(MVC.Account.Name, result.RouteValues["Controller"]);
            Assert.AreEqual(MVC.Account.ActionNames.AddNewUser, result.RouteValues["Action"]);
            _authMock.Verify(i => i.Register(It.Is<Register>(o => o.UserName == _registerData.UserName)), Times.Once);
            _authMock.Verify(i => i.Register(It.Is<Register>(o => o.Password == _registerData.Password && o.PasswordConfirm == _registerData.PasswordConfirm)), Times.Once);
        }

        #endregion
        #region Change password

        [TestMethod]
        public void ChangePassword()
        {
            // act
            var result = _accountController.ChangePassword() as ViewResult;
            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ChangePasswordPost()
        {
            // act
            this.RegisterPost();
            _authMock.Setup(x => x.ChangePassword(_changePasswordData)).Returns(true);
            var result = _accountController.ChangePassword(_changePasswordData) as RedirectToRouteResult;
            // assert
            Assert.AreEqual(MVC.Document.Name, result.RouteValues["Controller"]);
            Assert.AreEqual(MVC.Document.ActionNames.Index, result.RouteValues["Action"]);
            _authMock.Verify(i => i.ChangePassword(It.Is<ChangePassword>(o => o.UserName == _changePasswordData.UserName)), Times.Once);
            _authMock.Verify(i => i.ChangePassword(It.Is<ChangePassword>(o => o.PasswordOld == _changePasswordData.PasswordOld && o.PasswordNew == _changePasswordData.PasswordNew)), Times.Once);
            _authMock.Verify(i => i.ChangePassword(It.Is<ChangePassword>(o => o.UserName == _registerData.UserName)), Times.Once);
            _authMock.Verify(i => i.ChangePassword(It.Is<ChangePassword>(o => o.PasswordOld == _registerData.Password)), Times.Once);
        }

        [TestMethod]
        public void ChangePasswordNotValidResult()
        {
            // act
            this.RegisterPost();
            _authMock.Setup(x => x.ChangePassword(_changePasswordData)).Returns(false);
            var result = _accountController.ChangePassword(_changePasswordData) as ViewResult;
            // assert
            Assert.AreEqual(MVC.Account.ActionNames.ChangePassword, result.ViewName);
            Assert.IsNotEmpty(result.ViewData.ModelState.Values.SelectMany(x => x.Errors));
            Assert.AreEqual(ConstantDocument.ErrorChangePassword, result.ViewData.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).FirstOrDefault());
            Assert.IsFalse(result.ViewData.ModelState.IsValid);
        }

        #endregion
        #region Restore password

        [TestMethod]
        public void RestorePassword()
        {
            // act
            var result = _accountController.RestorePassword() as ViewResult;
            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RestorePasswordPost()
        {
            // act 
            _accountController.Url = Tools.MockUrlHelper("/", _routes);
            string urlConfirmPassword = "/" + MVC.Account.Name + "/" + MVC.Account.ActionNames.ConfirmPassword;
            _authMock.Setup(x => x.SendEmail(_userProfileData, urlConfirmPassword)).Returns(ConstantDocument.Url + urlConfirmPassword + "?token=" + _securityToken);

            var result = _accountController.RestorePassword(_restorePasswordData) as RedirectToRouteResult;
            // assert
            RouteAssert.GeneratesActionUrl(_routes, urlConfirmPassword, MVC.Account.ActionNames.ConfirmPassword, MVC.Account.Name);
            Assert.IsNotNull(result);
            Assert.AreEqual(MVC.Account.Name, result.RouteValues["Controller"]);
            Assert.AreEqual(MVC.Account.ActionNames.ShowRestorePasswordResult, result.RouteValues["Action"]);
            _authMock.Verify(i => i.SendEmail(_userProfileData, urlConfirmPassword), Times.Once);
            Assert.That(_authMock.Object.SendEmail(_userProfileData, urlConfirmPassword), Is.StringContaining(_securityToken));
        }

        [TestMethod]
        public void ConfirmPassword()
        {
            // act 
            var result = _accountController.ConfirmPassword();
            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ConfirmPasswordPost()
        {
            // act
            _authMock.Setup(x => x.ResetPassword(_restorePasswordParamData)).Returns(true);
            var result = _accountController.ConfirmPassword(_restorePasswordParamData) as RedirectToRouteResult;
            // assert
            Assert.AreEqual(MVC.Account.Name, result.RouteValues["Controller"]);
            Assert.AreEqual(MVC.Account.ActionNames.Login, result.RouteValues["Action"]);
            _authMock.Verify(x => x.ResetPassword(It.Is<RestorePasswordParam>(o => o.token == _restorePasswordParamData.token)), Times.Once);
            _authMock.Verify(x => x.ResetPassword(_restorePasswordParamData), Times.Once);
        }

        [TestMethod]
        public void ConfirmPasswordPostNotValid()
        {
            // act
            _authMock.Setup(x => x.ResetPassword(_restorePasswordParamData)).Returns(false);
            var result = _accountController.ConfirmPassword(_restorePasswordParamData) as ViewResult;
            // assert
            Assert.AreEqual(MVC.Account.ActionNames.ConfirmPassword, result.ViewName);
            Assert.AreEqual(ConstantDocument.ErrorResetPassword, result.ViewData.ModelState.Values.SelectMany(x => x.Errors).Where(x => !string.IsNullOrEmpty(x.ErrorMessage)).Select(x => x.ErrorMessage).FirstOrDefault());
            _authMock.Verify(x => x.ResetPassword(It.Is<RestorePasswordParam>(o => o.token == _securityToken)));
            _authMock.Verify(x => x.ResetPassword(_restorePasswordParamData), Times.Once);
        }

        #endregion
        #region Init

        [TestInitialize]
        public void Init()
        {
            // arrange route assert
            _routes = new RouteCollection();
            RouteConfig.RegisterRoutes(_routes);
            RouteAssert.UseAssertEngine(new NunitAssertEngine());

            // login
            _loginData = new Login()
             {
                 UserName = "coda admin",
                 Password = "2222",
             };

            // register
            _registerData = new Register()
            {
                UserName = "test user",
                Password = "1234",
                PasswordConfirm = "1234"
            };

            // change password
            _changePasswordData = new ChangePassword()
            {
                UserName = "test user",
                PasswordNew = "9944",
                PasswordOld = _registerData.Password
            };

            // user profile
            _userProfileData = new UserProfile()
            {
                EmailAddress = "ophite@ukr.net",
                FirstName = "Yura",
                LastName = "Kobernik",
                IsEnabled = true,
                UserId = 1,
                UserName = "ophite"
            };

            // restore password
            _restorePasswordData = new RestorePassword()
            {
                Email = "testEmail@ukr.net"
            };

            // restore password param
            _restorePasswordParamData = new RestorePasswordParam()
            {
                Password = "not checking password",
                token = _securityToken
            };

            // arrange auth mock
            _authMock = new Mock<IAuthenticationProvider>();
            _authMock.Setup(x => x.GeneratePasswordResetToken(_userProfileData.UserName)).Returns(_securityToken);
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _accountRepositoryMock.Setup(x => x.GetUserByEmail(_restorePasswordData.Email)).Returns(_userProfileData);

            // arrange account controller
            _uowMock = new Mock<IUow>();
            _uowMock.Setup(x => x.AccountRepository).Returns(_accountRepositoryMock.Object);
            _accountController = new AccountController(_uowMock.Object, _authMock.Object);
        }

        [TestCleanup]
        public void Dispose()
        {
            _loginData = null;
            _routes = null;
            _uowMock = null;
            _accountController.Dispose();
            _accountController = null;
        }

        #endregion
    }
}