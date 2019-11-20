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

public partial class HR_DAF_FAMLI : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection dbcon = new DBConnection();
    StudentWebService service = new StudentWebService();
    DataTable dt = new DataTable();
    private static int PageSize = 20;
    string Status = string.Empty;
    string CommandArgument1;
    string r1, r2, r3, r4;
    int billno = 00;
    string userid;
    string gt_val1 = string.Empty, gt_val2 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        assgn_roles();
        app_language();
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
               
                var samp = Request.Url.Query;
                wargna();
                Janitna();
                hubungan();
                hubungan1();
                pendi();
                TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtaplno.Attributes.Add("readonly", "readonly");
                txtnama.Attributes.Add("readonly", "readonly");
                txtgred.Attributes.Add("readonly", "readonly");
                txtjab.Attributes.Add("readonly", "readonly");
                txtjaw.Attributes.Add("readonly", "readonly");
                //txttarlah.Attributes.Add("readonly", "readonly");
                //TextBox20.Attributes.Add("readonly", "readonly");
                txt_org.Attributes.Add("readonly", "readonly");
                TextBox2.Attributes.Add("readonly", "readonly");
                ddwarga.SelectedValue = "MYS";
                DropDownList2.SelectedValue = "MYS";

                DropDownList3.SelectedValue = "02";
                DropDownList19.SelectedValue = "02";
                Applcn_no1.Text = "";
                if (samp != "")
                {
                    txtaplno.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
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
        ste_set = dbcon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

        DataTable gt_lng = new DataTable();
        gt_lng = dbcon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('478','448','1674','505','484','77','1565','513','1675','497','1288','190','176','1676','1677','1678','16','46','17','27','45','1141','1679','1680','1681','61','44','1682','1683','1684','34','1685','51','1686','1687','1688','1273','1264','15','133','1141','915','27') order by ID ASC");

        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;

          h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
          bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
          bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[26][0].ToString().ToLower());

          h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());  
          h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower()); 
          h3_tag3.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower()); 
          h3_tag4.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower()); 

          pt1.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower()); 
          pt2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower()); 
          pt3.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower()); 

          lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower()); 
          lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[25][0].ToString().ToLower()); 
          lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower()); 
          lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[27][0].ToString().ToLower());
          lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
          lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
          lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
          lbl8_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[30][0].ToString().ToLower());
          lbl9_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
          lbl10_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
          lbl11_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
          lbl12_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
          lbl13_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());

          lbl14_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower()); 
          lbl15_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower());
          lbl16_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower());
          lbl17_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());

          Rdbwar.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
          RdbBwar.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
          RadioButton6.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
          RadioButton9.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
           
          Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower()); 
          Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower()); 

          lbl18_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[30][0].ToString().ToLower()); 
          lbl19_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower()); 
          lbl20_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower()); 
          lbl21_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
          lbl22_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
          lbl23_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
          lbl24_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower());
          lbl25_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
          lbl26_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[37][0].ToString().ToLower());
          lbl27_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
          lbl28_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[40][0].ToString().ToLower());
          lbl29_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());

          RadioButton7.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
          RadioButton8.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
          RadioButton10.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
          RadioButton11.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
          RadioButton1.Text = txtinfo.ToTitleCase(gt_lng.Rows[34][0].ToString().ToLower());
          RadioButton2.Text = txtinfo.ToTitleCase(gt_lng.Rows[35][0].ToString().ToLower());
          RadioButton3.Text = txtinfo.ToTitleCase(gt_lng.Rows[36][0].ToString().ToLower());
          RadioButton4.Text = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower());
          RadioButton5.Text = txtinfo.ToTitleCase(gt_lng.Rows[39][0].ToString().ToLower());

          Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower()); 
          Button8.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower()); 
          Button7.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower()); 

          lbl30_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
          lbl31_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower()+" (R/P) "); 
          lbl32_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
          lbl33_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
          lbl34_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
          Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
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
            ddokdicno = dbcon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

            if (ddokdicno.Rows.Count != 0)
            {
                DataTable ddokdicno_1 = new DataTable();
                ddokdicno_1 = dbcon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0079' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

                if (ddokdicno_1.Rows.Count != 0)
                {

                    gt_val1 = ddokdicno_1.Rows[0]["Edit_chk"].ToString();
                    if (gt_val1 == "1")
                    {
                        Button2.Visible = true;
                        Button3.Visible = true;
                       
                    }
                    else
                    {
                        Button2.Visible = false;
                        Button3.Visible = false;
                       
                    }
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
        try
        {
            DataTable dd1 = new DataTable();
            dd1 = dbcon.Ora_Execute_table("select stf_staff_no from hr_staff_profile where '" + txtaplno.Text + "' IN (stf_staff_no)");
            if (dd1.Rows.Count > 0)
            {
                Applcn_no1.Text = dd1.Rows[0]["stf_staff_no"].ToString();
                bind();
                bind1();
                BindGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
            }
        }
        catch
        {

        }
    }


  

    void gv_list1()
    {
        p1.Attributes.Add("class", "tab-pane active");
        p2.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane");

        pp1.Attributes.Add("class", "active");
        pp2.Attributes.Remove("class");
        pp3.Attributes.Remove("class");

    }

    void gv_list2()
    {
        p2.Attributes.Add("class", "tab-pane active");
        p1.Attributes.Add("class", "tab-pane");
        p3.Attributes.Add("class", "tab-pane");

        pp2.Attributes.Add("class", "active");
        pp1.Attributes.Remove("class");
        pp3.Attributes.Remove("class");

    }

    void gv_list3()
    {
        p3.Attributes.Add("class", "tab-pane active");
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane");

        pp3.Attributes.Add("class", "active");
        pp1.Attributes.Remove("class");
        pp2.Attributes.Remove("class");

    }

    public void bind()
    {
        dt = dbcon.Ora_Execute_table("select stf_name,stf_curr_grade_cd,stf_curr_dept_cd,rhj.hr_jaba_desc,stf_curr_post_cd,rh.hr_jaw_desc,hsp.stf_er_contact_name,hsp.stf_er_contact_rel_cd,hsp.stf_er_contact_address,hsp.stf_er_contact_phone_p,hsp.stf_er_contact_phone_m,ho.org_name,o1.op_perg_name from hr_staff_profile hsp left join Ref_hr_jabatan rhj on hsp.stf_curr_dept_cd=rhj.hr_jaba_Code left join ref_hr_jawatan rh on hsp.stf_curr_post_cd=rh.hr_jaw_Code left join hr_organization ho on ho.org_gen_id=str_curr_org_cd left join hr_organization_pern o1 on o1.op_perg_code=stf_cur_sub_org where stf_staff_no ='" + Applcn_no1.Text + "'");
        if (dt.Rows.Count > 0)
        {
            txtnama.Text = dt.Rows[0][0].ToString().Trim();
            txtgred.Text = dt.Rows[0][1].ToString().Trim();
            txtjab.Text = dt.Rows[0][3].ToString().Trim();
            txtjaw.Text = dt.Rows[0][5].ToString().Trim();

            TextBox22.Text = dt.Rows[0]["stf_er_contact_name"].ToString().Trim();
            txt_org.Text = dt.Rows[0]["org_name"].ToString();
            TextBox23.Text = dt.Rows[0]["stf_er_contact_phone_p"].ToString().Trim();
            DropDownList19.SelectedValue = dt.Rows[0]["stf_er_contact_rel_cd"].ToString().Trim();
            TextBox9.Text = dt.Rows[0]["stf_er_contact_phone_m"].ToString().Trim();
            txtAla.Value = dt.Rows[0]["stf_er_contact_address"].ToString().Trim();
            TextBox2.Text = dt.Rows[0]["op_perg_name"].ToString().Trim();
        }

    }

    void Janitna()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select gender_cd,gender_desc from ref_gender";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "gender_desc";
            DropDownList1.DataValueField = "gender_cd";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void pendi()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_pendi_Code,hr_pendi_desc from Ref_hr_pendidikan";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList4.DataSource = dt;
            DropDownList4.DataBind();
            DropDownList4.DataTextField = "hr_pendi_desc";
            DropDownList4.DataValueField = "hr_pendi_Code";
            DropDownList4.DataBind();
            DropDownList4.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void hubungan()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select Contact_Code,UPPER(Contact_Name) as Contact_Name from Ref_Hubungan where Contact_Code IN('02','99','03','06') order by Contact_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "Contact_Name";
            DropDownList3.DataValueField = "Contact_Code";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void hubungan1()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select Contact_Code,UPPER(Contact_Name) as Contact_Name from Ref_Hubungan where Contact_Code NOT IN('05') order by Contact_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList19.DataSource = dt;
            DropDownList19.DataTextField = "Contact_Name";
            DropDownList19.DataValueField = "Contact_Code";
            DropDownList19.DataBind();
            DropDownList19.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void wargna()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_wargan_code,hr_wargan_desc  from Ref_hr_wargan";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddwarga.DataSource = dt;
            ddwarga.DataBind();
            ddwarga.DataTextField = "hr_wargan_desc";
            ddwarga.DataValueField = "hr_wargan_code";
            ddwarga.DataBind();
            ddwarga.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DropDownList2.DataSource = dt;
            DropDownList2.DataBind();
            DropDownList2.DataTextField = "hr_wargan_desc";
            DropDownList2.DataValueField = "hr_wargan_code";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindGrid()
    {
        
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select chl_name,chl_kid_rel_cd,ISNULL(rh.Contact_Name,'') as Contact_Name,case(chl_dependant_sts_ind) when '01' then 'Ya' when '02' then 'Tidak' end as chl_dependant_sts_ind,chl_seq_no  from hr_children hc Left join Ref_Hubungan rh on  hc.chl_kid_rel_cd=rh.Contact_Code  where chl_staff_no ='" + Applcn_no1.Text + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            int columncount = gvSelected.Rows[0].Cells.Count;
            gvSelected.Rows[0].Cells.Clear();
            gvSelected.Rows[0].Cells.Add(new TableCell());
            gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
            gvSelected.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
           
        }
        if (gt_val1 == "1")
        {
            Button5.Visible = true;
            Button7.Visible = true;
        }
        else
        {
            Button5.Visible = false;
            Button7.Visible = false;
        }
        con.Close();
    }

    public void bind1()
    {
        dt = dbcon.Ora_Execute_table("select spo_idno,spo_name,FORMAT(spo_dob,'dd/MM/yyyy', 'en-us') as spo_dob,spo_nationality_cd,spo_address,spo_age,spo_phone,spo_work_ind,spo_post_emp,spo_oku_ind from hr_spouse WHERE spo_staff_no ='" + Applcn_no1.Text + "'");
        if (dt.Rows.Count > 0)
        {
            txtic.Text = dt.Rows[0][0].ToString().Trim();
            txtnama1.Text = dt.Rows[0][1].ToString().Trim();
            txttarlah.Text = dt.Rows[0][2].ToString().Trim();
            ddwarga.Text = dt.Rows[0][3].ToString().Trim();
            txtalamat.Value = dt.Rows[0][4].ToString();
            txtumur.Text = dt.Rows[0][5].ToString().Trim();
            txttel.Text = dt.Rows[0][6].ToString().Trim();
            txtjawmaj.Text = dt.Rows[0][8].ToString().Trim();
            if (dt.Rows[0][7].ToString() == "01")
            {
                Rdbwar.Checked = true;

                RdbBwar.Checked = false;
            }
            else if (dt.Rows[0][7].ToString() == "02")
            {
                RdbBwar.Checked = true;
                Rdbwar.Checked = false;

            }
            else
            {
                RdbBwar.Checked = false;
                Rdbwar.Checked = false;
            }

            if (dt.Rows[0][9].ToString().Trim() == "01")
            {
                RadioButton6.Checked = true;
                RadioButton9.Checked = false;
            }
            else if (dt.Rows[0][9].ToString().Trim() == "02")
            {
                RadioButton6.Checked = false;
                RadioButton9.Checked = true;
            }
            else
            {

                RadioButton9.Checked = false;
                RadioButton6.Checked = false;

            }
        }

    }

    protected void lblSubItemName_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        string[] CommadArgument = btn.CommandArgument.Split(',');
        CommandArgument1 = CommadArgument[0];
        bindchl();
    }

    public void bindchl()
    {
        SqlConnection conn = new SqlConnection(cs);
        string query2 = "select chl_idno,chl_seq_no,chl_name,chl_dob,chl_age,chl_nationality_cd,chl_sex_cd,chl_work_sts_ind,chl_kid_rel_cd,chl_marital_sts_ind,chl_kid_edu_lvl_cd,chl_dependant_sts_ind,chl_oku_sts_ind from hr_children where chl_staff_no ='" + Applcn_no1.Text + "' and chl_seq_no='" + CommandArgument1 + "'";
        conn.Open();
        var sqlCommand3 = new SqlCommand(query2, conn);
        var sqlReader3 = sqlCommand3.ExecuteReader();
        while (sqlReader3.Read())
        {
            if (gt_val1 == "1")
            {
                Button5.Visible = false;
                Button6.Visible = true;
            }
            else
            {
                Button5.Visible = false;
            }
          
            txtsqno.Text = (string)sqlReader3["chl_seq_no"].ToString().Trim();
            TextBox18.Text = (string)sqlReader3["chl_idno"].ToString().Trim();
            TextBox19.Text = (string)sqlReader3["chl_name"].ToString().Trim();
            var feedt = Convert.ToDateTime(sqlReader3["chl_dob"]).ToString("dd/MM/yyyy");
            if (feedt != "01/01/1900")
            {
                TextBox20.Text = feedt;
            }
            else
            {
                TextBox20.Text = "";
            }
            TextBox21.Text = (string)sqlReader3["chl_age"].ToString().Trim();
            DropDownList2.SelectedValue = (string)sqlReader3["chl_nationality_cd"].ToString().Trim();
            DataTable Check = new DataTable();
            Check = dbcon.Ora_Execute_table("select gender_desc from ref_gender where  gender_cd='" + sqlReader3["chl_sex_cd"].ToString() + "'");
            DropDownList1.SelectedValue = (string)sqlReader3["chl_sex_cd"].ToString().Trim();
            if (sqlReader3["chl_work_sts_ind"].ToString() == "01")
            {
                RadioButton1.Checked = true;
                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
            }
            else if (sqlReader3["chl_work_sts_ind"].ToString() == "02")
            {
                RadioButton2.Checked = true;
                RadioButton1.Checked = false;
                RadioButton3.Checked = false;
            }
            else if (sqlReader3["chl_work_sts_ind"].ToString() == "03")
            {
                RadioButton3.Checked = true;
                RadioButton2.Checked = false;
                RadioButton1.Checked = false;
            }
            else
            {
                RadioButton3.Checked = false;
                RadioButton2.Checked = false;
                RadioButton1.Checked = false;
            }

            DropDownList3.SelectedValue = (string)sqlReader3["chl_kid_rel_cd"].ToString().Trim();
            DropDownList4.SelectedValue = (string)sqlReader3["chl_kid_edu_lvl_cd"].ToString().Trim();
            if (sqlReader3["chl_marital_sts_ind"].ToString() == "01")
            {
                RadioButton4.Checked = true;
                RadioButton5.Checked = false;
            }

            else if (sqlReader3["chl_marital_sts_ind"].ToString() == "02")
            {
                RadioButton5.Checked = true;
                RadioButton4.Checked = false;
            }
            else
            {
                RadioButton5.Checked = false;
                RadioButton4.Checked = false;
            }

            if (sqlReader3["chl_dependant_sts_ind"].ToString() == "01")
            {
                RadioButton10.Checked = true;
                RadioButton11.Checked = false;
            }
            else if (sqlReader3["chl_dependant_sts_ind"].ToString() == "02")
            {
                RadioButton11.Checked = true;
                RadioButton10.Checked = false;
            }
            else
            {
                RadioButton11.Checked = false;
                RadioButton10.Checked = false;
            }

            if (sqlReader3["chl_oku_sts_ind"].ToString() == "01")
            {
                RadioButton7.Checked = true;
                RadioButton8.Checked = false;
            }
            else if (sqlReader3["chl_oku_sts_ind"].ToString() == "02")
            {
                RadioButton8.Checked = true;
                RadioButton7.Checked = false;
            }
            else
            {
                RadioButton8.Checked = false;
                RadioButton7.Checked = false;
            }
        }
        gv_list2();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtaplno.Text != "")
            {
                if (txtic.Text != "" && txtnama1.Text != "")
                {
                    DateTime ft = DateTime.ParseExact(txttarlah.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    string sdte = ft.ToString("yyyy-mm-dd");
                    string war1, war2;
                    dt = dbcon.Ora_Execute_table("select spo_staff_no from hr_spouse where spo_staff_no='" + Applcn_no1.Text + "'");
                    if (Rdbwar.Checked == true)
                    {
                        war1 = "01";
                    }
                    else
                    {
                        war1 = "02"; ;
                    }
                    if (RadioButton6.Checked == true)
                    {
                        war2 = "01";
                    }
                    else
                    {
                        war2 = "02"; ;
                    }

                    if (dt.Rows.Count > 0)
                    {

                        string Inssql = "update hr_spouse set spo_idno='" + txtic.Text + "',spo_name='" + txtnama1.Text.Replace("'", "''") + "',spo_dob='" + sdte + "',spo_nationality_cd='" + ddwarga.SelectedValue + "',spo_address='" + txtalamat.Value.Replace("'", "''") + "',spo_age='" + txtumur.Text + "',spo_phone='" + txttel.Text + "',spo_work_ind='" + war1 + "',spo_oku_ind='" + war2 + "',spo_post_emp='" + txtjawmaj.Text.Replace("'", "''") + "',spo_upd_id='" + userid + "',spo_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "'  where spo_staff_no ='" + Applcn_no1.Text + "' and spo_idno ='" + txtic.Text + "'";
                        Status = dbcon.Ora_Execute_CommamdText(Inssql);
                        if (Status == "SUCCESS")
                        {
                            service.audit_trail("P0079", "Pasangan Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Pasangan Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        }
                    }
                    else
                    {

                        SqlCommand ins_prof = new SqlCommand("insert into hr_spouse( spo_staff_no,spo_idno,spo_name,spo_dob,spo_nationality_cd,spo_address,spo_age,spo_phone,spo_work_ind,spo_oku_ind,spo_post_emp,spo_crt_id,spo_crt_dt)values(@spo_staff_no,@spo_idno,@spo_name,@spo_dob,@spo_nationality_cd,@spo_address,@spo_age,@spo_phone,@spo_work_ind,@spo_oku_ind,@spo_post_emp,@spo_crt_id,@spo_crt_dt)", con);
                        ins_prof.Parameters.AddWithValue("spo_staff_no", Applcn_no1.Text.Replace("'", "'"));
                        ins_prof.Parameters.AddWithValue("spo_idno", txtic.Text.Replace("'", "'"));
                        ins_prof.Parameters.AddWithValue("spo_name", txtnama1.Text.Replace("'", "'"));
                        ins_prof.Parameters.AddWithValue("spo_dob", sdte);
                        ins_prof.Parameters.AddWithValue("spo_nationality_cd", ddwarga.SelectedItem.Value);
                        ins_prof.Parameters.AddWithValue("spo_address", txtalamat.Value);
                        ins_prof.Parameters.AddWithValue("spo_age", txtumur.Text.Replace("'", "'"));
                        ins_prof.Parameters.AddWithValue("spo_phone", txttel.Text.Replace("'", "'"));
                        ins_prof.Parameters.AddWithValue("spo_work_ind", war1);
                        ins_prof.Parameters.AddWithValue("spo_oku_ind", war2);
                        ins_prof.Parameters.AddWithValue("spo_post_emp", txtjawmaj.Text.Replace("'", "'"));
                        ins_prof.Parameters.AddWithValue("spo_crt_id", Session["New"].ToString());
                        ins_prof.Parameters.AddWithValue("spo_crt_dt", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                        con.Open();
                        int i = ins_prof.ExecuteNonQuery();
                        con.Close();
                        service.audit_trail("P0079", "Pasangan Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Pasangan Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                    }
                }
                else
                {                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Kakitangan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch
        {
            con.Close();            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Issue.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        gv_list1();
    }

    public void stsPekerjaan()
    {
        if (RadioButton1.Checked == true)
        {
            r1 = "01";
        }
        else if (RadioButton2.Checked == true)
        {
            r1 = "02";
        }
        else if (RadioButton3.Checked == true)
        {
            r1 = "03";
        }
        else
        {
            r1 = "";
        }
    }
    public void oku()
    {
        if (RadioButton7.Checked == true)
        {
            r4 = "01";
        }
        else if (RadioButton8.Checked == true)
        {
            r4 = "02";
        }

        else
        {
            r4 = "";
        }
    }

    public void Perkahwinan()
    {
        if (RadioButton4.Checked == true)
        {
            r2 = "01";
        }
        else if (RadioButton5.Checked == true)
        {
            r2 = "02";
        }

        else
        {
            r2 = "";
        }
    }

    public void Tanggungan()
    {
        if (RadioButton10.Checked == true)
        {
            r3 = "01";
        }
        else if (RadioButton11.Checked == true)
        {
            r3 = "02";
        }

        else
        {
            r3 = "";
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        gv_list2();
        if (txtaplno.Text != "")
        {
            if (TextBox18.Text != "" && TextBox19.Text != "")
            {

                string sdte = string.Empty;
                if (TextBox20.Text != "")
                {
                    DateTime ft = DateTime.ParseExact(TextBox20.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    sdte = ft.ToString("yyyy-mm-dd");
                }
                else
                {
                    sdte = "";
                }
                dt = dbcon.Ora_Execute_table("select count(*) as cnt from hr_children where chl_staff_no='" + Applcn_no1.Text + "' ");
                if (dt.Rows.Count > 0)
                {
                    billno = Convert.ToInt32(dt.Rows[0]["cnt"].ToString()) + 1;
                }
                else
                {
                    billno = 1;
                }
            
                stsPekerjaan();
                oku();
                Perkahwinan();
                Tanggungan();

                string Inssql = "insert into hr_children(chl_staff_no,chl_seq_no,chl_idno,chl_name,chl_dob,chl_age,chl_sex_cd,chl_kid_rel_cd,chl_nationality_cd,chl_work_sts_ind,chl_kid_edu_lvl_cd,chl_oku_sts_ind,chl_dependant_sts_ind,chl_marital_sts_ind,chl_crt_id,chl_crt_dt)values('" + Applcn_no1.Text + "','" + billno + "','" + TextBox18.Text + "','" + TextBox19.Text.Replace("'", "''") + "','" + sdte + "','" + TextBox21.Text + "','" + DropDownList1.SelectedValue + "','" + DropDownList3.SelectedValue + "','" + DropDownList2.SelectedValue + "','" + r1 + "','" + DropDownList4.SelectedValue + "','" + r4 + "','" + r3 + "','" + r2 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                Status = dbcon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    BindGrid();
                    clear_anak();
                    DropDownList3.SelectedValue = "02";
                    service.audit_trail("P0079", "Anak Simpan", "NO KAKITANGAN", Applcn_no1.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                { 
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Insert Gagal.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No kakitangan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    void clear_anak()
    {
        TextBox18.Text = "";
        TextBox19.Text = "";
        TextBox20.Text = "";
        TextBox21.Text = "";
        DropDownList1.SelectedValue = "";
        DropDownList2.SelectedValue = "MYS";
        DropDownList3.SelectedValue = "02";
        DropDownList4.SelectedValue = "";
        RadioButton1.Checked = false;
        RadioButton2.Checked = false;
        RadioButton3.Checked = false;
        RadioButton4.Checked = false;
        RadioButton5.Checked = false;
        RadioButton7.Checked = false;
        RadioButton8.Checked = false;
        RadioButton10.Checked = false;
        RadioButton11.Checked = false;
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        gv_list2();
        if (txtaplno.Text != "")
        {
            if (TextBox18.Text != "" && TextBox19.Text != "")
            {
                string sdte = string.Empty;
                if (TextBox20.Text != "")
                {
                    DateTime ft = DateTime.ParseExact(TextBox20.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    sdte = ft.ToString("yyyy-mm-dd");
                }
                else
                {
                    sdte = "";
                }
                stsPekerjaan();
                oku();
                Perkahwinan();
                Tanggungan();
                dt = dbcon.Ora_Execute_table("select chl_seq_no from hr_children where chl_staff_no='" + Applcn_no1.Text + "' and chl_seq_no = '" + txtsqno.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    string Inssql = "update hr_children set chl_idno='" + TextBox18.Text + "',chl_name='" + TextBox19.Text.Replace("'", "''") + "',chl_dob='" + sdte + "',chl_age='" + TextBox21.Text + "',chl_sex_cd='" + DropDownList1.SelectedItem.Value + "',chl_kid_rel_cd='" + DropDownList3.SelectedItem.Value + "',chl_nationality_cd='" + DropDownList2.SelectedItem.Value + "',chl_work_sts_ind='" + r1 + "',chl_kid_edu_lvl_cd='" + DropDownList4.SelectedItem.Value + "',chl_oku_sts_ind='" + r4 + "',chl_dependant_sts_ind='" + r3 + "',chl_marital_sts_ind='" + r2 + "',chl_upd_id='" + Session["New"].ToString() + "',chl_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  WHERE chl_staff_no = '" + Applcn_no1.Text + "' AND chl_seq_no = '" + txtsqno.Text + "'";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        service.audit_trail("P0079", "Anak Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        BindGrid();
                        clear_anak();
                        DropDownList3.SelectedValue = "02";
                        Button5.Visible = true;
                        Button6.Visible = false;
                      
                    }
                }
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No kakitangan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        gv_list2();
        if (txtaplno.Text != "")
        {
            string rcount = string.Empty;
            int count = 0;
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.RadioButton;
                if (rb.Checked)
                {
                    count++;
                }
                rcount = count.ToString();
            }
            if (rcount != "0")
            {
                foreach (GridViewRow gvrow in gvSelected.Rows)
                {
                    //Finiding checkbox control in gridview for particular row
                    RadioButton chkdelete = (RadioButton)gvrow.FindControl("RadioButton1");
                    //Condition to check checkbox selected or not
                    if (chkdelete.Checked)
                    {
                        //Getting EmployeeID of particular row using datakey value
                        string EmployeeID = ((LinkButton)gvrow.FindControl("lblSubItemName")).Text.ToString();
                        //Getting Connection String from Web.Config

                        using (SqlConnection con = new SqlConnection(cs))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("delete from hr_children where chl_name='" + EmployeeID.Replace("'", "''") + "'", con);
                            cmd.ExecuteNonQuery();
                            con.Close();

                        }
                    }
                }
                DropDownList3.SelectedValue = "02";
                Button5.Visible = true;
                Button6.Visible = false;
                clear_anak();
                BindGrid();
                service.audit_trail("P0079", "Anak Hapus", "NO KAKITANGAN", Applcn_no1.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No Kakitangan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (txtaplno.Text != "")
        {
            string Inssql = "update hr_staff_profile set stf_er_contact_name='" + TextBox22.Text.Replace("'", "''") + "',stf_er_contact_phone_p='" + TextBox23.Text + "',stf_er_contact_phone_m='" + TextBox9.Text + "',stf_er_contact_rel_cd='" + DropDownList19.SelectedValue + "',stf_er_contact_address='" + txtAla.Value.Replace("'", "''") + "' where stf_staff_no ='" + Applcn_no1.Text + "'";
            Status = dbcon.Ora_Execute_CommamdText(Inssql);
            if (Status == "SUCCESS")
            {
                service.audit_trail("P0079", "Orang Hubungan Kecemasan Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Orang Hubungan Kecemasan Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan kakitangan Tiada.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        gv_list3();
    }
    protected void Rdbwar_CheckedChanged(object sender, EventArgs e)
    {
        if (Rdbwar.Checked == true)
        {
            RdbBwar.Checked = false;

        }
        gv_list1();
    }
    protected void RdbBwar_CheckedChanged(object sender, EventArgs e)
    {
        if (RdbBwar.Checked == true)
        {
            Rdbwar.Checked = false;

        }
        gv_list1();
    }
    protected void RadioButton6_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton6.Checked == true)
        {
            RadioButton9.Checked = false;

        }
        gv_list1();
    }
    protected void RadioButton9_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton9.Checked == true)
        {
            RadioButton6.Checked = false;

        }
        gv_list1();
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton1.Checked == true)
        {
            RadioButton2.Checked = false;
            RadioButton3.Checked = false;

        }
        gv_list2();
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton2.Checked == true)
        {
            RadioButton1.Checked = false;
            RadioButton3.Checked = false;

        }
        gv_list2();
    }
    protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton3.Checked == true)
        {
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;

        }
        gv_list2();
    }
    protected void RadioButton4_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton4.Checked == true)
        {
            RadioButton5.Checked = false;

        }
        gv_list2();
    }
    protected void RadioButton5_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton5.Checked == true)
        {
            RadioButton4.Checked = false;

        }
        gv_list2();
    }
    protected void RadioButton10_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton10.Checked == true)
        {
            RadioButton11.Checked = false;

        }
        gv_list2();
    }
    protected void RadioButton11_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton11.Checked == true)
        {
            RadioButton10.Checked = false;

        }
        gv_list2();
    }
    protected void RadioButton7_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton7.Checked == true)
        {
            RadioButton8.Checked = false;

        }
        gv_list2();
    }
    protected void RadioButton8_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton8.Checked == true)
        {
            RadioButton7.Checked = false;

        }
        gv_list2();
    }
 
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        BindGrid();

    }

    protected void marst_Click(object sender, EventArgs e)
    {

        BindGrid();
        clear_anak();
        DropDownList3.SelectedValue = "02";
        gv_list2();
        
        if (gt_val1 == "1")
        {
            Button5.Visible = true;
            Button6.Visible = false;
        }
        else
        {
            Button5.Visible = false;
            Button6.Visible = false;
        }
    }


    protected void rst_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_DAF_FAMLI.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_DAF_FAMLI_view.aspx");
    }


}