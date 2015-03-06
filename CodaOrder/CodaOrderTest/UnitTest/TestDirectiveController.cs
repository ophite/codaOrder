using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iOrder.Controllers;
using System.Web.Routing;
using CodaOrderTest.MockContext;
using iOrder;
using MvcRouteTester;
using System.Web.Mvc;

namespace CodaOrderTest.UnitTest
{
    [TestClass]
    public class TestDirectiveController
    {
        #region Properties

        private DirectiveController _directiveController;
        private RouteCollection _routes;

        #endregion
        #region Methods

        [TestMethod]
        public void TestDirDatesValidation()
        {
            // act
            var result = _directiveController.DirDatesValidation() as PartialViewResult;
            // assert
            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        public void TestDirShowError()
        {
            // act
            var result = _directiveController.DirShowError() as PartialViewResult;
            // assert
            Assert.IsNotNull(result);
        }

        #endregion
        #region Init

        [TestInitialize]
        public void Init()
        {
            _directiveController = new DirectiveController();

            // arrange route assert
            _routes = new RouteCollection();
            RouteConfig.RegisterRoutes(_routes);
            RouteAssert.UseAssertEngine(new NunitAssertEngine());
        }

        #endregion
    }
}