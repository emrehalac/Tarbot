using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICowService
    {
        Task<bool> AreAllCowsCompletedAsync(int userId);
        Task CreateEmptyCowsAsync(int userId, int count);
        Task<int> GetCompletedCowCountAsync(int userId);

        Task<Cow?> GetFirstUncompletedCowAsync(int userId);
        Task UpdateAsync(Cow cow);
    }
}
