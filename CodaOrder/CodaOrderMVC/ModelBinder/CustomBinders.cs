using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Entity;
using WebApplication3.Models;

namespace WebApplication3.ModelBinder
{
    public class LineBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext modelContext)
        {
            LinesViewModel item = new LinesViewModel();
            // send array from one element (our lines)
            ValueProviderResult result = modelContext.ValueProvider.GetValue("0");
            if (result == null || string.IsNullOrEmpty(result.AttemptedValue))
                return item;

            item.lines = JsonConvert.DeserializeObject<DocTradeLine[]>(result.AttemptedValue);
            return item;
        }
    }

    public class UserProfileBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext modelContext)
        {
            WebApplication3.Entity.Repositories.UserProfile item = new WebApplication3.Entity.Repositories.UserProfile();
            // send array from one element (our lines)
            ValueProviderResult result = modelContext.ValueProvider.GetValue("0");
            if (result == null || string.IsNullOrEmpty(result.AttemptedValue))
                return item;

            item = JsonConvert.DeserializeObject<WebApplication3.Entity.Repositories.UserProfile>(result.AttemptedValue);
            return item;
        }
    }
}