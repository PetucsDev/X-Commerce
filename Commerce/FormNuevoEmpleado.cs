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
    public partial class FormNuevoEmpleado : Form
    {
        public FormNuevoEmpleado()
        {
            InitializeComponent();
        }

        private void imgCerrar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtApellido.Text)
                || !string.IsNullOrEmpty(txtNombre.Text)
                || !string.IsNullOrEmpty(txtDni.Text)
                || !string.IsNullOrEmpty(txtCalle.Text)
                || !string.IsNullOrEmpty(txtNumero.Text)) {
                if (MessageBox.Show("Desea cerrar la carga de un nuevo empleado", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) {
                    return;
                }
            }

            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtApellido.Text)) {
                MessageBox.Show("Por favor ingrese un apellido");
                txtApellido.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Por favor ingrese un nombre");
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDni.Text))
            {
                MessageBox.Show("Por favor ingrese un DNI");
                txtDni.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCalle.Text))
            {
                MessageBox.Show("Por favor ingrese una Calle");
                txtCalle.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtNumero.Text))
            {
                MessageBox.Show("Por favor ingrese un Numero");
                txtNumero.Focus();
                return;
            }

            try
            {
                var nuevoEmpleado = new Empleado
                {
                    Legajo = int.Parse(txtLegajo.Text),
                    Apellido = txtApellido.Text,
                    Nombre = txtNombre.Text,
                    Dni = txtDni.Text,
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Calle = txtCalle.Text,
                    Numero = txtNumero.Text,
                    Piso = txtPiso.Text,
                    Dpto = txtDpto.Text
                };

                EmpleadoServicio.Add(nuevoEmpleado);

                MessageBox.Show("Los datos se grabaron Correctamente", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                Close();
            }
            catch (Exception ex) {
                MessageBox.Show("Ocurrio un Error Grave. Contactese con el Profesor", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void FormNuevoEmpleado_Load(object sender, EventArgs e)
        {
            txtLegajo.Text = EmpleadoServicio.ObtenerSiguienteLegajo().ToString();
            dtpFechaNacimiento.Value = DateTime.Today;
        }

    }
}
