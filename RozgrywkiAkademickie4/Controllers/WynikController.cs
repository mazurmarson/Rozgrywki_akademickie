using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Frameworks;
using RozgrywkiAkademickie4.Models;
using RozgrywkiAkademickie4.PomoKlas;
using RozgrywkiAkademickie4.ViewsModel;

namespace RozgrywkiAkademickie4.Controllers
{
    public class WynikController : Controller
    {
        private readonly IZawodyRepository _zawodyRepository;
        private readonly IKierunekRepository _kierunekRepository;
        private readonly ISportRepository _sportRepository;
        private readonly IWynikRepository _wynikRepository;

        public WynikController(IWynikRepository wynikRepository, IKierunekRepository kierunekRepository, IZawodyRepository zawodyRepository, ISportRepository sportRepository)
        {
            _wynikRepository = wynikRepository;
            _kierunekRepository = kierunekRepository;
            _zawodyRepository = zawodyRepository;
            _sportRepository = sportRepository;
        }

        public IActionResult Index()
        {
            var wyniki = _wynikRepository.PobierzWszystkieWyniki();
            var kierunki = _kierunekRepository.PobierzWszystkieKierunki();


            var result = from w in wyniki
                         join k in kierunki on w.Kierunek equals k
                         select new
                         {
                             Nazwa = k.Nazwa,
                             Rok = k.Rok,
                             Punkty = w.Punkty,
                             CzyBonus = k.CzyBonus
                             
                         };

            var result2 = from q in result
                          group q by new { q.Nazwa, q.Rok, q.CzyBonus } into g
                          select new Tabela
                          {
                              Nazwa = g.Key.Nazwa,
                              Rok = g.Key.Rok,
                              
                              Punkty = g.Sum(q => q.Punkty),
                              
                              CzyBonus = g.Key.CzyBonus,

                              

                          };

            var result3 = from q in result2
                          where q.CzyBonus == true
                          select new Tabela
                          {
                              Nazwa = q.Nazwa,
                              Rok = q.Rok,
                              CzyBonus = q.CzyBonus,
                              Punkty = q.Punkty + 5
                          };

            result3 = result3.Concat(result2);



            //result2 = result2.OrderByDescending(g => g.Punkty);
            result3 = result3.OrderByDescending(g => g.Punkty);





            return View(result3);
        }

        public IActionResult WybierzZawody(string searchString, int pageNumber = 1, int pageSize = 3)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;


            // var users = _context.User.Skip(ExcludeRecords).Take(pageSize);

            var zawody = _wynikRepository.PobierzZawody(ExcludeRecords, pageSize);
            var sporty = _sportRepository.PobierzWszystkieSporty();
            int iloscZawodow = _wynikRepository.IloscZawodow();

            var multipletable = from z in zawody
                                join s in sporty on z.Sport equals s
                                select new Zawody
                                {
                                    Sport = s,
                                    DataZawodow = z.DataZawodow,
                                    Id = z.Id
                                    

                                };

            //if (!String.IsNullOrEmpty(searchString))
            //{

            //    var users2 = (from u in _context.User where u.FirstName.Contains(searchString) || u.LastName.Contains(searchString) select u).Skip(ExcludeRecords)
            //    .Take(pageSize);

            //    var ilosc = (from u in _context.User where u.FirstName.Contains(searchString) || u.FirstName.Contains(searchString) select u);

            //    // users = _context.User(s => (s.FirstName.Contains(searchString) || s.LastName.Contains(searchString)));
            //    var result2 = new PagedResult<User>
            //    {
            //        Data = users2.AsNoTracking().ToList(),
            //        //TotalItems = _context.User.Count(),
            //        TotalItems = ilosc.Count(),
            //        PageNumber = pageNumber,
            //        PageSize = pageSize
            //    };

            //    return View(result2);
            //}

            var result = new PagedResult<Zawody>
            {
                Data = multipletable.ToList(),
                TotalItems = iloscZawodow,
                PageNumber = pageNumber,
                PageSize = pageSize
            };


            return View(result);
        }

        public IActionResult WybierzKierunek(int? id, string searchString, int pageNumber = 1, int pageSize = 3)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            if (id == null)
            {
                return NotFound();
            }

            ViewBag.id = id;

            var kierunki = _kierunekRepository.PobierzWszystkieKierunki().Skip(ExcludeRecords).Take(pageSize);
            int ilosc = _kierunekRepository.PobierzWszystkieKierunki().Count();

