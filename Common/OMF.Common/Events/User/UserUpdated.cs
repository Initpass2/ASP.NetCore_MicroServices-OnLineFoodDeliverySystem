namespace OMF.Common.Events.User
{
    public class UserUpdated  : IEvent
    {
        public string UpdatedEmail { get; }
        public string Name { get; }

        protected UserUpdated() { }
        public UserUpdated(string email, string name)
        {
            UpdatedEmail = email;
            Name = name;
        }
    }
}
