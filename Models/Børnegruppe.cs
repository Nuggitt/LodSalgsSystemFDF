using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LodSalgsSystemFDF.Models
{
    [Table("Børnegruppe")]
    public partial class Børnegruppe
    {
        [Key]
        public int Børnegruppe_ID { get; set; }
        [Required]
        [StringLength(30)]
        public string Gruppenavn { get; set; }
        [Required]
        public string Lokale { get; set; }
        public int Antalbørn { get; set; }
        public int Leder_ID { get; set; }
        public int AntalLodSeddelerPrGruppe { get; set; }
        public int AntalSolgteLodSeddeler {  get; set; }
    }
}
