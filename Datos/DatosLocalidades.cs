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
    public class DatosLocalidades
    {
        AccesoDatos accesoDatos = new AccesoDatos();

        public DatosLocalidades() { }

        public List<Localidades> ObtenerLocalidadesPorProvincia(string IdProvincia)
        {
            List<Localidades> listaLocalidades = new List<Localidades>();

            string consulta = "SELECT IdLocalidades, localidad, idProvincia FROM Localidades WHERE idProvincia = @IdProvincia";

            using (SqlConnection sqlConnection = accesoDatos.conexionBDD())
            {
                try
                {
                    sqlConnection.Open();
                }
                catch (Exception ex)
                {
                    return null;
                }

                using (SqlCommand command = new SqlCommand(consulta, sqlConnection))
                {
                    command.Parameters.AddWithValue("@IdProvincia", IdProvincia);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            Localidades localidades = new Localidades();

                            localidades.idLocalidades = reader["IdLocalidades"].ToString();
                            localidades.Localidad = reader["localidad"].ToString();
                            localidades.idProvincia = reader["idProvincia"].ToString();
                            listaLocalidades.Add(localidades);
                        }
                    }
                }
            }

            return listaLocalidades;
        }
    
        public DataTable ObtenerLocalidades(string idProvincia)
        {
            string query = "SELECT IdLocalidades, localidad FROM Localidades WHERE IdProvincia = @IdProvincia";

            return accesoDatos.EjecutarSelect(query, new SqlParameter ("@IdProvincia", idProvincia));
        }
    }
}
