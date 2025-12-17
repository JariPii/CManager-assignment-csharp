using CManager.Application.Services;
using CManager.Presentation.ConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CManager.Presentation.ConsoleApp.Controllers
{
    public class MenuController
    {
        private readonly ICustomerService _customerService;

        public MenuController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($@"Customer Manager 

1. Create Customer
2. View All Customers
0. Exit program");
                Console.Write("Choose an option: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CreateCustomer();
                        break;
                    case "2":
                        ViewAllCustomers();
                        break;
                    case "0":
                        return;
                    default:
                        OutputDialog("Invalid option!");
                            break;
                }
            }
        }
    private void CreateCustomer()
        {
            Console.Clear();
            Console.WriteLine("Create customer");

            var firstName = InputHelper.ValidateInput("First name", ValidationType.Required);
            var lastName = InputHelper.ValidateInput("Last name", ValidationType.Required);
            var email = InputHelper.ValidateInput("Email", ValidationType.Email);
            var phoneNumber = InputHelper.ValidateInput("Phone number", ValidationType.Required);
            var streetAddress = InputHelper.ValidateInput("streetAddress", ValidationType.Required);
            var postalCode = InputHelper.ValidateInput("postalCode", ValidationType.Required);
            var city = InputHelper.ValidateInput("city", ValidationType.Required);

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

        private void ViewAllCustomers()
        {
            Console.Clear();
            Console.WriteLine("All Customers \n");

            var customers = _customerService.GetAllCustomers(out bool hasError);

            if (hasError)
            {
                Console.WriteLine("Failed to get customers. Please try again later!");
            }

            if (!customers.Any())
            {
                Console.WriteLine("No customers found");
            }
            else
            {
                foreach(var customer in customers)
                {
                    Console.WriteLine($@"Name: {customer.FirstName} {customer.LastName}
Email: {customer.Email}
Phone: {customer.PhoneNumber}
Address: {customer.Address.StreetAddress}
{customer.Address.PostalCode} {customer.Address.City}
Id: {customer.Id}");
                    Console.WriteLine();
                }
            }

            OutputDialog("Press any key...");
        }

        private void OutputDialog(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }

}
