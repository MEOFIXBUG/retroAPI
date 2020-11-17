using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retroAPI.Modules.Users.Domain
{
    public class UserResource
    {
        public int Id { get; set; }
        public string Username { get; set; }
        //public string Password { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public uint IsDeleted { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Token { get; set; }

        public UserResource(Models.DbModels.Users user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            Facebook = user.Facebook;
            Token = token;
        }
    }
}
