using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.ModelViews.WorkSeasons
{
    public class WorkSeasonEMV
    {
        public int Id { get; set; }
        [Display(Name = "الموسم "), Required(ErrorMessage = "{0} مطلوب")]
        public int SeasonId { get; set; }
  


        [Display(Name = " من تاريخ "), Required(ErrorMessage = "{0} مطلوب"),
   DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
   DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = " الى تاريخ "), Required(ErrorMessage = "{0} مطلوب"),
DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "الحالة   "), Required(ErrorMessage = "{0} مطلوب")]
        public bool Status { get; set; } = true;
    }
}
