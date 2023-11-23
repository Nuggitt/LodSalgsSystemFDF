using System.ComponentModel.DataAnnotations;

namespace LodSalgsSystemFDF.Models
{
    public class Lodseddelark
    {
        [Key]
        public int Ark_ID { get; set; }
        [Required]
        public int AntalLodSeddeler { get; set; }
        [Required]
        public float PrisPrLod { get; set; }
        [Required]
        public float PrisPrArk { get; set; }
        
    }
}
