using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_ef_videogame
{
    [Table("software_houses")]
    public class SoftwareHouse
    {
        [Key] public long SoftwareHouseId { get; set; }
        public string Name { get; set; }
        
        [Column("tax_id")]
        public string TaxId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public List<VideoGame> VideoGames { get; set; }

        public SoftwareHouse() { }
        public SoftwareHouse(string name, string taxId, string city, string country, DateTime createdAt, DateTime? updatedAt)
        {
            Name = name;
            TaxId = taxId;
            City = city;
            Country = country;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public override string ToString()
        {
            return $"Id: {SoftwareHouseId}\nName: {Name}\nTaxId: {TaxId}\nCity: {City}\nCountry: {Country}\nCreated at: {CreatedAt}\nUpdated at: {UpdatedAt}";
        }
    }
}
