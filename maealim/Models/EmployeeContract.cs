using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class EmployeeContract
    {
        public int Id { get; set; }
        [Display(Name = "الموسم"), Required(ErrorMessage = "{0} مطلوب")]
        public int SeasonId { get; set; }
        [ForeignKey("SeasonId"), Display(Name = " الموسم   ")]
        public Season Season { get; set; }

        [Display(Name = "الموظف"), Required(ErrorMessage = "{0} مطلوب")]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId"), Display(Name = " الموظف   ")]
        public Employee Employee { get; set; }

        [Display(Name = "الوظيفة"), Required(ErrorMessage = "{0} مطلوب")]
        public int JopId { get; set; }
        [ForeignKey("JopId"), Display(Name = " الوظيفة   ")]
        public Jop Jop { get; set; }
        
        [Display(Name = "ساعات العمل اليومية"), Required(ErrorMessage = "{0} مطلوب")]
        public int DailyWorkingHours { get; set; }

        [Display(Name = "اجرة الساعة"), Required(ErrorMessage = "{0} مطلوب")]
        public float HourlyPay { get; set; }

        [Display(Name = "المبلغ الإضافي "), Required(ErrorMessage = "{0} مطلوب")]
        public float Extra { get; set; }

        [Display(Name = "من تاريخ"), Required(ErrorMessage = "{0} مطلوب"),
      DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
      DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [Display(Name = "الى تاريخ"), Required(ErrorMessage = "{0} مطلوب"),
      DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
      DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }
        [Display(Name = "الحالة"), Required(ErrorMessage = "{0} مطلوب")]
        public bool Status { get; set; }

        public IEnumerable<Attend> Attends { get; set; }

    }
}
