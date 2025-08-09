using Business.Abstract;
using Business.Handlers;
using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace Tarbot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhatsAppController : TwilioController
    {
        private readonly IUserService _userService;
        private readonly MessageHandlerResolver _handlerResolver;

        public WhatsAppController(IUserService userService, MessageHandlerResolver handlerResolver)
        {
            _userService = userService;
            _handlerResolver = handlerResolver;
        }

        [HttpPost]
        public async Task<TwiMLResult> Index()
        {
            var form = await Request.ReadFormAsync();
            var body = form["Body"].ToString().Trim();
            var from = form["From"].ToString();
            var profileName = form["ProfileName"].ToString();

            var user = await _userService.GetOrCreateUserByPhoneNumberAsync(from, profileName);
            
            // Doğru handler'ı bul ve görevi ona devret
            var handler = _handlerResolver.Resolve(user.ConversationState);
            await handler.ProcessAsync(user, body);

            var emptyResponse = new MessagingResponse();


            return new TwiMLResult(new MessagingResponse()); // Boş cevap, ama HTTP 200
        }
    }
}
