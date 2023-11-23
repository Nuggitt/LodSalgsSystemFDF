using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodSalgsSystemFDF.Models
{
    [Table("Børn")]
    public class Børn
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]

        public string Navn { get; set; }

        public string Adresse { get; set; }

        public string Telefon { get; set; }

        public int Ark_ID { get; set; }

        public int Børnegruppe_ID { get; set; }
    }
}
