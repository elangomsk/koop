using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Net;

public partial class ksaimb_forgot_pwd : System.Web.UI.Page
{
    DBConnection DBCon = new DBConnection();
    string Status = string.Empty;
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


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (email.Text != "")
        {
            DataTable dd1 = new DataTable();
            dd1 = DBCon.Ora_Execute_table("select * from kk_user_login where KK_email='" + email.Text + "'");
            if (dd1.Rows.Count != 0)
            {
                string Inssql = "Update kk_User_Login SET KK_password='12345',KK_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where KK_email='" + email.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    SendVerificationLinkEmail(email.Text);
                    //Session["validate_success"] = "SUCCESS";
                    //Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("KSAIMB_Login.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert',{'type': 'error','title': 'Error','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Enter Valid Email Address.',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Email.',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
        }
    }

    public void SendVerificationLinkEmail(string EmailID)
    {
        //DataTable dd1 = new DataTable();
        //dd1 = DBCon.Ora_Execute_table("select * from kk_user_login where KK_email='" + email.Text + "'");
        //var verifyurl = "/KAIMB_Login.aspx";
        //var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyurl);
        //var fromemail = new MailAddress("vengatit09@gmail.com", "KAIMB INTEGRATED MANAGEMENT SYSTEM");
        //var toemail = new MailAddress(EmailID);
        //var fromemailpassword = "vengatesan9023";
        //string subject = "KAIMB - User Created Successfully";
        //string email_message = "<table border='0' cellpadding='2' cellspacing='1' width='780' bgcolor='ffffff'><tr bgcolor='2963AD'><td align='center' colspan='2'><font color='FFFFFF'><b>User Registration Details</b></font></td></tr><tr bgcolor='E5E5E5'><td width='156'>User Name :</td><td width=624>" + dd1.Rows[0]["KK_userid"].ToString() + "</td></tr><tr bgcolor='E5E5E5'><td width='156'>Password :</td><td width='624'>12345</td></tr><tr bgcolor='E5E5E5'><td width='156'>URL</td><td width=624>" + link + "</td></tr></table>";
        //string body = "Dear Sir / Madam,<br/><br/>We are excited to tell you that. Successfully Created your Account.<br/><br/>" + email_message + "<br/> Regards,<br/> Email Sysytem.";

        //System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
        //{
        //    Host = "smtp.gmail.com",
        //    Port = 587,
        //    EnableSsl = true,
        //    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
        //    UseDefaultCredentials = false,
        //    Credentials = new NetworkCredential(fromemail.Address, fromemailpassword)


        //};
        //using (var message = new MailMessage(fromemail, toemail)
        //{
        //    Subject = subject,
        //    Body = body,
        //    IsBodyHtml = true
        //})
        //    smtp.Send(message);
    }
}