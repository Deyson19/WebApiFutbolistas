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
    public class ContinenteController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public ContinenteController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {

            var traerDatos = await dbContext.Continentes.Select(n => new
            {
                Id = n.Id,
                Nombre = n.Nombre
            }).ToListAsync();

            return Ok(traerDatos);
        }

        //Este metodo es para buscar un resultado en la DB mediante un campo para ID
        [HttpGet("{id}",Name ="ObtenerContinente")]
        public async Task<ActionResult> Get(int id)
        {
            var continente = await dbContext.Continentes.Select(m=> new
            {
                Id = m.Id,
                Nombre = m.Nombre
            }).FirstOrDefaultAsync(c => c.Id == id);

            if (continente ==null)
            {
                return BadRequest();
            }
            return Ok(continente);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ContinenteViewModel continente)
        {
            Continente c = new()
            {
                Nombre = continente.Nombre
            };
            dbContext.Continentes.Add(c);
            await dbContext.SaveChangesAsync();

            return Ok("Se ha ingresado un nuevo continente");
            return new CreatedAtRouteResult("ObtenerContinente", new { id = continente.Id },continente);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> Put(int id, [FromBody] Continente co)
        {
            var dato = await dbContext.Continentes.FirstOrDefaultAsync(i => i.Id == id);

            if (dato is not null)
            {
                if (id == dato.Id)
                {
                    dato.Nombre = co.Nombre;

                    dbContext.Continentes.Update(dato);
                    await dbContext.SaveChangesAsync();

                    return Ok($"Se ha actualizado el registro con nombre: {dato.Nombre}");
                }

                return BadRequest();
            }

            return NotFound();
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            var deleteContinente = await dbContext.Continentes.FirstOrDefaultAsync(c => c.Id == id);
            if (deleteContinente == null)
            {
                return NotFound();
            }

            dbContext.Continentes.Remove(deleteContinente);
            await dbContext.SaveChangesAsync();

            return Ok("Se ha eliminado el continente: "+deleteContinente.Nombre +" ya no esta disponible.");
        }



    }
}
