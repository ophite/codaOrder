﻿// system
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;
using System.Collections.Specialized;
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
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CodaOrderTest
{
    [TestClass]
    public class AccountControllerTest
    {
        #region Properties

        private Mock<IUow> _uowMock;
        private Mock<IAuthenticationProvider> _authMock;

        private AccountController _accountController;
        private RouteCollection _routes;

        private Login _loginData;
        private Register _registerData;

        #endregion
        #region Login/Logout

        [TestMethod]
        public void TestLogOut()
        {
            // act
            var result = _accountController.Logout() as RedirectToRouteResult;
            // assert
            Assert.NotNull(result);
            Assert.AreEqual(MVC.Account.ActionNames.Login, result.RouteValues["Action"]);
            Assert.AreEqual(MVC.Account.Name, result.RouteValues["Controller"]);
            _authMock.Verify(x => x.SignOut(), Times.Once);
        }

        [TestMethod]
        public void TestLogin()
        {
            // act 
            ViewResult result = _accountController.Login() as ViewResult;
            // assert
            Assert.IsNotNull(result, "Should return view result from Login");
        }

        [TestMethod]
        public void TestLoginPost()
        {
            // act 
            var result = _accountController.Login(_loginData, null) as RedirectToRouteResult;
            // assert
            Assert.AreEqual(MVC.Document.ActionNames.Index, result.RouteValues["Action"]);
            Assert.AreEqual(MVC.Document.Name, result.RouteValues["Controller"]);
            _authMock.Verify(x => x.Login(It.Is<Login>(o => o.UserName == "coda admin")));
            _authMock.Verify(x => x.Login(It.Is<Login>(o => o.Password == "2222")));
        }

        [TestMethod]
        public void TestLoginPostWithReturnUrlUsing()
        {
            // arrange
            string urlLogin = "/" + MVC.Account.Name + "/" + MVC.Account.ActionNames.Login;
            RouteAssert.GeneratesActionUrl(_routes, urlLogin, MVC.Account.ActionNames.Login, MVC.Account.Name);
            _accountController.Url = Tools.MockUrlHelper(urlLogin, _routes);
            string returnUrl = "/" + MVC.Account.Name + "/" + MVC.Account.ActionNames.UserProfile;
            // act
            var result = _accountController.Login(_loginData, returnUrl) as RedirectResult;
            // assert
            Assert.AreEqual(returnUrl, result.Url);
            _authMock.Verify(x => x.Login(It.Is<Login>(o => o.UserName == _loginData.UserName)));
            _authMock.Verify(x => x.Login(It.Is<Login>(o => o.Password == _loginData.Password)));
        }

        [TestMethod]
        public void TestLoginPostInvalid()
        {
            // arrange
            string urlLogin = "/" + MVC.Account.Name + "/" + MVC.Account.ActionNames.Login;
            // act
            _authMock.Setup(x => x.Login(_loginData)).Returns(false);
            ViewResult result = _accountController.Login(_loginData, null) as ViewResult;
            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ConstantDocument.ErrorLogin, result.ViewData.ModelState[""].Errors.First().ErrorMessage);
            RouteAssert.HasRoute(_routes, urlLogin);
            RouteAssert.GeneratesActionUrl(_routes, urlLogin, MVC.Account.ActionNames.Login, MVC.Account.Name);
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

            // arrange auth mock
            _authMock = new Mock<IAuthenticationProvider>();
            _authMock.Setup(x => x.SignOut());
            // data
            _loginData = new Login()
             {
                 UserName = "coda admin",
                 Password = "2222",
             };
            _authMock.Setup(x => x.Login(_loginData)).Returns(true);
            _registerData = new Register()
            {
                UserName = "test user",
                Password = "1234",
                PasswordConfirm = "1234"
            };
            _authMock.Setup(x => x.Register(_registerData)).Returns(string.Empty);

            // arrange account controller
            _uowMock = new Mock<IUow>();
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