using Commerce.Entidades;
using Commerce.PlantillasDeCorte;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

namespace Commerce.Servicios
{
    public static class EmpleadoServicio
    {
        private static string NombreArchivo = $@"{Environment.CurrentDirectory}\Empleados.txt";

        public static List<Empleado> Empleados = new List<Empleado>();

        public static long empleadoLogeado;
        public static void ObtenerDatosDelArchivo()
        {
            string[] empleados = File.ReadAllLines(NombreArchivo);

            foreach (var linea in empleados)
            {
                if (string.IsNullOrEmpty(linea)) continue;

                var nuevoEmpleado = new Empleado()
                {
                                                                 

                    Id = long.Parse(linea.Substring(PlantillaEmpleado.IdDesde, PlantillaEmpleado.IdCantidad)),
                    Legajo = int.Parse(linea.Substring(PlantillaEmpleado.LegajoDesde, PlantillaEmpleado.LegajoCantidad)),
                    Apellido = linea.Substring(PlantillaEmpleado.ApellidoDesde, PlantillaEmpleado.ApellidoCantidad).Trim(),
                    Nombre = linea.Substring(PlantillaEmpleado.NombreDesde, PlantillaEmpleado.NombreCantidad).Trim(),
                    Dni = linea.Substring(PlantillaEmpleado.DniDesde, PlantillaEmpleado.DniCantidad).Trim(),
                    FechaNacimiento = DateTime.Parse(linea.Substring(PlantillaEmpleado.FechaNacimientoDesde, PlantillaEmpleado.FechaNacimientoCantidad)),
                    Calle = linea.Substring(PlantillaEmpleado.CalleDesde, PlantillaEmpleado.CalleCantidad).Trim(),
                    Numero = linea.Substring(PlantillaEmpleado.NumeroDesde, PlantillaEmpleado.NumeroCantidad).Trim(),
                    Piso = linea.Substring(PlantillaEmpleado.PisoDesde, PlantillaEmpleado.PisoCantidad).Trim(),
                    Dpto = linea.Substring(PlantillaEmpleado.DptoDesde, PlantillaEmpleado.DptoCantidad).Trim()
                };
              
                Empleados.Add(nuevoEmpleado);
            }
        }

        public static void Add(Empleado nuevoEmpleado)
        {
            // Obtiene un Identificador Unico para el nuevo Empleado
            nuevoEmpleado.Id = Empleados.Any() ? Empleados.Max(x => x.Id) + 1 : 1;

            // Se agrega al Archivo
            var archivoEmpelado = new StreamWriter(NombreArchivo, true);

            var crearLinea = $"{nuevoEmpleado.Id.ToString().PadLeft(PlantillaEmpleado.IdCantidad, '0')}" +
                $"{nuevoEmpleado.Legajo.ToString().PadLeft(PlantillaEmpleado.LegajoCantidad, '0')}" +
                $"{nuevoEmpleado.Apellido.PadRight(PlantillaEmpleado.ApellidoCantidad, ' ')}" +
                $"{nuevoEmpleado.Nombre.PadRight(PlantillaEmpleado.NombreCantidad, ' ')}" +
                $"{nuevoEmpleado.Dni.PadRight(PlantillaEmpleado.DniCantidad, ' ')}" +
                $"{nuevoEmpleado.FechaNacimiento.Day.ToString().PadLeft(2, '0')}/{nuevoEmpleado.FechaNacimiento.Month.ToString().PadLeft(2, '0')}/{nuevoEmpleado.FechaNacimiento.Year.ToString().PadLeft(4, '0')}" +
                $"{nuevoEmpleado.Calle.PadRight(PlantillaEmpleado.CalleCantidad, ' ')}" +
                $"{nuevoEmpleado.Numero.PadRight(PlantillaEmpleado.NumeroCantidad, ' ')}" +
                $"{nuevoEmpleado.Piso.PadRight(PlantillaEmpleado.PisoCantidad, ' ')}" +
                $"{nuevoEmpleado.Dpto.PadRight(PlantillaEmpleado.DptoCantidad, ' ')}";

            archivoEmpelado.WriteLine(crearLinea);
            archivoEmpelado.Close();

            // Se agrega a la Lista Estatica
            Empleados.Add(nuevoEmpleado);

            // Crear El USuario
            UsuarioServicio.Crear(nuevoEmpleado.Id, nuevoEmpleado.Apellido, nuevoEmpleado.Nombre);
        }

        public static int ObtenerSiguienteLegajo() {
            return Empleados.Any() // Pregunto si tiene algun registro cargado
                ? Empleados.Max(x => x.Legajo) + 1 // SI DEL IF => Obtengo el maximo valor del LEgajo + 1
                : 1;
        }

        public static Empleado ObtenerPorNombreUsuario(string nombreUsuario) 
        {

            var usuario = SeguridadServicio.Usuarios.FirstOrDefault(x => x.NombreUsuario == nombreUsuario);

            if (usuario == null) throw new Exception("El Usuario no Existe");

            return Empleados.FirstOrDefault(x => x.Id == usuario.EmpleadoId);
        }

        public static Empleado Obtener(long id) {
            return Empleados.FirstOrDefault(x => x.Id == id);
        }

        public static List<Empleado> Obtener(string cadenaBuscar)
        {
            return Empleados.Where(x => x.Apellido.Contains(cadenaBuscar)
                                        || x.Nombre.Contains(cadenaBuscar)
                                        || x.Dni == cadenaBuscar)
                .ToList();
        }
    }
}
