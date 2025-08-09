using Business.Abstract;
using DataAccess.Abstract;
using Entities.Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CowManager : ICowService
    {

        private readonly ICowDal _cowDal;

        public CowManager(ICowDal cowDal)
        {
            _cowDal = cowDal;
        }

        public async Task<bool> AreAllCowsCompletedAsync(int userId)
        {
            return await _cowDal.AreAllCowsCompletedAsync(userId);
        }

        public async Task CreateEmptyCowsAsync(int userId, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var cow = new Cow
                {
                    UserId = userId,
                    Status = PregnancyStatus.Unknown,
                    IsCompleted = false
                };
                await _cowDal.AddAsync(cow);
            }
        }

        public Task<int> GetCompletedCowCountAsync(int userId)
        {
            return _cowDal.GetCompletedCowCountAsync(userId);
        }

        public Task<Cow?> GetFirstUncompletedCowAsync(int userId)
        {
            return _cowDal.GetFirstUncompletedCowAsync(userId);
        }

        public async Task UpdateAsync(Cow cow)
        {
            _cowDal.Update(cow);
            await _cowDal.SaveChangesAsync();
        }
    }
}
