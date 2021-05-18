using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGStore.Models
{
    public class Game : BaseModel
    {
        public Game()
        {
            Orders = new HashSet<Order>();
        }

        [Required(ErrorMessage = Resourses.ERROR_IsEmpty)]
        [Display(Name = "Жанр")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = Resourses.ERROR_IsEmpty)]
        [Display(Name = "Статус")]
        public int StatusId { get; set; }

        [Required(ErrorMessage = Resourses.ERROR_IsEmpty)]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Required(ErrorMessage = Resourses.ERROR_IsEmpty)]
        [RegularExpression(Resourses.REG_EXP_Price, ErrorMessage = Resourses.ERROR_IncorectPrice)]
        [Display(Name = "Вартість")]
        public decimal Price { get; set; }

        [Display(Name = "Розробник")]
        public int DeveloperId { get; set; }

        [Required(ErrorMessage = Resourses.ERROR_IsEmpty)]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        [Display(Name = "Жанр")]
        public virtual Genre Genre { get; set; }

        [Display(Name = "Статус")]
        public virtual Status Status { get; set; }

        [Display(Name = "Розробник")]
        public virtual Developer Developer { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
