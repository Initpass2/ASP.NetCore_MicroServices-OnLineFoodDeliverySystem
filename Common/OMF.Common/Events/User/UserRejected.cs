namespace OMF.Common.Events
{
    public class UserRejected : IRejectedEvent
    {
        public string Reason { get; }

        public string Code { get; }

        public string Email { get; }

        protected UserRejected() { }

        public UserRejected(string email, string reason, string code)
        {
            Email = email;
            Reason = reason;
            Code = code;
        }
    }

}
