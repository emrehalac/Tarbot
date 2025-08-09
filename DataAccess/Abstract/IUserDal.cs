using DataAccess.Abstract;
using Entities.Entities;

namespace DataAccess.Abstract
{
    public interface IUserDal : IRepository<User>
    {
        Task<User?> GetUserByPhoneNumberAsync(string phoneNumber);
    }
}

