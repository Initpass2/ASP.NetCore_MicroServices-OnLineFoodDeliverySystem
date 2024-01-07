using GeoAPI.Geometries;
using System.Collections;

namespace OMF.Api.DTO.Models
{
    public class RestaurantBrowserModel
    {      
        public int MasterRestaurantID { get; set; }      
        public string Name { get; set; }       
        public IEnumerable Coordinates { get; set; }     
        public string Cuisine { get; set; }      
        public string Address { get; set; }      
        public int Budget { get; set; }
        public decimal Rating { get; set; }
        public bool IsActive { get; set; }
    }
}
