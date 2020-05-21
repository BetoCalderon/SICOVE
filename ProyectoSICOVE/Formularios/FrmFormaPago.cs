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

namespace ProyectoSICOVE.Formularios
{
    public partial class frmFormaPago : Form
    {
        public frmFormaPago()
        {
            InitializeComponent();
        }
        tb_FormaPago formaPago = new tb_FormaPago();
        private void frmFormaPago_Load(object sender, EventArgs e)
        {
            cargardatos();
            txtFPago.Focus();
        }
        void cargardatos()
        {
            using (SICOVEEntities db = new SICOVEEntities())
            {
                var tb_FormaPago = db.tb_FormaPago;
                foreach (var iterardatostbUsuario in tb_FormaPago)
                {
                    dgvFPagos.Rows.Add(iterardatostbUsuario.IdFormaPago, iterardatostbUsuario.Nombre);
                }
                //dgvUsuarios.DataSource = db.tb_usuarios.ToList();
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (SICOVEEntities db = new SICOVEEntities())
            {
                formaPago.Nombre = txtFPago.Text;

                db.tb_FormaPago.Add(formaPago);
                db.SaveChanges();
            }
            MessageBox.Show("La Forma de Pago se ha registrado con éxito");
            dgvFPagos.Rows.Clear();
            cargardatos();
            txtFPago.Clear();
            txtFPago.Focus();
        }

        private void dgvFPagos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String fPagos = dgvFPagos.CurrentRow.Cells[1].Value.ToString();
            txtFPago.Text = fPagos;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (SICOVEEntities db = new SICOVEEntities())
            {
                string Id = dgvFPagos.CurrentRow.Cells[0].Value.ToString();
                int IdC = int.Parse(Id);
                formaPago = db.tb_FormaPago.Where(VerificarId => VerificarId.IdFormaPago == IdC).First();
                formaPago.Nombre = txtFPago.Text;
                db.Entry(formaPago).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            MessageBox.Show("La Forma de Pago se ha Actualizado con éxito");
            dgvFPagos.Rows.Clear();
            cargardatos();
            txtFPago.Clear();
            txtFPago.Focus();
        }

        private void btnEminiar_Click(object sender, EventArgs e)
        {
            using (SICOVEEntities db = new SICOVEEntities())
            {
                string Id = dgvFPagos.CurrentRow.Cells[0].Value.ToString();

                formaPago = db.tb_FormaPago.Find(int.Parse(Id));
                db.tb_FormaPago.Remove(formaPago);
                db.SaveChanges();
            }
            MessageBox.Show("La Forma de Pago se ha Eliminado con éxito");
            dgvFPagos.Rows.Clear();
            cargardatos();
            txtFPago.Clear();
            txtFPago.Focus();
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
