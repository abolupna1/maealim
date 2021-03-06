﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class WorkSeason
    {
        public int Id { get; set; }
        [Display(Name = "الموسم "), Required(ErrorMessage = "{0} مطلوب")]
        public int SeasonId { get; set; }
        [Display(Name = "الموسم "), ForeignKey("SeasonId")]
        public Season Season { get; set; }


        [Display(Name = " من تاريخ "), Required(ErrorMessage = "{0} مطلوب"),
   DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
   DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = " الى تاريخ "), Required(ErrorMessage = "{0} مطلوب"),
DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "الحالة   "), Required(ErrorMessage = "{0} مطلوب")]
        public bool Status { get; set; } = true;
    }
}
