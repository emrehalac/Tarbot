using Business.Abstract;
using DataAccess.Abstract;
using Entities.Entities;
using Entities.Enums;
using System;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<User> GetOrCreateUserByPhoneNumberAsync(string phoneNumber, string profileName)
        {
            var user = await _userDal.GetUserByPhoneNumberAsync(phoneNumber);
            if (user == null)
            {
                user = new User
                {
                    PhoneNumber = phoneNumber,
                    ProfileName = profileName,
                    ConversationState = ConversationState.NotStarted,
                    CreatedAt = DateTime.UtcNow
                };
                await _userDal.AddAsync(user);
                await _userDal.SaveChangesAsync(); // Add SaveChangesAsync after AddAsync
            }
            return user;
        }

        public async Task UpdateConversationStateAsync(int userId, ConversationState newState)
        {
            var user = await _userDal.GetByIdAsync(userId); 
            if (user != null)
            {
                user.ConversationState = newState;
                _userDal.Update(user);
                await _userDal.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.UpdatedAt = DateTime.UtcNow; // BaseEntity'deki updated alaný güncelle
            _userDal.Update(user);
            await _userDal.SaveChangesAsync();
        }
    }
}
