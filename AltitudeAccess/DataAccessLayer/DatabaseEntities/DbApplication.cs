namespace AltitudeAccess.DataAccessLayer.DatabaseEntities
{
    public class DbApplication
    {
        public int ApplicationId {  get; private set; }
        public string ApplicationName { get; private set; }

        public DbApplication(int applicationId, string applicationName)
        {
            ApplicationId = applicationId;
            ApplicationName = applicationName;
        }
    }
}
