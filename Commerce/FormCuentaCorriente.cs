using Commerce.Entidades;
using Commerce.Servicios;
using System;
using System.Data;
using System.Windows.Forms;

namespace Commerce
{
    public partial class FormCuentaCorriente : Form
    {
        private CuentaCorriente _cuentaCorriente;
        public object pago;
        public string _clienteSeleccionado;
        public string deuda;
        

        public FormCuentaCorriente( )
        {
            InitializeComponent();

            DoubleBuffered = true;
            _cuentaCorriente = new CuentaCorriente();
           
            pago = null;
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {




           


        }

        private void CalcularDeuda(DataGridView dgv)
        {
            decimal deuda = 0m;
            decimal pagos = 0m;


            foreach (DataGridViewRow dr in dgv.Rows)
            {

                if(dr.Cells["TipoMovimiento"].Value.ToString() == "Egreso")
                {
                    deuda = deuda + decimal.Parse(dr.Cells["Monto"].Value.ToString());
                }

                if(dr.Cells["TipoMovimiento"].Value.ToString() == "Ingreso")
                {
                    pagos = pagos + decimal.Parse(dr.Cells["Monto"].Value.ToString());

                }
            }

            txtSaldo.Text = ( deuda - pagos).ToString();
        }

        private void FormatearGrilla(DataGridView dgv)
        {
            
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                dgv.Columns[i].Visible = false;
            }

            dgv.Columns["Id"].Visible = true;
            dgv.Columns["Id"].Width = 100;
            dgv.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Id"].HeaderText = " Id";

            dgv.Columns["ClienteId"].Visible = true;
            dgv.Columns["ClienteId"].Width = 200;
            dgv.Columns["ClienteId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["ClienteId"].HeaderText = "Cliente Id";

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].Width = 600;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].HeaderText = "Descripcion";

            dgv.Columns["Fecha"].Visible = true;
            dgv.Columns["Fecha"].HeaderText = "Fecha y Hora";
            dgv.Columns["Fecha"].Width = 100;
            dgv.Columns["Fecha"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["Monto"].Visible = true;
            dgv.Columns["Monto"].HeaderText = "Monto";
            dgv.Columns["Monto"].Width = 100;
            dgv.Columns["Monto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["TipoMovimiento"].Visible = true;
            dgv.Columns["TipoMovimiento"].HeaderText = "TipoMovimiento";
            dgv.Columns["TipoMovimiento"].Width = 120;
            dgv.Columns["TipoMovimiento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            
        }


        private void btnNuevoPago_Click(object sender, EventArgs e)
        {
            deuda = txtSaldo.Text;
            var fPago = new FormPagoCtaCte(deuda);
            
            fPago.clienteSeleccionado = _clienteSeleccionado;
            fPago.ShowDialog();
            FormCuentaCorriente_Load(sender, e);


        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir de la Ventana?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;
            this.Close();
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.RowCount <= 0) return;

            _cuentaCorriente.ClienteId = (long)dgv["ClienteId", e.RowIndex].Value;

            _cuentaCorriente = (CuentaCorriente)dgv.Rows[e.RowIndex].DataBoundItem;

            pago= _cuentaCorriente;
                
        }

        private void FormCuentaCorriente_Load(object sender, EventArgs e)
        {
            dgv.Columns.Clear();
            dgv.DataSource = CuentaCorrienteServicio.ObtenerCtaCteCliente(_clienteSeleccionado);

            FormatearGrilla(dgv);
            CalcularDeuda(dgv);

        }
    }
}
