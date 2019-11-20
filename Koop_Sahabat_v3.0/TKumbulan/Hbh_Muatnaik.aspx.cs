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
using System.Data.OleDb;
using System.IO;
using System.Net;

public partial class Hbh_Muatnaik : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string fileName;
    string Status = string.Empty;
    int i = 0;
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    StudentWebService service = new StudentWebService();
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string userid;
        string script1 = "$(function () {$('.select2').select2()  });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {   
                userid = Session["New"].ToString();
                bind_year();
                bind();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void bind_year()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select year(ast_end_date) ast_year from aim_st group by year(ast_end_date)";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "ast_year";
            DropDownList1.DataValueField = "ast_year";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

  
    void instaudt()
    {
        string audcd = "020101";
        string auddec = "MUATNAIK FAIL ST";
        string usrid = Session["New"].ToString();
        string curdt = DateTime.Now.ToString();
        string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc) values ('" + usrid + "','" + curdt + "','" + audcd + "','" + auddec + "')";
        Status = Dblog.Ora_Execute_CommamdText(Inssql);
    }
  
    protected void clk_reset(object sender, EventArgs e)
    {
        DropDownList1.SelectedValue = "";
        bind();
    }

    protected void UploadFile(object sender, EventArgs e)
    {
        fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/ST/") + fileName);
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void lnkView_Click11(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lblRequestor3");
        string lblid = lblTitle.Text;
        string filePath = Server.MapPath("~/FILES/HIBAH/" + lblid.Trim());
        Response.ContentType = ContentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(filePath) + "\"");
        Response.WriteFile(filePath);
        Response.End();
    }

    protected void lnkView_Click12(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lblRequestor3");
        System.Web.UI.WebControls.Label lbl_Batch_id = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label3");
        string lblid = lblTitle.Text;
        string filePath = Server.MapPath("~/Files/HIBAH/" + lblTitle.Text.Trim());
        DataTable delete_file = new DataTable();
        DataTable chk_file = new DataTable();
        chk_file = Dblog.Ora_Execute_table("select * from mem_hibah_st where mem_hbh_file_name='" + lblTitle.Text + "' and mem_hbh_batch_id='" + lbl_Batch_id.Text + "'");
        if (chk_file.Rows.Count != 0)
        {            
            string Inssql_item = "update mem_hibah_st set mem_hbh_batcht_stat='HAPUS' ,mem_hbh_del_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',mem_hbh_upd_id='" + Session["New"].ToString() + "',mem_hbh_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' where   mem_hbh_file_name='" + lblTitle.Text + "' and mem_hbh_batch_id='" + lbl_Batch_id.Text + "'";
            Status = Dblog.Ora_Execute_CommamdText(Inssql_item);
            if (Status == "SUCCESS")
            {
                File.Delete(filePath);
                bind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }

    }
    void UPLOAD()
    {
        fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/HIBAH/") + fileName);
        //Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        String datetime2, datetime1, datetime;
        DataTable dtdd = new DataTable();
        String DD;
        string Inssql = string.Empty, UpSql = string.Empty, SmsMessage = string.Empty, MobileNo = string.Empty;
        if (FileUpload1.FileName != "")
        {
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string excelPath = Server.MapPath("~/Files/HIBAH/") + fileName;
            string directoryPath = Path.GetDirectoryName(excelPath);
            FileUpload1.SaveAs(excelPath);

            string conString = string.Empty;
            string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

            switch (extension)
            {
                case ".xls": //Excel 97-03
                    conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                    break;

            }
            cs = string.Format(conString, excelPath);

            try
            {
                using (OleDbConnection excel_con = new OleDbConnection(cs))
                {
                    excel_con.Open();
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    DataTable dtExcelData = new DataTable();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                    {
                        oda.Fill(dtExcelData);
                    }
                    excel_con.Close();
                    if (dtExcelData.Columns.Count <= 25 && dtExcelData.Columns.Count >= 24)
                    {
                        for (int i = 0; i < dtExcelData.Rows.Count; i++)
                        {

                            DataTable dtcenter = new DataTable();
                            dtcenter = Dblog.Ora_Execute_table("select ast_new_icno from aim_st where ast_new_icno='" + dtExcelData.Rows[i][7].ToString() + "'");
                            if (dtExcelData.Rows[i][7].ToString() != "")
                            {
                                if (dtcenter.Rows.Count == 0)
                                {
                                    string mpvalue = dtExcelData.Rows[i][6].ToString();
                                    string mvalue = mpvalue.Replace("'", "''");

                                    string datedari = dtExcelData.Rows[i][9].ToString();
                                    DateTime date = DateTime.Parse(datedari);
                                    //string result = dateTime.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                                    //DateTime date = DateTime.ParseExact(datedari, "yyyy/MM/dd", null);
                                    ////DateTime dt = DateTime.ParseExact(datedari, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                                    datetime = date.ToString("yyyy-MM-dd hh:mm:ss");

                                    string datedari1 = dtExcelData.Rows[i][21].ToString();
                                    DateTime date1 = DateTime.Parse(datedari1);
                                    datetime1 = date1.ToString("yyyy-MM-dd hh:mm:ss");

                                    string datedari2 = dtExcelData.Rows[i][22].ToString();
                                    DateTime date2 = DateTime.Parse(datedari2);
                                    datetime2 = date2.ToString("yyyy-MM-dd hh:mm:ss");

                                    string mpval = dtExcelData.Rows[i][10].ToString();
                                    string mval = mpval.Replace("'", "''");

                                    string mpval1 = dtExcelData.Rows[i][20].ToString();
                                    string mval1 = mpval.Replace("'", "''");
                                    dtdd = Dblog.Ora_Execute_table("select isnull(max(mem_hbh_batch_bil),0) +1 mem_hbh_batch_bil from mem_hibah_st where  mem_hbh_batch_yr_id ='" + DropDownList1.SelectedValue + "'");
                                    DD = DropDownList1.SelectedValue + "(" + dtdd.Rows[0][0].ToString() + ")";
                                    fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);


                                    UpSql = "insert into mem_hibah_st_item values('" + DD + "','" + dtExcelData.Rows[i][0].ToString() + "','" + dtExcelData.Rows[i][1].ToString() + "','" + dtExcelData.Rows[i][2].ToString() + "','" + dtExcelData.Rows[i][3].ToString() + "','" + dtExcelData.Rows[i][4].ToString() + "','" + dtExcelData.Rows[i][5].ToString() + "','" + mvalue + "','" + dtExcelData.Rows[i][7].ToString() + "','" + dtExcelData.Rows[i][8].ToString() + "','" + datetime + "','" + dtExcelData.Rows[i][10].ToString().Replace(",", "").Replace("\r", " ").Replace("\n", " ") + "','" + dtExcelData.Rows[i][11].ToString() + "','" + dtExcelData.Rows[i][12].ToString() + "','" + dtExcelData.Rows[i][13].ToString() + "','" + dtExcelData.Rows[i][14].ToString() + "','" + dtExcelData.Rows[i][15].ToString() + "','" + dtExcelData.Rows[i][16].ToString() + "','" + dtExcelData.Rows[i][17].ToString() + "','" + dtExcelData.Rows[i][18].ToString() + "','" + dtExcelData.Rows[i][19].ToString() + "','" + dtExcelData.Rows[i][20].ToString() + "','" + datetime1 + "','" + datetime2 + "','" + dtExcelData.Rows[i][23].ToString() + "','SAH','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "','','')";
                                    Status = Dblog.Ora_Execute_CommamdText(UpSql);
                                }


                            }
                        }
                       
                        DD = DropDownList1.SelectedValue + "(" + dtdd.Rows[0][0].ToString() + ")";
                        Inssql = "insert into mem_hibah_st(mem_hbh_batch_id,mem_hbh_batch_id_dt,mem_hbh_batcht_stat,mem_hbh_del_dt,mem_hbh_batch_mth,mem_hbh_batch_bil,mem_hbh_file_name,mem_hbh_batch_yr_id,mem_hbh_load_dt,mem_hbh_crt_id,mem_hbh_crt_dt)values('" + DD + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "','BARU','','','" + dtdd.Rows[0][0].ToString() + "','" + fileName + "','" + DropDownList1.SelectedValue + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')";
                        Status = Dblog.Ora_Execute_CommamdText(Inssql);

                        if (Status == "SUCCESS")
                        {
                            service.audit_trail("P0217", "", "Muatnaik Hibah", DD);
                          
                            UPLOAD();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dimuatnaik.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('File Not Support.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Fail Berkenaan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        bind();
    }

    void bind()
    {
        con1.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select mem_hbh_batch_id,mem_hbh_batch_id_dt,mem_hbh_batch_mth,mem_hbh_file_name,mem_hbh_batch_yr_id from mem_hibah_st where mem_hbh_batcht_stat='BARU'", con1);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        con1.Close();
    }
}