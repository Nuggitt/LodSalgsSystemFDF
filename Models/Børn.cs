﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodSalgsSystemFDF.Models
{
    [Table("Børn")]
    public class Børn
    {
        [Key]
        public int Børn_ID { get; set; }
        [Required]
        [StringLength(30)]

        public string Navn { get; set; }
        [Required]

        public string Adresse { get; set; }
        [Required]

        public string Telefon { get; set; }
        [Required]

        public int AntalSolgteLodsedler { get; set; }
        [Required]

        public int Børnegruppe_ID { get; set; }
        
    }
}
