
using Entities.Enums;
using System;

namespace Entities.Entities
{
    public class Reminder : BaseEntity
    {
        public int InekId { get; set; }
        public virtual Cow Cow { get; set; }

        public DateTime ReminderDate { get; set; }
        public ReminderType Type { get; set; }
        public bool IsSent { get; set; } = false; // Kullanýcýya hatýrlatma gönderildi mi?
        public bool? UserAcknowledged { get; set; } // Kullanýcýdan geri cevap alýndý mý?
    }
}
