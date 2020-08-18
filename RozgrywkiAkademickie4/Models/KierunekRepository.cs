using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Models
{
    public class KierunekRepository : IKierunekRepository
    {
        private readonly AppDbContext _appDbContext;

        public KierunekRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Kierunek> PobierzWszystkieKierunki()
        {
            return _appDbContext.Kierunki;
        }

        public void UsunKierunek(Kierunek kierunek)
        {
            _appDbContext.Kierunki.Remove(kierunek);
            _appDbContext.SaveChanges();
        }



        public void DodajKierunek(Kierunek kierunek)
        {
            _appDbContext.Kierunki.Add(kierunek);
            _appDbContext.SaveChanges();
        }

        public void EdytujKierunek(Kierunek kierunek)
        {
            _appDbContext.Kierunki.Update(kierunek);
            _appDbContext.SaveChanges();
        }

        public Kierunek PobierzKierunekOId(int kierunekId)
        {
            return _appDbContext.Kierunki.FirstOrDefault(s => s.Id == kierunekId);
        }


    }
}
