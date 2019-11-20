using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Threading;
using System.Net;
using System.Net.Mail;

public partial class pengasahan_tuntutan : System.Web.UI.Page
{
    DBConnection dbcon = new DBConnection();
    Mailcoms ObjMail = new Mailcoms();
    SMS ObjSms = new SMS();
    CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
    DBConnection Dblog = new DBConnection();
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string level, userid;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty, Status2 = string.Empty, Status3 = string.Empty, sqry = string.Empty;
    String count_text;
    int incNumber = 1;
    int incNumber1 = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                Month();
                Year();
                userid = Session["New"].ToString();
                grid();
                //TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                Response.Redirect("~/KSAIMB_Login.aspx");
            }
        }
    }


    void Month()
    {
        DateTimeFormatInfo info1 = DateTimeFormatInfo.GetInstance(null);
        int Month = DateTime.Now.Month - 4;
        for (int X = Month; X <= DateTime.Now.Month; X++)
        {
            DD_bulancaruman.Items.Add(new ListItem(X.ToString("#0"), X.ToString("#0")));
        }
        string abc = DateTime.Now.Month.ToString("#0");
        //string abc = DD_bulancaruman.SelectedValue;

        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_month_Code,hr_month_desc from Ref_hr_month ORDER BY hr_month_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_bulancaruman.DataSource = dt;
            DD_bulancaruman.DataBind();
            DD_bulancaruman.DataTextField = "hr_month_desc";
            DD_bulancaruman.DataValueField = "hr_month_Code";
            DD_bulancaruman.DataBind();
            DD_bulancaruman.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        DD_bulancaruman.SelectedValue = abc.PadLeft(2, '0');
    }

    private void Year()
    {

        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
        int year = DateTime.Now.Year - 5;
        for (int Y = year; Y <= DateTime.Now.Year; Y++)
        {
            txt_tahun.Items.Add(new ListItem(Y.ToString(), Y.ToString()));
        }
        txt_tahun.SelectedValue = DateTime.Now.Year.ToString();

    }
    protected void BindGridview(object sender, EventArgs e)
    {
        if (txt_tahun.SelectedValue != "")
        {
            //show_cnt1.Visible = true;
            grid();
        }
        else
        {
            grid();
            //show_cnt1.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tahun.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    protected void lnkView_Click11(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl1");
        string lblid = lblTitle.Text;
        string filePath = Server.MapPath("~/FILES/EFT/" + lblid.Trim());
        Response.ContentType = ContentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(filePath) + "\"");
        Response.WriteFile(filePath);
        Response.End();
    }

    void get_det()
    {
        string schk = string.Empty;
      
        if (txt_tahun.SelectedValue != "" && DD_bulancaruman.SelectedValue != "" && DropDownList1.SelectedValue != "")
        {
            sqry = " where ISNULL(clm_approve_sts_cd,'')='" + DropDownList1.SelectedValue + "' and Month(clm_rec_dt)='" + DD_bulancaruman.SelectedValue + "' and year(clm_rec_dt)='" + txt_tahun.SelectedValue + "'";
        }
        else if (txt_tahun.SelectedValue != "" && DD_bulancaruman.SelectedValue != "" && DropDownList1.SelectedValue == "")
        {
            sqry = " where Month(clm_rec_dt)='" + DD_bulancaruman.SelectedValue + "' and year(clm_rec_dt)='" + txt_tahun.SelectedValue + "'";
        }
        else if (txt_tahun.SelectedValue != "" && DD_bulancaruman.SelectedValue == "" && DropDownList1.SelectedValue != "")
        {
            sqry = "where ISNULL(clm_approve_sts_cd,'')='" + DropDownList1.SelectedValue + "' and year(clm_rec_dt)='" + txt_tahun.SelectedValue + "'";
        }
        else
        {
            sqry = "where ISNULL(clm_approve_sts_cd,'')='' and Month(clm_rec_dt)='' and year(clm_rec_dt)=''";
        }
    }
    protected void grid()
    {

        get_det();
        SqlConnection con = new SqlConnection(conString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select stf_staff_no,stf_kod_akaun,stf_name,stf_icno,clm_rec_dt,format(clm_rec_dt,'dd/MM/yyyy') clm_rec_dt1,ht.hr_tun_desc,clm_claim_amt,format(clm_app_dt,'dd/MM/yyyy') clm_app_dt1,ISNULL(clm_app_sts,'') as sts,clm_claim_cd,file_name,clm_jurnal_no,case when ISNULL(clm_app_sts,'') = '' then 'PENDING' when ISNULL(clm_app_sts,'') = '01' then 'SAH' when ISNULL(clm_app_sts,'')='02' then 'TIDAK SAH' end as sts_desc,case when ISNULL(clm_approve_sts_cd,'') = '' then 'PENDING' when ISNULL(clm_approve_sts_cd,'') = '01' then 'SAH' when ISNULL(clm_approve_sts_cd,'')='02' then 'TIDAK SAH' end as sts_desc1,clm_balance_amt,clm_sebap,case when clm_app_sts ='P' then '01' when clm_app_sts ='R' then '02' when clm_app_sts ='A' then '03' end as sno from hr_claim_new left join hr_staff_profile sp on sp.stf_staff_no=clm_staff_no left join Ref_hr_tuntutan ht on ht.hr_tun_Code=clm_claim_cd " + sqry + " order by sno,clm_app_dt desc", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {

            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            int columncount = gvSelected.Rows[0].Cells.Count;
            gvSelected.Rows[0].Cells.Clear();
            gvSelected.Rows[0].Cells.Add(new TableCell());
            gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
            gvSelected.Rows[0].Cells[0].Text = "<center><strong>Rekod Tidak Dijumpai. Sila Lakukan Semula Carian</strong></center>";
            Button3.Visible = false;
            Button4.Visible = false;
        }
        else
        {
           
            gvSelected.DataSource = ds;
            gvSelected.DataBind();

        }
        con.Close();
    }

    protected void OnCheckedChanged(object sender, EventArgs e)
    {
        bool isUpdateVisible = false;
        CheckBox chk = (sender as CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in gvSelected.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[9].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        CheckBox chkAll = (gvSelected.HeaderRow.FindControl("chkAll") as CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in gvSelected.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[9].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    if (isChecked && !isUpdateVisible)
                    {
                        isUpdateVisible = true;
                    }
                    if (!isChecked)
                    {
                        chkAll.Checked = false;

                    }
                }
            }
        }

    }
    protected void ctk_values(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count1 = 0;
        get_det();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt = dbcon.Ora_Execute_table("select stf_staff_no,stf_name,stf_icno,clm_rec_dt,format(clm_rec_dt,'dd/MM/yyyy') clm_rec_dt1,ht.hr_tun_desc,clm_claim_amt,format(clm_rec_dt,'dd/MM/yyyy') clm_app_dt1,ISNULL(clm_app_sts,'') as sts,clm_claim_cd,file_name,case when ISNULL(clm_app_sts,'') = '' then 'PENDING' when ISNULL(clm_app_sts,'') = '01' then 'SAH' when ISNULL(clm_app_sts,'')='02' then 'TIDAK SAH' end as sts_desc,case when ISNULL(clm_approve_sts_cd,'') = '' then 'PENDING' when ISNULL(clm_approve_sts_cd,'') = '01' then 'SAH' when ISNULL(clm_approve_sts_cd,'')='02' then 'TIDAK SAH' end as sts_desc1,clm_balance_amt,clm_sebap from hr_claim_new left join hr_staff_profile sp on sp.stf_staff_no=clm_staff_no left join Ref_hr_tuntutan ht on ht.hr_tun_Code=clm_claim_cd " + sqry + "");
        RptviwerStudent.Reset();
        ds.Tables.Add(dt);

        List<DataRow> listResult = dt.AsEnumerable().ToList();
        listResult.Count();
        int countRow = 0;
        countRow = listResult.Count();

        RptviwerStudent.LocalReport.DataSources.Clear();
        if (countRow != 0)
        {
            RptviwerStudent.LocalReport.ReportPath = "SUMBER_MANUSIA/pen_tuntutan.rdlc";
            ReportDataSource rds = new ReportDataSource("hrpentut", dt);
            ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1",txt_tahun.SelectedItem.Text ),
                     new ReportParameter("s2",DD_bulancaruman.SelectedItem.Text )

                     };
            RptviwerStudent.LocalReport.SetParameters(rptParams);
            RptviwerStudent.LocalReport.DataSources.Add(rds);
            RptviwerStudent.LocalReport.Refresh();

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string filename;

            if (sel_frmt.SelectedValue == "01")
            {
                filename = string.Format("{0}.{1}", "STATUS_TUNTUTAN_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                byte[] bytes = RptviwerStudent.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            else if (sel_frmt.SelectedValue == "02")
            {
                System.Text.StringBuilder builder = new StringBuilder();
                string strFileName = string.Format("{0}.{1}", "STATUS_TUNTUTAN_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                builder.Append("No Kakitangan ,Ic No,Nama Kakitangan, Reciept Date, Apply Date, Jenis Tuntutan, clm_sebap, Amaun (RM), Baki / Jumlah Terkini (RM), Status Kelulusan, Status Pengesahan" + Environment.NewLine);
                foreach (GridViewRow row in gvSelected.Rows)
                {
                    string stfno = ((Label)row.FindControl("Label2")).Text.ToString();
                    string icno = ((Label)row.FindControl("Label3")).Text.ToString();
                    string name = ((Label)row.FindControl("Label2_name")).Text.ToString();
                    string rec_dt = ((Label)row.FindControl("Label2_yr")).Text.ToString();
                    string app_dt = ((Label)row.FindControl("Label4")).Text.ToString();
                    string jenis = ((Label)row.FindControl("Label5")).Text.ToString();
                    string amt = ((Label)row.FindControl("Label6_amt")).Text.ToString();
                    string amt1 = ((Label)row.FindControl("Label6_amt1")).Text.ToString();
                    string sts = ((Label)row.FindControl("Label5_sts")).Text.ToString();
                    string sts1 = ((Label)row.FindControl("Label5_sts1")).Text.ToString();
                    string sebab = ((Label)row.FindControl("Label4_seb")).Text.ToString();

                    builder.Append(stfno + "," + icno + "," + name + "," + rec_dt + "," + app_dt + "," + jenis + ","+ sebab + "," + amt.Replace(",","") + "," + amt1.Replace(",", "") + "," + sts + "," + sts1 + Environment.NewLine);
                }
                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                Response.Write(builder.ToString());
                Response.End();
            }
            //else if (sel_frmt.SelectedValue == "03")
            //{
            //    byte[] bytes = RptviwerStudent.LocalReport.Render("Word", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            //    filename = string.Format("{0}.{1}", "PENDAFTARAN_REKOD_" + DateTime.Now.ToString("ddMMyyyy") + "", "doc");
            //    Response.Buffer = true;
            //    Response.Clear();
            //    Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            //    Response.ContentType = mimeType;
            //    Response.BinaryWrite(bytes);
            //    Response.Flush();
            //    Response.End();
            //}
        }
        else if (countRow == 0)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

        grid();

    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();        
    }

    protected void submit_button(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        string date1 = string.Empty, year1 = string.Empty, ruj1 = string.Empty, ruj2 = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in gvSelected.Rows)
        {
            var checkbox = gvrow.FindControl("chkSelect") as CheckBox;
            if (checkbox.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                var checkbox = gvrow.FindControl("chkSelect") as CheckBox;
                if (checkbox.Checked == true)
                {
                    string etdate1 = string.Empty;
                    var nokak = gvrow.FindControl("Label2") as Label;
                    var mnth = gvrow.FindControl("Label2_yr") as Label;
                    var app_date = gvrow.FindControl("Label4") as Label;
                    var year = gvrow.FindControl("ss_val2") as System.Web.UI.WebControls.Label;
                    var org_id = gvrow.FindControl("Label1_org_id") as System.Web.UI.WebControls.Label;
                    var kod_akaun = gvrow.FindControl("kod_akan") as System.Web.UI.WebControls.Label;
                    var clm_amt = gvrow.FindControl("Label6_amt") as System.Web.UI.WebControls.Label;
                    var name = gvrow.FindControl("Label2_name") as System.Web.UI.WebControls.Label;
                    var desc = gvrow.FindControl("Label5") as System.Web.UI.WebControls.Label;
                    DateTime Edt1 = DateTime.ParseExact(mnth.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    etdate1 = Edt1.ToString("yyyy-MM-dd");
                    DataTable chk_income = new DataTable();
                    chk_income = DBCon.Ora_Execute_table("select * from hr_claim_new where clm_staff_no='" + nokak.Text + "' and clm_claim_cd='" + org_id.Text + "' and clm_rec_dt='" + etdate1 + "' and ISNULL(clm_app_sts,'')='P'");
                    if (chk_income.Rows.Count != 0)
                    {
                        string str = desc.Text;
                        string[] output = str.Split(' ');
                        foreach (string s in output)
                        {
                            ruj2 += s[0];
                        }


                        DataTable get_jn_inv = new DataTable();
                        get_jn_inv = DBCon.Ora_Execute_table("select top(1) clm_jurnal_no,(RIGHT(clm_jurnal_no,1) + 1) invno from hr_claim_new where clm_jurnal_no like '%KTH-HR-" + ruj2 + "-" + DateTime.Now.ToString("MMM") + "" + DateTime.Now.ToString("yyyy") + "%' group by clm_jurnal_no order by clm_jurnal_no desc");
                        if (get_jn_inv.Rows.Count == 0)
                        {
                            ruj1 = "KTH-HR-" + ruj2 + "-" + DateTime.Now.ToString("MMM") + "" + DateTime.Now.ToString("yyyy") + "-1";
                            //ruj2 = "KTH-HR-" + firstChars + "-" + date1 + "" + year1 + "-1";
                        }
                        else
                        {
                            ruj1 = "KTH-HR-" + ruj2 + "-" + DateTime.Now.ToString("MMM") + "" + DateTime.Now.ToString("yyyy") + "-" + get_jn_inv.Rows[0]["invno"].ToString();
                            //ruj2 = "KTH-HR-" + firstChars + "-" + date1 + "" + year1 + "-" + get_jn_inv.Rows[0]["invno"].ToString();
                        }

                        string Inssql1_bon = "UPDATE hr_claim_new set clm_jurnal_no='"+ ruj1 + "',clm_app_sts='A',clm_upd_id='" + Session["New"].ToString() + "',clm_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where clm_staff_no='" + nokak.Text + "' and clm_claim_cd='" + org_id.Text + "' and clm_rec_dt='" + etdate1 + "' and ISNULL(clm_app_sts,'')='P'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1_bon);

                        if(Status == "SUCCESS")
                        {
                            //debit 
                           
                            string Inssql_debit = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('" + kod_akaun.Text + "','" + clm_amt.Text + "','0.00','12','"+ ruj1 + "','" + name.Text + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+ DateTime.Now.ToString("MMM") + DateTime.Now.ToString("yyyy")+ " - TUNTUTAN KAKITANGAN - " + desc.Text + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status2 = DBCon.Ora_Execute_CommamdText(Inssql_debit);

                            //kredit

                            string Inssql_kredit = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('04.26','0.00','" + clm_amt.Text + "','04','" + ruj1 + "','" + name.Text + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("MMM") + DateTime.Now.ToString("yyyy") + " - TUNTUTAN KAKITANGAN - AKAUN DI BAYAR','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status3 = DBCon.Ora_Execute_CommamdText(Inssql_kredit);
                        }

                    }
                }
            }
            if (Status == "SUCCESS")
            {
                //send_email();
                grid();
                service.audit_trail("P0205", "Pengesahan Tuntutan - Simpan");
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dipilih.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    void send_email()
    {
        TextInfo txtinfo = culinfo.TextInfo;
        DataTable Ds = new DataTable();
        DataTable Ds1 = new DataTable();
        DataTable email_settings = new DataTable();
        string Mailmsg = string.Empty, Mailmsg1 = string.Empty, Mail_id1 = string.Empty;
        try
        {
            Ds = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid='admin'");
            email_settings = DBCon.Ora_Execute_table("select config_email_head,config_email_host,config_email_id,config_email_port,config_email_pwd,config_email_url,config_email_web from site_settings where ID='1'");
            if (Ds.Rows[0]["KK_email"].ToString() != "")
            {
                Mail_id1 = Ds.Rows[0]["KK_email"].ToString();
            }
            var fromemail = new MailAddress(email_settings.Rows[0]["config_email_id"].ToString(), email_settings.Rows[0]["config_email_head"].ToString());
            var fromemailpassword = email_settings.Rows[0]["config_email_pwd"].ToString();
            string subject = email_settings.Rows[0]["config_email_web"].ToString() + " - Pengesahan Tuntutan";
            var verifyurl = email_settings.Rows[0]["config_email_url"].ToString();
            var link = verifyurl;

            if (Mail_id1 != "")
            {
                var toemail = new MailAddress(Mail_id1);
                string body = "Hello " + txtinfo.ToTitleCase(Ds.Rows[0]["Kk_username"].ToString().ToLower()) + ",<br/><br/>Tuntutan has been Approved on " + DD_bulancaruman.SelectedItem.Text + " " + txt_tahun.SelectedValue + " <br/><br/> Thank You,<br/><a><html><body><a href='"+ link + "'> "+ email_settings.Rows[0]["config_email_web"].ToString() + " </a></body></html> . </a>";

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

        }
        catch (Exception ex)
        {
            service.LogError(ex.ToString());
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           

            System.Web.UI.WebControls.Label stf_no = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label2");
            System.Web.UI.WebControls.Label rec_dt = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label2_yr");
            System.Web.UI.WebControls.Label clm_cd = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label1_org_id");
            string sdt = string.Empty;
            if (rec_dt.Text != "")
            {
                DateTime today1 = DateTime.ParseExact(rec_dt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                sdt = today1.ToString("yyyy-MM-dd");
            }
            GridView gvOrders = e.Row.FindControl("gvProducts") as GridView;
            gvOrders.DataSource = GetData("select Id, td1_Name from tblFiles2  where td1_staff_no='" + stf_no.Text + "' and td1_claim_dt='" + clm_cd.Text + "' and td1_rec_dt='" + sdt + "'");
            gvOrders.DataBind();
        }
    }
    private static DataTable GetData(string query)
    {
        string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect("../SUMBER_MANUSIA/pengasahan_tuntutan.aspx");
    }
}