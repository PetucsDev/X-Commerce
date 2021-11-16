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
    public partial class FormFacturar : Form
    {
        public Factura clienteVenta;
        public DataTable dtclienteVentaDetalle;
        public CuentaCorriente ctaCteCliente;
        public DataTable dtCtaCteCliente;
        private DateTime fecha = DateTime.Now;
        private Cliente clienteCta;
        public long idIncremental;
        public FormFacturar(string total, Factura ventasFacturar, DataTable detalleFactura)
        {
            InitializeComponent();
            nudtotal.Value = decimal.Parse(total);
            clienteVenta = ventasFacturar;
            dtclienteVentaDetalle = detalleFactura;
            idIncremental = FacturaServicio.ObtenerSiguienteId();
            if (clienteVenta.ClienteId == 0)
            {
                cmbFormaPago.SelectedItem = "EFECTIVO";
            }

        }

        private void imgCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar su Venta?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            Close();
        }

        private void nudPagaCon_ValueChanged(object sender, EventArgs e)
        {
            decimal vuelto;
           
            vuelto =  nudPagaCon.Value - nudtotal.Value;
            if(cmbFormaPago.SelectedItem == "EFECTIVO")
            {

                if (vuelto < 0)
                {
                    txtVuelto.Text = "";
                    MessageBox.Show("Le falta dinero para completar el pago", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                }

                else
                {
                    txtVuelto.Text = vuelto.ToString();
                    txtVuelto.BackColor = System.Drawing.Color.Green;

                }

            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool puedeFacturar = false;

            if (cmbFormaPago.SelectedItem == "EFECTIVO")
            {
                if((decimal.Parse(txtVuelto.Text) >= 0)) 
                {
                    puedeFacturar = true;
                }
                else
                {
                    MessageBox.Show("Dinero Insuficiente", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
            }

             if ( cmbFormaPago.SelectedItem == "CUENTA CORRIENTE")   
            {
               
                puedeFacturar = PagoCuentaCorrientes(clienteVenta);   // creo la cuenta corriente en el txt, si no da saldo, devuelve false.

            }

            if (puedeFacturar)
            {
                
                FacturaServicio.Add(clienteVenta); // creo la factura en el txt.
                foreach (DataRow dr in dtclienteVentaDetalle.Rows)
                {
                    var clienteDetalle = new DetalleFactura()
                    {

                        Id = idIncremental,
                        NumeroFactura = clienteVenta.NumeroFactura,
                        CodigoProducto = int.Parse(dr["CodigoProducto"].ToString()),
                        DescripcionProducto = dr["DescripcionProducto"].ToString(),
                        PrecioUnitario = decimal.Parse(dr["PrecioUnitario"].ToString()),
                        Cantidad = decimal.Parse(dr["Cantidad"].ToString()),
                        SubTotal1 = decimal.Parse(dr["SubTotal"].ToString()),
                    };

                    FacturaServicio.AddDetalleFactura(clienteDetalle); // creo los productos en el detalle de factura
                    ActualizarStockProductos(clienteDetalle); //actualizo el stock en el txt de productos.
                    
                }

                MessageBox.Show("La venta ha sido exitosa", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }


        private bool PagoCuentaCorrientes(Factura clienteVenta)
        {
            decimal deudaTotal = 0;
            var cliente = new Cliente();
            cliente = ClienteServicio.ObtenerLimiteCompra(clienteVenta.ClienteId);
            if (cliente.TieneLimiteCompra)
            {
                decimal deudaActualCliente;
                deudaActualCliente = CuentaCorrienteServicio.ObtenerDeuda(clienteVenta.ClienteId);
                deudaTotal = deudaActualCliente + nudtotal.Value;

                if (deudaTotal > cliente.MontoLimiteCompra)
                {
                    MessageBox.Show("El cliente no dispone de saldo para realizar la Compra", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return false;
                }
            }

            var ctaCte = new CuentaCorriente()
            {
                Id = CuentaCorrienteServicio.MaxId(),
                ClienteId = clienteVenta.ClienteId,
                Descripcion = clienteVenta.TipoFactura + clienteVenta.NumeroFactura,
                Fecha = fecha,
                Monto = nudtotal.Value,
                TipoMovimiento = TipoMovimiento.Egreso,
            };

            CuentaCorrienteServicio.Add(ctaCte);
            return true;
        }

        private void ActualizarStockProductos(DetalleFactura clienteDetalle)
        {
            var productoVendido = new Producto();

            productoVendido = ProductoServicio.ObtenerPorCodigo(clienteDetalle.CodigoProducto.ToString());

            var nuevoStock = new Stock()
            {
                Cantidad = productoVendido.Cantidad - clienteDetalle.Cantidad,
                ProductoId = productoVendido.Id,
            };

            StockServicio.Modificar(nuevoStock);
           
            ProductoServicio.ObtenerDatosDelArchivo();
        }

        private void cmbFormaPago_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cmbFormaPago.SelectedItem == "CUENTA CORRIENTE")
            {
                nudPagaCon.Enabled = false;
                txtVuelto.Enabled = false;
            }
        }
       
    }
}
