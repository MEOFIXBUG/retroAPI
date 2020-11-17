using retroAPI.Modules.Users.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retroAPI.Modules.Users.Interface
{
    public interface IUserService
    {

        Task<IEnumerable<retroAPI.Models.DbModels.Users>> GetAllAsync();
        Task<Models.DbModels.Users> GetByIDAsync(int id);
        Models.DbModels.Users GetByID(int id);
        string Authenticate(Models.DbModels.Users model);
        int Insert(Models.DbModels.Users u);
    }
}
