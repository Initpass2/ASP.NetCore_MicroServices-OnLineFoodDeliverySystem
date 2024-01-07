using OMF.RestaurantSearch.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMF.RestaurantManagement.Domain.Models
{
    public class RestaurantMenuItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        
        public int MenuID { get; set; }       
        [ForeignKey("MenuID")]
        public Menu Budget { get; set; }      
        public int ResturantID { get; set; }
        public int ItemCount { get; set; }
        [ForeignKey("ResturantID")]
        public Restaurant Restaurant { get; set; }
    }
}
