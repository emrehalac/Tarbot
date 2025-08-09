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
    public class CowLabelHandler : IMessageHandler
    {
        private readonly IUserService _userService;
        private readonly ICowService _cowService;
        private readonly ITwilioRestClient _twilioRestClient;

        public ConversationState State => ConversationState.AwaitingCowLabel;


        public CowLabelHandler(IUserService userService, ITwilioRestClient twilioRestClient, ICowService cowService)
        {
            _userService = userService;
            _twilioRestClient = twilioRestClient;
            _cowService = cowService;
        }

        public async Task ProcessAsync(User user, string messageBody)
        {
            var cow = await _cowService.GetFirstUncompletedCowAsync(user.Id);
            if (cow == null)
            {
                // (Beklenmeyen durum – istersen logla veya kullanıcıya bilgi ver)
                return;
            }

            cow.Label = messageBody.Trim();
            await _cowService.UpdateAsync(cow);

            await _userService.UpdateConversationStateAsync(user.Id, ConversationState.AwaitingCowStatus);
           
            var contentSid = "HX263581d555554c62c3d7cc81fd4abe2d"; //  inek_durumu_sorusu
            var contentVariables = new JsonObject
            {
                ["1"] = cow.Label
            };

            await MessageResource.CreateAsync(
                from: new PhoneNumber("whatsapp:+14155238886"),
                to: new PhoneNumber(user.PhoneNumber),
                contentSid: contentSid,
                contentVariables: contentVariables.ToString(),
                client: _twilioRestClient
            );

        }
    }
}