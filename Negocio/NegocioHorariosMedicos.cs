using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class NegocioHorariosMedicos
    {
        DatosHorariosMedicos datosHorariosMedicos = new DatosHorariosMedicos();

        public List<HorarioMedico> ObtenerHorariosMedico(int idMedico)
        {
            return datosHorariosMedicos.ObtenerHorariosMedico(idMedico);
        }

        public HorarioMedico ObtenerHorarioMedicoPorDia(int idMedico, int diaSemana)
        {
            return datosHorariosMedicos.ObtenerHorarioMedicoPorDia(idMedico, diaSemana);
        }

        public void EliminarHorariosMedico(int idMedico)
        {
            datosHorariosMedicos.EliminarHorariosMedico(idMedico);
        }
    
        public void InsertarHorario(HorarioMedico horarioMedico)
        {
            datosHorariosMedicos.InsertarHorario(horarioMedico);
        }
    }
}
