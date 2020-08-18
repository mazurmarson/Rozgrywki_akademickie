using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Models
{
    public interface IWynikRepository
    {
        IEnumerable<Wynik> PobierzWszystkieWyniki();
        Wynik PobierzWynikOId(int wynik);

        void DodajWynik(Wynik wynik);
        void EdytujWynik(Wynik wynik);
        void UsunWynik(Wynik wynik);

        IEnumerable<Zawody> PobierzZawody(int ExcludeRecords, int pageSize);

        int IloscZawodow();
    }
}
