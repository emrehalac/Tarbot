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
    public class CowStatusHandler : IMessageHandler
    {
        private readonly IUserService _userService;
        private readonly ICowService _cowService;
        private readonly ITwilioRestClient _twilioRestClient;

        public ConversationState State => ConversationState.AwaitingCowStatus;


        public CowStatusHandler(IUserService userService, ITwilioRestClient twilioRestClient, ICowService cowService)
        {
            _userService = userService;
            _twilioRestClient = twilioRestClient;
            _cowService = cowService;
        }

        public async Task ProcessAsync(User user, string messageBody)
        {

            var cow = await _cowService.GetFirstUncompletedCowAsync(user.Id);
            if (cow == null) return;

            var status = ParsePregnancyStatus(messageBody);
            if (status == null) return;

            cow.Status = status.Value;

            if (status == PregnancyStatus.Pregnant)
            {
                // Gebeyse → haftasını soracağız, isCompleted = false kalır
                await _cowService.UpdateAsync(cow);
                await _userService.UpdateConversationStateAsync(user.Id, ConversationState.AwaitingGestationWeek);

                var contentSid = "HXb5fcbf89461ed39a71795d24ed046ddf"; 

                var contentVariable = new JsonObject
                {
                    ["1"] = cow.Label
                };

                await MessageResource.CreateAsync(
                    from: new PhoneNumber("whatsapp:+14155238886"),
                    to: new PhoneNumber(user.PhoneNumber),
                    contentSid: contentSid,
                    contentVariables: contentVariable.ToString(),
                    client: _twilioRestClient
                );
                return;
            }

            // Boş veya Buzağıladı ise: tamamlandı
            cow.IsCompleted = true;
            cow.UpdatedAt = DateTime.UtcNow;
            await _cowService.UpdateAsync(cow);

            bool allCompleted = await _cowService.AreAllCowsCompletedAsync(user.Id);

            if (allCompleted)
            {
                await _userService.UpdateConversationStateAsync(user.Id, ConversationState.CowLoopCompleted);

                var contentSid = "HX4a9aa33dea8c48a8a8737db7aa8145ea";
                var contentVars = new JsonObject
                {
                    ["1"] = user.ProfileName
                };

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

                int nextIndex = await _cowService.GetCompletedCowCountAsync(user.Id) + 1;

                var contentSid = "HXdbeba6fd62286f665ce1a766979070fa";
                var contentVars = new JsonObject
                {
                    ["1"] = nextIndex.ToString()
                };

                await MessageResource.CreateAsync(
                    from: new PhoneNumber("whatsapp:+14155238886"),
                    to: new PhoneNumber(user.PhoneNumber),
                    contentSid: contentSid,
                    contentVariables: contentVars.ToString(),
                    client: _twilioRestClient
                );
            }

        }

        private PregnancyStatus? ParsePregnancyStatus(string message)
        {
            message = message.ToLower().Trim();
            return message switch
            {
                "gebe" => PregnancyStatus.Pregnant,
                "boş" => PregnancyStatus.NotPregnant,
                "buzağıladı" => PregnancyStatus.Calved,
                _ => null
            };
        }
    }
}