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
using System.Xml;

public partial class PP_Kutipan_kelompok : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string Status = string.Empty;
    string phdate;
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();

            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.FileName != "")
            {
                if (FileUpload1.HasFile)
                {
                    FileUpload1.SaveAs(Server.MapPath("~/FILES/kutipan/" + FileUpload1.FileName));
                    string path = Server.MapPath("~/FILES/kutipan/" + FileUpload1.FileName).ToString();
                    DataTable table = new DataTable();
                    table.Columns.Add("no_rujukan");
                    table.Columns.Add("nama_p");
                    table.Columns.Add("kod_t");
                    table.Columns.Add("rujukan_t");
                    table.Columns.Add("tarikh_b");
                    table.Columns.Add("amaun");

                    // CONVERT(datetime,LEFT(DTTRX,8) + ' ' + SUBSTRING(DTTRX,9,2) +':'+SUBSTRING(DTTRX,11,2) +':' + RIGHT(DTTRX,2)) as DTTRX
                    using (StreamReader sr = new StreamReader(path))
                    {

                        while (!sr.EndOfStream)
                        {
                            string myString1 = string.Empty;
                            string[] parts = sr.ReadLine().Split('|');
                            String dateString = parts[1];
                            String format = "ddMMyyyy";
                            DateTime result = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
                            string d1 = result.ToString("dd/MM/yyyy");
                            myString1 = parts[2];
                            List<string> strList = myString1.Split(' ').ToList();
                            string s1 = strList.First();
                            string s2 = strList.Last();
                            DataTable ddicno = new DataTable();
                            ddicno = Dblog.Ora_Execute_table("select app_applcn_no,app_new_icno,app_name from jpa_application JA where JA.app_new_icno = '" + parts[4] + "'");
                            string aname = string.Empty;
                            if (ddicno.Rows.Count != 0)
                            {
                                aname = ddicno.Rows[0]["app_name"].ToString();
                            }
                            else
                            {
                                aname = "";
                            }
                            table.Rows.Add(parts[0], aname, s1, s2, d1, parts[3]);
                        }
                    }
                    shw_htxt.Visible = true;
                    GridView1.DataSource = table;
                    GridView1.DataBind();
                   

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Fail',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }


        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('" + ex.Message + "');</script>");
        }
    }






    protected void update_click(object sender, EventArgs e)
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in GridView1.Rows)
                {
                    //var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
                    //if (checkbox.Checked == true)
                    //{
                    var no_rujjukan = gvrow.FindControl("Label2") as System.Web.UI.WebControls.Label;
                    var t_date = gvrow.FindControl("Label6") as System.Web.UI.WebControls.Label;
                    var amaun = gvrow.FindControl("amt") as System.Web.UI.WebControls.Label;
                    var rnt = gvrow.FindControl("Label5") as System.Web.UI.WebControls.Label;
                    var kod_t = gvrow.FindControl("Label4") as System.Web.UI.WebControls.Label;
                    var batname = gvrow.FindControl("Label3") as System.Web.UI.WebControls.Label;


                    string d2 = t_date.Text;
                    if (d2 != "")
                    {
                        DateTime today1 = DateTime.ParseExact(d2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        phdate = today1.ToString("yyyy-mm-dd");
                    }
                    else
                    {
                        phdate = "";
                    }

                    DataTable ddokdicno = new DataTable();
                    ddokdicno = Dblog.Ora_Execute_table("select * from jpa_collect_batch where bat_applcn_no='" + no_rujjukan.Text + "' and bat_pay_dt = '" + phdate + "'");
                    if (ddokdicno.Rows.Count > 0)
                    {
                        Dblog.Execute_CommamdText("Update jpa_collect_batch SET bat_name='" + batname.Text.Trim() + "',bat_txn_code='" + kod_t.Text.Trim() + "',bat_ref_no='" + rnt.Text.Trim() + "',bat_pay_dt='" + phdate.Trim() + "',bat_pay_amt='" + amaun.Text.Trim() + "',bat_upd_id='" + Session["New"].ToString() + "',bat_upd_dt='" + DateTime.Now + "' WHERE bat_applcn_no='" + no_rujjukan.Text.Trim() + "' and bat_pay_dt = '" + phdate.Trim() + "'");
                    }
                    else
                    {
                        Dblog.Execute_CommamdText("Insert into jpa_collect_batch (bat_applcn_no,bat_name,bat_txn_code,bat_ref_no,bat_pay_dt,bat_pay_amt,bat_batch_upd_ind,bat_crt_id,bat_crt_dt) values ('" + no_rujjukan.Text.Trim() + "','" + batname.Text.Trim() + "','" + kod_t.Text.Trim() + "','" + rnt.Text.Trim() + "','" + phdate + "','" + amaun.Text.Trim() + "','0','" + Session["New"].ToString() + "','" + DateTime.Now + "')");
                        //Dblog.Execute_CommamdText("insert into jpa_transaction (txn_applcn_no,txn_cd,txn_debit_amt,txn_bal_amt,txn_crt_id,txn_crt_dt) values ('" + no_rujjukan.Text.Trim() + "','10','" + TextBox5.Text + "','" + b_amt + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");

                    }

                }
                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});window.location='../Pelaburan_Anggota/PP_Kutipan_kelompok.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Fail',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Issue',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                //if (CAJ_amaun.Text != "" && CAJ_ds.Text != "" && CAJ_fi_p.Text != "" && CAJ_tkh.Text != "" && CAJ_deposit.Text != "")
                //{
                foreach (GridViewRow gvrow in GridView1.Rows)
                {
                    //var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
                    //if (checkbox.Checked == true)
                    //{
                    var no_rujjukan = gvrow.FindControl("Label2") as System.Web.UI.WebControls.Label;
                    var t_date = gvrow.FindControl("Label6") as System.Web.UI.WebControls.Label;
                    var amaun = gvrow.FindControl("amt") as System.Web.UI.WebControls.Label;
                    var rnt = gvrow.FindControl("Label5") as System.Web.UI.WebControls.Label;
                    var kod_t = gvrow.FindControl("Label4") as System.Web.UI.WebControls.Label;
                    var batname = gvrow.FindControl("Label3") as System.Web.UI.WebControls.Label;

                    userid = Session["New"].ToString();
                    DataTable dtGL = new DataTable();
                    string ket = no_rujjukan.Text + batname.Text.Trim() + "Ansuran bulanan bagi" + DateTime.Now.ToString("MMMM") + DateTime.Now.Year;

                    dtGL = Dblog.Ora_Execute_table("insert into KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,crt_id,cr_dt,kat_akaun,GL_invois_no,ref2,GL_process_dt,GL_desc1,GL_nama_kod,GL_sts)values('03.03','0.00','" + amaun.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','03','" + no_rujjukan.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket + " ','03.03','A')");
                    dtGL = Dblog.Ora_Execute_table("insert into KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,crt_id,cr_dt,kat_akaun,GL_invois_no,ref2,GL_process_dt,GL_desc1,GL_nama_kod,GL_sts)values('10.06','0.00','" + amaun.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','10','" + no_rujjukan.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket + "','10.06','A')");
                    dtGL = Dblog.Ora_Execute_table("insert into KW_General_Ledger (kod_akaun,KW_Debit_amt,KW_kredit_amt,crt_id,cr_dt,kat_akaun,GL_invois_no,ref2,GL_process_dt,GL_desc1,GL_nama_kod,GL_sts)values('03.14.01','" + amaun.Text + "','0.00','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','03','" + no_rujjukan.Text + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + ket + "','03.14.01','A')");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Inserted Successfully');", true);
                }
            }
            //}

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rst_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Pelaburan_Anggota/PP_Kutipan_kelompok.aspx");
    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_Kutipan_kelompok_view.aspx");
    }
}
