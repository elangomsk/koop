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


public partial class kw_carta_akaun_view : System.Web.UI.Page
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
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        app_language();
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button5);
        if (!IsPostBack)
        {
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
                userid = Session["New"].ToString();
                //Button1.Visible = false;
                bind_kat_akaun();
                bind_kod_akaun();
                bind_kod_industry();
                bind_aliran_items();
                bind_pnl_items();
                BindData();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void bind_kod_akaun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kod_akaun,(kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end) as name from KW_Ref_Carta_Akaun where jenis_akaun_type !='1' order by kod_akaun asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_akaun.DataSource = dt;
            dd_akaun.DataTextField = "name";
            dd_akaun.DataValueField = "kod_akaun";
            dd_akaun.DataBind();
            dd_akaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void bind_aliran_items()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "SELECT ref_alrn_nama,ref_alrn_nama FROM KW_Ref_aliran_item WHERE ref_alrn_header='N'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ali_item.DataSource = dt;
            ali_item.DataTextField = "ref_alrn_nama";
            ali_item.DataValueField = "ref_alrn_nama";
            ali_item.DataBind();
            ali_item.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void bind_pnl_items()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "SELECT ref_pnl_nama FROM KW_Ref_pnl_item WHERE ref_pnl_header='Y'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            pnl_item.DataSource = dt;
            pnl_item.DataTextField = "ref_pnl_nama";
            pnl_item.DataValueField = "ref_pnl_nama";
            pnl_item.DataBind();
            pnl_item.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void bind_kod_industry()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select DISTINCT kod_industry,(kod_industry +' | ' + msic_desc) as name from Kw_kod_industry left join KW_Ref_Kod_Industri on msic_kod=kod_industry where kod_industry != ''";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_kodind.DataSource = dt;
            dd_kodind.DataTextField = "name";
            dd_kodind.DataValueField = "kod_industry";
            dd_kodind.DataBind();
            dd_kodind.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void bind_kat_akaun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kat_cd,UPPER(kat_akuan) kat_akuan from KW_Kategori_akaun order by kat_cd asc";
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
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());               
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

    protected void BindGrid()
    {
        string sqry = string.Empty;
      
        string query = "select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,m1.under_parent,case when s1.opening_amt > 0 then opening_amt else '0.00' end as KW_Debit_amt,case when s1.opening_amt < 0 then opening_amt else '0.00' end as KW_kredit_amt,ISNULL(kw_acc_header,'0') isHeader from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance s1 on s1.kod_akaun=m1.kod_akaun and s1.kat_akaun=m1.kat_akaun and s1.set_sts='1' " + sqry + " order by kat_akaun";

        //string query = "BEGIN  with  ITERATE_NODES_RECURSIVE AS (select a1.kat_akaun,a1.kod_akaun,a1.nama_akaun,a1.Id,a1.under_parent, case when a1.under_parent = '0' then a1.nama_akaun else a2.nama_akaun end as jenis_name, 0 as LEVEL_DEPTH,a1.KW_Debit_amt,a1.KW_kredit_amt,a1.Kw_open_amt,a1.under_jenis,a1.jenis_akaun_type from dbo.KW_Ref_Carta_Akaun a1 left join dbo.KW_Ref_Carta_Akaun as a2 on a2.kod_akaun=a1.jenis_akaun where (a1.Id IN (select Id from dbo.KW_Ref_Carta_Akaun where (under_parent = '0')))UNION ALL select super.kat_akaun,super.kod_akaun,super.nama_akaun,super.Id,super.under_parent,case when super.under_parent = '0' then super.nama_akaun else sub.nama_akaun end as jenis_name, sub.LEVEL_DEPTH + 1  as LEVEL_DEPTH,super.KW_Debit_amt,super.KW_kredit_amt,super.Kw_open_amt,super.under_jenis,super.jenis_akaun_type from dbo.KW_Ref_Carta_Akaun as super INNER JOIN ITERATE_NODES_RECURSIVE as sub on sub.id=super.under_parent ) select m1.Id,m1.jenis_akaun_type,m1.kat_akaun,m1.nama_akaun,m1.kod_akaun,m1.under_parent,case when s1.opening_amt > 0 then opening_amt else '0.00' end as KW_Debit_amt,case when s1.opening_amt < 0 then opening_amt else '0.00' end as KW_kredit_amt from ITERATE_NODES_RECURSIVE m1 left join KW_Opening_Balance s1 on s1.kod_akaun=m1.kod_akaun and s1.kat_akaun=m1.kat_akaun and s1.set_sts='1' " + sqry + " order by m1.kat_akaun,m1.Id,m1.LEVEL_DEPTH; END ";

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

    protected void BindGrid1()
    {
        //SqlCommand cmd2 = new SqlCommand("select ISNULL(ho.org_name,'') as org_name,ISNULL(dd.dis_dispose_type_cd,'') as dis_dispose_type_cd,rk.ast_kategori_desc,rja.ast_jeniaset_desc,aca.cas_asset_desc,a.sas_asset_id,a.sas_curr_price_amt, a.sas_asset_cat_cd,a.sas_asset_sub_cat_cd,a.sas_asset_type_cd,a.sas_asset_cd,a.sas_org_id, case a.sas_asset_cat_cd when '01' then (select FORMAT(com_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd) when '02' then (select FORMAT(car_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select FORMAT(inv_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a1, case a.sas_asset_cat_cd when '01' then (select DATEDIFF(day,com_reg_dt,GETDATE()) as u_dt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '02' then (select DATEDIFF(day,car_reg_dt,GETDATE()) as u_dt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select  DATEDIFF(day,inv_reg_dt,GETDATE()) as u_dt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a2, case a.sas_asset_cat_cd when '01' then (select com_price_amt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '02' then (select car_price_amt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select inv_price_amt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a3 from (select * from ast_staff_asset  where sas_cond_sts_cd = '03' and ISNULL(sas_dispose_cfm_ind,'' ) !='Y' and sas_staff_no='" + Session["New"].ToString() + "') as a left join Ref_ast_kategori as rk on rk.ast_kategori_code=a.sas_asset_cat_cd left join Ref_ast_jenis_aset as rja on rja.ast_jeniaset_Code=a.sas_asset_type_cd left join ast_cmn_asset as aca on aca.cas_asset_cd=a.sas_asset_cd and aca.cas_asset_cat_cd=a.sas_asset_cat_cd and aca.cas_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and aca.cas_asset_type_cd=a.sas_asset_type_cd left join hr_organization as ho on ho.org_gen_id=a.sas_org_id left join ast_dispose as dd on dd.dis_asset_id=a.sas_asset_id", con);
        SqlCommand cmd2 = new SqlCommand("select *,case when jenis_akaun_type='1' then kat_akaun else kod_akaun end as kod_akaun1 from KW_Ref_Carta_Akaun", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        gv_refdata.DataSource = ds2;
        gv_refdata.DataBind();

    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_refdata.PageIndex = e.NewPageIndex;
        gv_refdata.DataBind();
        BindData();
    }

    protected void aliran_chnaged(object sender, EventArgs e)
    {
        if(CheckBox3.Checked == true)
        {
            aliran_shw.Visible = true;
        }
        else
        {
            aliran_shw.Visible = false;
        }
        BindGrid();
        ModalPopupExtender1.Show();
    }

    protected void pnl_chnaged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            pnl_shw.Visible = true;
        }
        else
        {
            pnl_shw.Visible = false;
        }
        BindGrid();
        ModalPopupExtender1.Show();
    }


    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_carta_akaun.aspx");
    }

   
    protected void ctk_values(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt = DBCon.Ora_Execute_table("select *,case when jenis_akaun_type='1' then kat_akaun else kod_akaun end as kod_akaun1 from KW_Ref_Carta_Akaun");
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
            string strFileName = string.Format("{0}.{1}", "CARTA_AKAUN_" + DateTime.Now.ToString("yyyyMMdd") + "", "csv");
            builder.Append("NAMA AKAUN ,KOD AKAUN" + Environment.NewLine);
            string oamt = string.Empty;
            foreach (GridViewRow row in gv_refdata.Rows)
            {
                string kodakaun = ((Label)row.FindControl("lbl1")).Text.ToString();
                string akaunname = ((Label)row.FindControl("lbl2")).Text.ToString();
                string openamt = ((Label)row.FindControl("lbl3")).Text.ToString();
                string type = ((Label)row.FindControl("lbl4")).Text.ToString();
                if (type == "1")
                {
                    oamt = "";
                }
                else
                {
                    oamt = openamt;
                }

                builder.Append(akaunname.Replace(",", "").ToUpper() + "," + kodakaun + Environment.NewLine);
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

    protected void clk_new(object sender, EventArgs e)
    {
        try
        {
            
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label og_genid = (System.Web.UI.WebControls.Label)gvRow.FindControl("og_genid");
            string ogid = og_genid.Text;

            DataTable get_value1_1 = new DataTable();
            get_value1_1 = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where Id='" + og_genid.Text + "'");

            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * from KW_Ref_Carta_Akaun where jenis_akaun='" + get_value1_1.Rows[0]["kod_akaun"].ToString() + "'");
            //if (ddokdicno.Rows.Count == 0 && get_value.Rows[0]["jenis_akaun_type"].ToString() != "1")
            BindGrid();
            ModalPopupExtender1.Show();
            if (get_value1_1.Rows[0]["jenis_akaun_type"].ToString() != "1")
            {
                if (ddokdicno.Rows.Count == 0)
                {
                    if (get_value1_1.Rows[0]["sts_kawalan"].ToString() != "Y")
                    {
                        dd_list_sts.Attributes.Remove("Readonly");
                        dd_list_sts.Attributes.Remove("Style");

                    }
                    else
                    {
                        dd_list_sts.Attributes.Add("Readonly", "Readonly");
                        dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
                    }
                }
                else
                {
                    dd_list_sts.Attributes.Add("Readonly", "Readonly");
                    dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
                }

                if (ddokdicno.Rows.Count == 0 && get_value1_1.Rows[0]["jenis_akaun_type"].ToString() == "1" && get_value1_1.Rows[0]["sts_kawalan"].ToString() != "Y")
                {
                    dd_list_sts.Attributes.Remove("Readonly");
                    dd_list_sts.Attributes.Remove("Style");
                }
                else if (ddokdicno.Rows.Count == 0 && get_value1_1.Rows[0]["jenis_akaun_type"].ToString() != "1" && get_value1_1.Rows[0]["sts_kawalan"].ToString() != "Y")
                {
                    dd_list_sts.Attributes.Remove("Readonly");
                    dd_list_sts.Attributes.Remove("Style");
                }
                else if (ddokdicno.Rows.Count != 0 && get_value1_1.Rows[0]["jenis_akaun_type"].ToString() != "1" && get_value1_1.Rows[0]["sts_kawalan"].ToString() != "Y")
                {
                    dd_list_sts.Attributes.Add("Readonly", "Readonly");
                    dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
                }
                else if (ddokdicno.Rows.Count != 0 && get_value1_1.Rows[0]["jenis_akaun_type"].ToString() != "1" && get_value1_1.Rows[0]["sts_kawalan"].ToString() == "Y")
                {
                    dd_list_sts.Attributes.Add("Readonly", "Readonly");
                    dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
                }
            }
            else
            {
                dd_list_sts.Attributes.Add("Readonly", "Readonly");
                dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
            }
            hdr_txt.Text = "New Carta Akaun";
            DataTable get_value1 = new DataTable();
            get_value1 = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where Id='" + og_genid.Text + "'");
            if (get_value1.Rows.Count != 0)
            {
                
                kat_akaun.SelectedValue = get_value1.Rows[0]["kat_akaun"].ToString();

                TextBox2.Text = get_value1.Rows[0]["nama_akaun"].ToString();
                get_cd.Text = get_value1.Rows[0]["kod_akaun"].ToString();
                dd_kodind.SelectedValue = get_value1.Rows[0]["ct_kod_industry"].ToString();
                dd_list_sts.SelectedValue = get_value1.Rows[0]["Status"].ToString();
             

                if (get_value1.Rows[0]["kkk_rep"].ToString() == "1")
                {
                    CheckBox1.Checked = true;
                }
                else
                {
                    CheckBox1.Checked = false;
                }

                if (get_value1.Rows[0]["PAL_rep"].ToString() == "1")
                {
                    CheckBox2.Checked = true;
                }
                else
                {
                    CheckBox2.Checked = false;
                }

                if (get_value1.Rows[0]["AT_rep"].ToString() == "1")
                {
                    CheckBox3.Checked = true;
                }
                else
                {
                    CheckBox3.Checked = false;
                }

                if (get_value1.Rows[0]["AP_rep"].ToString() == "1")
                {
                    CheckBox4.Checked = true;
                }
                else
                {
                    CheckBox4.Checked = false;
                }

                if (get_value1.Rows[0]["COGS_rep"].ToString() == "1")
                {
                    CheckBox5.Checked = true;
                }
                else
                {
                    CheckBox5.Checked = false;
                }

                if (get_value1.Rows[0]["ca_cyp"].ToString() == "1")
                {
                    CheckBox7.Checked = true;
                }

                if (get_value1.Rows[0]["ca_re"].ToString() == "1")
                {
                    CheckBox8.Checked = true;                    
                }

                if (get_value1.Rows[0]["jenis_akaun_type"].ToString() != "1")
                {
                    ss1.Visible = true;
                }
                else
                {
                    ss1.Visible = false;
                }
            }
            TextBox1.Text = "";
            TextBox1.Focus();
            ver_id.Text = "0";
            show_ddvalue();
            get_id.Text = og_genid.Text;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void clk_yes(object sender, EventArgs e)
    {
        BindGrid();
        ModalPopupExtender1.Show();
        show_ddvalue();
    }

  
    void show_ddvalue()
    {
        if (RadioButton1.Checked == true)
        {
            set_kakaun.Visible = true;
        }
        else
        {
            set_kakaun.Visible = false;
        }
    }

   
    protected void clk_update(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label og_genid = (System.Web.UI.WebControls.Label)gvRow.FindControl("og_genid");
        string ogid = og_genid.Text;
        DataTable get_value = new DataTable();
        get_value = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where Id='" + og_genid.Text + "'");
        if (get_value.Rows.Count != 0)
        {
            hdr_txt.Text = "Update Carta Akaun";
            BindGrid();
            ModalPopupExtender1.Show();
            Button4.Text = "Kemaskini";

            DataTable chk_rpt1 = new DataTable();
            chk_rpt1 = DBCon.Ora_Execute_table("select kod_akaun,ISNULL(ca_cyp,'') as val1 from KW_Ref_Carta_Akaun where ISNULL(ca_cyp,'')='1'");
            if(chk_rpt1.Rows.Count != 0)
            {
                if(chk_rpt1.Rows[0]["val1"].ToString() == "1")
                {
                    if (chk_rpt1.Rows[0]["kod_akaun"].ToString() == get_value.Rows[0]["kod_akaun"].ToString())
                    {
                        rpt_shw1.Visible = true;
                    }
                    else
                    {
                        rpt_shw1.Visible = false;
                    }
                }
                else
                {
                    rpt_shw1.Visible = true;
                }

            }
            else
            {
                rpt_shw1.Visible = true;
            }

            DataTable chk_rpt2 = new DataTable();
            chk_rpt2 = DBCon.Ora_Execute_table("select kod_akaun,ISNULL(ca_re,'') as val2 from KW_Ref_Carta_Akaun where ISNULL(ca_re,'')='1'");
            if (chk_rpt2.Rows.Count != 0)
            {
                if (chk_rpt2.Rows[0]["kod_akaun"].ToString() == get_value.Rows[0]["kod_akaun"].ToString())
                {
                    rpt_shw2.Visible = true;
                }
                else
                {
                    rpt_shw2.Visible = false;
                }

            }
            else
            {
                rpt_shw2.Visible = true;
            }


            ver_id.Text = "1";

            //Button6.Visible = true;
            DataTable ddokdicno_tmp_ali = new DataTable();
            ddokdicno_tmp_ali = DBCon.Ora_Execute_table("select m1.*,s1.ref_alrn_kod From kw_template_aliran m1 inner join KW_Ref_aliran_item s1 on s1.ref_alrn_nama=m1.ref_alrn_nama where tmp_kod_akaun='" + get_value.Rows[0]["kod_akaun"].ToString() + "' and tmp_Status='Y'");

            DataTable ddokdicno_tmp_pnl = new DataTable();
            ddokdicno_tmp_pnl = DBCon.Ora_Execute_table("select m1.*,s1.ref_pnl_kod From kw_template_pl m1 inner join KW_Ref_pnl_item s1 on s1.ref_pnl_nama=m1.ref_pnl_nama where tmp_kod_akaun='" + get_value.Rows[0]["kod_akaun"].ToString() + "' and tmp_Status='Y'");

            DataTable get_value2 = new DataTable();
            get_value2 = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='" + get_value.Rows[0]["jenis_akaun"].ToString() + "'");
            if (get_value2.Rows.Count != 0)
            {
                TextBox2.Text = get_value2.Rows[0]["nama_akaun"].ToString();
            }

            kat_akaun.SelectedValue = get_value.Rows[0]["kat_akaun"].ToString();
            TextBox1.Text = get_value.Rows[0]["nama_akaun"].ToString();
            dd_akaun.SelectedValue = get_value.Rows[0]["under_jenis"].ToString();
            dd_kodind.SelectedValue = get_value.Rows[0]["ct_kod_industry"].ToString();
            dd_list_sts.SelectedValue = get_value.Rows[0]["Status"].ToString();
            if (get_value.Rows[0]["kw_acc_header"].ToString() == "1")
            {
                CheckBox6.Checked = true;
            }
            else
            {
                CheckBox6.Checked = false;
            }
            if (get_value.Rows[0]["Susu_nilai"].ToString() == "1")
            {
                RadioButton1.Checked = true;
            }
            else
            {
                RadioButton1.Checked = false;
            }

            if (get_value.Rows[0]["kkk_rep"].ToString() == "1")
            {
                CheckBox1.Checked = true;
            }
            else
            {
                CheckBox1.Checked = false;
            }

          
            if (get_value.Rows[0]["ca_cyp"].ToString() == "1")
            {
                CheckBox7.Checked = true;                
            }
            if (get_value.Rows[0]["ca_re"].ToString() == "1")
            {
                CheckBox8.Checked = true;                
            }
            if (get_value.Rows[0]["AT_rep"].ToString() == "1")
            {
                CheckBox3.Checked = true;
                aliran_shw.Visible = true;
                if(ddokdicno_tmp_ali.Rows.Count != 0)
                {
                    ali_item.SelectedValue = ddokdicno_tmp_ali.Rows[0]["ref_alrn_nama"].ToString();
                }
            }
            else
            {
                aliran_shw.Visible = false;
                CheckBox3.Checked = false;
            }

            if (get_value.Rows[0]["PAL_rep"].ToString() == "1")
            {
                CheckBox2.Checked = true;
                pnl_shw.Visible = true;
                if (ddokdicno_tmp_pnl.Rows.Count != 0)
                {
                    pnl_item.SelectedValue = ddokdicno_tmp_pnl.Rows[0]["ref_nl_nama"].ToString();
                }
            }
            else
            {
                CheckBox2.Checked = false;
                pnl_shw.Visible = false;
            }

            if (get_value.Rows[0]["AP_rep"].ToString() == "1")
            {
                CheckBox4.Checked = true;
            }
            else
            {
                CheckBox4.Checked = false;
            }
            if (get_value.Rows[0]["COGS_rep"].ToString() == "1")
            {
                CheckBox5.Checked = true;
            }
            else
            {
                CheckBox5.Checked = false;
            }

            if (get_value.Rows[0]["jenis_akaun_type"].ToString() == "1")
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
            TextBox1.Focus();
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
        LinkButton Button1 = e.Row.FindControl("LinkButton2") as LinkButton;
        LinkButton Button2 = e.Row.FindControl("lnkView") as LinkButton;
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

            if (clmn4.Text == "0")
            {
                e.Row.BackColor = Color.FromName("#FFFFFF");
            }
           

            string get_id = string.Empty;

            DataTable get_value = new DataTable();
            //get_value = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where Id='" + clmn3.Text + "' and jenis_akaun NOT IN ('12.01')");
            get_value = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where Id='" + clmn3.Text + "'");
            if (get_value.Rows.Count != 0)
            {

                DataTable chk_kategory = new DataTable();
                chk_kategory = DBCon.Ora_Execute_table("select * From KW_Kategori_akaun where kat_cd='" + get_value.Rows[0]["kat_akaun"].ToString() + "'");
                if (chk_kategory.Rows[0]["Status"].ToString() != "T")
                {
                    if (get_value.Rows[0]["jenis_akaun_type"].ToString() == "10")
                    {
                        get_id = clmn3.Text;
                        Button2.Attributes.Add("style", "pointer-events:None; opacity: 0.5;");
                        Button3.Attributes.Remove("style");
                    }
                    if (get_value.Rows[0]["jenis_akaun_type"].ToString() == "1")
                    {
                        get_id = clmn3.Text;
                        if (get_value.Rows[0]["Status"].ToString() == "A")
                        {
                            Button2.Attributes.Remove("style");
                        }
                        else
                        {
                            Button2.Attributes.Add("style", "pointer-events:None; opacity: 0.5;");
                        }
                        Button3.Attributes.Remove("style");

                    }
                    else
                    {
                        get_id = clmn3.Text;
                        Button3.Attributes.Remove("style");
                        if (get_value.Rows[0]["Status"].ToString() == "A")
                        {
                            Button2.Attributes.Remove("style");
                        }
                        else
                        {
                            Button2.Attributes.Add("style", "pointer-events:None; opacity: 0.5;");
                        }
                    }


                    DataTable ddokdicno = new DataTable();
                    ddokdicno = DBCon.Ora_Execute_table("select * from KW_Ref_Carta_Akaun where jenis_akaun='" + get_value.Rows[0]["kod_akaun"].ToString() + "'");
                    //if (ddokdicno.Rows.Count == 0 && get_value.Rows[0]["jenis_akaun_type"].ToString() != "1")
                    if (ddokdicno.Rows.Count == 0)
                    {
                        if (get_value.Rows[0]["sts_kawalan"].ToString() != "Y")
                        {
                            Button1.Visible = true;
                        }
                        else
                        {
                            Button1.Visible = false;
                        }
                    }
                    else
                    {
                        Button1.Visible = false;
                    }

                    if (ddokdicno.Rows.Count == 0 && get_value.Rows[0]["jenis_akaun_type"].ToString() == "1" && get_value.Rows[0]["sts_kawalan"].ToString() != "Y")
                    {
                        Button1.Visible = true;
                    }
                    else if (ddokdicno.Rows.Count == 0 && get_value.Rows[0]["jenis_akaun_type"].ToString() != "1" && get_value.Rows[0]["sts_kawalan"].ToString() != "Y")
                    {
                        Button1.Visible = true;
                    }
                    else if (ddokdicno.Rows.Count != 0 && get_value.Rows[0]["jenis_akaun_type"].ToString() != "1" && get_value.Rows[0]["sts_kawalan"].ToString() != "Y")
                    {
                        Button1.Visible = false;
                    }
                    else if (ddokdicno.Rows.Count != 0 && get_value.Rows[0]["jenis_akaun_type"].ToString() != "1" && get_value.Rows[0]["sts_kawalan"].ToString() == "Y")
                    {
                        Button1.Visible = false;
                    }
                }
                else
                {
                    get_id = clmn3.Text;
                    Button3.Attributes.Remove("style");
                    Button2.Attributes.Add("style", "pointer-events:None; opacity: 0.5;");
                }
            }
            else
            {
                Button1.Visible = false;
                Button2.Attributes.Add("style", "Pointer-events:none; opacity: 0.5;");
                Button3.Attributes.Add("style", "Pointer-events:none; opacity: 0.5;");
            }


        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        clr_txt();
    }

    void clr_txt()
    {
        BindGrid();
        Button4.Text = "Simpan";
        hdr_txt.Text = "";
        kat_akaun.SelectedValue = "";
        TextBox1.Attributes.Remove("Readonly");
        TextBox2.Text = "";
        TextBox1.Text = "";
        dd_kodind.SelectedValue = "";
        RadioButton1.Checked = false;
        set_kakaun.Visible = false;
        ss1.Visible = false;
        CheckBox1.Checked = false;
        CheckBox2.Checked = false;
        CheckBox3.Checked = false;
        CheckBox4.Checked = false;
        CheckBox5.Checked = false;
        CheckBox6.Checked = false;
        CheckBox7.Checked = false;
        CheckBox8.Checked = false;
        aliran_shw.Visible = false;
        pnl_shw.Visible = false;
        rpt_shw1.Visible = false;
        rpt_shw2.Visible = false;
        ali_item.SelectedValue = "";
        pnl_item.SelectedValue = "";
        dd_list_sts.SelectedValue = "A";
    }
    protected void clk_submit(object sender, EventArgs e)
    {
        if (kat_akaun.SelectedValue != "" && TextBox1.Text != "")
        {
            string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
            DataTable cnt_no = new DataTable();


            string susnilai = string.Empty, ckbox1 = string.Empty, ckbox2 = string.Empty, ckbox3 = string.Empty, ckbox4 = string.Empty, ckbox5 = string.Empty, ckbox6 = string.Empty, ckbox7 = string.Empty, ckbox8 = string.Empty;
            if (RadioButton1.Checked == true)
            {
                susnilai = "1";
            }
            else
            {
                susnilai = "0";
            }

            if (CheckBox1.Checked == true)
            {
                ckbox1 = "1";
            }
            else
            {
                ckbox1 = "0";
            }

            if (CheckBox2.Checked == true)
            {
                ckbox2 = "1";
            }
            else
            {
                ckbox2 = "0";
            }

            if (CheckBox3.Checked == true)
            {
                ckbox3 = "1";
            }
            else
            {
                ckbox3 = "0";
            }

            if (CheckBox4.Checked == true)
            {
                ckbox4 = "1";
            }
            else
            {
                ckbox4 = "0";
            }

            if (CheckBox5.Checked == true)
            {
                ckbox5 = "1";
            }
            else
            {
                ckbox5 = "0";
            }

            if (CheckBox6.Checked == true)
            {
                ckbox6 = "1";
            }
            else
            {
                ckbox6 = "0";
            }

            if (CheckBox7.Checked == true)
            {
                ckbox7 = "1";
            }
            else
            {
                ckbox7 = "0";
            }
            if (CheckBox8.Checked == true)
            {
                ckbox8 = "1";
            }
            else
            {
                ckbox8 = "0";
            }

            if (ver_id.Text == "0")
            {
                if (get_cd.Text != "")
                {

                    //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
                    cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='" + get_cd.Text + "'");

                    set_cnt1 = cnt_no.Rows[0]["cnt"].ToString();
                    mcnt = cnt_no.Rows[0]["rcnt"].ToString();
                    set_cnt = get_cd.Text;
                    //set_cnt1 = "1";
                }
                else
                {
                    set_cnt1 = "0";
                    mcnt = "1";
                    set_cnt = "00";
                }

                string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
                DataTable cnt_no1 = new DataTable();
                cnt_no1 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_akaun_cnt,'') +1) as mcnt From KW_Ref_Carta_Akaun where jenis_akaun='" + get_cd.Text + "' order by cast(kod_akaun_cnt as int) desc");

                if (cnt_no1.Rows.Count != 0)
                {
                    cnt_value = cnt_no1.Rows[0]["mcnt"].ToString();
                }
                else
                {
                    cnt_value = "1";
                }
                string chk_role = string.Empty;
                string[] sp_no = set_cnt.Split('.');
                int sp_no1 = sp_no.Length;
                string ss1 = string.Empty;
                for (int i = 0; i <= sp_no1; i++)
                {
                    if (i == (Int32.Parse(mcnt) - 1))
                    {
                        sno1 = cnt_value.PadLeft(2, '0');
                    }
                    else
                    {
                        sno1 = sp_no[i].ToString();
                    }
                    if (i < (sp_no1))
                    {
                        ss1 = ".";
                    }
                    else
                    {
                        ss1 = "";
                    }
                    chk_role += (sno1).PadLeft(2, '0') + "" + ss1;
                }

                string sso1 = string.Empty, sso2 = string.Empty, sso3 = string.Empty;
                if (set_cnt1 == "0")
                {
                    sso1 = "";
                }
                else
                {
                    if (cnt_no.Rows[0]["jenis_akaun_type"].ToString() == "1")
                    {
                        sso1 = chk_role + "," + cnt_no.Rows[0]["kod_akaun"].ToString();
                    }
                    else
                    {
                        sso1 = chk_role + "," + cnt_no.Rows[0]["under_jenis"].ToString();
                    }
                }

                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + TextBox1.Text + "'");
                if (ddokdicno.Rows.Count == 0)
                {


                    string Inssql = "insert into KW_Ref_Carta_Akaun(kat_akaun,nama_akaun,kod_akaun,jenis_akaun,under_parent,KW_Debit_amt,KW_kredit_amt,Kw_open_amt,jenis_akaun_type,under_jenis,Status,crt_id,cr_dt,Susu_nilai,kod_akaun_cnt,kkk_rep,PAL_rep,AT_rep,AP_rep,COGS_rep,ct_kod_industry,kw_acc_header,ca_cyp,ca_re) values ('" + kat_akaun.SelectedValue + "','" + TextBox1.Text.Replace("'", "''") + "','" + chk_role + "','" + get_cd.Text + "','" + set_cnt1 + "','0.00','0.00','0.00','" + mcnt + "','" + dd_akaun.SelectedValue + "','" + dd_list_sts.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + susnilai + "','" + cnt_value + "','" + ckbox1 + "','" + ckbox2 + "','" + ckbox3 + "','" + ckbox4 + "','" + ckbox5 + "','" + dd_kodind.SelectedValue + "','" + ckbox6 + "','"+ ckbox7 + "','" + ckbox8 + "')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        if(ckbox3 == "1")
                        {
                            DataTable ddokdicno_tmp_ali = new DataTable();
                            ddokdicno_tmp_ali = DBCon.Ora_Execute_table("select * From kw_template_aliran where tmp_kod_akaun='" + chk_role + "' and tmp_Status='Y'");
                            if (ddokdicno_tmp_ali.Rows.Count == 0)
                            {
                                string Ins_tmp = "insert into kw_template_aliran(tmp_kod_akaun,ref_alrn_nama,tmp_Status) values ('" + chk_role + "','" + ali_item.SelectedItem.Text + "','Y')";
                                Status = DBCon.Ora_Execute_CommamdText(Ins_tmp);
                            }
                            else
                            {
                                string Upd_tmp = "Update kw_template_aliran set ref_alrn_nama='" + ali_item.SelectedItem.Text + "' where tmp_kod_akaun='" + chk_role + "' and tmp_Status='Y'";
                                Status = DBCon.Ora_Execute_CommamdText(Upd_tmp);
                            }
                        }

                        if (ckbox2 == "1")
                        {
                            DataTable ddokdicno_tmp_ali = new DataTable();
                            ddokdicno_tmp_ali = DBCon.Ora_Execute_table("select * From kw_template_pl where tmp_kod_akaun='" + chk_role + "' and tmp_Status='Y'");
                            if (ddokdicno_tmp_ali.Rows.Count == 0)
                            {
                                string Ins_tmp = "insert into kw_template_pl(tmp_kod_akaun,ref_alrn_nama,tmp_Status) values ('" + chk_role + "','" + pnl_item.SelectedItem.Text + "','Y')";
                                Status = DBCon.Ora_Execute_CommamdText(Ins_tmp);
                            }
                            else
                            {
                                string Upd_tmp = "Update kw_template_pl set ref_alrn_nama='" + pnl_item.SelectedItem.Text + "' where tmp_kod_akaun='" + chk_role + "' and tmp_Status='Y'";
                                Status = DBCon.Ora_Execute_CommamdText(Upd_tmp);
                            }
                        }
                        //Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        //Session["validate_success"] = "SUCCESS";
                        //Response.Redirect("../kewengan/kw_carta_akaun_view.aspx");
                        clr_txt();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success'});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Memasukkan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                string Inssql = "UPDATE KW_Ref_Carta_Akaun set ct_kod_industry='" + dd_kodind.SelectedValue + "',kat_akaun='" + kat_akaun.SelectedValue + "',under_jenis='" + dd_akaun.SelectedValue + "',nama_akaun='" + TextBox1.Text.Replace("'", "''") + "',Susu_nilai='" + susnilai + "',kkk_rep='" + ckbox1 + "',PAL_rep='" + ckbox2 + "',AT_rep='" + ckbox3 + "',AP_rep='" + ckbox4 + "',COGS_rep='" + ckbox5 + "',Status='" + dd_list_sts.SelectedValue + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',kw_acc_header='" + ckbox6 + "',ca_cyp='"+ ckbox7 + "',ca_re='" + ckbox8 + "' where Id = '" + get_id.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    cnt_no = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where Id = '" + get_id.Text + "'");
                    string Inssql2 = "UPDATE KW_Ref_Carta_Akaun set ct_kod_industry='" + dd_kodind.SelectedValue + "',kkk_rep='" + ckbox1 + "',PAL_rep='" + ckbox2 + "',AT_rep='" + ckbox3 + "',AP_rep='" + ckbox4 + "',COGS_rep='" + ckbox5 + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where jenis_akaun = '" + cnt_no.Rows[0]["kod_akaun"].ToString() + "' ";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql2);
                    if (ckbox3 == "1")
                    {
                        DataTable ddokdicno_tmp_ali = new DataTable();
                        ddokdicno_tmp_ali = DBCon.Ora_Execute_table("select * From kw_template_aliran where tmp_kod_akaun='" + cnt_no.Rows[0]["kod_akaun"].ToString() + "' and tmp_Status='Y'");
                        if (ddokdicno_tmp_ali.Rows.Count == 0)
                        {
                            string Ins_tmp = "insert into kw_template_aliran(tmp_kod_akaun,ref_alrn_nama,tmp_Status) values ('" + cnt_no.Rows[0]["kod_akaun"].ToString() + "','" + ali_item.SelectedItem.Text + "','Y')";
                            Status = DBCon.Ora_Execute_CommamdText(Ins_tmp);
                        }
                        else
                        {
                            string Upd_tmp = "Update kw_template_aliran set ref_alrn_nama='" + ali_item.SelectedItem.Text + "' where tmp_kod_akaun='" + cnt_no.Rows[0]["kod_akaun"].ToString() + "' and tmp_Status='Y'";
                            Status = DBCon.Ora_Execute_CommamdText(Upd_tmp);
                        }

                        if(Status == "SUCCESS")
                        {
                            DataTable jenis_tmp_ali = new DataTable();
                            jenis_tmp_ali = DBCon.Ora_Execute_table("select kod_akaun From KW_Ref_Carta_Akaun where jenis_akaun = '" + cnt_no.Rows[0]["kod_akaun"].ToString() + "'");

                            if(jenis_tmp_ali.Rows.Count != 0)
                            {
                                for(int i = 0; i < jenis_tmp_ali.Rows.Count; i++)
                                {
                                    DataTable ddokdicno_tmp_ali1 = new DataTable();
                                    ddokdicno_tmp_ali1 = DBCon.Ora_Execute_table("select * From kw_template_aliran where tmp_kod_akaun='" + jenis_tmp_ali.Rows[i]["kod_akaun"].ToString() + "' and tmp_Status='Y'");
                                    if (ddokdicno_tmp_ali1.Rows.Count == 0)
                                    {
                                        string Ins_tmp = "insert into kw_template_aliran(tmp_kod_akaun,ref_alrn_nama,tmp_Status) values ('" + jenis_tmp_ali.Rows[i]["kod_akaun"].ToString() + "','" + ali_item.SelectedItem.Text + "','Y')";
                                        Status = DBCon.Ora_Execute_CommamdText(Ins_tmp);
                                    }
                                    else
                                    {
                                        string Upd_tmp = "Update kw_template_aliran set ref_alrn_nama='" + ali_item.SelectedItem.Text + "' where tmp_kod_akaun='" + jenis_tmp_ali.Rows[i]["kod_akaun"].ToString() + "' and tmp_Status='Y'";
                                        Status = DBCon.Ora_Execute_CommamdText(Upd_tmp);
                                    }

                                }
                            }

                        }
                    }
                    else
                    {
                        DataTable ddokdicno_tmp_ali = new DataTable();
                        ddokdicno_tmp_ali = DBCon.Ora_Execute_table("select * From kw_template_aliran where tmp_kod_akaun='" + cnt_no.Rows[0]["kod_akaun"].ToString() + "' and tmp_Status='Y'");
                        if (ddokdicno_tmp_ali.Rows.Count != 0)
                        {
                            string delete_tmp = "Delete from kw_template_aliran where tmp_kod_akaun='" + cnt_no.Rows[0]["kod_akaun"].ToString() + "' and tmp_Status='Y'";
                            Status = DBCon.Ora_Execute_CommamdText(delete_tmp);
                        }
                    }

                    if (ckbox2 == "1")
                    {
                        DataTable ddokdicno_tmp_ali = new DataTable();
                        ddokdicno_tmp_ali = DBCon.Ora_Execute_table("select * From kw_template_pl where tmp_kod_akaun='" + cnt_no.Rows[0]["kod_akaun"].ToString() + "' and tmp_Status='Y'");
                        if (ddokdicno_tmp_ali.Rows.Count == 0)
                        {
                            string Ins_tmp = "insert into kw_template_pl(tmp_kod_akaun,ref_alrn_nama,tmp_Status) values ('" + cnt_no.Rows[0]["kod_akaun"].ToString() + "','" + pnl_item.SelectedItem.Text + "','Y')";
                            Status = DBCon.Ora_Execute_CommamdText(Ins_tmp);
                        }
                        else
                        {
                            string Upd_tmp = "Update kw_template_pl set ref_alrn_nama='" + pnl_item.SelectedItem.Text + "' where tmp_kod_akaun='" + cnt_no.Rows[0]["kod_akaun"].ToString() + "' and tmp_Status='Y'";
                            Status = DBCon.Ora_Execute_CommamdText(Upd_tmp);
                        }

                        if (Status == "SUCCESS")
                        {
                            DataTable jenis_tmp_ali = new DataTable();
                            jenis_tmp_ali = DBCon.Ora_Execute_table("select kod_akaun From KW_Ref_Carta_Akaun where jenis_akaun = '" + cnt_no.Rows[0]["kod_akaun"].ToString() + "'");

                            if (jenis_tmp_ali.Rows.Count != 0)
                            {
                                for (int i = 0; i < jenis_tmp_ali.Rows.Count; i++)
                                {
                                    DataTable ddokdicno_tmp_ali1 = new DataTable();
                                    ddokdicno_tmp_ali1 = DBCon.Ora_Execute_table("select * From kw_template_pl where tmp_kod_akaun='" + jenis_tmp_ali.Rows[i]["kod_akaun"].ToString() + "' and tmp_Status='Y'");
                                    if (ddokdicno_tmp_ali1.Rows.Count == 0)
                                    {
                                        string Ins_tmp = "insert into kw_template_pl(tmp_kod_akaun,ref_alrn_nama,tmp_Status) values ('" + jenis_tmp_ali.Rows[i]["kod_akaun"].ToString() + "','" + pnl_item.SelectedItem.Text + "','Y')";
                                        Status = DBCon.Ora_Execute_CommamdText(Ins_tmp);
                                    }
                                    else
                                    {
                                        string Upd_tmp = "Update kw_template_pl set ref_alrn_nama='" + pnl_item.SelectedItem.Text + "' where tmp_kod_akaun='" + jenis_tmp_ali.Rows[i]["kod_akaun"].ToString() + "' and tmp_Status='Y'";
                                        Status = DBCon.Ora_Execute_CommamdText(Upd_tmp);
                                    }

                                }
                            }

                        }
                    }
                    else
                    {
                        DataTable ddokdicno_tmp_ali = new DataTable();
                        ddokdicno_tmp_ali = DBCon.Ora_Execute_table("select * From kw_template_pl where tmp_kod_akaun='" + cnt_no.Rows[0]["kod_akaun"].ToString() + "' and tmp_Status='Y'");
                        if (ddokdicno_tmp_ali.Rows.Count != 0)
                        {
                            string delete_tmp = "Delete from kw_template_pl where tmp_kod_akaun='" + cnt_no.Rows[0]["kod_akaun"].ToString() + "' and tmp_Status='Y'";
                            Status = DBCon.Ora_Execute_CommamdText(delete_tmp);
                        }
                    }
                    //Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    //Session["validate_success"] = "SUCCESS";
                    //Response.Redirect("../kewengan/kw_carta_akaun_view.aspx");
                    clr_txt();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success'});", true);
                }
                else
                {
                    BindData();
                    ModalPopupExtender1.Show();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Memasukkan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }

        }
        else
        {
            BindData();
            ModalPopupExtender1.Show();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }


    }
    protected void btn_hups_Click(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label og_genid = (System.Web.UI.WebControls.Label)gvRow.FindControl("og_genid");

        DataTable clk_hps = new DataTable();
        clk_hps = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where Id='" + og_genid.Text + "'");
        if (clk_hps.Rows.Count != 0)
        {
            string Inssql = "Delete from KW_Ref_Carta_Akaun where Id = '" + og_genid.Text + "'";
            Status = DBCon.Ora_Execute_CommamdText(Inssql);
            if (Status == "SUCCESS")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Dihapus Tidak Mungkin.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        BindData();

    }
}