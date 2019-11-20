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
using System.Xml;

public partial class HR_AKADEMIK : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty;
    string level;
    string Status = string.Empty, Status1 = string.Empty;
    string userid;
    string gt_val1 = string.Empty, gt_val2 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        assgn_roles();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                btn_simp.Visible = true;
                btb_kmes.Visible = false;
                DD_pendi.Enabled = true;
                unive();
                bida();
                pendid();
                txt_nama.Attributes.Add("readonly", "readonly");
                txt_gred.Attributes.Add("readonly", "readonly");
                txt_jabat.Attributes.Add("readonly", "readonly");
                txt_jawa.Attributes.Add("readonly", "readonly");
                txt_org.Attributes.Add("readonly", "readonly");
                TextBox3.Attributes.Add("readonly", "readonly");
                txt_stfno.Attributes.Add("readonly", "readonly");
                if (samp != "")
                {
                    txt_stfno.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();

                }
                else
                {

                }
                userid = Session["New"].ToString();


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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1697','448','505','484','77','1565','513','1675','497','1288','190','1698','461','1699','1700','1701','1702','1703','61','133','15') order by ID ASC");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;

            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());

            h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());

            lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            lbl8_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            lbl9_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            lbl10_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            lbl11_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
            lbl12_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            lbl13_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());

            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            btn_simp.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            btn_hups.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
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
                ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0089' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

                if (ddokdicno_1.Rows.Count != 0)
                {

                    gt_val1 = ddokdicno_1.Rows[0]["Edit_chk"].ToString();

                }
            }
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void view_details()
    {
        if (txt_stfno.Text != "")
        {
            if (gt_val1 == "1")
            {
                btn_simp.Visible = true;
                btn_hups.Visible = true;
            }
            else
            {
                btn_hups.Visible = false;
                btn_simp.Visible = false;
            }
            btb_kmes.Visible = false;
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where '" + txt_stfno.Text + "' IN (stf_staff_no)");
            //string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
            if (ddicno.Rows.Count > 0)
            {
                Applcn_no1.Text = ddicno.Rows[0]["stf_staff_no"].ToString();
                DataTable ddbind = new DataTable();
                ddbind = DBCon.Ora_Execute_table("select stf_staff_no,hr_gred_desc,hr_jaba_desc,stf_name,hr_jaw_desc,ho.org_name,o1.op_perg_name From hr_staff_profile as SP left join Ref_hr_jabatan as JB on JB.hr_jaba_Code=SP.stf_curr_dept_cd left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=SP.stf_curr_post_cd left join Ref_hr_gred as GR on GR.hr_gred_Code=SP.stf_curr_grade_cd left join hr_organization ho on ho.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern o1 on o1.op_perg_code=stf_cur_sub_org where stf_staff_no='" + Applcn_no1.Text + "'");
                //txt_stfno.Text = ddbind.Rows[0]["stf_staff_no"].ToString();
                txt_org.Text = ddbind.Rows[0]["org_name"].ToString();
                txt_jabat.Text = ddbind.Rows[0]["hr_jaba_desc"].ToString();
                txt_gred.Text = ddbind.Rows[0]["hr_gred_desc"].ToString();
                txt_nama.Text = ddbind.Rows[0]["stf_name"].ToString();
                txt_jawa.Text = ddbind.Rows[0]["hr_jaw_desc"].ToString();
                TextBox3.Text = ddbind.Rows[0]["op_perg_name"].ToString();
                //txt_sipr.Text = "";
                //txt_noru.Text = "";
                //txt_tadi.Text = "";
                //txt_tata.Text = "";
                grid();
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Kakitangan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }



    

    void pendid()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_pendi_Code,UPPER(hr_pendi_desc) as hr_pendi_desc From Ref_hr_pendidikan where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_pendi.DataSource = dt;
            DD_pendi.DataTextField = "hr_pendi_desc";
            DD_pendi.DataValueField = "hr_pendi_Code";
            DD_pendi.DataBind();
            DD_pendi.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void bida()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_bidang_Code,UPPER(hr_bidang_desc) as hr_bidang_desc From Ref_hr_bidang where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_bida.DataSource = dt;
            DD_bida.DataTextField = "hr_bidang_desc";
            DD_bida.DataValueField = "hr_bidang_Code";
            DD_bida.DataBind();
            DD_bida.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void sel_inst(object sender, EventArgs e)
    {
        unive();
        grid();
    }

    void unive()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_ins_Code,hr_ins_desc from Ref_hr_institute where status='A' and hr_ins_penCode='" + DD_pendi.SelectedValue + "' order by hr_ins_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_insti.DataSource = dt;
            DD_insti.DataTextField = "hr_ins_desc";
            DD_insti.DataValueField = "hr_ins_Code";
            DD_insti.DataBind();
            DD_insti.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btn_cari_Click(object sender, EventArgs e)
    {
        if (txt_stfno.Text != "")
        {
            
            if (gt_val1 == "1")
            {
                btn_simp.Visible = true;
            }
            else
            {
                btn_simp.Visible = false;
            }
            btb_kmes.Visible = false;
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where '" + txt_stfno.Text + "' IN (stf_staff_no,stf_name)");            
            if (ddicno.Rows.Count > 0)
            {
                Applcn_no1.Text = ddicno.Rows[0]["stf_staff_no"].ToString();
                DataTable ddbind = new DataTable();
                ddbind = DBCon.Ora_Execute_table("select stf_staff_no,hr_gred_desc,hr_jaba_desc,stf_name,hr_jaw_desc,ho.org_name,o1.op_perg_name From hr_staff_profile as SP left join Ref_hr_jabatan as JB on JB.hr_jaba_Code=SP.stf_curr_dept_cd left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=SP.stf_curr_post_cd left join Ref_hr_gred as GR on GR.hr_gred_Code=SP.stf_curr_grade_cd left join hr_organization ho on ho.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern o1 on o1.op_perg_code=stf_cur_sub_org where stf_staff_no='" + Applcn_no1.Text + "'");
                txt_org.Text = ddbind.Rows[0]["org_name"].ToString();
                txt_jabat.Text = ddbind.Rows[0]["hr_jaba_desc"].ToString();
                txt_gred.Text = ddbind.Rows[0]["hr_gred_desc"].ToString();
                txt_nama.Text = ddbind.Rows[0]["stf_name"].ToString();
                txt_jawa.Text = ddbind.Rows[0]["hr_jaw_desc"].ToString();
                TextBox3.Text = ddbind.Rows[0]["op_perg_name"].ToString();
                grid();
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Kakitangan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    void grid()
    {
        con.Open();
        DataTable ddicno = new DataTable();        
        SqlCommand cmd = new SqlCommand("select hr_pendi_desc,edu_lvl_cd,hr_bidang_desc,hr_ins_desc,edu_year_gain,edu_result,FORMAT(edu_crt_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as crdt From hr_education as HR left join Ref_hr_pendidikan as PE ON PE.hr_pendi_Code=HR.edu_lvl_cd left join Ref_hr_bidang as BI on BI.hr_bidang_Code=HR.edu_program_cd left Join Ref_hr_institute as UN on UN.hr_ins_Code=HR.edu_institution_cd and un.hr_ins_penCode=hr.edu_lvl_cd where edu_staff_no='" + Applcn_no1.Text + "'", con);
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
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        con.Close();
        DD_pendi.Enabled = true;
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        btn_simp.Visible = false;
        if (gt_val1 == "1")
        {
            btb_kmes.Visible = true;
        }
        else
        {
            btb_kmes.Visible = false;
        }

        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label1");
        System.Web.UI.WebControls.Label lbl_cdt = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label4_cdt");
        string abc = lblTitle.Text;
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "' ");
        //string stffno = ddokdicno.Rows[0][0].ToString();
        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno1 = new DataTable();
            ddokdicno1 = DBCon.Ora_Execute_table("select edu_lvl_cd,edu_program_cd,edu_institution_cd,edu_year_gain,edu_result,edu_ref_institution,FORMAT(edu_crt_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as crdt From hr_education where edu_lvl_cd='" + abc + "' and edu_crt_dt='" + lbl_cdt.Text + "' and edu_staff_no='" + Applcn_no1.Text + "'");
            if (ddokdicno1.Rows.Count != 0)
            {
                //DD_pendi.Attributes.Add("Style","Pointer-events:none;");
                DD_pendi.SelectedValue = ddokdicno1.Rows[0]["edu_lvl_cd"].ToString();
                DD_bida.SelectedValue = ddokdicno1.Rows[0]["edu_program_cd"].ToString().Trim();
                unive();
                DD_insti.SelectedValue = ddokdicno1.Rows[0]["edu_institution_cd"].ToString().Trim();
                txt_diper.Text = ddokdicno1.Rows[0]["edu_year_gain"].ToString();
                txt_keput.Text = ddokdicno1.Rows[0]["edu_result"].ToString();
                txt_rujins.Text = ddokdicno1.Rows[0]["edu_ref_institution"].ToString();
                TextBox2.Text = ddokdicno1.Rows[0]["crdt"].ToString();              
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
            TextBox1.Text = ddokdicno1.Rows[0]["edu_lvl_cd"].ToString();

        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void btn_simp_Click(object sender, EventArgs e)
    {
        if (txt_stfno.Text != "")
        {
            DD_pendi.Enabled = true;
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "' ");
            //string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
            if (ddicno.Rows.Count > 0)
            {
                if (txt_stfno.Text != "" && DD_pendi.SelectedValue != "")
                {
                    DataTable ddicno1 = new DataTable();
                    ddicno1 = DBCon.Ora_Execute_table("select * From hr_education where edu_staff_no='" + Applcn_no1.Text + "' and edu_lvl_cd='" + DD_pendi.SelectedValue + "' and edu_program_cd='" + DD_bida.SelectedValue + "' and edu_institution_cd='" + DD_insti.SelectedValue + "'");
                    if (ddicno1.Rows.Count == 0)
                    {
                        SqlCommand ins_prof = new SqlCommand("insert into hr_education (edu_staff_no,edu_lvl_cd,edu_program_cd,edu_institution_cd,edu_year_gain,edu_result,edu_crt_id,edu_crt_dt,edu_ref_institution) values(@edu_staff_no,@edu_lvl_cd,@edu_program_cd,@edu_institution_cd,@edu_year_gain,@edu_result,@edu_crt_id,@edu_crt_dt,@edu_ref_institution)", con);
                        ins_prof.Parameters.AddWithValue("edu_staff_no", Applcn_no1.Text);
                        ins_prof.Parameters.AddWithValue("edu_lvl_cd", DD_pendi.SelectedValue);
                        ins_prof.Parameters.AddWithValue("edu_program_cd", DD_bida.SelectedValue);
                        ins_prof.Parameters.AddWithValue("edu_institution_cd", DD_insti.SelectedValue);
                        ins_prof.Parameters.AddWithValue("edu_year_gain", txt_diper.Text);
                        ins_prof.Parameters.AddWithValue("edu_result", txt_keput.Text);
                        ins_prof.Parameters.AddWithValue("edu_crt_id", Session["New"].ToString());
                        ins_prof.Parameters.AddWithValue("edu_crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        ins_prof.Parameters.AddWithValue("edu_ref_institution", txt_rujins.Text);
                        con.Open();
                        int i = ins_prof.ExecuteNonQuery();
                        con.Close();
                        txt_clr();
                        grid();
                        service.audit_trail("P0089", "Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {                        
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    grid();                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Maklumat.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                grid();                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('ERROR.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Kakitangan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void btb_kmes_Click(object sender, EventArgs e)
    {
        DD_pendi.Enabled = false;
        DataTable ddicno = new DataTable();
        ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "' ");
        //string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
        if (txt_stfno.Text != "" && DD_pendi.SelectedValue != "")
        {
            SqlCommand akad_upd = new SqlCommand("UPDATE hr_education SET [edu_program_cd] = @edu_program_cd, [edu_institution_cd] = @edu_institution_cd, [edu_year_gain] = @edu_year_gain, [edu_result] = @edu_result,[edu_upd_id]=@edu_upd_id,[edu_upd_dt]=@edu_upd_dt,[edu_ref_institution]=@edu_ref_institution  WHERE edu_staff_no='" + Applcn_no1.Text + "' and edu_crt_dt='" + TextBox2.Text + "' ", con);
            akad_upd.Parameters.AddWithValue("edu_staff_no", Applcn_no1.Text);
            //akad_upd.Parameters.AddWithValue("edu_lvl_cd", DD_pendi.SelectedValue);
            akad_upd.Parameters.AddWithValue("edu_program_cd", DD_bida.SelectedValue);
            akad_upd.Parameters.AddWithValue("edu_institution_cd", DD_insti.SelectedValue);
            akad_upd.Parameters.AddWithValue("edu_year_gain", txt_diper.Text);
            akad_upd.Parameters.AddWithValue("edu_result", txt_keput.Text);
            akad_upd.Parameters.AddWithValue("edu_upd_id", Session["New"].ToString());
            akad_upd.Parameters.AddWithValue("edu_upd_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            akad_upd.Parameters.AddWithValue("edu_ref_institution", txt_rujins.Text);
            con.Open();
            int i = akad_upd.ExecuteNonQuery();
            con.Close();
            grid();
            txt_clr();
            service.audit_trail("P0089", "Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Maklumat.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            
        }
    }
    protected void btn_hups_Click(object sender, EventArgs e)
    {
        if (txt_stfno.Text != "")
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
                    System.Web.UI.WebControls.CheckBox rbn = new System.Web.UI.WebControls.CheckBox();
                    rbn = (CheckBox)row.FindControl("RadioButton1");
                    if (rbn.Checked)
                    {
                        int RowIndex = row.RowIndex;
                        DataTable ddicno = new DataTable();
                        //ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_icno='" + txt_stfno.Text + "' ");
                        //string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
                        string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("Label1")).Text.ToString(); //this store the  value in varName1
                        string crdate = ((Label)row.FindControl("Label4_cdt")).Text.ToString();
                        SqlCommand ins_peng = new SqlCommand("delete from hr_education where edu_lvl_cd='" + varName1 + "' and edu_staff_no='" + Applcn_no1.Text + "' and edu_crt_dt='" + crdate + "'", con);
                        con.Open();
                        int i = ins_peng.ExecuteNonQuery();
                        con.Close();
                        service.audit_trail("P0089", "Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        grid();
                        txt_clr();
                    }

                }
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Kakitangan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    void txt_clr()
    {
        if (gt_val1 == "1")
        {
            btn_simp.Visible = true;
        }
        else
        {
            btn_simp.Visible = false;
        }
        btb_kmes.Visible = false;
        DD_pendi.Attributes.Remove("Style");
        DD_pendi.SelectedValue = "";
        DD_bida.SelectedValue = "";
        DD_insti.SelectedValue = "";
        txt_diper.Text = "";
        txt_keput.Text = "";
        txt_rujins.Text = "";
    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();
        GridView1.DataBind();
    }


    protected void Button5_Click(object sender, EventArgs e)
    {
        txt_clr();
        grid();
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_AKADEMIK_view.aspx");
    }


}