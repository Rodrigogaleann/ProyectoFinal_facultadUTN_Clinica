using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class DatosHorariosMedicos
    {
        private AccesoDatos accesoDatos = new AccesoDatos();

        public List<HorarioMedico> ObtenerHorariosMedico(int idMedico)
        {
            string query = "SELECT " +
                "idDia, " +
                "horaInicio, " +
                "horaFin " +
                "FROM HorariosMedicos " +
                "WHERE idMedico = @idMedico " +
                "AND estado = 1;";

            DataTable dt = accesoDatos.EjecutarSelect(query, new SqlParameter("@idMedico", idMedico));

            List<HorarioMedico> lista = new List<HorarioMedico>();

            foreach (DataRow row in dt.Rows)
            {
                HorarioMedico horario = new HorarioMedico
                {
                    IdDia = Convert.ToInt32(row["idDia"]),
                    HoraInicio = row["horaInicio"].ToString(),
                    HoraFin = row["horaFin"].ToString()
                };

                lista.Add(horario);
            }

            return lista;
        }

        public HorarioMedico ObtenerHorarioMedicoPorDia(int idMedico, int diaSemana)
        {
            string query = "SELECT " +
                "idDia, " +
                "horaInicio, " +
                "horaFin " +
                "FROM HorariosMedicos " +
                "WHERE idMedico = @idMedico " +
                "AND idDia = @idDia " +
                "AND estado = 1;";

            SqlParameter[] parametros =
            {
                new SqlParameter("@idMedico", idMedico),
                new SqlParameter("@idDia", diaSemana)
            };

            DataTable dt = accesoDatos.EjecutarSelect(query, parametros);

            HorarioMedico horarioMedico = new HorarioMedico();

            foreach (DataRow row in dt.Rows)
            {
                horarioMedico.IdDia = Convert.ToInt32(row["idDia"]);
                horarioMedico.HoraInicio = row["horaInicio"].ToString();
                horarioMedico.HoraFin = row["horaFin"].ToString();
            }

            return horarioMedico;
        }

        public void EliminarHorariosMedico(int idMedico)
        {
            string query = "DELETE FROM HorariosMedicos WHERE idMedico = @idMedico";

            accesoDatos.EjecutarOperacion(query, new SqlParameter("@idMedico", idMedico));
        }

        public void InsertarHorario(HorarioMedico horarioMedico)
        {
            string query = "INSERT INTO HorariosMedicos (idMedico, idDia, horaInicio, horaFin, estado) " +
                "VALUES (@idMedico, @idDia, @inicio, @fin, 1)";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idMedico", horarioMedico.IdMedico),
                new SqlParameter("@idDia", horarioMedico.IdDia),
                new SqlParameter("@inicio", horarioMedico.HoraInicio),
                new SqlParameter("@fin", horarioMedico.HoraFin)
            };

            accesoDatos.EjecutarOperacion(query, parametros);
        }
    }
}
