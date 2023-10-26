using CadPsiquiatrico;
using ClnPsiquiatrico;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CpPsiquiatrico
{
    public partial class FrmTerapeuta : Form
    {
        bool esNuevo = false;
        public FrmTerapeuta()
        {
            InitializeComponent();
        }
        private void listar()
        {
            var terapeuta = TerapeutaCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = terapeuta;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["nombre"].HeaderText = "Nombre";
            dgvLista.Columns["apellido"].HeaderText = "Apellido";
            dgvLista.Columns["especialidad"].HeaderText = "Especialidad";
            dgvLista.Columns["telefono"].HeaderText = "Telefono";
            dgvLista.Columns["direccionClinica"].HeaderText = "Dirección Clinica";
            dgvLista.Columns["horarioTrabajo"].HeaderText = "Horario del trabajo";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha del Registro";
            btnEditar.Enabled = terapeuta.Count > 0;
            btnEliminar.Enabled = terapeuta.Count > 0;
            if (terapeuta.Count > 0) dgvLista.Rows[0].Cells["nombre"].Selected = true;

        }

        private void FrmTerapeuta_Load(object sender, EventArgs e)
        {
            //Size = new Size(832, 240); 
            //this.MinimumSize = new Size(832, 364);
            //this.MaximumSize = new Size(832, 364);
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //Size = new Size(600, 364);
            esNuevo = true;
            gbxDatos.BackColor = Color.FromArgb(0, 255, 128);
            txtNombre.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //Size = new Size(600, 364);
            esNuevo = false;
            gbxDatos.BackColor = Color.Cyan;

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var terapeuta = TerapeutaCln.get(id);
            txtNombre.Text = terapeuta.nombre;
            txtApellido.Text = terapeuta.apellido;
            txtEspecialidad.Text = terapeuta.especialidad;
            txtTelefono.Text = terapeuta.telefono;
            txtDireccionClinica.Text = terapeuta.direccionClinica;
            txtHorarioTrabajo.Text = terapeuta.horarioTrabajo;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
           //  Size = new Size(600, 240);
            gbxDatos.BackColor = Color.White;
            limpiar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter) listar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var terapeuta = new Terapeuta();
            terapeuta.nombre = txtParametro.Text.Trim();
            terapeuta.apellido = txtApellido.Text.Trim();
            terapeuta.especialidad = txtEspecialidad.Text;
            terapeuta.telefono = txtTelefono.Text;
            terapeuta.direccionClinica = txtDireccionClinica.Text;
            terapeuta.horarioTrabajo = txtHorarioTrabajo.Text;
            terapeuta.usuarioRegistro = "SIS257"; 
            if (esNuevo)
            {
                terapeuta.fechaRegistro = DateTime.Now;
                terapeuta.estado = 1;
                TerapeutaCln.insertar(terapeuta);
            }
            else
            {
                int index = dgvLista.CurrentCell.RowIndex;
                terapeuta.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                TerapeutaCln.actualizar(terapeuta);
            }
            listar();
            btnCancelar.PerformClick();
            MessageBox.Show("Terapeuta guardado correctamente", "::: Psiquiátrico - Mensaje::: ",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            gbxDatos.BackColor = Color.Yellow;
        }
        private void limpiar()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEspecialidad.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccionClinica.Text = string.Empty;
            txtHorarioTrabajo.Text = string.Empty;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string nombre = dgvLista.Rows[index].Cells["nombre"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja el terapeuta {nombre}?",
                "::: LabPsiquiátrico - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                TerapeutaCln.eliminar(id, "SIS457");
                listar();
                MessageBox.Show("Producto dado de baja correctamente", "::: LabPsiquiátrico - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
