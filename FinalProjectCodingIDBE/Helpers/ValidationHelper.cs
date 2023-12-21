using System.Text.RegularExpressions;

namespace FinalProjectCodingIDBE.Helpers
{
    public class ValidationHelper
    { 
        public static string ValidationPassword(string password)
        {
            string response = string.Empty;
            
            string uppercaseRegExp   = @"(?=.*?[A-Z])";
            string lowercaseRegExp   = @"(?=.*?[a-z])";
            string digitsRegExp      = @"(?=.*?[0-9])";
            string specialCharRegExp = @"(?=.*?[#?!@$%^&*-])";
            string minLengthRegExp = @"^(.{8,})$";

            if (password.Length == 0)
            {
                response = "Password Not Null";
            }
            else if(!Regex.IsMatch(password,minLengthRegExp))
            {
                response = "At Least Minimum Character is 8";
            }
            else if (!Regex.IsMatch(password, lowercaseRegExp))
            {
                response = "At Least One Character Lowercase";
            }
            else if (!Regex.IsMatch(password, uppercaseRegExp))
            {
                response = "At Least One Character Uppercase";
            }
            else if (!Regex.IsMatch(password, digitsRegExp))
            {
                response = "At Least One Character is Number";
            }
            else if (!Regex.IsMatch(password, specialCharRegExp))
            {
                response = "At Least One Character is Special Character";
            }
            return response;
        }

        public static string ValidationConfirmPassword (string oldPass, string newPass) {
            string response = string.Empty;

            if (string.IsNullOrEmpty(newPass) || newPass == "")
            {
                response = "Confirm Password Must Not Null";
            }
            else if (oldPass.Length != newPass.Length)
            {
                response = "Confirm Password Not Same Length";
            }

                for (int i = 0; i < oldPass.Length; i++)
                {
                    if (newPass[i] != oldPass[i])
                     {
                         response = "Confirm Password Not Same";
                         break;
                     }
                }

           
           
            return response;
        }
    }
}
