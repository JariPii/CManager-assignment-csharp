using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CManager.Presentation.ConsoleApp.Helpers
{

    public enum ValidationType
    {
        Required,
        Email,
        PhoneNumber,
        PostalCode
    }
    public static class InputHelper
    {
        public static string ValidateInput(string fieldName, ValidationType validationType)
        {
            while (true)
            {
                Console.Write($"{fieldName}: ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine($"{fieldName} is required");
                    Console.ReadKey();
                    continue;
                }

                var (isValid, errorMessage) = ValidateType(input, validationType);

                if (isValid)
                    return input;

                //if (input.Length > 50)
                //{
                //    Console.WriteLine($"{fieldName} is too long");
                //    Console.ReadKey();
                //    continue;
                //}
                //return input;

                Console.WriteLine($"{errorMessage}. Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static (bool isValid, string errorMessage) ValidateType(string input, ValidationType type)
        {
            switch (type)
            {
                case ValidationType.Required:
                    return (true, "");
                case ValidationType.Email:
                    if (IsValidEmail(input))
                        return (true, "");
                    return (false, "Invalid email");
                default:
                    return (true, "");
            }
        }

        private static bool IsValidEmail(string input)
        {
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$";
            return Regex.IsMatch(input, pattern);
        }
    }
}
