using System;
using System.Collections.Generic;
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
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Threading;
using System.Net;
using System.Net.Mail;
public partial class HR_tuntutan : System.Web.UI.Page
{

    DBConnection DBCon = new DBConnection();
    Mailcoms ObjMail = new Mailcoms();
    SMS ObjSms = new SMS();
    CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    string useid = string.Empty;
    string Status = string.Empty;
    string Status1 = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string act_dt = string.Empty;
    string etdate1 = string.Empty, etdate2 = string.Empty, etdate3 = string.Empty, etdate4 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, gt_val2 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
     
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                ET_rno.Text = "0";
                useid = Session["New"].ToString();
                Applcn_no1.Attributes.Add("Readonly", "Readonly");
                TextBox14.Attributes.Add("Readonly", "Readonly");
                TextBox15.Attributes.Add("Readonly", "Readonly");
                TextBox16.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
                txt_org.Attributes.Add("Readonly", "Readonly");
                TextBox5.Attributes.Add("Readonly", "Readonly");
                JelaunBind();
                bind();
                grid();
                useid = Session["New"].ToString();


            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void grid()
    {
        if (ET_mdate.Text != "")
        {
            string E1 = ET_mdate.Text;
            DateTime Edt1 = DateTime.ParseExact(E1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            etdate1 = Edt1.ToString("yyyy-mm-dd");
        }
        else
        {
            etdate1 = "";
        }
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select Id, td1_Name from tblFiles2  where td1_staff_no='"+ Applcn_no1.Text + "' and td1_rec_dt='"+ etdate1 + "'", con);
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
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        con.Close();

    }




    protected void lnkView_Click11(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl1");
        string lblid = lblTitle.Text;
        string filePath = Server.MapPath("~/FILES/EFT/" + lblid.Trim());
        Response.ContentType = ContentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(filePath) +"\"");
        Response.WriteFile(filePath);
        Response.End();
    }

