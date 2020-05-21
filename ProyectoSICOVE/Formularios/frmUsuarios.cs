using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoSICOVE.Model;

namespace ProyectoSICOVE.Formularios
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }
        tb_Usuarios user = new tb_Usuarios();
        private void frmUsuarios_Load(object sender, EventArgs e)
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
            using (SICOVEEntities db = new SICOVEEntities())
            {
                var innerJoin =from tb_Roles in db.tb_Roles
                               from tb_Usuarios in db.tb_Usuarios
                                where  tb_Roles.IdRole == tb_Usuarios.IdRoles


                               select new
                                {
                                    IdUsuario = tb_Usuarios.IdUsuario,
                                    Usuario = tb_Usuarios.Usuario,
                                    Clave = tb_Usuarios.Clave,
                                    Rol = tb_Roles.Nombre,
                                    IdRol = tb_Roles.IdRole

                                };

                foreach (var iterardatostbUsuario in innerJoin)
                {
                    dgvUsuarios.Rows.Add(
                        iterardatostbUsuario.IdUsuario,
                        iterardatostbUsuario.Usuario,
                        iterardatostbUsuario.Clave,
                        iterardatostbUsuario.Rol,
                        iterardatostbUsuario.IdRol);
                }
            }
        }
        void limpiardatos()
        {
            txtUsuario.Clear();
            txtClave.Text = "";
            cmbRol.Text = "";

           // txtUsuario.Focus();
        }
        void CargarCombo()
        {
            using (SICOVEEntities db = new SICOVEEntities())
            {
                var Rol = db.tb_Roles.ToList();

                cmbRol.DataSource = Rol;
                cmbRol.DisplayMember = "Nombre";
                cmbRol.ValueMember = "IdRole";
                if (cmbRol.Items.Count > 1)
                    cmbRol.SelectedIndex = -1;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SICOVEEntities db = new SICOVEEntities())
                {
                    user.Usuario = txtUsuario.Text;
                    user.Clave = txtClave.Text;

                    String comboRol = cmbRol.SelectedValue.ToString();
                    user.IdRoles = Convert.ToInt32(comboRol);

                    db.tb_Usuarios.Add(user);
                    db.SaveChanges();
                }
                MessageBox.Show("Usuario registrado con éxito");
                dgvUsuarios.Rows.Clear();
                cargarGridview();
                limpiardatos();
                CargarCombo();
            }
            catch(Exception ex)
            {
                MessageBox.Show("...");
            }
            
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvUsuarios_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            String usuario = dgvUsuarios.CurrentRow.Cells[1].Value.ToString();
            String clave = dgvUsuarios.CurrentRow.Cells[2].Value.ToString();
            String rol = dgvUsuarios.CurrentRow.Cells[3].Value.ToString();
            txtUsuario.Text = usuario;
            txtClave.Text = clave;
            cmbRol.Text = rol;
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            
            try
            {
                using (SICOVEEntities db = new SICOVEEntities())
                {
                    string Id = dgvUsuarios.CurrentRow.Cells[0].Value.ToString();
                    int IdC = int.Parse(Id);
                    user = db.tb_Usuarios.Where(VerificarId => VerificarId.IdUsuario == IdC).First();
                    user.Usuario = txtUsuario.Text;
                    user.Clave = txtClave.Text;

                    String comboRol = cmbRol.SelectedValue.ToString();
                    user.IdRoles = Convert.ToInt32(comboRol);

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                MessageBox.Show("Se Actualizo con éxito");
                dgvUsuarios.Rows.Clear();
                cargarGridview();
                limpiardatos();
                CargarCombo();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Algo Salio Mal, intente de nuevo. ");
            }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SICOVEEntities db = new SICOVEEntities())
                {
                    string Id = dgvUsuarios.CurrentRow.Cells[0].Value.ToString();

                    user = db.tb_Usuarios.Find(int.Parse(Id));
                    db.tb_Usuarios.Remove(user);
                    db.SaveChanges();
                }
                MessageBox.Show("El registro se eliminó con éxito");
                dgvUsuarios.Rows.Clear();
                cargarGridview();
                limpiardatos();
                CargarCombo();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Algo salio mal, intente de nuevo");
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            btnGuardar.Enabled = true;
            btnEditar.Enabled = true;
            btnNuevo.Enabled = false;
            btnEliminar.Enabled = true;

            dgvUsuarios.Rows.Clear();
            cargarGridview();
            limpiardatos();
            CargarCombo();
        }
    }
}
