using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using retroAPI.Commons.Extension;
using retroAPI.Commons.Provider;
using retroAPI.Commons.Response;
using retroAPI.Models.DbModels;
using retroAPI.Modules.Boards.Domain;
using retroAPI.Modules.Boards.Interface;
using retroAPI.Modules.Jobs.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace retroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService _boardsService;
        private readonly IMapper _mapper;

        public BoardsController(IBoardService boardsService, IMapper mapper)
        {
            this._boardsService = boardsService;
            this._mapper = mapper;
        }

        // GET: api/<BoardsController>
        [HttpGet]
        public async Task<Response> GetMyBoardsAsync()
        {
            try
            {
                var user = (Users)HttpContext.Items["User"];
                var boards = await _boardsService.GetAllByUserIDAsync(user.Id);
                var resources = _mapper.Map<IEnumerable<Boards>, IEnumerable<BoardResource>>(boards);
                foreach (var item in resources)
                {
                    item.mine = true;
                }
                return new Response(resources);
            }
            catch (Exception ex)
            {
                return new Response(ex.ToString());
            }

        }
        //[HttpGet]
        //public Response Get()
        //{
        //    try
        //    {
        //        var boards =  _boardsService.GetAll();
        //        var resources = _mapper.Map<IEnumerable<Boards>, IEnumerable<BoardResource>>(boards);
        //        foreach (var item in resources)
        //        {
        //            item.mine = true;
        //        }
        //        return new Response(resources);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response(ex.ToString());
        //    }
        //}

        // GET api/<BoardsController>/5
        [HttpGet("{id}")]
        public async Task<Response> Get(int id)
        {
            try
            {
                var user = (Users)HttpContext.Items["User"];
                var detail = await _boardsService.GetByIDAsync(id);
                if (detail.UserId != user.Id)
                {
                    if (detail.IsPublished == 0)
                    {
                        return new Response("This is a private board");
                    }
                }
                var jobs = await _boardsService.GetJobByIDAsync(id);
                var resources = _mapper.Map<IEnumerable<Jobs>, IEnumerable<JobResource>>(jobs);
                return new Response(resources);
            }
            catch (Exception ex)
            {
                return new Response(ex.ToString());
            }
        }
        [HttpGet("detail/{id}")]
        public async Task<Response> Detail(int id)
        {
            try
            {
                var detail = await _boardsService.GetByIDAsync(id);
                var resources = _mapper.Map<Boards, BoardResource>(detail);
                return new Response(resources);
            }
            catch (Exception ex)
            {
                return new Response(ex.ToString());
            }
        }
        [HttpGet("shared")]
        public async Task<Response> GetSharedBoards()
        {
            try
            {
                var user = (Users)HttpContext.Items["User"];
                var boards = await _boardsService.GetSharedBoardsAsync(user.Id);
                var resources = _mapper.Map<IEnumerable<Boards>, IEnumerable<BoardResource>>(boards);
                foreach (var item in resources)
                {
                    item.mine = false;
                }
                return new Response(resources);
            }
            catch (Exception ex)
            {
                return new Response(ex.ToString());
            }
        }
        // POST api/<BoardsController>
        [HttpPost]
        public Response Post([FromBody] BoardReq value)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new Response(ModelState.GetErrorMessages());
                var user = (Users)HttpContext.Items["User"];
                var temp = _mapper.Map<BoardReq, Boards>(value);
                temp.UserId = user.Id;
                var response = _boardsService.Insert(temp);
                return new Response(response);
            }
            catch (Exception ex)
            {
                return new Response(ex.ToString());
            }
        }

        // PUT api/<BoardsController>/5
        [HttpPut("{id}")]
        public Response Put(int id, [FromBody] BoardReq value)
        {
            if (!ModelState.IsValid)
                return new Response(ModelState.GetErrorMessages());

            var model = _mapper.Map<BoardReq, Boards>(value);
            var result = _boardsService.Update(id, model);
            if (!result)
                return new Response($"Lỗi khi cập nhật");

            return new Response(result);
        }

        // DELETE api/<BoardsController>/5
        [HttpDelete("{id}")]
        public Response Delete(int id)
        {
            var result = _boardsService.Delete(id);

            if (!result)
                return new Response($"Lỗi khi xóa");

            return new Response(result);
        }
    }
}
