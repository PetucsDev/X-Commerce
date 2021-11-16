using System;
using System.Windows.Forms;
using Commerce.Entidades;
using Commerce.Servicios;

namespace Commerce
{
    public partial class FormPagoCtaCte : Form
    {
        private Producto _producto;
        public string clienteSeleccionado;
        private CuentaCorriente ctacte = new CuentaCorriente();
        private DateTime fecha = DateTime.Now;
        public FormPagoCtaCte( string saldo)
        {
            InitializeComponent();

            nudMonto.Value = decimal.Parse(saldo);
        }

        private void imgCerrar_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar el Pago?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;
            this.Close();
        }

        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar el Pago?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            this.Close();
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            var ctacte = new CuentaCorriente()
            {
                Id = CuentaCorrienteServicio.MaxId(),
                ClienteId = long.Parse(clienteSeleccionado),
                Descripcion = "Pago CtaCte",
                Fecha = fecha,
                Monto = nudMonto.Value,
                TipoMovimiento = TipoMovimiento.Ingreso,
            };
            CuentaCorrienteServicio.Add(ctacte);

            MessageBox.Show("Pago acreditado satisfactoriamente", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            this.Close();
           
        }

        
    }
}
