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

public partial class Ast_pelupusan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection dbcon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty, str_qry = string.Empty;
    string clmfd = string.Empty, clm_name = string.Empty;
    string ss1 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Btn_Cetak);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                BindGrid();
                useid = Session["New"].ToString();
               
             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }



    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            var ddl = (DropDownList)e.Row.FindControl("dd_lab1");
            string com = "select UPPER(kaedah_desc) as kaedah_desc,kaedah_id from Ref_ast_kaedah_palupusan where Status ='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddl.DataSource = dt;
            ddl.DataTextField = "kaedah_desc";
            ddl.DataValueField = "kaedah_id";
            ddl.DataBind();
            ddl.SelectedValue = ((DataRowView)e.Row.DataItem)["dis_dispose_type_cd"].ToString();
            ddl.Items.Insert(0, new ListItem("--- PILIH ---", ""));



        }
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void BindGrid()
    {
        //SqlCommand cmd2 = new SqlCommand("select ISNULL(ho.org_name,'') as org_name,ISNULL(dd.dis_dispose_type_cd,'') as dis_dispose_type_cd,rk.ast_kategori_desc,rja.ast_jeniaset_desc,aca.cas_asset_desc,a.sas_asset_id,a.sas_curr_price_amt, a.sas_asset_cat_cd,a.sas_asset_sub_cat_cd,a.sas_asset_type_cd,a.sas_asset_cd,a.sas_org_id, case a.sas_asset_cat_cd when '01' then (select FORMAT(com_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd) when '02' then (select FORMAT(car_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select FORMAT(inv_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a1, case a.sas_asset_cat_cd when '01' then (select DATEDIFF(day,com_reg_dt,GETDATE()) as u_dt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '02' then (select DATEDIFF(day,car_reg_dt,GETDATE()) as u_dt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select  DATEDIFF(day,inv_reg_dt,GETDATE()) as u_dt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a2, case a.sas_asset_cat_cd when '01' then (select com_price_amt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '02' then (select car_price_amt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select inv_price_amt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a3 from (select * from ast_staff_asset  where sas_cond_sts_cd = '03' and ISNULL(sas_dispose_cfm_ind,'' ) !='Y' and sas_staff_no='" + Session["New"].ToString() + "') as a left join Ref_ast_kategori as rk on rk.ast_kategori_code=a.sas_asset_cat_cd left join Ref_ast_jenis_aset as rja on rja.ast_jeniaset_Code=a.sas_asset_type_cd left join ast_cmn_asset as aca on aca.cas_asset_cd=a.sas_asset_cd and aca.cas_asset_cat_cd=a.sas_asset_cat_cd and aca.cas_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and aca.cas_asset_type_cd=a.sas_asset_type_cd left join hr_organization as ho on ho.org_gen_id=a.sas_org_id left join ast_dispose as dd on dd.dis_asset_id=a.sas_asset_id", con);
        SqlCommand cmd2 = new SqlCommand("select ISNULL(ho.org_name,'') as org_name,ISNULL(dd.dis_dispose_type_cd,'') as dis_dispose_type_cd,rk.ast_kategori_desc,rja.ast_jeniaset_desc,aca.cas_asset_desc,a.sas_asset_id,a.sas_curr_price_amt, a.sas_asset_cat_cd,a.sas_asset_sub_cat_cd,a.sas_asset_type_cd,a.sas_asset_cd,a.sas_org_id, case a.sas_asset_cat_cd when '01' then (select FORMAT(com_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_component where com_asset_id=a.sas_asset_id) when '02' then (select FORMAT(car_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_car where car_asset_id=a.sas_asset_id) when '04' then (select FORMAT(pro_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_property where pro_asset_id=a.sas_asset_id) when '03' then (select FORMAT(inv_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_inventory where inv_asset_id=a.sas_asset_id) end as a1, case a.sas_asset_cat_cd when '01' then (select DATEDIFF(day,com_reg_dt,GETDATE()) as u_dt from ast_component where com_asset_id=a.sas_asset_id) when '02' then (select DATEDIFF(day,car_reg_dt,GETDATE()) as u_dt from ast_car where car_asset_id=a.sas_asset_id) when '04' then (select DATEDIFF(day,pro_reg_dt,GETDATE()) as u_dt from ast_property where pro_asset_id=a.sas_asset_id) when '03' then (select  DATEDIFF(day,inv_reg_dt,GETDATE()) as u_dt from ast_inventory where inv_asset_id=a.sas_asset_id) end as a2, case a.sas_asset_cat_cd when '01' then (select com_price_amt from ast_component where com_asset_id=a.sas_asset_id) when '02' then (select car_price_amt from ast_car where car_asset_id=a.sas_asset_id) when '04' then (select pro_buy_amt from ast_property where pro_asset_id=a.sas_asset_id) when '03' then (select inv_price_amt from ast_inventory where inv_asset_id=a.sas_asset_id) end as a3 from (select * from ast_staff_asset  where sas_dispose_cfm_ind !='Y' or sas_dispose_cfm_ind IS NULL and sas_cond_sts_cd IN ('03','04','05')) as a left join Ref_ast_kategori as rk on rk.ast_kategori_code=a.sas_asset_cat_cd left join Ref_ast_jenis_aset as rja on rja.ast_jeniaset_Code=a.sas_asset_type_cd and rja.ast_sub_cat_Code=a.sas_asset_sub_cat_cd and rja.ast_cat_Code=a.sas_asset_cat_cd left join ast_cmn_asset as aca on aca.cas_asset_cd=a.sas_asset_cd and aca.cas_asset_cat_cd=a.sas_asset_cat_cd and aca.cas_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and aca.cas_asset_type_cd=a.sas_asset_type_cd left join hr_organization as ho on ho.org_gen_id=a.sas_org_id left join ast_dispose as dd on dd.dis_asset_id=a.sas_asset_id", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);

        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            gvSelected.DataSource = ds2;
            gvSelected.DataBind();
            int columncount = gvSelected.Rows[0].Cells.Count;
            gvSelected.Rows[0].Cells.Clear();
            gvSelected.Rows[0].Cells.Add(new TableCell());
            gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
            gvSelected.Rows[0].Cells[0].Text = "<strong>Maklumat Carian Tidak Dijumpai</strong>";
            
        }
        else
        {
            gvSelected.DataSource = ds2;
            gvSelected.DataBind();
        }
    }

    protected void insert_values(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count1 = 0;
        foreach (GridViewRow gvrow in gvSelected.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.RadioButton;
            if (rb.Checked)
            {
                count1++;
            }
            rcount = count1.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in gvSelected.Rows)
            {
                RadioButton rbn = new RadioButton();
                rbn = (RadioButton)row.FindControl("RadioButton1");
                System.Web.UI.WebControls.Label ss = (System.Web.UI.WebControls.Label)row.FindControl("Label8");
                if (rbn.Checked)
                {
                    string ast_id = ((Label)row.FindControl("Label8")).Text.ToString(); //this store the  value in varName1
                    string ccd = ((Label)row.FindControl("Label_ccd")).Text.ToString().Trim();
                    string sccd = ((Label)row.FindControl("Label_sccd")).Text.ToString();
                    string atcd = ((Label)row.FindControl("Label_atcd")).Text.ToString();
                    string ascd = ((Label)row.FindControl("Label_acd")).Text.ToString();
                    string orid = ((Label)row.FindControl("Label_oid")).Text.ToString();
                    string ns_amt = ((Label)row.FindControl("Label12")).Text.ToString();
                    string purchase_dt = ((Label)row.FindControl("Label9")).Text.ToString();
                    string age = ((Label)row.FindControl("Label10")).Text.ToString();
                    string purchase_amt = ((Label)row.FindControl("Label11")).Text.ToString();
                    string dd_lst = ((DropDownList)row.FindControl("dd_lab1")).Text.ToString();

                    DateTime dt = DateTime.ParseExact(purchase_dt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    String pdate = dt.ToString("yyyy-MM-dd");

                    DataTable ddicno1 = new DataTable();
                    ddicno1 = dbcon.Ora_Execute_table("select * from ast_dispose where dis_asset_id='" + ast_id + "'");
                    if (ddicno1.Rows.Count == 0)
                    {
                        dbcon.Execute_CommamdText("INSERT INTO ast_dispose (dis_asset_id,dis_asset_cat_cd,dis_asset_sub_cat_cd,dis_asset_type_cd,dis_asset_cd,dis_dispose_type_cd,dis_dispose_reg_dt,dis_org_id,dis_purchase_dt,dis_asset_age,dis_purchase_amt,dis_curr_amt,dis_crt_id,dis_crt_dt) values ('" + ast_id + "','" + ccd + "','" + sccd + "','" + atcd + "','" + ascd + "','" + dd_lst + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + orid + "','" + pdate + "','" + age + "','" + purchase_amt + "','" + ns_amt + "','" + Session["new"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");

                        if (dd_lst == "03" || dd_lst == "04")
                        {
                            dbcon.Execute_CommamdText("UPDATE ast_staff_asset set flag_set = '1' where sas_asset_id='" + ast_id + "'");
                            if (ccd == "01")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_component set flag_set = '1' where com_asset_id='" + ast_id + "'");
                            }
                            else if (ccd == "02")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_car set flag_set = '1' where car_asset_id='" + ast_id + "'");
                            }
                            else if (ccd == "03")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_inventory set flag_set = '1' where inv_asset_id='" + ast_id + "'");
                            }
                            else if (ccd == "04")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_property set flag_set = '1' where pro_asset_id='" + ast_id + "'");
                            }

                        }
                        else if (dd_lst == "01" || dd_lst == "02")
                        {
                            dbcon.Execute_CommamdText("UPDATE ast_staff_asset set flag_set = '0' where sas_asset_id='" + ast_id + "'");
                            if (ccd == "01")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_component set flag_set = '0' where com_asset_id='" + ast_id + "'");
                            }
                            else if (ccd == "02")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_car set flag_set = '0' where car_asset_id='" + ast_id + "'");
                            }
                            else if (ccd == "03")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_inventory set flag_set = '0' where inv_asset_id='" + ast_id + "'");
                            }
                            else if (ccd == "04")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_property set flag_set = '0' where pro_asset_id='" + ast_id + "'");
                            }
                        }
                    }
                    else
                    {
                        dbcon.Execute_CommamdText("UPDATE ast_dispose SET dis_asset_age='" + age + "',dis_dispose_type_cd='" + dd_lst + "',dis_upd_id='" + Session["new"].ToString() + "',dis_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where dis_asset_id='" + ast_id + "'");
                        if (dd_lst == "03" || dd_lst == "04")
                        {
                            dbcon.Execute_CommamdText("UPDATE ast_staff_asset set flag_set = '1' where sas_asset_id='" + ast_id + "'");
                            if (ccd == "01")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_component set flag_set = '1' where com_asset_id='" + ast_id + "'");
                            }
                            else if (ccd == "02")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_car set flag_set = '1' where car_asset_id='" + ast_id + "'");
                            }
                            else if (ccd == "03")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_inventory set flag_set = '1' where inv_asset_id='" + ast_id + "'");
                            }
                            else if (ccd == "04")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_property set flag_set = '1' where pro_asset_id='" + ast_id + "'");
                            }
                        }
                        else if (dd_lst == "01" || dd_lst == "02")
                        {
                            dbcon.Execute_CommamdText("UPDATE ast_staff_asset set flag_set = '0' where sas_asset_id='" + ast_id + "'");
                            if (ccd == "01")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_component set flag_set = '0' where com_asset_id='" + ast_id + "'");
                            }
                            else if (ccd == "02")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_car set flag_set = '0' where car_asset_id='" + ast_id + "'");
                            }
                            else if (ccd == "03")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_inventory set flag_set = '0' where inv_asset_id='" + ast_id + "'");
                            }
                            else if (ccd == "04")
                            {
                                dbcon.Execute_CommamdText("UPDATE ast_property set flag_set = '0' where pro_asset_id='" + ast_id + "'");
                            }
                        }

                    }
                }
            }
            BindGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
        else
        {
            BindGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        string script = " $(function () {$(" + gvSelected.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }



    protected void ctk_values(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count1 = 0;
        foreach (GridViewRow gvrow in gvSelected.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.RadioButton;
            if (rb.Checked)
            {
                count1++;
            }
            rcount = count1.ToString();
        }
        if (rcount != "0")
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = dbcon.Ora_Execute_table("select ISNULL(ho.org_name,'') as org_name,ISNULL(dd.dis_dispose_type_cd,'') as dis_dispose_type_cd,rk.ast_kategori_desc,ISNULL(kk.kaedah_desc,'') kaedah_desc,rja.ast_jeniaset_desc,aca.cas_asset_desc,a.sas_asset_id,a.sas_curr_price_amt, a.sas_asset_cat_cd,a.sas_asset_sub_cat_cd,a.sas_asset_type_cd,a.sas_asset_cd,a.sas_org_id, case a.sas_asset_cat_cd when '01' then (select FORMAT(com_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_component where com_asset_id=a.sas_asset_id) when '02' then (select FORMAT(car_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_car where car_asset_id=a.sas_asset_id) when '04' then (select FORMAT(pro_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_property where pro_asset_id=a.sas_asset_id) when '03' then (select FORMAT(inv_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_inventory where inv_asset_id=a.sas_asset_id) end as a1, case a.sas_asset_cat_cd when '01' then (select DATEDIFF(day,com_reg_dt,GETDATE()) as u_dt from ast_component where com_asset_id=a.sas_asset_id) when '02' then (select DATEDIFF(day,car_reg_dt,GETDATE()) as u_dt from ast_car where car_asset_id=a.sas_asset_id) when '04' then (select DATEDIFF(day,pro_reg_dt,GETDATE()) as u_dt from ast_property where pro_asset_id=a.sas_asset_id) when '03' then (select  DATEDIFF(day,inv_reg_dt,GETDATE()) as u_dt from ast_inventory where inv_asset_id=a.sas_asset_id) end as a2, case a.sas_asset_cat_cd when '01' then (select com_price_amt from ast_component where com_asset_id=a.sas_asset_id) when '02' then (select car_price_amt from ast_car where car_asset_id=a.sas_asset_id) when '04' then (select pro_buy_amt from ast_property where pro_asset_id=a.sas_asset_id) when '03' then (select inv_price_amt from ast_inventory where inv_asset_id=a.sas_asset_id) end as a3 from (select * from ast_staff_asset  where sas_dispose_cfm_ind !='Y' or sas_dispose_cfm_ind IS NULL and sas_cond_sts_cd = '03') as a left join Ref_ast_kategori as rk on rk.ast_kategori_code=a.sas_asset_cat_cd left join Ref_ast_jenis_aset as rja on rja.ast_jeniaset_Code=a.sas_asset_type_cd left join ast_cmn_asset as aca on aca.cas_asset_cd=a.sas_asset_cd and aca.cas_asset_cat_cd=a.sas_asset_cat_cd and aca.cas_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and aca.cas_asset_type_cd=a.sas_asset_type_cd left join hr_organization as ho on ho.org_gen_id=a.sas_org_id left join ast_dispose as dd on dd.dis_asset_id=a.sas_asset_id left join Ref_ast_kaedah_palupusan as kk on kk.kaedah_id=dd.dis_dispose_type_cd");
            RptviwerStudent.Reset();
            ds.Tables.Add(dt);

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            RptviwerStudent.LocalReport.DataSources.Clear();
            if (countRow != 0)
            {
                RptviwerStudent.LocalReport.ReportPath = "Aset/ast_prp.rdlc";
                ReportDataSource rds = new ReportDataSource("astprp", dt);
                RptviwerStudent.LocalReport.DataSources.Add(rds);
                RptviwerStudent.LocalReport.Refresh();

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                string filename;

                if (sel_frmt.SelectedValue == "01")
                {
                    filename = string.Format("{0}.{1}", "PENDAFTARAN_REKOD_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                    byte[] bytes = RptviwerStudent.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
                else if (sel_frmt.SelectedValue == "02")
                {
                    StringBuilder builder = new StringBuilder();
                    string strFileName = string.Format("{0}.{1}", "PENDAFTARAN_REKOD_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                    builder.Append("Organisation ,Kategori Aset,Jenis Aset, Nama Aset, Aset ID, Tarikh Perolehan, Usia Aset, Nilai Perolehan (RM), Nilai Semasa (RM), Kaedah Pelupusan" + Environment.NewLine);
                    foreach (GridViewRow row in gvSelected.Rows)
                    {
                        string oname = ((Label)row.FindControl("Label3")).Text.ToString();
                        string kaset = ((Label)row.FindControl("Label2")).Text.ToString();
                        string jaset = ((Label)row.FindControl("Label6")).Text.ToString();
                        string naset = ((Label)row.FindControl("Label7")).Text.ToString();
                        string asid = ((Label)row.FindControl("Label8")).Text.ToString();
                        string tp = ((Label)row.FindControl("Label9")).Text.ToString();
                        string ua = ((Label)row.FindControl("Label10")).Text.ToString();
                        string np = ((Label)row.FindControl("Label11")).Text.ToString();
                        string ns = ((Label)row.FindControl("Label12")).Text.ToString();
                        string kp = ((DropDownList)row.FindControl("dd_lab1")).SelectedItem.Text.ToString();
                        builder.Append(oname + "," + kaset + "," + jaset + "," + naset + "," + asid + "," + tp + "," + ua + "," + np + "," + ns + "," + kp + Environment.NewLine);
                    }
                    Response.Clear();
                    Response.ContentType = "text/csv";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                    Response.Write(builder.ToString());
                    Response.End();
                }
                //else if (sel_frmt.SelectedValue == "03")
                //{
                //    byte[] bytes = RptviwerStudent.LocalReport.Render("Word", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                //    filename = string.Format("{0}.{1}", "PENDAFTARAN_REKOD_" + DateTime.Now.ToString("ddMMyyyy") + "", "doc");
                //    Response.Buffer = true;
                //    Response.Clear();
                //    Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                //    Response.ContentType = mimeType;
                //    Response.BinaryWrite(bytes);
                //    Response.Flush();
                //    Response.End();
                //}
            }
            else if (countRow == 0)
            {
                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            BindGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        string script = " $(function () {$(" + gvSelected.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_avail_ast.aspx");
    }

 
    
}