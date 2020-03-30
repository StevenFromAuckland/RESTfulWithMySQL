﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HillLabTestEntities
{
  
    [Table("product")]
    public class Product
    {
        [Key]
        [Display(Name = "ID")]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public double Quantity { get; set; }

        [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
        public string Unit { get; set; }

        [ForeignKey("fk_product_category")]
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category ID is required")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
 
}
