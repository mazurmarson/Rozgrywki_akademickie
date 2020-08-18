using Microsoft.AspNetCore.Http;
using RozgrywkiAkademickie4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.ViewsModel
{
    public class ZawodyViewModel
    {
        [DataType(DataType.Date)]
        public DateTime DataZawodow { get; set; }
        public Sport Sport { get; set; }

        public IFormFile ZdjecieUrl { get; set; }
        public string MiniaturkaUrl { get; set; }
    }
}
