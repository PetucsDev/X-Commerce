using Commerce.Servicios;
using System;
using System.Windows.Forms;

namespace Commerce
{
    public partial class FormClientes : Form
    {
        private int filaSeleccionada;
        private string clienteSeleccionado;

        public FormClientes()
        {
            InitializeComponent();

            DoubleBuffered = true;
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {
            PoblarGrilla(txtBuscar.Text);           
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

            dgv.Columns["DireccionCompleta"].Visible = true;
            dgv.Columns["DireccionCompleta"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["DireccionCompleta"].HeaderText = "Domicilio";

            dgv.Columns["Dni"].Visible = true;
            dgv.Columns["Dni"].Width = 100;
            dgv.Columns["Dni"].HeaderText = "DNI";

            dgv.Columns["FechaNacimientoStr"].Visible = true;
            dgv.Columns["FechaNacimientoStr"].HeaderText = "Fecha Nac.";
            dgv.Columns["FechaNacimientoStr"].Width = 150;
            dgv.Columns["FechaNacimientoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["TieneLimiteCompra"].Visible = true;
            dgv.Columns["TieneLimiteCompra"].HeaderText = "Limite Compra";
            dgv.Columns["TieneLimiteCompra"].Width = 150;
            dgv.Columns["TieneLimiteCompra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dgv.Columns["MontoLimiteCompra"].Visible = true;
            dgv.Columns["MontoLimiteCompra"].HeaderText = "Monto Maximo";
            dgv.Columns["MontoLimiteCompra"].Width = 150;
            dgv.Columns["MontoLimiteCompra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var fNuevoCliente = new FormNuevoCliente();
            fNuevoCliente.ShowDialog();
            FormClientes_Load(sender, e);
        }

        private void btnCuentaCorriente_Click(object sender, EventArgs e)
        {
            filaSeleccionada = dgv.CurrentRow.Index;
            clienteSeleccionado = (dgv.Rows[filaSeleccionada].Cells[0].Value).ToString();

             var fCuentaCorriente = new FormCuentaCorriente();
             fCuentaCorriente._clienteSeleccionado = clienteSeleccionado;
             fCuentaCorriente.ShowDialog();   
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir de la Ventana?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgv.DataSource = ClienteServicio
               .Obtener(!string.IsNullOrEmpty(txtBuscar.Text) ? txtBuscar.Text : string.Empty);

            FormatearGrilla(dgv);
        }
    }
}
