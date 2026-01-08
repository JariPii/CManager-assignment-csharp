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
        private readonly CustomerController _customerController;

        public MenuController(CustomerController customerController)
        {
            _customerController = customerController;
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
                        _customerController.CreateCustomer();
                        break;
                    case "2":
                        _customerController.ViewAllCustomers();
                        break;
                    //case "3":
                    //    _customerController.DeleteCustomer();
                    //    break;
                    case "0":
                        return;
                    default:
                        OutputDialog("Invalid option!");
                        break;
                }
            }
        }
        private void OutputDialog(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }

}
