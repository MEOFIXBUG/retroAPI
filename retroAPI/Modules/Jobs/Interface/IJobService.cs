using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retroAPI.Modules.Jobs.Interface
{
    public interface IJobService
    {
        Task<IEnumerable<Models.DbModels.Jobs>>  GetAllJobsByBoardIDAsync(int id);
        int Insert(Models.DbModels.Jobs model);
        bool Delete(int id);
        Models.DbModels.Jobs getJobById(int id);
        Models.DbModels.Jobs Update(Models.DbModels.Jobs model);
    }
}
