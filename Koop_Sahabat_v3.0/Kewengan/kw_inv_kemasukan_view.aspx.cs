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


public partial class kw_inv_kemasukan_view : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string level;
    string Status = string.Empty, Status1 = string.Empty;
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1814','705','133','39','1815')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());         
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());

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
        //    sqry = "where Status='A' and closing_sts='0'";
        //}
        //else
        //{
        //    sqry = "where Status='A' and closing_sts='0' and s1.kod_barang LIKE'%" + txtSearch.Text + "%' OR s1.jenis_barang LIKE'%" + txtSearch.Text + "%'";
        //}
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select s1.id,s1.kod_barang,s1.jenis_barang,s1.keterangan,FORMAT(s1.do_tarikh,'dd/MM/yyyy', 'en-us') as do_tarikh,s1.kuantiti,s1.unit,s1.gst_amaun,s1.gst_amaun_jum,s1.baki_kuantiti,s1.jumlah,s1.jumlah_baki,case when s1.ki_uom='01' then 'UNIT'  when s1.ki_uom='02' then 'KILO GRAM' else '' end as uom from KW_kemasukan_inventori s1 where Status='A' and closing_sts='0' order by s1.kod_barang,s1.do_tarikh Asc", con);
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        System.Web.UI.WebControls.Label kodbarang = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl1");
        System.Web.UI.WebControls.Label baki = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl9");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable get_value = new DataTable();
            get_value = DBCon.Ora_Execute_table("select kod_barang,cast(ISNULL(threshold,'0') as int) threshold from KW_INVENTORI_BARANG where kod_barang='" + kodbarang.Text + "'");

            if (get_value.Rows.Count != 0)
            {
                if (double.Parse(get_value.Rows[0]["threshold"].ToString()) > double.Parse(baki.Text))
                {
                    e.Row.ForeColor = Color.FromName("Red");
                }
            }
        }
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
        Response.Redirect("../kewengan/kw_inv_kemasukan.aspx");
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
            ddokdicno = DBCon.Ora_Execute_table("select * From KW_kemasukan_inventori where Id='" + lblid + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(lblTitle.Text));
                Response.Redirect(string.Format("../kewengan/kw_inv_kemasukan.aspx?edit={0}", name));
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


    protected void btn_closing_acc(object sender, EventArgs e)
    {
      
        if(gv_refdata.Rows.Count != 0)
        {
            foreach (GridViewRow row in gv_refdata.Rows)
            {
                int RowIndex = row.RowIndex;
                string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl_id")).Text.ToString();
                string varName2 = ((System.Web.UI.WebControls.Label)row.FindControl("Label_kod")).Text.ToString();

                string Inssql = "update KW_kemasukan_inventori set closing_sts='1',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id='" + varName1 + "' and Status='A'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    DataTable get_rec = new DataTable();
                    get_rec = DBCon.Ora_Execute_table("select top(1)* from KW_inv_lep_kad_stok where kod_barang= '" + varName2 + "' order by seq_no DESC");

                    string Inssql1 = "update KW_inv_lep_kad_stok set op_Status='1',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id='" + get_rec.Rows[0]["Id"].ToString() + "'";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                }
            }
            if(Status == "SUCCESS")
            {
                BindData();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Penutupan Inventori Kejayaan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }

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
                    string fmdate = string.Empty, fmdate1 = string.Empty, tmdate = string.Empty;

                    int RowIndex = row.RowIndex;
                    string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl_id")).Text.ToString(); //this store the  value in varName1
                    //string varName2 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl1")).Text.ToString();
                    //string varName3 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl4")).Text.ToString();
                   

                    DataTable get_bord = new DataTable();
                    get_bord = DBCon.Ora_Execute_table("select *,FORMAT(do_tarikh,'dd/MM/yyyy', 'en-us') as do_tarikh1 From KW_kemasukan_inventori where Id = '" + varName1 + "'");

                    DataTable get_bord_pen = new DataTable();
                    get_bord_pen = DBCon.Ora_Execute_table("select * From KW_pengeluaran_inventori where kod_barang = '" + get_bord.Rows[0]["kod_barang"].ToString() + "'");
                    if (get_bord_pen.Rows.Count == 0)
                    {
                        if (get_bord.Rows[0]["do_tarikh"].ToString() != "")
                        {
                            string fdate = get_bord.Rows[0]["do_tarikh1"].ToString();
                            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            fmdate = fd.ToString("yyyy-MM-dd");
                        }
                        DataTable ddokdicno1 = new DataTable();
                        ddokdicno1 = DBCon.Ora_Execute_table("select *,FORMAT(do_tarikh,'dd/MM/yyyy', 'en-us') as do_tarikh1 From KW_kemasukan_inventori where kod_barang='" + get_bord.Rows[0]["kod_barang"].ToString() + "' and do_tarikh>'" + fmdate + "' and Status='A'");

                        for (int j = 0; j < ddokdicno1.Rows.Count; j++)
                        {
                            if (ddokdicno1.Rows[j]["do_tarikh1"].ToString() != "")
                            {
                                string fdate1 = ddokdicno1.Rows[j]["do_tarikh1"].ToString();
                                DateTime fd1 = DateTime.ParseExact(fdate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                fmdate1 = fd1.ToString("yyyy-MM-dd");
                            }
                            DataTable ddokdicno2 = new DataTable();
                            ddokdicno2 = DBCon.Ora_Execute_table("select * From KW_kemasukan_inventori where kod_barang='" + get_bord.Rows[0]["kod_barang"].ToString() + "' and do_tarikh = '" + fmdate1 + "' and Status='A'");
                            if (ddokdicno2.Rows.Count != 0)
                            {
                                double ss1 = (double.Parse(ddokdicno2.Rows[0]["baki_kuantiti"].ToString()) - double.Parse(get_bord.Rows[0]["baki_kuantiti"].ToString()));
                                double ss2 = (double.Parse(ddokdicno2.Rows[0]["jumlah"].ToString()) - double.Parse(get_bord.Rows[0]["jumlah"].ToString()));
                                string Inssql1 = "update KW_kemasukan_inventori set baki_kuantiti='" + ss1 + "',jumlah='" + ss2 + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where kod_barang='" + get_bord.Rows[0]["kod_barang"].ToString() + "' and do_tarikh = '" + fmdate1 + "' and Status='A'";
                                Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                            }

                        }
                        //if (Status1 == "SUCCESS")
                        //{

                            string Inssql = "update KW_kemasukan_inventori set Status='T',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id = '" + varName1 + "'";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql);
                            if (Status == "SUCCESS")
                            {
                                string Inssql1 = "update KW_inv_lep_kad_stok set Status='T',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where kod_barang = '" + get_bord.Rows[0]["kod_barang"].ToString() + "' and tarikh='" + fmdate + "'";
                                Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                            }
                        //}
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Dihapus Tidak Mungkin.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
            }

            if (Status == "SUCCESS")
            {
               
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Dihapus Tidak Mungkin.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
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
}