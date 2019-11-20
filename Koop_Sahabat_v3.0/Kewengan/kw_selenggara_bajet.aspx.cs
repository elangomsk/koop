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


public partial class kw_selenggara_bajet : System.Web.UI.Page
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
        string script = " $(document).ready(function () { $(" + dd_akaun.ClientID + ").SumoSelect({ selectAll: true }); $('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        app_language();
  
        if (!IsPostBack)
        {
            get_profile_view();
            if (Session["New"] != null)
            {
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    Session["validate_success"] = "";
                    Session["alrt_msg"] = "";
                    Session["pro_type"] = "";
                }
                //Button2.Attributes.Add("style", "Pointer-events:none; opacity: 0.5;");
                //Button3.Attributes.Add("style", "Pointer-events:none; opacity: 0.5;");
                kat_bajet.SelectedValue = "01";
                userid = Session["New"].ToString();
                //Button1.Visible = false;
                bind_kat_akaun();
                bind_kod_akaun();
                bind_kod_industry();
                bind_tah_kew();
                BindData();
                tah_kewangan.SelectedValue = DateTime.Now.Year.ToString();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void bind_tah_kew()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select fin_year from KW_financial_Year group by fin_year";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            tah_kewangan.DataSource = dt;
            tah_kewangan.DataTextField = "fin_year";
            tah_kewangan.DataValueField = "fin_year";
            tah_kewangan.DataBind();
            tah_kewangan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void get_profile_view()
    {
        string get_sdt = string.Empty, get_edt = string.Empty;
        DataTable sel_gst2 = new DataTable();
        sel_gst2 = DBCon.Ora_Execute_table("select Format(fin_start_dt,'dd/MM/yyyy') as st_dt,Format(fin_end_dt,'dd/MM/yyyy') as end_dt from KW_financial_Year where Status='1' ");
        if (sel_gst2.Rows.Count != 0)
        {
            get_sdt = sel_gst2.Rows[0]["st_dt"].ToString();
            get_edt = sel_gst2.Rows[0]["end_dt"].ToString();
            DateTime fd = DateTime.ParseExact(get_sdt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime fd1 = DateTime.ParseExact(get_edt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            TextBox5.Text = fd.ToString("dd/MM/yyyy");
            TextBox6.Text = fd1.ToString("dd/MM/yyyy");
            //Tk_mula.Text = fd.ToString("dd/MM/yyyy");
            //Tk_akhir.Text = fd1.ToString("dd/MM/yyyy");



        }
    }
    void bind_kod_akaun()
    {

        DataSet Ds = new DataSet();
        try
        {
            string get_qry = string.Empty;

            if (dd_kodind.SelectedValue == "1")
            {
                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A' order by kod_akaun asc";
            }
            else if (dd_kodind.SelectedValue == "2")
            {
                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun s1 inner join KW_Ref_Pelanggan on Ref_kod_akaun = kod_akaun where jenis_akaun_type != '1' and s1.Status='A' order by kod_akaun asc";
            }
            else if (dd_kodind.SelectedValue == "3")
            {
                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun s1 inner join KW_Ref_Pembekal on Ref_kod_akaun = kod_akaun where jenis_akaun_type != '1' and s1.Status='A' order by kod_akaun asc";
            }
            else if (dd_kodind.SelectedValue == "4")
            {
                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and jenis_akaun='12.01' order by kod_akaun asc";
            }
            else
            {

                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A' order by kod_akaun asc";

            }

            string com = get_qry;
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_akaun.DataSource = dt;
            dd_akaun.DataTextField = "name";
            dd_akaun.DataValueField = "kod_akaun";
            dd_akaun.DataBind();            

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

  
    void bind_kod_industry()
    {
        //DataSet Ds = new DataSet();
        //try
        //{
        //    string com = "select DISTINCT kod_industry,(kod_industry +' | ' + msic_desc) as name from Kw_kod_industry left join KW_Ref_Kod_Industri on msic_kod=kod_industry where kod_industry != ''";
        //    SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        //    DataTable dt = new DataTable();
        //    adpt.Fill(dt);
        //    dd_kodind.DataSource = dt;
        //    dd_kodind.DataTextField = "name";
        //    dd_kodind.DataValueField = "kod_industry";
        //    dd_kodind.DataBind();
        //    dd_kodind.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
    void bind_kat_akaun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kat_cd,UPPER(kat_akuan) kat_akuan from KW_Kategori_akaun where kat_type='02' order by kat_cd asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            kat_akaun.DataSource = dt;
            kat_akaun.DataTextField = "kat_akuan";
            kat_akaun.DataValueField = "kat_cd";
            kat_akaun.DataBind();
            kat_akaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void app_language()

    {
        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('731','705','36','1646','1647','133')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            //Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());               
            //Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            //Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            //Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
           
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    protected void BindData()
    {
        //PopulateTreeview();
        BindGrid();
    }

    protected void clk_srch(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void BindGrid()
    {
        string sqry = string.Empty, st_dt = string.Empty;
        //if (Tk_mula.Text != "")
        //{
        //    string fdate = Tk_mula.Text;
        //    DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    st_dt = fd.ToString("yyyy-MM-dd");
        //}
        if (kat_bajet.SelectedValue != "")
        {
            sqry = "and Ref_kat_bajet='"+ kat_bajet.SelectedValue + "' and ref_bjt_year ='"+ tah_kewangan.SelectedValue +"'";
        }
        string query = " with Recurse as ("
                          + " select a.bjt_Id as DirectChildId, a.bjt_Id, a.kod_bajet"
                          + " from KW_Ref_kod_bajet a     union all"
                          + " select b.DirectChildId, a.bjt_Id, a.kod_bajet "
                          + " from KW_Ref_kod_bajet a "
                          + " join Recurse b on b.bjt_Id = a.bjt_under_parent)"
                          + " select * from (select a.DirectChildId, isnull(sum(cast(Ref_jumlah_bajet as money)),'0.00')  as Amount "
                          + " from Recurse a"
                          + " left join KW_Ref_Bajet b on a.kod_bajet = b.Ref_kod_bajet "+ sqry + ""
                          + " group by DirectChildId) as a "
                          + " inner join  (select m1.bjt_Id,m1.jenis_bajet_type,m1.kat_bajet,m1.nama_bajet,m1.kod_bajet,m1.jenis_bajet,m1.bjt_under_parent,isHeader,ISNULL(s1.Ref_jumlah_bajet,'0.00') as KW_Debit_amt,Ref_kat_bajet,Ref_tk_mula,Ref_tk_akhir "
                          + " from KW_Ref_kod_bajet m1 left join KW_Ref_Bajet s1 on s1.Ref_kod_bajet=m1.kod_bajet "+ sqry + " where m1.bjt_Status='A') as b on b.bjt_Id=a.DirectChildId";

        //string query = "select m1.bjt_Id,m1.jenis_bajet_type,m1.kat_bajet,m1.nama_bajet,m1.kod_bajet,m1.jenis_bajet,m1.bjt_under_parent,ISNULL(s1.Ref_jumlah_bajet,'0.00') as KW_Debit_amt,isHeader,Ref_kat_bajet,Ref_tk_mula,Ref_tk_akhir from KW_Ref_kod_bajet m1 left join KW_Ref_Bajet s1 on s1.Ref_kod_bajet=m1.kod_bajet where m1.bjt_Status='A' order by kat_bajet";

        
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            BindGrid1();
                            //Calculate Sum and display in Footer Row
                            //decimal debit = dt.AsEnumerable().Sum(row => row.Field<decimal>("KW_Debit_amt"));
                            //decimal kredit = dt.AsEnumerable().Sum(row => row.Field<decimal>("KW_Kredit_amt"));
                            //GridView1.FooterRow.Cells[2].Text = "<strong>JUMLAH (RM)</strong>";
                            //GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                            //GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                            //GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                            //GridView1.FooterRow.Cells[3].Text = debit.ToString("C").Replace("RM", "").Replace("$", "");
                            //GridView1.FooterRow.Cells[4].Text = kredit.ToString("C").Replace("RM", "").Replace("$", "");
                        }

                    }
                }
            }
        }

    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_refdata.PageIndex = e.NewPageIndex;
        gv_refdata.DataBind();
        BindData();
    }

    protected void BindGrid1()
    {
        //SqlCommand cmd2 = new SqlCommand("select ISNULL(ho.org_name,'') as org_name,ISNULL(dd.dis_dispose_type_cd,'') as dis_dispose_type_cd,rk.ast_kategori_desc,rja.ast_jeniaset_desc,aca.cas_asset_desc,a.sas_asset_id,a.sas_curr_price_amt, a.sas_asset_cat_cd,a.sas_asset_sub_cat_cd,a.sas_asset_type_cd,a.sas_asset_cd,a.sas_org_id, case a.sas_asset_cat_cd when '01' then (select FORMAT(com_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd) when '02' then (select FORMAT(car_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select FORMAT(inv_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a1, case a.sas_asset_cat_cd when '01' then (select DATEDIFF(day,com_reg_dt,GETDATE()) as u_dt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '02' then (select DATEDIFF(day,car_reg_dt,GETDATE()) as u_dt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select  DATEDIFF(day,inv_reg_dt,GETDATE()) as u_dt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a2, case a.sas_asset_cat_cd when '01' then (select com_price_amt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '02' then (select car_price_amt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select inv_price_amt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a3 from (select * from ast_staff_asset  where sas_cond_sts_cd = '03' and ISNULL(sas_dispose_cfm_ind,'' ) !='Y' and sas_staff_no='" + Session["New"].ToString() + "') as a left join Ref_ast_kategori as rk on rk.ast_kategori_code=a.sas_asset_cat_cd left join Ref_ast_jenis_aset as rja on rja.ast_jeniaset_Code=a.sas_asset_type_cd left join ast_cmn_asset as aca on aca.cas_asset_cd=a.sas_asset_cd and aca.cas_asset_cat_cd=a.sas_asset_cat_cd and aca.cas_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and aca.cas_asset_type_cd=a.sas_asset_type_cd left join hr_organization as ho on ho.org_gen_id=a.sas_org_id left join ast_dispose as dd on dd.dis_asset_id=a.sas_asset_id", con);
        SqlCommand cmd2 = new SqlCommand("select *,case when jenis_bajet_type='1' then kat_bajet else kod_bajet end as kod_akaun1 from KW_Ref_kod_bajet", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        gv_refdata.DataSource = ds2;
        gv_refdata.DataBind();

    }

    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_carta_akaun.aspx");
    }

   
   

   
    void show_ddvalue()
    {
        //if (RadioButton1.Checked == true)
        //{
        //    set_kakaun.Visible = true;
        //}
        //else
        //{
        //    set_kakaun.Visible = false;
        //}
    }

   
    protected void clk_update(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label og_genid = (System.Web.UI.WebControls.Label)gvRow.FindControl("og_genid");

        System.Web.UI.WebControls.Label lbl1 = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label4");
        System.Web.UI.WebControls.Label lbl2 = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label5");
        System.Web.UI.WebControls.Label lbl3 = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label6");
        string ogid = og_genid.Text;
        DataTable get_value = new DataTable();
        get_value = DBCon.Ora_Execute_table("select * From KW_Ref_kod_bajet where bjt_Id='" + og_genid.Text + "'");
        if (get_value.Rows.Count != 0)
        {
            string fmdate = string.Empty, tmdate = string.Empty, tmdate1 = string.Empty;
            //if (Tk_mula.Text != "")
            //{
            //    string fdate = Tk_mula.Text;
            //    DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    fmdate = fd.ToString("yyyy-MM-dd");
            //}
            //if (Tk_akhir.Text != "")
            //{
            //    string tdate = Tk_akhir.Text;
            //    DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    tmdate = td.ToString("yyyy-MM-dd");
            //}

            DataTable sel_val3 = new DataTable();
            sel_val3 = DBCon.Ora_Execute_table("select *,Format(Ref_tk_mula,'dd/MM/yyyy') s1,Format(Ref_tk_akhir,'dd/MM/yyyy') s2 From KW_Ref_Bajet where Ref_kod_bajet='" + get_value.Rows[0]["kod_bajet"].ToString() + "' and Ref_kat_bajet='" + lbl1.Text + "' and ref_bjt_year ='" + tah_kewangan.SelectedValue + "'");

            if(sel_val3.Rows.Count != 0)
            {
                TextBox5.Text = sel_val3.Rows[0]["s1"].ToString();
                TextBox6.Text = sel_val3.Rows[0]["s2"].ToString();
                dd_kodind.SelectedValue = sel_val3.Rows[0]["Ref_kat_bajet"].ToString();
                TextBox4.Text = double.Parse(sel_val3.Rows[0]["Ref_jumlah_bajet"].ToString()).ToString("C").Replace("RM","").Replace("$", "");
                txt_nota.Value = sel_val3.Rows[0]["Ref_nota_bajet"].ToString();
                Button4.Text = "Kemaskini";
            }
            else
            {
                Button4.Text = "Simpan";
            }

            hdr_txt.Text = "Selenggara Bajet";
            BindGrid();
            ModalPopupExtender1.Show();
          

            ver_id.Text = "1";

            //Button6.Visible = true;

            DataTable get_value2 = new DataTable();
            get_value2 = DBCon.Ora_Execute_table("select * From KW_Ref_kod_bajet where kod_bajet='" + get_value.Rows[0]["jenis_bajet"].ToString() + "'");
            if (get_value2.Rows.Count != 0)
            {
                TextBox2.Text = get_value2.Rows[0]["nama_bajet"].ToString();
                TextBox3.Text = get_value2.Rows[0]["kod_bajet"].ToString();
            }

            kat_akaun.SelectedValue = get_value.Rows[0]["kat_bajet"].ToString();
            TextBox1.Text = get_value.Rows[0]["nama_bajet"].ToString();
            //dd_akaun.SelectedValue = get_value.Rows[0]["bjt_under_jenis"].ToString();
            //dd_kodind.SelectedValue = get_value.Rows[0]["bjt_ct_kod_industry"].ToString();

            string[] thisArray = get_value.Rows[0]["kod_akaun"].ToString().Split(',');
            List<string> myList = new List<string>();
            myList.AddRange(thisArray);

            for (int i = 0; i < dd_akaun.Items.Count; i++)
            {
                if (myList.Contains(dd_akaun.Items[i].Value))
                {
                    dd_akaun.Items[i].Selected = true;
                }
            }

            dd_list_sts.SelectedValue = get_value.Rows[0]["bjt_Status"].ToString();
          
            if (get_value.Rows[0]["jenis_bajet_type"].ToString() == "1")
            {
                TextBox1.Attributes.Add("Readonly", "Readonly");
                ss1.Visible = false;
            }
            else
            {
                TextBox1.Attributes.Remove("Readonly");
                ss1.Visible = true;
            }
            get_id.Text = og_genid.Text;
            TextBox4.Focus();
            show_ddvalue();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        System.Web.UI.WebControls.Label lbl1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label1");
        System.Web.UI.WebControls.Label clmn1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("bal_type");
        System.Web.UI.WebControls.Label clmn2 = (System.Web.UI.WebControls.Label)e.Row.FindControl("kat_cd");
        System.Web.UI.WebControls.Label clmn3 = (System.Web.UI.WebControls.Label)e.Row.FindControl("og_genid");
        System.Web.UI.WebControls.Label clmn4 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label2");
        LinkButton Button3 = e.Row.FindControl("LinkButton1") as LinkButton;
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           

            if(lbl1.Text == "1")
            {
                e.Row.BackColor = Color.FromName("#D4D4D4");
            }
            else if (lbl1.Text == "2")
            {
                e.Row.BackColor = Color.FromName("#D2D7D3");
                clmn1.Attributes.Add("style", "padding-left:10px;");
                clmn2.Attributes.Add("style", "padding-left:10px;");
            }
            else if (lbl1.Text == "3")
            {
                e.Row.BackColor = Color.FromName("#E7E7E7");
                clmn1.Attributes.Add("style", "padding-left:25px;");
                clmn2.Attributes.Add("style", "padding-left:25px;");
            }
            else if (lbl1.Text == "4")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:40px;");
                clmn2.Attributes.Add("style", "padding-left:40px;");
            }
            else if (lbl1.Text == "5")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:55px;");
                clmn2.Attributes.Add("style", "padding-left:55px;");
            }
            else if (lbl1.Text == "6")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:70px;");
                clmn2.Attributes.Add("style", "padding-left:70px;");
            }
            else if (lbl1.Text == "7")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:85px;");
                clmn2.Attributes.Add("style", "padding-left:85px;");
            }
            else if (lbl1.Text == "8")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:100px;");
                clmn2.Attributes.Add("style", "padding-left:100px;");
            }

            if (clmn4.Text == "1")
            {
                e.Row.BackColor = Color.FromName("#FFFFFF");
                Button3.Attributes.Remove("style");
                //clmn1.Attributes.Add("style", "padding-left:60px;");
                //clmn2.Attributes.Add("style", "padding-left:60px;");
            }
            else
            {
                Button3.Attributes.Add("style", "display:none;");
            }
                     

        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        clr_txt();
    }

    void clr_txt()
    {
        get_profile_view();
        bind_kod_akaun();
        BindGrid();
        Button4.Text = "Simpan";
        hdr_txt.Text = "";
        kat_akaun.SelectedValue = "";
        TextBox2.Text = "";
        TextBox1.Text = "";
        dd_kodind.SelectedValue = "";
        dd_akaun.ClearSelection();
        RadioButton1.Checked = false;
        //set_kakaun.Visible = false;
        ss1.Visible = false;        
        TextBox4.Text = "";
        txt_nota.Value = "";
    }
    protected void clk_submit(object sender, EventArgs e)
    {

        string samt1 = string.Empty, samt2 = string.Empty;
    
        if (TextBox1.Text != "" && dd_kodind.SelectedValue != "" && TextBox4.Text != "")
        {
            string fmdate = string.Empty, tmdate = string.Empty, tmdate1 = string.Empty;
            if (TextBox5.Text != "")
            {
                string fdate = TextBox5.Text;
                DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                fmdate = fd.ToString("yyyy-MM-dd");                
            }
            if (TextBox6.Text != "")
            {
                string tdate = TextBox6.Text;
                DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tmdate = td.ToString("yyyy-MM-dd");                
            }
            string tot_amt = string.Empty, tot_amt1 = string.Empty;
            DataTable sel_val = new DataTable();
            sel_val = DBCon.Ora_Execute_table("select * From KW_Ref_kod_bajet where bjt_Id='" + get_id.Text + "'");
            if (sel_val.Rows.Count != 0)
            {
               
                    DataTable sel_val3 = new DataTable();
                    sel_val3 = DBCon.Ora_Execute_table("select * From KW_Ref_Bajet where Ref_kod_bajet='" + sel_val.Rows[0]["kod_bajet"].ToString() + "' and Ref_kat_bajet='" + dd_kodind.SelectedValue + "' and Ref_tk_mula='" + fmdate + "' and Ref_tk_akhir='" + tmdate + "' and ref_bjt_year='"+ tah_kewangan.SelectedValue +"' and Status='A'");
                    if (sel_val3.Rows.Count == 0)
                    {
                        string Inssql1 = "Insert into KW_Ref_Bajet (Ref_kod_bajet,Ref_kat_bajet,Ref_jumlah_bajet,Ref_tk_mula,Ref_tk_akhir,Status,Ref_nota_bajet,crt_id,cr_dt,ref_bjt_year) values('" + sel_val.Rows[0]["kod_bajet"].ToString() + "','" + dd_kodind.SelectedValue + "','" + double.Parse(TextBox4.Text).ToString("C").Replace("RM", "").Replace("$", "") + "','" + fmdate + "','" + tmdate + "','A','"+ txt_nota.Value +"','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+ tah_kewangan.SelectedValue +"')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                    }
                    else
                    {
                        string Inssql1 = "Update  KW_Ref_Bajet set Ref_jumlah_bajet='" + double.Parse(TextBox4.Text).ToString("C").Replace("RM", "").Replace("$", "") + "',Ref_nota_bajet='" + txt_nota.Value + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Ref_kod_bajet='" + sel_val.Rows[0]["kod_bajet"].ToString() + "' and Ref_kat_bajet='" + dd_kodind.SelectedValue + "' and Ref_tk_mula='" + fmdate + "' and Ref_tk_akhir='" + tmdate + "' and Status='A'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                    }
                    clr_txt();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                
            }
        }
        else
        {
            BindGrid();
            ModalPopupExtender1.Show();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }

    protected void ctk_values(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string query = " with Recurse as ("
                         + " select a.bjt_Id as DirectChildId, a.bjt_Id, a.kod_bajet"
                         + " from KW_Ref_kod_bajet a     union all"
                         + " select b.DirectChildId, a.bjt_Id, a.kod_bajet "
                         + " from KW_Ref_kod_bajet a "
                         + " join Recurse b on b.bjt_Id = a.bjt_under_parent)"
                         + " select * from (select a.DirectChildId, isnull(sum(cast(Ref_jumlah_bajet as money)),'0.00')  as Amount "
                         + " from Recurse a"
                         + " left join KW_Ref_Bajet b on a.kod_bajet = b.Ref_kod_bajet"
                         + " group by DirectChildId) as a "
                         + " inner join  (select m1.bjt_Id,m1.jenis_bajet_type,m1.kat_bajet,m1.nama_bajet,m1.kod_bajet,m1.jenis_bajet,m1.bjt_under_parent,isHeader,ISNULL(s1.Ref_jumlah_bajet,'0.00') as KW_Debit_amt,Ref_kat_bajet,Ref_tk_mula,Ref_tk_akhir "
                         + " from KW_Ref_kod_bajet m1 left join KW_Ref_Bajet s1 on s1.Ref_kod_bajet=m1.kod_bajet where m1.bjt_Status='A') as b on b.bjt_Id=a.DirectChildId";
        dt = DBCon.Ora_Execute_table(query);
        Rptviwer_baki.Reset();
        ds.Tables.Add(dt);

        List<DataRow> listResult = dt.AsEnumerable().ToList();
        listResult.Count();
        int countRow = 0;
        countRow = listResult.Count();

        Rptviwer_baki.LocalReport.DataSources.Clear();
        if (countRow != 0)
        {

            StringBuilder builder = new StringBuilder();
            string strFileName = string.Format("{0}.{1}", "SELENGGARA_BAJET_" + DateTime.Now.ToString("yyyyMMdd") + "", "csv");
            builder.Append("NAMA BAJET ,KOD BAJET, AMAUN (RM)" + Environment.NewLine);
            string oamt = string.Empty;
            foreach (GridViewRow row in GridView1.Rows)
            {
                string kodakaun = ((Label)row.FindControl("bal_type")).Text.ToString();
                string akaunname = ((Label)row.FindControl("kat_cd")).Text.ToString();
                string openamt = ((Label)row.FindControl("Label3")).Text.ToString();
                //string type = ((Label)row.FindControl("lbl4")).Text.ToString();
                //if (type == "1")
                //{
                //    oamt = "";
                //}
                //else
                //{
                //    oamt = openamt;
                //}

                builder.Append(akaunname.Replace(",", "").ToUpper() + "," + kodakaun + "," + openamt.Replace(",","") + Environment.NewLine);
            }
            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
            Response.Write(builder.ToString());
            Response.End();

        }
        else if (countRow == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul');", true);
        }

    }
}