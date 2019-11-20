using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
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
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web.SessionState;
using System.Xml;
using System.Security.Cryptography;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class StudentWebService : System.Web.Services.WebService {

    string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    DBConnection DBCon = new DBConnection();
    DataTable get_desc = new DataTable();
    string aud_desc = string.Empty;
    //Encrypt
    [WebMethod]
    public string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    //Decrypt
    [WebMethod]
    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    //Decrypt
    [WebMethod]
    public void LogError(string msg)
    {
        try
        {
            StackFrame CallStack = new StackFrame(1, true);
            string path = "~/Files/Log_files/" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt";
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
            }
            using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                w.WriteLine("\r\nLog Entry :");
                w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() + ". \n\n Error Message:" + msg + "Line :" + CallStack.GetFileLineNumber();
                w.WriteLine(err);
                w.WriteLine("==========================================");
                w.Flush();
                w.Close();
            }

        }
        catch
        {
            throw;
        }
    }

    //Audit trail
    [WebMethod]
    public int audit_trail(string txn_cd, string action, string action1, string txn_desc)
    {
        SqlConnection con = null;
        if (txn_cd == "01" || txn_cd == "02")
        {
            aud_desc = txn_desc;
        }
        else
        {
            get_desc = DBCon.Ora_Execute_table("select * from ( select KK_Sskrin_name as name from kk_pid_sub_skrin where kk_skrin_id='" + txn_cd + "' and Status='A' "
                + "union all select KK_Spreskrin_name as name from KK_PID_presub_Skrin where KK_Spreskrin_id ='" + txn_cd + "' and Status='A' "
                + "union all select KK_Spreskrin1_name from KK_PID_presub1_Skrin where KK_Spreskrin1_id='" + txn_cd + "' and Status='A'"
                + " ) as a");
            if (get_desc.Rows.Count != 0)
            {
                aud_desc = get_desc.Rows[0]["name"].ToString() + " for " + action + " on " + action1 + "- " + txn_desc + "";
            }
        }

        string query = "INSERT INTO cmn_audit_trail (aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc) VALUES (@aud_crt_id,@aud_crt_dt,@aud_txn_cd,@aud_txn_desc)";

        con = new SqlConnection(constr);
        SqlCommand command = new SqlCommand(query, con);
        command.Parameters.Add("@aud_crt_id", SqlDbType.NVarChar).Value = Session["userid"].ToString();
        command.Parameters.Add("@aud_crt_dt", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        command.Parameters.Add("@aud_txn_cd", SqlDbType.NVarChar).Value = txn_cd;
        command.Parameters.Add("@aud_txn_desc", SqlDbType.NVarChar).Value = aud_desc;

        int result = -1;
        try
        {
            con.Open();
            result = command.ExecuteNonQuery();
        }
        catch (Exception)
        { }
        finally
        {
            con.Close();
        }
        return result;
    }

}

