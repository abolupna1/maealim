using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class Notable
    {
        public int Id { get; set; }
        [Display(Name = "رقم الحجز"), Required(ErrorMessage = "{0} مطلوب")]
        public int GuestReservationId { get; set; }
        [ForeignKey("GuestReservationId"), Display(Name = " الحجز   ")]
        public GuestReservation GuestReservation { get; set; }


        [Display(Name = "الجنسية"), Required(ErrorMessage = "{0} مطلوب")]
        public int? CountryId { get; set; }
        [ForeignKey("CountryId"), Display(Name = " الجنسية   ")]
        public Country Country { get; set; }

        [Display(Name = "المنصب"), Required(ErrorMessage = "{0} مطلوب")]
        public int? JobNotableId { get; set; }
        [ForeignKey("JobNotableId"), Display(Name = " المنصب   ")]
        public JobNotable JobNotable { get; set; }


        [Display(Name = "الاسم"), Required(ErrorMessage = "{0} مطلوب")]
        public string Name  { get; set; }

        [EmailAddress(ErrorMessage = "{0} يجب ان يكون صالح")]
        [Display(Name = "الايميل"), Required(ErrorMessage = "{0} مطلوب")]
        public string Email  { get; set; }

        [Display(Name = "الجوال في بلدي"), Required(ErrorMessage = "{0} مطلوب")]
        public string MobileInMyCountry  { get; set; }

        [Display(Name = "الجوال في السعودية")]
        public string MobileInSaudi  { get; set; }



        public IEnumerable<MessageSend> MessageSends { get; set; }
    }
}
