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

public partial class HR_SELE_Mcuti_view : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string level;
    string Status = string.Empty;
    string userid;
    string ref_id;
    string confirmValue, am;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty;
    int chk_v1 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success'});", true);

                }
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";
                userid = Session["New"].ToString();
                ann_rno.Text = "0";
                com_rno.Text = "0";
                out_rno.Text = "0";
                l1.Attributes.Add("style", "display:none");
                l2.Attributes.Add("style", "display:none");
                l3.Attributes.Add("style", "display:none");
                l4.Attributes.Add("style", "display:none");
                l_umum.Attributes.Add("style", "display:none");
                JcutiBind();
                JcutiBind1();
                JawaBind();
                KatJawaBind();
                grid_annual();
                grid_comp();
                grid_out();
                NegriBind();
                OrgBind();
                M_title.Text = "&nbsp;";

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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('508','448','473','190','29','1397','1399','1400','1401','1403','1405','1407','1408','67','61','883','133')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;


            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower()); 
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());

            //M_title.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());  
                      
            lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower()); //Jenis Cuti
            lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower()); //Jawatan
            //lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower()); //Kategori Jawatan
            lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower()); //Tempoh Khidmat Minimum (Tahun)
            lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower()); //Tempoh Khidmat Maksimum (Tahun)
            lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower()); //Kelayakan Cuti (Hari)

            lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower()); //Kategori Cuti
            lbl8_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower()); //Kelayakan Cuti (Hari)
            lbl9_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower()); //Tempoh Khidmat Minimum (Tahun)
            lbl10_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower()); //Tempoh Khidmat Maksimum (Tahun)
            lbl11_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower()); //Kelayakan Cuti (Hari)
            //lbl12_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower()); //Kelayakan Cuti (Hari)

            lbl13_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower()); //Organisasi
            lbl14_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower()); //Negeri
            lbl15_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower()); //Dari
            lbl16_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower()); //Sehingga
            lbl17_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());  //Keterangan         

            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower()); //Simpan
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower()); //Batal
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower()); //Simpan
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower()); //Hapus

            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower()); //Simpan
            Button7.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower()); //Hapus
                        
            Button8.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower()); //Simpan
            Button10.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower()); //Hapus
            Button11.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower()); //Simpan
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    void OrgBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select DISTINCT org_id, UPPER(org_name) as org_name from hr_organization  order by org_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "org_name";
            DropDownList1.DataValueField = "org_id";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void JcutiBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_jenis_Code,UPPER(hr_jenis_desc) as hr_jenis_desc from Ref_hr_jenis_cuti WHERE Status = 'A' and hr_jenis_Code NOT IN ('15','02','07','08','13') ORDER BY hr_jenis_desc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_jcuti.DataSource = dt;
            dd_jcuti.DataTextField = "hr_jenis_desc";
            dd_jcuti.DataValueField = "hr_jenis_Code";
            dd_jcuti.DataBind();
            dd_jcuti.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void JcutiBind1()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_kategori_Code,upper(hr_kategori_desc) as hr_kategori_desc from Ref_hr_kategori_cuti";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            com_cuti.DataSource = dt;
            com_cuti.DataTextField = "hr_kategori_desc";
            com_cuti.DataValueField = "hr_kategori_Code";
            com_cuti.DataBind();
            com_cuti.Items.Insert(0, new ListItem("--- PILIH ---", ""));


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void JawaBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_jaw_Code, UPPER(hr_jaw_desc) as hr_jaw_desc from Ref_hr_Jawatan  WHERE Status = 'A' order by hr_jaw_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            an_jawa.DataSource = dt;
            an_jawa.DataTextField = "hr_jaw_desc";
            an_jawa.DataValueField = "hr_jaw_Code";
            an_jawa.DataBind();
            an_jawa.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void KatJawaBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_kate_Code, UPPER(hr_kate_desc) as hr_kate_desc from Ref_hr_penj_kategori  WHERE Status = 'A' order by hr_kate_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            an_katjawa.DataSource = dt;
            an_katjawa.DataTextField = "hr_kate_desc";
            an_katjawa.DataValueField = "hr_kate_Code";
            an_katjawa.DataBind();
            an_katjawa.Items.Insert(0, new ListItem("--- PILIH ---", ""));


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
            string com = "select hr_negeri_Code, UPPER(hr_negeri_desc) as hr_negeri_desc from Ref_hr_negeri  WHERE Status = 'A' order by hr_negeri_desc";
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


    void grid_annual()
    {
        SqlCommand cmd2 = new SqlCommand("select pk.hr_kate_desc,al.ann_post_cd,rj.hr_jaw_desc,al.ann_post_cat_cd,al.ann_min_service,al.ann_max_service,al.ann_leave_day,al.ann_leave_duration,al.ann_leave_type_cd from hr_cmn_annual_leave as al left join Ref_hr_penj_kategori as pk on pk.hr_kate_Code = al.ann_post_cat_cd left join Ref_hr_Jawatan as rj on rj.hr_jaw_Code=al.ann_post_cd where ann_leave_type_cd='" + dd_jcuti.SelectedValue +"' ORDER BY ann_post_cd", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }
    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid_annual();

    }


    void grid_comp()
    {
        SqlCommand cmd2 = new SqlCommand("select cl.com_cat_cd,rc.hr_jenis_desc,cl.com_leave_day from hr_cmn_com_leave as cl left join Ref_hr_jenis_cuti as rc on rc.hr_jenis_Code=cl.com_cat_cd ORDER BY com_cat_cd", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView2.DataSource = ds2;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            GridView2.DataSource = ds2;
            GridView2.DataBind();
        }
    }
    protected void gvSelected_PageIndexChanging_comp(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        grid_comp();

    }

    void grid_out()
    {
        SqlCommand cmd2 = new SqlCommand("select * from hr_cmn_outpatient_leave ORDER BY out_min_yr", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView3.DataSource = ds2;
            GridView3.DataBind();
            int columncount = GridView3.Rows[0].Cells.Count;
            GridView3.Rows[0].Cells.Clear();
            GridView3.Rows[0].Cells.Add(new TableCell());
            GridView3.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView3.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            GridView3.DataSource = ds2;
            GridView3.DataBind();
        }
    }
    protected void gvSelected_PageIndexChanging_out(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        grid_out();

    }

    protected void Annual_insert_Click(object sender, EventArgs e)
    {
        try
        {

            if (an_tk_min.Text != "")
            {
                if (chk_man_ann.Checked == true)
                {
                    chk_v1 = 1;
                }
                if (ann_rno.Text == "0")
                {
                    DataTable ddokdicno2 = new DataTable();
                    ddokdicno2 = DBCon.Ora_Execute_table("select * from hr_cmn_annual_leave where ann_post_cat_cd='" + an_katjawa.SelectedValue + "' and ann_min_service='" + an_tk_min.Text + "' and ann_leave_type_cd='" + dd_jcuti.SelectedValue + "'");
                    if (ddokdicno2.Rows.Count == 0)
                    {
                        
                      
                     

                        DBCon.Ora_Execute_table("INSERT INTO hr_cmn_annual_leave (ann_post_cd,ann_post_cat_cd,ann_min_service,ann_max_service,ann_leave_day,ann_crt_id,ann_crt_dt,ann_leave_duration,ann_leave_type_cd,chk_lampiran) VALUES ('"+ an_jawa.SelectedValue + "','" + an_katjawa.SelectedValue + "','" + an_tk_min.Text + "','" + an_tk_mak.Text + "','" + an_kel_cuti.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','"+ TextBox3.Text + "','"+ dd_jcuti.SelectedValue +"','"+ chk_v1 + "')");                        
                        grid_annual();
                        //an_jawa.SelectedValue = "";
                        //an_katjawa.SelectedValue = "";
                        an_tk_min.Text = "";
                        an_tk_mak.Text = "";
                        an_kel_cuti.Text = "";
                        TextBox3.Text = "";
                        chk_man_ann.Checked = false;
                        service.audit_trail("P0057", "Simpan", "JENIS CUTI", dd_jcuti.SelectedItem.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                    }
                    else
                    {
                        grid_annual();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    //DataTable ddokdicno2 = new DataTable();
                    //ddokdicno2 = DBCon.Ora_Execute_table("select * from hr_cmn_annual_leave where ann_post_cd='" + an_jawa.SelectedValue + "' and ann_post_cat_cd='" + an_katjawa.SelectedValue + "' and ann_min_service='" + an_tk_min.Text + "' and ann_leave_type_cd='" + dd_jcuti.SelectedValue + "'");
                    //if (ddokdicno2.Rows.Count == 0)
                    //{
                        DBCon.Ora_Execute_table("update hr_cmn_annual_leave set chk_lampiran='"+ chk_v1 + "',ann_leave_type_cd='" + dd_jcuti.SelectedValue +"',ann_leave_duration='" + TextBox3.Text + "',ann_post_cat_cd='" + an_katjawa.SelectedValue + "',ann_max_service='" + an_tk_mak.Text + "',ann_leave_day='" + an_kel_cuti.Text + "',ann_upd_id='" + Session["New"].ToString() + "',ann_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where ann_min_service='" + an_tk_min.Text + "' and ann_leave_type_cd='" + dd_jcuti.SelectedValue + "'");                    
                    grid_annual();
                    Button5.Text = "Simpan";
                    //an_jawa.Attributes.Remove("Readonly");
                    an_tk_min.Attributes.Remove("Readonly");
                    service.audit_trail("P0057", "Kemaskini", "JENIS CUTI", dd_jcuti.SelectedItem.Text);
                    //an_jawa.SelectedValue = "";
                    //an_katjawa.SelectedValue = "";
                    an_tk_min.Text = "";
                    an_tk_mak.Text = "";
                    an_kel_cuti.Text = "";
                    TextBox3.Text = "";
                    ann_rno.Text = "0";
                    chk_man_ann.Checked = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    //}
                    //else
                    //{
                    //    grid_annual();
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    //}
                }
            }
            else
            {
                grid_annual();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void Comp_insert_Click(object sender, EventArgs e)
    {
        try
        {

            if (com_kel_cuti.Text != "" && com_cuti.SelectedValue != "")
            {
                if (com_rno.Text == "0")
                {
                    DataTable ddokdicno2 = new DataTable();
                    ddokdicno2 = DBCon.Ora_Execute_table("select * from hr_cmn_com_leave where com_cat_cd='" + com_cuti.SelectedValue + "'");
                    if (ddokdicno2.Rows.Count == 0)
                    {
                        DBCon.Ora_Execute_table("INSERT INTO hr_cmn_com_leave (com_cat_cd,com_leave_day,com_crt_id,com_crt_dt) VALUES ('" + com_cuti.SelectedValue + "','" + com_kel_cuti.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')");                        
                        grid_comp();
                        com_cuti.SelectedValue = "";
                        com_kel_cuti.Text = "";
                        service.audit_trail("P0057", "Simpan","JENIS CUTI", dd_jcuti.SelectedItem.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                    }
                    else
                    {
                        grid_comp();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    DBCon.Ora_Execute_table("update hr_cmn_com_leave set com_leave_day='" + com_kel_cuti.Text + "',com_upd_id='" + Session["New"].ToString() + "',com_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where com_cat_cd='" + com_cuti.SelectedValue + "'");                    
                    grid_comp();
                    Button2.Text = "Simpan";
                    com_cuti.Attributes.Remove("Readonly");
                    com_cuti.SelectedValue = "";
                    com_kel_cuti.Text = "";
                    com_rno.Text = "0";
                    service.audit_trail("P0057", "Kemaskini", "JENIS CUTI", dd_jcuti.SelectedItem.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }
            else
            {
                grid_comp();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void out_insert_Click(object sender, EventArgs e)
    {
        try
        {

            if (out_tk_min.Text != "")
            {
                if (out_rno.Text == "0")
                {
                    DataTable ddokdicno2 = new DataTable();
                    ddokdicno2 = DBCon.Ora_Execute_table("select * from hr_cmn_outpatient_leave where out_min_yr='" + out_tk_min.Text + "'");
                    if (ddokdicno2.Rows.Count == 0)
                    {
                        DBCon.Ora_Execute_table("INSERT INTO hr_cmn_outpatient_leave (out_min_yr,out_max_yr,out_leave_day,out_crt_id,out_crt_dt) VALUES ('" + out_tk_min.Text + "','" + out_tk_mak.Text + "','" + out_kel_cuti.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')");                        
                        grid_out();
                        out_tk_min.Text = "";
                        out_tk_mak.Text = "";
                        out_kel_cuti.Text = "";
                        service.audit_trail("P0057", "Simpan", "JENIS CUTI", dd_jcuti.SelectedItem.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                    }
                    else
                    {
                        grid_out();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    DBCon.Ora_Execute_table("update hr_cmn_outpatient_leave set out_max_yr='" + out_tk_mak.Text + "',out_leave_day='" + out_kel_cuti.Text + "',out_upd_id='" + Session["New"].ToString() + "',out_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where out_min_yr='" + out_tk_min.Text + "'");                    
                    grid_out();
                    Button8.Text = "Simpan";
                    out_tk_min.Attributes.Remove("Readonly");
                    out_tk_min.Text = "";
                    out_tk_mak.Text = "";
                    out_kel_cuti.Text = "";
                    out_rno.Text = "0";
                    service.audit_trail("P0057", "Kemaskini", "JENIS CUTI", dd_jcuti.SelectedItem.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }
            else
            {
                grid_out();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gen_insert_Click(object sender, EventArgs e)
    {
        try
        {

            if (dd_jcuti.SelectedValue != "" && gen_kel_cuti.Text != "" && TextBox4.Text != "")
            {
                if (chk_man.Checked == true)
                {
                    chk_v1 = 1;
                }

                DataTable ddokdicno2 = new DataTable();
                ddokdicno2 = DBCon.Ora_Execute_table("select * from hr_cmn_general_leave where gen_leave_type_cd='" + dd_jcuti.SelectedValue + "'");
                if (ddokdicno2.Rows.Count == 0)
                {
                    DBCon.Ora_Execute_table("INSERT INTO hr_cmn_general_leave (gen_leave_type_cd,gen_leave_day,gen_crt_id,gen_crt_dt,gen_leave_duration,chk_lampiran) VALUES ('" + dd_jcuti.SelectedValue + "','" + gen_kel_cuti.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','"+ TextBox4.Text + "','"+ chk_v1 +"')");
                    service.audit_trail("P0057", "Simpan", "JENIS CUTI", dd_jcuti.SelectedItem.Text);
                    Button11.Text = "Simpan";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                }
                else
                {
                    DBCon.Ora_Execute_table("update hr_cmn_general_leave set chk_lampiran='" + chk_v1 + "',gen_leave_duration='" + TextBox4.Text +"',gen_leave_day='" + gen_kel_cuti.Text + "',gen_upd_id='" + Session["New"].ToString() + "',gen_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where gen_leave_type_cd='" + dd_jcuti.SelectedValue + "'");
                    service.audit_trail("P0057", "Kemaskini", "JENIS CUTI", dd_jcuti.SelectedItem.Text);
                    Button11.Text = "Kemaskini";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void gen_leave()
    {
        try
        {
            DataTable gen_leave = new DataTable();
            gen_leave = DBCon.Ora_Execute_table("select gen_leave_duration,gen_leave_day,chk_lampiran from hr_cmn_general_leave where gen_leave_type_cd='" + dd_jcuti.SelectedValue + "'");
            if (gen_leave.Rows.Count != 0)
            {
                Button11.Text = "Kemaskini";
                gen_kel_cuti.Text = gen_leave.Rows[0]["gen_leave_day"].ToString();
                TextBox4.Text = gen_leave.Rows[0]["gen_leave_duration"].ToString();
                if (gen_leave.Rows[0]["chk_lampiran"].ToString() == "1")
                {
                    chk_man.Checked = true;
                }
                else
                {
                    chk_man.Checked = false;
                }
            }
            else
            {
                gen_kel_cuti.Text = "";
                TextBox4.Text = "";
                Button11.Text = "Simpan";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Cuti Tidak Wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label jaw_code = (System.Web.UI.WebControls.Label)gvRow.FindControl("pos_cd");
            System.Web.UI.WebControls.Label mser = (System.Web.UI.WebControls.Label)gvRow.FindControl("min_ser");
            System.Web.UI.WebControls.Label actyear = (System.Web.UI.WebControls.Label)gvRow.FindControl("ser_year");
            System.Web.UI.WebControls.Label lbl7 = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label7");
            string jcd = jaw_code.Text;
            string minser = mser.Text;

            DataTable ann_leave_det = new DataTable();
            ann_leave_det = DBCon.Ora_Execute_table("select * from hr_cmn_annual_leave where ann_post_cd='" + jcd + "' and ann_min_service='" + minser + "' and ann_leave_type_cd='" + lbl7.Text + "'");
            if (ann_leave_det.Rows.Count != 0)
            {
                Button5.Text = "Kemaskini";
                ann_rno.Text = "1";
                an_jawa.Attributes.Add("Readonly", "Readonly");
                an_tk_min.Attributes.Add("Readonly", "Readonly");
                an_jawa.SelectedValue = ann_leave_det.Rows[0]["ann_post_cd"].ToString().Trim();
                an_katjawa.SelectedValue = ann_leave_det.Rows[0]["ann_post_cat_cd"].ToString().Trim();
                an_tk_min.Text = ann_leave_det.Rows[0]["ann_min_service"].ToString();
                an_tk_mak.Text = ann_leave_det.Rows[0]["ann_max_service"].ToString();
                an_kel_cuti.Text = ann_leave_det.Rows[0]["ann_leave_day"].ToString();
                TextBox3.Text = ann_leave_det.Rows[0]["ann_leave_duration"].ToString();
                if (ann_leave_det.Rows[0]["chk_lampiran"].ToString() == "1")
                {
                    chk_man_ann.Checked = true;
                }
                else
                {
                    chk_man_ann.Checked = false;
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lnkView_Click_comp(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label ccd = (System.Web.UI.WebControls.Label)gvRow.FindControl("com_cd");
            string jcode = ccd.Text;

            DataTable com_leave_det = new DataTable();
            com_leave_det = DBCon.Ora_Execute_table("select * from hr_cmn_com_leave where com_cat_cd='" + jcode + "'");
            if (com_leave_det.Rows.Count != 0)
            {
                Button2.Text = "Kemaskini";
                com_rno.Text = "1";
                com_cuti.Attributes.Add("Readonly", "Readonly");
                com_cuti.SelectedValue = com_leave_det.Rows[0]["com_cat_cd"].ToString().Trim();
                com_kel_cuti.Text = com_leave_det.Rows[0]["com_leave_day"].ToString();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lnkView_Click_out(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label ccd = (System.Web.UI.WebControls.Label)gvRow.FindControl("omin_yr");
            string jcode = ccd.Text;

            DataTable out_leave_det = new DataTable();
            out_leave_det = DBCon.Ora_Execute_table("select * from hr_cmn_outpatient_leave where out_min_yr='" + jcode + "'");
            if (out_leave_det.Rows.Count != 0)
            {
                Button8.Text = "Kemaskini";
                out_rno.Text = "1";
                out_tk_min.Attributes.Add("Readonly", "Readonly");
                out_tk_min.Text = out_leave_det.Rows[0]["out_min_yr"].ToString().Trim();
                out_tk_mak.Text = out_leave_det.Rows[0]["out_max_yr"].ToString().Trim();
                out_kel_cuti.Text = out_leave_det.Rows[0]["out_leave_day"].ToString();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void dd_jeniscuti(object sender, EventArgs e)
    {
        if (dd_jcuti.SelectedValue == "01" || dd_jcuti.SelectedValue == "04")
        {
          
            M_title.Text = dd_jcuti.SelectedItem.Text;
            l1.Attributes.Remove("style");
            l2.Attributes.Add("style", "display:none");
            l3.Attributes.Add("style", "display:none");
            l4.Attributes.Add("style", "display:none");
            l_umum.Attributes.Add("style", "display:none");
            grid_annual();
        }
        
        else
        {
            M_title.Text = dd_jcuti.SelectedItem.Text;
            l4.Attributes.Remove("style");
            l2.Attributes.Add("style", "display:none");
            l3.Attributes.Add("style", "display:none");
            l1.Attributes.Add("style", "display:none");
            l_umum.Attributes.Add("style", "display:none");
            gen_leave();
        }
    }

    protected void OnCheckedChanged_ann(object sender, EventArgs e)
    {
        bool isUpdateVisible = false;
        System.Web.UI.WebControls.CheckBox chk = (sender as System.Web.UI.WebControls.CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[0].Controls.OfType<System.Web.UI.WebControls.CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        System.Web.UI.WebControls.CheckBox chkAll = (GridView1.HeaderRow.FindControl("chkAll") as System.Web.UI.WebControls.CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[0].Controls.OfType<System.Web.UI.WebControls.CheckBox>().FirstOrDefault().Checked;
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    if (isChecked && !isUpdateVisible)
                    {
                        isUpdateVisible = true;
                    }
                    if (!isChecked)
                    {
                        chkAll.Checked = false;

                    }
                }
            }
        }

    }

    protected void OnCheckedChanged_com(object sender, EventArgs e)
    {
        bool isUpdateVisible = false;
        System.Web.UI.WebControls.CheckBox chk = (sender as System.Web.UI.WebControls.CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[0].Controls.OfType<System.Web.UI.WebControls.CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        System.Web.UI.WebControls.CheckBox chkAll = (GridView1.HeaderRow.FindControl("chkAll") as System.Web.UI.WebControls.CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in GridView2.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[0].Controls.OfType<System.Web.UI.WebControls.CheckBox>().FirstOrDefault().Checked;
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    if (isChecked && !isUpdateVisible)
                    {
                        isUpdateVisible = true;
                    }
                    if (!isChecked)
                    {
                        chkAll.Checked = false;

                    }
                }
            }
        }

    }

    protected void OnCheckedChanged_out(object sender, EventArgs e)
    {
        bool isUpdateVisible = false;
        System.Web.UI.WebControls.CheckBox chk = (sender as System.Web.UI.WebControls.CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in GridView3.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[0].Controls.OfType<System.Web.UI.WebControls.CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        System.Web.UI.WebControls.CheckBox chkAll = (GridView1.HeaderRow.FindControl("chkAll") as System.Web.UI.WebControls.CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in GridView3.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[0].Controls.OfType<System.Web.UI.WebControls.CheckBox>().FirstOrDefault().Checked;
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    if (isChecked && !isUpdateVisible)
                    {
                        isUpdateVisible = true;
                    }
                    if (!isChecked)
                    {
                        chkAll.Checked = false;

                    }
                }
            }
        }

    }

    protected void ann_hapus_click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
            if (checkbox.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
                if (checkbox.Checked == true)
                {
                    var lblID = gvrow.FindControl("pos_cd") as System.Web.UI.WebControls.Label;
                    var seqno = gvrow.FindControl("min_ser") as System.Web.UI.WebControls.Label;
                    DBCon.Execute_CommamdText("DELETE hr_cmn_annual_leave WHERE ann_post_cd='" + lblID.Text + "' and ann_min_service= '" + seqno.Text + "'");
                }
            }
            Button5.Text = "Simpan";
            //an_jawa.Attributes.Remove("Readonly");
            an_tk_min.Attributes.Remove("Readonly");
            //an_jawa.SelectedValue = "";
            //an_katjawa.SelectedValue = "";
            an_tk_min.Text = "";
            an_tk_mak.Text = "";
            an_kel_cuti.Text = "";
            ann_rno.Text = "0";
            grid_annual();
            service.audit_trail("P0057", "Hapus", "JENIS CUTI", dd_jcuti.SelectedItem.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Kotak One Daftar Minimum',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    protected void com_hapus_click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView2.Rows)
        {
            var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
            if (checkbox.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow gvrow in GridView2.Rows)
            {
                var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
                if (checkbox.Checked == true)
                {
                    var lblID = gvrow.FindControl("com_cd") as System.Web.UI.WebControls.Label;
                    DBCon.Execute_CommamdText("DELETE hr_cmn_com_leave WHERE com_cat_cd='" + lblID.Text + "'");
                }
            }
            Button2.Text = "Simpan";
            com_cuti.Attributes.Remove("Readonly");
            com_cuti.SelectedValue = "";
            com_kel_cuti.Text = "";
            com_rno.Text = "0";
            grid_comp();
            service.audit_trail("P0057", "Hapus", "JENIS CUTI", dd_jcuti.SelectedItem.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Kotak One Daftar Minimum',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void out_hapus_click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView3.Rows)
        {
            var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
            if (checkbox.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow gvrow in GridView3.Rows)
            {
                var checkbox = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
                if (checkbox.Checked == true)
                {
                    var lblID = gvrow.FindControl("omin_yr") as System.Web.UI.WebControls.Label;
                    DBCon.Execute_CommamdText("DELETE hr_cmn_outpatient_leave WHERE out_min_yr='" + lblID.Text + "'");
                }
            }
            Button8.Text = "Simpan";
            out_tk_min.Attributes.Remove("Readonly");
            out_tk_min.Text = "";
            out_tk_mak.Text = "";
            out_kel_cuti.Text = "";
            out_rno.Text = "0";
            grid_out();
            service.audit_trail("P0057", "Hapus", "JENIS CUTI", dd_jcuti.SelectedItem.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Kotak One Daftar Minimum',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void umum_insert_Click(object sender, EventArgs e)
    {
        try
        {
            if (dd_jcuti.SelectedItem.Text != "CUTI HUJUNG MINGGU")
            {
                v1.Visible = false;
                if (dd_jcuti.SelectedItem.Value != "" && DropDownList1.SelectedItem.Value != "" && DD_NegriBind1.SelectedItem.Value != "" && td_date.Text != "" && ts_date.Text != "")
                {
                    string month = string.Empty;
                    string year = string.Empty;
                    string v1_1 = td_date.Text;
                    DateTime dt_1 = DateTime.ParseExact(v1_1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string fdate = dt_1.ToString("MM/dd/yyyy");

                    string v1_2 = ts_date.Text;
                    DateTime dt_2 = DateTime.ParseExact(v1_2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //string tdate = dt_2.ToString("dd/MM/yyyy");
                    double dcount = (dt_2 - dt_1).TotalDays;


                    for (int i = 0; i <= dcount; i++)
                    {
                        DateTime mm = Convert.ToDateTime(fdate);
                        DateTime mm1 = mm.AddDays(i);
                        string pdt_month = mm1.ToString("yyyy-MM-dd");
                        DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + DropDownList1.SelectedItem.Value + "','" + DD_NegriBind1.SelectedItem.Value + "','" + dd_jcuti.SelectedItem.Value + "','" + pdt_month + "','" + TextBox1.Text + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','')");
                    }
                    clear_umum();
                    grid_umum();
                    service.audit_trail("P0057", "Simpan", "JENIS CUTI", dd_jcuti.SelectedItem.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    grid_umum();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
              
                if (dd_jcuti.SelectedItem.Value != "" && DropDownList1.SelectedItem.Value != "" && DD_NegriBind1.SelectedItem.Value != "" )
                {

                    double dcount = Convert.ToDouble(TextBox2.Text);


                    for (int i = 0; i <= dcount; i++)
                    {
                      
                        DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt) VALUES ('" + DropDownList1.SelectedItem.Value + "','" + DD_NegriBind1.SelectedItem.Value + "','" + dd_jcuti.SelectedItem.Value + "','','" + TextBox1.Text + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')");
                    }
                    clear_umum();
                    grid_umum();
                    service.audit_trail("P0057", "Simpan", "JENIS CUTI", dd_jcuti.SelectedItem.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    grid_umum();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void clear_umum()
    {

        DropDownList1.SelectedValue = "";
        DD_NegriBind1.SelectedValue = "";
        td_date.Text = "";
        ts_date.Text = "";
        TextBox1.Text = "";
    }

    void grid_umum()
    {
        SqlCommand cmd2 = new SqlCommand("select hol_org_id,hol_state_cd,hol_holiday_cd,FORMAT(hol_dt,'dd/MM/yyyy', 'en-us') as hol_dt,hol_remark,hro.org_name,rn.hr_negeri_desc,jc.hr_jenis_desc from hr_holiday as hrh left join hr_organization as hro on hro.org_id= hrh.hol_org_id left join Ref_hr_jenis_cuti as jc on jc.hr_jenis_Code=hrh.hol_holiday_cd left join Ref_hr_negeri as rn on rn.hr_negeri_Code=hrh.hol_state_cd order by hol_crt_dt", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView4.DataSource = ds2;
            GridView4.DataBind();
            int columncount = GridView4.Rows[0].Cells.Count;
            GridView4.Rows[0].Cells.Clear();
            GridView4.Rows[0].Cells.Add(new TableCell());
            GridView4.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView4.Rows[0].Cells[0].Text = "<strong>Maklumat Carian Tidak Dijumpai</strong>";
        }
        else
        {
            GridView4.DataSource = ds2;
            GridView4.DataBind();
        }
    }

    protected void gvSelected_PageIndexChanging_umum(object sender, GridViewPageEventArgs e)
    {
        GridView4.PageIndex = e.NewPageIndex;
        grid_umum();

    }

    protected void batal_Click(object sender, EventArgs e)
    {

        using (SqlConnection con = new SqlConnection(cs))
        {
            string strDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string rcount = string.Empty;
            int count = 0;
            foreach (GridViewRow gvrow in GridView4.Rows)
            {
                var rb = gvrow.FindControl("chkStatus") as System.Web.UI.WebControls.CheckBox;
                if (rb.Checked == true)
                {
                    count++;
                }
                rcount = count.ToString();
            }
            if (rcount != "0")
            {
                foreach (GridViewRow gvrow in GridView4.Rows)
                {
                    var checkbox = gvrow.FindControl("chkStatus") as System.Web.UI.WebControls.CheckBox;
                    if (checkbox.Checked == true)
                    {
                        var o_id = gvrow.FindControl("oid") as System.Web.UI.WebControls.Label;
                        var h_id = gvrow.FindControl("hid") as System.Web.UI.WebControls.Label;
                        var s_id = gvrow.FindControl("sid") as System.Web.UI.WebControls.Label;
                        var hdate = gvrow.FindControl("Label5") as System.Web.UI.WebControls.Label;
                        String up_date;
                        DateTime bd = DateTime.ParseExact(hdate.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        up_date = bd.ToString("yyyy-mm-dd");

                        string userid = Session["New"].ToString();
                        DBCon.Ora_Execute_table("Update hr_holiday set hol_cancel_ind='Y',hol_upd_id='" + userid + "',hol_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where hol_org_id = '" + o_id.Text + "' AND hol_state_cd = '" + s_id.Text + "' AND hol_holiday_cd = '" + h_id.Text + "' and hol_dt='" + up_date + "'");
                    }

                }                
                grid_umum();
                service.audit_trail("P0057", "Kemaskini", "JENIS CUTI", dd_jcuti.SelectedItem.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Hendak Dibatalkan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
    }

  
}

//protected void gvSelected_PageIndexChanging1(object sender, GridViewPageEventArgs e)
//{
//    GridView1.PageIndex = e.NewPageIndex;
//    GridView1.DataBind();
//    BindData_jenis();
//}
