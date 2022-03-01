using System;
using System.Collections.Generic;

namespace Cinema.Models
{
    public partial class UserOrders
    {
        public UserOrders()
        {
            OrderTickets = new HashSet<OrderTickets>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderTickets> OrderTickets { get; set; }
    }
}
