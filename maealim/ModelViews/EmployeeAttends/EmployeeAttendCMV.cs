﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.ModelViews.EmployeeAttends
{
    public class EmployeeAttendCMV
    {
        public int Id { get; set; }

        [Display(Name = "الموظف"), Required(ErrorMessage = "{0} مطلوب")]
        public int EmployeeId { get; set; }
     
        [Display(Name = "التاريخ"), Required(ErrorMessage = "{0} مطلوب"),
DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AttendDate { get; set; }

        [Display(Name = "العمل"), Required(ErrorMessage = "{0} مطلوب")]
        public string TheWork { get; set; }




    }
}
