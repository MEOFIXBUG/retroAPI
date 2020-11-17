using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retroAPI.Modules.Boards.Interface
{
    public interface IBoardService
    {
        Task<IEnumerable<retroAPI.Models.DbModels.Boards>> GetAllAsync();
        Task<IEnumerable<Models.DbModels.Boards>> GetAllByUserIDAsync(int userID);
        Task<IEnumerable<retroAPI.Models.DbModels.Boards>> GetSharedBoardsAsync(int userId);
        Task<IEnumerable<Models.DbModels.Jobs>> GetJobByIDAsync(int id);
        Task<Models.DbModels.Boards> GetByIDAsync(int id);
        int Insert(Models.DbModels.Boards model);

        bool Update(int id,Models.DbModels.Boards model);
        bool Delete(int id);
    }
}
