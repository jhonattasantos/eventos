using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [ApiController]
    [Route("api/eventos")]
    public class EventoController : ControllerBase
    {
        public readonly IProAgilRepository _repository;
        public EventoController(IProAgilRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllEventosAsync(true);
                return Ok(results);
            }
            catch (System.Exception)
            {
               return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento evento)
        {
              
            _repository.Add(evento);

            if(await _repository.SaveChangesAsync())
            {
                return Created($"/api/evento/{evento.Id}", evento);
            }          

            return BadRequest();
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> Put(int eventId,Evento model)
        {
            try
            {
                var evento = await _repository.GetAllEventoAsyncById(eventId, false);
                if(evento == null) return NotFound();

               _repository.Update(evento);

               if(await _repository.SaveChangesAsync())
               {
                   return Created($"/api/evento/{evento.Id}", evento);
               }
            }
            catch (System.Exception)
            {
               return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

            return BadRequest();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete(int eventId)
        {
            try
            {
                var evento = await _repository.GetAllEventoAsyncById(eventId, false);
                if(evento == null) return NotFound();

               _repository.Delete(evento);

               if(await _repository.SaveChangesAsync())
               {
                   return Ok();
               }
            }
            catch (System.Exception)
            {
               return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

            return BadRequest();
        }
        
        [HttpGet("{eventId}")]
        public async Task<ActionResult<Evento>> Get(int eventId)
        {
            try
            {
                var result =  await _repository.GetAllEventoAsyncById(eventId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<ActionResult<Evento>> Get(string tema)
        {
            try
            {
                var result =  await _repository.GetAllEventosAsyncByTema(tema, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }
       
    }
}
