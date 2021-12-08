using Presentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace Presentacion
{
    public partial class FrmViajes : Form
    {
        private static Entidades.Usuario usuarioActual = null;
        public FrmViajes(Entidades.Usuario objEntidadesUsuario)
        {
            InitializeComponent();
            usuarioActual = objEntidadesUsuario;
        }

        private void FrmViajes_Load(object sender, EventArgs e)
        {
            ListarViajes();

            txtID.Text = "0";
            HabilitarButton(this.btnGuardar);
            DeshabilitarButton(this.btnEditar);
            DeshabilitarButton(this.btnEliminar);

            foreach (DataGridViewColumn columna in dgvListadoViajes.Columns)
            {

                if (columna.Visible == true && columna.Name != "btnSeleccionar" && columna.Name != "btnVerDetalle")
                {
                    cboFiltro.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }

            cboFiltro.DisplayMember = "Texto";
            cboFiltro.ValueMember = "Valor";
            cboFiltro.SelectedIndex = 0;
        }

        private void ListarViajes()
        {
            dgvListadoViajes.Rows.Clear();  

            List<Entidades.Viaje> listaViajes = new Logica.Viaje().Listar();

            foreach (Entidades.Viaje item in listaViajes)
            {

                dgvListadoViajes.Rows.Add(new object[] {"",item.IdViaje,item.oUsuario.NombreUsuario,
                    item.Titulo,
                    item.Descripcion,
                    item.FechaRegistro
                });
            }

            ContarRegistrosVisibles();
        }

        private void dgvListadoViajes_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                return;
            }

            if(e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check20.Width;
                var h = Properties.Resources.check20.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(x, y, w, h));
                e.Handled = true;
            }

            if (e.ColumnIndex == 6)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.detalle.Width;
                var h = Properties.Resources.detalle.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.detalle, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void DeshabilitarButton(IconButton boton)
        {
            boton.Enabled = false;
            boton.BackColor = Color.Silver;
        }

        private void HabilitarButton(IconButton boton)
        {
            boton.Enabled = true;
            if(boton.Name == "btnEditar")
            {
                boton.BackColor = Color.RoyalBlue;
            }
            else if (boton.Name == "btnEliminar")
            {
                boton.BackColor = Color.Firebrick;
            }
            else if (boton.Name == "btnGuardar")
            {
                boton.BackColor = Color.Green;
            }

        }

        private void frm_detalle_closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }

        private void dgvListadoViajes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvListadoViajes.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if(indice >= 0)
                {
                    txtID.Text = dgvListadoViajes.Rows[indice].Cells["IdViaje"].Value.ToString();
                    txtTitulo.Text = dgvListadoViajes.Rows[indice].Cells["Titulo"].Value.ToString();
                    txtDescripcion.Text = dgvListadoViajes.Rows[indice].Cells["Descripcion"].Value.ToString();
                    DeshabilitarButton(this.btnGuardar);
                    HabilitarButton(this.btnEditar);
                    HabilitarButton(this.btnEliminar);
                }
            }

            if(dgvListadoViajes.Columns[e.ColumnIndex].Name == "btnVerDetalle")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    int id = Convert.ToInt32(dgvListadoViajes.Rows[indice].Cells["IdViaje"].Value);
                    string titulo = dgvListadoViajes.Rows[indice].Cells["Titulo"].Value.ToString();
                    FrmDetalleViajes form = new FrmDetalleViajes(id,titulo);
                    form.Show();
                    this.Hide();
                    form.FormClosing += frm_detalle_closing;
                }

            }
        }

        private void LimpiarCeldas()
        {
            txtID.Text = "";
            txtTitulo.Text = "";
            txtDescripcion.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            Entidades.Viaje objEntidadesViaje = new Entidades.Viaje();
            objEntidadesViaje.oUsuario = usuarioActual;
            objEntidadesViaje.Titulo = txtTitulo.Text;
            objEntidadesViaje.Descripcion = txtDescripcion.Text;

            int IdViajeGenerado = new Logica.Viaje().Registrar(objEntidadesViaje, out Mensaje);

            if(IdViajeGenerado != 0)
            {
                ListarViajes();
                LimpiarCeldas();
                
            }
            else
            {
                MessageBox.Show(Mensaje);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboFiltro.SelectedItem).Valor.ToString();

            if (dgvListadoViajes.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvListadoViajes.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }

            ContarRegistrosVisibles();

        }

        private void ContarRegistrosVisibles()
        {
            int cant = 0;

            foreach (DataGridViewRow row in dgvListadoViajes.Rows)
            {
                if(row.Visible)
                    cant++;
            }

            lblTotalViajes.Text = "Total de viajes: " + cant.ToString();
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvListadoViajes.Rows)
            {
                row.Visible = true;
            }
            ListarViajes();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            Entidades.Viaje objEntidadesViaje = new Entidades.Viaje();
            objEntidadesViaje.IdViaje = Convert.ToInt32(txtID.Text);
            objEntidadesViaje.oUsuario = usuarioActual;
            objEntidadesViaje.Titulo = txtTitulo.Text;
            objEntidadesViaje.Descripcion = txtDescripcion.Text;

            bool resultado = new Logica.Viaje().Editar(objEntidadesViaje, out Mensaje);

            if (resultado)
            {
                ListarViajes();
                LimpiarCeldas();

            }
            else
            {
                MessageBox.Show(Mensaje);
            }

            HabilitarButton(this.btnGuardar);
            DeshabilitarButton(this.btnEditar);
            DeshabilitarButton(this.btnEliminar);
            txtID.Text = "0";

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            Entidades.Viaje objEntidadesViaje = new Entidades.Viaje();
            objEntidadesViaje.IdViaje = Convert.ToInt32(txtID.Text);

            DialogResult op = MessageBox.Show("Esta seguro que desea eliminar el viaje?", "Confirmacion de eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (op == DialogResult.Yes)
            {
                bool resultado = new Logica.Viaje().Eliminar(objEntidadesViaje, out Mensaje);

                if (resultado)
                {
                    ListarViajes();
                    LimpiarCeldas();
                    MessageBox.Show("El viaje ha sido eliminado con exito!");

                }
                else
                {
                    MessageBox.Show(Mensaje);
                }

            }

            HabilitarButton(this.btnGuardar);
            DeshabilitarButton(this.btnEditar);
            DeshabilitarButton(this.btnEliminar);
            txtID.Text = "0";



        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCeldas();
            HabilitarButton(btnGuardar);
            DeshabilitarButton(btnEditar);
            DeshabilitarButton(btnEliminar);
            txtID.Text = "0";
        }
    }
}
