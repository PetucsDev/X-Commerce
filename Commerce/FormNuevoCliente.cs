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
    public partial class FormNuevoCliente : Form
    {
        public FormNuevoCliente()
        {
            InitializeComponent();
            txtCodigo.Text = "1";
        }

        private void imgCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea cerrar la carga de un nuevo cliente", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

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

            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                MessageBox.Show("Por favor ingrese un apellido");
                txtApellido.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Por favor ingrese un Nombre");
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDni.Text))
            {
                MessageBox.Show("Por favor ingrese el Dni");
                txtDni.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCalle.Text))
            {
                MessageBox.Show("Por favor ingrese una calle");
                txtCalle.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtNumero.Text))
            {
                MessageBox.Show("Por favor ingrese un numero");
                txtNumero.Focus();
                return;
            }

            try
            {
                var nuevoCliente = new Cliente
                {
                    Codigo = int.Parse(txtCodigo.Text),
                    Apellido = txtApellido.Text,
                    Nombre = txtNombre.Text,
                    Dni = txtDni.Text,
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Calle = txtCalle.Text,
                    Numero = txtNumero.Text,
                    Piso = txtPiso.Text,
                    Dpto = txtDpto.Text,
                    //gb limite compra
                    TieneLimiteCompra = chkLimiteCompra.Checked,
                    MontoLimiteCompra = nudMontoLimiteCompra.Value,
                };

                ClienteServicio.Add(nuevoCliente);

                MessageBox.Show("Los datos se grabaron Correctamente","Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un Error Grave. Contactese con el Profesor", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void FormNuevoCliente_Load(object sender, EventArgs e)
        {
            txtCodigo.Text = ClienteServicio.Codigo().ToString();
        }
    }
}
