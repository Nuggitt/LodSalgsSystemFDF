using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodSalgsSystemFDF.Models
{
    [Table("Børn")]
    public class Børn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Børn_ID { get; set; }
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

        public int Børnegruppe_ID { get; set; }
        
    }
}
