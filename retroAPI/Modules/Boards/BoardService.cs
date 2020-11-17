using retroAPI.Commons.Repository.Interface;
using retroAPI.Modules.Boards.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace retroAPI.Modules.Boards
{
    public class BoardService : IBoardService
    {
        private readonly IRepository<Models.DbModels.Boards> _boardRepository;
        private readonly IRepository<Models.DbModels.Jobs> _jobRepository;

        public BoardService(IRepository<Models.DbModels.Boards> boardRepository, IRepository<Models.DbModels.Jobs> jobRepository)
        {
            this._boardRepository = boardRepository;
            this._jobRepository = jobRepository;
        }
        public async Task<IEnumerable<Models.DbModels.Boards>> GetAllAsync()
        {
            return await _boardRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Models.DbModels.Boards>> GetAllByUserIDAsync(int userID)
        {
            return await _boardRepository.GetAsync(x => x.UserId == userID);
        }
        public async Task<Models.DbModels.Boards> GetByIDAsync(int id)
        {
            return await _boardRepository.GetByIDAsync(id);
        }
        private Models.DbModels.Boards GetByID(int id)
        {
            return _boardRepository.GetByID(id);
        }
        public int Insert(Models.DbModels.Boards model)
        {
            try
            {
                _boardRepository.Insert(model);

                return model.Id;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return -1;
            }
        }
        public bool Update(int id, Models.DbModels.Boards model)
        {
            try
            {
                var board = this.GetByID(id);
                if (!String.IsNullOrEmpty(model.ByName))
                {
                    board.ByName = model.ByName;
                }
                board.IsPublished = model.IsPublished;
                _boardRepository.Update(board);

                return true;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                _boardRepository.SoftDelete(id);

                return true;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return false;
            }
        }
        public async Task<IEnumerable<Models.DbModels.Boards>> GetSharedBoardsAsync(int userId)
        {
            var boards = await _boardRepository.GetAsync(x => x.SharedLists.Contains(";" + userId.ToString() + ";"));
            return boards;

        }

        public async Task<IEnumerable<Models.DbModels.Jobs>> GetJobByIDAsync(int id)
        {
            return await _jobRepository.GetAsync(x => x.BoardId == id);
        }
    }
}
