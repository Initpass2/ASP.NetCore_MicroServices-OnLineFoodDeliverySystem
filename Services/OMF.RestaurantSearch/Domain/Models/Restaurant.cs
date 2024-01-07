using GeoAPI.Geometries;
using OMF.RestaurantManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMF.RestaurantSearch.Domain.Models
{
    public class Restaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        public string City { get; set; }
        [Required]
        public string Zip { get; set; }
        [Column(TypeName = "geometry")]
        [Required]
        public IPoint Location { get; set; }
        public int BudgetId { get; set; }
        [ForeignKey("BudgetId")]
        public Budget Budget { get; set; }
        public int CuisineId { get; set; }
        [ForeignKey("CuisineId")]
        public Cuisine Cuisine { get; set; }       
        public bool IsActive { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public DateTime UpdatedOn { get; set; }     

        public ICollection<RestaurantMenuItem> restaurantMenuItems { get; set; }
        // public Cuisine cuisine { get; set; }
        // public ICollection<Menu> menus { get; set; }
    }
}
