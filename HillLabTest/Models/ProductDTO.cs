using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HillLabTest.Models
{
  
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double Quantity { get; set; }

        public string Unit { get; set; }

        public int CategoryId { get; set; }
    }
}
