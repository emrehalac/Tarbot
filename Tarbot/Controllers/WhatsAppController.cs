using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace Tarbot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhatsAppController : TwilioController
    {
        [HttpPost]
        public TwiMLResult Index()
        {
            var body = Request.Form["Body"];
            var from = Request.Form["-from"]; // Tireli olduğu için model binding çalışmaz!
            var profileName = Request.Form["ProfileName"];
            var waId = Request.Form["WaId"];
            var messagingResponse = new MessagingResponse();
            messagingResponse.Message($"Hoş geldin {profileName}! Mesajın: {body}");
            return TwiML(messagingResponse);
        }
    }
}
