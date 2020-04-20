namespace OMF.Api.ResponseModel.Restaurant
{
    public class Menu
    {       
        public int Id { get; set; }       
        public string ItemName { get; set; }    
        public string Description { get; set; }      
        public string Ingredients { get; set; }      
        public double Price { get; set; }
        public bool IsActive { get; set; }     
    }
}
