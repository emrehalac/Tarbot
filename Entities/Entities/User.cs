using Entities.Enums;

namespace Entities.Entities
{
    public class User : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string ProfileName { get; set; }
        public ConversationState ConversationState { get; set; }

        public bool KvkkApproved { get; set; }
        public DateTime? KvkkApprovedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
