using RozgrywkiAkademickie4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.PomoKlas
{
    public class WynikIZawody
    {
       public  Zawody zawody { get; set; }
       public Wynik wynik { get; set; }

        public WynikIZawody(Wynik _wynik, Zawody _zawody)
        {
            
            wynik = _wynik;
            zawody = _zawody;
        }
    }
}
