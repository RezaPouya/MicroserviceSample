namespace UserService.ETOs
{
    [Serializable]
    [EventName("UserService:UserDeleted")]
    public class UserDeletedEto
    {
        public UserDeletedEto()
        {

        }
        public Guid Id { get; set; }

        public UserDeletedEto(Guid id)
        {
            Id = id;
        }
    }
}
