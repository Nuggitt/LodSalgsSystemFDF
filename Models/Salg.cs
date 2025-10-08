using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodSalgsSystemFDF.Models
{
    [Table("Salg")]
    public class Salg
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Salg_ID { get; set; }
        [Required]
        public int Born_ID { get; set; }
        [Required]
        public int Bornegruppe_ID { get; set; }
        [Required]
        public int Leder_ID { get; set; }
        [Required]
        public DateTime Dato { get; set; }

        public int AntalLodseddelerRetur { get; set; }

        public int AntalSolgteLodseddelerPrSalg { get; set; }

        public double Pris { get; set; }

        public virtual Leder Leder { get; set; }

        public virtual Born Born { get; set; }
        public virtual Bornegruppe Bornegruppe { get; set; }



    }
}
