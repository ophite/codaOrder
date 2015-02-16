using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Entity.Repositories;
using WebApplication3.Models;

namespace WebApplication3.Entity.Interfaces
{
    public interface IAccountRepository : IRepository<UserProfile>
    {
        UserProfile GetUserByEmail(string email);
        SqlResult GetCodaUserProfile(string userName);
    }
}