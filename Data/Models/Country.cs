using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WorldCities2.Data.Models
{
    public class Country
    {
        //note: #region is like //<editor-fold desc=""></editor-fold> in intelliJ
        #region Constructor
        public Country()
        {

        }
        #endregion

        //note: #region is like //<editor-fold desc=""></editor-fold> in intelliJ
        #region Properties
        // The unique id and primary key for this Country
        [Key]
        [Required]
        public int Id { get; set; }

        // Country name (in UTF8 format)
        public string Name { get; set; }

        // Country code (in ISO 3166-1 ALPHA-2 format)
        [JsonPropertyName("iso2")] 
        public string ISO2 { get; set; }

        // Country code (in ISO 3166-1 ALPHA-3 format)
        [JsonPropertyName("iso3")]
        public string ISO3 { get; set; }
        #endregion
    }
}
