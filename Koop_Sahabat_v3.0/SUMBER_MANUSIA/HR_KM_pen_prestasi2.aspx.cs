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

public partial class HR_KM_pen_prestasi2 : System.Web.UI.Page
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
    string currentClass = "alternateDataRow";
    string currentGroup = string.Empty;

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
            
           
            
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
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

     

    void grid_list()
    {
        grid_kpr2();
        grid_kpr3();
        grid_kpr();           
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        for (int i = GridView3.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = GridView3.Rows[i];
            GridViewRow previousRow = GridView3.Rows[i - 1];
            for (int j = 0; j < 1; j++)
            {
                //run this loop for the column which you thing the data will be similar
                if (((System.Web.UI.WebControls.Label)row.Cells[j].FindControl("lbl_bha_desc")).Text == ((System.Web.UI.WebControls.Label)previousRow.Cells[j].FindControl("lbl_bha_desc")).Text)
                {
                    if (previousRow.Cells[j].RowSpan == 0)
                    {
                        if (row.Cells[j].RowSpan == 0)
                        {
                            previousRow.Cells[j].RowSpan += 2;
                            
                        }
                        else
                        {
                            previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            
                        }
                        row.Cells[j].Visible = false;
                    }
                }
                
            }           

        }
    }
    protected void click_insert(object sender, EventArgs e)
    {
        try
        {
            if (Kaki_no.Text != "")
            {
                
                foreach (GridViewRow dataItem in GridView3.Rows)
                {
                        string markah1 = ((System.Web.UI.WebControls.TextBox)dataItem.FindControl("lbl_markah")).Text.Trim();
                        string bah_cd1 = ((System.Web.UI.WebControls.TextBox)dataItem.FindControl("Label2")).Text.Trim();
                        string sub_cd1 = ((System.Web.UI.WebControls.TextBox)dataItem.FindControl("Label5")).Text.Trim();
                        string sec_type1 = ((System.Web.UI.WebControls.TextBox)dataItem.FindControl("Label4")).Text.Trim();
                  

                    if (markah1 != "0")
                    {
                        DataTable chk_apprisal = new DataTable();
                        chk_apprisal = DBCon.Ora_Execute_table("select * from hr_staff_appraisal where sap_staff_no='" + Kaki_no.Text + "' and sap_section_cd ='" + bah_cd1 + "' and sap_post_cat_cd='" + sub_cd1 + "' and cse_sec_type='" + sec_type1 + "' and sap_year='" + txt_tahun.Text + "'");
                        if (chk_apprisal.Rows.Count != 0)
                        {
                            string Inssql1 = "Update hr_staff_appraisal set sap_ppp_score='" + markah1 + "',sap_upd_ppp_id='" + userid + "',sap_upd_ppp_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sap_staff_no='" + Kaki_no.Text + "' and sap_section_cd ='" + bah_cd1 + "' and sap_post_cat_cd='" + sub_cd1 + "' and cse_sec_type='" + sec_type1 + "' and sap_year='" + txt_tahun.Text + "'";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                        }
                    }
                }
            
                if (Status == "SUCCESS")
                {
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
                   
            SqlCommand cmd2 = new SqlCommand("select * from (SELECT sap_section_cd,sap_post_cat_cd,sap_subject_cd,sap_staff_remark,sap_weightage,b.cse_section_desc,A.cse_sec_type,case when ISNULL(sap_ppp_score,'0')='0' then '0' else sap_ppp_score end as sap_ppp_score FROM hr_staff_appraisal A INNER JOIN hr_cmn_appr_section B ON A. sap_section_cd=B.cse_section_cd WHERE sap_staff_no='" + Kaki_no.Text + "' AND sap_year='" + txt_tahun.Text + "' AND cap_status='A' ) as a "
                                            + " outer apply(SELECT sap_section_cd, sum(sap_weightage) cnt FROM hr_staff_appraisal A INNER JOIN hr_cmn_appr_section B ON A.sap_section_cd = B.cse_section_cd WHERE sap_staff_no = '" + Kaki_no.Text + "' AND sap_year = '" + txt_tahun.Text +"' AND cap_status = 'A' group by sap_section_cd) as b where b.sap_section_cd = a.sap_section_cd ORDER BY a.sap_section_cd, a.sap_post_cat_cd", con);
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
          
        }

    }


    void grid_kpr2()
    {

        SqlCommand cmd2 = new SqlCommand("SELECT FORMAT(trn_start_dt,'dd/MM/yyyy') dari,trn_venue,trn_training_name,trn_venue trn_dur,trn_organiser,trn_fee_amt,trn_catatan FROM hr_training WHERE year(trn_start_dt) ='" + txt_tahun.Text +"' AND  trn_staff_no='"+ Kaki_no.Text +"'", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView4.DataSource = ds2;
            GridView4.DataBind();
            int columncount = GridView4.Rows[0].Cells.Count;
            GridView4.Rows[0].Cells.Clear();
            GridView4.Rows[0].Cells.Add(new TableCell());
            GridView4.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView4.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            GridView4.DataSource = ds2;
            GridView4.DataBind();
           
        }

    }

    void grid_kpr3()
    {

        SqlCommand cmd2 = new SqlCommand("select dis_staff_no,s2.hr_discipline_desc,FORMAT(dis_eff_dt,'dd/MM/yyyy') eff_dt,case when FORMAT(dis_exp_dt,'dd/MM/yyyy') ='01/01/1900' then '' else FORMAT(dis_exp_dt,'dd/MM/yyyy') end as  exp_dt,s1.dis_catatan from hr_discipline s1 left join Ref_hr_discipline s2 on s2.hr_discipline_Code=dis_discipline_type_cd where dis_staff_no='"+ Kaki_no.Text +"' and year(dis_eff_dt) = '"+ txt_tahun.Text +"'", con);
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

    //private void helper_GroupSummary(string groupName, object[] values, GridViewRow row)
    //{
    //        row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
    //        row.Cells[0].Text = "Jumlah Markah";  
    //}
    //private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    //{

    //    if (groupName == "cse_section_desc")
    //    {
    //        row.BackColor = Color.LightGray;
    //        row.ForeColor = Color.DarkRed;
    //        row.Font.Bold = true;
    //        row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
    //    }

    //}

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        GridView3.DataBind();
    }
    protected void gvSelected_PageIndexChanging_out(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        grid_kpr();

    }

    protected void gvSelected_PageIndexChanging_out1(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid_kpr3();

    }


    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.Label lblctrl = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_bha_desc");
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("SELECT ROW_NUMBER() OVER(ORDER BY sap_section_cd,cse_section_desc) Id,sap_section_cd,cse_section_desc FROM hr_staff_appraisal A INNER JOIN hr_cmn_appr_section B ON A. sap_section_cd=B.cse_section_cd WHERE sap_staff_no='" + Kaki_no.Text + "' AND sap_year='" + txt_tahun.Text + "' AND cap_status='A' group by sap_section_cd,cse_section_desc ORDER BY sap_section_cd,cse_section_desc");
            for (int i = 0; i < ddokdicno.Rows.Count; i++)
            {
                if (ddokdicno.Rows[i]["cse_section_desc"].ToString() == lblctrl.Text)
                {
                    int number;
                    number = int.Parse(ddokdicno.Rows[i]["Id"].ToString());
                    if (number % 2 == 0)
                    {
                        e.Row.Attributes.Add("style", "background-color:#EFF3FB;");
                    }
                    else
                    {
                        e.Row.Attributes.Add("style", "background-color:#EEEEEE;");
                    }
                }
            }
        }
    }

    protected void gvEmp_RowDataBound1(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvEmp_RowDataBound11(object sender, GridViewRowEventArgs e)
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

    //protected void txt_changed_markah(object sender, EventArgs e)
    //{
    //    GridViewRow row = ((GridViewRow)((System.Web.UI.WebControls.TextBox)sender).NamingContainer);
    //    System.Web.UI.WebControls.TextBox markah = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_markah");
    //    System.Web.UI.WebControls.TextBox bah_cd = (System.Web.UI.WebControls.TextBox)row.FindControl("Label2");
    //    System.Web.UI.WebControls.TextBox sub_cd = (System.Web.UI.WebControls.TextBox)row.FindControl("Label5");
    //    System.Web.UI.WebControls.TextBox sec_type = (System.Web.UI.WebControls.TextBox)row.FindControl("Label4");
       
    //    DataTable chk_apprisal = new DataTable();
    //    chk_apprisal = DBCon.Ora_Execute_table("select * from hr_staff_appraisal where sap_staff_no='" + Kaki_no.Text + "' and sap_section_cd ='" + bah_cd.Text + "' and sap_post_cat_cd='" + sub_cd.Text + "' and cse_sec_type='"+ sec_type.Text + "' and sap_year='"+ txt_tahun.Text +"'");
    //    if (chk_apprisal.Rows.Count != 0)
    //    {
    //        string Inssql1 = "Update hr_staff_appraisal set sap_ppp_score='" + markah.Text + "' where sap_staff_no='" + Kaki_no.Text + "' and sap_section_cd ='" + bah_cd.Text + "' and sap_post_cat_cd='" + sub_cd.Text + "' and cse_sec_type='" + sec_type.Text + "' and sap_year='" + txt_tahun.Text + "'";
    //        Status = DBCon.Ora_Execute_CommamdText(Inssql1);
    //    }
    //    grid_list();
    //}
    protected void Click_bck(object sender, EventArgs e)
    {        
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";        
        Response.Redirect("../SUMBER_MANUSIA/HR_KM_pen_prestasi1_view.aspx");
    }
  
}