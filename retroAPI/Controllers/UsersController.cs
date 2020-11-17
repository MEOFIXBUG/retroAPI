using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using retroAPI.Commons.Extension;
using retroAPI.Commons.Provider;
using retroAPI.Commons.Response;
using retroAPI.Models.DbModels;
using retroAPI.Modules.Users.Domain;
using retroAPI.Modules.Users.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace retroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            this._userService = userService;
            this._mapper = mapper;
        }
        [HttpGet("auth")]
        [Authorize]
        public Response Auth()
        {
            var user = (Users)HttpContext.Items["User"];

            if (user == null)
            {

                return new Response(false);
            }
            else
            {
                return new Response(true);
            }
        }
        [HttpPost("login")]
        public Response Login([FromBody] UserReq model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new Response(ModelState.GetErrorMessages());
                var temp = _mapper.Map<UserReq, Users>(model);
                var response = _userService.Authenticate(temp);
                if (response == null)
                {

                    return new Response("Username or password is incorrect");
                }
                else
                {
                    return new Response(new Token(response));
                }
            }
            catch (Exception ex)
            {
                return new Response(ex.ToString());
            }
        }

        [HttpPost("register")]
        public Response Register([FromBody] UserRegister model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new Response(ModelState.GetErrorMessages());
                var temp = _mapper.Map<UserRegister, Users>(model);
                var response = _userService.Insert(temp);
                if (response == -1)
                {
                    return new Response("Đã xảy ra lỗi trong quá trình thêm mới");
                }
                else
                {
                    return new Response(response);
                }
            }
            catch (Exception ex)
            {
                return new Response(ex.ToString());
            }
        }
        [HttpPut("update")]
        [Authorize]
        public Response Update(int id, [FromBody] UserReq model)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                return new Response(ex.ToString());
            }
        }


        //[Authorize]
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var users = _userService.GetAll();
        //    return Ok(users);
        //}
    }
}
