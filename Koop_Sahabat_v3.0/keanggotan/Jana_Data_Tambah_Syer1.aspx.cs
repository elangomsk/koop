using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Threading;



public partial class Jana_Data_Tambah_Syer1 : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    DataTable pusat = new DataTable();
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string userid;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                Txtfromdate.Attributes.Add("readonly", "readonly");
                Txttodate.Attributes.Add("readonly", "readonly");
                //wilahBind();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('79','1052','902','63','64','65','66','68','883','1053')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0212' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();
                //if (role_view == "1")
                //{
                //    Button2.Visible = true;
                //}
                //else
                //{
                //    Button2.Visible = false;
                //}
                //if (role_add == "1")
                //{
                //    Button2.Visible = true;
                //}
                //else
                //{
                //    Button2.Visible = false;
                //}

                //if (role_edit == "1")
                //{
                //    Button5.Visible = true;
                //}
                //else
                //{
                //    Button5.Visible = false;
                //}

            }
        }
    }

    private DataTable GetData(string fromDate, string toDate, string shbtid)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ToString();
        using (SqlConnection cn = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("jana_dta_ta_sy", cn);
            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromDate;
            cmd.Parameters.Add("@todate", SqlDbType.DateTime).Value = toDate;
            cmd.Parameters.Add("@apply_status", SqlDbType.VarChar).Value = shbtid;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
        }
        return dt;
    }

    public void showreport_test()
    {
        try
        {
            string script1 = "$(function () { $('[id*=GridView1]').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); });";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);


            string fdate = Txtfromdate.Text;
            DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datedari = ft.ToString("yyyy-mm-dd");

            string tdate = Txttodate.Text;
            DateTime tt = DateTime.ParseExact(tdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datehingga = tt.ToString("yyyy-mm-dd");

            //string shbtid = ddlpj.SelectedItem.Value;
            string shbtid = "";
            //string txtara = TextArea3.Value;
            //TextArea3.Value = "P";
            string stsno = "P";
            txtcurntdt.Text = "AS" + DateTime.Now.ToString("yyyyMMdd");

            DateTime dmula;
            DateTime dakhir;
            //DateTime dty;

            DateTime today = DateTime.Now;
            DateTime update = DateTime.Now;
            //DataSource
            DataTable dt = new DataTable();

            dmula = today;
            dakhir = today;
            if ((datedari == "") && (datehingga == ""))
            {
                dmula = today;
                dakhir = today;

                Txttodate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                Txtfromdate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, shbtid);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataTable ddfee = new DataTable();
                    ddfee = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtcurntdt.Text + "',sha_apply_sts_ind='" + stsno + "',sha_upd_id = '" + Session["New"].ToString() + "',sha_upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sha_new_icno ='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt = '" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
                service.audit_trail("P0212", "Proses", "Nama Kelompok", txtcurntdt.Text);
                //gvCustomers.DataSource = dt;
                //gvCustomers.DataBind();
                Button1.Visible = true;
                Button3.Visible = true;
            }

            else if ((datedari != "") && (datehingga != ""))
            {
                dmula = DateTime.ParseExact(datedari, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = DateTime.ParseExact(datehingga, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, shbtid);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataTable ddfee = new DataTable();
                    ddfee = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtcurntdt.Text + "',sha_apply_sts_ind='" + stsno + "',sha_upd_id = '" + Session["New"].ToString() + "',sha_upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sha_new_icno ='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt = '" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");
                    txtcurntdt.Text = "AS" + DateTime.Now.ToString("yyyyMMdd");
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
                service.audit_trail("P0212", "Proses", "Nama Kelompok", txtcurntdt.Text);
                //gvCustomers.DataSource = dt;
                //gvCustomers.DataBind();
                Button1.Visible = true;
                Button3.Visible = true;

            }
            //date mula ada, date akhir tiada
            else if ((datedari != "") && (datehingga == ""))
            {
                dmula = DateTime.ParseExact(datedari, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = today;
                Txttodate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                //("MM/dd/yyyy HH:mm:ss.fff",
                //     CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, shbtid);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataTable ddfee = new DataTable();
                    ddfee = DBCon.Ora_Execute_table(" update mem_share set sha_batch_name='" + txtcurntdt.Text + "',sha_apply_sts_ind='" + stsno + "',sha_upd_id = '" + Session["New"].ToString() + "',sha_upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sha_new_icno ='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt = '" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
                service.audit_trail("P0212", "Proses","Nama Kelompok", txtcurntdt.Text);
                //gvCustomers.DataSource = dt;
                //gvCustomers.DataBind();
                Button1.Visible = true;
                Button3.Visible = true;
            }

            else if ((datedari == "") && (datehingga != ""))
            {
                dmula = today;
                dakhir = DateTime.ParseExact(datehingga, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                Txtfromdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, shbtid);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataTable ddfee = new DataTable();
                    ddfee = DBCon.Ora_Execute_table(" update mem_share set sha_batch_name='" + txtcurntdt.Text + "',sha_upd_id = '" + Session["New"].ToString() + "',sha_upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sha_new_icno ='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt = '" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
                service.audit_trail("P0212", "Proses", "Nama Kelompok", txtcurntdt.Text);
                //gvCustomers.DataSource = dt;
                //gvCustomers.DataBind();
                Button1.Visible = true;
                Button3.Visible = true;
            }

            
         
        }

        catch (Exception ex)
        {
            //txtError.Text = ex.ToString();
        }

    }


    protected void ExportToPDF(object sender, EventArgs e)
    {
        try
        {
            string fdate = Txtfromdate.Text;
            DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datedari = ft.ToString("yyyy-mm-dd");

            string tdate = Txttodate.Text;
            DateTime tt = DateTime.ParseExact(tdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datehingga = tt.ToString("yyyy-mm-dd");

            //string shbtid = ddlpj.SelectedItem.Value;
            string shbtid = "";
            //string txtara = TextArea3.Value;
            //TextArea3.Value = "P";
            string stsno = "P";
            txtcurntdt.Text = "AS" + DateTime.Now.ToString("yyyyMMdd");

            DateTime dmula;
            DateTime dakhir;
            //DateTime dty;

            DateTime today = DateTime.Now;
            DateTime update = DateTime.Now;
            //DataSource
            DataTable dt = new DataTable();

            dmula = today;
            dakhir = today;
            if ((datedari == "") && (datehingga == ""))
            {
                dmula = today;
                dakhir = today;

                Txttodate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                Txtfromdate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, shbtid);

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{

                //    DataTable ddfee = new DataTable();
                //    ddfee = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtcurntdt.Text + "',sha_apply_sts_ind='" + stsno + "',sha_upd_id = '" + Session["New"].ToString() + "',sha_upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sha_new_icno ='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt = '" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");
                //}

            }

            else if ((datedari != "") && (datehingga != ""))
            {
                dmula = DateTime.ParseExact(datedari, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = DateTime.ParseExact(datehingga, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, shbtid);

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{

                //    DataTable ddfee = new DataTable();
                //    ddfee = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtcurntdt.Text + "',sha_apply_sts_ind='" + stsno + "',sha_upd_id = '" + Session["New"].ToString() + "',sha_upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sha_new_icno ='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt = '" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");
                //    txtcurntdt.Text = "AS" + DateTime.Now.ToString("yyyyMMdd");
                //}

            }
            //date mula ada, date akhir tiada
            else if ((datedari != "") && (datehingga == ""))
            {
                dmula = DateTime.ParseExact(datedari, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = today;
                Txttodate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                //("MM/dd/yyyy HH:mm:ss.fff",
                //     CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, shbtid);

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{

                //    DataTable ddfee = new DataTable();
                //    ddfee = DBCon.Ora_Execute_table(" update mem_share set sha_batch_name='" + txtcurntdt.Text + "',sha_apply_sts_ind='" + stsno + "',sha_upd_id = '" + Session["New"].ToString() + "',sha_upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sha_new_icno ='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt = '" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");
                //}

            }

            else if ((datedari == "") && (datehingga != ""))
            {
                dmula = today;
                dakhir = DateTime.ParseExact(datehingga, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                Txtfromdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, shbtid);
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{

                //    DataTable ddfee = new DataTable();
                //    ddfee = DBCon.Ora_Execute_table(" update mem_share set sha_batch_name='" + txtcurntdt.Text + "',sha_upd_id = '" + Session["New"].ToString() + "',sha_upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sha_new_icno ='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt = '" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");
                //}
            }


            //  Reset
            ReportViewer1.Reset();

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            if (countRow != 0)
            {
                // txtError.Text = "";
                //Display Report
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);


                ReportViewer1.LocalReport.DataSources.Add(rds);
                //Path
                ReportViewer1.LocalReport.ReportPath = "keanggotan/jana_dta_sy.rdlc";
                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                //Parameters
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("fromDate", Txtfromdate.Text),
                     new ReportParameter("toDate", Txttodate.Text),
                     new ReportParameter("Kelompok", txtcurntdt.Text),
                     //new ReportParameter("Janaan", ddlpj.SelectedItem.Text),
                     new ReportParameter("Janaan", ""),
                     //new ReportParameter("Katerangan", Txtkat.Value),
                     new ReportParameter("current_date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                     };


             
                //Refresh
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.LocalReport.SetParameters(rptParams);
                ReportViewer1.LocalReport.Refresh();
                string filename = string.Format("{0}.{1}", "Jana_Data_Tambah_Syer_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                ReportViewer1.LocalReport.DisplayName = "Jana_Data_Tambah_Syer_" + DateTime.Now.ToString("ddMMyyyy");
                //}
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul');", true);

                // txtError.Text = "Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.";
            }
        }

        catch (Exception ex)
        {
            //txtError.Text = ex.ToString();
        }
        string script1 = "$(function () { $('[id*=GridView1]').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void ExportToEXCEL(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count1 = 0;


        string fdate = Txtfromdate.Text;
        DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
        String datedari = ft.ToString("yyyy-mm-dd");

        string tdate = Txttodate.Text;
        DateTime tt = DateTime.ParseExact(tdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
        String datehingga = tt.ToString("yyyy-mm-dd");

        //string shbtid = ddlpj.SelectedItem.Value;
        string shbtid = "";
        //string txtara = TextArea3.Value;
        //TextArea3.Value = "P";
        string stsno = "P";
        txtcurntdt.Text = "AS" + DateTime.Now.ToString("yyyyMMdd");

        DateTime dmula;
        DateTime dakhir;
        //DateTime dty;

        DateTime today = DateTime.Now;
        DateTime update = DateTime.Now;
        //DataSource
        DataTable dt = new DataTable();

        dmula = today;
        dakhir = today;
        if ((datedari == "") && (datehingga == ""))
        {
            dmula = today;
            dakhir = today;

            Txttodate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
            Txtfromdate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
            dt = GetData(datedari, datehingga, shbtid);


        }

        else if ((datedari != "") && (datehingga != ""))
        {
            dmula = DateTime.ParseExact(datedari, "yyyy-mm-dd", CultureInfo.InvariantCulture);
            dakhir = DateTime.ParseExact(datehingga, "yyyy-mm-dd", CultureInfo.InvariantCulture);
            dt = GetData(datedari, datehingga, shbtid);


        }
        //date mula ada, date akhir tiada
        else if ((datedari != "") && (datehingga == ""))
        {
            dmula = DateTime.ParseExact(datedari, "yyyy-mm-dd", CultureInfo.InvariantCulture);
            dakhir = today;
            Txttodate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
            //("MM/dd/yyyy HH:mm:ss.fff",
            //     CultureInfo.InvariantCulture);
            dt = GetData(datedari, datehingga, shbtid);


        }

        else if ((datedari == "") && (datehingga != ""))
        {
            dmula = today;
            dakhir = DateTime.ParseExact(datehingga, "yyyy-mm-dd", CultureInfo.InvariantCulture);
            Txtfromdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
            dt = GetData(datedari, datehingga, shbtid);

        }



        if (dt.Rows.Count != 0)
        {


            StringBuilder builder = new StringBuilder();
            string strFileName = string.Format("{0}.{1}", "JANA_DATA_TAMBAH_SYER_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
            builder.Append("No KP Baru,Nama,No Anggota, Wilayah, Nama Pusat, Alamat,Jenis Caruman, Jumlah Belian Syer(RM)" + Environment.NewLine);
            for (int k = 0; k <= (dt.Rows.Count - 1); k++)
            {

                builder.Append(dt.Rows[k]["mem_new_icno"].ToString() + " , " + dt.Rows[k]["mem_name"].ToString() + "," + dt.Rows[k]["mem_member_no"].ToString() + "," + dt.Rows[k]["Wilayah_Name"].ToString() + "," + dt.Rows[k]["mem_centre"].ToString() + "," + dt.Rows[k]["mem_address"].ToString().Replace(",", " ") + "," + dt.Rows[k]["sha_ref"].ToString() + "," + dt.Rows[k]["sha_debit_amt"].ToString().Replace(",", "") + Environment.NewLine);

            }
            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
            Response.Write(builder.ToString());
            Response.End();

        }
        else
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

        string script1 = "$(function () { $('[id*=GridView1]').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    private void ShowReport()
    {
        try
        {
            string fdate = Txtfromdate.Text;
            DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datedari = ft.ToString("yyyy-mm-dd");

            string tdate = Txttodate.Text;
            DateTime tt = DateTime.ParseExact(tdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datehingga = tt.ToString("yyyy-mm-dd");

            //string shbtid = ddlpj.SelectedItem.Value;
            string shbtid = "";
            //string txtara = TextArea3.Value;
            //TextArea3.Value = "P";
            string stsno = "P";
            txtcurntdt.Text = "AS" + DateTime.Now.ToString("yyyyMMdd");

            DateTime dmula;
            DateTime dakhir;
            //DateTime dty;

            DateTime today = DateTime.Now;
            DateTime update = DateTime.Now;
            //DataSource
            DataTable dt = new DataTable();

            dmula = today;
            dakhir = today;
            if ((datedari == "") && (datehingga == ""))
            {
                dmula = today;
                dakhir = today;

                Txttodate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                Txtfromdate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, shbtid);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataTable ddfee = new DataTable();
                    ddfee = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtcurntdt.Text + "',sha_apply_sts_ind='" + stsno + "',sha_upd_id = '" + Session["New"].ToString() + "',sha_upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sha_new_icno ='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt = '" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");                    
                }

            }

            else if ((datedari != "") && (datehingga != ""))
            {
                dmula = DateTime.ParseExact(datedari, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = DateTime.ParseExact(datehingga, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, shbtid);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataTable ddfee = new DataTable();
                    ddfee = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtcurntdt.Text + "',sha_apply_sts_ind='" + stsno + "',sha_upd_id = '" + Session["New"].ToString() + "',sha_upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sha_new_icno ='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt = '" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");                    
                    txtcurntdt.Text = "AS" + DateTime.Now.ToString("yyyyMMdd");
                }

            }
            //date mula ada, date akhir tiada
            else if ((datedari != "") && (datehingga == ""))
            {
                dmula = DateTime.ParseExact(datedari, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = today;
                Txttodate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                //("MM/dd/yyyy HH:mm:ss.fff",
                //     CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, shbtid);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataTable ddfee = new DataTable();
                    ddfee = DBCon.Ora_Execute_table(" update mem_share set sha_batch_name='" + txtcurntdt.Text + "',sha_apply_sts_ind='" + stsno + "',sha_upd_id = '" + Session["New"].ToString() + "',sha_upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sha_new_icno ='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt = '" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");                    
                }

            }

            else if ((datedari == "") && (datehingga != ""))
            {
                dmula = today;
                dakhir = DateTime.ParseExact(datehingga, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                Txtfromdate.Text = today.ToString("yyyy-mm-dd", CultureInfo.InvariantCulture);
                dt = GetData(datedari, datehingga, shbtid);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataTable ddfee = new DataTable();
                    ddfee = DBCon.Ora_Execute_table(" update mem_share set sha_batch_name='" + txtcurntdt.Text + "',sha_upd_id = '" + Session["New"].ToString() + "',sha_upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sha_new_icno ='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt = '" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");                    
                }
            }

            ReportViewer1.Reset();
            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();
            if (countRow != 0)
            {

                //Label1.Text = "";
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);


                ReportViewer1.LocalReport.DataSources.Add(rds);
                //Path
                ReportViewer1.LocalReport.ReportPath = "keanggotan/jana_dta_sy.rdlc";
                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                //Parameters
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("fromDate", Txtfromdate.Text),
                     new ReportParameter("toDate", Txttodate.Text),
                     new ReportParameter("Kelompok", txtcurntdt.Text),
                     //new ReportParameter("Janaan", ddlpj.SelectedItem.Text),
                     new ReportParameter("Janaan", ""),
                     //new ReportParameter("Katerangan", Txtkat.Value),
                     new ReportParameter("current_date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                     };


                ReportViewer1.LocalReport.SetParameters(rptParams);

                //Refresh
                ReportViewer1.LocalReport.Refresh();

            }
            else if (countRow == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai Dalam Julat Tarikh Yang Dimasukkan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                // Label1.Text = "Rekod Tidak Dijumpai Dalam Julat Tarikh Yang Dimasukkan";
            }

        }

        catch (Exception ex)
        {
            //Label1.Text = ex.ToString();
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Txtfromdate.Text != "" && Txttodate.Text != "")
        {
            showreport_test();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

}