namespace OMF.Common.Events.User
{
    public class UserDeleted : IEvent
    {
        public string Email { get; }       

        protected UserDeleted() { }
        public UserDeleted(string email)
        {
            Email = email;            
        }
    }
}
