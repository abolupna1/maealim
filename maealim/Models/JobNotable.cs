using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class JobNotable
    {
        public int Id { get; set; }
        [Display(Name = "المنصب "), Required(ErrorMessage = "{0} مطلوب")]
        public string Name { get; set; }

        [Display(Name = "النوع "), Required(ErrorMessage = "{0} مطلوب")]
        public int TypeNotableId { get; set; }
        [Display(Name = "النوع "), ForeignKey("TypeNotableId")]
        public TypeNotable TypeNotable { get; set; }


    }
}
