using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Usuario
    {
        public List<Entidades.Usuario> Listar()
        {
            List<Entidades.Usuario> lista =  new List<Entidades.Usuario>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.strConexion))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT u.IdUsuario,u.DNI,u.NombreCompleto,u.NombreUsuario,u.Correo,u.Clave,u.Estado,u.FechaRegistro FROM Usuarios u");

                    SqlCommand cmd = new SqlCommand(query.ToString(), objConexion);
                    cmd.CommandType = CommandType.Text;

                    objConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            lista.Add(new Entidades.Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                DNI = dr["DNI"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                            });

                        }

                    }


                }
                catch (Exception ex)
                {

                    lista = new List<Entidades.Usuario>();
                }
            }

            return lista;
        }

    }
}
