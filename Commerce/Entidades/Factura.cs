using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Commerce.Entidades
{
    public class Factura
    {
        /// <summary>
        /// Constructor de la Clase Factura
        /// </summary>
        public Factura()
        {
            if(Items == null)
                Items = new List<DetalleFactura>();
        }

        public int NumeroFactura { get; set; }

        public string TipoFactura { get; set; }

        public DateTime Fecha { get; set; }

        public long EmpleadoId { get; set; }

        public string ApyNomEmpleado { get; set; }

        public long ClienteId { get; set; }

        public string ApyNomCliente { get; set; }

        public decimal SubTotal => Items.Sum(x => x.SubTotal);

        public decimal Descuento { get; set; }

        public decimal Total => SubTotal - (SubTotal * (Descuento / 100m));

        
        public List<DetalleFactura> Items { get; set; }

        public decimal SubTotal1 { get; set; }

        public decimal Total1 { get; set; }
    }
}
