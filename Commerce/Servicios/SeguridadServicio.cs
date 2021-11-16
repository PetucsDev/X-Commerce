using Commerce.Entidades;
using Commerce.PlantillasDeCorte;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Commerce.Servicios
{
    public static class SeguridadServicio
    {
        private static string NombreArchivo = $@"{Environment.CurrentDirectory}\Usuarios.txt";

        public static List<Usuario> Usuarios = new List<Usuario>();

        public static void ObtenerDatosDelArchivo()
        {
            string[] usuarios = File.ReadAllLines(NombreArchivo);

            foreach (var linea in usuarios)
            {
                if (string.IsNullOrEmpty(linea)) continue;

                var nuevoUsuario = new Usuario
                {
                    EmpleadoId = long.Parse(linea.Substring(PlantillaUsuario.EmpleadoIdDesde, PlantillaUsuario.EmpleadoIdCantidad)),
                    NombreUsuario = linea.Substring(PlantillaUsuario.NombreDesde, PlantillaUsuario.NombreCantidad).Trim(),
                    Password = linea.Substring(PlantillaUsuario.PasswordDesde, PlantillaUsuario.PasswordCantidad).Trim()
                };

                Usuarios.Add(nuevoUsuario);
            }
        }

        public static bool VerificarSiExiste(string usuairo, string password) 
        {
            return Usuarios.Any(usu => usu.NombreUsuario == usuairo && usu.Password == password);
        }
    }
}
