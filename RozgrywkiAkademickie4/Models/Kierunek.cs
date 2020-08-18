using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Models
{
    public class Kierunek
    {

        public int Id { get; set; }
       
        [Required(ErrorMessage = "Nazwa kierunku jest wymagana")]
        [StringLength(100, ErrorMessage = "Nazwa kierunku jest za długa")]

        [RegularExpression(@"^[A-Z][a-z]([a-z]| ?)*",
         ErrorMessage = "Nazwa kierunku musi zaczynać sie z wielkiej litery i nie może zawierać liczb.")]
        public string Nazwa { get; set; }

        [Required(ErrorMessage = "Rok jest wymagany")]
        [RegularExpression(@"^[1-5]$",
         ErrorMessage = "Nazwa kierunku moze zawierac sie tylko z cyfry od 1-5")]
         public int Rok { get; set; }


        public bool CzyBonus { get; set; }



    }
}
