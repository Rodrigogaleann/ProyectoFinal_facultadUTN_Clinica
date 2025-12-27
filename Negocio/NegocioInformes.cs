using System;
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
    public class NegocioInformes
    {
        private DatosInformes datosInformes = new DatosInformes();

        public int AsistenciaPacietes(DateTime fechaInicio, DateTime fechaFin)
        {
            return datosInformes.InformeAsisteciaPacientes(fechaInicio, fechaFin);
        }
        public int InformeNoAsistenciaPacientes(DateTime fechaInicio, DateTime fechaFin)
        {
            return datosInformes.InformeNoAsistenciaPacientes(fechaInicio, fechaFin);
        }
        public int MedicosConActividad(DateTime fechaInicio, DateTime fechaFin)
        {
            return datosInformes.MedicosConActividad(fechaInicio, fechaFin);
        }
        public int MedicosSinActividad(DateTime fechaInicio, DateTime fechaFin)
        {
            return datosInformes.MedicosSinActividad(fechaInicio, fechaFin);
        }


    }
}
