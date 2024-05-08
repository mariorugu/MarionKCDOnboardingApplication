using System;
using System.Reflection.Metadata;

namespace DAL.Models;

public class Employee : AuditableEntity
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; } // to encrypt
    public string Country { get; set; }
    public string Company { get; set; }
    public string ProfilePicture { get; set; }
    public string JobTitle { get; set; }

    public bool IsActive { get; set; }
    public bool IsAdministrator { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    public string LastUpdatedBy { get; set; }
}