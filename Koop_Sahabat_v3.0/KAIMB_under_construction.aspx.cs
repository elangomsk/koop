using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;
using System.Data;  

public partial class KAIMB_under_construction : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
            }
            else
            {
                Response.Redirect("~/KTHB_Logout.aspx", true);
            }
        }
        
    }
    
    //void BindData()
    //{
    //    string query1 = "select e.Name as Name,t.TitleName as Title ,e.Email as Email,EmployeeId From [Employee] e inner join Title t on t.TitleId=e.TitleId Group by Name,TitleName,Email,EmployeeId order by Name,TitleName,Email";
    //    SqlCommand com = new SqlCommand(query1);
    //    GridView1.DataSource = GetData(com);
    //    GridView1.DataBind();
    //}
    //private DataTable GetData(SqlCommand cmd)
    //{
    //    string strConnString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(strConnString))
    //    {
    //        using (SqlDataAdapter sda = new SqlDataAdapter())
    //        {
    //            cmd.Connection = con;
    //            sda.SelectCommand = cmd;
    //            using (DataTable dt = new DataTable())
    //            {
    //                sda.Fill(dt);
    //                return dt;
    //            }
    //        }
    //    }
    //}

    //void bBind()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {

    //        SqlConnection con = new SqlConnection(cs);
    //        string com = "select keterangan,keterangan_code from ref_status_aduan order by keterangan";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        ddadu.DataSource = dt;
    //        ddadu.DataBind();
    //        ddadu.DataTextField = "keterangan";
    //        ddadu.DataValueField = "keterangan_code";
    //        ddadu.DataBind();
    //        ddadu.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
}