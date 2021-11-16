namespace Commerce.Entidades
{
    public class Stock
    {
        public long ProductoId { get; set; }

        public decimal Cantidad { get; set; }

        public TipoMovimiento TipoMovimiento { get; set; }  
    }
}
