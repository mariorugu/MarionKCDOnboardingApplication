using System;
using System.Collections.Generic;
using System.Linq;
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
        ValidateEmployee(employee);

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
    
    public async Task ApprovalForListOfUsers(string id, List<string> userIds, bool approve)
    {
        var employee = GetEmployee(id);
        ValidateEmployee(employee);

        // account to approve - should get userId instead correct later
        // accounts to approve
        var users =  _unitOfWork.Users.GetUsers(userIds);
        if (users.Any() == false)
        {
            throw new NullReferenceException(nameof(KCDUser));
        }

        // approve the new user by setting it to active, check based on some business requirements
        _unitOfWork.Users.ApproveListOfUsers(users.ToList());
        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveUser(string id, string userId)
    {
        var employee = GetEmployee(id);
        ValidateEmployee(employee);
        // perharps check if a user is active before removing 
        _unitOfWork.Users.RemoveUser(userId);
    }
    
    #region PrivateMethods
    private Employee GetEmployee(string id)
    {
        var employee =  _unitOfWork.Employees.GetEmployee(id);
        if (employee != null) 
            return employee;
        return null;
    }
    
    private void ValidateEmployee(Employee employee)
    {
        if (employee == null)
        {
            throw new NullReferenceException(nameof(Employee));
        }

        // check if user is an administrator and can approve new users
        if (!employee.IsAdministrator)
        {
            throw new Exception("Employee does not have the authorization to perform this task");
        }
    }
    #endregion
}