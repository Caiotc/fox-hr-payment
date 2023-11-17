using Azure.Core;
using fox_web_api.Dtos;
using fox_web_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace fox_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public UserController(
            AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }


     
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<IList<User>>> Get() => await _appDbContext.Users.Include(user=>user.Employee).ToListAsync();


        [HttpPut("changePassword"),Authorize]
        public async Task<ActionResult<User>> ChangePassword(NewUserDto request)
        {
            if (request.Username is not null)
            {
                User user = await _appDbContext.Users.Where(user => user.Username.Equals(request.Username)).Include(user => user.Role).FirstOrDefaultAsync();
                
                
                if (user == null)
                    return NotFound();


                if (VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    _appDbContext.Update(user);
                    _appDbContext.SaveChanges();
                    return Ok(user);

                }


               
            }

            return BadRequest();

        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginDto>> Login(UserDto request)
        {
            if (request.Username is not null)
            {
                User user = await _appDbContext.Users.Where(user => user.Username.Equals(request.Username)).Include(user => user.Role).Include(user => user.Employee).FirstOrDefaultAsync();
                if (user == null)
                    return NotFound();
                
                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                    return BadRequest("Wrong password");
                string token = CreateToken(user);


                return Ok(new LoginDto(){
                    Id = user.Id,
                    JwtBearer = token,
                    Username = user.Username,
                    RoleId = user.RoleId,
                    UserPhoto = user.UserPhoto,
                    EmployeeId = (int)user.EmployeeId,
                    Role = new Role()
                    {
                        Id = user.RoleId,
                        SystemRole = user.Role.SystemRole
                    },
                    Employee = user.Employee, 
                });
            }

            return BadRequest(); 
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role.SystemRole)            
            };
            // token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

           return jwt;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Add(UserDto user)
        {
            if (user == null || user.RoleId == null)
                return BadRequest("Invalid Request");

            var role = await _appDbContext.Roles.Where(role => role.Id == user.RoleId).FirstOrDefaultAsync();

            if(role == null)
                return BadRequest("Role does not exist");


            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            
            var userToBeAdded = new User()
            {
                PasswordHash = passwordHash,
                Username = user.Username,
                PasswordSalt = passwordSalt,
                RoleId = role.Id
            };


            var result = _appDbContext.Add(userToBeAdded).Entity;
            await _appDbContext.SaveChangesAsync();
            return Ok(result);
        }


        private void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt) {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password,  byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

    }
}
