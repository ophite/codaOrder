using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication3.Controllers;
using Moq;
using WebApplication3.Entity;
//using Microsoft.VisualStudio.TestTools.UITest.Common.Service;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using WebApplication3.Infrastructure;
using Assert = NUnit.Framework.Assert;
using WebApplication3.Models;
using System.Linq;
using WebApplication3;
using System.Collections.Specialized;
using CodaOrderTest.MockContext;
using MvcRouteTester;
using NUnit.Framework;

namespace CodaOrderTest
{
    [TestClass]
    public class AccountControllerTest
    {
        #region Properties

        private Mock<IUow> _uowMock;
        private Mock<IAuthenticationProvider> _authMock;
        private AccountController _accountController;
        private Login _loginData;
        private RouteCollection routes;

        #endregion
        #region Logout

        [TestMethod]
        public void TestLogOut()
        {
            var result = _accountController.Logout() as RedirectToRouteResult;
            Assert.NotNull(result);
            Assert.AreEqual(MVC.Account.ActionNames.Login, result.RouteValues["Action"]);
            Assert.AreEqual(MVC.Account.Name, result.RouteValues["Controller"]);
            _authMock.Verify(x => x.SignOut(), Times.Once);
        }

        [TestMethod]
        public void TestLogin()
        {
            ViewResult result = _accountController.Login() as ViewResult;
            Assert.IsNotNull(result, "Should return view result from Login");
        }

        [TestMethod]
        public void TestLoginPost()
        {
            var result = _accountController.Login(_loginData, null) as RedirectToRouteResult;
            Assert.AreEqual(MVC.Document.ActionNames.Index, result.RouteValues["Action"]);
            Assert.AreEqual(MVC.Document.Name, result.RouteValues["Controller"]);
            _authMock.Verify(x => x.Login(It.Is<Login>(o => o.UserName == "coda admin")));
            _authMock.Verify(x => x.Login(It.Is<Login>(o => o.Password == "2222")));
        }

        [TestMethod]
        public void TestLoginPostInvalid()
        {
            _authMock.Setup(x => x.Login(_loginData)).Returns(false);
            ViewResult result = _accountController.Login(_loginData, null) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("User name or password is invalid", result.ViewData.ModelState[""].Errors.First().ErrorMessage);

            RouteAssert.HasRoute(routes, "/Account/Login");
            RouteAssert.GeneratesActionUrl(routes, "/Account/Login", "Login", "Account");
        }

        #endregion
        #region Init

        [TestInitialize]
        public void Init()
        {
            routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteAssert.UseAssertEngine(new NunitAssertEngine());

            _uowMock = new Mock<IUow>();
            _authMock = new Mock<IAuthenticationProvider>();
            _authMock.Setup(x => x.SignOut());

            _loginData = new Login()
             {
                 UserName = "coda admin",
                 Password = "2222",
             };
            _authMock.Setup(x => x.Login(_loginData)).Returns(true);
            _accountController = new AccountController(_uowMock.Object, _authMock.Object);
        }

        [TestCleanup]
        public void Dispose()
        {
            _uowMock = null;
            _accountController.Dispose();
            _accountController = null;
        }

        #endregion
    }
}