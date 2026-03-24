using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace kulonut_Mobil.Validation
{
    public static class InputValidator
    {
        public static string? ValidateEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return "Email is required.";

            email = email.Trim();
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, pattern))
                return "Invalid email format.";

            return null;
        }
        public static string? ValidateName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Name is required.";

            name = name.Trim();

            if (name.Length < 2)
                return "Name must be at least 2 characters long.";

            if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
                return "Name can only contain letters and spaces.";

            return null;
        }
        public static string? ValidatePassword(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return "Password is required.";

            if (password.Length < 8)
                return "Password must be at least 8 characters.";

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return "Must contain an uppercase letter.";

            if (!Regex.IsMatch(password, @"[a-z]"))
                return "Must contain a lowercase letter.";

            if (!Regex.IsMatch(password, @"\d"))
                return "Must contain a number.";

            return null;
        }
    }
}