using ProyectoSICOVE.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoSICOVE.Formularios.frmBusquedas
{
    public partial class frmBuscarProducto : Form
    {
        public frmBuscarProducto()
        {
            InitializeComponent();
        }

        private void btnCerrar1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmBuscarProducto_Load(object sender, EventArgs e)
        {
            filtro();
        }

        void filtro()
        {
            using (SICOVE1Entities db = new SICOVE1Entities())
            {
                string nombre = txtBuscarProducto.Text;

                var buscarprod = from tb_Categorias in db.tb_Categorias
                                 from tb_productos in db.tb_Productos
                                 where tb_productos.IdProducto == tb_Categorias.IdCategoria 
                                 where tb_productos.Nombre.Contains(nombre)
                                 //where tb_Categorias.Nombre.Contains(nombre)


                                 select new
                                 {
                                     Codigo = tb_productos.IdProducto,
                                     Nombre = tb_productos.Nombre,
                                     IdCategoria = tb_Categorias,
                                     Categoria = tb_Categorias.Nombre
                                 };

                dgvBuscarProd.DataSource = buscarprod.ToList();
            }
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            filtro();
        }

        void envio()
        {
            string id = dgvBuscarProd.CurrentRow.Cells[0].Value.ToString();
            string Nombre = dgvBuscarProd.CurrentRow.Cells[1].Value.ToString();
            string IdCategoria = dgvBuscarProd.CurrentRow.Cells[2].Value.ToString();
            string Categoria = dgvBuscarProd.CurrentRow.Cells[3].Value.ToString();

            frmMenu.compras.txtCodProducto.Text = id;
            frmMenu.compras.txtNombreProducto.Text = Nombre;
            frmMenu.compras.txtIdCategoria.Text = IdCategoria;
            frmMenu.compras.txtCategoriaProd.Text = Categoria;

            frmMenu.compras.txtPrecio.Select();
        }

        private void dgvBuscarProd_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            envio();
            frmMenu.compras.txtPrecio.Focus();
            this.Hide();
        }

        private void dgvBuscarProd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                envio();
                frmMenu.compras.txtPrecio.Focus();
                this.Hide();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmProductos productos = new frmProductos();
            productos.ShowDialog();
        }
    }
}
