using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodSalgsSystemFDF.Models
{
    [Table("Bruger")]
    public class Bruger
    {
        [Key]
        [StringLength(20)]
        public string BrugerNavn { get; set; }
        [Required]
        public string Password { get; set; }

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
