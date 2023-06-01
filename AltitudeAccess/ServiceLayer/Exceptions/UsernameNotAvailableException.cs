namespace AltitudeAccess.ServiceLayer.Common
{
    public class UsernameNotAvailableException : Exception
    {
        public UsernameNotAvailableException()
        {

        }
        public UsernameNotAvailableException(string message) : base(message)
        {
            
        }

        public UsernameNotAvailableException(string message, Exception innerException) : base(message, innerException) 
        { 

        }

    }
}
