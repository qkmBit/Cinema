using System;
using System.Collections.Generic;

namespace Cinema.Models
{
    public partial class Hall
    {
        public Hall()
        {
            Session = new HashSet<Session>();
        }

        public short HallId { get; set; }
        public string HallName { get; set; }
        public string HallType { get; set; }
        public int HallSetCount { get; set; }

        public virtual ICollection<Session> Session { get; set; }
    }
}
