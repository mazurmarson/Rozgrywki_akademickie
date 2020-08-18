using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Models
{
    public class ZawodyRepository : IZawodyRepository
    {
        private readonly AppDbContext _appDbContext;

        public ZawodyRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Zawody> PobierzWszystkieZawody()
        {
            return _appDbContext.Zawody;
        }

        public Zawody PobierzZawodyOId(int zawodyId)
        {
            return _appDbContext.Zawody.FirstOrDefault(z => z.Id == zawodyId);
        }

        
        
        public void DodajZawody(Zawody zawody)
        {
            _appDbContext.Zawody.Add(zawody);
            _appDbContext.SaveChanges();
        }



        public void EdytujZawody(Zawody zawody)
        {
            _appDbContext.Zawody.Update(zawody);
            _appDbContext.SaveChanges();
        }
         public void UsunZawody(Zawody zawody)
        {
            _appDbContext.Zawody.Remove(zawody);
            _appDbContext.SaveChanges();
        }

        //public void PobierzSporty(int ExcludeRecords, int pageSize)
        //{
        //    var  sporty = (from s in _appDbContext.Sporty select s).OrderBy(s => s.Nazwa).Skip(ExcludeRecords).Take(pageSize);

        //}

        IEnumerable<Sport> IZawodyRepository.PobierzSporty(int ExcludeRecords, int pageSize)
        {
            var sporty = (from s in _appDbContext.Sporty select s).OrderBy(s => s.Nazwa).Skip(ExcludeRecords).Take(pageSize);
            return sporty;
        }

        public int IloscSportow()
        {
           return _appDbContext.Sporty.Count();
        }
    }
}

