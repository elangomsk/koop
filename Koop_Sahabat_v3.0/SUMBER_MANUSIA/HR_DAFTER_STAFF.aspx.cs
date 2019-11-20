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

using System.Net;
using System.Net.Mail;
public partial class HR_DAFTER_STAFF : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection dbcon = new DBConnection();
    StudentWebService service = new StudentWebService();

    DataTable dt = new DataTable();
    string useid = string.Empty;
    string Status = string.Empty, Status1 = string.Empty, Status2 = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string stno = string.Empty;
    string gt_val1 = string.Empty, gt_val2 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        assgn_roles();
        app_language();
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
              
                var samp = Request.Url.Query;
                gelaran();
                Janitna();
                wargna();
                bangsa();
                agama();
                Perkha();
                negeri();
                organsi();
                Jaw();
                skim();
                gred();
                pang();
                //Jabatan();
                perkahwinan();
                kat();
              
             
                sebab1();
                sebab2();
                waktu();
                //TxtTaradu.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txttlan.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txthin.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txttarber.Text = "31/12/9999";
                txthin.Text = "31/12/9999";
                if (samp != "")
                {
                    Applcn_no1.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                    Button8.Visible = true;
                    pp2.Attributes.Remove("Style");
                }
                else
                {
                    pp2.Attributes.Add("Style", "pointer-events:None;");
                    ImgPrv.Attributes.Add("src", "../files/user/user.gif");
                    ddwarg.SelectedValue = "MYS";
                    Applcn_no1.Text = "";
                }
                useid = Session["New"].ToString();


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
        ste_set = dbcon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

        DataTable gt_lng = new DataTable();
        gt_lng = dbcon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('451','448','505','1415','484','37','16','496','497','36','44','1416','46','1417','19','45','1418','1419','17','51','170','171','28','1420', '29', '26', '915', '1036', '9', '61', '15', '77','480','1663','110','513','1288','1664','1566','1665','1666','1667','1668','1669','1670','1671','1672','133','35') order by ID ASC");

        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;

        h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower()); //Maklumat Kakitangan
        bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower()); //HR
        bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower()); //Maklumat Kakitangan

        //h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[27][0].ToString().ToLower());  //MAKLUMAT PERIBADI
        //pt1.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[27][0].ToString().ToLower()); //MAKLUMAT PERIBADI
        //pt2.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower()); //Maklumat Penjawatan
        lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower()); //No Kakitangan
        lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower()); //No KP Baru
        lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower()); //Nama
        lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[25][0].ToString().ToLower()); //Nama Syarikat
        lbl5_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[26][0].ToString().ToLower()); //Perniagaan
        lbl6_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower()); //Jantina
        lbl7_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower()); //Gelaran
        lbl8_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower()); //Tarikh Lahir
        lbl9_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[34][0].ToString().ToLower()); //Pangkat
        lbl10_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower()); //Bangsa
        lbl11_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower()); //Umur
        lbl12_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[35][0].ToString().ToLower()); //Agama
        lbl13_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[36][0].ToString().ToLower()); //Negeri Lahir

        lbl14_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower()); //Warganegara

        lbl15_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower()); //Status Perkahwinan
        lbl16_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower()); //Alamat Tetap
        lbl17_text.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());  //Alamat Surat-Menyurat                 

        lbl18_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower()); //Poskod
        lbl19_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower()); //Poskod
        lbl20_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[37][0].ToString().ToLower()); //Bandar
        lbl21_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[37][0].ToString().ToLower()); //Bandar        
        lbl22_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower()); //Negeri
        lbl23_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower()); //Negeri
        lbl24_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower()); //No Telefon (R)
        lbl25_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower()); //No Telefon (B)
        lbl26_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[30][0].ToString().ToLower()); //Email
        lbl27_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());  //Gambar Pengguna  
    
        lbl28_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());  //Jawatan   
        lbl29_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[39][0].ToString().ToLower()); //Skim
        lbl30_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower()); //Kategori
        lbl31_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower());  //Gred                

        lbl32_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower()); //Jabatan
        lbl33_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[40][0].ToString().ToLower()); //Status Penjawatan
        lbl34_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower()); //Unit
        lbl35_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[41][0].ToString().ToLower()); //Tarikh Mula Khidmat
        lbl36_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[42][0].ToString().ToLower()); //Tarikh Mula Lantikan
        lbl37_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[43][0].ToString().ToLower()); //Tarikh Akhir Lantikan
        lbl38_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[44][0].ToString().ToLower()+" 1"); //Penyelia 1
        lbl39_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[44][0].ToString().ToLower()+" 2"); //Penyelia 2
        lbl40_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[45][0].ToString().ToLower()); //Waktu Bekerja 
        lbl41_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[46][0].ToString().ToLower());  //Sebab Pergerakan
        lbl42_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[47][0].ToString().ToLower()); //Tarikh Berhenti
        lbl43_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[48][0].ToString().ToLower());  //Sebab Berhenti

        Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower()); //Kemaskini - 8
        Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower()); //Set Semula
        Button8.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower()); //Kembali

        Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower()); //Simpan 
        Button7.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower()); //Set Semula
        btn_hups.Text = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower()); //Hapus
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void view_details()
    {
        bind();
    }


    void assgn_roles()
    {
        if (Session["New"] != null)
        {
            DataTable ddokdicno = new DataTable();
            ddokdicno = dbcon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

            if (ddokdicno.Rows.Count != 0)
            {
                DataTable ddokdicno_1 = new DataTable();
                ddokdicno_1 = dbcon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0078' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

                if (ddokdicno_1.Rows.Count != 0)
                {

                    gt_val1 = ddokdicno_1.Rows[0]["Edit_chk"].ToString();

                }
            }
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    public void bind()
    {
        SqlConnection conn = new SqlConnection(cs);
        string query2 = "select stf_staff_no,stf_icno,stf_salutation_cd,stf_name,stf_title_cd,stf_sex_cd,stf_age,stf_dob,stf_pob,stf_race_cd,stf_nationality_cd,stf_religion_cd,stf_marital_sts_cd,stf_permanent_address,stf_permanent_postcode,stf_permanent_city,stf_permanent_state_cd,stf_mailing_address,stf_mailing_postcode,stf_mailing_city,stf_mailing_state_cd,stf_phone_h,stf_phone_m,stf_email,stf_image,FORMAT(stf_service_start_dt,'dd/MM/yyyy', 'en-us') as sdate,str_curr_org_cd,stf_cur_sub_org from hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "'";
        conn.Open();
        var sqlCommand2 = new SqlCommand(query2, conn);
        var sqlReader2 = sqlCommand2.ExecuteReader();
        while (sqlReader2.Read())
        {
            if (gt_val1 == "1")
            {
                Button3.Visible = true;
                Button5.Visible = true;
                btn_hups.Visible = true;
            }
            else
            {
                Button3.Visible = false;
                Button5.Visible = false;
                btn_hups.Visible = false;
            }
            Button3.Text = "Kemaskini";
            Button2.Visible = true;
            Button1.Visible = false;
            Applcn_no.Attributes.Add("Readonly", "Readonly");
            Applcn_no.Text = Applcn_no1.Text;
            txtic.Text = (string)sqlReader2["stf_icno"].ToString().Trim();
            txtname.Text = (string)sqlReader2["stf_name"].ToString().Trim();
            ddorganisasi.SelectedValue = (string)sqlReader2["str_curr_org_cd"].ToString();
            pen1();
            pen2();
            ddgel.SelectedValue = (string)sqlReader2["stf_salutation_cd"].ToString().Trim();
            ddpang.SelectedValue = (string)sqlReader2["stf_title_cd"].ToString().Trim();
            DataTable Check = new DataTable();
            //Check = dbcon.Ora_Execute_table("select gender_desc from ref_gender where  gender_cd='" + sqlReader2["stf_sex_cd"].ToString() + "'");
            ddjanita.SelectedValue = sqlReader2["stf_sex_cd"].ToString().Trim();
            if (sqlReader2["stf_dob"].ToString() != "")
            {
                var feedt = Convert.ToDateTime(sqlReader2["stf_dob"]).ToString("dd/MM/yyyy");
                TxtTaradu.Text = feedt;
            }
            else
            {
                TxtTaradu.Text = "";
            }
            ddBangsa.SelectedValue = (string)sqlReader2["stf_race_cd"].ToString().Trim();
            txtumur.Text = sqlReader2["stf_age"].ToString();
            ddagama.SelectedValue = sqlReader2["stf_religion_cd"].ToString().Trim();
            ddnegeri.SelectedValue = (string)sqlReader2["stf_pob"].ToString();
            ddwarg.SelectedValue = sqlReader2["stf_nationality_cd"].ToString().Trim();
            ddstsper.SelectedValue = (string)sqlReader2["stf_marital_sts_cd"].ToString().Trim();
            txtalamat.Value = (string)sqlReader2["stf_permanent_address"].ToString();
            txtalamatsurat.Value = (string)sqlReader2["stf_mailing_address"].ToString();

            txtpstcd.Text = sqlReader2["stf_permanent_postcode"].ToString();
            txtmposkod.Text = sqlReader2["stf_mailing_postcode"].ToString();
            txtbandar.Text = (string)sqlReader2["stf_permanent_city"].ToString();
            txtmbandar.Text = (string)sqlReader2["stf_mailing_city"].ToString();
            ddnegeri1.SelectedValue = (string)sqlReader2["stf_permanent_state_cd"].ToString().Trim();
            ddnegeri2.SelectedValue = (string)sqlReader2["stf_mailing_state_cd"].ToString().Trim();
            Txtnotel_R.Text = sqlReader2["stf_phone_h"].ToString();
            Txtnotel_P.Text = sqlReader2["stf_phone_m"].ToString();
            txtemail.Text = (string)sqlReader2["stf_email"].ToString();
            org_pern();
            dd_org_pen.SelectedValue = (string)sqlReader2["stf_cur_sub_org"].ToString().Trim();
            org_jaba();
            if (sqlReader2["stf_image"].ToString() != "")
            {

                ImgPrv.Attributes.Add("src", "../Files/user/" + sqlReader2["stf_image"].ToString());
                ImgPrv.Visible = true;
            }
            else
            {
                ImgPrv.Attributes.Add("src", "../files/user/user.gif");
            }

            if (sqlReader2["sdate"].ToString() != "01/01/1900")
            {
                txttmk.Text = sqlReader2["sdate"].ToString();
            }
            else
            {
                txttmk.Text = "";
            }
        }
        bind1();
        sqlReader2.Close();

    }

    void tab2()
    {
        bind1();
        p1.Attributes.Add("class", "tab-pane");
        p2.Attributes.Add("class", "tab-pane active");
        pp1.Attributes.Remove("class");
        pp2.Attributes.Add("class", "active");
    }
    public void bind1()
    {
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select FORMAT(pos_start_dt,'dd/MM/yyyy', 'en-us') as pos_start_dt,FORMAT(pos_end_dt,'dd/MM/yyyy', 'en-us') as pos_end_dt,pos_post_cd,hj.hr_jaw_desc,pos_grade_cd,UPPER(ISNULL(hg.hr_gred_desc,'')) as hr_gred_desc,pos_unit_cd,UPPER(ISNULL(hu.hr_unit_desc,'')) as hr_unit_desc,pos_dept_cd,UPPER(ISNULL(jab.hr_jaba_desc,'')) as hr_jaba_desc,pos_staff_no,pos_subjek from hr_post_his ph left join ref_hr_jawatan hj on hj.hr_jaw_Code=ph.pos_post_cd left join ref_hr_gred hg on hg.hr_gred_Code=ph.pos_grade_cd left join ref_hr_unit hu on hu.hr_unit_Code=ph.pos_unit_cd left join Ref_hr_jabatan jab on jab.hr_jaba_Code=ph.pos_dept_cd where pos_staff_no ='" + Applcn_no1.Text + "' order by ph.pos_start_dt desc", con);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        kem_view();
    }

    protected void clk_chgaddress(object sender, EventArgs e)
    {
        if (ck_address.Checked == true)
        {
            txtalamatsurat.Value = txtalamat.Value;
            txtmposkod.Text = txtpstcd.Text;
            txtmbandar.Text = txtbandar.Text;
            ddnegeri2.SelectedValue = ddnegeri1.SelectedValue;
        }
        else
        {
            DataTable dd1_stf = new DataTable();
            dd1_stf = dbcon.Ora_Execute_table("select stf_staff_no,stf_mailing_address,stf_mailing_postcode,stf_mailing_city,stf_mailing_state_cd from hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "'");
            if (dd1_stf.Rows.Count > 0)
            {
                txtalamatsurat.Value = dd1_stf.Rows[0]["stf_mailing_address"].ToString();
                txtmposkod.Text = dd1_stf.Rows[0]["stf_mailing_postcode"].ToString(); ;
                txtmbandar.Text = dd1_stf.Rows[0]["stf_mailing_city"].ToString(); ;
                ddnegeri2.SelectedValue = dd1_stf.Rows[0]["stf_mailing_state_cd"].ToString(); ;
            }
            else
            {
                txtalamatsurat.Value = "";
                txtmposkod.Text = "";
                txtmbandar.Text = "";
                ddnegeri2.SelectedValue = "";
            }
        }
    }

    protected void btn_hups_Click(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "")
        {
            string rcount = string.Empty;
            int count = 0;
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.CheckBox;
                if (rb.Checked == true)
                {
                    count++;
                }
                rcount = count.ToString();
            }
            if (rcount != "0")
            {
                foreach (GridViewRow row in gvSelected.Rows)
                {
                    var rbn = row.FindControl("RadioButton1") as System.Web.UI.WebControls.CheckBox;
                    if (rbn.Checked == true)
                    {
                        int RowIndex = row.RowIndex;
                        DataTable ddicno = new DataTable();

                        string sno = ((System.Web.UI.WebControls.Label)row.FindControl("lb_sno")).Text.ToString(); //this store the  value in varName1
                        string sdt = ((System.Web.UI.WebControls.Label)row.FindControl("lb_sdt")).Text.ToString(); //this store the  value in varName1
                        DateTime time3 = DateTime.ParseExact(sdt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string ssdt = time3.ToString("yyyy-MM-dd");
                        SqlCommand ins_peng = new SqlCommand("delete from hr_post_his where pos_staff_no='" + sno + "' and pos_start_dt='" + ssdt + "'", con);
                        con.Open();
                        int i = ins_peng.ExecuteNonQuery();
                        con.Close();
                        bind1();                        
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                    }

                }
            }
            else
            {
                bind1();                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            bind1();            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Kakitangan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        tab2();
    }


    void kem_view()
    {
        if (Applcn_no.Text != "")
        {
            DataTable dd1 = new DataTable();
            dd1 = dbcon.Ora_Execute_table("select stf_staff_no from hr_staff_profile where '" + Applcn_no.Text + "' IN (stf_staff_no,stf_name)");
            if (dd1.Rows.Count != 0)
            {
                if (!string.IsNullOrEmpty(Applcn_no.Text))
                {
                    int EnteredIntValue = 0;
                    bool IsIntOrNot = false;
                    IsIntOrNot = int.TryParse(Applcn_no.Text, out EnteredIntValue);
                    if (IsIntOrNot)
                    {
                        txt1.Visible = false;
                        TextBox2.Text = dd1.Rows[0]["stf_staff_no"].ToString();
                    }
                    else
                    {
                        txt1.Visible = true;
                        TextBox2.Text = dd1.Rows[0]["stf_staff_no"].ToString();
                    }
                }

                Applcn_no1.Text = dd1.Rows[0]["stf_staff_no"].ToString();

            }
            else
            {
                Applcn_no1.Text = Applcn_no.Text;
            }

            if (dd1.Rows.Count > 0)
            {

                bind();
                bind1();

            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No Kakitangan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    public void clear1()
    {

        txtic.Text = "";
        txtname.Text = "";
        ddjanita.Items.Clear();
        //Janitna();
        ddgel.Text = "";
        ddpang.Text = "";
        TxtTaradu.Text = "";
        ddBangsa.Text = "";
        txtumur.Text = "";
        ddagama.Text = "";
        ddnegeri.Text = "";
        ddwarg.Text = "";
        ddstsper.Text = "";
        txtalamat.Value = "";
        txtalamatsurat.Value = "";
        txtpstcd.Text = "";
        txtmposkod.Text = "";
        txtbandar.Text = "";
        txtmbandar.Text = "";
        ddnegeri1.Text = "";
        ddnegeri2.Text = "";
        Txtnotel_R.Text = "";
        Txtnotel_P.Text = "";
        txtemail.Text = "";
        ImgPrv.Attributes.Add("src", "../Files/user/user.gif");
    }
    public void clear2()
    {
        //ddorganisasi.Attributes.Remove("style");
        if (gt_val1 == "1")
        {
            Button5.Visible = true;
        }
        else
        {
            Button5.Visible = false;
        }
        Button6.Visible = false;
        //ddorganisasi.Text = "";
        ddjawatan.Text = "";
        ddKategori.Text = "";
        ddskim.Text = "";
        ddjabatan.Text = "";
        ddgred.Text = "";
        ddunit.Text = "";
        ddstspenj.Text = "";
        //pen();
        ddpen1.SelectedValue = "";
        ddpen2.SelectedValue = "";
        ddSebab.Text = "";
        ddsebabber.Text = "";
        //txttmk.Text = "";
        DropDownList1.Text = "";
        //txttmk.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txttlan.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txthin.Text = "31/12/9999";
        txttarber.Text = "31/12/9999";



    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("HR_DAFTER_STAFF.aspx");
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        clear2();
        tab2();
    }

    void gelaran()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_gelaran_Code,UPPER(hr_gelaran_desc) as hr_gelaran_desc from Ref_hr_gelaran";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddgel.DataSource = dt;
            ddgel.DataBind();
            ddgel.DataTextField = "hr_gelaran_desc";
            ddgel.DataValueField = "hr_gelaran_Code";
            ddgel.DataBind();
            ddgel.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void pang()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_titl_Code,UPPER(hr_titl_desc) as hr_titl_desc from Ref_hr_title";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddpang.DataSource = dt;
            ddpang.DataBind();
            ddpang.DataTextField = "hr_titl_desc";
            ddpang.DataValueField = "hr_titl_Code";
            ddpang.DataBind();
            ddpang.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void sel_orgbind(object sender, EventArgs e)
    {
        org_pern();
    }
    void org_pern()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select * from hr_organization_pern where op_org_id='" + ddorganisasi.SelectedValue + "' and Status = 'A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_org_pen.DataSource = dt;
            dd_org_pen.DataTextField = "op_perg_name";
            dd_org_pen.DataValueField = "op_perg_code";
            dd_org_pen.DataBind();
            dd_org_pen.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            string com1 = "select stf_staff_no,upper(stf_name) as stf_name from hr_staff_profile where stf_staff_no Not In ('"+ Applcn_no.Text + "') and  str_curr_org_cd='" + ddorganisasi.SelectedValue + "'";
            SqlDataAdapter adpt1 = new SqlDataAdapter(com1, con);
            DataTable dt1 = new DataTable();
            adpt1.Fill(dt1);
            ddpen1.DataSource = dt1;
            ddpen1.DataTextField = "stf_name";
            ddpen1.DataValueField = "stf_staff_no";
            ddpen1.DataBind();
            ddpen1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            string com2 = "select stf_staff_no,upper(stf_name) as stf_name from hr_staff_profile where stf_staff_no Not In ('" + Applcn_no.Text + "') and str_curr_org_cd='" + ddorganisasi.SelectedValue + "'";
            SqlDataAdapter adpt2 = new SqlDataAdapter(com2, con);
            DataTable dt2 = new DataTable();
            adpt2.Fill(dt2);
            ddpen2.DataSource = dt2;
            ddpen2.DataTextField = "stf_name";
            ddpen2.DataValueField = "stf_staff_no";
            ddpen2.DataBind();
            ddpen2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void sel_orgjaba(object sender, EventArgs e)
    {
        org_jaba();
    }
    void org_jaba()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select * from hr_organization_jaba where oj_perg_code='" + dd_org_pen.SelectedValue + "' and Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddjabatan.DataSource = dt;
            ddjabatan.DataTextField = "oj_jaba_desc";
            ddjabatan.DataValueField = "oj_jaba_cd";
            ddjabatan.DataBind();
            ddjabatan.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //void Jabatan()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {

    //        string com = "select hr_jaba_Code,UPPER(hr_jaba_desc) as hr_jaba_desc  from Ref_hr_jabatan";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        ddjabatan.DataSource = dt;
    //        ddjabatan.DataBind();
    //        ddjabatan.DataTextField = "hr_jaba_desc";
    //        ddjabatan.DataValueField = "hr_jaba_Code";
    //        ddjabatan.DataBind();
    //        ddjabatan.Items.Insert(0, new ListItem("--- PILIH ---", ""));


    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    void sebab1()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_perkaki_Code,UPPER(hr_perkaki_desc) as hr_perkaki_desc from Ref_hr_per_kaki";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddSebab.DataSource = dt;
            ddSebab.DataBind();
            ddSebab.DataTextField = "hr_perkaki_desc";
            ddSebab.DataValueField = "hr_perkaki_Code";
            ddSebab.DataBind();
            ddSebab.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void sebab2()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_pen_Code,UPPER(hr_pen_desc) as hr_pen_desc from Ref_hr_penamatan";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            ddsebabber.DataSource = dt;
            ddsebabber.DataBind();
            ddsebabber.DataTextField = "hr_pen_desc";
            ddsebabber.DataValueField = "hr_pen_Code";
            ddsebabber.DataBind();
            ddsebabber.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Janitna1()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select gender_cd,UPPER(gender_desc) as gender_desc from ref_gender where gender_desc='" + ddjanita.SelectedItem.Text + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddjanita.DataSource = dt;
            ddjanita.DataBind();
            ddjanita.DataTextField = "gender_desc";
            ddjanita.DataValueField = "gender_cd";
            ddjanita.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void Janitna()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select gender_cd,upper(gender_desc) as gender_desc  from ref_gender";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddjanita.DataSource = dt;
            ddjanita.DataBind();
            ddjanita.DataTextField = "gender_desc";
            ddjanita.DataValueField = "gender_cd";
            ddjanita.DataBind();
            ddjanita.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void waktu()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_jad_Code,UPPER(hr_jad_desc) as hr_jad_desc from Ref_hr_Jad_kerj";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "hr_jad_desc";
            DropDownList1.DataValueField = "hr_jad_Code";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void wargna()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_wargan_code,UPPER(hr_wargan_desc) as hr_wargan_desc from Ref_hr_wargan";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddwarg.DataSource = dt;
            ddwarg.DataBind();
            ddwarg.DataTextField = "hr_wargan_desc";
            ddwarg.DataValueField = "hr_wargan_code";
            ddwarg.DataBind();
            ddwarg.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void bangsa()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_bangsa_code,UPPER(hr_bangsa_desc) as hr_bangsa_desc from Ref_hr_bangsa";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddBangsa.DataSource = dt;
            ddBangsa.DataBind();
            ddBangsa.DataTextField = "hr_bangsa_desc";
            ddBangsa.DataValueField = "hr_bangsa_code";
            ddBangsa.DataBind();
            ddBangsa.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void agama()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_agama_code,UPPER(hr_agama_desc) as hr_agama_desc from Ref_hr_agama";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddagama.DataSource = dt;
            ddagama.DataBind();
            ddagama.DataTextField = "hr_agama_desc";
            ddagama.DataValueField = "hr_agama_code";
            ddagama.DataBind();
            ddagama.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Perkha()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_perkha_Code,UPPER(hr_perkha_desc) as hr_perkha_desc from Ref_hr_sts_perkha";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddstsper.DataSource = dt;
            ddstsper.DataBind();
            ddstsper.DataTextField = "hr_perkha_desc";
            ddstsper.DataValueField = "hr_perkha_Code";
            ddstsper.DataBind();
            ddstsper.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void negeri()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_negeri_code,UPPER(hr_negeri_desc) as hr_negeri_desc from ref_hr_negeri";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddnegeri.DataSource = dt;
            ddnegeri.DataBind();
            ddnegeri.DataTextField = "hr_negeri_desc";
            ddnegeri.DataValueField = "hr_negeri_code";
            ddnegeri.DataBind();
            ddnegeri.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            ddnegeri1.DataSource = dt;
            ddnegeri1.DataBind();
            ddnegeri1.DataTextField = "hr_negeri_desc";
            ddnegeri1.DataValueField = "hr_negeri_code";
            ddnegeri1.DataBind();
            ddnegeri1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            ddnegeri2.DataSource = dt;
            ddnegeri2.DataBind();
            ddnegeri2.DataTextField = "hr_negeri_desc";
            ddnegeri2.DataValueField = "hr_negeri_code";
            ddnegeri2.DataBind();
            ddnegeri2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lblSubItemName_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        CommandArgument1 = commandArgs[0];
        CommandArgument2 = commandArgs[1];


        sdd = CommandArgument2;
        TextBox1.Text = oid;
        bindchl();
        tab2();
    }



    public void bindchl()
    {
        
        //ddorganisasi.Attributes.Add("style","Pointer-events:none;");
        //txttlan.Attributes.Add("style", "Pointer-events:none;");
        DataTable ddps = new DataTable();
        ddps = dbcon.Ora_Execute_table("select hr_jaw_Code from ref_hr_jawatan where  hr_jaw_desc='" + CommandArgument1 + "'");
     
        //DateTime date = DateTime.ParseExact(CommandArgument2, "dd/MM/yyyy", null);
        DateTime time = DateTime.ParseExact(CommandArgument2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        string sdt1 = time.ToString("yyyy-MM-dd");
        SqlConnection conn = new SqlConnection(cs);
        DataTable ddorg = new DataTable();
        ddorg = dbcon.Ora_Execute_table("select count(*) as cnt from hr_post_his where pos_staff_no='" + Applcn_no.Text + "' and pos_start_dt > '" + sdt1 + "'");
        if(ddorg.Rows[0]["cnt"].ToString() == "0")
        {
            if (gt_val1 == "1")
            {
                Button6.Visible = true;
                Button5.Visible = false;
            }
            else
            {
                Button6.Visible = false;
            }
        }
        else
        {
            Button6.Visible = false;
        }
        //organsi();
        unit();
        string query2 = "select pos_post_cd,pos_job_cat_cd,pos_scheme_cd,pos_dept_cd,pos_grade_cd,pos_unit_cd,pos_job_sts_cd,pos_start_dt,pos_end_dt,pos_spv_name1,pos_spv_name2,pos_move_reason_cd,sp.stf_service_end_reason,sp.stf_working_hour,FORMAT(sp.stf_service_start_dt,'dd/MM/yyyy', 'en-us') as sdate,FORMAT(sp.stf_service_end_dt,'dd/MM/yyyy', 'en-us') as edate from hr_post_his left join hr_staff_profile as sp on sp.stf_staff_no=pos_staff_no  where pos_start_dt='" + sdt1 + "' and pos_staff_no='" + Applcn_no1.Text + "'";
        conn.Open();
        var sqlCommand3 = new SqlCommand(query2, conn);
        var sqlReader3 = sqlCommand3.ExecuteReader();
        while (sqlReader3.Read())
        {
            //ddorganisasi.SelectedValue = (string)sqlReader3["pos_gen_ID"].ToString().Trim();
            ddjawatan.SelectedValue = (string)sqlReader3["pos_post_cd"].ToString().Trim();
            ddKategori.SelectedValue = (string)sqlReader3["pos_job_cat_cd"].ToString().Trim();
            ddskim.SelectedValue = (string)sqlReader3["pos_scheme_cd"].ToString().Trim();
            ddjabatan.SelectedValue = (string)sqlReader3["pos_dept_cd"].ToString().Trim();
            ddgred.SelectedValue = (string)sqlReader3["pos_grade_cd"].ToString().Trim();
            ddunit.SelectedValue = (string)sqlReader3["pos_unit_cd"].ToString().Trim();
            perkahwinan();
            if (sqlReader3["pos_job_sts_cd"].ToString() != "")
            {
                ddstspenj.SelectedValue = (string)sqlReader3["pos_job_sts_cd"].ToString().Trim();
            }
            var feedt = Convert.ToDateTime(sqlReader3["pos_start_dt"]).ToString("dd/MM/yyyy");
            txttlan.Text = feedt;
            var feedt1 = Convert.ToDateTime(sqlReader3["pos_end_dt"]).ToString("dd/MM/yyyy");
            txthin.Text = feedt1;
            DataTable chk_stf1 = new DataTable();
            chk_stf1 = dbcon.Ora_Execute_table("select stf_staff_no from hr_staff_profile where  stf_staff_no='" + (string)sqlReader3["pos_spv_name1"].ToString() + "'");
            DataTable chk_stf2 = new DataTable();
            chk_stf2 = dbcon.Ora_Execute_table("select stf_staff_no from hr_staff_profile where  stf_staff_no='" + (string)sqlReader3["pos_spv_name2"].ToString() + "'");
            if (chk_stf1.Rows.Count != 0)
            {
                ddpen1.SelectedValue = (string)sqlReader3["pos_spv_name1"].ToString();
            }
            else
            {
                ddpen1.SelectedValue = "";
            }
            if (chk_stf2.Rows.Count != 0)
            {
                ddpen2.SelectedValue = (string)sqlReader3["pos_spv_name2"].ToString();
            }
            else
            {
                ddpen2.SelectedValue = "";
            }
            ddSebab.SelectedValue = (string)sqlReader3["pos_move_reason_cd"].ToString().Trim();
            ddsebabber.SelectedValue = (string)sqlReader3["stf_service_end_reason"].ToString().Trim();
            DropDownList1.SelectedValue = (string)sqlReader3["stf_working_hour"].ToString();
            if (sqlReader3["sdate"].ToString() != "01/01/1900")
            {
                txttmk.Text = sqlReader3["sdate"].ToString();
            }
            else
            {
                txttmk.Text = "";
            }

            if (sqlReader3["edate"].ToString() != "01/01/1900")
            {
                txttarber.Text = sqlReader3["edate"].ToString();
            }
            else
            {
                txttarber.Text = "31/12/9999";
            }

        }
    }

    void organsi()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select org_gen_id,UPPER(org_name) as org_name from  hr_organization order by org_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddorganisasi.DataSource = dt;
            ddorganisasi.DataTextField = "org_name";
            ddorganisasi.DataValueField = "org_gen_id";
            ddorganisasi.DataBind();
            ddorganisasi.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            org_pern();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Jaw()

    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_jaw_code,UPPER(hr_jaw_desc) as hr_jaw_desc from ref_hr_jawatan";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddjawatan.DataSource = dt;
            ddjawatan.DataBind();
            ddjawatan.DataTextField = "hr_jaw_desc";
            ddjawatan.DataValueField = "hr_jaw_code";
            ddjawatan.DataBind();
            ddjawatan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void pen1()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select stf_staff_no,upper(stf_name) as stf_name from hr_staff_profile inner join KK_User_Login lo on lo.KK_userid=stf_staff_no where kk_roles like '%R0011%' and stf_staff_no != '" + useid + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddpen1.DataSource = dt;
            ddpen1.DataTextField = "stf_name";
            ddpen1.DataValueField = "stf_staff_no";
            ddpen1.DataBind();
            ddpen1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void pen2()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select stf_staff_no,upper(stf_name) as stf_name from hr_staff_profile inner join KK_User_Login lo on lo.KK_userid=stf_staff_no where kk_roles like '%R0012%' and stf_staff_no != '"+ useid +"'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddpen2.DataSource = dt;
            ddpen2.DataTextField = "stf_name";
            ddpen2.DataValueField = "stf_staff_no";
            ddpen2.DataBind();
            ddpen2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    void skim()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_skim_code,UPPER(hr_skim_desc) as hr_skim_desc from ref_hr_skim";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddskim.DataSource = dt;
            ddskim.DataBind();
            ddskim.DataTextField = "hr_skim_desc";
            ddskim.DataValueField = "hr_skim_code";
            ddskim.DataBind();
            ddskim.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void gred()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_gred_code,UPPER(hr_gred_desc) as hr_gred_desc from ref_hr_gred";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddgred.DataSource = dt;
            ddgred.DataBind();
            ddgred.DataTextField = "hr_gred_desc";
            ddgred.DataValueField = "hr_gred_code";
            ddgred.DataBind();
            ddgred.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void unit()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_unit_code,UPPER(hr_unit_desc) as hr_unit_desc from ref_hr_unit";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddunit.DataSource = dt;
            ddunit.DataBind();
            ddunit.DataTextField = "hr_unit_desc";
            ddunit.DataValueField = "hr_unit_Code";
            ddunit.DataBind();
            ddunit.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void kat()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_kate_Code,UPPER(hr_kate_desc) as hr_kate_desc from Ref_hr_penj_kategori where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddKategori.DataSource = dt;
            ddKategori.DataBind();
            ddKategori.DataTextField = "hr_kate_desc";
            ddKategori.DataValueField = "hr_kate_Code";
            ddKategori.DataBind();
            ddKategori.Items.Insert(0, new ListItem("--- PILIH ---", ""));


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    void perkahwinan()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_traf_Code,UPPER(hr_traf_desc) as hr_traf_desc from Ref_hr_penj_traf where Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddstspenj.DataSource = dt;
            ddstspenj.DataTextField = "hr_traf_desc";
            ddstspenj.DataValueField = "hr_traf_Code";
            ddstspenj.DataBind();
            ddstspenj.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "" && txtname.Text != "")
        {

            DataTable dd = new DataTable();
            dd = dbcon.Ora_Execute_table("select stf_staff_no,Stf_image,stf_kod_akaun from hr_staff_profile where stf_staff_no='" + Applcn_no1.Text + "'");

            string ss_no = string.Empty;
            if (dd.Rows.Count > 0)
            {
                ss_no = Applcn_no1.Text;
            }
            else
            {
                ss_no = Applcn_no.Text;
            }

            //Janitna1();

            string fileName = FileUpload1.PostedFile.FileName;

            string fname = string.Empty;
            string tldt = string.Empty;
            if (TxtTaradu.Text != "")
            {
                DateTime today1 = DateTime.ParseExact(TxtTaradu.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tldt = today1.ToString("yyyy-MM-dd");
            }
            else
            {
                tldt = "";
            }

            if (dd.Rows.Count == 0)
            {
                if (Convert.ToInt32(Applcn_no.Text.Length) <= 10)
                {
                    
                   
                    if (fileName != "")
                    {
                        int contentLength = FileUpload1.PostedFile.ContentLength;//You may need it for validation
                        string contentType = FileUpload1.PostedFile.ContentType;//You may need it for validation
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/user/" + fileName));//Or code to save in the DataBase.
                        fname = fileName;
                    }

                    string Inssql = "insert into hr_staff_profile(stf_staff_no,stf_icno,stf_name,stf_salutation_cd,stf_title_cd,stf_sex_cd,stf_age,stf_dob,stf_pob,stf_nationality_cd,stf_race_cd,stf_religion_cd,stf_marital_sts_cd,stf_permanent_address,stf_permanent_postcode,stf_permanent_city,stf_permanent_state_cd,stf_mailing_address,stf_mailing_postcode,stf_mailing_city,stf_mailing_state_cd,stf_phone_h,stf_phone_m,stf_email,stf_image,stf_crt_id,stf_crt_dt,str_curr_org_cd,stf_cur_sub_org,Status)values('" + ss_no + "','" + txtic.Text + "','" + txtname.Text.Replace("'", "''") + "','" + ddgel.SelectedValue + "','" + ddpang.SelectedValue + "','" + ddjanita.SelectedValue + "','" + txtumur.Text + "','" + tldt + "','" + ddnegeri.SelectedValue + "','" + ddwarg.SelectedValue + "','" + ddBangsa.SelectedValue + "','" + ddagama.SelectedValue + "','" + ddstsper.SelectedValue + "','" + txtalamat.Value.Replace("'", "''") + "','" + txtpstcd.Text + "','" + txtbandar.Text + "','" + ddnegeri1.SelectedValue + "','" + txtalamatsurat.Value.Replace("'", "''") + "','" + txtmposkod.Text + "','" + txtmbandar.Text + "','" + ddnegeri2.SelectedValue + "','" + Txtnotel_R.Text + "','" + Txtnotel_P.Text + "','" + txtemail.Text + "','" + fname + "','" + Session["New"].ToString() + "','" + DateTime.Now + "','" + ddorganisasi.SelectedValue + "','" + dd_org_pen.SelectedValue + "','A')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql);
                    //clear1();
                    if (Status == "SUCCESS")
                    {
                        DataTable cnt_no1 = new DataTable();
                        cnt_no1 = dbcon.Ora_Execute_table("select * from KW_Ref_Carta_Akaun where kod_akaun='12.01'");
                        if (cnt_no1.Rows.Count != 0)
                        {
                            DataTable cnt_no2 = new DataTable();
                            cnt_no2 = dbcon.Ora_Execute_table("select (count(*) + 1) as cnt from KW_Ref_Carta_Akaun where jenis_akaun='12.01'");

                            string ss1_cnt = cnt_no2.Rows[0]["cnt"].ToString();
                            string ss2_cnt = cnt_no1.Rows[0]["kod_akaun"].ToString() + "." + ss1_cnt.PadLeft(2, '0');

                            string Inssql1 = "insert into KW_Ref_Carta_Akaun(kat_akaun,nama_akaun,kod_akaun,jenis_akaun,under_parent,KW_Debit_amt,KW_kredit_amt,Kw_open_amt,jenis_akaun_type,under_jenis,Status,crt_id,cr_dt,Susu_nilai,kod_akaun_cnt,sts_kawalan,ct_kod_industry) values ('" + cnt_no1.Rows[0]["kat_akaun"].ToString() + "','" + txtname.Text.Replace("'", "''") + "','" + ss2_cnt + "','" + cnt_no1.Rows[0]["kod_akaun"].ToString() + "','" + cnt_no1.Rows[0]["Id"].ToString() + "','0.00','0.00','0.00','3','','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0','" + cnt_no2.Rows[0]["cnt"].ToString() + "','S','')";
                            Status1 = dbcon.Ora_Execute_CommamdText(Inssql1);
                            if (Status1 == "SUCCESS")
                            {
                                string Inssql2 = "Update hr_staff_profile set stf_kod_akaun='" + ss2_cnt + "' where stf_staff_no='" + ss_no + "'";
                                Status2 = dbcon.Ora_Execute_CommamdText(Inssql2);
                            }
                        }

                        service.audit_trail("P0078", "Peribadi Simpan", "NO KAKITANGAN", ss_no);
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Response.Redirect("../SUMBER_MANUSIA/HR_DAFTER_STAFF_view.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('No Kakitangan Allowed only 10 Characters.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                if (fileName != "")
                {
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/user/" + fileName));//Or code to save in the DataBase.
                    fname = fileName;
                }
                else
                {
                    fname = dd.Rows[0]["Stf_image"].ToString();
                }
                string Inssql = "update hr_staff_profile set str_curr_org_cd='" + ddorganisasi.SelectedValue + "', stf_cur_sub_org='" + dd_org_pen.SelectedValue + "',stf_icno='" + txtic.Text + "',stf_name='" + txtname.Text.Replace("'", "''") + "',stf_salutation_cd='" + ddgel.SelectedValue + "',stf_title_cd='" + ddpang.SelectedValue + "',stf_sex_cd='" + ddjanita.SelectedValue + "',stf_age='" + txtumur.Text + "',stf_dob='" + tldt + "',stf_pob='" + ddnegeri.SelectedValue + "',stf_nationality_cd='" + ddwarg.SelectedValue + "',stf_race_cd='" + ddBangsa.SelectedValue + "',stf_religion_cd='" + ddagama.SelectedValue + "',stf_marital_sts_cd='" + ddstsper.SelectedValue + "',stf_permanent_address='" + txtalamat.Value.Replace("'", "''") + "',stf_permanent_postcode='" + txtpstcd.Text + "',stf_permanent_city='" + txtbandar.Text + "',stf_permanent_state_cd='" + ddnegeri1.SelectedValue + "',stf_mailing_address='" + txtalamatsurat.Value.Replace("'", "''") + "',stf_mailing_postcode='" + txtmposkod.Text + "',stf_mailing_city='" + txtmbandar.Text + "',stf_mailing_state_cd='" + ddnegeri2.SelectedValue + "',stf_phone_h='" + Txtnotel_R.Text + "',stf_phone_m='" + Txtnotel_P.Text + "',stf_email='" + txtemail.Text + "',stf_image='" + fname + "',stf_upd_id='" + Session["New"].ToString() + "',stf_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where stf_staff_no='" + ss_no + "' ";
                Status = dbcon.Ora_Execute_CommamdText(Inssql);
                //dt = dbcon.Ora_Execute_table("update hr_staff_profile set str_curr_org_cd='" + ddorganisasi.SelectedValue + "', stf_cur_sub_org='" + dd_org_pen.SelectedValue + "',stf_icno='" + txtic.Text + "',stf_name='" + txtname.Text.Replace("'", "''") + "',stf_salutation_cd='" + ddgel.SelectedValue + "',stf_title_cd='" + ddpang.SelectedValue + "',stf_sex_cd='" + ddjanita.SelectedValue + "',stf_age='" + txtumur.Text + "',stf_dob='" + tldt + "',stf_pob='" + ddnegeri.SelectedValue + "',stf_nationality_cd='" + ddwarg.SelectedValue + "',stf_race_cd='" + ddBangsa.SelectedValue + "',stf_religion_cd='" + ddagama.SelectedValue + "',stf_marital_sts_cd='" + ddstsper.SelectedValue + "',stf_permanent_address='" + txtalamat.Value.Replace("'", "''") + "',stf_permanent_postcode='" + txtpstcd.Text + "',stf_permanent_city='" + txtbandar.Text + "',stf_permanent_state_cd='" + ddnegeri1.SelectedValue + "',stf_mailing_address='" + txtalamatsurat.Value.Replace("'", "''") + "',stf_mailing_postcode='" + txtmposkod.Text + "',stf_mailing_city='" + txtmbandar.Text + "',stf_mailing_state_cd='" + ddnegeri2.SelectedValue + "',stf_phone_h='" + Txtnotel_R.Text + "',stf_phone_m='" + Txtnotel_P.Text + "',stf_email='" + txtemail.Text + "',stf_image='" + fname + "',stf_upd_id='" + Session["New"].ToString() + "',stf_upd_dt='" + DateTime.Now + "' where stf_staff_no='" + ss_no + "' ");
                //clear1();
                if (Status == "SUCCESS")
                {
                    string Inssql1 = "update KW_Ref_Carta_Akaun set nama_akaun='" + txtname.Text.Replace("'", "''") + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where kod_akaun='" + dd.Rows[0]["stf_kod_akaun"].ToString() + "' ";
                    Status1 = dbcon.Ora_Execute_CommamdText(Inssql1);
                    service.audit_trail("P0078", "Peribadi Kemaskini", "NO KAKITANGAN", ss_no);
                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("../SUMBER_MANUSIA/HR_DAFTER_STAFF_view.aspx");
                }
                else
                {                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Update.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }

        }
        else
        {
            Applcn_no.Focus();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "" && ddjawatan.SelectedValue != "" && txttlan.Text != "" && txthin.Text != "")
        {
            if (ddorganisasi.SelectedValue != "")
            {
                string sdt = string.Empty, edt = string.Empty, ssdt = string.Empty, ssdt1 = string.Empty;

                if (txttlan.Text != "")
                {
                    DateTime time1 = DateTime.ParseExact(txttlan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    sdt = time1.ToString("yyyy-MM-dd");
                }
                if (txthin.Text != "")
                {
                    DateTime time2 = DateTime.ParseExact(txthin.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    edt = time2.ToString("yyyy-MM-dd");
                }
                if (txttarber.Text != "")
                {
                    DateTime time3 = DateTime.ParseExact(txttarber.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ssdt = time3.ToString("yyyy-MM-dd");
                }
                if (txttmk.Text != "")
                {
                    DateTime time4 = DateTime.ParseExact(txttmk.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ssdt1 = time4.ToString("yyyy-MM-dd");
                }

                DataTable sel_phis = new DataTable();
                sel_phis = dbcon.Ora_Execute_table("select * from hr_post_his where pos_staff_no='" + Applcn_no1.Text + "' and pos_start_dt='" + sdt + "'");

                DataTable sel_phiscnt = new DataTable();
                sel_phiscnt = dbcon.Ora_Execute_table("select * from hr_post_his where pos_staff_no='" + Applcn_no1.Text + "'");
                string subj = string.Empty;
                if (sel_phiscnt.Rows.Count == 0 && ssdt == "9999-12-31")
                {
                    subj = "MENYERTAI";
                }

                if (sel_phiscnt.Rows.Count > 0 && ssdt == "9999-12-31")
                {
                    subj = ddSebab.SelectedItem.Text;
                }

                if (sel_phiscnt.Rows.Count > 0 && ssdt != "9999-12-31")
                {
                    subj = "PENAMATAN";
                }

                DataTable sel = new DataTable();
                sel = dbcon.Ora_Execute_table("select org_id,org_gen_id from hr_organization where org_gen_id='" + ddorganisasi.SelectedValue + "'");

                if (sel_phis.Rows.Count == 0)
                {
                    if (ssdt == "9999-12-31")
                    {
                        string Inssql = "insert into hr_post_his(pos_staff_no,pos_post_cd,pos_scheme_cd,pos_job_cat_cd,pos_grade_cd,pos_unit_cd,pos_dept_cd,pos_job_sts_cd,pos_start_dt,pos_end_dt,pos_spv_name1,pos_spv_name2,pos_move_reason_cd,pos_crt_id,pos_crt_dt,pos_subjek) values('" + Applcn_no1.Text + "','" + ddjawatan.SelectedValue + "','" + ddskim.SelectedValue + "','" + ddKategori.SelectedValue + "','" + ddgred.SelectedValue + "','" + ddunit.SelectedValue + "','" + ddjabatan.SelectedValue + "','" + ddstspenj.SelectedValue + "','" + sdt + "','" + edt + "','" + ddpen1.SelectedValue + "','" + ddpen2.SelectedValue + "','" + ddSebab.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + subj + "')";
                        Status = dbcon.Ora_Execute_CommamdText(Inssql);
                    }
                    else
                    {
                        string Inssql = "insert into hr_post_his(pos_staff_no,pos_post_cd,pos_scheme_cd,pos_job_cat_cd,pos_grade_cd,pos_unit_cd,pos_dept_cd,pos_job_sts_cd,pos_start_dt,pos_end_dt,pos_spv_name1,pos_spv_name2,pos_move_reason_cd,pos_crt_id,pos_crt_dt,pos_subjek) values('" + Applcn_no1.Text + "','" + ddjawatan.SelectedValue + "','" + ddskim.SelectedValue + "','" + ddKategori.SelectedValue + "','" + ddgred.SelectedValue + "','" + ddunit.SelectedValue + "','" + ddjabatan.SelectedValue + "','" + ddstspenj.SelectedValue + "','" + ssdt + "','9999/12/31','" + ddpen1.SelectedValue + "','" + ddpen2.SelectedValue + "','" + ddSebab.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + subj + "')";
                        Status = dbcon.Ora_Execute_CommamdText(Inssql);
                    }
                    if (Status == "SUCCESS")
                    {

                        DataTable chk_login = new DataTable();
                        chk_login = dbcon.Ora_Execute_table("select * from kk_User_Login where KK_userid='" + Applcn_no1.Text + "' and Status='A'");
                        
                        if (chk_login.Rows.Count == 0)
                        {
                            string Inssql = "Insert into kk_User_Login (KK_userid,KK_password,Kk_username,KK_email,KK_roles,Status,Kk_crt_id,KK_crt_dt,KK_skrins,user_img,KK_user_type) Values('" + Applcn_no1.Text + "','12345','" + txtname.Text.Replace("''", "'") + "','" + txtemail.Text + "','R0030','A','" + Session["new"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','','','N')";
                            Status = dbcon.Ora_Execute_CommamdText(Inssql);
                        }
                        else
                        {
                            string Inssql = "Update kk_User_Login set KK_user_type='N',Kk_username='" + txtname.Text.Replace("''", "'") + "',KK_email='" + txtemail.Text + "',Kk_upd_id='" + Session["new"].ToString() + "',KK_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where  KK_userid='" + Applcn_no1.Text + "' and Status='A'";
                            Status = dbcon.Ora_Execute_CommamdText(Inssql);

                        }

                        DataTable sel_phis1 = new DataTable();
                        if (ssdt == "9999-12-31")
                        {
                            sel_phis1 = dbcon.Ora_Execute_table("select top(1) FORMAT(pos_start_dt,'yyyy-MM-dd', 'en-us') as s1 from hr_post_his where pos_staff_no='" + Applcn_no1.Text + "' and pos_start_dt !='" + sdt + "' order by pos_crt_dt desc");
                        }
                        else
                        {

                            sel_phis1 = dbcon.Ora_Execute_table("select top(1) FORMAT(pos_start_dt,'yyyy-MM-dd', 'en-us') as s1 from hr_post_his where pos_staff_no='" + Applcn_no1.Text + "' and pos_start_dt !='" + ssdt + "' order by pos_crt_dt desc");
                        }
                        if (sel_phis1.Rows.Count != 0)
                        {
                            DateTime time1_pos = DateTime.ParseExact(txttlan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string pdt1 = time1_pos.AddDays(-1).ToString("yyyy-MM-dd");
                            DateTime time1_pos1 = DateTime.ParseExact(txttarber.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string pdt2 = time1_pos1.AddDays(-1).ToString("yyyy-MM-dd");
                            if (ssdt == "9999-12-31")
                            {

                                string upssql1 = "update hr_post_his set pos_end_dt='" + pdt1 + "',pos_upd_id ='" + Session["New"].ToString() + "',pos_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where pos_staff_no ='" + Applcn_no1.Text + "' and pos_start_dt ='" + sel_phis1.Rows[0]["s1"].ToString() + "'";
                                Status = dbcon.Ora_Execute_CommamdText(upssql1);
                            }
                            else
                            {
                                string upssql1 = "update hr_post_his set pos_end_dt='" + pdt2 + "',pos_upd_id ='" + Session["New"].ToString() + "',pos_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where pos_staff_no ='" + Applcn_no1.Text + "' and pos_start_dt ='" + sel_phis1.Rows[0]["s1"].ToString() + "'";
                                Status = dbcon.Ora_Execute_CommamdText(upssql1);
                            }
                        }
                        string upssql = "update hr_staff_profile set stf_curr_post_cd='" + ddjawatan.SelectedValue + "',stf_curr_scheme_cd ='" + ddskim.SelectedValue + "',stf_curr_job_cat_cd ='" + ddKategori.SelectedValue + "',stf_curr_grade_cd ='" + ddgred.SelectedValue + "',stf_curr_unit_cd ='" + ddunit.SelectedValue + "',stf_curr_dept_cd ='" + ddjabatan.SelectedValue + "',stf_curr_job_sts_cd ='" + ddstspenj.SelectedValue + "',stf_service_start_dt='" + ssdt1 + "',stf_service_end_dt ='" + ssdt + "',stf_service_end_reason ='" + ddsebabber.SelectedValue + "',stf_working_hour='" + DropDownList1.SelectedValue + "',stf_upd_id ='" + Session["New"].ToString() + "',stf_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where stf_staff_no ='" + Applcn_no1.Text + "'";
                        Status = dbcon.Ora_Execute_CommamdText(upssql);
                        send_email();
                        clear2();
                        bind1();
                        txthin.Text = "31/12/9999";
                        service.audit_trail("P0078", "Penjawatan Simpan","NO KAKITANGAN", Applcn_no1.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {
                        bind1();                        
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        tab2();
    }


    void send_email()
    {
        CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
        TextInfo txtinfo = culinfo.TextInfo;

        try
        {
            DataTable Ds1 = new DataTable();
            DataTable Ds2 = new DataTable();
            DataTable email_settings = new DataTable();
            email_settings = Dblog.Ora_Execute_table("select config_email_head,config_email_host,config_email_id,config_email_port,config_email_pwd,config_email_url,auto_email_id,config_email_web from site_settings where ID='1'");
            //Ds1 = Dblog.Ora_Execute_table("select pos_spv_name1,h2.stf_name,h2.stf_email from hr_post_his h1 left join hr_staff_profile h2 on h2.stf_staff_no=h1.pos_spv_name1 where pos_staff_no='" + Applcn_no.Text + "' and pos_end_dt >='" + DateTime.Now.ToString("yyyy-MM-dd") + "'");
            Ds2 = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid IN ('" + email_settings.Rows[0]["auto_email_id"].ToString().Replace(",", "','") + "')");
            var fromemail = new MailAddress(email_settings.Rows[0]["config_email_id"].ToString(), email_settings.Rows[0]["config_email_head"].ToString());
            var fromemailpassword = email_settings.Rows[0]["config_email_pwd"].ToString();
            string subject = string.Empty, body = string.Empty, email = string.Empty;


            // Employee Email
            if (txtemail.Text != "")
            {
                var toemail = new MailAddress(txtemail.Text);
                subject = "Employee Registeration";
                body = "Dear <strong>" + txtinfo.ToTitleCase(txtname.Text.ToLower()) + "</strong>,<br/>Successfully Completed your Registration.<br/>Username : " + Applcn_no.Text + " <br/>Password : 12345 <br/>Position : " + ddjawatan.SelectedItem.Text + "<br/><br/> If You require any clarifications about this application, please contact your HR Department.<br/><br/> Thank You,<br/><a><html><body><a href='" + email_settings.Rows[0]["config_email_url"].ToString() + "'> HRMS </a></body></html> . </a>";

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = email_settings.Rows[0]["config_email_host"].ToString(),
                    Port = Int32.Parse(email_settings.Rows[0]["config_email_port"].ToString()),
                    EnableSsl = false,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromemail.Address, fromemailpassword)
                };
                using (var message = new MailMessage(fromemail, toemail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);

            }

            // Manager Email
            //if (Ds1.Rows.Count != 0)
            //{
            //    if (Ds1.Rows[0]["stf_email"].ToString() != "")
            //    {
            //        var toemail = new MailAddress(Ds1.Rows[0]["stf_email"].ToString());
            //        subject = "Employee Registeration";
            //        body = "Dear <strong>" + txtinfo.ToTitleCase(Ds1.Rows[0]["stf_name"].ToString().ToLower()) + "</strong>,<br/>Successfully Added New Employee.<br/>Username : " + Applcn_no.Text + " <br/>Position : " + ddjawatan.SelectedItem.Text + "<br/><br/> If You require any clarifications about this application, please contact your HR Department.<br/><br/> Thank You,<br/><a><html><body><a href='" + email_settings.Rows[0]["config_email_url"].ToString() + "'> HRMS </a></body></html> . </a>";

            //        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
            //        {
            //            Host = email_settings.Rows[0]["config_email_host"].ToString(),
            //            Port = Int32.Parse(email_settings.Rows[0]["config_email_port"].ToString()),
            //            EnableSsl = false,
            //            DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
            //            UseDefaultCredentials = false,
            //            Credentials = new NetworkCredential(fromemail.Address, fromemailpassword)
            //        };
            //        using (var message = new MailMessage(fromemail, toemail)
            //        {
            //            Subject = subject,
            //            Body = body,
            //            IsBodyHtml = true
            //        })
            //            smtp.Send(message);

            //    }
            //}

            // HR Email

            if (Ds2.Rows.Count != 0)
            {
                for (int i = 0; i < Ds2.Rows.Count; i++)
                {
                    if (Ds2.Rows[i]["Kk_email"].ToString() != "")
                    {
                        var toemail = new MailAddress(Ds2.Rows[i]["Kk_email"].ToString());
                        subject = "Employee Registeration";
                        body = "Dear <strong>" + txtinfo.ToTitleCase(Ds2.Rows[i]["Kk_username"].ToString().ToLower()) + "</strong>,<br/>Successfully Added New Employee.<br/>Username : " + Applcn_no.Text + " <br/>Position : " + ddjawatan.SelectedItem.Text + "<br/><br/> If You require any clarifications about this application, please contact your HR Department.<br/><br/> Thank You,<br/><a><html><body><a href='" + email_settings.Rows[0]["config_email_url"].ToString() + "'> HRMS </a></body></html> . </a>";

                        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                        {
                            Host = email_settings.Rows[0]["config_email_host"].ToString(),
                            Port = Int32.Parse(email_settings.Rows[0]["config_email_port"].ToString()),
                            EnableSsl = false,
                            DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(fromemail.Address, fromemailpassword)
                        };
                        using (var message = new MailMessage(fromemail, toemail)
                        {
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = true
                        })
                            smtp.Send(message);

                    }
                }
            }

        }
        catch (Exception ex)
        {
            service.LogError(ex.ToString());
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        if (Applcn_no.Text != "" && ddjawatan.SelectedValue != "" && txttlan.Text != "" && txthin.Text != "")
        {
            //if (ddorganisasi.SelectedItem.Text != "--- PILIH ---")
            //{

            string sdt = string.Empty, edt = string.Empty, ssdt = string.Empty, ssdt1 = string.Empty, sd = string.Empty;

            if (txttlan.Text != "")
            {
                DateTime time1 = DateTime.ParseExact(txttlan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                sdt = time1.ToString("yyyy-MM-dd");
            }
            if (txthin.Text != "")
            {
                DateTime time2 = DateTime.ParseExact(txthin.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                edt = time2.ToString("yyyy-MM-dd");
            }
            if (txttarber.Text != "31/12/9999")
            {

                DateTime time3 = DateTime.ParseExact(txttarber.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ssdt = time3.ToString("yyyy-MM-dd");
            }
            else
            {
                txttarber.Text = "31/12/9999";
                DateTime time3 = DateTime.ParseExact(txttarber.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ssdt = time3.ToString("yyyy-MM-dd");
            }
            if (txttmk.Text != "")
            {
                DateTime time4 = DateTime.ParseExact(txttmk.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ssdt1 = time4.ToString("yyyy-MM-dd");
            }
            DataTable sel = new DataTable();
            sel = dbcon.Ora_Execute_table("select org_id,org_gen_id from hr_organization where org_gen_id='" + ddorganisasi.SelectedValue + "'");

            //DataTable sel_org = new DataTable();
            //sel_org = dbcon.Ora_Execute_table("select org_id,org_gen_id from hr_organization where org_gen_id='" + ddorganisasi.SelectedValue + "'");



            //string upssql = "update hr_staff_profile set stf_service_start_dt ='" + ssdt1 + "',stf_service_end_dt ='" + ssdt + "',stf_service_end_reason ='" + ddsebabber.SelectedValue + "',stf_working_hour='" + DropDownList1.SelectedValue + "',stf_upd_id ='" + Session["New"].ToString() + "',stf_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where stf_staff_no ='" + Applcn_no.Text + "'";
            //Status = dbcon.Ora_Execute_CommamdText(upssql);
            //DataTable sel1 = new DataTable();
            //sel1 = dbcon.Ora_Execute_table("select format(pos_start_dt,'yyyy/MM/dd') pos_start_dt from hr_post_his where pos_org_id='" + sel.Rows[0]["org_id"].ToString() + "' and pos_gen_ID='" + sel.Rows[0]["org_gen_id"].ToString() + "' and pos_staff_no='" + Applcn_no.Text + "'");

            string inssql = "update hr_post_his set pos_post_cd='" + ddjawatan.SelectedValue + "',pos_scheme_cd='" + ddskim.SelectedValue + "',pos_job_cat_cd='" + ddKategori.SelectedValue + "',pos_grade_cd='" + ddgred.SelectedValue + "',pos_unit_cd='" + ddunit.SelectedValue + "',pos_dept_cd='" + ddjabatan.SelectedValue + "',pos_job_sts_cd='" + ddstspenj.SelectedValue + "',pos_end_dt='" + edt + "',pos_start_dt='" + sdt + "',pos_spv_name1='" + ddpen1.SelectedValue + "',pos_spv_name2='" + ddpen2.SelectedValue + "',pos_move_reason_cd='" + ddSebab.SelectedValue + "',pos_upd_id='" + Session["New"].ToString() + "',pos_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where pos_staff_no='" + Applcn_no1.Text + "' and pos_start_dt='" + sdt + "'";
            Status = dbcon.Ora_Execute_CommamdText(inssql);
            if (Status == "SUCCESS")
            {
                if (edt == "9999-12-31")
                {

                    string inssql1 = "update hr_staff_profile set stf_curr_post_cd='" + ddjawatan.SelectedValue + "',stf_curr_scheme_cd ='" + ddskim.SelectedValue + "',stf_curr_job_cat_cd ='" + ddKategori.SelectedValue + "',stf_curr_grade_cd ='" + ddgred.SelectedValue + "',stf_curr_unit_cd ='" + ddunit.SelectedValue + "',stf_curr_dept_cd ='" + ddjabatan.SelectedValue + "',stf_curr_job_sts_cd ='" + ddstspenj.SelectedValue + "',stf_service_start_dt='" + ssdt1 + "',stf_service_end_dt ='" + ssdt + "',stf_service_end_reason ='" + ddsebabber.SelectedValue + "',stf_working_hour='" + DropDownList1.SelectedValue + "',stf_upd_id ='" + Session["New"].ToString() + "',stf_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where stf_staff_no ='" + Applcn_no1.Text + "'";
                    Status = dbcon.Ora_Execute_CommamdText(inssql1);
                }
                clear2();
                bind1();
                Button5.Visible = true;
                Button6.Visible = false;
                //ddorganisasi.Attributes.Remove("style");
                txttlan.Attributes.Remove("style");
                txthin.Text = "31/12/9999";
                service.audit_trail("P0078", "Penjawatan Kemaskini", "NO KAKITANGAN", Applcn_no1.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud Rekod Tidak Dibenarkan Untuk Dikemaskini.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukan Organisasi');", true);
            //}
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Staff No.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        tab2();
    }
    protected void txtemail_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var eMailValidator = new System.Net.Mail.MailAddress("xyz@blabla.com");
        }
        catch
        {
            // wrong e-mail address
        }
    }
    protected void ddjabatan_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_unit_code,UPPER(hr_unit_desc) as hr_unit_desc from ref_hr_unit where hr_jaba_code='" + ddjabatan.SelectedItem.Value + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddunit.DataSource = dt;
            ddunit.DataBind();
            ddunit.DataTextField = "hr_unit_desc";
            ddunit.DataValueField = "hr_unit_Code";
            ddunit.DataBind();
            ddunit.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
        
        tab2();
    }


    protected void cetak_Click(object sender, EventArgs e)
    {
        try
        {
            if (Applcn_no.Text != "")
            {
                string filename = string.Empty;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = dbcon.Ora_Execute_table("select stf_staff_no as v1,stf_icno as v2,ISNULL(hg.hr_gelaran_desc,'-') as v3,stf_name as v4,ISNULL(ht.hr_titl_desc,'-') as v5,ISNULL(rg.gender_desc,'-') as v6,stf_age  as v7,format(stf_dob,'dd/MM/yyyy') as v8,ISNULL(hn.hr_negeri_desc,'-') as v9,ISNULL(hb.hr_bangsa_desc,'-') as v10,ISNULL(hw.hr_wargan_desc,'-') as v11,ISNULL(ha.hr_agama_desc,'-') as v12,ISNULL(hsp.hr_perkha_desc,'-') as v13,ISNULL(stf_permanent_address,'-') as v14,ISNULL(stf_permanent_postcode,'-') as v15,ISNULL(stf_permanent_city,'-') as v16,ISNULL(hn1.hr_negeri_desc,'-') as v17,ISNULL(stf_mailing_address,'-') as v18,ISNULL(stf_mailing_postcode,'-') as v19,ISNULL(stf_mailing_city,'-') as v20,ISNULL(hn2.hr_negeri_desc,'-') as v21,ISNULL(stf_phone_h,'-') as v22,ISNULL(stf_phone_m,'-') as v23,ISNULL(stf_email,'-') as v24,stf_image as v25,FORMAT(stf_service_start_dt,'dd/MM/yyyy', 'en-us') as v26, o1.org_name v27, o2.op_perg_name v28 from hr_staff_profile left join ref_gender as rg on rg.gender_cd=stf_sex_cd left join Ref_hr_gelaran as hg on hg.hr_gelaran_Code=stf_salutation_cd left join Ref_hr_title ht on ht.hr_titl_Code=stf_title_cd left join ref_hr_negeri hn on hn.hr_negeri_Code=stf_pob left join Ref_hr_bangsa hb on hb.hr_bangsa_Code=stf_race_cd left join Ref_hr_wargan hw on hw.hr_wargan_Code=stf_nationality_cd left join Ref_hr_agama ha on ha.hr_agama_Code=stf_religion_cd left join Ref_hr_sts_perkha hsp on hsp.hr_perkha_Code=stf_marital_sts_cd left join ref_hr_negeri hn1 on hn1.hr_negeri_Code=stf_permanent_state_cd left join ref_hr_negeri hn2 on hn2.hr_negeri_Code=stf_mailing_state_cd left join hr_organization o1 on o1.org_gen_id=str_curr_org_cd left join hr_organization_pern o2 on o2.op_perg_code=stf_cur_sub_org where stf_staff_no='" + Applcn_no1.Text + "'");
                ds.Tables.Add(dt);

                DataTable dt1 = new DataTable();
                dt1 = dbcon.Ora_Execute_table("select top(1) '' as p1,FORMAT(pos_start_dt,'dd/MM/yyyy', 'en-us') as p2,FORMAT(pos_end_dt,'dd/MM/yyyy', 'en-us') as p3,ISNULL(hj.hr_jaw_desc,'') as p4,UPPER(ISNULL(hg.hr_gred_desc,'-')) as p5,UPPER(ISNULL(hu.hr_unit_desc,'-')) as p6,UPPER(ISNULL(jab.hr_jaba_desc,'-')) as p7,ISNULL(kp.hr_kate_desc,'-') as p8,ISNULL(hs1.hr_skim_desc,'-') as p9,ISNULL(pt.hr_traf_desc,'-') as p10,ISNULL(sp1.stf_name,'-') as p11,ISNULL(sp2.stf_name,'-') as p12,ISNULL(jk.hr_jad_desc,'-') as p13,format(sp3.stf_service_start_dt,'dd/MM/yyyy') as p14,format(sp3.stf_service_end_dt,'dd/MM/yyyy') as p15,ISNULL(pk.hr_perkaki_desc,'-') as p16,isnull(hp1.hr_pen_desc,'-') as p17 from hr_post_his ph left join ref_hr_jawatan hj on hj.hr_jaw_Code=ph.pos_post_cd left join ref_hr_gred hg on hg.hr_gred_Code=ph.pos_grade_cd left join ref_hr_unit hu on hu.hr_unit_Code=ph.pos_unit_cd left join Ref_hr_jabatan jab on jab.hr_jaba_Code=ph.pos_dept_cd left join Ref_hr_penj_kategori kp on kp.hr_kate_Code=ph.pos_job_cat_cd left join ref_hr_skim hs1 on hs1.hr_skim_Code=ph.pos_scheme_cd left join Ref_hr_penj_traf pt on pt.hr_traf_Code=ph.pos_job_sts_cd left join hr_staff_profile sp1 on sp1.stf_staff_no=ph.pos_spv_name1 left join hr_staff_profile sp2 on sp2.stf_staff_no=ph.pos_spv_name2 left join hr_staff_profile sp3 on sp3.stf_staff_no=ph.pos_staff_no left join Ref_hr_Jad_kerj as jk on jk.hr_jad_Code=sp3.stf_working_hour left join Ref_hr_per_kaki as pk on pk.hr_perkaki_Code=ph.pos_move_reason_cd left join Ref_hr_penamatan as hp1 on hp1.hr_pen_Code=sp3.stf_service_end_reason where pos_staff_no ='" + Applcn_no1.Text + "' order by pos_end_dt desc");
                ds.Tables.Add(dt1);
                RptviwerStudent.Reset();
                RptviwerStudent.LocalReport.ReportPath = "SUMBER_MANUSIA/Dafter_stf.rdlc";
                RptviwerStudent.LocalReport.EnableExternalImages = true;
                string imagePath = string.Empty;
                if (dt.Rows[0]["v25"].ToString() != "")
                {
                    imagePath = new Uri(Server.MapPath("~/FILES/user/" + dt.Rows[0]["v25"].ToString() + "")).AbsoluteUri;
                }
                else
                {
                    imagePath = new Uri(Server.MapPath("~/FILES/user/no_image.jpg")).AbsoluteUri;
                }
                ReportDataSource rds = new ReportDataSource("DS1", dt);
                ReportDataSource rds1 = new ReportDataSource("DS2", dt1);

                ReportParameter[] rptParams = new ReportParameter[]{
                    new ReportParameter("imgname", imagePath)
                 };
                RptviwerStudent.LocalReport.SetParameters(rptParams);
                RptviwerStudent.LocalReport.DataSources.Add(rds);
                RptviwerStudent.LocalReport.DataSources.Add(rds1);
                RptviwerStudent.LocalReport.Refresh();
                filename = string.Format("{0}.{1}", "MAKLUMAT_KAKITANGAN_" + dt.Rows[0]["v1"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                //}
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                byte[] bytes = RptviwerStudent.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sila masukan Maklumat Carian');", true);
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Issue');", true);
        }
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        bind1();

    }

    //protected void Button5_Click(object sender, EventArgs e)
    //{
    //    Session["validate_success"] = "";
    //    Session["alrt_msg"] = "";
    //    Session["pro_id"] = "";
    //    Response.Redirect("../SUMBER_MANUSIA/HR_DAFTER_STAFF.aspx");
    //}

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_DAFTER_STAFF_view.aspx");
    }


}