using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Models
{
    public class MGuide
    {
        public int Id { get; set; }
        [Display(Name = "الاسم"), Required(ErrorMessage = "{0} مطلوب")]
        public string Name { get; set; }

        [Display(Name = "الجنسية")]
        public int? CountryId { get; set; }
        [ForeignKey("CountryId"), Display(Name = " الجنسية   ")]
        public Country Country { get; set; }

        [Display(Name = "البنك")]
        public int? BankId { get; set; }
        [ForeignKey("BankId"), Display(Name = " البنك   ")]
        public Bank Bank { get; set; }


        [Display(Name = "الجامعة")]
        public int? UniversityId { get; set; }
        [ForeignKey("UniversityId"), Display(Name = " الجامعة   ")]
        public University University { get; set; }


        [Display(Name = "المرحلة")]
        public int? StageId { get; set; }
        [ForeignKey("StageId"), Display(Name = " المرحلة   ")]
        public Stage Stage { get; set; }


        [Display(Name = "المستوى")]
        public int? LevelId { get; set; }
        [ForeignKey("LevelId"), Display(Name = " المستوى   ")]
        public Level Level { get; set; }


        [Display(Name = "اللغة")]
        public int? LanguageId { get; set; }
        [ForeignKey("LanguageId"), Display(Name = " اللغة   ")]
        public Language Language { get; set; }

        [Display(Name = "التخصص")]
        public int? SpecializationId { get; set; }
        [ForeignKey("SpecializationId"), Display(Name = " التخصص   ")]
        public Specialization Specialization { get; set; }

        [Display(Name = "المستخدم")]
        public int? AppUserId { get; set; }
        [ForeignKey("AppUserId"), Display(Name = " المستخدم   ")]
        public AppUser AppUser { get; set; }

        [Display(Name = "الكلية")]
        public int? CollegeId { get; set; }
        [ForeignKey("CollegeId"), Display(Name = " الكلية   ")]
        public College College { get; set; }

        [Display(Name = "الإيميل")]
        public string Email { get; set; }

        [Display(Name = "الجوال")]
        public string Mobile { get; set; }


        public IEnumerable<GuestReservation> GuestReservations { get; set; }
        public IEnumerable<Notable> Notables { get; set; }
        public IEnumerable<WjhaaMessage> WjhaaMessage { get; set; }
        public IEnumerable<Attend> Attends { get; set; }
        public IEnumerable<GuideContract> GuideContracts { get; set; }


    }
}
