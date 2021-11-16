using System;
using System.Windows.Forms;
using Commerce.Entidades;
using Commerce.Servicios;

namespace Commerce
{
    public partial class FormAjusteStock : Form
    {
        private Producto _producto;
        private long idProductoSeleccionado;
        private string cantProductSeleccionado;

        
        public FormAjusteStock(long productoGridSeleccionado, string cantidadGridSeleccionada)

        {
            InitializeComponent();

            idProductoSeleccionado =  productoGridSeleccionado;
            cantProductSeleccionado = cantidadGridSeleccionada;
            nudStock.Value = decimal.Parse(cantidadGridSeleccionada);

        }

        private void imgCerrar_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("¿Desea Salir", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;
            this.Close();
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if (nudStock.Value == 0)
            {
                MessageBox.Show("Por favor ingrese una cantidad de Stock", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                nudStock.Focus();
                return;
            }
            
                try
                {

                    var nuevoStock = new Stock
                    {
                        ProductoId = idProductoSeleccionado,
                        Cantidad = nudStock.Value,
                        TipoMovimiento = TipoMovimiento.Ingreso 
                    };

                    StockServicio.Modificar(nuevoStock);
                MessageBox.Show("Los datos se grabaron Correctamente", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                Close();


                var fNuevoProducto = new FormProductos();
                fNuevoProducto.PoblarGrilla(string.Empty);

            }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio un Error Grave. Contactese con el Profesor", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            
        }

       
    }
}
