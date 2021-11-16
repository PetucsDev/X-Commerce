using System;

namespace Commerce.Entidades
{
    public class CuentaCorriente
    {
        public long Id { get; set; }

        public long ClienteId { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Monto { get; set; }

        public TipoMovimiento TipoMovimiento { get; set; }
    }
}
