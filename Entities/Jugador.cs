using System.ComponentModel.DataAnnotations;

namespace WebApiFutbolistas.Entities
{
    public class Jugador
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public int Dorsal { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public int PosicionId { get; set; }
        [Required]
        public int ContinenteId { get; set; }

    }
}
