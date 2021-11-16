namespace Commerce.Entidades
{
    public class DetalleFactura
    {
        public long Id { get; set; }

        public int NumeroFactura { get; set; }

        public int CodigoProducto { get; set; }

        public string DescripcionProducto { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Cantidad { get; set; }

        public decimal SubTotal1 { get; set; }
        public decimal SubTotal => PrecioUnitario * Cantidad;
    }
}
