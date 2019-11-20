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

public partial class TKumbulan_TK_Mak_Fail : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();

    string Status = string.Empty;
    int i = 0;
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    StudentWebService service = new StudentWebService();
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string userid;

        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                assgn_roles();
                userid = Session["New"].ToString();
                bind();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void bind()
    {
        con1.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select * from aim_st_loc where st_file_type='01' and st_file_sts_cd='A'", con1);
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

    protected void lnkView_Click11(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl1");
        string lblid = lblTitle.Text;
        string filePath = Server.MapPath("~/FILES/ST/" + lblid.Trim());
        Response.ContentType = ContentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(filePath) + "\"");
        Response.WriteFile(filePath);
        Response.End();
    }

    protected void lnkView_Click12(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl1");
        string lblid = lblTitle.Text;
        string filePath = Server.MapPath("~/FILES/ST/" + lblTitle.Text.Trim());
        DataTable delete_file = new DataTable();
        DataTable chk_file = new DataTable();
        chk_file = Dblog.Ora_Execute_table("select * from aim_st_loc where st_file_type='01' and st_file_nm='" + lblTitle.Text + "'");
        if (chk_file.Rows.Count != 0)
        {
            string Inssql_item = "delete from aim_st_loc where st_file_type='01' and st_file_nm='" + lblTitle.Text + "'";
            Status = Dblog.Ora_Execute_CommamdText(Inssql_item);
            if (Status == "SUCCESS")
            {
                File.Delete(filePath);
                bind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }

    }
    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = Dblog.Ora_Execute_table("select m1.* from KK_Role_skrins m1   where sub_skrin_id='S0057' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
             
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();

                if (role_add == "1")
                {
                    btnsmit.Visible = true;
                }
                else
                {
                    btnsmit.Visible = false;
                }

            }
        }
    }
  
    void filepath()
    {

        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        string excelPath = Server.MapPath("~/FILES/") + fileName;
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


    }

  

    protected void UploadFile(object sender, EventArgs e)
    {
        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Files/ST/") + fileName);
        Response.Redirect(Request.Url.AbsoluteUri);
    }

  
    void UPLOAD()
    {
        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Files/ST/") + fileName);
        //Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        String datetime2, datetime1, datetime;
        if (FileUpload1.FileName != "")
        {
            DataTable chk_file = new DataTable();
            chk_file = Dblog.Ora_Execute_table("select * from aim_st_loc where st_file_type='01' and st_file_nm='" + Path.GetFileName(FileUpload1.PostedFile.FileName) + "'");
            if (chk_file.Rows.Count == 0)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string excelPath = Server.MapPath("~/Files/ST/") + fileName;
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

                using (OleDbConnection excel_con = new OleDbConnection(cs))
                {
                    excel_con.Open();
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    DataTable dtExcelData = new DataTable();
                    //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                    dtExcelData.Columns.AddRange(new DataColumn[25] {

               new DataColumn("WILAYAH", typeof(string)),
             new DataColumn("CAWANGAN", typeof(string)),
             new DataColumn("KOD CAW", typeof(string)),
            new DataColumn("KOD PUSAT", typeof(string)),
            new DataColumn("NAMA PUSAT", typeof(string)),
            new DataColumn("NO SAHABAT", typeof(string)),
            new DataColumn("NAMA SAHABAT", typeof(string)),
            new DataColumn("IC BARU", typeof(string)),
              new DataColumn("IC LAMA", typeof(string)),
                new DataColumn("TARIKH MASUK AIM", typeof(DateTime)),
                  new DataColumn("ALAMAT", typeof(string)),
                    new DataColumn("STATUS PERKAHWINAN", typeof(string)),
                      new DataColumn("PEKERJAAN", typeof(string)),
                        new DataColumn("TELEFON", typeof(string)),
                          new DataColumn("NO AKAUN ST", typeof(string)),
                            new DataColumn("BAKI ST", typeof(string)),
                              new DataColumn("JUMLAH KUTIPAN BULANAN", typeof(string)),
                                new DataColumn("NAMA BANK PENERIMA", typeof(string)),
                                  new DataColumn("NO AKAUN BANK PENERIMA", typeof(string)),
                                    new DataColumn("NO IC PENERIMA", typeof(string)),
                                      new DataColumn("NAMA PENERIMA", typeof(string)),
                                        new DataColumn("TARIKH MULA", typeof(DateTime)),
                                          new DataColumn("TARIKH AKHIR", typeof(DateTime)),
                                          new DataColumn("STS SAHABAT", typeof(string)),
                                          new DataColumn("SAHAM", typeof(string))

       });
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                    {
                        oda.Fill(dtExcelData);

                    }
                    excel_con.Close();
                    if (dtExcelData.Columns.Count <= 26 && dtExcelData.Columns.Count >= 24)
                    {
                        string Upd_st = "Update aim_st set ast_sahabat_sts_cd='T' where ISNULL(ast_sahabat_sts_cd,'') !='T'";
                        string Status_Upd_st = Dblog.Ora_Execute_CommamdText(Upd_st);
                        if (Status_Upd_st == "SUCCESS")
                        {
                            for (int i = 0; i < dtExcelData.Rows.Count; i++)
                            {
                                string Inssql = string.Empty, UpSql = string.Empty, SmsMessage = string.Empty, MobileNo = string.Empty, sahabat_sts = string.Empty;
                                DataTable dtcenter = new DataTable();
                                dtcenter = Dblog.Ora_Execute_table("select ast_new_icno from aim_st where ast_new_icno='" + dtExcelData.Rows[i][7].ToString() + "' and ast_no_sahabat='"+ dtExcelData.Rows[i][5].ToString() + "'");
                                if (dtExcelData.Rows[i][7].ToString() != "")
                                {
                                    if (dtcenter.Rows.Count == 0)
                                    {
                                        string mpvalue = dtExcelData.Rows[i][3].ToString();
                                        string mvalue = mpvalue.Replace("'", "''");
                                        string datedari = dtExcelData.Rows[i][9].ToString();
                                        if (datedari == "")
                                        {
                                            datetime = "";
                                        }
                                        else
                                        {
                                            DateTime date1 = DateTime.Parse(datedari);
                                            datetime = date1.ToString("yyyy/MM/dd");
                                        }
                                        string datedari1 = dtExcelData.Rows[i][21].ToString();
                                        if (datedari1 == "")
                                        {
                                            datetime1 = "";
                                        }
                                        else
                                        {
                                            DateTime date1 = DateTime.Parse(datedari1);
                                            datetime1 = date1.ToString("yyyy/MM/dd");
                                        }
                                        string datedari2 = dtExcelData.Rows[i][22].ToString();
                                        if (datedari2 == "")
                                        {
                                            datetime2 = "";
                                        }
                                        else
                                        {

                                            DateTime date2 = DateTime.Parse(datedari2);
                                            datetime2 = date2.ToString("yyyy/MM/dd");
                                        }

                                        string mpval = dtExcelData.Rows[i][6].ToString();
                                        string mval = mpval.Replace("'", "''");

                                        string mpval1 = dtExcelData.Rows[i][20].ToString();
                                        string mval1 = mpval.Replace("'", "''");

                                        string mpval2 = dtExcelData.Rows[i][10].ToString();
                                        string mval2 = mpval.Replace("'", "''");
                                        if(dtExcelData.Rows[i][23].ToString().Trim() == "A" || dtExcelData.Rows[i][23].ToString().Trim() == "B" || dtExcelData.Rows[i][23].ToString().Trim() == "N")
                                        {
                                            sahabat_sts = "A";
                                        }
                                        else
                                        {
                                            sahabat_sts = "T";
                                        }
                                        Inssql = "insert into aim_st (ast_region_name,ast_branch_name,ast_branch_cd,ast_centre_cd,ast_centre_name,ast_no_sahabat,ast_name,ast_new_icno,ast_old_icno,ast_aim_join_date,ast_address,ast_marital_sts_cd,ast_job,ast_phone_no,ast_st_acc_no,ast_st_balance_amt,ast_monthly_collect_amt,ast_receive_bank_cd,ast_receive_acc_no,ast_receive_icno,ast_receive_name,ast_start_date,ast_end_date,ast_sahabat_sts_cd,ast_crt_id,ast_crt_dt) values('" + dtExcelData.Rows[i][0].ToString() + "','" + dtExcelData.Rows[i][1].ToString() + "','" + dtExcelData.Rows[i][2].ToString() + "','" + dtExcelData.Rows[i][3].ToString() + "','" + dtExcelData.Rows[i][4].ToString() + "','" + dtExcelData.Rows[i][5].ToString() + "','" + mval + "','" + dtExcelData.Rows[i][7].ToString() + "','" + dtExcelData.Rows[i][8].ToString() + "','" + datetime + "','" + mval2 + "','" + dtExcelData.Rows[i][11].ToString() + "','" + dtExcelData.Rows[i][12].ToString() + "','" + dtExcelData.Rows[i][13].ToString() + "','" + dtExcelData.Rows[i][14].ToString() + "','" + dtExcelData.Rows[i][15].ToString() + "','" + dtExcelData.Rows[i][16].ToString() + "','" + dtExcelData.Rows[i][17].ToString() + "','" + dtExcelData.Rows[i][18].ToString() + "','" + dtExcelData.Rows[i][19].ToString() + "','" + mval1 + "','" + datetime1 + "','" + datetime2 + "','" + sahabat_sts + "','"+ Session["new"].ToString() +"','"+ DateTime.Now.ToString("yyyy-MM-dd") +"')";
                                        Status = Dblog.Ora_Execute_CommamdText(Inssql);
                                        i++;
                                    }
                                    else
                                    {
                                        string mpvalue = dtExcelData.Rows[i][3].ToString();
                                        string mvalue = mpvalue.Replace("'", "''");
                                        string datedari = dtExcelData.Rows[i][9].ToString();
                                        if (datedari == "")
                                        {
                                            datetime = "";
                                        }
                                        else
                                        {
                                            DateTime date1 = DateTime.Parse(datedari);
                                            datetime = date1.ToString("yyyy/MM/dd");
                                        }

                                        string datedari1 = dtExcelData.Rows[i][21].ToString();
                                        if (datedari1 == "")
                                        {
                                            datetime1 = "";
                                        }
                                        else
                                        {

                                            DateTime date1 = DateTime.Parse(datedari1);
                                            datetime1 = date1.ToString("yyyy/MM/dd");
                                        }

                                        string datedari2 = dtExcelData.Rows[i][22].ToString();
                                        if (datedari2 == "")
                                        {
                                            datetime2 = "";
                                        }
                                        else
                                        {

                                            DateTime date2 = DateTime.Parse(datedari2);
                                            datetime2 = date2.ToString("yyyy/MM/dd");
                                        }

                                        string mpval = dtExcelData.Rows[i][6].ToString();
                                        string mval = mpval.Replace("'", "''");
                                       
                                        Inssql = "update aim_st set ast_st_balance_amt='" + dtExcelData.Rows[i][15].ToString() + "',ast_start_date='" + datetime1 + "',ast_end_date='" + datetime2 + "',ast_sahabat_sts_cd='A',ast_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd") +"',ast_upd_id='"+ Session["new"].ToString() +"' where  ast_new_icno='" + dtExcelData.Rows[i][7].ToString() + "' and ast_no_sahabat='"+ dtExcelData.Rows[i][5].ToString() + "'";
                                        Status = Dblog.Ora_Execute_CommamdText(Inssql);
                                        i++;
                                    }
                                }
                            }
                            if (Status == "SUCCESS")
                            {
                                string Inssql_file = "insert into aim_st_loc (st_file_type,st_file_nm,st_file_loc,st_file_sts_cd,st_file_create_id,st_file_create_dte) values('01','" + fileName + "','" + excelPath + "','A','" + Session["new"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                string Status_file = Dblog.Ora_Execute_CommamdText(Inssql_file);                                
                                service.audit_trail("S0057", "", "Muatnaik Fail ST", fileName);
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dimuatnaik.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('File Not Support',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Nama Telah Didaftar, Sila Semak.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nama Fail.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        bind();
    }

}