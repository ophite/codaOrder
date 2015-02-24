using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        /// <summary>
        /// Mock UrlHelper for controller
        /// </summary>
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

        /// <summary>
        /// Mock TryUpdateModel method for controller, return ModelState for testing IsValid behaviour
        /// </summary> 
        public static ModelStateDictionary MockTryUpdateModel<TModel>(TModel model, IValueProvider valueProvider)
            where TModel : class
        {
            var modelState = new ModelStateDictionary();
            var controllerContext = new ControllerContext();

            var bindingContext = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, typeof(TModel)),
                ModelState = modelState,
                ValueProvider = valueProvider
            };

            var binder = ModelBinders.Binders.GetBinder(typeof(TModel));
            binder.BindModel(controllerContext, bindingContext);

            return modelState;
        }

        /// <summary>
        /// Mock validation of models throw annotation
        /// </summary>
        public static IList<ValidationResult> MockValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}