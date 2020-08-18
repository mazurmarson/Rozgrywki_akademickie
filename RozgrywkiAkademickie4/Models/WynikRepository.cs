using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Models
{
    public class WynikRepository : IWynikRepository
    {
        private readonly AppDbContext _appDbContext;

        public WynikRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Wynik> PobierzWszystkieWyniki()
        {
            return _appDbContext.Wyniki;
        }

        public Wynik PobierzWynikOId(int wynikId)
        {
            return _appDbContext.Wyniki.FirstOrDefault(s => s.Id == wynikId);
        }

        public void DodajWynik(Wynik wynik)
        {
            _appDbContext.Wyniki.Add(wynik);
            _appDbContext.SaveChanges();
        }

        public void EdytujWynik(Wynik wynik)
        {
            _appDbContext.Wyniki.Update(wynik);
            _appDbContext.SaveChanges();
        }

        public void UsunWynik(Wynik wynik)
        {
            _appDbContext.Wyniki.Remove(wynik);
            _appDbContext.SaveChanges();
        }




        public int IloscZawodow()
        {
            return _appDbContext.Zawody.Count();
        }

        IEnumerable<Zawody> IWynikRepository.PobierzZawody(int ExcludeRecords, int pageSize)
        {
            var zawody = (from s in _appDbContext.Zawody select s).OrderBy(s => s.DataZawodow).ThenBy(s => s.Sport).Skip(ExcludeRecords).Take(pageSize);
            return zawody;
        }
    }
}
