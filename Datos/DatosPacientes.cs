using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class DatosPaciente
    {
        private AccesoDatos acceso = new AccesoDatos();

        public bool AgregarPaciente(Paciente p)
        {
            using (SqlConnection conexion = acceso.conexionBDD())
            {

                try
                {
                    conexion.Open();
                }
                catch (Exception ex)
                {
                    return false;
                }

                string consulta = @"INSERT INTO Pacientes
                                (nombre, apellido, sexo, dni, nacionalidad, fechaNacimiento, direccion, correoElectronico, telefono, idLocalidad, idProvincia)
                                 VALUES (@nombre, @apellido, @sexo, @dni, @nacionalidad, @fechaNacimiento, @direccion, @correo, @telefono, @idLocalidad, @idProvincia)";

                try
                {
                    SqlCommand cmd = new SqlCommand(consulta, conexion);
                    cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", p.Apellido);
                    cmd.Parameters.AddWithValue("@sexo", p.Sexo);
                    cmd.Parameters.AddWithValue("@dni", p.Dni);
                    cmd.Parameters.AddWithValue("@nacionalidad", p.Nacionalidad);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", p.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@direccion", p.Direccion);
                    cmd.Parameters.AddWithValue("@correo", p.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@telefono", p.Telefono);
                    cmd.Parameters.AddWithValue("@idLocalidad", p.IdLocalidad);
                    cmd.Parameters.AddWithValue("@idProvincia", p.IdProvincia);

                    cmd.ExecuteNonQuery();
                    return true;
                }//agrego info al catch
                catch (Exception ex)
                {
                    throw new Exception("Error al insertar paciente: " + ex.Message);
                    //return false;
                }
            }
        }

        public DataTable ObtenerPacientes()
        {
            string consulta = "SELECT " +
                "Pac.idPaciente, " +
                "Pac.nombre, " +
                "Pac.apellido, " +
                "(Pac.apellido + ', ' + Pac.nombre + ', DNI ' + Pac.dni) AS datosIdentificadores, " +
                "Pac.sexo, " +
                "Pac.dni, " +
                "Pac.nacionalidad, " +
                "Pac.fechaNacimiento, " +
                "Pac.direccion, " +
                "Pac.correoElectronico, " +
                "Pac.telefono, " +
                "Pac.idProvincia, " +
                "Pac.idLocalidad, " +
                "Pac.estado, " +

                "Prov.provincia AS Provincia, " +

                "Loc.localidad AS Localidad " +

                "FROM Pacientes Pac " +
                "INNER JOIN Provincias Prov ON Pac.idProvincia = Prov.IdProvincia " +
                "INNER JOIN Localidades Loc ON Pac.idLocalidad = Loc.IdLocalidades " +
                "WHERE Pac.estado = 1;";

            return acceso.EjecutarSelect(consulta);
        }

        public List<Paciente> ListarPacientes()
        {
            List<Paciente> listaPacientes = new List<Paciente>();
            using (SqlConnection conexion = acceso.conexionBDD())
            {
                try
                {
                    conexion.Open();
                }
                catch (Exception ex)
                {
                    return null;
                }

                string consulta = @"SELECT idPaciente, nombre, apellido, sexo, dni, nacionalidad, 
                               fechaNacimiento, direccion, correoElectronico, telefono, 
                               idLocalidad, idProvincia, estado 
                               FROM Pacientes 
                               WHERE estado = 1";

                try
                {
                    SqlCommand cmd = new SqlCommand(consulta, conexion);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Paciente p = new Paciente()
                        {
                            IdPaciente = Convert.ToInt32(reader["idPaciente"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Sexo = reader["sexo"].ToString(),
                            Dni = reader["dni"].ToString(),
                            Nacionalidad = reader["nacionalidad"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]),
                            Direccion = reader["direccion"].ToString(),
                            CorreoElectronico = reader["correoElectronico"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            IdLocalidad = reader["idLocalidad"].ToString(),
                            IdProvincia = reader["idProvincia"].ToString(),
                            Estado = Convert.ToBoolean(reader["estado"])
                        };

                        listaPacientes.Add(p);
                    }

                    reader.Close();
                    return listaPacientes;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar pacientes: " + ex.Message);
                }
            }
        }

        public bool BajaPaciente(int idPaciente)
        {
            using (SqlConnection conexion = acceso.conexionBDD())
            {
                try
                {
                    conexion.Open();
                }
                catch
                {
                    return false;
                }

                string consulta = "UPDATE Pacientes SET estado = 0 WHERE idPaciente = @id";

                try
                {
                    SqlCommand cmd = new SqlCommand(consulta, conexion);
                    cmd.Parameters.AddWithValue("@id", idPaciente);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al dar de baja el paciente: " + ex.Message);
                }
            }
        }

        public bool ModificarPaciente(Paciente p)
        {
            using (SqlConnection conexion = acceso.conexionBDD())
            {
                try
                {
                    conexion.Open();
                    string consulta = @"UPDATE Pacientes SET
                                nombre = @nombre,
                                apellido = @apellido,
                                sexo = @sexo,
                                dni = @dni,
                                nacionalidad = @nacionalidad,
                                fechaNacimiento = @fechaNacimiento,
                                direccion = @direccion,
                                correoElectronico = @correo,
                                telefono = @telefono,
                                idLocalidad = @idLocalidad,
                                idProvincia = @idProvincia
                                WHERE idPaciente = @idPaciente";

                    SqlCommand cmd = new SqlCommand(consulta, conexion);

                    cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", p.Apellido);
                    cmd.Parameters.AddWithValue("@sexo", p.Sexo);
                    cmd.Parameters.AddWithValue("@dni", p.Dni);
                    cmd.Parameters.AddWithValue("@nacionalidad", p.Nacionalidad);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", p.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@direccion", p.Direccion);
                    cmd.Parameters.AddWithValue("@correo", p.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@telefono", p.Telefono);
                    cmd.Parameters.AddWithValue("@idLocalidad", p.IdLocalidad);
                    cmd.Parameters.AddWithValue("@idProvincia", p.IdProvincia);
                    cmd.Parameters.AddWithValue("@idPaciente", p.IdPaciente);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al modificar paciente: " + ex.Message);
                }
            }
        }

        public DataTable BuscarPacientePorDni(string dni)
        {
            string query = "SELECT " +
                "Pac.idPaciente, " +
                "Pac.nombre, " +
                "Pac.apellido, " +
                "(Pac.apellido + ', ' + Pac.nombre + ', DNI ' + Pac.dni) AS datosIdentificadores, " +
                "Pac.sexo, " +
                "Pac.dni, " +
                "Pac.nacionalidad, " +
                "Pac.fechaNacimiento, " +
                "Pac.direccion, " +
                "Pac.correoElectronico, " +
                "Pac.telefono, " +
                "Pac.idProvincia, " +
                "Pac.idLocalidad, " +
                "Pac.estado, " +

                "Prov.provincia AS Provincia, " +
                "Loc.localidad AS Localidad " +

                "FROM Pacientes Pac " +
                "INNER JOIN Provincias Prov ON Pac.idProvincia = Prov.IdProvincia " +
                "INNER JOIN Localidades Loc ON Pac.idLocalidad = Loc.IdLocalidades " +
                "WHERE Pac.estado = 1 AND Pac.dni LIKE @dni";

            return acceso.EjecutarSelect(query, new SqlParameter("@dni", "%" + dni + "%"));
        }

        public Paciente BuscarPaciente(string dniPaciente)
        {
            string consutaSQL = "SELECT * FROM Pacientes " +
                                "WHERE dni = @dni";
            Paciente pacienteEncontrado = null;

            using (SqlConnection connection = acceso.conexionBDD())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(consutaSQL, connection))
                {
                    command.Parameters.AddWithValue("@dni", "%" + dniPaciente + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pacienteEncontrado = new Paciente
                            {
                                IdPaciente = Convert.ToInt32(reader["idPaciente"]),
                                IdLocalidad = reader["idLocalidad"].ToString(),
                                IdProvincia = reader["idProvincia"].ToString(),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Sexo = reader["sexo"].ToString(),
                                Dni = reader["dni"].ToString(),
                                Nacionalidad = reader["nacionalidad"].ToString(),
                                FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("fechaNacimiento")),
                                Direccion = reader["direccion"].ToString(),
                                CorreoElectronico = reader["correoElectronico"].ToString(),
                                Telefono = reader["telefono"].ToString(),
                                Estado = reader.GetBoolean(reader.GetOrdinal("estado")),
                            };
                        }

                    }
                }

                return pacienteEncontrado;
            }
        }
        
        /*
        public bool YaExisteLegajo(string Legajo, int idPaciente)
        {
            string query = "SELECT COUNT(+) " +
                "FROM Pacientes " +
                "WHERE Legajo= @Legajo " +
                "AND(@idPaciente = -1 OR idPaciente <> @idPaciente); ";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Legajo", Legajo),
                new SqlParameter("@idPaciente", idPaciente),
            };

            DataTable dt = acceso.EjecutarSelect(query, parametros);

            int cantidad = Convert.ToInt32(dt.Rows[0][0]);

            return cantidad > 0;
        }
        */

        public bool YaExisteDni(string dni, int idPaciente)
        {
            string query = "SELECT COUNT(*) " +
                "FROM Pacientes " +
                "WHERE dni = @dni " +
                "AND(@idPaciente = -1 OR idPaciente <> @idPaciente); ";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@dni", dni),
                new SqlParameter("@idPaciente", idPaciente),
            };

            DataTable dt = acceso.EjecutarSelect(query, parametros);

            int cantidad = Convert.ToInt32(dt.Rows[0][0]);

            return cantidad > 0;
        }

       public bool YaExisteCorreo(string correo, int idPaciente)
       {
            string query = "SELECT COUNT(*) " +
                "FROM Pacientes " +
                "WHERE correoElectronico = @correoElectronico " +
                "AND(@idPaciente = -1 OR idPaciente <> @idPaciente); ";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@correoElectronico", correo),
                new SqlParameter("@idPaciente", idPaciente),
            };

            DataTable dt = acceso.EjecutarSelect(query, parametros);

            int cantidad = Convert.ToInt32(dt.Rows[0][0]);

            return cantidad > 0;
       }

        public bool YaExisteTelefono(string telefono, int idPaciente)
        {
            string query = "SELECT COUNT(*) " +
                "FROM Pacientes " +
                "WHERE telefono = @telefono " +
                "AND(@idPaciente =-1 OR idPaciente <> @idPaciente); ";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@telefono", telefono),
                new SqlParameter("@idPaciente", idPaciente),
            };

            DataTable dt = acceso.EjecutarSelect(query, parametros);

            int cantidad = Convert.ToInt32(dt.Rows[0][0]);

            return cantidad > 0;
        }
    }
}
