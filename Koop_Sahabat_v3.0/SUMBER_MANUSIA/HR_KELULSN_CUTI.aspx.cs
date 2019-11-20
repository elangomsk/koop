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
using System.Threading;
using System.Net.Mail;
using System.Net;

public partial class HR_KELULSN_CUTI : System.Web.UI.Page
{
  
    DBConnection Dblog = new DBConnection();
    DBConnection DBCon = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection dbcon = new DBConnection();
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string sno = string.Empty;
    string v1 = string.Empty, v2 = string.Empty, v3 = string.Empty, v4 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, strQuery = string.Empty;
    string gt_val1 = string.Empty, gt_val2 = string.Empty;
    string gt_mnth = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    Applcn_no1.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    leave();                    
                    gbind2();
                    TextBox11.Attributes.Add("Readonly", "Readonly");
                    //TextBox12.Attributes.Add("Readonly", "Readonly");
                    TextBox7.Attributes.Add("Readonly", "Readonly");
                    TextBox8.Attributes.Add("Readonly", "Readonly");
                    TextBox5.Attributes.Add("Readonly", "Readonly");
                    TextBox9.Attributes.Add("Readonly", "Readonly");
                    Pengesahan();
                }
                else
                {
                    TextBox10.Text = "";
                }
                useid = Session["New"].ToString();


            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void assgn_roles()
    {
        if (Session["New"] != null)
        {
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

            if (ddokdicno.Rows.Count != 0)
            {
                DataTable ddokdicno_1 = new DataTable();
                ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0061' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

                if (ddokdicno_1.Rows.Count != 0)
                {
                    gt_val1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                }
            }

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void Pengesahan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_Pengesahan_desc,hr_Pengesahan_Code From Ref_hr_Pengesahan_sts where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Penge.DataSource = dt;
            DD_Penge.DataTextField = "hr_Pengesahan_desc";
            DD_Penge.DataValueField = "hr_Pengesahan_Code";
            DD_Penge.DataBind();
            DD_Penge.Items.Insert(0, new ListItem("--- PILIH ---", ""));
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('507','448','1971','484','1565','1788','473','1790','1791','1792','1972','1528','283','1793','53','61','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            //ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            //ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            //ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());          
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());             
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl15.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl16.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        if (TextBox6.Text != "")
        {
            string filePath = Server.MapPath("~/FILES/Attendance/" + TextBox6.Text);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

   
    }

    void leave()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_leave_code,hr_leave_desc from Ref_hr_leave_sts where hr_leave_Code IN ('01','02','03')";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataBind();
            DropDownList2.DataTextField = "hr_leave_desc";
            DropDownList2.DataValueField = "hr_leave_code";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }





    void bind_prorate()
    {
        DateTime feedt2 = DateTime.ParseExact(TextBox7.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        gt_mnth = feedt2.ToString("MM");
    }
    public void gbind2()
    {
        gbind1();
        bind_prorate();
        string year = DateTime.Now.ToString("yyyy");
        strQuery = "select *,case when a.sno ='01' then convert(float,(a.a + ((a.c/12) * (" + gt_mnth + ")) - d)) else '0' end  as pro_rate from (select a1.lev_staff_no,a1.hr_jenis_desc,case when a1.hr_jenis_desc = 'cuti tahunan' then '01' when a1.hr_jenis_desc = 'cuti sakit' then '02' when a1.hr_jenis_desc = 'cuti ganti' then '04'  "
                  + " when a1.hr_jenis_desc = 'cuti bawa hadapan' then '04' end as sno, a1.a,cast(a1.c as float) as c,a1.b, a1.ab,((ISNULL(b1.e,'0')) + ISNULL(c2.pre_e,'0')) as e,a1.d,"
                  + " ((((convert(float,(a1.c)) + a1.a) - (convert(float,(a1.d)) + ISNULL(b1.e, '0')))) - ISNULL(c2.pre_e, '0')) as res,ISNULL(c2.pre_e, '0') as pre_bal from (select  "
                  + " lev_staff_no,lev_leave_type_cd,hr_jenis_desc,lev_carry_fwd_day as a,lev_entitle_day as c,convert(float,(((convert(float,lev_entitle_day) / 12) *  DATEPART(month, GETDATE())))) as b,"
                  + " (lev_carry_fwd_day + convert(float,(((convert(float,lev_entitle_day)))))) as ab,lev_taken_day as d,lev_balance_day from hr_staff_profile as SP left join hr_leave as LV on LV.lev_staff_no= "
                  + " SP.stf_staff_no and LV.lev_org_id=sp.str_curr_org_cd left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code=LV.lev_leave_type_cd where stf_staff_no='" + TextBox10.Text + "' AND lev_year='" + year + "') as a1 "
                  + " full outer join(select lap_staff_no, lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + TextBox10.Text + "' and lap_cancel_ind='N' and "
                  + " lap_leave_type_cd IN('01','04') and lap_approve_sts_cd NOT IN ('04','01') and  ISNULL(lap_approve_sts_cd,'') = '00' group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no="
                  + " a1.lev_staff_no and b1.lap_leave_type_cd=a1.lev_leave_type_cd left join(select lap_staff_no,lap_pre_type_cd,sum(convert(float,lap_pre_balance)) as pre_e from  "
                  + " hr_leave_application where lap_staff_no ='" + TextBox10.Text + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('04','05') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_approve_sts_cd,'') = '00' "
                  + "  group by lap_staff_no,lap_pre_type_cd ) as c2 on  c2.lap_staff_no=a1.lev_staff_no and c2.lap_pre_type_cd = a1.lev_leave_type_cd "

                  + "   union all select a1.col_staff_no,a1.hr_jenis_desc,case when a1.hr_jenis_desc = 'cuti tahunan' then '01' when a1.hr_jenis_desc = 'cuti sakit' then '02' when a1.hr_jenis_desc = 'cuti ganti' then '04' "
                  + "  when a1.hr_jenis_desc = 'cuti bawa hadapan' then '04' end as sno, a1.a,cast(a1.c as float) as c,a1.b, a1.ab,((ISNULL(b1.e, '0')) + ISNULL(c2.pre_e, '0')) as e,a1.d, "
                  + "  ((((convert(float, (a1.c)) + a1.a) - (convert(float, (a1.d)) + ISNULL(b1.e, '0')))) - ISNULL(c2.pre_e, '0')) as res,ISNULL(c2.pre_e, '0') as pre_bal from(select "
                  + "  col_staff_no, col_cat_cd, hr_jenis_desc, '0' as a, sum(convert(float,col_entitle_day)) as c, sum(convert(float, (((convert(float, col_entitle_day) / 12) * DATEPART(month, GETDATE()))))) as b,  "
                  + "   sum(('0' + convert(float, (((convert(float, col_entitle_day))))))) as ab, sum(convert(float, col_taken_day)) as d, sum(convert(float, col_balance_day)) col_balance_day from hr_staff_profile as SP left join hr_com_leave as LV on LV.col_staff_no = "
                  + "  SP.stf_staff_no and LV.col_org_id = sp.str_curr_org_cd left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code = LV.col_cat_cd where stf_staff_no = '" + TextBox10.Text + "' AND col_year = '" + year + "' and col_end_dt >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' group by col_staff_no,col_cat_cd,JC.hr_jenis_desc) as a1 "
                  + "  full outer join(select lap_staff_no, lap_leave_type_cd, sum(convert(float, lap_leave_day)) as e from hr_leave_application where lap_staff_no = '" + TextBox10.Text + "' and lap_cancel_ind = 'N' and "
                  + "  lap_leave_type_cd IN('12') and lap_approve_sts_cd NOT IN('04', '01') and  ISNULL(lap_approve_sts_cd, '') = '00' group by lap_staff_no, lap_leave_type_cd) as b1 on b1.lap_staff_no = "
                  + "  a1.col_staff_no and b1.lap_leave_type_cd = a1.col_cat_cd left join(select lap_staff_no, lap_pre_type_cd, sum(convert(float, lap_pre_balance)) as pre_e from "
                  + "  hr_leave_application where lap_staff_no = '" + TextBox10.Text + "' and lap_cancel_ind = 'N' and lap_leave_type_cd IN('12') and lap_approve_sts_cd NOT IN('04', '01') and ISNULL(lap_approve_sts_cd, '') = '00' "
                  + "  group by lap_staff_no, lap_pre_type_cd ) as c2 on c2.lap_staff_no = a1.col_staff_no and c2.lap_pre_type_cd = a1.col_cat_cd "

                  + "   union all select a.lev_staff_no,a.hr_jenis_desc,case when "
                  + "   a.hr_jenis_desc = 'cuti tahunan' then '01' when a.hr_jenis_desc = 'cuti sakit' then '02' when a.hr_jenis_desc = 'cuti ganti' then '04' when a.hr_jenis_desc = 'cuti bawa hadapan' then '03' "
                  + "   end as sno,a.a,cast(a.c as float) as c,a.b,a.ab,ISNULL(b1.e, '0') as e,ISNULL(b2.e, '0') as d,(a.ab - (ISNULL(b1.e, '0') + ISNULL(b2.e, '0'))) as res,  '0' pre_bal from (select "
                  + "   hsp.stf_staff_no as lev_staff_no, hr_jenis_desc, '0' a, gen_leave_day as c, '' as b, gen_leave_day as ab, '' e, '' d, '' res, s1.hr_jenis_Code from Ref_hr_jenis_cuti s1 inner "
                  + "   join hr_staff_profile as hsp on hsp.stf_staff_no = '" + TextBox10.Text + "' inner "
                  + "   join hr_cmn_general_leave as s2 on s2.gen_leave_type_cd = s1.hr_jenis_Code where hr_jenis_Code In('17')) as a full "
                  + "   outer join(select lap_staff_no, lap_leave_type_cd, sum(convert(float, lap_leave_day)) as e from hr_leave_application where lap_staff_no = '" + TextBox10.Text + "' and lap_cancel_ind = 'N' and "
                  + "   lap_leave_type_cd IN('17') and lap_approve_sts_cd NOT IN('04', '01') and ISNULL(lap_approve_sts_cd, '') = '00' group by   lap_staff_no, lap_leave_type_cd) as b1 on b1.lap_staff_no = "
                  + "   a.lev_staff_no and b1.lap_leave_type_cd = a.hr_jenis_Code full outer join(select lap_staff_no, lap_leave_type_cd, sum(convert(float, lap_leave_day)) as e from hr_leave_application where "
                  + "   lap_staff_no = '" + TextBox10.Text + "' and lap_cancel_ind = 'N' and lap_leave_type_cd IN('12', '17') and lap_approve_sts_cd NOT IN('04', '01') and ISNULL(lap_approve_sts_cd, '00') = '01' and lap_cur_sts = 'Y' "
                  + "   group by   lap_staff_no, lap_leave_type_cd) as b2 on b2.lap_staff_no = a.lev_staff_no and b2.lap_leave_type_cd = a.hr_jenis_Code ) as a where a.lev_staff_no != '' order by sno ";
        //SqlCommand cmd = new SqlCommand("select a1.lev_staff_no,a1.hr_jenis_desc,a1.a,a1.c,a1.b, a1.ab,ISNULL(b1.e,'0') as e,a1.d,((convert(int,(a1.c)) + a1.a) - (convert(int,(a1.d)) + ISNULL(b1.e,'0'))) as res from (select lev_staff_no,lev_leave_type_cd,hr_jenis_desc,lev_carry_fwd_day as a,lev_entitle_day as c,convert(int,(((lev_entitle_day / 12) * DATEPART(month, GETDATE())))) as b,(lev_carry_fwd_day + convert(int,(((lev_entitle_day / 12) * DATEPART(month, GETDATE()))))) as ab,lev_taken_day as d,lev_balance_day from hr_staff_profile as SP left join hr_leave as LV on LV.lev_staff_no=SP.stf_staff_no and LV.lev_org_id=sp.str_curr_org_cd left join Ref_hr_jenis_cuti as JC on JC.hr_jenis_Code=LV.lev_leave_type_cd where stf_staff_no='" + TextBox10.Text + "' AND lev_year='" + DateTime.Now.Year + "') as a1 full outer join(select lap_staff_no,lap_leave_type_cd,sum(lap_leave_day) as e from hr_leave_application where lap_staff_no='" + TextBox10.Text + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('01','04','05') and lap_approve_sts_cd NOT IN ('04','01') group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no=a1.lev_staff_no and b1.lap_leave_type_cd=a1.lev_leave_type_cd", con);
        SqlCommand cmd = new SqlCommand(strQuery, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView2.DataSource = ds;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }
       
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        gbind2();        
    }

  
    public void gbind1()
    {
        
        SqlConnection conn = new SqlConnection(cs);
        string query2 = "select hsp1.stf_name as end_name,sp.stf_name,format(lap_application_dt,'dd/MM/yyyy') lap_application_dt,lap_leave_type_cd,lap_leave_cat_cd,ISNULL(c1.hr_jenis_desc,'') hr_jenis_desc,ISNULL(c2.hr_kategori_desc,'') hr_kategori_desc,format(lap_leave_start_dt,'dd/MM/yyyy') lap_leave_start_dt,format(lap_leave_end_dt,'dd/MM/yyyy')lap_leave_end_dt,lap_reason,ISNULL(lap_cancel_reason,'') as lap_cancel_reason,lap_cancel_ind,lap_leave_day,lap_doc,lap_ref_no,lap_staff_no,ISNULL(lap_approve_remark,'') as remark,lap_approve_sts_cd,lap_endorse_sts_cd,lap_endorse_remark,lap_endorse_id,lap_endorse_dt,ISNULL(lap_pre_balance,'0') lap_pre_balance from hr_leave_application as l1 left join Ref_hr_jenis_cuti c1 on c1.hr_jenis_Code=lap_leave_type_cd left join Ref_hr_kategori_cuti c2 on c2.hr_jenis_code=lap_leave_type_cd and c2.hr_kategori_Code=lap_leave_cat_cd left join hr_staff_profile as sp on sp.stf_staff_no=l1.lap_staff_no left join hr_staff_profile as hsp1 on hsp1.stf_staff_no=l1.lap_endorse_id where l1.Id='" + Applcn_no1.Text +"'";
        conn.Open();
        var sqlCommand3 = new SqlCommand(query2, conn);
        var sqlReader3 = sqlCommand3.ExecuteReader();
        while (sqlReader3.Read())
        {
            TextBox4.Text = sqlReader3["lap_leave_day"].ToString();
            Label4.Text = sqlReader3["lap_staff_no"].ToString();
            Label5.Text = sqlReader3["stf_name"].ToString();
            TextBox10.Text = sqlReader3["lap_staff_no"].ToString();
            //DropDownList1.SelectedValue = (string)sqlReader3["lap_leave_type_cd"].ToString().Trim();
            //DropDownList3.SelectedValue = (string)sqlReader3["lap_leave_cat_cd"].ToString().Trim();
            TextBox11.Text = (string)sqlReader3["hr_jenis_desc"].ToString().Trim();
            TextBox13.Text = (string)sqlReader3["lap_leave_type_cd"].ToString().Trim();
            TextBox12.Text = (string)sqlReader3["lap_ref_no"].ToString().Trim();
            //TextBox12.Text = (string)sqlReader3["hr_kategori_desc"].ToString().Trim();
            TextBox14.Text = (string)sqlReader3["lap_leave_cat_cd"].ToString().Trim();
            TextBox16.Text = (string)sqlReader3["lap_pre_balance"].ToString();
            TextBox7.Text = (string)sqlReader3["lap_leave_start_dt"].ToString().Trim();
            TextBox8.Text = (string)sqlReader3["lap_leave_end_dt"].ToString().Trim();
            Label8.Text = (string)sqlReader3["lap_leave_day"].ToString().Trim();
            if ((string)sqlReader3["lap_cancel_ind"].ToString().Trim() == "N")
            {
                sbc_id.Attributes.Add("style", "Display:none;");
                sc_id.Attributes.Remove("style");
                TextBox5.Text = (string)sqlReader3["lap_reason"].ToString().Trim();
            }
            else if ((string)sqlReader3["lap_cancel_ind"].ToString().Trim() == "Y")
            {
                sc_id.Attributes.Add("style", "Display:none;");
                sbc_id.Attributes.Remove("style");
                TextBox9.Text = (string)sqlReader3["lap_cancel_reason"].ToString().Trim();
            }
            TextBox2.Text = (string)sqlReader3["lap_application_dt"].ToString();
            TextBox3.Text = v1;
            if ((string)sqlReader3["lap_doc"].ToString() != "")
            {
                TextBox6.Text = (string)sqlReader3["lap_doc"].ToString();
                //dfile.Text = (string)sqlReader3["lap_doc"].ToString();
                lnkDownload.Text = (string)sqlReader3["lap_doc"].ToString();
                dfile_id.Attributes.Remove("Style");
            }
            else
            {
                dfile_id.Attributes.Add("Style", "display:None;");
            }
            if(sqlReader3["lap_approve_sts_cd"].ToString() == "01")
            {
                TextBox1.Text = sqlReader3["remark"].ToString();
                DropDownList2.SelectedValue = sqlReader3["lap_approve_sts_cd"].ToString();
                Button2.Visible = false;
            }
            else
            {
                Button2.Visible = true;
            }

            if (sqlReader3["lap_endorse_sts_cd"].ToString() != "")
            {
                DD_Penge.SelectedValue = sqlReader3["lap_endorse_sts_cd"].ToString();
            }
          
            txt_catat.Text = sqlReader3["lap_endorse_remark"].ToString();
            //txt_peny.Text = sqlReader3["end_name"].ToString();
            //if (sqlReader3["lap_endorse_dt"].ToString() != "")
            //{
            //    txt_kelu.Text = Convert.ToDateTime(sqlReader3["lap_endorse_dt"]).ToString("dd/MM/yyyy");
            //}
            //else
            //{
            //    txt_kelu.Text = "";
            //}
        }

    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        if (DD_Penge.SelectedValue != "")
        {
            useid = Session["New"].ToString();
            //dt = dbcon.Ora_Execute_table("select stf_staff_no from hr_staff_profile where stf_icno='" + useid + "'");
            //sno = dt.Rows[0][0].ToString();
            DateTime feedt2 = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string ss1 = feedt2.ToString("yyyy-MM-dd");
            DataTable dtu = new DataTable();
            dtu = dbcon.Ora_Execute_table("select lap_staff_no,lap_application_dt ,lap_leave_type_cd,lap_ref_no from hr_leave_application where Id='" + Applcn_no1.Text + "'");
            if (dtu.Rows.Count > 0)
            {
                DateTime ck_dt = DateTime.ParseExact(TextBox7.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ck_cdt1 = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string vv1 = string.Empty, vv2 = string.Empty;
                if (TextBox13.Text.Trim() == "01")
                {
                    if (ck_dt < ck_cdt1)
                    {
                        vv1 = "02";
                    }
                    else
                    {
                        vv1 = "01";
                    }
                }
                else
                {
                    vv1 = TextBox13.Text.Trim();
                }

                dt = dbcon.Ora_Execute_table("Update  hr_leave_application  set lap_leave_type_cd = '" + vv1 + "',lap_approve_sts_cd ='" + DD_Penge.SelectedItem.Value + "',lap_approve_remark ='" + txt_catat.Text.Replace("'", "''") + "',lap_approve_id ='" + useid + "',lap_approve_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',lap_upd_id ='" + useid + "',lap_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "'  WHERE Id='" + Applcn_no1.Text + "'");

                if (DD_Penge.SelectedValue == "01")
                {
                    string lday = string.Empty, typecd = string.Empty;
                    if (vv1 == "03")
                    {
                        lday = "0.5";
                        typecd = "01";
                    }
                    else if (vv1 == "02")
                    {
                        lday = TextBox4.Text;
                        typecd = "01";
                    }
                    else
                    {
                        lday = TextBox4.Text;
                        typecd = TextBox13.Text;
                    }

                    if (TextBox13.Text == "04" || TextBox13.Text == "05" || TextBox13.Text == "01")
                    {
                        DataTable tot_dtu = new DataTable();
                        tot_dtu = DBCon.Ora_Execute_table("select lev_entitle_day,lev_taken_day,lev_balance_day from hr_leave where lev_staff_no='" + dtu.Rows[0][0].ToString() + "' AND lev_year='" + DateTime.Now.ToString("yyyy") + "' and lev_leave_type_cd='" + typecd + "'");

                        if (tot_dtu.Rows.Count != 0)
                        {
                            string clt = tot_dtu.Rows[0]["lev_entitle_day"].ToString();
                            string cdt = (double.Parse(tot_dtu.Rows[0]["lev_taken_day"].ToString()) + double.Parse(lday)).ToString();
                            string bct = ((double.Parse(tot_dtu.Rows[0]["lev_balance_day"].ToString()) - double.Parse(lday.ToString()))).ToString();
                            dt = DBCon.Ora_Execute_table("Update hr_leave set lev_taken_day='" + cdt.ToString() + "',lev_balance_day='" + bct.ToString() + "' where lev_staff_no='" + dtu.Rows[0][0].ToString() + "' AND lev_year='" + DateTime.Now.ToString("yyyy") + "' and lev_leave_type_cd='" + typecd + "'");

                            if (TextBox13.Text == "04")
                            {
                                DataTable tot_dtu1 = new DataTable();
                                tot_dtu1 = DBCon.Ora_Execute_table("select lev_entitle_day,lev_taken_day,lev_balance_day from hr_leave where lev_staff_no='" + dtu.Rows[0][0].ToString() + "' AND lev_year='" + DateTime.Now.ToString("yyyy") + "' and lev_leave_type_cd='05'");
                                if (tot_dtu1.Rows.Count != 0)
                                {
                                    string cdt1 = (double.Parse(tot_dtu1.Rows[0]["lev_taken_day"].ToString()) + double.Parse(TextBox16.Text)).ToString();
                                    string bct1 = ((double.Parse(tot_dtu1.Rows[0]["lev_balance_day"].ToString()) - double.Parse(TextBox16.Text))).ToString();
                                    dt = DBCon.Ora_Execute_table("Update hr_leave set lev_taken_day='" + cdt1.ToString() + "',lev_balance_day='" + bct1.ToString() + "' where lev_staff_no='" + dtu.Rows[0][0].ToString() + "' AND lev_year='" + DateTime.Now.ToString("yyyy") + "' and lev_leave_type_cd='05'");

                                }
                            }
                            else if (TextBox13.Text == "05")
                            {
                                DataTable tot_dtu2 = new DataTable();
                                tot_dtu2 = DBCon.Ora_Execute_table("select lev_entitle_day,lev_taken_day,lev_balance_day from hr_leave where lev_staff_no='" + dtu.Rows[0][0].ToString() + "' AND lev_year='" + DateTime.Now.ToString("yyyy") + "' and lev_leave_type_cd='05'");
                                if (tot_dtu2.Rows.Count != 0)
                                {
                                    string cdt2 = (double.Parse(tot_dtu2.Rows[0]["lev_taken_day"].ToString()) + double.Parse(TextBox16.Text)).ToString();
                                    string bct2 = ((double.Parse(tot_dtu2.Rows[0]["lev_balance_day"].ToString()) - double.Parse(TextBox16.Text))).ToString();
                                    dt = DBCon.Ora_Execute_table("Update hr_leave set lev_taken_day='" + cdt2.ToString() + "',lev_balance_day='" + bct2.ToString() + "' where lev_staff_no='" + dtu.Rows[0][0].ToString() + "' AND lev_year='" + DateTime.Now.ToString("yyyy") + "' and lev_leave_type_cd='04'");
                                }
                            }
                            else { 
                                DataTable tot_dtu2 = new DataTable();
                                tot_dtu2 = DBCon.Ora_Execute_table("select lev_entitle_day,lev_taken_day,lev_balance_day from hr_leave where lev_staff_no='" + dtu.Rows[0][0].ToString() + "' AND lev_year='" + DateTime.Now.ToString("yyyy") + "' and lev_leave_type_cd='"+ typecd +"'");
                                if (tot_dtu2.Rows.Count != 0)
                                {
                                    string cdt2 = (double.Parse(tot_dtu2.Rows[0]["lev_taken_day"].ToString()) + double.Parse(TextBox16.Text)).ToString();
                                    string bct2 = ((double.Parse(tot_dtu2.Rows[0]["lev_balance_day"].ToString()) - double.Parse(TextBox16.Text))).ToString();
                                    dt = DBCon.Ora_Execute_table("Update hr_leave set lev_taken_day='" + cdt2.ToString() + "',lev_balance_day='" + bct2.ToString() + "' where lev_staff_no='" + dtu.Rows[0][0].ToString() + "' AND lev_year='" + DateTime.Now.ToString("yyyy") + "' and lev_leave_type_cd='" + typecd + "'");
                                }
                            }

                        }
                    }
                    //else if (TextBox13.Text == "12")
                    //{
                    //    string cur_blnce = string.Empty, act_blnce = string.Empty, less_blnce = string.Empty;
                    //    string cdt = string.Empty, bct = string.Empty;
                    //    DataTable tot_dtu = new DataTable();
                    //    tot_dtu = DBCon.Ora_Execute_table("select col_entitle_day,col_taken_day,col_balance_day,FORMAT(col_start_dt,'yyyy-MM-dd') start_dt from hr_com_leave where col_end_dt >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and col_staff_no='" + dtu.Rows[0][0].ToString() + "' and col_year='" + DateTime.Now.ToString("yyyy") + "' and col_cat_cd='12' and col_balance_day != '0' order by col_start_dt");
                    //    if (tot_dtu.Rows.Count != 0)
                    //    {
                    //        for (int i = 0; i < tot_dtu.Rows.Count; i++)
                    //        {
                    //            if (i == 0)
                    //            {
                    //                act_blnce = lday.ToString();
                    //            }
                    //            else
                    //            {
                    //                act_blnce = less_blnce;
                    //            }
                    //            if (act_blnce != "")
                    //            {
                    //                string clt = tot_dtu.Rows[i]["col_entitle_day"].ToString();

                    //                if (double.Parse(act_blnce) <= double.Parse(tot_dtu.Rows[i]["col_balance_day"].ToString()))
                    //                {
                    //                    cur_blnce = act_blnce;
                    //                    cdt = (double.Parse(tot_dtu.Rows[i]["col_taken_day"].ToString()) + double.Parse(act_blnce)).ToString();
                    //                    bct = ((double.Parse(tot_dtu.Rows[i]["col_balance_day"].ToString()) - double.Parse(act_blnce))).ToString();
                    //                }
                    //                else
                    //                {
                    //                    cdt = (double.Parse(tot_dtu.Rows[i]["col_taken_day"].ToString()) + double.Parse(tot_dtu.Rows[i]["col_balance_day"].ToString())).ToString();
                    //                    bct = ((double.Parse(tot_dtu.Rows[i]["col_balance_day"].ToString()) - double.Parse(tot_dtu.Rows[i]["col_balance_day"].ToString()))).ToString();
                    //                    less_blnce = (double.Parse(act_blnce) - double.Parse(tot_dtu.Rows[i]["col_balance_day"].ToString())).ToString();
                    //                    cur_blnce = tot_dtu.Rows[i]["col_balance_day"].ToString();

                    //                }

                    //                dt = DBCon.Ora_Execute_table("Update hr_com_leave set col_taken_day='" + cdt.ToString() + "',col_balance_day='" + bct.ToString() + "' where col_end_dt >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and col_staff_no='" + dtu.Rows[0][0].ToString() + "' and col_year='" + DateTime.Now.ToString("yyyy") + "' and col_cat_cd='12' and col_balance_day != '0' and col_start_dt='" + tot_dtu.Rows[i]["start_dt"].ToString() + "'");
                    //            }

                    //        }

                    //    }
                    //}
                    
                }
                //send_email();
                service.audit_trail("P0061", "Kemaskini", Label4.Text, dtu.Rows[0]["lap_ref_no"].ToString());
                Session["validate_success"] = "SUCCESS";
                Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                Response.Redirect("../SUMBER_MANUSIA/HR_KELULSN_CUTI_view.aspx");
            }
        }
        else
        {            
            gbind2();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Maklumat Kelulusan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }


    void send_email()
    {
        TextInfo txtinfo = culinfo.TextInfo;
        DataTable Ds = new DataTable();
        DataTable Ds1 = new DataTable();
        DataTable get_stf = new DataTable();
        DataTable Ds3 = new DataTable();
        DataTable get_stf_lev = new DataTable();
        string Mailmsg = string.Empty, Mailmsg1 = string.Empty, Mail_id1 = string.Empty, Mail_id2 = string.Empty, Mail_id3 = string.Empty, lev_sts = string.Empty, bal_lve = string.Empty;
        try
        {
            DataTable email_settings = new DataTable();
            email_settings = DBCon.Ora_Execute_table("select config_email_head,config_email_host,config_email_id,config_email_port,config_email_pwd,config_email_url,config_email_web from site_settings where ID='1'");
            if (DD_Penge.SelectedValue == "01")
            {
                lev_sts = "Approved";
            }
            else
            {
                lev_sts = "Rejected";
            }

            if (Ds3.Rows.Count != 0)
            {
                bal_lve = Ds3.Rows[0]["lev_balance_day"].ToString();
            }
            else
            {
                bal_lve = "0";
            }

            Ds3 = Dblog.Ora_Execute_table("select * from hr_leave where lev_year='" + DateTime.Now.Year.ToString() + "' and lev_staff_no='" + Session["new"].ToString() + "' and lev_leave_type_cd='" + TextBox13.Text + "'");

            Ds = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid='admin'");
            Ds1 = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid='C0019'");
            get_stf = Dblog.Ora_Execute_table("select * from hr_staff_profile where stf_staff_no='" + Label4.Text + "'");


            if (TextBox13.Text == "01" || TextBox13.Text == "04" || TextBox13.Text == "05")
            {
                get_stf_lev = Dblog.Ora_Execute_table("select *,lev_balance_day as res from hr_leave where lev_year='" + DateTime.Now.Year.ToString() + "' and lev_staff_no='" + Label4.Text + "' and lev_leave_type_cd='" + TextBox13.Text + "'");
            }
            else
            {
                get_stf_lev = DBCon.Ora_Execute_table("select a.lev_staff_no,a.hr_jenis_desc,a.a,cast(a.c as float) as c,a.b,a.ab,ISNULL(b1.e,'0') as e,ISNULL(b2.e,'0') as d,(a.ab - (ISNULL(b1.e,'0') + ISNULL(b2.e,'0'))) as res from (select '" + Session["New"].ToString() + "' as lev_staff_no,hr_jenis_desc,'0' a,gen_leave_day as c,'' as b,gen_leave_day as ab,'' e, '' d, '' res,s1.hr_jenis_Code from Ref_hr_jenis_cuti s1 inner join hr_cmn_general_leave as s2 on s2.gen_leave_type_cd=s1.hr_jenis_Code where hr_jenis_Code In ('" + TextBox13.Text + "')) as a full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('" + TextBox13.Text + "') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '' group by lap_staff_no,lap_leave_type_cd) as b1 on b1.lap_staff_no=a.lev_staff_no and b1.lap_leave_type_cd=a.hr_jenis_Code full outer join(select lap_staff_no,lap_leave_type_cd,sum(convert(float,lap_leave_day)) as e from hr_leave_application where lap_staff_no='" + Session["New"].ToString() + "' and lap_cancel_ind='N' and lap_leave_type_cd IN('" + TextBox13.Text + "') and lap_approve_sts_cd NOT IN ('04','01') and ISNULL(lap_endorse_sts_cd,'') = '01' and lap_cur_sts='Y' group by lap_staff_no,lap_leave_type_cd) as b2 on b2.lap_staff_no=a.lev_staff_no and b2.lap_leave_type_cd=a.hr_jenis_Code");
            }


            if (Ds3.Rows.Count != 0)
            {
                bal_lve = get_stf_lev.Rows[0]["res"].ToString();
            }
            else
            {
                bal_lve = "0";
            }



            if (Ds.Rows[0]["KK_email"].ToString() != "")
            {
                Mail_id1 = Ds.Rows[0]["KK_email"].ToString();
            }

            if (Ds1.Rows[0]["KK_email"].ToString() != "")
            {
                Mail_id2 = Ds1.Rows[0]["KK_email"].ToString();
            }
            if (get_stf.Rows[0]["stf_email"].ToString() != "")
            {
                Mail_id3 = get_stf.Rows[0]["stf_email"].ToString();
            }
            DateTime dt1 = DateTime.ParseExact(TextBox7.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact(TextBox8.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var fromemail = new MailAddress(email_settings.Rows[0]["config_email_id"].ToString(), email_settings.Rows[0]["config_email_head"].ToString());
            var fromemailpassword = email_settings.Rows[0]["config_email_pwd"].ToString();
            string subject = email_settings.Rows[0]["config_email_web"].ToString() + " - Leave Approval";
            var verifyurl = email_settings.Rows[0]["config_email_url"].ToString();
            var link = verifyurl;
            // Applicant email

            if (Mail_id3 != "")
            {
                var toemail = new MailAddress(Mail_id3);
                string body = "Hello " + txtinfo.ToTitleCase(get_stf.Rows[0]["stf_name"].ToString().ToLower()) + ",<br/> Your Leave application on " + dt1.ToString("dd/MM/yyyy") + " to " + dt2.ToString("dd/MM/yyyy") + " has been " + lev_sts + ". <br/><br/>Remaining " + TextBox11.Text + " is " + bal_lve + " days from this application.<br/><br/> If You require any clarifications about this application, please contact your HR Department.<br/><br/> Thank You,<br/><a><html><body><a href='" + link + "'> " + email_settings.Rows[0]["config_email_web"].ToString() + " </a></body></html> . </a>";

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = email_settings.Rows[0]["config_email_host"].ToString(),
                    Port = Int32.Parse(email_settings.Rows[0]["config_email_port"].ToString()),
                    EnableSsl = false,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromemail.Address, fromemailpassword)


                };
                using (var message = new MailMessage(fromemail, toemail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);



            }
            if (Mail_id2 != "")
            {

                var toemail = new MailAddress(Mail_id2);
                string body = "Hello " + txtinfo.ToTitleCase(Ds1.Rows[0]["Kk_username"].ToString().ToLower()) + ",<br/> " + txtinfo.ToTitleCase(get_stf.Rows[0]["stf_name"].ToString().ToLower()) + " " + TextBox11.Text + " application on " + dt1.ToString("dd/MM/yyyy") + " to " + dt2.ToString("dd/MM/yyyy") + " has been " + lev_sts + ".<br/><br/>Remaining " + TextBox11.Text + " is " + bal_lve + " days from this application.<br/><br/> If You require any clarifications about this application, please contact your HR Department.<br/><br/> Thank You,<br/><a><html><body><a href='" + link + "'> " + email_settings.Rows[0]["config_email_web"].ToString() + " </a></body></html> . </a>";

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = email_settings.Rows[0]["config_email_host"].ToString(),
                    Port = Int32.Parse(email_settings.Rows[0]["config_email_port"].ToString()),
                    EnableSsl = false,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromemail.Address, fromemailpassword)


                };
                using (var message = new MailMessage(fromemail, toemail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);


            }


        }
        catch (Exception ex)
        {
            service.LogError(ex.ToString());
        }
    }
    protected void gvSelected_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        gbind2();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_KELULSN_CUTI.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_KELULSN_CUTI_view.aspx");
    }


}