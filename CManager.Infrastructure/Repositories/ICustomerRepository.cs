using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Infrastructure.Repositories
{
    public interface ICustomerRepository
    {
        List<CustomerModel> GetAllCustomers();

        bool SaveCustomers(List<CustomerModel> customers);
    }
}
