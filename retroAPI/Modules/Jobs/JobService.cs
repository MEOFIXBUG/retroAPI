using retroAPI.Commons.Repository.Interface;
using retroAPI.Modules.Jobs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retroAPI.Modules.Jobs
{
    public class JobService : IJobService
    {
        private readonly IRepository<Models.DbModels.Jobs> _jobRepository;

        public JobService(IRepository<Models.DbModels.Jobs> jobRepository)
        {
            this._jobRepository = jobRepository;
        }
        public async Task<IEnumerable<Models.DbModels.Jobs>> GetAllJobsByBoardIDAsync(int id)
        {
            return await _jobRepository.GetAsync(x => x.BoardId == id);
        }
        public int Insert(Models.DbModels.Jobs model)
        {
            try
            {
                _jobRepository.Insert(model);

                return model.Id;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return -1;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                _jobRepository.SoftDelete(id);

                return true;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return false;
            }
        }

        public Models.DbModels.Jobs getJobById(int id)
        {
            return _jobRepository.GetByID(id);
        }
        public Models.DbModels.Jobs Update(Models.DbModels.Jobs model)
        {
            try
            {
                var job= this.getJobById(model.Id);
                job.ByName = model.ByName;
                _jobRepository.Update(job);
                return _jobRepository.GetByID(model.Id);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }
}
