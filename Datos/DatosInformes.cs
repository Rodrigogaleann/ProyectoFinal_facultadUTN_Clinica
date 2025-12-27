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
    public class DatosInformes
    {
        private AccesoDatos acceso = new AccesoDatos();

        public int InformeAsisteciaPacientes(DateTime fechaInicio, DateTime fechaFin)
        {
            string query = @"SELECT COUNT(DISTINCT idPaciente) AS PacientesAsistieron
                FROM Turnos 
                WHERE asistencia = 1 
                AND dia BETWEEN @FechaInicio AND @FechaFin; ";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@FechaInicio", fechaInicio ),
                new SqlParameter("@FechaFin", fechaFin),

            };

            object asisteciaPaciente = acceso.EjecutarEscalar(query, parametros);

            //valida en caso de no encontrar los datos que pide la consulta devuelve 0
            if (asisteciaPaciente == null)
            {
                return 0;
            }
            else
            {
                //convierte el "object" en un int
                return Convert.ToInt32(asisteciaPaciente);
            }

        }
        public int InformeNoAsistenciaPacientes(DateTime fechaInicio, DateTime fechaFin)
        {
            string query = @"SELECT COUNT(DISTINCT idPaciente) AS PacientesNoAsistieron
                FROM Turnos
                WHERE asistencia = 0
                AND dia BETWEEN @FechaInicio AND @FechaFin; ";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@FechaInicio", fechaInicio ),
                new SqlParameter("@FechaFin", fechaFin),

            };

            object faltaPaciente = acceso.EjecutarEscalar(query, parametros);
            if (faltaPaciente == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(faltaPaciente);
            }
        }
       
        public int MedicosConActividad(DateTime fechaInicio, DateTime fechaFin)
        {
            string query = @"
               SELECT COUNT(DISTINCT T.idMedico) AS MedicosActivos
               FROM Turnos T
               INNER JOIN Medicos M ON T.idMedico = M.idMedico
               WHERE M.estado = 1
               AND T.dia BETWEEN @FechaInicio AND @FechaFin; ";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@FechaInicio", fechaInicio),
                new SqlParameter("@FechaFin", fechaFin),
            };

            Object MedicosActivos = acceso.EjecutarEscalar(query, parametros);

            //en caso de no encontrar los datos devuelve 0
            if (MedicosActivos == null||MedicosActivos==DBNull.Value)
            {
                return 0;
            }
            else
            {
                //de un object pasa a un int
                return Convert.ToInt32(MedicosActivos);
            }
        }

        public int MedicosSinActividad(DateTime fechaInicio, DateTime fechaFin)
        {
            string query = "SELECT COUNT(*) AS MedicosSinActividad " +
                "FROM Medicos M " +
                "LEFT JOIN Turnos T " +
                "ON M.idMedico = T.idMedico " +
                "AND T.dia BETWEEN @FechaInicio AND @FechaFin " +
                "WHERE M.estado = 1 " +
                "AND T.idMedico IS NULL; ";

            SqlParameter[] parametro = new SqlParameter[]
            {
                new SqlParameter("@FechaInicio", fechaInicio),
                new SqlParameter("@FechaFin", fechaFin),
            };

            Object MedicosNoActivos = acceso.EjecutarEscalar(query, parametro);

            if (MedicosNoActivos == null || MedicosNoActivos == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(MedicosNoActivos);
            }
        }

    }
}
