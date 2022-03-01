using System;
using System.Collections.Generic;

namespace Cinema.Models
{
    public partial class UserConfirm
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string Token { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual User User { get; set; }
    }
}
