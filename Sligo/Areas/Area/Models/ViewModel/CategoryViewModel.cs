using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sligo.Areas.Area.Models.ViewModel
{
    public class CategoryViewModel
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public List<CategoryViewModel> CategoryList { get; set; }
    }
}