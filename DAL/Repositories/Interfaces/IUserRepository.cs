using System.Collections.Generic;
using DAL.Models;

namespace DAL.Repositories.Interfaces;

public interface IUserRepository
{
    KCDUser GetUserByEmail(string email);
    KCDUser GetUser(string id);
    bool IsNewUser(string email);
    IEnumerable<KCDUser> GetUsers(IEnumerable<string> ids);
    IEnumerable<KCDUser> GetActiveUsers();
    IList<KCDUser> GetAllUsers();
    KCDUser GetUserNoTracking(string id);
    void ApproveListOfUsers(List<KCDUser> users, bool approve);
     void DisableListOfUsers(List<KCDUser> users);
    IEnumerable<KCDUser> GetInctiveUsers();
    void RemoveUser(string id);

}