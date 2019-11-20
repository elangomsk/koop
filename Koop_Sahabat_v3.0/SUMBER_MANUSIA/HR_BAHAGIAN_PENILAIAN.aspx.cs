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
using System.Xml;
using System.Threading;

public partial class HR_BAHAGIAN_PENILAIAN : System.Web.UI.Page
{
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string rcount = string.Empty, rcount1 = string.Empty;
    int count = 0, count1 = 1, pyr = 0, prdt = 0;
    string ss1 = string.Empty;
    string selectedValues = string.Empty;
    string chk_val1 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string userid;
        app_language();
        string script = " $(document).ready(function () { $(" + chk_lst.ClientID + ").SumoSelect({ selectAll: true }); $('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                userid = Session["New"].ToString();
                kategory();
                if (samp != "")
                {
                    txt_bahagian.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                }
                else
                {
                    btb_kmes.Visible = false;
                    btn_simp.Visible = true;
                }
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void kategory()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_kate_Code,UPPER(hr_kate_desc) hr_kate_desc from Ref_hr_penj_kategori where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            chk_lst.DataSource = dt;
            chk_lst.DataTextField = "hr_kate_desc";
            chk_lst.DataValueField = "hr_kate_Code";
            chk_lst.DataBind();
            //chk_lst.Items.Insert(0, new ListItem("--- PILIH ---", ""));

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
            DataTable ste_set = new DataTable();
        ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

        DataTable gt_lng = new DataTable();
        gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1430','448','1431','529','67','53','61', '15', '77')");

        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;

