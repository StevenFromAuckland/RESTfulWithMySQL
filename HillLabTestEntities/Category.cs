using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HillLabTestEntities
{

    //insert into productdb.category values(1, 'Food'), (2, 'Beverage'), (3, 'Vegetable');
    //insert into productdb.product(ProductId, ProductName, Quantity, Unit, CategoryId) values(1, 'Rice', 10, 'bag', 1), (2, 'Orange Juice', 20, 'bottle', 2), (3, 'Totato', 30, 'kg', 3);


    [Table("category")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters")]
        public string CategoryName { get; set; }

    }
 
}