    protected void DeleteFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        DataTable dt = new DataTable();
        dt = DBCon.Ora_Execute_table("delete from tblFiles2 where id='" + id + "' ");
        bind();
        grid();

    }
    void view_details()
    {

    }


    public void bind()
    {
        SqlConnection conn = new SqlConnection(cs);
        //string query2 = "select top(1) stf_staff_no,stf_icno,inc_dept_cd,rhj.hr_jaw_desc,inc_grade_cd,isnull(rhg.hr_gred_desc,'') hr_gred_desc,pos_post_cd,jb.hr_jaba_desc,ho.org_name from hr_income hi left join hr_staff_profile hsp on hsp.stf_staff_no=hi.inc_staff_no left join   hr_post_his hph on hph.pos_staff_no=hsp.stf_staff_no left join ref_hr_jawatan rhj on rhj.hr_jaw_Code=hph.pos_post_cd left join Ref_hr_gred rhg on rhg.hr_gred_Code=hi.inc_grade_cd left join Ref_hr_jabatan as jb on jb.hr_jaba_Code= hi.inc_dept_cd left join hr_organization ho on ho.org_gen_id=hsp.str_curr_org_cd where hsp.stf_staff_no='" + Session["New"].ToString() + "' and pos_end_dt = '9999/12/31' and hi.inc_month='" + DropDownList1.SelectedValue + "' and inc_year='" + TextBox4.Text + "'";
        //string query2 = "select top(1) stf_staff_no,stf_icno,inc_dept_cd,rhj.hr_jaw_desc,inc_grade_cd,isnull(rhg.hr_gred_desc,'') hr_gred_desc,pos_post_cd,jb.hr_jaba_desc,ho.org_name from hr_income hi left join hr_staff_profile hsp on hsp.stf_staff_no=hi.inc_staff_no left join   hr_post_his hph on hph.pos_staff_no=hsp.stf_staff_no left join ref_hr_jawatan rhj on rhj.hr_jaw_Code=hph.pos_post_cd left join Ref_hr_gred rhg on rhg.hr_gred_Code=hi.inc_grade_cd left join Ref_hr_jabatan as jb on jb.hr_jaba_Code= hi.inc_dept_cd left join hr_organization ho on ho.org_gen_id=hsp.str_curr_org_cd where hsp.stf_staff_no='" + Session["New"].ToString() + "' and pos_end_dt = '9999/12/31' and hi.inc_month='" + DropDownList1.SelectedValue + "' and inc_year='" + TextBox4.Text + "'";
        string query2 = "select *,o1.op_perg_name from hr_staff_profile as hsp left join   hr_post_his hph on hph.pos_staff_no=hsp.stf_staff_no left join ref_hr_jawatan rhj on rhj.hr_jaw_Code=hph.pos_post_cd left join Ref_hr_gred rhg on rhg.hr_gred_Code=hsp.stf_curr_grade_cd left join Ref_hr_jabatan as jb on jb.hr_jaba_Code= hsp.stf_curr_dept_cd left join hr_organization ho on ho.org_gen_id=hsp.str_curr_org_cd left join hr_organization_pern o1 on o1.op_perg_code=hsp.stf_cur_sub_org where hsp.stf_staff_no='" + Session["New"].ToString() + "' and pos_end_dt = '9999/12/31'";
        conn.Open();
        var sqlCommand2 = new SqlCommand(query2, conn);
        var sqlReader2 = sqlCommand2.ExecuteReader();
        while (sqlReader2.Read())
        {
            Applcn_no1.Text = (string)sqlReader2["stf_staff_no"].ToString().Trim();
            ET_sdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ET_sdate.Attributes.Add("Readonly", "Readonly");
            TextBox14.Text = (string)sqlReader2["stf_icno"].ToString().Trim();
            TextBox15.Text = (string)sqlReader2["hr_gred_desc"].ToString().Trim();
            TextBox16.Text = (string)sqlReader2["hr_jaba_desc"].ToString().Trim();
            TextBox3.Text = (string)sqlReader2["hr_jaw_desc"].ToString().Trim();
            txt_org.Text = (string)sqlReader2["org_name"].ToString().Trim();
            TextBox5.Text = (string)sqlReader2["op_perg_name"].ToString().Trim();
            grid_2();
        }
        sqlReader2.Close();
    }

    protected void gvSelected_PageIndexChanging_2(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        grid_2();
        bind();

    }

  
    protected void insert_Click2(object sender, EventArgs e)
    {
        try
        {
            string fileName=string.Empty, excelPath = string.Empty, directoryPath = string.Empty, clm_amt = string.Empty;
            if (Applcn_no1.Text != "" && ET_jelaun.SelectedValue != "")
            {

                if (ET_mdate.Text != "" && ET_sdate.Text != "")
                {
                    string E1 = ET_mdate.Text;
                    DateTime Edt1 = DateTime.ParseExact(E1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    etdate1 = Edt1.ToString("yyyy-mm-dd");

                    string E2 = ET_sdate.Text;
                    DateTime Edt2 = DateTime.ParseExact(E2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    etdate2 = Edt2.ToString("yyyy-mm-dd");

                    if (ET_jelaun.SelectedValue == "02")
                    {
                        clm_amt = (double.Parse(TextBox2.Text) - double.Parse(ET_amaun.Text)).ToString("C").Replace("$", "").Replace("RM", "");
                    }
                    else
                    {
                        clm_amt = (double.Parse(TextBox2.Text) + double.Parse(ET_amaun.Text)).ToString("C").Replace("$", "").Replace("RM", "");
                    }

                    //if ((Edt2 - Edt1).TotalDays >= 0)
                    //{
                    //if (FileUpload1.PostedFile.FileName != "")
                    //{

                    //    fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    //    excelPath = Server.MapPath("~/FILES/EFT/") + fileName;
                    //    directoryPath = Path.GetDirectoryName(excelPath);
                    //    FileUpload1.SaveAs(excelPath);

                    //}
                    //else
                    //{
                    //    fileName = TextBox1.Text;
                    //}

                    //if (fileName != "")
                    //{
                        if (ET_rno.Text == "0")
                        {
                            DataTable ddokdicno2 = new DataTable();
                            ddokdicno2 = DBCon.Ora_Execute_table("select * from hr_claim_new where clm_staff_no='" + Applcn_no1.Text + "' and clm_claim_cd='" + ET_jelaun.SelectedValue + "' and clm_rec_dt='" + etdate1 + "'");

                            if (ddokdicno2.Rows.Count == 0)
                            {
                                string Inssql = "INSERT INTO hr_claim_new (clm_staff_no,clm_claim_cd,clm_rec_dt,clm_app_dt,clm_claim_amt,clm_crt_id,clm_crt_dt,file_name,clm_app_sts,clm_balance_amt,clm_cur_balance_amt,clm_sebap) VALUES ('" + Applcn_no1.Text + "','" + ET_jelaun.SelectedValue + "','" + etdate1 + "','" + etdate2 + "','" + ET_amaun.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','','"+ TextBox2.Text +"','"+ clm_amt + "','"+ TextBox4.Text +"')";
                                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                if (Status == "SUCCESS")
                                {
                                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                                    string contentType = FileUpload1.PostedFile.ContentType;
                                    if (filename != "")
                                    {
                                    excelPath = Server.MapPath("~/FILES/EFT/") + filename;
                                    directoryPath = Path.GetDirectoryName(excelPath);
                                    FileUpload1.SaveAs(excelPath);
                                    string Inssql1 = "INSERT INTO tblFiles2 (td1_staff_no,td1_rec_dt,td1_claim_dt,td1_Name,td1_ContentType,td1_crt_id,td1_crt_dt) VALUES ('" + Applcn_no1.Text + "','" + etdate1 + "','" + ET_jelaun.SelectedValue + "','" + filename + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                                        Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                                    }
                                    send_email();
                                    ET_mdate.Text = "";
                                    ET_sdate.Text = "31/12/9999";
                                    ET_amaun.Text = "";
                                    ET_jelaun.SelectedValue = "";
                                    TextBox1.Text = "";
                                    TextBox2.Text = "";
                                    TextBox4.Text = "";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tuntutan Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                            }
                        }
                        else
                        {
                            
                            string Inssql = "update hr_claim_new set clm_sebap='" + TextBox4.Text + "',file_name='',clm_rec_dt='" + etdate1 + "',clm_claim_amt='" + ET_amaun.Text + "',clm_upd_id='" + Session["New"].ToString() + "',clm_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where clm_staff_no='" + Applcn_no1.Text + "' and clm_claim_cd='" + ET_jelaun.SelectedValue + "' and clm_rec_dt='" + etdate1 + "'";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql);
                            if (Status == "SUCCESS")
                            {
                                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                                string contentType = FileUpload1.PostedFile.ContentType;
                               
                                if (filename != "")
                                {
                                excelPath = Server.MapPath("~/FILES/EFT/") + filename;
                                directoryPath = Path.GetDirectoryName(excelPath);
                                FileUpload1.SaveAs(excelPath);
                                string Inssql1 = "INSERT INTO tblFiles2 (td1_staff_no,td1_rec_dt,td1_claim_dt,td1_Name,td1_ContentType,td1_crt_id,td1_crt_dt) VALUES ('" + Applcn_no1.Text + "','" + etdate1 + "','" + ET_jelaun.SelectedValue + "','" + filename + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                                }

                                //send_email();
                                ET_mdate.Text = "";
                                ET_sdate.Text = "31/12/9999";
                                ET_amaun.Text = "";
                                ET_jelaun.SelectedValue = "";
                                TextBox4.Text = "";
                                //ET_jelaun.Attributes.Remove("Disabled");
                                Button2.Text = "Simpan";
                                TextBox1.Text = "";
                                TextBox2.Text = "";
                                ET_rno.Text = "0";
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tuntutan Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                            }
                        }
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan Dokumen.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan Field Tarikh.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
               
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        bind();
        grid();
        grid_2();
    }


    void send_email()
    {
        TextInfo txtinfo = culinfo.TextInfo;
        DataTable Ds = new DataTable();
        DataTable Ds1 = new DataTable();
        DataTable Ds2 = new DataTable();
        DataTable Ds3 = new DataTable();
        DataTable Ds4 = new DataTable();
        string Mailmsg = string.Empty, Mailmsg1 = string.Empty, Mail_id1 = string.Empty, Mail_id2 = string.Empty, stf_name = string.Empty;
        try
        {
            Ds = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid='admin'");
            Ds1 = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid='C0019'");
            DataTable email_settings = new DataTable();
            email_settings = DBCon.Ora_Execute_table("select config_email_head,config_email_host,config_email_id,config_email_port,config_email_pwd,config_email_url,config_email_web from site_settings where ID='1'");
            var fromemail = new MailAddress(email_settings.Rows[0]["config_email_id"].ToString(), email_settings.Rows[0]["config_email_head"].ToString());
            var fromemailpassword = email_settings.Rows[0]["config_email_pwd"].ToString();
            string subject = email_settings.Rows[0]["config_email_web"].ToString() + " - Leave Application";
            var verifyurl = email_settings.Rows[0]["config_email_url"].ToString();
            var link = verifyurl;
            //String strDate = "01/" + DD_bulancaruman.SelectedValue + "/" + txt_tahun.SelectedValue + "";
            DateTime dateTime = DateTime.ParseExact(ET_mdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //string pre_year = dateTime.AddMonths(-1).ToString("yyyy");
            //string pre_mnth = dateTime.AddMonths(-1).ToString("MM");
            //Ds2 = Dblog.Ora_Execute_table("select ISNULL(sum(inc_nett_amt),'0.00') as inc_nett_amt from hr_income where inc_month='" + pre_mnth + "' and inc_year='" + pre_year + "'");
            Ds3 = Dblog.Ora_Execute_table("select * from hr_staff_profile where stf_staff_no='"+ Applcn_no1.Text + "'");
            Ds4 = Dblog.Ora_Execute_table("select hr_month_Code,hr_month_desc from Ref_hr_month where hr_month_Code='" + dateTime.ToString("MM") + "' ORDER BY hr_month_Code");

            if (Ds3.Rows.Count != 0)
            {
                stf_name = Ds3.Rows[0]["stf_name"].ToString();
            }
            else
            {
                stf_name = "";
            }

            if (Ds.Rows[0]["KK_email"].ToString() != "")
            {
                Mail_id1 = Ds.Rows[0]["KK_email"].ToString();
            }

            if (Ds1.Rows[0]["KK_email"].ToString() != "")
            {
                Mail_id2 = Ds1.Rows[0]["KK_email"].ToString();
            }
            // HR Email
            if (Mail_id1 != "")
            {
                var toemail = new MailAddress(Mail_id1);
                string body = "Hello " + txtinfo.ToTitleCase(Ds.Rows[0]["Kk_username"].ToString().ToLower()) + ",<br/>Pending Claim Approval for " + stf_name + " RM " + ET_amaun.Text + " on "+ Ds4.Rows[0]["hr_month_desc"].ToString() + ".<br/><br/> If You require any clarifications about this application, please contact your HR Department.<br/><br/> Thank You,<br/><a><html><body><a href='" + link + "'> " + email_settings.Rows[0]["config_email_web"].ToString() + " </a></body></html> . </a>";

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = email_settings.Rows[0]["config_email_host"].ToString(),
                    Port = Int32.Parse(email_settings.Rows[0]["config_email_port"].ToString()),
                    EnableSsl = false,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromemail.Address, fromemailpassword)


                };
                using (var message = new MailMessage(fromemail, toemail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);

            }
            // Azhari Email
            if (Mail_id2 != "")
            {
                
                var toemail = new MailAddress(Mail_id2);                                                
                string body = "Hello " + txtinfo.ToTitleCase(Ds1.Rows[0]["Kk_username"].ToString().ToLower()) + ",<br/>Claim applied by " + stf_name + " RM " + ET_amaun.Text + " on " + Ds4.Rows[0]["hr_month_desc"].ToString() + " pending for approval (" + txtinfo.ToTitleCase(Ds.Rows[0]["Kk_username"].ToString().ToLower())+ ").<br/><br/> If You require any clarifications about this application, please contact your HR Department.<br/><br/> Thank You,<br/><a><html><body><a href='" + link + "'> " + email_settings.Rows[0]["config_email_web"].ToString() + " </a></body></html> . </a>";

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = email_settings.Rows[0]["config_email_host"].ToString(),
                    Port = Int32.Parse(email_settings.Rows[0]["config_email_port"].ToString()),
                    EnableSsl = false,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromemail.Address, fromemailpassword)


                };
                using (var message = new MailMessage(fromemail, toemail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);

            }
            Session["cr_mnth_slry"] = "";

        }
        catch (Exception ex)
        {
            service.LogError(ex.ToString());
        }
    }
    protected void hapus_click2(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no1.Text != "")
            {
                string rcount = string.Empty;
                int count = 0;
                foreach (GridViewRow gvrow in GridView2.Rows)
                {
                    var rb = gvrow.FindControl("rbtnSelect2") as System.Web.UI.WebControls.CheckBox;
                    if (rb.Checked == true)
                    {
                        count++;
                    }
                    rcount = count.ToString();
                }
                if (rcount != "0")
                {
                    foreach (GridViewRow gvrow in GridView2.Rows)
                    {
                        var checkbox = gvrow.FindControl("rbtnSelect2") as System.Web.UI.WebControls.CheckBox;
                        var app_sts = (System.Web.UI.WebControls.Label)gvrow.FindControl("app_sts");
                        if (checkbox.Checked == true)
                        {
                            if (app_sts.Text == "P")
                            {
                                var a_no = (System.Web.UI.WebControls.Label)gvrow.FindControl("est_no");
                                var a_id = (System.Web.UI.WebControls.Label)gvrow.FindControl("eall_cd");
                                var fct_dt = (System.Web.UI.WebControls.Label)gvrow.FindControl("fxactdt");
                                string fdt = fct_dt.Text;
                                DateTime fdt1 = Convert.ToDateTime(fdt);
                                string fctdt = fdt1.ToString("yyyy/MM/dd");


                                string Inssql = "DELETE from hr_claim_new where clm_staff_no='" + a_no.Text + "' and clm_claim_cd='" + a_id.Text + "' and clm_rec_dt='" + fctdt + "' and clm_app_sts = 'P'";
                                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                            }
                        }
                    }
                    if (Status == "SUCCESS")
                    {
                        ET_mdate.Text = "";
                        ET_sdate.Text = "31/12/9999";
                        ET_amaun.Text = "";
                        TextBox1.Text = "";
                        ET_jelaun.SelectedValue = "";
                        //ET_jelaun.Attributes.Remove("Disabled");
                        Button2.Text = "Simpan";
                        ET_rno.Text = "0";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Elaun Tetap Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
               
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
            bind();
            grid();
            grid_2();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    void JelaunBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_tun_Code,hr_tun_desc from Ref_hr_tuntutan where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ET_jelaun.DataSource = dt;
            ET_jelaun.DataTextField = "hr_tun_desc";
            ET_jelaun.DataValueField = "hr_tun_Code";
            ET_jelaun.DataBind();
            ET_jelaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rset_Click2(object sender, EventArgs e)
    {
        bind();
     
        grid_2();
        Button4.Visible = true;
        ET_mdate.Text = "";
        Button2.Visible = true;
        ET_sdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        ET_amaun.Text = "";
        TextBox2.Text = "";
        TextBox4.Text = "";
        ET_jelaun.SelectedValue = "";
        ET_jelaun.Attributes.Remove("Disabled");
        Button2.Text = "Simpan";        
        ET_rno.Text = "0";
        grid();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        System.Web.UI.WebControls.Label sts = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label5_sts");
        System.Web.UI.WebControls.Label sts_cd = (System.Web.UI.WebControls.Label)e.Row.FindControl("app_sts");
        System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("rbtnSelect2");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (sts.Text != "")
            {
                
                if (sts.Text == "TIDAK SAH")
                {
                    //sts.ForeColor = Color.FromName("Red");
                    sts.Attributes.Add("Class", "label label-danger Uppercase");
                }
                else if (sts.Text == "SAH")
                {
                    //sts.ForeColor = Color.FromName("Green");
                    sts.Attributes.Add("Class", "label label-warning Uppercase");

                }
                else if (sts.Text == "PENDING")
                {                    
                    sts.Attributes.Add("Class", "label label-info Uppercase");

                }
            }
            else
            {
                sts.ForeColor = Color.FromName("Red");
            }
            if(sts_cd.Text == "01" || sts_cd.Text == "02")
            {
                cb.Enabled = false;
            }
            else
            {
                cb.Enabled = true;
               

            }
        }
    }
    protected void lnkView_Click1(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label staffno = (System.Web.UI.WebControls.Label)gvRow.FindControl("est_no");
            System.Web.UI.WebControls.Label eallcd = (System.Web.UI.WebControls.Label)gvRow.FindControl("eall_cd");
            System.Web.UI.WebControls.Label rec_dt = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label2");
            DateTime Edt2 = DateTime.ParseExact(rec_dt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string stno = staffno.Text;
            string eacd = eallcd.Text;


            DataTable ann_leave_det = new DataTable();
            ann_leave_det = DBCon.Ora_Execute_table("select clm_staff_no,FORMAT(clm_rec_dt,'dd/MM/yyyy', 'en-us') as rec_dt,FORMAT(clm_app_dt,'dd/MM/yyyy', 'en-us') as app_dt,clm_claim_cd,clm_claim_amt,file_name,clm_app_sts,clm_balance_amt,clm_sebap,clm_approve_sts_cd from hr_claim_new where clm_staff_no='" + stno + "' and clm_claim_cd='" + eacd + "' and clm_rec_dt='"+ Edt2.ToString("yyyy-MM-dd") + "'");
            if (ann_leave_det.Rows.Count != 0)
            {
                //ET_jelaun.Attributes.Add("Disabled", "Disabled");
                ET_mdate.Text = ann_leave_det.Rows[0]["rec_dt"].ToString().Trim();
                ET_sdate.Text = ann_leave_det.Rows[0]["app_dt"].ToString().Trim();
                TextBox4.Text = ann_leave_det.Rows[0]["clm_sebap"].ToString();
                decimal at1 = decimal.Parse(ann_leave_det.Rows[0]["clm_claim_amt"].ToString());
                ET_amaun.Text = at1.ToString("C").Replace("$", "").Replace("RM", "");
                if (ann_leave_det.Rows[0]["clm_balance_amt"].ToString() != "")
                {
                    TextBox2.Text = double.Parse(ann_leave_det.Rows[0]["clm_balance_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                }
                else
                {
                    TextBox2.Text = "0.00";
                }
                ET_jelaun.SelectedValue = ann_leave_det.Rows[0]["clm_claim_cd"].ToString().Trim();
                TextBox1.Text = ann_leave_det.Rows[0]["file_name"].ToString();
                if ( ann_leave_det.Rows[0]["clm_approve_sts_cd"].ToString() == "01")
                {
                    Button2.Visible = false;
                    Button4.Visible = false;
                }
                else if (ann_leave_det.Rows[0]["clm_approve_sts_cd"].ToString() == "02")
                {
                    Button2.Visible = false;
                    Button4.Visible = false;
                }
                else if (ann_leave_det.Rows[0]["clm_approve_sts_cd"].ToString() == "")
                {
                    Button4.Visible = true;
                    Button2.Visible = true;
                    Button2.Text = "Kemaskini";
                }                
                ET_rno.Text = "1";
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        grid();
        //bind();
        grid_2();
    }
    void grid_2()
    {
        SqlCommand cmd2 = new SqlCommand("select hfa.clm_staff_no,hfa.clm_claim_cd,hfa.clm_rec_dt,hfa.clm_app_dt,je.hr_tun_desc,hfa.clm_claim_amt,FORMAT(hfa.clm_rec_dt,'yyyy-MM-dd') as rec_dt,ISNULL(hfa.clm_approve_sts_cd,'') clm_approve_sts_cd,case when ISNULL(hfa.clm_approve_sts_cd,'') = '' then 'PENDING' when ISNULL(hfa.clm_approve_sts_cd,'') = '01' then 'SAH' when ISNULL(hfa.clm_approve_sts_cd,'') ='02' then 'TIDAK SAH' end as sts_desc,clm_balance_amt,clm_sebap from hr_claim_new as hfa left join hr_staff_profile as hsp on hsp.stf_staff_no=hfa.clm_staff_no left join Ref_hr_tuntutan as je on je.hr_tun_Code=hfa.clm_claim_cd where hfa.clm_staff_no='" + Applcn_no1.Text + "' ORDER BY hfa.clm_rec_dt DESC", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView2.DataSource = ds2;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<strong><center>Maklumat Carian Tidak Dijumpai</center></strong>";
        }
        else
        {
            GridView2.DataSource = ds2;
            GridView2.DataBind();
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_PAYSLIP.aspx");
    }

    
    protected void dd_sel_txtchanged(object sender, EventArgs e)
    {
        if (ET_jelaun.SelectedValue != "")
        {
            decimal myDec;
            var Result = decimal.TryParse(ET_amaun.Text, out myDec);
            if (Result == true)
            {
                DataTable get_calc = new DataTable();
                get_calc = DBCon.Ora_Execute_table("select * from Ref_hr_tuntutan where hr_tun_Code='" + ET_jelaun.SelectedValue + "' and Status='A'");

                if (ET_jelaun.SelectedValue == "02")
                {
                    if (double.Parse(TextBox2.Text) >= double.Parse(ET_amaun.Text))
                    {
                        ET_amaun.Text = double.Parse(ET_amaun.Text).ToString("C").Replace("$", "").Replace("RM", "");
                    }
                    else
                    {
                        ET_amaun.Text = double.Parse(TextBox2.Text).ToString("C").Replace("$", "").Replace("RM", "");
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Enter valid Claim Amount.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
            }
            else
            {
                ET_amaun.Text = "0.00";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Enter valid Claim Amount.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
           
        }
        bind();
        grid();
        grid_2();
    }

    protected void dd_sel_txtchanged1(object sender, EventArgs e)
    {
        if (ET_jelaun.SelectedValue != "")
        {
            ET_amaun.Text = "";
            string ref_amt = string.Empty;
            DataTable get_calc_ref = new DataTable();
            get_calc_ref = DBCon.Ora_Execute_table("select * from Ref_hr_tuntutan where hr_tun_Code='" + ET_jelaun.SelectedValue + "' and Status='A'");
            if(get_calc_ref.Rows.Count != 0)
            {
                ref_amt = get_calc_ref.Rows[0]["amt"].ToString();
            }
            else
            {
                ref_amt = "0.00";
            }
            DataTable get_calc_main = new DataTable();
            get_calc_main = DBCon.Ora_Execute_table("select ISNULL(sum(clm_claim_amt),'0.00') as camt from hr_claim_new where clm_staff_no='" + Applcn_no1.Text + "' and clm_claim_cd='" + ET_jelaun.SelectedValue + "' AND year(clm_app_dt)='"+ DateTime.Now.Year +"' and clm_app_sts='A'");

            if (ET_jelaun.SelectedValue == "02")
            {
               Label7.InnerText = "Baki Terkini (RM)";
               TextBox2.Text = (double.Parse(ref_amt) - double.Parse(get_calc_main.Rows[0]["camt"].ToString())).ToString("C").Replace("$", "").Replace("RM", "");
               

            }
            else
            {
                Label7.InnerText = "Jumlah Terkini (RM)";
                TextBox2.Text = (double.Parse(get_calc_main.Rows[0]["camt"].ToString())).ToString("C").Replace("$", "").Replace("RM", "");
            }

      
               

                
        }
        bind();
        grid();
        grid_2();

    }
    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_PAYSLIP_view.aspx");
    }


}