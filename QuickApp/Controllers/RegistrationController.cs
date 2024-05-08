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

        public RegistrationController(IMapper mapper, IUnitOfWork unitOfWork, ApplicationDbContext context,
            IPasswordService passwordService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
            _passwordService = passwordService;
        }

        #region Private methods

        // GET: api/values
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

        #endregion

        #region Private methods

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
                userModel = _mapper.Map<KCDUser>(user);
                // encrypt password 
                var password = _passwordService.SetPassword(userModel.Password);
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

            return Ok($"User {userModel.Id}, {userModel.FirstName } {userModel.LastName }has been saved pending approval from administration");
        }

        #endregion


        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}