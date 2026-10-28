using Tienda_don_Pedro.Data;
using Tienda_don_Pedro.DTOs;
using Tienda_don_Pedro.Models;

namespace Tienda_don_Pedro.GraphQL.Mutations
{
    public class ProductoMutation
    {
        public async Task<Producto> CrearProducto(CrearProductoDto input, [Service] AppDbContext context)
        {
            var producto = new Producto
            {
                Nombre = input.Nombre,
                Precio = input.Precio
            };

            context.Productos.Add(producto);
            await context.SaveChangesAsync();
            return producto;
        }

        public async Task<Producto?> ActualizarProducto(int id, ActualizarProductoDto input, [Service] AppDbContext context)
        {
            var producto = await context.Productos.FindAsync(id);
            if (producto == null) return null;

            producto.Nombre = input.Nombre;
            producto.Precio = input.Precio;

            await context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> EliminarProducto(int id, [Service] AppDbContext context)
        {
            var producto = await context.Productos.FindAsync(id);
            if (producto == null) return false;

            context.Productos.Remove(producto);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
