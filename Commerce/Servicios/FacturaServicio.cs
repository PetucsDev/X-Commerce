using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Commerce.Entidades;

namespace Commerce.Servicios
{
    public static class FacturaServicio
    {
        private static string NombreArchivo = $@"{Environment.CurrentDirectory}/Facturas.txt";
        private static string DetalleArchivo = $@"{Environment.CurrentDirectory}/DetalleFacturas.txt";
        
        public static List<Factura> Facturas = new List<Factura>();
        public static List<DetalleFactura> DetalleFacturas = new List<DetalleFactura>();

        // 1 Solo Obtener datos que accede a los Archivos
        public static void ObtenerDatosDelArchivo()
        {
            string[] facturas = File.ReadAllLines(NombreArchivo);

            string[] detalleFacturas = File.ReadAllLines(DetalleArchivo);

            
            foreach (var linea in facturas)
            {
                if (string.IsNullOrEmpty(linea)) continue;

                var nuevaFactura = new Factura()
                {
                    NumeroFactura = int.Parse(linea.Substring(PlantillasDeCorte.PlantillaFactura.NumeroFacturaDesde, PlantillasDeCorte.PlantillaFactura.NumeroFacturaCantidad)),
                    TipoFactura = linea.Substring(PlantillasDeCorte.PlantillaFactura.TipoFacturaDesde, PlantillasDeCorte.PlantillaFactura.TipoFacturaCantidad),
                    Fecha = DateTime.Parse(linea.Substring(PlantillasDeCorte.PlantillaFactura.FechaDesde, PlantillasDeCorte.PlantillaFactura.FechaCantidad)),
                    EmpleadoId = long.Parse(linea.Substring(PlantillasDeCorte.PlantillaFactura.EmpleadoIdDesde, PlantillasDeCorte.PlantillaFactura.EmpleadoIdCantidad)),
                    ClienteId = long.Parse(linea.Substring(PlantillasDeCorte.PlantillaFactura.ClienteIdDesde, PlantillasDeCorte.PlantillaFactura.ClienteIdCantidad)),
                    SubTotal1 = decimal.Parse(linea.Substring(PlantillasDeCorte.PlantillaFactura.SubTotalDesde, PlantillasDeCorte.PlantillaFactura.SubTotalCantidad)),
                    Descuento = decimal.Parse(linea.Substring(PlantillasDeCorte.PlantillaFactura.DescuentoDesde, PlantillasDeCorte.PlantillaFactura.DescuentoCantidad)),
                    Total1 = decimal.Parse(linea.Substring(PlantillasDeCorte.PlantillaFactura.TotalDesde, PlantillasDeCorte.PlantillaFactura.TotalCantidad)),
                };

                Facturas.Add(nuevaFactura);
            }

            foreach (var item in detalleFacturas)
            {
                if (string.IsNullOrEmpty(item)) continue;

                var nuevoDetalle = new DetalleFactura()
                {
                    Id = long.Parse(item.Substring(PlantillasDeCorte.PlantillaDetalleFactura.IdDesde, PlantillasDeCorte.PlantillaDetalleFactura.IdCantidad)),
                    NumeroFactura = int.Parse(item.Substring(PlantillasDeCorte.PlantillaDetalleFactura.NumeroFacturaDesde, PlantillasDeCorte.PlantillaDetalleFactura.NumeroFacturaCantidad)),
                    CodigoProducto = int.Parse(item.Substring(PlantillasDeCorte.PlantillaDetalleFactura.CodigoProductoDesde, PlantillasDeCorte.PlantillaDetalleFactura.CodigoProductoCantidad)),
                    DescripcionProducto = item.Substring(PlantillasDeCorte.PlantillaDetalleFactura.DescripcionProductoDesde, PlantillasDeCorte.PlantillaDetalleFactura.DescripcionProductoCantidad),
                    PrecioUnitario = decimal.Parse(item.Substring(PlantillasDeCorte.PlantillaDetalleFactura.PrecioUnitarioDesde, PlantillasDeCorte.PlantillaDetalleFactura.PrecioUnitarioCantidad)),
                    Cantidad = decimal.Parse(item.Substring(PlantillasDeCorte.PlantillaDetalleFactura.CantidadDesde, PlantillasDeCorte.PlantillaDetalleFactura.CantidadCantidad)),
                    SubTotal1 = decimal.Parse(item.Substring(PlantillasDeCorte.PlantillaDetalleFactura.SubTotalDesde, PlantillasDeCorte.PlantillaDetalleFactura.SubTotalCantidad)),
                };

                DetalleFacturas.Add(nuevoDetalle);
            }
        }


