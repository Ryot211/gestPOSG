using System;
using System.Data;
using System.Data.SqlClient;

namespace TesisPosgrados.DataAccess
{
    public class NotasClass
    {
        private readonly string _connectionString;

        public NotasClass()
        {
            // Ajusta esta cadena de conexión según tu entorno
            _connectionString = "data source=.; database=TESIS; integrated security=SSPI";
        }

        // Método para obtener los periodos académicos desde la tabla SIG_PERIODOS
        public DataTable ObtenerPeriodosAcademicos()
        {
            string query = "SELECT DISTINCT strNombre_per FROM SIG_PERIODOS";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        // Método para obtener los nombres de maestría desde la tabla UB_CARRERAS
        public DataTable ObtenerNombreMaestria()
        {
            string query = "SELECT DISTINCT strNombre_Car FROM UB_CARRERAS";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        // Método para obtener las notas según el período académico y nombre de maestría
        public DataTable ObtenerNotas(string periodoAcademico, string nombreMaestria)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("POSG_GetNotas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Comodin", "byPeriodo");
                    command.Parameters.AddWithValue("@FILTRO1", string.IsNullOrEmpty(periodoAcademico) ? DBNull.Value : (object)periodoAcademico);
                    command.Parameters.AddWithValue("@FILTRO2", string.IsNullOrEmpty(nombreMaestria) ? DBNull.Value : (object)nombreMaestria);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
