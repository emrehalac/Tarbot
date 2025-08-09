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
    public class CompletedHandler : IMessageHandler
    {
        private readonly IUserService _userService;


        public ConversationState State => ConversationState.Completed;


        public CompletedHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task ProcessAsync(User user, string messageBody)
        {
            return Task.CompletedTask; 
        }
    }
}