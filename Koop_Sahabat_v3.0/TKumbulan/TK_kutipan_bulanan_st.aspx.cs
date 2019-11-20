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
using System.Net;
using System.Threading;


public partial class TK_kutipan_bulanan_st : System.Web.UI.Page
{
    DBConnection dbcon = new DBConnection();
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable wilayah = new DataTable();
    DataTable caw = new DataTable();
    StudentWebService service = new StudentWebService();    
    DataTable pusat = new DataTable();
    DataTable dt = new DataTable();
    DataTable ddtt = new DataTable();
    DataTable Mpcheck = new DataTable();
    DataTable MpWil = new DataTable();
    DataTable MpWila = new DataTable();
    DBConnection DBCon = new DBConnection();
    DataSet ds = new DataSet();
    string level, userid;
    string filename = string.Empty;
    string ss1 = string.Empty, ss2 = string.Empty, ss3 = string.Empty, ss4 = string.Empty, ss5 = string.Empty, rdlc_name = string.Empty, DS_name = string.Empty;
    string status;
    DateTime dmula;
    string bcode;
    string wcode;
    string sno;
    string ccode;
    string Status = string.Empty, sqry = string.Empty, sqry1 = string.Empty;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    string DD_sts = string.Empty;
    string rpt_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();

      
        //this.Button1.Attributes.Add("onclick", DisableTheButton(this.Page, this.Button1));



        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                f_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                t_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                              
                level = Session["level"].ToString();
                userid = Session["New"].ToString();                                
                
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    private string DisableTheButton(Control pge, Control btn)

    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("if (typeof(Page_ClientValidate) == 'function') {");

        sb.Append("if (Page_ClientValidate() == false) { return false; }} ");

        //sb.Append("if (confirm('Are you sure to proceed?') == false) { return false; } ");

        sb.Append("this.value = 'Please wait...';");

        sb.Append("this.disabled = true;");

        sb.Append(pge.Page.ClientScript.GetPostBackEventReference(btn, ""));

        sb.Append(";");

