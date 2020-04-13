using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dipayan_project.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is Required")]
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category category { get; set; }
     
        
    }
}