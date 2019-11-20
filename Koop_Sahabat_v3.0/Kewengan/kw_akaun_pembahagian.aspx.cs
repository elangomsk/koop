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
using System.Threading;

public partial class kw_akaun_pembahagian : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    decimal total = 0, total1 = 0;
    string level, userid;
    string Status = string.Empty;
    string qry1 = string.Empty, qry2 = string.Empty;
    string fmdate = string.Empty, tmdate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        app_language();
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button4);
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                ver_id.Text = "0";
                BindDropdown();
                var samp = Request.Url.Query;

                if (samp != "")
                {
                    Button8.Visible = true;
                    Tahun_kew.SelectedValue = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                }
                else
                {
                    Button8.Visible = false;
                }
                view_details();
              
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1723','705','1724','827','65','1725','1726','1728','1729','121','754','755','14','15')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());       
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
         
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            //Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            //Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
           

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void BindDropdown()
    {
        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

        int year = DateTime.Now.Year - 5;

        for (int Y = year; Y <= DateTime.Now.Year; Y++)
        {

            Tahun_kew.Items.Add(new ListItem(Y.ToString(), Y.ToString()));

        }

        Tahun_kew.SelectedValue = DateTime.Now.Year.ToString();

    }
    protected void view_details()
    {
        DataTable chk_fin_year = new DataTable();
        chk_fin_year = DBCon.Ora_Execute_table("SELECT FORMAT(ISNULL(fin_close_dt,''),'yyyy-MM-dd')  as cls_dt,ISNULL(fin_acc_distribution,'0') acc_dist,ISNULL(fin_amt_cyp,'0.00') cyp_amt,ISNULL(fin_amt_re,'0.00') re_amt FROM  KW_financial_Year WHERE fin_year='" + Tahun_kew.SelectedValue + "'");
        if(chk_fin_year.Rows.Count != 0)
        {
            DataTable chk_det1 = new DataTable();
            chk_det1 = DBCon.Ora_Execute_table("select * from KW_rpt_trialbalance where rpt_tb_type='03' and Rpt_tb_year='" + Tahun_kew.SelectedValue + "'");
            if (chk_det1.Rows.Count == 0)
            {
                Button4.Visible = false;
                Button2.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('SILA TETAPKAN THE POST-CLOSING TRIAL BALANCE.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);                
            }
            else
            {
                if (chk_fin_year.Rows[0]["cls_dt"].ToString() == "1900-01-01" && chk_fin_year.Rows[0]["acc_dist"].ToString() == "0")
                {
                    Button4.Text = "Simpan";
                    Button2.Visible = false;
                }
                else if (chk_fin_year.Rows[0]["cls_dt"].ToString() == "1900-01-01" && chk_fin_year.Rows[0]["acc_dist"].ToString() == "1")
                {
                    Button4.Text = "Kemaskini";
                    Button2.Visible = true;
                    TextBox17.Text = chk_fin_year.Rows[0]["re_amt"].ToString();
                }
                else if (chk_fin_year.Rows[0]["cls_dt"].ToString() != "1900-01-01" && chk_fin_year.Rows[0]["acc_dist"].ToString() == "1")
                {
                    Button4.Text = "Simpan";
                    Button4.Visible = false;
                    Button2.Visible = true;
                    TextBox17.Text = chk_fin_year.Rows[0]["re_amt"].ToString();
                }
            }
        }
        BindData1();
        BindData();
    }
    void BindData1()
    {
        DataTable dd_pembahagian = new DataTable();
        dd_pembahagian = DBCon.Ora_Execute_table("select tmp_kod_akaun col1,tmp_descript col2,Ref_peratus col3,tmp_amt col4,tmp_jum_amt col5,tmp_nota col6 from KW_Ref_Pembahagian where tmp_tahun_kewangan='" + Tahun_kew.SelectedValue + "'");
       
        if (dd_pembahagian.Rows.Count != 0)
        {
            ViewState["CurrentTable"] = dd_pembahagian;
            grvStudentDetails.DataSource = dd_pembahagian;
            grvStudentDetails.DataBind();
            grvStudentDetails.FooterRow.Cells[3].Text = "JUMLAH (RM) :";
            grvStudentDetails.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
        }
        else
        {
            FirstGridViewRow();
        }
        

    }

    void fin_details()
    {
        DataTable get_fin_year = new DataTable();
        get_fin_year = DBCon.Ora_Execute_table("select FORMAT(fin_start_dt,'yyyy-MM-dd') st_dt,FORMAT(fin_end_dt,'yyyy-MM-dd') ed_dt from KW_financial_Year where fin_year='" + Tahun_kew.SelectedValue + "' and Status='1'");
        if (get_fin_year.Rows.Count != 0)
        {
            fmdate = get_fin_year.Rows[0]["st_dt"].ToString();
            tmdate = get_fin_year.Rows[0]["ed_dt"].ToString();
        }
    }
        void BindData()
    {
        
        string sqry = string.Empty;

        fin_details();

        if (Tahun_kew.SelectedValue != "")
        {
            sqry = "select kk1.kat_cd a1, cyp.nama_akaun a2, cyp.kod_akaun a3, sum(ISNULL(s1.KW_Debit_amt, '0.00')) KW_Debit_amt, sum(ISNULL(s1.KW_kredit_amt, '0.00')) KW_kredit_amt "
	              + " , (((sum(ISNULL(cast(s2.opn_kredit_amt as money), '0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money), '0.00'))) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)), '0.00') - "
                  + " isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + isnull(sum(cast(s1.KW_kredit_amt as money)), '0.00')) - isnull(sum(cast(s1.KW_Debit_amt as money)), '0.00') as a4 "
                  + " from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + Tahun_kew.SelectedValue + "' between YEAR(s2.start_dt)and YEAR(s2.end_dt) "
                  + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = m1.kod_akaun and YEAR(s2_1.GL_post_dt) = '" + Tahun_kew.SelectedValue + "' and  s2_1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '"+ fmdate + "'), 0) "
                  + " left join KW_General_Ledger s1 on s1.kod_akaun = m1.kod_akaun and s1.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and s1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) "
                  + " and s1.GL_sts = 'L'  left join KW_Kategori_akaun kk on kk.kat_cd = m1.kat_akaun and kat_type = '01' inner join kw_ref_report_1 s5 on s5.kat_cd = kk.kat_rpt_kk and kat_rpt_cd = '02' "
                  + " left join KW_Ref_Carta_Akaun cyp on cyp.ca_cyp = '1'  left join KW_Kategori_akaun kk1 on kk1.kat_cd = cyp.kat_akaun  where m1.jenis_akaun_type != '1' and m1.Status = 'A' group by "
                  + " kk1.kat_cd, cyp.nama_akaun, cyp.kod_akaun";
        }
        else
        {
            sqry = "select kk1.kat_cd a1, cyp.nama_akaun a2, cyp.kod_akaun a3, sum(ISNULL(s1.KW_Debit_amt, '0.00')) KW_Debit_amt, sum(ISNULL(s1.KW_kredit_amt, '0.00')) KW_kredit_amt "
                   + " , (((sum(ISNULL(cast(s2.opn_kredit_amt as money), '0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money), '0.00'))) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)), '0.00') - "
                   + " isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + isnull(sum(cast(s1.KW_kredit_amt as money)), '0.00')) - isnull(sum(cast(s1.KW_Debit_amt as money)), '0.00') as a4 "
                   + " from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '" + Tahun_kew.SelectedValue + "' between YEAR(s2.start_dt)and YEAR(s2.end_dt) "
                   + " left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = m1.kod_akaun and YEAR(s2_1.GL_post_dt) = '" + Tahun_kew.SelectedValue + "' and  s2_1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) "
                   + " left join KW_General_Ledger s1 on s1.kod_akaun = m1.kod_akaun and s1.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and s1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) "
                   + " and s1.GL_sts = 'L'  left join KW_Kategori_akaun kk on kk.kat_cd = m1.kat_akaun and kat_type = '01' inner join kw_ref_report_1 s5 on s5.kat_cd = kk.kat_rpt_kk and kat_rpt_cd = '02' "
                   + " left join KW_Ref_Carta_Akaun cyp on cyp.ca_cyp = '1'  left join KW_Kategori_akaun kk1 on kk1.kat_cd = cyp.kat_akaun  where m1.jenis_akaun_type != '1' and m1.Status = 'A' group by "
                   + " kk1.kat_cd, cyp.nama_akaun, cyp.kod_akaun";
        }

        DataTable dd_hrsal = new DataTable();
        dd_hrsal = DBCon.Ora_Execute_table(sqry);
        if (dd_hrsal.Rows.Count != 0)
        {
            TextBox1.Text = double.Parse(dd_hrsal.Rows[0]["a4"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
            cyp_txt.Text = dd_hrsal.Rows[0]["a2"].ToString().ToUpper();
        }
        else
        {
            TextBox1.Text = "0.00";
        }
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand(sqry, con);
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
        //FirstGridViewRow();
        //FirstGridViewRow1();
        //FirstGridViewRow2();
        //FirstGridViewRow3();
    }

    protected void Back_to_profile(object sender, EventArgs e)
    {
        string name = Request.QueryString["prfle_cd"];        
        Response.Redirect(string.Format("../kewengan/kw_profil_syarikat.aspx?edit={0}", name));
    }
    protected void clk_cerian(object sender, EventArgs e)
    {
        //if (Tk_akhir.Text != "")
        //{
            view_details();
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        //}
    }

    //1st grid

    protected void QtyChanged(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        decimal unit1 = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox per = (TextBox)row.FindControl("Col3");
        TextBox amt1 = (TextBox)row.FindControl("Col4");
        TextBox tot1 = (TextBox)row.FindControl("Col5");
        //Label jum1 = (Label)grvStudentDetails.FooterRow.FindControl("lblTotal2");
        int qty1 = Convert.ToInt32(per.Text);
        if (amt1.Text != "")
        {
            unit1 = Convert.ToDecimal(amt1.Text.Replace("(","-").Replace(")", ""));
        }
        else
        {
            unit1 = 0;
        }

        if (qty1.ToString() != "")
        {
            if (amt1.Text != "")
            {
                numTotal = (qty1 * unit1) / 100;
                if (unit1 > 0)
                {
                    tot1.Text = numTotal.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
                }
                else
                {
                    tot1.Text = numTotal.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
                }

            }
        }
       
        amt1.Focus();
        GrandTotal();
        tab1();
    }

    protected void changed_kod(object sender, EventArgs eventArgs)
    {
        GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
        DropDownList kod_akaun = (DropDownList)row.FindControl("Col1");
        TextBox ket = (TextBox)row.FindControl("Col2");
        DataTable get_desc = new DataTable();
        get_desc = DBCon.Ora_Execute_table("select nama_akaun from KW_Ref_Carta_Akaun where kod_akaun='" + kod_akaun.SelectedValue + "'");
        if(get_desc.Rows.Count != 0)
        {
            ket.Text = get_desc.Rows[0]["nama_akaun"].ToString();
        }
    }
    protected void QtyChanged1(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;
        decimal unit1 = 0;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox per = (TextBox)row.FindControl("Col3");
        TextBox amt1 = (TextBox)row.FindControl("Col4");
        TextBox tot1 = (TextBox)row.FindControl("Col5");
        if (per.Text != "")
        {
            int qty1 = Convert.ToInt32(per.Text);
            if (amt1.Text != "")
            {
                unit1 = Convert.ToDecimal(amt1.Text.Replace("(","-").Replace(")", ""));
            }
            else
            {
                unit1 = 0;
            }

            if (qty1.ToString() != "")
            {
                if (amt1.Text != "")
                {
                    numTotal = (qty1 * unit1) / 100;
                    if (unit1 > 0)
                    {
                        tot1.Text = numTotal.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
                    }
                    else
                    {
                        tot1.Text = numTotal.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
                    }
                   
                }
            }
        }
       
        tab1();
        GrandTotal();
    }

    private void GrandTotal()
    {
        decimal GTotal = 0;
        decimal GTotal1 = 0;
        decimal ITotal = 0;
        decimal Gst = 0;
        for (int i = 0; i < grvStudentDetails.Rows.Count; i++)
        {
            String total = (grvStudentDetails.Rows[i].FindControl("col5") as TextBox).Text;

            TextBox jumI = (TextBox)grvStudentDetails.FooterRow.FindControl("lblTotal2");
            if (total != "")
            {
                GTotal1 = (decimal)Convert.ToDecimal(total);
            }
            else
            {
                GTotal1 = 0;
            }
            GTotal += (decimal)Convert.ToDecimal(GTotal1);

            jumI.Text = GTotal.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");            
        }
        decimal cyp_tot = (decimal.Parse(TextBox1.Text) - GTotal);
        TextBox17.Text = cyp_tot.ToString("C").Replace("$", "").Replace("RM", "");
    }

    private void FirstGridViewRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;        
        dt.Columns.Add(new DataColumn("Col1", typeof(string)));
        dt.Columns.Add(new DataColumn("Col2", typeof(string)));
        dt.Columns.Add(new DataColumn("Col3", typeof(string)));
        dt.Columns.Add(new DataColumn("Col4", typeof(string)));
        dt.Columns.Add(new DataColumn("Col5", typeof(string)));
        dt.Columns.Add(new DataColumn("Col6", typeof(string)));
        dr = dt.NewRow();
        //dr["RowNumber"] = 1;
        dr["Col1"] = string.Empty;
        dr["Col2"] = string.Empty;
        dr["Col3"] = string.Empty;
        dr["Col4"] = string.Empty;
        dr["Col5"] = string.Empty;
        dr["Col6"] = string.Empty;
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;

        grvStudentDetails.DataSource = dt;
        grvStudentDetails.DataBind();
        grvStudentDetails.FooterRow.Cells[3].Text = "JUMLAH (RM) :";
        grvStudentDetails.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRow();
        tab1();
    }

    void tab1()
    {
        p6.Attributes.Add("class", "tab-pane active");
        pp6.Attributes.Add("class", "active");
        BindData();

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
                    TextBox v2 =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col2");
                    TextBox v2_1 =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("Col3");
                    TextBox v3 =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("Col4");
                    TextBox v4 =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("Col5");
                    TextBox v5 =
                     (TextBox)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("Col6");
                    drCurrentRow = dtCurrentTable.NewRow();
                    

                    dtCurrentTable.Rows[i - 1]["Col1"] = v1.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = v2.Text;
                    dtCurrentTable.Rows[i - 1]["Col3"] = v2_1.Text;
                    dtCurrentTable.Rows[i - 1]["Col4"] = v3.Text;
                    dtCurrentTable.Rows[i - 1]["Col5"] = v4.Text;
                    dtCurrentTable.Rows[i - 1]["Col6"] = v5.Text;
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

                //grvStudentDetails.FooterRow.Cells[4].Text = "JUMLAH (RM) :";
                //grvStudentDetails.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //((TextBox)grvStudentDetails.FooterRow.Cells[4].FindControl("lblTotal2")).Text = total1.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");

            }
            //}
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
                    TextBox v2 =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col2");
                    TextBox v2_1 =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("Col3");
                    TextBox v3 =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("Col4");
                    TextBox v4 =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("Col5");
                    TextBox v5 =
                     (TextBox)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("Col6");

                    v1.SelectedValue = dt.Rows[i]["Col1"].ToString();
                    v2.Text = dt.Rows[i]["Col2"].ToString();
                    v2_1.Text = dt.Rows[i]["Col3"].ToString();
                    v3.Text = dt.Rows[i]["Col4"].ToString();
                    v4.Text = dt.Rows[i]["Col5"].ToString();
                    v5.Text = dt.Rows[i]["Col6"].ToString();
                    rowIndex++;
                    if (v3.Text != "")
                    {
                        decimal amt1 = Convert.ToDecimal(v3.Text.Replace("(", "").Replace(")", ""));
                        total += amt1;
                    }
                    if (v4.Text != "")
                    {
                        decimal amt2 = Convert.ToDecimal(v4.Text);
                        total1 += amt2;
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
                grvStudentDetails.FooterRow.Cells[3].Text = "JUMLAH (RM) :";
                grvStudentDetails.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //((TextBox)grvStudentDetails.FooterRow.Cells[3].FindControl("lblTotal1")).Text = total.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
                ((TextBox)grvStudentDetails.FooterRow.Cells[4].FindControl("lblTotal2")).Text = total1.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
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
                    TextBox v2 =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col2");
                    TextBox v2_1 =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("Col3");
                    TextBox v3 =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("Col4");
                    TextBox v4 =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("Col5");
                    TextBox v5 =
                     (TextBox)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("Col6");
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Col1"] = v1.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = v2.Text;
                    dtCurrentTable.Rows[i - 1]["Col3"] = v2_1.Text;
                    dtCurrentTable.Rows[i - 1]["Col4"] = v3.Text;
                    dtCurrentTable.Rows[i - 1]["Col5"] = v4.Text;
                    dtCurrentTable.Rows[i - 1]["Col6"] = v5.Text;
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
            SqlCommand cmd = new SqlCommand("select kod_akaun,(kod_akaun + ' | ' + UPPER(nama_akaun)) as name from KW_Ref_Carta_Akaun where ISNULL(kw_acc_header,'0')='0' and jenis_akaun_type !='1' and Status='A' order by kod_akaun asc", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddl.DataSource = ds;
            ddl.DataTextField = "name";
            ddl.DataValueField = "kod_akaun";
            ddl.DataBind();
            ddl.SelectedValue = ((DataRowView)e.Row.DataItem)["Col1"].ToString();
            ddl.Items.Insert(0, new ListItem("--- PILIH ---", ""));



            System.Web.UI.WebControls.TextBox txt3 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("Col4");
            System.Web.UI.WebControls.TextBox txt1 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("Col5");
         
            decimal sval1 = 0, sval2 = 0;


            if (txt1.Text == "")
            {
                sval2 = 0;
            }
            else
            {
                sval2 = Convert.ToDecimal(txt1.Text);
            }

            total1 += sval2;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //System.Web.UI.WebControls.TextBox lblamount1 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("lblTotal1");
            System.Web.UI.WebControls.TextBox lblamount2 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("lblTotal2");
            //lblamount1.Text = total.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
            lblamount2.Text = total1.ToString("C").Replace("RM", "").Replace("RM", "").Replace("$", "");
        }
    }

    //1st grid end
  

    protected void clk_submit(object sender, EventArgs e)
    {
        if (Tahun_kew.SelectedValue != "")
        {
            string tk_m = string.Empty, tk_a = string.Empty;
            string rcount1 = string.Empty, rcount2 = string.Empty, rcount3 = string.Empty, rcount4 = string.Empty;
            int count1 = 0;
            foreach (GridViewRow gvrow in grvStudentDetails.Rows)
            {
                count1++;
                rcount1 = count1.ToString();
            }
                       
            if (rcount1 != "0")
            {
                foreach (GridViewRow row in grvStudentDetails.Rows)
                {
                    string val1 = ((DropDownList)row.FindControl("Col1")).SelectedValue;
                    string val2 = ((TextBox)row.FindControl("Col2")).Text.ToString();
                    string val3 = ((TextBox)row.FindControl("Col3")).Text.ToString();
                    string val4 = ((TextBox)row.FindControl("Col4")).Text.ToString();
                    string val5 = ((TextBox)row.FindControl("Col5")).Text.ToString();
                    string val6 = ((TextBox)row.FindControl("Col6")).Text.ToString();
                    if (val1 != "")
                    {
                        DataTable chk_row = new DataTable();
                        chk_row = DBCon.Ora_Execute_table("select * From KW_Ref_Pembahagian where tmp_tahun_kewangan='" + Tahun_kew.SelectedValue + "' and tmp_kod_akaun='"+ val1 +"'");
                        if (chk_row.Rows.Count == 0)
                        {
                            string Inssql1 = "insert into KW_Ref_Pembahagian(tmp_tahun_kewangan,tmp_kod_akaun,tmp_descript,Ref_peratus,tmp_amt,tmp_jum_amt,tmp_nota,crt_id,cr_dt) values ('" + Tahun_kew.SelectedValue + "','" + val1 + "','" + val2 + "','" + val3 + "','" + val4 + "','" + val5 + "','" + val6 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                        }
                        else
                        {
                            string Updsql1 = "update KW_Ref_Pembahagian set Ref_peratus='" + val3 + "',tmp_amt='" + val4 + "',tmp_jum_amt='" + val5 + "',tmp_nota='" + val6 + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where tmp_tahun_kewangan='" + Tahun_kew.SelectedValue + "' and tmp_kod_akaun='" + val1 + "'";
                            Status = DBCon.Ora_Execute_CommamdText(Updsql1);
                        }
                    }
                }
            }
            if (Status == "SUCCESS")
            {
                string Upd_fin_year = "Update KW_financial_Year set fin_acc_distribution='1',fin_amt_cyp='"+ TextBox1.Text + "',fin_amt_re='"+ TextBox17.Text + "' where  fin_year='"+ Tahun_kew.SelectedValue + "'";
                Status = DBCon.Ora_Execute_CommamdText(Upd_fin_year);

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                view_details();
            }
           
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }      
    }
    
    protected void BindData_show()
    {

        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select s1.seq_no as RowNumber,s1.kod_akaun as col1,s1.akaun_keterangan as col2,s1.debit_amt as col3,s1.kredit_amt as col4 from KW_Pelarasan s1 where s1.no_rujukan='" + get_id.Text + "' order by s1.ID asc", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            FirstGridViewRow();
            AddNewRow();
        }
        else
        {
            grvStudentDetails.DataSource = ds;
            grvStudentDetails.DataBind();
        }

        con.Close();
    }

    protected void clk_prnt(object sender, EventArgs e)
    {
        
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty, val6 = string.Empty;
        fin_details();



            dt = DBCon.Ora_Execute_table("select * from ( "
                    + "  select  ROW_NUMBER() OVER(ORDER BY cyp.kod_akaun) AS RowNum,'1' sno,'K' as a,cyp.kod_akaun a1,cyp.nama_akaun a2,  "
                    + "  (((sum(ISNULL(cast(s2.opn_kredit_amt as money), '0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money), '0.00'))) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)), '0.00') -  isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + isnull(sum(cast(s1.KW_kredit_amt as money)), '0.00')) - isnull(sum(cast(s1.KW_Debit_amt as money)), '0.00') as a3 "
                    + " ,(((sum(ISNULL(cast(s2.opn_kredit_amt as money), '0.00')) - sum(ISNULL(cast(s2.opn_debit_amt as money), '0.00'))) + (isnull(sum(cast(s2_1.KW_kredit_amt as money)), '0.00') -  isnull(sum(cast(s2_1.KW_Debit_amt as money)), '0.00'))) + isnull(sum(cast(s1.KW_kredit_amt as money)), '0.00')) - isnull(sum(cast(s1.KW_Debit_amt as money)), '0.00') as a4  from "
                    + " KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s2 on s2.set_sts = '1' and s2.kod_akaun = m1.kod_akaun and '"+ Tahun_kew.SelectedValue +"' between YEAR(s2.start_dt)and YEAR(s2.end_dt)  left join KW_General_Ledger s2_1 on  s2_1.kod_akaun = m1.kod_akaun and YEAR(s2_1.GL_post_dt) = '"+Tahun_kew.SelectedValue+"' and  s2_1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '"+ fmdate + "'), 0)  left join KW_General_Ledger s1 on s1.kod_akaun = m1.kod_akaun and s1.GL_post_dt >= DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and s1.GL_post_dt <= DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)  and s1.GL_sts = 'L'  left join KW_Kategori_akaun kk on kk.kat_cd = m1.kat_akaun and kat_type = '01' inner join kw_ref_report_1 s5 on s5.kat_cd = kk.kat_rpt_kk and kat_rpt_cd = '02'  left join KW_Ref_Carta_Akaun cyp on cyp.ca_cyp = '1'  left join KW_Kategori_akaun kk1 on kk1.kat_cd = cyp.kat_akaun  where m1.jenis_akaun_type != '1' and m1.Status = 'A' group by  kk1.kat_cd, cyp.nama_akaun, cyp.kod_akaun "
                    + " union all select ROW_NUMBER() OVER(ORDER BY tmp_kod_akaun) AS RowNum,'2' sno,'D' as a,tmp_kod_akaun a1,tmp_descript a2,tmp_jum_amt a3,('0.00' - tmp_jum_amt) a4 from KW_Ref_Pembahagian where tmp_tahun_kewangan='" + Tahun_kew.SelectedValue + "') as a order by sno "); 
            
            //dt1 = DBCon.Ora_Execute_table(val5);
            Rptviwerlejar.Reset();
            ds.Tables.Add(dt);
            //ds1.Tables.Add(dt1);

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();



            Rptviwerlejar.LocalReport.DataSources.Clear();
            if (countRow != 0)
            {
            DataTable get_pfl = new DataTable();
            get_pfl = DBCon.Ora_Execute_table("select syar_logo from KW_Profile_syarikat where cur_sts='1' and Status='A'");
            string imagePath = string.Empty;
            if (get_pfl.Rows[0]["syar_logo"].ToString() != "")
            {
                imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/" + get_pfl.Rows[0]["syar_logo"].ToString() + "")).AbsoluteUri;

            }
            else
            {
                imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/user.png")).AbsoluteUri;
            }
            Rptviwerlejar.LocalReport.EnableExternalImages = true;
            Rptviwerlejar.LocalReport.ReportPath = "kewengan/KW_akb.rdlc";
                ReportDataSource rds = new ReportDataSource("kwakb", dt);                
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("d1", imagePath)  ,
                     new ReportParameter("d2", Tahun_kew.SelectedValue),
                     new ReportParameter("d3", cyp_txt.Text)
                   };

                Rptviwerlejar.LocalReport.SetParameters(rptParams);
                Rptviwerlejar.LocalReport.DataSources.Add(rds);
                //Rptviwerlejar.LocalReport.DataSources.Add(rds1);
                Rptviwerlejar.LocalReport.Refresh();
            Warning[] warnings;

            string[] streamids;

            string mimeType;

            string encoding;

            string extension;

            byte[] bytes = Rptviwerlejar.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            Response.Buffer = true;

            Response.Clear();

            Response.ContentType = mimeType;

            Response.AddHeader("content-disposition", "attatchment; filename=Akaun_Pembahagian_." + extension);

            Response.BinaryWrite(bytes);

            Response.Flush();

            Response.End();
        }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
      
    }

    //protected void clk_pstledger(object sender, EventArgs e)
    //{
    //    if (Session["run_sqno"].ToString() != "")
    //    {
    //        DataTable ddokdicno = new DataTable();
    //        ddokdicno = DBCon.Ora_Execute_table("select * from KW_Ref_Pembahagian where tmp_seq_no = '" + Session["run_sqno"].ToString() + "'");
    //        if (ddokdicno.Rows.Count != 0)
    //        {
    //            for (int k = 0; k < ddokdicno.Rows.Count; k++)
    //            {
    //                DataTable ddokdicno1 = new DataTable();
    //                ddokdicno1 = DBCon.Ora_Execute_table("select kat_akaun,kod_akaun from KW_Ref_Carta_Akaun where kod_akaun = '" + ddokdicno.Rows[k]["tmp_kod_akaun"].ToString() + "'");
    //                string Inssql1 = "insert into KW_General_Ledger(kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_post_dt,GL_desc1,crt_id,cr_dt,GL_sts) values ('" + ddokdicno.Rows[k]["tmp_kod_akaun"].ToString() + "','" + ddokdicno.Rows[k]["tmp_jum_amt"].ToString() + "','','" + ddokdicno1.Rows[0]["kat_akaun"].ToString() + "','" + ddokdicno1.Rows[0]["cr_dt"].ToString() + "','" + ddokdicno.Rows[k]["tmp_descript"].ToString() + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
    //                Status = DBCon.Ora_Execute_CommamdText(Inssql1);
    //            }

    //            if (Status == "SUCCESS")
    //            {
                    
    //                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});window.location ='kw_akaun_pembahagian.aspx';", true);
    //            }
    //        }
    //    }
    //}
    protected void btn_reset(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_akaun_pembahagian.aspx");
        Session["run_sqno"] = "";
    }


}