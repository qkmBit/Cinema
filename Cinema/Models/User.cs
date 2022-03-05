using System;
using System.Collections.Generic;

namespace Cinema.Models
{
    public partial class User
    {
        public User()
        {
            UserOrders = new HashSet<UserOrders>();
        }

        public int UserId { get; set; }
        public string UserPhone { get; set; }
        public string? UserEmail { get; set; }
        public bool? EmailStatus { get; set; }
        public string HashPassword { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual UserConfirm UserConfirm { get; set; }
        public virtual ICollection<UserOrders> UserOrders { get; set; }
    }
}
