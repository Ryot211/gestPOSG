﻿using System;
using ClassLibraryTesis;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class POSG_ActaGradoFirmada : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Verificar si el usuario está logueado y tiene el rol adecuado
            if (Session["userId"] != null)
            {
                string userId = Session["userId"].ToString();
                lblUser.Text = userId; // Asignando el valor del userId al Label

                USERINROLES ur = new USERINROLES();
                var rolUser = ur.LoadUSERINROLES("xPK", "SEACADEMICO", lblUser.Text, "", ""); // Usando lblUser.Text para la validación

                if (rolUser.Count == 1)  // Validación de rol
                {
                    string idActa = Request.QueryString["idActa"];
                    if (!string.IsNullOrEmpty(idActa))
                    {
                        hdnIdActa.Value = idActa;
                    }
                    else
                    {
                        Response.Redirect("Actas_grado.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/POSG/ErrorPage.aspx");  // Redirige si no tiene el rol adecuado
                }
            }
            else
            {
                // Si la sesión es nula, redirige al usuario a la página de inicio de sesión
                Response.Redirect("/Login.aspx");
            }
        }

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fileUpload.HasFile)
        {
            string idActa = hdnIdActa.Value;
            string folderPath = Server.MapPath("~/Files/ACTAS_GRADO/Firmadas/");
            string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
            string filePath = Path.Combine(folderPath, fileName);

            // Crear el directorio si no existe
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Guardar el archivo
            fileUpload.SaveAs(filePath);

            // Actualizar la base de datos con la URL del archivo
            string relativePath = "/Files/ACTAS_GRADO/Firmadas/" + fileName;
            string connectionString = "data source=.; database=TESIS; integrated security=SSPI";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("POSG_UPDATE_ACTA_FIRMADA", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@strId_act", idActa);
                    cmd.Parameters.AddWithValue("@UrlArchivo_act", relativePath);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // Mostrar SweetAlert con éxito y redirigir
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "Swal.fire({title: 'Éxito', text: 'El archivo ha sido subido y actualizado correctamente.', icon: 'success'}).then((result) => { window.location.href = 'POSG_ActasGrado.aspx'; });", true);

        }
        else
        {
            // Mostrar SweetAlert de error
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "Swal.fire({title: 'Error', text: 'Por favor, seleccione un archivo para subir.', icon: 'error'});", true);
        }
    }
}