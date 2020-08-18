using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RozgrywkiAkademickie4.Models;

namespace RozgrywkiAkademickie4.Controllers
{
    public class SportController : Controller
    {
        private readonly ISportRepository _sportRepository;

        public SportController(ISportRepository sportRepository)
        {
            _sportRepository = sportRepository;
        }
        public IActionResult Index()
        {
            return View(_sportRepository.PobierzWszystkieSporty());
        }

        public IActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dodaj(Sport sport)
        {
            if (ModelState.IsValid)
            {
                _sportRepository.DodajSport(sport);
                return RedirectToAction("Index");
            }
            return View(sport);
        }




        public IActionResult Edytuj(int Id)
        {
            var sport = _sportRepository.PobierzSportOId(Id);

            if (sport == null)
                return NotFound();


            return View(sport);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edytuj(Sport sport)
        {
            if (ModelState.IsValid)
            {
                _sportRepository.EdytujSport(sport);
                return RedirectToAction(nameof(Index));
            }
            return View(sport);
        }

        public IActionResult Usun(int id)
        {
            Sport sport = _sportRepository.PobierzSportOId(id);


            return View(sport);
        }
        [HttpPost]
        public IActionResult Usun(Sport sport)
        {

            _sportRepository.UsunSport(sport);

            return RedirectToAction("Index");
        }

    }
}
