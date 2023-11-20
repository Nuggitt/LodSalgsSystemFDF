using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodSalgsSystemFDF.Models
{
    [Table("Indtægt")]
    public partial class Indtægt
    {
        [Key]
        [Required]
        public int Indtægt_ID { get; set; }
        [Required]
        [Column("Dato")]
        public DateTime Dato { get; set; }
        [Required]
        public int Salg_ID { get; set; }



    }
}