            if (!String.IsNullOrEmpty(searchString))
            {

                var kierunki2 = _kierunekRepository.PobierzWszystkieKierunki().Where(k => k.Nazwa.Contains(searchString)).Skip(ExcludeRecords).Take(pageSize);



                int ilosc2 = _kierunekRepository.PobierzWszystkieKierunki().Where(k => k.Nazwa.Contains(searchString)).Count();

                //    // users = _context.User(s => (s.FirstName.Contains(searchString) || s.LastName.Contains(searchString)));

                var result2 = new PagedResult<Kierunek>
                    {
                       Data = kierunki2.ToList(),
                        //TotalItems = _context.User.Count(),
                        TotalItems = ilosc2,
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    };

                return View(result2);
            }

            var result = new PagedResult<Kierunek>
            {
                Data = kierunki.ToList(),
                TotalItems = ilosc,
                PageNumber = pageNumber,
                PageSize = pageSize
            };


            return View(result);




            
        }
        public IActionResult Dodaj()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dodaj(int id, int id2, Wynik wynik)
        {
            if(ModelState.IsValid)
            {
                Zawody zawody = _zawodyRepository.PobierzZawodyOId(id2);
                Kierunek kierunek = _kierunekRepository.PobierzKierunekOId(id);
                Wynik _wynik = new Wynik();
                _wynik.Punkty = wynik.Punkty;
                _wynik.Miejsce = wynik.Miejsce;
                _wynik.Kierunek = kierunek;
                _wynik.Zawody = zawody;

                _wynikRepository.DodajWynik(_wynik);

                return RedirectToAction("Index", "Zawody");
            }
            return View();

        }

        //public IActionResult Edytuj(int id, int id2)
        //{
        //    var zawody = _zawodyRepository.PobierzWszystkieZawody();
        //    var kierunki = _kierunekRepository.PobierzWszystkieKierunki();
        //    var wyniki = _wynikRepository.PobierzWszystkieWyniki();

        //    var wynik = from w in wyniki
        //                join z in zawody on w.Zawody equals z
        //                join k in kierunki on w.Kierunek equals k
        //                where z.Id == id && k.Id == id2
        //                select w.Id;

        //    int IdWynik = wynik.FirstOrDefault();

        //   Wynik edytowanyWynik =  _wynikRepository.PobierzWynikOId(IdWynik);



        //    //               var zawody = _wynikRepository.PobierzZawody(ExcludeRecords, pageSize);
        //    //var sporty = _sportRepository.PobierzWszystkieSporty();
        //    //int iloscZawodow = _wynikRepository.IloscZawodow();

        //    //var multipletable = from z in zawody
        //    //                    join s in sporty on z.Sport equals s
        //    //                    select new Zawody
        //    //                    {
        //    //                        Sport = s,
        //    //                        DataZawodow = z.DataZawodow,
        //    //                        Id = z.Id


        //    //                    };

        //    return View(edytowanyWynik);
        //}

        public IActionResult Edytuj(int id)
        {
            Wynik wynik = _wynikRepository.PobierzWynikOId(id);
            ViewBag.IdZawodow = (int)wynik.Id;
            return View(wynik);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edytuj(Wynik wynik)
        {
            if (ModelState.IsValid)
            {


                //var wyniki = _wynikRepository.PobierzWszystkieWyniki();
                //var zawody = _zawodyRepository.PobierzWszystkieZawody();

                //int idZawodow = Convert.ToInt32((from w in wyniki
                //                                join z in zawody on w.Zawody equals z
                //                                 where w.Id == wynik.Id
                //                                select z.Id).FirstOrDefault());


                //int idZawodow = ViewBag.IdZawodow;
                _wynikRepository.EdytujWynik(wynik);

                return RedirectToAction("Index", "Zawody");
            }
            return View();
           // return RedirectToAction("Szczegoly", "Zawody", new { id = idZawodow });
        }

        public IActionResult Usun(int id)
        {
            Wynik wynik = _wynikRepository.PobierzWynikOId(id);




            return View(wynik);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Usun(Wynik wynik)
        {

            //var wyniki = _wynikRepository.PobierzWszystkieWyniki();
            //var zawody = _zawodyRepository.PobierzWszystkieZawody();

            //int idZawodow = Convert.ToInt32((from w in wyniki
            //                 join z in zawody on w.Zawody equals z
            //                 where w.Id == wynik.Id
            //                 select z.Id).FirstOrDefault());

            //Wynik wynik2 = _wynikRepository.PobierzWynikOId(wynik.Id);




              _wynikRepository.UsunWynik(wynik);

            return RedirectToAction("Index", "Zawody");
        }

        public IActionResult Wroc()
        {

            return View();
        }


    }
}
