using iOrder.Entity.Repositories;
using iOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iOrder.Entity.Interfaces
{
    public interface IAccountRepository : IRepository<UserProfile>
    {
        UserProfile GetUserByEmail(string email);
        SqlResult GetCodaUserProfile(string userName);
    }
}