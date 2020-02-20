using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class Country
    {
        public int Id { get; set; }
        [Display(Name = "الدولة "), Required(ErrorMessage = "{0} مطلوب")]
        public string Name { get; set; }

        [Display(Name = "القارة "), Required(ErrorMessage = "{0} مطلوب")]
        public int ContinentId { get; set; }
        [Display(Name = "القارة "), ForeignKey("ContinentId")]
        public Continent Continent { get; set; }

        [Display(Name = "الحالة "), Required(ErrorMessage = "{0} مطلوب")]
        public bool Status { get; set; } // اظهار / مخفي

        public IEnumerable<MGuide> MGuides { get; set; }
        public IEnumerable<WjhaaMessage> WjhaaMessage { get; set; }
        public IEnumerable<Notable> Notables { get; set; }

    }
}
