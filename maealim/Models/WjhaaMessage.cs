using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class WjhaaMessage
    {
        public int Id { get; set; }
        [Display(Name = "نوع الرسالة"), Required(ErrorMessage = "{0} مطلوب")]
        public int TypesMessageId { get; set; }
        [ForeignKey("TypesMessageId"), Display(Name = " نوع الرسالة   ")]
        public TypesMessage TypesMessage { get; set; }

        [Display(Name = "الدولة"), Required(ErrorMessage = "{0} مطلوب")]
        public int CountryId { get; set; }
        [ForeignKey("CountryId"), Display(Name = " الدولة   ")]
        public Country Country { get; set; }

        [Display(Name = "المترجم"), Required(ErrorMessage = "{0} مطلوب")]
        public int MGuideId { get; set; }
        [ForeignKey("MGuideId"), Display(Name = " المترجم   ")]
        public MGuide MGuide { get; set; }

        [Display(Name = "الرسالة"), Required(ErrorMessage = "{0} مطلوب")]
        public string Message { get; set; }

        [Display(Name = "الحالة"), Required(ErrorMessage = "{0} مطلوب")]
        public bool Status { get; set; }


        public IEnumerable<MessageSend> MessageSends { get; set; }


    }
}
