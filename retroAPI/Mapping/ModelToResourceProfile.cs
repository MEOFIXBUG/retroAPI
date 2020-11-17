using AutoMapper;
using retroAPI.Models.DbModels;
using retroAPI.Modules.Boards.Domain;
using retroAPI.Modules.Jobs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retroAPI.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Boards, BoardResource>();
            CreateMap<Jobs, JobResource>();
        }
    }
}
