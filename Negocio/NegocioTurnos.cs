using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class NegocioTurnos
    {
        private DatosTurnos datosTurnos = new DatosTurnos();

        public int AltaTurno(Turno turno)
        {
            if (turno == null)
            {
                throw new ArgumentNullException(nameof(turno));
            }

            if (turno.idMedico <= 0 || Convert.ToInt32(turno.idEspecialidad) <= 0 || turno.idPaciente <= 0)
            {
                throw new Exception("Es obligatiorio completar los campos. ");
            }

            return datosTurnos.AgregarTurno(turno);
        }

        public int BajaLogicaTurno(int numTurno)
        {
            return datosTurnos.BajaLogicaTurno(numTurno);
        }

        public DataTable ListarTodosLosTurnos()
        {
            return datosTurnos.ObtenerTurnos();
        }

        public DataTable ObtenerTurnosAsignadosPorDNI(string idMedico, string dniPaciente)
        {
            return datosTurnos.ObtenerTurnosAsignadosPorDNI(idMedico, dniPaciente);
        }

        public DataTable ObtenerTurnosPorDNI(string dniPaciente)
        {
            DataTable data = datosTurnos.buscarTurno(dniPaciente);
            return data;
            /*List<Turno> listaTurnos = new List<Turno>();

            if (data == null || data.Rows.Count == 0)
            {
                return new List<Turno>();
            }


            foreach (DataRow fila in data.Rows)
            {
                Turno t = new Turno
                {
                    numTurno = Convert.ToInt32(fila["numTurno"]),
                    idEspecialidad = fila["idEspecilidad"].ToString(),
                    dia = Convert.ToDateTime(fila["dia"]),
                    horario = (TimeSpan)fila["horario"],
                    asistencia = Convert.ToBoolean(fila["asistencia"]),
                    idPaciente = Convert.ToInt32(fila["idPaciente"]),
                    idMedico = Convert.ToInt32(fila["idMedico"]),
                    observaciones = fila["observaciones"].ToString(),
                };

                listaTurnos.Add(t);
            }

            return listaTurnos;*/
        }

        public List<Turno> ObtenerTurnosOcupados(int idMedico, DateTime fechaSeleccionada)
        {
            return datosTurnos.ObtenerTurnosOcupados(idMedico, fechaSeleccionada);
        }
    
        public List<Turno> ObtenerTurnosAsignados(string idMedico)
        {
            DataTable dt = datosTurnos.ObtenerTurnosAsignados(idMedico);

            List<Turno> lista = new List<Turno>();

            foreach (DataRow row in dt.Rows)
            {
                Turno turno = new Turno
                {
                    numTurno = Convert.ToInt32(row["numTurno"]),
                    dia = Convert.ToDateTime(row["dia"]),
                    horario = TimeSpan.Parse(row["horario"].ToString()),
                    asistencia = Convert.ToBoolean(row["asistencia"]),
                    observaciones = row["observaciones"].ToString()
                };

                lista.Add(turno);
            }

            return lista;
        }
    
        public bool VerificarAsistencia(int numTurno)
        {
            DataTable dt = datosTurnos.VerificarAsistencia(numTurno);

            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToBoolean(dt.Rows[0]["asistencia"]);
            }

            return false;
        }

        public string ObtenerObservaciones(int numTurno)
        {
            DataTable dt = datosTurnos.ObtenerObservaciones(numTurno);

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["observaciones"].ToString();
            }

            return "";
        }

        public int ActualizarAsistencia(int numTurno, bool asistencia)
        {
            return datosTurnos.ActualizarAsistencia(numTurno, asistencia);
        }

        public int ActualizarObservaciones(int numTurno, string observaciones)
        {
            return datosTurnos.ActualizarObservaciones(numTurno, observaciones);
        }
    }
}
