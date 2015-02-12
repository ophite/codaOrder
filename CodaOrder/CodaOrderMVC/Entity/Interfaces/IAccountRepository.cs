using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Entity.Repositories;

namespace WebApplication3.Entity.Interfaces
{
    public interface IAccountRepository : IRepository<JournalSale_Documents>
    {
        SqlResult GetUserProfile(string userName);
    }
}