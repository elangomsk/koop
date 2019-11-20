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

public partial class kw_pelanggan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();


    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty;
    string level;
    string Status = string.Empty, Status_del = string.Empty, Status1 = string.Empty, Status2 = string.Empty;
    string userid;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
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

                negeri();
                negera();
                bind_bank();
                bind_kod_akaun();
                bind_kod_industry();
                dd_negera.SelectedValue = "129";
                TextBox10.Attributes.Add("Readonly", "Readonly");
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('402','705','1471','709','496','1704','739','1705','1526','1420','1018','29','347','28','1650','1706','824','738','1651','61','15','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());            
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl15.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl16.Text = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
            ps_lbl17.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            ps_lbl18.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl19.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl20.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());


        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void bind_kod_industry()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select distinct kod_industry,(kod_industry +' | ' + msic_desc) as name from Kw_kod_industry left join KW_Ref_Kod_Industri on msic_kod=kod_industry where kod_industry != ''";
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


    void bind_bank()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Bank_Code,Bank_Name from Ref_Nama_Bank where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "Bank_Name";
            DropDownList1.DataValueField = "Bank_Code";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void view_details()
    {
        Button1.Visible = false;
        try
        {
            string lblid = lbl_name.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_Pelanggan where Id='" + lblid + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                Button4.Text = "Kemaskini";
                ver_id.Text = "1";
                get_id.Text = lblid;
                TextBox1.Text = ddokdicno.Rows[0]["Ref_nama_syarikat"].ToString();
                TextBox2.Text = ddokdicno.Rows[0]["Ref_no_fax"].ToString();
                TextBox4.Text = ddokdicno.Rows[0]["Ref_no_telefone"].ToString();
                TextBox5.Text = ddokdicno.Rows[0]["Ref_no_syarikat"].ToString();
                TextBox3.Text = ddokdicno.Rows[0]["Ref_email"].ToString();
                TextBox6.Text = ddokdicno.Rows[0]["Ref_gst_id"].ToString();
                TextBox7.Text = ddokdicno.Rows[0]["Ref_alamat"].ToString();
                TextBox8.Text = ddokdicno.Rows[0]["Ref_kawalan"].ToString();
                TextBox10.Text = ddokdicno.Rows[0]["Ref_kod_akaun"].ToString();
                dd_akaun.SelectedValue = ddokdicno.Rows[0]["Ref_kod_akaun"].ToString();
                DropDownList1.SelectedValue = ddokdicno.Rows[0]["pel_bank_cd"].ToString();
                TextBox12.Text = ddokdicno.Rows[0]["pel_bank_accno"].ToString();
                TextBox11.Text = ddokdicno.Rows[0]["ref_alamat_ked"].ToString();
                TextBox9.Text = ddokdicno.Rows[0]["pel_bandar"].ToString();
                ddnegeri.SelectedValue = ddokdicno.Rows[0]["pel_negeri"].ToString();
                TextBox14.Text = ddokdicno.Rows[0]["pel_poskod"].ToString();
                TextBox13.Text = double.Parse(ddokdicno.Rows[0]["pel_credit_lmt"].ToString()).ToString("C").Replace("$","").Replace("RM", "");
                dd_kodind.SelectedValue = ddokdicno.Rows[0]["pel_kod_industry"].ToString();
                if (ddokdicno.Rows[0]["pel_negera"].ToString() != "")
                {
                    dd_negera.SelectedValue = ddokdicno.Rows[0]["pel_negera"].ToString();
                }
                else
                {
                    dd_negera.SelectedValue = "129";
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void bind_kod_akaun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select distinct kod_akaun,(kod_akaun + ' | ' + nama_akaun) nama_akaun from KW_Ref_Carta_Akaun where ISNULL(kw_acc_header,'0')='0' and jenis_akaun_type !='1' and Status='A' order by kod_akaun asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_akaun.DataSource = dt;
            dd_akaun.DataTextField = "nama_akaun";
            dd_akaun.DataValueField = "kod_akaun";
            dd_akaun.DataBind();
            dd_akaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void negeri()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select DISTINCT hr_negeri_code,UPPER(hr_negeri_desc) as hr_negeri_desc from ref_hr_negeri";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddnegeri.DataSource = dt;
            ddnegeri.DataBind();
            ddnegeri.DataTextField = "hr_negeri_desc";
            ddnegeri.DataValueField = "hr_negeri_code";
            ddnegeri.DataBind();
            ddnegeri.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void negera()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select * from Country";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_negera.DataSource = dt;
            dd_negera.DataBind();
            dd_negera.DataTextField = "CountryName";
            dd_negera.DataValueField = "ID";
            dd_negera.DataBind();
            dd_negera.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void katcd_TextChanged(object sender, EventArgs e)
    {
        DataTable sel_kat = new DataTable();
        sel_kat = DBCon.Ora_Execute_table("select * from KW_Ref_Pelanggan where Ref_no_syarikat = '" + TextBox5.Text + "'");
        if (sel_kat.Rows.Count != 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pendaftaran Syarikat Kod Telah Wujud.');", true);
            TextBox5.Text = "";
            TextBox5.Focus();
        }
        else
        {
            TextBox1.Focus();
        }
    }

    protected void sel_jenis(object sender, EventArgs e)
    {
        TextBox10.Text = dd_akaun.SelectedValue;
        if (ver_id.Text == "0")
        {
            Button4.Text = "Simpan";
        }
        else
        {
            Button4.Text = "Kemaskini";
        }
    }

    protected void clk_submit(object sender, EventArgs e)
    {
        if (TextBox1.Text != "" && TextBox5.Text != "")
        {
            string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
            string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
            DataTable cnt_no = new DataTable();
            if (dd_akaun.SelectedValue != "")
            {

                //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
                cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='" + dd_akaun.SelectedValue + "'");

                set_cnt1 = cnt_no.Rows[0]["cnt"].ToString();
                mcnt = cnt_no.Rows[0]["rcnt"].ToString();
                set_cnt = dd_akaun.SelectedValue;
                //set_cnt1 = "1";
            }
            else
            {
                set_cnt1 = "0";
                mcnt = "1";
                set_cnt = "00";
            }


            //DataTable cnt_no1 = new DataTable();
            //cnt_no1 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_akaun_cnt,'') +1) as mcnt From KW_Ref_Carta_Akaun where jenis_akaun='" + dd_akaun.SelectedValue + "' order by kod_akaun desc");

            //if (cnt_no1.Rows.Count != 0)
            //{
            //    cnt_value = cnt_no1.Rows[0]["mcnt"].ToString();
            //}
            //else
            //{
            //    cnt_value = "1";
            //}
            //string chk_role = string.Empty;
            //string[] sp_no = set_cnt.Split('.');
            //int sp_no1 = sp_no.Length;
            //string ss1 = string.Empty;
            //for (int i = 0; i <= sp_no1; i++)
            //{
            //    if (i == (Int32.Parse(mcnt) - 1))
            //    {
            //        sno1 = cnt_value.PadLeft(2, '0');
            //    }
            //    else
            //    {
            //        sno1 = sp_no[i].ToString();
            //    }
            //    if (i < (sp_no1))
            //    {
            //        ss1 = ".";
            //    }
            //    else
            //    {
            //        ss1 = "";
            //    }
            //    chk_role += (sno1).PadLeft(2, '0') + "" + ss1;
            //}

            //string sso1 = string.Empty, sso2 = string.Empty, sso3 = string.Empty;
            //if (set_cnt1 == "0")
            //{
            //    sso1 = "";
            //}
            //else
            //{
            //    if (cnt_no.Rows[0]["jenis_akaun_type"].ToString() == "1")
            //    {
            //        sso1 = chk_role + "," + cnt_no.Rows[0]["kod_akaun"].ToString();
            //    }
            //    else
            //    {
            //        sso1 = chk_role + "," + cnt_no.Rows[0]["under_jenis"].ToString();
            //    }
            //}
            string ss_kod = string.Empty;
            DataTable get_val = new DataTable();
            get_val = DBCon.Ora_Execute_table("select kat_akaun From KW_Ref_Carta_Akaun where kod_akaun='" + dd_akaun.SelectedValue + "'");
            if (dd_akaun.SelectedValue != "")
            {
                ss_kod = get_val.Rows[0]["kat_akaun"].ToString();
            }
            if (ver_id.Text == "0")
            {
                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_Pelanggan where Ref_nama_syarikat='" + TextBox1.Text + "' and Ref_no_syarikat='" + TextBox5.Text + "'");
                if (ddokdicno.Rows.Count == 0)
                {
                    //DataTable get_val = new DataTable();
                    //get_val = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='" + dd_akaun.SelectedValue + "'");
                    //string Inssql1 = "insert into KW_Ref_Carta_Akaun(kat_akaun,nama_akaun,kod_akaun,jenis_akaun,under_parent,KW_Debit_amt,KW_kredit_amt,Kw_open_amt,jenis_akaun_type,under_jenis,Status,crt_id,cr_dt,Susu_nilai,kod_akaun_cnt,sts_kawalan,ct_kod_industry) values ('" + get_val.Rows[0]["kat_akaun"].ToString() + "','" + TextBox1.Text.Replace("'", "''") + "','" + chk_role + "','" + dd_akaun.SelectedValue + "','" + set_cnt1 + "','0.00','0.00','0.00','" + mcnt + "','','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0','" + cnt_value + "','Y','"+ dd_kodind.SelectedValue +"')";
                    //Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                    //if (Status == "SUCCESS")
                    //{
                        string Inssql = "insert into KW_Ref_Pelanggan(Ref_nama_syarikat,Ref_no_syarikat,Ref_no_telefone,Ref_no_fax,Ref_email,Ref_gst_id,Ref_kod_akaun,Ref_jenis_akaun,Ref_alamat,Status,crt_id,cr_dt,Ref_kawalan,ref_alamat_ked,pel_bandar,pel_negeri,pel_poskod,pel_negera,pel_kod_industry,pel_bank_cd,pel_bank_accno,pel_credit_lmt) values ('" + TextBox1.Text.Replace("'", "''") + "','" + TextBox5.Text.Replace("'", "''") + "','" + TextBox4.Text.Replace("'", "''") + "','" + TextBox2.Text.Replace("'", "''") + "','" + TextBox3.Text.Replace("'", "''") + "','" + TextBox6.Text.Replace("'", "''") + "','"+ TextBox10.Text +"','" + ss_kod + "','" + TextBox7.Text.Replace("'", "''") + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + TextBox8.Text.Replace("'", "''") + "','" + TextBox11.Text.Replace("'", "''") + "','" + TextBox9.Text.Replace("'", "''") + "','" + ddnegeri.SelectedValue + "','" + TextBox14.Text.Replace("'", "''") + "','" + dd_negera.SelectedValue + "','"+ dd_kodind.SelectedValue +"','"+ DropDownList1.SelectedValue + "','" + TextBox12.Text + "','"+ TextBox13.Text + "')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Session["validate_success"] = "SUCCESS";
                        Response.Redirect("../kewengan/kw_pelanggan_view.aspx");
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                //DataTable ddokdicno = new DataTable();
                //ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_Pelanggan where Id = '" + get_id.Text + "'");
                //if (ddokdicno.Rows[0]["Ref_jenis_akaun"].ToString() != dd_akaun.SelectedValue)
                //{
                //    DataTable get_val1 = new DataTable();
                //    get_val1 = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='" + ddokdicno.Rows[0]["Ref_kod_akaun"].ToString() + "'");
                //    if (get_val1.Rows.Count != 0)
                //    {
                //        string Inssql2 = "delete from KW_Ref_Carta_Akaun where kod_akaun = '" + ddokdicno.Rows[0]["Ref_kod_akaun"].ToString() + "'";
                //        Status_del = DBCon.Ora_Execute_CommamdText(Inssql2);
                //        if (Status_del == "SUCCESS")
                //        {
                //            DataTable get_val = new DataTable();
                //            get_val = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='" + dd_akaun.SelectedValue + "'");
                //            string Inssql1 = "insert into KW_Ref_Carta_Akaun(kat_akaun,nama_akaun,kod_akaun,jenis_akaun,under_parent,KW_Debit_amt,KW_kredit_amt,Kw_open_amt,jenis_akaun_type,under_jenis,Status,crt_id,cr_dt,Susu_nilai,kod_akaun_cnt,sts_kawalan,ct_kod_industry) values ('" + get_val.Rows[0]["kat_akaun"].ToString() + "','" + TextBox1.Text.Replace("'", "''") + "','" + chk_role + "','" + dd_akaun.SelectedValue + "','" + set_cnt1 + "','0.00','0.00','0.00','" + mcnt + "','','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0','" + cnt_value + "','Y','"+ dd_kodind.SelectedValue +"')";
                //            Status2 = DBCon.Ora_Execute_CommamdText(Inssql1);
                //        }
                //        else
                //        {
                //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Dihapus Tidak Mungkin.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                //        }
                //    }

                //}
                //else
                //{
                //    string Inssql1 = "Update KW_Ref_Carta_Akaun set ct_kod_industry='"+ dd_kodind.SelectedValue +"',nama_akaun='" + TextBox1.Text.Replace("'", "''") + "' where kod_akaun='" + ddokdicno.Rows[0]["Ref_kod_akaun"].ToString() + "'";
                //    Status2 = DBCon.Ora_Execute_CommamdText(Inssql1);
                //}
                string ckod = string.Empty;
                //if (Status2 == "SUCCESS")
                //{
                    
                //    if (ddokdicno.Rows[0]["Ref_jenis_akaun"].ToString() != dd_akaun.SelectedValue)
                //    {
                //        ckod = "Ref_kod_akaun='" + chk_role + "',";
                //    }
                //    else
                //    {
                //        ckod = "";
                //    }

                    string Inssql = "UPDATE KW_Ref_Pelanggan set " + ckod + " pel_bank_cd='"+ DropDownList1.SelectedValue +"',pel_bank_accno='"+ TextBox12.Text + "',pel_kod_industry='"+ dd_kodind.SelectedValue +"',Ref_nama_syarikat='" + TextBox1.Text.Replace("'", "''") + "',Ref_no_syarikat='" + TextBox5.Text.Replace("'", "''") + "',Ref_no_telefone='" + TextBox4.Text.Replace("'", "''") + "',Ref_no_fax='" + TextBox2.Text.Replace("'", "''") + "',Ref_email='" + TextBox3.Text.Replace("'", "''") + "',Ref_gst_id='" + TextBox6.Text.Replace("'", "''") + "',Ref_alamat='" + TextBox7.Text.Replace("'", "''") + "',Ref_kawalan='" + TextBox8.Text.Replace("'", "''") + "',ref_alamat_ked='" + TextBox11.Text.Replace("'", "''") + "',pel_bandar='" + TextBox9.Text.Replace("'", "''") + "',pel_negeri='" + ddnegeri.SelectedValue + "',pel_poskod='" + TextBox14.Text.Replace("'", "''") + "',pel_negera='" + dd_negera.SelectedValue + "',Ref_jenis_akaun='" + ss_kod + "',pel_credit_lmt='"+ TextBox13.Text +"',Ref_kod_akaun='" + TextBox10.Text + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id = '" + get_id.Text + "'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);

                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Session["validate_success"] = "SUCCESS";
                    Response.Redirect("../kewengan/kw_pelanggan_view.aspx");
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                //}
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
        Response.Redirect("../kewengan/kw_pelanggan.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_pelanggan_view.aspx");
    }

    
}