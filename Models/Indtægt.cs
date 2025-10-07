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

        [ForeignKey("Salg_ID")]
        public int Salg_ID { get; set; }

        // Navigation property til at lave joins.
        public virtual Salg Salg { get; set; }
        public virtual Børn Børn { get; set; }
        public virtual Børnegruppe Børnegruppe { get; set; }


        
    }
}

