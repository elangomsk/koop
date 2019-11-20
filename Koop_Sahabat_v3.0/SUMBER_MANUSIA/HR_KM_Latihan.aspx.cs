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


public partial class HR_KM_Latihan : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid, stscd, abc1;
    string phdate1 = string.Empty, phdate2 = string.Empty, phdate3 = string.Empty, phdate4 = string.Empty;
    string etdate1 = string.Empty, etdate2 = string.Empty, etdate3 = string.Empty, etdate4 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                //SebabBind();


                s_nama.Attributes.Add("Readonly", "Readonly");
                s_jaw.Attributes.Add("Readonly", "Readonly");
                s_jab.Attributes.Add("Readonly", "Readonly");
                s_gred.Attributes.Add("Readonly", "Readonly");
                updt_txt.Text = "0";
                //tm_lati.Attributes.Add("Readonly", "Readonly");
                //ta_lati.Attributes.Add("Readonly", "Readonly");
                txt_org.Attributes.Add("Readonly", "Readonly");
                TextBox2.Attributes.Add("Readonly", "Readonly");
                KlatBind();
                JlatBind();
                grid();


                double number = 0;
                yuran_lati.Text = number.ToString("N2");
                if (samp != "")
                {
                    Kaki_no.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                }
                else
                {
                   
                }
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
        gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1796','448','505','484','1565','513','1675','497','1288','190','1797','551','552','1798','1799','1800','1801','1407','1468','1802','1528','1422','61','77','133') order by ID ASC");

        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;

         h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower()); 
         bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
         bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());

         h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
         h3_tag2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());

         lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
         lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
         lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower()); 
         lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
         lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
         lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
         lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
         lbl8_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
         lbl9_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
         lbl10_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
         lbl11_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
         lbl12_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
         lbl13_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
         lbl14_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
         lbl15_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
         lbl16_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower()+" (RM)");
         lbl17_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());

         btnUpload.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower()); 
         Button6.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower()); 
         Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
         Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
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
            if (Kaki_no.Text != "")
            {
                DataTable dd1 = new DataTable();
                dd1 = DBCon.Ora_Execute_table("select stf_staff_no from hr_staff_profile where '" + Kaki_no.Text + "' IN (stf_staff_no,stf_name)");
                if (dd1.Rows.Count > 0)
                {
                    Applcn_no1.Text = dd1.Rows[0]["stf_staff_no"].ToString();
                    DataTable select_kaki = new DataTable();
                    select_kaki = DBCon.Ora_Execute_table("select upper(sp.stf_name) as stf_name,ISNULL(upper(hj.hr_jaba_desc),'') as hr_jaba_desc,ISNULL(upper(hg.hr_gred_desc),'') as hr_gred_desc,ISNULL(upper(rhj.hr_jaw_desc),'') as hr_jaw_desc,ho.org_name,o1.op_perg_name  from (select * from hr_post_his where pos_staff_no='" + Applcn_no1.Text + "' and pos_end_dt='9999-12-31') as b left join hr_staff_profile as sp on sp.stf_staff_no=b.pos_staff_no left join Ref_hr_jabatan as hj on hj.hr_jaba_Code=b.pos_dept_cd left join Ref_hr_gred as hg on hg.hr_gred_Code=b.pos_grade_cd left join Ref_hr_Jawatan as rhj on rhj.hr_jaw_Code=b.pos_post_cd left join hr_organization ho on ho.org_gen_id=sp.str_curr_org_cd left join hr_organization_pern o1 on o1.op_perg_code=sp.stf_cur_sub_org");
                    if (select_kaki.Rows.Count != 0)
                    {
                        s_nama.Text = select_kaki.Rows[0]["stf_name"].ToString();
                        s_gred.Text = select_kaki.Rows[0]["hr_gred_desc"].ToString();
                        s_jab.Text = select_kaki.Rows[0]["hr_jaba_desc"].ToString();
                        s_jaw.Text = select_kaki.Rows[0]["hr_jaw_desc"].ToString();
                        txt_org.Text = select_kaki.Rows[0]["org_name"].ToString().ToUpper();
                        TextBox2.Text = select_kaki.Rows[0]["op_perg_name"].ToString().ToUpper();
                        grid();
                        BindGrid();

                    }
                    else
                    {
                        grid();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }
    }
    void KlatBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kat_latihan_cd, UPPER(kat_latihan_desc) as kat_latihan_desc from Ref_hr_kat_latihan  WHERE Status = 'A' order by kat_latihan_desc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_kat_latihan.DataSource = dt;
            dd_kat_latihan.DataTextField = "kat_latihan_desc";
            dd_kat_latihan.DataValueField = "kat_latihan_cd";
            dd_kat_latihan.DataBind();
            dd_kat_latihan.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    void JlatBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select jen_latihan_cd, UPPER(jen_latihan_desc) as jen_latihan_desc from Ref_hr_jenis_latihan  WHERE Status = 'A' order by jen_latihan_desc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_jen_latihan.DataSource = dt;
            dd_jen_latihan.DataTextField = "jen_latihan_desc";
            dd_jen_latihan.DataValueField = "jen_latihan_cd";
            dd_jen_latihan.DataBind();
            dd_jen_latihan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void grid()
    {
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select ISNULL(CASE WHEN trn_start_dt = '1900-01-01 00:00:00.000' THEN '' ELSE FORMAT(trn_start_dt,'dd/MM/yyyy', 'en-us') END, '') AS d1,ISNULL(CASE WHEN trn_end_dt = '1900-01-01 00:00:00.000' THEN '' ELSE FORMAT(trn_end_dt,'dd/MM/yyyy', 'en-us') END, '') AS d2,trn_training_name,trn_venue,trn_organiser,trn_dur,trn_fee_amt from hr_training where trn_staff_no='" + Applcn_no1.Text + "'", con);
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

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();
        GridView1.DataBind();
    }




    protected void click_insert(object sender, EventArgs e)
    {
        try
        {
            if (Kaki_no.Text != "" && tm_lati.Text != "" && ta_lati.Text != "")
            {
                int contentLength = FileUpload1.PostedFile.ContentLength;//You may need it for validation
                string contentType = FileUpload1.PostedFile.ContentType;//You may need it for validation
                string fileName = FileUpload1.PostedFile.FileName;
                string fname = string.Empty;

                string d1 = tm_lati.Text;
                DateTime today1 = DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                phdate1 = today1.ToString("yyyy-MM-dd");


                string d2 = ta_lati.Text;
                DateTime today2 = DateTime.ParseExact(d2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                phdate2 = today2.ToString("yyyy-MM-dd");
                if ((today2 - today1).TotalDays > 0)
                {

                    DataTable select_kaki = new DataTable();
                    select_kaki = DBCon.Ora_Execute_table("select trn_staff_no,trn_start_dt,trn_doc from hr_training where trn_staff_no='" + Applcn_no1.Text + "' and trn_start_dt='" + phdate1 + "'");



                    if (select_kaki.Rows.Count == 0 && updt_txt.Text == "0")
                    {
                        if (fileName != "")
                        {
                            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/Attendance/" + fileName));//Or code to save in the DataBase.
                            fname = fileName;
                        }
                        DBCon.Ora_Execute_table("INSERT INTO hr_training (trn_staff_no,trn_cat_cd,trn_type_cd,trn_training_name,trn_venue,trn_organiser,trn_start_dt,trn_end_dt,trn_dur,trn_fee_amt,trn_crt_id,trn_crt_dt,trn_doc,trn_catatan) VALUES ('" + Applcn_no1.Text + "', '" + dd_kat_latihan.SelectedValue + "','" + dd_jen_latihan.SelectedValue + "','" + nama_lat.Text.Replace("'", "''") + "','" + tempat_lat.Text.Replace("'", "''") + "','" + nama_peny.Text.Replace("'", "''") + "','" + phdate1 + "','" + phdate2 + "','" + tempoh_lat.Text + "','" + yuran_lati.Text + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd ") + "','" + fname + "','"+ TextBox1.Text + "')");
                        clr_cnt();
                        BindGrid();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {
                        if (fileName != "")
                        {
                            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/Attendance/" + fileName));//Or code to save in the DataBase.
                            fname = fileName;
                        }
                        else
                        {
                            fname = select_kaki.Rows[0]["trn_doc"].ToString();
                        }
                        DBCon.Ora_Execute_table("UPDATE hr_training set trn_catatan='"+ TextBox1.Text + "',trn_cat_cd='" + dd_kat_latihan.SelectedValue + "',trn_type_cd='" + dd_jen_latihan.SelectedValue + "',trn_training_name='" + nama_lat.Text.Replace("'", "''") + "',trn_venue='" + tempat_lat.Text.Replace("'", "''") + "',trn_organiser='" + nama_peny.Text.Replace("'", "''") + "',trn_end_dt='" + phdate2 + "',trn_dur='" + tempoh_lat.Text + "',trn_fee_amt='" + yuran_lati.Text + "',trn_upd_id='" + Session["New"].ToString() + "',trn_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',trn_doc='" + fname + "' where trn_staff_no='" + Applcn_no1.Text + "' and trn_start_dt='" + phdate1 + "'");
                        clr_cnt();
                        Button6.Text = "Simpan";
                        updt_txt.Text = "0";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                }
                else
                {
                    grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tarikh Tamat Tidak Sah',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }
        BindGrid();
    }



    void clr_cnt()
    {
        dd_kat_latihan.SelectedValue = "";
        dd_jen_latihan.SelectedValue = "";
        TextBox1.Text = "";
        nama_lat.Text = "";
        tempat_lat.Text = "";
        nama_peny.Text = "";
        tempoh_lat.Text = "";
        tm_lati.Text = "";
        ta_lati.Text = "";
        yuran_lati.Text = "";
        //lab_fname.Text = "";        
        grid();
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {

        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label8");
        DateTime feedt2 = DateTime.ParseExact(lblTitle.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        string ss1 = feedt2.ToString("yyyy-MM-dd");
        string ss2 = feedt2.ToString("dd/MM/yyyy");
        DataTable ddokdicno1 = new DataTable();
        ddokdicno1 = DBCon.Ora_Execute_table("select *,FORMAT(trn_start_dt,'dd/MM/yyyy') sdt,FORMAT(trn_end_dt,'dd/MM/yyyy') edt from hr_training where trn_staff_no='" + Applcn_no1.Text + "' and trn_start_dt='" + ss1 + "'");
        if (ddokdicno1.Rows.Count != 0)
        {
            updt_txt.Text = "1";
            Button6.Text = "Kemaskini";
            dd_kat_latihan.SelectedValue = ddokdicno1.Rows[0]["trn_cat_cd"].ToString().Trim();
            dd_jen_latihan.SelectedValue = ddokdicno1.Rows[0]["trn_type_cd"].ToString().Trim();
            nama_lat.Text = ddokdicno1.Rows[0]["trn_training_name"].ToString();
            tempat_lat.Text = ddokdicno1.Rows[0]["trn_venue"].ToString();
            nama_peny.Text = ddokdicno1.Rows[0]["trn_organiser"].ToString();
            tempoh_lat.Text = ddokdicno1.Rows[0]["trn_dur"].ToString();
            TextBox1.Text = ddokdicno1.Rows[0]["trn_catatan"].ToString();
            tm_lati.Text = ddokdicno1.Rows[0]["sdt"].ToString();
            ta_lati.Text = ddokdicno1.Rows[0]["edt"].ToString();
            yuran_lati.Text = double.Parse(ddokdicno1.Rows[0]["trn_fee_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            //lab_fname.Text = ddokdicno1.Rows[0]["trn_doc"].ToString();


            con.Open();
            DataTable ddicno = new DataTable();
            SqlCommand cmd = new SqlCommand("select Id, Name from tblFiles1  where td_staff_no='" + Applcn_no1.Text + "' and td_start_dt='" + ss2 + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridView2.DataSource = ds;
                GridView2.DataBind();
                int columncount = GridView2.Rows[0].Cells.Count;
                GridView2.Rows[0].Cells.Clear();
                GridView2.Rows[0].Cells.Add(new TableCell());
                GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView2.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
                //btn_hups.Visible = false;
            }
            else
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
            }
            con.Close();

        }
        else
        {
            grid();
              ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    private void BindGrid()
    {
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select Id, Name from tblFiles1  where td_staff_no='" + Applcn_no1.Text + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView2.DataSource = ds;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }
        con.Close();
    }
    protected void Upload(object sender, EventArgs e)
    {
        if (Kaki_no.Text != "")
        {
            if (tm_lati.Text != "")
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string contentType = FileUpload1.PostedFile.ContentType;
                if (filename != "")
                {
                    userid = Session["New"].ToString();
                    using (Stream fs = FileUpload1.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(constr))
                            {
                                string query = "insert into tblFiles1 values (@sno,@sdt,@Name, @ContentType, @Data,@cid,@cdt)";
                                using (SqlCommand cmd = new SqlCommand(query))
                                {
                                    cmd.Connection = con;
                                    cmd.Parameters.AddWithValue("@sno", Applcn_no1.Text);
                                    cmd.Parameters.AddWithValue("@sdt", tm_lati.Text);
                                    cmd.Parameters.AddWithValue("@Name", filename);
                                    cmd.Parameters.AddWithValue("@ContentType", contentType);
                                    cmd.Parameters.AddWithValue("@Data", bytes);
                                    cmd.Parameters.AddWithValue("@cid", userid);
                                    cmd.Parameters.AddWithValue("@cdt", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Lampiran Upload Successfully.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                }
                            }
                            grid();
                            BindGrid();
                        }
                    }
                }
                else
                {
                    grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Dari Input Carian',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        BindGrid();
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName, contentType;
        string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Name, Data, ContentType from tblFiles1 where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Data"];
                    contentType = sdr["ContentType"].ToString();
                    fileName = sdr["Name"].ToString();
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }




    protected void UploadFile(object sender, EventArgs e)
    {
        if (Kaki_no.Text != "")
        {
            if (tm_lati.Text != "")
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/Attendance/") + fileName);


                DataTable dt = new DataTable();
                dt = DBCon.Ora_Execute_table("insert into hr_training_doc(td_staff_no,td_start_dt,td_doc,td_crt_id,td_crt_dt)values('" + Applcn_no1.Text + "','" + tm_lati.Text + "','" + fileName + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')");
                BindGrid();
                //Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Dari Input Carian',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }



    protected void btn_hups_Click(object sender, EventArgs e)
    {
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.CheckBox;
            if (rb.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                System.Web.UI.WebControls.CheckBox rbn = new System.Web.UI.WebControls.CheckBox();
                rbn = (System.Web.UI.WebControls.CheckBox)row.FindControl("RadioButton1");
                if (rbn.Checked)
                {
                    int RowIndex = row.RowIndex;
                    string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("Label8")).Text.ToString(); //this store the  value in varName1
                    DateTime feedt2 = DateTime.ParseExact(varName1.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string ss1 = feedt2.ToString("yyyy-MM-dd");
                    SqlCommand ins_peng = new SqlCommand("Delete from hr_training where trn_staff_no='" + Applcn_no1.Text + "' and trn_start_dt='" + ss1 + "'", con);
                    con.Open();
                    int i = ins_peng.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    Button6.Text = "Simpan";
                    updt_txt.Text = "0";
                    grid();                    
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        BindGrid();
    }

    protected void DeleteFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);

        DataTable dt = new DataTable();

        dt = DBCon.Ora_Execute_table("delete from tblFiles1 where id='" + id + "' ");
        BindGrid();
        grid();

    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_KM_Latihan_view.aspx");
    }
}