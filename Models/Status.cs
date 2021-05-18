using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGStore.Models
{
    public class Status : BaseModel
    {
        public Status()
        {
            Games = new HashSet<Game>();
        }

        [Required(ErrorMessage = Resourses.ERROR_IsEmpty)]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}