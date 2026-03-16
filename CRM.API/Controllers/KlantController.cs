using CRM.API.Repo;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KlantController : Controller
    {
        private readonly IKlantenRepository _repository;
        public KlantController(IKlantenRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetKlanten()
        {
            try
            {
                return Ok(await _repository.GetKlantenAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error");
            }
        }
    }
}
