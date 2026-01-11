using CManager.Application.Services;
using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Presentation.ConsoleApp.Controllers
{
    public class CustomerController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void CreateCustomer()
        {
            Console.Clear();
            Console.WriteLine("Create customer");

            Console.Write("First name: ");
            var firstName = Console.ReadLine()!;

            Console.Write("Last name: ");
            var lastName = Console.ReadLine()!;

            Console.Write("Email: ");
            var email = Console.ReadLine()!;

            Console.Write("Phonenumber: ");
            var phoneNumber = Console.ReadLine()!;

            Console.Write("Street: ");
            var streetAddress = Console.ReadLine()!;

            Console.Write("Postal code: ");
            var postalCode = Console.ReadLine()!;

            Console.Write("City: ");
            var city = Console.ReadLine()!;

            var result = _customerService.CreateCustomer(firstName, lastName, email, phoneNumber, streetAddress, postalCode, city);

            if (result)
            {
                Console.WriteLine($"Customer: {firstName} {lastName} added.");
            }
            else
            {
                Console.WriteLine("There was a hickup while creating customer, please try again!");
            }

            OutputDialog("Press any key to continue...");
        }

        public void ViewAllCustomers()
        {
            Console.Clear();
            Console.WriteLine("All customers");

            var customers = _customerService.GetAllCustomers(out bool hasError).ToList();

            if (hasError)
            {
                OutputDialog("Failed to get customers");
                return;
            }

            if (!customers.Any())
            {
                OutputDialog("No customers found");
                return;
            }

            for (int i = 0; i < customers.Count; i++)
            {
                var c = customers[i];
                Console.WriteLine($"[{i + 1}] {c.FirstName} {c.LastName} - {c.Email}");
            }

            Console.WriteLine("[0] Go back to menu");
            Console.Write("Enter customer number to select: ");

            var input = Console.ReadLine();
            if(!int.TryParse(input, out int choise) || choise < 0 || choise > customers.Count)
            {
                OutputDialog("Invlaid selection!");
                return;
            }

            if (choise == 0) return;

            if(choise < 1 || choise > customers.Count)
            {
                OutputDialog($"Number must be between 1 and {customers.Count}");
                return;
            }

            var selectedCustomer = customers[choise - 1];

            ShowCustomerMenu(selectedCustomer);
        }

        public void ShowCustomerMenu(CustomerModel customer)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($@"Customer: {customer.FirstName} {customer.LastName}
1. View Details
2. Delete customer
0. Go back
");

                Console.WriteLine("Choose an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ViewCustomer(customer);
                        break;
                    case "2":
                        DeleteCustomer(customer);
                        return;
                    case "0":
                        return;

                    default:
                        OutputDialog("Invalid option! Enter 1, 2, or 0.");
                        break;
                }
            }
        }

//       
        public void ViewCustomer(CustomerModel customer)
        {
            Console.Clear();
            Console.WriteLine("Customer details\n");

            Console.WriteLine($@"Name: {customer.FirstName} {customer.LastName}
Email: {customer.Email}
Phone: {customer.PhoneNumber}
Address: {customer.Address.StreetAddress}
{customer.Address.PostalCode} {customer.Address.City}
");

            OutputDialog("Press any key to go back");

        }

        public void DeleteCustomer(CustomerModel customer)
        {
            Console.Clear();
            Console.Write($"Are you sure you want to delete {customer.FirstName} {customer.LastName}? (y/n): ");

            var confirmation = Console.ReadLine()?.Trim().ToLower();

            if(confirmation == "y")
            {
                var result = _customerService.DeleteCustomer(customer.Id);
                OutputDialog(result ? "Customer was removed" : "Something want wrong. Support is available for help");
            }
            else
            {
                OutputDialog("Deletion canceled");
            }
        }

        
        private void OutputDialog(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
