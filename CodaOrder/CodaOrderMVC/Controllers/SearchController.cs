using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Entity;

namespace WebApplication3.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public string Find(string searchText)
        {
            string json = string.Empty;
            using (var db = new codaJournal())
            {
                string className = "Subject";
                string fieldName = string.Empty;
                long parentID = 8000000000180;
                searchText = searchText ?? string.Empty;
                var results = db.FindObject(className, fieldName, parentID, searchText, false, 10, null);
                json = JsonConvert.SerializeObject(results);
                //Dictionary<string, string> dict = new Dictionary<string, string>();
                //dict.Add("data", json);
                //json = JsonConvert.SerializeObject(dict);
            }

            return json;
        }
    }
}