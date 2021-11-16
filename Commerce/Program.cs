using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using Commerce.Entidades;
using Commerce.Servicios;

namespace Commerce
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
       
        static void Main()
        {
             

            SeguridadServicio.ObtenerDatosDelArchivo();
            EmpleadoServicio.ObtenerDatosDelArchivo();
            CuentaCorrienteServicio.ObtenerDatosDelArchivo();
            ClienteServicio.ObtenerDatosDelArchivo();
            ProductoServicio.ObtenerDatosDelArchivo();
           
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var fLogin = new FormLogin();
            fLogin.ShowDialog();

            if (fLogin.Empleado != null)
            {
                Application.Run(new Principal(fLogin.Empleado));
            }
            else 
            {
                Application.Exit();
            }
        }
    }
}
