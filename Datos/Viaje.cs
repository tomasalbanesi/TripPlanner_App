using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Viaje
    {
        public List<Entidades.Viaje> Listar()
        {
            List<Entidades.Viaje> lista = new List<Entidades.Viaje>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.strConexion))
            {

                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT v.IdViaje,u.IdUsuario,u.NombreUsuario,v.Titulo,v.Descripcion,v.FechaRegistro FROM Viajes v");
                    query.AppendLine("INNER JOIN Usuarios u on u.IdUsuario = v.IdUsuario");


                    SqlCommand cmd = new SqlCommand(query.ToString(), objConexion);
                    cmd.CommandType = CommandType.Text;

                    objConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            lista.Add(new Entidades.Viaje()
                            {
                                IdViaje = Convert.ToInt32(dr["IdViaje"]),
                                oUsuario = new Entidades.Usuario() { IdUsuario = Convert.ToInt32(dr["IdUsuario"]), NombreUsuario = dr["NombreUsuario"].ToString()},
                                Titulo = dr["Titulo"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                            });

                        }

                    }


                }
                catch (Exception ex)
                {

                    lista = new List<Entidades.Viaje>();
                }
            }

            return lista;

        }

        public int Registrar(Entidades.Viaje objEntidadesViaje, out string Mensaje)
        {
            int IdViajeGenerado = 0;
            Mensaje = String.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.strConexion))
                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarViaje", objConexion);
                    cmd.Parameters.AddWithValue("@IdUsuario",objEntidadesViaje.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@Titulo", objEntidadesViaje.Titulo);
                    cmd.Parameters.AddWithValue("@Descripcion", objEntidadesViaje.Descripcion);
                    cmd.Parameters.Add("IdResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("MsjResultado", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    objConexion.Open();

                    cmd.ExecuteNonQuery();

                    IdViajeGenerado = Convert.ToInt32(cmd.Parameters["IdResultado"].Value);
                    Mensaje = cmd.Parameters["MsjResultado"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                IdViajeGenerado = 0;
                Mensaje = ex.Message;
            }



            return IdViajeGenerado;
        }

        public bool Editar(Entidades.Viaje objEntidadesViaje, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = String.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.strConexion))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarViaje", objConexion);
                    cmd.Parameters.AddWithValue("@IdViaje", objEntidadesViaje.IdViaje);
                    cmd.Parameters.AddWithValue("@IdUsuario", objEntidadesViaje.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@Titulo", objEntidadesViaje.Titulo);
                    cmd.Parameters.AddWithValue("@Descripcion", objEntidadesViaje.Descripcion);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    objConexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                Respuesta = false;
                Mensaje = ex.Message;
            }

            return Respuesta;
        }

        public bool Eliminar(Entidades.Viaje objEntidadesViaje, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = String.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.strConexion))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarViaje", objConexion);
                    cmd.Parameters.AddWithValue("@IdViaje", objEntidadesViaje.IdViaje);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    objConexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                Respuesta = false;
                Mensaje = ex.Message;
            }

            return Respuesta;
        }

    }
}
