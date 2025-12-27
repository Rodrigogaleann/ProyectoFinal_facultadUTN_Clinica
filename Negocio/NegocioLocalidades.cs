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
    public class NegocioLocalidades
    {
        private DatosLocalidades datosLocalidades = new DatosLocalidades();

        public NegocioLocalidades() { }

        public List<Localidades> ObtenerLocalidadesPorProvincia(string IdProvincia)
        {
            try
            {
                return datosLocalidades.ObtenerLocalidadesPorProvincia(IdProvincia);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable ObtenerLocalidades(string idProvincia)
        {
            return datosLocalidades.ObtenerLocalidades(idProvincia);
        }
    }
}
