using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enums
{
    public enum ConversationState
    {
        NewUser = 0,              // Sohbete yeni başlayan kullanıcı
        AwaitingCowCount = 1,     // İnek sayısı soruluyor
        AwaitingCowStatus = 2,    // Belirli bir ineğin durumu soruluyor
        AwaitingPregnancyWeek = 3,// Gebelik haftası soruluyor
        Completed = 4
    }
}
