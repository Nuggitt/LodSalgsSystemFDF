using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodSalgsSystemFDF.Models
{
    [Table("Salg")]
    public class Salg
    {
        [Key]
        [Required]
        public int Salg_ID { get; set; }
        [Required]
        public int Ark_ID { get; set; }
        [Required]
        public int Børn_ID { get; set; }
        [Required]
        public double Pris { get; set; }


    }
}
