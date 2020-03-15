using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class Attend
    {
        public int Id { get; set; }

        [Display(Name = "الموظف")]
        public int? EmployeeId { get; set; }
        [ForeignKey("EmployeeId"), Display(Name = " الموظف   ")]
        public Employee Employee { get; set; }

        [Display(Name = "عقد الموظف")]
        public int? EmployeeContractId { get; set; }
        [ForeignKey("EmployeeContractId"), Display(Name = " عقد الموظف   ")]
        public EmployeeContract EmployeeContract { get; set; }




        [Display(Name = "المرشد")]
        public int? GuideId { get; set; }
        [ForeignKey("GuideId"), Display(Name = " المرشد   ")]
        public MGuide Guide { get; set; }

        [Display(Name = "عقد المرشد")]
        public int? GuideContractId { get; set; }
        [ForeignKey("GuideContractId"), Display(Name = " عقد المرشد   ")]
        public GuideContract GuideContract { get; set; }


        [Display(Name = "التاريخ"), Required(ErrorMessage = "{0} مطلوب"),
DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AttendDate { get; set; }

        [Display(Name = "العمل"), Required(ErrorMessage = "{0} مطلوب")]
        public string TheWork { get; set; }

        [Display(Name = "مسؤول التحضير")]
        public int? AppUserId { get; set; }
        [ForeignKey("AppUserId"), Display(Name = " مسؤول التحضير   ")]
        public AppUser AppUser { get; set; }

        [Display(Name = "عدد ساعات العمل"),Required(ErrorMessage = "{0} مطلوب")]
        public double WorkingHours { get; set; }



    }
}
