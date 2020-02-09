using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class Sheikh
    {
        public int Id { get; set; }
        [Display(Name = "اسم الشيخ"), MaxLength(100, ErrorMessage = "{0} طول النص 100 فقط"),
    MinLength(5, ErrorMessage = "{0} طول النص على الاقل  5 احرف "), Required(ErrorMessage = "{0} مطلوب")]
        public string Name { get; set; }

        [Display(Name = "الجوال "), MaxLength(14, ErrorMessage = "{0} طول النص 14 فقط"),
             MinLength(10, ErrorMessage = "{0} طول النص على الاقل  10 احرف "),
            Required(ErrorMessage = "{0} مطلوب")]
        public string Mobile { get; set; }


        [Display(Name = "رقم الهوية "), MaxLength(10, ErrorMessage = "{0} طول النص 10 فقط"),
             MinLength(10, ErrorMessage = "{0} طول النص على الاقل  10 احرف ")
          ]
        public string CardId { get; set; }
        [Display(Name = "البريد الالكتروني "), MaxLength(100, ErrorMessage = "{0} طول النص 100 فقط"),
          MinLength(10, ErrorMessage = "{0} طول النص على الاقل  5 احرف ")
       ]
        public string Email { get; set; }
        [Display(Name = "الجوال "), MaxLength(100, ErrorMessage = "{0} طول النص 100 فقط"),
          MinLength(3, ErrorMessage = "{0} طول النص على الاقل  3 احرف ")
       ]
        public string Job { get; set; }


        [Display(Name = "متطوع"),  Required(ErrorMessage = "{0} مطلوب")]
        public bool Volunteer { get; set; }

        public IEnumerable<GuestReservation> GuestReservations { get; set; }



    }
}
