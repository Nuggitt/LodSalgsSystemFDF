using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LodSalgsSystemFDF.Models
{
    [Table("Børnegruppe")]
    public partial class Børnegruppe
    {
        [Key]
        public int Børnegruppe_Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Gruppenavn { get; set; }
        [Required]
        public string Lokale { get; set; }
        public int Antalbørn { get; set; }
        public int LederId { get; set; }
    }
}
