using AltitudeAccess.ServiceLayer.ServiceInterfaces;
using System.Text.RegularExpressions;

namespace AltitudeAccess.ServiceLayer.Services
{   
    public class ValidationService : IValidationService
    {   
        public ValidationService() { }

        public bool IsValidUsername(string username)
        {
            string pattern = @"^[a-zA-Z0-9-_]{4,20}$";
            return Regex.IsMatch(username, pattern);
        }

        public bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,30}$";
            return Regex.IsMatch(password, pattern);
        }

    }
}
