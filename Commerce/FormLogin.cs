using Commerce.Entidades;
using Commerce.Servicios;
using System;
using System.Windows.Forms;

namespace Commerce
{
    public partial class FormLogin : Form
    {
        private Empleado empleadoLogin;
        public Empleado Empleado => empleadoLogin;

        public FormLogin()
        {
            InitializeComponent();

            empleadoLogin = null;
            txtUsuario.Text = "mcostilla";
            txtPassword.Text = "P$assword";

        }

        private void linkRegistrarEmpleado_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var fNuevoEmpleado = new FormNuevoEmpleado();
            fNuevoEmpleado.ShowDialog();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Salir?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if(SeguridadServicio.VerificarSiExiste(txtUsuario.Text, txtPassword.Text))
            {
                empleadoLogin = EmpleadoServicio.ObtenerPorNombreUsuario(txtUsuario.Text);

              
                
                var fprincipal = new Principal();
                fprincipal.lblUsuarioLogin.Text = empleadoLogin.ApyNomCompleto;
                EmpleadoServicio.empleadoLogeado = empleadoLogin.Id;
                fprincipal.ShowDialog();
            }
            else
            {
                MessageBox.Show("El Usuario o Contraseña son incorrectos. Intente nuevamente", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

            this.Close();
        }

    }
}
