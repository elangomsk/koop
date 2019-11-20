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


public partial class PP_Guaman_penjamin : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    string level, userid, stscd, abc1;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp_req = Request.UrlReferrer;
                if (samp_req != null)
                {
                    ViewState["ReferrerUrl"] = Request.UrlReferrer.ToString();
                }
                else
                {
                    ViewState["ReferrerUrl"] = "";
                }
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                txtname.Attributes.Add("readonly", "readonly");
                txtnokp.Attributes.Add("readonly", "readonly");
                //txtcawa.Attributes.Add("readonly", "readonly");
                //txtwila.Attributes.Add("readonly", "readonly");
                txtamaun.Attributes.Add("readonly", "readonly");
                txttemp.Attributes.Add("readonly", "readonly");

                txttp5.Attributes.Add("readonly", "readonly");
                txttpt4.Attributes.Add("readonly", "readonly");


                defendan();
                penghukuman();
                tindakan();
                //Button5.Enabled = false;
                Button5.Visible = false;
                var samp = Request.Url.Query;

                if (samp != "")
                {
                    txtappno.Text = Request.QueryString["spi_icno"].ToString();
                    srch();
                }
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select app_applcn_no from jpa_application JA Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no where app_applcn_no like '%' + @Search + '%' and JJA.jkk_result_ind='L' and JA.applcn_clsed ='N'";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    if (sdr.HasRows == true)
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["app_applcn_no"].ToString());

                        }
                    }
                    else
                    {
                        countryNames.Add("Rekod Tidak Dijumpai.");
                    }
                }

                con.Close();
                return countryNames;
            }
        }
    }
    void defendan()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select defendan_cd,defendan_desc from ref_defendan where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlkd1.DataSource = dt;
            ddlkd1.DataBind();
            ddlkd1.DataTextField = "defendan_desc";
            ddlkd1.DataValueField = "defendan_cd";
            ddlkd1.DataBind();
            ddlkd1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void penghukuman()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select penghukuman_cd,penghukuman_desc from ref_penghukuman where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddljt2.DataSource = dt;
            ddljt2.DataBind();
            ddljt2.DataTextField = "penghukuman_desc";
            ddljt2.DataValueField = "penghukuman_cd";
            ddljt2.DataBind();
            ddljt2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void tindakan()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select tindakan_cd,tindakan_desc from ref_tindakan where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlpen.DataSource = dt;
            ddlpen.DataBind();
            ddlpen.DataTextField = "tindakan_desc";
            ddlpen.DataValueField = "tindakan_cd";
            ddlpen.DataBind();
            ddlpen.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridView1, "Select$" + e.Row.RowIndex);
        }
    }

    protected void btnsrch_Click(object sender, EventArgs e)
    {

        srch();
    }

    void grid()
    {
        SqlCommand cmd2 = new SqlCommand("select rd.defendan_desc,penghukuman_desc,leg_lawyer_name,leg_apply_dt,leg_court_dt,leg_case_no,tindakan_desc,leg_court_result from jpa_legal_action jl left join jpa_application as ja on ja.app_new_icno=jl.leg_applcn_no left join ref_defendan as rd on rd.defendan_cd=jl.leg_plaintif_cat_cd left join ref_penghukuman as rp on rp.penghukuman_cd=jl.leg_legal_action_cd left join ref_tindakan as rt on rt.tindakan_cd=jl.leg_action_type_cd where leg_applcn_no='" + txtappno.Text + "' ", con1);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<strong><center>Maklumat Carian Tidak Dijumpai</center></strong>";
        }
        else
        {
            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }
    }

    protected void srch()
    {
        try
        {
            if (txtappno.Text != "")
            {

                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select ja.app_name,ja.app_new_icno,rc.branch_desc,rw.Wilayah_Name,ja.app_loan_amt,ja.appl_loan_dur,ja.app_applcn_no from jpa_application as ja left join jpa_jkkpa_approval as jk on jk.jkk_applcn_no=ja.app_applcn_no left join ref_branch as rc on rc.branch_cd=ja.app_branch_cd left join ref_wilayah as rw on rw.Wilayah_Code=ja.app_region_cd  where app_applcn_no='" + txtappno.Text + "' and jkk_result_ind='L'");
                if (ddokdicno.Rows.Count != 0)
                {
                    string appno;
                    
                    txtname.Text = ddokdicno.Rows[0][0].ToString();
                    txtnokp.Text = ddokdicno.Rows[0][1].ToString();
                    //txtcawa.Text = ddokdicno.Rows[0][2].ToString();
                    //txtwila.Text = ddokdicno.Rows[0][3].ToString();
                    txtamaun.Text = Convert.ToDecimal(ddokdicno.Rows[0][4]).ToString("C").Replace("RM","").Replace("$", "");
                    txttemp.Text = ddokdicno.Rows[0][5].ToString();
                    appno = ddokdicno.Rows[0][6].ToString();
                    SqlCommand cmd2 = new SqlCommand("select rd.defendan_desc,penghukuman_desc,leg_lawyer_name,leg_apply_dt,leg_court_dt,leg_case_no,tindakan_desc,leg_court_result from jpa_legal_action jl left join jpa_application as ja on ja.app_new_icno=jl.leg_applcn_no left join ref_defendan as rd on rd.defendan_cd=jl.leg_plaintif_cat_cd left join ref_penghukuman as rp on rp.penghukuman_cd=jl.leg_legal_action_cd left join ref_tindakan as rt on rt.tindakan_cd=jl.leg_action_type_cd where leg_applcn_no='" + appno + "' ", con1);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2);
                    if (ds2.Tables[0].Rows.Count == 0)
                    {

                        ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
                        GridView1.DataSource = ds2;
                        GridView1.DataBind();
                        int columncount = GridView1.Rows[0].Cells.Count;
                        GridView1.Rows[0].Cells.Clear();
                        GridView1.Rows[0].Cells.Add(new TableCell());
                        GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                        GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
                    }
                    else
                    {
                        GridView1.DataSource = ds2;
                        GridView1.DataBind();
                    }
                   


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No IC.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PP_Guaman_penjamin.aspx");
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {

        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label2");
        string abc = lblTitle.Text;

        if (abc == "Peminjam")
        {
            abc1 = "PJ";
        }
        else if (abc == "Penjamin 1")
        {
            abc1 = "P1";
        }
        else if (abc == "Penjamin 2")
        {
            abc1 = "P2";
        }
        else if (abc == "Penjamin 3")
        {
            abc1 = "P3";
        }

        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select leg_applcn_no,leg_plaintif_cat_cd from jpa_legal_action where leg_applcn_no='" + txtappno.Text + "'");
        stscd = ddokdicno.Rows[0][1].ToString();
        if (ddokdicno.Rows.Count != 0)
        {
            string appno;
            appno = ddokdicno.Rows[0][0].ToString();
            DataTable ddokdicno1 = new DataTable();
            ddokdicno1 = DBCon.Ora_Execute_table("select leg_plaintif_cat_cd,leg_legal_action_cd,leg_lawyer_name,leg_apply_dt,leg_court_dt,leg_case_no,leg_action_type_cd,leg_court_result from jpa_legal_action jl left join jpa_application as ja on ja.app_new_icno=jl.leg_applcn_no where leg_applcn_no='" + appno + "' and leg_plaintif_cat_cd='" + abc1 + "'");
            if (ddokdicno1.Rows.Count != 0)
            {
                ddlkd1.SelectedValue = ddokdicno1.Rows[0][0].ToString();
                ddljt2.SelectedValue = ddokdicno1.Rows[0][1].ToString();
                txtpp3.Text = ddokdicno1.Rows[0][2].ToString();
                txttpt4.Text = Convert.ToDateTime(ddokdicno1.Rows[0][3]).ToString("dd/MM/yyyy");
                txttp5.Text = Convert.ToDateTime(ddokdicno1.Rows[0][4]).ToString("dd/MM/yyyy");
                txtndk.Text = ddokdicno1.Rows[0][5].ToString();
                ddlpen.SelectedValue = ddokdicno1.Rows[0][6].ToString();
                textarea1.Value = ddokdicno1.Rows[0][7].ToString();
                Button5.Visible = true;
                Button2.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        Button2.Visible = false;
        Button1.Visible = true;
        //Button3.Visible = false;
        Button5.Visible = true;
    }

    protected void btnkmes_Click(object sender, EventArgs e)
    {
        if (ddlkd1.SelectedValue != "" && ddljt2.SelectedValue != "" && txtpp3.Text != "" && txttpt4.Text != "" && txttp5.Text != "" && txtndk.Text != "" && textarea1.Value != "" && ddlpen.SelectedValue != "")
        {

            DataTable ddokdicno2 = new DataTable();
            ddokdicno2 = DBCon.Ora_Execute_table("select leg_applcn_no,leg_plaintif_cat_cd from jpa_legal_action  where leg_applcn_no='" + txtappno.Text + "' and leg_plaintif_cat_cd='" + ddlkd1.SelectedValue + "'");
            if (ddokdicno2.Rows.Count != 0)
            {

                string fdate = txttpt4.Text;
                DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                String fmdate = ft.ToString("mm/dd/yyyy");
                string fdate1 = txttp5.Text;
                DateTime ft1 = DateTime.ParseExact(fdate1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                String fmdate1 = ft1.ToString("mm/dd/yyyy");
                DataTable ddokdicno3 = new DataTable();
                ddokdicno3 = DBCon.Ora_Execute_table("update jpa_legal_action set leg_plaintif_cat_cd='" + ddlkd1.SelectedValue.ToUpper() + "',leg_legal_action_cd='" + ddljt2.SelectedValue.ToUpper() + "',leg_lawyer_name='" + txtpp3.Text.ToUpper() + "',leg_apply_dt='" + fmdate + "',leg_court_dt='" + fmdate1 + "',leg_case_no='" + txtndk.Text.ToUpper() + "',leg_action_type_cd='" + ddlpen.SelectedValue.ToUpper() + "',leg_upd_id='" + Session["New"].ToString() + "',leg_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',leg_court_result='" + textarea1.Value.ToUpper() + "' where leg_applcn_no='" + txtappno.Text + "' and leg_plaintif_cat_cd='" + ddlkd1.SelectedValue.ToUpper() + "'");
                //DataTable ddaudit = new DataTable();
                //ddaudit = DBCon.Ora_Execute_table("insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc)values('" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','031501','TINDAKAN LITIGASI - PENJAMIN '");
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                ddlkd1.SelectedValue = "";
                ddljt2.SelectedValue = "";
                txtpp3.Text = "";
                txttpt4.Text = "";
                txttp5.Text = "";
                txtndk.Text = "";
                textarea1.Value = "";
                ddlpen.SelectedValue = "";
                Button5.Visible = false;
                Button2.Visible = true;
                
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        grid();
    }
    protected void btnsmmit_Click(object sender, EventArgs e)
    {
        if (ddlkd1.SelectedValue != "" && ddljt2.SelectedValue != "" && txtpp3.Text != "" && txttpt4.Text != "" && txttp5.Text != "" && txtndk.Text != "" && textarea1.Value != "" && ddlpen.SelectedValue != "")
        {
            DataTable ddokdicno2 = new DataTable();
            ddokdicno2 = DBCon.Ora_Execute_table("select leg_applcn_no from jpa_legal_action  where leg_applcn_no='" + txtappno.Text + "' and leg_plaintif_cat_cd='" + ddlkd1.SelectedValue + "'");
            if (ddokdicno2.Rows.Count == 0)
            {
                string fdate = txttpt4.Text;
                DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                String fmdate = ft.ToString("mm/dd/yyyy");
                string fdate1 = txttp5.Text;
                DateTime ft1 = DateTime.ParseExact(fdate1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                String fmdate1 = ft1.ToString("mm/dd/yyyy");
                DataTable ddokdicno3 = new DataTable();
                ddokdicno3 = DBCon.Ora_Execute_table("insert into jpa_legal_action (leg_applcn_no,leg_plaintif_cat_cd,leg_legal_action_cd,leg_lawyer_name,leg_apply_dt,leg_court_dt,leg_case_no,leg_court_result,leg_action_type_cd,leg_crt_id,leg_crt_dt)values('" + txtappno.Text + "','" + ddlkd1.SelectedValue.ToUpper() + "','" + ddljt2.SelectedValue.ToUpper() + "','" + txtpp3.Text.ToUpper() + "','" + fmdate + "','" + fmdate1 + "','" + txtndk.Text.ToUpper() + "','" + textarea1.Value.ToUpper() + "','" + ddlpen.SelectedValue.ToUpper() + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                //DataTable ddaudit = new DataTable();
                //ddaudit = DBCon.Ora_Execute_table("insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc)values('" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','031501','TINDAKAN LITIGASI - PENJAMIN'");
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                ddlkd1.SelectedValue = "";
                ddljt2.SelectedValue = "";
                txtpp3.Text = "";
                txttpt4.Text = "";
                txttp5.Text = "";
                txtndk.Text = "";
                textarea1.Value = "";
                ddlpen.SelectedValue = "";                            

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
               
               
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        grid();
    }

    protected void click_batal(object sender, EventArgs e)
    {
        Session["sess_no"] = txtappno.Text;
        object referrer = ViewState["ReferrerUrl"];
        if (Session["sess_no"] != "")
            Response.Redirect(referrer.ToString());
    }

  
}