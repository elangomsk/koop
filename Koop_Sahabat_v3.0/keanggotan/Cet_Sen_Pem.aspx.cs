using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Data;
using System.Threading;

public partial class Cet_Sen_Pem : System.Web.UI.Page
{
    public string autotag = "";
    DBConnection Con = new DBConnection();
    DataTable zon = new DataTable();
    DataTable wilayah = new DataTable();
    DataTable caw = new DataTable();
    DataTable pusat = new DataTable();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    string Status = string.Empty;
    string usrid;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                usrid = Session["New"].ToString();
                //TxtFdate.Attributes.Add("readonly", "readonly");
                //TxtTdate.Attributes.Add("readonly", "readonly");
                BindData();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }

    }


    void app_language()
    {
        if (Session["New"] != null)
        {
            assgn_roles();
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('127','1052','1077','70','64','65','126','500','1042','886')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;



            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            shwrpt.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            btnrest.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }

    }
    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0136' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();

                if (role_add == "1")
                {
                    shwrpt.Visible = true;
                }
                else
                {
                    shwrpt.Visible = false;
                }

            }
        }
    }

    private void BindData()
    {
        DataSet Ds = new DataSet();
        try
        {
            //SqlConnection con = new SqlConnection(cs);
            string com = "select code,discription from ref_appl_bank_sts order by discription";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlstpm.DataSource = dt;
            ddlstpm.DataBind();
            ddlstpm.DataTextField = "discription";
            ddlstpm.DataValueField = "code";
            ddlstpm.DataBind();
            ddlstpm.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }


    }


    private DataTable GetData(string fromDate, string toDate, string appsts)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ToString();
        using (SqlConnection cn = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Cetak_kad_keng", cn);
            //cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromDate;
            cmd.Parameters.Add("@todate", SqlDbType.DateTime).Value = toDate;
            cmd.Parameters.Add("@rak_appl_sts_cd", SqlDbType.VarChar).Value = appsts;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);

        }
        return dt;
    }


    public void ShowReport()
    {
        try
        {
            string fmdate = TxtFdate.Text;
            DateTime ft = DateTime.ParseExact(fmdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datedari = ft.ToString("yyyy-mm-dd");

            string tdate = TxtTdate.Text;
            DateTime td = DateTime.ParseExact(tdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datehingga = td.ToString("yyyy-mm-dd");

            string appsts = ddlstpm.SelectedItem.Value;
            DateTime dmula;
            DateTime dakhir;
            DateTime today = DateTime.Now;
            //DataSource
            DataTable dt = new DataTable();

            dmula = today;
            dakhir = today;

            if ((datedari == "") && (datehingga == ""))
            {
                dmula = today;
                dakhir = today;
                TxtFdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                TxtTdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, appsts);                
            }
            //date mula ada, date akhir ada
            else if ((datedari != "") && (datehingga != ""))
            {
                dmula = DateTime.ParseExact(datedari, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = DateTime.ParseExact(datehingga, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, appsts);

            }
            //date mula ada, date akhir tiada
            else if ((datedari != "") && (datehingga == ""))
            {
                
                dmula = DateTime.ParseExact(datedari, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = today;
                TxtTdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, appsts);                
            }

            else if ((datedari == "") && (datehingga != ""))
            {
                dmula = today;
                dakhir = DateTime.ParseExact(datehingga, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                TxtFdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);                
                dt = GetData(datedari, datehingga, appsts);                
            }

            //Reset
            ReportViewer1.Reset();

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            if (countRow != 0)
            {
                disp_hdr_txt.Visible = true;
                // Label1.Text = "";
                //Label2.Text = "";
                ReportDataSource rds = new ReportDataSource("Cetak_kad_keng", dt);

                ReportViewer1.LocalReport.DataSources.Add(rds);

                //Path
                ReportViewer1.LocalReport.ReportPath = "keanggotan/Cet_Sen_Pem.rdlc";
                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                //Parameters
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("fromDate",TxtFdate.Text ),
                     new ReportParameter("toDate",TxtTdate.Text ),
                     new ReportParameter("Status",ddlstpm.SelectedItem.Text),
                     new ReportParameter("current_date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                     };


                ReportViewer1.LocalReport.SetParameters(rptParams);

                //Refresh
                ReportViewer1.LocalReport.Refresh();
            }
            else if (countRow == 0)
            {
                disp_hdr_txt.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tiada Rekod Dijumpai Dalam Julat Tarikh Yang Dimasukkan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }

        catch (Exception ex)
        {
            //Label2.Text = ex.ToString();
        }
    }

    
    protected void shwrpt_Click(object sender, EventArgs e)
    {
        if (TxtFdate.Text != "" && TxtTdate.Text != "" && ddlstpm.Text != "")
        {
            ShowReport();
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Harus Mandatory.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void btnrest_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}