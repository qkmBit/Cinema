using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Cinema.Models
{
    public partial class Users: IdentityUser
    {
        public Users()
        {
            UserOrders = new HashSet<UserOrders>();
        }
        public virtual ICollection<UserOrders> UserOrders { get; set; }
    }
}
