using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGStore.Models
{
    public class Genre : BaseModel
    {
        public Genre()
        {
            Games = new HashSet<Game>();
        }

        [Required(ErrorMessage = Resourses.ERROR_IsEmpty)]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}