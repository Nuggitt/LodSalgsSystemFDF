using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LodSalgsSystemFDF.Models
{
    [Table("Bornegruppe")]
    public partial class Bornegruppe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Bornegruppe_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Gruppenavn { get; set; }
        [Required]
        [StringLength(50)]
        public string Lokale { get; set; }
        [Required]
        public int Antalborn { get; set; }
        [Required]
        public int Leder_ID { get; set; }
        [Required]
        public int AntalLodSeddelerPrGruppe { get; set; }
        [Required]
        public int AntalSolgteLodseddelerPrGruppe {  get; set; }

        public virtual Leder Leder { get; set; }
    }
}
