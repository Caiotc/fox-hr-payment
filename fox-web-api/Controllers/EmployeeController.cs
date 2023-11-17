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
    public class EmployeeController : ControllerBase
    {

        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public EmployeeController(
            AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Employee>>> Get()
        {
            var employee = await _appDbContext.Employees.Include(employee => employee.Department).ToListAsync();
            if(employee.Count > 0)
            {
                return Ok(employee);
            }
            else
            {
                return Ok(new List<Employee>()); 
                    
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetById(int id) {
            var employee = await _appDbContext.Employees.Where(employee => employee.Id == id).Include(employee=>employee.Department).FirstOrDefaultAsync();
            if (employee == null)
                NotFound();

            return Ok(employee);
            
        }


        [HttpPost,Authorize]
        public async Task<ActionResult<Employee>> Post(EmployeeDto employee)
        {
            if (employee is not null)
            {
                var insertEmployee = new Employee()
                {
                    Name = employee.Name,
                    Address = employee.Address,
                    AdmissionDate = employee.AdmissionDate,
                    BirthDate = employee.BirthDate,
                    CPF = employee.CPF,
                    DepartmentId = employee.DepartmentId,
                    Email = employee.Email,
                    Position = employee.Position,
                    Income = employee.Income,
                };
                await _appDbContext.Employees.AddAsync(insertEmployee);

                CreatePasswordHash("123", out byte[] passwordHash, out byte[] passwordSalt);
                
        

                _appDbContext.SaveChanges();
                await _appDbContext.Users.AddAsync(new User()
                {
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Username = employee.Email,
                    UserPhoto = employee.UserPhoto,
                    EmployeeId = insertEmployee.Id,
                    RoleId = 2,
                });
                _appDbContext.SaveChanges();

               return Ok(insertEmployee);

            }

            return BadRequest();

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
