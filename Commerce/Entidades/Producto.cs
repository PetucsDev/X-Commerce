using System.Collections.Generic;
using System.Linq;
using Commerce.Servicios;

namespace Commerce.Entidades
{
    public class Producto
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string CodigoBarra { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public decimal Cantidad { get; set; }

        //public decimal Cantidad => StockServicio.ObtenerPorId(Id);
    }
}
