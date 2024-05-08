using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Mvc;
using QuickApp.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using DAL.Core.Interfaces;
using DAL.Models;

namespace QuickApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;

        #region Constructor
        public RegistrationController(IMapper mapper, IUnitOfWork unitOfWork, ApplicationDbContext context,
            IPasswordService passwordService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
            _passwordService = passwordService;
        }
        #endregion

        #region Public methods

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var allUsers = _unitOfWork.Users.GetAllUsers();
            var users = allUsers.Select(user => _mapper.Map<KCDUserViewModel>(user)).ToList();
            return Ok(users);
        }

        [HttpPost("user")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> RegisterUser([FromBody] KCDUserViewModel user)
        {
            return await AddUser(user);
        }
        
        // api call for a user to update their own information 
        [HttpPut("user/update")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateUserDetails(string id, [FromBody] KCDUserViewModel model)
        {
            if (model == null)
                return BadRequest($"{nameof(model)} cannot be null");
            
            // validating model -- did not have time to abstract validation
            var validator = new KCDUserViewModelValidator();
            var validationResult = validator.Validate(model);

            if (validationResult.IsValid == false)
            {
                return BadRequest(validationResult.Errors);
            }
            
            var user =  _unitOfWork.Users.GetUser(id);
            
            // on front end the email part would be disabled but check to ensure no email change
            if (user.Email != model.Email)
            {
                return BadRequest("Change to email is not accepted");
            }
            var isPasswordNew = IsPasswordChanged(user.Password, model.Password);
            var updatedUser  = _mapper.Map<KCDUser>(model);
            if (isPasswordNew)
            {
                updatedUser.Password = _passwordService.SetPassword(model.Password);
                // encrypt new password 
            }

            updatedUser.Id = user.Id; //to get around my primary key issue -- should generate in appdbcontext not on model fix
            _context.Update(updatedUser);
            return Ok($"User {updatedUser.FirstName } {updatedUser.LastName } details have been updated");
        }

        #endregion

        #region Private methods

        private bool IsPasswordChanged(string storedPassword, string newPassword)
        {
            // decode stored password
            var oldPassword = _passwordService.GetPassword(storedPassword);
            return oldPassword != newPassword;
        }

        private void AddError(string error, string key = "")
        {
            ModelState.AddModelError(key, error);
        }

        private async Task<IActionResult> AddUser([FromBody] KCDUserViewModel user)
        {
            if (user == null)
                return BadRequest($"{nameof(user)} cannot be null");
            
            // validating model -- did not have time to abstract validation
            var validator = new KCDUserViewModelValidator();
            var validationResult = validator.Validate(user);

            if (validationResult.IsValid == false)
            {
                return BadRequest(validationResult.Errors);
            }

            KCDUser userModel;
            var isNewUser = _unitOfWork.Users.IsNewUser(user.Email);
            if (isNewUser == false)
            {
                // encrypt password 
                var password = _passwordService.SetPassword(user.Password);
                userModel = _mapper.Map<KCDUser>(user);
                
                userModel.Password = password;
                // store in database
                _context.KcdUsers.Add(userModel);
                
                await _context.SaveChangesAsync();
            }
            else
            {
                AddError("An account with this email already exists", "Email");
                return BadRequest(ModelState);
            }

            return Ok($"User {userModel.FirstName } {userModel.LastName} has been saved pending approval from administration");
        }

        #endregion
    }
}