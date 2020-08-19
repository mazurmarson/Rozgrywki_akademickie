using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RozgrywkiAkademickie4.Models;
using RozgrywkiAkademickie4.PomoKlas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Controllers
{
    [Authorize]
    public class KierunekController : Controller
    {
        private readonly IKierunekRepository _kierunekRepository;

        public KierunekController(IKierunekRepository kierunekRepository)
        {
            _kierunekRepository = kierunekRepository;
        }

        //public IActionResult Index()
        //{

        //    return View(_kierunekRepository.PobierzWszystkieKierunki());
        //}

        public IActionResult Index(string searchString, int pageNumber = 1, int pageSize = 3)
        {

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;



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

        public IActionResult Szczegoly(int id)
        {
            var kierunek = _kierunekRepository.PobierzKierunekOId(id);
            if (kierunek == null)
                return NotFound();
           KierunekSzczegoly kierunekSzczegoly = new KierunekSzczegoly();

            kierunekSzczegoly.Nazwa = kierunek.Nazwa;
            kierunekSzczegoly.Rok = kierunek.Rok;
            kierunekSzczegoly.CzyBonus = kierunek.CzyBonus;

            if(kierunekSzczegoly.CzyBonus == true)
            {
                kierunekSzczegoly.BonusString = "Tak";
            }
            else
            {
                kierunekSzczegoly.BonusString = "Nie";
            }

            

            return View(kierunekSzczegoly);
        }

        public IActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dodaj(Kierunek kierunek)
        {
            if (ModelState.IsValid)
            {
                _kierunekRepository.DodajKierunek(kierunek);
                return RedirectToAction("Index");
            }
            return View(kierunek);
        }

        public IActionResult Usun(int Id)
        {
            try
            {
                var kierunek = _kierunekRepository.PobierzKierunekOId(Id);

                if (kierunek == null)
                    return NotFound();

                _kierunekRepository.UsunKierunek(kierunek);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains("statement conflicted with the reference constraint"))
                {
                    return Content("Kierunek który chcesz usunąć jest wykorzystywany przez jakiś wynik, usuń wynik by móc usunąć kierunek");
                }
                return Content(ex.ToString());
            }

        }


        public IActionResult Edytuj(int Id)
        {
            
            var kierunek = _kierunekRepository.PobierzKierunekOId(Id);

            if (kierunek == null)
                return NotFound();


            return View(kierunek);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edytuj(Kierunek kierunek)
        {
            if (ModelState.IsValid)
            {
                _kierunekRepository.EdytujKierunek(kierunek);
                return RedirectToAction(nameof(Index));
            }
            return View(kierunek);
        }



    }
}
