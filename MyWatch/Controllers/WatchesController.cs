using Microsoft.AspNetCore.Mvc;
using MyWatchLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWatchRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchesController : ControllerBase
    {
        private readonly MyWatchRepository _repository;

        public WatchesController(MyWatchRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<WatchesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Watch>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        // GET api/<WatchesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Watch> GetById(int id)
        {
            var watch = _repository.GetById(id);
            if (watch == null) return NotFound($"A watch with id {id} was not found");
            return Ok(watch);
        }

        // POST api/<WatchesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Add([FromBody] Watch watch)
        {
            if (watch == null) return BadRequest("Watch cant be empty");

            _repository.Add(watch);
            return CreatedAtAction(nameof(GetById), new { id = watch.Id }, watch);
        }

        // PUT api/<WatchesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Update(int id, [FromBody] Watch watch)
        {
            if (watch == null) return BadRequest("Watch cant be null");

            var existingWatch = _repository.GetById(id);
            if (existingWatch == null) return NotFound($"The watch with ID {id} was not found");

            watch.Id = id;
            _repository.Update(watch);
            return NoContent();
        }

        // DELETE api/<WatchesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(int id)
        {
            var watch = _repository.GetById(id);
            if (watch == null) return NotFound($"The watch with ID {id} was not found");

            _repository.Delete(id);
            return NoContent();
        }
    }
}
