using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using retroAPI.Commons.Extension;
using retroAPI.Commons.Provider;
using retroAPI.Commons.Response;
using retroAPI.Models.DbModels;
using retroAPI.Modules.Jobs.Domain;
using retroAPI.Modules.Jobs.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace retroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;
        public JobsController(IJobService jobService, IMapper mapper)
        {
            this._jobService = jobService;
            this._mapper = mapper;
        }
        // GET: api/<JobsController>
        [HttpGet("ofBoard/{id}")]
        public async Task<Response> GetJobsAsync(int id)
        {
            try
            {
                var user = (Users)HttpContext.Items["User"];

                var boards = await _jobService.GetAllJobsByBoardIDAsync(id);
                var resources = _mapper.Map<IEnumerable<Jobs>, IEnumerable<JobResource>>(boards);
                return new Response(resources);
            }
            catch (Exception ex)
            {
                return new Response(ex.ToString());
            }

        }

        // GET api/<JobsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<JobsController>
        [HttpPost("ofboard/{id}")]
        public Response Post(int id,[FromBody] JobReq value)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new Response(ModelState.GetErrorMessages());
                var user = (Users)HttpContext.Items["User"];
                var temp = _mapper.Map<JobReq, Jobs>(value);
                temp.BoardId = id;
                temp.IsDeleted = 0;
                var response = _jobService.Insert(temp);
                return new Response(response);
            }
            catch (Exception ex)
            {
                return new Response(ex.ToString());
            }
        }

        // PUT api/<JobsController>/5
        [HttpPut("{id}")]
        public Response Put(int id, [FromBody] JobReq value)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new Response(ModelState.GetErrorMessages());
                var user = (Users)HttpContext.Items["User"];
                var temp = _mapper.Map<JobReq, Jobs>(value);
                temp.Id = id;
                var response = _jobService.Update(temp);
                return new Response(response);
            }
            catch (Exception ex)
            {
                return new Response(ex.ToString());
            }
        }

        // DELETE api/<JobsController>/5
        [HttpDelete("{id}")]
        public Response Delete(int id)
        {
            var result = _jobService.Delete(id);

            if (!result)
                return new Response($"Lỗi khi xóa");

            return new Response(result);
        }
    }
}
