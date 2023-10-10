using fox_web_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fox_web_api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UserController(
            AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IList<User>>> Get() => await _appDbContext.Users.ToListAsync();

        [HttpGet("{email}/{password}")]
        public async Task<ActionResult<User>> Login(string email, string password)
        {
            if (email is not null || password is not null)
            {
                User user = await _appDbContext.Users.Where(user => user.Email.ToLower().Equals(email.ToLower()) && 
                    user.Password.Equals(password))
                        .FirstOrDefaultAsync();


                return user != null ? Ok(user) : NotFound(user);            
            }

            return BadRequest();        
        }
        

        [HttpPost]
        public async Task<ActionResult<User>> Add(User user){
            if (user == null)
                return BadRequest("Invalid Request");
            var result = _appDbContext.Add<User>(user).Entity;
            await _appDbContext.SaveChangesAsync();
            return Ok(result);
        }

    }
}
