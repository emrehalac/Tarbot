using Business.Abstract;
using Entities.Entities;
using Entities.Enums;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;
using Twilio.Types;

namespace Business.Handlers
{
    public class CowGestationWeekHandler : IMessageHandler
    {
        private readonly IUserService _userService;
        private readonly ICowService _cowService;
        private readonly ITwilioRestClient _twilioRestClient;

        public ConversationState State => ConversationState.AwaitingGestationWeek;


        public CowGestationWeekHandler(IUserService userService, ITwilioRestClient twilioRestClient, ICowService cowService)
        {
            _userService = userService;
            _twilioRestClient = twilioRestClient;
            _cowService = cowService;
        }

        public async Task ProcessAsync(User user, string messageBody)
        {
            // 1) Aktif (tamamlanmamış) ineği al
            var cow = await _cowService.GetFirstUncompletedCowAsync(user.Id);
            if (cow == null) return; // happy path dışı

            // 2) Haftayı al (happy path: sayı)
            int week = int.Parse(messageBody.Trim());

            // 3) Tarih hesapla ve tamamla
            cow.PregnancyStartDate = DateTime.UtcNow.AddDays(-7 * week);
            cow.IsCompleted = true;
            cow.UpdatedAt = DateTime.UtcNow;
            await _cowService.UpdateAsync(cow);

            // 4) Hepsi tamam mı?
            bool allCompleted = await _cowService.AreAllCowsCompletedAsync(user.Id);

            if (allCompleted)
            {
                await _userService.UpdateConversationStateAsync(user.Id, ConversationState.CowLoopCompleted);

                // Loop Completed template (kullanıcıya genel bitiş)
                var contentSid = "HX4a9aa33dea8c48a8a8737db7aa8145ea"; 
                var contentVars = new JsonObject { ["1"] = user.ProfileName };

                await MessageResource.CreateAsync(
                    from: new PhoneNumber("whatsapp:+14155238886"),
                    to: new PhoneNumber(user.PhoneNumber),
                    contentSid: contentSid,
                    contentVariables: contentVars.ToString(),
                    client: _twilioRestClient
                );
            }
            else
            {
                await _userService.UpdateConversationStateAsync(user.Id, ConversationState.AwaitingCowLabel);

                // Sıradaki ineğin adını sor (dinamik index ile)
                int nextIndex = await _cowService.GetCompletedCowCountAsync(user.Id) + 1;

                var contentSid = "HXdbeba6fd62286f665ce1a766979070fa"; 
                var contentVars = new JsonObject { ["1"] = nextIndex.ToString() };

                await MessageResource.CreateAsync(
                    from: new PhoneNumber("whatsapp:+14155238886"),
                    to: new PhoneNumber(user.PhoneNumber),
                    contentSid: contentSid,
                    contentVariables: contentVars.ToString(),
                    client: _twilioRestClient
                );
            }
        }
    }
}