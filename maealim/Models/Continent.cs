﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class Continent
    {
        public int Id { get; set; }

        [Display(Name = "القارة"), MaxLength(100, ErrorMessage = "{0} طول النص 100 فقط"),
            MinLength(3, ErrorMessage = "{0} طول النص على الاقل  3 احرف "), Required(ErrorMessage = "{0} مطلوب")]
        public string Name { get; set; }
    }
}
