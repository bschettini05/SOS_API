using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOS_API.Models
{
    [Table("countries", Schema = "SOS")]
    public class Country
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("iso3_code")]
        public required string Iso3Code { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("iso2_code")]
        public required string Iso2Code { get; set; }
    }
}