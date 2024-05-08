using DAL.Models;

namespace DAL.Core.Interfaces;

public interface IPasswordService
{
    string GetPassword(string password);
    string SetPassword(string password);
}