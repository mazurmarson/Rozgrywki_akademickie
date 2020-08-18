using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Models
{
    public class Wynik
    {

        public int Id { get; set; }

        public Kierunek Kierunek { get; set; }

        public Zawody Zawody { get; set; }
        
        [Required(ErrorMessage = "Zajęte miejsce jest wymagane")]
        [Range(1, 50,
        ErrorMessage = "Wartosc {0} powinna zawierać się {1} i {2}.")]

        public int Miejsce { get; set; }
        [Required(ErrorMessage = "Ilosc punktów jest wymagana")]
        [Range(1, 50,
        ErrorMessage = "Wartosc {0} powinna zawierać się {1} i {2}.")]
        public int Punkty { get; set; }

        
    }
}
