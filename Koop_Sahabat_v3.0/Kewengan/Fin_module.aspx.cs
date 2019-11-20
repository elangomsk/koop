using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Threading;
using System;
using System.Web;
using System.Collections.Generic;

public partial class Fin_module : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable get_lv_det = new DataTable();
    StudentWebService service = new StudentWebService();
    DataTable ddicno1_stf = new DataTable();
    DataTable ddicno1 = new DataTable();
    DataTable tot_salary_wtng = new DataTable();
    DataTable ddicno1_role = new DataTable();
    DataTable get_role_no = new DataTable();
    DataTable get_role_no1 = new DataTable();
    string checkimage = string.Empty;
    string fileName = string.Empty;
    string strQuery = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                Load_menus();
            }
            else
            {
                Response.Redirect("KSAIMB_Login.aspx");
            }
        }
    }

    public class Protopbaners
    {
        public string mod_val1 { get; set; }
        public string mod_val2 { get; set; }
        public string mod_val3 { get; set; }
        public string mod_image_path { get; set; }
        public string mod_val4 { get; set; }
        public string mod_val5 { get; set; }

        public string mod_val6 { get; set; }
        public string mod_val7 { get; set; }
        public string mod_val8 { get; set; }

    }
    protected void Load_menus()
    {

        CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
        TextInfo txtinfo = culinfo.TextInfo;
        DataTable ddicno1_stf = new DataTable();
        ddicno1_stf = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + Session["New"].ToString() + "'");
       
        DataSet ds = new DataSet();
        DataTable FromTable = new DataTable();
        List<Protopbaners> details1 = new List<Protopbaners>();
        con1.Open();
        string cmdstr = "select * from KK_Role_skrins s1 left join KK_PID_presub1_Skrin s2 on s2.KK_Skrin_id=s1.skrin_id and s1.sub_skrin_id=s2.KK_Sskrin_id and s1.psub_skrin_id=s2.KK_Spreskrin_id and s1.psub1_skrin_id=s2.KK_Spreskrin1_id where Role_id IN ('" + ddicno1_stf.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "') and KK_Spreskrin_id IN ('" + Request.QueryString["edit"].ToString() + "')";
        //string cmdstr = "select s1.skrin_id,s2.position,s2.KK_Skrin_name,s2.skrin_ikon from KK_Role_skrins s1 left join KK_PID_Skrin s2 on s2.KK_Skrin_id=s1.skrin_id where Role_id IN ('" + ddicno1_stf.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "') group by s1.skrin_id,s2.position,s2.KK_Skrin_name,s2.skrin_ikon order by cast(s2.position as int) asc";
        SqlCommand cmd = new SqlCommand(cmdstr, con1);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.ExecuteNonQuery();
        FromTable = ds.Tables[0];
        if (FromTable.Rows.Count != 0)
        {
            foreach (DataRow dtrow in FromTable.Rows)
            {
                Protopbaners user1 = new Protopbaners();

                user1.mod_val1 = txtinfo.ToTitleCase(dtrow["KK_Spreskrin1_name"].ToString().ToLower());
                if (dtrow["KK_preskrin1_link"].ToString() == "#")
                {
                    user1.mod_val6 = "href='#'";
                }
                else
                {
                    user1.mod_val6 = "href='/"+dtrow["KK_preskrin1_link"].ToString() +"'";
                }

                user1.mod_val2 = dtrow["KK_Spreskrin1_ikon"].ToString();
                details1.Add(user1);
            }
            ds.Dispose();
            con1.Close();
            bnd_mmenus.DataSource = details1.ToArray();
            bnd_mmenus.DataBind();
        }
    }
}