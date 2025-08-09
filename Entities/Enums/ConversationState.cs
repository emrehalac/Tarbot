using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enums
{
    public enum ConversationState
    {
        NotStarted, // Henüz KVKK metni gösterilmedi

        AwaitingKvkkApproval, // KVKK onayı bekleniyor
        AwaitingKvkkRetryConfirmation, // Red durumunda kullanıcıya "tekrar görmek ister misiniz?" gibi sorulacak

        AwaitingCowCount, // "Kaç ineğiniz var?"
        AwaitingCowLabel, // "İneklerinizi etiketleyelim, 1. ineğinizin adı nedir?" gibi sorulacak
        AwaitingCowStatus, // Sıradaki ineğin gebe olup olmadığı soruluyor
        AwaitingGestationWeek, // Eğer gebe ise haftası soruluyor

        CowLoopCompleted, // Tüm inek bilgileri girildi (tamamlandı)
        AwaitingFinalConfirmation, // "Bilgiler doğru mu, tamam diyelim mi?" tarzı son onay ekranı 

        Completed, // Konuşma başarıyla tamamlandı
        CancelledByUser // Kullanıcı KVKK onaylamadı veya başka bir sebeple süreç iptal edildi
    }

}

