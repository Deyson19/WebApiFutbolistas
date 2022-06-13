using System.ComponentModel.DataAnnotations;

namespace WebApiFutbolistas.Models
{
    public class ContinenteViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}
