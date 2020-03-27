namespace SampleWebApp.B2c.Integration.Models
{
    public class UserProfileResponse
    {
        public string version { get; set; }
        public int status { get; set; }
        public string userMessage { get; set; }

        // Optional claims
        public string email { get; set; }
        public string emailHash { get; set; }
        public string displayName { get; set; }
        public string givenName { get; set; }
        public string surName { get; set; }
    }
}