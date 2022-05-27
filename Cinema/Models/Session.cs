using System;
using System.Collections.Generic;

namespace Cinema.Models
{
    public partial class Session
    {
        public Session()
        {
            Ticket = new List<Ticket>();
        }

        public long SessionId { get; set; }
        public short? HallId { get; set; }
        public long FilmId { get; set; }
        public DateTime SessionDateTime { get; set; }

        public virtual Film Film { get; set; }
        public virtual Hall Hall { get; set; }
        public virtual List<Ticket> Ticket { get; set; }
    }
}
