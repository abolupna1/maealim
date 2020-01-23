using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class ItemExport
    {
        public int Id { get; set; }
        [Display(Name = " الصنف "), Required(ErrorMessage = "{0} مطلوب")]
        public int? ItemOfProductId { get; set; }
        [Display(Name = " الصنف "), ForeignKey("ItemOfProductId")]
        public ItemOfProduct ItemOfProduct { get; set; }

        [Display(Name = " التاريخ"), Required(ErrorMessage = "{0} مطلوب"),
DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = " الكمية "), Required(ErrorMessage = "{0} مطلوب")]
        public int Qty { get; set; }

        [Display(Name = " المستلم "), Required(ErrorMessage = "{0} مطلوب")]
        public string Recipient { get; set; }


        [Display(Name = " الملاحظات ")]
        public string Notce { get; set; }

    }
}
