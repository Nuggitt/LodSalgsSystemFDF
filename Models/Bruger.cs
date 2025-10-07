using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodSalgsSystemFDF.Models
{
    [Table("Bruger")]
    public class Bruger
    {
        [Key]
        [StringLength(50)] 
        public string BrugerNavn { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public Bruger() { }

        public Bruger(string brugerNavn, string passwordHash)
        {
            BrugerNavn = brugerNavn;
            Password = passwordHash;
        }
    }
}
