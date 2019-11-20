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


public partial class PP_Pambayaran_Caj : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    int total = 0;
    int total1 = 0;
    int total2 = 0;
    int total3 = 0;
    string selphase_no;
    string pay_date;
    string icno = string.Empty;
    string phdate;
    string txn_code = string.Empty;
    string post, late;
    string tcode, ct;
    string tamt = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {

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
                TextBox3.Attributes.Add("Readonly", "Readonly");
                TextBox4.Attributes.Add("Readonly", "Readonly");
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
                select_app = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,ja.app_sts_cd,ISNULL(ja.app_loan_amt,'') as app_loan_amt,ja.appl_loan_dur,ISNULL(ja.app_bal_loan_amt,'') as app_bal_loan_amt,ISNULL(ja.app_backdated_amt,'') as app_backdated_amt from jpa_application as JA  Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where ja.app_sts_cd= 'Y' and '" + Applcn_no.Text + "' IN(JA.app_applcn_no, JA.app_new_icno)");
                if (select_app.Rows.Count == 0)
                {
                    Applcn_no.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
                else
                {

                    MP_nama.Text = select_app.Rows[0]["app_name"].ToString();
                    MP_icno.Text = select_app.Rows[0]["app_new_icno"].ToString();
                    //MP_wilayah.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                    //MP_cawangan.Text = select_app.Rows[0]["branch_desc"].ToString();
                    decimal amt = (decimal)select_app.Rows[0]["app_loan_amt"];
                    TextBox1.Text = amt.ToString("0.00");
                    TextBox2.Text = select_app.Rows[0]["appl_loan_dur"].ToString();
                    decimal amt1 = (decimal)select_app.Rows[0]["app_bal_loan_amt"];
                    TextBox3.Text = amt1.ToString("0.00");
                    decimal amt2 = (decimal)select_app.Rows[0]["app_backdated_amt"];
                    TextBox4.Text = amt2.ToString("0.00");
                    if (TextBox4.Text != "0.00")
                    {
                        Button7.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Pelanggan Perlu Lunaskan Bayaran Tunggakan Terlebih Dahulu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                grid();
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


    protected void Bayar_click(object sender, EventArgs e)
    {
        try
        {
            int counter = 0;

            if (Applcn_no.Text != "")
            {
                foreach (GridViewRow gvrow1 in gvSelected.Rows)
                {
                    var cb = gvrow1.FindControl("chkStatus") as System.Web.UI.WebControls.CheckBox;
                    if (cb.Checked)
                    {
                        counter = counter + 1;
                    }
                }
                if (counter != 0)
                {
                    foreach (GridViewRow gvrow in gvSelected.Rows)
                    {
                        var a_no = (System.Web.UI.WebControls.Label)gvrow.FindControl("app_icno");
                        var a_id = (System.Web.UI.WebControls.Label)gvrow.FindControl("app_id");
                        var cp_amt = (System.Web.UI.WebControls.Label)gvrow.FindControl("Label2");
                        var cl_amt = (System.Web.UI.WebControls.Label)gvrow.FindControl("Label4");
                        var cg_amt = (System.Web.UI.WebControls.Label)gvrow.FindControl("Label41");

                        //if (c_dte.Text != "")
                        //{
                        var checkbox = gvrow.FindControl("chkStatus") as System.Web.UI.WebControls.CheckBox;

                        //counter = counter + 1;


                        if (checkbox.Checked == true)
                        {
                            DBCon.Execute_CommamdText("UPDATE jpa_charge set caj_postage_pay_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',caj_postage_pay_id='" + Session["New"].ToString() + "',caj_late_pay_pay_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',caj_late_pay_pay_id='" + Session["New"].ToString() + "',caj_upd_id='" + Session["New"].ToString() + "',caj_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where caj_applcn_no='" + a_no.Text + "' and ID='" + a_id.Text + "'");
                            if (cg_amt.Text != "0.00" && cp_amt.Text == "0.00" && cl_amt.Text == "0.00")
                            {
                                DBCon.Execute_CommamdText("UPDATE jpa_lawyer_fee set lfe_pay_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',lfe_pay_id='" + Session["New"].ToString() + "',lfe_upd_id='" + Session["New"].ToString() + "',lfe_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where lfe_applcn_no='" + a_no.Text + "' and ID='" + a_id.Text + "'");
                            }

                        }

                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    grid();
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Select Min One Check box',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
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

    //protected void rbtnSelect_CheckedChanged()
    //{
    //    System.Web.UI.WebControls.RadioButton selectButton = (System.Web.UI.WebControls.RadioButton)sender;
    //    GridViewRow row = (GridViewRow)selectButton.Parent.Parent;
    //    int a = row.RowIndex;
    //    foreach (GridViewRow rw in gvSelected.Rows)
    //    {
    //        if (selectButton.Checked)
    //        {
    //            if (rw.RowIndex != a)
    //            {
    //                System.Web.UI.WebControls.RadioButton rd = rw.FindControl("rbtnSelect") as System.Web.UI.WebControls.RadioButton;
    //                rd.Checked = false;
    //            }
    //        }
    //    }
    //}

    protected void BindGridview(object sender, EventArgs e)
    {
        gvSelected.Visible = true;
        grid();
    }

    protected void grid()
    {


        var ic_count = Applcn_no.Text.Length;
        DataTable app_icno = new DataTable();
        app_icno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA where JA.app_new_icno= '" + Applcn_no.Text + "'");
        if (ic_count == 12)
        {
            icno = app_icno.Rows[0]["app_applcn_no"].ToString();
        }
        else
        {
            icno = Applcn_no.Text;
        }

        gvSelected.Visible = true;
        con.Open();
        //SqlCommand cmd = new SqlCommand("select * from jpa_charge where caj_applcn_no='" + icno + "'", con);
        //SqlCommand cmd = new SqlCommand("SELECT caj_applcn_no, caj_crt_dt, caj_postage,caj_late_pay,ISNULL('','') as lfe_amt,caj_crt_id,ID,'c' as t3,caj_postage_pay_id,caj_late_pay_pay_id FROM jpa_charge where caj_applcn_no = '" + icno + "' and ISNULL(caj_postage_pay_dt,'') IN('1900-01-01','') UNION ALL SELECT lfe_applcn_no,lfe_crt_dt,ISNULL('','') as t1,ISNULL('','') as t2, lfe_legal_fee_amt,lfe_crt_id,ID,'l' as t3,ISNULL('','') as t4,ISNULL('','') as t5 FROM jpa_lawyer_fee where lfe_applcn_no='" + icno + "' and ISNULL(lfe_pay_dt,'') IN('1900-01-01','')", con);
        SqlCommand cmd = new SqlCommand("SELECT caj_applcn_no, caj_crt_dt, caj_postage,caj_late_pay,ISNULL('','') as lfe_amt,caj_crt_id,ID,'c' as t3,caj_postage_pay_id,caj_late_pay_pay_id FROM jpa_charge where caj_applcn_no = '" + icno + "' UNION ALL SELECT lfe_applcn_no,lfe_crt_dt,ISNULL('','') as t1,ISNULL('','') as t2, lfe_legal_fee_amt,lfe_crt_id,ID,'l' as t3,ISNULL('','') as t4,ISNULL('','') as t5 FROM jpa_lawyer_fee where lfe_applcn_no='" + icno + "'", con);
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
            //gvSelected.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
        }
        else
        {
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            //gvSelected.FooterRow.Cells[1].Text = "<strong>JUMLAH (RM)</strong>";
            //gvSelected.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;

        }
        con.Close();
    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
    }



    protected void btn_rstclick(object sender, EventArgs e)
    {
        Response.Redirect("PP_Pambayaran_Caj.aspx");
    }

    
}