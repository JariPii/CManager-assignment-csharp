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

//        public void ViewAllCustomers()
//        {
//            Console.Clear();
//            Console.WriteLine("All Customers \n");

//            var customers = _customerService.GetAllCustomers(out bool hasError);

//            if (hasError)
//            {
//                Console.WriteLine("Failed to get customers. Please try again later!");
//            }

//            if (!customers.Any())
//            {
//                Console.WriteLine("No customers found");
//            }
//            else
//            {
//                foreach (var customer in customers)
//                {
//                    Console.WriteLine($@"Name: {customer.FirstName} {customer.LastName}
//        Email: {customer.Email}
//"
//        );
//                    Console.WriteLine();
//                }
//            }

//            Console.WriteLine("Enter customer id");
//            var input = Console.ReadLine();

//            if (!int.TryParse(input, out int customerId) || customerId == 0)
//                return;

//            //OutputDialog("Press any key...");
//        }

        public void ViewCustomer(CustomerModel customer)
        {
            Console.Clear();

        }

        public void DeleteCustomer(CustomerModel customer)
        {

        }

        //public void DeleteCustomer(CustomerModel customer)
        //{
        //    Console.Clear();
        //    Console.WriteLine("Delete Customer");

        //    var customers = _customerService.GetAllCustomers(out bool hasError).ToList();

        //    if (hasError)
        //    {
        //        Console.WriteLine("Something went wrong. Please try again later");
        //    }

        //    if (!customers.Any())
        //    {
        //        Console.WriteLine("No customers found");
        //    }
        //    else
        //    {
        //        while (true)
        //        {
        //            for(int i = 0; i < customers.Count; i++)
        //            {
        //                var customer = customers[i];
        //                Console.WriteLine($"[{i + 1}] {customer.FirstName} {customer.LastName} {customer.Email}");
        //            }

        //            Console.WriteLine("[0] Go back to menu");
        //            Console.Write("Enter custimer number to delete:");
        //            var input = Console.ReadLine();

        //            if(!int.TryParse(input, out int choise))
        //            {
        //                OutputDialog("Not a valid number!");
        //                    continue;
        //            }

        //            if(choise == 0)
        //            {
        //                return;
        //            }

        //            if (choise > customers.Count)
        //            {
        //                Console.WriteLine($"Number must be 1 and {customers.Count}");
        //                Console.ReadKey();
        //                continue;
        //            }

        //            var idx = choise - 1;
        //            var selectedCustomer = customers[idx];

        //            Console.WriteLine("You have selected: ");
        //            Console.WriteLine($"Name: {selectedCustomer.FirstName} {selectedCustomer.LastName}");

        //            while (true)
        //            {
        //                Console.WriteLine("Are you sure you want to delete this customer? y/n");
        //                var confirmation = Console.ReadLine()!.ToLower();

        //                if(confirmation == "y")
        //                {
        //                    var result = _customerService.DeleteCustomer(selectedCustomer.Id);
        //                    if (result)
        //                    {
        //                        OutputDialog("Customer was removed");
        //                        return;
        //                    }
        //                    else
        //                    {
        //                        OutputDialog("Something went wrong. Support can help you");
        //                        return;
        //                    }
        //                }
        //                else if (confirmation == "n")
        //                {
        //                    return;
        //                }
        //                else
        //                {
        //                    OutputDialog("Please enter 'y' for yes or 'n' for no");
        //                    continue;
        //                }
        //            }
        //        }
        //    }
        //    OutputDialog("Press any key to continue");
        //}

        

        private void OutputDialog(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
