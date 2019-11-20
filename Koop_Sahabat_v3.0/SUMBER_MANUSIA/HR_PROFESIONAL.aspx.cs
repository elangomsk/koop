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

public partial class HR_PROFESIONAL : System.Web.UI.Page
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
        assgn_roles();
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                userid = Session["New"].ToString();
                btn_simp.Visible = true;
                btb_kmes.Visible = false;
                txt_stfno.Attributes.Add("readonly", "readonly");
                txt_nama.Attributes.Add("readonly", "readonly");
                txt_gred.Attributes.Add("readonly", "readonly");
                txt_jabat.Attributes.Add("readonly", "readonly");
                txt_jawa.Attributes.Add("readonly", "readonly");
                txt_org.Attributes.Add("readonly", "readonly");
                Applcn_no1.Text = "";
                TextBox3.Attributes.Add("readonly", "readonly");
                txt_tata.Text = "31/12/9999";
                //txt_tadi.Attributes.Add("readonly", "readonly");
                //txt_tata.Attributes.Add("readonly", "readonly");
                grid();
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
        gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1693','448','505','484','77','1565','513','1675','497','1288','190','1694','1695','326','1696','482','61','133','15') order by ID ASC");

        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;

       h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
        bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
        bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());

        h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
        h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());

        lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
        lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
        lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
        lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
        lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
        lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
        lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
        lbl8_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
        lbl9_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
        lbl10_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
        lbl11_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());

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
                ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0088' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

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
            
            btb_kmes.Visible = false;
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
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where '" + txt_stfno.Text + "' IN (stf_staff_no)");
            // string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
            if (ddicno.Rows.Count > 0)
            {
                Applcn_no1.Text = ddicno.Rows[0]["stf_staff_no"].ToString();
                DataTable ddbind = new DataTable();
                ddbind = DBCon.Ora_Execute_table("select stf_staff_no,hr_gred_desc,hr_jaba_desc,stf_name,hr_jaw_desc,ho.org_name,o1.op_perg_name From hr_staff_profile as SP left join Ref_hr_jabatan as JB on JB.hr_jaba_Code=SP.stf_curr_dept_cd left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=SP.stf_curr_post_cd left join Ref_hr_gred as GR on GR.hr_gred_Code=SP.stf_curr_grade_cd left join hr_organization ho on ho.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern o1 on o1.op_perg_code=sp.stf_cur_sub_org where stf_staff_no='" + Applcn_no1.Text + "'");
                //txt_stfno.Text = ddbind.Rows[0]["stf_staff_no"].ToString();
                txt_org.Text = ddbind.Rows[0]["org_name"].ToString();
                txt_jabat.Text = ddbind.Rows[0]["hr_jaba_desc"].ToString();
                txt_gred.Text = ddbind.Rows[0]["hr_gred_desc"].ToString();
                txt_nama.Text = ddbind.Rows[0]["stf_name"].ToString();
                txt_jawa.Text = ddbind.Rows[0]["hr_jaw_desc"].ToString();
                TextBox3.Text = ddbind.Rows[0]["op_perg_name"].ToString();
                clr_txt();
            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
            }
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan No Kakitangan');", true);
        }
    }


  

    void grid()
    {
        con.Open();
        DataTable ddicno = new DataTable();
        //ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_icno='" + Session["New"].ToString() + "' ");
        //string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
        SqlCommand cmd = new SqlCommand("select cer_name,cer_ref_no,ISNULL(CASE WHEN cer_issue_date = '1900-01-01 00:00:00.000' THEN '' ELSE FORMAT(cer_issue_date,'dd/MM/yyyy', 'en-us') END, '') AS d1, ISNULL(CASE WHEN cer_expiry_date = '1900-01-01 00:00:00.000' THEN '' ELSE FORMAT(cer_expiry_date,'dd/MM/yyyy', 'en-us') END, '') AS d2,cer_seq_no From hr_pro_cert where cer_staff_no='" + Applcn_no1.Text + "'", con);
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
        string abc = lblTitle.Text;
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "' ");
        //string stffno = ddokdicno.Rows[0][0].ToString();
        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno1 = new DataTable();
            ddokdicno1 = DBCon.Ora_Execute_table("select cer_name,cer_ref_no,ISNULL(CASE WHEN cer_issue_date = '1900-01-01 00:00:00.000' THEN '' ELSE FORMAT(cer_issue_date,'dd/MM/yyyy', 'en-us') END, '') AS d1, ISNULL(CASE WHEN cer_expiry_date = '1900-01-01 00:00:00.000' THEN '' ELSE FORMAT(cer_expiry_date,'dd/MM/yyyy', 'en-us') END, '') AS d2,cer_seq_no From hr_pro_cert where cer_staff_no='" + Applcn_no1.Text + "' and cer_seq_no='" + abc + "'");
            if (ddokdicno1.Rows.Count != 0)
            {
                txt_sipr.Text = ddokdicno1.Rows[0]["cer_name"].ToString();
                txt_noru.Text = ddokdicno1.Rows[0]["cer_ref_no"].ToString();
                if (ddokdicno1.Rows[0]["d1"].ToString() != "")
                {
                    txt_tadi.Text = ddokdicno1.Rows[0]["d1"].ToString();
                }
                else
                {
                    txt_tadi.Text = "";
                }
                if (ddokdicno1.Rows[0]["d2"].ToString() != "")
                {
                    txt_tata.Text = ddokdicno1.Rows[0]["d2"].ToString();
                }
                else
                {
                    txt_tata.Text = "31/12/9999";
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
            TextBox1.Text = ddokdicno1.Rows[0]["cer_seq_no"].ToString();
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

            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "' ");
            //string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
            if (ddicno.Rows.Count > 0)
            {
                if (txt_stfno.Text != "" && txt_sipr.Text != "")
                {
                    DataTable ddicno1 = new DataTable();
                    ddicno1 = DBCon.Ora_Execute_table("select count(*) as cnt From hr_pro_cert where cer_staff_no='" + Applcn_no1.Text + "' ");
                    int updseq;
                    if (ddicno1.Rows.Count == 0)
                    {
                        updseq = 1;
                    }
                    else
                    {
                        string seqno = ddicno1.Rows[0]["cnt"].ToString();
                        updseq = Convert.ToInt32(seqno) + 1;
                    }
                    Int32 seq = updseq;

                    SqlCommand ins_prof = new SqlCommand("insert into hr_pro_cert (cer_staff_no,cer_name,cer_ref_no,cer_issue_date,cer_expiry_date,cer_seq_no,cer_crt_id,cer_crt_dt) values(@cer_staff_no,@cer_name,@cer_ref_no,@cer_issue_date,@cer_expiry_date,@cer_seq_no,@cer_crt_id,@cer_crt_dt)", con);
                    String fromdate = string.Empty, todate = string.Empty;
                    if (txt_tadi.Text != "")
                    {
                        string datedari = txt_tadi.Text;
                        DateTime dt = DateTime.ParseExact(datedari, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        fromdate = dt.ToString("yyyy-mm-dd");
                    }
                    else
                    {
                        fromdate = "";
                    }
                    if (txt_tata.Text != "")
                    {
                        string datedari1 = txt_tata.Text;
                        DateTime dt1 = DateTime.ParseExact(datedari1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        todate = dt1.ToString("yyyy-mm-dd");
                    }
                    else
                    {
                        todate = "";
                    }
                    ins_prof.Parameters.AddWithValue("cer_staff_no", Applcn_no1.Text);
                    ins_prof.Parameters.AddWithValue("cer_seq_no", seq);
                    ins_prof.Parameters.AddWithValue("cer_issue_date", fromdate);
                    ins_prof.Parameters.AddWithValue("cer_expiry_date", todate);
                    ins_prof.Parameters.AddWithValue("cer_name", txt_sipr.Text.Replace("'", "'"));
                    ins_prof.Parameters.AddWithValue("cer_ref_no", txt_noru.Text);
                    ins_prof.Parameters.AddWithValue("cer_crt_id", Session["New"].ToString());
                    ins_prof.Parameters.AddWithValue("cer_crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    con.Open();
                    int i = ins_prof.ExecuteNonQuery();
                    con.Close();
                    clr_txt();
                    service.audit_trail("P0088", "Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Maklumat.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
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
        DataTable ddicno = new DataTable();
        ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "' ");
        //string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
        if (txt_stfno.Text != "" && txt_sipr.Text != "")
        {
            SqlCommand prof_upd = new SqlCommand("UPDATE hr_pro_cert SET [cer_name] = @cer_name, [cer_ref_no] = @cer_ref_no, [cer_issue_date] = @cer_issue_date, [cer_expiry_date] = @cer_expiry_date,[cer_upd_dt]=@cer_upd_dt,[cer_upd_id]=@cer_upd_id  WHERE cer_seq_no ='" + TextBox1.Text + "' and cer_staff_no='" + Applcn_no1.Text + "' ", con);
            String fromdate = string.Empty, todate = string.Empty;
            if (txt_tadi.Text != "")
            {
                string datedari = txt_tadi.Text;
                DateTime dt = DateTime.ParseExact(datedari, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                fromdate = dt.ToString("yyyy-mm-dd");
            }
            else
            {
                fromdate = "";
            }
            if (txt_tata.Text != "")
            {
                string datedari1 = txt_tata.Text;
                DateTime dt1 = DateTime.ParseExact(datedari1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                todate = dt1.ToString("yyyy-mm-dd");
            }
            else
            {
                todate = "";
            }

            prof_upd.Parameters.AddWithValue("cer_issue_date", fromdate);
            prof_upd.Parameters.AddWithValue("cer_expiry_date", todate);
            prof_upd.Parameters.AddWithValue("cer_name", txt_sipr.Text.Replace("'", "'"));
            prof_upd.Parameters.AddWithValue("cer_ref_no", txt_noru.Text);
            prof_upd.Parameters.AddWithValue("cer_upd_id", Session["New"].ToString());
            prof_upd.Parameters.AddWithValue("cer_upd_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            con.Open();
            int j = prof_upd.ExecuteNonQuery();
            con.Close();
            service.audit_trail("P0088", "Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            clr_txt();
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Maklumat.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    void clr_txt()
    {
        grid();        
        if (gt_val1 == "1")
        {
            btn_simp.Visible = true;
        }
        else
        {
            btn_simp.Visible = false;
        }
        btb_kmes.Visible = false;
        txt_tata.Text = "31/12/9999";
        txt_tadi.Text = "";
        txt_noru.Text = "";
        txt_sipr.Text = "";
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
                if (rb.Checked == true)
                {
                    count++;
                }
                rcount = count.ToString();
            }
            if (rcount != "0")
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    CheckBox rbn = new CheckBox();
                    rbn = (CheckBox)row.FindControl("RadioButton1");
                    if (rbn.Checked == true)
                    {
                        int RowIndex = row.RowIndex;

                        int varName1 = Convert.ToInt32(((Label)row.FindControl("Label1")).Text.ToString()); //this store the  value in varName1
                        DataTable ddicno = new DataTable();
                        ddicno = DBCon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "' ");
                        //string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
                        SqlCommand ins_peng = new SqlCommand("delete from hr_pro_cert where cer_seq_no ='" + varName1 + "' and cer_staff_no='" + Applcn_no1.Text + "'", con);
                        con.Open();
                        int i = ins_peng.ExecuteNonQuery();
                        con.Close();
                        service.audit_trail("P0088", "Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        clr_txt();                       
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
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();

    }


    protected void Button5_Click(object sender, EventArgs e)
    {
        clr_txt();
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_PROFESIONAL_view.aspx");
    }


}