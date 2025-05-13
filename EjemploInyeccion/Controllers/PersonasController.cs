
using EjemploPersonasData.Models;
using EjemploPersonasData.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EjemploInyeccion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {

        PersonasService _personasService;

        public PersonasController(PersonasService _personasService)
        {
            this._personasService = _personasService;
        }

        [HttpGet]
        async public Task<IActionResult> Get()
        {
            var personas = await _personasService.GetAll();

            if(personas.Count<=0)
                return NotFound();

            return Ok(personas);
        }

        
    }
}
