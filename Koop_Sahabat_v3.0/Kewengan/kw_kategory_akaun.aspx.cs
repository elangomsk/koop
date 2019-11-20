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

public partial class kw_kategory_akaun : System.Web.UI.Page
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
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
           
            if (Session["New"] != null)
            {
                
                userid = Session["New"].ToString();
                btb_kmes.Visible = false;
                if (DropDownList1.SelectedValue == "01")
                {
                    ss1.Visible = true;
                }
                else
                {
                    ss1.Visible = false;
                }

                jenis_akaun();
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                }

            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void jenis_akaun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kat_cd,kat_desc from kw_ref_report_1 where kat_sts='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "kat_desc";
            DropDownList2.DataValueField = "kat_cd";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void app_language()

    {
        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('711','705','1621','712','711','1622','714','1486','61','35','15','77','1623')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            //ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());                   
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
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

    void view_details()
    {
        try
        {
            
            btb_kmes.Visible = true;
            Button4.Visible = false;
            Button1.Visible = false;
            txt_kat_cd.Attributes.Add("Readonly", "Readonly");
            string ogid = lbl_name.Text;
            gen_id.Text = lbl_name.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = Dblog.Ora_Execute_table("select * from KW_Kategori_akaun where id = '" + ogid + "'");
            if (ddokdicno.Rows.Count != 0)
            {
                DropDownList1.SelectedValue = ddokdicno.Rows[0]["kat_type"].ToString();
                txt_kat_cd.Text = ddokdicno.Rows[0]["kat_cd"].ToString();
                txt_kat_akaun.Text = ddokdicno.Rows[0]["kat_akuan"].ToString();
                txt_deskrip.Text = ddokdicno.Rows[0]["deskripsi"].ToString();
                DDL_bal_type.SelectedValue = ddokdicno.Rows[0]["bal_type"].ToString();
                dd_list_sts.SelectedValue = ddokdicno.Rows[0]["Status"].ToString();
                DropDownList2.SelectedValue = ddokdicno.Rows[0]["kat_rpt_kk"].ToString();
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

    protected void Button4_Click(object sender, EventArgs e)
    {
        if (txt_kat_akaun.Text != "" && txt_kat_cd.Text != "")
        {
            DataTable ddokdicno = new DataTable();
            ddokdicno = Dblog.Ora_Execute_table("select * from KW_Kategori_akaun where kat_type='" + DropDownList1.SelectedValue + "' and kat_cd = '" + txt_kat_cd.Text + "'");
            if (ddokdicno.Rows.Count == 0)
            {
                string kdakaun = string.Empty;
                string Inssql1 = string.Empty;
                string Inssql = "insert into KW_Kategori_akaun (kat_cd,kat_akuan,deskripsi,bal_type,Status,crt_id,cr_dt,kat_type,kat_rpt_kk)values('" + txt_kat_cd.Text.Replace("'", "''") + "','" + txt_kat_akaun.Text.Replace("'", "''") + "','" + txt_deskrip.Text.Replace("'", "''") + "','" + DDL_bal_type.SelectedValue + "','" + dd_list_sts.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DropDownList1.SelectedValue + "','" + DropDownList2.SelectedValue + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    kdakaun = txt_kat_cd.Text.Replace("'", "''");
                    if (DropDownList1.SelectedValue == "01")
                    {
                        Inssql1 = "insert into KW_Ref_Carta_Akaun(kat_akaun,nama_akaun,kod_akaun,jenis_akaun,under_parent,KW_Debit_amt,KW_kredit_amt,Kw_open_amt,jenis_akaun_type,under_jenis,Status,crt_id,cr_dt) values ('" + txt_kat_cd.Text.Replace("'", "''") + "','" + txt_kat_akaun.Text.Replace("'", "''") + "','" + kdakaun + "','','0','0.00','0.00','0.00','1','','" + dd_list_sts.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                    }
                    else
                    {
                        Inssql1 = "insert into KW_Ref_kod_bajet(kat_bajet,nama_bajet,kod_bajet,jenis_bajet,bjt_under_parent,bjt_Debit_amt,bjt_kredit_amt,bjt_open_amt,jenis_bajet_type,bjt_under_jenis,bjt_Status,bjt_crt_id,bjt_cr_dt) values ('" + txt_kat_cd.Text.Replace("'", "''") + "','" + txt_kat_akaun.Text.Replace("'", "''") + "','" + kdakaun + "','','0','0.00','0.00','0.00','1','','" + dd_list_sts.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                    }
                    if (Status == "SUCCESS")
                    {
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Session["validate_success"] = "SUCCESS";
                        Response.Redirect("../kewengan/kw_kategory_akaun_view.aspx");
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void btb_kmes_Click(object sender, EventArgs e)
    {
        if (txt_kat_akaun.Text != "" && txt_kat_cd.Text != "")
        {
            DataTable sel_kat = new DataTable();
            sel_kat = DBCon.Ora_Execute_table("select * from KW_General_Ledger where kat_akaun='" + txt_kat_cd.Text.Replace("'", "''") + "' and YEAR(GL_process_dt)='" + DateTime.Now.Year + "'");
            string sts1 = string.Empty;
            if (sel_kat.Rows.Count != 0)
            {
                if (dd_list_sts.SelectedValue == "T")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Status Dikemaskini Tidak Kemungkinan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
                sts1 = "A";
            }
            else
            {
                sts1 = dd_list_sts.SelectedValue;
            }


            string Inssql = "update KW_Kategori_akaun set kat_rpt_kk='" + DropDownList2.SelectedValue + "',kat_type='" + DropDownList1.SelectedValue +"',kat_cd='" + txt_kat_cd.Text.Replace("'", "''") + "',kat_akuan='" + txt_kat_akaun.Text.Replace("'", "''") + "',deskripsi='" + txt_deskrip.Text.Replace("'", "''") + "',Status='" + sts1 + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "',bal_type='" + DDL_bal_type.SelectedValue + "',upd_id='" + Session["New"].ToString() + "' where Id='" + gen_id.Text + "'";
            Status = DBCon.Ora_Execute_CommamdText(Inssql);
            if (Status == "SUCCESS")
            {
                string Inssql1 = string.Empty;
                string Inssql2 = string.Empty;
                if (sel_kat.Rows.Count == 0)
                {
                    if (DropDownList1.SelectedValue == "01")
                    {
                        Inssql1 = "update KW_Ref_Carta_Akaun set nama_akaun='" + txt_kat_akaun.Text.Replace("'", "''") + "',Status='" + dd_list_sts.SelectedValue + "' where kod_akaun='" + txt_kat_cd.Text.Replace("'", "''") + "'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                        Inssql2 = "update KW_Ref_Carta_Akaun set Status='" + sts1 + "' where kat_akaun='" + txt_kat_cd.Text.Replace("'", "''") + "'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql2);
                    }
                    else
                    {
                        Inssql1 = "update KW_Ref_kod_bajet set nama_bajet='" + txt_kat_akaun.Text.Replace("'", "''") + "',bjt_Status='" + dd_list_sts.SelectedValue + "' where kod_bajet='" + txt_kat_cd.Text.Replace("'", "''") + "'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                        Inssql2 = "update KW_Ref_kod_bajet set bjt_Status='" + sts1 + "' where kat_bajet='" + txt_kat_cd.Text.Replace("'", "''") + "'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql2);
                    }
                }
                txt_kat_cd.Attributes.Remove("Readonly");
                Session["validate_success"] = "SUCCESS";
                Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                Response.Redirect("../kewengan/kw_kategory_akaun_view.aspx");
            }
            
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        Button4.Visible = true;
        btb_kmes.Visible = false;
    }


    protected void katcd_TextChanged(object sender, EventArgs e)
    {
        DataTable sel_kat = new DataTable();
        sel_kat = DBCon.Ora_Execute_table("select * from KW_Kategori_akaun where kat_type='"+ DropDownList1.SelectedValue +"' and kat_cd = '" + txt_kat_cd.Text + "' order by kat_cd asc");
        if (sel_kat.Rows.Count != 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Kategori Kod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            txt_kat_cd.Text = "";
            txt_kat_cd.Focus();
        }
        else
        {
            txt_kat_akaun.Focus();
        }

       
    }
    protected void katcd_TextChanged1(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "01")
        {
            ss1.Visible = true;
        }
        else
        {
            ss1.Visible = false;
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_kategory_akaun.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_kategory_akaun_view.aspx");
    }

    
}