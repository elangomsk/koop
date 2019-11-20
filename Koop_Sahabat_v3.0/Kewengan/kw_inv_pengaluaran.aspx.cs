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

public partial class kw_inv_pengaluaran : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    private static int PageSize = 20;
    decimal total = 0, total1 = 0;
    string uniqueId;
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
                TextBox5.Attributes.Add("Readonly", "Readonly");
                grd_pembekal();
                get_barang();
                FirstGridViewRow();
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    grd2.Visible = true;
                    grd1.Visible = false;
                    view_details();
                }
                else
                {
                    gen_do();
                    grd1.Visible = true;
                    grd2.Visible = false;
                    ver_id.Text = "0";
                    Rdya.Checked = true;
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1828','705','1724','1829','1818','1819','1820','1830','1831','1832','1833','1263','1264','61','15','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());        
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());      
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
           

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    void gen_do()
    {
        DataTable dt = DBCon.Ora_Execute_table("select    ISNULL(max(SUBSTRING(do_no,6,2000)),'0') from KW_pengeluaran_inventori");
        if (dt.Rows.Count > 0)
        {
            int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());
            int newNumber = seqno + 1;
            uniqueId = newNumber.ToString("DO" +""+ "00000");
            TextBox5.Text = uniqueId;
            //TextBox5.Attributes.Add("disabled", "disabled");
        }
        else
        {
            int newNumber = Convert.ToInt32(uniqueId) + 1;
            uniqueId = newNumber.ToString("DO"+"" + "00000");
            TextBox5.Text = uniqueId;

            //TextBox5.Attributes.Add("disabled", "disabled");
        }
    }
    void view_details()
    {
        Button1.Visible = false;
        try
        {
            string lblid = lbl_name.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * From KW_pengeluaran_inventori where do_no='" + lblid + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                Button4.Text = "Kemaskini";
                ver_id.Text = "1";
                get_id.Text = lblid;
               
               
                TextBox5.Attributes.Add("readonly", "readonly");
                TextBox7.Attributes.Add("readonly", "readonly");

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
               
                DropDownList3.SelectedValue = ddokdicno.Rows[0]["nama_pelengan"].ToString();
                TextBox2.Text = ddokdicno.Rows[0]["alamat_pelengan"].ToString();
                TextBox1.Text = ddokdicno.Rows[0]["keterangan_peng"].ToString();

                if(ddokdicno.Rows[0]["gst_op"].ToString() == "1")
                {
                    Rdya.Checked = true;
                }
                else
                {
                    Rdtidak.Checked = true;
                }
                BindData_show();
                get_barang();


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

    protected void BindData_show()
    {

        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select * from (select distinct m1.do_no,FORMAT(m1.crt_dt,'dd/MM/yyyy', 'en-us') crt_dt,m1.kod_barang,s1.jenis_barang,m1.keterangan_peng,m1.jum_kuantiti as jum_qty,m1.jumlah from KW_pengeluaran_inventori m1 left join KW_kemasukan_inventori s1 on s1.kod_barang=m1.kod_barang) as a left join(select sum(cast(baki_kuantiti as int)) as bqty,kod_barang from KW_kemasukan_inventori group by kod_barang) as b on b.kod_barang=a.kod_barang where a.do_no='" + TextBox5.Text + "'", con);
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

    protected void gvSelected_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        gv_refdata.PageIndex = e.NewPageIndex;
        gv_refdata.DataBind();
        BindData_show();
    }
    void grd_pembekal()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select Ref_nama_syarikat,Ref_no_syarikat,Ref_kod_akaun from KW_Ref_Pelanggan where Status='A' order by Ref_nama_syarikat";
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

  
   
    void get_barang()
    {
        //DataSet Ds = new DataSet();
        //try
        //{

        //    string com = "SELECT kod_barang, (kod_barang +' : ' + nama_barang) as name FROM KW_kemasukan_inventori";
        //    SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        //    DataTable dt = new DataTable();
        //    adpt.Fill(dt);
        //    DropDownList1.DataSource = dt;
        //    DropDownList1.DataBind();
        //    DropDownList1.DataTextField = "name";
        //    DropDownList1.DataValueField = "kod_barang";
        //    DropDownList1.DataBind();
        //    DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
    protected void get_pelinfo(object sender, EventArgs e)
    {
        DataTable ddokdicno1 = new DataTable();
        ddokdicno1 = DBCon.Ora_Execute_table("select distinct Ref_nama_syarikat,(ref_alamat +', '+ Ref_kawalan +', '+ ref_alamat_ked +', '+pel_bandar +', ' + s1.hr_negeri_desc +', '+pel_poskod +', '+ s2.CountryName) as address from KW_Ref_Pelanggan left join Ref_hr_negeri as s1 on s1.hr_negeri_Code=pel_negeri left join Country s2 on s2.ID=pel_negera where Ref_no_syarikat='" + DropDownList3.SelectedValue + "' ");
        if (ddokdicno1.Rows.Count != 0)
        {
            TextBox2.Text = ddokdicno1.Rows[0]["address"].ToString();
        }
    }

    protected void clk_submit(object sender, EventArgs e)
    {
        if (TextBox5.Text != "" && tk_mula.Text != "" && DropDownList3.SelectedValue != "")
        {
            string tk_m = string.Empty, tk_a = string.Empty, fmdate2 = string.Empty, fmdate3 = string.Empty;
            string rcount = string.Empty;
            int count = 0;
            if (tk_mula.Text != "")
            {
                DateTime dt_1 = DateTime.ParseExact(tk_mula.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tk_m = dt_1.ToString("yyyy-MM-dd");
            }
            if (tk_akhir.Text != "")
            {
                DateTime dt_2 = DateTime.ParseExact(tk_akhir.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tk_a = dt_2.ToString("yyyy-MM-dd");
            }

            string chk_gst = string.Empty;
            if(Rdya.Checked == true)
            {
                chk_gst = "1";
            }
            else if(Rdtidak.Checked == true)
            {
                chk_gst = "0";
            }

            if (ver_id.Text == "0")
            {

                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select * From KW_pengeluaran_inventori where do_no='" + TextBox5.Text + "'");
                if (ddokdicno.Rows.Count == 0)
                {
                    foreach (GridViewRow row in grvStudentDetails.Rows)
                    {
                        count++;
                        rcount = count.ToString();
                        string val1 = ((DropDownList)row.FindControl("Col1")).SelectedValue;
                        string val2 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col2")).Text.ToString();
                        string val3 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col3")).Text.ToString();
                        
                        string Inssql = "insert into KW_pengeluaran_inventori(do_no,do_tarikh,po_no,po_tarikh,nama_pelengan,alamat_pelengan,keterangan_peng,kod_barang,jum_kuantiti,unit,Jumlah,gst_op,crt_id,crt_dt) values ('" + TextBox5.Text + "','" + tk_m + "','" + TextBox7.Text.Replace("'", "''") + "','" + tk_a + "','" + DropDownList3.SelectedValue + "','" + TextBox2.Text.Replace("'", "''") + "','" + TextBox1.Text.Replace("'", "''") + "','" + val1 + "','" + val2 + "','','" + val3 + "','" + chk_gst + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                        if (Status == "SUCCESS")
                        {

                           
                        

                            int ssv1 = 0;
                            double ssv_amt1 = 0;
                            int ssv2 = 0;
                            double ssv_amt2 = 0;
                            int ssv3 = 0;
                            DataTable sel_gst1 = new DataTable();
                            sel_gst1 = DBCon.Ora_Execute_table("select *,FORMAT(do_tarikh,'dd/MM/yyyy', 'en-us') as do_tarikh1 From KW_kemasukan_inventori where kod_barang='" + val1 + "' and baki_kuantiti !='0' and Status='A' and closing_sts='0' order by do_tarikh ASC");
                            if (sel_gst1.Rows.Count != 0)
                            {
                                for(int m = 0; m< sel_gst1.Rows.Count; m++)
                                {
                                    string fdate2 = sel_gst1.Rows[m]["do_tarikh1"].ToString();
                                    DateTime fd2 = DateTime.ParseExact(fdate2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    fmdate2 = fd2.ToString("yyyy-MM-dd");
                                    DataTable sel_gst2_pre = new DataTable();
                                    if (m > 0)
                                    {
                                        string fdate3 = sel_gst1.Rows[m - 1]["do_tarikh1"].ToString();
                                        DateTime fd3 = DateTime.ParseExact(fdate3, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        fmdate3 = fd3.ToString("yyyy-MM-dd");
                                       
                                        sel_gst2_pre = DBCon.Ora_Execute_table("select * From KW_kemasukan_inventori where kod_barang='" + val1 + "' and do_tarikh ='" + fmdate3 + "'");
                                    }

                                    DataTable sel_gst2 = new DataTable();
                                    sel_gst2 = DBCon.Ora_Execute_table("select * From KW_kemasukan_inventori where kod_barang='" + val1 + "' and do_tarikh ='" + fmdate2 + "'");

                                   

                                    if (m == 0)
                                    {
                                        if (sel_gst2.Rows.Count != 0)
                                        {
                                            if (int.Parse(sel_gst2.Rows[0]["baki_kuantiti"].ToString()) <= int.Parse(val2))
                                            {
                                                ssv1 = 0;
                                                ssv3 = int.Parse(sel_gst2.Rows[0]["kuantiti"].ToString());
                                                ssv_amt1 = 0;
                                            }
                                            else
                                            {
                                                ssv1 = (int.Parse(sel_gst2.Rows[0]["baki_kuantiti"].ToString()) - int.Parse(val2));
                                                ssv3 = (int.Parse(sel_gst2.Rows[0]["baki_kuantiti"].ToString()) - ssv1);
                                                ssv_amt1 = ((ssv1) * double.Parse(sel_gst2.Rows[0]["unit"].ToString()));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //if (ssv1 != 0)
                                        //{
                                            if (int.Parse(sel_gst2_pre.Rows[0]["baki_kuantiti"].ToString()) == 0)
                                            {
                                                ssv1 = (int.Parse(sel_gst2.Rows[0]["baki_kuantiti"].ToString()) - int.Parse(val2));
                                            ssv_amt1 = ((ssv1) * double.Parse(sel_gst2.Rows[0]["unit"].ToString()));
                                            if (int.Parse(sel_gst2_pre.Rows[0]["kuantiti"].ToString()) <= int.Parse(val2))
                                                {
                                                    ssv3 = (int.Parse(val2) - int.Parse(sel_gst2_pre.Rows[0]["ext_qty"].ToString())); 
                                                }
                                                else
                                                {
                                                    ssv3 = (int.Parse(sel_gst2.Rows[0]["baki_kuantiti"].ToString()) - int.Parse(sel_gst2_pre.Rows[0]["ext_qty"].ToString()));
                                                }
                                            }
                                            else
                                            {
                                                ssv1 = (int.Parse(sel_gst2.Rows[0]["baki_kuantiti"].ToString()) - int.Parse(sel_gst2_pre.Rows[0]["ext_qty"].ToString()));
                                            ssv_amt1 = (((double.Parse(sel_gst2.Rows[0]["kuantiti"].ToString())) * double.Parse(sel_gst2.Rows[0]["unit"].ToString())) + double.Parse(sel_gst2_pre.Rows[0]["jumlah_baki"].ToString()));
                                            if (int.Parse(sel_gst2_pre.Rows[0]["ext_qty"].ToString()) <= int.Parse(val2))
                                            {
                                                ssv3 = 0;
                                            }
                                            else
                                            {
                                                ssv3 = (int.Parse(sel_gst2.Rows[0]["baki_kuantiti"].ToString()) - int.Parse(sel_gst2_pre.Rows[0]["ext_qty"].ToString()));
                                            }
                                        }
                                        //}
                                    }

                                    DataTable sel_kod = new DataTable();
                                    sel_kod = Dblog.Ora_Execute_table("select (count(*) + 1) as cnt from KW_inv_lep_kad_stok where kod_barang='" + val1 + "'");
                                    ssv2 = ssv1;
                                    //int ss1 = (int.Parse(sel_gst1.Rows[0]["baki_kuantiti"].ToString()) - int.Parse(val2));
                                    int ss1 = ssv2;
                                    
                                    int ss2 = ssv3;
                                    string Inssql1 = "UPDATE KW_kemasukan_inventori SET jumlah_baki='"+ ssv_amt1 + "',ext_qty='" + ss2 + "',baki_kuantiti='" + ss1 + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where kod_barang='" + val1 + "' and do_tarikh ='" + fmdate2 + "' ";
                                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                                    if (sel_gst1.Rows.Count == (m + 1))
                                    {
                                        string Inssql2 = "insert into KW_inv_lep_kad_stok (kod_barang,tarikh,nama_syarikat,qty_masuk,qty_keluar,qty_baki,qty,harga_kos,jumlah_kos,crt_id,cr_dt,seq_no,Status)values('" + val1 + "','" + tk_m + "','"+ DropDownList3.SelectedItem.Text + "','','" + val2 + "','" + ss1 + "','" + ss1 + "','" + sel_gst2.Rows[0]["unit"].ToString() + "','" + ssv_amt1 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + sel_kod.Rows[0]["cnt"].ToString() + "','A')";
                                        Status1 = DBCon.Ora_Execute_CommamdText(Inssql2);
                                    }
                                }

                             
                            }
                        }
                    }
                    if (Status1 == "SUCCESS")
                    {
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Session["validate_success"] = "SUCCESS";
                        Response.Redirect("../kewengan/kw_inv_pengaluaran_view.aspx");
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
            else
            {
                //foreach (GridViewRow row in grvStudentDetails.Rows)
                //{
                //    string rno = ((System.Web.UI.WebControls.Label)row.FindControl("RowNumber")).Text.ToString();
                //    string val1 = ((DropDownList)row.FindControl("Col1")).SelectedValue;
                //    string val2 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col2")).Text.ToString();
                //    string val3 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col3")).Text.ToString();
                //    string val4 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col4")).Text.ToString();

                //}
                string Inssql = "UPDATE KW_pengeluaran_inventori SET do_tarikh='"+ tk_m + "',po_tarikh='" + tk_a + "',nama_pelengan='"+ DropDownList3.SelectedValue + "',alamat_pelengan='"+ TextBox2.Text.Replace("'", "''") + "',keterangan_peng='"+ TextBox1.Text.Replace("'", "''") + "',gst_op='"+ chk_gst +"',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where do_no='" + TextBox5.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {

                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Session["validate_success"] = "SUCCESS";
                    Response.Redirect("../kewengan/kw_inv_pengaluaran_view.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_inv_pengaluaran.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_inv_pengaluaran_view.aspx");
    }

    private void FirstGridViewRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Col1", typeof(string)));
        dt.Columns.Add(new DataColumn("Col2", typeof(string)));
        dt.Columns.Add(new DataColumn("Col3", typeof(string)));
        dr = dt.NewRow();
        //dr["RowNumber"] = 1;
        dr["Col1"] = string.Empty;
        dr["Col2"] = string.Empty;
        dr["Col3"] = string.Empty;
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;

        grvStudentDetails.DataSource = dt;
        grvStudentDetails.DataBind();
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRow();
    }

    private void AddNewRow()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            //if (dtCurrentTable.Rows.Count < 2)
            //{
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList v1 =
                      (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col1");
                    System.Web.UI.WebControls.TextBox v2 =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col2");
                    System.Web.UI.WebControls.TextBox v2_1 =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("Col3");
                 
                    
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = v1.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = v2.Text;
                    dtCurrentTable.Rows[i - 1]["Col3"] = v2_1.Text;
                    
                    rowIndex++;
                    //if (v3.Text != "")
                    //{
                    //    decimal amt1 = Convert.ToDecimal(v3.Text);
                    //    total += (double)amt1;
                    //}
                    //if (v4.Text != "")
                    //{
                    //    decimal amt2 = Convert.ToDecimal(v4.Text);
                    //    total1 += (double)amt2;
                    //}
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                grvStudentDetails.DataSource = dtCurrentTable;
                grvStudentDetails.DataBind();

               

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData();
    }



    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList v1 =
                          (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col1");
                    System.Web.UI.WebControls.TextBox v2 =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col2");
                    System.Web.UI.WebControls.TextBox v2_1 =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("Col3");
                                      

                    v1.SelectedValue = dt.Rows[i]["Col1"].ToString();
                    v2.Text = dt.Rows[i]["Col2"].ToString();
                    v2_1.Text = dt.Rows[i]["Col3"].ToString();
                    
                    rowIndex++;
                    if (v2.Text != "")
                    {
                        decimal amt1 = Convert.ToDecimal(v2.Text);
                        total += amt1;
                    }
                  
                }
            }
        }
    }

    protected void grvStudentDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                grvStudentDetails.DataSource = dt;
                grvStudentDetails.DataBind();

                for (int i = 0; i < grvStudentDetails.Rows.Count - 1; i++)
                {
                    grvStudentDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousData();
            }
        }
    }

    private void SetRowData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList v1 =
                         (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col1");
                    System.Web.UI.WebControls.TextBox v2 =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col2");
                    System.Web.UI.WebControls.TextBox v2_1 =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("Col3");
                  
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Col1"] = v1.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = v2.Text;
                    dtCurrentTable.Rows[i - 1]["Col3"] = v2_1.Text;
                    
                    rowIndex++;

                }

                ViewState["CurrentTable"] = dtCurrentTable;
                //grvStudentDetails.DataSource = dtCurrentTable;
                //grvStudentDetails.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //SetPreviousData();
    }

    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            var ddl = (DropDownList)e.Row.FindControl("Col1");
            //int CountryId = Convert.ToInt32(e.Row.Cells[0].Text);
            SqlCommand cmd = new SqlCommand("SELECT distinct m1.kod_barang,(m1.kod_barang +' | '+ s1.nama_barang) as name FROM KW_kemasukan_inventori m1 inner join KW_INVENTORI_BARANG s1 on s1.kod_barang=m1.kod_barang order by kod_barang asc", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddl.DataSource = ds;
            ddl.DataTextField = "name";
            ddl.DataValueField = "kod_barang";
            ddl.DataBind();
            ddl.SelectedValue = ((DataRowView)e.Row.DataItem)["Col1"].ToString();
            ddl.Items.Insert(0, new ListItem("--- PILIH ---", ""));



            //System.Web.UI.WebControls.TextBox txt3 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("Col3");
            //System.Web.UI.WebControls.TextBox txt1 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("Col4");
            //if (TextBox1.Text != "")
            //{
            //    txt3.Text = double.Parse(TextBox1.Text).ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
            //}
            //else
            //{
            //    txt3.Text = "0.00";
            //}
            //decimal sval1 = 0, sval2 = 0;


            //if (txt1.Text == "")
            //{
            //    sval2 = 0;
            //}
            //else
            //{
            //    sval2 = Convert.ToDecimal(txt1.Text);
            //}

            //total1 += sval2;
        }
       
    }

    protected void QtyChanged(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        decimal unit1 = 0;
        string fmdate = string.Empty, fmdate1 = string.Empty, fmdate2 = string.Empty;
        GridViewRow row = ((GridViewRow)((System.Web.UI.WebControls.TextBox)sender).NamingContainer);

        DropDownList kod_bor = (DropDownList)row.FindControl("Col1");
        System.Web.UI.WebControls.TextBox per = (System.Web.UI.WebControls.TextBox)row.FindControl("Col2");
        System.Web.UI.WebControls.TextBox tot1 = (System.Web.UI.WebControls.TextBox)row.FindControl("Col3");
        if (tk_mula.Text != "")
        {

            string fdate = tk_mula.Text;
            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate = fd.ToString("yyyy-MM-dd");

            int qty1 = Convert.ToInt32(per.Text);
            DataTable sel_gst1 = new DataTable();
            sel_gst1 = DBCon.Ora_Execute_table("select top(1)* From KW_kemasukan_inventori where kod_barang='" + kod_bor.SelectedValue + "' and do_tarikh<='"+ fmdate + "' and Status='A' order by do_tarikh desc");
            if (sel_gst1.Rows.Count != 0)
            {
                if (Int32.Parse(sel_gst1.Rows[0]["baki_kuantiti"].ToString()) >= qty1)
                {
                    string chk_qty = string.Empty;
                    double chk_amt = 0;
                    double sv2 = 0;
                    double sv1 = 0;
                    DataTable sel_gst2 = new DataTable();
                    sel_gst2 = DBCon.Ora_Execute_table("select *,FORMAT(do_tarikh,'dd/MM/yyyy', 'en-us') as do_tarikh1 From KW_kemasukan_inventori where kod_barang='" + kod_bor.SelectedValue + "' and do_tarikh<='" + fmdate + "' and Status='A' order by do_tarikh ASC");

                    for(int k = 0; k< sel_gst2.Rows.Count; k++)
                    {
                        string fdate1 = sel_gst2.Rows[k]["do_tarikh1"].ToString();
                        DateTime fd1 = DateTime.ParseExact(fdate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        fmdate1 = fd1.ToString("yyyy-MM-dd");

                       

                        DataTable sel_gst3 = new DataTable();
                        sel_gst3 = DBCon.Ora_Execute_table("select * From KW_kemasukan_inventori where kod_barang='" + kod_bor.SelectedValue + "' and do_tarikh ='" + fmdate1 + "' and Status='A' order by do_tarikh desc");
                        DataTable sel_gst_pre = new DataTable();
                        int pre_qty = 0;
                        if (k > 0)
                        {
                            string fdate2 = sel_gst2.Rows[k - 1]["do_tarikh1"].ToString();
                            DateTime fd2 = DateTime.ParseExact(fdate2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            fmdate2 = fd2.ToString("yyyy-MM-dd");
                            sel_gst_pre = DBCon.Ora_Execute_table("select * From KW_kemasukan_inventori where kod_barang='" + kod_bor.SelectedValue + "' and do_tarikh ='" + fmdate2 + "' and Status='A' order by do_tarikh ASC");
                            pre_qty = Int32.Parse(sel_gst_pre.Rows[0]["baki_kuantiti"].ToString());
                            if (sel_gst3.Rows[0]["baki_kuantiti"].ToString() != "0")
                            {
                                if (Int32.Parse(sel_gst3.Rows[0]["baki_kuantiti"].ToString()) <= qty1)
                                {
                                    sv1 = ((double.Parse(sel_gst3.Rows[0]["baki_kuantiti"].ToString())) * double.Parse(sel_gst3.Rows[0]["unit"].ToString()));
                                    sv2 = qty1 - double.Parse(sel_gst3.Rows[0]["baki_kuantiti"].ToString());
                                }
                                else
                                {
                                    sv1 = (sv2 * double.Parse(sel_gst3.Rows[0]["unit"].ToString()));
                                    sv2 = 0;
                                }
                            }
                            else
                            {
                                if (Int32.Parse(sel_gst3.Rows[0]["baki_kuantiti"].ToString()) <= qty1)
                                {
                                    sv1 = 0;
                                    sv2 = 0;
                                }
                                else
                                {
                                    sv1 = 0;
                                    sv2 = 0;
                                }
                            }
                        }
                        else
                        {

                            if (sel_gst3.Rows.Count != 0)
                            {
                                if (Int32.Parse(sel_gst3.Rows[0]["baki_kuantiti"].ToString()) >= qty1)
                                {
                                    sv1 = ((qty1 - sv2) * double.Parse(sel_gst3.Rows[0]["unit"].ToString()));
                                    sv2 = 0;

                                }
                                else
                                {
                                    sv1 = ((double.Parse(sel_gst3.Rows[0]["baki_kuantiti"].ToString()) - sv2) * double.Parse(sel_gst3.Rows[0]["unit"].ToString()));
                                    sv2 = (qty1 - double.Parse(sel_gst3.Rows[0]["baki_kuantiti"].ToString()));
                                }

                               
                            }
                        }
                        chk_amt += sv1;
                    }

                   
                    if (qty1.ToString() != "")
                    {
                        if (chk_amt != 0)
                        {
                            tot1.Text = double.Parse(chk_amt.ToString()).ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
                        }
                        else
                        {
                            tot1.Text = "0.00";
                        }
                    }
                    //amt1.Focus();
                }
                else
                {
                    per.Focus();
                    per.Text = "";
                    tot1.Text = "0.00";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Quantity Must be allowed in " + sel_gst1.Rows[0]["baki_kuantiti"].ToString() + ".',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
        }
        else
        {
            //per.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh DO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }

    //protected void QtyChanged1(object sender, EventArgs eventArgs)
    //{
    //    decimal numTotal = 0;
    //    decimal unit1 = 0;
    //    GridViewRow row = ((GridViewRow)((System.Web.UI.WebControls.TextBox)sender).NamingContainer);
    //    System.Web.UI.WebControls.TextBox per = (System.Web.UI.WebControls.TextBox)row.FindControl("Col2");
    //    System.Web.UI.WebControls.TextBox amt1 = (System.Web.UI.WebControls.TextBox)row.FindControl("Col3");

    //    if (per.Text != "")
    //    {
    //        int qty1 = Convert.ToInt32(per.Text);
    //        if (amt1.Text != "")
    //        {
    //            unit1 = Convert.ToDecimal(amt1.Text);
    //        }
    //        else
    //        {
    //            unit1 = 0;
    //        }

    //        if (qty1.ToString() != "")
    //        {
    //            numTotal = (qty1 * unit1);
    //            //tot1.Text = numTotal.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
    //        }
    //    }

    //}

    protected void ddgstdeboth_SelectedIndexChanged(object sender, EventArgs e)
    {

        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        DropDownList c1 = (DropDownList)grvStudentDetails.Rows[selRowIndex].FindControl("Col1");
        System.Web.UI.WebControls.TextBox c2 = (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[selRowIndex].FindControl("col3");

        decimal tgst;
        decimal tgst1;
        if (tk_mula.Text != "")
        {
            DataTable sel_gst = new DataTable();
            sel_gst = DBCon.Ora_Execute_table("select * From KW_kemasukan_inventori where kod_barang='" + c1.SelectedValue + "' and do_tarikh=''");
            if (sel_gst.Rows.Count != 0)
            {
                tgst = Convert.ToDecimal(sel_gst.Rows[0]["unit"].ToString());
            }
            else
            {
                tgst = 0;
            }
            //c2.Text = tgst.ToString("C").Replace("$", "").Replace("RM", "");
        }
        else
        {
            c1.SelectedValue = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh DO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }


}