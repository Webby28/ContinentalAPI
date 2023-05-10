using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContinentalAPI.Data;
using ContinentalAPI.Models;

namespace ContinentalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly PersonaContext _context;

        public PersonasController(PersonaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Persona>> Get()
        {
            return await _context.Personas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> Get(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Persona persona)
        {
            // Validar duplicidad de personas por nro de documento
            var existeNroDocumento = await _context.Personas.AnyAsync(p => p.NroDocumento == persona.NroDocumento);
            if (existeNroDocumento)
            {
                ModelState.AddModelError("NroDocumento", "Ya existe una persona con el mismo número de documento.");
                return BadRequest(ModelState);
            }

            // Validar duplicidad de personas por correo electrónico
            if (!string.IsNullOrEmpty(persona.CorreoElectronico))
            {
                var existeCorreoElectronico = await _context.Personas.AnyAsync(p => p.CorreoElectronico == persona.CorreoElectronico);
                if (existeCorreoElectronico)
                {
                    ModelState.AddModelError("CorreoElectronico", "Ya existe una persona con el mismo correo electrónico.");
                    return BadRequest(ModelState);
                }
            }
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Persona persona)
        {
            var personaExistente = await _context.Personas.FindAsync(id);
            if (personaExistente == null)
            {
                return NotFound();
            }

            personaExistente.NombreApellido = persona.NombreApellido;
            personaExistente.NroDocumento = persona.NroDocumento;
            personaExistente.CorreoElectronico = persona.CorreoElectronico;
            personaExistente.Telefono = persona.Telefono;
            personaExistente.FechaNacimiento = persona.FechaNacimiento;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}