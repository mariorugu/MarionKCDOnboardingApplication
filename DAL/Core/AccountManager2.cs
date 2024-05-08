using System.Linq;
using System.Threading.Tasks;
using DAL.Core.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Core;

public class AccountManager2 : IAccountManager2
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ApplicationDbContext _context;
    public AccountManager2(
        ApplicationDbContext context,
        IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        //_context.CurrentUserId = httpAccessor.HttpContext?.User.FindFirst(ClaimConstants.Subject)?.Value?.Trim();
    }
    
    public async Task<KCDUser> GetUsersAsync(string userId)
    {
        var user = await _context.KcdUsers
            .Where(u => u.Id == userId)
            .SingleOrDefaultAsync();
        return user;
    }

    public void ApproveUser(KCDUser user)
    {
        user.IsActive = true;
        user.Role = Role.User;
    }
}