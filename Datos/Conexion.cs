using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    internal static class Conexion
    {
        internal static string strConexion = ConfigurationManager.ConnectionStrings["cadena_conexion"].ToString();
    }
}
