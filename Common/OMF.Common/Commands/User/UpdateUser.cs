namespace OMF.Common.Commands.User
{
    public class UpdateUser : ICommand
    {
        public string OldEmail { get; set; }
        public string UpdatedEmail { get; set; }      
        public string Name { get; set; }
    }
}
