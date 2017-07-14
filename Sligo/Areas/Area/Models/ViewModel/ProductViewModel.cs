using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sligo.Areas.Area.Models.ViewModel
{
    public class ProductViewModel
    {
        
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Model { get; set; }

        public string ReleasedDate { get; set; }

        public int ReleasedYear { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime SearchProductFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime SearchProductTo { get; set; }

        public List<ProductViewModel> ProductList { get; set; }

        public Product Product { get; set; }


    }
}