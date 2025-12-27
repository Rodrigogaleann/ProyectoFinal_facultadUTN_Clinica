using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;


namespace Datos
{
    public class AccesoDatos
    {
        private string BDDClinica = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=ClinicaMedica_TPINT;Integrated Security=True";

        public SqlConnection conexionBDD()
        {
            return new SqlConnection(BDDClinica);
        }

        // Método para INSERT, UPDATE, DELETE.
        public int EjecutarOperacion(string consultaSQL, params SqlParameter[] parametros)
        {
            using (SqlConnection sqlConnection = new SqlConnection(BDDClinica))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(consultaSQL, sqlConnection))
                {
                    sqlCommand.Parameters.AddRange(parametros);
                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    return filasAfectadas;
                }
            }
        }

        // Método para SELECT.
        public DataTable EjecutarSelect(string consultaSQL, params SqlParameter[] parametros)
        {
            using (SqlConnection sqlConnection = new SqlConnection(BDDClinica))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(consultaSQL, sqlConnection))
                {
                    sqlCommand.Parameters.AddRange(parametros);

                    using (SqlDataAdapter dataAdap = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable dt = new DataTable();
                        dataAdap.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // Método para SELECT que devuelve un solo valor (COUNT, MAX, MIN, etc).
        public object EjecutarEscalar(string consultaSQL, params SqlParameter[] parametros)
        {
            using (SqlConnection sqlConnection = new SqlConnection(BDDClinica))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(consultaSQL, sqlConnection))
                {
                    sqlCommand.Parameters.AddRange(parametros);
                    object resultado = sqlCommand.ExecuteScalar();
                    return resultado;
                }
            }
        }

        public int EjecutarProcedimiento(string nombreSP, params SqlParameter[] parametros)
        {
            using (SqlConnection sqlConnection = new SqlConnection(BDDClinica))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(nombreSP, sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddRange(parametros);

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    return filasAfectadas;
                }
            }
        }
    }
}
