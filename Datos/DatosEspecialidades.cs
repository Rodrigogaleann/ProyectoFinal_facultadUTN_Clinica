using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DatosEspecialidades
    {
        AccesoDatos accesoDatos = new AccesoDatos();

        public DatosEspecialidades() { }

        public DataTable ObtenerEspecialidades()
        {
            string query = "SELECT IdEspecialidad, nombreEspecialidad FROM Especialidades";

            return accesoDatos.EjecutarSelect(query);
        }
    }
}
