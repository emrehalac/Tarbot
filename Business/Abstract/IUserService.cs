using Entities.Entities;
using Entities.Enums;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<User> GetOrCreateUserByPhoneNumberAsync(string phoneNumber, string profileName);
        Task UpdateConversationStateAsync(int userId, ConversationState newState);

        Task UpdateUserAsync(User user);
       
    }
}
