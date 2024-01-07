namespace OMF.Common.Events.Restaurant
{
    public class MenuItemStock
    {
        public int MenuId { get; set; }
        public int OldItemCount { get; set; }
        public int NewItemCount { get; set; }
    }
}
