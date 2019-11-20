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



public partial class kw_profil_syarikat_view : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
               
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    
                }
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";
                Session["con_id"] = "";
                userid = Session["New"].ToString();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1615','705','133','39')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());            
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            confirmValue = Request.Form["confirm_value"];
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label og_genid = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label1");
            Session["con_id"] = confirmValue;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * from KW_Profile_syarikat where id = '" + og_genid.Text + "'");
            if (ddokdicno.Rows.Count != 0)
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(og_genid.Text));
                //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
                Response.Redirect(string.Format("../kewengan/kw_profil_syarikat.aspx?edit={0}", name));
            }
            else
            {
                
                Session["con_id"] = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

   
    protected void BindData()
    {
      
        string sqry = string.Empty;
        //if (srch_id.Text == "")
        //{
        //    sqry = "where status='A'";
        //}
        //else
        //{
        //    sqry = "where nama_syarikat like '%" + srch_id.Text + "%' and status='A'";
        //}

        qry1 = "select a.*,b.fin_year,case when b.Status = '1' then 'OPEN' when b.Status = '0' then 'CLOSE' when b.Status = '2' then 'TRIAL' else '' end as sts from (select kod_syarikat,nama_syarikat,alamat_syarikat,tarikh_mula,tarikh_akhir,id,case when cur_sts = '1' then 'OPEN' else 'CLOSE' end as sts1,cr_dt from KW_Profile_syarikat where status='A' ) as a outer apply(select * from kw_financial_year where fin_kod_syarikat=a.kod_syarikat and Status='1') as b  order by a.cr_dt";
        SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            gv_refdata.DataSource = ds2;
            gv_refdata.DataBind();
            int columncount = gv_refdata.Rows[0].Cells.Count;
            gv_refdata.Rows[0].Cells.Clear();
            gv_refdata.Rows[0].Cells.Add(new TableCell());
            gv_refdata.Rows[0].Cells[0].ColumnSpan = columncount;
            gv_refdata.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            gv_refdata.DataSource = ds2;
            gv_refdata.DataBind();
        }
    }
    //protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gv_refdata.PageIndex = e.NewPageIndex;
    //    BindData();

    //}
    //protected void srch_id_TextChanged(object sender, EventArgs e)
    //{
    //    BindData();
       
    //}

    //protected void btn_search_Click(object sender, EventArgs e)
    //{
    //    BindData();

    //}
    //string Form2;

    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_profil_syarikat.aspx");
    }

    protected void btn_hups_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in gv_refdata.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.CheckBox;
            if (rb.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in gv_refdata.Rows)
            {
                System.Web.UI.WebControls.CheckBox rbn = new System.Web.UI.WebControls.CheckBox();
                rbn = (System.Web.UI.WebControls.CheckBox)row.FindControl("RadioButton1");
                if (rbn.Checked)
                {
                    int RowIndex = row.RowIndex;
                    string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("Label1")).Text.ToString(); //this store the  value in varName1

                    DataTable ddokdicno01 = new DataTable();
                    ddokdicno01 = DBCon.Ora_Execute_table("select syar_logo,tarikh_mula,tarikh_akhir,syar_Cd from KW_Profile_syarikat where id = '" + varName1 + "'");
                    string checkimage01 = ddokdicno01.Rows[0]["syar_logo"].ToString();
                    DateTime smp1 = DateTime.Parse(ddokdicno01.Rows[0]["tarikh_mula"].ToString());
                    //SqlCommand prof_del = new SqlCommand("update KW_financial_Year set Status='T' where fin_start_dt>'" + smp1.Date.ToString("MM/dd/yyyy") + "' and fin_Syar_Cd='" + ddokdicno01.Rows[0]["syar_Cd"] + "'", con);
                    //SqlCommand ins_peng = new SqlCommand("Delete from KW_Profile_syarikat where id='" + varName1 + "'", con);
                    SqlCommand ins_peng = new SqlCommand("update KW_Profile_syarikat set Status='T' where id='" + varName1 + "'", con);

                    con.Open();
                    int i = ins_peng.ExecuteNonQuery();
                    con.Close();

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'success','auto_close': 2000}); ", true);
                    //BindData();

                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000}); ", true);
        }
        string script = " $(function () {$(" + gv_refdata.ClientID + ").prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        BindData();
    }
}