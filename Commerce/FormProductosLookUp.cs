using Commerce.Entidades;
using Commerce.Servicios;
using System;
using System.Windows.Forms;

namespace Commerce
{
    public partial class FormProductosLookUp : Form
    {
        public object EntidadSeleccionada;
        private Producto _producto;

        public FormProductosLookUp()
        {
            InitializeComponent();

            DoubleBuffered = true;

            _producto = new Producto();

            EntidadSeleccionada = null;
        }

        private void PoblarGrilla(string cadenaBuscar)
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            PoblarGrilla(txtBuscar.Text);
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
           
            Close();
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.RowCount <= 0) return;

            _producto.Id = (long)dgv["Id", e.RowIndex].Value;

            _producto = (Producto)dgv.Rows[e.RowIndex].DataBoundItem;

            EntidadSeleccionada = _producto;
        }

        private void FormProductosLookUp_Load(object sender, EventArgs e)
        {
            PoblarGrilla(!string.IsNullOrEmpty(txtBuscar.Text) ? txtBuscar.Text : string.Empty);
        }
    }
}
