using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Commerce.Entidades;
using FontAwesome.Sharp;

namespace Commerce
{
    public partial class Principal : Form
    {
        private IconButton btnActual;
        private Panel pnlBordeBoton;
        private Form formularioHijoActual;

        private Empleado empleado;

        /// <summary>
        /// Constructor Vacio
        /// </summary>
        public Principal()
        {
            InitializeComponent();

            pnlBordeBoton = new Panel();
            pnlBordeBoton.Size = new Size(7, 39);
            pnlMenu.Controls.Add(pnlBordeBoton);
            this.DoubleBuffered = true;
            

        }

        /// <summary>
        /// Constructor con Empleado
        /// </summary>
        /// <param name="empleado">Empleado Login</param>
        public Principal(Empleado empleado)
            : this() 
        {
            this.empleado = empleado;
             
            
        }
                
        private void btnSalirSistema_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿ Desea salir del Sistema ?", "Atención", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void imgCerrar_Click(object sender, EventArgs e)
        {
            btnSalirSistema.PerformClick();
        }

        private void imgMaximizar_Click(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Normal 
                ? FormWindowState.Maximized 
                : FormWindowState.Normal;
        }

        private void imgMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            ActivarBoton(sender, Color.Orange);
            AbrirFormularioHijo(new FormVentas());
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ActivarBoton(sender, Color.Orange);
            AbrirFormularioHijo(new FormClientes());
        }

        private void btnProducto_Click(object sender, EventArgs e)
        {
            ActivarBoton(sender, Color.Orange);
            AbrirFormularioHijo(new FormProductos());
        }

        private void imgLogo_Click(object sender, EventArgs e)
        {
            if (formularioHijoActual != null)
            {
                formularioHijoActual.Close();
            }

            Reset();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Principal_Resize(object sender, EventArgs e)
        {
            FormBorderStyle = WindowState == FormWindowState.Maximized 
                ? FormBorderStyle.None 
                : FormBorderStyle.Sizable;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (pnlMenu.Width == 147)
            {
                btnVenta.Text = string.Empty;
                btnClientes.Text = string.Empty;
                btnProducto.Text = string.Empty;
                btnSalirSistema.Text = string.Empty;
                pnlMenu.Width = 75;
                imgLogo.Image = Properties.Resources.LogoSolo;
                imgLogo.Height = imgLogo.Width;
            }
            else
            {
                btnVenta.Text = "Ventas";
                btnClientes.Text = "Clientes";
                btnProducto.Text = "Productos";
                btnSalirSistema.Text = "Salir del Sistema";
                pnlMenu.Width = 147;
                imgLogo.Image = Properties.Resources.LogoCommerce2;
                imgLogo.Height = 58;
            }
        }

        // Metodos Privados
        private void ActivarBoton(object sender, Color color)
        {
            if (sender != null)
            {
                DesactivarBoton();

                btnActual = (IconButton) sender;
                btnActual.BackColor = Color.FromArgb(80, 80, 80);
                btnActual.ForeColor = color;
                btnActual.IconColor = color;
                btnActual.TextAlign = ContentAlignment.MiddleCenter;

                imgTitulo.IconChar = btnActual.IconChar;
                lblTituloFormulario.Text = btnActual.Text;

                pnlBordeBoton.BackColor = color;
                pnlBordeBoton.Location = new Point(0, btnActual.Location.Y);
                pnlBordeBoton.Visible = true;
                pnlBordeBoton.BringToFront();
            }
        }

        private void DesactivarBoton()
        {
            if (btnActual != null)
            {
                btnActual.BackColor = Color.FromArgb(50, 50, 50);
                btnActual.ForeColor = Color.FromArgb(224, 224, 224);
                btnActual.IconColor = Color.FromArgb(224, 224, 224);
                btnActual.TextAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void AbrirFormularioHijo(Form formularioHijo)
        {
            if (formularioHijoActual != null)
            {
                formularioHijoActual.Close();
            }

            formularioHijoActual = formularioHijo;
            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;
            this.pnlCentral.Controls.Add(formularioHijo);
            this.pnlCentral.Tag = formularioHijo;
            formularioHijo.BringToFront();
            formularioHijo.Show();
        }

        private void Reset()
        {
            DesactivarBoton();
            imgTitulo.IconChar = IconChar.Home;
            lblTituloFormulario.Text = "Home";
            pnlBordeBoton.Visible = false;
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            if (empleado != null) {
                lblUsuarioLogin.Text = empleado.ApyNomCompleto;
            }
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            ActivarBoton(sender, Color.Orange);
            AbrirFormularioHijo(new FormEmpleados());
        }
    }
}
