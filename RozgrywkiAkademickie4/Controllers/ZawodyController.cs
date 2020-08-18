using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using RozgrywkiAkademickie4.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RozgrywkiAkademickie4.ViewsModel;
using RozgrywkiAkademickie4.PomoKlas;
using Microsoft.AspNetCore.Authorization;

namespace RozgrywkiAkademickie4.Controllers
{
    public class ZawodyController : Controller
    {
        private readonly IZawodyRepository _zawodyRepository;
        private readonly IKierunekRepository _kierunekRepository;
        private readonly ISportRepository _sportRepository;
        private readonly IWynikRepository _wynikRepository;
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public object Context { get; private set; }

        public ZawodyController(IZawodyRepository zawodyRepository, IKierunekRepository kierunekRepository, ISportRepository sportRepository, AppDbContext dbContext, IWebHostEnvironment webHostEnvironment, IWynikRepository wynikRepository)
        {
            _zawodyRepository = zawodyRepository;
            _sportRepository = sportRepository;
            _kierunekRepository = kierunekRepository;
            _wynikRepository = wynikRepository;
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index(string searchString, int pageNumber = 1, int pageSize = 3)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var zawody = _zawodyRepository.PobierzWszystkieZawody();
            var sporty = _sportRepository.PobierzWszystkieSporty();

            var multipletable = from z in zawody
                                join s in sporty on z.Sport equals s
                                select new Zawody
                                {
                                    Sport = s,
                                    DataZawodow = z.DataZawodow,
                                    Id = z.Id,
                                    ZdjecieUrl = z.ZdjecieUrl


                                };
            if (!String.IsNullOrEmpty(searchString))
            {
                var multipletable2 = multipletable.Where(m => m.Sport.Nazwa.Contains(searchString)).Skip(ExcludeRecords).Take(pageSize);
                int ilosc2 = multipletable2.Count();

                var result2 = new PagedResult<Zawody>
                {
                    Data = multipletable2.ToList(),
                    TotalItems = ilosc2,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                    return View(result2);
            
            }

                int ilosc = multipletable.Count();

            multipletable = multipletable.OrderByDescending(m => m.DataZawodow);
            multipletable = multipletable.Skip(ExcludeRecords).Take(pageSize);

            var result = new PagedResult<Zawody>
            {
                Data = multipletable.ToList(),
                TotalItems = ilosc,
                PageNumber = pageNumber,
                PageSize = pageSize
            };


            return View(result);
        }



        //public IActionResult Dodaj(int id, string FileName)
        //{

        //    ustawId(id);

        //    if (!string.IsNullOrEmpty(FileName))
        //    {
        //        ViewBag.ImgPath = "/Images/" + FileName;
        //    }
        //    return View();


        //}



        [Authorize]
        public IActionResult Dodaj2(int id)
        {


            return View();


        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dodaj2(int id, ZawodyViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                Sport sport = _sportRepository.PobierzSportOId(id);
                Zawody zawody = new Zawody
                {
                    DataZawodow = model.DataZawodow,
                    ZdjecieUrl = uniqueFileName,

                    Sport = sport,


                };
                _zawodyRepository.DodajZawody(zawody);


                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [Authorize]
        private string UploadedFile(ZawodyViewModel model)
        {
            string uniqueFileName = null;

            if (model.ZdjecieUrl != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ZdjecieUrl.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ZdjecieUrl.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }



        //public IActionResult Dodaj(string FileName)
        //{



        //    if (!string.IsNullOrEmpty(FileName))
        //    {
        //        ViewBag.ImgPath = "/Images/" + FileName;
        //    }
        //    return View();


        //}



        // [HttpPost]
        // public IActionResult Dodaj(int id, string FileName,Zawody _zawody)
        // {

        ////     public IActionResult Dodaj(int id, [Bind("Id, DataZawodow")] Zawody zawody)
        //     Sport sport =_sportRepository.PobierzSportOId(id);
        //     //      DateTime date1 = new DateTime(2008, 6, 1, 7, 47, 0);


        //     //To ponizej przy edycji
        //     //  return RedirectToAction(nameof(Edit), new { FileName = Convert.ToString(form.Files[0].FileName), id = Convert.ToString(form["Id"]) });


        //     Zawody zawody = new Zawody();
        //     zawody.Sport = sport;
        //     zawody.DataZawodow = _zawody.DataZawodow;


        //  //   zawody.DataZawodow = date1;
        //     _zawodyRepository.DodajZawody(zawody);



        //     return View(zawody);
        // }
        [Authorize]
        public IActionResult WybierzSport(string searchString, int pageNumber = 1, int pageSize = 3)
        {

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;


            // var users = _context.User.Skip(ExcludeRecords).Take(pageSize);

            var sporty = _zawodyRepository.PobierzSporty(ExcludeRecords, pageSize);
            int iloscSportow = _zawodyRepository.IloscSportow();


            if (!String.IsNullOrEmpty(searchString))
            {

                var sporty2 = _sportRepository.PobierzWszystkieSporty().Where(s => s.Nazwa.Contains(searchString)).Skip(ExcludeRecords).Take(pageSize);
                int iloscSportow2 = _sportRepository.PobierzWszystkieSporty().Where(s => s.Nazwa.Contains(searchString)).Count();
                

          

                
                    var result2 = new PagedResult<Sport>
                    {
                        Data = sporty2.ToList(),
                        //TotalItems = _context.User.Count(),
                        TotalItems = iloscSportow2,
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    };

                return View(result2);
            }

            var result = new PagedResult<Sport>
            {
                Data = sporty.ToList(),
                TotalItems = iloscSportow,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        //[HttpPost("UploadFile")]
        //public async Task<IActionResult> UploadFile(int id, IFormCollection form)
        //{
        //    var webRoot = _env.WebRootPath;
        //    var filePath = Path.Combine(webRoot.ToString() + "\\images\\" + form.Files[0].FileName);

        //    if (form.Files[0].FileName.Length > 0)
        //    {
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await form.Files[0].CopyToAsync(stream);
        //        }
        //    }

        //    if (Convert.ToString(form["Id"]) == string.Empty || Convert.ToString(form["Id"]) == "0")
        //    {
        //        return RedirectToAction(nameof(Dodaj), new { id , FileName = Convert.ToString(form.Files[0].FileName)});
        //    }
        //    //To ponizej przy edycji
        //    //  return RedirectToAction(nameof(Edit), new { FileName = Convert.ToString(form.Files[0].FileName), id = Convert.ToString(form["Id"]) });
        //    return View();
        //}

        //private void ustawId(int Id)
        //{
        //    IdSport = Id;
        //}

        //private int pobierzId()
        //{
        //    return IdSport;
        //}




        public IActionResult Szczegoly(int id)
        {
            ViewBag.Id = id;
            var kierunki = _kierunekRepository.PobierzWszystkieKierunki();
            var zawody = _zawodyRepository.PobierzWszystkieZawody();
            var sporty = _sportRepository.PobierzWszystkieSporty();
            var wyniki = _wynikRepository.PobierzWszystkieWyniki();

            var multipletable = from w in wyniki
                                join z in zawody on w.Zawody equals z
                                join s in sporty on z.Sport equals s
                                join k in kierunki on w.Kierunek equals k
                                where z.Id == id
                                select new TabelaZawody
                                {
                                    Data = z.DataZawodow,
                                    NazwaK = k.Nazwa,
                                    Rok = k.Rok,
                                    NazwaS = s.Nazwa,
                                    Miejsce = w.Miejsce,
                                    Punkty = w.Punkty,
                                    Id = w.Id


                                };

            multipletable = multipletable.OrderBy(x => x.Miejsce);

            return View(multipletable);
        }

        [Authorize]
        public IActionResult Usun(int id)
        {
            Zawody zawody = _zawodyRepository.PobierzZawodyOId(id);
            

            return View(zawody);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Usun(Zawody zawody)
        {
            
            _zawodyRepository.UsunZawody(zawody);

            return RedirectToAction("Index");
        }

    }
}
