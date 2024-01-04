using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LodSalgsSystemFDF.Models
{
    [Table("Børnegruppe")]
    public partial class Børnegruppe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Børnegruppe_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Gruppenavn { get; set; }
        [Required]
        [StringLength(50)]
        public string Lokale { get; set; }
        [Required]
        public int Antalbørn { get; set; }
        [Required]
        public int Leder_ID { get; set; }
        [Required]
        public int AntalLodSeddelerPrGruppe { get; set; }
        [Required]
        public int AntalSolgteLodseddelerPrGruppe {  get; set; }
    }
}
