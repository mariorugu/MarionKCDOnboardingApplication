using DAL.Models;

namespace DAL.Core.Interfaces;

public interface IPasswordService
{
    string SetPassword(string password);
    string EncryptPassword(string password);
}