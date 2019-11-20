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
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Windows.Forms;


public partial class PP_Kos_guaman : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
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
                MP_Nama.Attributes.Add("Readonly", "Readonly");
                MP_kpbaru.Attributes.Add("Readonly", "Readonly");
                //MP_wilayah.Attributes.Add("Readonly", "Readonly");
                //MP_cawangan.Attributes.Add("Readonly", "Readonly");
                MP_amt.Attributes.Add("Readonly", "Readonly");
                MP_duration.Attributes.Add("Readonly", "Readonly");
                TextBox7.Attributes.Add("Readonly", "Readonly");
                Tindakan();
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
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

    void Tindakan()
    {
        string com = "select * from ref_tindakan WHERE Status = 'A' order by tindakan_cd DESC";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        ddl_tindakan.DataSource = dt;
        ddl_tindakan.DataBind();
        ddl_tindakan.DataTextField = "tindakan_desc";
        ddl_tindakan.DataValueField = "tindakan_cd";
        ddl_tindakan.DataBind();
        ddl_tindakan.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void Click_update(object sender, EventArgs e)
    {

        try
        {
            if (Applcn_no.Text != "")
            {
                DataTable ddicno = new DataTable();
                DateTime dt = Convert.ToDateTime(TextBox1_id.Text);
                string id = dt.ToString("yyyy-MM-dd hh:mm:ss");
                ddicno = DBCon.Ora_Execute_table("select lfe_applcn_no from jpa_lawyer_fee where lfe_applcn_no='" + Applcn_no.Text + "' and lfe_legal_action_type_cd='" + ddl_tindakan.SelectedValue + "' ");
                if (ddicno.Rows.Count != 0)
                {
                    if (TextBox6.Text != "" && ddl_tindakan.SelectedValue != "" && TextBox5.Text != "" && TextBox7.Text != "")
                    {


                        string phdate;
                        string d2 = TextBox7.Text;
                        DateTime today1 = DateTime.ParseExact(d2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        phdate = today1.ToString("yyyy-mm-dd");
                        string bamt = string.Empty;
                        string b_amt = string.Empty;
                        DBCon.Execute_CommamdText("UPDATE jpa_lawyer_fee SET lfe_lawyer_name='" + TextBox6.Text + "',lfe_legal_action_type_cd='" + ddl_tindakan.SelectedValue + "',lfe_legal_fee_amt='" + TextBox5.Text + "',lfe_pay_dt='" + phdate + "',lfe_upd_id='" + Session["New"].ToString() + "',lfe_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',lfe_legal_pay_id='" + Session["New"].ToString() + "',lfe_legal_pay_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where lfe_applcn_no='" + Applcn_no.Text + "' and lfe_crt_dt='" + id  + "'");
                        DataTable select_ic_amt = new DataTable();
                        select_ic_amt = DBCon.Ora_Execute_table("select lfe_applcn_no,sum(lfe_legal_fee_amt) as tb_amt from jpa_lawyer_fee where lfe_applcn_no='" + Applcn_no.Text + "' and lfe_crt_dt < '" + TextBox1_id.Text + "' group by lfe_applcn_no");

                        DataTable max_dt = new DataTable();
                        max_dt = DBCon.Ora_Execute_table("select MAX(txn_crt_dt) as crt_dt from jpa_transaction where txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt < '" + TextBox1_id.Text + "'");

                        DataTable max_dt_txn = new DataTable();
                        max_dt_txn = DBCon.Ora_Execute_table("select * from jpa_transaction where txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt = '" + max_dt.Rows[0]["crt_dt"].ToString() + "'");

                        string sn = string.Empty;
                        if (max_dt_txn.Rows.Count == 0)
                        {
                            sn = "0.00";
                        }
                        else
                        {
                            sn = max_dt_txn.Rows[0]["txn_bal_amt"].ToString();
                        }

                        DataTable ddokdicno_del = new DataTable();
                        ddokdicno_del = DBCon.Ora_Execute_table("select * from jpa_transaction where txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt >= '" + TextBox1_id.Text + "' order by txn_crt_dt");
                        for (int i = 0; i <= ddokdicno_del.Rows.Count - 1; i++)
                        {
                            DateTime dt1 = Convert.ToDateTime(ddokdicno_del.Rows[i]["txn_crt_dt"].ToString());
                            string txt = dt1.ToString("yyyy-MM-dd hh:mm:ss");
                            if (i == 0)
                            {
                                bamt = sn;
                                b_amt = (double.Parse(bamt) + double.Parse(TextBox5.Text)).ToString("0.00");
                            }
                            else
                            {
                                //bamt = "0.00";
                                DataTable max_dt1 = new DataTable();
                                max_dt1 = DBCon.Ora_Execute_table("select MAX(txn_crt_dt) as crt_dt from jpa_transaction where txn_applcn_no='" + ddokdicno_del.Rows[i]["txn_applcn_no"].ToString() + "' and txn_crt_dt < '" + txt + "'");

                                if (max_dt1.Rows.Count > 0)
                                {
                             
                                DataTable max_dt_txn1 = new DataTable();
                                DateTime dt11 = Convert.ToDateTime(max_dt1.Rows[0]["crt_dt"].ToString());
                                string txt1 = dt11.ToString("yyyy-MM-dd hh:mm:ss");
                                max_dt_txn1 = DBCon.Ora_Execute_table("select * from jpa_transaction where txn_applcn_no='" + ddokdicno_del.Rows[i]["txn_applcn_no"].ToString() + "' and txn_crt_dt = '" + txt1 + "'");

                                b_amt = (double.Parse(max_dt_txn1.Rows[0]["txn_bal_amt"].ToString()) + double.Parse(ddokdicno_del.Rows[i]["txn_debit_amt"].ToString())).ToString("0.00");
                                }
                            }
                            if (Applcn_no.Text == ddokdicno_del.Rows[i]["txn_applcn_no"].ToString() && TextBox1_id.Text == ddokdicno_del.Rows[i]["txn_crt_dt"].ToString())
                            {
                                DBCon.Execute_CommamdText("UPDATE jpa_transaction SET txn_debit_amt='" + TextBox5.Text + "',txn_bal_amt='" + b_amt + "',txn_upd_id='" + Session["New"].ToString() + "',txn_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' WHERE txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt='" + txt + "'");
                            }
                            else
                            {
                               
                             
                                DBCon.Execute_CommamdText("UPDATE jpa_transaction SET txn_bal_amt='" + b_amt + "',txn_upd_id='" + Session["New"].ToString() + "',txn_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' WHERE txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt='" + txt + "'");
                            }
                        }


                        ddl_tindakan.SelectedValue = "";
                        TextBox6.Text = "";
                        TextBox5.Text = "";
                        TextBox7.Text = "";
                        TextBox1_id.Text = "";
                        Button2.Visible = false;
                        Button5.Visible = true;                        
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        grid();
                    }
                    else
                    {

                        grid();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Harus Mandatori',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please Enter No Permohonan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    protected void get_gridvalue(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        string icno = lb.CommandArgument;
        DateTime ic = Convert.ToDateTime(icno);
        string iiic = ic.ToString("yyyy-MM-dd hh:mm:ss");
        DataTable law_details = new DataTable();
        law_details = DBCon.Ora_Execute_table("select law.lfe_crt_dt,law.lfe_legal_action_type_cd,law.lfe_lawyer_name,law.lfe_legal_fee_amt,law.lfe_pay_dt from jpa_lawyer_fee as law where law.lfe_applcn_no='" + Applcn_no.Text + "' and law.lfe_crt_dt='" + iiic + "'");
        if (law_details.Rows.Count != 0)
        {
            TextBox6.Text = law_details.Rows[0]["lfe_lawyer_name"].ToString();
            ddl_tindakan.SelectedValue = law_details.Rows[0]["lfe_legal_action_type_cd"].ToString();
            decimal fee_amt = (decimal)law_details.Rows[0]["lfe_legal_fee_amt"];
            TextBox5.Text = fee_amt.ToString("C").Replace("RM", "").Replace("$", "");
            TextBox7.Text = Convert.ToDateTime(law_details.Rows[0]["lfe_pay_dt"]).ToString("dd/MM/yyyy");
            Button2.Visible = true;
            Button5.Visible = false;
            TextBox1_id.Text = icno;
            Button1.Visible = true;
            DataTable ddokdicno_del = new DataTable();
            ddokdicno_del = DBCon.Ora_Execute_table("select * from jpa_transaction where txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt >= '" + icno + "'");
            for (int i = 0; i <= ddokdicno_del.Rows.Count - 1; i++)
            {
                string v1 = string.Empty;
                string aa1 = ddokdicno_del.Rows[i]["txn_bal_amt"].ToString();

                v1 = (double.Parse(aa1) - double.Parse(TextBox5.Text)).ToString("C").Replace("RM","").Replace("$", "");

                DBCon.Execute_CommamdText("UPDATE jpa_transaction set txn_bal_amt='" + v1 + "' where txn_applcn_no='" + ddokdicno_del.Rows[i]["txn_applcn_no"].ToString() + "' and txn_crt_dt='" + ddokdicno_del.Rows[i]["txn_crt_dt"].ToString() + "'");
            }
        }

    }

    protected void Click_insert(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select lfe_applcn_no from jpa_lawyer_fee where lfe_applcn_no='" + Applcn_no.Text + "' and lfe_legal_action_type_cd='" + ddl_tindakan.SelectedValue + "'");
                if (ddicno.Rows.Count == 0)
                {
                    if (TextBox6.Text != "" && ddl_tindakan.SelectedValue != "" && TextBox5.Text != "" && TextBox7.Text != "")
                    {
                        DataTable select_ic_amt = new DataTable();
                        select_ic_amt = DBCon.Ora_Execute_table("select txn_bal_amt from jpa_transaction where txn_applcn_no='" + Applcn_no.Text + "' and txn_crt_dt = (select max(txn_crt_dt) from jpa_transaction where txn_applcn_no='" + Applcn_no.Text + "')");
                        string bamt = string.Empty;
                        string b_amt = string.Empty;
                        if (select_ic_amt.Rows.Count != 0)
                        {
                            bamt = select_ic_amt.Rows[0]["txn_bal_amt"].ToString();
                            b_amt = (double.Parse(bamt) + double.Parse(TextBox5.Text)).ToString("0.00");
                        }
                        else
                        {
                            b_amt = (double.Parse(TextBox5.Text)).ToString("0.00");
                        }

                        string phdate;
                        string d2 = TextBox7.Text;
                        DateTime today1 = DateTime.ParseExact(d2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        phdate = today1.ToString("yyyy-mm-dd");
                        DBCon.Execute_CommamdText("insert into jpa_lawyer_fee (lfe_applcn_no,lfe_lawyer_name,lfe_legal_action_type_cd,lfe_legal_fee_amt,lfe_pay_dt,lfe_crt_id,lfe_crt_dt,lfe_legal_pay_id,lfe_legal_pay_dt) values ('" + Applcn_no.Text + "','" + TextBox6.Text + "','" + ddl_tindakan.SelectedValue + "','" + TextBox5.Text + "','" + phdate + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')");
                        DBCon.Execute_CommamdText("insert into jpa_transaction (txn_applcn_no,txn_cd,txn_debit_amt,txn_bal_amt,txn_crt_id,txn_crt_dt) values ('" + Applcn_no.Text + "','10','" + TextBox5.Text + "','" + b_amt + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')");                        
                        grid();
                        ddl_tindakan.SelectedValue = "";
                        TextBox6.Text = "";
                        TextBox5.Text = "";
                        TextBox7.Text = "";
                        TextBox1_id.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {

                        grid();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Harus Mandatori',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No Permohonan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    protected void btnsrch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application JA Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no where JA.app_applcn_no='" + Applcn_no.Text + "' and JJA.jkk_result_ind='L'");
                DataTable select_app = new DataTable();
                select_app = DBCon.Ora_Execute_table("select JA.app_new_icno,JA.app_name,RW.Wilayah_Name,ISNULL(RB.branch_desc, '') AS branch_desc,JA.app_loan_amt,JA.appl_loan_dur from jpa_application as JA  Left Join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no Left Join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd Left join ref_branch AS RB ON RB.branch_cd=JA.app_branch_cd where JA.app_applcn_no='" + Applcn_no.Text + "'");
                if (ddicno.Rows.Count == 0)
                {
                    TextBox6.Text = "";
                    TextBox5.Text = "";
                    TextBox7.Text = "";
                    TextBox1_id.Text = "";
                    MP_Nama.Text = "";
                    MP_kpbaru.Text = "";
                    //MP_wilayah.Text = "";
                    //MP_cawangan.Text = "";
                    MP_amt.Text = "";
                    MP_duration.Text = "";
                    gvSelected.Visible = false;
                    Button3.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
                else
                {
                    gvSelected.Visible = true;
                    Button3.Visible = true;
                    MP_Nama.Text = (string)select_app.Rows[0]["app_name"];
                    MP_kpbaru.Text = (string)select_app.Rows[0]["app_new_icno"];
                    //MP_wilayah.Text = (string)select_app.Rows[0]["Wilayah_Name"];
                    //MP_cawangan.Text = (string)select_app.Rows[0]["branch_desc"];
                    decimal amt = (decimal)select_app.Rows[0]["app_loan_amt"];
                    MP_amt.Text = amt.ToString("C").Replace("RM", "").Replace("$", "");
                    MP_duration.Text = select_app.Rows[0]["appl_loan_dur"].ToString();
                    grid();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }

    void grid()
    {
        Button3.Visible = true;
        con.Open();
        SqlCommand cmd = new SqlCommand("select law.Id,law.lfe_applcn_no,law.lfe_lawyer_name,law.lfe_legal_fee_amt,law.lfe_pay_dt,law.lfe_crt_dt,RT.tindakan_desc from jpa_lawyer_fee as law left join ref_tindakan as RT ON RT.tindakan_cd=law.lfe_legal_action_type_cd WHERE lfe_applcn_no = '" + Applcn_no.Text + "'", con);
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
            gvSelected.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {

            gvSelected.DataSource = ds;
            gvSelected.DataBind();

        }
        con.Close();
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("PP_Kos_guaman.aspx");
    }
    protected void btnrst_Click(object sender, EventArgs e)
    {
        ddl_tindakan.SelectedValue = "";
        TextBox6.Text = "";
        TextBox5.Text = "";
        TextBox7.Text = "";
        TextBox1_id.Text = "";
        Button2.Visible = false;
        Button5.Visible = true;
    }

 
}