using System.ComponentModel.DataAnnotations;

namespace Tienda_don_Pedro.DTOs
{
    public class CrearProductoDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nombre { get; set; } = string.Empty;

        [Range(0.01, 999999)]
        public decimal Precio { get; set; }
    }
}
