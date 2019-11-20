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
using System.Text.RegularExpressions;

public partial class Daft : System.Web.UI.Page
{
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable wilayah = new DataTable();
    DataTable caw = new DataTable();
    DataTable pusat = new DataTable();
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string status;
    DateTime dmula;
    string bcode;
    string wcode;
    string sno;
    string ccode;
    string userid;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        
       
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            app_language();
            if (Session["New"] != null)
            {

                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success'});", true);

                }
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";

                //if (role_view == "1")
                //{
                //    Button1.Visible = true;
                //}
                //else
                //{
                //    Button1.Visible = false;
                //}

              

                userid = Session["New"].ToString();                
                category();
                Bangsa();
                Banknama();
                Paytype();
                Hun();
                Mstatus();
                gender();
                kawasan();
                wilahBind();
                branch();
                negriBind();
                negriBind1();
                negriBind2();
                jobnegriBind();
                occtype();
                txtamt1.Text = "30.00";
                TextBox3.Text = Session["New"].ToString();
                TextBox4.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Txtcat.Value = "PERMOHONAN KEANGGOTAAN PEMOHON DIDAFTARKAN PADA" + " " + DateTime.Now.ToString("dd/MM/yyyy");

                var samp = Request.Url.Query;
                if (samp != "")
                {
                    TXTNOKP.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch_Click();
                    Button1.Visible = false;
                    Button2.Visible = false;
                }

