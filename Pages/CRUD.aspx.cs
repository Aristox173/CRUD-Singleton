using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using CRUD.Utilities;

namespace CRUD.Pages
{
    public partial class CRUD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    CargarDatos();
                }

                if (Request.QueryString["op"] != null)
                {
                    string sOpc = Request.QueryString["op"].ToString();

                    switch (sOpc)
                    {
                        case "C":
                            lbltitulo.Text = "Ingresar nuevo usuario";
                            BtnCreate.Visible = true;
                            break;
                        case "R":
                            lbltitulo.Text = "Consulta de usuario";
                            break;
                        case "U":
                            lbltitulo.Text = "Modificar usuario";
                            BtnUpdate.Visible = true;
                            break;
                        case "D":
                            lbltitulo.Text = "Eliminar usuario";
                            BtnDelete.Visible = true;
                            break;
                    }
                }
            }
        }

        void CargarDatos()
        {
            string sID = Request.QueryString["id"].ToString();

            using (SqlConnection con = DBHelper.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_read", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = sID;
                con.Open();
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    ds.Clear();
                    da.Fill(ds);
                }
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                tbCodigoPeriodoLectivo.Text = row[1].ToString();
                tbDescripcionPeriodoLectivo.Text = row[2].ToString();
                tbsucursal.Text = row[3].ToString();
                tbestado.Text = row[4].ToString();
            }
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DBHelper.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_create", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CodigoPeriodoLectivo", SqlDbType.VarChar).Value = tbCodigoPeriodoLectivo.Text;
                cmd.Parameters.Add("@DescripcionPeriodoLectivo", SqlDbType.Int).Value = tbDescripcionPeriodoLectivo.Text;
                cmd.Parameters.Add("@AAia", SqlDbType.VarChar).Value = tbsucursal.Text;
                cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = tbestado.Text;
                cmd.ExecuteNonQuery();
            }

            Response.Redirect("Index.aspx");
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DBHelper.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_update", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CodigoPeriodoLectivo", SqlDbType.Int).Value = tbCodigoPeriodoLectivo.Text;
                cmd.Parameters.Add("@DescripcionPeriodoLectivo", SqlDbType.VarChar).Value = tbCodigoPeriodoLectivo.Text;
                cmd.Parameters.Add("@AAia", SqlDbType.Int).Value = tbDescripcionPeriodoLectivo.Text;
                cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = tbestado.Text;
                cmd.ExecuteNonQuery();
            }

            Response.Redirect("Index.aspx");
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DBHelper.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_delete", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CodigoPeriodoLectivo", SqlDbType.Int).Value = tbCodigoPeriodoLectivo.Text;
                cmd.ExecuteNonQuery();
            }

            Response.Redirect("Index.aspx");
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}
