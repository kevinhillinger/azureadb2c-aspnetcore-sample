namespace SampleWebApp.B2c.Models
{
    public class UserProfileRequest
    {
        public string ObjectId { get; set; }
        public string Email { get; set; }


        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(ObjectId))
            {
                return false;
            }
            return true;
        }
    }
}