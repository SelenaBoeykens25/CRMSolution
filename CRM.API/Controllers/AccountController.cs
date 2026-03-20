using CRM.API.Repo;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _repository;
        public AccountController(IAccountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{email}/{wachtwoord}")]
        public async Task<ActionResult<GebruikersAccount>> GetAccountAsync(string email, string wachtwoord)
        {
            try
            {
                var result = await _repository.GetAccountAsync(email, wachtwoord);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error");
            }
        }
        [HttpGet("{email}")]
        public async Task<ActionResult<bool>> BestaatAl(string email)
        {
            try
            {
                return await _repository.BestaatAlAsync(email);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error");
            }
        }
        [HttpPost]
        public async Task<ActionResult<GebruikersAccount>> CreateAccount(GebruikersAccount Account)
        {
            try
            {
                var createdAccount = await _repository.AddAccountAsync(Account);
                return Ok(createdAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating account: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Database error: {ex.Message}");
            }
        }
    }
}
