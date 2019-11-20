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


public partial class PEN_MAK_DIV : System.Web.UI.Page
{
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable wilayah = new DataTable();
    DataTable caw = new DataTable();
    DBConnection dbcon = new DBConnection();
    DataTable pusat = new DataTable();
    DataTable dt = new DataTable();
    DataTable ddtt = new DataTable();
    DataTable Mpcheck = new DataTable();
    DataTable MpWil = new DataTable();
    DataTable MpWila = new DataTable();
    DBConnection DBCon = new DBConnection();
    DataSet ds = new DataSet();
    StudentWebService service = new StudentWebService();
    string status;
    DateTime dmula;
    string bcode;
    string wcode;
    string sno;
    string ccode;
    string Status = string.Empty;
    string level, userid;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                Batch();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void Batch()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select div_batch_name from mem_divident where ISNULL(div_approve_id,'') ='' and  Acc_sts='Y' group by div_batch_name order by div_batch_name";
            //string com = "select div_batch_name from mem_divident group by div_batch_name order by div_batch_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            adpt.SelectCommand.CommandTimeout = 180;
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "div_batch_name";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    void app_language()
    {
        if (Session["New"] != null)
        {
            assgn_roles();
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('131','1052','1079','153','66','1080','481','36','883')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;



            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());

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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0138' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();

                if (role_add == "1")
                {
                    //Button2.Visible = true;
                    Button5.Visible = true;
                    Button6.Visible = true;
                }
                else
                {
                    Button2.Visible = false;
                    Button5.Visible = false;
                    Button6.Visible = false;
                }

                if (role_edit == "1")
                {
                    Button4.Visible = true;
                }
                else
                {
                    Button4.Visible = false;
                }

            }
        }
    }

    //public void showreport()
    //{
    //    try
    //    {

    //        DataTable dt = new DataTable();
    //        dt = DBCon.Ora_Execute_table("select a.mem_new_icno,a.mem_name,a.mem_centre,a.mem_sahabat_no,a.mem_address,sum(a.div_debit_amt) div_debit_amt,b.Wilayah_Name,c.branch_desc as cawangan_name from (select mm.mem_new_icno,mm.mem_name,mm.mem_centre,mm.mem_sahabat_no,mm.mem_address,sum(md.div_debit_amt) as div_debit_amt,mm.mem_region_cd,mm.mem_branch_cd,md.div_batch_name  from mem_member mm inner join mem_divident md on md.div_new_icno=mm.mem_new_icno and md.Acc_sts ='Y' where mm.Acc_sts ='Y' group by mem_new_icno,mem_name,mem_centre,mem_sahabat_no,mem_address,mm.mem_region_cd,mm.mem_branch_cd,md.div_batch_name) a left join (select Wilayah_Code,Wilayah_Name from Ref_Wilayah rw )b on a.mem_region_cd=b.Wilayah_Code left join (select branch_cd,branch_desc from ref_branch rw )c on a.mem_branch_cd=c.branch_cd where a.div_batch_name='" + txtnama.Text + "'  group by  a.mem_new_icno,a.mem_name,a.mem_centre,a.mem_sahabat_no,a.mem_address,b.Wilayah_Name,c.branch_desc Order by b.Wilayah_Name,Cawangan_name,mem_centre,mem_new_icno");

    //        //Reset
    //        ReportViewer2.Reset();

    //        List<DataRow> listResult = dt.AsEnumerable().ToList();
    //        listResult.Count();
    //        int countRow = 0;
    //        countRow = listResult.Count();

    //        if (countRow != 0)
    //        {

    //            txtError.Text = "";
    //            //Display Report
    //            ReportDataSource rds = new ReportDataSource("DIV_MAK", dt);

    //            ReportViewer2.LocalReport.DataSources.Add(rds);

    //            //Path
    //            ReportViewer2.LocalReport.ReportPath = "keanggotan/Mak_Div.rdlc";
    //            //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

    //            //Parameters
    //            ReportParameter[] rptParams = new ReportParameter[]{
    //                 new ReportParameter("Kelompok",txtnama.Text ),
    //                 new ReportParameter("current_date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
    //                 //new ReportParameter("toDate",ToDate .Text )
    //                 //new ReportParameter("fromDate",dmula.ToShortDateString()  ),
    //                 //new ReportParameter("toDate",dakhir.ToShortDateString() )
    //                 };


    //            ReportViewer2.LocalReport.SetParameters(rptParams);

    //            //Refresh
    //            ReportViewer2.LocalReport.Refresh();
    //            System.Threading.Thread.Sleep(1);
    //        }
    //        else if (countRow == 0)
    //        {                
    //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        txtError.Text = ex.ToString();
    //    }

    //}

    public void showreport_test()
    {
        try
        {

            string script = "$(function () { $('[id*=GridView1]').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); $('.select2').select2() });";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
            string fno = "010702";
            string fname = "PENGESAHAN MAKLUMAT DIVIDEN";
            DataTable dt = new DataTable();
            dt = DBCon.Ora_Execute_table("select a.mem_new_icno,a.mem_name,a.mem_centre,a.mem_sahabat_no,a.mem_address,sum(ISNULL(a.div_debit_amt,'0.00')) div_debit_amt,sha_amt as jumlah,b.Wilayah_Name,c.branch_desc as cawangan_name,a.div_bank_acc_no,c1.Bank_Name from (select mm.mem_new_icno,mm.mem_name,mm.mem_centre,mm.mem_member_no as mem_sahabat_no,mm.mem_address,sum(ISNULL(md.div_debit_amt,'0.00')) as div_debit_amt,sha_amt,mm.mem_region_cd,mm.mem_branch_cd,md.div_batch_name,div_bank_cd,div_bank_acc_no  from mem_member mm inner join mem_divident md on md.div_new_icno=mm.mem_new_icno and md.Acc_sts ='Y'  where mm.Acc_sts ='Y' group by mem_new_icno,mem_name,mem_centre,mem_member_no,sha_amt,mem_address,mm.mem_region_cd,mm.mem_branch_cd,md.div_batch_name,div_bank_cd,div_bank_acc_no) a left join (select Wilayah_Code,Wilayah_Name from Ref_Wilayah rw )b on a.mem_region_cd=b.Wilayah_Code left join (select branch_cd,branch_desc from ref_branch rw )c on a.mem_branch_cd=c.branch_cd left join (select rnb.Bank_Code,rnb.Bank_Name from Ref_Nama_Bank rnb)c1 on a.div_bank_cd=c1.Bank_Code  where a.div_batch_name='" + DropDownList1.SelectedItem.Text + "'  group by  a.mem_new_icno,a.mem_name,a.mem_centre,a.mem_sahabat_no,sha_amt,a.mem_address,b.Wilayah_Name,c.branch_desc,a.div_bank_acc_no,c1.Bank_Name Order by b.Wilayah_Name,Cawangan_name,mem_centre,mem_new_icno");
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                GridView1.DataSource = dt;
                GridView1.DataBind();
                int columncount = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
                Button5.Visible = false;
                Button6.Visible = false;
            }
            else
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Button5.Visible = true;
                Button6.Visible = true;

            }


        }

        catch (Exception ex)
        {

        }
        
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text != "--- PILIH ---")
        {
            showreport_test();

        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nama Kelompok.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
      
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void ExportToPDF(object sender, EventArgs e)
    {

        try
        {

            if (DropDownList1.SelectedValue != "")
            {
                string fno = "010702";
                string fname = "PENGESAHAN MAKLUMAT DIVIDEN";
                DataTable dt = new DataTable();
                dt = DBCon.Ora_Execute_table("select row_number() OVER (ORDER BY c.kavasan_name,c.Wilayah_Name,branch_desc,mem_centre,mem_new_icno) as Id,a.mem_new_icno,a.mem_name,a.mem_centre,a.mem_sahabat_no,a.mem_address, sum(a.div_debit_amt) div_debit_amt,sha_amt as jumlah,c.kavasan_name,c.Wilayah_Name,c.branch_desc as cawangan_name,a.div_bank_acc_no,c1.Bank_Name from (select mm.mem_new_icno,mm.mem_name,mm.mem_centre,mm.mem_member_no as mem_sahabat_no,mm.mem_address,sum(md.div_debit_amt) as div_debit_amt,sha_amt,mm.mem_region_cd,mm.mem_branch_cd,md.div_batch_name,div_bank_cd,div_bank_acc_no  from mem_member mm inner join mem_divident md on md.div_new_icno=mm.mem_new_icno and md.Acc_sts ='Y'  where mm.Acc_sts ='Y' group by mem_new_icno,mem_name,mem_centre,mem_member_no,sha_amt,mem_address,mm.mem_region_cd,mm.mem_branch_cd,md.div_batch_name,div_bank_cd,div_bank_acc_no) a left join (select kavasan_name,wilayah_name,cawangan_code branch_cd, cawangan_name branch_desc from Ref_Cawangan rw where Isnull(cawangan_code,'') !='')c on a.mem_branch_cd=c.branch_cd left join (select rnb.Bank_Code,rnb.Bank_Name from Ref_Nama_Bank rnb)c1 on a.div_bank_cd=c1.Bank_Code  where a.div_batch_name='" + DropDownList1.SelectedItem.Text + "'  group by  a.mem_new_icno,a.mem_name,a.mem_centre,a.mem_sahabat_no,sha_amt,a.mem_address,c.kavasan_name,c.Wilayah_Name,c.branch_desc,a.div_bank_acc_no,c1.Bank_Name Order by c.kavasan_name,c.Wilayah_Name,branch_desc,mem_centre,mem_new_icno");

                // Reset
                ReportViewer2.Reset();

                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

                if (countRow != 0)
                {
                    string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc)values('" + Session["New"].ToString() + "','" + DateTime.Now.ToString() + "','" + fno + "','" + fname + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql);
                    txtError.Text = "";
                    //Display Report
                    ReportDataSource rds = new ReportDataSource("DIV_MAK", dt);

                    ReportViewer2.LocalReport.DataSources.Add(rds);

                    //Path
                    ReportViewer2.LocalReport.ReportPath = "keanggotan/Mak_Div.rdlc";
                    //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                    //Parameters
                    ReportParameter[] rptParams = new ReportParameter[]{
                         new ReportParameter("Kelompok",DropDownList1.SelectedItem.Text ),
                         new ReportParameter("current_date", DateTime.Now.ToString())
                         //new ReportParameter("toDate",ToDate .Text )
                         //new ReportParameter("fromDate",dmula.ToShortDateString()  ),
                         //new ReportParameter("toDate",dakhir.ToShortDateString() )
                         };


                    ReportViewer2.LocalReport.SetParameters(rptParams);

                    //Refresh
                    ReportViewer2.LocalReport.Refresh();
                    string filename = string.Format("{0}.{1}", "PENGESAHAN_DIVIDEN_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                    ReportViewer2.LocalReport.DisplayName = "PENGESAHAN_DIVIDEN_" + DateTime.Now.ToString("ddMMyyyy");
                    //}
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;

                    byte[] bytes = ReportViewer2.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
                else if (countRow == 0)
                {
                    txtError.Text = "Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.";
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nama Kelompok.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }

        catch (Exception ex)
        {

        }
    }

    protected void ExportToEXCEL(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        decimal count1 = 0, count2 = 0;

        if (DropDownList1.SelectedValue != "")
        {
            DataTable dt = new DataTable();
            dt = DBCon.Ora_Execute_table("select row_number() OVER (ORDER BY c.kavasan_name,c.Wilayah_Name,branch_desc,mem_centre,mem_new_icno) as Id,a.mem_new_icno,a.mem_name,a.mem_centre,a.mem_sahabat_no,a.mem_address, sum(a.div_debit_amt) div_debit_amt,sha_amt as jumlah,c.kavasan_name,c.Wilayah_Name,c.branch_desc as cawangan_name,a.div_bank_acc_no,c1.Bank_Name from (select mm.mem_new_icno,mm.mem_name,mm.mem_centre,mm.mem_member_no as mem_sahabat_no,mm.mem_address,sum(md.div_debit_amt) as div_debit_amt,sha_amt,mm.mem_region_cd,mm.mem_branch_cd,md.div_batch_name,div_bank_cd,div_bank_acc_no  from mem_member mm inner join mem_divident md on md.div_new_icno=mm.mem_new_icno and md.Acc_sts ='Y'  where mm.Acc_sts ='Y' group by mem_new_icno,mem_name,mem_centre,mem_member_no,sha_amt,mem_address,mm.mem_region_cd,mm.mem_branch_cd,md.div_batch_name,div_bank_cd,div_bank_acc_no) a left join (select kavasan_name,wilayah_name,cawangan_code branch_cd, cawangan_name branch_desc from Ref_Cawangan rw where Isnull(cawangan_code,'') !='')c on a.mem_branch_cd=c.branch_cd left join (select rnb.Bank_Code,rnb.Bank_Name from Ref_Nama_Bank rnb)c1 on a.div_bank_cd=c1.Bank_Code  where a.div_batch_name='" + DropDownList1.SelectedItem.Text + "'  group by  a.mem_new_icno,a.mem_name,a.mem_centre,a.mem_sahabat_no,sha_amt,a.mem_address,c.kavasan_name,c.Wilayah_Name,c.branch_desc,a.div_bank_acc_no,c1.Bank_Name Order by c.kavasan_name,c.Wilayah_Name,branch_desc,mem_centre,mem_new_icno");


            if (dt.Rows.Count != 0)
            {


                StringBuilder builder = new StringBuilder();
                string strFileName = string.Format("{0}.{1}", "PENGESAHAN_DIVIDEN_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                builder.Append("NO KP BARU, NAMA, NO ANGGOTA,KAWASAN, WILAYAH, CAWANGAN, PUSAT,ALAMAT, NO. AKAUN BANK,NAMA BANK, AMAUN SYER(RM),AMAUN DIVIDEN(RM)" + Environment.NewLine);
                for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                {
                    count1 += Convert.ToDecimal(dt.Rows[k]["jumlah"].ToString());
                    count2 += Convert.ToDecimal(dt.Rows[k]["div_debit_amt"].ToString());
                    builder.Append(dt.Rows[k]["mem_new_icno"].ToString() + " , " + dt.Rows[k]["mem_name"].ToString() + "," + dt.Rows[k]["mem_sahabat_no"].ToString() + "," + dt.Rows[k]["kavasan_name"].ToString() + "," + dt.Rows[k]["Wilayah_Name"].ToString() + "," + dt.Rows[k]["cawangan_name"].ToString() + "," + dt.Rows[k]["mem_centre"].ToString() + "," + dt.Rows[k]["mem_address"].ToString().Replace(",", "").Replace("\r", " ").Replace("\n", " ") + "," + dt.Rows[k]["div_bank_acc_no"].ToString() + "," + dt.Rows[k]["Bank_Name"].ToString() + "," + dt.Rows[k]["jumlah"].ToString() + "," + dt.Rows[k]["div_debit_amt"].ToString() + Environment.NewLine);
                    if (k == (dt.Rows.Count - 1))
                    {
                        builder.Append(", , , , , , , ,JUMLAH (RM)," + count1 + "," + count2 + Environment.NewLine);
                    }
                }
                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                Response.Write(builder.ToString());
                Response.End();

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nama Kelompok.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

        string script = " $(function () {$(" + GridView1.ClientID + ") .prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({'responsive': true,'sPaginationType': 'full_numbers',  'iDisplayLength': 15,'aLengthMenu': [[15, 30, 50, 100], [15, 30, 50, 100]]});});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }
    //protected void ExportToEXCEL(object sender, EventArgs e)
    //{
    //    //Export("", gvCustomers);
    //    GridView1.Visible = true;

    //    string FileName = "PEN_MAK_DIV_(" + DateTime.Now.AddHours(5).ToString("yyyy-MM-dd") + ").xls";

    //    Response.Clear();
    //    Response.Buffer = true;

    //    Response.AddHeader("content-disposition",
    //     "attachment;filename=" + FileName);
    //    Response.Charset = "";
    //    Response.ContentType = "application/vnd.ms-excel";
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter hw = new HtmlTextWriter(sw);

    //    GridView1.HeaderRow.Style.Add("color", "#FFFFFF");
    //    GridView1.HeaderRow.Style.Add("background-color", "#1F437D");

    //    for (int i = 0; i < GridView1.Rows.Count; i++)
    //    {
    //        GridViewRow row = GridView1.Rows[i];
    //        row.BackColor = System.Drawing.Color.White;
    //        row.Attributes.Add("class", "textmode");
    //        if (i % 2 != 0)
    //        {
    //            for (int j = 0; j < row.Cells.Count; j++)
    //            {
    //                //row.Cells[j].Style.Add("background-color", "#eff3f8");
    //            }
    //        }
    //    }

    //    GridView1.RenderControl(hw);
    //    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
    //    Response.Write(style);
    //    Response.Output.Write(sw.ToString());

    //    Response.End();
    //    GridView1.Visible = false;
    //}
    protected void Hapus_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList1.SelectedItem.Text != "--- PILIH ---")
            {
                string Inssql_delete = "Delete from mem_divident where div_batch_name='" + DropDownList1.SelectedItem.Text + "' and Acc_sts ='Y'";
                Status = dbcon.Ora_Execute_CommamdText(Inssql_delete);
                if (Status == "SUCCESS")
                {
                    service.audit_trail("P0138", "Hapus","Nama Kelompok", DropDownList1.SelectedItem.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan',{'type': 'confirmation','title': 'Success'});window.location ='../keanggotan/PEN_MAK_DIV.aspx';", true);
                }
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Enter Nama Kelompok.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Error.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
}