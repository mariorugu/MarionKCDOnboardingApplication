using System.Threading.Tasks;
using QuickApp.ViewModels;

namespace QuickApp.Features.Interfaces;

public interface IAdminFunctions
{
     Task Approval(string id, KCDUserViewModel user, bool approve);
}