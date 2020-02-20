using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.ModelViews.MessageSends
{
    public class MessageSendCMV
    {

        [Display(Name = "نوع الرسالة"), Required(ErrorMessage = "{0} مطلوب")]
        public int TypesMessageId { get; set; }


        [Display(Name = "الرسالة"), Required(ErrorMessage = "{0} مطلوب")]
        public int WjhaaMessageId { get; set; }

        [Display(Name = "الدولة"), Required(ErrorMessage = "{0} مطلوب")]
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        [Display(Name = "المرسل"), Required(ErrorMessage = "{0} مطلوب")]
        public int AppUserId { get; set; }


    }
}
