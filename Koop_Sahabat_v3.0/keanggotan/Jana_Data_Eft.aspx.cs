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
using Ionic.Zip;
using System.Text.RegularExpressions;

public partial class Jana_Data_Eft : System.Web.UI.Page
{
    //log objBusi = new log();
    DBConnection Dbcon = new DBConnection();
    string Status = string.Empty;
    SqlDataReader Dr;
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable wilayah = new DataTable();
    DataTable caw = new DataTable();
    DataTable pusat = new DataTable();
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string status;
    DateTime dmula, fromdate, todate;
    string bcode;
    string wcode;
    string userid;
    string sno;
    string ccode;
    string Sql = string.Empty;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                batchBind();
                wilahBind();
                bindgrid();
                txtbeftname.Text = "PS" + DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0130' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('101','1052','1067','1068','64','65','66','23','102','898','899','883')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;


            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            //ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            //ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }


    }


        string RightJustified(int count)
    {
        if (count < 0)
            count = 0;
        return new string('0', count);
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
        SqlCommand cmd = new SqlCommand("select Batch_name,COUNT(FAIL_NAME) File_count from Settlement_batch GROUP BY Batch_name order by Batch_name desc", con);
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


    protected void DownloadFiles(object sender, EventArgs e)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            zip.AddDirectoryByName("EFT");
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl2");
            string lblid = lblTitle.Text;
            DataTable dt = Dbcon.Ora_Execute_table("select Fail_name from Settlement_batch where Batch_name='" + lblid + "'");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string filePath = Server.MapPath("~/FILES/SETGFILE/" + dt.Rows[i][0].ToString());
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

    protected void lnkView_Click2(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl2");
        string lblid = lblTitle.Text;



        DataTable dt = Dbcon.Ora_Execute_table("select Fail_name from Settlement_batch where Batch_name='" + lblid + "'");
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Inssql1 = "delete from Settlement_batch where Fail_name='" + dt.Rows[i][0].ToString() + "' and Batch_name='" + lblid + "'";
                Status = Dbcon.Ora_Execute_CommamdText(Inssql1);
                if (Status == "SUCCESS")
                {
                    string filePath = Server.MapPath("~/FILES/SETGFILE/" + dt.Rows[i][0].ToString());
                    File.Delete(filePath);
                }
            }
            //string filePath = (sender as LinkButton).CommandArgument;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Rekod Berjaya Dihapuskan');window.location ='Jana_Data_Eft.aspx';", true);
            //Response.Redirect(Request.Url.AbsoluteUri);
        }

    }

    void wilahBind()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select Wilayah_Name,Wilayah_Code from Ref_Wilayah";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddwil.DataSource = dt;
            ddwil.DataBind();
            ddwil.DataTextField = "Wilayah_Name";
            ddwil.DataValueField = "Wilayah_Code";
            ddwil.DataBind();            
            ddwil.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void batchBind()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select set_batch_name from mem_settlement where ISNULL(set_batch_name,'') != '' and ISNULL(set_eft_batch_name,'') ='' group by set_batch_name order by set_batch_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddbat.DataSource = dt;
            ddbat.DataBind();
            ddbat.DataTextField = "set_batch_name";
            ddbat.DataBind();            
            ddbat.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   
    protected void DeleteFile(object sender, EventArgs e)
    {
        string Inssql1 = "delete from Settlement_batch where Fail_name=''";
        Status = Dbcon.Ora_Execute_CommamdText(Inssql1);
        if (Status == "SUCCESS")
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void ibtn_download_Click(object sender, EventArgs e)
    {
        int cut1 = 0;
        int cnt1 = 0;
        int cnt2 = 0;
        int counter = 0;
        try
        {

            if (ddbat.SelectedItem.Text != "--- PILIH ---")
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
                string eftbatchname = "";
                string set_new_icno = "";
                string set_app_amt = "";
                string bankaccno = "";
                string set_bank_cd = "";
                string Bop = "";
                string mem_name = "";
                string set_bank_Acc = "";
                string set_newicno = "";
                string pname = "";
                string pdet = "";
                string mem_add = "";
                string mem_old_icno = "";
                string mem_mno = "";
                string mem_sno = "";
                //todate = DateTime.ParseExact(Txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //fromdate = DateTime.ParseExact(Txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string from = fromdate.ToString("yyyy/MM/dd");
                string to = todate.ToString("yyyy/MM/dd");
                string cdate = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                userid = Session["New"].ToString();
                DataTable dt = Dbcon.Ora_Execute_table("select count(*) as cnt from mem_settlement ms inner join mem_member mm on ms.set_new_icno=mm.mem_new_icno and mm.Acc_sts ='Y' left join Ref_jenis_permohonan rf on ms.set_reason_cd=rf.Application_code  where ms.Acc_sts ='Y' and ms.set_batch_name= '" + ddbat.SelectedItem.Text + "' ");
                if (ddbat.SelectedItem.Text != "--- PILIH ---" && ddwil.SelectedItem.Text == "--- PILIH ---")
                {

                    Sql = "select ms.set_batch_name,ms.set_new_icno,ms.set_apply_amt,ms.set_bank_cd,mm.mem_name,ms.set_bank_acc_no,rf.Application_name,mm.mem_email,mm.mem_old_icno,mm.mem_member_no,mm.mem_sahabat_no,ms.set_wasi_icno,set_wasi_name from mem_settlement ms inner join mem_member mm on ms.set_new_icno=mm.mem_new_icno and mm.Acc_sts ='Y' left join Ref_jenis_permohonan rf on ms.set_appl_type_cd=rf.Application_code  where ms.Acc_sts ='Y' and ms.set_batch_name= '" + ddbat.SelectedItem.Text + "' ";
                }
                else if (ddbat.SelectedItem.Text != "--- PILIH ---" && ddwil.SelectedItem.Text != "--- PILIH ---")
                {
                    Sql = "select ms.set_batch_name,ms.set_new_icno,ms.set_apply_amt,ms.set_bank_cd,mm.mem_name,ms.set_bank_acc_no,rf.Application_name,mm.mem_email,mm.mem_old_icno,mm.mem_member_no,mm.mem_sahabat_no,ms.set_wasi_icno,set_wasi_name from mem_settlement ms inner join mem_member mm on ms.set_new_icno=mm.mem_new_icno and mm.Acc_sts ='Y' left join Ref_jenis_permohonan rf on ms.set_appl_type_cd=rf.Application_code  where ms.Acc_sts ='Y' and ms.set_batch_name= '" + ddbat.SelectedItem.Text + "'  and mm.mem_region_cd='" + ddwil.SelectedItem.Text + "' ";
                }
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

                        //ddbat.SelectedItem.Text = "S" + DateTime.Now.ToString("yyMMdd") + cnt1;
                        eftbatchname = "S" + DateTime.Now.ToString("yyMMdd") + cnt1.ToString().PadLeft(3,'0');
                        set_new_icno = Dr["set_wasi_icno"].ToString();//b
                        set_app_amt = Dr["set_apply_amt"].ToString();
                        bankaccno = "12289010000076";
                        set_bank_cd = Dr["set_bank_cd"].ToString();//b
                        Bop = "1";
                        mem_name = Dr["set_wasi_name"].ToString();//b
                        set_bank_Acc = Dr["set_bank_acc_no"].ToString();//b
                        set_newicno = Dr["set_wasi_icno"].ToString();//b
                        pname = "KOPERASI AMANAH IKHTIAR MALAYSIA BHD";
                        pdet = Dr["Application_name"].ToString();
                        mem_add = Dr["mem_email"].ToString();
                        mem_old_icno = Dr["set_new_icno"].ToString();//a
                        mem_mno = Dr["mem_member_no"].ToString();
                        mem_sno = Dr["mem_sahabat_no"].ToString();
                        double tot_transaction_amt = 0;
                        tot_transaction_amt = tot_transaction_amt + Convert.ToDouble(set_app_amt.ToString());
                        string[] tarytran = tot_transaction_amt.ToString("#.00").Split('.');
                        newText.AppendLine(eftbatchname + Space(10 - eftbatchname.ToString().Length) + set_new_icno + Space(14 - set_new_icno.ToString().Length) + Space(8) + Space(8) + Space(8) + Space(8) + Space(3) + RightJustified(13 - tarytran[0].Length) + tarytran[0] + tarytran[1] + bankaccno + Space(20 - bankaccno.ToString().Length) + Space(14) + Space(40) + set_bank_cd + Space(11 - set_bank_cd.ToString().Length) + Bop + Space(1 - Bop.ToString().Length) + Space(20) + mem_name + Space(120 - mem_name.ToString().Length) + set_bank_Acc + Space(20 - set_bank_Acc.ToString().Length) + set_newicno + Space(15 - set_newicno.ToString().Length) + pname + Space(40 - pname.ToString().Length) + pdet + Space(140 - pdet.ToString().Length) + Space(14) + Space(5) + Space(5) + Space(1) + Space(40) + Space(12) + mem_add + Space(105 - mem_add.ToString().Length) + Space(12) + Space(40) + Space(40) + Space(2) + Space(40) + Space(20) + Space(10) + mem_old_icno + Space(15 - mem_old_icno.ToString().Length) + mem_mno + Space(15 - mem_mno.ToString().Length) + mem_sno + Space(15 - mem_sno.ToString().Length));
                        newText1.AppendLine(eftbatchname + Space(10 - eftbatchname.ToString().Length) + set_new_icno + Space(14 - set_new_icno.ToString().Length) + Space(8) + Space(8) + Space(8) + Space(8) + Space(3) + RightJustified(13 - tarytran[0].Length) + tarytran[0] + tarytran[1] + bankaccno + Space(20 - bankaccno.ToString().Length) + Space(14) + Space(40) + set_bank_cd + Space(11 - set_bank_cd.ToString().Length) + Bop + Space(1 - Bop.ToString().Length) + Space(20) + mem_name + Space(120 - mem_name.ToString().Length) + set_bank_Acc + Space(20 - set_bank_Acc.ToString().Length) + set_newicno + Space(15 - set_newicno.ToString().Length) + pname + Space(40 - pname.ToString().Length) + pdet + Space(140 - pdet.ToString().Length) + Space(14) + Space(5) + Space(5) + Space(1) + Space(40) + Space(12) + mem_add + Space(105 - mem_add.ToString().Length) + Space(12) + Space(40) + Space(40) + Space(2) + Space(40) + Space(20) + Space(10) + mem_old_icno + Space(15 - mem_old_icno.ToString().Length) + mem_mno + Space(15 - mem_mno.ToString().Length) + mem_sno + Space(15 - mem_sno.ToString().Length));
                        ++cut1;

                        using (SqlCommand cmd = new SqlCommand("update mem_settlement set set_eft_batch_name=@Name,set_eft_id=@eftid,set_eft_dt=@eftdate where set_batch_name='"+ ddbat.SelectedValue + "' and set_new_icno='" + set_newicno + "' and Acc_sts ='Y'", con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Name", eftbatchname);
                            cmd.Parameters.AddWithValue("@eftid", userid);
                            cmd.Parameters.AddWithValue("@eftdate", cdate);

                            con.Open();
                            int k = cmd.ExecuteNonQuery();
                            con.Close();

                        }
                        int count = Dr.VisibleFieldCount;
                        string baseName;
                        if (cut1 == 999)
                        {
                            ++cnt2;

                            baseName = "SETGFILE" + "\\" + DateTime.Now.ToString("yyMMdd") + cnt2.ToString().PadLeft(3, '0') + ".txt";

                            using (StreamWriter sw = new StreamWriter(Server.MapPath("~/FILES/" + baseName), true))
                            {
                                using (SqlCommand cmd = new SqlCommand("insert into Settlement_batch(Fail_name,Batch_name,crt_id,crt_dt)values(@Fail_name,@Batch_name,@crt_id,@crt_dt)", con))
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Parameters.AddWithValue("@Fail_name", DateTime.Now.ToString("yyMMdd") + cnt2.ToString().PadLeft(3, '0') + ".txt");
                                    cmd.Parameters.AddWithValue("@Batch_name", ddbat.SelectedItem.Text);
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
                            baseName = "SETGFILE" + "\\" + DateTime.Now.ToString("yyMMdd") + cnt2.ToString().PadLeft(3, '0') + ".txt";
                            using (StreamWriter sw1 = new StreamWriter(Server.MapPath("~/FILES/" + baseName), true))
                            {
                                using (SqlCommand cmd = new SqlCommand("insert into Settlement_batch(Fail_name,Batch_name,crt_id,crt_dt)values(@Fail_name,@Batch_name,@crt_id,@crt_dt)", con))
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Parameters.AddWithValue("@Fail_name", DateTime.Now.ToString("yyMMdd") + cnt2.ToString().PadLeft(3, '0') + ".txt");
                                    cmd.Parameters.AddWithValue("@Batch_name", ddbat.SelectedItem.Text);
                                    cmd.Parameters.AddWithValue("@crt_id", Session["New"].ToString());
                                    cmd.Parameters.AddWithValue("@crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                                    con.Open();
                                    int k = cmd.ExecuteNonQuery();
                                    con.Close();

                                }

                                sw1.WriteLine(RemoveEmptyLines(newText.ToString()));
                                sw1.Close();
                            }
                        }
                    }
                    Dr.Close();
                    service.audit_trail("P0130", ddbat.SelectedItem.Text, "Nama Kelompok Bayaran", txtbeftname.Text);
                    bindgrid();
                    ddbat.SelectedValue = "";
                    ddwil.SelectedValue = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dimuatnaik.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Harus Mandatory.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    
                }

            }


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
    void clr_value()
    {
        //Txtfromdate.Text = "";
        //Txttodate.Text = "";
        ddbat.SelectedValue = "";
        ddwil.SelectedValue = "";
       
    }
}