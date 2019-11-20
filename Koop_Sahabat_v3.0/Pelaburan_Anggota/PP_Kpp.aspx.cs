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


public partial class PP_Kpp : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string level, userid;
    //int total = 0;
    decimal total = 0M;
    string selphase_no;
    string pay_date;

    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                MP_nama.Attributes.Add("Readonly", "Readonly");
                MP_icno.Attributes.Add("Readonly", "Readonly");
                MP_wilayah.Attributes.Add("Readonly", "Readonly");
                MP_cawangan.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                TextBox1.Attributes.Add("Readonly", "Readonly");
                MPP_jb.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
                Button3.Visible = false;
                Button7.Visible = false;
                RadioButton1.Checked = true;
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    Applcn_no.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch_Click();
                    Button4.Visible = false;
                    Button6.Visible = false;
                }
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    void srch_Click()
    {
        if (Applcn_no.Text != "")
        {
            tab1();
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no where JA.app_applcn_no='" + Applcn_no.Text + "' and JJA.jkk_result_ind='L'");
            DataTable select_app = new DataTable();
            select_app = DBCon.Ora_Execute_table("select JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc, JJA.jkk_approve_amt, JJA.jkk_approve_dur from jpa_application as JA  Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Applcn_no.Text + "'");

            DataTable select_penama = new DataTable();
            select_penama = DBCon.Ora_Execute_table("select count(*) as cc from jpa_khairat where tkh_applcn_no='" + Applcn_no.Text + "'");
            DataTable select_app1 = new DataTable();
            select_app1 = DBCon.Ora_Execute_table("select cal_approve_amt - (cal_stamp_duty_amt +cal_process_fee +cal_deposit_amt+(cal_tkh_amt * " + select_penama.Rows[0]["cc"].ToString() + ")+cal_other_fee + cal_credit_fee) as all_char from jpa_calculate_fee where cal_applcn_no='" + Applcn_no.Text + "'");
            if (ddicno.Rows.Count == 0)
            {
                //RadioButton1.Checked = false;
                //RadioButton2.Checked = false;
                //RadioButton3.Checked = false;
                //gvSelected.Visible = false;
                //Button3.Visible = false;
                //Button7.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
            else
            {
                //RadioButton1.Checked = false;
                //RadioButton2.Checked = false;
                //RadioButton3.Checked = false;
                //gvSelected.Visible = false;
                //Button3.Visible = false;
                //Button7.Visible = false;
                MP_nama.Text = select_app.Rows[0]["app_name"].ToString();
                MP_icno.Text = select_app.Rows[0]["app_new_icno"].ToString();
                MP_wilayah.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                MP_cawangan.Text = select_app.Rows[0]["branch_desc"].ToString();
                decimal amt = (decimal)select_app.Rows[0]["jkk_approve_amt"];
                TextBox1.Text = amt.ToString("#,##.00");
                TextBox2.Text = select_app.Rows[0]["jkk_approve_dur"].ToString();
                decimal amt6 = (decimal)select_app1.Rows[0]["all_char"];
                TextBox3.Text = amt6.ToString("C").Replace("$", "");

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    protected void btnsrch_Click(object sender, EventArgs e)
    {
        try
        {
            srch_Click();
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

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
        SqlCommand cmd = new SqlCommand("select pha_applcn_no,pha_seqno,pha_phase_no,pha_name,pha_reg_no,pha_bank_acc_no,pha_bank_cd,pha_pay_type_cd,pha_pay_amt,RNB.Bank_Name,pha_pay_dt from jpa_disburse left join Ref_Nama_Bank as RNB ON RNB.Bank_Code=pha_bank_cd where pha_applcn_no = '" + Applcn_no.Text + "' and pha_phase_no = '" + selphase_no + "' ORDER BY pha_seqno ASC", con);
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
            gvSelected.FooterRow.Cells[6].Text = "<strong>JUMLAH AMAUN PENGELUARAN (RM)</strong>";
            gvSelected.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
        }
        else
        {
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            gvSelected.FooterRow.Cells[6].Text = "<strong>JUMLAH AMAUN PENGELUARAN (RM)</strong>";
            gvSelected.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;

        }
        con.Close();
    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
    }

    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "pha_pay_amt") != DBNull.Value)
            {
                //total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "pha_pay_amt"));
                Decimal ss = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pha_pay_amt"));
                total += ss;
            }

            var ddl = (DropDownList)e.Row.FindControl("Bank_details");
            string com = "select Bank_Name,Bank_Code from Ref_Nama_Bank WHERE Status = 'A' order by Bank_Name ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.DataTextField = "Bank_Name";
            ddl.DataValueField = "Bank_Code";
            ddl.DataBind();
            ddl.SelectedValue = ((DataRowView)e.Row.DataItem)["pha_bank_cd"].ToString();
            ddl.Items.Insert(0, new ListItem("--- PILIH ---", ""));


            var ddl_cb = (DropDownList)e.Row.FindControl("CB_details");
            string com1 = "select KETERANGAN,KETERANGAN_Code from Ref_Jenis_Bayaran WHERE Status = 'A' order by KETERANGAN ASC";
            SqlDataAdapter adpt1 = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt1.Fill(dt1);
            ddl_cb.DataSource = dt1;
            ddl_cb.DataBind();
            ddl_cb.DataTextField = "KETERANGAN";
            ddl_cb.DataValueField = "KETERANGAN_Code";
            ddl_cb.DataBind();
            ddl_cb.SelectedValue = ((DataRowView)e.Row.DataItem)["pha_pay_type_cd"].ToString();
            ddl_cb.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            System.Web.UI.WebControls.Label lblamount = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal");
            lblamount.Text = double.Parse(total.ToString()).ToString("C").Replace("$", "");
            MPP_jb.Text = double.Parse(total.ToString()).ToString("C").Replace("$", "");
        }
    }


    protected void update_click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                if (RadioButton1.Checked == true || RadioButton2.Checked == true || RadioButton3.Checked == true)
                {
                    foreach (GridViewRow gvrow in gvSelected.Rows)
                    {
                        //var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
                        //if (checkbox.Checked == true)
                        //{
                        var lblID = gvrow.FindControl("app_no") as System.Web.UI.WebControls.Label;
                        var seqno = gvrow.FindControl("seq_no") as System.Web.UI.WebControls.Label;
                        var ph_no1 = gvrow.FindControl("Label3") as System.Web.UI.WebControls.Label;

                        System.Web.UI.WebControls.TextBox box1 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Label2");
                        System.Web.UI.WebControls.TextBox box2 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Label4");
                        System.Web.UI.WebControls.TextBox box3 = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("Label5");
                        DropDownList ddl = (DropDownList)gvrow.FindControl("Bank_details");
                        DropDownList ddl_cb = (DropDownList)gvrow.FindControl("CB_details");

                        DBCon.Execute_CommamdText("Update jpa_disburse SET pha_name='" + box1.Text.Replace("'", "''") + "', pha_reg_no='" + box2.Text.Trim() + "', pha_bank_acc_no='" + box3.Text + "',pha_bank_cd='" + ddl.SelectedValue + "',pha_pay_type_cd='" + ddl_cb.SelectedValue + "',pha_upd_id='" + Session["New"].ToString() + "',pha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE pha_applcn_no='" + lblID.Text + "' and pha_phase_no = '" + ph_no1.Text + "' and pha_seqno='" + seqno.Text + "'");
                        //}

                    }
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan'); window.location='PP_Kpp.aspx';", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan no ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila masukkan no ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
            tab1();
            //tab2();
            //tab3();
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }


    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        tab1();
    }

    void tab1()
    {
        if (RadioButton1.Checked == true)
        {
            Button3.Visible = true;
            Button7.Visible = true;
            if (Applcn_no.Text != "")
            {
                selphase_no = "1";
                grid();

                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
            }
            else
            {
                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        tab2();
    }

    void tab2()
    {
        if (RadioButton2.Checked == true)
        {
            Button3.Visible = true;
            Button7.Visible = true;
            if (Applcn_no.Text != "")
            {
                selphase_no = "2";
                grid();

                RadioButton1.Checked = false;
                RadioButton3.Checked = false;
            }
            else
            {
                RadioButton1.Checked = false;
                RadioButton3.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
    }
    protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
    {
        tab3();
    }

    void tab3()
    {
        if (RadioButton3.Checked == true)
        {
            Button3.Visible = true;
            Button7.Visible = true;
            if (Applcn_no.Text != "")
            {
                selphase_no = "3";
                grid();
                RadioButton1.Checked = false;
                RadioButton2.Checked = false;
            }
            else
            {
                RadioButton1.Checked = false;
                RadioButton2.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_Kpp_view.aspx");
    }
}