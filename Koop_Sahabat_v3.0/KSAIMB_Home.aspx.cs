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
using System.Threading;

public partial class KSAIMB_Home : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable get_ahli_sa = new DataTable();
    DataTable get_jum_syer = new DataTable();
    DataTable get_sts_pa = new DataTable();
    DataTable get_sts_ts = new DataTable();
    DataTable chk_menu = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                Modules();
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
        public string mod_val9 { get; set; }

    }
    protected void Modules()
    {
        DataTable ddicno1_stf = new DataTable();

        ddicno1_stf = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + Session["New"].ToString() + "'");

        DataSet ds = new DataSet();
        DataTable FromTable = new DataTable();
        List<Protopbaners> details1 = new List<Protopbaners>();
        con.Open();
        string cmdstr = "select s1.skrin_id,s2.position,s2.KK_Skrin_name,s2.skrin_ikon from KK_Role_skrins s1 left join KK_PID_Skrin s2 on s2.KK_Skrin_id=s1.skrin_id where Status='A' group by s1.skrin_id,s2.position,s2.KK_Skrin_name,s2.skrin_ikon order by cast(s2.position as int) asc";
        //string cmdstr = "select s1.skrin_id,s2.position,s2.KK_Skrin_name,s2.skrin_ikon from KK_Role_skrins s1 left join KK_PID_Skrin s2 on s2.KK_Skrin_id=s1.skrin_id where Role_id IN ('" + ddicno1_stf.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "') group by s1.skrin_id,s2.position,s2.KK_Skrin_name,s2.skrin_ikon order by cast(s2.position as int) asc";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.ExecuteNonQuery();
        FromTable = ds.Tables[0];
        if (FromTable.Rows.Count != 0)
        {
            foreach (DataRow dtrow in FromTable.Rows)
            {
                Protopbaners user1 = new Protopbaners();

                user1.mod_val1 = dtrow["skrin_id"].ToString();
                user1.mod_val2 = dtrow["KK_Skrin_name"].ToString();
                user1.mod_val3 = dtrow["skrin_ikon"].ToString();
                user1.mod_val4 = dtrow["skrin_id"].ToString();
                user1.mod_val5 = dtrow["position"].ToString();
                if (dtrow["skrin_id"].ToString() == "M0007") { user1.mod_val7 = "1"; }
                else if (dtrow["skrin_id"].ToString() == "M0005") { user1.mod_val7 = "2"; }
                else if (dtrow["skrin_id"].ToString() == "M0008") { user1.mod_val7 = "3"; }
                else if (dtrow["skrin_id"].ToString() == "M0001") { user1.mod_val7 = "4"; }
                else if (dtrow["skrin_id"].ToString() == "M0006") { user1.mod_val7 = "5"; }
                else if (dtrow["skrin_id"].ToString() == "M0002") { user1.mod_val7 = "6"; }
                else if (dtrow["skrin_id"].ToString() == "M0003") { user1.mod_val7 = "7"; }
                else if (dtrow["skrin_id"].ToString() == "M0004") { user1.mod_val7 = "8"; }

                chk_menu = DBCon.Ora_Execute_table("select s1.skrin_id,s2.position,s2.KK_Skrin_name,s2.skrin_ikon from KK_Role_skrins s1 left join KK_PID_Skrin s2 on s2.KK_Skrin_id=s1.skrin_id where Role_id IN ('" + ddicno1_stf.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "') and s2.KK_Skrin_id = '" + dtrow["skrin_id"].ToString() + "' group by s1.skrin_id,s2.position,s2.KK_Skrin_name,s2.skrin_ikon order by cast(s2.position as int) asc");

                if (chk_menu.Rows.Count == 0)
                {
                    user1.mod_val6 = "style='cursor:no-drop'";
                    user1.mod_val7 = "9";
                    user1.mod_val8 = "#";
                }
                else
                {
                    if (dtrow["skrin_id"].ToString() == "M0001")
                    {
                        user1.mod_val8 = "/HR_Dashboard.aspx";
                       
                    }
                    else if (dtrow["skrin_id"].ToString() == "M0002")
                    {
                        user1.mod_val8 = "/Finance_dashboard.aspx";
                    }
                    else if (dtrow["skrin_id"].ToString() == "M0007")
                    {
                        if (Session["roles"].ToString() != "R0016")
                        {
                            user1.mod_val8 = "/Dashboard.aspx";
                        }
                        else
                        {
                            user1.mod_val8 = "keanggotan/Maklumat_Anggota.aspx";
                        }
                    }
                    else if (dtrow["skrin_id"].ToString() == "M0003")
                    {
                        user1.mod_val8 = "/Dashboard.aspx";
                    }
                    else if (dtrow["skrin_id"].ToString() == "M0006")
                    {
                        user1.mod_val8 = "/#";
                        user1.mod_val9 = "style='pointer-events:none;'";
                    }
                    //else if (dtrow["skrin_id"].ToString() == "M0005")
                    //{
                    //    user1.mod_val8 = "/#";
                    //    user1.mod_val9 = "style='pointer-events:none;'";
                    //}
                    else if (dtrow["skrin_id"].ToString() == "M0008")
                    {
                        user1.mod_val8 = "/Dashboard.aspx";
                    }
                    else if (dtrow["skrin_id"].ToString() == "M0004")
                    {
                        user1.mod_val8 = "/settings/site_settings.aspx";
                    }
                    else
                    {
                        user1.mod_val8 = "/Dashboard.aspx";                        
                    }
                    user1.mod_val6 = "style='cursor:pointer'";
                }

                details1.Add(user1);
            }
            ds.Dispose();
            con.Close();
            bnd_modules.DataSource = details1.ToArray();
            bnd_modules.DataBind();
        }
    }

    protected void get_mnulink(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        Label lblName = (Label)item.FindControl("ss1");
        Label lbl_link = (Label)item.FindControl("ss2");
        Session["mnu_id"] = lblName.Text;
        Response.Redirect(lbl_link.Text);
    }
}