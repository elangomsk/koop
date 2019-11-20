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


public partial class kw_sebut_harga_view : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string CommandArgument1;
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
                userid = Session["New"].ToString();
                Bindbecut();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('784','705','39')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            Buttton5.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    //protected void txtSearch_TextChanged(object sender, EventArgs e)
    //{

    //    SearchText();

    //}

    //protected void btn_search_Click(object sender, EventArgs e)
    //{
    //    Bindbecut();
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
    //    Bindbecut();
    //}


    protected void Bindbecut()
    {
        string sqry = string.Empty;
        //if (txtSearch.Text == "")
        //{
        //    sqry = "";
        //}
        //else
        //{
        //    sqry = "where no_baucer LIKE'%" + txtSearch.Text + "%'";
        //}
        qry1 = "select  no_baucer, FORMAT(tarkih_mula,'dd/MM/yyyy') tarkih_mula,FORMAT(tarkih_akhir,'dd/MM/yyyy') tarkih_akhir,nama_sebut, jumlah_amt as jumlah_amt from KW_Sebut_harga "+ sqry + "";
        SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            Gridview1.DataSource = ds2;
            Gridview1.DataBind();
            int columncount = Gridview1.Rows[0].Cells.Count;
            Gridview1.Rows[0].Cells.Clear();
            Gridview1.Rows[0].Cells.Add(new TableCell());
            Gridview1.Rows[0].Cells[0].ColumnSpan = columncount;
            Gridview1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            Gridview1.DataSource = ds2;
            Gridview1.DataBind();
        }
    }
    protected void lblSubItemName_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            string[] CommadArgument = btn.CommandArgument.Split(',');
            CommandArgument1 = CommadArgument[0];
            DataTable dt = new DataTable();
            dt = DBCon.Ora_Execute_table("select  no_baucer,nama_sebut, FORMAT(tarkih_mula,'dd/MM/yyyy') tarkih_mula,FORMAT(tarkih_akhir,'dd/MM/yyyy') tarkih_akhir,tajuk,produk,deskripsi,quantiti,format(harga_amt,'##,###.00') harga_amt,diskaun,akaun,cukai,format(jumlah_amt,'##,###.00')jumlah_amt,jumlah_keseluruhan from KW_Sebut_harga where no_baucer='" + CommandArgument1 + "'");

            if (dt.Rows.Count > 0)
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(CommandArgument1));
                Response.Redirect(string.Format("../kewengan/kw_sebut_harga.aspx?edit={0}", name));
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

    //protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    Gridview1.PageIndex = e.NewPageIndex;
    //    Gridview1.DataBind();
    //    Bindbecut();
    //}
    
    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_sebut_harga.aspx");
    }

    
}