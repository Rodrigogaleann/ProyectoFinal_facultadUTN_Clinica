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
    public class DatosTurnos
    {
        private AccesoDatos acceso = new AccesoDatos();

        public DataTable ObtenerTurnos()
        {
            string query = "SELECT T.numTurno, " +
                        "E.nombreEspecialidad AS Especialidad, " +
                        "T.dia, T.horario, T.asistencia, " +
                        "P.nombre AS [Nombre Paciente], " +
                        "M.nombre + ' ' + M.apellido AS [Nombre Medico], " +
                        "T.observaciones " +

                        "FROM Turnos T " +

                        "INNER JOIN Especialidades E ON T.idEspecilidad = E.IdEspecialidad " +
                        "INNER JOIN Pacientes P ON T.idPaciente = P.idPaciente " +
                        "INNER JOIN Medicos M ON T.idMedico = M.idMedico " +
                        "WHERE T.estado = 1";

            return acceso.EjecutarSelect(query);
        }

        public int AgregarTurno(Turno turno)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                //new SqlParameter("numTurno", turno.numTurno),
                new SqlParameter("idMedico", turno.idMedico),
                new SqlParameter("idEspecialidad", turno.idEspecialidad),
                new SqlParameter("dia", turno.dia),
                new SqlParameter("horario", turno.horario),
                new SqlParameter("idPaciente", turno.idPaciente),
                //new SqlParameter("asistencia", turno.asistencia),
                new SqlParameter("observaciones", turno.observaciones),
            };

            return acceso.EjecutarProcedimiento("AltaTurno", parametros);
        }

        public int BajaLogicaTurno(int numTurno)
        {
            string query = "UPDATE Turnos SET estado = 0 WHERE numTurno = @numTurno";

            return acceso.EjecutarOperacion(query, new SqlParameter("@numTurno", numTurno));
        }

        public List<Turno> ObtenerTurnosOcupados(int idMedico, DateTime fechaSeleccionada)
        {
            string query = "SELECT " +
                "dia, " +
                "horario " +
                "FROM Turnos " +
                "WHERE idMedico = @idMedico " +
                "AND dia = @dia " +
                "AND estado = 1";

            SqlParameter[] parametros =
            {
                new SqlParameter("@idMedico", idMedico),
                new SqlParameter("@dia", fechaSeleccionada)
            };


            DataTable dt = acceso.EjecutarSelect(query, parametros);

            List<Turno> lista = new List<Turno>();

            foreach (DataRow row in dt.Rows)
            {
                Turno turno = new Turno
                {
                    dia = Convert.ToDateTime(row["dia"]),
                    horario = TimeSpan.Parse(row["horario"].ToString())
                };

                lista.Add(turno);
            }

            return lista;
        }

        public DataTable buscarTurno(string dni)
        {
            string query = "SELECT T.numTurno, " +
                        "E.nombreEspecialidad AS Especialidad, " +
                        "T.dia, T.horario, T.asistencia, " +
                        "P.nombre AS [Nombre Paciente], " +
                        "M.nombre + ' ' + M.apellido AS [Nombre Medico], " +
                        "T.observaciones " +

                        "FROM Turnos T " +

                        "INNER JOIN Especialidades E ON T.idEspecilidad = E.IdEspecialidad " +
                        "INNER JOIN Pacientes P ON T.idPaciente = P.idPaciente " +
                        "INNER JOIN Medicos M ON T.idMedico = M.idMedico " +
                        "WHERE T.estado = 1 " +
                        "And P.dni LIKE @dniPaciente";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@dniPaciente", "%" + dni + "%")
            };

            return acceso.EjecutarSelect(query, parametros);
        }

        public DataTable ObtenerTurnosAsignados(string idMedico)
        {
            string query = "SELECT T.numTurno, " +
                        "E.nombreEspecialidad AS Especialidad, " +
                        "T.dia, T.horario, T.asistencia, " +
                        "P.nombre AS [Nombre Paciente], " +
                        "M.nombre + ' ' + M.apellido AS [Nombre Medico], " +
                        "T.observaciones " +

                        "FROM Turnos T " +

                        "INNER JOIN Especialidades E ON T.idEspecilidad = E.IdEspecialidad " +
                        "INNER JOIN Pacientes P ON T.idPaciente = P.idPaciente " +
                        "INNER JOIN Medicos M ON T.idMedico = M.idMedico " +
                        "WHERE T.estado = 1 " +
                        "AND T.idMedico = @idMedico";

            return acceso.EjecutarSelect(query, new SqlParameter("@idMedico", idMedico));
        }

        public DataTable VerificarAsistencia(int numTurno)
        {
            string query = "SELECT asistencia FROM Turnos WHERE numTurno = @numTurno";

            return acceso.EjecutarSelect(query, new SqlParameter("@numTurno", numTurno));
        }

        public DataTable ObtenerObservaciones(int numTurno)
        {
            string query = "SELECT observaciones FROM Turnos WHERE numTurno = @numTurno";

            return acceso.EjecutarSelect(query, new SqlParameter("@numTurno", numTurno));
        }

        public int ActualizarAsistencia(int numTurno, bool asistencia)
        {
            string query = "UPDATE Turnos SET asistencia = @asistencia WHERE numTurno = @numTurno";

            SqlParameter[] parametros = new SqlParameter[]
            {
              new SqlParameter("@numTurno", numTurno),
              new SqlParameter("@asistencia", asistencia)
            };

            return acceso.EjecutarOperacion(query, parametros);
        }

        public int ActualizarObservaciones(int numTurno, string observaciones)
        {
            string query = "UPDATE Turnos SET observaciones = @observaciones WHERE numTurno = @numTurno";

            SqlParameter[] parametros = new SqlParameter[]
            {
              new SqlParameter("@numTurno", numTurno),
              new SqlParameter("@observaciones", observaciones)
            };

            return acceso.EjecutarOperacion(query, parametros);
        }

        public DataTable ObtenerTurnosAsignadosPorDNI(string idMedico, string dniPaciente)
        {
            string query = "SELECT T.numTurno, " +
                        "E.nombreEspecialidad AS Especialidad, " +
                        "T.dia, T.horario, T.asistencia, " +
                        "P.nombre AS [Nombre Paciente], " +
                        "M.nombre + ' ' + M.apellido AS [Nombre Medico], " +
                        "T.observaciones " +

                        "FROM Turnos T " +

                        "INNER JOIN Especialidades E ON T.idEspecilidad = E.IdEspecialidad " +
                        "INNER JOIN Pacientes P ON T.idPaciente = P.idPaciente " +
                        "INNER JOIN Medicos M ON T.idMedico = M.idMedico " +
                        "WHERE T.estado = 1 " +
                        "AND T.idMedico = @idMedico " +
                        "AND P.dni LIKE @dniPaciente ";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idMedico", idMedico),
                new SqlParameter("@dniPaciente", "%" + dniPaciente + "%")
            };

            return acceso.EjecutarSelect(query, parametros);
        }
    }
}

