
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
        public bool IsSent { get; set; } = false; // Kullan�c�ya hat�rlatma g�nderildi mi?
        public bool? UserAcknowledged { get; set; } // Kullan�c�dan geri cevap al�nd� m�?
    }
}
