using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfRepository<User>, IUserDal
    {
        private readonly TarbotDBContext _context;

        public EfUserDal(TarbotDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }
    }
}
