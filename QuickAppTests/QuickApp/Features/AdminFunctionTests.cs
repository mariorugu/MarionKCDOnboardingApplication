using Moq;
using DAL;
using DAL.Models;
using QuickApp.Features;
using QuickApp.ViewModels;
using Microsoft.EntityFrameworkCore;

[TestFixture]
public class AdminFunctionTests
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private ApplicationDbContext _context;
    private AdminFunctions _adminFunctions;
    private Employee _employee;
    private KCDUser _user;

    [SetUp]
    public void Setup()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;
        _context = new ApplicationDbContext(options);
        _employee = new Employee
        {
            Id = "testId", 
            IsAdministrator = true,
            Email = "test@test.com",
            IsActive = false,
            FirstName = "Marion",
            MiddleName = "",
            LastName = "Llyod",
            PhoneNumber = "06051465869",
            Password = "password",
            Country = "Netherlands",
            Company = "Lalandi",
        };
        _user = new KCDUser
        {
            Id = "id",
            Email = "test@test.com",
            IsActive = false,
            FirstName = "Matthew",
            MiddleName = "",
            LastName = "Llyod",
            PhoneNumber = "06051465869",
            Password = "password",
            Country = "Netherlands",
            Company = "Lalandi",
        };
        _context.KcdUsers.Add(_user);
        _context.Employees.Add(_employee);
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _adminFunctions = new AdminFunctions(_mockUnitOfWork.Object, _context);
    }

    [Test]
    public async Task NullReferenceExceptionThrownWhenEmployeeNotFound()
    {
        // Arrange
        var id = "testId";
        var user = new KCDUserViewModel
        {
            Email = "test@test.com", FirstName = "Matthew",
            MiddleName = "",
            LastName = "Llyod",
            PhoneNumber = "06051465869",
            Password = "password",
            Country = "Netherlands",
            Company = "Lalandi",
        };
        var approve = true;

        _employee = null;
        _mockUnitOfWork.Setup(u => u.Employees.GetEmployee(id)).Returns(_employee);
        _mockUnitOfWork.Setup(u => u.Users.GetUserByEmail(user.Email)).Returns(_user);

        // Assert
        Assert.ThrowsAsync<NullReferenceException>(() => _adminFunctions.Approval(id, user, approve));
    }

    [Test]
    public async Task ExceptionThrownWhenEmployeeNotAdministrator()
    {
        // Arrange
        var id = "testId";
        var user = new KCDUserViewModel
        {
            Email = "test@test.com", FirstName = "Matthew",
            MiddleName = "",
            LastName = "Llyod",
            PhoneNumber = "06051465869",
            Password = "password",
            Country = "Netherlands",
            Company = "Lalandi",
        };
        var approve = true;
        _employee.IsAdministrator = false;
        _mockUnitOfWork.Setup(u => u.Employees.GetEmployee(id)).Returns(_employee);
        _mockUnitOfWork.Setup(u => u.Users.GetUserByEmail(user.Email)).Returns(_user);

        // Assert
        Assert.ThrowsAsync<Exception>(() => _adminFunctions.Approval(id, user, approve));
    }

    [Test]
    public async Task Approval_ShouldSetUserToActive_WhenApproveIsTrue()
    {
        // Arrange
        var id = "testId";
        var user = new KCDUserViewModel
        {
            Email = "test@test.com", FirstName = "Matthew",
            MiddleName = "",
            LastName = "Llyod",
            PhoneNumber = "06051465869",
            Password = "password",
            Country = "Netherlands",
            Company = "Lalandi",
        };
        var approve = true;


        _mockUnitOfWork.Setup(u => u.Employees.GetEmployee(id)).Returns(_employee);
        _mockUnitOfWork.Setup(u => u.Users.GetUserByEmail(user.Email)).Returns(_user);

        // Act
        await _adminFunctions.Approval(id, user, approve);

        // Assert
        Assert.IsTrue(_user.IsActive);
    }
    
    [Test]
    public async Task Approval_ShouldSetUserToActive_WhenApproveIsFalse()
    {
        // Arrange
        var id = "testId";
        var user = new KCDUserViewModel
        {
            Email = "test@test.com", FirstName = "Matthew",
            MiddleName = "",
            LastName = "Llyod",
            PhoneNumber = "06051465869",
            Password = "password",
            Country = "Netherlands",
            Company = "Lalandi",
        };
        var approve = false;


        _mockUnitOfWork.Setup(u => u.Employees.GetEmployee(id)).Returns(_employee);
        _mockUnitOfWork.Setup(u => u.Users.GetUserByEmail(user.Email)).Returns(_user);

        // Act
        await _adminFunctions.Approval(id, user, approve);

        // Assert
        Assert.IsFalse(_user.IsActive);
    }
}