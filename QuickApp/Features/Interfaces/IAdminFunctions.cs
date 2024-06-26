using System.Collections.Generic;
using System.Threading.Tasks;
using QuickApp.ViewModels;

namespace QuickApp.Features.Interfaces;

public interface IAdminFunctions
{
     Task Approval(string id, KCDUserViewModel user, bool approve);
     Task ApprovalForListOfUsers(string id, List<string> userIds, bool approve);
     Task ApproveUsingUserId(string id, string userId, bool approve);
     Task RemoveUser(string id, string userId);
}