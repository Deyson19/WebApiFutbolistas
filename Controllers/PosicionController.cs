using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFutbolistas.Context;
using WebApiFutbolistas.Entities;

namespace WebApiFutbolistas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosicionController : ControllerBase
    {
        private readonly AppDbContext appDb;
        private readonly ILogger<PosicionController> _logger;

        public PosicionController(AppDbContext appDb, ILogger<PosicionController> logger)
        {
            this.appDb = appDb;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Posicion>> Get()
        {
            return appDb.Posiciones.Include(x=>x.JugadoresP).ToList();
        }
        //Obtener resultado por el Id
        [HttpGet("{id}", Name = "ObtenerPosicion")]
        public ActionResult<Posicion> Get(int id)
        {
            var posicion = appDb.Posiciones.Include(xP => xP.JugadoresP).FirstOrDefault(x => x.Id == id);
            if (posicion == null)
            {
                return NotFound();
            }
            return posicion;
        }

        //Metodo para enviar nuevos registros hacia la tabla
        [HttpPost]
        public ActionResult Post([FromBody] Posicion posi)
        {
            appDb.Posiciones.Add(posi);
            appDb.SaveChanges();
            return new CreatedAtRouteResult("ObtenerPosicion", new { id = posi.Id }, posi);
        }

        //Actualizar el registro mediante un Id
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromBody] Posicion updatePosicion)
        {
            var dato = await appDb.Posiciones.FirstOrDefaultAsync(x => x.Id == id);

            if (dato is not null)
            {
                if (id ==dato.Id)
                {
                    dato.Nombre = updatePosicion.Nombre;
                    dato.JugadoresP = updatePosicion.JugadoresP;

                    appDb.Posiciones.Update(dato);
                    await appDb.SaveChangesAsync();
                    return Ok($"Se realizaron los ajustes de {dato}");
                }
                return BadRequest();
            }

            return NotFound();
            
        }

        [HttpDelete("{id}")]
        public async Task <ActionResult> Delete(int id)
        {
            var consultarPosicion = await appDb.Posiciones.FirstOrDefaultAsync(cP => cP.Id==id);

            if (consultarPosicion is null) return NotFound();

            appDb.Posiciones.Remove(consultarPosicion);
            await appDb.SaveChangesAsync();
            _logger.LogInformation("Se eliminó un registro");
            return Ok($"Se ha eliminado la posición de nombre: {consultarPosicion.Nombre}");
        } 
    }
}
