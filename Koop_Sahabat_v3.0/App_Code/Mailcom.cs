using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Threading;

/// <summary>
/// Summary description for Mailcom
/// </summary>
public class Mailcoms
{
   
    //public string SendMail(string ToMailid,string Subject,string Message)
    //{
    //    string Result = string.Empty;
    //    MailMessage mail = new MailMessage();
    //    SmtpClient smtp = new SmtpClient();
    //    try
    //    {
    //        string SMTPUser = "tekuncybermall@plenitudedsb.com", SMTPPassword = "tekunCM@123";

    //        mail.From = new System.Net.Mail.MailAddress(SMTPUser, "TEKUN CyberMall");
    //        mail.To.Add(ToMailid);
    //        mail.Subject = Subject;
    //        mail.Body = Message;
    //        mail.IsBodyHtml = true;
    //        mail.Priority = System.Net.Mail.MailPriority.Normal;

    //        smtp.Host = "finch.mschosting.com";
    //        smtp.Port = 25;
    //        smtp.EnableSsl = false;
    //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
    //        smtp.UseDefaultCredentials = false;
    //        smtp.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPassword);
    //        smtp.Send(mail);
    //        Result = "SUCCESS";
    //        mail.Dispose();
    //        smtp.Dispose();
    //    }
    //    catch (Exception ex)
    //    {
    //        Result = ex.ToString();
    //        mail.Dispose();
    //        smtp.Dispose();
    //    }
    //    finally
    //    {
    //        mail.Dispose();
    //        smtp.Dispose();
    //    }
    //    return Result;
    //}

    //public void SendMail(string ToMailid, string Subject, string Message)
    //{
    //    Thread email = new Thread(delegate()
    //    {
    //        SendAsyncEmail(ToMailid, Subject, Message);
    //    });
    //    email.IsBackground = true;
    //    email.Start();
    //}



    public string SendMail(string ToMailid, string CCmailid, string Bccmailid, string Subject, string Message)
    {
        string Status;
        MailMessage mail = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        try
        {
            string SMTPUser = "status@aimsolutions.com.my", SMTPPassword = "cmccs@123";

            mail.From = new System.Net.Mail.MailAddress(SMTPUser, "KSAIMB");

            if (ToMailid.Trim() != string.Empty)
            {

                mail.To.Add(ToMailid);
            }
            if (CCmailid.Trim() != string.Empty)
            {
                mail.CC.Add(CCmailid);
            }
            if (Bccmailid != string.Empty)
            {

                mail.Bcc.Add(Bccmailid);
            }

            mail.Subject = Subject;
            mail.Body = Message;
            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;

            smtp.Host = "smtp.aimsolutions.com.my";
            smtp.Port = 587;
            smtp.EnableSsl = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPassword);


            smtp.Send(mail);
            Status = "SUCCESS";

        }
        catch (Exception ex)
        {
            Status = "FAILER";
        }
        return Status;
    }
    private static void SendAsyncEmail(string ToMailid, string Subject, string Message)
    {
        DBConnection ObjDb = new DBConnection();
        string Result = string.Empty;
        MailMessage mail = new MailMessage();
        try
        {
            
            mail.From = new System.Net.Mail.MailAddress("", "KSAIMB");
            mail.To.Add(ToMailid);
            mail.Subject = Subject;
            mail.Body = Message;
            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;
            SmtpClient smtp = new SmtpClient();
            try
            {
                smtp.Host = "mail.sahabatbazaar.my";
                smtp.EnableSsl = false ; //Depending on server SSL Settings true/false
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = "admin@sahabatbazaar.my";
                NetworkCred.Password = "P@ssw0rd";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;//Specify your port No;
                smtp.Send(mail);

            }
            catch(SmtpException smex)
            {
                //mail.Dispose();
                //string Sql = "INSERT INTO MAIL_LOG VALUES('" + ToMailid + "','" + Subject + "','" + Message + "','SMTPFAILED')";
                //ObjDb.Execute_CommamdText(Sql);
                smtp = null;

            }

        }
        catch (Exception ex)
        {
            //Result = ex.ToString();
            //string Sql = "INSERT INTO MAIL_LOG VALUES('" + ToMailid + "','" + Subject + "','" + Message + "','FAILED')";
            //ObjDb.Execute_CommamdText(Sql);
            mail.Dispose();
        }
        finally
        {
            mail.Dispose();
        }

    }
}