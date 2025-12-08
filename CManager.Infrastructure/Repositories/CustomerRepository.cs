using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CManager.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _filePath;
        private readonly string _directoryPath;
        private readonly JsonSerializerOptions _jsonOptions;

        public CustomerRepository(string directoryPath = "Data", string filename = "customers.json")
        {
            _directoryPath = directoryPath;
            _filePath = Path.Combine(_directoryPath, filename);
        }

        public List<CustomerModel> GetAllCustomers()
        {
            if (!File.Exists(_filePath))
            {
                return [];
            }

            try
            {
                var json = File.ReadAllText(_filePath);
                var customers = JsonSerializer.Deserialize<List<CustomerModel>>(json, _jsonOptions);
                return customers ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load customers: {ex.Message}");
                throw;
            }
        }

        public bool SaveCustomers(List<CustomerModel> customers)
        {
            if(customers == null)
            {
                return false;
            }

            try
            {
                var json = JsonSerializer.Serialize(customers, _jsonOptions);

                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);

                    File.WriteAllText(_filePath, json);
                    return true;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Failed to save customers: {ex.Message}");
                return false;
            }
        }
    }
}
