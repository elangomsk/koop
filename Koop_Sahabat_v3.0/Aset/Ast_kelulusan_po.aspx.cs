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
using System.Threading;

public partial class Ast_kelulusan_po : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    DBConnection dbcon = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    private static int PageSize = 20;
    DataTable dt = new DataTable();
    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty, var_nam = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Cetak);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
              
                Simpan.Visible = true;
              
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    txt_nopo.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    pengesahan();
                    stskelulusan();
                    txt_nokaki.Attributes.Add("Readonly", "Readonly");
                    txt_nanakaki.Attributes.Add("Readonly", "Readonly");
                    txt_org.Attributes.Add("Readonly", "Readonly");
                    txt_jaba.Attributes.Add("Readonly", "Readonly");
                    txt_unit.Attributes.Add("Readonly", "Readonly");
                    txt_tarik.Attributes.Add("Readonly", "Readonly");
                    DD_stsPengesahan1.Attributes.Add("readonly", "readonly");
                    DD_stsPengesahan1.Attributes.Add("Style", "pointer-events:none;");
                    txt_Catatan1.Attributes.Add("readonly", "readonly");
                    txt_Pengesah1.Attributes.Add("readonly", "readonly");
                    txt_Pengesahan1.Attributes.Add("readonly", "readonly");
                    txt_Pengesah2.Attributes.Add("readonly", "readonly");
                  
                    view_details();

                }
               
                useid = Session["New"].ToString();
               
             
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1894','563','1563','1564','205','484','1565','1405','1288','1566','1553','1895','1573','1574','53','1896','1868','1897','1793','53','1898','71','61','36','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;

            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());

            h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower()); 
            lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            h3_tag1.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());        
            lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
            h3_tag3.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            lbl8_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
            lbl9_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            lbl10_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
            lbl11_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
            h3_tag4.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
            lbl12_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());       
            lbl13_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            lbl14_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            lbl15_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Simpan.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            Cetak.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
         
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select pur_po_no from ast_purchase where pur_po_no like '%' + @Search + '%' group by pur_po_no order by pur_po_no";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    if (sdr.HasRows == true)
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["pur_po_no"].ToString());

                        }
                    }
                    else
                    {
                        countryNames.Add("Rekod Tidak Dijumpai.");
                    }
                }

                con.Close();
                return countryNames;
            }
        }
    }


    
    void view_details()
    {
        try
        {
            //Button7.Visible = false;
            if (txt_nopo.Text != "")
            {
                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select pur_po_no From ast_purchase where pur_po_no='" + txt_nopo.Text.Trim() + "' ");
                if (ddicno.Rows.Count > 0)
                {
                    string nopo = ddicno.Rows[0]["pur_po_no"].ToString();
                    DataTable ddbind = new DataTable();
                    ddbind = DBCon.Ora_Execute_table("select SF.stf_staff_no,SF.stf_name,org_name,hr_jaba_desc,hr_unit_desc,ISNULL(pur_crt_dt,'') as pur_crt_dt,pur_verify_sts_cd,pur_verify_remark,pur_verify_id,ISNULL(pur_verify_dt,'') as pur_verify_dt ,pur_approve_sts_cd , pur_approve_remark , pur_approve_id, FORMAT(ISNULL(pur_approve_dt,''),'dd/MM/yyyy', 'en-us')  as pur_approve_dt,pur_mp_sts,pur_inv_no,pur_pv_no,pur_cek_no,pur_tarikh_bayaran  From hr_staff_profile as SF  left join ast_purchase as PC on PC.pur_apply_staff_no=SF.stf_staff_no  left join hr_organization as ORG on ORG.org_gen_id=SF.str_curr_org_cd left join Ref_hr_unit as UN on UN.hr_unit_Code=SF.stf_curr_unit_cd left join Ref_hr_jabatan as JB on JB.hr_jaba_Code=SF.stf_curr_dept_cd left join [Ref_ast_sts_kelulusan] as KU on KU.ast_stskelulusan_code=PC.pur_approve_sts_cd where pur_po_no='" + nopo + "' and ISNULL(pur_del_ind,'') != 'D'");
                    //DD_Kategori.SelectedValue = ddbind.Rows[0]["sup_asset_cat_cd"].ToString().Trim();
                    //DD_Sub_Kateg.SelectedValue = ddbind.Rows[0]["sup_asset_sub_cat_cd"].ToString().Trim();
                    if (ddbind.Rows.Count > 0)
                    {
                        txt_nokaki.Text = ddbind.Rows[0]["stf_staff_no"].ToString();
                        txt_nanakaki.Text = ddbind.Rows[0]["stf_name"].ToString();
                        txt_org.Text = ddbind.Rows[0]["org_name"].ToString();
                        txt_jaba.Text = ddbind.Rows[0]["hr_jaba_desc"].ToString();
                        txt_unit.Text = ddbind.Rows[0]["hr_unit_desc"].ToString();
                        grid();
                        txt_tarik.Text = Convert.ToDateTime(ddbind.Rows[0]["pur_crt_dt"]).ToString("dd/MM/yyyy");
                        DD_stsPengesahan1.SelectedValue = ddbind.Rows[0]["pur_verify_sts_cd"].ToString().Trim();
                        txt_Catatan1.Text = ddbind.Rows[0]["pur_verify_remark"].ToString();
                        DataTable dt_ver = new DataTable();
                        dt_ver = DBCon.Ora_Execute_table("select stf_name from hr_staff_profile where stf_staff_no='" + ddbind.Rows[0]["pur_verify_id"].ToString() + "'");
                        if (dt_ver.Rows.Count > 0)
                        {
                            txt_Pengesah1.Text = dt_ver.Rows[0]["stf_name"].ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Purchase Not Yet Verified');", true);
                        }
                        txt_Pengesahan1.Text = Convert.ToDateTime(ddbind.Rows[0]["pur_verify_dt"]).ToString("dd/MM/yyyy");
                        DataTable dt = new DataTable();


                      
                        if (ddbind.Rows[0]["pur_approve_dt"].ToString() != "01/01/1900")
                        {
                            txt_Pengesahan2.Text = ddbind.Rows[0]["pur_approve_dt"].ToString();
                        }
                        else
                        {
                            txt_Pengesahan2.Text = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).ToString("dd/MM/yyyy");
                        }
                        DD_stsPengesahan2.SelectedValue = ddbind.Rows[0]["pur_approve_sts_cd"].ToString().Trim();
                        txt_Catatan2.Text = ddbind.Rows[0]["pur_approve_remark"].ToString();

                        //Kewangan

                       

                        if (ddbind.Rows[0]["pur_approve_sts_cd"].ToString() == "01")
                        {
                            DD_stsPengesahan2.Attributes.Add("Readonly", "Readonly");
                            DD_stsPengesahan2.Attributes.Add("style", "pointer-events:none;");
                            txt_Catatan2.Attributes.Add("Readonly", "Readonly");
                            txt_Pengesahan2.Attributes.Add("Readonly", "Readonly");
                            Simpan.Attributes.Add("Style", "display:None;");
                        }
                        else
                        {
                            DD_stsPengesahan2.Attributes.Remove("Readonly");
                            DD_stsPengesahan2.Attributes.Remove("Style");
                            txt_Catatan2.Attributes.Remove("Readonly");
                            Simpan.Attributes.Remove("Style");
                        }

                        if (ddbind.Rows[0]["pur_approve_id"].ToString() == "")
                        {
                            dt = DBCon.Ora_Execute_table("select stf_name from hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "'");
                            if (dt.Rows.Count != 0)
                            {
                                txt_Pengesah2.Text = dt.Rows[0][0].ToString();
                                Simpan.Attributes.Remove("Style");
                            }
                            else
                            {
                                txt_Pengesah2.Text = "";
                                Simpan.Attributes.Add("Style", "display:None;");
                            }
                        }
                        else
                        {
                            dt = DBCon.Ora_Execute_table("select stf_name from hr_staff_profile where stf_staff_no='" + ddbind.Rows[0]["pur_approve_id"].ToString() + "'");
                            if (dt.Rows.Count != 0)
                            {
                                txt_Pengesah2.Text = dt.Rows[0][0].ToString();
                                Simpan.Attributes.Remove("Style");
                            }
                            else
                            {
                                txt_Pengesah2.Text = "";
                                Simpan.Attributes.Add("Style", "display:None;");
                            }

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukan No PO.');", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void pengesahan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_pengesahan_code,UPPER(hr_pengesahan_desc) as hr_pengesahan_desc from Ref_hr_Pengesahan_sts where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_stsPengesahan1.DataSource = dt;
            DD_stsPengesahan1.DataTextField = "hr_pengesahan_desc";
            DD_stsPengesahan1.DataValueField = "hr_pengesahan_code";
            DD_stsPengesahan1.DataBind();
            DD_stsPengesahan1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();

    }

    void stskelulusan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select UPPER(ast_stskelulusan_desc) as ast_stskelulusan_desc,ast_stskelulusan_code From Ref_ast_sts_kelulusan where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_stsPengesahan2.DataSource = dt;
            DD_stsPengesahan2.DataTextField = "ast_stskelulusan_desc";
            DD_stsPengesahan2.DataValueField = "ast_stskelulusan_code";
            DD_stsPengesahan2.DataBind();
            DD_stsPengesahan2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void grid()
    {
        con.Open();
        DataTable ddicno = new DataTable();
        ddicno = DBCon.Ora_Execute_table("select pur_po_no From ast_purchase where pur_po_no='" + txt_nopo.Text.Trim() + "' ");
        string nopo = ddicno.Rows[0]["pur_po_no"].ToString();
        SqlCommand cmd = new SqlCommand("select ast_kategori_desc,ast_subkateast_desc,ast_jeniaset_desc,cas_asset_desc,pur_asset_qty,pur_verify_qty,pur_asset_amt,pur_asset_tot_amt,pur_mp_sts,pur_inv_no,pur_pv_no,pur_cek_no,format(pur_tarikh_bayaran,'dd/MM/yyyy') as pur_tarikh_bayaran from ast_purchase as PC left join Ref_ast_kategori as KE on KE.ast_kategori_Code=PC.pur_asset_cat_cd left join Ref_ast_sub_kategri_Aset as SKE on SKE.ast_subkateast_Code=PC.pur_asset_sub_cat_cd left join Ref_ast_jenis_aset as JE on JE.ast_jeniaset_Code=PC.pur_asset_type_cd and JE.ast_sub_cat_Code=PC.pur_asset_sub_cat_cd and JE.ast_cat_Code=PC.pur_asset_cat_cd left join ast_cmn_asset as KO on KO.cas_asset_cd=PC.pur_asset_cd and KO.cas_asset_cat_cd=PC.pur_asset_cat_cd and KO.cas_asset_sub_cat_cd=PC.pur_asset_sub_cat_cd and KO.cas_asset_type_cd=PC.pur_asset_type_cd where pur_po_no='" + nopo.Trim() + "' and pur_del_ind != 'D'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        con.Close();
    }

    protected void Simpan_Click(object sender, EventArgs e)
    {
        if (txt_nopo.Text != "")
        {
            if (DD_stsPengesahan2.SelectedValue != "" || txt_Catatan2.Text != "")
            {
                string sts_mb = string.Empty;
                if (DD_stsPengesahan2.SelectedValue == "01")
                {
                    sts_mb = "Proses";
                }
                else
                {
                    sts_mb = "";
                }

                DateTime today1 = DateTime.ParseExact(txt_Pengesahan2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string ctdt = today1.ToString("yyyy-MM-dd");
                DataTable dt2 = new DataTable();
                dt2 = DBCon.Ora_Execute_table("select stf_staff_no from hr_staff_profile where stf_name='" + txt_Pengesah2.Text + "'");

                SqlCommand ins_prof = new SqlCommand("update ast_purchase set pur_approve_sts_cd ='" + DD_stsPengesahan2.SelectedValue + "',pur_mp_sts='" + sts_mb + "',pur_approve_remark='" + txt_Catatan2.Text + "',pur_approve_id='" + dt2.Rows[0]["stf_staff_no"].ToString() + "',pur_approve_dt='" + ctdt + "',pur_upd_id='" + Session["New"].ToString() + "',pur_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where pur_po_no ='" + txt_nopo.Text + "'", con);
                con.Open();
                ins_prof.ExecuteNonQuery();
                con.Close();
                if (DD_stsPengesahan2.SelectedValue == "01")
                {
                    DD_stsPengesahan2.Attributes.Add("Readonly", "Readonly");
                    DD_stsPengesahan2.Attributes.Add("style", "pointer-events:none;");
                    txt_Catatan2.Attributes.Add("Readonly", "Readonly");
                    txt_Pengesahan2.Attributes.Add("Readonly", "Readonly");
                    Simpan.Attributes.Add("Style", "display:None;");
                }
                view_details();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No PO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void Cetak_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_nopo.Text != "")
            {
                DataTable dt = new DataTable();
                Page.Header.Title = "CARIAN MAKLUMAT BELIAN ASET";
                if ((DD_stsPengesahan2.SelectedValue != "") && (txt_Catatan2.Text != ""))
                {
                    DataSet dsCustomers = GetData("select SF.stf_staff_no,UPPER(SF.stf_name) stf_name,UPPER(org_name) org_name,UPPER(hr_jaba_desc) hr_jaba_desc,UPPER(hr_unit_desc) hr_unit_desc,UPPER(pur_crt_dt) hr_unit_desc ,UPPER(hr_pengesahan_desc) hr_pengesahan_desc,UPPER(pur_verify_remark) pur_verify_remark,pur_verify_id,pur_verify_dt,UPPER(ast_kategori_desc) ast_kateast_desc,UPPER(ast_subkateast_desc) ast_subkateast_desc,UPPER(ast_jeniaset_desc) ast_jeniaset_desc,UPPER(cas_asset_desc) ast_kodast_desc,pur_asset_qty,pur_verify_qty,pur_asset_amt,pur_asset_tot_amt   ,UPPER(ast_stskelulusan_desc) ast_stskelulusan_desc  ,UPPER(pur_approve_remark) pur_approve_remark , pur_approve_id, pur_approve_dt,format(pur_crt_dt,'dd/MM/yyyy') as pur_crt_dt  From hr_staff_profile as SF   left join ast_purchase as PC on PC.pur_apply_staff_no=SF.stf_staff_no   left join hr_organization as ORG on ORG.org_gen_id=SF.str_curr_org_cd  left join Ref_hr_unit as UN on UN.hr_unit_Code=SF.stf_curr_unit_cd  left join Ref_hr_jabatan as JB on JB.hr_jaba_Code=SF.stf_curr_dept_cd   left join Ref_ast_kategori as KE on KE.ast_kategori_Code=PC.pur_asset_cat_cd  left join Ref_ast_sub_kategri_Aset as SKE on SKE.ast_subkateast_Code=PC.pur_asset_sub_cat_cd  left join Ref_ast_jenis_aset as JE on JE.ast_jeniaset_Code=PC.pur_asset_type_cd and JE.ast_sub_cat_Code=PC.pur_asset_sub_cat_cd and JE.ast_cat_Code=PC.pur_asset_cat_cd  left join ast_cmn_asset as KO on KO.cas_asset_cd=PC.pur_asset_cd and KO.cas_asset_cat_cd=PC.pur_asset_cat_cd and KO.cas_asset_sub_cat_cd=PC.pur_asset_sub_cat_cd and KO.cas_asset_type_cd=PC.pur_asset_type_cd left join Ref_hr_Pengesahan_sts as PE on PE.hr_pengesahan_code=PC.pur_verify_sts_cd left join [Ref_ast_sts_kelulusan] as KU on KU.ast_stskelulusan_code=PC.pur_approve_sts_cd  where pur_po_no='" + txt_nopo.Text.Trim() + "' and ISNULL(pur_del_ind,'') != 'D' ");
                    dt = dsCustomers.Tables[0];
                }
                ReportViewer1.Reset();

                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

                if (countRow != 0)
                {
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    //Path
                    ReportViewer1.LocalReport.ReportPath = "Aset/AST_KELULUSAN_PO.rdlc";
                    //Parameters
                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("penname",txt_Pengesah2.Text ),
                     new ReportParameter("dt1",txt_Pengesahan1.Text ),
                     new ReportParameter("dt2",txt_Pengesahan2.Text ),
                };
                    //     //new ReportParameter("toDate",ToDate .Text )
                    //     //new ReportParameter("fromDate",datedari  ),
                    //     //new ReportParameter("toDate",datehingga ),
                    //          //new ReportParameter("caw",branch ),     
                    //            //new ReportParameter("Cdate",DateTime.Now.ToString("dd/MM/yyyy") ),     
                    //     //new ReportParameter("Date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  )
                    //     };


                    ReportViewer1.LocalReport.SetParameters(rptParams);
                    //Refresh
                    ReportViewer1.LocalReport.Refresh();

                    Warning[] warnings;

                    string[] streamids;

                    string mimeType;

                    string encoding;

                    string extension;

                    string devinfo = "<DeviceInfo>" +
              "  <OutputFormat>PDF</OutputFormat>" +
              "  <PageSize>A4</PageSize>" +
              "  <PageWidth>8in</PageWidth>" +
              "  <PageHeight>11in</PageHeight>" +
              "  <MarginTop>0.25in</MarginTop>" +
              "  <MarginLeft>0.5in</MarginLeft>" +
              "  <MarginRight>0.5in</MarginRight>" +
              "  <MarginBottom>0.5in</MarginBottom>" +
              "</DeviceInfo>";

                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF", devinfo, out mimeType, out encoding, out extension, out streamids, out warnings);

                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    string extfile = txt_nopo.Text.Trim();
                    Response.AddHeader("content-disposition", "attachment; filename=AST_KELULUSAN_PO_" + extfile + "." + extension);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                }
                else if (countRow == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No PO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            //txtError.Text = ex.ToString();

        }
    }

    private DataSet GetData(string query)
    {
        string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
        SqlCommand cmd = new SqlCommand(query);
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                cmd.CommandTimeout = 200;

                sda.SelectCommand = cmd;
                using (DataSet dsCustomers = new DataSet())
                {
                    sda.Fill(dsCustomers, "Payment");
                    return dsCustomers;
                }
            }
        }
    }
    //protected void Button5_Click(object sender, EventArgs e)
    //{
    //    Session["validate_success"] = "";
    //    Session["alrt_msg"] = "";
    //    Response.Redirect("../Aset/Ast_pengasahan_po.aspx");
    //}

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_kelulusan_po_view.aspx");
    }

    
}