using DAL.Models;

namespace DAL.Repositories.Interfaces;

public interface IEmployeeRepository
{
    bool IsAdministrator(string id);
    Employee GetEmployee(string id);
}