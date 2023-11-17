using fox_web_api.Dtos;
using fox_web_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fox_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public PaymentController(
            AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }


        [HttpGet("{cpf}")]
        public async Task<ActionResult<IList<Payment>>> Get(string cpf)
        {
            if(cpf is not null)
            {
                Employee employee = await _appDbContext.Employees.Where(e => e.CPF == cpf).FirstOrDefaultAsync();
                if (employee == null)
                    return BadRequest();



                List<Payment> payments = await _appDbContext.Payments.Where(pay => pay.EmployeeId == employee.Id).ToListAsync();

                return Ok(payments);
            }
            return BadRequest();

        }

        [HttpPost("{cpf}")]
        public async Task<ActionResult<Payment>> Post(string cpf)
        {
            if (cpf is not null)
            {              
                Employee employee = await _appDbContext.Employees.Where(e => e.CPF == cpf).FirstOrDefaultAsync();
                
                if (employee == null)
                    return BadRequest();

                Payment newPayment = new Payment
                {
                    EmployeeId = employee.Id,
                    PaymentAmount = employee.Income,
                    PaymentDate = DateTime.Now,
                };

                await _appDbContext.Payments.AddAsync(newPayment);

                _appDbContext.SaveChanges();


                return Ok(newPayment);
            }
            return BadRequest();
        }

    }
}
