using Tienda_don_Pedro.DTOs;

namespace Tienda_don_Pedro.Services
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> ObtenerTodos();
        Task<ProductoDto?> ObtenerPorId(int id);
        Task<ProductoDto> Crear(CrearProductoDto dto);
        Task<bool> Actualizar(int id, ActualizarProductoDto dto);
        Task<bool> Eliminar(int id);
    }
}
