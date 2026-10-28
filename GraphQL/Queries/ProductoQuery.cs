using Microsoft.EntityFrameworkCore;
using Tienda_don_Pedro.Data;
using Tienda_don_Pedro.Models;

namespace Tienda_don_Pedro.GraphQL.Queries
{
    public class ProductoQuery
    {
        public async Task<List<Producto>> GetProductos([Service] AppDbContext context)
        {
            return await context.Productos.ToListAsync();
        }

        public async Task<Producto?> GetProducto(int id, [Service] AppDbContext context)
        {
            return await context.Productos.FindAsync(id);
        }
    }
}
