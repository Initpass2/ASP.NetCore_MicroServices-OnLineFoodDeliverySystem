using GeoAPI.Geometries;
using System.ComponentModel.DataAnnotations;

namespace OMF.Api.DTO.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        public int MasterRestaurantID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public IPoint Location { get; set; }
        [Required]
        public string Cuisine { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Budget { get; set; }     
        public decimal Rating { get; set; }
        public bool IsActive { get; set; }
    }
}
