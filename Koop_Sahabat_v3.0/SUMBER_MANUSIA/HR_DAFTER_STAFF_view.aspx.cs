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
using System.Text.RegularExpressions;
using System.Threading;

public partial class HR_DAFTER_STAFF_view : System.Web.UI.Page
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
    string role_1 = string.Empty, role_2 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        
        //string script = " $(function () {$('.select2').select2()});";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
       
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success'});", true);

                }
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";
                userid = Session["New"].ToString();
                BindData_Grid();
                organsi();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void organsi()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select org_gen_id,UPPER(org_name) as org_name from  hr_organization order by org_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddorganisasi.DataSource = dt;
            ddorganisasi.DataTextField = "org_name";
            ddorganisasi.DataValueField = "org_gen_id";
            ddorganisasi.DataBind();
            ddorganisasi.Items.Insert(0, new ListItem("--- PILIH ---", ""));
           

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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1507','448','133','39')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;

            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());

            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            btn_tmbah.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '"+ Session["userid"].ToString() +"'");

        if(ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0078' and Role_id IN ('"+ ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",","','") + "') order by Role_id");

            if(ddokdicno_1.Rows.Count != 0)
            {
                for (int i = 0; i < ddokdicno_1.Rows.Count; i++)
                {
                    role_1 = ddokdicno_1.Rows[i]["ctrl_type"].ToString();
                    if (ddokdicno_1.Rows[i]["Add_chk"].ToString() == "1")
                    {
                        Button1.Visible = true;
                        btn_tmbah.Visible = true;
                        Button3.Visible = true;
                    }
                    else
                    {
                        Button1.Visible = false;
                        btn_tmbah.Visible = false;
                        Button3.Visible = false;
                    }
                }
            }
        }
    }

    //protected void gvSelected_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    //{
    //    GridView1.PageIndex = e.NewPageIndex;
    //    GridView1.DataBind();
    //    BindData_jenis();
    //}
    protected void BindData_Grid()
    {
        assgn_roles();
        string sqry = string.Empty;
        if (role_1 == "1")
        {
            sqry = "";
        }
        else
        {
            sqry = "where stf_staff_no = '" + Session["userid"].ToString() + "'";
        }
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select *,case when ISNULL(pos_end_dt,'') = '' then 'User is InActive' else 'User is Active' end as stf_ss,case when ISNULL(pos_end_dt,'') = '' then 'Meletak jawatan' else job_sts end as job_sts1 from (select stf_staff_no,stf_icno,stf_name,stf_age from hr_staff_profile) as a left join( select FORMAT(pos_start_dt,'dd/MM/yyyy', 'en-us') as pos_start_dt,FORMAT(pos_end_dt,'dd/MM/yyyy', 'en-us') as pos_end_dt,pos_post_cd,hj.hr_jaw_desc,pos_grade_cd,UPPER(ISNULL(hg.hr_gred_desc,'')) as hr_gred_desc,pos_unit_cd,UPPER(ISNULL(hu.hr_unit_desc,'')) as hr_unit_desc,pos_dept_cd,UPPER(ISNULL(jab.hr_jaba_desc,'')) as hr_jaba_desc,pos_staff_no,pos_subjek,ISNULL(pt.hr_traf_desc,'') as job_sts from hr_post_his ph left join ref_hr_jawatan hj on hj.hr_jaw_Code=ph.pos_post_cd left join ref_hr_gred hg on hg.hr_gred_Code=ph.pos_grade_cd left join ref_hr_unit hu on hu.hr_unit_Code=ph.pos_unit_cd left join Ref_hr_jabatan jab on jab.hr_jaba_Code=ph.pos_dept_cd left join Ref_hr_penj_traf pt on pt.hr_traf_Code=pos_job_sts_cd where pos_end_dt ='9999-12-31') as b on b.pos_staff_no = a.stf_staff_no " + sqry + " order by b.pos_end_dt desc", con);
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
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        con.Close();
    }

    protected void BindData_Grid1()
    {
        assgn_roles();
        string sqry = string.Empty;
        if (role_1 == "1")
        {
            sqry = "";
        }
        else
        {
            sqry = "where stf_staff_no = '" + Session["userid"].ToString() + "'";
        }
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select * from (select stf_staff_no,stf_icno,stf_name,str_curr_org_cd from hr_staff_profile where str_curr_org_cd='" + ddorganisasi.SelectedItem.Value + "' ) as a left join( select FORMAT(pos_start_dt,'dd/MM/yyyy', 'en-us') as pos_start_dt,FORMAT(pos_end_dt,'dd/MM/yyyy', 'en-us') as pos_end_dt,pos_post_cd,hj.hr_jaw_desc,pos_grade_cd,UPPER(ISNULL(hg.hr_gred_desc,'')) as hr_gred_desc,pos_unit_cd,UPPER(ISNULL(hu.hr_unit_desc,'')) as hr_unit_desc,pos_dept_cd,UPPER(ISNULL(jab.hr_jaba_desc,'')) as hr_jaba_desc,pos_staff_no,pos_subjek from hr_post_his ph left join ref_hr_jawatan hj on hj.hr_jaw_Code=ph.pos_post_cd left join ref_hr_gred hg on hg.hr_gred_Code=ph.pos_grade_cd left join ref_hr_unit hu on hu.hr_unit_Code=ph.pos_unit_cd left join Ref_hr_jabatan jab on jab.hr_jaba_Code=ph.pos_dept_cd left join Ref_hr_penj_traf pt on pt.hr_traf_Code=pos_job_sts_cd where pos_end_dt ='9999-12-31') as b on b.pos_staff_no = a.stf_staff_no " + sqry + "   ", con);
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
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        con.Close();
    }


    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../SUMBER_MANUSIA/HR_DAFTER_STAFF.aspx");
    }

    protected void btn_hups_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.CheckBox;
            if (rb.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                var rbn = row.FindControl("RadioButton1") as System.Web.UI.WebControls.CheckBox;
                if (rbn.Checked)
                {
                    int RowIndex = row.RowIndex;
                    string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("lb_sno")).Text.ToString(); //this store the  value in varName1

                    string Inssql = "Delete from hr_post_his where pos_staff_no='" + varName1 + "'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        SqlCommand ins_peng = new SqlCommand("Delete from hr_staff_profile where stf_staff_no='" + varName1 + "'", conn);
                        conn.Open();
                        int i = ins_peng.ExecuteNonQuery();
                        conn.Close();
                        service.audit_trail("P0078", "Hapus","NO KAKITANGAN", varName1);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        BindData_Grid();
                    }
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        
        BindData_Grid();
        string script = " $(function () {$(" + GridView1.ClientID + ").prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }
    protected void lnkselect_Click(object sender, EventArgs e)
    {
        if (ddorganisasi.SelectedItem.Text != "")
        {
            BindData_Grid1();
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lb_sdt");
            string lblid1 = lblTitle.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * From hr_staff_profile where stf_staff_no='" + lblTitle.Text + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                string name = HttpUtility.UrlEncode(service.Encrypt(lblTitle.Text));
                //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
                Response.Redirect(string.Format("../SUMBER_MANUSIA/HR_DAFTER_STAFF.aspx?edit={0}", name));
            }
            else
            {
                Session["validate_success"] = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void ctk_values(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count1 = 0;
       
            
            DataTable dt = new DataTable();
            dt = DBCon.Ora_Execute_table("select stf_staff_no as v1,stf_icno as v2,ISNULL(hg.hr_gelaran_desc,'-') as v3,stf_name as v4,ISNULL(ht.hr_titl_desc,'-') as v5,ISNULL(rg.gender_desc,'-') as v6,stf_age  as v7,format(stf_dob,'dd/MM/yyyy') as v8,ISNULL(hn.hr_negeri_desc,'-') as v9,ISNULL(hb.hr_bangsa_desc,'-') as v10,ISNULL(hw.hr_wargan_desc,'-') as v11,ISNULL(ha.hr_agama_desc,'-') as v12,ISNULL(hsp.hr_perkha_desc,'-') as v13,ISNULL(stf_permanent_address,'-') as v14,ISNULL(stf_permanent_postcode,'-') as v15,ISNULL(stf_permanent_city,'-') as v16,ISNULL(hn1.hr_negeri_desc,'-') as v17,ISNULL(stf_mailing_address,'-') as v18,ISNULL(stf_mailing_postcode,'-') as v19,ISNULL(stf_mailing_city,'-') as v20,ISNULL(hn2.hr_negeri_desc,'-') as v21,ISNULL(stf_phone_h,'-') as v22,ISNULL(stf_phone_m,'-') as v23,ISNULL(stf_email,'-') as v24,stf_image as v25,FORMAT(stf_service_start_dt,'dd/MM/yyyy', 'en-us') as v26, o1.org_name v27, o2.op_perg_name v28 from hr_staff_profile left join ref_gender as rg on rg.gender_cd=stf_sex_cd left join Ref_hr_gelaran as hg on hg.hr_gelaran_Code=stf_salutation_cd left join Ref_hr_title ht on ht.hr_titl_Code=stf_title_cd left join ref_hr_negeri hn on hn.hr_negeri_Code=stf_pob left join Ref_hr_bangsa hb on hb.hr_bangsa_Code=stf_race_cd left join Ref_hr_wargan hw on hw.hr_wargan_Code=stf_nationality_cd left join Ref_hr_agama ha on ha.hr_agama_Code=stf_religion_cd left join Ref_hr_sts_perkha hsp on hsp.hr_perkha_Code=stf_marital_sts_cd left join ref_hr_negeri hn1 on hn1.hr_negeri_Code=stf_permanent_state_cd left join ref_hr_negeri hn2 on hn2.hr_negeri_Code=stf_mailing_state_cd left join hr_organization o1 on o1.org_gen_id=str_curr_org_cd left join hr_organization_pern o2 on o2.op_perg_code=stf_cur_sub_org");
           
          
            if (dt.Rows.Count != 0)
            {
            

            StringBuilder builder = new StringBuilder();
                    string strFileName = string.Format("{0}.{1}", "MAKLUMAT_KAITANGAN_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                    builder.Append("No Kakitangan ,No KP Baru / Passport,Nama, Organisasi, Pernigaan, Jantina, Gelaran, Tarikh Lahir,Pangkat, Bangsa,Umur, Agama,Negeri Lahir,Warganegara,Status Perkahwinan,Alamat Tetap,Poskod,Bandar,Negeri,Alamat Surat-Menyurat,Poskod,Bandar,Negeri,No Telefon (R),No Telefon (B),Email,Jawatan,Kategori,Skim,Jabatan,Unit,Gred,Status Penjawatan,Tarikh Mula Khidmat,Tarikh Lantikan1,Tarikh Lantikan2,Penyelia 1,Penyelia 2,Waktu Bekerja,Sebab Pergerakan,Tarikh Berhenti,Sebab Berhenti" + Environment.NewLine);
            for (int k = 0; k <= (dt.Rows.Count - 1); k++)
            {
                DataTable dt1 = new DataTable();
                dt1 = DBCon.Ora_Execute_table("select '' as p1,FORMAT(pos_start_dt,'dd/MM/yyyy', 'en-us') as p2,FORMAT(pos_end_dt,'dd/MM/yyyy', 'en-us') as p3,ISNULL(hj.hr_jaw_desc,'') as p4,UPPER(ISNULL(hg.hr_gred_desc,'-')) as p5,UPPER(ISNULL(hu.hr_unit_desc,'-')) as p6,UPPER(ISNULL(jab.hr_jaba_desc,'-')) as p7,ISNULL(kp.hr_kejaw_desc,'-') as p8,ISNULL(hs1.hr_skim_desc,'-') as p9,ISNULL(pt.hr_traf_desc,'-') as p10,ISNULL(sp1.stf_name,'-') as p11,ISNULL(sp2.stf_name,'-') as p12,ISNULL(jk.hr_jad_desc,'-') as p13,format(sp3.stf_service_start_dt,'dd/MM/yyyy') as p14,format(sp3.stf_service_end_dt,'dd/MM/yyyy') as p15,ISNULL(pk.hr_perkaki_desc,'-') as p16,isnull(hp1.hr_pen_desc,'-') as p17 from hr_post_his ph left join ref_hr_jawatan hj on hj.hr_jaw_Code=ph.pos_post_cd left join ref_hr_gred hg on hg.hr_gred_Code=ph.pos_grade_cd left join ref_hr_unit hu on hu.hr_unit_Code=ph.pos_unit_cd left join Ref_hr_jabatan jab on jab.hr_jaba_Code=ph.pos_dept_cd left join Ref_hr_kategori_perjawatn kp on kp.hr_kejaw_Code=ph.pos_job_cat_cd left join ref_hr_skim hs1 on hs1.hr_skim_Code=ph.pos_scheme_cd left join Ref_hr_penj_traf pt on pt.hr_traf_Code=ph.pos_job_sts_cd left join hr_staff_profile sp1 on sp1.stf_staff_no=ph.pos_spv_name1 left join hr_staff_profile sp2 on sp2.stf_staff_no=ph.pos_spv_name2 left join hr_staff_profile sp3 on sp3.stf_staff_no=ph.pos_staff_no left join Ref_hr_Jad_kerj as jk on jk.hr_jad_Code=sp3.stf_working_hour left join Ref_hr_per_kaki as pk on pk.hr_perkaki_Code=ph.pos_move_reason_cd left join Ref_hr_penamatan as hp1 on hp1.hr_pen_Code=sp3.stf_service_end_reason where pos_end_dt='9999-12-31' and  pos_staff_no ='" + dt.Rows[k]["v1"].ToString() + "'");
                if (dt1.Rows.Count != 0)
                {
                    builder.Append(dt.Rows[k]["v1"].ToString() + " , " + dt.Rows[k]["v2"].ToString() + "," + dt.Rows[k]["v4"].ToString() + "," + dt.Rows[k]["v27"].ToString() + "," + dt.Rows[k]["v28"].ToString() + "," + dt.Rows[k]["v6"].ToString() + "," + dt.Rows[k]["v3"].ToString() + "," + dt.Rows[k]["v8"].ToString() + "," + dt.Rows[k]["v5"].ToString() + "," + dt.Rows[k]["v10"].ToString() + "," + dt.Rows[k]["v7"].ToString() + "," + dt.Rows[k]["v12"].ToString() + "," + dt.Rows[k]["v9"].ToString() + "," + dt.Rows[k]["v11"].ToString() + "," + dt.Rows[k]["v13"].ToString() + "," + dt.Rows[k]["v14"].ToString().Replace(",", "") + "," + dt.Rows[k]["v15"].ToString() + "," + dt.Rows[k]["v16"].ToString() + "," + dt.Rows[k]["v17"].ToString() + "," + dt.Rows[k]["v18"].ToString().Replace(",", "") + "," + dt.Rows[k]["v19"].ToString() + "," + dt.Rows[k]["v20"].ToString() + "," + dt.Rows[k]["v21"].ToString() + "," + dt.Rows[k]["v22"].ToString() + "," + dt.Rows[k]["v23"].ToString() + "," + dt.Rows[k]["v24"].ToString() + "," + dt1.Rows[0]["p4"].ToString() + "," + dt1.Rows[0]["p8"].ToString() + "," + dt1.Rows[0]["p9"].ToString() + "," + dt1.Rows[0]["p7"].ToString() + "," + dt1.Rows[0]["p5"].ToString() + "," + dt1.Rows[0]["p6"].ToString() + "," + dt1.Rows[0]["p10"].ToString() + "," + dt1.Rows[0]["p14"].ToString() + "," + dt1.Rows[0]["p2"].ToString() + "," + dt1.Rows[0]["p3"].ToString() + "," + dt1.Rows[0]["p11"].ToString() + "," + dt1.Rows[0]["p12"].ToString() + "," + dt1.Rows[0]["p13"].ToString() + "," + dt1.Rows[0]["p16"].ToString() + "," + dt1.Rows[0]["p15"].ToString() + "," + dt1.Rows[0]["p17"].ToString() + Environment.NewLine);
                }
                else
                {
                    builder.Append(dt.Rows[k]["v1"].ToString() + " , " + dt.Rows[k]["v2"].ToString() + "," + dt.Rows[k]["v4"].ToString() + "," + dt.Rows[k]["v27"].ToString() + "," + dt.Rows[k]["v28"].ToString() + "," + dt.Rows[k]["v6"].ToString() + "," + dt.Rows[k]["v3"].ToString() + "," + dt.Rows[k]["v8"].ToString() + "," + dt.Rows[k]["v5"].ToString() + "," + dt.Rows[k]["v10"].ToString() + "," + dt.Rows[k]["v7"].ToString() + "," + dt.Rows[k]["v12"].ToString() + "," + dt.Rows[k]["v9"].ToString() + "," + dt.Rows[k]["v11"].ToString() + "," + dt.Rows[k]["v13"].ToString() + "," + dt.Rows[k]["v14"].ToString().Replace(",", "") + "," + dt.Rows[k]["v15"].ToString() + "," + dt.Rows[k]["v16"].ToString() + "," + dt.Rows[k]["v17"].ToString() + "," + dt.Rows[k]["v18"].ToString().Replace(",", "") + "," + dt.Rows[k]["v19"].ToString() + "," + dt.Rows[k]["v20"].ToString() + "," + dt.Rows[k]["v21"].ToString() + "," + dt.Rows[k]["v22"].ToString() + "," + dt.Rows[k]["v23"].ToString() + "," + dt.Rows[k]["v24"].ToString()  + Environment.NewLine);
                }
            }
                    Response.Clear();
                    Response.ContentType = "text/csv";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                    Response.Write(builder.ToString());
                    Response.End();
               
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
       
        string script = " $(function () {$(" + GridView1.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }
}