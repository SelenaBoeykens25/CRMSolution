using CRM.API.Repo;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FactuurController : Controller
    {
        private readonly IFactuurRepository _repository;
        public FactuurController(IFactuurRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Factuur>> GetFactuurAsync(int id)
        {
            try
            {
                var result = await _repository.GetFactuurAsync(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetFacturen()
        {
            try
            {
                return Ok(await _repository.GetFacturenAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error");
            }
        }
        [HttpGet("van/{klantId:int}")]
        public async Task<IActionResult> GetFacturenVan(int klantId)
        {
            try
            {
                return Ok(await _repository.GetFacturenVanAsync(klantId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Factuur>> CreateFactuur(Factuur Factuur)
        {
            try
            {
                var createdFactuur = await _repository.AddFactuurAsync(Factuur);
                return Ok(createdFactuur);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating factuur: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Database error: {ex.Message}");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Factuur>> UpdateFactuur(int id, Factuur Factuur)
        {
            try
            {
                var contactToUpdate = await _repository.GetFactuurAsync(id);

                if (contactToUpdate == null)
                    return NotFound($"Factuur with id = {id} is not found");

                return await _repository.UpdateFactuurAsync(Factuur);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Factuur>> DeleteFactuur(int id)
        {
            try
            {
                var contactToDelete = await _repository.GetFactuurAsync(id);

                if (contactToDelete == null)
                    return NotFound($"Factuur with id = {id} is not found");

                return await _repository.DeleteFactuurAsync(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
