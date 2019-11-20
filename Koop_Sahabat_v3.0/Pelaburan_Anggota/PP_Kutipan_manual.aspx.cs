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


public partial class PP_Kutipan_manual : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;

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
                //MP_wilayah.Attributes.Add("Readonly", "Readonly");
                //MP_cawangan.Attributes.Add("Readonly", "Readonly");
                CAJ_amaun.Attributes.Add("Readonly", "Readonly");
                CAJ_tempoh.Attributes.Add("Readonly", "Readonly");
                TextBox10.Attributes.Add("Readonly", "Readonly");

                TextBox6.Attributes.Add("Readonly", "Readonly");
                Banknama();
              
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    Applcn_no.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch_Click();
                    Button4.Visible = false;
                    Button6.Visible = false;
                }
                BindData_jenis();

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
                com.CommandText = "select app_applcn_no from jpa_application JA Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no where app_applcn_no like '%' + @Search + '%' and JJA.jkk_result_ind='L' and applcn_clsed ='N' ";
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
    protected void BindData_jenis()
    {
        con.Open();
        
        SqlCommand cmd = new SqlCommand("select s2.Id,app_applcn_no,FORMAT(man_pay_dt,'dd/MM/yyyy', 'en-us') as dt1,app_name,app_new_icno, man_pay_amt,man_bank_acc_no from jpa_collect_manual s2 left join jpa_application s1 on s1.app_applcn_no=man_applcn_no where man_applcn_no='"+ Applcn_no.Text + "'", con);
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
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        con.Close();
    }

    void Banknama()
    {
        string com = "select * from Ref_Panel_Bank WHERE Status = 'A' order by Bank_Name ASC";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        DropDownList2.DataSource = dt;
        DropDownList2.DataBind();
        DropDownList2.DataTextField = "Bank_Name";
        DropDownList2.DataValueField = "Bank_Code";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }

    protected void ddkaw_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable ddicno = new DataTable();
        ddicno = DBCon.Ora_Execute_table("select * from Ref_Panel_Bank where Bank_Name='" + DropDownList2.SelectedItem.Text + "'");
        TextBox6.Text = ddicno.Rows[0]["Bank_no"].ToString();
        BindData_jenis();
    }

    void srch_Click()
    {
        if (Applcn_no.Text != "")
        {
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select app_applcn_no,applcn_clsed from jpa_application JA Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no where JA.app_applcn_no='" + Applcn_no.Text + "' and JJA.jkk_result_ind='L'");
            DataTable select_app = new DataTable();
            // select_app = DBCon.Ora_Execute_table("select a.app_applcn_no,a.app_loan_amt,a.app_name,a.app_new_icno,a.appl_loan_dur,a.branch_desc,a.Wilayah_Name,ISNULL(b.man_applcn_no,'') as man_applcn_no,ISNULL(b.man_pay_dt,'') as man_pay_dt,ISNULL(b.man_bank_acc_no,'') as man_bank_acc_no,ISNULL(b.man_bank_cd,'') as man_bank_cd,ISNULL(b.man_pay_type_cd,'') as man_pay_type_cd,ISNULL(b.man_pay_amt,'') as man_pay_amt,ISNULL(b.man_ref_no,'') as man_ref_no from (select JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur,JA.app_applcn_no from jpa_application as JA  Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Applcn_no.Text + "') as a full outer join (select * from jpa_collect_manual where man_applcn_no='" + Applcn_no.Text + "') as b on a.app_applcn_no='" + Applcn_no.Text + "'");
            select_app = DBCon.Ora_Execute_table("select JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur,JA.app_applcn_no from jpa_application as JA  Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Applcn_no.Text + "'");
            if (ddicno.Rows.Count == 0)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                MP_nama.Text = "";
                MP_icno.Text = "";
                //MP_wilayah.Text = "";
                //MP_cawangan.Text = "";
                CAJ_amaun.Text = "";
                CAJ_tempoh.Text = "";
            }
            else
            {
                if(ddicno.Rows[0]["applcn_clsed"].ToString() == "Y")
                {
                    Button2.Visible = false;
                }
                else
                {
                    Button2.Visible = true;
                }
                MP_nama.Text = select_app.Rows[0]["app_name"].ToString();
                MP_icno.Text = select_app.Rows[0]["app_new_icno"].ToString();
                //MP_wilayah.Text = select_app.Rows[0]["Wilayah_Name"].ToString();
                //MP_cawangan.Text = select_app.Rows[0]["branch_desc"].ToString();
                decimal amt1 = (decimal)select_app.Rows[0]["app_loan_amt"];
                CAJ_amaun.Text = double.Parse(amt1.ToString()).ToString("C").Replace("$","").Replace("RM", "");
                CAJ_tempoh.Text = select_app.Rows[0]["appl_loan_dur"].ToString();
                
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void btnsrch_Click(object sender, EventArgs e)
    {
        try
        {
            srch_Click();
            BindData_jenis();
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                if (TextBox10.Text != "" && TextBox6.Text != "" && DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue != "" && TextBox7.Text != "")
                {

                    string pay_date = TextBox10.Text;
                    DateTime pydt = DateTime.ParseExact(pay_date, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    String paydt = pydt.ToString("yyyy-mm-dd");
                    DataTable maxrow_count = new DataTable();
                    maxrow_count = DBCon.Ora_Execute_table("SELECT man_applcn_no FROM jpa_collect_manual where man_applcn_no='" + Applcn_no.Text + "' and man_pay_dt='" + paydt + "'");
                    if (maxrow_count.Rows.Count == 0)
                    {
                        DBCon.Execute_CommamdText("insert into jpa_collect_manual (man_applcn_no,man_pay_dt,man_pay_type_cd,man_pay_amt,man_ref_no,man_bank_acc_no,man_bank_cd,man_batch_upd_ind,man_crt_id,man_crt_dt) values ('" + Applcn_no.Text + "','" + paydt + "','" + DropDownList1.SelectedValue + "','" + TextBox7.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + DropDownList2.SelectedValue + "','0','" + Session["New"].ToString() + "','" + DateTime.Now + "')");
                        //DBCon.Execute_CommamdText("insert into jpa_transaction (txn_applcn_no,txn_cd,txn_debit_amt,txn_bal_amt,txn_crt_id,txn_crt_dt) values ('" + Applcn_no.Text + "','10','" + TextBox5.Text + "','" + b_amt + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");                        
                        clr_flds();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Bayaran Telah Wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                  
                    con.Close();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Medan Mandatori',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Medan Mandatori',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Semak Isu',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
        BindData_jenis();
    }

    void clr_flds()
    {
        DropDownList1.SelectedValue = "";
        TextBox10.Text = "";
        TextBox7.Text = "";
        TextBox5.Text = "";
        DropDownList2.SelectedValue = "";
        TextBox6.Text = "";
    }

    //protected void lnkView_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LinkButton btnButton = sender as LinkButton;
    //        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
    //        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl_sts1");
    //        string lblid1 = lblTitle.Text;
    //        DataTable ddokdicno = new DataTable();
    //        ddokdicno = DBCon.Ora_Execute_table("select * from jpa_collect_manual where Id='" + lblTitle.Text + "' ");
    //        if (ddokdicno.Rows.Count != 0)
    //        {
    //            string name = HttpUtility.UrlEncode(service.Encrypt(lblTitle.Text));
    //            //Response.Redirect("../kewengan/kw_profil_syarikat.aspx?edit={0}"+ og_genid.Text + "");
    //            Response.Redirect(string.Format("../Pelaburan_Anggota/PP_Kutipan_manual.aspx?edit={0}", name));
    //        }
    //        else
    //        {
    //            Session["validate_success"] = "";
    //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }

    //}
    protected void btn_rstclick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_Kutipan_manual_view.aspx");
    }

}