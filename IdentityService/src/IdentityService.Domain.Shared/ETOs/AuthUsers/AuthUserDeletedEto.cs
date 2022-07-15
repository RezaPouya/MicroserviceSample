namespace IdentityService.ETOs.Users
{
    [Serializable]
    [EventName("IdentityService:AuthUserDeleted")]
    public class AuthUserDeletedEto
    {
        public AuthUserDeletedEto()
        {

        }
        public Guid Id { get; set; }

        public AuthUserDeletedEto(Guid id)
        {
            Id = id;
        }
    }
}
