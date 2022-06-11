using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFutbolistas.Context;
using WebApiFutbolistas.Entities;

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
        public ActionResult<IEnumerable<Continente>> Get()
        {
            return dbContext.Continentes.Include(cont => cont.Jugadores).ToList();
        }

        //Este metodo es para buscar un resultado en la DB mediante un campo para ID
        [HttpGet("{id}",Name ="ObtenerContinente")]
        public ActionResult<Continente> Get(int id)
        {
            var continente = dbContext.Continentes.Include(con => con.Jugadores).FirstOrDefault(c => c.Id == id);

            if (continente ==null)
            {
                return BadRequest();
            }
            return continente;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Continente continente)
        {
            dbContext.Continentes.Add(continente);
            dbContext.SaveChanges();
            return new CreatedAtRouteResult("ObtenerContinente", new { id = continente.Id },continente);
        }

        [HttpPut("{id}")]

        public ActionResult Put(int id, [FromBody] Continente co)
        {
            if (id != co.Id)
            {
                return BadRequest();
            }

            try
            {
                dbContext.Entry(co).State = EntityState.Modified;
                dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Continente> Delete(int id)
        {
            var deleteContinente = dbContext.Continentes.FirstOrDefault(c => c.Id == id);
            if (deleteContinente == null)
            {
                return NotFound();
            }

            dbContext.Continentes.Remove(deleteContinente);
            dbContext.SaveChanges();

            return deleteContinente;
        }



    }
}
