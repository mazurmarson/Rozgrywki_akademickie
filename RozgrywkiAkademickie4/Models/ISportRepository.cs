using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Models
{
    public interface ISportRepository
    {

        IEnumerable<Sport> PobierzWszystkieSporty();
        Sport PobierzSportOId(int SportId);

        void DodajSport(Sport sport);
        void EdytujSport(Sport sport);
        void UsunSport(Sport sport);
    }
}
