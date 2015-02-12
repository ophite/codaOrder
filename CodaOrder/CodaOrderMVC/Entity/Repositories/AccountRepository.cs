using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Entity.Interfaces;

namespace WebApplication3.Entity.Repositories
{
    public class Profile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class DirectoryProfile
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class FirmProfile
    {
        public List<DirectoryProfile> Subjects { get; set; }
        public int DefaultSubject { get; set; }
        public DirectoryProfile Profile { get; set; }
    }

    public class UserProfile
    {
        public Profile Profile { get; set; }
        public List<FirmProfile> Firms { get; set; }
    }

    public class AccountRepository : BaseRepository<JournalSale_Documents>, IAccountRepository
    {
        public AccountRepository(DbContext dbContext) : base(dbContext) { }

        public SqlResult GetUserProfile(string userName)
        {
            SqlResult result = new SqlResult();
            
            UserProfile uProfile = new UserProfile();
            uProfile.Profile = new Profile() { LastName = "Kobernik", FirstName = "Yura", Email = "ophite@ukr.net", Phone = "0681991555" };

            // firm 1 
            FirmProfile firmStolitsa = new FirmProfile() { Profile = new DirectoryProfile() { ID = 10, Name = "Stolitsa" } };
            firmStolitsa.Subjects = new List<DirectoryProfile>()
            {
                new DirectoryProfile() {ID = 1, Name = "stolitsa subject 1"},
                new DirectoryProfile() {ID = 2, Name = "stolitsa subject 2"},
                new DirectoryProfile() {ID = 3, Name = "stolitsa subject 3"},
            };
            // firm 2
            FirmProfile firmKarpatu = new FirmProfile() { Profile = new DirectoryProfile() { ID = 20, Name = "Karpatu" } };
            firmKarpatu.Subjects = new List<DirectoryProfile>()
            {
                new DirectoryProfile() {ID = 4, Name = "Karpatu subject 1"},
                new DirectoryProfile() {ID = 5, Name = "Karpatu subject 2"},
                new DirectoryProfile() {ID = 6, Name = "Karpatu subject 3"},
            };
            // firm 3 
            FirmProfile firmMova = new FirmProfile() { Profile = new DirectoryProfile() { ID = 30, Name = "Mova" } };
            firmMova.Subjects = new List<DirectoryProfile>()
            {
                new DirectoryProfile() {ID = 7, Name = "Mova subject 1"},
                new DirectoryProfile() {ID = 8, Name = "Mova subject 2"},
                new DirectoryProfile() {ID = 9, Name = "Mova subject 3"},
            };
            uProfile.Firms = new List<FirmProfile>() { firmStolitsa, firmKarpatu, firmMova };

            result.Result = uProfile;
            //result.Result = JsonConvert.SerializeObject(uProfile);
            return result;
        }
    }
}