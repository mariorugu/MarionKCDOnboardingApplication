using System;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using QuickApp.Features.Interfaces;
using QuickApp.ViewModels;

namespace QuickApp.Features;

public class AdminFunctions : IAdminFunctions
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;

    public AdminFunctions(IUnitOfWork unitOfWork, ApplicationDbContext context)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Approval(string id, KCDUserViewModel user, bool approve)
    {
        var employee = GetEmployee(id);
        if (employee == null)
        {
            throw new NullReferenceException(nameof(Employee));
        }

        // check if user is an administrator and can approve new users
        if (!employee.IsAdministrator)
        {
            throw new Exception("Employee does not have the authorization to perform this task");
        }

        // account to approve - should get userId instead correct later
        var userToApprove = _unitOfWork.Users.GetUserByEmail(user.Email);
        if (userToApprove == null)
        {
            throw new NullReferenceException(nameof(KCDUser));
        }

        // approve the new user by setting it to active, check based on some business requirements
        userToApprove.IsActive = approve;
        await _context.SaveChangesAsync();
    }
    
    #region PrivateMethods
    private Employee GetEmployee(string id)
    {
        var employee =  _unitOfWork.Employees.GetEmployee(id);
        if (employee != null) 
            return employee;
        return null;
    }
    #endregion
}