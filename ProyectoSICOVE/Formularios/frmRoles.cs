﻿using ProyectoSICOVE.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoSICOVE.Formularios
{
    public partial class frmRoles : Form
    {
        public frmRoles()
        {
            InitializeComponent();
        }
        tb_Roles roles = new tb_Roles();
        private void frmRoles_Load(object sender, EventArgs e)
        {
            cargardatos();
            txtRol.Focus();
        }
        void cargardatos()
        {
            using (SICOVEEntities db = new SICOVEEntities())
            {
                var tb_Roles = db.tb_Roles;
                foreach (var iterardatostbUsuario in tb_Roles)
                {
                    dgvRoles.Rows.Add(
                        iterardatostbUsuario.IdRole, 
                        iterardatostbUsuario.Nombre);
                }
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (SICOVEEntities db = new SICOVEEntities())
            {
                roles.Nombre = txtRol.Text;

                db.tb_Roles.Add(roles);
                db.SaveChanges();
            }
            MessageBox.Show("El Rol se ha Registrado con éxito");
            dgvRoles.Rows.Clear();
            cargardatos();
            txtRol.Clear();
            txtRol.Focus();
        }

        private void dgvRol_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String Roles = dgvRoles.CurrentRow.Cells[1].Value.ToString();
            txtRol.Text = Roles;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (SICOVEEntities db = new SICOVEEntities())
            {
                string Id = dgvRoles.CurrentRow.Cells[0].Value.ToString();
                int IdC = int.Parse(Id);
                roles = db.tb_Roles.Where(VerificarId => VerificarId.IdRole == IdC).First();
                roles.Nombre = txtRol.Text;
                db.Entry(roles).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            MessageBox.Show("El Rol se ha Actuliazado con éxito");
            dgvRoles.Rows.Clear();
            cargardatos();
            txtRol.Clear();
            txtRol.Focus();
        }

        private void btnEminiar_Click(object sender, EventArgs e)
        {
            using (SICOVEEntities db = new SICOVEEntities())
            {
                string Id = dgvRoles.CurrentRow.Cells[0].Value.ToString();

                roles = db.tb_Roles.Find(int.Parse(Id));
                db.tb_Roles.Remove(roles);
                db.SaveChanges();
            }
            MessageBox.Show("El Rol se ha Eliminado con éxito");
            dgvRoles.Rows.Clear();
            cargardatos();
            txtRol.Clear();
            txtRol.Focus();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}