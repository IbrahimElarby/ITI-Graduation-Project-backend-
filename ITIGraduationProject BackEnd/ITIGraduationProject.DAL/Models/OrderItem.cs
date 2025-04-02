using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public Order? Order { get; set; }
        public int IngredientID { get; set; }
        public Ingredient? Ingredient { get; set; }
        public decimal Quantity { get; set; }
    }
}
