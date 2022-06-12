using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiFutbolistas.Context;

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
    }
}
