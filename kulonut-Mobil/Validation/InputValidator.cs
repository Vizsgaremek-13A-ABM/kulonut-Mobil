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
				return "Az e-mail cím megadása kötelező.";

			email = email.Trim();
			var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

			if (!Regex.IsMatch(email, pattern))
				return "Érvénytelen e-mail formátum.";

			return null;
		}
		public static string? ValidateName(string? name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return "A név megadása kötelező.";

			name = name.Trim();

			if (name.Length < 2)
				return "A névnek legalább 2 karakter hosszúnak kell lennie.";

			if (!Regex.IsMatch(name, @"^[\p{L}\s]+$"))
				return "A név csak betűket és szóközt tartalmazhat.";

			return null;
		}
		public static string? ValidatePassword(string? password)
		{
			if (string.IsNullOrWhiteSpace(password))
				return "A jelszó megadása kötelező.";

			if (password.Length < 8)
				return "A jelszónak legalább 8 karakter hosszúnak kell lennie.";

			if (!Regex.IsMatch(password, @"[a-z]"))
				return "A jelszónak tartalmaznia kell legalább egy kisbetűt.";

			if (!Regex.IsMatch(password, @"[A-Z]"))
				return "A jelszónak tartalmaznia kell legalább egy nagybetűt.";

			if (!Regex.IsMatch(password, @"\d"))
				return "A jelszónak tartalmaznia kell legalább egy számot.";

			if (!Regex.IsMatch(password, @"[^A-Za-z\d\.]"))
				return "A jelszónak tartalmaznia kell legalább egy speciális karaktert.";

			return null;
		}
		public static string? ValidateDateInterval(DateTime? startDate, DateTime? endDate)
		{
			if (startDate != null && startDate.Value < DateTime.Now)
				return "A kezdő dátum nem lehet a múltban.";
			if (startDate != null && endDate != null && startDate > endDate)
				return "A kezdő dátum nem lehet későbbi, mint a befejező dátum.";

			return null;
		}
	}
}