        h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower()); 
        bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
        bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());

        h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());  

        //lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower()); 
        //lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower()); 
        //lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());

        btn_simp.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower()); //Simpan 
        Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower()); //Set Semula
        Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower()); //Kembali
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    protected void btn_simp_Click(object sender, EventArgs e)
    {
        if (txt_bahagian.Text != "" && txt_keteran.Text != "")
        {
          
           
            DataTable ddokdicno1 = new DataTable();
            ddokdicno1 = DBCon.Ora_Execute_table("select cse_section_cd,cse_section_desc,cse_info From hr_cmn_appr_section where cse_section_cd='" + txt_bahagian.Text + "'");
            if (ddokdicno1.Rows.Count == 0)
            {
                if(jenis_chk1.Checked == true)
                {
                    chk_val1 = "01";
                }
                else if (jenis_chk2.Checked == true)
                {
                    chk_val1 = "02";
                }

                foreach (ListItem li in chk_lst.Items)
                {
                    if (li.Selected == true)
                    {
                        count++;
                    }
                    rcount = count.ToString();
                }

                foreach (ListItem li in chk_lst.Items)
                {
                    if (li.Selected == true)
                    {
                        if (Int32.Parse(rcount) > count1)
                        {
                            ss1 = ",";
                        }
                        else
                        {
                            ss1 = "";
                        }

                        selectedValues += li.Value + ss1;

                        count1++;
                    }
                    rcount1 = count1.ToString();
                }

                SqlCommand ins_prof = new SqlCommand("insert into hr_cmn_appr_section(cse_section_cd,cse_section_desc,cse_info,cse_marks,cse_sec_jawatan,cse_sec_type,cse_crt_id,cse_crt_dt) values(@cse_section_cd,@cse_section_desc,@cse_info,@cse_marks,@cse_sec_jawatan,@cse_sec_type,@cse_crt_id,@cse_crt_dt)", con);
                ins_prof.Parameters.AddWithValue("cse_section_cd", txt_bahagian.Text);
                ins_prof.Parameters.AddWithValue("cse_section_desc", txt_keteran.Text.Replace("'", "''"));
                ins_prof.Parameters.AddWithValue("cse_info", text_area.Text.Replace("'", "''"));
                ins_prof.Parameters.AddWithValue("cse_marks", TextBox2.Text);
                ins_prof.Parameters.AddWithValue("cse_sec_jawatan", selectedValues);
                ins_prof.Parameters.AddWithValue("cse_sec_type", chk_val1);
                ins_prof.Parameters.AddWithValue("cse_crt_id", Session["New"].ToString());
                ins_prof.Parameters.AddWithValue("cse_crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                con.Open();
                int i = ins_prof.ExecuteNonQuery();
                con.Close();
                Session["validate_success"] = "SUCCESS";
                Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                Response.Redirect("../SUMBER_MANUSIA/HR_BAHAGIAN_PENILAIAN_view.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }

  
    void view_details()
    {
       
        string abc = txt_bahagian.Text;
        DataTable ddokdicno1 = new DataTable();
        ddokdicno1 = DBCon.Ora_Execute_table("select cse_section_cd,cse_section_desc,cse_info,cse_marks,cse_sec_type,cse_sec_jawatan From hr_cmn_appr_section where cse_section_cd='" + abc + "'");
        if (ddokdicno1.Rows.Count != 0)
        {
            btb_kmes.Visible = true;
            btn_simp.Visible = false;
            Button1.Visible = false;
            txt_bahagian.Attributes.Add("Readonly", "Readonly");
            txt_bahagian.Text = ddokdicno1.Rows[0]["cse_section_cd"].ToString();
            txt_keteran.Text = ddokdicno1.Rows[0]["cse_section_desc"].ToString();
            text_area.Text = ddokdicno1.Rows[0]["cse_info"].ToString();
            TextBox2.Text = ddokdicno1.Rows[0]["cse_marks"].ToString();
            if(ddokdicno1.Rows[0]["cse_sec_type"].ToString() == "01")
            {
                jenis_chk1.Checked = true;
                jenis_chk2.Checked = false;
            }
            else if (ddokdicno1.Rows[0]["cse_sec_type"].ToString() == "02")
            {
                jenis_chk1.Checked = false;
                jenis_chk2.Checked = true;
            }

            if(ddokdicno1.Rows[0]["cse_sec_jawatan"].ToString().Trim() != "")

            {
                string[] jawatan = ddokdicno1.Rows[0]["cse_sec_jawatan"].ToString().Trim().Split(',');
                
                for (int i=0; i < Int32.Parse(jawatan.Length.ToString()); i++)
                {
                    foreach (ListItem item in chk_lst.Items)
                    {
                        if (jawatan[i] == item.Value)
                        {
                            item.Attributes.Add("selected", "selected");
                        }
                    }
                    
                }                
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        TextBox1.Text = abc.ToString();


    }
    protected void btb_kmes_Click(object sender, EventArgs e)
    {
        if (txt_bahagian.Text != "" &&  txt_keteran.Text != "")
        {
            if (jenis_chk1.Checked == true)
            {
                chk_val1 = "01";
            }
            else if (jenis_chk2.Checked == true)
            {
                chk_val1 = "02";
            }

            foreach (ListItem li in chk_lst.Items)
            {
                if (li.Selected == true)
                {
                    count++;
                }
                rcount = count.ToString();
            }

            foreach (ListItem li in chk_lst.Items)
            {
                if (li.Selected == true)
                {
                    if (Int32.Parse(rcount) > count1)
                    {
                        ss1 = ",";
                    }
                    else
                    {
                        ss1 = "";
                    }

                    selectedValues += li.Value + ss1;

                    count1++;
                }
                rcount1 = count1.ToString();
            }

            SqlCommand prof_upd = new SqlCommand("UPDATE hr_cmn_appr_section SET [cse_section_desc] = @cse_section_desc, [cse_info] = @cse_info,[cse_marks] = @cse_marks,[cse_sec_jawatan]=@cse_sec_jawatan,[cse_sec_type]=@cse_sec_type, [cse_upd_id] = @cse_upd_id, [cse_upd_dt] = @cse_upd_dt WHERE cse_section_cd ='" + TextBox1.Text + "' ", con);
            prof_upd.Parameters.AddWithValue("cse_section_desc", txt_keteran.Text);
            prof_upd.Parameters.AddWithValue("cse_info", text_area.Text);
            prof_upd.Parameters.AddWithValue("cse_marks", TextBox2.Text);
            prof_upd.Parameters.AddWithValue("cse_sec_jawatan", selectedValues);
            prof_upd.Parameters.AddWithValue("cse_sec_type", chk_val1);
            prof_upd.Parameters.AddWithValue("cse_upd_id", Session["New"].ToString());
            prof_upd.Parameters.AddWithValue("cse_upd_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            con.Open();
            int j = prof_upd.ExecuteNonQuery();
            con.Close();
            Session["validate_success"] = "SUCCESS";
            Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
            Response.Redirect("../SUMBER_MANUSIA/HR_BAHAGIAN_PENILAIAN_view.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Bidang.',{'type': 'warning ,'title':'warning','auto_close':2000});", true);
        }
        btn_simp.Visible = true;
        btb_kmes.Visible = false;
    }
   
   
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["con_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_BAHAGIAN_PENILAIAN.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_BAHAGIAN_PENILAIAN_view.aspx");
    }


}