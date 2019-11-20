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
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

public partial class kw_inventori : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    string level;
    string Status = string.Empty;
    string userid;
    string ref_id;
    string confirmValue, am;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                bind_jenis();
                BindData1();
                userid = Session["New"].ToString();
                if (Session["pro_id"].ToString() != "")
                {
                    view_details();
                }
                else
                {
                   
                    ver_id.Text = "0";
                    Button2.Visible = false;
                }
                
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void view_details()
    {
        Button1.Visible = false;
        Button4.Visible = false;
        Button2.Visible = true;
        try
        {
            string lblid = Session["pro_id"].ToString();
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * from KW_Inventori s1 where s1.Id='" + lblid + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                TextBox3.Attributes.Add("Readonly", "Readonly");
                TextBox4.Attributes.Add("Readonly", "Readonly");
                TextBox1.Attributes.Add("Readonly", "Readonly");
                DropDownList2.Attributes.Add("Readonly", "Readonly");
                DropDownList1.Attributes.Add("Readonly", "Readonly");
                Textarea1.Attributes.Add("Readonly", "Readonly");
                TextBox10.Attributes.Add("Readonly", "Readonly");

                TextBox3.Text = ddokdicno.Rows[0]["kod_produk"].ToString();
                TextBox4.Text = ddokdicno.Rows[0]["nama_produk"].ToString();
                TextBox1.Text = ddokdicno.Rows[0]["kuantiti"].ToString();
             
                DropDownList2.SelectedValue = ddokdicno.Rows[0]["jenis"].ToString().Trim();
                bind_akaun();
                DropDownList1.SelectedValue = ddokdicno.Rows[0]["kod_akaun"].ToString().Trim();
                if (DropDownList1.SelectedValue != "")
                {
                    bind_amt();
                }
                Textarea1.Value = ddokdicno.Rows[0]["keterangan"].ToString();
                //Tab_1.Visible = false;
                //Tab_2.Visible = true;
                //header_txt1.Text = "TAMBAH BAUCER BAYARAN";
                //ver_id.Text = "1";
                //get_id.Text = ddokdicno.Rows[0]["kod_produk"].ToString();
                //TextBox2.Text = ddokdicno.Rows[0]["kod_produk"].ToString();
                //TextBox7.Text = ddokdicno.Rows[0]["nama_produk"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
            }
            BindData1();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   
    protected void dd_jenis(object sender, EventArgs e)
    {
        bind_akaun();
        BindData1();
    }
    void bind_jenis()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kat_cd,Kat_akuan from KW_Kategori_akaun where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "Kat_akuan";
            DropDownList2.DataValueField = "kat_cd";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void bind_akaun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Id,kod_akaun,(kod_akaun + ' | ' + nama_akaun) as name from KW_Ref_Carta_Akaun where kat_akaun='" + DropDownList2.SelectedValue + "' and Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "name";
            DropDownList1.DataValueField = "kod_akaun";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Click_tambah(object sender, EventArgs e)
    {
        BindData1();
        ModalPopupExtender1.Show();
    }
    protected void BindData1()
    {

        qry1 = "select s1.Id,s1.kod_produk,FORMAT(s1.tarikh,'dd/MM/yyyy', 'en-us') tarikh,s1.masa,s1.kuantiti_masuk,s1.kuantiti_keluar from KW_Inventori_stok s1 where s1.kod_produk='" + TextBox3.Text + "'";
        SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
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

    protected void clk_submit(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedValue != "" && TextBox3.Text != "" && TextBox4.Text != "")
        {
            if (ver_id.Text == "0")
            {
                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select * From KW_Inventori where kod_produk='" + TextBox3.Text + "' and nama_produk='" + TextBox4.Text + "'");
                if (ddokdicno.Rows.Count == 0)
                {
                    string Inssql = "insert into KW_Inventori(jenis,kod_produk,nama_produk,keterangan,kuantiti,kod_akaun,Status,crt_id,cr_dt) values ('" + DropDownList2.SelectedValue + "','" + TextBox3.Text.Replace("'", "''") + "','" + TextBox4.Text.Replace("'", "''") + "','" + Textarea1.Value.Replace("'", "''") + "','" + TextBox1.Text.Replace("'", "''") + "','" + DropDownList1.SelectedValue + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Session["validate_success"] = "SUCCESS";
                        Response.Redirect("../kewengan/kw_inventori_view.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }
        
    }

    protected void clk_submit1(object sender, EventArgs e)
    {
        if (TextBox3.Text != "" && TextBox5.Text != "")
        {
                string tk_m = string.Empty, tk_a = string.Empty;
                if (TextBox5.Text != "")
                {
                    DateTime dt_1 = DateTime.ParseExact(TextBox5.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    tk_m = dt_1.ToString("yyyy-MM-dd");
                }
                string Inssql = "insert into KW_Inventori_stok(kod_produk,tarikh,masa,kuantiti_masuk,kuantiti_keluar,Status,crt_id,cr_dt) values ('" + TextBox3.Text + "','" + tk_m + "','" + TextBox6.Text + "','" + TextBox8.Text.Replace("'", "''") + "','" + TextBox9.Text.Replace("'", "''") + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                grd2_empty();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
                else
                {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }
        BindData1();
    }


   void grd2_empty()
    {
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
    }


    protected void sel_jenis(object sender, EventArgs e)
    {
        bind_amt();
    }

    void bind_amt()
    {
        DataTable sel_balance = new DataTable();
        sel_balance = DBCon.Ora_Execute_table("select Kw_open_amt From KW_Ref_Carta_Akaun where Id='" + DropDownList1.SelectedValue + "'");
        if (sel_balance.Rows.Count != 0)
        {
            TextBox10.Text = double.Parse(sel_balance.Rows[0]["Kw_open_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
        }
        else
        {
            TextBox10.Text = "0.00";
        }
        BindData1();
    }

    protected void gvSelected_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        BindData1();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../kewengan/kw_inventori.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../kewengan/kw_inventori_view.aspx");
    }

    
}