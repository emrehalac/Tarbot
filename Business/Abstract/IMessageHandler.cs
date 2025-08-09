using Entities.Entities;
using Entities.Enums;
using Twilio.TwiML;

namespace Business.Abstract
{
    public interface IMessageHandler
    {
        ConversationState State { get; }
        Task ProcessAsync(User user, string messageBody);
    }
}
