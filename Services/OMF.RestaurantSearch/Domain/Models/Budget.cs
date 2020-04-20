using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMF.RestaurantSearch.Domain.Models
{
    public class Budget
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }       
        public int Value { get; set; }
        public string Desctiption { get; set; }

        public ICollection<Restaurant> Restaurant { get; set; }
    }
}
