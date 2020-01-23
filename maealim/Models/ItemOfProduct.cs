using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class ItemOfProduct
    {
        public int Id { get; set; }
        [Display(Name = "نوع الصنف "), Required(ErrorMessage = "{0} مطلوب")]
        public int? TypeOfProductId { get; set; }
        [Display(Name = "نوع الصنف "), ForeignKey("TypeOfProductId")]
        public TypeOfProduct TypeOfProduct { get; set; }

        [Display(Name = " الصنف"), MaxLength(100, ErrorMessage = "{0} طول النص 100 فقط"),
            MinLength(3, ErrorMessage = "{0} طول النص على الاقل  3 احرف "), Required(ErrorMessage = "{0} مطلوب")]
        public string Name { get; set; }

        public IEnumerable<ItemExport> ItemExports { get; set; }
        public IEnumerable<ItemImport> ItemImports { get; set; }
    }
}
