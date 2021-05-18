﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGStore.Models
{
    public class Developer : BaseModel
    {
        public Developer()
        {
            Games = new HashSet<Game>();
        }

        [Display(Name = "Країна")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = Resourses.ERROR_IsEmpty)]
        [Display(Name = "Ім\'я")]
        public string Name { get; set; }

        [Display(Name = "Країна")]
        public virtual Country Country { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
