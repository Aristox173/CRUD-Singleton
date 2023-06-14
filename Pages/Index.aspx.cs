using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRUD.Utilities;

namespace CRUD.Pages
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarTabla();
            }
        }

        void CargarTabla()
        {
            using (SqlConnection con = DBHelper.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_load", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvusuarios.DataSource = dt;
                gvusuarios.DataBind();
            }
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            // Obtener la fecha y hora actual
            DateTime fechaActual = DateTime.Now;

            // Obtener la dirección IP del cliente
            string direccionIP = Request.UserHostAddress;

            // Crear la cadena de texto a guardar en el archivo de registro
            string logText = $"{fechaActual}|{direccionIP}|CREATE";

            // Obtener la ruta completa del archivo de registro
            string rutaArchivoLog = Server.MapPath("~/Logs/log.txt");

            // Escribir la cadena de texto en el archivo de registro
            using (StreamWriter sw = File.AppendText(rutaArchivoLog))
            {
                sw.WriteLine(logText);
            }

            // Redireccionar a la página CRUD.aspx con el parámetro 'op=C'
            Response.Redirect("~/Pages/CRUD.aspx?op=C");
        }

        protected void BtnRead_Click(object sender, EventArgs e)
        {
            string id;
            Button BtnConsultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)BtnConsultar.NamingContainer;
            id = selectedrow.Cells[1].Text;
            Response.Redirect("~/Pages/CRUD.aspx?id=" + id + "&op=R");
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            string id;
            Button BtnConsultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)BtnConsultar.NamingContainer;
            id = selectedrow.Cells[1].Text;
            Response.Redirect("~/Pages/CRUD.aspx?id=" + id + "&op=U");
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            string id;
            Button BtnConsultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)BtnConsultar.NamingContainer;
            id = selectedrow.Cells[1].Text;
            Response.Redirect("~/Pages/CRUD.aspx?id=" + id + "&op=D");
        }
    }
}
