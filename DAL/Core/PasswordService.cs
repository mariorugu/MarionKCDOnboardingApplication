using System;
using DAL.Core.Interfaces;
using DAL.Models;

namespace DAL.Core;

public class PasswordService : IPasswordService
{
    public string GetPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException(nameof(password));
        }
        byte[] data = Convert.FromBase64String(password);
        string decodedString = System.Text.Encoding.UTF8.GetString(data);
        return decodedString;
    } 
    public string SetPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException(nameof(password));
        }

        var encryptedPassword = EncryptPassword(password);
        return encryptedPassword;
    }

    private string EncryptPassword(string password)
    {
        try
        {
            byte[] encodedPassword;
            encodedPassword = System.Text.Encoding.UTF8.GetBytes(password);
            var encodedData = Convert.ToBase64String(encodedPassword);
            return encodedData;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }
}