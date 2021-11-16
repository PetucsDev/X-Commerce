using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Commerce.Entidades;
using Commerce.PlantillasDeCorte;

namespace Commerce.Servicios
{
    public class CuentaCorrienteServicio
    {
        private static string NombreArchivo = $@"{Environment.CurrentDirectory}/CuentaCorrientes.txt";


        public static List<CuentaCorriente> CuentaCorrientes = new List<CuentaCorriente>();


        public static void ObtenerDatosDelArchivo()
        {
            string[] cuentaCorrientes = File.ReadAllLines(NombreArchivo);

            foreach (var linea in cuentaCorrientes)
            {
                if (string.IsNullOrEmpty(linea)) continue;

                var nuevaCtaCte = new CuentaCorriente()
                {
                    Id = long.Parse(linea.Substring(PlantillaCuentaCorriente.IdDesde, PlantillaCuentaCorriente.IdCantidad)),
                    ClienteId = long.Parse(linea.Substring(PlantillaCuentaCorriente.ClienteIdDesde, PlantillaCuentaCorriente.ClienteIdCantidad)),
                    Descripcion = linea.Substring(PlantillaCuentaCorriente.DescripcionDesde, PlantillaCuentaCorriente.DescripcionCantidad),
                    Fecha = DateTime.Parse(linea.Substring(PlantillaCuentaCorriente.FechaDesde, PlantillaCuentaCorriente.FechaCantidad)),
                    Monto = decimal.Parse(linea.Substring(PlantillaCuentaCorriente.MontoDesde, PlantillaCuentaCorriente.MontoCantidad)),
                    TipoMovimiento = linea.Substring(PlantillaCuentaCorriente.TipoMovimientoDesde, PlantillaCuentaCorriente.TipoMovimientoCantidad) == "-1"
                   ? TipoMovimiento.Egreso
                   : TipoMovimiento.Ingreso
                };

                CuentaCorrientes.Add(nuevaCtaCte);
            }
        }



        public static void Add(CuentaCorriente nuevaCtaCte)
        {
            nuevaCtaCte.Id = CuentaCorrientes.Any() ? CuentaCorrientes.Max(x => x.Id) + 1 : 1;

            // Se agrega al Archivo
            var archivoStock = new StreamWriter(NombreArchivo, true);

            string tipoMovimiento = nuevaCtaCte.TipoMovimiento == TipoMovimiento.Ingreso
                ? "01"
                : "-1";


            var crearLinea = $"{nuevaCtaCte.Id.ToString().PadLeft(PlantillaCuentaCorriente.IdCantidad, '0')}" +
                             $"{nuevaCtaCte.ClienteId.ToString().PadLeft(PlantillaCuentaCorriente.ClienteIdCantidad, ' ')}" +
                             $"{nuevaCtaCte.Descripcion.ToString().PadLeft(PlantillaCuentaCorriente.DescripcionCantidad, ' ')}" +
                             $"{nuevaCtaCte.Fecha.ToString().PadLeft(PlantillaCuentaCorriente.FechaCantidad, ' ')}" +
                             $"{nuevaCtaCte.Monto.ToString().PadLeft(PlantillaCuentaCorriente.MontoCantidad, ' ')}" +
                             $"{tipoMovimiento}";

            // Se agrega a la Lista Estatica

            archivoStock.WriteLine(crearLinea);
            archivoStock.Close();

            CuentaCorrientes.Add(nuevaCtaCte);
        }

        public static List<CuentaCorriente> Obtener(string cadenaBuscar)
        {
            return CuentaCorrientes.Where(x => x.Id.ToString().Contains(cadenaBuscar)
                                        
                                        )
                .ToList();

        }

       public static long  MaxId()
        {

            string[] CuentaCorrientes = File.ReadAllLines(NombreArchivo);

            long maximaCtaCte = 0;

            foreach (var linea in CuentaCorrientes)
            {
                if (string.IsNullOrEmpty(linea)) continue;
                if ( long.Parse(linea.Substring(PlantillaCuentaCorriente.IdDesde, PlantillaCuentaCorriente.IdCantidad)) > maximaCtaCte)
                {
                    maximaCtaCte = long.Parse(linea.Substring(PlantillaCuentaCorriente.IdDesde, PlantillaCuentaCorriente.IdCantidad));
                }
            }


            return maximaCtaCte;
        }

        public static List<CuentaCorriente> ObtenerCtaCteCliente( string clienteId)
        {
  
            string[] cuentaCorrientes = File.ReadAllLines(NombreArchivo);

           List<CuentaCorriente> CuentaCorrientesCliente = new List<CuentaCorriente>();

            foreach (var linea in cuentaCorrientes)
            {
                if (string.IsNullOrEmpty(linea)) continue;

                if ((linea.Substring(PlantillaCuentaCorriente.ClienteIdDesde, PlantillaCuentaCorriente.ClienteIdCantidad)).Trim() == clienteId)
                {
                   

                    var nuevactacte = new CuentaCorriente()
                    {
                        Id = long.Parse(linea.Substring(PlantillaCuentaCorriente.IdDesde, PlantillaCuentaCorriente.IdCantidad)),
                        ClienteId = long.Parse(linea.Substring(PlantillaCuentaCorriente.ClienteIdDesde, PlantillaCuentaCorriente.ClienteIdCantidad)),
                        Descripcion = linea.Substring(PlantillaCuentaCorriente.DescripcionDesde, PlantillaCuentaCorriente.DescripcionCantidad).Trim(),
                        Fecha = DateTime.Parse(linea.Substring(PlantillaCuentaCorriente.FechaDesde, PlantillaCuentaCorriente.FechaCantidad)),
                        Monto = decimal.Parse(linea.Substring(PlantillaCuentaCorriente.MontoDesde, PlantillaCuentaCorriente.MontoCantidad)),
                        TipoMovimiento = linea.Substring(PlantillaCuentaCorriente.TipoMovimientoDesde, PlantillaCuentaCorriente.TipoMovimientoCantidad) == "-1"
                  ? TipoMovimiento.Egreso
                  : TipoMovimiento.Ingreso
                    };

                     CuentaCorrientesCliente.Add(nuevactacte);
                    

                }
            }
            return CuentaCorrientesCliente.ToList(); 
            
        }

        public static decimal  ObtenerDeuda(long cadenaString)
        {
            List<CuentaCorriente>cteCliente;

            decimal montoEgreso = 0m;
            decimal montoIngreso = 0m;

            cteCliente = CuentaCorrientes.Where(x => x.ClienteId == cadenaString).ToList();  
                
            foreach(var linea in cteCliente)
            {
                if(linea.TipoMovimiento == TipoMovimiento.Egreso)
                {
                    montoEgreso = montoEgreso + linea.Monto;
                }
               
                if(linea.TipoMovimiento == TipoMovimiento.Ingreso)
                {
                    montoIngreso = montoIngreso + linea.Monto;
                }

            }


            return (montoEgreso - montoIngreso);
            
        }


    }
}
