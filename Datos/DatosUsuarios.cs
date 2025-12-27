using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Datos
{
    public class DatosUsuarios
    {
        private AccesoDatos acceso = new AccesoDatos();

        public Usuario validarAcceso(string nombreUsuario, string contrasenia)
        {
            string consultaSQL = "SELECT idUsuario,nombreUsuario,tipoUsuario,contrasenia FROM Usuarios" + " " +
                " WHERE NombreUsuario=@NombreUsuario AND Contrasenia=@Contrasenia";

            using (SqlConnection sqlConnection = acceso.conexionBDD())
            {
                try
                {
                    sqlConnection.Open();
                }
                catch (Exception ex)
                {
                    return null;
                }

                using (SqlCommand command = new SqlCommand(consultaSQL, sqlConnection))
                {
                    command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                    command.Parameters.AddWithValue("@Contrasenia", contrasenia);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            Usuario usuario = new Usuario()
                            {
                                IdUsuario = reader["idUsuario"].ToString(),
                                NombreUsuario = reader["nombreUsuario"].ToString(),
                                TipoUsuario = (bool)reader["tipoUsuario"],
                                Contrasenia = reader["contrasenia"].ToString().Trim(),
                            };

                            return usuario;
                        }
                        else
                        {
                            return null;
                        }

                    }
                }

            }
        }

        public bool AgregarUsuario(Usuario usuario)
        {
            string consultaSQL = @"INSERT into Usuarios(idUsuario, nombreUsuario, tipoUsuario, contrasenia) 
                                   VALUES (@idUsuario, @NombreUsuario, @TipoUsuario, @Contrasenia)";

            using (SqlConnection sqlConnection = acceso.conexionBDD())
            {
                try
                {
                    sqlConnection.Open();
                }
                catch (Exception ex)
                {
                    return false;
                }
                SqlCommand sqlCommand = new SqlCommand(consultaSQL, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@idUsuario", usuario.IdUsuario);
                sqlCommand.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                sqlCommand.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                sqlCommand.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);

                int filas = sqlCommand.ExecuteNonQuery();

                return filas > 0;
            }
        }
    
        public DataTable ObtenerUsuarios()
        {
            string query = "SELECT " +
                "U.idUsuario AS id, " +
                "U.nombreUsuario AS usuario, " +
                "U.tipoUsuario, " +
                "M.legajo, " +
                "M.apellido " +

                "FROM Usuarios U " +
                "LEFT JOIN Medicos M ON U.idUsuario = M.idUsuario " +
                "WHERE U.estado = 1";

            return acceso.EjecutarSelect(query);
        }

        public Usuario ObtenerUsuarioPorId(string id)
        {
            string query = @"SELECT 
                idUsuario, 
                nombreUsuario, 
                tipoUsuario
                FROM Usuarios 
                WHERE idUsuario = @id";

            DataTable dt = acceso.EjecutarSelect(query, new SqlParameter("@id", id));

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dt.Rows[0];
            Usuario usuario = new Usuario();

            usuario.IdUsuario = row["idUsuario"].ToString();
            usuario.NombreUsuario = row["nombreUsuario"].ToString();
            usuario.TipoUsuario = Convert.ToBoolean(row["tipoUsuario"]);

            return usuario;
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            string query = "UPDATE Usuarios " +
                "SET nombreUsuario = @nombreUsuario " +
                "WHERE idUsuario = @idUsuario";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idUsuario", usuario.IdUsuario),
                new SqlParameter("@nombreUsuario", usuario.NombreUsuario),
            };

            acceso.EjecutarOperacion(query, parametros);
        }
    
        public void ActualizarContrasenia(string idUsuario, string nuevaContrasenia)
        {
            string query = @"UPDATE Usuarios 
                SET contrasenia = @contrasenia 
                WHERE idUsuario = @id";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@contrasenia", nuevaContrasenia),
                new SqlParameter("@id", idUsuario)
            };

            acceso.EjecutarOperacion(query, parametros);
        }

        public bool Existe(string usuario, string idAExcluir = null)
        {
            string query = "SELECT COUNT(*) FROM Usuarios WHERE nombreUsuario = @usuario";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@usuario", usuario)
            };

            if (!string.IsNullOrEmpty(idAExcluir))
            {
                query += " AND idUsuario <> @idAExcluir";
                parametros.Add(new SqlParameter("@idAExcluir", idAExcluir));
            }

            DataTable dt = acceso.EjecutarSelect(query, parametros.ToArray());

            int cantidad = Convert.ToInt32(dt.Rows[0][0]);

            return cantidad > 0;
        }

        public int BajaLogicaUsuario(string idUsuario)
        {
            string query = "UPDATE Usuarios SET estado = 0 WHERE idUsuario = @idUsuario";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idUsuario", idUsuario)
            };

            return acceso.EjecutarOperacion(query, parametros);
        }

        public bool EsAdministrador(string nombreUsuario)
        {
            string query = "SELECT tipoUsuario FROM Usuarios WHERE nombreUsuario = @nombreUsuario";

            DataTable dt = acceso.EjecutarSelect(query, new SqlParameter("@nombreUsuario", nombreUsuario));

            int cantidad = Convert.ToInt32(dt.Rows[0][0]);

            return cantidad > 0;
        }
    }
}
