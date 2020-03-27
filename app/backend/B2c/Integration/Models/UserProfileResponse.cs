using System.Collections.Generic;

namespace SampleWebApp.B2c.Integration.Models
{
    public class UserProfileResponse
    {
        // Optional claims
        public string Email { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<string> Roles {get;set;}

        public UserProfileResponse()
        {
            Roles = new List<string>(10);
        }
    }
}