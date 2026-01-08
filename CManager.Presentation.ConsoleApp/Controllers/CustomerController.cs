using CManager.Application.Services;
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

        private void OutputDialog(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
