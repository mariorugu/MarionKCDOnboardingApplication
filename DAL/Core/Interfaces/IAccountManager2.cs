using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Core.Interfaces;

public interface IAccountManager2
{
        Task<KCDUser> GetUsersAsync(string userId);

        void ApproveUser(KCDUser user);
        /* Task<bool> CheckPasswordAsync(ApplicationUser user, string password);




         Task<(bool Succeeded, string[] Errors)> CreateUserAsync(ApplicationUser user, string password);
         Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(ApplicationUser user);
         Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(string userId);
         Task<KCDUser> GetUserByEmailAsync(string email);
         Task<KCDUser> GetUserByIdAsync(string userId);
         Task<KCDUser> GetUserByUserNameAsync(string userName);
         Task<IList<string>> GetUserAsync(KCDUser user);
         Task<(bool Succeeded, string[] Errors)> ResetPasswordAsync(KCDUser user, string newPassword);
         Task<(bool Succeeded, string[] Errors)> UpdatePasswordAsync(KCDUser user, string currentPassword, string newPassword);
         Task<(bool Succeeded, string[] Errors)> UpdateRoleAsync(ApplicationRole role, IEnumerable<string> claims);
         Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(ApplicationUser user);
         Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(ApplicationUser user, IEnumerable<string> roles);*/
}