using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class MessageSend
    {
        public MessageSend()
        {
            this.SendDate = DateTime.Now;
        }
        public int Id { get; set; }
        [Display(Name = "الوجيه"), Required(ErrorMessage = "{0} مطلوب")]
        public int NotableId { get; set; }
        [ForeignKey("NotableId"), Display(Name = " الوجيه")]
        public Notable Notable { get; set; }

        [Display(Name = "الرسالة"), Required(ErrorMessage = "{0} مطلوب")]
        public int WjhaaMessageId { get; set; }
        [ForeignKey("WjhaaMessageId"), Display(Name = " الرسالة")]
        public WjhaaMessage WjhaaMessage { get; set; }

        [Display(Name = "المرسل"), Required(ErrorMessage = "{0} مطلوب")]
        public int AppUserId { get; set; }
        [ForeignKey("AppUserId"), Display(Name = " المرسل")]
        public AppUser AppUser { get; set; }


        [Display(Name = " تاريخ الارسال"), Required(ErrorMessage = "{0} مطلوب"),
      DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
      DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SendDate { get; set; }




    }
}
