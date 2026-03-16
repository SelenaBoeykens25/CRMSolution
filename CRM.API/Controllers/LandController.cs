using CRM.API.Repo;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LandController : Controller
    {
        private readonly ILandRepository _repository;
        public LandController(ILandRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetLanden()
        {
            try
            {
                return Ok(await _repository.GetLandenAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Land>> CreateLand(Land land)
        {
            try
            {
                var createdLand = await _repository.AddLandAsync(land);
                return CreatedAtAction(nameof(GetLandAsync),
                    new { landcode = createdLand.LandCode }, createdLand);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error");
            }
        }

        [HttpGet("{landcode}")]
        public async Task<ActionResult<Land>> GetLandAsync(string landcode)
        {
            try
            {
                var result = await _repository.GetLandAsync(landcode);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error");
            }
        }
    }
}
