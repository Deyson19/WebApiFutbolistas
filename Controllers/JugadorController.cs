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
                    Id = m.Id,
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
                    PosicionId = l.PosicionId,
                    Continente = l.Continente.Nombre,
                    ContinenteId = l.Continente.Id
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Jugador jugador)
        {   
            var dato = await context.Jugadores.FirstOrDefaultAsync(i => i.Id == id);

            if (dato is not null)
            {
                if (id == dato.Id)
                {
                    dato.Nombre = jugador.Nombre;
                    dato.Apellido = jugador.Apellido;
                    dato.Edad = jugador.Edad;
                    dato.Dorsal = jugador.Dorsal;
                    dato.PosicionId = jugador.PosicionId;
                    dato.ContinenteId = jugador.ContinenteId;
                    
                    context.Jugadores.Update(dato);
                    await context.SaveChangesAsync();

                    return Ok($"Se han actualizado el registro con nombre: {dato.Nombre}");
                }
                return BadRequest();
            }

            return NotFound();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            var juElimiar = await context.Jugadores.FirstOrDefaultAsync(j => j.Id == id);

            if(juElimiar is null)
            {
                return NotFound();
            }

            context.Jugadores.Remove(juElimiar);
            await context.SaveChangesAsync();

            return Ok("Se ha eliminado un jugador");

        }

    }
}
