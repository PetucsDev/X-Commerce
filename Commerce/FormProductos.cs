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
    public partial class FormProductos : Form
    {
        private int  NumeroDeFilaSeleccionada;
        private long codigoProductoSeleccionado;
        private string cantidadProductoSeleccionado;
        public object cant;
        public FormProductos()
        {
            InitializeComponent();

            DoubleBuffered = true;

        }

        public void PoblarGrilla(string cadenaBuscar)
        {
            dgv.DataSource = ProductoServicio
                .Obtener(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            FormatearGrilla(dgv);
        }

        private void FormatearGrilla(DataGridView dgv)
        {
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                dgv.Columns[i].Visible = false;
            }

            dgv.Columns["Codigo"].Visible = true;
            dgv.Columns["Codigo"].HeaderText = "Codigo";
            dgv.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["CodigoBarra"].Visible = true;
            dgv.Columns["CodigoBarra"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["CodigoBarra"].HeaderText = "Codigo de Barras";

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].HeaderText = "Descripcion";

            dgv.Columns["Precio"].Visible = true;
            dgv.Columns["Precio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Precio"].HeaderText = "Precio";

            dgv.Columns["Cantidad"].Visible = true;
            dgv.Columns["Cantidad"].HeaderText = "Cantidad";
            dgv.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var fNuevoProducto = new FormNuevoProducto();
            fNuevoProducto.ShowDialog();
            FormProductos_Load(sender, e);
        }

        private void btnCargaStock_Click(object sender, EventArgs e)
        {

            if (dgv.RowCount > 0)
            {
                NumeroDeFilaSeleccionada = dgv.CurrentRow.Index;
                codigoProductoSeleccionado = (long) dgv.Rows[NumeroDeFilaSeleccionada].Cells[0].Value;
                cantidadProductoSeleccionado =  dgv.Rows[NumeroDeFilaSeleccionada].Cells[5].Value.ToString();

                var fAjusteStock = new FormAjusteStock(codigoProductoSeleccionado, cantidadProductoSeleccionado);
                fAjusteStock.ShowDialog();
                dgv.Rows[NumeroDeFilaSeleccionada].Cells[5].Value = fAjusteStock.nudStock.Value;

            }
                 
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir de la Ventana", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;
            this.Close();
        }

        private void FormProductos_Load(object sender, EventArgs e)
        {
            PoblarGrilla(txtBuscar.Text);
               
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.RowCount <= 0) return;

            cant = dgv.Rows[e.RowIndex].Cells[5].Value;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            PoblarGrilla(txtBuscar.Text);
            FormatearGrilla(dgv);
        }
    }
}
