using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Commerce.Entidades;

namespace Commerce.Servicios
{
    public static class ClienteServicio
    {
        private static string NombreArchivo = $@"{Environment.CurrentDirectory}/Clientes.txt";
        
        // Lista de Clientes para el sistema
        public static List<Cliente> Clientes = new List<Cliente>();
        
        public static void ObtenerDatosDelArchivo()
        {
            string[] clientes = File.ReadAllLines(NombreArchivo);

            foreach (var linea in clientes)
            {
                if (string.IsNullOrEmpty(linea)) continue;

                var nuevoCliente = new Cliente()
                {                  
                    Id = long.Parse(linea.Substring(PlantillasDeCorte.PlantillaCliente.IdDesde, PlantillasDeCorte.PlantillaCliente.IdCantidad)),
                    Codigo = int.Parse(linea.Substring(PlantillasDeCorte.PlantillaCliente.CodigoDesde, PlantillasDeCorte.PlantillaCliente.CodigoCantidad)),
                    Apellido = linea.Substring(PlantillasDeCorte.PlantillaCliente.ApellidoDesde, PlantillasDeCorte.PlantillaCliente.ApellidoCantidad).Trim(),
                    Nombre = linea.Substring(PlantillasDeCorte.PlantillaCliente.NombreDesde, PlantillasDeCorte.PlantillaCliente.NombreCantidad).Trim(),
                    Dni = linea.Substring(PlantillasDeCorte.PlantillaCliente.DniDesde, PlantillasDeCorte.PlantillaCliente.DniCantidad).Trim(),                   
                    FechaNacimiento = DateTime.Parse(linea.Substring(PlantillasDeCorte.PlantillaCliente.FechaNacimientoDesde, PlantillasDeCorte.PlantillaCliente.FechaNacimientoCantidad)),
                    Calle = linea.Substring(PlantillasDeCorte.PlantillaCliente.CalleDesde, PlantillasDeCorte.PlantillaCliente.CalleCantidad).Trim(),
                    Numero = linea.Substring(PlantillasDeCorte.PlantillaCliente.NumeroDesde, PlantillasDeCorte.PlantillaCliente.NumeroCantidad).Trim(),
                    Piso = linea.Substring(PlantillasDeCorte.PlantillaCliente.PisoDesde, PlantillasDeCorte.PlantillaCliente.PisoCantidad).Trim(),
                    Dpto = linea.Substring(PlantillasDeCorte.PlantillaCliente.DptoDesde, PlantillasDeCorte.PlantillaCliente.DptoCantidad).Trim(),
                    TieneLimiteCompra = bool.Parse(linea.Substring(PlantillasDeCorte.PlantillaCliente.TieneLimiteCompraDesde, PlantillasDeCorte.PlantillaCliente.TieneLimiteCompraCantidad)),
                    MontoLimiteCompra = decimal.Parse(linea.Substring(PlantillasDeCorte.PlantillaCliente.MontoLimiteCompraDesde, PlantillasDeCorte.PlantillaCliente.MontoLimiteCompraCantidad))         
                    
                    
                };

                Clientes.Add(nuevoCliente);
            }
        }

        public static void Add(Cliente cliente)
        {
           cliente.Id = Clientes.Any() ? Clientes.Max(x => x.Id) + 1 : 1;

            // Se agrega al Archivo
            var archivoCliente = new StreamWriter(NombreArchivo, true);

            var crearLinea = $"{cliente.Id.ToString().PadLeft(PlantillasDeCorte.PlantillaCliente.IdCantidad, '0')}" +
                $"{cliente.Codigo.ToString().PadLeft(PlantillasDeCorte.PlantillaCliente.CodigoCantidad, '0')}" +
                $"{cliente.Apellido.PadRight(PlantillasDeCorte.PlantillaCliente.ApellidoCantidad, ' ')}" +
                $"{cliente.Nombre.PadRight(PlantillasDeCorte.PlantillaCliente.NombreCantidad, ' ')}" +
                $"{cliente.Dni.PadRight(PlantillasDeCorte.PlantillaCliente.DniCantidad, ' ')}" +
                $"{cliente.FechaNacimiento.Day.ToString().PadLeft(2, '0')}/{cliente.FechaNacimiento.Month.ToString().PadLeft(2, '0')}/{cliente.FechaNacimiento.Year.ToString().PadLeft(4, '0')}" +
                $"{cliente.Calle.PadRight(PlantillasDeCorte.PlantillaCliente.CalleCantidad, ' ')}" +
                $"{cliente.Numero.PadRight(PlantillasDeCorte.PlantillaCliente.NumeroCantidad, ' ')}" +
                $"{cliente.Piso.PadRight(PlantillasDeCorte.PlantillaCliente.PisoCantidad, ' ')}" +
                $"{cliente.Dpto.PadRight(PlantillasDeCorte.PlantillaCliente.DptoCantidad, ' ')}" +
                $"{cliente.TieneLimiteCompra.ToString().PadRight(PlantillasDeCorte.PlantillaCliente.TieneLimiteCompraCantidad, ' ')}" +
                $"{cliente.MontoLimiteCompra.ToString().PadRight(PlantillasDeCorte.PlantillaCliente.MontoLimiteCompraCantidad, ' ')}";


            archivoCliente.WriteLine(crearLinea);
            archivoCliente.Close();

            // Se agrega a la Lista Estatica
            Clientes.Add(cliente);

        }

        public static List<Cliente> Obtener(string cadenaBuscar)
        {
             return Clientes.Where(x => x.Apellido.Contains(cadenaBuscar)
                                        || x.Nombre.Contains(cadenaBuscar)
                                        || x.Dni == cadenaBuscar)
                .ToList();
        }

        public static Cliente Obtener(long id)
        {
            return Clientes.FirstOrDefault(x => x.Id == id);
        }

        public static int Codigo()
        {
            return Clientes.Any()
                ? Clientes.Max(x => x.Codigo) + 1
                : 1;
        }

        public static Cliente ObtenerCodigo(string cadenaString)
        {
            return Clientes.Where(x => x.Codigo.ToString().Contains(cadenaString)
            || x.Dni == cadenaString)
                .FirstOrDefault();
        }

        public static Cliente ObtenerMontoMaximo(string cadenaString)
        {
            return Clientes.Where(x => x.MontoLimiteCompra.ToString().Contains(cadenaString)
            || x.Dni == cadenaString)
                .FirstOrDefault();
        }

        public static Cliente ObtenerLimiteCompra(long cadenaString)
        {
            return Clientes.Where(x => x.Codigo == cadenaString)
                .FirstOrDefault();
        }
        public static  Cliente ObtenerNombreCliente(string nombre)
        {
            return Clientes.FirstOrDefault(x => x.ApyNomCompleto == nombre);
        }

    }
}
