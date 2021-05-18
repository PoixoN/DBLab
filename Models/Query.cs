using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheGStore.Models
{
    public class Query
    {
        public string QueryId { get; set; }

        public string Error { get; set; }

        public int ErrorFlag { get; set; }

        public string DevName { get; set; }

        public string CountryName { get; set; }

        public decimal MaxPrice { get; set; }

        public List<string> CustNames { get; set; } = new List<string>();

        public List<string> CustLastNames { get; set; } = new List<string>();

        public List<string> CustEmails { get; set; } = new List<string>();

        public List<string> ProdNames { get; set; } = new List<string>();

        public List<decimal> ProdPrices { get; set; } = new List<decimal>();

        public string CustName { get; set; }

        public string CustSurname { get; set; }

        public string CustEmail { get; set; }

        public List<string> DevNames { get; set; } = new List<string>();

        [Required(ErrorMessage = Resourses.ERROR_IsEmpty)]
        [RegularExpression(Resourses.REG_EXP_Price, ErrorMessage = Resourses.ERROR_IncorectPrice)]
        [Display(Name = "Вартість В")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = Resourses.ERROR_IsEmpty)]
        public string ProdName { get; set; }

        public string GenreName { get; set; }

        public string StatusName { get; set; }

        public int DevId { get; set; }

        public List<string> CountryNames { get; set; } = new List<string>();

        public List<string> GenreNames { get; set; } = new List<string>();

        public List<string> StatusNames { get; set; } = new List<string>();
    }
}
