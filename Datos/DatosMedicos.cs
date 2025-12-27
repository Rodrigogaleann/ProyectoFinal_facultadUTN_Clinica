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
    public class DatosMedico
    {
        private AccesoDatos acceso = new AccesoDatos();

        public DataTable ObtenerMedicos()
        {
            string query = "SELECT " +
                "M.idMedico, " +
                "M.legajo, " +
                "M.dni, " +
                "M.nombre, " +
                "M.apellido, " +
                "(M.apellido + ', ' + M.nombre) AS nombreCompleto, " +
                "M.sexo, " +
                "M.idProvincia, " +
                "M.idLocalidad, " +
                "M.idEspecialidad, " +
                "M.nacionalidad, " +
                "M.fechaNacimiento, " +
                "M.direccion, " +
                "M.correoElectronico, " +
                "M.telefono, " +

                "P.provincia AS Provincia, " +

                "L.localidad AS Localidad, " +

                "E.nombreEspecialidad AS Especialidad, " +

                "U.idUsuario, " +
                "U.nombreUsuario AS Usuario " +
                "FROM Medicos M " +
                "INNER JOIN Provincias P ON M.idProvincia = P.IdProvincia " +
                "INNER JOIN Localidades L ON M.idLocalidad = L.IdLocalidades " +
                "INNER JOIN Especialidades E ON M.idEspecialidad = E.IdEspecialidad " +
                "INNER JOIN Usuarios U ON M.idUsuario = U.idUsuario " +
                "WHERE M.estado = 1;";

            return acceso.EjecutarSelect(query);
        }

        public int AgregarMedicoYUsuario(Usuario usuario, Medicos medico)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombreUsuario", usuario.NombreUsuario),
                new SqlParameter("@contrasenia", usuario.Contrasenia),

                new SqlParameter("@idLocalidad", medico.IdLocalidad),
                new SqlParameter("@idProvincia", medico.IdProvincia),
                new SqlParameter("@dni", medico.Dni),
                new SqlParameter("@legajo", medico.Legajo),
                new SqlParameter("@nombre", medico.Nombre),
                new SqlParameter("@apellido", medico.Apellido),
                new SqlParameter("@sexo", medico.Sexo),
                new SqlParameter("@nacionalidad", (object)medico.Nacionalidad ?? DBNull.Value),
                new SqlParameter("@fechaNacimiento", medico.FechaNacimiento),
                new SqlParameter("@correoElectronico", medico.CorreoElectronico),
                new SqlParameter("@telefono", medico.Telefono),
                new SqlParameter("@direccion", medico.Direccion),
                new SqlParameter("@idEspecialidad", medico.IdEspecialidad)
            };

            return acceso.EjecutarProcedimiento("AltaMedicoYUsuario", parametros);
        }

        public void ActualizarMedico(Medicos medico)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idMedico", medico.IdMedico),
                new SqlParameter("@idLocalidad", medico.IdLocalidad),
                new SqlParameter("@idProvincia", medico.IdProvincia),
                new SqlParameter("@dni", medico.Dni),
                new SqlParameter("@legajo", medico.Legajo),
                new SqlParameter("@nombre", medico.Nombre),
                new SqlParameter("@apellido", medico.Apellido),
                new SqlParameter("@sexo", medico.Sexo),
                new SqlParameter("@nacionalidad", (object)medico.Nacionalidad ?? DBNull.Value),
                new SqlParameter("@fechaNacimiento", medico.FechaNacimiento),
                new SqlParameter("@correoElectronico", medico.CorreoElectronico),
                new SqlParameter("@telefono", medico.Telefono),
                new SqlParameter("@direccion", medico.Direccion),
                new SqlParameter("@idEspecialidad", medico.IdEspecialidad)
            };

            acceso.EjecutarProcedimiento("ModificacionMedico", parametros);
        }

        public int BajaLogicaMedico(int idMedico)
        {
            string query = "UPDATE Medicos SET estado = 0 WHERE idMedico = @idMedico";

            return acceso.EjecutarOperacion(query, new SqlParameter("@idMedico", idMedico));
        }

        public bool ExistePorLegajo(string legajo, int idMedico)
        {
            string query = "SELECT COUNT(*) " +
                "FROM Medicos " +
                "WHERE legajo = @legajo " +
                "AND(@idMedico = -1 OR idMedico <> @idMedico);";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@legajo", legajo),
                new SqlParameter("@idMedico", idMedico),
            };

            DataTable dt = acceso.EjecutarSelect(query, parametros);

            int cantidad = Convert.ToInt32(dt.Rows[0][0]);

            return cantidad > 0;
        }

        public bool ExistePorDNI(string dni, int idMedico)
        {
            string query = "SELECT COUNT(*) " +
                "FROM Medicos " +
                "WHERE dni = @dni " +
                "AND(@idMedico = -1 OR idMedico <> @idMedico);";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@dni", dni),
                new SqlParameter("@idMedico", idMedico),
            };

            DataTable dt = acceso.EjecutarSelect(query, parametros);

            int cantidad = Convert.ToInt32(dt.Rows[0][0]);

            return cantidad > 0;
        }

        public bool CorreoEnUso(string correo, int idMedico)
        {
            string query = "SELECT COUNT(*) " +
                "FROM Medicos " +
                "WHERE correoElectronico = @correoElectronico " +
                "AND(@idMedico = -1 OR idMedico <> @idMedico);";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@correoElectronico", correo),
                new SqlParameter("@idMedico", idMedico),
            };

            DataTable dt = acceso.EjecutarSelect(query, parametros);

            int cantidad = Convert.ToInt32(dt.Rows[0][0]);

            return cantidad > 0;
        }

        public bool TelefonoEnUso(string telefono, int idMedico)
        {
            string query = "SELECT COUNT(*) " +
                "FROM Medicos " +
                "WHERE telefono = @telefono " +
                "AND(@idMedico = -1 OR idMedico <> @idMedico);";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@telefono", telefono),
                new SqlParameter("@idMedico", idMedico),
            };

            DataTable dt = acceso.EjecutarSelect(query, parametros);

            int cantidad = Convert.ToInt32(dt.Rows[0][0]);

            return cantidad > 0;
        }

        public DataTable ObtenerMedicosPorIdEspecialidad(int idEspecialidad)
        {
            string query = "SELECT idMedico, " +
                "(apellido + ', ' + nombre) AS nombreCompleto " +
                "FROM  Medicos " +
                "WHERE idEspecialidad = @idEspecialidad " +
                "AND estado = 1";

            return acceso.EjecutarSelect(query, new SqlParameter("@idEspecialidad", idEspecialidad));
        }

        public DataTable BuscarMedicoPorDNI(string dni)
        {
            string consultaSQL = "SELECT " +
                "M.idMedico, " +
                "M.legajo, " +
                "M.dni, " +
                "M.nombre, " +
                "M.apellido, " +
                "M.sexo, " +
                "M.idProvincia, " +
                "M.idLocalidad, " +
                "M.idEspecialidad, " +
                "M.nacionalidad, " +
                "M.fechaNacimiento, " +
                "M.direccion, " +
                "M.correoElectronico, " +
                "M.telefono, " +

                "P.provincia AS Provincia, " +

                "L.localidad AS Localidad, " +

                "E.nombreEspecialidad AS Especialidad, " +

                "U.idUsuario, " +
                "U.nombreUsuario AS Usuario " +
                "FROM Medicos M " +
                "INNER JOIN Provincias P ON M.idProvincia = P.IdProvincia " +
                "INNER JOIN Localidades L ON M.idLocalidad = L.IdLocalidades " +
                "INNER JOIN Especialidades E ON M.idEspecialidad = E.IdEspecialidad " +
                "INNER JOIN Usuarios U ON M.idUsuario = U.idUsuario " +
                "WHERE M.dni LIKE @dni " +
                "AND M.estado = 1";

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@dni","%" + dni + "%")
            };

            return acceso.EjecutarSelect(consultaSQL, sqlParameter);
        }
    
        public DataTable ObtenerMedicoPorIdUsuario(string idUsuario)
        {
            string query = "SELECT * FROM Medicos WHERE idUsuario = @idUsuario";

            return acceso.EjecutarSelect(query, new SqlParameter("@idUsuario", idUsuario));
        }
    }
}
