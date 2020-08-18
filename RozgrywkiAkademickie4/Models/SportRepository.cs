using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Models
{
    public class SportRepository : ISportRepository
    {
        private readonly AppDbContext _appDbContext;

        public SportRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Sport> PobierzWszystkieSporty()
        {
            return _appDbContext.Sporty;
        }

        public Sport PobierzSportOId(int sportId)
        {
            return _appDbContext.Sporty.FirstOrDefault(s => s.Id == sportId);
        }


        public void DodajSport(Sport sport)
        {
            _appDbContext.Sporty.Add(sport);
            _appDbContext.SaveChanges();
        }

        public void EdytujSport(Sport sport)
        {
            _appDbContext.Sporty.Update(sport);
            _appDbContext.SaveChanges();
        }





        public void UsunSport(Sport sport)
        {

                _appDbContext.Sporty.Remove(sport);
                _appDbContext.SaveChanges();


        }

        Sport ISportRepository.PobierzSportOId(int SportId)
        {
            return _appDbContext.Sporty.FirstOrDefault(s => s.Id == SportId);
        }




    }
}
