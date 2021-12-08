using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Usuario
    {
        private Datos.Usuario objDatosUsuario = new Datos.Usuario();

        public List<Entidades.Usuario> Listar()
        {
            return objDatosUsuario.Listar();
        }
    }
}
