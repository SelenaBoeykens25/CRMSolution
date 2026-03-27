using CRM.API.Repo;
using Microsoft.AspNetCore.Mvc;
using CRM.Models;

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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Klant>> GetKlantAsync(int id)
        {
            try
            {
                var result = await _repository.GetKlantAsync(id);
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
        [HttpPost]
        public async Task<ActionResult<Klant>> CreateKlant(Klant klant)
        {
            try
            {
                var createdContact = await _repository.AddKlantAsync(klant);
                return CreatedAtAction(nameof(GetKlantAsync),
                    new { id = createdContact.Id }, createdContact);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Klant>> UpdateKlant(int id, Klant klant)
        {
            try
            {
                var contactToUpdate = await _repository.GetKlantAsync(id);

                if (contactToUpdate == null)
                    return NotFound($"Klant with id = {id} is not found");

                return await _repository.UpdateKlantAsync(klant);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Klant>> DeleteKlant(int id)
        {
            try
            {
                var contactToDelete = await _repository.GetKlantAsync(id);

                if (contactToDelete == null)
                    return NotFound($"Klant with id = {id} is not found");

                return await _repository.DeleteKlant(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        [HttpDelete("reset")]
        public async Task ResetAlles()
        {
            await _repository.Reset();
        }
    }
}
