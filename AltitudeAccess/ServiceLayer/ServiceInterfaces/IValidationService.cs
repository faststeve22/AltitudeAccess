namespace AltitudeAccess.ServiceLayer.ServiceInterfaces
{
    public interface IValidationService
    {
        public bool IsValidPassword(string password);
        public bool IsValidUsername(string userName);
    }
}
