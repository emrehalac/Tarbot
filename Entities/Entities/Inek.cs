using Entities.Enums;
using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class Inek : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public string Label { get; set; }
        public PregnancyStatus Status { get; set; }
        public DateTime? PregnancyStartDate { get; set; }

        public virtual List<Reminder> Reminders { get; set; } = new List<Reminder>();
    }
}
