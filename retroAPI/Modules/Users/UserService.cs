using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using retroAPI.Commons.Provider;
using retroAPI.Commons.Repository.Interface;
using retroAPI.Modules.Users.Domain;
using retroAPI.Modules.Users.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace retroAPI.Modules.Users
{
    public class UserService :IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private readonly AppSettings _appSettings;

        private readonly IRepository<Models.DbModels.Users> _userRepository;

        public UserService(IRepository<Models.DbModels.Users> userRepository, IOptions<AppSettings> appSettings)
        {
            this._userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public string Authenticate(Models.DbModels.Users model)
        {
            var user = _userRepository.Get(x => (x.Email==model.Username|| x.Username == model.Username) && x.Password == model.Password).FirstOrDefault();

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return token;
        }

        // helper methods

        private string generateJwtToken(Models.DbModels.Users user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()),new Claim("Name",user.Username) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<IEnumerable<Models.DbModels.Users>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<Models.DbModels.Users> GetByIDAsync(int id)
        {
            return await _userRepository.GetByIDAsync(id);
        }

        public Models.DbModels.Users GetByID(int id)
        {
            return  _userRepository.GetByID(id);
        }
        public int Insert(Models.DbModels.Users u)
        {
            try
            {
                _userRepository.Insert(u);

                return u.Id;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return -1;
            }
        }
    }
}
