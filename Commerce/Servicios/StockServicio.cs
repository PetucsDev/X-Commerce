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
    public static class StockServicio
    {
        private static string NombreArchivoStock = $@"{Environment.CurrentDirectory}/Stocks.txt";

        private static string NombreArchivoProducto = $@"{Environment.CurrentDirectory}/Productos.txt";

        public static List<Stock> Stocks = new List<Stock>();

        public static void ObtenerDatosDelArchivo()
        {
            string[] stocks = File.ReadAllLines(NombreArchivoStock);

            foreach (var item in stocks)
            {
                var nuevoStock = new Stock()
                {
                    Cantidad = decimal.Parse(item.Substring(PlantillasDeCorte.PlantillaStock.CantidadDesde, PlantillasDeCorte.PlantillaStock.CantidadCantidad)),
                    ProductoId = long.Parse(item.Substring(PlantillasDeCorte.PlantillaStock.ProductoIdDesde, PlantillasDeCorte.PlantillaStock.ProductoIdCantidad)),
                    
                };

                Stocks.Add(nuevoStock);
            }         
        }

        public static void Add(Stock stock)
        {
            stock.ProductoId = Stocks.Any() ? Stocks.Max(x => x.ProductoId) + 1 : 1;

            // Se agrega al Archivo
            var archivoStock = new StreamWriter(NombreArchivoStock, true);

            var crearLinea = $"{stock.ProductoId.ToString().PadLeft(PlantillasDeCorte.PlantillaStock.ProductoIdCantidad, ' ')}" +
                             $"{stock.Cantidad.ToString().PadLeft(PlantillasDeCorte.PlantillaStock.CantidadCantidad, ' ')}" +
                             $"{stock.TipoMovimiento.ToString().PadLeft(PlantillasDeCorte.PlantillaStock.TipoMovimientoCantidad, ' ')}";

            // Se agrega a la Lista Estatica

            archivoStock.WriteLine(crearLinea);
            archivoStock.Close();

            Stocks.Add(stock);
        }

        public static decimal ObtenerPorId(long productoId)
        {
            return decimal.Parse(Stocks.FirstOrDefault(x => x.ProductoId == productoId).ToString()); 
        }

        public static void Modificar(Stock stock)
        {

            string[] productos = File.ReadAllLines(NombreArchivoProducto);


            File.WriteAllText(NombreArchivoProducto, "");
            var archivoProducto = new StreamWriter(NombreArchivoProducto, true);


            foreach (var item in productos)
            {
                var modificarStock = new Producto()
                {
                    Id = long.Parse(item.Substring(PlantillasDeCorte.PlantillaProducto.IdDesde, PlantillasDeCorte.PlantillaProducto.IdCantidad)),
                    Codigo = int.Parse(item.Substring(PlantillasDeCorte.PlantillaProducto.CodigoDesde, PlantillasDeCorte.PlantillaProducto.CodigoCantidad)),
                    CodigoBarra = item.Substring(PlantillasDeCorte.PlantillaProducto.CodigoBarraDesde, PlantillasDeCorte.PlantillaProducto.CodigoBarraCantidad).Trim(),
                    Descripcion = item.Substring(PlantillasDeCorte.PlantillaProducto.DescripcionDesde, PlantillasDeCorte.PlantillaProducto.DescripcionCantidad).Trim(),
                    Precio = decimal.Parse(item.Substring(PlantillasDeCorte.PlantillaProducto.PrecioDesde, PlantillasDeCorte.PlantillaProducto.PrecioCantidad)),
                    Cantidad = int.Parse(item.Substring(PlantillasDeCorte.PlantillaProducto.CantidadDesde, PlantillasDeCorte.PlantillaProducto.CantidadCantidad)),
                };

                if(modificarStock.Codigo == stock.ProductoId)
                {
                    modificarStock.Cantidad = stock.Cantidad;
                    
                }

                var crearLinea = $"{modificarStock.Id.ToString().PadLeft(PlantillaProducto.IdCantidad, '0')}" +
                               $"{modificarStock.Codigo.ToString().PadLeft(PlantillaProducto.CodigoCantidad, ' ')}" +
                               $"{modificarStock.CodigoBarra.ToString().PadLeft(PlantillaProducto.CodigoBarraCantidad, ' ')}" +
                               $"{modificarStock.Descripcion.ToString().PadLeft(PlantillaProducto.DescripcionCantidad, ' ')}" +
                               $"{modificarStock.Precio.ToString().PadLeft(PlantillaProducto.PrecioCantidad, ' ')}" +
                               $"{modificarStock.Cantidad.ToString().PadLeft(PlantillaProducto.CantidadCantidad, ' ')}";

                archivoProducto.WriteLine(crearLinea);
                
             

            }

            archivoProducto.Close();






        }

      


    }
}
