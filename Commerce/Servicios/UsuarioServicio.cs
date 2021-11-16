using Commerce.Entidades;
using Commerce.PlantillasDeCorte;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Servicios
{
    public static class UsuarioServicio
    {
        private static string NombreArchivo = $@"{Environment.CurrentDirectory}\Usuarios.txt";

        public static void Crear(long empleadoId, string apellidoEmpleado, string nombreEmpleado) {

            var nuevoUsuario = new Usuario()
            {
                EmpleadoId = empleadoId,
                NombreUsuario = ObtenerNombre(apellidoEmpleado, nombreEmpleado),
                Password = "P$assword"
            };

            // Grabamos en la lista Statica de Seguridad para poder Loguearnos
            SeguridadServicio.Usuarios.Add(nuevoUsuario);

            // Grabar en el Archivo de Usuario
            var archivoUsuario = new StreamWriter(NombreArchivo, true);

            var crearLinea = $"{nuevoUsuario.EmpleadoId.ToString().PadLeft(PlantillaUsuario.EmpleadoIdCantidad, '0')}" +
                $"{nuevoUsuario.NombreUsuario.PadRight(PlantillaUsuario.NombreCantidad, ' ')}" +
                $"{nuevoUsuario.Password.PadRight(PlantillaUsuario.PasswordCantidad, ' ')}";

            archivoUsuario.WriteLine(crearLinea);
            archivoUsuario.Close();
        }

        private static string ObtenerNombre(string apellido, string nombre) {

            int contadorLetras = 1;

            string nombreUsuarioNuevo = $"{nombre.Trim().Substring(0, contadorLetras)}{apellido.Trim()}";

            while (SeguridadServicio.Usuarios.Any(x => x.NombreUsuario == nombreUsuarioNuevo.ToLower())) 
            {
                contadorLetras++;

                nombreUsuarioNuevo = $"{nombre.Trim().Substring(0, contadorLetras)}{apellido.Trim()}";
            }
            
            return nombreUsuarioNuevo.ToLower();
        }
    }
}
