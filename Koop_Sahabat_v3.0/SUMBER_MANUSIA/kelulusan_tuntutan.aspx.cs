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
using System.Threading;
using System.Net;
using System.Net.Mail;
public partial class kelulusan_tuntutan : System.Web.UI.Page
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
                Response.Redirect("../KSAIMB_Login.aspx");
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

    protected void gvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void grid()
    {
        string schk = string.Empty;
        DataTable ddokdicno = new DataTable();
        string ssno = string.Empty;
        //ddokdicno = DBCon.Ora_Execute_table("select STUFF ((select ',''' + hr_tun_Code + '''' from Ref_hr_tuntutan where '" + Session["New"].ToString() + "' IN (tun_spv_name1)  FOR XML PATH ('')  ),1,1,'')  as scode");
        ddokdicno = DBCon.Ora_Execute_table("select STUFF ((select ',''' + pos_staff_no + '''' from hr_post_his where pos_end_dt='9999-12-31' and '" + Session["New"].ToString() + "' IN (pos_spv_name1,pos_spv_name2)  FOR XML PATH ('')  ),1,1,'')  as scode");

        if (ddokdicno.Rows[0]["scode"].ToString() != "")
        {
            ssno = ddokdicno.Rows[0]["scode"].ToString();
        }
        else
        {
            ssno = "''";
        }
        if (chk_assign_rkd.Checked == true)
        {
            schk = " and ISNULL(clm_approve_sts_cd,'')='01'";
            Button3.Visible = false;
            Button6.Visible = false;
        }
        else
        {
            schk = " and ISNULL(clm_approve_sts_cd,'')=''";
            Button3.Visible = true;
            Button6.Visible = true;
        }

        if (txt_tahun.SelectedValue != "" && DD_bulancaruman.SelectedValue != "")
        {
            sqry = " where clm_staff_no IN (" + ssno +") and Month(clm_rec_dt)='" + DD_bulancaruman.SelectedValue + "' and year(clm_rec_dt)='" + txt_tahun.SelectedValue + "' "+ schk + "";
        }
        else if (txt_tahun.SelectedValue != "" && DD_bulancaruman.SelectedValue == "")
        {
            sqry = "where clm_staff_no IN (" + ssno + ") and year(clm_rec_dt)='" + txt_tahun.SelectedValue + "' " + schk + "";
        }
        else
        {
            sqry = "where clm_staff_no IN (" + ssno + ") and Month(clm_rec_dt)='' and year(clm_rec_dt)='' " + schk + "";
        }

        SqlConnection con = new SqlConnection(conString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select stf_staff_no,stf_kod_akaun,stf_name,stf_icno,clm_rec_dt,format(clm_rec_dt,'dd/MM/yyyy') clm_rec_dt1,ht.hr_tun_desc,clm_claim_amt,format(clm_rec_dt,'dd/MM/yyyy') clm_app_dt1,ISNULL(clm_approve_sts_cd,'') as sts,clm_claim_cd,file_name,clm_balance_amt,clm_sebap from hr_claim_new left join hr_staff_profile sp on sp.stf_staff_no=clm_staff_no left join Ref_hr_tuntutan ht on ht.hr_tun_Code=clm_claim_cd " + sqry +"", con);
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
            Button6.Visible = false;
        }
        else
        {            
            gvSelected.DataSource = ds;
            gvSelected.DataBind();

        }
        con.Close();
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            string recdt = string.Empty;
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label stf_no = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label2");
            System.Web.UI.WebControls.Label rec_dt = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label2_yr");
            System.Web.UI.WebControls.Label clm_cd = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label1_org_id");
            System.Web.UI.WebControls.Label clm_amt = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label6_amt");
            DateTime Edt1 = DateTime.ParseExact(rec_dt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            recdt = Edt1.ToString("yyyy-MM-dd");
            DataTable chk_clm = new DataTable();
            chk_clm = DBCon.Ora_Execute_table("select * from hr_claim_new where clm_staff_no='" + stf_no.Text + "' and clm_claim_cd='" + clm_cd.Text + "' and clm_rec_dt='" + recdt + "' and ISNULL(clm_approve_sts_cd,'') = '' ");
            if (chk_clm.Rows.Count != 0)
            {
                TextBox1.Text = stf_no.Text;
                TextBox2.Text = recdt;
                TextBox3.Text = clm_cd.Text;
                TextBox4.Text = clm_amt.Text;
                ModalPopupExtender1.Show();
            }
          
        }
        catch
        {

        }
    }

    protected void clk_insert_clm(object sender, EventArgs e)
    {
        try
        {
           
            DataTable chk_clm = new DataTable();
            chk_clm = DBCon.Ora_Execute_table("select * from hr_claim_new where clm_staff_no='" + TextBox1.Text + "' and clm_claim_cd='" + TextBox3.Text + "' and clm_rec_dt='" + TextBox2.Text + "' and ISNULL(clm_approve_sts_cd,'') = '' ");
            if (chk_clm.Rows.Count != 0)
            {
                string Inssql = "Update hr_claim_new set clm_approve_sts_cd='"+ dd_sts.SelectedValue + "',clm_approve_remark='"+ catatan.Text.Replace("'","''") + "',clm_approve_id='"+ Session["New"].ToString() + "',clm_approve_dt='"+ DateTime.Now.ToString("yyyy-MM-dd") +"' where clm_staff_no='" + TextBox1.Text + "' and clm_claim_cd='" + TextBox3.Text + "' and clm_rec_dt='" + TextBox2.Text + "' and ISNULL(clm_approve_sts_cd,'') = ''";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if(Status == "SUCCESS")
                {
                    Session["nokak"] = TextBox1.Text;
                    Session["rec_dt"] = TextBox2.Text;
                    Session["rec_amt"] = TextBox4.Text;
                    Session["sts_cd"] = dd_sts.SelectedItem.Text;
                    //send_email();
                    dd_sts.SelectedValue = "";
                    catatan.Text = "";
                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }

        }
        catch
        {

        }
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
                    row.Cells[10].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        CheckBox chkAll = (gvSelected.HeaderRow.FindControl("chkAll") as CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in gvSelected.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[10].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
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
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();        
    }

    protected void submit_button(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        string date1 = string.Empty, year1 = string.Empty, ruj1 = string.Empty, ruj2 = string.Empty;
        foreach (GridViewRow gvrow in gvSelected.Rows)
        {
            //var checkbox = gvrow.FindControl("chkSelect") as CheckBox;
            //if (checkbox.Checked)
            //{
                count++;
            //}
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                var checkbox = gvrow.FindControl("chkSelect") as CheckBox;

                string etdate1 = string.Empty;
                var nokak = gvrow.FindControl("Label2") as Label;
                var mnth = gvrow.FindControl("Label2_yr") as Label;
                var app_date = gvrow.FindControl("Label4") as Label;
                var year = gvrow.FindControl("ss_val2") as System.Web.UI.WebControls.Label;
                var org_id = gvrow.FindControl("Label1_org_id") as System.Web.UI.WebControls.Label;
                var kod_akaun = gvrow.FindControl("kod_akan") as System.Web.UI.WebControls.Label;
                DateTime Edt1 = DateTime.ParseExact(mnth.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                etdate1 = Edt1.ToString("yyyy-MM-dd");
                var recamt = gvrow.FindControl("Label6_amt") as Label;
                var name = gvrow.FindControl("Label2_name") as System.Web.UI.WebControls.Label;
                var desc = gvrow.FindControl("Label5") as System.Web.UI.WebControls.Label;
                DataTable chk_income = new DataTable();
                Session["nokak"] = nokak.Text;
                Session["rec_dt"] = Edt1.ToString("MM");
                Session["rec_amt"] = recamt.Text;
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

                    if (checkbox.Checked == true)
                    {
                        string Inssql1_bon = "UPDATE hr_claim_new set clm_jurnal_no='" + ruj1 + "',clm_app_sts='A',clm_upd_id='" + Session["New"].ToString() + "',clm_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where clm_staff_no='" + nokak.Text + "' and clm_claim_cd='" + org_id.Text + "' and clm_rec_dt='" + etdate1 + "' and ISNULL(clm_app_sts,'')='P'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1_bon);

                        if(Status == "SUCCESS")
                        {
                            // upd claim balance
                            if (org_id.Text == "02")
                            {
                                DataTable sel_upd_claim = new DataTable();
                                sel_upd_claim = DBCon.Ora_Execute_table("select *,FORMAT(clm_rec_dt,'yyyy-MM-dd') as rec_dt from hr_claim_new where clm_app_sts ='P' and clm_claim_cd='02'");

                                for (int i = 0; i < sel_upd_claim.Rows.Count; i++)
                                {
                                    string samt1 = (double.Parse(sel_upd_claim.Rows[i]["clm_balance_amt"].ToString()) - double.Parse(recamt.Text)).ToString("C").Replace("$", "").Replace("RM", "");
                                    string samt2 = (double.Parse(sel_upd_claim.Rows[i]["clm_cur_balance_amt"].ToString()) - double.Parse(recamt.Text)).ToString("C").Replace("$", "").Replace("RM", "");
                                    string upd_clm = "UPDATE hr_claim_new set clm_balance_amt='" + samt1 + "',clm_cur_balance_amt='" + samt2 + "',clm_upd_id='" + Session["New"].ToString() + "',clm_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where clm_staff_no='" + nokak.Text + "' and clm_claim_cd='" + org_id.Text + "' and clm_rec_dt='" + sel_upd_claim.Rows[i]["rec_dt"].ToString() + "' and ISNULL(clm_app_sts,'')='P'";
                                    Status = DBCon.Ora_Execute_CommamdText(upd_clm);
                                }

                            }

                            //debit 

                            string Inssql_debit = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('" + kod_akaun.Text + "','" + recamt.Text + "','0.00','12','" + ruj1 + "','" + name.Text + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("MMM") + DateTime.Now.ToString("yyyy") + " - TUNTUTAN KAKITANGAN - " + desc.Text + "','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status2 = DBCon.Ora_Execute_CommamdText(Inssql_debit);

                            //kredit

                            string Inssql_kredit = "INSERT INTO KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,Tax_type,Tot_Amt,GL_process_dt,GL_desc1,GL_desc2,GL_nama_kod,crt_id,cr_dt,GL_sts) VALUES ('04.26','0.00','" + recamt.Text + "','04','" + ruj1 + "','" + name.Text + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("MMM") + DateTime.Now.ToString("yyyy") + " - TUNTUTAN KAKITANGAN - AKAUN DI BAYAR','','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status3 = DBCon.Ora_Execute_CommamdText(Inssql_kredit);

                            Session["sts_cd"] = "Approved";
                          send_email();
                        }

                    }
                    //else
                    //{
                    //    string Inssql1_bon = "UPDATE hr_claim_new set clm_app_sts='R',clm_upd_id='" + Session["New"].ToString() + "',clm_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where clm_staff_no='" + nokak.Text + "' and clm_claim_cd='" + org_id.Text + "' and clm_rec_dt='" + etdate1 + "' and ISNULL(clm_app_sts,'')='P'";
                    //    Status = DBCon.Ora_Execute_CommamdText(Inssql1_bon);
                    //    if (Status == "SUCCESS")
                    //    {
                            

                    //        Session["sts_cd"] = "Rejected";
                    //        send_email();
                    //    }
                    //}

                }
            }
        
            if (Status == "SUCCESS")
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dipilih.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }


    protected void submit_button_tl(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        string date1 = string.Empty, year1 = string.Empty, ruj1 = string.Empty, ruj2 = string.Empty;
        foreach (GridViewRow gvrow in gvSelected.Rows)
        {
            count++;
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                var checkbox = gvrow.FindControl("chkSelect") as CheckBox;

                string etdate1 = string.Empty;
                var nokak = gvrow.FindControl("Label2") as Label;
                var mnth = gvrow.FindControl("Label2_yr") as Label;
                var app_date = gvrow.FindControl("Label4") as Label;
                var year = gvrow.FindControl("ss_val2") as System.Web.UI.WebControls.Label;
                var org_id = gvrow.FindControl("Label1_org_id") as System.Web.UI.WebControls.Label;
                var kod_akaun = gvrow.FindControl("kod_akan") as System.Web.UI.WebControls.Label;
                DateTime Edt1 = DateTime.ParseExact(mnth.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                etdate1 = Edt1.ToString("yyyy-MM-dd");
                var recamt = gvrow.FindControl("Label6_amt") as Label;
                var name = gvrow.FindControl("Label2_name") as System.Web.UI.WebControls.Label;
                var desc = gvrow.FindControl("Label5") as System.Web.UI.WebControls.Label;
                DataTable chk_income = new DataTable();
                Session["nokak"] = nokak.Text;
                Session["rec_dt"] = Edt1.ToString("MM");
                Session["rec_amt"] = recamt.Text;
                chk_income = DBCon.Ora_Execute_table("select * from hr_claim_new where clm_staff_no='" + nokak.Text + "' and clm_claim_cd='" + org_id.Text + "' and clm_rec_dt='" + etdate1 + "' and ISNULL(clm_app_sts,'')='P'");
                if (chk_income.Rows.Count != 0)
                {
                    if (checkbox.Checked == true)
                    {
                        string Inssql1_bon = "UPDATE hr_claim_new set clm_app_sts='R',clm_upd_id='" + Session["New"].ToString() + "',clm_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where clm_staff_no='" + nokak.Text + "' and clm_claim_cd='" + org_id.Text + "' and clm_rec_dt='" + etdate1 + "' and ISNULL(clm_app_sts,'')='P'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1_bon);
                        if (Status == "SUCCESS")
                        {
                            Session["sts_cd"] = "Rejected";
                            send_email();
                        }

                    }
                   
                }
            }

            if (Status == "SUCCESS")
            {
                grid();
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
        DataTable Ds2 = new DataTable();
        DataTable Ds3 = new DataTable();
        string Mailmsg = string.Empty, Mailmsg1 = string.Empty, Mail_id1 = string.Empty, Mail_id2 = string.Empty, bal_lve = string.Empty;
        try
        {
            Ds = Dblog.Ora_Execute_table("select * from hr_staff_profile where stf_staff_no='" + Session["nokak"].ToString() + "'");
            Ds1 = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid='C0019'");
                      
            Ds3 = Dblog.Ora_Execute_table("select hr_month_Code,hr_month_desc from Ref_hr_month where hr_month_Code='" + Session["rec_dt"].ToString() + "' ORDER BY hr_month_Code");

            if (Ds3.Rows.Count != 0)
            {
                bal_lve = Ds3.Rows[0]["hr_month_desc"].ToString();
            }
            else
            {
                bal_lve = "";
            }

            if (Ds.Rows[0]["stf_email"].ToString() != "")
            {
                Mail_id1 = Ds.Rows[0]["stf_email"].ToString();
            }

            if (Ds1.Rows[0]["KK_email"].ToString() != "")
            {
                Mail_id2 = Ds1.Rows[0]["KK_email"].ToString();
            }
            // HR Email
            if (Mail_id1 != "")
            {

                var fromemail = new MailAddress("support@koopims.com", "KTHB Integrated Management System");
                //var toemail = new MailAddress("vengatit09@gmail.com");
                var toemail = new MailAddress(Mail_id1);
                var fromemailpassword = "P@ssw0rd";
                string subject = "KTHB - Claim Approval";
                string body = "Hello " + txtinfo.ToTitleCase(Ds.Rows[0]["stf_name"].ToString().ToLower()) + ",<br/>Your " + bal_lve + " claim RM " + Session["rec_amt"].ToString() + " has been "+ Session["sts_cd"].ToString() + ".<br/><br/> If You require any clarifications about this application, please contact your HR Department.<br/><br/> Thank You,<br/><a><html><body><a href='http://www.koopims.com'> KTHB.koopims.com </a></body></html> . </a>";

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "webmail.koopims.com",
                    Port = 25,
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

                var fromemail = new MailAddress("support@koopims.com", "KTHB Integrated Management System");
                var toemail = new MailAddress(Mail_id2);
                //var toemail = new MailAddress("vengatit09@gmail.com");
                var fromemailpassword = "P@ssw0rd";
                string subject = "KTHB - Claim Approval";
                string body = "Hello " + txtinfo.ToTitleCase(Ds1.Rows[0]["Kk_username"].ToString().ToLower()) + ",<br/>" + txtinfo.ToTitleCase(Ds.Rows[0]["stf_name"].ToString().ToLower()) + " " + bal_lve + " claim RM " + Session["rec_amt"].ToString() + " has been " + Session["sts_cd"].ToString() + ".<br/><br/> If You require any clarifications about this application, please contact your HR Department.<br/><br/> Thank You,<br/><a><html><body><a href='http://www.koopims.com'> KTHB.koopims.com </a></body></html> . </a>";

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "webmail.koopims.com",
                    Port = 25,
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
            Session["nokak"] = "";
            Session["rec_dt"] = "";
            Session["rec_amt"] = "";
            Session["sts_cd"] = "";

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect("../SUMBER_MANUSIA/kelulusan_tuntutan.aspx");
    }
}