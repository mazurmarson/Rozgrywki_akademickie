using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Controllers
{
    public class BladController : Controller
    {
        public IActionResult Index(string blad)
        {
            return View(blad);
        }
    }
}
