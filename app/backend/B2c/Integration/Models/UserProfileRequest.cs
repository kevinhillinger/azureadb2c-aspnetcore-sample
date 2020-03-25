namespace SampleWebApp.B2C.Models
{
    public class UserProfileRequest
    {
        public string ObjectId { get; set; }
        public string EmailAddress { get; set; }


        public bool IsValid()
        {
            if (string.IsNullOrEmpty(EmailAddress) && string.IsNullOrEmpty(ObjectId))
            {
                return false;
            }
            return true;
        }
    }
}