using FontAwesome.Sharp;
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
    public partial class FrmInicio : Form
    {
        private static Entidades.Usuario usuarioActual = null;
        private static IconMenuItem MenuActivo = null;
        private static Form FormularioActivo = null;

        public FrmInicio(Entidades.Usuario objUsuarioActual)
        {
            InitializeComponent();
            usuarioActual = objUsuarioActual;
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {
            lblUsuarioActual.Text = usuarioActual.NombreCompleto;
        }

        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {

            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.White;
            }
            menu.BackColor = Color.Silver;
            MenuActivo = menu;

            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }

            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.SteelBlue;

            contenedor.Controls.Add(formulario);
            formulario.Show();


        }

        private void menuCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuViajes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FrmViajes(usuarioActual));
        }
    }
}
