using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICowDal : IRepository<Cow>
    {
        Task<int> GetCompletedCowCountAsync(int userId);

        Task<Cow?> GetFirstUncompletedCowAsync(int userId);

        Task<bool> AreAllCowsCompletedAsync(int userId);


    }
}
