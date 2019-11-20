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

public partial class kw_akaun_bank : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string level;
    string Status = string.Empty, Status_del = string.Empty, Status1 = string.Empty, Status2 = string.Empty;
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
                bind_kod_akaun();
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
              
                //TextBox4.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1638','705','1639','52','1640','1641','824','727','1642','1486','61','15','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());            
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());             
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
           
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void view_details()
    {
        Button1.Visible = false;
        try
        {
            string lblid = lbl_name.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_Akaun_bank where Id='" + lblid + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                Button4.Text = "Kemaskini";
                ver_id.Text = "1";
                get_id.Text = lblid;                
                TextBox1.Text = ddokdicno.Rows[0]["Ref_nama_bank"].ToString();
                TextBox5.Text = ddokdicno.Rows[0]["Ref_No_akaun"].ToString();
                TextBox2.Text = ddokdicno.Rows[0]["Ref_Nama_akaun"].ToString();
                //TextBox4.Text = ddokdicno.Rows[0]["Ref_kod_akaun"].ToString();
                alamat_akn.Value = ddokdicno.Rows[0]["Ref_Alamat_akaun"].ToString();
                dd_akaun.SelectedValue = ddokdicno.Rows[0]["Ref_kod_akaun"].ToString();
                sts.SelectedValue = ddokdicno.Rows[0]["Status"].ToString();
                DataTable sel_balance = new DataTable();
                sel_balance = DBCon.Ora_Execute_table("select Kw_open_amt From KW_Ref_Carta_Akaun where kod_akaun='" + ddokdicno.Rows[0]["Ref_kod_akaun"].ToString() + "'");
                if (sel_balance.Rows.Count != 0)
                {
                    TextBox3.Text = double.Parse(sel_balance.Rows[0]["Kw_open_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    TextBox3.Text = "0.00";
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
            string com = "select kod_akaun,(kod_akaun + ' | ' + nama_akaun) as name from KW_Ref_Carta_Akaun where ISNULL(kw_acc_header,'0')='0' and jenis_akaun_type !='1' and Status='A' order by kod_akaun asc";
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

    protected void sel_jenis(object sender, EventArgs e)
    {
        DataTable sel_balance = new DataTable();
        sel_balance = DBCon.Ora_Execute_table("select Kw_open_amt From KW_Ref_Carta_Akaun where kod_akaun='" + dd_akaun.SelectedValue + "'");
        if (sel_balance.Rows.Count != 0)
        {
            TextBox3.Text = double.Parse(sel_balance.Rows[0]["Kw_open_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
        }
        else
        {
            TextBox3.Text = "0.00";
        }
        if(ver_id.Text =="1")
        {
            Button4.Text = "Kemaskini";
        }
        else
        {
            Button4.Text = "Simpan";
        }
        
    }

    protected void clk_submit(object sender, EventArgs e)
    {
        if (TextBox1.Text != "" && TextBox5.Text != "" && TextBox2.Text != "" && dd_akaun.SelectedValue != "")
        {
            string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
            DataTable cnt_no = new DataTable();
            //if (dd_akaun.SelectedValue != "")
            //{

            //    //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
            //    cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='" + dd_akaun.SelectedValue + "'");

            //    set_cnt1 = cnt_no.Rows[0]["cnt"].ToString();
            //    mcnt = cnt_no.Rows[0]["rcnt"].ToString();
            //    set_cnt = dd_akaun.SelectedValue;
            //    //set_cnt1 = "1";
            //}
            //else
            //{
            //    set_cnt1 = "0";
            //    mcnt = "1";
            //    set_cnt = "00";
            //}

            //string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
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
            if (ver_id.Text == "0")
            {


                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_Akaun_bank where Ref_nama_bank='" + TextBox1.Text + "' and Ref_No_akaun='" + TextBox5.Text + "' and Ref_Nama_akaun='" + TextBox2.Text + "' ");
                if (ddokdicno.Rows.Count == 0)
                {
                    DataTable get_val = new DataTable();
                    get_val = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='" + dd_akaun.SelectedValue + "'");
                    //string Inssql1 = "insert into KW_Ref_Carta_Akaun(kat_akaun,nama_akaun,kod_akaun,jenis_akaun,under_parent,KW_Debit_amt,KW_kredit_amt,Kw_open_amt,jenis_akaun_type,under_jenis,Status,crt_id,cr_dt,Susu_nilai,kod_akaun_cnt) values ('" + get_val.Rows[0]["kat_akaun"].ToString() + "','" + TextBox2.Text.Replace("'", "''") + "','" + chk_role + "','" + dd_akaun.SelectedValue + "','" + set_cnt1 + "','0.00','0.00','0.00','" + mcnt + "','','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0','" + cnt_value + "')";
                    //Status = DBCon.Ora_Execute_CommamdText(Inssql1);

                    //if (Status == "SUCCESS")
                    //{
                    string Inssql = "insert into KW_Ref_Akaun_bank(Ref_nama_bank,Ref_No_akaun,Ref_Nama_akaun,Ref_Alamat_akaun,jenis_kod_akaun,Ref_kod_akaun,Status,crt_id,cr_dt) values ('" + TextBox1.Text.Replace("'", "''") + "','" + TextBox5.Text.Replace("'", "''") + "','" + TextBox2.Text.Replace("'", "''") + "','" + alamat_akn.Value.Replace("'", "''") + "','" + get_val.Rows[0]["jenis_akaun"].ToString() + "','" + dd_akaun.SelectedValue + "','" + sts.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                    Session["validate_success"] = "SUCCESS";
                    Response.Redirect("../kewengan/kw_akaun_bank_view.aspx");
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Ada.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else

            {
                //DataTable ddokdicno = new DataTable();
                //ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_Akaun_bank where Id = '" + get_id.Text + "'");
                //if (ddokdicno.Rows[0]["jenis_kod_akaun"].ToString() != dd_akaun.SelectedValue)
                //{
                //    DataTable get_val1 = new DataTable();
                //    get_val1 = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='" + ddokdicno.Rows[0]["Ref_kod_akaun"].ToString() + "'");
                //    if (get_val1.Rows.Count != 0)
                //    {
                //        DataTable get_val11 = new DataTable();
                //        get_val11 = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where jenis_akaun='" + ddokdicno.Rows[0]["Ref_kod_akaun"].ToString() + "'");
                //        if (get_val11.Rows.Count == 0)
                //        {
                //            string Inssql2 = "delete from KW_Ref_Carta_Akaun where kod_akaun = '" + ddokdicno.Rows[0]["Ref_kod_akaun"].ToString() + "'";
                //            Status_del = DBCon.Ora_Execute_CommamdText(Inssql2);
                //            if (Status_del == "SUCCESS")
                //            {
                //                DataTable get_val = new DataTable();
                //                get_val = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='" + dd_akaun.SelectedValue + "'");
                //                string Inssql1 = "insert into KW_Ref_Carta_Akaun(kat_akaun,nama_akaun,kod_akaun,jenis_akaun,under_parent,KW_Debit_amt,KW_kredit_amt,Kw_open_amt,jenis_akaun_type,under_jenis,Status,crt_id,cr_dt,Susu_nilai,kod_akaun_cnt,sts_kawalan) values ('" + get_val.Rows[0]["kat_akaun"].ToString() + "','" + TextBox1.Text.Replace("'", "''") + "','" + chk_role + "','" + dd_akaun.SelectedValue + "','" + set_cnt1 + "','0.00','0.00','0.00','" + mcnt + "','','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0','" + cnt_value + "','Y')";
                //                Status2 = DBCon.Ora_Execute_CommamdText(Inssql1);
                //            }
                //            else
                //            {
                //                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Dihapus Tidak Mungkin.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                //            }
                //        }
                //        else
                //        {
                //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Dihapus Tidak Mungkin.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                //        }
                //    }

                //}
                //else
                //{
                //    string Inssql1 = "Update KW_Ref_Carta_Akaun set nama_akaun='" + TextBox1.Text.Replace("'", "''") + "' where kod_akaun='" + ddokdicno.Rows[0]["Ref_kod_akaun"].ToString() + "'";
                //    Status2 = DBCon.Ora_Execute_CommamdText(Inssql1);
                //}
                //string ckod = string.Empty;
                //if (Status2 == "SUCCESS")
                //{

                //    if (ddokdicno.Rows[0]["jenis_kod_akaun"].ToString() != dd_akaun.SelectedValue)
                //    {
                //        ckod = "Ref_kod_akaun='" + chk_role + "',jenis_kod_akaun='" + dd_akaun.SelectedValue + "',";
                //    }
                //    else
                //    {
                //        ckod = "";
                //    }
                DataTable get_val = new DataTable();
                get_val = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='" + dd_akaun.SelectedValue + "'");

                string Inssql = "UPDATE KW_Ref_Akaun_bank set jenis_kod_akaun='" + get_val.Rows[0]["jenis_akaun"].ToString() + "',Ref_kod_akaun='" + dd_akaun.SelectedValue + "',Ref_nama_bank='" + TextBox1.Text.Replace("'", "''") + "',Ref_No_akaun='" + TextBox5.Text.Replace("'", "''") + "',Ref_Nama_akaun='" + TextBox2.Text.Replace("'", "''") + "',Ref_Alamat_akaun='" + alamat_akn.Value.Replace("'", "''") + "',Status='" + sts.SelectedValue + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id = '" + get_id.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);

                Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                Session["validate_success"] = "SUCCESS";
                Response.Redirect("../kewengan/kw_akaun_bank_view.aspx");

            }
        }
        else
        {
            if(ver_id.Text == "1")
            {
                Button4.Text = "Kemaskini";
            }
            else
            {
                Button4.Text = "Simpan";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_akaun_bank.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_akaun_bank_view.aspx");
    }

    
}