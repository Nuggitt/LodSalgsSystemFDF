using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodSalgsSystemFDF.Models
{
    [Table("Born")]
    public class Born
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Born_ID { get; set; }
        [Required]
        [StringLength(30)]

        public string Navn { get; set; }
        [Required]

        public string Adresse { get; set; }
        [Required]

        public string Telefon { get; set; }
        [Required]

        public int GivetLodsedler { get; set; }
        [Required]

        public int AntalSolgteLodseddeler { get; set; }
        [Required]

        public int Bornegruppe_ID { get; set; }

        public virtual Bornegruppe Bornegruppe { get; set; }

        public virtual Leder Leder { get; set; }
        
    }
}
