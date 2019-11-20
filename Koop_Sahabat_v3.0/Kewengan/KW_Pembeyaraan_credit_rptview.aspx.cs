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
using System.Collections;
using System.Web.SessionState;
using System.Threading;
using System.Reflection;

public partial class KW_Pembeyaraan_credit_rptview : System.Web.UI.Page
{
    DBConnection Dbcon = new DBConnection();
    string query1 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                string nameshbt = Session["dbtno"].ToString();
                if (nameshbt != "")
                {
                    DataTable dt = new DataTable();
                    query1 = "select tb1.no_notakredit vv1,tb3.syar_logo v1, tb3.nama_syarikat v2,tb3.kod_syarikat v3, tb3.alamat_syarikat v4, tb3.syar_nombo_teleph v5,tb3.syar_nombo_fax v6, "
                          + " (tb4.Ref_nama_syarikat +' & '+ tb4.Ref_no_syarikat) v7,Ref_alamat v8, Ref_no_telefone v9, Ref_no_fax v10, Ref_kawalan v11,tb1.no_notakredit v12,  "
                          + " Format(tb6.tarikh_invois,'dd/MM/yyyy') v13, tb1.no_invois v14,Format(tb1.tarikh_kredit,'dd/MM/yyyy') v15,tb1.Terma v16,tb1.perkara v17,tb5.Bank_Name v18,tb1.inv_noacc_bank v19,tb1.Overall v20, "
                          + " tb2.keterangan v21,tb2.Unit v22,tb2.quantiti v23,tb2.jumlah v24,tb2.gstjumlah v25,tb2.overall v26,tb2.discount v27 "
                          + "from KW_Penerimaan_Credit tb1 left join KW_Penerimaan_Credit_item tb2 on tb2.no_notakredit=tb1.no_notakredit "
                          + "left join KW_Penerimaan_invois tb6 on tb6.no_invois=tb1.no_invois and tb6.Status ='A' "
                          + "  left join KW_Profile_syarikat tb3 on tb3.kod_syarikat=tb1.id_profile_syarikat and tb3.Status='A' and cur_sts='1' "
                          + " left join KW_Ref_Pelanggan tb4 on tb4.Ref_no_syarikat=tb1.nama_pelanggan_code "
                          + " left join Ref_Nama_Bank tb5 on tb5.Bank_Code=tb1.inv_bank  where tb1.Status = 'A' and tb1.no_notakredit ='"+ nameshbt + "'";
                    dt = Dbcon.Ora_Execute_table(query1);

                    rptcredit.Reset();

                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();

                    if (countRow != 0)
                    {

                        string imagePath = string.Empty;
                        if (dt.Rows[0]["v1"].ToString() != "")
                        {
                            imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/" + dt.Rows[0]["v1"].ToString() + "")).AbsoluteUri;

                        }
                        else
                        {
                            imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/user.png")).AbsoluteUri;
                        }

                        txtError.Text = "";
                        //Display Report
                        //rptcredit.LocalReport.DataSources.Clear();


                        ReportDataSource rds = new ReportDataSource("kwkredit", dt);


                        //Path
                        if (Session["rpt_lang"].ToString() == "mal")
                        {
                            rptcredit.LocalReport.ReportPath = "kewengan/KW_Kredit_Note.rdlc";
                        }
                        else
                        {
                            rptcredit.LocalReport.ReportPath = "kewengan/KW_Kredit_Note_eng.rdlc";
                        }
                        rptcredit.LocalReport.EnableExternalImages = true;
                        

                        //Parameters
                        ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("v1",imagePath)
                   
                     };


                        rptcredit.LocalReport.SetParameters(rptParams);
                        rptcredit.LocalReport.DataSources.Add(rds);
                        //ReportViewer1.LocalReport.ExecuteReportInCurrentAppDomain(AppDomain.CurrentDomain.Evidence);
                        //Refresh
                        rptcredit.LocalReport.Refresh();

                        Warning[] warnings;

                        string[] streamids;

                        string mimeType;

                        string encoding;

                        string extension;

                        //string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
                        //       "  <PageWidth>12.20in</PageWidth>" +
                        //        "  <PageHeight>8.27in</PageHeight>" +
                        //        "  <MarginTop>0.1in</MarginTop>" +
                        //        "  <MarginLeft>0.5in</MarginLeft>" +
                        //         "  <MarginRight>0in</MarginRight>" +
                        //         "  <MarginBottom>0in</MarginBottom>" +
                        //       "</DeviceInfo>";

                        byte[] bytes = rptcredit.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = mimeType;
                        Response.AddHeader("content-disposition", "inline; filename=myfile." + extension);
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                txtError.Text = ex.ToString();

            }

        }
    }
}