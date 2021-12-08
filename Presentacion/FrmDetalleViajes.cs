using FontAwesome.Sharp;
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

namespace Presentacion
{
    public partial class FrmDetalleViajes : Form
    {
        private int IdViaje = 0;
        private string TituloViaje = string.Empty;
        public FrmDetalleViajes(int IdViaje, string TituloViaje)
        {
            InitializeComponent();
            this.IdViaje = IdViaje;
            this.TituloViaje = TituloViaje;
        }

        private void FrmDetalleViajes_Load(object sender, EventArgs e)
        {
            txtIdViaje.Text = IdViaje.ToString();
            txtTituloViaje.Text = TituloViaje.ToString();

            ListarDetalleViaje();

            txtIdDetalleViaje.Text = "0";
            HabilitarButton(this.btnGuardar);
            DeshabilitarButton(this.btnEditar);
            DeshabilitarButton(this.btnEliminar);

            foreach (DataGridViewColumn columna in dgvListadoDias.Columns)
            {

                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboFiltro.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }

            cboFiltro.DisplayMember = "Texto";
            cboFiltro.ValueMember = "Valor";
            cboFiltro.SelectedIndex = 0;
        }

        private void ListarDetalleViaje()
        {
            dgvListadoDias.Rows.Clear();

            List<Entidades.Detalle_Viaje> listaDetallesViaje = new Logica.Detalle_Viaje().Listar(IdViaje);

            foreach (Entidades.Detalle_Viaje item in listaDetallesViaje)
            {
                dgvListadoDias.Rows.Add(new object[] {"",item.IdDetalleViaje,item.NroDia,item.Descripcion,
                    item.FechaRegistro
                });
            }

            ContarRegistrosVisibles();
        }

        private void DeshabilitarButton(IconButton boton)
        {
            boton.Enabled = false;
            boton.BackColor = Color.Silver;
        }

        private void HabilitarButton(IconButton boton)
        {
            boton.Enabled = true;
            if (boton.Name == "btnEditar")
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

        private void dgvListadoDias_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 0)
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

        private void dgvListadoDias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvListadoDias.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIdDetalleViaje.Text = dgvListadoDias.Rows[indice].Cells["IdDetalleViaje"].Value.ToString();
                    txtDiaNro.Text = dgvListadoDias.Rows[indice].Cells["DiaNro"].Value.ToString();
                    txtDescripcion.Text = dgvListadoDias.Rows[indice].Cells["Descripcion"].Value.ToString();
                    DeshabilitarButton(this.btnGuardar);
                    HabilitarButton(this.btnEditar);
                    HabilitarButton(this.btnEliminar);
                }
            }
        }
        private void LimpiarCeldas()
        {
            txtDiaNro.Text = "";
            txtDescripcion.Text = "";
        }

        private void ContarRegistrosVisibles()
        {
            int cant = 0;

            foreach (DataGridViewRow row in dgvListadoDias.Rows)
            {
                if (row.Visible)
                    cant++;
            }

            lblTotalDias.Text = "Total de dias: " + cant.ToString();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboFiltro.SelectedItem).Valor.ToString();

            if (dgvListadoDias.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvListadoDias.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }

            ContarRegistrosVisibles();
        }

        private void btnLimpiarBusqueda_Click_1(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvListadoDias.Rows)
            {
                row.Visible = true;
            }
            ListarDetalleViaje();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            if(txtDiaNro.Text == String.Empty)
            {
                MessageBox.Show("Es necesario el numero de dia del viaje");
                return;
            }

            Entidades.Detalle_Viaje objEntidadesDetalleViaje = new Entidades.Detalle_Viaje();
            objEntidadesDetalleViaje.IdViaje = IdViaje;
            objEntidadesDetalleViaje.NroDia = Convert.ToInt32(txtDiaNro.Text);
            objEntidadesDetalleViaje.Descripcion = txtDescripcion.Text;

            int IdDetalleViajeGenerado = new Logica.Detalle_Viaje().Registrar(objEntidadesDetalleViaje, out Mensaje);

            if (IdDetalleViajeGenerado != 0)
            {
                ListarDetalleViaje();
                LimpiarCeldas();

            }
            else
            {
                MessageBox.Show(Mensaje);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            Entidades.Detalle_Viaje objEntidadesDetalleViaje = new Entidades.Detalle_Viaje();
            objEntidadesDetalleViaje.IdDetalleViaje = Convert.ToInt32(txtIdDetalleViaje.Text);
            objEntidadesDetalleViaje.IdViaje = Convert.ToInt32(txtIdViaje.Text);
            objEntidadesDetalleViaje.NroDia = Convert.ToInt32(txtDiaNro.Text);
            objEntidadesDetalleViaje.Descripcion = txtDescripcion.Text;

            bool resultado = new Logica.Detalle_Viaje().Editar(objEntidadesDetalleViaje, out Mensaje);

            if (resultado)
            {
                ListarDetalleViaje();
                LimpiarCeldas();

            }
            else
            {
                MessageBox.Show(Mensaje);
            }

            HabilitarButton(this.btnGuardar);
            DeshabilitarButton(this.btnEditar);
            DeshabilitarButton(this.btnEliminar);
            txtIdDetalleViaje.Text = "0";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            Entidades.Detalle_Viaje objEntidadesDetalleViaje = new Entidades.Detalle_Viaje();
            objEntidadesDetalleViaje.IdDetalleViaje = Convert.ToInt32(txtIdDetalleViaje.Text);

            DialogResult op = MessageBox.Show("Esta seguro que desea eliminar el viaje?", "Confirmacion de eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (op == DialogResult.Yes)
            {
                bool resultado = new Logica.Detalle_Viaje().Eliminar(objEntidadesDetalleViaje, out Mensaje);

                if (resultado)
                {
                    ListarDetalleViaje();
                    LimpiarCeldas();
                    MessageBox.Show("El dia del viaje ha sido eliminado con exito!");

                }
                else
                {
                    MessageBox.Show(Mensaje);
                }

            }

            HabilitarButton(this.btnGuardar);
            DeshabilitarButton(this.btnEditar);
            DeshabilitarButton(this.btnEliminar);
            txtIdDetalleViaje.Text = "0";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCeldas();
            HabilitarButton(btnGuardar);
            DeshabilitarButton(btnEditar);
            DeshabilitarButton(btnEliminar);
            txtIdDetalleViaje.Text = "0";
        }
    }
}
