using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public partial class UserOrders
    {
        public UserOrders()
        {
            Tickets = new List<Ticket>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Users Use { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
    }
}
