using CManager.Domain.Models;
using CManager.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city)
        {
            CustomerModel customerModel = new()
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = new AddressModel
                {
                    StreetAddress = streetAddress,
                    PostalCode = postalCode,
                    City = city
                }
            };

            try
            {
                var customers = _customerRepository.GetAllCustomers();
                customers.Add(customerModel);
                var result = _customerRepository.SaveCustomers(customers);
                return result;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<CustomerModel> GetAllCustomers(out bool hasError)
        {
            hasError = false;

            try
            {
                var customers = _customerRepository.GetAllCustomers();
                return customers;
            }
            catch (Exception)
            {

                hasError = true;
                return [];
            }
        }

        public bool DeleteCustomer(Guid id)
        {
            try
            {
            var customers = _customerRepository.GetAllCustomers();
                var customer = customers.FirstOrDefault(c => c.Id == id);

                if (customer == null)
                    return false;

                customers.Remove(customer);
                var result = _customerRepository.SaveCustomers(customers);
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting customer: {ex.Message}");
                return false;
            }
        }
    }
}
