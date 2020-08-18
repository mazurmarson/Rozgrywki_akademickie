using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RozgrywkiAkademickie4.Models
{
    public class Sport
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa sportu jest wymagana")]
        [StringLength(100, ErrorMessage = "Nazwa sportu jest za długa")]
        [RegularExpression(@"^[A-Z][a-z]([a-z]| ?)*",
         ErrorMessage = "Nazwa sportu musi zaczynać sie z wielkiej litery i nie może zawierać liczb.")]
        public string Nazwa { get; set; }
    }
}
