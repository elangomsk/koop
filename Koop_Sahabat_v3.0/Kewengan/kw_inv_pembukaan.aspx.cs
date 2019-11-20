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


public partial class kw_inv_pembukaan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();

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
                Session["pro_id"] = "";
                userid = Session["New"].ToString();
                grd_kodakaun();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('790','705','791','1813')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower()); 

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void grd_kodakaun()
    {
        //DataSet Ds = new DataSet();
        //try
        //{

        //    string com = "select kat_akaun,nama_akaun,kod_akaun from KW_Ref_Carta_Akaun where kat_akaun='15' and under_parent != '0' order by kod_akaun";
        //    SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        //    DataTable dt = new DataTable();
        //    adpt.Fill(dt);
        //    ddnegeri.DataSource = dt;
        //    ddnegeri.DataBind();
        //    ddnegeri.DataTextField = "nama_akaun";
        //    ddnegeri.DataValueField = "kod_akaun";
        //    ddnegeri.DataBind();
        //    ddnegeri.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }

    protected void BindData()
    {
        string sqry = string.Empty;
        //if (txtSearch.Text == "")
        //{
            sqry = "where Status='A'";
        //}
        //else
        //{
        //    sqry = "where Status='A' and s1.kod_barang LIKE'%" + txtSearch.Text + "%' OR s1.jenis_barang LIKE'%" + txtSearch.Text + "%'";
        //}
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select a.kod_barang,a.jenis_barang,a.bqty,a.jum_kes,b.harga_kos from (select s1.kod_barang,s1.jenis_barang,SUM(cast(s1.baki_kuantiti as int)) as bqty,ISNULL(SUM(s1.jumlah_baki),'0.00') as jum_kes from KW_kemasukan_inventori s1 where s1.closing_sts='1' and s1.baki_kuantiti !='0' group by s1.jenis_barang,s1.kod_barang ) as a left join(select * from KW_inv_lep_kad_stok where op_Status='1') as b on b.kod_barang=a.kod_barang order by a.jenis_barang,a.kod_barang Asc", con);
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

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_refdata.PageIndex = e.NewPageIndex;
        gv_refdata.DataBind();
        BindData();
    }

    protected void btn_open_acc(object sender, EventArgs e)
    {
        string varName1 = string.Empty, varName2 = string.Empty, varName3 = string.Empty, varName4 = string.Empty, varName5 = string.Empty;
        if (gv_refdata.Rows.Count != 0)
        {
            foreach (GridViewRow row in gv_refdata.Rows)
            {
                int RowIndex = row.RowIndex;
                varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl1")).Text.ToString();
                varName2 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl2")).Text.ToString();
                varName3 = ((System.Web.UI.WebControls.TextBox)row.FindControl("lbl3")).Text.ToString();
                varName4 = ((System.Web.UI.WebControls.TextBox)row.FindControl("lbl4")).Text.ToString();
                varName5 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl5")).Text.ToString();
                string Inssql1 = "insert into KW_inv_Opening_Balance (kod_akaun,kod_barang,jenis_barangan,baki_keseluruhan,unit,jumlah_keseluruhan,opeing_date,opening_year,Status,crt_id,cr_dt,set_sts) Values('','"+ varName1 + "','" + varName2 + "','"+ varName3 + "','" + varName4 + "','" + varName5 + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.Year.ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','1')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                if (Status == "SUCCESS")
                {
                    string Inssql = "update KW_kemasukan_inventori set closing_sts='0',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where kod_barang='" + varName1 + "' and jenis_barang='" + varName2 + "' and closing_sts='1' and baki_kuantiti !='0' and Status='A'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                }
                if (Status == "SUCCESS")
                {
                    DataTable get_rec = new DataTable();
                    get_rec = DBCon.Ora_Execute_table("select top(1)* from KW_inv_lep_kad_stok where kod_barang= '" + varName1 + "' order by seq_no DESC");

                    string Inssql2 = "update KW_inv_lep_kad_stok set op_Status='0',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id='" + get_rec.Rows[0]["Id"].ToString() + "'";
                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql2);
                }

            }
            if (Status == "SUCCESS")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Pembukaan Inventori Berjaya.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                BindData();
            }
        }

    }

    protected void QtyChanged(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        decimal unit1 = 0;
        GridViewRow row = ((GridViewRow)((System.Web.UI.WebControls.TextBox)sender).NamingContainer);
        System.Web.UI.WebControls.TextBox qty = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl3");
        System.Web.UI.WebControls.TextBox amt1 = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl4");
        System.Web.UI.WebControls.Label jum = (System.Web.UI.WebControls.Label)row.FindControl("lbl5");

        if (amt1.Text != "")
        {
            int qty1 = Convert.ToInt32(qty.Text);
            if (amt1.Text != "")
            {
                unit1 = Convert.ToDecimal(amt1.Text);
            }
            else
            {
                unit1 = 0;
            }

            if (qty1.ToString() != "")
            {
                numTotal = (qty1 * unit1);
                jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
            }
        }

    }


    protected void QtyChanged1(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        decimal unit1 = 0;
        GridViewRow row = ((GridViewRow)((System.Web.UI.WebControls.TextBox)sender).NamingContainer);
        System.Web.UI.WebControls.TextBox qty = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl3");
        System.Web.UI.WebControls.TextBox amt1 = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl4");
        System.Web.UI.WebControls.Label jum = (System.Web.UI.WebControls.Label)row.FindControl("lbl5");

        if (qty.Text != "")
        {
            int qty1 = Convert.ToInt32(qty.Text);
            if (amt1.Text != "")
            {
                unit1 = Convert.ToDecimal(amt1.Text);
            }
            else
            {
                unit1 = 0;
            }

            if (qty1.ToString() != "")
            {
                numTotal = (qty1 * unit1);
                jum.Text = numTotal.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
            }
        }

    }
}