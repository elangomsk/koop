using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Forms;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web.SessionState;
using System.Xml;


public partial class PP_Cetak_pt : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);

    DBConnection DBCon = new DBConnection();

    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    //int total = 0;
    //int total1 = 0;
    //int total2 = 0;
    //int total3 = 0;
    decimal total = 0M, total1 = 0M, total2 = 0M, total3 = 0M;

    string icno = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button1);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                MP_nama.Attributes.Add("Readonly", "Readonly");
                MP_icno.Attributes.Add("Readonly", "Readonly");
                //MP_wilayah.Attributes.Add("Readonly", "Readonly");
                //MP_cawangan.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                TextBox1.Attributes.Add("Readonly", "Readonly");
                grid();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select app_applcn_no from jpa_application JA Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no where app_applcn_no like '%' + @Search + '%' and JJA.jkk_result_ind='L' and JA.applcn_clsed ='N'";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    if (sdr.HasRows == true)
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["app_applcn_no"].ToString());

                        }
                    }
                    else
                    {
                        countryNames.Add("Rekod Tidak Dijumpai.");
                    }
                }

                con.Close();
                return countryNames;
            }
        }
    }

    protected void btnsrch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {

                DataTable select_app = new DataTable();
                select_app = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,ja.app_sts_cd,ISNULL(ja.app_loan_amt,'') as app_loan_amt,ja.appl_loan_dur from jpa_application as JA  Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Applcn_no.Text + "' and ja.app_sts_cd= 'Y'");
                if (select_app.Rows.Count == 0)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    grid();
                }
                else
                {
                    if (Applcn_no.Text != "")
                    {
                        icno = select_app.Rows[0]["app_applcn_no"].ToString();
                    }
                    else
                    {
                        icno = "";
                    }

                    MP_nama.Text = select_app.Rows[0]["app_name"].ToString();
                    MP_icno.Text = select_app.Rows[0]["app_new_icno"].ToString();
                    //MP_wilayah.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                    //MP_cawangan.Text = select_app.Rows[0]["branch_desc"].ToString();
                    decimal amt = (decimal)select_app.Rows[0]["app_loan_amt"];
                    TextBox1.Text = amt.ToString("C").Replace("$", "");
                    TextBox2.Text = select_app.Rows[0]["appl_loan_dur"].ToString();
                    grid();
                }
            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }


    protected void BindGridview(object sender, EventArgs e)
    {
        gvSelected.Visible = true;
        grid();
    }

    protected void grid()
    {

        gvSelected.Visible = true;
        con.Open();
        SqlCommand cmd = new SqlCommand("select jt.txn_applcn_no,jt.txn_cd,ISNULL(jt.txn_credit_amt,'') as txn_credit_amt,ISNULL(jt.txn_bal_amt,'') as txn_bal_amt,ISNULL(jt.txn_debit_amt,'') as txn_debit_amt,ISNULL(jt.txn_gst_amt,'') as txn_gst_amt,jt.txn_crt_dt,rt.txn_description from jpa_transaction as jt left join Ref_jpa_txn as rt on rt.txn_cd=jt.txn_cd where txn_applcn_no='" + icno + "' ORDER BY txn_crt_dt", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {

            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            int columncount = gvSelected.Rows[0].Cells.Count;
            gvSelected.Rows[0].Cells.Clear();
            gvSelected.Rows[0].Cells.Add(new TableCell());
            gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
            gvSelected.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            gvSelected.FooterRow.Cells[3].Text = "<strong>JUMLAH (RM)</strong>";
            gvSelected.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;

        }
        else
        {
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            gvSelected.FooterRow.Cells[3].Text = "<strong>JUMLAH (RM)</strong>";
            gvSelected.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;

        }
        con.Close();
    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        //gvSelected.DataBind();
    }



    //protected void update_click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Applcn_no.Text != "")
    //        {

    //            foreach (GridViewRow gvrow in gvSelected.Rows)
    //            {
    //                //var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
    //                //if (checkbox.Checked == true)
    //                //{
    //                var lblID = gvrow.FindControl("app_no") as System.Web.UI.WebControls.Label;
    //                var seqno = gvrow.FindControl("seq_no") as System.Web.UI.WebControls.Label;
    //                var ph_no1 = gvrow.FindControl("Label3") as System.Web.UI.WebControls.Label;

    //                System.Web.UI.WebControls.TextBox box1 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Label2");
    //                System.Web.UI.WebControls.TextBox box2 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Label4");
    //                System.Web.UI.WebControls.TextBox box3 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Label5");
    //                DropDownList ddl = (DropDownList)gvrow.FindControl("Bank_details");
    //                DropDownList ddl_cb = (DropDownList)gvrow.FindControl("CB_details");

    //                DBCon.Execute_CommamdText("Update jpa_disburse SET pha_name='" + box1.Text + "', pha_reg_no='" + box2.Text + "', pha_bank_acc_no='" + box3.Text + "',pha_bank_cd='" + ddl.SelectedValue + "',pha_pay_type_cd='" + ddl_cb.SelectedValue + "',pha_upd_id='" + Session["New"].ToString() + "',pha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE pha_applcn_no='" + lblID.Text + "' and pha_phase_no = '" + ph_no1.Text + "' and pha_seqno='" + seqno.Text + "'");
    //                //}

    //            }
    //            Ins_aud();
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Rekod Berjaya Disimpan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

    //    }
    //}


    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (DataBinder.Eval(e.Row.DataItem, "txn_bal_amt") != DBNull.Value)
            //{
            //    total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "txn_bal_amt"));
            //}
            if (DataBinder.Eval(e.Row.DataItem, "txn_gst_amt") != DBNull.Value)
            {
                total1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "txn_gst_amt"));
            }
            if (DataBinder.Eval(e.Row.DataItem, "txn_credit_amt") != DBNull.Value)
            {
                total2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "txn_credit_amt"));
            }
            if (DataBinder.Eval(e.Row.DataItem, "txn_debit_amt") != DBNull.Value)
            {
                total3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "txn_debit_amt"));
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //System.Web.UI.WebControls.Label lblamount = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal");
            //lblamount.Text = total.ToString("C").Replace("$", "");
            System.Web.UI.WebControls.Label lblamount1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal_Gst");
            lblamount1.Text = total1.ToString("C").Replace("$", "");
            System.Web.UI.WebControls.Label lblamount2 = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal_credit");
            lblamount2.Text = total2.ToString("C").Replace("$", "");
            System.Web.UI.WebControls.Label lblamount3 = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal_debit");
            lblamount3.Text = total3.ToString("C").Replace("$", "");
            //MPP_jb.Text = total.ToString("0.00");
        }
    }

    protected void click_print(object sender, EventArgs e)
    {
        {
            try
            {
                //grid();
                if (Applcn_no.Text != "")
                {
                    //Path

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    //dt = DBCon.Ora_Execute_table("select jt.txn_applcn_no,jt.txn_cd,ISNULL(jt.txn_credit_amt,'') as txn_credit_amt,ISNULL(jt.txn_bal_amt,'') as txn_bal_amt,ISNULL(jt.txn_debit_amt,'') as txn_debit_amt,ISNULL(jt.txn_gst_amt,'') as txn_gst_amt,jt.txn_crt_dt,rt.txn_description,sum(ISNULL(jt.txn_credit_amt,'')) as tc_amt,sum(ISNULL(jt.txn_bal_amt,'')) as tb_amt,sum(ISNULL(jt.txn_debit_amt,'')) as td_amt,sum(ISNULL(jt.txn_gst_amt,'')) as tg_amt from jpa_transaction as jt left join Ref_jpa_txn as rt on rt.txn_cd=jt.txn_cd where txn_applcn_no='"+Applcn_no.Text+"' group by jt.txn_applcn_no,jt.txn_bal_amt,jt.txn_credit_amt,jt.txn_debit_amt,jt.txn_gst_amt,rt.txn_description,jt.txn_cd,jt.txn_crt_dt");
                    dt = DBCon.Ora_Execute_table("select tc_amt,tb_amt,td_amt,tg_amt,ISNULL(txn_applcn_no,'') as txn_applcn_no,ISNULL(txn_cd,'') txn_cd, ISNULL(txn_credit_amt,'') txn_credit_amt,ISNULL(txn_bal_amt,'') txn_bal_amt,ISNULL(txn_debit_amt,'') txn_debit_amt,ISNULL(txn_gst_amt,'') txn_gst_amt,ISNULL(txn_crt_dt,'') txn_crt_dt,ISNULL(txn_description,'') txn_description from (select ISNULL(sum(jt.txn_credit_amt),'') as tc_amt,ISNULL(sum(jt.txn_bal_amt),'') as tb_amt,ISNULL(sum(jt.txn_debit_amt),'') as td_amt,ISNULL(sum(jt.txn_gst_amt),'') as tg_amt from jpa_transaction as jt where jt.txn_applcn_no='" + Applcn_no.Text + "') as a full outer join(select jt.txn_applcn_no,jt.txn_cd,ISNULL(jt.txn_credit_amt,'') as txn_credit_amt,ISNULL(jt.txn_bal_amt,'') as txn_bal_amt,ISNULL(jt.txn_debit_amt,'') as txn_debit_amt,ISNULL(jt.txn_gst_amt,'') as txn_gst_amt,jt.txn_crt_dt,rt.txn_description from jpa_transaction as jt left join Ref_jpa_txn as rt on rt.txn_cd=jt.txn_cd where txn_applcn_no='" + Applcn_no.Text + "') as b on b.txn_applcn_no='" + Applcn_no.Text + "' ORDER BY b.txn_crt_dt");
                    Rptviwer_lt.Reset();
                    ds.Tables.Add(dt);

                    Rptviwer_lt.LocalReport.DataSources.Clear();

                    Rptviwer_lt.LocalReport.ReportPath = "PELABURAN_ANGGOTA/transaksi.rdlc";
                    ReportDataSource rds = new ReportDataSource("pp_transaksi", dt);

                    Rptviwer_lt.LocalReport.DataSources.Add(rds);
                    DataTable applcn = new DataTable();
                    //applcn = DBCon.Ora_Execute_table("select ja.app_bank_acc_no,ja.app_applcn_no,ja.app_name,ja.app_permnt_address,ja.app_permnt_postcode,rn.Decription from jpa_application ja left join Ref_Negeri rn on rn.Decription_Code=ja.app_permnt_state_cd where app_applcn_no='"+Applcn_no.Text+"'");
                    applcn = DBCon.Ora_Execute_table("select ja.app_bank_acc_no,ja.app_applcn_no,ja.app_name,ja.app_mailing_address,ja.app_permnt_postcode,rn.Decription,(ja.app_loan_amt + cf.cal_profit_amt) as jum_amt,jp.Description as l_desc from jpa_application ja left join Ref_Negeri rn on rn.Decription_Code=ja.app_permnt_state_cd left join jpa_calculate_fee as cf on cf.cal_applcn_no=ja.app_applcn_no left join Ref_Jenis_Pelaburan as jp on jp.Description_Code=ja.app_loan_type_cd where app_applcn_no='" + Applcn_no.Text + "'");
                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("no_akaun", applcn.Rows[0]["app_bank_acc_no"].ToString()),
                     new ReportParameter("no_applcn", applcn.Rows[0]["app_applcn_no"].ToString()),
                     new ReportParameter("tarikh", DateTime.Now.ToString("dd/MM/yyyy")),
                     new ReportParameter("name", applcn.Rows[0]["app_name"].ToString()),
                     new ReportParameter("address", applcn.Rows[0]["app_mailing_address"].ToString()),
                     new ReportParameter("s_code", applcn.Rows[0]["app_permnt_postcode"].ToString()),
                     new ReportParameter("state", applcn.Rows[0]["Decription"].ToString()),
                      new ReportParameter("jumamt",applcn.Rows[0]["jum_amt"].ToString()),
                       new ReportParameter("loandesc", applcn.Rows[0]["l_desc"].ToString())
                     };


                    Rptviwer_lt.LocalReport.SetParameters(rptParams);

                    //Refresh
                    Rptviwer_lt.LocalReport.Refresh();
                    Warning[] warnings;

                    string[] streamids;

                    string mimeType;

                    string encoding;

                    string extension;

                    string fname = DateTime.Now.ToString("dd_MM_yyyy");

                    string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
                           "  <PageWidth>12.20in</PageWidth>" +
                            "  <PageHeight>8.27in</PageHeight>" +
                            "  <MarginTop>0.1in</MarginTop>" +
                            "  <MarginLeft>0.5in</MarginLeft>" +
                             "  <MarginRight>0in</MarginRight>" +
                             "  <MarginBottom>0in</MarginBottom>" +
                           "</DeviceInfo>";

                    byte[] bytes = Rptviwer_lt.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                    Response.Buffer = true;

                    Response.Clear();

                    Response.ClearHeaders();

                    Response.ClearContent();

                    Response.ContentType = "application/pdf";


                    Response.AddHeader("content-disposition", "attachment; filename=CETAK_PENYATA_TRANSAKSI_" + Applcn_no.Text + "." + extension);

                    Response.BinaryWrite(bytes);

                    //Response.Write("<script>");
                    //Response.Write("window.open('', '_newtab');");
                    //Response.Write("</script>");
                    Response.Flush();

                    Response.End();
                }
                else
                {
                    grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

  
}