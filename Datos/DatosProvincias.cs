using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DatosProvincias
    {
        AccesoDatos accesoDatos = new AccesoDatos();
        public DatosProvincias() { }

        public List<Provincias> obtenerProvincias()
        {
            List<Provincias> listProvincias = new List<Provincias>();

            string consultaSQL = "SELECT IdProvincia, provincia FROM Provincias";

            using (SqlConnection connection = accesoDatos.conexionBDD())
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    return null;
                }
                {
                    using (SqlCommand command = new SqlCommand(consultaSQL, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                Provincias provincia = new Provincias();

                                provincia.IdProvincia = reader["IdProvincia"].ToString();
                                provincia.Provincia = reader["provincia"].ToString();
                                listProvincias.Add(provincia);
                            }
                        }

                    }
                }
            }

            return listProvincias;
        }
    
        public DataTable ObtenerProvincias()
        {
            string query = "SELECT IdProvincia, provincia FROM Provincias";

            return accesoDatos.EjecutarSelect(query);
        }
    }
}
