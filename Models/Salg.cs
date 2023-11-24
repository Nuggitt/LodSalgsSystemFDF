﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodSalgsSystemFDF.Models
{
    [Table("Salg")]
    public class Salg
    {
        [Key]
        [Required]
        public int Salg_ID { get; set; }
        [Required]
        public int Børn_ID { get; set; }
        [Required]
        public int Leder_ID { get; set; }
        [Required]
        public DateTime Dato { get; set; }

        public int AntalLodseddelerRetur { get; set; }

        public int AntalSolgteLodSeddelerPrSalg { get; set; }

        public double Pris { get; set; }


    }
}
