using System;
using System.Collections.Generic;
using System.Globalization;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class POSG_Dashboard : System.Web.UI.Page
{
    protected string LabelsDefense { get; set; }
    protected string DataDefense { get; set; }
    protected string LabelsPie { get; set; }
    protected string DataPie { get; set; }
    protected string LabelsDoughnut { get; set; }
    protected string DataDoughnut { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Implementa los métodos para obtener las etiquetas y los datos
            LoadChartData();
            LoadPieChartData();
            LoadDoughnutChartData();

        }
    }

    private void LoadChartData()
    {
        List<string> labels = new List<string>();
        List<string> data = new List<string>();
        List<string> backgroundColors = new List<string>();
        Random rand = new Random();

        string connectionString = "data source=.; database=TESIS; integrated security=SSPI";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_PromedioDefensaPorCarreraYPeriodo", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                labels.Add(string.Format("{0} ({1})", reader["Carrera"], reader["PeriodoAcademico"]));

                decimal promedioDefensa;
                if (decimal.TryParse(reader["PromedioDefensa"].ToString(), out promedioDefensa) && promedioDefensa >= 0 && promedioDefensa <= 10)
                {
                    data.Add(promedioDefensa.ToString(System.Globalization.CultureInfo.InvariantCulture));
                }
                else
                {
                    data.Add("0.00");
                }

                // Generar un color aleatorio para cada barra
                string color = string.Format("rgba({0},{1},{2},0.5)", rand.Next(256), rand.Next(256), rand.Next(256));
                backgroundColors.Add(color);

            }
        }

        LabelsDefense = string.Join("\",\"", labels);
        DataDefense = string.Join(",", data);
    }

    private void LoadPieChartData()
    {
        List<string> labels = new List<string>();
        List<string> data = new List<string>();
        List<string> backgroundColors = new List<string>();
        Random rand = new Random();

        string connectionString = "data source=.; database=TESIS; integrated security=SSPI";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_TopMaestriaMatriculados", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                labels.Add(string.Format("{0} ({1})", reader["Carrera"], reader["PeriodoAcademico"]));

                data.Add(reader["NumeroMatriculados"].ToString());

                // Generar un color aleatorio para cada sección del pastel
                string color = string.Format("rgba({0},{1},{2},0.5)", rand.Next(256), rand.Next(256), rand.Next(256));
                backgroundColors.Add(color);
            }

        }

        LabelsPie = string.Join("\",\"", labels);
        DataPie = string.Join(",", data);
    }

    private void LoadDoughnutChartData()
    {
        List<string> labels = new List<string>();
        List<string> data = new List<string>();
        List<string> backgroundColors = new List<string>();
        Random rand = new Random();

        string connectionString = "data source=.; database=TESIS; integrated security=SSPI";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_PromedioActasPorCarreraYPeriodo", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                labels.Add(string.Format("{0} ({1})", reader["Carrera"], reader["PeriodoAcademico"]));

                decimal promedioActa;
                if (decimal.TryParse(reader["PromedioActa"].ToString(), out promedioActa) && promedioActa >= 0 && promedioActa <= 10)
                {
                    data.Add(promedioActa.ToString(System.Globalization.CultureInfo.InvariantCulture));
                }
                else
                {
                    data.Add("0.00");
                }

                // Generar un color aleatorio para cada sección del donut
                string color = string.Format("rgba({0},{1},{2},0.5)", rand.Next(256), rand.Next(256), rand.Next(256));
                backgroundColors.Add(color);

            }
        }

        LabelsDoughnut = string.Join("\",\"", labels);
        DataDoughnut = string.Join(",", data);
    }

}
