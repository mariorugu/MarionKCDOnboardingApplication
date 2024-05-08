namespace QuickApp.ViewModels;

public class EmployeeViewModel
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; protected set; } // to encrypt
    public string Country { get; set; }
    public string Company { get; set; }
}