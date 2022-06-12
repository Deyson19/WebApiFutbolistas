using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFutbolistas.Context;
using WebApiFutbolistas.Entities;
using WebApiFutbolistas.Models;

namespace WebApiFutbolistas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadorController : ControllerBase
    {
        private readonly AppDbContext context;

        public JugadorController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var listarJugadores = await context.Jugadores.Include(p=>p.Posicion).
                Include(c => c.Continente).Select(m => new
                {
                    //traer objeto de bd con los campos especificos
                    Nombre = m.Nombre,
                    Apellido = m.Apellido,
                    Dorsal = m.Dorsal,
                    Edad = m.Edad,
                    Posicion = m.Posicion.Nombre,
                    Continente = m.Continente.Nombre

                }).ToListAsync();

            return Ok(listarJugadores);
        }
        
        //Realizar consulta por id
        [HttpGet("{id}",Name = "ObtenerJugador")]
        public async Task<ActionResult<object>> Get(int id)
        {
            //l = listarJugador

            var obtenerJugador = await context.Jugadores.Include(x => x.Posicion)
                .Include(z => z.Continente)
                .Select(l => new
                {
                    Id = l.Id,
                    Nombre = l.Nombre,
                    Apellido = l.Apellido,
                    Edad = l.Edad,
                    Dorsal = l.Dorsal,
                    Posicion = l.Posicion.Nombre,
                    Continente = l.Continente.Nombre
                })
                .FirstOrDefaultAsync(i => i.Id == id);

            if (obtenerJugador is null)
            {
                return NotFound();
            }

            return Ok(obtenerJugador);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] JugadorViewModel jugador)
        {
            Jugador j = new()
            {
                Nombre = jugador.Nombre,
                Apellido = jugador.Apellido,
                Edad = jugador.Edad,
                Dorsal = jugador.Dorsal,
                PosicionId = jugador.PosicionId,
                ContinenteId = jugador.ContinenteId
            };

            context.Add(j);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObtenerJugador", new { id = j.Id }, j);
        }

    }
}
