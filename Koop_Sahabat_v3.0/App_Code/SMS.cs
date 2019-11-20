using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for SMS
/// </summary>
public class SMS
{
    public  String SendMessageToCustomer(string Message, string MobileNo)
    {
        DBConnection ObjDb = new DBConnection();
        String webresponsestring = "", url = "";       
      //  Int32 reqesttimeoutinsecond = 10000;
        url = "http://login.sms99.net/websmsapi/ISendSMS.aspx?username=plenitudedsb&password=pdsb@123&message=" + Message + "&mobile=" + MobileNo + "&sender=1234&type=1";
        WebRequest webrequest;
        WebResponse webresponse;
        webrequest = HttpWebRequest.Create(url);
      //  webrequest.Timeout = reqesttimeoutinsecond;
        String[] str;

        try
        {
            do
            {
                webresponse = (HttpWebResponse)webrequest.GetResponse();
                StreamReader reader = new StreamReader(webresponse.GetResponseStream());
                webresponsestring = reader.ReadToEnd();
                reader.Close();
                webresponse.Close();
                str = webresponsestring.Split(new char[] { '|' });
            } while (str[0] == "1709");


        }
        catch (Exception ex)
        {
            //webresponsestring = ex.Message.ToString();
            //string Sql = "INSERT INTO SMS_LOG VALUES('" + MobileNo + "','" + Message + "',GETDATE(),'failed')";
            //ObjDb.Execute_CommamdText(Sql);
        }
        finally
        {
            webrequest.Abort();
        }
        return webresponsestring;
    }

    public bool ValidMobileNo(string MobileNo)
    {
        string ValidNumber = string.Empty;
        try
        {
            if (MobileNo.Trim().Length < 11)
            {
                return false;
            }
            else if (MobileNo.StartsWith("+6")==false )
            {
                return false;
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
        finally
        {
        }
        return true ;
    }
}