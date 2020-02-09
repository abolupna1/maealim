using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class GuestReservation
    {
        public int Id { get; set; }
        [Display(Name = "المرشد")]
        public int? MGuideId { get; set; }
        [ForeignKey("MGuideId"), Display(Name = " المرشد   ")]
        public MGuide MGuide { get; set; }
        [Display(Name = " الشيخ   ") ]
        public int? SheikhId { get; set; }
        [ForeignKey("SheikhId"), Display(Name = " الشيخ   ")]
        public Sheikh Sheikh { get; set; }

        [Display(Name = "التاريخ"), Required(ErrorMessage = "{0} مطلوب"),
DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReservationDate { get; set; }

        [Display(Name = "الفترة")]
        public string Duration { get; set; }

        [Display(Name = "رقم الجلسة")]
        public int? SessionNo { get; set; }

        [Display(Name = "حالة الحجز")]
        public int? Status { get; set; }

        [Display(Name = "ملاحظات")]
        public string Notce { get; set; }
        public IEnumerable<Notable> Notables { get; set; }
        public IEnumerable<Gift> Gifts { get; set; }


    }
}
