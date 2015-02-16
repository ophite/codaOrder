using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Entity.Interfaces;
using WebApplication3.Models;

namespace WebApplication3.Entity.Repositories
{
    public class AccountRepository : BaseRepository<UserProfile>, IAccountRepository
    {
        public AccountRepository(DbContext dbContext) : base(dbContext) { }

        public UserProfile GetUserByEmail(string email)
        {
            UserProfile userProfile = ((IdentityContext)this.dbContext).UserProfiles.Where(i => i.EmailAddress == email).FirstOrDefault();
            return userProfile;
        }

        public SqlResult GetCodaUserProfile(string userName)
        {
            SqlResult result = new SqlResult();

            // firm 1 
            FirmProfile firmStolitsa = new FirmProfile() { Profile = new DirectoryProfile() { ID = 10, Name = "Stolitsa" } };
            firmStolitsa.Subjects = new List<DirectoryProfile>()
            {
                new DirectoryProfile() {ID = 1, Name = "stolitsa subject 1"},
                new DirectoryProfile() {ID = 2, Name = "stolitsa subject 2"},
                new DirectoryProfile() {ID = 3, Name = "stolitsa subject 3"},
            };
            firmStolitsa.DefaultSubject = firmStolitsa.Subjects[0];
            // firm 2
            FirmProfile firmKarpatu = new FirmProfile() { Profile = new DirectoryProfile() { ID = 20, Name = "Karpatu" } };
            firmKarpatu.Subjects = new List<DirectoryProfile>()
            {
                new DirectoryProfile() {ID = 4, Name = "Karpatu subject 1"},
                new DirectoryProfile() {ID = 5, Name = "Karpatu subject 2"},
                new DirectoryProfile() {ID = 6, Name = "Karpatu subject 3"},
            };
            firmKarpatu.DefaultSubject = firmKarpatu.Subjects[1];
            // firm 3 
            FirmProfile firmMova = new FirmProfile() { Profile = new DirectoryProfile() { ID = 30, Name = "Mova" } };
            firmMova.Subjects = new List<DirectoryProfile>()
            {
                new DirectoryProfile() {ID = 7, Name = "Mova subject 1"},
                new DirectoryProfile() {ID = 8, Name = "Mova subject 2"},
                new DirectoryProfile() {ID = 9, Name = "Mova subject 3"},
            };
            firmMova.DefaultSubject = firmMova.Subjects[2];
            // profile
            CodaUserProfile uProfile = new CodaUserProfile();
            uProfile.Profile = new Profile()
            {
                LastName = "Kobernik",
                FirstName = "Yura",
                Email = "ophite@ukr.net",
                Phone = "0681991555",
                Name = "Profile",
                DefaultNode = firmKarpatu.Profile.Name
            };
            uProfile.Profile.Nodes = new List<string>() { firmKarpatu.Profile.Name, firmMova.Profile.Name, firmStolitsa.Profile.Name };
            uProfile.Firms = new List<FirmProfile>() { firmStolitsa, firmKarpatu, firmMova };

            result.Result = uProfile;
            return result;
        }
    }
}