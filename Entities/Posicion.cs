using System.ComponentModel.DataAnnotations;

namespace WebApiFutbolistas.Entities
{
    public class Posicion
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public List<Jugador> JugadoresP { get; set; }
    }
}
