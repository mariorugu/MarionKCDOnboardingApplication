using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class UserRepository : Repository<KCDUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        { }
        
        public void ApproveListOfUsers(List<KCDUser> users, bool approve)
        {
            foreach (var user in users)
            {
                user.IsActive = approve;
            }
        }

        public void DisableListOfUsers(List<KCDUser> users)
        {
            foreach (var user in users)
            {
                user.IsActive = false;
            }
        }

       
        public KCDUser GetUserByEmail(string email)
        {
            var user =  _appContext.KcdUsers.Where(x=> x.Email == email);
            return user.SingleOrDefault();
        }

        public KCDUser GetUser(string id)
        {
            var user =  _appContext.KcdUsers.Where(x=> x.Id == id);
            return user.SingleOrDefault();
        }
        
        public KCDUser GetUserNoTracking(string id)
        {
            var user =  _appContext.KcdUsers.Where(x=> x.Id == id).AsNoTracking();
            return user.SingleOrDefault();
        }

        public IEnumerable<KCDUser> GetUsers(IEnumerable<string> ids)
        {
            return ids.Select(id => _appContext.KcdUsers.SingleOrDefault(x => x.Id == id)).ToList();
        }

        public bool IsNewUser(string email)
        {
            var user =  _appContext.KcdUsers.Where(x=> x.Email == email);
            return user.Any();
        }


        public IEnumerable<KCDUser> GetActiveUsers()
        {
            return _appContext.KcdUsers.Where(x=> x.IsActive)
                .AsSingleQuery()
                .ToList();
        }

        public IEnumerable<KCDUser> GetInctiveUsers()
        {
            return _appContext.KcdUsers.Where(x=> x.IsActive == false)
                .AsSingleQuery()
                .ToList();
        }

        public void RemoveUser(string id)
        {
            var user = GetUser(id);
            _appContext.KcdUsers.Remove(user);
            _context.SaveChanges();
        }

        public IList<KCDUser> GetAllUsers()
        {
            return _appContext.KcdUsers
                .AsSingleQuery()
                .OrderBy(c => c.FirstName)
                .ToList();
        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}