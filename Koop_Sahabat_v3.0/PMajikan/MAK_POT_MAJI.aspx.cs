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


public partial class PMajikan_MAK_POT_MAJI : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    String datetime2, datetime1, datetime;
    string Status = string.Empty;
    string userid;
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
      

        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/FILES/PMAJ/"));
                List<ListItem> files = new List<ListItem>();
                foreach (string filePath in filePaths)
                {
                    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                }
                GridView1.DataSource = files;
                GridView1.DataBind();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    


    void instaudt()
    {
        string audcd = "020101";
        string auddec = "MUATNAIK DATA POTONGAN MAJIKAN";
        string usrid = Session["New"].ToString();
        string curdt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc) values ('" + usrid + "','" + curdt + "','" + audcd + "','" + auddec + "')";
        Status = Dblog.Ora_Execute_CommamdText(Inssql);
    }
    void filepath()
    {

        string fileName = Path.GetFileName(ImageFile.PostedFile.FileName);
        string excelPath = Server.MapPath("~/FILES/") + fileName;
        string directoryPath = Path.GetDirectoryName(excelPath);

        ImageFile.PostedFile.SaveAs(excelPath);
        string conString = string.Empty;
        string extension = Path.GetExtension(ImageFile.PostedFile.FileName);

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

    protected void DownloadFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }

    protected void UploadFile(object sender, EventArgs e)
    {
        string fileName = Path.GetFileName(ImageFile.PostedFile.FileName);
        ImageFile.PostedFile.SaveAs(Server.MapPath("~/FILES/PMAJ/") + fileName);
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void DeleteFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        File.Delete(filePath);
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    void UPLOAD()
    {
        string fileName = Path.GetFileName(ImageFile.PostedFile.FileName);
        ImageFile.PostedFile.SaveAs(Server.MapPath("~/FILES/PMAJ/") + fileName);
        //Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
      
        if (ImageFile.PostedFile.FileName != "")
        {
            filepath();
            try
            {
                using (OleDbConnection excel_con = new OleDbConnection(cs))
                {
                    excel_con.Open();
                 
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[9]["TABLE_NAME"].ToString();
                    

                    DataTable dtExcelData = new DataTable();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                    {
                        oda.Fill(dtExcelData);
                     

                    }
                    excel_con.Close();
                    if (dtExcelData.Columns.Count <= 49 && dtExcelData.Columns.Count >= 24)
                    {
                        for (int i = 0; i < dtExcelData.Rows.Count; i++)
                        {
                            string Inssql = string.Empty,fsql=string.Empty,dsql=string.Empty,spsql=string.Empty ,UpSql = string.Empty, SmsMessage = string.Empty, MobileNo = string.Empty;
                           
                            if (dtExcelData.Rows[i][3].ToString() != "")
                            {
                               
                                    string mpvalue = dtExcelData.Rows[i][2].ToString();
                                    string mvalue = mpvalue.Replace("'", "''");


                                    string datedari = dtExcelData.Rows[i][7].ToString();
                                    if (datedari == "")
                                    {
                                        datetime = "";
                                    }
                                    else
                                    {

                                        DateTime date = DateTime.Parse(datedari);
                                        datetime = date.ToString("yyyy/MM/dd");
                                    }

                                    string datedari1 = dtExcelData.Rows[i][8].ToString();
                                    if (datedari1 == "")
                                    {
                                        datetime1 = "";
                                    }

                                    else
                                    {

                                        DateTime date1 = DateTime.Parse(datedari);
                                        datetime1 = date1.ToString("yyyy/MM/dd");
                                    }

                                        string datedari2 = dtExcelData.Rows[i][13].ToString();
                                        if (datedari2 == "")
                                        {
                                        datetime2 = "";
                                        }

                                        else
                                        {

                                            DateTime date2 = DateTime.Parse(datedari);
                                            datetime2 = date2.ToString("yyyy/MM/dd");
                                        }

                                    userid = Session["New"].ToString();
                                    Inssql = "insert into TH_PM(PM_NO_KT,PM_NAMA_ANGGOTA,PM_NO_AHLI,PM_No_KP,PM_KADAR,PM_JUMLAH_PINJAMAN,PM_TARIKH_MULA_PINJAMAN,PM_TARIKH_AKHIR_PINJAMAN,PM_FORMULA_TEMPOH_PINJAMAN,PM_TEMPOH_PINJAMAN_TAHUN,PM_JUMLAH_POKOK_UNTUNG,PM_BYRN_POKOK_KEUNTUNGAN_BULANAN,TARIKH_CLDate,JUMLAH_BULAN_TERIMA,JUMLAH_POKOK_UNTUNG_TELAH_TERIMA,BAKI_PINJ_UNTUNG,PM_FI_MASUK,PM_SAHAM,PM_YURAN,PM_SIMP_KHAS,PM_BAYARAN_POKOK_KTHB,PM_KEUNTUNGAN_KTHB,PM_BAYARAN_POKOK_BIMB,PM_KEUNTUNGAN_BIMB,PM_JUMLAH,PM_crt_id,PM_crt_dt,PM_upd_id,PM_upd_dt,Acc_sts) values('" + dtExcelData.Rows[i][1].ToString() + "','" + mvalue + "','" + dtExcelData.Rows[i][3].ToString() + "','" + dtExcelData.Rows[i][4].ToString() + "','" + dtExcelData.Rows[i][5].ToString() + "','" + dtExcelData.Rows[i][6].ToString() + "','" + datetime + "','" + datetime1 + "','" + dtExcelData.Rows[i][9].ToString() + "','" + dtExcelData.Rows[i][10].ToString() + "','" + dtExcelData.Rows[i][11].ToString() + "','" + dtExcelData.Rows[i][12].ToString() + "','" + datetime2 + "','" + dtExcelData.Rows[i][14].ToString() + "','" + dtExcelData.Rows[i][15].ToString() + "','" + dtExcelData.Rows[i][16].ToString() + "','" +  dtExcelData.Rows[i][17].ToString() + "','" + dtExcelData.Rows[i][18].ToString() + "','" + dtExcelData.Rows[i][19].ToString() + "','" + dtExcelData.Rows[i][20].ToString() + "','" + dtExcelData.Rows[i][21].ToString() + "','" + dtExcelData.Rows[i][22].ToString() + "','" + dtExcelData.Rows[i][23].ToString() + "','" + dtExcelData.Rows[i][24].ToString() + "','" + dtExcelData.Rows[i][25].ToString() + "','Migrate','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','','','A')";
                                        Status = Dblog.Ora_Execute_CommamdText(Inssql);

                                    if (dtExcelData.Rows[i][19].ToString() != "")
                                    {
                                    DataTable dtcenter = new DataTable();
                                    dtcenter = Dblog.Ora_Execute_table("select fee_new_icno from mem_fee where fee_new_icno='" + dtExcelData.Rows[i][4].ToString() + "' and fee_sts_cd='SA'");
                                    if (dtcenter.Rows.Count > 0)
                                    {

                                        fsql = "insert into mem_fee(fee_new_icno,fee_txn_dt,fee_payment_type_cd,fee_sts_cd,fee_amount,fee_batch_name,fee_approval_dt,fee_remark,fee_pay_remark,fee_refund_ind,Acc_sts) values('" + dtExcelData.Rows[i][4].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','P', 'SA','" + dtExcelData.Rows[i][19].ToString() + "', '', '', '','', 'N','Y')";
                                        Status = Dblog.Ora_Execute_CommamdText(fsql);
                                    }

                                    }
                                    if (dtExcelData.Rows[i][18].ToString() != "")
                                    {
                                           DataTable dtcenter = new DataTable();
                                          dtcenter = Dblog.Ora_Execute_table("select sha_new_icno from mem_share where sha_new_icno='" + dtExcelData.Rows[i][4].ToString() + "' and sha_approve_sts_cd='SA'");
                                    if (dtcenter.Rows.Count > 0)
                                    {
                                        dsql = " insert into mem_share(sha_new_icno,sha_txn_dt,sha_txn_ind,sha_debit_amt,sha_credit_amt,sha_reference_no,sha_item,sha_batch_name,sha_reference_ind,sha_apply_sts_ind,sha_approve_sts_cd,sha_approve_dt,sha_crt_id,sha_crt_dt,sha_upd_id,sha_upd_dt,sha_refund_ind,Acc_sts) values('" + dtExcelData.Rows[i][4].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "', 'B', '" + dtExcelData.Rows[i][18].ToString() + "', '', '','', '', 'P', '', 'SA', '', '" + userid + "', '" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "', '', '', 'N', 'Y')";
                                        Status = Dblog.Ora_Execute_CommamdText(dsql);
                                    }
                                    }
                                    if (dtExcelData.Rows[i][20].ToString() != "")
                                    {
                                    DataTable dtcenter = new DataTable();
                                    dtcenter = Dblog.Ora_Execute_table("select sav_new_icno from mem_save where sav_new_icno='" + dtExcelData.Rows[i][4].ToString() + "' and sav_sts_cd='SA'");
                                    if (dtcenter.Rows.Count > 0)
                                    {

                                        spsql = "insert into mem_save(sav_new_icno,sav_txn_dt,sav_payment_type_cd,sav_sts_cd,sav_amount,sav_batch_name,sav_approval_dt,sav_remark,sav_pay_remark,sav_refund_ind,Acc_sts) values('" + dtExcelData.Rows[i][4].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','P', 'SA','" + dtExcelData.Rows[i][20].ToString() + "', '', '', '','', 'N','Y')";
                                        Status = Dblog.Ora_Execute_CommamdText(spsql);
                                    }
                                    }
                                    
                                
                             
                            }

                            if (Status != "SUCCESS")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('" + dtExcelData.Rows[i][7].ToString() + "');", true);
                            }
                        }


                        instaudt();
                        UPLOAD();
                        Page.ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('Rekod Berjaya Dimuatnaik.'); window.location ='MAK_POT_MAJI.aspx';</script>");

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('FILE NOT SUPPORT');", true);
                    }

                }

            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Format fail ST Tidak Memenuhi Kriteria. Sila Semak Semula');", true);
                throw ex;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila pilih fail berkenaan');", true);
        }
    }
}