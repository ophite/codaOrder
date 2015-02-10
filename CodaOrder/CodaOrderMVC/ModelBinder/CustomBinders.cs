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
    public class LineBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext modelContext)
        {
            HttpRequestBase req = controllerContext.HttpContext.Request;
            LinesViewModel item = new LinesViewModel(); 
            var lines = req["0"];
            if (lines == null)
                return item;

            DocTradeLine[] tradeLines = JsonConvert.DeserializeObject<DocTradeLine[]>(lines);
            item.lines = tradeLines.ToArray();
            return item;
        }
    }
}