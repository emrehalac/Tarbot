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
    public class CowLoopCompletedHandler : IMessageHandler
    {
        private readonly IUserService _userService;


        public ConversationState State => ConversationState.CowLoopCompleted;


        public CowLoopCompletedHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task ProcessAsync(User user, string messageBody)
        {
            if (messageBody == "Okudum, anladım. :)")
            {
                await _userService.UpdateConversationStateAsync(user.Id, ConversationState.Completed);
            }
        }
    }
}