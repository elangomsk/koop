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
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Windows.Forms;

public partial class site_settings : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty;
    string sqry1 = string.Empty, sqry2 = string.Empty;
    string level;
    string Status = string.Empty;
    string userid;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                ImgPrv.Attributes.Add("src", "../files/Profile_syarikat/user.png");
                view_details();
            }
            else
            {
                Response.Redirect("KSAIMB_Login.aspx");
            }
        }
    }


    void view_details()
    {
        DataTable dd1 = new DataTable();
        dd1 = DBCon.Ora_Execute_table("select * from site_settings where Id = '1'");
        if(dd1.Rows.Count != 0)
        {
            log_title.Text = dd1.Rows[0]["log_title"].ToString();
            foot_comp.Text = dd1.Rows[0]["foot_comp"].ToString();
            foot_copy.Text = dd1.Rows[0]["foot_copy"].ToString();
            string checkimage = dd1.Rows[0]["log_logo"].ToString();

            string fileName = Path.GetFileName(checkimage);
            if (fileName != "")
            {
                ImgPrv.Attributes.Add("src", "../Files/site/" + fileName);
            }
            else
            {
                ImgPrv.Attributes.Add("src", "../Files/Profile_syarikat/user.png");
            }
        }
    }
    protected void clk_submit(object sender, EventArgs e)
    {
        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        if (log_title.Text != "" && foot_comp.Text != "" && foot_copy.Text != "")
        {
            DataTable dd1 = new DataTable();
            dd1 = DBCon.Ora_Execute_table("select * from site_settings where Id = '1'");
            if (fileName != "")
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/site/" + fileName));//Or code to save in the DataBase.
                System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
            }
            else
            {
                fileName = dd1.Rows[0]["log_logo"].ToString();
            }
            string Inssql = "Update site_settings SET log_logo='" + fileName + "', log_title='" + log_title.Text + "',foot_copy='"+ foot_copy.Text + "',foot_comp='"+ foot_comp.Text + "' where Id = '1'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Settings Changed.',{'type': 'confirmation','title': 'Success'});", true);
                    }
               
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
        }

    }
    //protected void btn_reset(object sender, EventArgs e)
    //{
    //    Response.Redirect("KSAIMB_Home.aspx");
    //}


}