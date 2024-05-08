using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickApp.Helpers;
using QuickApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Core.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace QuickApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountManagerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountManager _accountManager;
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly ILogger<AccountController> _logger;
      
        #region Constructor
        public AccountManagerController(IMapper mapper, IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        #endregion
        
        #region Public Methods
        [HttpGet("users/{id}")]
        [ProducesResponseType(200, Type = typeof(KCDUserViewModel))]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public IActionResult GetUserById(string id)
        {
            var user =  GetUserViewModelHelper(id);

            if (user != null)
                return Ok(user);
            return NotFound(id);
        }
        
        // api call to get all inactive users
        [HttpGet("users/inactive")]
        [ProducesResponseType(200, Type = typeof(KCDUserViewModel))]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public Task<IActionResult> GetInactiveUsers()
        {
            var users= _unitOfWork.Users.GetInctiveUsers();
            var userModelViews = users.Select(user => _mapper.Map<KCDUserViewModel>(user)).ToList();
            return Task.FromResult<IActionResult>(Ok(userModelViews));
            
        }
        
        // api call to activate/approve a given user that has already been saved admin is the assumed current user that should be an administrator
        [HttpPut("user/approve")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> ApproveUser([FromQuery]string id, [FromBody] KCDUserViewModel user)
        {
            // validate Request
            if (user == null)
                return BadRequest($"{nameof(user)} cannot be null");
            if (id.IsNullOrEmpty())
                return BadRequest($"{nameof(id)} cannot be null");
            var validator = new KCDUserViewModelValidator();
            var validationResult = validator.Validate(user);

            if (validationResult.IsValid == false)
            {
                return BadRequest(validationResult.Errors);
            }
            
            
            var employee = GetEmployee(id);
            if (employee == null)
            {
                AddError("Employee with this ID does not exist", "EmployeeId");
                return BadRequest(ModelState);
            }
            // check if user is an administrator and can approve new users
            if (!employee.IsAdministrator)
            {
                AddError("Employee does not have the authorization to perform this task", "EmployeeId");
                return BadRequest(ModelState);
            }
            // account to approve - should get userId instead correct later
            var userToApprove =  _unitOfWork.Users.GetUserByEmail(user.Email);
            if (userToApprove == null)
            {
                AddError("User with this ID does not exist", "EmployeeId");
                return BadRequest(ModelState);
            }

            // approve the new user by setting it to active, check based on some business requirements
            userToApprove.IsActive = true;
            await _context.SaveChangesAsync();
            return Ok($"User {userToApprove.FirstName } {userToApprove.LastName } has been approved/enabled to the KCD system");
        }
        
        // api call to deactivate a given user 
        [HttpPut("user/deactivate/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeactivateUser(string id, [FromBody] KCDUserViewModel user)
        {
            // checking for authorizing employee
            var employee = GetEmployee(id);
            if (employee == null)
            {
                AddError("Employee with this ID does not exist", "EmployeeId");
                return BadRequest(ModelState);
            }
            // check if user is an administrator and can approve new users
            if (!employee.IsAdministrator)
            {
                AddError("Employee does not have the authorization to perform this task", "EmployeeId");
                return BadRequest(ModelState);
            }
            // account to deactivate
            var userToApprove =  _unitOfWork.Users.GetUserByEmail(user.Email);
            if (userToApprove == null)
            {
                AddError("User with this ID does not exist", "EmployeeId");
                return BadRequest(ModelState);
            }

            // approve the new user by setting it to active, check based on some business requirements
            userToApprove.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok($"User {userToApprove.FirstName} has been disabled/disabled to the KCD system");
        }
        
        // api call to approve a list of new users
        [HttpPut("users/approve")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> ApproveListOfUsers(string id, List<string> userIds)
        {
            // checking for authorizing employee
            var employee = GetEmployee(id);
            if (employee == null)
            {
                AddError("Employee with this ID does not exist", "EmployeeId");
                return BadRequest(ModelState);
            }
            // check if user is an administrator and can approve new users
            if (!employee.IsAdministrator)
            {
                AddError("Employee does not have the authorization to perform this task", "EmployeeId");
                return BadRequest(ModelState);
            }
            // accounts to approve
            var users =  _unitOfWork.Users.GetUsers(userIds);
            if (users.Any() == false)
            {
                AddError("Users with these IDs do not exist", "EmployeeId");
                return BadRequest(ModelState);
            }

            // approve the new user by setting it to active, check based on some business requirements
            _unitOfWork.Users.ApproveListOfUsers(users.ToList());
            await _context.SaveChangesAsync();
            return Ok($"Users have been approved/enabled to the KCD system");
        }
        
        // api call to deactivate a list of new users
        [HttpPut("users/deactivate")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeactivateListOfUsers([FromQuery]string id,   [FromBody] List<string> userIds)
        {
            if (userIds.Any() == false)
            {
                return BadRequest("User Ids missing");
            }
            var employee = GetEmployee(id);
            if (employee == null)
            {
                AddError("Employee with this ID does not exist", "EmployeeId");
                return BadRequest(ModelState);
            }
            // check if user is an administrator and can approve new users
            if (!employee.IsAdministrator)
            {
                AddError("Employee does not have the authorization to perform this task", "EmployeeId");
                return BadRequest(ModelState);
            }
            // accounts to approve
            var users =  _unitOfWork.Users.GetUsers(userIds);
            if (users.Any() == false)
            {
                AddError("Users with these IDs do not exist", "EmployeeId");
                return BadRequest(ModelState);
            }

            // approve the new user by setting it to active, check based on some business requirements
            _unitOfWork.Users.DisableListOfUsers(users.ToList());
            await _context.SaveChangesAsync();
            return Ok($"Users have been disabled from the KCD system");
        }
        
        // api call to remove user
        [HttpDelete("users/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public Task<IActionResult> RemoveUser(string id, string userId)
        {
            var employee = GetEmployee(id);
            if (employee == null)
            {
                AddError("Employee with this ID does not exist", "EmployeeId");
                return Task.FromResult<IActionResult>(BadRequest(ModelState));
            }
            // check if user is an administrator and can approve new users
            if (!employee.IsAdministrator)
            {
                AddError("Employee does not have the authorization to perform this task", "EmployeeId");
                return Task.FromResult<IActionResult>(BadRequest(ModelState));
            }
            
            // perharps check if a user is active before removing 
            _unitOfWork.Users.RemoveUser(id);
            return Task.FromResult<IActionResult>(Ok($"User {id} has been removed from the KCD system"));
        }
        
        #endregion

        #region PrivateMethods

        private Employee GetEmployee(string id)
        {
            var employee =  _unitOfWork.Employees.GetEmployee(id);
            if (employee != null) 
                return employee;
            AddError("Employee with this ID does not exist", "EmployeeId");
            return null;
        }
        private KCDUserViewModel GetUserViewModelHelper(string userId)
        {
            var user =  _unitOfWork.Users.GetUser(userId);
            if (user == null)
                return null;

            var userViewModel = _mapper.Map<KCDUserViewModel>(user);
            return userViewModel;
        }

        private async Task<RoleViewModel> GetRoleViewModelHelper(string roleName)
        {
            var role = await _accountManager.GetRoleLoadRelatedAsync(roleName);
            if (role != null)
                return _mapper.Map<RoleViewModel>(role);

            return null;
        }

        private void AddError(IEnumerable<string> errors, string key = "")
        {
            foreach (var error in errors)
            {
                AddError(error, key);
            }
        }

        private void AddError(string error, string key = "")
        {
            ModelState.AddModelError(key, error);
        }
        #endregion
    }
}
