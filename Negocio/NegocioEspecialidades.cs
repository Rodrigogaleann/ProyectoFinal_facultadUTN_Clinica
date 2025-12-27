using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class NegocioEspecialidades
    {
        DatosEspecialidades datosEspecialidades = new DatosEspecialidades();

        public DataTable ObtenerEspecialidades()
        {
            return datosEspecialidades.ObtenerEspecialidades();
        }
    }
}