                //TxtAlaPek.Value = "Koperasi Amanah Ikhtiar Malaysia Berhad \nLot 2-3, 2-3A, 2-5 & 1-5, Jalan Cempaka SD12/1A, \nBandar Sri Damansara PJU9, \n52200 Kuala Lumpur ";
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        if (TextBox4.Text != "")
        {
            Txtcat.Value = "PERMOHONAN KEANGGOTAAN PEMOHON DIDAFTARKAN PADA" + " " + TextBox4.Text;
        }

    }

    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0120' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();
                if (role_view == "1")
                {
                    Button1.Visible = true;
                }
                else
                {
                    Button1.Visible = false;
                }
                if (role_add == "1")
                {
                    Button7.Visible = true;
                }
                else
                {
                    Button7.Visible = false;
                }

                //if (role_edit == "1")
                //{
                //    Button3.Visible = true;
                //}
                //else
                //{
                //    Button3.Visible = false;
                //}
            }
        }
    }
    void app_language()
    {
        if (Session["New"] != null)
        {
            assgn_roles();
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1031','1032','13','14','15','1033','16','17','877','41','42','43','18','44','19','45','46','22','1034','24','25','27','28','29','26','914','915','1035','1036', '48', '49', '50', '51', '1037', '52', '53', '54', '55', '56', '57', '1038', '1039', '1040', '60', '1009', '1022', '37', '1024', '1041', '34', '1012', '1015', '1017', '1020', '1023', '1043', '1006', '1025', '1026', '1027', '1028', '1029', '61', '1042', '883')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;


            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[53][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[54][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[51][0].ToString().ToLower());
            //Label1.Text = txtinfo.ToTitleCase(gt_lng.Rows[45][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[30][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[52][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[26][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl15.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl16.Text = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower());
            ps_lbl17.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl18.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl19.Text = txtinfo.ToTitleCase(gt_lng.Rows[59][0].ToString().ToLower());
            ps_lbl20.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl21.Text = txtinfo.ToTitleCase(gt_lng.Rows[25][0].ToString().ToLower());
            ps_lbl22.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl23.Text = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower());
            ps_lbl24.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl25.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl26.Text = txtinfo.ToTitleCase(gt_lng.Rows[36][0].ToString().ToLower());
            ps_lbl27.Text = txtinfo.ToTitleCase(gt_lng.Rows[55][0].ToString().ToLower());
            ps_lbl28.Text = txtinfo.ToTitleCase(gt_lng.Rows[46][0].ToString().ToLower());
            ps_lbl29.Text = txtinfo.ToTitleCase(gt_lng.Rows[63][0].ToString().ToLower());
            ps_lbl30.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl31.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
            ps_lbl32.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());
            ps_lbl33.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl34.Text = txtinfo.ToTitleCase(gt_lng.Rows[60][0].ToString().ToLower());
            ps_lbl35.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl36.Text = txtinfo.ToTitleCase(gt_lng.Rows[27][0].ToString().ToLower());
            ps_lbl37.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl38.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            ps_lbl39.Text = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
            ps_lbl40.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            Button6.Text = txtinfo.ToTitleCase(gt_lng.Rows[47][0].ToString().ToLower());
            ps_lbl42.Text = txtinfo.ToTitleCase(gt_lng.Rows[61][0].ToString().ToLower());
            ps_lbl43.Text = txtinfo.ToTitleCase(gt_lng.Rows[56][0].ToString().ToLower());
            ps_lbl44.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
            ps_lbl45.Text = txtinfo.ToTitleCase(gt_lng.Rows[37][0].ToString().ToLower());
            ps_lbl46.Text = txtinfo.ToTitleCase(gt_lng.Rows[62][0].ToString().ToLower());
            ps_lbl47.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            //Label2.Text = txtinfo.ToTitleCase(gt_lng.Rows[48][0].ToString().ToLower());
            ps_lbl49.Text = txtinfo.ToTitleCase(gt_lng.Rows[34][0].ToString().ToLower());
            ps_lbl50.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl51.Text = txtinfo.ToTitleCase(gt_lng.Rows[57][0].ToString().ToLower());
            ps_lbl52.Text = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower());
            ps_lbl53.Text = txtinfo.ToTitleCase(gt_lng.Rows[39][0].ToString().ToLower());
            ps_lbl54.Text = txtinfo.ToTitleCase(gt_lng.Rows[40][0].ToString().ToLower());
            ps_lbl55.Text = txtinfo.ToTitleCase(gt_lng.Rows[51][0].ToString().ToLower());
            //Label3.Text = txtinfo.ToTitleCase(gt_lng.Rows[64][0].ToString().ToLower());
            ps_lbl57.Text = txtinfo.ToTitleCase(gt_lng.Rows[50][0].ToString().ToLower());
            ps_lbl58.Text = txtinfo.ToTitleCase(gt_lng.Rows[41][0].ToString().ToLower());
            ps_lbl59.Text = txtinfo.ToTitleCase(gt_lng.Rows[58][0].ToString().ToLower());
            ps_lbl60.Text = txtinfo.ToTitleCase(gt_lng.Rows[42][0].ToString().ToLower());
            ps_lbl61.Text = txtinfo.ToTitleCase(gt_lng.Rows[43][0].ToString().ToLower());
            ps_lbl62.Text = txtinfo.ToTitleCase(gt_lng.Rows[44][0].ToString().ToLower());
            Button7.Text = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[49][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[35][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }

    }
    void kawasan()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select kavasan_name from Ref_Cawangan  group by kavasan_name order by kavasan_name asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddkaw.DataSource = dt;
            ddkaw.DataBind();
            ddkaw.DataTextField = "kavasan_name";

            ddkaw.DataBind();
            ddkaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void kawasan1()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select kavasan_name,kawasan_code from Ref_Cawangan where  cawangan_name='" + ddcaw.SelectedItem.Text + "' and wilayah_name='" + ddwil.SelectedItem.Text + "'   group by kavasan_name,kawasan_code order by kavasan_name asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddkaw.DataSource = dt;
            ddkaw.DataBind();
            ddkaw.DataTextField = "kavasan_name";
            ddkaw.DataValueField = "kawasan_code";
            ddkaw.DataBind();
            //ddkaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void wilahBind()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select Cawangan_Name,Cawangan_Code from Ref_Cawangan  group by Cawangan_Name,Cawangan_Code order by Cawangan_Name,Cawangan_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddcaw.DataSource = dt;
            ddcaw.DataBind();
            ddcaw.DataTextField = "Cawangan_Name";
            ddcaw.DataValueField = "Cawangan_Code";
            ddcaw.DataBind();
            ddcaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void branch()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select Wilayah_Name,Wilayah_Code from Ref_Wilayah  group by Wilayah_Name,Wilayah_Code order by Wilayah_Name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddwil.DataSource = dt;
            ddwil.DataBind();
            ddwil.DataTextField = "Wilayah_Name";
            ddwil.DataValueField = "Wilayah_Code";
            ddwil.DataBind();
            ddwil.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void negriBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(conString);
            string com = "select Decription,Decription_Code from Ref_Negeri  group by Decription,Decription_Code order by Decription,Decription_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlnegri.DataSource = dt;
            ddlnegri.DataBind();
            ddlnegri.DataTextField = "Decription";
            ddlnegri.DataValueField = "Decription_Code";
            ddlnegri.DataBind();
            ddlnegri.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void negriBind1()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(conString);
            string com = "select Decription,Decription_Code from Ref_Negeri  group by Decription,Decription_Code order by Decription,Decription_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlnegri1.DataSource = dt;
            ddlnegri1.DataBind();
            ddlnegri1.DataTextField = "Decription";
            ddlnegri1.DataValueField = "Decription_Code";
            ddlnegri1.DataBind();
            ddlnegri1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void negriBind2()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(conString);
            string com = "select Decription,Decription_Code from Ref_Negeri  group by Decription,Decription_Code order by Decription,Decription_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlnegri2.DataSource = dt;
            ddlnegri2.DataBind();
            ddlnegri2.DataTextField = "Decription";
            ddlnegri2.DataValueField = "Decription_Code";
            ddlnegri2.DataBind();
            ddlnegri2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void jobnegriBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(conString);
            string com = "select Decription,Decription_Code from Ref_Negeri  group by Decription,Decription_Code order by Decription,Decription_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlpeke.DataSource = dt;
            ddlpeke.DataBind();
            ddlpeke.DataTextField = "Decription";
            ddlpeke.DataValueField = "Decription_Code";
            ddlpeke.DataBind();
            ddlpeke.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void bBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(conString);
            string com = "select kavasan_name,kawasan_code from Ref_Cawangan where cawangan_code='" + bcode + "'  ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddkaw.DataSource = dt;
            ddkaw.DataBind();
            ddkaw.DataTextField = "kavasan_name";
            ddkaw.DataValueField = "kawasan_code";
            ddkaw.DataBind();
            ddkaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void gender()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select gender_cd,gender_desc from  ref_gender order by gender_desc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddJant.DataSource = dt;
            ddJant.DataBind();
            ddJant.DataTextField = "gender_desc";
            ddJant.DataValueField = "gender_cd";
            ddJant.DataBind();
            ddJant.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void category()
    {
        SqlConnection con = new SqlConnection(conString);
        string com = "select Applicant_Name,Applicant_Code from Ref_Applicant_Category order by Applicant_Name";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        ddcat.DataSource = dt;
        ddcat.DataBind();
        ddcat.DataTextField = "Applicant_Name";
        ddcat.DataValueField = "Applicant_Code";
        ddcat.DataBind();
        ddcat.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }
    void Bangsa()
    {
        SqlConnection con = new SqlConnection(conString);
        string com = "select Bangsa_Name,Bangsa_Code from Ref_Bangsa order by Bangsa_Code";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        ddBangsa.DataSource = dt;
        ddBangsa.DataBind();
        ddBangsa.DataTextField = "Bangsa_Name";
        ddBangsa.DataValueField = "Bangsa_Code";
        ddBangsa.DataBind();
        ddBangsa.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    }
    void Banknama()
    {
        SqlConnection con = new SqlConnection(conString);
        string com = "select Bank_Name,Bank_Code from Ref_Nama_Bank order by Bank_Name";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        ddbank.DataSource = dt;
        ddbank.DataBind();
        ddbank.DataTextField = "Bank_Name";
        ddbank.DataValueField = "Bank_Code";
        ddbank.DataBind();
        ddbank.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }
    void Mstatus()
    {
        SqlConnection con = new SqlConnection(conString);
        string com = "select Marital_Status,Marital_Code from status_perkahwinan order by Marital_Status";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        ddstt.DataSource = dt;
        ddstt.DataBind();
        ddstt.DataTextField = "Marital_Status";
        ddstt.DataValueField = "Marital_Code";
        ddstt.DataBind();
        ddstt.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }
    void Paytype()
    {
        SqlConnection con = new SqlConnection(conString);
        string com = "select KETERANGAN,KETERANGAN_Code from Ref_Jenis_Bayaran WHERE Status='A'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        ddpay.DataSource = dt;
        ddpay.DataBind();
        ddpay.DataTextField = "KETERANGAN";
        ddpay.DataValueField = "KETERANGAN_Code";
        ddpay.DataBind();
        ddpay.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }
    void occtype()
    {
        SqlConnection con = new SqlConnection(conString);
        string com = "select occ_cd,occ_name from REF_OCCUPATION where status='A'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        ddlTxtPek.DataSource = dt;
        ddlTxtPek.DataBind();
        ddlTxtPek.DataTextField = "occ_name";
        ddlTxtPek.DataValueField = "occ_cd";
        ddlTxtPek.DataBind();
        ddlTxtPek.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }
    void Hun()
    {
        SqlConnection con = new SqlConnection(conString);
        string com = "select  Contact_Name,Contact_Code from Ref_Hubungan order by Contact_Code";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        ddhun.DataSource = dt;
        ddhun.DataBind();
        ddhun.DataTextField = "Contact_Name";
        ddhun.DataValueField = "Contact_Code";
        ddhun.DataBind();
        ddhun.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        ddhun1.DataSource = dt;
        ddhun1.DataBind();
        ddhun1.DataTextField = "Contact_Name";
        ddhun1.DataValueField = "Contact_Code";
        ddhun1.DataBind();
        ddhun1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }
    protected void Rdbwar_CheckedChanged(object sender, EventArgs e)
    {
        if (Rdbwar.Checked == true)
        {
            RdbBwar.Checked = false;
            RdbPT.Checked = false;
        }
        srch_Click();
    }
    protected void RdbBwar_CheckedChanged(object sender, EventArgs e)
    {
        if (RdbBwar.Checked == true)
        {
            Rdbwar.Checked = false;
            RdbPT.Checked = false;
        }
        srch_Click();
    }
    protected void RdbPT_CheckedChanged(object sender, EventArgs e)
    {
        if (RdbPT.Checked == true)
        {
            RdbBwar.Checked = false;
            Rdbwar.Checked = false;
        }
        srch_Click();
    }
    protected void ddcaw_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    void reset()
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void CustomValidator1_ServerValidate(object sender, ServerValidateEventArgs e)
    {
        DateTime d;
        e.IsValid = DateTime.TryParseExact(e.Value, "yyyy-mm-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (TXTNOKP.Text != "" && Txtnama.Text != "" && ddcat.SelectedValue != "" && TxtTaradu.Text != "" && ddkaw.SelectedValue != "" && ddwil.SelectedValue != "" && ddcaw.SelectedValue != "")
        {
            if (ddpay.SelectedItem.Text != "--- PILIH ---")
            {
                DataTable chk_rows = new DataTable();
                chk_rows = DBCon.Ora_Execute_table("select count(*) as cnt from mem_member where mem_new_icno='" + TXTNOKP.Text + "'");
                if (double.Parse(chk_rows.Rows[0]["cnt"].ToString()) < 2)
                {
                    if (Int32.Parse(chk_rows.Rows[0]["cnt"].ToString()) != 0)
                    {
                        string Inssql = "Update mem_member SET Acc_sts='N' WHERE mem_new_icno='" + TXTNOKP.Text + "' and Acc_sts='Y'";
                        status = DBCon.Ora_Execute_CommamdText(Inssql);
                        if (status == "SUCCESS")
                        {
                            // IN Active Details
                            DBCon.Execute_CommamdText("Update mem_share SET Acc_sts='N' WHERE sha_new_icno='" + TXTNOKP.Text + "' and Acc_sts='Y'");
                            DBCon.Execute_CommamdText("Update mem_fee SET Acc_sts='N' WHERE fee_new_icno='" + TXTNOKP.Text + "' and Acc_sts='Y'");
                            DBCon.Execute_CommamdText("Update mem_divident SET Acc_sts='N' WHERE div_new_icno='" + TXTNOKP.Text + "' and Acc_sts='Y'");
                            DBCon.Execute_CommamdText("Update mem_settlement SET Acc_sts='N' WHERE set_new_icno='" + TXTNOKP.Text + "' and Acc_sts='Y'");
                            DBCon.Execute_CommamdText("Update mem_wasi SET Acc_sts='N' WHERE was_new_icno='" + TXTNOKP.Text + "' and Acc_sts='Y'");
                            DBCon.Execute_CommamdText("Update mem_complaint SET Acc_sts='N' WHERE com_new_icno='" + TXTNOKP.Text + "' and Acc_sts='Y'");
                            DBCon.Execute_CommamdText("Update mem_bank_rakyat SET Acc_sts='N' WHERE rak_new_icno='" + TXTNOKP.Text + "' and Acc_sts='Y'");
                        }
                    }
                    DataTable chk_rows_pst = new DataTable();
                    chk_rows_pst = DBCon.Ora_Execute_table("select top(1)* from aim_pst where pst_new_icno='" + TXTNOKP.Text + "' and Acc_sts='Y' and ISNULL(pst_txn_ind,'') ='1' order by pst_crt_dt");
                    if (chk_rows_pst.Rows.Count != 0)
                    {
                        DBCon.Execute_CommamdText("Update aim_pst SET Acc_sts='N' WHERE pst_new_icno='" + TXTNOKP.Text + "' and ISNULL(pst_txn_ind,'') ='1' and ID = '" + chk_rows_pst.Rows[0]["ID"].ToString() + "'");
                    }


                    if (Rdbwar.Checked == true)
                    {
                        status = "W";
                    }
                    else if (RdbBwar.Checked == true)
                    {
                        status = "N";
                    }
                    else
                    {
                        status = "P";
                    }

                    string datedari = TxtTaradu.Text;

                    DateTime dt = DateTime.ParseExact(datedari, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    String datetime = dt.ToString("yyyy-mm-dd");
                    //Convert.ToDateTime((TxtTaradu.Text), "yyyy-MM-dd");

                    string userid = Session["New"].ToString();


                    string txn_ind = "B";
                    string shaitem = "ANGGOTA BAHARU";


                    DataTable dtarea = new DataTable();
                    dtarea = DBCon.Ora_Execute_table("select kawasan_code from Ref_Cawangan where kavasan_name='" + ddkaw.SelectedItem.Text + "' and cawangan_name='" + ddcaw.SelectedItem.Text + "' and wilayah_name='" + ddwil.SelectedItem.Text + "'");

                    SqlConnection con = new SqlConnection(conString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable chek = new DataTable();
                    chek = DBCon.Ora_Execute_table("select mem_new_icno from mem_member where mem_new_icno='" + TXTNOKP.Text + "' and Acc_sts ='Y'");
                    if (chek.Rows.Count > 0)
                    {
                        cmd.CommandText = "Anggota_update";
                    }
                    else
                    {
                        cmd.CommandText = "Anggota";
                    }
                    cmd.Parameters.Add("@nokp", SqlDbType.VarChar).Value = TXTNOKP.Text.Trim();
                    cmd.Parameters.Add("@sahabatno", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@memno", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Txtnama.Text.Trim().ToUpper();
                    cmd.Parameters.Add("@Statwar", SqlDbType.VarChar).Value = status;
                    cmd.Parameters.Add("@appcode", SqlDbType.VarChar).Value = ddcat.SelectedItem.Value;
                    cmd.Parameters.Add("@oldnokp", SqlDbType.VarChar).Value = Txtnokplama.Text.Trim();
                    cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = ddJant.SelectedItem.Value;
                    cmd.Parameters.Add("@race", SqlDbType.VarChar).Value = ddBangsa.SelectedItem.Value;
                    cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = Txtage.Text.Trim();
                    cmd.Parameters.Add("@dob", SqlDbType.Date).Value = datetime;
                    cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = ddcaw.SelectedItem.Value;
                    cmd.Parameters.Add("@area", SqlDbType.VarChar).Value = dtarea.Rows[0][0].ToString();
                    cmd.Parameters.Add("@wilayah", SqlDbType.VarChar).Value = ddwil.SelectedItem.Value;
                    cmd.Parameters.Add("@pusat", SqlDbType.VarChar).Value = TxtPus.Text.Trim().ToUpper();
                    //string adds = txtAla.Value.Replace("\r\n", "<br />");
                    cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = txtAla.Value.Trim().Replace(",", "").ToUpper();
                    cmd.Parameters.Add("@phone_h", SqlDbType.VarChar).Value = Txtnotel_R.Text.Trim();
                    cmd.Parameters.Add("@phone_o", SqlDbType.VarChar).Value = Txtnotel_P.Text.Trim();
                    cmd.Parameters.Add("@phone_m", SqlDbType.VarChar).Value = Txtnotel_B.Text.Trim();
                    cmd.Parameters.Add("@job", SqlDbType.VarChar).Value = ddlTxtPek.SelectedItem.Value;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = Txtemail.Text.Trim();
                    //string adds1 = TxtAlaPek.Value.Replace("\r\n", "<br />");
                    cmd.Parameters.Add("@jobaddress", SqlDbType.VarChar).Value = TxtAlaPek.Value.Trim().Replace(",", "").ToUpper();


                    cmd.Parameters.Add("@jobpostcd", SqlDbType.VarChar).Value = txtpeke.Text.Trim();
                    cmd.Parameters.Add("@jobnegri", SqlDbType.VarChar).Value = ddlpeke.SelectedItem.Value;
                    cmd.Parameters.Add("@martial_status", SqlDbType.VarChar).Value = ddstt.SelectedItem.Value;
                    cmd.Parameters.Add("@bankaccno", SqlDbType.VarChar).Value = Txtnobank.Text.Trim();
                    cmd.Parameters.Add("@bankname", SqlDbType.VarChar).Value = ddbank.SelectedItem.Value;
                    cmd.Parameters.Add("@catatan", SqlDbType.VarChar).Value = Txtcat.Value.Trim();
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = TextBox3.Text;

                    DateTime dt12 = DateTime.ParseExact(TextBox4.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string datetime1 = dt12.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("hh:mm:ss");
                    string datetime2 = dt12.ToString("yyyy-MM-dd");
                    cmd.Parameters.Add("@cdate1", SqlDbType.DateTime).Value = datetime1;
                    cmd.Parameters.Add("@cdate2", SqlDbType.DateTime).Value = datetime2;
                    cmd.Parameters.Add("@cdate", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    cmd.Parameters.Add("@Acc_sts", SqlDbType.VarChar).Value = "Y";
                    //cmd.Parameters.Add("@feenokp", SqlDbType.VarChar).Value = TXTNOKP.Text.Trim();
                    //cmd.Parameters.Add("@feecdate", SqlDbType.VarChar).Value =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    if (ddpay.SelectedItem.Value == "C")
                    {
                        cmd.Parameters.Add("@feepaytype", SqlDbType.VarChar).Value = "C";
                        cmd.Parameters.Add("@shaind", SqlDbType.VarChar).Value = "C";
                        cmd.Parameters.Add("@pstind", SqlDbType.VarChar).Value = "";

                    }
                    else if (ddpay.SelectedItem.Value == "P")
                    {
                        cmd.Parameters.Add("@feepaytype", SqlDbType.VarChar).Value = "P";
                        cmd.Parameters.Add("@shaind", SqlDbType.VarChar).Value = "P";
                        cmd.Parameters.Add("@pstind", SqlDbType.VarChar).Value = "1";

                    }



                    DataTable dd = new DataTable();
                    dd = DBCon.Ora_Execute_table("select pst_new_icno from aim_pst where pst_new_icno='" + TXTNOKP.Text + "' and ISNULL(pst_txn_ind,'') !='1' and Acc_sts ='Y' ");
                    //if (dd.Rows.Count == 1)
                    //{
                    //    cmd.Parameters.Add("@feepaytype", SqlDbType.VarChar).Value = "P";
                    //}
                    cmd.Parameters.Add("@feeamount", SqlDbType.VarChar).Value = txtamt1.Text.Trim();
                    cmd.Parameters.Add("@fee_pay_remark", SqlDbType.VarChar).Value = TextBox2.Text.Trim();

                    //cmd.Parameters.Add("@shanokp", SqlDbType.VarChar).Value = TXTNOKP.Text.Trim();
                    //cmd.Parameters.Add("@shacdate", SqlDbType.VarChar).Value =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters.Add("@shatxtind", SqlDbType.VarChar).Value = txn_ind;
                    cmd.Parameters.Add("@shadebamt", SqlDbType.VarChar).Value = Txtamt2.Text.Trim();
                    decimal a;
                    if (Txtamt2.Text != "")
                    {
                        a = Convert.ToDecimal(Txtamt2.Text);
                    }
                    else
                    {
                        a = Convert.ToDecimal("0.00");
                    }
                    int s1 = Convert.ToInt32(a);
                    if (s1 == 0.00 || Txtamt2.Text == "" || s1 < 100.00)
                    {
                        cmd.Parameters.Add("@memsts", SqlDbType.VarChar).Value = "FM";
                    }

                    else
                    {
                        cmd.Parameters.Add("@memsts", SqlDbType.VarChar).Value = "";
                    }

                    cmd.Parameters.Add("@shaitem", SqlDbType.VarChar).Value = shaitem;


                    //if (ddpay.SelectedItem.Value == "Q")
                    //{
                    //    cmd.Parameters.Add("@shaind", SqlDbType.VarChar).Value = "Q";
                    //}



                    //if (dd.Rows.Count == 1)
                    //{
                    //    cmd.Parameters.Add("@shaind", SqlDbType.VarChar).Value = "P";
                    //    cmd.Parameters.Add("@pstind", SqlDbType.VarChar).Value = "1";
                    //}


                    cmd.Parameters.Add("@postcd", SqlDbType.VarChar).Value = TextBox1.Text.Trim();
                    cmd.Parameters.Add("@negri", SqlDbType.VarChar).Value = ddlnegri.SelectedItem.Value;
                    cmd.Parameters.Add("@p1nokp", SqlDbType.VarChar).Value = TxtNoKPP1.Text.Trim();
                    //cmd.Parameters.Add("@p1newnokp", SqlDbType.VarChar).Value = TXTNOKP.Text.Trim();
                    cmd.Parameters.Add("@p1seqno", SqlDbType.VarChar).Value = "1";
                    cmd.Parameters.Add("@p1name", SqlDbType.VarChar).Value = TxtnamaP1.Text.Trim().ToUpper();
                    cmd.Parameters.Add("@p1relationcode", SqlDbType.VarChar).Value = ddhun.SelectedItem.Value;
                    cmd.Parameters.Add("@p1phno", SqlDbType.VarChar).Value = Txtnotelp1.Text.Trim();
                    //string adds2 = TxtAlamatp1.Value.Replace("\r\n", "<br />");
                    cmd.Parameters.Add("@p1address", SqlDbType.VarChar).Value = TxtAlamatp1.Value.Trim().Replace(",", "").ToUpper();
                    cmd.Parameters.Add("@postcd1", SqlDbType.VarChar).Value = txtpost1.Text.Trim();
                    cmd.Parameters.Add("@negri1", SqlDbType.VarChar).Value = ddlnegri1.SelectedItem.Value;
                    cmd.Parameters.Add("@p2nokp", SqlDbType.VarChar).Value = TxtNoKPP2.Text.Trim();
                    //cmd.Parameters.Add("@p2newnokp", SqlDbType.VarChar).Value = TXTNOKP.Text.Trim();
                    cmd.Parameters.Add("@p2seqno", SqlDbType.VarChar).Value = "2";
                    cmd.Parameters.Add("@P2name", SqlDbType.VarChar).Value = TxtnamaP2.Text.Trim().ToUpper();
                    cmd.Parameters.Add("@P2relationcode", SqlDbType.VarChar).Value = ddhun1.SelectedItem.Value;
                    cmd.Parameters.Add("@p2Phno", SqlDbType.VarChar).Value = Txttelnop2.Text.Trim();
                    //string adds3 = TxtAlamatp2.Value.Replace("\r\n", "<br />");
                    cmd.Parameters.Add("@p2address", SqlDbType.VarChar).Value = TxtAlamatp2.Value.Trim().Replace(",", "").ToUpper();
                    cmd.Parameters.Add("@postcd2", SqlDbType.VarChar).Value = txtpost2.Text.Trim();
                    cmd.Parameters.Add("@negri2", SqlDbType.VarChar).Value = ddlnegri2.SelectedItem.Value;
                    cmd.Connection = con;

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        service.audit_trail("P0120", "Simpan","No KP Baru",TXTNOKP.Text.Trim());
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan";
                        Response.Redirect("../keanggotan/Daft.aspx");
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'SUCCESS','title': 'SUCCESS','auto_close': 2000});", true);
                       
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    finally
                    {
                        con.Close();
                        con.Dispose();
                        //clear();
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Not Allowed More than Two Registrations.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Kaedah Bayaran.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Fill up the All Mandatory Fields.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        reset();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        reset();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        assgn_roles();
        srch_clk1();
    }

    void srch_clk1()
    {
        DataTable Dt = new DataTable();

        try
        {
            if (TXTNOKP.Text != "")
            {
                DataTable chk_rows_cnt = new DataTable();
                chk_rows_cnt = DBCon.Ora_Execute_table("select count(*) as cnt from mem_member where mem_new_icno='" + TXTNOKP.Text + "'");

                DataTable ddicno = new DataTable();
                DataTable ddicno1 = new DataTable();
                DataTable ddpst = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select mem_new_icno,mem_sts_cd from mem_member where mem_new_icno='" + TXTNOKP.Text + "' and Acc_sts='Y'");
                ddicno1 = DBCon.Ora_Execute_table("select stf_icno from hr_staff_profile where stf_icno='" + TXTNOKP.Text + "'");
                ddpst = DBCon.Ora_Execute_table("select pst_new_icno from aim_pst where pst_new_icno='" + TXTNOKP.Text + "' and ISNULL(pst_txn_ind,'') !='1' and Acc_sts='Y' and pst_withdrawal_type_cd IN ('WSYE')");
                if (ddicno.Rows.Count > 0)
                {

                    if (ddicno.Rows[0][1].ToString() == "SA")
                    {
                        Button7.Visible = false;
                        Button3.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Permohonan Telah Wujud');", true);
                    }
                    else if (ddicno.Rows[0][1].ToString() == "TS")
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Tidak Dijumpai');", true);

                        DataTable ddbind = new DataTable();
                        ddbind = DBCon.Ora_Execute_table("select * from mem_member  where mem_new_icno='" + ddicno.Rows[0][0].ToString() + "' and Acc_sts='Y' and mem_sts_cd='" + ddicno.Rows[0][1].ToString() + "'");

                        Txtnama.Text = ddbind.Rows[0]["mem_name"].ToString();
                        if (ddbind.Rows[0]["mem_nationality_ind"].ToString() == "W")
                        {
                            Rdbwar.Checked = true;
                        }

                        else if (ddbind.Rows[0]["mem_nationality_ind"].ToString() == "N")
                        {
                            RdbBwar.Checked = true;
                        }
                        else
                        {
                            RdbPT.Checked = true;
                        }

                        ddcat.SelectedValue = ddbind.Rows[0]["mem_applicant_type_cd"].ToString();
                        Txtnokplama.Text = ddbind.Rows[0]["mem_old_icno"].ToString();
                        ddJant.SelectedValue = ddbind.Rows[0]["mem_gender_cd"].ToString();
                        ddBangsa.SelectedValue = ddbind.Rows[0]["mem_race_cd"].ToString();
                        Txtage.Text = ddbind.Rows[0]["mem_age"].ToString();
                        TxtTaradu.Text = Convert.ToDateTime(ddbind.Rows[0]["mem_dob"]).ToString("dd/MM/yyyy");

                        string kwil = ddbind.Rows[0]["mem_region_cd"].ToString();
                        DataTable dw = new DataTable();
                        dw = DBCon.Ora_Execute_table("select Wilayah_Name,Wilayah_Code from Ref_Wilayah where Wilayah_Code='" + kwil + "' ");
                        if (dw.Rows.Count != 0)
                        {
                            ddwil.SelectedValue = dw.Rows[0][1].ToString();
                        }
                        else
                        {
                            ddwil.SelectedValue = "";
                        }
                        string kcaw = ddbind.Rows[0]["mem_branch_cd"].ToString();
                        DataTable dc = new DataTable();
                        dc = DBCon.Ora_Execute_table("select cawangan_name,cawangan_code from Ref_Cawangan where cawangan_code='" + kcaw + "'");
                        if (dc.Rows.Count != 0)
                        {
                            ddcaw.SelectedValue = dc.Rows[0][1].ToString();
                        }
                        else
                        {
                            ddcaw.SelectedValue = "";
                        }

                        string kkaw = ddbind.Rows[0]["mem_area_cd"].ToString();
                        DataTable dk = new DataTable();
                        dk = DBCon.Ora_Execute_table("select kavasan_name,kawasan_code from Ref_Cawangan where kawasan_code='" + kkaw + "'");
                        if (dk.Rows.Count != 0)
                        {
                            ddkaw.SelectedValue = dk.Rows[0][0].ToString();
                        }
                        else
                        {
                            kawasan();
                            DataTable dc_kaw = new DataTable();
                            dc_kaw = DBCon.Ora_Execute_table("select * from Ref_Kawasan where wilayah_cd='" + kwil + "' and cawangan_cd='" + kcaw + "'");
                            if (dc_kaw.Rows.Count != 0)
                            {
                                ddkaw.SelectedValue = dc_kaw.Rows[0]["Area_Name"].ToString();
                            }
                            else
                            {
                                ddkaw.SelectedValue = "";
                            }
                        }


                        if (ddbind.Rows[0]["mem_centre"].ToString() != "NULL")
                        {
                            TxtPus.Text = ddbind.Rows[0]["mem_centre"].ToString();
                        }
                        else
                        {
                            TxtPus.Text = "";
                        }
                        //txtAla.Value = ddbind.Rows[0]["mem_address"].ToString();
                        txtAla.Value = Regex.Replace(ddbind.Rows[0]["mem_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                        TextBox1.Text = ddbind.Rows[0]["mem_postcd"].ToString();
                        ddlnegri.SelectedValue = ddbind.Rows[0]["mem_negri"].ToString();
                        if (ddbind.Rows[0]["mem_phone_h"].ToString() != "NULL" && ddbind.Rows[0]["mem_phone_h"].ToString() != "")
                        {
                            Txtnotel_R.Text = ddbind.Rows[0]["mem_phone_h"].ToString();
                        }
                        else
                        {
                            Txtnotel_R.Text = "";
                        }
                        if (ddbind.Rows[0]["mem_phone_o"].ToString() != "NULL" && ddbind.Rows[0]["mem_phone_o"].ToString() != "")
                        {
                            Txtnotel_P.Text = ddbind.Rows[0]["mem_phone_o"].ToString();
                        }
                        else
                        {
                            Txtnotel_P.Text = "";
                        }
                        if (ddbind.Rows[0]["mem_phone_m"].ToString() != "NULL" && ddbind.Rows[0]["mem_phone_m"].ToString() != "")
                        {
                            Txtnotel_B.Text = ddbind.Rows[0]["mem_phone_m"].ToString();
                        }
                        else
                        {
                            Txtnotel_B.Text = "";
                        }
                        if (ddbind.Rows[0]["mem_job"].ToString() != "NULL" && ddbind.Rows[0]["mem_job"].ToString() != "")
                        {
                            ddlTxtPek.SelectedItem.Text = ddbind.Rows[0]["mem_job"].ToString();
                        }
                        else
                        {
                            ddlTxtPek.SelectedValue = "";
                        }
                        Txtemail.Text = ddbind.Rows[0]["mem_email"].ToString();
                        //TxtAlaPek.Value = ddbind.Rows[0]["mem_job_address"].ToString();
                        TxtAlaPek.Value = Regex.Replace(ddbind.Rows[0]["mem_job_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                        txtpeke.Text = ddbind.Rows[0]["mem_job_postcd"].ToString();
                        ddlpeke.SelectedValue = ddbind.Rows[0]["mem_job_negri"].ToString();
                        ddstt.SelectedValue = ddbind.Rows[0]["mem_marital_sts_cd"].ToString();
                        Txtnobank.Text = ddbind.Rows[0]["mem_bank_acc_no"].ToString();
                        ddbank.SelectedValue = ddbind.Rows[0]["mem_bank_cd"].ToString();
                        if (ddbind.Rows[0]["mem_remark"].ToString() != "")
                        {
                            Txtcat.Value = ddbind.Rows[0]["mem_remark"].ToString();
                        }
                        //txtamt1.Text = "50.00";
                        //TextBox3.Text = ddbind.Rows[0]["mem_crt_id"].ToString();
                        //TextBox4.Text = Convert.ToDateTime(ddbind.Rows[0]["mem_register_dt"]).ToString("dd/MM/yyyy");

                        DataTable ddbind_wasi = new DataTable();
                        ddbind_wasi = DBCon.Ora_Execute_table("select * from mem_wasi  where was_new_icno='" + ddicno.Rows[0][0].ToString() + "' and Acc_sts ='Y' order by was_seqno");
                        if (ddbind_wasi.Rows.Count != 0)
                        {
                            if (ddbind_wasi.Rows.Count > 0)
                            {
                                TxtNoKPP1.Text = ddbind_wasi.Rows[0]["was_icno"].ToString();
                                TxtnamaP1.Text = ddbind_wasi.Rows[0]["was_name"].ToString();
                                ddhun.SelectedValue = ddbind_wasi.Rows[0]["was_relation_cd"].ToString();
                                Txtnotelp1.Text = ddbind_wasi.Rows[0]["was_phone_no"].ToString();
                                TxtAlamatp1.Value = Regex.Replace(ddbind_wasi.Rows[0]["was_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                                txtpost1.Text = ddbind_wasi.Rows[0]["was_postcd"].ToString();
                                ddlnegri1.SelectedValue = ddbind_wasi.Rows[0]["was_negri"].ToString();
                            }
                            if (ddbind_wasi.Rows.Count > 1)
                            {
                                TxtNoKPP2.Text = ddbind_wasi.Rows[1]["was_icno"].ToString();
                                TxtnamaP2.Text = ddbind_wasi.Rows[1]["was_name"].ToString();
                                ddhun1.SelectedValue = ddbind_wasi.Rows[1]["was_relation_cd"].ToString();
                                Txttelnop2.Text = ddbind_wasi.Rows[1]["was_phone_no"].ToString();
                                TxtAlamatp2.Value = Regex.Replace(ddbind_wasi.Rows[1]["was_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                                txtpost2.Text = ddbind_wasi.Rows[1]["was_postcd"].ToString();
                                ddlnegri2.SelectedValue = ddbind_wasi.Rows[1]["was_negri"].ToString();
                            }
                        }

                        if (role_add == "1")
                        {
                            Button7.Visible = true;
                        }
                        else
                        {
                            Button7.Visible = false;
                        }

                        Button3.Visible = false;

                        SqlConnection con = new SqlConnection(conString);
                        con.Open();
                        string query1 = "select pst_region_name,pst_branch_name,pst_branch_cd,pst_centre_cd,pst_centre_name,pst_name,pst_bank_acc_no,SUM(pst_nett_amt) as pst_nett_amt from aim_pst where pst_new_icno='" + TXTNOKP.Text + "' and ISNULL(pst_txn_ind,'') !='1' and pst_withdrawal_type_cd IN ('WSYE') and Acc_sts ='Y' group by pst_region_name,pst_branch_name,pst_branch_cd,pst_centre_cd,pst_centre_name,pst_name,pst_bank_acc_no";
                        //DataTable ddw_pst = new DataTable();
                        //ddw_pst = DBCon.Ora_Execute_table("select sum(pst_nett_amt) pst_nett_amt from aim_pst where pst_new_icno='640414035728' and pst_withdrawal_type_cd IN ('WSYE') and Acc_sts ='Y'");
                        var sqlCommand1 = new SqlCommand(query1, con);
                        var sqlReader1 = sqlCommand1.ExecuteReader();
                        if (sqlReader1.Read() == true)
                        {
                            Txtnama.Text = (string)sqlReader1["pst_name"];

                            ddcaw.SelectedValue = (string)sqlReader1["pst_branch_cd"];
                            bcode = (string)sqlReader1["pst_branch_cd"];
                            DataTable ddw = new DataTable();
                            ddw = DBCon.Ora_Execute_table("select Wilayah_Name,Wilayah_Code from Ref_Cawangan where cawangan_code='" + bcode + "'  group by Wilayah_Name,Wilayah_Code order by Wilayah_Name");

                            ddwil.SelectedValue = ddw.Rows[0][1].ToString();
                            DataTable ddkawa = new DataTable();
                            ddkawa = DBCon.Ora_Execute_table("select kavasan_name,kawasan_code from Ref_Cawangan where  cawangan_name='" + ddcaw.SelectedItem.Text + "' and wilayah_name='" + ddwil.SelectedItem.Text + "'   group by kavasan_name,kawasan_code order by kavasan_name asc");
                            ddkaw.SelectedValue = ddkawa.Rows[0][0].ToString();
                            // kawasan1();
                            //negriBind();
                            if ((string)sqlReader1["pst_centre_name"] != "NULL")
                            {
                                TxtPus.Text = (string)sqlReader1["pst_centre_name"];
                            }
                            else
                            {
                                TxtPus.Text = "";
                            }
                            //Txtnobank.Text = (string)sqlReader1["pst_bank_acc_no"];
                            decimal a = (decimal)sqlReader1["pst_nett_amt"];
                            float s1 = (float)a;
                            //ddpay.SelectedItem.Text = "PST";
                            ddpay.SelectedValue = "P";
                            Button6.Visible = false;
                            ddpay.Attributes.Add("Readonly", "Readonly");
                            ddpay.Attributes.Add("style", "pointer-events:none;");
                            if (s1 >= 130.00)
                            {
                                float temp1 = float.Parse(txtamt1.Text);
                                float temp2 = s1 - temp1;
                                Txtamt2.Text = temp2.ToString("C").Replace("$", "");
                            }
                            else if (s1 < 130.00 && s1 > 30.00)
                            {
                                Txtamt2.Text = "0.00";
                            }
                            else if (s1 < 30.00)
                            {
                                Txtamt2.Text = "0.00";
                            }
                            sqlReader1.Close();
                        }
                        con.Close();

                    }
                    else if (ddicno.Rows[0][1].ToString() == "" || ddicno.Rows[0][1].ToString() == "FM")
                    {

                        Button7.Visible = false;
                        if (role_edit == "1")
                        {
                            Button3.Visible = true;
                        }
                        else
                        {
                            Button3.Visible = false;
                        }
                        //}
                        DataTable ddbind = new DataTable();
                        ddbind = DBCon.Ora_Execute_table("select * from mem_member  where mem_new_icno='" + ddicno.Rows[0][0].ToString() + "' and Acc_sts='Y' and mem_sts_cd='" + ddicno.Rows[0][1].ToString() + "'");

                        Txtnama.Text = ddbind.Rows[0]["mem_name"].ToString();
                        if (ddbind.Rows[0]["mem_nationality_ind"].ToString() == "W")
                        {
                            Rdbwar.Checked = true;
                        }

                        else if (ddbind.Rows[0]["mem_nationality_ind"].ToString() == "N")
                        {
                            RdbBwar.Checked = true;
                        }
                        else
                        {
                            RdbPT.Checked = true;
                        }

                        ddcat.SelectedValue = ddbind.Rows[0]["mem_applicant_type_cd"].ToString();
                        Txtnokplama.Text = ddbind.Rows[0]["mem_old_icno"].ToString();
                        ddJant.SelectedValue = ddbind.Rows[0]["mem_gender_cd"].ToString();
                        ddBangsa.SelectedValue = ddbind.Rows[0]["mem_race_cd"].ToString();
                        Txtage.Text = ddbind.Rows[0]["mem_age"].ToString();
                        TxtTaradu.Text = Convert.ToDateTime(ddbind.Rows[0]["mem_dob"]).ToString("dd/MM/yyyy");

                        string kwil = ddbind.Rows[0]["mem_region_cd"].ToString();
                        DataTable dw = new DataTable();
                        dw = DBCon.Ora_Execute_table("select Wilayah_Name,Wilayah_Code from Ref_Wilayah where Wilayah_Code='" + kwil + "' ");
                        if (dw.Rows.Count != 0)
                        {
                            ddwil.SelectedValue = dw.Rows[0][1].ToString();
                        }
                        else
                        {
                            ddwil.SelectedValue = "";
                        }
                        string kcaw = ddbind.Rows[0]["mem_branch_cd"].ToString();
                        DataTable dc = new DataTable();
                        dc = DBCon.Ora_Execute_table("select cawangan_name,cawangan_code from Ref_Cawangan where cawangan_code='" + kcaw + "'");
                        if (dc.Rows.Count != 0)
                        {
                            ddcaw.SelectedValue = dc.Rows[0][1].ToString();
                        }
                        else
                        {
                            ddcaw.SelectedValue = "";
                        }

                        string kkaw = ddbind.Rows[0]["mem_area_cd"].ToString();
                        DataTable dk = new DataTable();
                        dk = DBCon.Ora_Execute_table("select kavasan_name,kawasan_code from Ref_Cawangan where kawasan_code='" + kkaw + "'");
                        if (dk.Rows.Count != 0)
                        {
                            ddkaw.SelectedValue = dk.Rows[0][0].ToString();
                        }
                        else
                        {
                            kawasan();
                            DataTable dc_kaw = new DataTable();
                            dc_kaw = DBCon.Ora_Execute_table("select * from Ref_Kawasan where wilayah_cd='" + kwil + "' and cawangan_cd='" + kcaw + "'");
                            if (dc_kaw.Rows.Count != 0)
                            {
                                ddkaw.SelectedValue = dc_kaw.Rows[0]["Area_Name"].ToString();
                            }
                            else
                            {
                                ddkaw.SelectedValue = "";
                            }
                        }


                        if (ddbind.Rows[0]["mem_centre"].ToString() != "NULL")
                        {
                            TxtPus.Text = ddbind.Rows[0]["mem_centre"].ToString();
                        }
                        else
                        {
                            TxtPus.Text = "";
                        }
                        txtAla.Value = Regex.Replace(ddbind.Rows[0]["mem_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                        TextBox1.Text = ddbind.Rows[0]["mem_postcd"].ToString();
                        ddlnegri.SelectedValue = ddbind.Rows[0]["mem_negri"].ToString();
                        if (ddbind.Rows[0]["mem_phone_h"].ToString() != "NULL" && ddbind.Rows[0]["mem_phone_h"].ToString() != "")
                        {
                            Txtnotel_R.Text = ddbind.Rows[0]["mem_phone_h"].ToString();
                        }
                        else
                        {
                            Txtnotel_R.Text = "";
                        }
                        if (ddbind.Rows[0]["mem_phone_o"].ToString() != "NULL" && ddbind.Rows[0]["mem_phone_o"].ToString() != "")
                        {
                            Txtnotel_P.Text = ddbind.Rows[0]["mem_phone_o"].ToString();
                        }
                        else
                        {
                            Txtnotel_P.Text = "";
                        }
                        if (ddbind.Rows[0]["mem_phone_m"].ToString() != "NULL" && ddbind.Rows[0]["mem_phone_m"].ToString() != "")
                        {
                            Txtnotel_B.Text = ddbind.Rows[0]["mem_phone_m"].ToString();
                        }
                        else
                        {
                            Txtnotel_B.Text = "";
                        }
                        if (ddbind.Rows[0]["mem_job"].ToString() != "NULL" && ddbind.Rows[0]["mem_job"].ToString() != "")
                        {
                            ddlTxtPek.SelectedItem.Text = ddbind.Rows[0]["mem_job"].ToString();
                        }
                        else
                        {
                            ddlTxtPek.SelectedValue = "";
                        }
                        Txtemail.Text = ddbind.Rows[0]["mem_email"].ToString();
                        TxtAlaPek.Value = Regex.Replace(ddbind.Rows[0]["mem_job_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                        txtpeke.Text = ddbind.Rows[0]["mem_job_postcd"].ToString();
                        ddlpeke.SelectedValue = ddbind.Rows[0]["mem_job_negri"].ToString();
                        ddstt.SelectedValue = ddbind.Rows[0]["mem_marital_sts_cd"].ToString();
                        Txtnobank.Text = ddbind.Rows[0]["mem_bank_acc_no"].ToString();
                        ddbank.SelectedValue = ddbind.Rows[0]["mem_bank_cd"].ToString();
                        if (ddbind.Rows[0]["mem_remark"].ToString() != "")
                        {
                            Txtcat.Value = ddbind.Rows[0]["mem_remark"].ToString();
                        }
                        //txtamt1.Text = "50.00";
                        TextBox3.Text = ddbind.Rows[0]["mem_crt_id"].ToString();
                        TextBox4.Text = Convert.ToDateTime(ddbind.Rows[0]["mem_register_dt"]).ToString("dd/MM/yyyy");

                        DataTable ddtpst1 = new DataTable();
                        ddtpst1 = DBCon.Ora_Execute_table("select * from mem_share where sha_new_icno='" + TXTNOKP.Text + "' and Acc_sts ='Y' and sha_item='ANGGOTA BAHARU'");

                        DataTable ddtpst2 = new DataTable();
                        ddtpst2 = DBCon.Ora_Execute_table("select * from mem_fee where fee_new_icno='" + TXTNOKP.Text + "' and Acc_sts ='Y'");
                        if (ddtpst1.Rows.Count != 0)
                        {

                            ddpay.SelectedValue = ddtpst1.Rows[0]["sha_reference_ind"].ToString();
                            txtamt1.Text = double.Parse(ddtpst2.Rows[0]["fee_amount"].ToString()).ToString("C").Replace("$", "");
                            Txtamt2.Text = double.Parse(ddtpst1.Rows[0]["sha_debit_amt"].ToString()).ToString("C").Replace("$", "");
                            TextBox2.Text = ddtpst2.Rows[0]["fee_pay_remark"].ToString();
                            if (ddtpst1.Rows[0]["sha_reference_ind"].ToString() == "P")
                            {
                                ddpay.Attributes.Add("Style", "Pointer-events:None;");
                                Txtamt2.Attributes.Add("Readonly", "Readonly");
                                TextBox2.Attributes.Add("Readonly", "Readonly");
                            }
                            else
                            {
                                ddpay.Attributes.Remove("Style");
                                Txtamt2.Attributes.Remove("Readonly");
                                TextBox2.Attributes.Remove("Readonly");
                            }
                        }

                        // }

                        DataTable ddbind_wasi = new DataTable();
                        ddbind_wasi = DBCon.Ora_Execute_table("select * from mem_wasi  where was_new_icno='" + ddicno.Rows[0][0].ToString() + "' and Acc_sts ='Y' order by was_seqno");
                        if (ddbind_wasi.Rows.Count != 0)
                        {
                            if (ddbind_wasi.Rows.Count > 0)
                            {
                                TxtNoKPP1.Text = ddbind_wasi.Rows[0]["was_icno"].ToString();
                                TxtnamaP1.Text = ddbind_wasi.Rows[0]["was_name"].ToString();
                                ddhun.SelectedValue = ddbind_wasi.Rows[0]["was_relation_cd"].ToString();
                                Txtnotelp1.Text = ddbind_wasi.Rows[0]["was_phone_no"].ToString();
                                TxtAlamatp1.Value = Regex.Replace(ddbind_wasi.Rows[0]["was_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                                txtpost1.Text = ddbind_wasi.Rows[0]["was_postcd"].ToString();
                                ddlnegri1.SelectedValue = ddbind_wasi.Rows[0]["was_negri"].ToString();
                            }
                            if (ddbind_wasi.Rows.Count > 1)
                            {
                                TxtNoKPP2.Text = ddbind_wasi.Rows[1]["was_icno"].ToString();
                                TxtnamaP2.Text = ddbind_wasi.Rows[1]["was_name"].ToString();
                                ddhun1.SelectedValue = ddbind_wasi.Rows[1]["was_relation_cd"].ToString();
                                Txttelnop2.Text = ddbind_wasi.Rows[1]["was_phone_no"].ToString();
                                TxtAlamatp2.Value = Regex.Replace(ddbind_wasi.Rows[1]["was_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                                txtpost2.Text = ddbind_wasi.Rows[1]["was_postcd"].ToString();
                                ddlnegri2.SelectedValue = ddbind_wasi.Rows[1]["was_negri"].ToString();
                            }
                        }

                        SqlConnection con = new SqlConnection(conString);
                        con.Open();
                        string query1 = "select pst_region_name,pst_branch_name,pst_branch_cd,pst_centre_cd,pst_centre_name,pst_name,pst_bank_acc_no,SUM(pst_nett_amt) as pst_nett_amt from aim_pst where pst_new_icno='" + TXTNOKP.Text + "' and ISNULL(pst_txn_ind,'') !='1' and pst_withdrawal_type_cd IN ('WSYE') and Acc_sts ='Y' group by pst_region_name,pst_branch_name,pst_branch_cd,pst_centre_cd,pst_centre_name,pst_name,pst_bank_acc_no";
                        //DataTable ddw_pst = new DataTable();
                        //ddw_pst = DBCon.Ora_Execute_table("select sum(pst_nett_amt) pst_nett_amt from aim_pst where pst_new_icno='640414035728' and pst_withdrawal_type_cd IN ('WSYE') and Acc_sts ='Y'");
                        var sqlCommand1 = new SqlCommand(query1, con);
                        var sqlReader1 = sqlCommand1.ExecuteReader();
                        if (sqlReader1.Read() == true)
                        {

                            decimal a = (decimal)sqlReader1["pst_nett_amt"];
                            float s1 = (float)a;
                            //ddpay.SelectedItem.Text = "PST";
                            ddpay.SelectedValue = "P";
                            Button6.Visible = false;
                            ddpay.Attributes.Add("Readonly", "Readonly");
                            ddpay.Attributes.Add("style", "pointer-events:none;");
                            if (s1 >= 130.00)
                            {
                                float temp1 = float.Parse(txtamt1.Text);
                                float temp2 = s1 - temp1;
                                Txtamt2.Text = temp2.ToString("C").Replace("$", "").Replace("RM", "");
                            }
                            else if (s1 < 130.00 && s1 > 30.00)
                            {
                                Txtamt2.Text = "0.00";
                            }
                            else if (s1 < 30.00)
                            {
                                Txtamt2.Text = "0.00";
                            }
                            sqlReader1.Close();
                        }
                        con.Close();

                    }
                    else
                    {

                        //Button7.Enabled = false;
                        Button7.Visible = false;
                        Button3.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Permohonan Telah Wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }

                else if (ddicno1.Rows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    string query1 = "select hr.stf_name,hr.stf_age,isnull(hr.stf_dob,'') stf_dob,hr.stf_nationality_cd,hr.stf_sex_cd,hr.stf_permanent_address,hr.stf_phone_h,hr.stf_phone_m,hr.stf_marital_sts_cd,hr.stf_mailing_postcode,hr.stf_permanent_state_cd from hr_staff_profile as hr left join ref_gender as rg on rg.gender_cd=hr.stf_sex_cd left join  status_perkahwinan as sp on sp.Marital_Code=hr.stf_marital_sts_cd left join Ref_Negeri as ne on ne.Decription_Code=hr.stf_permanent_state_cd where stf_icno='" + TXTNOKP.Text + "'";
                    var sqlCommand1 = new SqlCommand(query1, con);
                    var sqlReader1 = sqlCommand1.ExecuteReader();
                    if (sqlReader1.Read() == true)
                    {
                        Txtnama.Text = sqlReader1["stf_name"].ToString();
                        Txtage.Text = sqlReader1["stf_age"].ToString();
                        TxtTaradu.Text = Convert.ToDateTime(sqlReader1["stf_dob"]).ToString("dd/MM/yyyy");
                        ddJant.SelectedValue = sqlReader1["stf_sex_cd"].ToString();
                        txtAla.Value = Regex.Replace(sqlReader1["stf_permanent_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                        TextBox1.Text = (string)sqlReader1["stf_mailing_postcode"];
                        ddlnegri.SelectedValue = (string)sqlReader1["stf_permanent_state_cd"];
                        Txtnotel_R.Text = (string)sqlReader1["stf_phone_h"];
                        Txtnotel_P.Text = (string)sqlReader1["stf_phone_m"];
                        System.Web.UI.WebControls.RadioButton rb = null;
                        if ((string)sqlReader1["stf_nationality_cd"].ToString() == "W")
                        {
                            Rdbwar.Checked = true;
                            //rb = Rdbwar;
                        }
                        else if ((string)sqlReader1["stf_nationality_cd"].ToString() == "N")
                        {
                            RdbBwar.Checked = true;
                            //rb = RdbBwar;
                        }
                        else if ((string)sqlReader1["stf_nationality_cd"].ToString() == "P")
                        {
                            RdbPT.Checked = true;
                            //rb = RdbPT;
                        }
                        //wilahBind();
                        kawasan();
                        bBind();
                        //negriBind();
                        sqlReader1.Close();
                    }
                    con.Close();
                }
                else if (ddpst.Rows.Count > 0)
                {

                    if (role_add == "1")
                    {
                        Button7.Visible = true;
                    }
                    else
                    {
                        Button7.Visible = false;
                    }
                    Button3.Visible = false;

                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    string query1 = "  select pst_region_name,pst_branch_name,pst_branch_cd,pst_centre_cd,pst_centre_name,pst_name,pst_bank_acc_no,SUM(pst_nett_amt) as pst_nett_amt from aim_pst where pst_new_icno='" + TXTNOKP.Text + "' and ISNULL(pst_txn_ind,'') !='1' and pst_withdrawal_type_cd IN ('WSYE') and Acc_sts ='Y' group by pst_region_name,pst_branch_name,pst_branch_cd,pst_centre_cd,pst_centre_name,pst_name,pst_bank_acc_no";
                    var sqlCommand1 = new SqlCommand(query1, con);
                    var sqlReader1 = sqlCommand1.ExecuteReader();
                    if (sqlReader1.Read() == true)
                    {
                        Txtnama.Text = (string)sqlReader1["pst_name"];

                        ddcaw.SelectedValue = (string)sqlReader1["pst_branch_cd"];
                        bcode = (string)sqlReader1["pst_branch_cd"];
                        DataTable ddw = new DataTable();
                        ddw = DBCon.Ora_Execute_table("select Wilayah_Name,Wilayah_Code from Ref_Cawangan where cawangan_code='" + bcode + "'  group by Wilayah_Name,Wilayah_Code order by Wilayah_Name");
                        ddwil.SelectedValue = ddw.Rows[0][1].ToString();
                        kawasan1();
                        negriBind();
                        if ((string)sqlReader1["pst_centre_name"] != "NULL")
                        {
                            TxtPus.Text = (string)sqlReader1["pst_centre_name"];
                        }
                        else
                        {
                            TxtPus.Text = "";
                        }
                        //Txtnobank.Text = (string)sqlReader1["pst_bank_acc_no"];
                        decimal a = (decimal)sqlReader1["pst_nett_amt"];
                        float s1 = (float)a;
                        //ddpay.SelectedItem.Text = "PST";
                        ddpay.SelectedValue = "P";
                        Button6.Visible = false;
                        ddpay.Attributes.Add("Readonly", "Readonly");
                        ddpay.Attributes.Add("style", "pointer-events:none;");
                        if (s1 >= 130.00)
                        {
                            float temp1 = float.Parse(txtamt1.Text);
                            float temp2 = s1 - temp1;
                            Txtamt2.Text = temp2.ToString("C").Replace("$", "");
                            Txtamt2.Text = Txtamt2.Text.Replace("RM", "");
                        }
                        else if (s1 < 130.00 && s1 > 30.00)
                        {
                            Txtamt2.Text = "0.00";
                        }
                        else if (s1 < 30.00)
                        {
                            Txtamt2.Text = "0.00";
                        }
                        sqlReader1.Close();
                    }
                    con.Close();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    if (role_add == "1")
                    {
                        Button7.Visible = true;
                    }
                    else
                    {
                        Button7.Visible = false;
                    }
                    Button3.Visible = false;
                    kawasan();
                    negriBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            //wilahBind();
            kawasan();
            negriBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    void srch_Click()
    {
        DataTable Dt = new DataTable();

        try
        {
            if (TXTNOKP.Text != "")
            {
                DataTable chk_rows_cnt = new DataTable();
                chk_rows_cnt = DBCon.Ora_Execute_table("select count(*) as cnt from mem_member where mem_new_icno='" + TXTNOKP.Text + "'");
                //if (chk_rows_cnt.Rows[0]["cnt"].ToString() == "2")
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Not Allowed More than Two Registrations...',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                //}
                DataTable ddicno = new DataTable();
                DataTable ddicno1 = new DataTable();
                DataTable ddpst = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select mem_new_icno,mem_sts_cd from mem_member where mem_new_icno='" + TXTNOKP.Text + "' and Acc_sts='Y'");
                ddicno1 = DBCon.Ora_Execute_table("select stf_icno from hr_staff_profile where stf_icno='" + TXTNOKP.Text + "'");
                ddpst = DBCon.Ora_Execute_table("select pst_new_icno from aim_pst where pst_new_icno='" + TXTNOKP.Text + "' and ISNULL(pst_txn_ind,'') !='1' and Acc_sts='Y' and pst_withdrawal_type_cd IN ('WSYE')");
                if (ddicno.Rows.Count > 0)
                {

                    if (ddicno.Rows[0][1].ToString() == "SA")
                    {
                        Button7.Visible = false;
                        Button3.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Permohonan Telah Wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                    else if (ddicno.Rows[0][1].ToString() == "TS")
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

                        DataTable ddbind = new DataTable();
                        ddbind = DBCon.Ora_Execute_table("select * from mem_member  where mem_new_icno='" + ddicno.Rows[0][0].ToString() + "' and Acc_sts='Y' and mem_sts_cd='" + ddicno.Rows[0][1].ToString() + "'");

                        Txtnama.Text = ddbind.Rows[0]["mem_name"].ToString();
                        if (ddbind.Rows[0]["mem_nationality_ind"].ToString() == "W")
                        {
                            Rdbwar.Checked = true;
                        }

                        else if (ddbind.Rows[0]["mem_nationality_ind"].ToString() == "N")
                        {
                            RdbBwar.Checked = true;
                        }
                        else
                        {
                            RdbPT.Checked = true;
                        }

                        ddcat.SelectedValue = ddbind.Rows[0]["mem_applicant_type_cd"].ToString();
                        Txtnokplama.Text = ddbind.Rows[0]["mem_old_icno"].ToString();
                        ddJant.SelectedValue = ddbind.Rows[0]["mem_gender_cd"].ToString();
                        ddBangsa.SelectedValue = ddbind.Rows[0]["mem_race_cd"].ToString();
                        Txtage.Text = ddbind.Rows[0]["mem_age"].ToString();
                        TxtTaradu.Text = Convert.ToDateTime(ddbind.Rows[0]["mem_dob"]).ToString("dd/MM/yyyy");

                        string kwil = ddbind.Rows[0]["mem_region_cd"].ToString();
                        DataTable dw = new DataTable();
                        dw = DBCon.Ora_Execute_table("select Wilayah_Name,Wilayah_Code from Ref_Wilayah where Wilayah_Code='" + kwil + "' ");
                        if (dw.Rows.Count != 0)
                        {
                            ddwil.SelectedValue = dw.Rows[0][1].ToString();
                        }
                        else
                        {
                            ddwil.SelectedValue = "";
                        }
                        string kcaw = ddbind.Rows[0]["mem_branch_cd"].ToString();
                        DataTable dc = new DataTable();
                        dc = DBCon.Ora_Execute_table("select cawangan_name,cawangan_code from Ref_Cawangan where cawangan_code='" + kcaw + "'");
                        if (dc.Rows.Count != 0)
                        {
                            ddcaw.SelectedValue = dc.Rows[0][1].ToString();
                        }
                        else
                        {
                            ddcaw.SelectedValue = "";
                        }

                        string kkaw = ddbind.Rows[0]["mem_area_cd"].ToString();
                        DataTable dk = new DataTable();
                        dk = DBCon.Ora_Execute_table("select kavasan_name,kawasan_code from Ref_Cawangan where kawasan_code='" + kkaw + "'");
                        if (dk.Rows.Count != 0)
                        {
                            ddkaw.SelectedValue = dk.Rows[0][0].ToString();
                        }
                        else
                        {
                            kawasan();
                            DataTable dc_kaw = new DataTable();
                            dc_kaw = DBCon.Ora_Execute_table("select * from Ref_Kawasan where wilayah_cd='" + kwil + "' and cawangan_cd='" + kcaw + "'");
                            if (dc_kaw.Rows.Count != 0)
                            {
                                ddkaw.SelectedValue = dc_kaw.Rows[0]["Area_Name"].ToString();
                            }
                            else
                            {
                                ddkaw.SelectedValue = "";
                            }
                        }


                        if (ddbind.Rows[0]["mem_centre"].ToString() != "NULL")
                        {
                            TxtPus.Text = ddbind.Rows[0]["mem_centre"].ToString();
                        }
                        else
                        {
                            TxtPus.Text = "";
                        }
                        //txtAla.Value = ddbind.Rows[0]["mem_address"].ToString();
                        txtAla.Value = Regex.Replace(ddbind.Rows[0]["mem_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                        TextBox1.Text = ddbind.Rows[0]["mem_postcd"].ToString();
                        ddlnegri.SelectedValue = ddbind.Rows[0]["mem_negri"].ToString();
                        if (ddbind.Rows[0]["mem_phone_h"].ToString() != "NULL" && ddbind.Rows[0]["mem_phone_h"].ToString() != "")
                        {
                            Txtnotel_R.Text = ddbind.Rows[0]["mem_phone_h"].ToString();
                        }
                        else
                        {
                            Txtnotel_R.Text = "";
                        }
                        if (ddbind.Rows[0]["mem_phone_o"].ToString() != "NULL" && ddbind.Rows[0]["mem_phone_o"].ToString() != "")
                        {
                            Txtnotel_P.Text = ddbind.Rows[0]["mem_phone_o"].ToString();
                        }
                        else
                        {
                            Txtnotel_P.Text = "";
                        }
                        if (ddbind.Rows[0]["mem_phone_m"].ToString() != "NULL" && ddbind.Rows[0]["mem_phone_m"].ToString() != "")
                        {
                            Txtnotel_B.Text = ddbind.Rows[0]["mem_phone_m"].ToString();
                        }
                        else
                        {
                            Txtnotel_B.Text = "";
                        }
                        if (ddbind.Rows[0]["mem_job"].ToString() != "NULL" && ddbind.Rows[0]["mem_job"].ToString() != "")
                        {
                            ddlTxtPek.SelectedItem.Text = ddbind.Rows[0]["mem_job"].ToString();
                        }
                        else
                        {
                            ddlTxtPek.SelectedValue = "";
                        }
                        Txtemail.Text = ddbind.Rows[0]["mem_email"].ToString();
                        //TxtAlaPek.Value = ddbind.Rows[0]["mem_job_address"].ToString();
                        TxtAlaPek.Value = Regex.Replace(ddbind.Rows[0]["mem_job_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                        txtpeke.Text = ddbind.Rows[0]["mem_job_postcd"].ToString();
                        ddlpeke.SelectedValue = ddbind.Rows[0]["mem_job_negri"].ToString();
                        ddstt.SelectedValue = ddbind.Rows[0]["mem_marital_sts_cd"].ToString();
                        Txtnobank.Text = ddbind.Rows[0]["mem_bank_acc_no"].ToString();
                        ddbank.SelectedValue = ddbind.Rows[0]["mem_bank_cd"].ToString();
                        if (ddbind.Rows[0]["mem_remark"].ToString() != "")
                        {
                            Txtcat.Value = ddbind.Rows[0]["mem_remark"].ToString();
                        }
                        //txtamt1.Text = "50.00";
                        //TextBox3.Text = ddbind.Rows[0]["mem_crt_id"].ToString();
                        //TextBox4.Text = Convert.ToDateTime(ddbind.Rows[0]["mem_register_dt"]).ToString("dd/MM/yyyy");

                        DataTable ddbind_wasi = new DataTable();
                        ddbind_wasi = DBCon.Ora_Execute_table("select * from mem_wasi  where was_new_icno='" + ddicno.Rows[0][0].ToString() + "' and Acc_sts ='Y' order by was_seqno");
                        if (ddbind_wasi.Rows.Count != 0)
                        {
                            if (ddbind_wasi.Rows.Count > 0)
                            {
                                TxtNoKPP1.Text = ddbind_wasi.Rows[0]["was_icno"].ToString();
                                TxtnamaP1.Text = ddbind_wasi.Rows[0]["was_name"].ToString();
                                ddhun.SelectedValue = ddbind_wasi.Rows[0]["was_relation_cd"].ToString();
                                Txtnotelp1.Text = ddbind_wasi.Rows[0]["was_phone_no"].ToString();
                                TxtAlamatp1.Value = Regex.Replace(ddbind_wasi.Rows[0]["was_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                                txtpost1.Text = ddbind_wasi.Rows[0]["was_postcd"].ToString();
                                ddlnegri1.SelectedValue = ddbind_wasi.Rows[0]["was_negri"].ToString();
                            }
                            if (ddbind_wasi.Rows.Count > 1)
                            {
                                TxtNoKPP2.Text = ddbind_wasi.Rows[1]["was_icno"].ToString();
                                TxtnamaP2.Text = ddbind_wasi.Rows[1]["was_name"].ToString();
                                ddhun1.SelectedValue = ddbind_wasi.Rows[1]["was_relation_cd"].ToString();
                                Txttelnop2.Text = ddbind_wasi.Rows[1]["was_phone_no"].ToString();
                                TxtAlamatp2.Value = Regex.Replace(ddbind_wasi.Rows[1]["was_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                                txtpost2.Text = ddbind_wasi.Rows[1]["was_postcd"].ToString();
                                ddlnegri2.SelectedValue = ddbind_wasi.Rows[1]["was_negri"].ToString();
                            }
                        }

                        if (role_add == "1")
                        {
                            Button7.Visible = true;
                        }
                        else
                        {
                            Button7.Visible = false;
                        }
                        Button3.Visible = false;

                        SqlConnection con = new SqlConnection(conString);
                        con.Open();
                        string query1 = "select pst_region_name,pst_branch_name,pst_branch_cd,pst_centre_cd,pst_centre_name,pst_name,pst_bank_acc_no,SUM(pst_nett_amt) as pst_nett_amt from aim_pst where pst_new_icno='" + TXTNOKP.Text + "' and ISNULL(pst_txn_ind,'') !='1' and pst_withdrawal_type_cd IN ('WSYE') and Acc_sts ='Y' group by pst_region_name,pst_branch_name,pst_branch_cd,pst_centre_cd,pst_centre_name,pst_name,pst_bank_acc_no";
                        //DataTable ddw_pst = new DataTable();
                        //ddw_pst = DBCon.Ora_Execute_table("select sum(pst_nett_amt) pst_nett_amt from aim_pst where pst_new_icno='640414035728' and pst_withdrawal_type_cd IN ('WSYE') and Acc_sts ='Y'");
                        var sqlCommand1 = new SqlCommand(query1, con);
                        var sqlReader1 = sqlCommand1.ExecuteReader();
                        if (sqlReader1.Read() == true)
                        {
                            Txtnama.Text = (string)sqlReader1["pst_name"];

                            ddcaw.SelectedValue = (string)sqlReader1["pst_branch_cd"];
                            bcode = (string)sqlReader1["pst_branch_cd"];
                            DataTable ddw = new DataTable();
                            ddw = DBCon.Ora_Execute_table("select Wilayah_Name,Wilayah_Code from Ref_Cawangan where cawangan_code='" + bcode + "'  group by Wilayah_Name,Wilayah_Code order by Wilayah_Name");
                            ddwil.SelectedValue = ddw.Rows[0][1].ToString();
                            kawasan1();
                            //negriBind();
                            if ((string)sqlReader1["pst_centre_name"] != "NULL")
                            {
                                TxtPus.Text = (string)sqlReader1["pst_centre_name"];
                            }
                            else
                            {
                                TxtPus.Text = "";
                            }
                            //Txtnobank.Text = (string)sqlReader1["pst_bank_acc_no"];
                            decimal a = (decimal)sqlReader1["pst_nett_amt"];
                            float s1 = (float)a;
                            //ddpay.SelectedItem.Text = "PST";
                            ddpay.SelectedValue = "P";
                            Button6.Visible = false;
                            ddpay.Attributes.Add("Readonly", "Readonly");
                            ddpay.Attributes.Add("style", "pointer-events:none;");
                            if (s1 >= 130.00)
                            {
                                float temp1 = float.Parse(txtamt1.Text);
                                float temp2 = s1 - temp1;
                                Txtamt2.Text = temp2.ToString("C").Replace("$", "").Replace("RM", "");
                            }
                            else if (s1 < 130.00 && s1 > 30.00)
                            {
                                Txtamt2.Text = "0.00";
                            }
                            else if (s1 < 30.00)
                            {
                                Txtamt2.Text = "0.00";
                            }
                            sqlReader1.Close();
                        }
                        con.Close();

                    }
                    else if (ddicno.Rows[0][1].ToString() == "")
                    {

                        Button7.Visible = false;
                        if (role_edit == "1")
                        {
                            Button3.Visible = true;
                        }
                        else
                        {
                            Button3.Visible = false;
                        }
                        //}
                        DataTable ddbind = new DataTable();
                        ddbind = DBCon.Ora_Execute_table("select * from mem_member  where mem_new_icno='" + ddicno.Rows[0][0].ToString() + "' and Acc_sts='Y' and mem_sts_cd='" + ddicno.Rows[0][1].ToString() + "'");

                        Txtnama.Text = ddbind.Rows[0]["mem_name"].ToString();
                        if (ddbind.Rows[0]["mem_nationality_ind"].ToString() == "W")
                        {
                            Rdbwar.Checked = true;
                        }

                        else if (ddbind.Rows[0]["mem_nationality_ind"].ToString() == "N")
                        {
                            RdbBwar.Checked = true;
                        }
                        else
                        {
                            RdbPT.Checked = true;
                        }

                        ddcat.SelectedValue = ddbind.Rows[0]["mem_applicant_type_cd"].ToString();
                        Txtnokplama.Text = ddbind.Rows[0]["mem_old_icno"].ToString();
                        ddJant.SelectedValue = ddbind.Rows[0]["mem_gender_cd"].ToString();
                        ddBangsa.SelectedValue = ddbind.Rows[0]["mem_race_cd"].ToString();
                        Txtage.Text = ddbind.Rows[0]["mem_age"].ToString();
                        TxtTaradu.Text = Convert.ToDateTime(ddbind.Rows[0]["mem_dob"]).ToString("dd/MM/yyyy");

                        string kwil = ddbind.Rows[0]["mem_region_cd"].ToString();
                        DataTable dw = new DataTable();
                        dw = DBCon.Ora_Execute_table("select Wilayah_Name,Wilayah_Code from Ref_Wilayah where Wilayah_Code='" + kwil + "' ");
                        if (dw.Rows.Count != 0)
                        {
                            ddwil.SelectedValue = dw.Rows[0][1].ToString();
                        }
                        else
                        {
                            ddwil.SelectedValue = "";
                        }
                        string kcaw = ddbind.Rows[0]["mem_branch_cd"].ToString();
                        DataTable dc = new DataTable();
                        dc = DBCon.Ora_Execute_table("select cawangan_name,cawangan_code from Ref_Cawangan where cawangan_code='" + kcaw + "'");
                        if (dc.Rows.Count != 0)
                        {
                            ddcaw.SelectedValue = dc.Rows[0][1].ToString();
                        }
                        else
                        {
                            ddcaw.SelectedValue = "";
                        }

                        string kkaw = ddbind.Rows[0]["mem_area_cd"].ToString();
                        DataTable dk = new DataTable();
                        dk = DBCon.Ora_Execute_table("select kavasan_name,kawasan_code from Ref_Cawangan where kawasan_code='" + kkaw + "'");
                        if (dk.Rows.Count != 0)
                        {
                            ddkaw.SelectedValue = dk.Rows[0][0].ToString();
                        }
                        else
                        {
                            kawasan();
                            DataTable dc_kaw = new DataTable();
                            dc_kaw = DBCon.Ora_Execute_table("select * from Ref_Kawasan where wilayah_cd='" + kwil + "' and cawangan_cd='" + kcaw + "'");
                            if (dc_kaw.Rows.Count != 0)
                            {
                                ddkaw.SelectedValue = dc_kaw.Rows[0]["Area_Name"].ToString();
                            }
                            else
                            {
                                ddkaw.SelectedValue = "";
                            }
                        }


                        if (ddbind.Rows[0]["mem_centre"].ToString() != "NULL")
                        {
                            TxtPus.Text = ddbind.Rows[0]["mem_centre"].ToString();
                        }
                        else
                        {
                            TxtPus.Text = "";
                        }
                        txtAla.Value = Regex.Replace(ddbind.Rows[0]["mem_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                        TextBox1.Text = ddbind.Rows[0]["mem_postcd"].ToString();
                        ddlnegri.SelectedValue = ddbind.Rows[0]["mem_negri"].ToString();
                        if (ddbind.Rows[0]["mem_phone_h"].ToString() != "NULL" && ddbind.Rows[0]["mem_phone_h"].ToString() != "")
                        {
                            Txtnotel_R.Text = ddbind.Rows[0]["mem_phone_h"].ToString();
                        }
                        else
                        {
                            Txtnotel_R.Text = "";
                        }
                        if (ddbind.Rows[0]["mem_phone_o"].ToString() != "NULL" && ddbind.Rows[0]["mem_phone_o"].ToString() != "")
                        {
                            Txtnotel_P.Text = ddbind.Rows[0]["mem_phone_o"].ToString();
                        }
                        else
                        {
                            Txtnotel_P.Text = "";
                        }
                        if (ddbind.Rows[0]["mem_phone_m"].ToString() != "NULL" && ddbind.Rows[0]["mem_phone_m"].ToString() != "")
                        {
                            Txtnotel_B.Text = ddbind.Rows[0]["mem_phone_m"].ToString();
                        }
                        else
                        {
                            Txtnotel_B.Text = "";
                        }
                        if (ddbind.Rows[0]["mem_job"].ToString() != "NULL" && ddbind.Rows[0]["mem_job"].ToString() != "")
                        {
                            ddlTxtPek.SelectedItem.Text = ddbind.Rows[0]["mem_job"].ToString();
                        }
                        else
                        {
                            ddlTxtPek.SelectedValue = "";
                        }
                        Txtemail.Text = ddbind.Rows[0]["mem_email"].ToString();
                        TxtAlaPek.Value = Regex.Replace(ddbind.Rows[0]["mem_job_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                        txtpeke.Text = ddbind.Rows[0]["mem_job_postcd"].ToString();
                        ddlpeke.SelectedValue = ddbind.Rows[0]["mem_job_negri"].ToString();
                        ddstt.SelectedValue = ddbind.Rows[0]["mem_marital_sts_cd"].ToString();
                        Txtnobank.Text = ddbind.Rows[0]["mem_bank_acc_no"].ToString();
                        ddbank.SelectedValue = ddbind.Rows[0]["mem_bank_cd"].ToString();
                        if (ddbind.Rows[0]["mem_remark"].ToString() != "")
                        {
                            Txtcat.Value = ddbind.Rows[0]["mem_remark"].ToString();
                        }
                        //txtamt1.Text = "50.00";
                        TextBox3.Text = ddbind.Rows[0]["mem_crt_id"].ToString();
                        TextBox4.Text = Convert.ToDateTime(ddbind.Rows[0]["mem_register_dt"]).ToString("dd/MM/yyyy");

                        DataTable ddtpst1 = new DataTable();
                        ddtpst1 = DBCon.Ora_Execute_table("select * from mem_share where sha_new_icno='" + TXTNOKP.Text + "' and Acc_sts ='Y' and sha_item='ANGGOTA BAHARU'");

                        DataTable ddtpst2 = new DataTable();
                        ddtpst2 = DBCon.Ora_Execute_table("select * from mem_fee where fee_new_icno='" + TXTNOKP.Text + "' and Acc_sts ='Y'");
                        if (ddtpst1.Rows.Count != 0)
                        {

                            ddpay.SelectedValue = ddtpst1.Rows[0]["sha_reference_ind"].ToString();
                            txtamt1.Text = double.Parse(ddtpst2.Rows[0]["fee_amount"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                            Txtamt2.Text = double.Parse(ddtpst1.Rows[0]["sha_debit_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                            TextBox2.Text = ddtpst2.Rows[0]["fee_pay_remark"].ToString();
                            if (ddtpst1.Rows[0]["sha_reference_ind"].ToString() == "P")
                            {
                                ddpay.Attributes.Add("Style", "Pointer-events:None;");
                                Txtamt2.Attributes.Add("Readonly", "Readonly");
                                TextBox2.Attributes.Add("Readonly", "Readonly");
                            }
                            else
                            {
                                ddpay.Attributes.Remove("Style");
                                Txtamt2.Attributes.Remove("Readonly");
                                TextBox2.Attributes.Remove("Readonly");
                            }
                        }

                        // }

                        DataTable ddbind_wasi = new DataTable();
                        ddbind_wasi = DBCon.Ora_Execute_table("select * from mem_wasi  where was_new_icno='" + ddicno.Rows[0][0].ToString() + "' and Acc_sts ='Y' order by was_seqno");
                        if (ddbind_wasi.Rows.Count != 0)
                        {
                            if (ddbind_wasi.Rows.Count > 0)
                            {
                                TxtNoKPP1.Text = ddbind_wasi.Rows[0]["was_icno"].ToString();
                                TxtnamaP1.Text = ddbind_wasi.Rows[0]["was_name"].ToString();
                                ddhun.SelectedValue = ddbind_wasi.Rows[0]["was_relation_cd"].ToString();
                                Txtnotelp1.Text = ddbind_wasi.Rows[0]["was_phone_no"].ToString();
                                TxtAlamatp1.Value = Regex.Replace(ddbind_wasi.Rows[0]["was_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                                txtpost1.Text = ddbind_wasi.Rows[0]["was_postcd"].ToString();
                                ddlnegri1.SelectedValue = ddbind_wasi.Rows[0]["was_negri"].ToString();
                            }
                            if (ddbind_wasi.Rows.Count > 1)
                            {
                                TxtNoKPP2.Text = ddbind_wasi.Rows[1]["was_icno"].ToString();
                                TxtnamaP2.Text = ddbind_wasi.Rows[1]["was_name"].ToString();
                                ddhun1.SelectedValue = ddbind_wasi.Rows[1]["was_relation_cd"].ToString();
                                Txttelnop2.Text = ddbind_wasi.Rows[1]["was_phone_no"].ToString();
                                TxtAlamatp2.Value = Regex.Replace(ddbind_wasi.Rows[1]["was_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                                txtpost2.Text = ddbind_wasi.Rows[1]["was_postcd"].ToString();
                                ddlnegri2.SelectedValue = ddbind_wasi.Rows[1]["was_negri"].ToString();
                            }
                        }
                    }
                    else
                    {

                        //Button7.Enabled = false;
                        Button7.Visible = false;
                        Button3.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Permohonan Telah Wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }

                else if (ddicno1.Rows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    string query1 = "select hr.stf_name,hr.stf_age,isnull(hr.stf_dob,'') stf_dob,hr.stf_nationality_cd,hr.stf_sex_cd,hr.stf_permanent_address,hr.stf_phone_h,hr.stf_phone_m,hr.stf_marital_sts_cd,hr.stf_mailing_postcode,hr.stf_permanent_state_cd from hr_staff_profile as hr left join ref_gender as rg on rg.gender_cd=hr.stf_sex_cd left join  status_perkahwinan as sp on sp.Marital_Code=hr.stf_marital_sts_cd left join Ref_Negeri as ne on ne.Decription_Code=hr.stf_permanent_state_cd where stf_icno='" + TXTNOKP.Text + "'";
                    var sqlCommand1 = new SqlCommand(query1, con);
                    var sqlReader1 = sqlCommand1.ExecuteReader();
                    if (sqlReader1.Read() == true)
                    {
                        Txtnama.Text = sqlReader1["stf_name"].ToString();
                        Txtage.Text = sqlReader1["stf_age"].ToString();
                        TxtTaradu.Text = Convert.ToDateTime(sqlReader1["stf_dob"]).ToString("dd/MM/yyyy");
                        ddJant.SelectedValue = sqlReader1["stf_sex_cd"].ToString();
                        txtAla.Value = Regex.Replace(sqlReader1["stf_permanent_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                        TextBox1.Text = (string)sqlReader1["stf_mailing_postcode"];
                        ddlnegri.SelectedValue = (string)sqlReader1["stf_permanent_state_cd"];
                        Txtnotel_R.Text = (string)sqlReader1["stf_phone_h"];
                        Txtnotel_P.Text = (string)sqlReader1["stf_phone_m"];
                        System.Web.UI.WebControls.RadioButton rb = null;
                        if ((string)sqlReader1["stf_nationality_cd"].ToString() == "W")
                        {
                            Rdbwar.Checked = true;
                            //rb = Rdbwar;
                        }
                        else if ((string)sqlReader1["stf_nationality_cd"].ToString() == "N")
                        {
                            RdbBwar.Checked = true;
                            //rb = RdbBwar;
                        }
                        else if ((string)sqlReader1["stf_nationality_cd"].ToString() == "P")
                        {
                            RdbPT.Checked = true;
                            //rb = RdbPT;
                        }
                        //wilahBind();
                        kawasan();
                        bBind();
                        //negriBind();
                        sqlReader1.Close();
                    }
                    con.Close();
                }
                else if (ddpst.Rows.Count > 0)
                {

                    if (role_add == "1")
                    {
                        Button7.Visible = true;
                    }
                    else
                    {
                        Button7.Visible = false;
                    }
                    Button3.Visible = false;

                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    string query1 = "  select pst_region_name,pst_branch_name,pst_branch_cd,pst_centre_cd,pst_centre_name,pst_name,pst_bank_acc_no,SUM(pst_nett_amt) as pst_nett_amt from aim_pst where pst_new_icno='" + TXTNOKP.Text + "' and ISNULL(pst_txn_ind,'') !='1' and pst_withdrawal_type_cd IN ('WSYE') and Acc_sts ='Y' group by pst_region_name,pst_branch_name,pst_branch_cd,pst_centre_cd,pst_centre_name,pst_name,pst_bank_acc_no";
                    var sqlCommand1 = new SqlCommand(query1, con);
                    var sqlReader1 = sqlCommand1.ExecuteReader();
                    if (sqlReader1.Read() == true)
                    {
                        Txtnama.Text = (string)sqlReader1["pst_name"];

                        ddcaw.SelectedValue = (string)sqlReader1["pst_branch_cd"];
                        bcode = (string)sqlReader1["pst_branch_cd"];
                        DataTable ddw = new DataTable();
                        ddw = DBCon.Ora_Execute_table("select Wilayah_Name,Wilayah_Code from Ref_Cawangan where cawangan_code='" + bcode + "'  group by Wilayah_Name,Wilayah_Code order by Wilayah_Name");
                        ddwil.SelectedValue = ddw.Rows[0][1].ToString();
                        kawasan1();
                        negriBind();
                        if ((string)sqlReader1["pst_centre_name"] != "NULL")
                        {
                            TxtPus.Text = (string)sqlReader1["pst_centre_name"];
                        }
                        else
                        {
                            TxtPus.Text = "";
                        }
                        //Txtnobank.Text = (string)sqlReader1["pst_bank_acc_no"];
                        decimal a = (decimal)sqlReader1["pst_nett_amt"];
                        float s1 = (float)a;
                        //ddpay.SelectedItem.Text = "PST";
                        ddpay.SelectedValue = "P";
                        Button6.Visible = false;
                        ddpay.Attributes.Add("Readonly", "Readonly");
                        ddpay.Attributes.Add("style", "pointer-events:none;");
                        if (s1 >= 130.00)
                        {
                            float temp1 = float.Parse(txtamt1.Text);
                            float temp2 = s1 - temp1;
                            Txtamt2.Text = temp2.ToString("C").Replace("$", "").Replace("RM", "");
                        }
                        else if (s1 < 130.00 && s1 > 30.00)
                        {
                            Txtamt2.Text = "0.00";
                        }
                        else if (s1 < 30.00)
                        {
                            Txtamt2.Text = "0.00";
                        }
                        sqlReader1.Close();
                    }
                    con.Close();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    if (role_add == "1")
                    {
                        Button7.Visible = true;
                    }
                    else
                    {
                        Button7.Visible = false;
                    }
                    Button3.Visible = false;
                    kawasan();
                    negriBind();
                }

                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Not Allowed More than Two Registrations...',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            //wilahBind();
            kawasan();

            negriBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {

        DataTable Dt = new DataTable();

        try
        {
            if (TXTNOKP.Text != "")
            {
                if (ddpay.SelectedItem.Text != "")
                {
                    if (ddpay.SelectedItem.Text == "TUNAI")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Carian PST Tidak Diperlukan Bagi Bayaran Secara Tunai',{'type': 'confirmation','title': 'Warning','auto_close': 2000});", true);
                    }

                    else if (ddpay.SelectedItem.Text == "PST")
                    {
                        Txtamt2.Attributes.Add("readonly", "readonly");
                        DataTable dt = new DataTable();
                        dt = DBCon.Ora_Execute_table("SELECT SUM(pst_nett_amt) as pst_nett_amt FROM aim_pst WHERE pst_new_icno='" + TXTNOKP.Text + "' and ISNULL(pst_txn_ind,'') !='1' and Acc_sts ='Y' and pst_withdrawal_type_cd IN ('WSYE')");
                        if (dt.Rows.Count != 0)
                        {
                            string a = dt.Rows[0]["pst_nett_amt"].ToString();
                            int s1 = Convert.ToInt32(dt.Rows[0]["pst_nett_amt"]);
                            float temp = float.Parse(a);
                            if (s1 >= 130.00)
                            {
                                float temp1 = float.Parse(txtamt1.Text);
                                //float temp2 = temp - temp1;
                                float temp2 = temp;
                                Txtamt2.Text = temp2.ToString("C").Replace("$", "");
                            }
                            else if (s1 < 130.00 && s1 > 30.00)
                            {
                                Txtamt2.Text = "0.00";
                            }
                            else if (s1 < 30.00)
                            {  
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Caruman PST Tidak Mencukupi Untuk Yuran Keanggotaan. Pemohon Tidak Layak Untuk Didaftarkan',{'type': 'confirmation','title': 'Warning','auto_close': 2000});", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat PST Sahabat Tidak Wujud',{'type': 'confirmation','title': 'Warning','auto_close': 2000});", true);
                            Txtamt2.Text = "0.00";
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Kaedah Bayaran',{'type': 'confirmation','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('ID Penggunna Telah Wujud',{'type': 'confirmation','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddpay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddpay.SelectedItem.Text == "TUNAI")
        {
            Txtamt2.Attributes.Remove("readonly");
            Txtamt2.Text = "";
        }
        else
        {
            Txtamt2.Attributes.Add("readonly", "readonly");
        }
    }
    protected void ddwil_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddkaw.SelectedItem.Text == "SEMUA KAWASAN")
        {
            pusat.Rows.Clear();
            ddkaw.Items.Clear();
            ddkaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        //-Pusat---------------------------------------------------------------------------------
        string cmd5 = "select cawangan_name,cawangan_code from Ref_Cawangan where kavasan_name='" + ddkaw.SelectedItem.Text + "' and wilayah_name='" + ddwil.SelectedItem.Text + "' ";
        pusat.Rows.Clear();
        ddcaw.Items.Clear();

        con.Open();
        SqlDataAdapter adapterP = new SqlDataAdapter(cmd5, con);
        adapterP.Fill(pusat);

        ddcaw.DataSource = pusat;
        ddcaw.DataTextField = "cawangan_name";
        ddcaw.DataValueField = "cawangan_code";
        ddcaw.DataBind();
        //ddPusat.Items.RemoveAt(0); //remove 'Semua Wilayah'
        con.Close();

        ddcaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    }
    protected void ddkaw_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddkaw.SelectedItem.Text == "SEMUA WILAYAH")
        {
            wilayah.Rows.Clear();
            ddwil.Items.Clear();
            ddwil.Items.Insert(0, new ListItem("SEMUA WILAYAH", ""));
        }
        //-Pusat---------------------------------------------------------------------------------
        string cmd6 = "select distinct wilayah_code,wilayah_name from  Ref_Cawangan where kavasan_name='" + ddkaw.SelectedItem.Text + "'";
        wilayah.Rows.Clear();
        ddwil.Items.Clear();

        con.Open();
        SqlDataAdapter adapterP = new SqlDataAdapter(cmd6, con);
        adapterP.Fill(wilayah);

        ddwil.DataSource = wilayah;
        ddwil.DataTextField = "wilayah_name";
        ddwil.DataValueField = "wilayah_code";
        ddwil.DataBind();
        //ddPusat.Items.RemoveAt(0); //remove 'Semua Wilayah'
        con.Close();

        ddwil.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    }
    protected void update(object sender, EventArgs e)
    {
        if (TXTNOKP.Text != "" && Txtnama.Text != "" && ddcat.SelectedValue != "" && TxtTaradu.Text != "" && ddkaw.SelectedValue != "" && ddwil.SelectedValue != "" && ddcaw.SelectedValue != "")
        {


            DataTable Check_mem = new DataTable();
            Check_mem = DBCon.Ora_Execute_table("select * from mem_member where mem_new_icno='" + TXTNOKP.Text + "' and Acc_sts ='Y'");

            if (Check_mem.Rows.Count != 0)
            {
                string fdate = string.Empty, fmdate = string.Empty, fmdate1 = string.Empty;
                if (TextBox4.Text != "")
                {
                    fdate = TextBox4.Text;
                    DateTime ft = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    fmdate = ft.ToString("yyyy-MM-dd");
                    fmdate1 = ft.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss");
                }


                DataTable Check_kaw = new DataTable();
                Check_kaw = DBCon.Ora_Execute_table("select kawasan_code from Ref_Cawangan where wilayah_code='" + ddwil.SelectedValue + "' and cawangan_code='" + ddcaw.SelectedValue + "'");
                SqlConnection con = new SqlConnection(conString);
                //insert Audit Trail
                SqlCommand cmd2 = new SqlCommand("UPDATE mem_member set [mem_register_dt] = @mem_register_dt, [mem_phone_h] = @mem_phone_h, [mem_phone_o] = @mem_phone_o, [mem_phone_m] = @mem_phone_m, [mem_address] = @mem_address,[mem_postcd] = @mem_postcd,[mem_negri] = @mem_negri, [mem_bank_acc_no] = @mem_bank_acc_no, [mem_bank_cd] = @mem_bank_cd,[mem_region_cd] =@mem_region_cd,[mem_branch_cd]=@mem_branch_cd,[mem_centre]=@mem_centre,[mem_area_cd]=@mem_area_cd,[mem_upd_id]=@mem_upd_id,[mem_upd_dt]=@mem_upd_dt,[mem_sts_cd]=@mem_sts_cd WHERE mem_new_icno ='" + TXTNOKP.Text + "' and Acc_sts ='Y'", con);

                decimal a;
                if (Txtamt2.Text != "")
                {
                    a = Convert.ToDecimal(Txtamt2.Text);
                }
                else
                {
                    a = Convert.ToDecimal("0.00");
                }
                int s1 = Convert.ToInt32(a);
                string mm_sts = string.Empty;
                if (s1 == 0.00 || Txtamt2.Text == "" || s1 < 100.00)
                {
                    mm_sts = "FM";
                    cmd2.Parameters.AddWithValue("mem_sts_cd", "FM");
                }

                else
                {
                    mm_sts = "";
                    cmd2.Parameters.AddWithValue("mem_sts_cd", "");
                }
                cmd2.Parameters.AddWithValue("mem_register_dt", fmdate1);
                cmd2.Parameters.AddWithValue("mem_phone_h", Txtnotel_R.Text);
                cmd2.Parameters.AddWithValue("mem_phone_o", Txtnotel_P.Text);
                cmd2.Parameters.AddWithValue("mem_phone_m", Txtnotel_B.Text);
                //string addrs3 = TextArea1.Value.Replace("\r\n", "<br />");
                cmd2.Parameters.AddWithValue("mem_address", txtAla.Value.Replace(",", ""));
                cmd2.Parameters.AddWithValue("mem_postcd", TextBox1.Text);
                cmd2.Parameters.AddWithValue("mem_negri", ddlnegri.SelectedValue);
                cmd2.Parameters.AddWithValue("mem_bank_acc_no", TextBox3.Text);
                cmd2.Parameters.AddWithValue("mem_bank_cd", ddbank.SelectedValue);
                cmd2.Parameters.AddWithValue("mem_region_cd", ddwil.SelectedValue);
                cmd2.Parameters.AddWithValue("mem_branch_cd", ddcaw.SelectedValue);
                cmd2.Parameters.AddWithValue("mem_centre", TxtPus.Text);
                cmd2.Parameters.AddWithValue("mem_area_cd", Check_kaw.Rows[0]["kawasan_code"].ToString());
                cmd2.Parameters.AddWithValue("mem_upd_id", Session["New"].ToString());
                cmd2.Parameters.AddWithValue("mem_upd_dt", fmdate1);

                con.Open();
                int k = cmd2.ExecuteNonQuery();
                con.Close();

                DataTable dtt1 = DBCon.Ora_Execute_table("select was_seqno from mem_wasi where was_new_icno='" + TXTNOKP.Text + "' and Acc_sts ='Y' and was_seqno='1'");
                if (dtt1.Rows.Count == 0)
                {
                    DataTable dw1 = DBCon.Ora_Execute_table("insert into mem_wasi(was_icno,was_name,was_relation_cd,was_phone_no,was_address,was_negri,was_postcd,was_seqno,was_new_icno,Acc_sts)values('" + TxtNoKPP1.Text + "','" + TxtnamaP1.Text + "','" + ddhun.SelectedItem.Value + "','" + Txtnotelp1.Text + "','" + TxtAlamatp1.Value.Replace(",", "") + "','" + ddlnegri1.SelectedItem.Value + "','" + txtpost1.Text + "','1','" + TXTNOKP.Text + "','Y')");
                }
                else
                {

                    DataTable dw1 = DBCon.Ora_Execute_table("update mem_wasi set was_icno='" + TxtNoKPP1.Text + "',was_name='" + TxtnamaP1.Text + "',was_relation_cd='" + ddhun.SelectedItem.Value + "',was_phone_no='" + Txtnotelp1.Text + "',was_address='" + TxtAlamatp1.Value.Replace(",", "") + "',was_negri='" + ddlnegri1.SelectedItem.Value + "',was_postcd='" + txtpost1.Text + "' where was_new_icno='" + TXTNOKP.Text + "' and Acc_sts ='Y' and was_seqno='1'");
                }
                DataTable dtt2 = DBCon.Ora_Execute_table("select was_seqno from mem_wasi where was_new_icno='" + TXTNOKP.Text + "' and Acc_sts ='Y' and was_seqno='2'");
                if (dtt2.Rows.Count == 0)
                {
                    DataTable dw1 = DBCon.Ora_Execute_table("insert into mem_wasi(was_icno,was_name,was_relation_cd,was_phone_no,was_address,was_negri,was_postcd,was_seqno,was_new_icno,Acc_sts)values('" + TxtNoKPP2.Text + "','" + TxtnamaP2.Text + "','" + ddlnegri2.SelectedItem.Value + "','" + Txttelnop2.Text + "','" + TxtAlamatp2.Value.Replace(",", "") + "','" + ddlnegri2.SelectedItem.Value + "','" + txtpost2.Text + "','2','" + TXTNOKP.Text + "','Y')");
                }
                else
                {
                    DataTable dw2 = DBCon.Ora_Execute_table("update mem_wasi set was_icno='" + TxtNoKPP2.Text + "',was_name='" + TxtnamaP2.Text + "',was_relation_cd='" + ddhun1.SelectedItem.Value + "',was_phone_no='" + Txttelnop2.Text + "',was_address='" + TxtAlamatp2.Value.Replace(",", "") + "',was_negri='" + ddlnegri2.SelectedItem.Value + "',was_postcd='" + txtpost2.Text + "' where was_new_icno='" + TXTNOKP.Text + "' and Acc_sts ='Y' and was_seqno='2'");
                }

                // share and fee

                DataTable dtt3 = DBCon.Ora_Execute_table("select * from mem_share where sha_new_icno='" + TXTNOKP.Text + "' and Acc_sts ='Y'");
                if (dtt3.Rows.Count == 0 && mm_sts != "FM")
                {
                    DataTable dw_share_ins = DBCon.Ora_Execute_table("Insert into mem_share (sha_new_icno,sha_txn_dt,sha_txn_ind,sha_debit_amt,sha_credit_amt,sha_reference_no,sha_item,sha_batch_name,sha_reference_ind,sha_crt_id,sha_crt_dt,sha_refund_ind,Acc_sts) Values ('" + TXTNOKP.Text + "','" + fmdate + "','B','" + Txtamt2.Text + "','0.00','','ANGGOTA BAHARU','','P','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','','Y')");
                }
                else
                {
                    DataTable dw_share_upd = DBCon.Ora_Execute_table("update mem_share set sha_txn_dt='" + fmdate + "',sha_debit_amt='" + Txtamt2.Text + "',sha_reference_ind='" + ddpay.SelectedValue + "',sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + Session["New"].ToString() + "' where sha_new_icno='" + TXTNOKP.Text + "' and sha_item='ANGGOTA BAHARU' and Acc_sts ='Y'");
                }

                DataTable dtt4 = DBCon.Ora_Execute_table("select * from mem_fee where fee_new_icno='" + TXTNOKP.Text + "' and Acc_sts ='Y'");
                if (dtt4.Rows.Count == 0 && mm_sts != "FM")
                {
                    DataTable dw_fee_ins = DBCon.Ora_Execute_table("insert into mem_fee(fee_new_icno,fee_txn_dt,fee_payment_type_cd,fee_sts_cd,fee_amount,fee_batch_name,fee_approval_dt,fee_remark,fee_pay_remark,fee_refund_ind,Acc_sts) values ('" + TXTNOKP.Text + "','" + fmdate + "','P','','" + txtamt1.Text + "','','','','" + TextBox2.Text + "','','Y')");
                }
                else
                {
                    DataTable dw_fee_upd = DBCon.Ora_Execute_table("update mem_fee set fee_txn_dt='" + fmdate + "',fee_amount='" + txtamt1.Text + "',fee_payment_type_cd='" + ddpay.SelectedValue + "',fee_pay_remark='" + TextBox2.Text + "' where fee_new_icno='" + TXTNOKP.Text + "' and Acc_sts ='Y'");
                }

                //insert Audit Trail
                service.audit_trail("P0120", "Kemaskini", "No KP Baru", TXTNOKP.Text.Trim());
                Session["validate_success"] = "SUCCESS";
                Session["alrt_msg"] = "Rekod Berjaya DiKemaskini";
                Response.Redirect("../keanggotan/Daft.aspx");
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya DiKemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'confirmation','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field harus Mandatory.',{'type': 'confirmation','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../keanggotan/Daft.aspx");
    }

}