using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodSalgsSystemFDF.Models
{
    [Table("Indtaegt")]
    public partial class Indtaegt
    {
        [Key]
        [Required]
        public int Indtaegt_ID { get; set; }

        [ForeignKey("Salg_ID")]
        public int Salg_ID { get; set; }

        // Navigation property til at lave joins.
        public virtual Salg Salg { get; set; }
        public virtual Born Born { get; set; }
        public virtual Bornegruppe Bornegruppe { get; set; }


        
    }
}
