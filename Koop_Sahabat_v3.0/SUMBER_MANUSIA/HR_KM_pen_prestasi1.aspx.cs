using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.ComponentModel;
using System.Text;
using System.Net;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web.SessionState;
using System.Xml;

public partial class HR_KM_pen_prestasi1 : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string level, userid, stscd, abc1;
    string phdate1 = string.Empty, phdate2 = string.Empty, phdate3 = string.Empty, phdate4 = string.Empty;
    string etdate1 = string.Empty, etdate2 = string.Empty, etdate3 = string.Empty, etdate4 = string.Empty;
    string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty;
    string cyr1 = string.Empty, cyr2 = string.Empty;
    decimal total = 0M;
    decimal total1 = 0M;
    decimal total2 = 0M;
    decimal total_jmk1 = 0M, total_jmk2 = 0M, total_jmk3 = 0M, total_jmk4 = 0M;
    DataTable delete_tmp = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();                
                txt_tahun.Text = DateTime.Now.Year.ToString();
                var samp = Request.Url.Query;

                if (samp != "")
                {
                    Kaki_no.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();                    
                }

                s_nama.Attributes.Add("Readonly", "Readonly");
                s_jaw.Attributes.Add("Readonly", "Readonly");
                s_jab.Attributes.Add("Readonly", "Readonly");
                s_gred.Attributes.Add("Readonly", "Readonly");
                s_kj.Attributes.Add("Readonly", "Readonly");
                s_unit.Attributes.Add("Readonly", "Readonly");
                txt_org.Attributes.Add("Readonly", "Readonly");
                FirstGridViewRow();
               
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1460','448','505','484','16','1405','1288','190','1397','1566','513','1464','274','77','1979','1439','1441','1376','1980','1981','1982','1983','61','1985')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());   
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());      
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
           
            
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    private void FirstGridViewRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null; 
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dr = dt.NewRow();        
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        grvStudentDetails.DataSource = dt;
        grvStudentDetails.DataBind();
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRow1();
    }


    private void AddNewRow1()
    {
        int rowIndex = 0;
        float total = 0, total1 = 0;
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
                    System.Web.UI.WebControls.TextBox txt_box1 =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[0].FindControl("Col1");
                    System.Web.UI.WebControls.TextBox txt_box2 =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col2");
                    drCurrentRow = dtCurrentTable.NewRow();                    

                    dtCurrentTable.Rows[i - 1]["column1"] = txt_box1.Text;
                    dtCurrentTable.Rows[i - 1]["column2"] = txt_box2.Text;
                 
                    rowIndex++;
                  
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
        float total = 0, total1 = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Web.UI.WebControls.TextBox txt_box1 =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[0].FindControl("Col1");
                    System.Web.UI.WebControls.TextBox txt_box2 =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col2");
                    txt_box1.Text = dt.Rows[i]["column1"].ToString();
                    txt_box2.Text = dt.Rows[i]["column2"].ToString();                  
                    rowIndex++;
                }
            }
        }
        grid_list();
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
                    System.Web.UI.WebControls.TextBox txt_box1 =
                         (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[0].FindControl("Col1");
                    System.Web.UI.WebControls.TextBox txt_box2 =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col2");
                    drCurrentRow = dtCurrentTable.NewRow();                    
                    dtCurrentTable.Rows[i - 1]["Col1"] = txt_box1.Text;
                    dtCurrentTable.Rows[i - 1]["Col2"] = txt_box2.Text;
                  
                    rowIndex++;

                }

                ViewState["CurrentTable"] = dtCurrentTable;
               
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }        
    }

    void view_details()
    {
        try
        {
            if (Kaki_no.Text != "")
            {
              
                    DataTable select_kaki = new DataTable();
                select_kaki = DBCon.Ora_Execute_table("SELECT sp.stf_staff_no,ho.org_name,sp.stf_name,hg.hr_gred_desc,hj.hr_jaba_desc,pk.hr_kate_desc,hsp1.stf_name pen1,hsp2.stf_name pen2,sp.stf_curr_job_cat_cd,ss1.op_perg_name FROM hr_staff_profile sp INNER JOIN hr_post_his B ON sp.stf_staff_no=B.pos_staff_no left join Ref_hr_gred as hg on hg.hr_gred_Code=sp.stf_curr_grade_cd left join Ref_hr_jabatan as hj on hj.hr_jaba_Code=sp.stf_curr_dept_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sp.stf_curr_job_cat_cd left join Ref_hr_unit as hu on hu.hr_unit_Code=sp.stf_curr_unit_cd left join hr_organization ho on ho.org_gen_id=sp.str_curr_org_cd left join hr_staff_profile hsp1 on hsp1.stf_staff_no=b.pos_spv_name1 left join hr_staff_profile hsp2 on hsp2.stf_staff_no=b.pos_spv_name2 left join hr_organization_pern ss1 on ss1.op_perg_code=sp.stf_cur_sub_org WHERE B.pos_spv_name1='" + userid +"' AND B.pos_staff_no='" + Kaki_no.Text + "' AND B. pos_job_sts_cd='01'");
                
                if (select_kaki.Rows.Count != 0)
                    {
                        s_nama.Text = select_kaki.Rows[0]["stf_name"].ToString();
                        s_gred.Text = select_kaki.Rows[0]["op_perg_name"].ToString();
                        s_jab.Text = select_kaki.Rows[0]["hr_jaba_desc"].ToString();
                        s_jaw.Text = select_kaki.Rows[0]["pen1"].ToString();
                        s_kj.Text = select_kaki.Rows[0]["hr_kate_desc"].ToString();
                        s_unit.Text = select_kaki.Rows[0]["pen2"].ToString();
                        txt_org.Text = select_kaki.Rows[0]["org_name"].ToString();
                        txt_kat_jaw.Text = select_kaki.Rows[0]["stf_curr_job_cat_cd"].ToString();
                    if (Session["rend_no"].ToString() == "")
                    {
                        Random rand = new Random((int)DateTime.Now.Ticks);
                        int RandomNumber;
                        RandomNumber = rand.Next(100000, 999999);
                        Session["rend_no"] = RandomNumber.ToString();
                    }
                    bind_bahagian();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    } 
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan yang Maklumat',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
        grid_list();
    }


    protected void srch_kpp(object sender, EventArgs e)
    {
       

    }

    void bind_bahagian()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "SELECT cse_section_cd,cse_section_desc FROM hr_cmn_appr_section WHERE cse_sec_type='02' AND cse_sec_jawatan like '%"+ txt_kat_jaw.Text +"%'"
                + " except select sap_section_cd, s1.cse_section_desc from hr_staff_appraisal_temp left join hr_cmn_appr_section s1 on s1.cse_section_cd = sap_section_cd where sap_rand_no = '"+ Session["rend_no"].ToString() + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_bah1.DataSource = dt;
            dd_bah1.DataTextField = "cse_section_desc";
            dd_bah1.DataValueField = "cse_section_cd";
            dd_bah1.DataBind();
            dd_bah1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            if(dt.Rows.Count == 0)
            {
                Button1.Visible = false;
            }
            else
            {
                Button1.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void click_insert_tmp(object sender, EventArgs e)
    {
        try
        {
            if (Kaki_no.Text != "")
            {
                int i = 1;
                foreach (GridViewRow gvrow in grvStudentDetails.Rows)
                {
                    var val1 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Col1");
                    var val2 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Col2");
                  
                    DataTable select_stapp = new DataTable();
                    select_stapp = DBCon.Ora_Execute_table("select * from hr_staff_appraisal_temp where sap_rand_no='" + Session["rend_no"].ToString() + "' and sap_section_cd='"+ dd_bah1.SelectedValue +"' and sap_post_cat_cd = '" + i +"'");
                    if (select_stapp.Rows.Count == 0)
                    {
                        string Inssql = "insert into hr_staff_appraisal_temp(sap_rand_no,sap_staff_no,sap_section_cd,sap_post_cat_cd,sap_subject_cd,sap_weightage,cse_sec_type,sap_year,cse_sec_jawatan,cap_status,sap_crt_id,sap_crt_dt) values ('" + Session["rend_no"].ToString() + "','" + Kaki_no.Text + "','" + dd_bah1.SelectedValue + "','" + i + "','" + val1.Text + "','" + val2.Text + "','02','"+ txt_tahun.Text + "','" + txt_kat_jaw.Text + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                        i++;
                    }
                }
                if (Status == "SUCCESS")
                {
                    bind_bahagian();
                    FirstGridViewRow();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }
            else
            {                                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan yang Maklumat',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
        grid_list();
    }

    void grid_list()
    {
        grid_kpr();
        grid_jmk();
        grid_kpr1();
    }
    protected void click_insert(object sender, EventArgs e)
    {
        try
        {
            if (Kaki_no.Text != "")
            {
                string gt_cnt = string.Empty;
                    DataTable select_stapp = new DataTable();
                DataTable select_stapp2 = new DataTable();
                DataTable select_app = new DataTable();
                select_stapp = DBCon.Ora_Execute_table("select * from hr_staff_appraisal_temp where sap_rand_no='" + Session["rend_no"].ToString() + "'");
                if (select_stapp.Rows.Count != 0)
                {
                    for (int i = 0; i < select_stapp.Rows.Count; i++)
                    {

                        DataTable get_sub_cnt = new DataTable();
                        get_sub_cnt = DBCon.Ora_Execute_table("select top(1) (ISNULL(sap_post_cat_cd,'0') + 1) cnt from hr_staff_appraisal where sap_staff_no='" + select_stapp.Rows[i]["sap_staff_no"].ToString() + "' and sap_section_cd ='" + select_stapp.Rows[i]["sap_section_cd"].ToString() + "' and cse_sec_type='02'  order by sap_post_cat_cd desc");
                        if (get_sub_cnt.Rows.Count != 0)
                        {
                            gt_cnt = get_sub_cnt.Rows[0]["cnt"].ToString();
                        }
                        else
                        {
                            gt_cnt = "1";
                        }
                        select_app = DBCon.Ora_Execute_table("select * from hr_staff_appraisal where sap_staff_no='" + select_stapp.Rows[i]["sap_staff_no"].ToString() + "' and sap_section_cd ='" + select_stapp.Rows[i]["sap_section_cd"].ToString() + "' and sap_post_cat_cd='" + gt_cnt + "' and sap_subject_cd='" + select_stapp.Rows[i]["sap_subject_cd"].ToString().Trim() + "'");
                        if (select_app.Rows.Count == 0)
                        {
                            string Inssql = "insert into hr_staff_appraisal(sap_staff_no,sap_section_cd,sap_post_cat_cd,sap_subject_cd,sap_weightage,cse_sec_type,sap_year,cse_sec_jawatan,cap_status,sap_crt_id,sap_crt_dt) values ('" + select_stapp.Rows[i]["sap_staff_no"].ToString() + "','" + select_stapp.Rows[i]["sap_section_cd"].ToString() + "','" + gt_cnt + "','" + select_stapp.Rows[i]["sap_subject_cd"].ToString().Trim() + "','" + select_stapp.Rows[i]["sap_weightage"].ToString() + "','" + select_stapp.Rows[i]["cse_sec_type"].ToString() + "','" + select_stapp.Rows[i]["sap_year"].ToString() + "','" + select_stapp.Rows[i]["cse_sec_jawatan"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql);
                        }
                        else
                        {
                            string Inssql1 = "Update hr_staff_appraisal set sap_weightage='"+ select_stapp.Rows[i]["sap_weightage"].ToString() + "',sap_upd_id='" + userid + "',sap_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sap_staff_no='" + select_stapp.Rows[i]["sap_staff_no"].ToString() + "' and sap_section_cd ='" + select_stapp.Rows[i]["sap_section_cd"].ToString() + "' and sap_post_cat_cd='" + select_stapp.Rows[i]["sap_post_cat_cd"].ToString() + "'";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                        }
                    }
                }

                
                select_stapp2 = DBCon.Ora_Execute_table("SELECT *,case when B.cap_status = 'A' then 'AKTIF' else 'TIDAK AKTIF' end as sts FROM hr_cmn_appr_section A INNER JOIN hr_cmn_appraisal B ON A. cse_section_cd=B.cap_section_cd WHERE A.cse_sec_type='01' AND A.cse_sec_jawatan like '%" + txt_kat_jaw.Text + "%'");
                if (select_stapp2.Rows.Count != 0)
                {
                    for (int j = 0; j < select_stapp2.Rows.Count; j++)
                    {
                        
                            select_app = DBCon.Ora_Execute_table("select * from hr_staff_appraisal where sap_staff_no='" + Kaki_no.Text + "' and sap_section_cd ='" + select_stapp2.Rows[j]["cap_section_cd"].ToString() + "' and sap_post_cat_cd='" + select_stapp2.Rows[j]["cap_post_cat_cd"].ToString() + "'");
                            if (select_app.Rows.Count == 0)
                            {
                                string Inssql1 = "insert into hr_staff_appraisal(sap_staff_no,sap_section_cd,sap_post_cat_cd,sap_subject_cd,sap_weightage,cse_sec_type,sap_year,cse_sec_jawatan,cap_status,sap_crt_id,sap_crt_dt) values ('" + Kaki_no.Text + "','" + select_stapp2.Rows[j]["cap_section_cd"].ToString() + "','" + select_stapp2.Rows[j]["cap_post_cat_cd"].ToString() + "','" + select_stapp2.Rows[j]["cap_subject_cd"].ToString() + "','" + select_stapp2.Rows[j]["cap_weightage"].ToString() + "','01','" + txt_tahun.Text + "','" + txt_kat_jaw.Text + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                            }
                            else
                            {
                                string Inssql1 = "Update hr_staff_appraisal set sap_weightage='" + select_stapp2.Rows[j]["cap_weightage"].ToString() + "',sap_upd_id='" + userid + "',sap_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sap_staff_no='" + Kaki_no.Text + "' and sap_section_cd ='" + select_stapp2.Rows[j]["cap_section_cd"].ToString() + "' and sap_post_cat_cd='" + select_stapp2.Rows[j]["cap_post_cat_cd"].ToString() + "'";
                                Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                            }
                    }
                }
                  
                if (Status == "SUCCESS")
                {
                    delete_tmp = DBCon.Ora_Execute_table("delete from hr_staff_appraisal_temp where sap_rand_no='" + Session["rend_no"].ToString() + "'");
                    Session["rend_no"] = "";
                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Disimpan";                    
                    Response.Redirect("../SUMBER_MANUSIA/HR_KM_pen_prestasi1_view.aspx");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan yang Maklumat',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
        grid_list();

    }

    void grid_kpr()
    {
                   
            SqlCommand cmd2 = new SqlCommand("SELECT *,case when B.cap_status = 'A' then 'AKTIF' else 'TIDAK AKTIF' end as sts FROM hr_cmn_appr_section A INNER JOIN hr_cmn_appraisal B ON A. cse_section_cd=B.cap_section_cd WHERE A.cse_sec_type='01' AND A.cse_sec_jawatan like '%" + txt_kat_jaw.Text + "%' order by cse_section_desc", con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count == 0)
            {
                ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView3.DataSource = ds2;
            GridView3.DataBind();
                int columncount = GridView3.Rows[0].Cells.Count;
            GridView3.Rows[0].Cells.Clear();
            GridView3.Rows[0].Cells.Add(new TableCell());
            GridView3.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView3.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
              
            }
            else
            {
            GridView3.DataSource = ds2;
            GridView3.DataBind();
            GridViewHelper helper = new GridViewHelper(this.GridView3);
            helper.RegisterGroup("cse_section_desc", true, true);
            helper.GroupHeader += new GroupEvent(helper_GroupHeader);
            helper.RegisterSummary("cap_weightage", SummaryOperation.Sum, "cse_section_desc");
            helper.GroupSummary += new GroupEvent(helper_GroupSummary);
            helper.ApplyGroupSort();

        }

    }

    void grid_kpr1()
    {

        SqlCommand cmd2 = new SqlCommand("select cse_section_desc as bah_dec,sap_post_cat_cd sub_cd,sap_subject_cd as sub_desc,sap_weightage weight from hr_staff_appraisal A INNER JOIN hr_cmn_appr_section B ON A. sap_section_cd=B.cse_section_cd where A.sap_staff_no='" + Kaki_no.Text + "' and A.sap_year= '"+ txt_tahun.Text +"' and A.cse_sec_jawatan='"+ txt_kat_jaw.Text + "' and A.cse_sec_type='02' order by cse_section_desc", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView2.DataSource = ds2;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            GridView2.Visible = false;
            grd2_shw.Visible = false;
            Button4.Text = "Simpan";
        }
        else
        {
            GridView2.DataSource = ds2;
            GridView2.DataBind();
            GridViewHelper helper = new GridViewHelper(this.GridView2);
            helper.RegisterGroup("bah_dec", true, true);
            helper.GroupHeader += new GroupEvent(helper_GroupHeader1);
            helper.RegisterSummary("weight", SummaryOperation.Sum, "bah_dec");
            helper.GroupSummary += new GroupEvent(helper_GroupSummary1);
            helper.ApplyGroupSort();
            GridView2.Visible = true;
            grd2_shw.Visible = true;
            Button4.Text = "Kemaskini";
        }

    }

    private void helper_GroupSummary(string groupName, object[] values, GridViewRow row)
    {
            row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[0].Text = "Jumlah Markah";
        row.Cells[0].Font.Bold.ToString();
    }

    private void helper_GroupSummary1(string groupName, object[] values, GridViewRow row)
    {
        row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        row.Cells[0].Text = "Jumlah Markah";
        row.Cells[0].Font.Bold.ToString();
    }
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
       
        if (groupName == "cse_section_desc")
        {
            row.BackColor = Color.LightGray;
            row.ForeColor = Color.DarkRed;
            row.Font.Bold = true;
            row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
        }
       
    }

    private void helper_GroupHeader1(string groupName, object[] values, GridViewRow row)
    {

        if (groupName == "bah_dec")
        {
            row.BackColor = Color.LightGray;
            row.ForeColor = Color.DarkRed;
            row.Font.Bold = true;
            row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
        }

    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        GridView3.DataBind();
    }

    protected void GridView1_Sorting1(object sender, GridViewSortEventArgs e)
    {
        GridView2.DataBind();
    }
    protected void gvSelected_PageIndexChanging_out(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        grid_kpr();

    }

    protected void gvSelected_PageIndexChanging_out1(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        grid_kpr1();

    }

    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

    protected void gvEmp_RowDataBound1(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvEmp_RowDataBound_jmk(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "wt") != DBNull.Value)
            {
                Decimal ss = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "wt"));
                total_jmk1 += ss;
            }
            if (DataBinder.Eval(e.Row.DataItem, "ppp") != DBNull.Value)
            {
                Decimal ss1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ppp"));
                total_jmk2 += ss1;
            }
            if (DataBinder.Eval(e.Row.DataItem, "ppk") != DBNull.Value)
            {
                Decimal ss2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ppk"));
                total_jmk3 += ss2;
            }
            if (DataBinder.Eval(e.Row.DataItem, "pro") != DBNull.Value)
            {
                Decimal ss3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pro"));
                total_jmk4 += ss3;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            System.Web.UI.WebControls.Label lblamount_wt = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal_wt");
            lblamount_wt.Text = total_jmk1.ToString();

            System.Web.UI.WebControls.Label lblamount_ppp = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal_ppp");
            lblamount_ppp.Text = total_jmk2.ToString();

            System.Web.UI.WebControls.Label lblamount_ppk = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal_ppk");
            lblamount_ppk.Text = total_jmk3.ToString();

            System.Web.UI.WebControls.Label lblamount_pro = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal_pro");
            lblamount_pro.Text = total_jmk4.ToString();

        }

    }

    void grid_jmk()
    {

        SqlCommand cmd2 = new SqlCommand("select sap_section_cd,s1.cse_section_desc,sap_subject_cd,sap_weightage,sap_post_cat_cd from hr_staff_appraisal_temp left join hr_cmn_appr_section s1 on s1.cse_section_cd=sap_section_cd where sap_rand_no='" + Session["rend_no"].ToString() + "' ", con);
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
            GridView1.Visible = false;
            grd1_shw.Visible = false;
        }
        else
        {
            GridView1.Visible = true;
            grd1_shw.Visible = true;
            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }
    }
  


    protected void Click_bck(object sender, EventArgs e)
    {
        delete_tmp = DBCon.Ora_Execute_table("delete from hr_staff_appraisal_temp where sap_rand_no='" + Session["rend_no"].ToString() + "'");
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["rend_no"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_KM_pen_prestasi1_view.aspx");
    }
  
}