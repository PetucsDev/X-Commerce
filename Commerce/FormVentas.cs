using Commerce.Entidades;
using Commerce.Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Commerce
{
    public partial class FormVentas : Form
    {
        private Cliente _clienteSeleccionado;
        private Factura _factura;
        private Producto _productoSeleccionado;
        private  DataTable dt = new DataTable();
        private DateTime fecha = DateTime.Now;

        private decimal Subtotal;
        
        public object cantidad;
       
        public FormVentas()
        {
            InitializeComponent();

            DoubleBuffered = true;

            _clienteSeleccionado = new Cliente();
            _productoSeleccionado = new Producto();
            _factura = new Factura();
           
        }

        private void btnNuevoCliente_Click(object sender, System.EventArgs e)
        {
            var fNuevoCliente = new FormNuevoCliente();
            fNuevoCliente.ShowDialog();
        }

        private void btnBuscarCliente_Click(object sender, System.EventArgs e)
        {

            var fLookUpCliente = new FormClientesLookUp(); 
            fLookUpCliente.ShowDialog();

            if (fLookUpCliente.EntidadSeleccionada != null)
            {
                _clienteSeleccionado = (Cliente)fLookUpCliente.EntidadSeleccionada;

                txtApyNomCliente.Text = _clienteSeleccionado.ApyNomCompleto;
                txtCodigoCliente.Text = _clienteSeleccionado.Codigo.ToString();
            }
            else
            {
                _clienteSeleccionado = null;
            }

        }

        private void btnEliminarItem_Click(object sender, System.EventArgs e)
        {
            
            var fEliminarItem = new FormEliminarItem();
            fEliminarItem.nudStockEliminar.Value = decimal.Parse(cantidad.ToString());
            fEliminarItem.ShowDialog();

            var fila = dgv.CurrentRow.Index;

            var resta = decimal.Parse(dgv.Rows[fila].Cells[3].Value.ToString());
        
            if(resta > 0)
            {
                dgv.Rows[fila].Cells[3].Value = resta - fEliminarItem.nudStockEliminar.Value;

                dgv.Rows[fila].Cells[4].Value = decimal.Parse(dgv.Rows[fila].Cells[2].Value.ToString()) * int.Parse(dgv.Rows[fila].Cells[3].Value.ToString());

                CalcularTotal();
            }
            else
            {
                MessageBox.Show("No se puede tener una cantidad negativa.Por favor INGRESE una nueva cantidad", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

        }

        private void btnFacturar_Click(object sender, System.EventArgs e)
        {
           
            if (cmbTipoFactura.SelectedItem != "" && txtNumeroFactura.Text != "" && txtApyNomCliente.Text != "")
            {


                var ventasFacturar = new Factura()
                {

                    NumeroFactura = int.Parse(txtNumeroFactura.Text),
                    TipoFactura = cmbTipoFactura.Text,
                    Fecha = fecha,
                    ClienteId = long.Parse(txtCodigoCliente.Text),
                    Descuento = nudDescuento.Value,
                    SubTotal1 = decimal.Parse(txtSubTotal.Text),
                    Total1 = decimal.Parse(txtTotal.Text),
                    EmpleadoId = EmpleadoServicio.empleadoLogeado,
                    
                    
                };

                var fFacturar = new FormFacturar(txtTotal.Text, ventasFacturar, dt);
                fFacturar.ShowDialog();
            }

            else
            {
                MessageBox.Show("Faltan Campos Obligatorios  por Completar", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

            LimpiarForm(); 

        }

        private void LimpiarForm()
        {
            txtCodigoProducto.Text = string.Empty;
            txtDescripcionProducto.Text = string.Empty;
            txtPrecioProducto.Text = string.Empty;
            nudCantidad.Value = 0m;
            nudSubTotal.Value = 0m;
            cmbTipoFactura.Text = string.Empty;
            txtNumeroFactura.Text = string.Empty;
            txtSubTotal.Text = string.Empty;
            nudDescuento.Value = 0m;
            txtTotal.Text = string.Empty;

            dgv.Columns.Clear();
            
        }

        private void btnCerrar_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("¿ Desea salir de Ventas ?", "Atención", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question)
                            == DialogResult.OK)
            {
                Close();
            }
        }

        private void imgCerrar_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("¿ Desea salir de Ventas ?", "Atención", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                Close();
            }
        }

        private void FormVentas_Load(object sender, System.EventArgs e)
        {
            txtFechaFactura.Text = DateTime.Now.ToShortDateString();


            dt.Columns.Add("CodigoProducto", typeof(string));
            dt.Columns.Add("DescripcionProducto", typeof(string));
            dt.Columns.Add("PrecioUnitario", typeof(string));
            dt.Columns.Add("Cantidad", typeof(decimal));
            dt.Columns.Add("SubTotal", typeof(decimal));

            dgv.DataSource = dt;
            FormatearGrilla(dgv);
            txtApyNomCliente.Text = "Consumidor Final";
        }

        private void txtCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool cliente = false;

            if (ProductoServicio.VerificarSiExiste(txtCodigoProducto.Text))
            {
                _productoSeleccionado = ProductoServicio.ObtenerPorCodigo(txtCodigoProducto.Text);

                txtDescripcionProducto.Text = _productoSeleccionado.Descripcion;

                if (_productoSeleccionado.Precio == 0)
                {
                    txtPrecioProducto.Text = string.Empty;
                }
                else
                {
                    txtPrecioProducto.Text = _productoSeleccionado.Precio.ToString();
                }

                nudCantidad.Value = 1;

                nudSubTotal.Value = _productoSeleccionado.Precio;
            }
            else
            {
                if (txtCodigoProducto.Text != string.Empty)
                {
                    cliente = true;
                }

                if (cliente)
                {
                    if (!ProductoServicio.VerificarSiExiste(txtCodigoProducto.Text))
                    {
                        var prod = new FormProductosLookUp();
                        prod.ShowDialog();

                        if (prod.EntidadSeleccionada != null)
                        {
                            _productoSeleccionado = (Producto)prod.EntidadSeleccionada;

                            txtCodigoProducto.Text = _productoSeleccionado.Codigo.ToString();
                            txtDescripcionProducto.Text = _productoSeleccionado.Descripcion;
                            txtPrecioProducto.Text = _productoSeleccionado.Precio.ToString();
                            nudCantidad.Value = 1;
                            nudSubTotal.Value = _productoSeleccionado.Precio;
                        }
                        else
                        {
                            _productoSeleccionado = null;
                        }
                    }
                }
            }


        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
            if (nudCantidad.Value > 0)
            {
                nudCantidad.Value = nudCantidad.Value;
                nudSubTotal.Value = (decimal.Parse(txtPrecioProducto.Text) * nudCantidad.Value);
            }
        }

        private void nudDescuento_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void txtCodigoCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtCodigoCliente.Text.Length > 0)
            {
                _clienteSeleccionado = ClienteServicio.ObtenerCodigo(txtCodigoCliente.Text);
            }

            txtApyNomCliente.Text = _clienteSeleccionado.ApyNomCompleto;
        }

        private void cmbTipoFactura_SelectedValueChanged(object sender, EventArgs e)
        {
                        
           txtNumeroFactura.Text = (FacturaServicio.ObtenerMaximaFactura(cmbTipoFactura.Text) + 1).ToString(); 
           
        }

        private void FormatearGrilla(DataGridView dgv)
        {
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                dgv.Columns[i].Visible = false;
            }

            dgv.Columns["CodigoProducto"].Visible = true;
            dgv.Columns["CodigoProducto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["CodigoProducto"].HeaderText = "Codigo de Producto";

            dgv.Columns["DescripcionProducto"].Visible = true;
            dgv.Columns["DescripcionProducto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["DescripcionProducto"].HeaderText = "Descripcion";

            dgv.Columns["PrecioUnitario"].Visible = true;
            dgv.Columns["PrecioUnitario"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["PrecioUnitario"].HeaderText = "Precio Unitario";

            dgv.Columns["Cantidad"].Visible = true;
            dgv.Columns["Cantidad"].HeaderText = "Cantidad";
            dgv.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["SubTotal"].Visible = true;
            dgv.Columns["SubTotal"].HeaderText = "Sub Total";
            dgv.Columns["SubTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["SubTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnAgregarItem_Click(object sender, EventArgs e)
        {
            if(txtCodigoProducto.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar un codigo para el producto", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtDescripcionProducto.Text = "";
                nudCantidad.Value = 0m;
                txtPrecioProducto.Text = "";
                nudSubTotal.Value = 0;

                return;
            }


            bool productoExistente = false;

            foreach(DataRow dr in dt.Rows)
            {
                if (dr["CodigoProducto"].ToString() == txtCodigoProducto.Text)
                {
                    dr["Cantidad"] =  decimal.Parse (dr["Cantidad"].ToString()) + nudCantidad.Value;
                    dr["SubTotal"] = decimal.Parse(dr["Cantidad"].ToString()) * decimal.Parse(dr["PrecioUnitario"].ToString());
                    productoExistente = true;
                }
            }

            if (!productoExistente)
            {
                dt.Rows.Add(new object[] { txtCodigoProducto.Text, txtDescripcionProducto.Text, txtPrecioProducto.Text, nudCantidad.Value, nudSubTotal.Value });
            }
           
            dgv.DataSource = dt;
            FormatearGrilla(dgv);
            Subtotal = Subtotal + nudSubTotal.Value;
            txtSubTotal.Text = Subtotal.ToString();
            CalcularTotal();
            LimpiarVenta();
        }


        private void LimpiarVenta()
        {
            nudCantidad.Value = 0;
            nudSubTotal.Value = 0;
            txtPrecioProducto.Text = string.Empty;
            txtCodigoProducto.Text = string.Empty;
            txtDescripcionProducto.Text = string.Empty;

        }

        private void CalcularTotal()
        {
            decimal subtotal = 0m;
            

            foreach (DataRow dr in dt.Rows)
            {
               subtotal = subtotal + decimal.Parse(dr["SubTotal"].ToString());

            }

            txtSubTotal.Text = subtotal.ToString();

            if (nudDescuento.Value > 0)
            {
                nudDescuento.Value = nudDescuento.Value;
                txtTotal.Text = (subtotal - (subtotal * (nudDescuento.Value / 100m))).ToString();
            }

            else
            {
                txtTotal.Text = txtSubTotal.Text;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)   
        {

            if(nudCantidad.Enabled == false)
            {
                nudCantidad.Enabled = true;
                nudCantidad.Focus();
            }

            else
            {
                nudCantidad.Enabled = false;
            }
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.RowCount <= 0) return;
            cantidad = dgv.Rows[e.RowIndex].Cells[3].Value;
        }
    }
}
