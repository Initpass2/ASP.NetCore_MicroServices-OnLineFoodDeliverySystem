using GeoAPI.Geometries;

namespace OMF.Common.Events.Restaurant
{
    public class RestaurantCreatedOrUpdated : IEvent
    {       
        public int RestaurantID { get; set; }        
        public string Name { get; set; }        
        public IPoint Location { get; set; }        
        public string Cuisine { get; set; }        
        public string Address { get; set; }        
        public int Budget { get; set; }
        public decimal Rating { get; set; }
        public bool IsActive { get; set; }

        public RestaurantCreatedOrUpdated(int restaurantId, string name, IPoint loc,string cuisine,string address,int budget, decimal rating, bool isactive)
        {
            RestaurantID = restaurantId;
            Name = name;
            Location = loc;
            Cuisine = cuisine;
            Address = address;
            Budget = budget;
            Rating = rating;
            IsActive = isactive;
        }

        protected RestaurantCreatedOrUpdated() { }
       
    }
}
