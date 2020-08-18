using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Models
{
    public interface IKierunekRepository
    {
        IEnumerable<Kierunek> PobierzWszystkieKierunki();
        Kierunek PobierzKierunekOId(int kierunekId);

        void DodajKierunek(Kierunek kierunek);
        void EdytujKierunek(Kierunek kierunek);
        void UsunKierunek(Kierunek kierunek);
    }
}
