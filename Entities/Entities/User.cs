using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class User : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public ConversationState State { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
    }
}
