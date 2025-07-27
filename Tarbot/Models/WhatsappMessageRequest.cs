using Microsoft.AspNetCore.Mvc;

namespace Tarbot.Models
{
    public class WhatsappMessageRequest
    {
        [FromForm(Name = "Body")]
        public string Body { get; set; }

        [FromForm(Name = "ProfileName")]
        public string ProfileName { get; set; }

        [FromForm(Name = "To")]
        public string To { get; set; }

        [FromForm(Name = "-from")]
        public string From { get; set; }

        [FromForm(Name = "WaId")]
        public string WaId { get; set; }
    }
}
