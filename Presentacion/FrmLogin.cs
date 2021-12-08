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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            // Primero oculto el campo de texto de la clave
            txtClave.PasswordChar = '*';
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if(checkBox.Checked == true)
            {
                txtClave.PasswordChar = '\0';
            }
            else
            {
                txtClave.PasswordChar = '*';
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            Entidades.Usuario objEntidadesUsuario = new Logica.Usuario().Listar().Where(u => u.NombreUsuario == txtNombreUsuario.Text && u.Clave == txtClave.Text).FirstOrDefault();

            if (objEntidadesUsuario != null)
            {

                FrmInicio form = new FrmInicio(objEntidadesUsuario);

                form.Show();
                this.Hide();

                form.FormClosing += frm_closing;

            }
            else
            {
                MessageBox.Show("No se pudo encontrar el usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            txtNombreUsuario.Text = "";
            txtClave.Text = "";
            this.Show();
        }
    }
}
