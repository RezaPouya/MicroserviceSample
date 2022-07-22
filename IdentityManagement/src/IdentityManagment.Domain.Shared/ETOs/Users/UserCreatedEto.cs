namespace IdentityManagment.ETOs.Users
{
    [Serializable]
    [EventName("UserService:UserCreated")]
    public class UserCreatedEto
    {
        public UserCreatedEto()
        {
        }

        public UserCreatedEto(Guid id, string fname, string lname)
        {
            Id = id;
            Fname = fname;
            Lname = lname;
        }

        public Guid Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
    }
}