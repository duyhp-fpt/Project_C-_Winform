namespace LibraryManagement.Utils {
    public class Validation {

        public static bool checkEmpty(string str) {
            return !string.IsNullOrEmpty(str);
        }

        public static bool checkInteger(string input) {
            try {
                int.Parse(input);
                return true;
            } catch {
                return false;
            }
        }

        public static bool checkFloat(string input) {
            try {
                float.Parse(input);
                return true;
            } catch {
                return false;
            }
        }

        public static bool checkEmail(string email) {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            } catch {
                return false;
            }
        }

        public static bool checkPassword(string password) {
            return password.Length >= 6;
        }
    }
}
