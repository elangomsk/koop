using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Threading;

public partial class Kmk_anggota : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    StudentWebService service = new StudentWebService();
    string str;
    DataTable ddcom = new DataTable();
    string status = string.Empty;
    string userid;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
              
                wilahBind();
                dropdownName();
                bankdetails();
                bankdet.SelectedValue = "BKRMMYKL";

                var samp = Request.Url.Query;
                if (samp != "")
                {
                    Button3.Text = "Kemaskini";
                    if (role_edit == "1")
                    {
                        Button3.Visible = true;
                    }
                    else
                    {
                        Button3.Visible = false;
                    }
                    TextBox1.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    bind();
                }
               
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }

        TextBox1.MaxLength = 12;
    }


    void app_language()
    {
        if (Session["New"] != null)
        {
            assgn_roles();
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('123','1052','909','124','13','14','1042','16','108','1012','55','30','52','125','126','61','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;


            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            txtSearch.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            restbutn.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl15.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }

    }


    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0135' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();

                if (role_add == "1")
                {
                    Button3.Visible = true;
                }
                else
                {
                    Button3.Visible = false;
                }
               
            }
        }
    }


    void wilahBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string com = "select * from Ref_Cawangan order by Cawangan_Name ASC";

            SqlDataAdapter adpt = new SqlDataAdapter(com, con1);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList3.DataSource = dt;
            DropDownList3.DataBind();
            DropDownList3.DataTextField = "Cawangan_Name";
            DropDownList3.DataValueField = "cawangan_code";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void bankdetails()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string bank_det = "SELECT Bank_Name, Bank_Code FROM Ref_Nama_Bank ORDER BY Id ASC";

            SqlDataAdapter adpt = new SqlDataAdapter(bank_det, con1);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            bankdet.DataSource = dt;
            bankdet.DataBind();
            bankdet.DataTextField = "Bank_Name";
            bankdet.DataValueField = "Bank_Code";
            bankdet.DataBind();
            bankdet.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void bind()
    {
        con1.Open();
        DataTable ddicno = new DataTable();
        if (TextBox1.Text != "")
        {

            ddicno = DBCon.Ora_Execute_table("select mem_new_icno from mem_member where mem_new_icno='" + TextBox1.Text + "' and Acc_sts ='Y'");
            if (ddicno.Rows.Count > 0)
            {
                str = "select m.mem_new_icno,m.mem_name,m.mem_branch_cd,m.mem_crt_dt,m.mem_phone_h,m.mem_phone_o,m.mem_phone_m,r.rak_bank_acc_no,r.rak_bank_cd,r.rak_form_submit_dt,r.rak_appl_sts_cd from mem_member AS m left join mem_bank_rakyat AS r ON r.rak_new_icno=m.mem_new_icno where m.mem_new_icno='" + TextBox1.Text.Trim() + "' and m.Acc_sts ='Y'";
                //str = "select rak_bank_acc_no,rak_bank_cd,rak_form_submit_dt,rak_appl_sts_cd from mem_bank_rakyat where rak_new_icno='" + TextBox1.Text.Trim() + "'";
                com = new SqlCommand(str, con1);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    TextBox1.Text = reader["mem_new_icno"].ToString();
                    TextBox2.Text = reader["mem_name"].ToString();
                    DropDownList3.SelectedValue = reader["mem_branch_cd"].ToString();
                    textdate1.Text = Convert.ToDateTime(reader["mem_crt_dt"]).ToString("dd/MM/yyyy");
                    if (reader["rak_form_submit_dt"].ToString() != "")
                    {
                        txtdate.Text = Convert.ToDateTime(reader["rak_form_submit_dt"]).ToString("dd/MM/yyyy");
                        ddlstspm.SelectedValue = reader["rak_appl_sts_cd"].ToString();
                    }


                    if (reader["mem_phone_h"].ToString() == "")
                    {
                        TextBox5.Text = reader["mem_phone_o"].ToString();
                        if (reader["mem_phone_o"].ToString() == "")
                        {
                            TextBox5.Text = reader["mem_phone_m"].ToString();
                        }
                    }
                    else
                    {
                        TextBox5.Text = reader["mem_phone_h"].ToString();
                    }

                    TextBox22.Text = reader["rak_bank_acc_no"].ToString();
                    if (reader["rak_bank_cd"].ToString() != "")
                    {
                        bankdet.SelectedValue = reader["rak_bank_cd"].ToString();
                    }
                    else
                    {
                        bankdet.SelectedValue = "BKRMMYKL";
                    }

                    
                    reader.Close();
                    con1.Close();
                }
            }
          

            else if (ddicno.Rows.Count == 0)
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }
        }
        else
        {
            bankdet.SelectedValue = "BKRMMYKL";            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No KP Baru.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void btnsrch_Click(object sender, EventArgs e)
    {
        bankdetails();
        bind();
      

    }

    private void dropdownName()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string com = "select discription,code from ref_appl_bank_sts ORDER BY id ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlstspm.DataSource = dt;
            ddlstspm.DataBind();
            ddlstspm.DataTextField = "discription";
            ddlstspm.DataValueField = "code";
            ddlstspm.DataBind();
            ddlstspm.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

   

    protected void Buttton_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "" && TextBox22.Text != "" && txtdate.Text != "" && ddlstspm.SelectedValue != "")
        {

            DateTime dmula;
            DateTime today = DateTime.Now;
            dmula = today;


            string Kp_baru = TextBox1.Text;
            string Name = TextBox2.Text;
            string Telefon_no = TextBox5.Text;
            string Update_date = textdate1.Text;
            string Account_number = TextBox22.Text;
            string Cawangan = DropDownList3.Text;
            string Bank_name = bankdet.SelectedValue;

            //string dmula = txtdate.Text;

            string Application_status = ddlstspm.Text;            


            // dt = GetData1(btsnm, apsts, upid, updt, icno, adid, addt, adcd, addes);

            dt = GetData(Kp_baru, Name, Telefon_no, Update_date, Account_number, Cawangan, Bank_name, Application_status);

            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select rak_new_icno from mem_bank_rakyat where rak_new_icno='" + Kp_baru + "' and Acc_sts ='Y'");
            if (ddicno.Rows.Count > 0)
            {
                service.audit_trail("P0135", "Kemaskini","No KP Baru", TextBox1.Text);
                Session["validate_success"] = "SUCCESS";
                Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                Response.Redirect("../keanggotan/Kmk_anggota_view.aspx");
            }
            else
            {
                service.audit_trail("P0135", "Simpan", "No KP Baru", TextBox1.Text);
                Session["validate_success"] = "SUCCESS";
                Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                Response.Redirect("../keanggotan/Kmk_anggota_view.aspx");
            }
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Harus Mandatory.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    private DataTable GetData(string Kp_baru, string Name, string Telefon_no, string Update_date, string Account_number, string Cawangan, string Bank_name, string Application_status)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ToString();
        using (SqlConnection cn = new SqlConnection(constr))
        {
            string fmdate = txtdate.Text;
            DateTime ft = DateTime.ParseExact(fmdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datedari = ft.ToString("yyyy-mm-dd");
            
            SqlCommand cmd = new SqlCommand("mem_bank_rakyat_Procedure", cn);
            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@rak_bank_acc_no", SqlDbType.VarChar).Value = Account_number;
            cmd.Parameters.Add("@rak_bank_cd", SqlDbType.VarChar).Value = Bank_name;
            cmd.Parameters.Add("@rak_form_submit_dt", SqlDbType.VarChar).Value = datedari;
            cmd.Parameters.Add("@rak_appl_sts_cd", SqlDbType.VarChar).Value = Application_status;
            cmd.Parameters.Add("@ref_upd_id", SqlDbType.VarChar).Value = Session["New"].ToString();
            cmd.Parameters.Add("@ref_upd_dt", SqlDbType.DateTime).Value =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            cmd.Parameters.Add("@rak_new_icno", SqlDbType.VarChar).Value = Kp_baru;
            cmd.Parameters.Add("@ref_crt_id", SqlDbType.VarChar).Value = Session["New"].ToString();
            cmd.Parameters.Add("@ref_crt_dt", SqlDbType.DateTime).Value =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
        }
        return dt;
    }


    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }


    protected void TextBox2_TextChanged1(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(TextBox2.Text) ||
        TextBox2.Text.Any(c => Char.IsNumber(c)))
        {
            //Label6.Text = "Please Remove the Number form Name Field";
            TextBox2.Focus();
        }

    }


    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../keanggotan/Kmk_anggota_view.aspx");
    }


    public object apsts { get; set; }

    public object btsnm { get; set; }
}
