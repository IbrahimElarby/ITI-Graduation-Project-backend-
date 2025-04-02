using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public ApplicationUser? User { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
