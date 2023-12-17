using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodSalgsSystemFDF.Models
{
    [Table("Bruger")]
    public class Bruger
    {
        [Key]
        [StringLength(100)]
        public string BrugerNavn { get; set; }
        [Required]
        public string Password { get; set; }

        private static PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        public Bruger(string brugerNavn, string password)
        {
            BrugerNavn = brugerNavn;
            Password = password;
        }

        public Bruger()
        {
            BrugerNavn = "";
            Password = "";
        }

    }
}
