using System;
using System.Reflection.Metadata;
using DAL.Core;

namespace DAL.Models;

public class KCDUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    
    public string IdentityDocument { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; } // to encrypt
    public string Country { get; set; }
    public string Company { get; set; }
    public string Miscellaneous  { get; set; }
    public string ProfilePicture { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    public Role Role { get; set; }

}