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
using System.Windows.Forms;
using System.Diagnostics;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.ComponentModel;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Threading;
using Ionic.Zip;
using System.Text.RegularExpressions;

public partial class Jana_Fail_Eft : System.Web.UI.Page
{
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    //log objBusi = new log();
    DBConnection Dbcon = new DBConnection();
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    StringBuilder sb = new StringBuilder();
    DataTable wilayah = new DataTable();
    DataTable caw = new DataTable();
    DataTable pusat = new DataTable();
    string Status = string.Empty;
    SqlDataReader Dr;
    string filename, fname;
    string userid;
    string Sql = string.Empty;
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
                userid = Session["New"].ToString();
                kawasan();

                batch();
                bindgrid();

                ddwil.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                ddcaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));

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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('137','1052','903','138','66','22','108','23','25','139','898','36','883')");


            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            s_bank.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0140' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();


            }
        }
    }


    string RightJustified(int count)
    {
        if (count < 0)
            count = 0;
        return new string('0', count);
    }
    string LeftJustified(int count)
    {
        if (count > 0)
            count = 0;
        return new string(' ', count);
    }
    string Space(int count)
    {
        if (count < 0)
            count = 0;
        return new string(' ', count);
    }

    void bindgrid()
    {
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select Batch_name,COUNT(FAIL_NAME) File_count from DIV_EFT_batch GROUP BY Batch_name order by Batch_name desc", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        con.Close();


    }

    void kawasan()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select kavasan_name from Ref_Cawangan  group by kavasan_name order by kavasan_name asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddkaw.DataSource = dt;
            ddkaw.DataBind();
            ddkaw.DataTextField = "kavasan_name";

            ddkaw.DataBind();
            ddkaw.Items.Insert(0, new ListItem("EMPTY", "EMPTY"));
            ddkaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void batch()
    {

        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select div_batch_name from mem_divident where div_batch_name !='' and ISNULL(div_eft_batch_name,'') ='' group by div_batch_name order by div_batch_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddbatch.DataSource = dt;
            ddbatch.DataBind();
            ddbatch.DataTextField = "div_batch_name";
            ddbatch.DataBind();
            ddbatch.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    private DataTable GetData(string kawas, string wilaya, string cawan, string pus, string batch)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ToString();
        using (SqlConnection cn = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("JANA_EFT", cn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@kawas", SqlDbType.VarChar).Value = kawas;
            cmd.Parameters.Add("@wilaya", SqlDbType.VarChar).Value = wilaya;
            cmd.Parameters.Add("@cawan", SqlDbType.VarChar).Value = cawan;
            cmd.Parameters.Add("@pus", SqlDbType.VarChar).Value = pus;
            cmd.Parameters.Add("@batch", SqlDbType.VarChar).Value = batch;
            //cmd.Parameters.Add("@acname", SqlDbType.VarChar).Value = acname;



            //cmd.Parameters.Add("@shbtid", SqlDbType.VarChar).Value = shbtid;
            //cmd.Parameters.Add("@shbtnama", SqlDbType.VarChar).Value = namashbt;

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);

        }
        return dt;
    }


    protected void DownloadFile(object sender, EventArgs e)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            zip.AddDirectoryByName("Files");
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl2");
            string lblid = lblTitle.Text;
            DataTable dt = Dbcon.Ora_Execute_table("select Distinct Fail_name from DIV_EFT_batch where Batch_name='" + lblid + "'");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string filePath = Server.MapPath("~/FILES/DIVEFT/" + dt.Rows[i][0].ToString());
                    zip.AddFile(filePath, "Files");
                }


                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("Zip_{0}.zip", lblid);
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }
        }
    }
    protected void DeleteFile(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl2");
        string lblid = lblTitle.Text;

        DataTable dt = Dbcon.Ora_Execute_table("select Fail_name from DIV_EFT_batch where Batch_name='" + lblid + "'");
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Inssql1 = "delete from DIV_EFT_batch where Fail_name='" + dt.Rows[i][0].ToString() + "' and Batch_name='" + lblid + "'";
                Status = Dbcon.Ora_Execute_CommamdText(Inssql1);
                if (Status == "SUCCESS")
                {
                    string filePath = Server.MapPath("~/FILES/DIVEFT/" + dt.Rows[i][0].ToString());
                    File.Delete(filePath);
                }
            }
            //string filePath = (sender as LinkButton).CommandArgument;
            bindgrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
           
            //Response.Redirect(Request.Url.AbsoluteUri);
        }

    }




    protected void ddwil_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddkaw.SelectedItem.Text == "SEMUA KAWASAN")
        {
            pusat.Rows.Clear();
            ddkaw.Items.Clear();
            ddkaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        //-Pusat---------------------------------------------------------------------------------
        string cmd5 = "select cawangan_name,cawangan_code from Ref_Cawangan where kavasan_name='" + ddkaw.SelectedItem.Text + "' and wilayah_name='" + ddwil.SelectedItem.Text + "' ";
        pusat.Rows.Clear();
        ddcaw.Items.Clear();

        con.Open();
        SqlDataAdapter adapterP = new SqlDataAdapter(cmd5, con);
        adapterP.Fill(pusat);

        ddcaw.DataSource = pusat;
        ddcaw.DataTextField = "cawangan_name";
        ddcaw.DataValueField = "cawangan_code";
        ddcaw.DataBind();
        //ddPusat.Items.RemoveAt(0); //remove 'Semua Wilayah'
        con.Close();

        ddcaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }
    protected void ddkaw_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddkaw.SelectedItem.Text == "SEMUA WILAYAH")
        {
            wilayah.Rows.Clear();
            ddwil.Items.Clear();
            ddwil.Items.Insert(0, new ListItem("SEMUA WILAYAH", ""));
        }
        //-Pusat---------------------------------------------------------------------------------
        string cmd6 = "select  wilayah_code,wilayah_name from  Ref_Cawangan where kavasan_name='" + ddkaw.SelectedItem.Text + "' group by wilayah_code,wilayah_name";
        wilayah.Rows.Clear();
        ddwil.Items.Clear();

        con.Open();
        SqlDataAdapter adapterP = new SqlDataAdapter(cmd6, con);
        adapterP.Fill(wilayah);

        ddwil.DataSource = wilayah;
        ddwil.DataTextField = "wilayah_name";
        ddwil.DataValueField = "wilayah_code";
        ddwil.DataBind();
        //ddPusat.Items.RemoveAt(0); //remove 'Semua Wilayah'
        con.Close();

        ddwil.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }


    protected void ExportTextFile(object sender, EventArgs e)
    {
        int cut1 = 0;
        int cnt1 = 0;
        int cnt2 = 0;
        int counter = 0;
        try
        {
            if (ddbatch.SelectedItem.Text != "--- PILIH ---")
            {
                string LogFilePath = System.Configuration.ConfigurationManager.AppSettings["fpath_succcess"];
                string logFileName = "", gstrLogFile = "";
                if (!System.IO.Directory.Exists(LogFilePath))
                {
                    System.IO.Directory.CreateDirectory(LogFilePath);
                }
                logFileName = System.DateTime.Today.ToString("dd-MMM-yyyy") + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".txt";
                gstrLogFile = logFileName;
                System.IO.File.Delete(logFileName);
                StringBuilder newText = new StringBuilder();
                StringBuilder newText1 = new StringBuilder();
                StreamWriter LogFile = null;
                //HEAD
                string Batchname = "";
                string newicno = "";
                string debitamt = "";
                string bankaccno = "";
                string bankcode = "";
                string bop = "";
                string name = "";
                string divbankaccno = "";
                string divicno = "";
                string pname = "";
                string paydet = "";
                string maddress = "";
                string poldicno = "";
                string mno = "";
                string sno = "";
                DataTable dtkaw = new DataTable();
                string stkaw = string.Empty;
                DataTable dtt = new DataTable();
                string kaw;
                string wil;
                string caw = ddcaw.SelectedItem.Text;
                string bat = ddbatch.SelectedItem.Text;
                DataTable dt = new DataTable();



                if (ddkaw.SelectedItem.Text == "--- PILIH ---")
                {
                    if (s_bank.Checked == true)
                    {
                        Sql = "SELECT ISNULL(mm.kavasan_name,'') kavasan_name,ISNULL(mm.wilayah_name,'') wilayah_name,ISNULL(mm.cawangan_name,'') cawangan_name,md.ID,md.div_batch_name,md.div_new_icno,md.div_debit_amt,md.div_bank_cd,mm.mem_name,md.div_bank_acc_no,md.div_new_icno,md.div_remark ,mm.mem_email,mm.mem_old_icno,mm.mem_member_no,mm.mem_sahabat_no FROM mem_divident md outer apply (select top(1) * from mem_member mm1 left join Ref_Cawangan as rb on rb.cawangan_code=mm1.mem_branch_cd where mm1.mem_new_icno=div_new_icno order by mem_crt_dt desc ) mm where md.div_batch_name='"+ddbatch.SelectedValue+"' and md.div_approve_ind='SA' and div_bank_acc_no ='' order by mm.Wilayah_Name,mm.cawangan_code,mm.mem_centre";
                        dt = Dbcon.Ora_Execute_table("SELECT COUNT(*) as cnt FROM mem_divident md outer apply (select top(1) * from mem_member mm1 left join Ref_Cawangan as rb on rb.cawangan_code=mm1.mem_branch_cd where mm1.mem_new_icno=div_new_icno order by mem_crt_dt desc ) mm where md.div_batch_name='" + ddbatch.SelectedValue + "' and md.div_approve_ind='SA' and div_bank_acc_no ='' ");
                    }
                    else
                    {
                        Sql = "SELECT ISNULL(mm.kavasan_name,'') kavasan_name,ISNULL(mm.wilayah_name,'') wilayah_name,ISNULL(mm.cawangan_name,'') cawangan_name,md.ID,md.div_batch_name,md.div_new_icno,md.div_debit_amt,md.div_bank_cd,mm.mem_name,md.div_bank_acc_no,md.div_new_icno,md.div_remark ,mm.mem_email,mm.mem_old_icno,mm.mem_member_no,mm.mem_sahabat_no FROM mem_divident md outer apply (select top(1) * from mem_member mm1 left join Ref_Cawangan as rb on rb.cawangan_code=mm1.mem_branch_cd where mm1.mem_new_icno=div_new_icno order by mem_crt_dt desc ) mm where md.div_batch_name='" + ddbatch.SelectedValue + "' and md.div_approve_ind='SA' and div_bank_acc_no !='' order by mm.Wilayah_Name,mm.cawangan_code,mm.mem_centre";
                        dt = Dbcon.Ora_Execute_table("SELECT COUNT(*) as cnt FROM mem_divident md outer apply (select top(1) * from mem_member mm1 left join Ref_Cawangan as rb on rb.cawangan_code=mm1.mem_branch_cd where mm1.mem_new_icno=div_new_icno order by mem_crt_dt desc ) mm where md.div_batch_name='" + ddbatch.SelectedValue + "' and md.div_approve_ind='SA' and div_bank_acc_no !='' ");
                    }
                }
                else if (ddkaw.SelectedItem.Text != "--- PILIH ---")
                {
                    string new_str = string.Empty;
                    string sql_2 = string.Empty;
                    if (ddkaw.SelectedItem.Text != "EMPTY")
                    {
                        new_str = ddkaw.SelectedItem.Text;

                    }
                    if (s_bank.Checked == true)
                    {
                        Sql = "SELECT ISNULL(mm.kavasan_name,'') kavasan_name,ISNULL(mm.wilayah_name,'') wilayah_name,ISNULL(mm.cawangan_name,'') cawangan_name,md.ID,md.div_batch_name,md.div_new_icno,md.div_debit_amt,md.div_bank_cd,mm.mem_name,md.div_bank_acc_no,md.div_new_icno,md.div_remark ,mm.mem_email,mm.mem_old_icno,mm.mem_member_no,mm.mem_sahabat_no FROM mem_divident md outer apply (select top(1) * from mem_member mm1 left join Ref_Cawangan as rb on rb.cawangan_code=mm1.mem_branch_cd where mm1.mem_new_icno=div_new_icno order by mem_crt_dt desc ) mm where md.div_batch_name='" + ddbatch.SelectedValue + "' and md.div_approve_ind='SA' and div_bank_acc_no ='' and ISNULL(mm.kavasan_name,'')='" + new_str + "' order by mm.Wilayah_Name,mm.cawangan_code,mm.mem_centre";
                        dt = Dbcon.Ora_Execute_table("SELECT COUNT(*) as cnt FROM mem_divident md outer apply (select top(1) * from mem_member mm1 left join Ref_Cawangan as rb on rb.cawangan_code=mm1.mem_branch_cd where mm1.mem_new_icno=div_new_icno order by mem_crt_dt desc ) mm where md.div_batch_name='" + ddbatch.SelectedValue + "' and md.div_approve_ind='SA' and div_bank_acc_no ='' and ISNULL(mm.kavasan_name,'')='" +new_str+ "' ");
                    }
                    else
                    {
                        Sql = "SELECT ISNULL(mm.kavasan_name,'') kavasan_name,ISNULL(mm.wilayah_name,'') wilayah_name,ISNULL(mm.cawangan_name,'') cawangan_name,md.ID,md.div_batch_name,md.div_new_icno,md.div_debit_amt,md.div_bank_cd,mm.mem_name,md.div_bank_acc_no,md.div_new_icno,md.div_remark ,mm.mem_email,mm.mem_old_icno,mm.mem_member_no,mm.mem_sahabat_no FROM mem_divident md outer apply (select top(1) * from mem_member mm1 left join Ref_Cawangan as rb on rb.cawangan_code=mm1.mem_branch_cd where mm1.mem_new_icno=div_new_icno order by mem_crt_dt desc ) mm where md.div_batch_name='" + ddbatch.SelectedValue + "' and md.div_approve_ind='SA' and div_bank_acc_no !='' and ISNULL(mm.kavasan_name,'')='" + new_str + "' order by mm.Wilayah_Name,mm.cawangan_code,mm.mem_centre";
                        sql_2 = "SELECT COUNT(*) as cnt FROM mem_divident md outer apply (select top(1) * from mem_member mm1 left join Ref_Cawangan as rb on rb.cawangan_code=mm1.mem_branch_cd where mm1.mem_new_icno=div_new_icno order by mem_crt_dt desc ) mm where md.div_batch_name='" + ddbatch.SelectedValue + "' and md.div_approve_ind='SA' and div_bank_acc_no !='' and ISNULL(mm.kavasan_name,'')='" + new_str + "'";
                        dt = Dbcon.Ora_Execute_table(sql_2);
                    }
                }
                    
                if (dt.Rows[0]["cnt"].ToString() != "0")
                {
                    Dr = Dbcon.Execute_Reader(Sql);
                    if (Dr.HasRows)
                    {


                        while (Dr.Read())
                        {
                            counter++;
                            if (newText.Length == 0 || cut1 == 999)
                            {
                                cut1 = 0;
                                ++cnt1;
                            }
                            else
                            {
                                ++cnt1;
                            }



                            Batchname = "D" + DateTime.Now.ToString("yyMMdd") + cnt1.ToString().PadLeft(3,'0');
                            newicno = Dr["div_new_icno"].ToString();
                            debitamt = Dr["div_debit_amt"].ToString();
                            bankaccno = "12289010000076";
                            bankcode = Dr["div_bank_cd"].ToString();
                            bop = "1";
                            name = Dr["mem_name"].ToString();
                            divbankaccno = Dr["div_bank_acc_no"].ToString();
                            divicno = Dr["div_new_icno"].ToString().Trim();
                            pname = "KOPERASI AMANAH IKHTIAR MALAYSIA BHD";
                            paydet = Dr["div_remark"].ToString();
                            maddress = Dr["mem_email"].ToString();
                            poldicno = Dr["mem_old_icno"].ToString().Trim();
                            mno = Dr["mem_member_no"].ToString();
                            sno = Dr["mem_sahabat_no"].ToString();
                            //h_fpx_exchange_id = Dr["FPX_EXCHANGE_ID"].ToString();
                            //h_fpx_seller_id = Dr["FPX_SELLER_ID"].ToString();
                            double tot_transaction_amt = 0;
                            tot_transaction_amt = tot_transaction_amt + Convert.ToDouble(debitamt.ToString());
                            string[] tarytran = tot_transaction_amt.ToString("#.00").Split('.');

                            string cdate = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                            userid = Session["New"].ToString();
                            if (Batchname != "")
                            {
                                //if (cut1 <= 500)
                                //{
                                    newText.AppendLine(Batchname + Space(10 - Batchname.ToString().Length) + newicno + Space(14 - newicno.ToString().Length) + Space(8) + Space(8) + Space(8) + Space(8) + Space(3) + RightJustified(13 - tarytran[0].Length) + tarytran[0] + tarytran[1] + bankaccno + Space(20 - bankaccno.Length) + Space(14) + Space(40) + bankcode + Space(11 - bankcode.Length) + bop + Space(1 - bop.Length) + Space(20) + name + Space(120 - name.ToString().Length) + divbankaccno + Space(20 - divbankaccno.Length) + divicno + Space(15 - divicno.Length) + pname + Space(40 - pname.ToString().Length) + paydet + Space(140 - paydet.Length) + Space(14) + Space(5) + Space(5) + Space(1) + Space(40) + Space(12) + maddress + Space(105 - maddress.Length) + Space(12) + Space(40) + Space(40) + Space(2) + Space(40) + Space(20) + Space(10) + poldicno + Space(15 - poldicno.Length) + mno + Space(15 - mno.Length) + sno + Space(15 - sno.Length));
                                //}
                                //else
                                //{
                                //    newText1.AppendLine(Batchname + Space(10 - Batchname.ToString().Length) + newicno + Space(14 - newicno.ToString().Length) + Space(8) + Space(8) + Space(8) + Space(8) + Space(3) + RightJustified(13 - tarytran[0].Length) + tarytran[0] + tarytran[1] + bankaccno + Space(20 - bankaccno.Length) + Space(14) + Space(40) + bankcode + Space(11 - bankcode.Length) + bop + Space(1 - bop.Length) + Space(20) + name + Space(120 - name.ToString().Length) + divbankaccno + Space(20 - divbankaccno.Length) + divicno + Space(15 - divicno.Length) + pname + Space(40 - pname.ToString().Length) + paydet + Space(140 - paydet.Length) + Space(14) + Space(5) + Space(5) + Space(1) + Space(40) + Space(12) + maddress + Space(105 - maddress.Length) + Space(12) + Space(40) + Space(40) + Space(2) + Space(40) + Space(20) + Space(10) + poldicno + Space(15 - poldicno.Length) + mno + Space(15 - mno.Length) + sno + Space(15 - sno.Length));
                                //}
                            }
                            ++cut1;

                            DataTable dtgen = new DataTable();
                            string file;
                            dtgen = Dbcon.Ora_Execute_table("select  max(isnull(cast(KE_EFT_File_no as float),0)) +1 KE_EFT_File_no from mem_divident where KE_EFT_Flag='1'");
                            if (dtgen.Rows.Count == 0)
                            {
                                file = DateTime.Now.ToString("yyMMdd") + cnt1;
                            }
                            else
                            {
                                file = dtgen.Rows[0][0].ToString();
                            }
                            using (SqlCommand cmd = new SqlCommand("update mem_divident set div_eft_batch_name=@Name,div_eft_id=@eftid,div_eft_dt =@eftdate,KE_EFT_Flag=@eftflag,KE_EFT_File_no=@fileno where Id='" + Dr["Id"].ToString() + "' and div_new_icno='" + newicno + "' and Acc_sts ='Y'", con))
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.AddWithValue("@Name", Batchname);
                                cmd.Parameters.AddWithValue("@eftid", userid);
                                cmd.Parameters.AddWithValue("@eftdate", cdate);
                                cmd.Parameters.AddWithValue("@eftflag", "1");
                                cmd.Parameters.AddWithValue("@fileno", file);
                                con.Open();
                                int k = cmd.ExecuteNonQuery();
                                con.Close();

                            }

                            string baseName;
                            if (cut1 == 999)
                            {
                                ++cnt2;
                                baseName = "DIVEFT" + "\\" + DateTime.Now.ToString("yyMMdd") + cnt2.ToString().PadLeft(3, '0') + ".txt";
                                using (StreamWriter sw = new StreamWriter(Server.MapPath("~/FILES/" + baseName), true))
                                {
                                    using (SqlCommand cmd = new SqlCommand("insert into DIV_EFT_batch(Fail_name,Batch_name,crt_id,crt_dt)values(@Fail_name,@Batch_name,@crt_id,@crt_dt)", con))
                                    {
                                        cmd.CommandType = CommandType.Text;
                                        cmd.Parameters.AddWithValue("@Fail_name", DateTime.Now.ToString("yyMMdd") + cnt2.ToString().PadLeft(3, '0') + ".txt");
                                        cmd.Parameters.AddWithValue("@Batch_name", ddbatch.SelectedItem.Text);
                                        cmd.Parameters.AddWithValue("@crt_id", Session["New"].ToString());
                                        cmd.Parameters.AddWithValue("@crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                                        con.Open();
                                        int k = cmd.ExecuteNonQuery();
                                        con.Close();

                                    }
                                    sw.WriteLine(RemoveEmptyLines(newText.ToString()));
                                    newText.Clear();
                                    sw.Close();
                                }
                                    cnt1 = 0;
                            }

                            if (Convert.ToInt32(dt.Rows[0][0].ToString()) == counter)
                            {
                                ++cnt2;
                                baseName = "DIVEFT" + "\\" + DateTime.Now.ToString("yyMMddHHmmss") + cnt2.ToString().PadLeft(3, '0') + ".txt";
                                using (StreamWriter sw1 = new StreamWriter(Server.MapPath("~/FILES/" + baseName), true))
                                {
                                    using (SqlCommand cmd = new SqlCommand("insert into DIV_EFT_batch(Fail_name,Batch_name,crt_id,crt_dt)values(@Fail_name,@Batch_name,@crt_id,@crt_dt)", con))
                                    {
                                        cmd.CommandType = CommandType.Text;
                                        cmd.Parameters.AddWithValue("@Fail_name", DateTime.Now.ToString("yyMMddHHmmss") + cnt2.ToString().PadLeft(3, '0') + ".txt");
                                        cmd.Parameters.AddWithValue("@Batch_name", ddbatch.SelectedItem.Text);
                                        cmd.Parameters.AddWithValue("@crt_id", Session["New"].ToString());
                                        cmd.Parameters.AddWithValue("@crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                                        con.Open();
                                        int k = cmd.ExecuteNonQuery();
                                        con.Close();

                                    }
                                    ;
                                    sw1.WriteLine(RemoveEmptyLines(newText.ToString()));
                                    sw1.Close();
                                }
                                
                            }

                        }
                    }
                    Dr.Close();


                    service.audit_trail("P0140", "Jana Fail EFT","Nama Kelompok", ddbatch.SelectedItem.Text);
                    bindgrid();
                    ddbatch.SelectedValue = "";
                    ddkaw.SelectedValue = "";
                    ddcaw.SelectedValue = "";
                    ddwil.SelectedValue = "";
                    TxtPus.Text = "";
                    s_bank.Checked = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dimuatnaik.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nama Kelompok.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);                
            }
            bindgrid();
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('" + ex.Message + "');</script>");
        }
    }

    private string RemoveEmptyLines(string lines)
    {
        return Regex.Replace(lines, @"^\r?\n?$", string.Empty, RegexOptions.Multiline).TrimEnd();
    }
}