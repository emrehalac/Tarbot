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
    public class CowCountHandler : IMessageHandler
    {
        private readonly IUserService _userService;
        private readonly ICowService _cowService;
        private readonly ITwilioRestClient _twilioRestClient;

        public ConversationState State => ConversationState.AwaitingCowCount;


        public CowCountHandler(IUserService userService, ITwilioRestClient twilioRestClient, ICowService cowService)
        {
            _userService = userService;
            _twilioRestClient = twilioRestClient;
            _cowService = cowService;
        }

        public async Task ProcessAsync(User user, string messageBody)
        {
            if (int.TryParse(messageBody, out int cowCount) && cowCount > 0)
            {

                await _cowService.CreateEmptyCowsAsync(user.Id, cowCount);


                await _userService.UpdateConversationStateAsync(user.Id, ConversationState.AwaitingCowLabel);

                int completedCount = await _cowService.GetCompletedCowCountAsync(user.Id);
                int displayIndex = completedCount + 1;

                var contentSid = "HXdbeba6fd62286f665ce1a766979070fa"; // Twilio'da onaylanmış template SID'i
                var contentVariables = new JsonObject
                {
                    ["1"] = displayIndex.ToString()
                };

                await MessageResource.CreateAsync(
                    from: new PhoneNumber("whatsapp:+14155238886"),
                    to: new PhoneNumber(user.PhoneNumber),
                    contentSid: contentSid,
                    contentVariables: contentVariables.ToString(),
                    client: _twilioRestClient
                );

            }
            else
            {
                // TODO: Kullanıcının geçersiz girdiği mesajı işleme ve geri bildirim gönder. 
            }
        }
    }
}