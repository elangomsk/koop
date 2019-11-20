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


public partial class kw_profil_syarikat : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string level;
    string Status = string.Empty;
    string userid;
    string ref_id;
    string confirmValue, am;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty;
    string url_name = string.Empty;
    DataTable fin_info1 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
               
                //level = Session["level"].ToString();
                userid = Session["New"].ToString();
               
                btb_kmes.Visible = false;
                ImgPrv.Attributes.Add("src", "../files/Profile_syarikat/user.png");
                var samp = Request.Url.Query;
              
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                }
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
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1615','705','1616','1617','709','1618','1010','1016','1011','1036','1013','1018','1011','1019','1014','1021','1620','61','35','15','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());    
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());            
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl16.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl17.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl18.Text = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            btb_kmes.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    protected void gvSelected_PageIndexChanging_g2(object sender, GridViewPageEventArgs e)
    {
        Gridview2.PageIndex = e.NewPageIndex;
        Gridview2.DataBind();
    }

    protected void BindData()
    {        
        qry1 = "select Id,fin_year,case when Status='1' then 'OPEN' when Status='0' then 'CLOSE' when Status='2' then 'TRIAL' else '' end sts,FORMAT(fin_start_dt,'dd/MM/yyyy') st_dt,FORMAT(fin_end_dt,'dd/MM/yyyy') ed_dt,fin_months,fin_period from KW_financial_Year where fin_kod_syarikat='" + txt_nombo.Text + "'";
        SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            Gridview2.DataSource = ds2;
            Gridview2.DataBind();
            int columncount = Gridview2.Rows[0].Cells.Count;
            Gridview2.Rows[0].Cells.Clear();
            Gridview2.Rows[0].Cells.Add(new TableCell());
            Gridview2.Rows[0].Cells[0].ColumnSpan = columncount;
            Gridview2.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            Gridview2.DataSource = ds2;
            Gridview2.DataBind();
        }
    }
    protected void view_details()
    {
        try
        {
            if (lbl_name.Text != "")
            {
                
                string ogid = lbl_name.Text;                
                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select * from KW_Profile_syarikat where id = '" + ogid + "'");
                if (ddokdicno.Rows.Count != 0)
                {
                    btb_kmes.Visible = true;
                    Button4.Visible = false;
                    Button1.Visible = false;
                    FileUpload1.Attributes.Add("contentEditable", "false");
                    txt_nama.Text = ddokdicno.Rows[0]["nama_syarikat"].ToString();
                    txt_nombo.Text = ddokdicno.Rows[0]["kod_syarikat"].ToString();
                    txt_alamat.Text = ddokdicno.Rows[0]["alamat_syarikat"].ToString();
                    txt_email.Text = ddokdicno.Rows[0]["syar_email"].ToString();
                    txt_faxno.Text = ddokdicno.Rows[0]["syar_nombo_fax"].ToString();
                    txt_teleph.Text = ddokdicno.Rows[0]["syar_nombo_teleph"].ToString();

                    con.Open();
                    string checkimage = ddokdicno.Rows[0]["syar_logo"].ToString();

                    string fileName = Path.GetFileName(checkimage);
                    if (fileName != "")
                    {
                        ImgPrv.Attributes.Add("src", "../Files/Profile_syarikat/" + fileName);
                    }
                    else
                    {
                        ImgPrv.Attributes.Add("src", "../Files/Profile_syarikat/user.png");
                    }
                    con.Close();
                    DataTable chk_fin_year = new DataTable();
                    chk_fin_year = DBCon.Ora_Execute_table("select fin_year from KW_financial_Year where fin_kod_syarikat='"+ txt_nombo.Text + "' and  fin_year='" + DateTime.Now.Year.ToString() +"'");
                    if(chk_fin_year.Rows.Count != 0)
                    {
                        Button3.Visible = false;
                    }
                    else
                    {
                        Button3.Visible = true;
                    }
                    //dd_cursts.SelectedValue = ddokdicno.Rows[0]["cur_sts"].ToString();
                    //TextBox3.Text = ddokdicno.Rows[0]["tahun_kew"].ToString();
                    //dd_tran_kew.SelectedValue = ddokdicno.Rows[0]["tt_kew"].ToString();

                    //txt_startdt.Text = Convert.ToDateTime(ddokdicno.Rows[0]["tarikh_mula"]).ToString("dd/MM/yyyy");
                    //DateTime smp = DateTime.Parse(ddokdicno.Rows[0]["tarikh_mula"].ToString());
                    //txt_enddt.Text = Convert.ToDateTime(ddokdicno.Rows[0]["tarikh_akhir"]).ToString("dd/MM/yyyy");

                    //if (smp <= DateTime.Now.Date)
                    //{
                    //    txt_startdt.Attributes.Add("Readonly", "Readonly");
                    //}
                    //else
                    //{
                    //    txt_startdt.Attributes.Remove("Readonly");
                    //    txt_enddt.Attributes.Remove("Readonly");
                    //}
                    //txt_gstid.Text = ddokdicno.Rows[0]["gst_id"].ToString();


                }

            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btb_kmes_Click(object sender, EventArgs e)
    {

      
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * from KW_Profile_syarikat where id = '"+ lbl_name.Text + "'");
        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        string checkimage = string.Empty;
        if (fileName == "")
        {
            checkimage = ddokdicno.Rows[0]["syar_logo"].ToString();
        }
        else
        {
            checkimage = fileName;
        }
        if (checkimage != "")
        {
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/Profile_syarikat/" + checkimage));//Or code to save in the DataBase.
            //System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
            decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
        }
        if (checkimage != null && checkimage != "")
            {
                if (txt_nombo.Text != "" && txt_alamat.Text != "")
                {
                    SqlCommand prof_upd = new SqlCommand("update KW_Profile_syarikat set kod_syarikat=@kod_syarikat,nama_syarikat=@nama_syarikat,alamat_syarikat=@alamat_syarikat,syar_logo=@syar_logo,syar_email=@syar_email,syar_nombo_fax=@syar_nombo_fax,syar_nombo_teleph=@syar_nombo_teleph, upd_dt=@upd_dt,upd_id=@upd_id where Id=@Id", con);
                    prof_upd.Parameters.AddWithValue("id", lbl_name.Text);
                    prof_upd.Parameters.AddWithValue("kod_syarikat", txt_nombo.Text);
                    prof_upd.Parameters.AddWithValue("nama_syarikat", txt_nama.Text);
                    prof_upd.Parameters.AddWithValue("alamat_syarikat", txt_alamat.Text);                    
                prof_upd.Parameters.AddWithValue("@syar_logo", checkimage);
                prof_upd.Parameters.AddWithValue("@syar_email", txt_email.Text);
                    prof_upd.Parameters.AddWithValue("@syar_nombo_fax", txt_faxno.Text);
                    prof_upd.Parameters.AddWithValue("@syar_nombo_teleph", txt_teleph.Text);
                    prof_upd.Parameters.AddWithValue("upd_id", Session["New"].ToString());
                    prof_upd.Parameters.AddWithValue("upd_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") );
                    con.Open();
                    int j = prof_upd.ExecuteNonQuery();
                    con.Close();

                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("../kewengan/kw_profil_syarikat_view.aspx");
                }
            }
            else if (checkimage == null || checkimage == "")
            {
                //if (FileUpload1.HasFile)
                //{
                
                if (txt_nombo.Text != "" && txt_alamat.Text != "")
                {

                   
                    //if (size <= 100)
                    //{
                    SqlCommand prof_upd = new SqlCommand("update KW_Profile_syarikat set kod_syarikat=@kod_syarikat,nama_syarikat=@nama_syarikat,alamat_syarikat=@alamat_syarikat,syar_email=@syar_email,syar_nombo_fax=@syar_nombo_fax,syar_nombo_teleph=@syar_nombo_teleph,syar_logo=@syar_logo, upd_dt=@upd_dt,upd_id=@upd_id where Id=@Id", con);
                    prof_upd.Parameters.AddWithValue("id", lbl_name.Text);
                    prof_upd.Parameters.AddWithValue("kod_syarikat", txt_nombo.Text);
                    prof_upd.Parameters.AddWithValue("nama_syarikat", txt_nama.Text);
                    prof_upd.Parameters.AddWithValue("alamat_syarikat", txt_alamat.Text);                    
                    prof_upd.Parameters.AddWithValue("@syar_email", txt_email.Text);
                    prof_upd.Parameters.AddWithValue("@syar_nombo_fax", txt_faxno.Text);
                    prof_upd.Parameters.AddWithValue("@syar_nombo_teleph", txt_teleph.Text);
                    prof_upd.Parameters.AddWithValue("@syar_logo", checkimage);
                    prof_upd.Parameters.AddWithValue("upd_id", Session["New"].ToString());
                    prof_upd.Parameters.AddWithValue("upd_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") );
                    con.Open();
                    int j = prof_upd.ExecuteNonQuery();
                    con.Close();
                    
                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("../kewengan/kw_profil_syarikat_view.aspx");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);

                }
               
            }
        //}
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        txt_nama.Text = "";
        txt_nombo.Text = "";
        txt_teleph.Text = "";
        txt_faxno.Text = "";
        txt_email.Text = "";
        txt_alamat.Text = "";
        BindData();
    }
    SqlDataAdapter adapt;
    DataTable dt;

    protected void fin_year_new(object sender, EventArgs e)
    {
        fin_year.Text = DateTime.Now.Year.ToString();
        tbl_rw2.Attributes.Add("style", "pointer-events:none;");
        ModalPopupExtender1.Show();
        BindData();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        System.Web.UI.WebControls.Label lbl1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("kew_tahun");
        System.Web.UI.WebControls.Label lbl2 = (System.Web.UI.WebControls.Label)e.Row.FindControl("kew_status");
        LinkButton lnk_btn1 = e.Row.FindControl("LinkButton1") as LinkButton;
        LinkButton lnk_btn2 = e.Row.FindControl("LinkButton2") as LinkButton;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            fin_info1 = DBCon.Ora_Execute_table("select opening_year from KW_Opening_Balance where opening_year='" + lbl1.Text + "' group by opening_year");
            if (fin_info1.Rows.Count == 0)
            {
                lnk_btn2.Attributes.Add("class", "btn btn-danger");
            }
            else
            {
                lnk_btn2.Attributes.Add("class", "btn btn-success");
            }

            if (lbl2.Text == "CLOSE")
            {
                lnk_btn1.Visible = false;
            }
            else
            {
                lnk_btn1.Visible = true;
            }
        }
    }
    protected void fin_open_balance(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label finyear = (System.Web.UI.WebControls.Label)gvRow.FindControl("kew_tahun");

        string name = HttpUtility.UrlEncode(service.Encrypt(finyear.Text));
        //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
        Response.Redirect("../kewengan/kw_baki_pembukaan_view.aspx?prfle_cd="+ Request.QueryString["edit"] + "&&edit=" + name + "");
    }
    protected void fin_update(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label finyear = (System.Web.UI.WebControls.Label)gvRow.FindControl("kew_tahun");
        Session["fin_yr"] = finyear.Text;
        load_popup();
    }

    void load_popup()
    {
        DataTable view_fin_info = new DataTable();
        view_fin_info = DBCon.Ora_Execute_table("select *,FORMAT(fin_start_dt,'dd/MM/yyyy') st_dt,FORMAT(fin_end_dt,'dd/MM/yyyy') ed_dt from KW_financial_Year where fin_kod_syarikat='" + txt_nombo.Text + "' and  fin_year='" + Session["fin_yr"].ToString() + "'");
        if (view_fin_info.Rows.Count != 0)
        {
            tbl_rw1.Attributes.Add("style", "pointer-events:none;");
            tbl_rw2.Attributes.Add("style", "pointer-events:none;");
            tbl_rw3.Attributes.Add("style", "pointer-events:none;");
            Button5.Text = "Kemaskini";
            Button6.Visible = false;
            fin_year.Text = Session["fin_yr"].ToString();
            dd_tran_kew.SelectedValue = view_fin_info.Rows[0]["fin_period"].ToString();
            TextBox3.Text = view_fin_info.Rows[0]["fin_months"].ToString();
            txt_startdt.Text = view_fin_info.Rows[0]["st_dt"].ToString();
            dd_cursts.SelectedValue = view_fin_info.Rows[0]["Status"].ToString();
            txt_enddt.Text = view_fin_info.Rows[0]["ed_dt"].ToString();
        }
        ModalPopupExtender1.Show();
    }

    protected void load_akaun_pem(object sender, EventArgs e)
    {
        
        DataTable chk_det1 = new DataTable();
        chk_det1 = DBCon.Ora_Execute_table("select * from KW_rpt_trialbalance where rpt_tb_type='03' and Rpt_tb_year='"+ fin_year.Text + "'");
        if(chk_det1.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('SILA TETAPKAN THE POST-CLOSING TRIAL BALANCE.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            load_popup();
        }
        else
        {
            string name = HttpUtility.UrlEncode(service.Encrypt(fin_year.Text));
            Response.Redirect("../kewengan/kw_akaun_pembahagian.aspx?prfle_cd=" + Request.QueryString["edit"] + "&&edit=" + name + "");
        }
       
    }

    protected void load_akaun_tutup(object sender, EventArgs e)
    {

        DataTable chk_det1 = new DataTable();
        chk_det1 = DBCon.Ora_Execute_table("SELECT * FROM  KW_Ref_Pembahagian WHERE tmp_tahun_kewangan='" + fin_year.Text + "'");
        if (chk_det1.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Pembahagian Keuntungan Masih Belum Dilaksanakan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            load_popup();
        }
        else
        {
            string name = HttpUtility.UrlEncode(service.Encrypt(fin_year.Text));
            Response.Redirect("../kewengan/kw_tutup_akaun.aspx?prfle_cd=" + Request.QueryString["edit"] + "&&edit=" + name + "");
        }
       
    }

    protected void load_akaun_kk(object sender, EventArgs e)
    {
            string name = HttpUtility.UrlEncode(service.Encrypt(fin_year.Text));
            Response.Redirect("../kewengan/kw_lep_kunci_kira.aspx?prfle_cd=" + Request.QueryString["edit"] + "&&edit=" + name + "");
    }
    protected void fin_clk_submit(object sender, EventArgs e)
    {
        if (fin_year.Text != "" && dd_cursts.SelectedValue != "" && txt_startdt.Text != "" && txt_enddt.Text != "")
        {
            string str = txt_startdt.Text;
            DateTime dt = DateTime.ParseExact(str, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string str1 = txt_enddt.Text;
            DateTime dt1 = DateTime.ParseExact(str1, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataTable chk_fin_year = new DataTable();
            chk_fin_year = DBCon.Ora_Execute_table("select fin_year from KW_financial_Year where fin_kod_syarikat='" + txt_nombo.Text + "' and  fin_year='" + DateTime.Now.Year.ToString() + "'");
            if(chk_fin_year.Rows.Count == 0)
            {
                string Inssql1 = "Insert into KW_financial_Year (fin_kod_syarikat,fin_year,fin_period,fin_months,fin_start_dt,fin_end_dt,Status,crt_id,cr_dt,opening_sts) values('" + txt_nombo.Text + "','" + fin_year.Text + "','" + dd_tran_kew.SelectedValue + "','" + TextBox3.Text + "','" + dt.ToString("yyyy-MM-dd") + "','" + dt1.ToString("yyyy-MM-dd") + "','"+ dd_cursts.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
            }
            else
            {
                string Updsql1 = "Update KW_financial_Year set  fin_period='"+ dd_tran_kew.SelectedValue + "',fin_months='"+ TextBox3.Text + "',fin_end_dt='" + dt1.ToString("yyyy-MM-dd") + "',Status='"+ dd_cursts.SelectedValue + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where fin_kod_syarikat='" + txt_nombo.Text + "' and  fin_year='" + DateTime.Now.Year.ToString() + "'";
                Status = DBCon.Ora_Execute_CommamdText(Updsql1);
                Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
            }
            if(Status == "SUCCESS")
            {
                Button3.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('"+ Session["alrt_msg"] + "',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                Session["alrt_msg"] = "";
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        clr_popup();
        BindData();
    }

    protected void get_tahun_bulan(object sender, EventArgs e)
    {
        if (txt_startdt.Text != "" && txt_enddt.Text != "")
        {
            if (chk_tahun.Checked == true)
            {
                DateTime startDate = DateTime.ParseExact(txt_startdt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(txt_enddt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                int months = endDate.Subtract(startDate).Days / 30;


                TextBox3.Text = months.ToString();
            }
            else
            {
                TextBox3.Text = "";
            }
        }
        ModalPopupExtender1.Show();
        BindData();
    }
    protected void fin_rst_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        clr_popup();
        BindData();
    }
    protected void fin_Button5_Click(object sender, EventArgs e)
    {
        clr_popup();
        BindData();
    }
    void clr_popup()
    {
        Button5.Text = "Simpan";
        chk_tahun.Checked = false;
        fin_year.Text = DateTime.Now.Year.ToString();
        dd_tran_kew.SelectedValue = "";
        TextBox3.Text = "";
        txt_startdt.Text = "";
        dd_cursts.SelectedValue = "1";
        txt_enddt.Text = "";
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_nama.Text != "" && txt_nombo.Text != "")
            {
                
                DataTable chk_profil = new DataTable();
                chk_profil = DBCon.Ora_Execute_table("select * from KW_Profile_syarikat where kod_syarikat = '" + txt_nombo.Text + "' and nama_syarikat='" + txt_nama.Text.Replace("'", "''") + "' and '" + dt + "' between tarikh_mula and tarikh_akhir");
                if (chk_profil.Rows.Count == 0)
                {
                    DataTable ddicno = new DataTable();
                    int contentLength = FileUpload1.PostedFile.ContentLength; //You may need it for validation
                    string contentType = FileUpload1.PostedFile.ContentType; //You may need it for validation
                    string fileName = FileUpload1.PostedFile.FileName;
                    if (fileName != "")
                    {
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Files/Profile_syarikat/" + fileName));//Or code to save in the DataBase.
                        System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                        decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                    }
                    //if (size <= 100)
                    //{

                    SqlCommand ins_prof = new SqlCommand("insert into KW_Profile_syarikat (kod_syarikat,nama_syarikat,alamat_syarikat,syar_email,syar_nombo_fax,syar_nombo_teleph,Status,cr_dt,crt_id,syar_logo)values(@kod_syarikat,@nama_syarikat,@alamat_syarikat,@syar_email,@syar_nombo_fax,@syar_nombo_teleph,@Status,@cr_dt,@crt_id,@syar_logo)", con);

                    ins_prof.Parameters.AddWithValue("@kod_syarikat", txt_nombo.Text);
                    ins_prof.Parameters.AddWithValue("@nama_syarikat", txt_nama.Text.Replace("'", "''"));
                    ins_prof.Parameters.AddWithValue("@alamat_syarikat", txt_alamat.Text.Replace("'", "''"));
                    ins_prof.Parameters.AddWithValue("@syar_email", txt_email.Text.Replace("'", "''"));
                    ins_prof.Parameters.AddWithValue("@syar_nombo_fax", txt_faxno.Text.Replace("'", "''"));
                    ins_prof.Parameters.AddWithValue("@syar_nombo_teleph", txt_teleph.Text.Replace("'", "''"));
                    ins_prof.Parameters.AddWithValue("@syar_logo", fileName);
                    ins_prof.Parameters.AddWithValue("@Status", 'A');                    
                    ins_prof.Parameters.AddWithValue("@crt_id", Session["New"].ToString());
                    ins_prof.Parameters.AddWithValue("@cr_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    con.Open();
                    int i = ins_prof.ExecuteNonQuery();
                    con.Close();
                   
                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                    Response.Redirect("../kewengan/kw_profil_syarikat_view.aspx");
                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sedia Ada.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   
    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["con_id"] = "";
        Response.Redirect("../kewengan/kw_profil_syarikat_view.aspx");
    }

    
}