﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoSICOVE.Model;

namespace ProyectoSICOVE.Formularios
{
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }
        tb_Productos productos = new tb_Productos();
        private void frmProductos_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            btnGuardar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            cargarGridview();
            limpiardatos();
            CargarCombo();
        }

        void cargarGridview()
        {
            using (SICOVE1Entities db = new SICOVE1Entities())
            {
                var innerJoin = from tb_Categorias in db.tb_Categorias
                                from tb_Productos in db.tb_Productos
                                where tb_Categorias.IdCategoria == tb_Productos.IdCategoria


                                select new
                                {
                                    IdProducto = tb_Productos.IdProducto,
                                    Nombre = tb_Productos.Nombre,
                                    Detalle = tb_Productos.Detalle,
                                    Fecha = tb_Productos.FechaRegistro,
                                    Categoria = tb_Categorias.Nombre,
                                    IdCategoria = tb_Categorias.IdCategoria

                                };

                foreach (var iterardatostbUsuario in innerJoin)
                {
                    dgvProductos.Rows.Add(
                        iterardatostbUsuario.IdProducto,
                        iterardatostbUsuario.Nombre,
                        iterardatostbUsuario.Detalle,
                        iterardatostbUsuario.Fecha,
                        iterardatostbUsuario.Categoria,
                        iterardatostbUsuario.IdCategoria
                        );
                }
            }
        }
        void limpiardatos()
        {
            txtNombre.Clear();
            txtDetalle.Text = "";
            cmbCategoria.Text = "";

            txtNombre.Focus();
        }
        void CargarCombo()
        {
            using (SICOVE1Entities db = new SICOVE1Entities())
            {
                //cargando el combo de Rol, con los roles disponibles
                var Categoria = db.tb_Categorias.ToList();

                cmbCategoria.DataSource = Categoria;
                cmbCategoria.DisplayMember = "Nombre";
                cmbCategoria.ValueMember = "IdCategoria";
                if (cmbCategoria.Items.Count > 1)
                    cmbCategoria.SelectedIndex = -1;
                cmbCategoria.Text = "Seleccione";
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {

                groupBox1.Enabled = true;
                btnGuardar.Enabled = true;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;

                btnNuevo.Enabled = false;

                dgvProductos.Rows.Clear();
                cargarGridview();
                limpiardatos();
                CargarCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo salio mal, intente de nuevo");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SICOVE1Entities db = new SICOVE1Entities())
                {
                    productos.Nombre = txtNombre.Text;
                    productos.Detalle = txtDetalle.Text;

                    productos.FechaRegistro = Convert.ToDateTime(dtpFechaReg.Text);

                    String comboCategoria = cmbCategoria.SelectedValue.ToString();
                    productos.IdCategoria = Convert.ToInt32(comboCategoria);

                    db.tb_Productos.Add(productos);
                    db.SaveChanges();
                }
                MessageBox.Show("Usuario registrado con éxito");
                dgvProductos.Rows.Clear();
                cargarGridview();
                limpiardatos();
                CargarCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo salio mal... " + ex.ToString());
            }

        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String Nombre = dgvProductos.CurrentRow.Cells[1].Value.ToString();
            String Detalle = dgvProductos.CurrentRow.Cells[2].Value.ToString();
            String fecha = dgvProductos.CurrentRow.Cells[3].Value.ToString();
            String Categoria = dgvProductos.CurrentRow.Cells[4].Value.ToString();
            txtNombre.Text = Nombre;
            txtDetalle.Text = Detalle;
            dtpFechaReg.Text = fecha;
            cmbCategoria.Text = Categoria;

            btnGuardar.Enabled = false;
            btnNuevo.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SICOVE1Entities db = new SICOVE1Entities())
                {
                    string Id = dgvProductos.CurrentRow.Cells[0].Value.ToString();
                    int IdC = int.Parse(Id);
                    productos = db.tb_Productos.Where(VerificarId => VerificarId.IdProducto == IdC).First();
                    productos.Nombre = txtNombre.Text;
                    productos.Detalle = txtDetalle.Text;

                    productos.FechaRegistro = Convert.ToDateTime(dtpFechaReg.Text);

                    String comboCategoria = cmbCategoria.SelectedValue.ToString();
                    productos.IdCategoria = Convert.ToInt32(comboCategoria);


                    db.Entry(productos).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                MessageBox.Show("Se Actualizo con éxito");
                dgvProductos.Rows.Clear();
                cargarGridview();
                limpiardatos();
                CargarCombo();

                btnGuardar.Enabled = true;
                btnNuevo.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo Salio Mal, intente de nuevo. ");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SICOVE1Entities db = new SICOVE1Entities())
                {
                    string Id = dgvProductos.CurrentRow.Cells[0].Value.ToString();

                    productos = db.tb_Productos.Find(int.Parse(Id));
                    db.tb_Productos.Remove(productos);
                    db.SaveChanges();
                }
                MessageBox.Show("El registro se eliminó con éxito");
                dgvProductos.Rows.Clear();
                cargarGridview();
                limpiardatos();
                CargarCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo salio mal, intente de nuevo");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            frmCategorias categorias = new frmCategorias();
            categorias.ShowDialog();
        }
    }
}
