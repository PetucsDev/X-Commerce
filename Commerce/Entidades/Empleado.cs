using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Entidades
{
    public class Empleado
    {
        // Propiedades
        public long Id { get; set; }

        public int Legajo { get; set; }

        public string Apellido { get; set; }

        public string Nombre { get; set; }

        public string ApyNomCompleto => $"{Apellido} {Nombre}";

        public string Dni { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string FechaNacimientoStr => FechaNacimiento.ToShortDateString();

        public string DireccionCompleta
        {
            get
            {
                var domicilio = $"{Calle} Nro: {Numero}";

                if (!string.IsNullOrEmpty(Piso)) {
                    domicilio += $" Piso: {Piso}";
                }

                if (!string.IsNullOrEmpty(Dpto))
                {
                    domicilio += $" Dpto: {Dpto}";
                }

                return domicilio;
            }
        }

        public string Calle { get; set; }

        public string Numero { get; set; }

        public string Piso { get; set; }

        public string Dpto { get; set; }
    }
}
