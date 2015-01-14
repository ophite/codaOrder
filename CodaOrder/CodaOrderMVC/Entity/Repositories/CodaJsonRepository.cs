using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication3.Entity
{
    public class CodaJsonRepository: BaseRepository<CodaJson>, ICodaJsonRepository
    {
        //public CodaJsonRepository() : base() { }
        public CodaJsonRepository(DbContext dbContext) : base(dbContext) { }

        public string FindSubject(string searchText)
        {
            string json = string.Empty;
            using (var db = new codaJournal())
            {
                string className = "Subject";
                string fieldName = string.Empty;
                long parentID = 8000000000180;
                searchText = searchText ?? string.Empty;
                var results = ((codaJournal)this.dbContext).FindObject(className, fieldName, parentID, searchText, false, 10, null);
                json = JsonConvert.SerializeObject(results);

                // not array
                //Dictionary<string, string> dict = new Dictionary<string, string>();
                //dict.Add("data", json);
                //json = JsonConvert.SerializeObject(dict);
            }

            return json;
        }
    }
}