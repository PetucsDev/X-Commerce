using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerce.Entidades;
using Commerce.PlantillasDeCorte;

namespace Commerce.Servicios
{
    public static class ProductoServicio 
    {
        private static string NombreArchivo = $@"{Environment.CurrentDirectory}/Productos.txt";
        
        public static List<Producto> Productos = new List<Producto>();
        
        public static void ObtenerDatosDelArchivo()
        {
            string[] productos = File.ReadAllLines(NombreArchivo);
            Productos.Clear(); //limpio la lista, y vuelvo a cargar los datos del archivo

            foreach (var item in productos)
            {
                if (string.IsNullOrEmpty(item)) continue;

                var nuevoProducto = new Producto()
                {
                    Id = long.Parse(item.Substring(PlantillasDeCorte.PlantillaProducto.IdDesde, PlantillasDeCorte.PlantillaProducto.IdCantidad)),
                    Codigo = int.Parse(item.Substring(PlantillasDeCorte.PlantillaProducto.CodigoDesde, PlantillasDeCorte.PlantillaProducto.CodigoCantidad)),
                    CodigoBarra = item.Substring(PlantillasDeCorte.PlantillaProducto.CodigoBarraDesde, PlantillasDeCorte.PlantillaProducto.CodigoBarraCantidad).Trim(),
                    Descripcion = item.Substring(PlantillasDeCorte.PlantillaProducto.DescripcionDesde, PlantillasDeCorte.PlantillaProducto.DescripcionCantidad).Trim(),
                    Precio = decimal.Parse(item.Substring(PlantillasDeCorte.PlantillaProducto.PrecioDesde, PlantillasDeCorte.PlantillaProducto.PrecioCantidad)),
                    Cantidad = int.Parse(item.Substring(PlantillasDeCorte.PlantillaProducto.CantidadDesde, PlantillasDeCorte.PlantillaProducto.CantidadCantidad)),
                };

                Productos.Add(nuevoProducto);
            }
        }

        public static void Add(Producto producto)
        {
            producto.Id = Productos.Any() ? Productos.Max(x => x.Id) + 1 : 1;

            var archivoProducto = new StreamWriter(NombreArchivo, true);

            var crearLinea = $"{producto.Id.ToString().PadLeft(PlantillaProducto.IdCantidad, '0')}" +
                             $"{producto.Codigo.ToString().PadLeft(PlantillaProducto.CodigoCantidad, ' ')}" +
                             $"{producto.CodigoBarra.ToString().PadLeft(PlantillaProducto.CodigoBarraCantidad, ' ')}" +
                             $"{producto.Descripcion.ToString().PadLeft(PlantillaProducto.DescripcionCantidad, ' ')}" +
                             $"{producto.Precio.ToString().PadLeft(PlantillaProducto.PrecioCantidad, ' ')}" +
                             $"{producto.Cantidad.ToString().PadLeft(PlantillaProducto.CantidadCantidad, ' ')}";

            archivoProducto.WriteLine(crearLinea);
            archivoProducto.Close();

            Productos.Add(producto);
        }

        public static List<Producto> Obtener(string cadenaBuscar)
        {
            return Productos.Where(x => x.Codigo.ToString().Contains(cadenaBuscar)
                                        || x.CodigoBarra.Contains(cadenaBuscar)
                                        || x.Descripcion == cadenaBuscar)
                .ToList();
        }

        public static Producto Obtener(long id)
        {
            return Productos.FirstOrDefault(x => x.Id == id);
        }

        public static int Codigo()
        {
            return Productos.Any()
                ? Productos.Max(x => x.Codigo) + 1
                : 1;
        }

        public static Producto ObtenerPorCodigo(string cadenaBuscar)
        {
            return Productos.Where(x => x.Codigo.ToString().Contains(cadenaBuscar)
            )
                .FirstOrDefault();
        }

        public static bool VerificarSiExiste(string cod)
        {
            return Productos.Any(prod => prod.Codigo.ToString() == cod);
        }

        public static Producto ObtenerProductoId(long producto)
        {
            return Productos.Where(x => x.Id == producto).FirstOrDefault();
        }


    }
}
