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
using iOrder.Controllers;
using iOrder.Entity;
using iOrder.Infrastructure;
using iOrder.Models;
using iOrder;
using iOrder.Helpers;
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
using iOrder.Entity.Interfaces;
using iOrder.Entity.Repositories;
using iOrder.Infrastructure.Entity;

namespace CodaOrderTest.UnitTest
{
    [TestClass]
    public class TestDocumentController
    {
        #region Properties

        private Mock<IUow> _uowMock;
        private Mock<IDocumentRepository> _documentRepositoryMock;

        private DocumentController _documentController;
        private RouteCollection _routes;

        private DocumentsParamsViewModel _documentsParamsViewModel;
        //private LinesViewModel _linesViewModel;

        #endregion
        #region Methods

        [TestMethod]
        public void Index()
        {
            // act
            var result = _documentController.Index() as ViewResult;
            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Documents()
        {
            // act
            var result = _documentController.Documents() as PartialViewResult;
            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Lines()
        {
            // act
            var result = _documentController.Lines() as PartialViewResult;
            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Verify_Method_Is_Decorated_With_Authorize_Attribute()
        {
            var type = _documentController.GetType();
            Dictionary<string, Type[]> methodsInfo = new Dictionary<string, Type[]>()
            {
                {MVC.Document.ActionNames.Index, new Type[] {}},
                {MVC.Document.ActionNames.Documents, new Type[] {}},
                {MVC.Document.ActionNames.Lines, new Type[] {}},
                {MVC.Document.ActionNames.NewOrder, new Type[] {}},
                {MVC.Document.ActionNames.OrdersDraft, new Type[] {}},
                {MVC.Document.ActionNames.OrdersHistory, new Type[] {}},
                {MVC.Document.ActionNames.GetDocuments, new Type[] {typeof(DocumentsParamsViewModel)}},
                {MVC.Document.ActionNames.GetLines, new Type[] {typeof(string)}},
                {MVC.Document.ActionNames.SaveLines, new Type[] {typeof(LinesViewModel)}},
            };

            Func<string, Type[], string> assert = (string name, Type[] types) =>
            {
                var methodInfoLocal = type.GetMethod(name, types);
                var attributesLocal = methodInfoLocal.GetCustomAttributes(typeof(AuthorizeAttribute), true);
                Assert.IsTrue(attributesLocal.Any(), string.Format("No AuthorizeAttribute found on {0}() method", name));

                return string.Empty;
            };

            foreach (string methodName in methodsInfo.Keys)
                assert(methodName, methodsInfo[methodName]);
        }

        [TestMethod]
        public void NewOrder()
        {
            // act
            var result = _documentController.NewOrder() as PartialViewResult;
            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OrdersDraft()
        {
            // act
            var result = _documentController.OrdersDraft() as PartialViewResult;
            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OrdersHistory()
        {
            // act
            var result = _documentController.OrdersHistory() as PartialViewResult;
            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDocuments()
        {
            // arrange
            _documentsParamsViewModel = new DocumentsParamsViewModel()
            {

            };
            // act
            SqlResult sqlResult = new SqlResult();
            _documentRepositoryMock.Setup(x => x.GetDocumentsJson(_documentsParamsViewModel)).Returns(sqlResult);
            var result = _documentController.GetDocuments() as JsonResult;
            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLines()
        {
            // arrange
            string documentID = "1";
            SqlResult sqlResult = new SqlResult();
            // act
            _documentRepositoryMock.Setup(x => x.GetLinesJson(documentID)).Returns(sqlResult);
            var result = _documentController.GetLines() as JsonResult;
            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SaveLines()
        {
            // arrange
            DocTradeLine[] lines = new DocTradeLine[] { };
            SqlResult sqlResult = new SqlResult();
            // act
            _documentRepositoryMock.Setup(x => x.UpdateLines(lines)).Returns(sqlResult);
            var result = _documentController.SaveLines() as JsonResult;
            // assert
            Assert.IsNotNull(result);
        }

        #endregion
        #region Init

        [TestInitialize]
        public void Init()
        {
            // arrange document mock
            _documentRepositoryMock = new Mock<IDocumentRepository>();

            // arrange account controller
            _uowMock = new Mock<IUow>();
            _uowMock.Setup(x => x.DocumentRepository).Returns(_documentRepositoryMock.Object);
            _documentController = new DocumentController(_uowMock.Object);

            // arrange route assert
            _routes = new RouteCollection();
            RouteConfig.RegisterRoutes(_routes);
            RouteAssert.UseAssertEngine(new NunitAssertEngine());
        }

        #endregion
    }
}