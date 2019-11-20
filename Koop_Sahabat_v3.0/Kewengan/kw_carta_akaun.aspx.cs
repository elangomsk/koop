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

public partial class kw_carta_akaun : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                TextBox2.Attributes.Add("Readonly", "Readonly");
                bind_kat_akaun();
                bind_kod_akaun();
                bind_kod_industry();
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                }
                else
                {
                    ver_id.Text = "0";
                }
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('731','705','1649','711','1650','1641','1651','1652','828','1653','1654','1655','1656','1657','1486','61','15','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            RadioButton1.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());     
            CheckBox1.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            CheckBox2.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());              
            CheckBox3.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            CheckBox4.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            CheckBox5.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl16.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
           

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void bind_kod_akaun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kod_akaun,(kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end) as name from KW_Ref_Carta_Akaun where jenis_akaun_type !='1' order by kod_akaun asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_akaun.DataSource = dt;
            dd_akaun.DataTextField = "name";
            dd_akaun.DataValueField = "kod_akaun";
            dd_akaun.DataBind();
            dd_akaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void bind_kod_industry()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kod_industry,(kod_industry +' | ' + msic_desc) as name from Kw_kod_industry left join KW_Ref_Kod_Industri on msic_kod=kod_industry where kod_industry != ''";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_kodind.DataSource = dt;
            dd_kodind.DataTextField = "name";
            dd_kodind.DataValueField = "kod_industry";
            dd_kodind.DataBind();
            dd_kodind.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void bind_kat_akaun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kat_cd,kat_akuan from KW_Kategori_akaun order by kat_cd asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            kat_akaun.DataSource = dt;
            kat_akaun.DataTextField = "kat_akuan";
            kat_akaun.DataValueField = "kat_cd";
            kat_akaun.DataBind();
            kat_akaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void view_details()
    {
      
        try
        {
            DataTable get_value1_1 = new DataTable();
            get_value1_1 = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where Id='" + lbl_name.Text + "'");

            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * from KW_Ref_Carta_Akaun where jenis_akaun='" + get_value1_1.Rows[0]["kod_akaun"].ToString() + "'");
            //if (ddokdicno.Rows.Count == 0 && get_value.Rows[0]["jenis_akaun_type"].ToString() != "1")
            if (get_value1_1.Rows[0]["jenis_akaun_type"].ToString() != "1")
            {
                if (ddokdicno.Rows.Count == 0)
                {
                    if (get_value1_1.Rows[0]["sts_kawalan"].ToString() != "Y")
                    {
                        dd_list_sts.Attributes.Remove("Readonly");
                        dd_list_sts.Attributes.Remove("Style");
                    }
                    else
                    {
                        dd_list_sts.Attributes.Add("Readonly", "Readonly");
                        dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
                    }
                }
                else
                {
                    dd_list_sts.Attributes.Add("Readonly", "Readonly");
                    dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
                }

                if (ddokdicno.Rows.Count == 0 && get_value1_1.Rows[0]["jenis_akaun_type"].ToString() == "1" && get_value1_1.Rows[0]["sts_kawalan"].ToString() != "Y")
                {
                    dd_list_sts.Attributes.Remove("Readonly");
                    dd_list_sts.Attributes.Remove("Style");
                }
                else if (ddokdicno.Rows.Count == 0 && get_value1_1.Rows[0]["jenis_akaun_type"].ToString() != "1" && get_value1_1.Rows[0]["sts_kawalan"].ToString() != "Y")
                {
                    dd_list_sts.Attributes.Remove("Readonly");
                    dd_list_sts.Attributes.Remove("Style");
                }
                else if (ddokdicno.Rows.Count != 0 && get_value1_1.Rows[0]["jenis_akaun_type"].ToString() != "1" && get_value1_1.Rows[0]["sts_kawalan"].ToString() != "Y")
                {
                    dd_list_sts.Attributes.Add("Readonly", "Readonly");
                    dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
                }
                else if (ddokdicno.Rows.Count != 0 && get_value1_1.Rows[0]["jenis_akaun_type"].ToString() != "1" && get_value1_1.Rows[0]["sts_kawalan"].ToString() == "Y")
                {
                    dd_list_sts.Attributes.Add("Readonly", "Readonly");
                    dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
                }
            }
            else
            {
                dd_list_sts.Attributes.Add("Readonly", "Readonly");
                dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
            }

            if (Session["pro_type"].ToString() == "new")
            {
                clk_new();
            }
          else if (Session["pro_type"].ToString() == "update")
            {
                clk_update();
            }
            get_id.Text = lbl_name.Text;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    void clk_new()
    {
        
        DataTable get_value1 = new DataTable();
        get_value1 = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where Id='" + lbl_name.Text + "'");
        if (get_value1.Rows.Count != 0)
        {

            kat_akaun.SelectedValue = get_value1.Rows[0]["kat_akaun"].ToString();

            TextBox2.Text = get_value1.Rows[0]["nama_akaun"].ToString();
            get_cd.Text = get_value1.Rows[0]["kod_akaun"].ToString();
            dd_kodind.SelectedValue = get_value1.Rows[0]["ct_kod_industry"].ToString();
            dd_list_sts.SelectedValue = get_value1.Rows[0]["Status"].ToString();

            if(get_value1.Rows[0]["kkk_rep"].ToString() == "1")
            {
                CheckBox1.Checked = true;
            }
            else
            {
                CheckBox1.Checked = false;
            }

            if (get_value1.Rows[0]["PAL_rep"].ToString() == "1")
            {
                CheckBox2.Checked = true;
            }
            else
            {
                CheckBox2.Checked = false;
            }

            if (get_value1.Rows[0]["AT_rep"].ToString() == "1")
            {
                CheckBox3.Checked = true;
            }
            else
            {
                CheckBox3.Checked = false;
            }

            if (get_value1.Rows[0]["AP_rep"].ToString() == "1")
            {
                CheckBox4.Checked = true;
            }
            else
            {
                CheckBox4.Checked = false;
            }

            if (get_value1.Rows[0]["COGS_rep"].ToString() == "1")
            {
                CheckBox5.Checked = true;
            }
            else
            {
                CheckBox5.Checked = false;
            }

            if (get_value1.Rows[0]["jenis_akaun_type"].ToString() != "1")
            {
                ss1.Visible = true;
            }
            else
            {
                ss1.Visible = false;
            }
        }
        TextBox1.Text = "";
        TextBox1.Focus();
        ver_id.Text = "0";
        show_ddvalue();
    }
    void clk_update()
    {
        
        DataTable get_value = new DataTable();
        get_value = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where Id='" + lbl_name.Text + "'");
        if (get_value.Rows.Count != 0)
        {
           
            Button4.Text = "Kemaskini";
           
            ver_id.Text = "1";
          
            //Button6.Visible = true;

            DataTable get_value2 = new DataTable();
            get_value2 = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='" + get_value.Rows[0]["jenis_akaun"].ToString() + "'");
            if (get_value2.Rows.Count != 0)
            {
                TextBox2.Text = get_value2.Rows[0]["nama_akaun"].ToString();
            }

            kat_akaun.SelectedValue = get_value.Rows[0]["kat_akaun"].ToString();
            TextBox1.Text = get_value.Rows[0]["nama_akaun"].ToString();
            dd_akaun.SelectedValue = get_value.Rows[0]["under_jenis"].ToString();
            dd_kodind.SelectedValue = get_value.Rows[0]["ct_kod_industry"].ToString();
            dd_list_sts.SelectedValue = get_value.Rows[0]["Status"].ToString();
            if (get_value.Rows[0]["Susu_nilai"].ToString() == "1")
            {
                RadioButton1.Checked = true;
            }
            else
            {
                RadioButton1.Checked = false;
            }

            if (get_value.Rows[0]["kkk_rep"].ToString() == "1")
            {
                CheckBox1.Checked = true;
            }
            else
            {
                CheckBox1.Checked = false;
            }

            if (get_value.Rows[0]["PAL_rep"].ToString() == "1")
            {
                CheckBox2.Checked = true;
            }
            else
            {
                CheckBox2.Checked = false;
            }

            if (get_value.Rows[0]["AT_rep"].ToString() == "1")
            {
                CheckBox3.Checked = true;
            }
            else
            {
                CheckBox3.Checked = false;
            }

            if (get_value.Rows[0]["AP_rep"].ToString() == "1")
            {
                CheckBox4.Checked = true;
            }
            else
            {
                CheckBox4.Checked = false;
            }
            if (get_value.Rows[0]["COGS_rep"].ToString() == "1")
            {
                CheckBox5.Checked = true;
            }
            else
            {
                CheckBox5.Checked = false;
            }

            if(get_value.Rows[0]["jenis_akaun_type"].ToString() == "1")
            {
                TextBox1.Attributes.Add("Readonly", "Readonly");
                ss1.Visible = false;
            }
            else
            {
                TextBox1.Attributes.Remove("Readonly");
                ss1.Visible = true;
            }

            TextBox1.Focus();
            show_ddvalue();
        }
    }

    protected void clk_yes(object sender, EventArgs e)
    {
        show_ddvalue();
    }

    void show_ddvalue()
    {
        if (RadioButton1.Checked == true)
        {
            set_kakaun.Visible = true;
        }
        else
        {
            set_kakaun.Visible = false;
        }
    }

    protected void clk_submit(object sender, EventArgs e)
    {
        if (kat_akaun.SelectedValue != "" && TextBox1.Text != "")
        {
            string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
            DataTable cnt_no = new DataTable();


            string susnilai = string.Empty, ckbox1 = string.Empty, ckbox2 = string.Empty, ckbox3 = string.Empty, ckbox4 = string.Empty, ckbox5 = string.Empty;
            if (RadioButton1.Checked == true)
            {
                susnilai = "1";
            }
            else
            {
                susnilai = "0";
            }

            if (CheckBox1.Checked == true)
            {
                ckbox1 = "1";
            }
            else
            {
                ckbox1 = "0";
            }

            if (CheckBox2.Checked == true)
            {
                ckbox2 = "1";
            }
            else
            {
                ckbox2 = "0";
            }

            if (CheckBox3.Checked == true)
            {
                ckbox3 = "1";
            }
            else
            {
                ckbox3 = "0";
            }

            if (CheckBox4.Checked == true)
            {
                ckbox4 = "1";
            }
            else
            {
                ckbox4 = "0";
            }

            if (CheckBox5.Checked == true)
            {
                ckbox5 = "1";
            }
            else
            {
                ckbox5 = "0";
            }

            if (ver_id.Text == "0")
            {
                if (get_cd.Text != "")
                {

                    //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
                    cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='" + get_cd.Text + "'");

                    set_cnt1 = cnt_no.Rows[0]["cnt"].ToString();
                    mcnt = cnt_no.Rows[0]["rcnt"].ToString();
                    set_cnt = get_cd.Text;
                    //set_cnt1 = "1";
                }
                else
                {
                    set_cnt1 = "0";
                    mcnt = "1";
                    set_cnt = "00";
                }

                string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
                DataTable cnt_no1 = new DataTable();
                cnt_no1 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_akaun_cnt,'') +1) as mcnt From KW_Ref_Carta_Akaun where jenis_akaun='" + get_cd.Text + "' order by cast(kod_akaun_cnt as int) desc");

                if (cnt_no1.Rows.Count != 0)
                {
                    cnt_value = cnt_no1.Rows[0]["mcnt"].ToString();
                }
                else
                {
                    cnt_value = "1";
                }
                string chk_role = string.Empty;
                string[] sp_no = set_cnt.Split('.');
                int sp_no1 = sp_no.Length;
                string ss1 = string.Empty;
                for (int i = 0; i <= sp_no1; i++)
                {
                    if (i == (Int32.Parse(mcnt) - 1))
                    {
                        sno1 = cnt_value.PadLeft(2, '0');
                    }
                    else
                    {
                        sno1 = sp_no[i].ToString();
                    }
                    if (i < (sp_no1))
                    {
                        ss1 = ".";
                    }
                    else
                    {
                        ss1 = "";
                    }
                    chk_role += (sno1).PadLeft(2, '0') + "" + ss1;
                }

                string sso1 = string.Empty, sso2 = string.Empty, sso3 = string.Empty;
                if (set_cnt1 == "0")
                {
                    sso1 = "";
                }
                else
                {
                    if (cnt_no.Rows[0]["jenis_akaun_type"].ToString() == "1")
                    {
                        sso1 = chk_role + "," + cnt_no.Rows[0]["kod_akaun"].ToString();
                    }
                    else
                    {
                        sso1 = chk_role + "," + cnt_no.Rows[0]["under_jenis"].ToString();
                    }
                }

                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + TextBox1.Text + "'");
                if (ddokdicno.Rows.Count == 0)
                {


                    string Inssql = "insert into KW_Ref_Carta_Akaun(kat_akaun,nama_akaun,kod_akaun,jenis_akaun,under_parent,KW_Debit_amt,KW_kredit_amt,Kw_open_amt,jenis_akaun_type,under_jenis,Status,crt_id,cr_dt,Susu_nilai,kod_akaun_cnt,kkk_rep,PAL_rep,AT_rep,AP_rep,COGS_rep,ct_kod_industry) values ('" + kat_akaun.SelectedValue + "','" + TextBox1.Text.Replace("'", "''") + "','" + chk_role + "','" + get_cd.Text + "','" + set_cnt1 + "','0.00','0.00','0.00','" + mcnt + "','" + dd_akaun.SelectedValue + "','"+ dd_list_sts.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + susnilai + "','" + cnt_value + "','" + ckbox1 + "','" + ckbox2 + "','" + ckbox3 + "','" + ckbox4 + "','" + ckbox5 + "','"+ dd_kodind.SelectedValue +"')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Session["validate_success"] = "SUCCESS";
                        Response.Redirect("../kewengan/kw_carta_akaun_view.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Memasukkan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                string Inssql = "UPDATE KW_Ref_Carta_Akaun set ct_kod_industry='"+ dd_kodind.SelectedValue +"',kat_akaun='" + kat_akaun.SelectedValue + "',under_jenis='" + dd_akaun.SelectedValue + "',nama_akaun='" + TextBox1.Text.Replace("'", "''") + "',Susu_nilai='" + susnilai + "',kkk_rep='" + ckbox1 + "',PAL_rep='" + ckbox2 + "',AT_rep='" + ckbox3 + "',AP_rep='" + ckbox4 + "',COGS_rep='" + ckbox5 + "',Status='"+ dd_list_sts.SelectedValue + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id = '" + get_id.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    cnt_no = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where Id = '" + get_id.Text + "'");
                    string Inssql2 = "UPDATE KW_Ref_Carta_Akaun set ct_kod_industry='" + dd_kodind.SelectedValue + "',kkk_rep='" + ckbox1 + "',PAL_rep='" + ckbox2 + "',AT_rep='" + ckbox3 + "',AP_rep='" + ckbox4 + "',COGS_rep='" + ckbox5 + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where jenis_akaun = '" + cnt_no.Rows[0]["kod_akaun"].ToString() + "'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql2);

                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Session["validate_success"] = "SUCCESS";
                    Response.Redirect("../kewengan/kw_carta_akaun_view.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Memasukkan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }
        
        
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_carta_akaun.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_carta_akaun_view.aspx");
    }

    
}