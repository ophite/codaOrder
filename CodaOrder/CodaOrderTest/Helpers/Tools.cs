using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodaOrderTest.MockContext
{
    public class Tools
    {
        public static UrlHelper MockUrlHelper(string url, RouteCollection routes)
        {
            // 1 by MvcRouteTester
            HttpContextBase httpContextBase = MvcRouteTester.HttpMocking.HttpMockery.ContextForUrl(url);
            RequestContext requestContext = new RequestContext(httpContextBase, new RouteData());
            return new UrlHelper(requestContext, routes);

            // 2 by Mock
            //var httpContext = new Mock<HttpContextBase>();
            //var request = new Mock<HttpRequestBase>();
            //httpContext.Setup(x => x.Request).Returns(request.Object);
            ////request.Setup(x => x.Url).Returns(new Uri(url));
            //var requestContext = new RequestContext(httpContext.Object, new RouteData());
            //return new UrlHelper(requestContext, routes);
        }
    }
}