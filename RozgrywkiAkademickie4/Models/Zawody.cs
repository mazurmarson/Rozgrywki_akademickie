using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Models
{
    public class Zawody
    {

        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Data zawodow jest wymagana.")]
        public DateTime DataZawodow { get; set; }
        //[Required(ErrorMessage = "Sport jest wymagany")]
        public Sport Sport { get; set; }

        public string ZdjecieUrl { get; set; }
    


    }
}
