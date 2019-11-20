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

public partial class TKumbulan_TK_Muatnaik_pst : System.Web.UI.Page
{

    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection Dblog = new DBConnection();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    string userid;

    protected void Page_Load(object sender, EventArgs e)
    {
        string script = "  $().ready(function () {  $('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                assgn_roles();
                userid = Session["New"].ToString();
                bindjpst();
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
        SqlCommand cmd = new SqlCommand("select * from aim_st_loc where st_file_type='02' and st_file_sts_cd='A'", con1);
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
        string filePath = Server.MapPath("~/FILES/PST/" + lblid.Trim());
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
        string filePath = Server.MapPath("~/FILES/PST/" + lblTitle.Text.Trim());
        DataTable delete_file = new DataTable();
        DataTable chk_file = new DataTable();
        chk_file = Dblog.Ora_Execute_table("select * from aim_st_loc where st_file_type='02' and st_file_nm='"+ lblTitle.Text + "'");
        if(chk_file.Rows.Count != 0)
        {
            string Inssql_item = "delete from aim_st_loc where st_file_type='02' and st_file_nm='" + lblTitle.Text + "'";
            Status = Dblog.Ora_Execute_CommamdText(Inssql_item);
            if(Status == "SUCCESS")
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
            ddokdicno_1 = Dblog.Ora_Execute_table("select m1.* from KK_Role_skrins m1   where sub_skrin_id='S0058' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

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

    void bindjpst()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string com = "select Product_Description,Product_code from Ref_Produk_TK WHERE Status = 'A' order by Product_code ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddljp.DataSource = dt;
            ddljp.DataBind();
            ddljp.DataTextField = "Product_code";
            ddljp.DataValueField = "Product_Description";
            ddljp.DataBind();
            ddljp.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }    
    void UPLOAD()
    {
        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Files/PST/") + fileName);
        //Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnsmit_Click(object sender, EventArgs e)
    {
        string datetime1, datetime2;
        string errorMessage = String.Empty;
        string datedari1 = string.Empty, datedari2 = string.Empty, pro_desc = string.Empty;
        string feedt = string.Empty, feedt1 = string.Empty, ssdt = string.Empty, ssdt1 = string.Empty, sd = string.Empty;
        string Inssql = string.Empty, Inssql_file = string.Empty, UpSql = string.Empty, SmsMessage = string.Empty, MobileNo = string.Empty;
        if (ddljp.SelectedValue != "")
        {
            if (FileUpload1.FileName != "")
            {
                //Upload and save the file
               
                try
                {
                    DataTable chk_file = new DataTable();
                    chk_file = Dblog.Ora_Execute_table("select * from aim_st_loc where st_file_type='02' and st_file_nm='" + Path.GetFileName(FileUpload1.PostedFile.FileName) + "'");
                    if (chk_file.Rows.Count == 0)
                    {
                        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                        string excelPath = Server.MapPath("~/Files/PST/") + fileName;
                        FileUpload1.SaveAs(excelPath);
                        string directoryPath = Path.GetDirectoryName(excelPath);
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
                            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                            {
                                oda.Fill(dtExcelData);
                            }
                            excel_con.Close();
                            if (dtExcelData.Columns.Count <= 24 && dtExcelData.Columns.Count >= 19)
                            {
                                for (int i = 0; i < dtExcelData.Rows.Count; i++)
                                {
                                    DataTable dtcenter = new DataTable();
                                    if (dtExcelData.Rows[i][10].ToString() != "NULL")
                                    {
                                        if (dtExcelData.Rows[i][10].ToString() != "")
                                        {
                                            datedari1 = dtExcelData.Rows[i][10].ToString();
                                            DateTime date1 = Convert.ToDateTime(datedari1);
                                            datetime1 = date1.ToString("yyyy/MM/dd");
                                        }
                                        else
                                        {
                                            datetime1 = "";
                                        }
                                    }
                                    else
                                    {
                                        datetime1 = "";
                                    }
                                    if (dtExcelData.Rows[i][16].ToString() != "")
                                    {
                                        if (dtExcelData.Rows[i][16].ToString() != "")
                                        {
                                            datedari2 = dtExcelData.Rows[i][16].ToString();


                                            DateTime date2 = Convert.ToDateTime(datedari2);
                                            datetime2 = date2.ToString("yyyy/MM/dd");
                                        }
                                        else
                                        {
                                            datetime2 = "";
                                        }
                                    }
                                    else
                                    {
                                        datetime2 = "";
                                    }

                                    string st_bcd = string.Empty;
                                    if (dtExcelData.Rows[i][7].ToString() != "")
                                    {
                                        int gt_len = Convert.ToString(dtExcelData.Rows[i][2]).Length;
                                        if (gt_len < 3)
                                        {
                                            st_bcd = dtExcelData.Rows[i][2].ToString().PadLeft(3, '0');
                                        }
                                        else
                                        {
                                            st_bcd = dtExcelData.Rows[i][2].ToString();
                                        }
                                        DataTable chk_pst_table = new DataTable();
                                        chk_pst_table = Dblog.Ora_Execute_table("select * from aim_pst where pst_new_icno='" + dtExcelData.Rows[i][7].ToString().Replace("'", "''") + "' and pst_apply_no='" + dtExcelData.Rows[i][9].ToString().Replace("'", "''") + "'");
                                        if (chk_pst_table.Rows.Count == 0)
                                        {
                                            Inssql = "insert into aim_pst (pst_region_name,pst_branch_name,pst_branch_cd,pst_centre_cd,pst_centre_name,pst_no_sahabat,pst_name,pst_new_icno,pst_bank_acc_no,pst_apply_no,pst_iq_dt,pst_wp4_no,pst_apply_amt,pst_nett_amt,pst_charge_amt,pst_balance_amt,pst_post_dt,pst_withdrawal_type_cd,pst_reason,pst_crt_id,pst_crt_dt,Flag,Flag_set,Acc_sts,Flag_set_remark) values('" + dtExcelData.Rows[i][0].ToString().Replace("'", "''") + "','" + dtExcelData.Rows[i][1].ToString().Replace("'", "''") + "','" + st_bcd + "','" + dtExcelData.Rows[i][3].ToString().Replace("'", "''") + "','" + dtExcelData.Rows[i][4].ToString().Replace("'", "''") + "','" + dtExcelData.Rows[i][5].ToString().Replace("'", "''") + "','" + dtExcelData.Rows[i][6].ToString().Replace("'", "''") + "','" + dtExcelData.Rows[i][7].ToString().Replace("'", "''") + "','" + dtExcelData.Rows[i][8].ToString().Replace("'", "''") + "','" + dtExcelData.Rows[i][9].ToString().Replace("'", "''") + "','" + datetime1 + "','" + dtExcelData.Rows[i][11].ToString().Replace("'", "''") + "','" + dtExcelData.Rows[i][12].ToString().Replace("'", "''") + "','" + dtExcelData.Rows[i][13].ToString().Replace("'", "''") + "','" + dtExcelData.Rows[i][14].ToString().Replace("'", "''") + "','" + dtExcelData.Rows[i][15].ToString().Replace("'", "''") + "','" + datetime2 + "','" + ddljp.SelectedValue + "','" + dtExcelData.Rows[i][18].ToString().Replace("'", "''") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','1','1','Y','')";
                                            Status = Dblog.Ora_Execute_CommamdText(Inssql);
                                        }
                                    }

                                }
                                if (Status == "SUCCESS")
                                {
                                    Inssql_file = "insert into aim_st_loc (st_file_type,st_file_nm,st_file_loc,st_file_sts_cd,st_file_create_id,st_file_create_dte) values('02','" + fileName + "','" + excelPath + "','A','" + Session["new"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    string Status_file = Dblog.Ora_Execute_CommamdText(Inssql_file);
                                    service.audit_trail("S0058", ddljp.SelectedItem.Text, "Muatnaik Fail PST", fileName);                                  
                                    clr_value();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dimuatnaik.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                }
                              
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('File Tidak Matching',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Nama Telah Didaftar, Sila Semak.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + errorMessage + "',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }

            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Fail Berkenaan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }

        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Produk',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        bind();
    }

    void clr_value()
    {
        ddljp.SelectedValue = "";
    }
}