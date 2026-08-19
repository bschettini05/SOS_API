using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOS_API.Models
{
    [Table("phones", Schema = "SOS")]
    public class Phone
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("country_id")]
        public int CountryId { get; set; }

        [Column("category")]
        public string? Category { get; set; }

        [Column("phone")]
        public string? PhoneNumber { get; set; }

        public Country Country { get; set; } = null!;
    }
}