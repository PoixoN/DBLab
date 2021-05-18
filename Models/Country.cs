using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheGStore.Models
{
    public class Country : BaseModel
    {
        public Country()
        {
            Developers = new HashSet<Developer>();
        }

        [Required(ErrorMessage = Resourses.ERROR_IsEmpty)]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        public virtual ICollection<Developer> Developers { get; set; }
    }
}
