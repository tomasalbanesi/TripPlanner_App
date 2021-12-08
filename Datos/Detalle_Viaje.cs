using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Detalle_Viaje
    {
        public List<Entidades.Detalle_Viaje> Listar(int idViaje)
        {
            List<Entidades.Detalle_Viaje> lista = new List<Entidades.Detalle_Viaje>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.strConexion))
            {

                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT IdDetalleViaje,DiaNro,Descripcion,FechaRegistro FROM Detalle_Viajes");
                    query.AppendLine("WHERE IdViaje = @IdViaje");
                    query.AppendLine("ORDER BY DiaNro ASC");


                    SqlCommand cmd = new SqlCommand(query.ToString(), objConexion);
                    cmd.Parameters.AddWithValue("@IdViaje",idViaje);
                    cmd.CommandType = CommandType.Text;

                    objConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            lista.Add(new Entidades.Detalle_Viaje()
                            {
                                IdDetalleViaje = Convert.ToInt32(dr["IdDetalleViaje"]),
                                NroDia = Convert.ToInt32(dr["DiaNro"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                            });

                        }

                    }


                }
                catch (Exception ex)
                {

                    lista = new List<Entidades.Detalle_Viaje>();
                }
            }

            return lista;

        }

        public int Registrar(Entidades.Detalle_Viaje objEntidadesDetalleViaje, out string Mensaje)
        {
            int IdViajeGenerado = 0;
            Mensaje = String.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.strConexion))
                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarDetalleViaje", objConexion);
                    cmd.Parameters.AddWithValue("@IdViaje", objEntidadesDetalleViaje.IdViaje);
                    cmd.Parameters.AddWithValue("@DiaNro", objEntidadesDetalleViaje.NroDia);
                    cmd.Parameters.AddWithValue("@Descripcion", objEntidadesDetalleViaje.Descripcion);
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

        public bool Editar(Entidades.Detalle_Viaje objEntidadesDetalleViaje, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = String.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.strConexion))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarDetalleViaje", objConexion);
                    cmd.Parameters.AddWithValue("@IdDetalleViaje", objEntidadesDetalleViaje.IdDetalleViaje);
                    cmd.Parameters.AddWithValue("@IdViaje", objEntidadesDetalleViaje.IdViaje);
                    cmd.Parameters.AddWithValue("@DiaNro", objEntidadesDetalleViaje.NroDia);
                    cmd.Parameters.AddWithValue("@Descripcion", objEntidadesDetalleViaje.Descripcion);
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

        public bool Eliminar(Entidades.Detalle_Viaje objEntidadesDetalleViaje, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = String.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.strConexion))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarDetalleViaje", objConexion);
                    cmd.Parameters.AddWithValue("@IdDetalleViaje", objEntidadesDetalleViaje.IdDetalleViaje);
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
