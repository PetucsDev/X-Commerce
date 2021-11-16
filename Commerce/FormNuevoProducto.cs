using Commerce.Entidades;
using Commerce.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Commerce
{
    public partial class FormNuevoProducto : Form
    {
        public FormNuevoProducto()
        {
            InitializeComponent();
            txtCodigo.Text = "1";
        }

        private void imgCerrar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text)
               || !string.IsNullOrEmpty(txtCodigoBarra.Text)
               || !string.IsNullOrEmpty(txtDescripcion.Text))
            {
                if (MessageBox.Show("Desea cerrar la carga de un nuevo producto", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
            }

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea cerrar la carga de un nuevo producto", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("Por favor ingrese el codigo");
                txtCodigo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCodigoBarra.Text))
            {
                MessageBox.Show("Por favor ingrese un codigo de barras");
                txtCodigoBarra.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MessageBox.Show("Por favor ingrese la descripcion");
                txtDescripcion.Focus();
                return;
            }

            try
            {
                var nuevoProducto = new Producto
                {                   
                    Codigo = int.Parse(txtCodigo.Text),
                    CodigoBarra = txtCodigoBarra.Text,
                    Descripcion = txtDescripcion.Text,
                    Precio = nudPrecio.Value, 
                    Cantidad = nudStockInicial.Value,
                };

                ProductoServicio.Add(nuevoProducto);

                MessageBox.Show("Los datos se grabaron Correctamente", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un Error Grave. Contactese con el Profesor", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void FormNuevoProducto_Load(object sender, EventArgs e)
        {
            txtCodigo.Text = ProductoServicio.Codigo().ToString();
        }
    }
}
