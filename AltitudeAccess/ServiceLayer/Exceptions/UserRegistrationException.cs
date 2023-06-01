namespace AltitudeAccess.ServiceLayer.Common
{
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException() : base()
        {

        }

        public UserRegistrationException(string message) : base(message)
        {

        }

        public UserRegistrationException(string message, Exception inner) : base(message, inner) 
        {

        }
    }
}
