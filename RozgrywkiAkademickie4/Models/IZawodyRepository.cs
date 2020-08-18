using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Models
{
    public interface IZawodyRepository
    {
        IEnumerable<Zawody> PobierzWszystkieZawody();
        Zawody PobierzZawodyOId(int zawodyId);

        void DodajZawody(Zawody zawody);
        void EdytujZawody(Zawody zawody);
        void UsunZawody(Zawody zawody);

       

        IEnumerable<Sport> PobierzSporty(int ExcludeRecords, int pageSize);

        int IloscSportow();
        
    }
}
