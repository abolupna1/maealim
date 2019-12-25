using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class Season
    {
        public int Id { get; set; }

        [Display(Name = "الموسم"), Required(ErrorMessage = "{0} مطلوب")]
        public string Name { get; set; }
    }
}
