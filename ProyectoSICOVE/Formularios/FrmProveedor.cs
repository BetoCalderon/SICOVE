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
    public partial class frmProveedor : Form
    {
        public frmProveedor()
        {
            InitializeComponent();
        }
        tb_Proveedores proveedores = new tb_Proveedores();
        private void frmProveedor_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            btnGuardar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            dgvProveedores.Rows.Clear();
            cargardatos();
            limpiartxt();
        }

        void cargardatos()
        {
            try
            {

                using (SICOVE1Entities db = new SICOVE1Entities())
                {
                    var tb_Proveedores = db.tb_Proveedores;
                    foreach (var iterardatostbUsuario in tb_Proveedores)
                    {
                        dgvProveedores.Rows.Add(
                            iterardatostbUsuario.IdProveedor,
                            iterardatostbUsuario.Nombre,
                            iterardatostbUsuario.Direccion,
                            iterardatostbUsuario.Celular,
                            iterardatostbUsuario.DUI,
                            iterardatostbUsuario.FechaRegistro);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Algo salio mal... Intente de nuevo");
            }
        }

        void limpiartxt()
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtCelular.Clear();
            txtDUI.Clear();

            txtNombre.Focus();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SICOVE1Entities db = new SICOVE1Entities())
                {
                    proveedores.Nombre = txtNombre.Text;
                    proveedores.Direccion = txtDireccion.Text;
                    proveedores.Celular = txtCelular.Text;
                    proveedores.DUI = txtDUI.Text;
                    proveedores.FechaRegistro = Convert.ToDateTime(dtpFechaReg.Text);

                    db.tb_Proveedores.Add(proveedores);
                    db.SaveChanges();
                }
                MessageBox.Show("El Proveedor se ha Registrado con éxito");
                dgvProveedores.Rows.Clear();
                cargardatos();
                limpiartxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo salio mal... Intente de nuevo");
            }
        }


        private void dgvProveedores_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                String Nombre = dgvProveedores.CurrentRow.Cells[1].Value.ToString();
                String Direccion = dgvProveedores.CurrentRow.Cells[2].Value.ToString();
                String Celular = dgvProveedores.CurrentRow.Cells[3].Value.ToString();
                String DUI = dgvProveedores.CurrentRow.Cells[4].Value.ToString();
                String Fecha = dgvProveedores.CurrentRow.Cells[5].Value.ToString();
                txtNombre.Text = Nombre;
                txtDireccion.Text = Direccion;
                txtCelular.Text = Celular;
                txtDUI.Text = DUI;
                dtpFechaReg.Text = Fecha;

                btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo salio mal... Intente de nuevo");
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {

                using (SICOVE1Entities db = new SICOVE1Entities())
                {
                    string Id = dgvProveedores.CurrentRow.Cells[0].Value.ToString();
                    int IdC = int.Parse(Id);
                    proveedores = db.tb_Proveedores.Where(VerificarId => VerificarId.IdProveedor == IdC).First();
                    proveedores.Nombre = txtNombre.Text;
                    proveedores.Direccion = txtDireccion.Text;
                    proveedores.Celular = txtCelular.Text;
                    proveedores.DUI = txtDUI.Text;
                    proveedores.FechaRegistro = Convert.ToDateTime(dtpFechaReg.Text);
                    db.Entry(proveedores).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                MessageBox.Show("El Proveedor se ha Actualizado con éxito");
                dgvProveedores.Rows.Clear();
                cargardatos();
                limpiartxt();

                btnGuardar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo salio mal... Intente de nuevo");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                using (SICOVE1Entities db = new SICOVE1Entities())
                {
                    string Id = dgvProveedores.CurrentRow.Cells[0].Value.ToString();

                    proveedores = db.tb_Proveedores.Find(int.Parse(Id));
                    db.tb_Proveedores.Remove(proveedores);
                    db.SaveChanges();
                }
                MessageBox.Show("El Proveedor se ha Eliminado con éxito");
                dgvProveedores.Rows.Clear();
                cargardatos();
                limpiartxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo salio mal... Intente de nuevo");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnxCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {

                groupBox1.Enabled = true;
                btnGuardar.Enabled = true;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                dgvProveedores.Rows.Clear();
                cargardatos();
                limpiartxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo salio mal... Intente de nuevo" + ex.ToString() + MessageBoxIcon.Error);
            }
        }
    }
}
