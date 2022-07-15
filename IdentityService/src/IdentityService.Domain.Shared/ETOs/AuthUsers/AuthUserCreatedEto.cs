namespace IdentityService.ETOs.Users
{
    [Serializable]
    [EventName("IdentityService:AuthUserCreated")]
    public class AuthUserCreatedEto
    {
        public AuthUserCreatedEto()
        {
        }

        public AuthUserCreatedEto(Guid id, string cellphone, string email)
        {
            Id = id;
            Cellphone = cellphone;
            Email = email;
        }

        public Guid Id { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
    }
}