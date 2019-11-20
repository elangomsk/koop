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

public partial class Cet_aduan : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    DBConnection dbcon = new DBConnection();
    DBConnection DBCon = new DBConnection();
    DataTable stats = new DataTable();
    //string CommandArgument1;
    static string statename = string.Empty, Resitdate = string.Empty;
    string Status = string.Empty;
    string uname;
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
                bBind();
                uname = Session["New"].ToString();
                TxtFdate.Attributes.Add("readonly", "readonly");
                TxtTdate.Attributes.Add("readonly", "readonly");
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('120','1052','1075','1073','64','65','117','500','15','1076')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;


            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());

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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0134' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();

                if (role_add == "1")
                {
                    Button1.Visible = true;
                }
                else
                {
                    Button1.Visible = false;
                }

            }
        }
    }


    void bBind()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select keterangan,keterangan_code from ref_status_aduan order by keterangan ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddadu.DataSource = dt;
            ddadu.DataBind();
            ddadu.DataTextField = "keterangan";
            ddadu.DataValueField = "keterangan_code";
            ddadu.DataBind();
            ddadu.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void showreport()
    {
        try
        {

            //string fdate = TxtFdate.Text;
            string fmdate = TxtFdate.Text;
            DateTime ft = DateTime.ParseExact(fmdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String fdate = ft.ToString("yyyy-mm-dd");

            string todate = TxtTdate.Text;
            DateTime td = DateTime.ParseExact(todate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String tdate = td.ToString("yyyy-mm-dd");

            DateTime dmula;
            DateTime dakhir;
            dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
            dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);

            string sts = ddadu.SelectedItem.Value;
            DateTime today = DateTime.Now;
            //DataSource
            DataTable dt = new DataTable();

            dmula = today;
            dakhir = today;



            // DataTable dt = GetData(DateTime.Parse(datedari), DateTime.Parse(datehingga), nokp, pusat, Caw, Zon, Wil);
            if ((fdate == "") && (tdate == ""))
            {
                dmula = today;
                dakhir = today;

                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                //FromDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                TxtTdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                TxtFdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                dt = GetData(fdate, tdate, sts);


            }
            //date mula ada, date akhir ada
            else if ((fdate != "") && (tdate != ""))
            {
                // dmula = DateTime.ParseExact(datedari, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                // dakhir = DateTime.ParseExact(datehingga, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dt = GetData(fdate, tdate, sts);

            }
            //date mula ada, date akhir tiada
            else if ((fdate != "") && (tdate == ""))
            {
                //   dmula = DateTime.ParseExact(datedari, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = today;
                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                TxtTdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                //("MM/dd/yyyy HH:mm:ss.fff",
                //     CultureInfo.InvariantCulture);
                dt = GetData(fdate, tdate, sts);

            }

            else if ((fdate == "") && (tdate != ""))
            {
                dmula = today;
                //  dakhir = DateTime.ParseExact(datehingga, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                // FromDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                TxtFdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);

                //kena add exception error, date akhir tak boleh previous dr date mula

                dt = GetData(fdate, tdate, sts);

           
            }

            //Reset
            ReportViewer2.Reset();

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            if (countRow != 0)
            {
                //txtError.Text = "";
                ss1.Visible = true;
                //Display Report
                ReportDataSource rds = new ReportDataSource("ADU_CET", dt);

                ReportViewer2.LocalReport.DataSources.Add(rds);

                //Path
                ReportViewer2.LocalReport.ReportPath = "keanggotan/CET_ADU.rdlc";
                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                //Parameters
                string status;
                if (ddadu.SelectedItem.Text == "--- PILIH ---")
                {
                    status = "SEMUA";
                }
                else
                {
                    status = ddadu.SelectedItem.Text;
                }
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("fromDate",TxtFdate.Text ),
                     new ReportParameter("toDate",TxtTdate.Text ),
                     new ReportParameter("Status",status),
                     new ReportParameter("current_date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                     };


                ReportViewer2.LocalReport.SetParameters(rptParams);

                //Refresh
                ReportViewer2.LocalReport.Refresh();
                string filename = string.Format("{0}.{1}", "Cetak_Aduan_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                ReportViewer2.LocalReport.DisplayName = "Cetak_Aduan_" + DateTime.Now.ToString("ddMMyyyy");
                //}
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                byte[] bytes = ReportViewer2.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            else if (countRow == 0)
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tiada Rekod Dijumpai Dalam Julat Tarikh Yang Dimasukkan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                //txtError.Text = "Maklumat Carian Tidak Dijumpai";
            }
        }

        catch (Exception ex)
        {
            //txtError.Text = ex.ToString();
        }

    }




    private DataTable GetData(string fromDate, string toDate, string sts)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ToString();
        using (SqlConnection cn = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("CET_ADU", cn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@frodate", SqlDbType.DateTime).Value = fromDate;
            cmd.Parameters.Add("@todate", SqlDbType.DateTime).Value = toDate;
            cmd.Parameters.Add("@sts", SqlDbType.VarChar).Value = sts;

            //cmd.Parameters.Add("@acname", SqlDbType.VarChar).Value = acname;



            //cmd.Parameters.Add("@shbtid", SqlDbType.VarChar).Value = shbtid;
            //cmd.Parameters.Add("@shbtnama", SqlDbType.VarChar).Value = namashbt;

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);

        }
        return dt;
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TxtFdate.Text != "" && TxtTdate.Text != "")
        {
            showreport();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh Mula & Tarikh Akhir.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (TxtFdate.Text != "" && TxtTdate.Text != "")
        {
            //string fdate = TxtFdate.Text;
            string fmdate = TxtFdate.Text;
            DateTime ft = DateTime.ParseExact(fmdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String fdate = ft.ToString("yyyy-mm-dd");

            string todate = TxtTdate.Text;
            DateTime td = DateTime.ParseExact(todate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String tdate = td.ToString("yyyy-mm-dd");

            DateTime dmula;
            DateTime dakhir;
            dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
            dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);

            string sts = ddadu.SelectedItem.Value;
            DateTime today = DateTime.Now;
            //DataSource
            DataTable dt = new DataTable();

            dmula = today;
            dakhir = today;



            // DataTable dt = GetData(DateTime.Parse(datedari), DateTime.Parse(datehingga), nokp, pusat, Caw, Zon, Wil);
            if ((fdate == "") && (tdate == ""))
            {
                dmula = today;
                dakhir = today;

                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                //FromDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                TxtTdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                TxtFdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                dt = GetData(fdate, tdate, sts);


            }
            //date mula ada, date akhir ada
            else if ((fdate != "") && (tdate != ""))
            {
                // dmula = DateTime.ParseExact(datedari, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                // dakhir = DateTime.ParseExact(datehingga, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dt = GetData(fdate, tdate, sts);

            }
            //date mula ada, date akhir tiada
            else if ((fdate != "") && (tdate == ""))
            {
                //   dmula = DateTime.ParseExact(datedari, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = today;
                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                TxtTdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                //("MM/dd/yyyy HH:mm:ss.fff",
                //     CultureInfo.InvariantCulture);
                dt = GetData(fdate, tdate, sts);

            }

            else if ((fdate == "") && (tdate != ""))
            {
                dmula = today;
                //  dakhir = DateTime.ParseExact(datehingga, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                // FromDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                TxtFdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);

                //kena add exception error, date akhir tak boleh previous dr date mula

                dt = GetData(fdate, tdate, sts);


            }
            ss1.Visible = true;
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                GridView1.DataSource = dt;
                GridView1.DataBind();
                int columncount = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
                Button2.Visible = false;
            }
            else
            {

                GridView1.DataSource = dt;
                GridView1.DataBind();
                Button2.Visible = true;

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh Mula & Tarikh Akhir.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}