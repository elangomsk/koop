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
using System.Text.RegularExpressions;
using System.Threading;


public partial class kw_diskaun_view : System.Web.UI.Page
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
                    Session["validate_success"] = "";
                    Session["alrt_msg"] = "";
                }
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('732','705','133','39')");

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
    protected void BindData()
    {
        string sqry = string.Empty;
        //if (txtSearch.Text == "")
        //{
        //    sqry = "";
        //}
        //else
        //{
        //    sqry = "where s1.Ref_nama_cukai LIKE'%" + txtSearch.Text + "%' OR s1.Ref_kod_cukai LIKE'%" + txtSearch.Text + "%'";
        //}
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select s1.id,s1.Ref_nama_diskaun,s1.Ref_kod_diskaun,s1.Ref_kadar,s1.Ref_kod_akaun,case when s1.tc_jenis = 'PEN' then 'PENERIMAAN' when s1.tc_jenis = 'PEM' then 'PEMBAYARAN' else 'OTHERS' end as jenis,s2.Ref_Jenis_cukai,s3.nama_akaun,s1.Ref_tarikh_mula,s1.Ref_tarikh_akhir,s1.Ref_klasi_akaun,s1.Ref_descripsi,case when s1.Status='A' then 'AKTIF' else 'TIDAK AKTIF' end as sts from KW_Ref_Diskaun s1 left join KW_Ref_Jenis_Cukai s2 on s2.Id=s1.Ref_jenis_cukai left join KW_Ref_Carta_Akaun s3 on s3.kod_akaun=s1.Ref_kod_akaun " + sqry + " order by s1.ID desc", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gv_refdata.DataSource = ds;
            gv_refdata.DataBind();
            int columncount = gv_refdata.Rows[0].Cells.Count;
            gv_refdata.Rows[0].Cells.Clear();
            gv_refdata.Rows[0].Cells.Add(new TableCell());
            gv_refdata.Rows[0].Cells[0].ColumnSpan = columncount;
            gv_refdata.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {
            gv_refdata.DataSource = ds;
            gv_refdata.DataBind();
        }

        con.Close();
    }

    //protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gv_refdata.PageIndex = e.NewPageIndex;
    //    gv_refdata.DataBind();
    //    BindData();
    //}

    //protected void txtSearch_TextChanged(object sender, EventArgs e)
    //{

    //    SearchText();
    //}

    //protected void btn_search_Click(object sender, EventArgs e)
    //{
    //    BindData();
    //}

  

    //public string Highlight(string InputTxt)
    //{
    //    string Search_Str = txtSearch.Text.ToString();
    //    // Setup the regular expression and add the Or operator.
    //    Regex RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(),
    //    RegexOptions.IgnoreCase);

    //    // Highlight keywords by calling the 
    //    //delegate each time a keyword is found.
    //    return RegExp.Replace(InputTxt,
    //    new MatchEvaluator(ReplaceKeyWords));

    //    // Set the RegExp to null.
    //    RegExp = null;

    //}

    //public string ReplaceKeyWords(Match m)
    //{

    //    return "<span class=highlight>" + m.Value + "</span>";

    //}
    //private void SearchText()
    //{
    //    BindData();

    //}
    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_diskaun.aspx");
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
                var rbn = row.FindControl("RadioButton1") as System.Web.UI.WebControls.CheckBox;
                if (rbn.Checked)
                {
                    int RowIndex = row.RowIndex;
                    string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl_id")).Text.ToString(); //this store the  value in varName1
                    SqlCommand ins_peng = new SqlCommand("Delete from KW_Ref_diskaun where id='" + varName1 + "'", conn);
                    conn.Open();
                    int i = ins_peng.ExecuteNonQuery();
                    conn.Close();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

        string script = " $(function () {$(" + gv_refdata.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        BindData();
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl_id");
            string lblid = lblTitle.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_diskaun where Id='" + lblid + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(lblTitle.Text));
                //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
                Response.Redirect(string.Format("../kewengan/kw_diskaun.aspx?edit={0}", name));
            }
            else
            {
                Session["validate_success"] = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}