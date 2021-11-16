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
    public partial class FormEliminarItem : Form
    {
        
        public FormEliminarItem()
        {
            InitializeComponent();
              
        }

        private void imgCerrar_Click(object sender, EventArgs e)
        {
            nudStockEliminar.Value = 0;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            nudStockEliminar.Value = 0;
            if (MessageBox.Show("Desea Cerrar la Ventana", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Close();
            
        }

       
    }
}
