using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCowDal : EfRepository<Cow>, ICowDal
    {
        private readonly TarbotDBContext _context;
        

        public EfCowDal(TarbotDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetCompletedCowCountAsync(int userId)
        {
            return await _context.Cow
                      .Where(c => c.UserId == userId && c.IsCompleted)
                      .CountAsync();
        }

        public async Task<Cow?> GetFirstUncompletedCowAsync(int userId)
        {
            return await _context.Cow
                      .Where(c => c.UserId == userId && !c.IsCompleted)
                      .OrderBy(c => c.Id)
                      .FirstOrDefaultAsync();
        }

        public async Task<bool> AreAllCowsCompletedAsync(int userId)
        {
            int uncompletedCount = await _context.Cow
                .AsNoTracking()
                .Where(c => c.UserId == userId && !c.IsCompleted)
                .CountAsync();

            return uncompletedCount == 0;
        }
    }
}
