using DAL.Repositories.Interfaces;
using System;
using System.Linq;

namespace DAL
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }
        IUserRepository Users { get; }
        
        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }
        IOrdersRepository Orders { get; }

        int SaveChanges();
    }
}
