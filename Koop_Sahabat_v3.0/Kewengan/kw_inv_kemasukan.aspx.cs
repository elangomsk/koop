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

public partial class kw_inv_kemasukan : System.Web.UI.Page
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
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                negeri();
                grid_tax();
                grd_pembekal();
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    //ver_id.Text = "1";
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('797','705','1724','1816','1818','1819','1820','799','1821','1822','1823','1824','1825','1826','1567','800','61','15','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());       
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());         
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl15.Text = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
            ps_lbl16.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl17.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
           
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
            ddokdicno = DBCon.Ora_Execute_table("select * From KW_kemasukan_inventori where Id='" + lblid + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                Button4.Text = "Kemaskini";
                //Button4.Visible = false;
                ver_id.Text = "1";
                get_id.Text = lblid;
                ddnegeri.Attributes.Add("Disabled", "Disabled");
                DropDownList1.Attributes.Add("Disabled", "Disabled");
                DropDownList2.Attributes.Add("Disabled", "Disabled");
                dd_uom.Attributes.Add("Disabled", "Disabled");
                tk_mula.Attributes.Add("readonly", "readonly");
                TextBox6.Attributes.Add("readonly", "readonly");
                TextBox9.Attributes.Add("readonly", "readonly");



                TextBox5.Text = ddokdicno.Rows[0]["do_no"].ToString();
                tk_mula.Text = Convert.ToDateTime(ddokdicno.Rows[0]["do_tarikh"]).ToString("dd/MM/yyyy");
                TextBox7.Text = ddokdicno.Rows[0]["po_no"].ToString();
                if (Convert.ToDateTime(ddokdicno.Rows[0]["po_tarikh"]).ToString("dd/MM/yyyy") != "01/01/1900")
                {
                    tk_akhir.Text = Convert.ToDateTime(ddokdicno.Rows[0]["po_tarikh"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    tk_akhir.Text = "";
                }
                ddnegeri.SelectedValue = ddokdicno.Rows[0]["jenis_barang"].ToString();
                get_barang();
                DropDownList1.SelectedValue = ddokdicno.Rows[0]["kod_barang"].ToString();
                DropDownList3.SelectedValue = ddokdicno.Rows[0]["nama_pembekal"].ToString();
                TextBox1.Text = ddokdicno.Rows[0]["nama_barang"].ToString();
                TextBox9.Text = double.Parse(ddokdicno.Rows[0]["unit"].ToString()).ToString("C").Replace("RM","").Replace("RM", "").Replace("$", "");
                DropDownList2.SelectedValue = ddokdicno.Rows[0]["kod_gst"].ToString();
                TextBox14.Text = ddokdicno.Rows[0]["keterangan"].ToString();
                TextBox6.Text = ddokdicno.Rows[0]["kuantiti"].ToString();
                dd_uom.SelectedValue = ddokdicno.Rows[0]["ki_uom"].ToString();
                
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
   
    void negeri()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select DISTINCT jenis_barang from KW_INVENTORI_BARANG where status= 'A' order by jenis_barang";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddnegeri.DataSource = dt;
            ddnegeri.DataBind();
            ddnegeri.DataTextField = "jenis_barang";
            ddnegeri.DataValueField = "jenis_barang";
            ddnegeri.DataBind();
            ddnegeri.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void grd_pembekal()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select Ref_no_syarikat,Ref_nama_syarikat,Ref_kod_akaun from KW_Ref_Pembekal where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList3.DataSource = dt;
            DropDownList3.DataBind();
            DropDownList3.DataTextField = "Ref_nama_syarikat";
            DropDownList3.DataValueField = "Ref_no_syarikat";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void grid_tax()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select Id,ref_nama_cukai,Ref_kod_cukai,Ref_kadar,Ref_kod_akaun from KW_Ref_Tetapan_cukai where Status='A' and tc_jenis='PEM'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataBind();
            DropDownList2.DataTextField = "ref_nama_cukai";
            DropDownList2.DataValueField = "Ref_kod_cukai";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void sel_jenis(object sender, EventArgs e)
    {
        get_barang();
    }

    void get_barang()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select distinct kod_barang from KW_INVENTORI_BARANG where jenis_barang='" + ddnegeri.SelectedValue + "' and status= 'A' order by kod_barang";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "kod_barang";
            DropDownList1.DataValueField = "kod_barang";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            TextBox1.Text = "";
            TextBox9.Text = "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void sel_barang(object sender, EventArgs e)
    {
        string fmdate = string.Empty, tmdate = string.Empty;
        if (tk_mula.Text != "")
        {
            string fdate = tk_mula.Text;
            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate = fd.ToString("yyyy-MM-dd");
        }
        else
        {
            fmdate = DateTime.Now.ToString("yyyy-MM-dd");
        }
        DataTable ddokdicno1 = new DataTable();
        ddokdicno1 = DBCon.Ora_Execute_table("select top(1)* From KW_INVENTORI_BARANG where kod_barang='"+ DropDownList1.SelectedValue +"' and tarikh_barang < ='"+ fmdate + "' order by tarikh_barang desc");
        if(ddokdicno1.Rows.Count != 0)
        {
            //TextBox1.Text = ddokdicno1.Rows[0]["nama_barang"].ToString();
            TextBox9.Text = double.Parse(ddokdicno1.Rows[0]["unit"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            //DataTable ddokdicno = new DataTable();
            //ddokdicno = DBCon.Ora_Execute_table("select top(1)* From KW_kemasukan_inventori where kod_barang='" + DropDownList1.SelectedValue + "' order by  do_tarikh desc");
            //if (ddokdicno.Rows.Count != 0)
            //{
            //    //Button4.Text = "Kemaskini";
            //    //Button4.Visible = false;
            //    //ver_id.Text = "1";
            //    //get_id.Text = lblid;
            //    //ddnegeri.Attributes.Add("style","pointer-events:none;");
            //    //DropDownList1.Attributes.Add("style", "pointer-events:none;");
            //    //ddnegeri.Attributes.Add("readonly", "readonly");
            //    //DropDownList1.Attributes.Add("readonly", "readonly");
            //    //TextBox1.Attributes.Add("readonly", "readonly");
            //    //TextBox5.Attributes.Add("readonly", "readonly");
            //    //TextBox7.Attributes.Add("readonly", "readonly");

            //    TextBox5.Text = ddokdicno.Rows[0]["do_no"].ToString();
            //    //tk_mula.Text = Convert.ToDateTime(ddokdicno.Rows[0]["do_tarikh"]).ToString("dd/MM/yyyy");
            //    TextBox7.Text = ddokdicno.Rows[0]["po_no"].ToString();
            //    //if (Convert.ToDateTime(ddokdicno.Rows[0]["po_tarikh"]).ToString("dd/MM/yyyy") != "01/01/1900")
            //    //{
            //    //    tk_akhir.Text = Convert.ToDateTime(ddokdicno.Rows[0]["po_tarikh"]).ToString("dd/MM/yyyy");
            //    //}
            //    //else
            //    //{
            //    //    tk_akhir.Text = "";
            //    //}
            //    ddnegeri.SelectedValue = ddokdicno.Rows[0]["jenis_barang"].ToString();
            //    get_barang();
            //    DropDownList1.SelectedValue = ddokdicno.Rows[0]["kod_barang"].ToString();
            //    DropDownList3.SelectedValue = ddokdicno.Rows[0]["nama_pembekal"].ToString();
            //    //TextBox1.Text = ddokdicno.Rows[0]["nama_barang"].ToString();
            //    //TextBox9.Text = double.Parse(ddokdicno.Rows[0]["unit"].ToString()).ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
            //    TextBox1.Text = ddokdicno1.Rows[0]["nama_barang"].ToString();
            //    TextBox9.Text = double.Parse(ddokdicno1.Rows[0]["unit"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            //    DropDownList2.SelectedValue = ddokdicno.Rows[0]["kod_gst"].ToString();
            //    TextBox14.Text = ddokdicno.Rows[0]["keterangan"].ToString();
            //    //TextBox6.Text = ddokdicno.Rows[0]["kuantiti"].ToString();


            //}
            //else
            //{
            //    //TextBox5.Text = "";
            //    //tk_mula.Text = "";
            //    //tk_akhir.Text = "";
            //    //TextBox7.Text = "";
            //    //DropDownList2.SelectedValue = "";
            //    //TextBox14.Text = "";
            //    //TextBox6.Text = "";
            //    TextBox1.Text = ddokdicno1.Rows[0]["nama_barang"].ToString();
            //    TextBox9.Text = double.Parse(ddokdicno1.Rows[0]["unit"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            //}

        }
        else
        {
            TextBox9.Text = "";
        }
    }

    protected void clk_submit(object sender, EventArgs e)
    {
        string amt1 = string.Empty, amt2 = string.Empty, amt3 = string.Empty;
        string fmdate = string.Empty, tmdate = string.Empty;
        if (tk_mula.Text != "" && ddnegeri.SelectedValue != "" && DropDownList1.SelectedValue != "" && TextBox6.Text != "")
        {
            DataTable get_gst = new DataTable();
            get_gst = DBCon.Ora_Execute_table("select * From KW_Ref_Tetapan_cukai where Ref_kod_cukai='" + DropDownList2.SelectedValue + "' ");
            if(get_gst.Rows.Count != 0)
            {
                double vv1 = ((double.Parse(get_gst.Rows[0]["Ref_kadar"].ToString()) * double.Parse(TextBox9.Text)) / 100);
                amt1 = vv1.ToString();
                double vv2 = (double.Parse(TextBox9.Text) + vv1);
                amt2 = vv2.ToString();
                double vv3 = (double.Parse(TextBox9.Text) * double.Parse(TextBox6.Text));
                amt3 = vv3.ToString();
            }

            if (tk_mula.Text != "")
            {
                string fdate = tk_mula.Text;
                DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                fmdate = fd.ToString("yyyy-MM-dd");
            }
            if (tk_akhir.Text != "")
            {
                string tdate = tk_akhir.Text;
                DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tmdate = td.ToString("yyyy-MM-dd");
            }
            if (ver_id.Text == "0")
            {
                DataTable ddokdicno = new DataTable();
                ddokdicno = Dblog.Ora_Execute_table("select * from KW_kemasukan_inventori where kod_barang='" + DropDownList1.SelectedValue + "' and do_tarikh >='"+ fmdate + "' and Status='A'");
                if (ddokdicno.Rows.Count == 0)
                {

                    DataTable ddokdicno1 = new DataTable();
                    ddokdicno1 = Dblog.Ora_Execute_table("select top(1) baki_kuantiti as qty,ISNULL(jumlah_baki,'0.00') as jum from KW_kemasukan_inventori where kod_barang='" + DropDownList1.SelectedValue + "' and do_tarikh <'" + fmdate + "' and Status='A' order by do_tarikh desc");
                    double ss_1 = 0;
                    double ss_2 = 0;
                    if (ddokdicno1.Rows.Count != 0)
                    {
                        ss_1 = (double.Parse(ddokdicno1.Rows[0]["qty"].ToString()) + double.Parse(TextBox6.Text));
                        ss_2 = (double.Parse(ddokdicno1.Rows[0]["jum"].ToString()) + double.Parse(amt3));

                    }
                    else
                    {
                        ss_1 = (double.Parse(TextBox6.Text));
                        ss_2 = (double.Parse(amt3));
                    }

                    string kdakaun = string.Empty;
                    string Inssql = "insert into KW_kemasukan_inventori (do_no,do_tarikh,po_no,po_tarikh,jenis_barang,kod_barang,nama_barang,nama_pembekal,kod_gst,keterangan,kuantiti,unit,gst_amaun,gst_amaun_jum,baki_kuantiti,jumlah,crt_id,crt_dt,Status,closing_sts,ext_qty,jumlah_baki,ki_uom)values('" + TextBox5.Text.Replace("'", "''") + "','" + fmdate + "','" + TextBox7.Text.Replace("'", "''") + "','" + tmdate + "','" + ddnegeri.SelectedValue + "','" + DropDownList1.SelectedValue + "','" + TextBox1.Text + "','" + DropDownList3.SelectedValue + "','" + DropDownList2.SelectedValue + "','" + TextBox14.Text.Replace("'", "''") + "','" + TextBox6.Text + "','" + TextBox9.Text + "','" + amt1 + "','" + amt2 + "','" + ss_1 + "','" + ss_2 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "','A','0','0','" + ss_2 + "','"+ dd_uom.SelectedValue +"')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        string qmsk = string.Empty;
                        DataTable sel_kod = new DataTable();
                        sel_kod = Dblog.Ora_Execute_table("select (count(*) + 1) as cnt from KW_inv_lep_kad_stok where kod_barang='" + DropDownList1.SelectedValue + "'");
                        if(sel_kod.Rows[0]["cnt"].ToString() != "1")
                        {
                            qmsk = TextBox6.Text;
                        }
                        else
                        {
                            qmsk = "";
                        }
                        string kdakaun1 = string.Empty;
                        string Inssql1 = "insert into KW_inv_lep_kad_stok (kod_barang,tarikh,nama_syarikat,qty_masuk,qty_keluar,qty_baki,qty,harga_kos,jumlah_kos,crt_id,cr_dt,seq_no,Status)values('" + DropDownList1.SelectedValue + "','" + fmdate + "','" + DropDownList3.SelectedItem.Text + "','" + qmsk + "','','" + ss_1 + "','" + ss_1 + "','" + TextBox9.Text + "','" + ss_2 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+ sel_kod.Rows[0]["cnt"].ToString() + "','A')";
                        Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                        if (Status1 == "SUCCESS")
                        {
                            Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                            Session["validate_success"] = "SUCCESS";
                            Response.Redirect("../kewengan/kw_inv_kemasukan_view.aspx");
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Ada.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                string Inssql1 = "Update KW_kemasukan_inventori set do_no='" + TextBox5.Text.Replace("'", "''") + "',po_no='" + TextBox7.Text.Replace("'", "''") + "',po_tarikh='" + tmdate + "',nama_pembekal='" + DropDownList3.SelectedValue + "',nama_barang='" + TextBox1.Text.Replace("'", "''") + "',keterangan='" + TextBox14.Text.Replace("'", "''") + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id='" + get_id.Text + "'";
                Status2 = DBCon.Ora_Execute_CommamdText(Inssql1);
                if (Status2 == "SUCCESS")
                {
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Session["validate_success"] = "SUCCESS";
                    Response.Redirect("../kewengan/kw_inv_kemasukan_view.aspx");
                }
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_inv_kemasukan.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_inv_kemasukan_view.aspx");
    }

    
}