        public static void Add(Factura nuevaFactura)
        {
            //nuevaFactura.NumeroFactura = Facturas.Any() ? Facturas.Max(x => x.NumeroFactura) + 1 : 1;

            var archivoFactura = new StreamWriter(NombreArchivo, true);

            var crearLinea = $"{nuevaFactura.NumeroFactura.ToString().PadLeft(PlantillasDeCorte.PlantillaFactura.NumeroFacturaCantidad, ' ')}" +
                $"{nuevaFactura.TipoFactura.ToString().PadLeft(PlantillasDeCorte.PlantillaFactura.TipoFacturaCantidad, ' ')}" +
                $"{nuevaFactura.Fecha.ToString().PadLeft(PlantillasDeCorte.PlantillaFactura.FechaCantidad, ' ')}" +
                $"{nuevaFactura.EmpleadoId.ToString().PadLeft(PlantillasDeCorte.PlantillaFactura.EmpleadoIdCantidad, ' ')}" +
                $"{nuevaFactura.ClienteId.ToString().PadLeft(PlantillasDeCorte.PlantillaFactura.ClienteIdCantidad, ' ')}" +
                $"{nuevaFactura.SubTotal1.ToString().PadLeft(PlantillasDeCorte.PlantillaFactura.SubTotalCantidad, ' ')}" +
                $"{nuevaFactura.Descuento.ToString().PadLeft(PlantillasDeCorte.PlantillaFactura.DescuentoCantidad, ' ')}" +
                $"{nuevaFactura.Total1.ToString().PadLeft(PlantillasDeCorte.PlantillaFactura.TotalCantidad, ' ')}";

            archivoFactura.WriteLine(crearLinea);
            archivoFactura.Close();

            Facturas.Add(nuevaFactura);
        }


        public static void AddDetalleFactura(DetalleFactura nuevoDetalleFactura)
        {
            nuevoDetalleFactura.Id = DetalleFacturas.Any() ? DetalleFacturas.Max(x => x.Id) + 1 : 1;

            var archivoFactura = new StreamWriter(DetalleArchivo, true); 

            var crearLinea = $"{nuevoDetalleFactura.Id.ToString().PadLeft(PlantillasDeCorte.PlantillaDetalleFactura.IdCantidad, ' ')}" +
                $"{nuevoDetalleFactura.NumeroFactura.ToString().PadLeft(PlantillasDeCorte.PlantillaDetalleFactura.NumeroFacturaCantidad, ' ')}" +
                $"{nuevoDetalleFactura.CodigoProducto.ToString().PadLeft(PlantillasDeCorte.PlantillaDetalleFactura.CodigoProductoCantidad, ' ')}" +
                $"{nuevoDetalleFactura.DescripcionProducto.ToString().PadLeft(PlantillasDeCorte.PlantillaDetalleFactura.DescripcionProductoCantidad, ' ')}" +
                $"{nuevoDetalleFactura.PrecioUnitario.ToString().PadLeft(PlantillasDeCorte.PlantillaDetalleFactura.PrecioUnitarioCantidad, ' ')}" +
                $"{nuevoDetalleFactura.Cantidad.ToString().PadLeft(PlantillasDeCorte.PlantillaDetalleFactura.CantidadCantidad, ' ')}" +
                $"{nuevoDetalleFactura.SubTotal1.ToString().PadLeft(PlantillasDeCorte.PlantillaDetalleFactura.SubTotalCantidad, ' ')}";

            archivoFactura.WriteLine(crearLinea);
            archivoFactura.Close();

            DetalleFacturas.Add(nuevoDetalleFactura);
        }

        public static List<Factura> Obtener( string cadenaBuscar)
        {
            return Facturas.Where(x => x.NumeroFactura.ToString().Contains(cadenaBuscar)
                                        || x.TipoFactura.Contains(cadenaBuscar)
                                        || x.ClienteId.ToString() == cadenaBuscar)
                .ToList();

        }


        public static int  ObtenerMaximaFactura(string tipoFactura)
        {
            string[] facturas = File.ReadAllLines(NombreArchivo);

            int maximaFactura = 0;

            

            foreach (var linea in facturas)
            {
                if (string.IsNullOrEmpty(linea)) continue;
                if (linea.Substring(PlantillasDeCorte.PlantillaFactura.TipoFacturaDesde, PlantillasDeCorte.PlantillaFactura.TipoFacturaCantidad) == tipoFactura)
                {
                    if (int.Parse(linea.Substring(PlantillasDeCorte.PlantillaFactura.NumeroFacturaDesde, PlantillasDeCorte.PlantillaFactura.NumeroFacturaCantidad)) > maximaFactura)
                    {

                        maximaFactura = int.Parse(linea.Substring(PlantillasDeCorte.PlantillaFactura.NumeroFacturaDesde, PlantillasDeCorte.PlantillaFactura.NumeroFacturaCantidad));
                    }

                }
               
               
            }

            return maximaFactura;
        }

        public static long ObtenerSiguienteId()
        {
            return DetalleFacturas.Any() 
                ? DetalleFacturas.Max(x => x.Id) + 1
                : 1;
        }



    }
}
