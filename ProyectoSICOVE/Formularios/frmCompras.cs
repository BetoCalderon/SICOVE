using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoSICOVE.Model;
using ProyectoSICOVE.Formularios.frmBusquedas;

namespace ProyectoSICOVE.Formularios
{
    public partial class frmCompras : Form
    {
        public frmCompras()
        {
            InitializeComponent();
        }
        tb_Compras compras = new tb_Compras();
        tb_Inventarios inventarios = new tb_Inventarios();
        private void frmCompras_Load(object sender, EventArgs e)
        {
            CargarComboFPago();
        }

        void CargarComboFPago()
        {
            using (SICOVE1Entities db = new SICOVE1Entities())
            {
                //cargando el combo de forma de pago.
                var FormaPago = db.tb_FormaPago.ToList();

                cmbFormaPago.DataSource = FormaPago;
                cmbFormaPago.DisplayMember = "Nombre";
                cmbFormaPago.ValueMember = "IdFormaPago";
                if (cmbFormaPago.Items.Count > 1)
                    cmbFormaPago.SelectedIndex = -1;
                cmbFormaPago.Text = "Seleccione";

                //cargando el combo con proveedores.
                var Proveedores = db.tb_Proveedores.ToList();

                cmbProveedor.DataSource = Proveedores;
                cmbProveedor.DisplayMember = "Nombre";
                cmbProveedor.ValueMember = "IdProveedor";
                if (cmbProveedor.Items.Count > 1)
                    cmbProveedor.SelectedIndex = -1;
                cmbProveedor.Text = "Seleccione";
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnRegistrarProveedor_Click(object sender, EventArgs e)
        {
            frmProveedor proveedor = new frmProveedor();
            proveedor.ShowDialog();
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            frmBuscarProducto productos = new frmBuscarProducto();
            productos.ShowDialog();
        }
    }
}
