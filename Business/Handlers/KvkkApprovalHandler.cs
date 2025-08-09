using Business.Abstract;
using Entities.Entities;
using Entities.Enums;
using System.Text.Json.Nodes;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.TwiML.Messaging;
using Twilio.Types;


namespace Business.Handlers
{
    public class KvkkApprovalHandler : IMessageHandler
    {
        private readonly IUserService _userService;
        public ConversationState State => ConversationState.AwaitingKvkkApproval;
        private readonly ITwilioRestClient _twilioRestClient;


        public KvkkApprovalHandler(IUserService userService, ITwilioRestClient twilioRestClient)
        {
            _userService = userService;
            _twilioRestClient = twilioRestClient;
        }

        public async Task ProcessAsync(User user, string messageBody)
        {
            if(messageBody == "Evet")
            {

                user.KvkkApproved = true;
                user.KvkkApprovedAt = DateTime.UtcNow;


                var contentSid = "HXdd9796a01bb3474dbc2cdd6b2e64eae8"; // Twilio Console'dan al�nan mesaj template SID si

                var contentVariables = new JsonObject
                {
                    ["1"] = user.ProfileName
                };

                // 3. REST API �zerinden �ablonlu mesaj� g�nderin.
                await MessageResource.CreateAsync(
                    from: new PhoneNumber("whatsapp:+14155238886"), // Twilio WhatsApp numaran�z
                    to: new PhoneNumber(user.PhoneNumber), // Kullan�c�n�n numaras�
                    contentSid: contentSid,
                    contentVariables: contentVariables.ToString(),
                    client: _twilioRestClient
                );

                // 4. Mesaj g�nderildikten sonra kullan�c�n�n ve state in durumunu g�ncellensin
                await _userService.UpdateUserAsync(user); // bu KVKK alanlar�n� da kaydeder

                await _userService.UpdateConversationStateAsync(user.Id, ConversationState.AwaitingCowCount);

            }
            else
            {
                await _userService.UpdateConversationStateAsync(user.Id, ConversationState.AwaitingKvkkRetryConfirmation);
            }
        }
    }
}
