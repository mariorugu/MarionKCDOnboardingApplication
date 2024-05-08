using DAL.Models;
using DAL.Repositories.Interfaces;
using System.Linq;

namespace DAL.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        { }

        public bool IsAdministrator(string id)
        {
            var employee =  _appContext.Employees.Where(x=> x.Id == id && x.IsAdministrator);
            return employee.Any();
        }

        public Employee GetEmployee(string id)
        {
            var employee =  _appContext.Employees.Where(x=> x.Id == id);
            return employee.SingleOrDefault();
        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}