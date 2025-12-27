using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioProvincias
    {
        private DatosProvincias datosProvincias = new DatosProvincias();

        public NegocioProvincias() { }

        public List<Provincias> ObtenerListadoProvincias()
        {
            try
            {
                return datosProvincias.obtenerProvincias();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataTable ObtenerProvincias()
        {
            return datosProvincias.ObtenerProvincias();
        }
    }
}
