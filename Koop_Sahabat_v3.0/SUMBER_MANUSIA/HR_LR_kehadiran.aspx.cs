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
using System.Threading;

public partial class HR_LR_kehadiran : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid, stscd, abc1;
    string phdate1 = string.Empty, phdate2 = string.Empty, phdate3 = string.Empty, phdate4 = string.Empty;
    string etdate1 = string.Empty, etdate2 = string.Empty, etdate3 = string.Empty, etdate4 = string.Empty;
    string stno = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        // ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.Button2);
        //scriptManager.RegisterPostBackControl(this.Button5);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();

                s_nama.Attributes.Add("Readonly", "Readonly");
                s_jaw.Attributes.Add("Readonly", "Readonly");
                s_jab.Attributes.Add("Readonly", "Readonly");
                s_gred.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                //tm_date.Text = "01/02/2015";
                //ta_date.Text = "28/02/2015";
                //tm_date.Attributes.Add("Readonly", "Readonly");
                //ta_date.Attributes.Add("Readonly", "Readonly");
                txt_org.Attributes.Add("Readonly", "Readonly");

                jm_hl.Attributes.Add("Readonly", "Readonly");
                jm_hc.Attributes.Add("Readonly", "Readonly");
                //grid();

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
        gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('556','448','505','64','1489','484','14', '15')");

        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;

        h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
        bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
        bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());

        h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());

        lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
        lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
        lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
       
        Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
        Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
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
            DBConnection dbcon1 = new DBConnection();
            DataTable qry1 = new DataTable();
            qry1 = dbcon1.Ora_Execute_table("select stf_staff_no from hr_staff_profile where stf_staff_no LIKE '%" + prefixText + "%'");
            if (qry1.Rows.Count != 0)
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "select stf_staff_no from hr_staff_profile where stf_staff_no LIKE '%' + @Search + '%'";
                    com.Parameters.AddWithValue("@Search", prefixText);
                    com.Connection = con;
                    con.Open();
                    List<string> countryNames = new List<string>();
                    using (SqlDataReader sdr = com.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["stf_staff_no"].ToString());

                        }
                    }

                    con.Close();
                    return countryNames;
                }
            }
            else
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "select stf_staff_no,stf_name from hr_staff_profile where stf_name LIKE '%' + @Search + '%'";
                    com.Parameters.AddWithValue("@Search", prefixText);
                    com.Connection = con;
                    con.Open();
                    List<string> countryNames = new List<string>();
                    using (SqlDataReader sdr = com.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["stf_name"].ToString());

                        }
                    }

                    con.Close();
                    return countryNames;
                }
            }
        }
    }

    protected void click_srch(object sender, EventArgs e)
    {
        try
        {
            if (Kaki_no.Text != "" && tm_date.Text != "" && ta_date.Text != "")
            {
                if (tm_date.Text != "" && ta_date.Text != "")
                {
                    string d1 = tm_date.Text;
                    DateTime today1 = DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    phdate1 = today1.ToString("yyyy-MM-dd");

                    string d2 = ta_date.Text;
                    DateTime today2 = DateTime.ParseExact(d2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    phdate2 = today2.ToString("yyyy-MM-dd");
                }
                else
                {
                    phdate1 = "";
                    phdate2 = "";
                }

                if (Kaki_no.Text != "")
                {
                    DataTable dd1 = new DataTable();
                    dd1 = DBCon.Ora_Execute_table("select stf_staff_no from hr_staff_profile where '" + Kaki_no.Text + "' IN (stf_staff_no,stf_name)");
                    Applcn_no1.Text = dd1.Rows[0]["stf_staff_no"].ToString();
                }

                DataTable select_kaki = new DataTable();
                select_kaki = DBCon.Ora_Execute_table("select sp.stf_staff_no,sp.stf_name,hg.hr_gred_desc,hj.hr_jaba_desc,rhj.hr_jaw_desc,pk.hr_kate_desc,hu.hr_unit_desc,ho.org_name,o1.op_perg_name from hr_staff_profile as sp left join Ref_hr_gred as hg on hg.hr_gred_Code=sp.stf_curr_grade_cd left join Ref_hr_jabatan as hj on hj.hr_jaba_Code=sp.stf_curr_dept_cd left join Ref_hr_Jawatan as rhj on rhj.hr_jaw_Code=sp.stf_curr_post_cd left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code=sp.stf_curr_job_cat_cd left join Ref_hr_unit as hu on hu.hr_unit_Code=sp.stf_curr_unit_cd left join hr_post_his as ph on ph.pos_staff_no=sp.stf_staff_no and ph.pos_end_dt='9999-12-31' left join hr_organization ho on ho.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern o1 on o1.op_perg_code=sp.stf_cur_sub_org where sp.stf_staff_no='" + Applcn_no1.Text + "'");
                if (select_kaki.Rows.Count != 0)
                {
                    shw_txtgrid1.Visible = true;
                    s_nama.Text = select_kaki.Rows[0]["stf_name"].ToString();
                    s_gred.Text = select_kaki.Rows[0]["hr_gred_desc"].ToString();
                    s_jab.Text = select_kaki.Rows[0]["hr_jaba_desc"].ToString();
                    s_jaw.Text = select_kaki.Rows[0]["hr_jaw_desc"].ToString();
                    stno = select_kaki.Rows[0]["stf_staff_no"].ToString();
                    txt_org.Text = select_kaki.Rows[0]["org_name"].ToString();
                    TextBox2.Text = select_kaki.Rows[0]["op_perg_name"].ToString();
                    //grid();



                    clk_print();
                }
                else
                {
                    shw_txtgrid1.Visible = false;
                    s_nama.Text = "";
                    s_gred.Text = "";
                    s_jab.Text = "";
                    s_jaw.Text = "";
                    //grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                //grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }

    }

    protected void Btn_Senarai_Click(object sender, EventArgs e)
    {
        try
        {
            shw_txtgrid1.Visible = true;
            if (Kaki_no.Text != "" && tm_date.Text != "" && ta_date.Text != "")
            {
                if (tm_date.Text != "" && ta_date.Text != "")
                {
                    string d1 = tm_date.Text;
                    DateTime today1 = DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    phdate1 = today1.ToString("yyyy-MM-dd");

                    string d2 = ta_date.Text;
                    DateTime today2 = DateTime.ParseExact(d2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    phdate2 = today2.ToString("yyyy-MM-dd");
                }
                else
                {
                    phdate1 = "";
                    phdate2 = "";
                }
                DataTable sel_rows_at = new DataTable();
                sel_rows_at = DBCon.Ora_Execute_table("select FORMAT(ha.atd_date,'dd/MM/yyyy', 'en-us') atd_date,ha.atd_clock_in,ha.atd_clock_out,case(sp.stf_working_hour ) when '01' then'08:30:00.0000000' when '02' then'08:00:00.0000000' when '03' then'08:45:00.0000000' when '04' then'12:45:00.0000000' when '05' then'09:00:00.0000000'  end stf_working_hour from hr_post_his as ph left join hr_staff_profile as sp on sp.stf_staff_no=ph.pos_staff_no left join hr_attendance as ha on ha.atd_staff_no=ph.pos_staff_no  where pos_end_dt='9999-12-31' and pos_staff_no='" + Applcn_no1.Text + "' and ha.atd_date>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and ha.atd_date<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), 0)");

                DataTable sel_rows_at_cnt = new DataTable();
                sel_rows_at_cnt = DBCon.Ora_Execute_table("select count(*) as cnt from hr_post_his as ph left join hr_staff_profile as sp on sp.stf_staff_no=ph.pos_staff_no left join hr_attendance as ha on ha.atd_staff_no=ph.pos_staff_no  where pos_end_dt='9999-12-31' and pos_staff_no='" + Applcn_no1.Text + "' and ha.atd_date>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and ha.atd_date<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), 0)");
                string r_cnt = (double.Parse(sel_rows_at_cnt.Rows[0]["cnt"].ToString()) - 1).ToString();
                if (sel_rows_at_cnt.Rows[0]["cnt"].ToString() != "0")
                {
                    for (int k = 0; k <= double.Parse(r_cnt); k++)
                    {
                        string d1_dt = sel_rows_at.Rows[k]["atd_date"].ToString();
                        DateTime today1_dt = DateTime.ParseExact(d1_dt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string atddt = today1_dt.ToString("yyyy-MM-dd");
                        string late_ind = string.Empty, hol = string.Empty, ind_desc = string.Empty;



                        //if (Convert.ToDateTime(sel_rows_at.Rows[k]["atd_clock_in"].ToString()) > Convert.ToDateTime(sel_rows_at.Rows[k]["stf_working_hour"].ToString()))
                        string ss1 = sel_rows_at.Rows[k]["atd_clock_in"].ToString().Replace(":", "");
                        string ss2 = "083000";
                        if (double.Parse(ss1) > double.Parse(ss2))
                        {
                            late_ind = "L";
                            ind_desc = "LEWAT";
                        }
                        else if (sel_rows_at.Rows[k]["atd_clock_in"].ToString() == "00:00:00" && sel_rows_at.Rows[k]["atd_clock_out"].ToString() == "00:00:00")
                        {
                            late_ind = "H";
                            ind_desc = "CUTI";
                        }

                        DBCon.Execute_CommamdText("UPDATE hr_attendance set atd_remark='" + ind_desc + "',atd_hol_late_ind='" + late_ind + "',atd_upd_id='" + Session["New"].ToString() + "',atd_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' WHERE atd_staff_no = '" + Applcn_no1.Text + "' AND atd_date = '" + atddt + "'");
                    }
                    clk_print();
                    service.audit_trail("P0047", "Simpan","NO KAKITANGN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                   
                }

            }
            else
            {
               // grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }
    }

    //void grid()
    //{      
    //    //SqlCommand cmd2 = new SqlCommand("select ht.trn_dur,upper(sp.stf_name) as stf_name from hr_post_his as ph left join hr_training as ht on ht.trn_staff_no=ph.pos_staff_no  left join hr_staff_profile as sp on sp.stf_staff_no=ht.trn_staff_no where ht.trn_start_dt>=DATEADD(day, DATEDIFF(day, 0, '2016-01-12'), 0) and ht.trn_start_dt<=DATEADD(day, DATEDIFF(day, 0, '2016-01-16'), 0) and  ph.pos_end_dt='9999-12-31' and ht.trn_cat_cd='' and ht.trn_type_cd='' and ht.trn_staff_no='0202020202' and ph.pos_org_id='s637456346' and ph.pos_dept_cd='01' and ph.pos_unit_cd='18'", con);
    //    SqlCommand cmd2 = new SqlCommand("select FORMAT(ha.atd_date,'dd/MM/yyyy', 'en-us') atd_date,ha.atd_clock_in,ha.atd_clock_out,sp.stf_working_hour from hr_post_his as ph left join hr_staff_profile as sp on sp.stf_staff_no=ph.pos_staff_no left join hr_attendance as ha on ha.atd_staff_no=ph.pos_staff_no  where pos_end_dt='9999-12-31' and pos_staff_no='" + Kaki_no.Text + "' and ha.atd_date>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and ha.atd_date<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), 0)", con);
    //    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
    //    DataSet ds2 = new DataSet();
    //    da2.Fill(ds2);
    //    if (ds2.Tables[0].Rows.Count == 0)
    //    {
    //        ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
    //        GridView1.DataSource = ds2;
    //        GridView1.DataBind();
    //        int columncount = GridView1.Rows[0].Cells.Count;
    //        GridView1.Rows[0].Cells.Clear();
    //        GridView1.Rows[0].Cells.Add(new TableCell());
    //        GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
    //        GridView1.Rows[0].Cells[0].Text = "<strong>Maklumat Carian Tidak Dijumpai</strong>";
    //    }
    //    else
    //    {
    //        GridView1.DataSource = ds2;
    //        GridView1.DataBind();
    //    }
    //}

    //protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView1.PageIndex = e.NewPageIndex;
    //    grid();
    //}

    void clk_print()
    {
        {
            try
            {
                if (tm_date.Text != "" && ta_date.Text != "" && Kaki_no.Text != "")
                {
                    DataTable select_kaki_jum = new DataTable();
                    select_kaki_jum = DBCon.Ora_Execute_table("select a.stf_staff_no,a.Hol,ISNULL(b.Late,'') as Late from (select sp.stf_staff_no,count(ha.atd_hol_late_ind) as Hol from hr_post_his as ph left join hr_staff_profile as sp on sp.stf_staff_no=ph.pos_staff_no left join hr_attendance as ha on ha.atd_staff_no=ph.pos_staff_no  where pos_end_dt='9999-12-31' and pos_staff_no='" + Applcn_no1.Text + "' and ha.atd_date>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and ha.atd_date<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), 0) and ha.atd_hol_late_ind = 'H' group by sp.stf_staff_no) as a full outer join (select sp.stf_staff_no,count(ha.atd_hol_late_ind) as Late from hr_post_his as ph left join hr_staff_profile as sp on sp.stf_staff_no=ph.pos_staff_no left join hr_attendance as ha on ha.atd_staff_no=ph.pos_staff_no  where pos_end_dt='9999-12-31' and pos_staff_no='" + Applcn_no1.Text + "' and ha.atd_date>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and ha.atd_date<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), 0) and ha.atd_hol_late_ind = 'L' group by sp.stf_staff_no) as b on b.stf_staff_no = a.stf_staff_no");

                    if (select_kaki_jum.Rows.Count != 0)
                    {
                        jm_hl.Text = select_kaki_jum.Rows[0]["Late"].ToString();
                        jm_hc.Text = select_kaki_jum.Rows[0]["Hol"].ToString();

                        DataTable sel_poshis = new DataTable();
                        sel_poshis = DBCon.Ora_Execute_table("select pos_spv_name1,sp.stf_name,rj.hr_jaw_desc from hr_post_his as ph left join hr_staff_profile sp on sp.stf_staff_no=ph.pos_spv_name1 left join Ref_hr_Jawatan rj on rj.hr_jaw_Code=sp.stf_curr_post_cd where pos_end_dt='9999-12-31' and pos_staff_no='" + Applcn_no1.Text + "'");

                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        dt = DBCon.Ora_Execute_table("select FORMAT(ha.atd_date,'dd/MM/yyyy', 'en-us') atd_date,ha.atd_clock_in,ha.atd_clock_out,sp.stf_working_hour,ha.atd_remark as stf_desc,stf_staff_no,UPPER(stf_name) as stf_name,UPPER(hr_gred_desc) as hr_gred_desc,UPPER(hr_jaba_desc) as hr_jaba_desc,UPPER(hr_jaw_desc) as hr_jaw_desc from hr_post_his as ph left join hr_staff_profile as sp on sp.stf_staff_no=ph.pos_staff_no left join Ref_hr_jabatan as JB on JB.hr_jaba_Code=SP.stf_curr_dept_cd  left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=SP.stf_curr_post_cd left join Ref_hr_gred as GR on GR.hr_gred_Code=SP.stf_curr_grade_cd   left join hr_attendance as ha on ha.atd_staff_no=ph.pos_staff_no  where pos_end_dt='9999-12-31' and pos_staff_no='" + Applcn_no1.Text + "' and ha.atd_date>=DATEADD(day, DATEDIFF(day, 0, '" + phdate1 + "'), 0) and ha.atd_date<=DATEADD(day, DATEDIFF(day, 0, '" + phdate2 + "'), 0)");
                        RptviwerStudent.Reset();
                        ds.Tables.Add(dt);

                        List<DataRow> listResult = dt.AsEnumerable().ToList();
                        listResult.Count();
                        int countRow = 0;
                        countRow = listResult.Count();

                        RptviwerStudent.LocalReport.DataSources.Clear();
                        if (countRow != 0)
                        {
                            RptviwerStudent.LocalReport.ReportPath = "SUMBER_MANUSIA/hr_lrk.rdlc";
                            ReportDataSource rds = new ReportDataSource("hr_lrk", dt);

                            ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("sup_name", sel_poshis.Rows[0]["stf_name"].ToString()),
                     new ReportParameter("jaw_name", sel_poshis.Rows[0]["hr_jaw_desc"].ToString()),
                     new ReportParameter("oname", txt_org.Text)
                          };

                            RptviwerStudent.LocalReport.SetParameters(rptParams);

                            RptviwerStudent.LocalReport.DataSources.Add(rds);
                            RptviwerStudent.LocalReport.DisplayName = "Laporan_Kehadiran_" + Applcn_no1.Text + "-" + DateTime.Now.ToString("ddMMyyyy");
                            //Refresh
                            RptviwerStudent.LocalReport.Refresh();

                        }
                        else if (countRow == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                       // grid();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                  //  grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    protected void click_rst(object sender, EventArgs e)
    {
        Response.Redirect("HR_LR_kehadiran.aspx");
    }
   
}