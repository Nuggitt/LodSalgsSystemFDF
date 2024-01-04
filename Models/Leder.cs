using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LodSalgsSystemFDF.Models
{
        [Table("Leder")]
        public partial class Leder
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Required]
            public int Leder_ID { get; set; }
            [Required]
            [StringLength(30)]
            public string Navn { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public string Adresse { get; set; }
            [Required]
            public string Telefon {  get; set; }
            [Required]
            public bool ErLotteriBestyrer { get; set; }
            [Required]
            public int Børnegruppe_ID { get; set; }
    }
}
