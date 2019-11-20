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


public partial class audit_trail_view : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    DataTable dt = new DataTable();
    string sqry = string.Empty;
    string level;
    string Status = string.Empty;
    string userid;
    string ref_id;
    string confirmValue, am;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Button2.OnClientClick = @"if(this.value == 'Please wait...')
           return false;
           this.value = 'Please wait...';this.disabled=true";
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                  
                }
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";
                userid = Session["New"].ToString();                
            }
            else
            {
                Response.Redirect("~/KSAIMB_Login.aspx");
            }
        }
    }

  
    protected void btn_clr_Click(object sender, EventArgs e)
    {

        Response.Redirect("audit_trail_view.aspx");
    }


    protected void BindData()
    {
        srch_bind();
        qry2 = "select s1.aud_txn_cd,s1.aud_txn_desc,s1.Id,s2.Kk_userid,s2.KK_username,s2.KK_email,case when Status = 'A' then 'AKTIF' else 'TIDAK AKTIF' end as sts,case when ISNULL(user_img,'') = '' then 'user.gif' else user_img end as img1,FORMAT(s1.aud_crt_dt,'dd/MM/yyyy HH:mm:ss', 'en-us') as aud_dt from cmn_audit_trail s1 inner join KK_User_Login s2 on s2.KK_userid=s1.aud_crt_id "+ qry1 + " order by s1.aud_crt_dt desc";
        dt = DBCon.Ora_Execute_table(qry2);
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        //Building an HTML string.
        StringBuilder htmlTable = new StringBuilder();
        htmlTable.Append("<table border='1' id='gv_refdata' cell style='width:100%;' class='table uppercase table-striped'>");
        htmlTable.Append("<tr style='background-color:#BDC4C7;'> <th>User Id</th> <th> User Name </th><th> User Email </th><th> Description </th><th> Tarikh </th><th> STATUS </th></tr>");

        if (!object.Equals(ds.Tables[0], null))
        {
            //Building the Data rows.
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    htmlTable.Append("<tr class='text-uppercase'>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["Kk_userid"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["KK_username"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["KK_email"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["aud_txn_desc"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["aud_dt"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["sts"] + "</td>");
                    htmlTable.Append("</tr>");
                }
                Button1.Visible = true;
                Button3.Visible = true;
                htmlTable.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }
        }
        string script1 = "$(function () { $('#gv_refdata').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); $('.select2').select2() });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

   

    protected void btn_search_Click(object sender, EventArgs e)
    {        
        BindData();
    }

    void srch_bind()
    {
        if (TextBox1.Text != "" && (f_date.Text == "" && t_date.Text == ""))
        {
            qry1 = "where (KK_userid like '%" + TextBox1.Text + "%' or s2.KK_username like '%" + TextBox1.Text + "%' or s1.aud_txn_desc like '%" + TextBox1.Text + "%')";
        }
        else if (TextBox1.Text != "" && (f_date.Text != "" && t_date.Text != ""))
        {
            string fdate = f_date.Text;
            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String fmdate = fd.ToString("yyyy-MM-dd");

            string tdate = t_date.Text;
            DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String tmdate = td.ToString("yyyy-MM-dd");

            qry1 = "where s1.aud_crt_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and s1.aud_crt_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and (KK_userid like '%" + TextBox1.Text + "%' or s2.KK_username like '%" + TextBox1.Text + "%' or s1.aud_txn_desc like '%" + TextBox1.Text + "%')";
        }
        else if (TextBox1.Text == "" && (f_date.Text != "" && t_date.Text != ""))
        {
            string fdate = f_date.Text;
            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String fmdate = fd.ToString("yyyy-MM-dd");

            string tdate = t_date.Text;
            DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            String tmdate = td.ToString("yyyy-MM-dd");
            qry1 = "where s1.aud_crt_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and s1.aud_crt_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)";
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        srch_bind();
        using (SqlConnection conn = new SqlConnection(conString))
        {
            using (SqlCommand command = new SqlCommand("select s2.Kk_userid as User_id,s2.KK_username as User_name,s2.KK_email as Email,s1.aud_txn_desc as Description,FORMAT(s1.aud_crt_dt,'dd/MM/yyyy HH:mm:ss', 'en-us') as Tarikh,case when Status = 'A' then 'AKTIF' else 'TIDAK AKTIF' end as Status from cmn_audit_trail s1 inner join KK_User_Login s2 on s2.KK_userid=s1.aud_crt_id " + qry1 + " order by s1.aud_crt_dt desc"))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    command.Connection = conn;
                    adapter.SelectCommand = command;
                    using (DataTable dtEmployee = new DataTable())
                    {
                        adapter.Fill(dtEmployee);
                        string str = string.Empty;
                        foreach (DataColumn column in dtEmployee.Columns)
                        {
                            // Add the header to the text file
                            str += column.ColumnName + "\t\t";
                        }
                        // Insert a new line.
                        str += "\r\n";
                        foreach (DataRow row in dtEmployee.Rows)
                        {
                            foreach (DataColumn column in dtEmployee.Columns)
                            {
                                // Insert the Data rows.
                                str += row[column.ColumnName].ToString() + "\t\t";
                            }
                            // Insert a  new line.
                            str += "\r\n";
                        }
                        // Download the Text file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment;filename=Audit_trail_"+ DateTime.Now.ToString("yyyyMMdd_HHmmss") +".txt");
                        Response.Charset = "";
                        Response.ContentType = "application/text";
                        Response.Output.Write(str);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
        BindData();
    }
}