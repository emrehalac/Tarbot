using Business.Abstract;
using Entities.Entities;
using Entities.Enums;
using System.Text.Json.Nodes;
using Twilio.TwiML;
using Twilio.TwiML.Messaging;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Clients;

namespace Business.Handlers;

public class WelcomeHandler : IMessageHandler
{
    private readonly IUserService _userService;
    private readonly ITwilioRestClient _twilioRestClient;
    public ConversationState State => ConversationState.NotStarted;

    public WelcomeHandler(IUserService userService, ITwilioRestClient twilioRestClient)
    {
        _userService = userService;
        _twilioRestClient = twilioRestClient;
    }

    public async Task ProcessAsync(User user, string messageBody)
    {
        var contentSid = "HX3837b220149ab1fa325a4835613d922b"; // Twilio Console'dan alınan mesaj template SID si

        // 2. Şablondaki değişkenler . {{1}} -> user.ProfileName
        var contentVariables = new JsonObject
        {
            ["1"] = user.ProfileName
        };
         
        // 3. REST API üzerinden şablonlu mesajı gönderin.
        await MessageResource.CreateAsync(
            from: new PhoneNumber("whatsapp:+14155238886"), // Twilio WhatsApp numaranız
            to: new PhoneNumber(user.PhoneNumber), // Kullanıcının numarası
            contentSid: contentSid,
            contentVariables: contentVariables.ToString(),
            client: _twilioRestClient 
        );

        // 4. Mesaj gönderildikten sonra kullanıcının durumunu güncellensin
        await _userService.UpdateConversationStateAsync(user.Id, ConversationState.AwaitingKvkkApproval);
    }
}