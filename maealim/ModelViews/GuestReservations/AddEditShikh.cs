using System.ComponentModel.DataAnnotations;


namespace maealim.ModelViews.GuestReservations
{
    public class AddEditShikh
    {

        public int Id { get; set; }

        [Display(Name = " الشيخ   "), Required(ErrorMessage = "{0} مطلوب")]
        public int? SheikhId { get; set; }

        [Display(Name = "رقم الجلسة"), Required(ErrorMessage = "{0} مطلوب")]
        public int SessionNo { get; set; }

    }
}
