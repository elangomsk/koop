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
using System.Threading;

public partial class HR_ORGNISASI : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    DataTable dt = new DataTable();
    string level, userid, stscd, abc1;
    string ogid;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                NegriBind();
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                }
                else
                {
                    ImgPrv.Attributes.Add("src", "../files/org_logo/user.png");
                    refno.Text = "0";
                }
                userid = Session["New"].ToString();


            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void app_language()
    {
        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
        ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

        DataTable gt_lng = new DataTable();
        gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('564','448','1520','1415','1521','496','27','1522','1523','28','1524','1420','1525','29','1036','1141','1526','1527','1528','37','190','16', '61', '15', '77')");

        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;

        h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower()); //SELENGGARA

        bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower()); //HR
        bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower()); //SELENGGARA MAKLUMAT ORGANISASI

        h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());  //SELENGGARA MAKLUMAT ORGANISASI
        h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());  //Maklumat Penjawatan
                
        lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower()); //No. Daftar Syarikat
        lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower()); //Nama Syarikat 
        lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower()); //Alamat
        lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString()); //No KWSP
        lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString()); //No PERKESO
        lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower()); //Poskod
        lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower()); //No Cukai Pendapatan
        lbl8_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower()); //Bandar
        lbl9_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower()); //No COID
        lbl10_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower()); //Negeri
        lbl11_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower()); //Email
        lbl12_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower()); //No Telefon
        lbl13_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower()); //No Fax
        lbl14_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower()); //Sektor
        lbl15_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower()); //Lampiran

        lbl16_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower()); //No KP Baru
        lbl17_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());  //No KP Baru               
        lbl18_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower()); //Nama
        lbl19_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower()); //Nama
        lbl20_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower()); //Jawatan
        lbl21_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower()); //Jawatan        
        
        Button8.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower()); //Simpan 
        Button10.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower()); //Set Semula
        Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower()); //Kembali 
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void view_details()
    {
        try
        {
            Button10.Visible = false;
            ogid = lbl_name.Text;
           
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * from hr_organization where org_gen_id='" + ogid + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                Label6.Text = ogid;
                org_no.Attributes.Add("Readonly", "Readonly");
                txt_negeri.Attributes.Add("Readonly", "Readonly");
                DD_NegriBind1.Visible = false;
                txt_negeri.Visible = true;
                refno.Text = "1";
                Button8.Text = "Kemaskini";
                org_no.Text = ddokdicno.Rows[0]["org_id"].ToString();
                org_name.Text = ddokdicno.Rows[0]["org_name"].ToString();
                org_address.Value = ddokdicno.Rows[0]["org_address"].ToString();
                org_postcd.Text = ddokdicno.Rows[0]["org_postcd"].ToString();
                org_city.Text = ddokdicno.Rows[0]["org_city"].ToString();
                DD_NegriBind1.SelectedValue = ddokdicno.Rows[0]["org_state_cd"].ToString();
                txt_negeri.Text = DD_NegriBind1.SelectedItem.Text;
                org_epf_no.Text = ddokdicno.Rows[0]["org_epf_no"].ToString();
                org_socso_no.Text = ddokdicno.Rows[0]["org_socso_no"].ToString();
                org_income_tax_no.Text = ddokdicno.Rows[0]["org_income_tax_no"].ToString();
                org_comp_sector.Text = ddokdicno.Rows[0]["org_comp_sector"].ToString();
                org_co_id.Text = ddokdicno.Rows[0]["org_co_id"].ToString();
                org_phone_no.Text = ddokdicno.Rows[0]["org_phone_no"].ToString();
                org_fax_no.Text = ddokdicno.Rows[0]["org_fax_no"].ToString();
                org_email.Text = ddokdicno.Rows[0]["org_email"].ToString();
                org_contact_icno1.Text = ddokdicno.Rows[0]["org_contact_icno1"].ToString();
                org_contact_name1.Text = ddokdicno.Rows[0]["org_contact_name1"].ToString();
                org_contact_post1.Text = ddokdicno.Rows[0]["org_contact_post1"].ToString();
                org_contact_icno2.Text = ddokdicno.Rows[0]["org_contact_icno2"].ToString();
                org_contact_name2.Text = ddokdicno.Rows[0]["org_contact_name2"].ToString();
                org_contact_post2.Text = ddokdicno.Rows[0]["org_contact_post2"].ToString();

                if (ddokdicno.Rows[0]["org_temp_ind"].ToString() != "")
                {

                    ImgPrv.Attributes.Add("src", "../FILES/org_logo/" + ddokdicno.Rows[0]["org_temp_ind"].ToString());
                    imname.Text = ddokdicno.Rows[0]["org_temp_ind"].ToString();
                }
                else
                {
                    ImgPrv.Attributes.Add("src", "../FILES/org_logo/user.png");
                    imname.Text = "";
                }


            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    void NegriBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select DISTINCT hr_negeri_Code, UPPER(hr_negeri_desc) as hr_negeri_desc from Ref_hr_negeri  WHERE Status = 'A' order by hr_negeri_desc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_NegriBind1.DataSource = dt;
            DD_NegriBind1.DataTextField = "hr_negeri_desc";
            DD_NegriBind1.DataValueField = "hr_negeri_Code";
            DD_NegriBind1.DataBind();
            DD_NegriBind1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected void insert_Click(object sender, EventArgs e)
    {
        try
        {

            if (org_no.Text != "" && DD_NegriBind1.SelectedItem.Value != "")
            {
                string ck = string.Empty;
                int contentLength = FileUpload1.PostedFile.ContentLength;//You may need it for validation
                string contentType = FileUpload1.PostedFile.ContentType;//You may need it for validation
                string fileName = FileUpload1.PostedFile.FileName;
                string fname = string.Empty;
                if (fileName != "")
                {
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/org_logo/" + fileName));//Or code to save in the DataBase.
                    fname = fileName;
                }
                else if (imname.Text != "")
                {
                    fname = imname.Text;
                }
                if (refno.Text == "0")
                {
                   
                    DataTable dd_cntorg = new DataTable();
                    string genid;
                    dd_cntorg = DBCon.Ora_Execute_table("select top(1) (org_gen_id + 1) as cnt_org from hr_organization order by cast(org_gen_id as int) desc");
                    if (dd_cntorg.Rows.Count == 0)
                    {
                         genid = "1";
                    }
                    else
                    {
                         genid = dd_cntorg.Rows[0]["cnt_org"].ToString();
                    }

                    DataTable ddokdicno2 = new DataTable();
                    
                    string Inssql = "INSERT INTO hr_organization (org_id,org_name,org_address,org_postcd,org_city,org_state_cd,org_epf_no,org_socso_no,org_income_tax_no,org_comp_sector,org_co_id,org_phone_no,org_fax_no,org_email,org_contact_icno1,org_contact_name1,org_contact_post1,org_contact_icno2,org_contact_name2,org_contact_post2,org_crt_id,org_crt_dt,org_gen_id,org_temp_ind) VALUES ('" + org_no.Text + "','" + org_name.Text.ToUpper().Replace("'", "''") + "','" + org_address.Value + "','" + org_postcd.Text + "','" + org_city.Text + "','" + DD_NegriBind1.SelectedValue + "','" + org_epf_no.Text + "','" + org_socso_no.Text + "','" + org_income_tax_no.Text + "','" + org_comp_sector.Text + "','" + org_co_id.Text + "','" + org_phone_no.Text + "','" + org_fax_no.Text + "','" + org_email.Text + "','" + org_contact_icno1.Text + "','" + org_contact_name1.Text.Replace("'", "''") + "','" + org_contact_post1.Text + "','" + org_contact_icno2.Text + "','" + org_contact_name2.Text.Replace("'", "''") + "','" + org_contact_post2.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + genid.ToString() + "','" + fname + "')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        service.audit_trail("P0100", "Simpan", "Nama Organisasi", org_name.Text);
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Response.Redirect("../SUMBER_MANUSIA/HR_ORGNISASI_view.aspx");
                    }
                   
                }
                else
                {
                    string Inssql = "update hr_organization set org_temp_ind='" + fname + "', org_id='" + org_no.Text + "',org_name='" + org_name.Text.ToUpper().Replace("'", "''") + "',org_address='" + org_address.Value + "',org_postcd='" + org_postcd.Text + "',org_city='" + org_city.Text + "',org_epf_no='" + org_epf_no.Text + "',org_socso_no='" + org_socso_no.Text + "',org_income_tax_no='" + org_income_tax_no.Text + "',org_comp_sector='" + org_comp_sector.Text + "',org_co_id='" + org_co_id.Text + "',org_phone_no='" + org_phone_no.Text + "',org_fax_no='" + org_fax_no.Text + "',org_email='" + org_email.Text + "',org_contact_icno1='" + org_contact_icno1.Text + "',org_contact_name1='" + org_contact_name1.Text.Replace("'", "''") + "',org_contact_post1='" + org_contact_post1.Text + "',org_contact_icno2='" + org_contact_icno2.Text + "',org_contact_name2='" + org_contact_name2.Text.Replace("'", "''") + "',org_contact_post2='" + org_contact_post2.Text + "',org_upd_id='" + Session["New"].ToString() + "',org_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where org_id = '" + org_no.Text + "' and org_gen_id='" + Label6.Text + "'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);

                    if (Status == "SUCCESS")
                    {
                        service.audit_trail("P0100", "Kemaskini", "Nama Organisasi", org_name.Text);
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                        Response.Redirect("../SUMBER_MANUSIA/HR_ORGNISASI_view.aspx");
                    }
                                 
                    
                }
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void clr()
    {
        org_no.Text = "";
        org_name.Text = "";
        org_address.Value = "";
        org_postcd.Text = "";
        org_city.Text = "";
        DD_NegriBind1.SelectedValue = "";
        org_epf_no.Text = "";
        org_socso_no.Text = "";
        org_income_tax_no.Text = "";
        org_comp_sector.Text = "";
        org_co_id.Text = "";
        org_phone_no.Text = "";
        org_fax_no.Text = "";
        org_email.Text = "";
        org_contact_icno1.Text = "";
        org_contact_name1.Text = "";
        org_contact_post1.Text = "";
        org_contact_icno2.Text = "";
        org_contact_name2.Text = "";
        org_contact_post2.Text = "";
        //temp_ch1.Checked = false;
        //temp_ch2.Checked = false;
        //temp_ch3.Checked = false;
        imname.Text = "";
        ImgPrv.Attributes.Add("src", "../files/org_logo/user.png");

    }

   
 
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_ORGNISASI.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_ORGNISASI_view.aspx");
    }


}