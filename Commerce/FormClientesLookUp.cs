using Commerce.Entidades;
using Commerce.Servicios;
using System;
using System.Windows.Forms;

namespace Commerce
{
    public partial class FormClientesLookUp : Form
    {
        public object EntidadSeleccionada;

        private  Cliente _cliente;

        public FormClientesLookUp()
        {
            InitializeComponent();

            DoubleBuffered = true;

            _cliente = new Cliente();
            EntidadSeleccionada = null;
        }

        private void PoblarGrilla(string cadenaBuscar)
        {
            dgv.DataSource = ClienteServicio
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
            dgv.Columns["Codigo"].HeaderText = "Código";
            dgv.Columns["Codigo"].Width = 100;
            
            dgv.Columns["ApyNomCompleto"].Visible = true;
            dgv.Columns["ApyNomCompleto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["ApyNomCompleto"].HeaderText = "Apellido y Nombre";

            dgv.Columns["Dni"].Visible = true;
            dgv.Columns["Dni"].Width = 100;
            dgv.Columns["Dni"].HeaderText = "DNI";
        }

        private void FormClientesLookUp_Load(object sender, EventArgs e)
        {
            PoblarGrilla(!string.IsNullOrEmpty(txtBuscar.Text) ? txtBuscar.Text : string.Empty);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
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

            _cliente.Id = (long)dgv["Id", e.RowIndex].Value;

            _cliente = (Cliente)dgv.Rows[e.RowIndex].DataBoundItem;

            EntidadSeleccionada = _cliente;
        }
    }
}
