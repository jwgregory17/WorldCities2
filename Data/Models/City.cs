using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorldCities2.Data.Models
{
    public class City
    {
        #region Constructor
        public City()
        {

        }
        #endregion

        #region Properties
        // The unique id and primary key for this City
        [Key]
        [Required]
        public int Id { get; set; }

        
        // City name (in UTF8 format)
        public string Name { get; set; }
        
        // City name (in ASCII format)
        public string Name_ASCII { get; set; }

        // City latitude
        [Column(TypeName = "decimal(7,4)")]
        public decimal Lat { get; set; }

        // City longitude
        [Column(TypeName = "decimal(7,4)")]
        public decimal Lon { get; set; }
        #endregion

        // Country Id (foreign key)
        public int CountryId { get; set; }
    }
}
