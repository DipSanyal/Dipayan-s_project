using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dipayan_project.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage="Category Name is Required")]
        public string CategoryName { get; set; }

    }
}