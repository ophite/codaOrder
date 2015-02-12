using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Entity.Interfaces;

namespace WebApplication3.Entity.Repositories
{
    public class AccountRepository : BaseRepository<JournalSale_Documents>, IAccountRepository
    {
        public AccountRepository(DbContext dbContext) : base(dbContext) { }

        public SqlResult GetUserProfile(string userName)
        {
            SqlResult result = new SqlResult();
            var userProfile = new
            {
                info = new Dictionary<string, string>() {
                    {"last_name", "Kobernik"},
                    {"first_name", "Yura"},
                    {"phone", "0681991555"},
                    {"email", "ophite@ukr.net"},
                },
                firms = new Dictionary<int, string>() { 
                    { 1, "stolitsa"} ,
                    { 2, "mova" } ,
                    { 3, "karpatu" } 
                },
                subjects = new Dictionary<int, Dictionary<int, string>>() { 
                    { 
                        1, 
                        new Dictionary<int, string>() { 
                            {1, "stolitsa subject 1"},
                            {2, "stolitsa subject 2"},
                            {3, "stolitsa subject 3"},
                        }
                    },
                    { 
                        2, 
                        new Dictionary<int, string>() { 
                            {4, "mova subject 1"},
                            {5, "mova subject 2"},
                            {6, "mova subject 3"},
                        }
                    },
                    { 
                        3, 
                        new Dictionary<int, string>() { 
                            {7, "karpatu subject 1"},
                            {8, "karpatu subject 2"},
                            {9, "karpatu subject 3"},
                        }
                    },
                },
                defaultSubject = new Dictionary<int, int> { 
                    {1, 2},
                    {2, 3},
                    {3, 1},
                }
            };

            result.Result = userProfile;
            return result;
        }
    }
}