using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Display(Name = "الإسم"), StringLength(50, MinimumLength = 3, 
            ErrorMessage ="{0} طول النص يجب ان يكون بين " +
            "{1} و" +
            "{2}"),Required(ErrorMessage = "{0} مطلوب")]
        public string Name { get; set; }

        [Display(Name = "رقم الجوال"), StringLength(10, MinimumLength = 10,
            ErrorMessage = "{0} طول النص يجب ان يكون بين " +
            "{1} و" +
            "{2}"), Required(ErrorMessage = "{0} مطلوب")]
        public string Mobile { get; set; }

        [Display(Name = "البريد الإلكتروني"), StringLength(100, MinimumLength = 3,
        ErrorMessage = "{0} طول النص يجب ان يكون بين " +
        "{1} و" +
        "{2}"), Required(ErrorMessage = "{0} مطلوب"), EmailAddressAttribute(ErrorMessage ="{0} البريد الإلكتروني غير صالح")]
        public string Email { get; set; }


        [Display(Name = "الوظيفة "),  Required(ErrorMessage = "{0} مطلوب")]
        public int JopId { get; set; }
        [Display(Name = "الوظيفة "), ForeignKey("JopId")]
        public Jop Jop { get; set; }

        [Display(Name = "المستخدم ")]
        public int? UserId { get; set; } = null;
        [Display(Name = "المستخدم "), ForeignKey("UserId")]
        public AppUser User { get; set; }

        public IEnumerable<EmployeeContract> EmployeeContracts { get; set; }
        public IEnumerable<Attend> Attends { get; set; }
    }
}
