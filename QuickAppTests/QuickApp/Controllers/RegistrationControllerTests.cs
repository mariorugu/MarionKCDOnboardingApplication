using AutoMapper;
using DAL;
using DAL.Core.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using QuickApp.Controllers;
using QuickApp.ViewModels;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace QuickAppTests.QuickApp.Controllers;

public class RegistrationControllerTests
{
    private Mock<IMapper> _mapperMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private ApplicationDbContext _context;
    private Mock<IPasswordService> _passwordServiceMock;
    private const string password = "password1234";
    private string encryptedPassword = "encryptedPassword";
    private KCDUser user;
    private List<KCDUser> users;
    private KCDUserViewModel userViewModel;
    private List<KCDUserViewModel> userViewModels;

    private RegistrationController _registrationController;

    [SetUp]
    public void Setup()
    {
        users = new List<KCDUser>();
        user = new KCDUser
        {
            Id = "41F481AF-21A4-44C7-B4AC-1CF43E7760E3",
            FirstName = "Matthew",
            MiddleName = "",
            LastName = "Llyod",
            Email = "mariorugu@lalandi.com",
            PhoneNumber = "06051465869",
            Password = "cGFzc3dvcmQxMjM0",
            Country = "Netherlands",
            Company = "Lalandi",
            IsActive = false
        };
        users.Add(user);
        
        userViewModels = new List<KCDUserViewModel>();
        userViewModel = new KCDUserViewModel
        {
            FirstName = "Matthew",
            MiddleName = "",
            LastName = "Llyod",
            Email = "mariorugu@lalandi.com",
            PhoneNumber = "06051465869",
            Password = password,
            Country = "Netherlands",
            Company = "Lalandi",
        };
        userViewModels.Add(userViewModel);
        
        _passwordServiceMock = new Mock<IPasswordService>();
        _mapperMock = new Mock<IMapper>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _passwordServiceMock = new Mock<IPasswordService>();

        _passwordServiceMock.Setup(p => p.SetPassword(password)).Returns(encryptedPassword);
        _unitOfWorkMock.Setup(p => p.Users.GetAllUsers()).Returns(users);
        _unitOfWorkMock.Setup(p => p.Users.IsNewUser(userViewModel.Email)).Returns(false);

        _mapperMock.Setup(m => m.Map<KCDUserViewModel>(user)).Returns(() => userViewModel);

       
        _registrationController = new RegistrationController(_mapperMock.Object, _unitOfWorkMock.Object, _context,
            _passwordServiceMock.Object);
    }

    [Test]
    public void GetUsers()
    {
        // Act
        var result = _registrationController.GetUsers() as OkObjectResult;
        var modelResults = (List<KCDUserViewModel>)result?.Value!;
        var modelResult = modelResults.First();
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(modelResult, Is.Not.Null);
            Assert.That(userViewModel.FirstName, Is.EqualTo(modelResult.FirstName));
            Assert.That(userViewModel.MiddleName, Is.EqualTo(modelResult.MiddleName));
            Assert.That(userViewModel.LastName, Is.EqualTo(modelResult.LastName));
            Assert.That(userViewModel.PhoneNumber, Is.EqualTo(modelResult.PhoneNumber));
            Assert.That(userViewModel.Password, Is.EqualTo(modelResult.Password));
            Assert.That(userViewModel.Country, Is.EqualTo(modelResult.Country));
            Assert.That(userViewModel.Company, Is.EqualTo(modelResult.Company));
            Assert.That(userViewModel.Email, Is.EqualTo(modelResult.Email));
        });
    }
    
    [Test]
    public async Task  RegisterUserFailsWhenUserModelNull()
    {
        // Act
        var result = await _registrationController.RegisterUser(null) as BadRequestObjectResult;

        // Assert
        Assert.That(result.Value, Is.EqualTo("user cannot be null"));
    }
    
    [Test]
    public async Task  RegisterUserFailsWhenFirstNameIsMissingIsInvalid()
    {
        // Act
        userViewModel.FirstName = "";
        var result = await _registrationController.RegisterUser(userViewModel) as BadRequestObjectResult;
        var validationFailure = result.Value as List<ValidationFailure>;
        var error = validationFailure.First();
        // Assert
        Assert.That(error.ErrorMessage, Is.EqualTo("User first name cannot be empty"));
    }
    
    [Test]
    public async Task  RegisterUserFailsWhenEmailIsInvalid()
    {
        // Act
        userViewModel.Email = "wrongEmailFormat";
        var result = await _registrationController.RegisterUser(userViewModel) as BadRequestObjectResult;
        var validationFailure = result.Value as List<ValidationFailure>;
        var error = validationFailure.First();
        // Assert
        Assert.That(error.ErrorMessage, Is.EqualTo("A valid email is required"));
    }
    
    [Test]
    public async Task  RegisterUserWithEmailThatIsAlreadySaved()
    {
        // Act
        userViewModel.Email = "wrongEmailFormat";
        var result = await _registrationController.RegisterUser(userViewModel) as BadRequestObjectResult;
        var validationFailure = result.Value as List<ValidationFailure>;
        var error = validationFailure.First();
        // Assert
        Assert.That(error.ErrorMessage, Is.EqualTo("A valid email is required"));
    }

    [Test]
    public async Task  RegisterUserWithEmailAlreadyInDatabaseFails()
    {
        // Act
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _unitOfWorkMock.Setup(p => p.Users.IsNewUser(userViewModel.Email)).Returns(true);
        
        _registrationController = new RegistrationController(_mapperMock.Object, _unitOfWorkMock.Object, _context,
            _passwordServiceMock.Object);
        var result = await _registrationController.RegisterUser(userViewModel) as BadRequestObjectResult;
        
        // Assert
        Assert.That(result.StatusCode, Is.EqualTo(400));
    }
    
    [Test]
    public async Task RegisterUserSuccessfully()
    {
        // Act
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()                
            .Options;
        _context = new ApplicationDbContext(options);
        
        _mapperMock = new Mock<IMapper>();
        _mapperMock.Setup(m => m.Map<KCDUser>(userViewModel)).Returns(() => user);
        _passwordServiceMock.Setup(p => p.SetPassword(password)).Returns(encryptedPassword);

        _registrationController = new RegistrationController(_mapperMock.Object, _unitOfWorkMock.Object, _context,
            _passwordServiceMock.Object);
        
        var result = await _registrationController.RegisterUser(userViewModel) as OkObjectResult;
        
        // Assert
        Assert.That(result.StatusCode, Is.EqualTo(200));
        Assert.That(result.Value, Is.EqualTo($"User {user.FirstName } {user.LastName} has been saved pending approval from administration"));
        // Mock.Get(_context).Verify(x => x.Add(It.IsAny<KCDUser>()), Times.Once); -- if mocked to test that the user was stored
        //Mock.Get(_context).Verify(x => x.SaveChangesAsync(), Times.Once); -- if mocked to test that the user was saved to database
    }
}