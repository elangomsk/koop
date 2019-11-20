using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Globalization;

public partial class KSAIMB_Login : System.Web.UI.Page
{
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dd1 = new DataTable();
        dd1 = DBCon.Ora_Execute_table("select * from site_settings where Id = '1'");
        if (dd1.Rows.Count != 0)
        {
            log_tit.Text = dd1.Rows[0]["log_title"].ToString();
            if (dd1.Rows[0]["log_logo"].ToString() != "")
            {
                log_logo.Text = "<img src='../files/site/" + dd1.Rows[0]["log_logo"].ToString() + "' alt='' />";
            }
            else
            {
                log_logo.Text = "<img src='../files/Profile_syarikat/user.png' alt='' />";
            }
        }
    }

    public string base64Decode2(string sData)
    {
        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        System.Text.Decoder utf8Decode = encoder.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(sData);
        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        char[] decoded_char = new char[charCount];
        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        string result = new String(decoded_char);
        return result;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Txtuser.Text != "")
        {
            if (Txtpwd.Text != "")
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
                conn.Open();

                string checkuser = "select count(*) from KK_User_Login where KK_userid='" + Txtuser.Text + "' and Status='A'";
                SqlCommand com = new SqlCommand(checkuser, conn);
                int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
                conn.Close();

                //user found, check password 
                if (temp == 1)
                {
                    conn.Open();
                    string checkPassword = "select KK_password from KK_User_Login where KK_userid='" + Txtuser.Text + "' and Status='A'";
                    SqlCommand passCom = new SqlCommand(checkPassword, conn);
                    //.Replace (" ","") - added because sometimes there are unwanted space in the text.
                    //string password = passCom.ExecuteScalar().ToString().Replace(" ", "");
                    string password = passCom.ExecuteScalar().ToString().Replace(" ", "");
                    //verify password entered
                    if (password == Txtpwd.Text)
                    {

                        string checkLevel = "select level from KK_User_Login where KK_userid='" + Txtuser.Text + "' and Status='A'";
                        SqlCommand levelCom = new SqlCommand(checkLevel, conn);
                        //.Replace (" ","") - added because sometimes there are unwanted space in the text.
                        string Level = levelCom.ExecuteScalar().ToString().Replace(" ", "");

                        Session["New"] = Txtuser.Text;
                        Session["Wil"] = "";
                        Response.Write("Password is correct");

                        DataTable clear_cache = new DataTable();
                        clear_cache = DBCon.Ora_Execute_table("CHECKPOINT DBCC DROPCLEANBUFFERS");

                        // Maintain Sessions
                        Session["site_languaage"] = "mal";
                        Session["getlang1"] = "";
                        Session["validate_success"] = "";
                        Session["pro_id"] = "";
                        Session["con_id"] = "";
                        Session["alrt_msg"] = "";
                        Session["mnu_id"] = "";
                        Session["bajt_det_cd"] = "";
                        Session["pem_update"] = "";
                        Session["tkh_fdt"] = "";
                        Session["tkh_tdt"] = "";
                        Session["tkh_caw"] = "";
                        Session["tkh_jtun"] = "";
                        Session["mnt_sts"] = "0";
                        Session["tkh_chk"] = "0";
                        Session["permohon_no"] = "";
                        DataTable dd1 = new DataTable();
                        dd1 = DBCon.Ora_Execute_table("select s1.*,s3.op_state_cd  from kk_User_Login s1 left join hr_staff_profile s2 on s2.stf_staff_no=s1.KK_userid left join hr_organization_pern s3 on s3.op_perg_code=s2.stf_cur_sub_org where KK_userid = '" + Txtuser.Text + "' and s1.Status='A'");

                        Session["userid"] = dd1.Rows[0]["KK_userid"].ToString();
                        Session["username"] = dd1.Rows[0]["KK_username"].ToString();
                        Session["roles"] = dd1.Rows[0]["KK_roles"].ToString();
                        Session["permission"] = dd1.Rows[0]["KK_skrins"].ToString();
                        Session["level"] = dd1.Rows[0]["KK_wilayah_cd"].ToString();
                        Session["tkh_wil"] = dd1.Rows[0]["KK_wilayah_cd"].ToString();
                        Session["user_state"] = dd1.Rows[0]["op_state_cd"].ToString();
                        Session["confirm"] = "1";
                        Session["get_cd"] = "";
                        service.LogError("successful Login for " + dd1.Rows[0]["KK_userid"].ToString());
                        service.audit_trail("01", "", "", "Login");
                        conn.Close();
                       
                            Response.Redirect("KSAIMB_Home.aspx");
                       
                      
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Kata Laluan Tidak Sah.',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
                        
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('ID Pengguna Tidak Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Kata Laluan.',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No IC.',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
        }
    }

   
}