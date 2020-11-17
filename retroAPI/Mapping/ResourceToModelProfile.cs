using AutoMapper;
using retroAPI.Models.DbModels;
using retroAPI.Modules.Boards.Domain;
using retroAPI.Modules.Jobs.Domain;
using retroAPI.Modules.Users.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retroAPI.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<BoardReq, Boards>();
            CreateMap<UserReq, Users>();
            CreateMap<JobReq, Jobs>();
            CreateMap<UserRegister, Users>();
        }
    }
}
