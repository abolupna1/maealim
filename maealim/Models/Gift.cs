using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class Gift
    {
        public int Id { get; set; }
        [Display(Name = " الصنف "), Required(ErrorMessage = "{0} مطلوب")]
        public int ItemOfProductId { get; set; }
        [Display(Name = " الصنف "), ForeignKey("ItemOfProductId")]
        public ItemOfProduct ItemOfProduct { get; set; }

        [Display(Name = "رقم الحجز"), Required(ErrorMessage = "{0} مطلوب")]
        public int GuestReservationId { get; set; }
        [ForeignKey("GuestReservationId"), Display(Name = " الحجز   ")]
        public GuestReservation GuestReservation { get; set; }

        [Display(Name = " الكمية "), Required(ErrorMessage = "{0} مطلوب")]
        public int Qty { get; set; }

        [Display(Name = " الملاحظات ")]
        public string Notce { get; set; }


    }
}
