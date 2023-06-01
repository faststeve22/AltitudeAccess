using System.Text.Json.Serialization;

namespace AltitudeAccess.PresentationLayer.DTOs
{
    public class LoginRequestDTO
    {
        [JsonPropertyName("username")]
        public string Username { get;  private set; }

        [JsonPropertyName("password")]
        public string Password { get;  private set; }

        [JsonPropertyName("applicationId")]
        
        public string ApplicationId { get; private set; }
        public LoginRequestDTO(string username, string password, string applicationId)
        {
            Username = username;
            Password = password;
            ApplicationId = applicationId;
        }


    }
}
