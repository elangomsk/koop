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

public partial class kw_baki_pembukaan_view : System.Web.UI.Page
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


        string script = " $(document).ready(function () { $('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "<script>MakeStaticHeader('" + gv_refdata.ClientID + "', 600, '100%' , 40 ,true); </script>", false);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        app_language();

        if (!IsPostBack)
        {
            bind_tah_kew();
            //get_profile_view();
            if (Session["New"] != null)
            {
               
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    Session["validate_success"] = "";
                    Session["alrt_msg"] = "";
                    Session["pro_type"] = "";
                }
             
                userid = Session["New"].ToString();
                var samp = Request.Url.Query;

                if (samp != "")
                {
                    Button8.Visible = true;
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    tah_kewangan.SelectedValue = lbl_name.Text;
                }
                else
                {
                    Button8.Visible = false;
                }              
                BindData();
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
    protected void Back_to_profile(object sender, EventArgs e)
    {
        string name = Request.QueryString["prfle_cd"];
        //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
        Response.Redirect(string.Format("../kewengan/kw_profil_syarikat.aspx?edit={0}", name));
    }

  
    protected void BindGrid()
    {
        string sqry = string.Empty;
       
        DataTable chk_open_sts = new DataTable();
        chk_open_sts = DBCon.Ora_Execute_table("select fin_year from KW_financial_Year where fin_year='" + tah_kewangan.SelectedValue + "' and ISNULL(opening_sts,'0')='0'");
        if(chk_open_sts.Rows.Count == 0)
        {
            Button7.Visible = false;
            Session["confirm"] = "0";
        }
        else
        {
            Button7.Visible = true;
            Session["confirm"] = "1";
        }
        if (tah_kewangan.SelectedValue != "")
        {
            Button6.Visible = true;
        }
        else
        {
            Button6.Visible = false;
        }

        string query = "with Recurse as ( select a.Id as DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a where kat_akaun In ('01','02','03')  union all select b.DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  join Recurse b on b.Id = a.under_parent) select * from "
           + "(select a.DirectChildId, isnull(sum(cast(replace(opn_kredit_amt,'-','') as money)),'0.00')  as Amount , isnull(sum(cast(replace(opn_debit_amt,'-','') as money)),'0.00')  as Amount1  from Recurse a  left join KW_Opening_Balance b on a.kod_akaun = b.kod_akaun and Status='A' and opening_year='"+ tah_kewangan.SelectedValue + "' "
           + " group by DirectChildId)  as a   inner join  (select m1.Id,m1.jenis_akaun_type,ISNULL(kw_acc_header,'0') isHeader,m1.kat_akaun ,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,ISNULL(b.opn_kredit_amt,'0.00') as KW_kredit_amt,ISNULL(b.opn_debit_amt,'0.00') as KW_Debit_amt from KW_Ref_Carta_Akaun m1 "
           + "left join KW_Opening_Balance b on m1.kod_akaun = b.kod_akaun and b.Status='A' and opening_year='" + tah_kewangan.SelectedValue + "' "
           + " where m1.Status='A') as b on b.Id=a.DirectChildId  ";
        
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
                           
                            
                            decimal kredit = dt.AsEnumerable().Sum(row => row.Field<decimal>("KW_Kredit_amt"));
                            decimal Debit = dt.AsEnumerable().Sum(row => row.Field<decimal>("KW_Debit_amt"));
                            GridView1.FooterRow.Cells[1].Text = "<strong>JUMLAH KESELURUHAN (RM)</strong>";
                            GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;                            
                            GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[3].Text = kredit.ToString("C").Replace("RM", "").Replace("$", "");
                            GridView1.FooterRow.Cells[2].Text = Debit.ToString("C").Replace("RM", "").Replace("$", "");
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
        if (tah_kewangan.SelectedValue != "")
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label og_genid = (System.Web.UI.WebControls.Label)gvRow.FindControl("og_genid");

            System.Web.UI.WebControls.Label lbl3 = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label6");
            string ogid = og_genid.Text;
            DataTable get_value = new DataTable();
            get_value = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where Id='" + og_genid.Text + "'");
            if (get_value.Rows.Count != 0)
            {
                string fmdate = string.Empty, tmdate = string.Empty, tmdate1 = string.Empty;


                DataTable sel_val3 = new DataTable();
                sel_val3 = DBCon.Ora_Execute_table("select *,Format(start_dt,'dd/MM/yyyy') s1,Format(end_dt,'dd/MM/yyyy') s2 From kw_opening_balance where kod_akaun='" + get_value.Rows[0]["kod_akaun"].ToString() + "' and opening_year='" + tah_kewangan.SelectedValue + "'");

                DataTable chk_txn = new DataTable();
                chk_txn = DBCon.Ora_Execute_table("select * from kw_general_ledger where kod_akaun='" + get_value.Rows[0]["kod_akaun"].ToString() + "' and gl_post_dt between '" + fmdate + "' and '" + tmdate + "'");

                if (sel_val3.Rows.Count != 0)
                {
                    if (float.Parse(sel_val3.Rows[0]["opn_debit_amt"].ToString()) > 0)
                    {
                        dd_kodind.SelectedValue = "D";
                        TextBox4.Text = double.Parse(sel_val3.Rows[0]["opn_debit_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "").Replace("(", "").Replace(")", "");
                    }
                    else if (float.Parse(sel_val3.Rows[0]["opn_kredit_amt"].ToString()) > 0)
                    {
                        dd_kodind.SelectedValue = "K";
                        TextBox4.Text = double.Parse(sel_val3.Rows[0]["opn_kredit_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "").Replace("(", "").Replace(")", "");
                    }


                    Button4.Text = "Kemaskini";
                }
                else
                {
                    Button4.Text = "Simpan";
                }

                if (chk_txn.Rows.Count == 0)
                {
                    Button4.Visible = true;
                }
                else
                {
                    Button4.Visible = false;
                }

                hdr_txt.Text = "Baki Pembukaan";
              
                ModalPopupExtender1.Show();


                ver_id.Text = "1";

                //Button6.Visible = true;

                get_id.Text = og_genid.Text;
                TextBox4.Focus();
                show_ddvalue();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Tahun Kewangan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        BindGrid();
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


            if (lbl1.Text == "1")
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
                if (Session["confirm"].ToString() == "0")
                {
                    Button3.Attributes.Add("style", "display:none;");
                }
                else
                {
                    Button3.Attributes.Remove("style");
                }
                
               
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
        BindGrid();
        Button4.Text = "Simpan";
        hdr_txt.Text = "";
        dd_kodind.SelectedValue = "";
        TextBox4.Text = "";        
    }

    protected void fin_opn_close(object sender, EventArgs e)
    {
        DataTable chk_fin_year = new DataTable();
        chk_fin_year = DBCon.Ora_Execute_table("select fin_year,fin_kod_syarikat from KW_financial_Year where fin_year='" + tah_kewangan.SelectedValue + "' and ISNULL(opening_sts,'0')='0'");
        if (chk_fin_year.Rows.Count != 0)
        {
            string Updsql1 = "Update KW_financial_Year set  opening_sts='1',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where fin_kod_syarikat='" + chk_fin_year.Rows[0]["fin_kod_syarikat"].ToString() + "' and  fin_year='" + DateTime.Now.Year.ToString() + "'";
            Status = DBCon.Ora_Execute_CommamdText(Updsql1);
            if (Status == "SUCCESS")
            {
                string Updsql2 = "UPDATE KW_Opening_Balance set set_sts='1' where opening_year='" + tah_kewangan.SelectedValue + "' and set_sts='0'";
                Status = DBCon.Ora_Execute_CommamdText(Updsql2);

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }
        BindGrid();
    }
    protected void clk_submit(object sender, EventArgs e)
    {
        if (dd_kodind.SelectedValue != "" && TextBox4.Text != "")
        {
            string samt1 = string.Empty, samt2 = string.Empty, samt1_1 = string.Empty, samt2_1 = string.Empty, fmdate = string.Empty, tmdate = string.Empty;
            if (dd_kodind.SelectedValue == "K")
            {
                samt1 = TextBox4.Text;
                samt1_1 = TextBox4.Text;
                samt2_1 = "0.00";
            }
            else if (dd_kodind.SelectedValue == "D")
            {
                samt1 = "-" + TextBox4.Text;
                samt2_1 = TextBox4.Text;
                samt1_1 = "0.00";
            }
            else
            {
                samt1 = "0.00";
            }

        
         
                string tot_amt = string.Empty, tot_amt1 = string.Empty;
                DataTable sel_val = new DataTable();
                sel_val = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where Id='" + get_id.Text + "'");
                if (sel_val.Rows.Count != 0)
                {
                    //DataTable sel_val3 = new DataTable();
                    //sel_val3 = DBCon.Ora_Execute_table("select Kw_open_amt From KW_Ref_Carta_Akaun where Id='" + get_id.Text + "'");

                    //tot_amt1 = (double.Parse(sel_val3.Rows[0]["Kw_open_amt"].ToString()).ToString() + double.Parse(txt_debit.Text).ToString());


                    string Inssql = "UPDATE KW_Ref_Carta_Akaun set Kw_open_amt='" + samt1 + "',KW_Debit_amt='" + samt2_1 + "',KW_kredit_amt='" + samt1_1 + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id = '" + get_id.Text + "'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        DataTable sel_val3 = new DataTable();
                        sel_val3 = DBCon.Ora_Execute_table("select * From KW_Opening_Balance where kod_akaun='" + sel_val.Rows[0]["kod_akaun"].ToString() + "' and opening_year='" + tah_kewangan.SelectedValue + "' and set_sts='0'");
                    DataTable fin_ino_det = new DataTable();
                    fin_ino_det = DBCon.Ora_Execute_table("select fin_start_dt,fin_end_dt from KW_financial_Year where fin_year='" + tah_kewangan.SelectedValue + "'");
                    if (sel_val3.Rows.Count == 0)
                        {
                            string Inssql1 = "Insert into KW_Opening_Balance (kod_akaun,opeing_date,opening_year,Status,crt_id,cr_dt,opn_debit_amt,opn_kredit_amt,opening_amt,ending_amt,kat_akaun,set_sts,start_dt,end_dt) values('" + sel_val.Rows[0]["kod_akaun"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + tah_kewangan.SelectedValue + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + samt2_1 + "','" + samt1_1 + "','" + samt1 + "','" + samt1 + "','" + sel_val.Rows[0]["kat_akaun"].ToString() + "','0','" + fin_ino_det.Rows[0]["fin_start_dt"].ToString() + "','" + fin_ino_det.Rows[0]["fin_end_dt"].ToString() + "')";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                        }
                        else
                        {
                            string Inssql1 = "Update  KW_Opening_Balance set kat_akaun='" + sel_val.Rows[0]["kat_akaun"].ToString() + "',opening_amt='" + samt1 + "',ending_amt='" + samt1 + "',opn_debit_amt='"+ samt2_1 + "',opn_kredit_amt='"+ samt1_1 + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where kod_akaun='" + sel_val.Rows[0]["kod_akaun"].ToString() + "' and opening_year='" + tah_kewangan.SelectedValue + "'";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                        }
                        empty_fld();
                        BindData();
                        BindGrid();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {
                        ModalPopupExtender1.Show();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
            
        }
        else
        {
            BindGrid();
            ModalPopupExtender1.Show();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Jumlah yang sah.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    void empty_fld()
    {
        get_id.Text = "";
        dd_kodind.SelectedValue = "";
        TextBox4.Text = "";
    }
    protected void ctk_values(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string sqry = string.Empty, st_dt = string.Empty, ed_dt = string.Empty;
      
        string query = "with Recurse as ( select a.Id as DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a where kat_akaun In ('01','02','03')  union all select b.DirectChildId, a.Id, a.kod_akaun from KW_Ref_Carta_Akaun a  join Recurse b on b.Id = a.under_parent) select * from "
            + "(select a.DirectChildId, isnull(sum(cast(replace(opn_kredit_amt,'-','') as money)),'0.00')  as Amount , isnull(sum(cast(replace(opn_debit_amt,'-','') as money)),'0.00')  as Amount1  from Recurse a  left join KW_Opening_Balance b on a.kod_akaun = b.kod_akaun and Status='A' and set_sts='1' and opening_year='" + tah_kewangan.SelectedValue + "' "
            + " group by DirectChildId)  as a   inner join  (select m1.Id,m1.jenis_akaun_type,ISNULL(kw_acc_header,'0') isHeader,m1.kat_akaun ,m1.nama_akaun,m1.kod_akaun,m1.jenis_akaun,ISNULL(b.opn_kredit_amt,'0.00') as KW_kredit_amt,ISNULL(b.opn_debit_amt,'0.00') as KW_Debit_amt from KW_Ref_Carta_Akaun m1 "
            + "left join KW_Opening_Balance b on m1.kod_akaun = b.kod_akaun and b.Status='A' and set_sts='1' and opening_year='" + tah_kewangan.SelectedValue + "' "
            + " where m1.Status='A') as b on b.Id=a.DirectChildId  ";
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
            string strFileName = string.Format("{0}.{1}", "BAKI_PEMBUKAAN_" + DateTime.Now.ToString("yyyyMMdd") + "", "csv");
            builder.Append("NAMA AKAUN,KOD AKAUN, KREDIT (RM), DEBIT (RM)" + Environment.NewLine);
            string oamt = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                builder.Append(dt.Rows[i]["nama_akaun"].ToString() + "," + dt.Rows[i]["kod_akaun"].ToString() + "," + dt.Rows[i]["Amount"].ToString() + "," + dt.Rows[i]["Amount1"].ToString() + Environment.NewLine);
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