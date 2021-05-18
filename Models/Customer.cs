using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGStore.Models
{
    public class Customer : BaseModel
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Required(ErrorMessage = Resourses.ERROR_IsEmpty)]
        [Display(Name = "Ім\'я")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = Resourses.ERROR_IsEmpty)]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Required(ErrorMessage = Resourses.ERROR_IsEmpty)]
        [RegularExpression(Resourses.REG_EXP_Email, ErrorMessage = Resourses.ERROR_IncorectEmail)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
