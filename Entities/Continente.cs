using System.ComponentModel.DataAnnotations;

namespace WebApiFutbolistas.Entities
{
    public class Continente
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Este campo {0} es obligatorio")]
        public string Nombre { get; set; }
        public List<Jugador> Jugadores { get; set; }
    }
}