        return sb.ToString();
        

    }

    private string EnableTheButton(Control pge, Control btn)

    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("if (typeof(Page_ClientValidate) == 'function') {");

        sb.Append("if (Page_ClientValidate() == false) { return false; }} ");

        //sb.Append("if (confirm('Are you sure to proceed?') == false) { return false; } ");

        sb.Append("this.value = 'Export To PDF';");

        sb.Append(pge.Page.ClientScript.GetPostBackEventReference(btn, ""));

        sb.Append(";");

        return sb.ToString();


    }


   
    void app_language()
    {
        if (Session["New"] != null)
        {
            assgn_roles();
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('146','1052','1085','147','64','65','22','1034','148','25','21','14','15','1086')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;            
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            
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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0215' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();
                if (role_add == "1")
                {
                    Button2.Visible = true;
                }
                else
                {
                    Button2.Visible = false;
                }

            }
        }
    }


    
    protected void rst_clk(object sender, EventArgs e)
    {
        Response.Redirect("../keanggotan/LK_SENARI.aspx");
    }

    void get_details()
    {
        string fdate = f_date.Text;
        DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        String fmdate = fd.ToString("yyyy-MM-dd");

        string tdate = t_date.Text;
        DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        String tmdate = td.ToString("yyyy-MM-dd");

        dt = GetData(fmdate, tmdate, DD_sts, rpt_type);

       
           
    }

    void get_details1()
    {
        string fdate = f_date.Text;
        DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        String fmdate = fd.ToString("yyyy-MM-dd");

        string tdate = t_date.Text;
        DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        String tmdate = td.ToString("yyyy-MM-dd");

        dt = GetData(fmdate, tmdate, DD_sts, rpt_type);
    }

    void grid()
    {
        get_details();
        string hd_nm1 = string.Empty;
        if (DD_STS_ANGGO.SelectedValue == "01")
        {
            hd_nm1 = "ANGGOTA KOOP";
        }
        else if (DD_STS_ANGGO.SelectedValue == "02")
        {
            hd_nm1 = "BUKAN ANGGOTA KOOP";
        }
        

        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        //Building an HTML string.
        StringBuilder htmlTable = new StringBuilder();
        htmlTable.Append("<table border='1' id='gvSelected' cell style='width:100%;' class='table uppercase table-striped'>");
        htmlTable.Append("<tr style='background-color:#BDC4C7;'> <th>WILAYAH</th> <th> CAWANGAN </th><th> NAMA PUSAT </th><th>NO SAHABAT </th><th> NO ANGGOTA </th><th> NAMA SAHABAT </th><th> IC BARU </th><th> TARIKH MASUK AIM </th><th> NO AKAUN ST </th><th> BAKI ST (RM) </th><th> TARIKH AKHIR </th> </tr>");

        if (!object.Equals(ds.Tables[0], null))
        {
            //Building the Data rows.
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    htmlTable.Append("<tr class='text-uppercase'>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["ast_region_name"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["ast_branch_name"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["ast_centre_name"] + "</td>");
                    htmlTable.Append("<td align='center'>" + ds.Tables[0].Rows[i]["ast_no_sahabat"] + "</td>");
                    htmlTable.Append("<td align='center'>" + ds.Tables[0].Rows[i]["mem_member_no"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["ast_name"] + "</td>");
                    htmlTable.Append("<td align='center'>" + ds.Tables[0].Rows[i]["ast_new_icno"] + "</td>");
                    htmlTable.Append("<td align='center'>" + ds.Tables[0].Rows[i]["join_dt"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["ast_st_acc_no"] + "</td>");
                    htmlTable.Append("<td align='center'>" + ds.Tables[0].Rows[i]["end_dt"] + "</td>");
                    htmlTable.Append("<td align='Right'>" + double.Parse(ds.Tables[0].Rows[i]["ast_st_balance_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "") + "</td>");
                    
                    htmlTable.Append("</tr>");
                }
                //Button1.Visible = true;
                //Button3.Visible = true;
                htmlTable.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
            }
            else
            {
                //Button1.Visible = false;
                //Button3.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }
          
        }
        string script1 = "$(function () { $('#gvSelected').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); $('.select2').select2() });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);

    }

    private DataTable GetData(string fromDate, string toDate, string dd_sts, string rpt_type)
    {
        
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ToString();
        using (SqlConnection cn = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("tk_Anggota_rpt", cn);
            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromDate;
            cmd.Parameters.Add("@todate", SqlDbType.DateTime).Value = toDate;
            cmd.Parameters.Add("@dd_sts", SqlDbType.VarChar).Value = dd_sts;
            cmd.Parameters.Add("@rpt_type", SqlDbType.VarChar).Value = rpt_type;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
        }
        return dt;
    }

  

    void grid1()
    {
        get_details1();
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        //Building an HTML string.
        StringBuilder htmlTable = new StringBuilder();
        htmlTable.Append("<table border='1' id='GridView1' cell style='width:100%;' class='table uppercase table-striped'>");
        htmlTable.Append("<tr style='background-color:#BDC4C7;'> <th>WILAYAH</th> <th> CAWANGAN </th><th> IC BARU </th><th> BAKI ST (RM) </th><th>TARIKH AKHIR</th></tr>");

        if (!object.Equals(ds.Tables[0], null))
        {
            //Building the Data rows.
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    htmlTable.Append("<tr class='text-uppercase'>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["ast_region_name"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["ast_branch_name"] + "</td>");
                    htmlTable.Append("<td align='center'>" + ds.Tables[0].Rows[i]["jum_anggota"] + "</td>");                    
                    htmlTable.Append("<td align='Right'>" + double.Parse(ds.Tables[0].Rows[i]["bal_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "") + "</td>");
                    htmlTable.Append("<td align='center'>" + ds.Tables[0].Rows[i]["end_dt"] + "</td>");
                    htmlTable.Append("</tr>");
                }
                //Button1.Visible = true;
                //Button3.Visible = true;
                htmlTable.Append("</table>");
                PlaceHolder2.Controls.Add(new Literal { Text = htmlTable.ToString() });
            }
            else
            {
                //Button1.Visible = false;
                //Button3.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }
          
        }
        string script1 = "$(function () { $('#GridView1').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); $('.select2').select2() });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void clk_batal(object sender, EventArgs e)
    {
        Response.Redirect("../TKumbulan/TK_kutipan_bulanan_st.aspx");
    }
    protected void ExportToPDF(object sender, EventArgs e)
    {
        try
        {
            if (f_date.Text != "" && t_date.Text != "" && DropDownList1.SelectedValue != "" && DD_STS_ANGGO.SelectedValue != "")
            {
                string fdate = f_date.Text;
                DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                String fmdate = fd.ToString("yyyy-MM-dd");

                string tdate = t_date.Text;
                DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                String tmdate = td.ToString("yyyy-MM-dd");
                DD_sts = DD_STS_ANGGO.SelectedValue;
                rpt_type = DropDownList1.SelectedValue;
                if (DD_STS_ANGGO.SelectedValue != "")
                {
                    
                    if (DropDownList1.SelectedValue == "01")
                    {                        
                        rdlc_name = "TKumbulan/tk_kut_anggota_ring.rdlc";
                        DS_name = "kut_anggota_ring";
                    }
                    
                    else
                    {
                                                
                        rdlc_name = "TKumbulan/tk_kut_anggota_sen.rdlc";
                        DS_name = "kut_anggota_sen";
                    }
                    string hd_nm1 = string.Empty;
                    if (DD_STS_ANGGO.SelectedValue == "01")
                    {
                        hd_nm1 = "ANGGOTA KOOP";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "02")
                    {
                        hd_nm1 = "BUKAN ANGGOTA KOOP";
                    }
                    dt = GetData(fmdate, tmdate, DD_sts, rpt_type);
                    //dt = DBCon.Ora_Execute_table(sqry);
                    ds.Tables.Add(dt);
                    ReportViewer1.Reset();
                    ReportViewer1.LocalReport.Refresh();
                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();

                    if (countRow != 0)
                    {                        
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.ReportPath = rdlc_name;
                        ReportDataSource rds = new ReportDataSource(DS_name, dt);
                        ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", f_date.Text),
                     new ReportParameter("s2", t_date.Text),
                     new ReportParameter("s3", DD_STS_ANGGO.SelectedItem.Text),
                     new ReportParameter("s4", hd_nm1)
                          };

                        ReportViewer1.LocalReport.SetParameters(rptParams);
                        ReportViewer1.LocalReport.DataSources.Add(rds);
                        //Refresh
                        ReportViewer1.LocalReport.Refresh();
                        
                        filename = string.Format("{0}.{1}", "IMS_" + DD_STS_ANGGO.SelectedItem.Text + "_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                        //}
                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string extension;

                        byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = mimeType;
                        Response.AddHeader("content-disposition", "attachment; filename=\"" + filename + "\"");
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        //HttpContext.Current.Response.End();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                        System.Threading.Thread.Sleep(1);
                        
                        //List<ReportParameter> paramList = new List<ReportParameter>();
                        //paramList.Add(new ReportParameter("RowsPerPage", "30"));
                        //RptviwerLKSENARI.LocalReport.SetParameters(paramList);


                    }
                    else
                    {                        
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Status Anggota.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);


                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Response.Redirect("LK_SENARI.aspx");
        }
    }

    protected void ExportToEXCEL(object sender, EventArgs e)
    {
        try
        {
            if (f_date.Text != "" && t_date.Text != "" && DropDownList1.SelectedValue != "")
            {
                if (DD_STS_ANGGO.SelectedValue != "")
                {
                    string fdate = f_date.Text;
                    DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    String fmdate = fd.ToString("yyyy-MM-dd");

                    string tdate = t_date.Text;
                    DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    String tmdate = td.ToString("yyyy-MM-dd");
                    DD_sts = DD_STS_ANGGO.SelectedValue;
                    rpt_type = DropDownList1.SelectedValue;

                    dt = GetData(fmdate, tmdate, DD_sts, rpt_type);
                    //dt = DBCon.Ora_Execute_table(sqry);
                    ds.Tables.Add(dt);
                    ReportViewer1.Reset();
                    ReportViewer1.LocalReport.Refresh();
                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();
                    string hd_nm1 = string.Empty;
                    if (DD_STS_ANGGO.SelectedValue == "01")
                    {
                        hd_nm1 = "ANGGOTA KOOP";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "02")
                    {
                        hd_nm1 = "BUKAN ANGGOTA KOOP";
                    }
                    if (countRow != 0)
                    {

                        StringBuilder builder = new StringBuilder();
                        string strFileName = string.Format("{0}.{1}", "IMS_"+ DD_STS_ANGGO.SelectedItem.Text +"_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                        if (DropDownList1.SelectedValue == "02")
                        {
                            builder.Append("WILAYAH,CAWANGAN,NAMA PUSAT,NO SAHABAT,NO ANGGOTA, NAMA SAHABAT, IC BARU,TARIKH MASUK AIM,NO AKAUN ST, BAKI ST (RM), TARIKH AKHIR" + Environment.NewLine);
                            for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                            {
                                builder.Append(dt.Rows[k]["ast_region_name"].ToString() + " , " + dt.Rows[k]["ast_branch_name"].ToString() + ", " + dt.Rows[k]["ast_centre_name"].ToString() + "," + dt.Rows[k]["ast_no_sahabat"].ToString() + "," + dt.Rows[k]["mem_member_no"].ToString() + "," + dt.Rows[k]["ast_name"].ToString().Replace("'", "''") + "," + dt.Rows[k]["ast_new_icno"].ToString() + "," + dt.Rows[k]["join_dt"].ToString() + "," + dt.Rows[k]["ast_st_acc_no"].ToString() + "," + dt.Rows[k]["ast_st_balance_amt"].ToString() + "," + dt.Rows[k]["end_dt"].ToString()  + Environment.NewLine);
                            }
                        }
                        else
                        {
                            builder.Append("WILAYAH,CAWANGAN,BIL. SAHABAT, BAKI ST (RM), TARIKH AKHIR" + Environment.NewLine);
                            for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                            {

                                builder.Append(dt.Rows[k]["ast_region_name"].ToString() + " , " + dt.Rows[k]["ast_branch_name"].ToString() + ", " + dt.Rows[k]["jum_anggota"].ToString() + "," + dt.Rows[k]["bal_amt"].ToString() + "," + dt.Rows[k]["end_dt"].ToString() + Environment.NewLine);

                            }
                        }
                        Response.Clear();
                        Response.ContentType = "text/csv";
                        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + strFileName + "\"");
                        Response.Write(builder.ToString());
                        Response.End();


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Status Anggota.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Response.Redirect("LK_SENARI.aspx");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        try
        {
            
            if (DD_STS_ANGGO.SelectedValue != "" && DropDownList1.SelectedValue != "")
            {
                DD_sts = DD_STS_ANGGO.SelectedValue;
                rpt_type = DropDownList1.SelectedValue;
                if (DropDownList1.SelectedValue == "02")
                {
                    //gvSelected.Visible = true;
                    //GridView1.Visible = false;
                    grid();
                   
                }
                else
                {
                    //gvSelected.Visible = false;
                    //GridView1.Visible = true;
                    grid1();
                }
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                string script1 = "$(function () {$('.select2').select2()  });";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
            }
        
        }

        catch (Exception ex)
        {
            //txtError.Text = ex.ToString();
        }
       
    }
}