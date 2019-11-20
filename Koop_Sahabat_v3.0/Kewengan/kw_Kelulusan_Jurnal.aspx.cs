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

using System.Data.OleDb;

using System.IO;

using System.Net;

using System.Threading;



public partial class kw_Kelulusan_Jurnal : System.Web.UI.Page

{



    DBConnection Dblog = new DBConnection();

    DBConnection DBCon = new DBConnection();

    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;

    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);

    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);

    DBConnection Dbcon = new DBConnection();

    string qry1 = string.Empty, qry2 = string.Empty, tbl_name1 = string.Empty, col_name1 = string.Empty;

    string str_sdt1 = string.Empty, end_edt1 = string.Empty;

    string query1 = string.Empty;

    string userid, level;

    int sum = 0;

    float total = 0;

    float total1 = 0;

    float total2 = 0;

    string uniqueId, Status;

    string jurnal_qry = string.Empty;

    string Status_del = string.Empty, Status1 = string.Empty, Status2 = string.Empty;

    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty;
    double tot1 = 0, tot2 = 0;
    protected void Page_Load(object sender, EventArgs e)

    {

        app_language();

        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        
        scriptManager.RegisterPostBackControl(this.btnprintmoh);



        string get_edt = string.Empty;

        DataTable sel_gst2 = new DataTable();

        sel_gst2 = Dbcon.Ora_Execute_table("select top(1) Format(tarikh_mula,'dd/MM/yyyy') as st_dt,Format(tarikh_akhir,'dd/MM/yyyy') as end_dt from kw_profile_syarikat where cur_sts='1' order by tarikh_akhir desc");

        if (sel_gst2.Rows.Count != 0)

        {

            get_edt = sel_gst2.Rows[0]["st_dt"].ToString();



            string fdate = get_edt;

            DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateTime fd1 = DateTime.ParseExact(sel_gst2.Rows[0]["end_dt"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string s1_dt = "" + fd.ToString("yyyy") + ", " + (double.Parse(fd.ToString("MM")) - 1) + ", " + fd.ToString("dd") + "";

            string s2_dt = "" + fd1.ToString("yyyy") + ", " + (double.Parse(fd1.ToString("MM")) - 1) + ", " + fd1.ToString("dd") + "";

            start_dt1.Text = s1_dt;

            end_dt1.Text = s2_dt;

            str_sdt1 = fd.ToString("yyyy-MM-dd");

            end_edt1 = fd1.ToString("yyyy-MM-dd");

            string script = "  $().ready(function () {  var today = new Date();   var preYear = today.getFullYear() - 1; var curYear = today.getFullYear() - 0; $('.datepicker2').datepicker({ format: 'dd/mm/yyyy',autoclose: true,inline: true,startDate: new Date(" + s1_dt + "),endDate: new Date(" + s2_dt + ")}).on('changeDate', function(ev) {(ev.viewMode == 'days') ? $(this).datepicker('hide') : '';}); $('.select2').select2(); $(" + pp6.ClientID + ").click(function() { $(" + hd_txt.ClientID + ").text('MOHON BAYAR'); });});";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);

        }

        if (!IsPostBack)

        {

            if (Session["New"] != null)

            {





                userid = Session["New"].ToString();

                txtid.Text = userid;

                txtid.Attributes.Add("disabled", "disabled");

                bind_doc_kod();

                bind_jen_jurnal();

                bind_kod_akaun();

                negeri();

                negera();

                bind_kod_industry();

                jenis_cara();

                SetInitialRowmoh();

                akaun();
                
                Bayaran();                

                BindMohon();

                hd_txt.Text = "MOHON BAYAR";

            }

            else

            {

                Response.Redirect("../KSAIMB_Login.aspx");

            }

        }

    }



    void bind_kod_akaun()

    {

        DataSet Ds = new DataSet();

        try

        {

            string com = "select distinct kod_akaun,(kod_akaun + ' | ' + upper(nama_akaun)) nama_akaun from KW_Ref_Carta_Akaun where jenis_akaun_type !='1' and Status='A' and ISNULL(kw_acc_header,'0') = '0' order by kod_akaun asc";

            SqlDataAdapter adpt = new SqlDataAdapter(com, con);

            DataTable dt = new DataTable();

            adpt.Fill(dt);

            pel_dd_akaun.DataSource = dt;

            pel_dd_akaun.DataTextField = "nama_akaun";

            pel_dd_akaun.DataValueField = "kod_akaun";

            pel_dd_akaun.DataBind();

            pel_dd_akaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));



        }

        catch (Exception ex)

        {

            throw ex;

        }

    }

    void bind_doc_kod()

    {

        DataSet Ds = new DataSet();

        try

        {

            string com = "select Ref_doc_code,Ref_doc_name from KW_Ref_Doc_kod where Status = 'A' and Ref_doc_code IN ('01','03','05','06','09','10','11')";

            SqlDataAdapter adpt = new SqlDataAdapter(com, con);

            DataTable dt = new DataTable();

            adpt.Fill(dt);

            jenis_trxn.DataSource = dt;

            jenis_trxn.DataTextField = "Ref_doc_name";

            jenis_trxn.DataValueField = "Ref_doc_code";

            jenis_trxn.DataBind();

            jenis_trxn.Items.Insert(0, new ListItem("--- PILIH ---", ""));



        }

        catch (Exception ex)

        {

            throw ex;

        }

    }





    void bind_jen_jurnal()

    {

        DataSet Ds = new DataSet();

        try

        {

            string com = "select jur_type_cd,jur_desc from KW_ref_jurnal_type where Status = 'A'";

            SqlDataAdapter adpt = new SqlDataAdapter(com, con);

            DataTable dt = new DataTable();

            adpt.Fill(dt);

            DropDownList5.DataSource = dt;

            DropDownList5.DataTextField = "jur_desc";

            DropDownList5.DataValueField = "jur_type_cd";

            DropDownList5.DataBind();

            DropDownList5.Items.Insert(0, new ListItem("--- PILIH ---", ""));



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



            string com = "select DISTINCT hr_negeri_code,UPPER(hr_negeri_desc) as hr_negeri_desc from ref_hr_negeri";

            SqlDataAdapter adpt = new SqlDataAdapter(com, con);

            DataTable dt = new DataTable();

            adpt.Fill(dt);



            pel_ddnegeri.DataSource = dt;

            pel_ddnegeri.DataBind();

            pel_ddnegeri.DataTextField = "hr_negeri_desc";

            pel_ddnegeri.DataValueField = "hr_negeri_code";

            pel_ddnegeri.DataBind();

            pel_ddnegeri.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }

        catch (Exception ex)

        {

            throw ex;

        }

    }



    void negera()

    {

        DataSet Ds = new DataSet();

        try

        {



            string com = "select * from Country";

            SqlDataAdapter adpt = new SqlDataAdapter(com, con);

            DataTable dt = new DataTable();

            adpt.Fill(dt);

            pel_dd_negera.DataSource = dt;

            pel_dd_negera.DataBind();

            pel_dd_negera.DataTextField = "CountryName";

            pel_dd_negera.DataValueField = "ID";

            pel_dd_negera.DataBind();

            pel_dd_negera.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }

        catch (Exception ex)

        {

            throw ex;

        }

    }

    void bind_kod_industry()

    {

        DataSet Ds = new DataSet();

        try

        {

            string com = "select kod_industry,(kod_industry +' | ' + msic_desc) as name from Kw_kod_industry left join KW_Ref_Kod_Industri on msic_kod=kod_industry where kod_industry != ''";

            SqlDataAdapter adpt = new SqlDataAdapter(com, con);

            DataTable dt = new DataTable();

            adpt.Fill(dt);

            pel_dd_kodind.DataSource = dt;

            pel_dd_kodind.DataTextField = "name";

            pel_dd_kodind.DataValueField = "kod_industry";

            pel_dd_kodind.DataBind();

            pel_dd_kodind.Items.Insert(0, new ListItem("--- PILIH ---", ""));



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

            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1776','705','1748','764','774','1777','776','767','768','1749','777','520','779','14','39','1778','324','1553','781','484','16','52','1037','1779','88','1486','1755', '61', '1756', '1757', '1780', '53', '35', '1781', '1553', '763', '88', '324', '711', '1765', '520', '770', '1782', '1779', '1755', '1757', '1783', '1784', '1765', '781', '1404','1785', '1786', '1779', '35', '1756', '1757', '1769', '1338', '1545', '769', '770', '1753', '1771', '169', '1772', '1748', '1755', '1773', '1774')");



            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;

            TextInfo txtinfo = culinfo.TextInfo;



            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[50][0].ToString().ToLower());

            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());

            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[50][0].ToString().ToLower());

            //ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[53][0].ToString().ToLower());

            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());

            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());

            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower());

            //ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());

            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());

            //ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());

            but.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());

            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());

            ps_lbl18.Text = txtinfo.ToTitleCase(gt_lng.Rows[51][0].ToString().ToLower());

            ps_lbl19.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());

            ps_lbl20.Text = txtinfo.ToTitleCase(gt_lng.Rows[49][0].ToString().ToLower());

            ps_lbl21.Text = txtinfo.ToTitleCase(gt_lng.Rows[25][0].ToString().ToLower());

            ps_lbl22.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());

            btncarian.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());

            ps_lbl24.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());

            ps_lbl25.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());

            ps_lbl26.Text = txtinfo.ToTitleCase(gt_lng.Rows[26][0].ToString().ToLower());

            ps_lbl27.Text = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower());

            ps_lbl28.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());

            ps_lbl29.Text = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower());

            ps_lbl30.Text = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower());

            ps_lbl31.Text = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower());            

            btnprintmoh.Text = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower());

            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());

            //ps_lbl35.Text = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower());

            ps_lbl36.Text = txtinfo.ToTitleCase(gt_lng.Rows[39][0].ToString().ToLower());

            //ps_lbl37.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());

            btnkem.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());

           



        }

        else

        {

            Response.Redirect("../KSAIMB_Login.aspx");

        }

    }

    protected void pel_Button5_Click(object sender, EventArgs e)

    {

        pel_clr_txt();

        ModalPopupExtender1.Show();

    }



    void pel_clr_txt()

    {

        chk_pel.Checked = false;

        pel_TextBox1.Text = "";

        pel_TextBox5.Text = "";

        pel_TextBox7.Text = "";

        pel_TextBox4.Text = "";

        pel_TextBox11.Text = "";

        pel_TextBox2.Text = "";

        pel_TextBox9.Text = "";

        pel_TextBox6.Text = "";

        pel_ddnegeri.SelectedValue = "";

        pel_TextBox8.Text = "";

        pel_TextBox14.Text = "";

        pel_dd_kodind.SelectedValue = "";

        pel_dd_negera.SelectedValue = "";

        pel_TextBox3.Text = "";



    }

    protected void pel_Click_bck(object sender, EventArgs e)

    {

        pel_clr_txt();

    }

    protected void pel_clk_submit(object sender, EventArgs e)

    {

        if (pel_TextBox1.Text != "" && pel_TextBox5.Text != "" && pel_dd_akaun.SelectedValue !="")

        {

            get_dd_value();

            string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty, chk_val1 = string.Empty;

            string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;

            DataTable cnt_no = new DataTable();

            if (pel_dd_akaun.SelectedValue != "")

            {
                if (Session["sno1"].ToString() == "PEM")
                {
                    tbl_name1 = "KW_Ref_Pembekal";
                    col_name1 = "pem";
                }
                else if (Session["sno1"].ToString() == "PEL")
                {
                    tbl_name1 = "KW_Ref_Pelanggan";
                    col_name1 = "pel";
                }

                //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");

                cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='" + pel_dd_akaun.SelectedValue + "'");



                set_cnt1 = cnt_no.Rows[0]["cnt"].ToString();

                mcnt = cnt_no.Rows[0]["rcnt"].ToString();

                set_cnt = pel_dd_akaun.SelectedValue;

                //set_cnt1 = "1";

            }

            else

            {

                set_cnt1 = "0";

                mcnt = "1";

                set_cnt = "00";

            }



            if (chk_pel.Checked == true)

            {

                chk_val1 = "1";

            }

            else

            {

                chk_val1 = "0";

            }

          
            string ss_kod = string.Empty;
            DataTable get_val = new DataTable();
            get_val = DBCon.Ora_Execute_table("select kat_akaun From KW_Ref_Carta_Akaun where kod_akaun='" + pel_dd_akaun.SelectedValue + "'");
            if (pel_dd_akaun.SelectedValue != "")
            {
                ss_kod = get_val.Rows[0]["kat_akaun"].ToString();
            }

            if (pel_ver_id.Text == "0")

            {

                DataTable ddokdicno = new DataTable();

                ddokdicno = DBCon.Ora_Execute_table("select * From " + tbl_name1 + " where Ref_nama_syarikat='" + pel_TextBox1.Text + "' and Ref_no_syarikat='" + pel_TextBox5.Text + "'");

                if (ddokdicno.Rows.Count == 0)

                {

                  

                    string Inssql = "insert into " + tbl_name1 + " (Ref_nama_syarikat,Ref_no_syarikat,Ref_no_telefone,Ref_no_fax,Ref_email,Ref_gst_id,Ref_kod_akaun,Ref_jenis_akaun,Ref_alamat,Status,crt_id,cr_dt,Ref_kawalan,ref_alamat_ked," + col_name1 + "_bandar," + col_name1 + "_negeri," + col_name1 + "_poskod," + col_name1 + "_negera," + col_name1 + "_kod_industry," + col_name1 + "_pelbagai) values ('" + pel_TextBox1.Text.Replace("'", "''") + "','" + pel_TextBox5.Text.Replace("'", "''") + "','" + pel_TextBox4.Text.Replace("'", "''") + "','" + pel_TextBox2.Text.Replace("'", "''") + "','" + pel_TextBox3.Text.Replace("'", "''") + "','" + pel_TextBox6.Text.Replace("'", "''") + "','"+ pel_TextBox10.Text +"','"+ ss_kod + "','" + pel_TextBox7.Text.Replace("'", "''") + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + pel_TextBox8.Text.Replace("'", "''") + "','" + pel_TextBox11.Text.Replace("'", "''") + "','" + pel_TextBox9.Text.Replace("'", "''") + "','" + pel_ddnegeri.SelectedValue + "','" + pel_TextBox14.Text.Replace("'", "''") + "','" + pel_dd_negera.SelectedValue + "','" + pel_dd_kodind.SelectedValue + "','" + chk_val1 + "')";

                    Status = DBCon.Ora_Execute_CommamdText(Inssql);

                    if (Status == "SUCCESS")

                    {

                        pel_clr_txt();

                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Label16.Text + " Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success'});", true);

                    }

                   
                }

                else

                {

                    ModalPopupExtender1.Show();

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

                }

            }

            else

            {

                

                    string Inssql = "UPDATE " + tbl_name1 + " set Ref_nama_syarikat='" + pel_TextBox1.Text.Replace("'", "''") + "',Ref_no_syarikat='" + pel_TextBox5.Text.Replace("'", "''") + "',Ref_no_telefone='" + pel_TextBox4.Text.Replace("'", "''") + "',Ref_no_fax='" + pel_TextBox2.Text.Replace("'", "''") + "',Ref_email='" + pel_TextBox3.Text.Replace("'", "''") + "',Ref_gst_id='" + pel_TextBox6.Text.Replace("'", "''") + "',Ref_alamat='" + pel_TextBox7.Text.Replace("'", "''") + "',Ref_kawalan='" + pel_TextBox8.Text.Replace("'", "''") + "',ref_alamat_ked='" + pel_TextBox11.Text.Replace("'", "''") + "'," + col_name1 + "_bandar='" + pel_TextBox9.Text.Replace("'", "''") + "'," + col_name1 + "_negeri='" + pel_ddnegeri.SelectedValue + "'," + col_name1 + "_poskod='" + pel_TextBox14.Text.Replace("'", "''") + "'," + col_name1 + "_negera='" + pel_dd_negera.SelectedValue + "'," + col_name1 + "_pelbagai='" + chk_val1 + "',Ref_jenis_akaun='" + ss_kod + "',Ref_kod_akaun='" + pel_TextBox10.Text + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id = '" + pel_get_id.Text + "'";

                    Status = DBCon.Ora_Execute_CommamdText(Inssql);

                    if (Status == "SUCCESS")

                    {
                        Session["pem_update"] = pel_TextBox10.Text.Replace("'", "''");
                    Session["sno1"] = "";
                        pel_clr_txt();                        
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success'});", true);
                        grd_details1();

                    }

               
            }



        }

        else

        {

            ModalPopupExtender1.Show();

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }



    }

  

    void jenis_cara()

    {

        DataSet Ds = new DataSet();

        try

        {

            string com = " select Jenis_bayaran_cd,Jenis_bayaran from KW_Jenis_Cara_bayaran where Status='A'";

            SqlDataAdapter adpt = new SqlDataAdapter(com, con);

            DataTable dt = new DataTable();

            adpt.Fill(dt);

            DropDownList2.DataSource = dt;

            DropDownList2.DataTextField = "Jenis_bayaran";

            DropDownList2.DataValueField = "Jenis_bayaran_cd";

            DropDownList2.DataBind();

            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));



        }

        catch (Exception ex)

        {

            throw ex;

        }

    }





   
   




    
   private void SetInitialRowmoh()

    {

        //DataTable dt = new DataTable();

        //DataRow dr = null;


        //dt.Columns.Add(new DataColumn("sno", typeof(string)));

        //dt.Columns.Add(new DataColumn("sno1", typeof(string)));

        //dt.Columns.Add(new DataColumn("kd_akaun", typeof(string)));

        //dt.Columns.Add(new DataColumn("jen_txi", typeof(string)));

        //dt.Columns.Add(new DataColumn("jum", typeof(string)));

        //dt.Columns.Add(new DataColumn("ket", typeof(string)));
       
        //dr = dt.NewRow();
        

        //dr["sno"] = string.Empty;

        //dr["sno1"] = string.Empty;

        //dr["kd_akaun"] = string.Empty;

        //dr["jen_txi"] = string.Empty;

        //dr["jum"] = string.Empty;

        //dr["ket"] = string.Empty;
      
        //dt.Rows.Add(dr);

        ////Store the DataTable in ViewState

        //ViewState["CurrentTable2"] = dt;

        //gridmohdup.DataSource = dt;

        //gridmohdup.DataBind();

    }





  
    protected void Gridview1_RowDataBound(object sender, GridViewRowEventArgs e)

    {

        if (e.Row.RowType == DataControlRowType.DataRow)

        {

            //Find the DropDownList in the Row

            DropDownList ddlCountries = (e.Row.FindControl("ddkodcre") as DropDownList);

            ddlCountries.DataSource = GetData("select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun from KW_ref_carta_akaun where jenis_akaun_type != '1' and Status='A'");

            ddlCountries.DataTextField = "nama_akaun";

            ddlCountries.DataValueField = "kod_akaun";

            ddlCountries.DataBind();



            //Add Default Item in the DropDownList

            ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));



            //Select the Country of Customer in DropDownList



        }

    }



    private DataSet GetData(string query)

    {

        string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;

        SqlCommand cmd = new SqlCommand(query);

        using (SqlConnection con = new SqlConnection(conString))

        {

            using (SqlDataAdapter sda = new SqlDataAdapter())

            {

                cmd.Connection = con;

                sda.SelectCommand = cmd;

                using (DataSet ds = new DataSet())

                {

                    sda.Fill(ds);

                    return ds;

                }

            }

        }

    }
    protected void ButtonAdd1_Click(object sender, EventArgs e)

    {
        AddNewRowToGrid1();
    }
    
    private void AddNewRowToGrid1()

    {

        int rowIndex = 0;

        decimal total1 = 0;

        decimal total2 = 0;

        decimal total3 = 0;

        decimal total4 = 0;

        if (ViewState["CurrentTable2"] != null)

        {

            DataTable dtCurrentTable2 = (DataTable)ViewState["CurrentTable2"];

            DataRow drCurrentRow = null;

            if (dtCurrentTable2.Rows.Count > 0)

            {

                for (int i = 1; i <= dtCurrentTable2.Rows.Count; i++)

                {

                    DropDownList box1 = (DropDownList)gridmohdup.Rows[rowIndex].Cells[1].FindControl("gridmohdup_ddakaun");
                    
                    TextBox box2 = (TextBox)gridmohdup.Rows[rowIndex].Cells[2].FindControl("ket");

                    //DropDownList box3 = (DropDownList)gridmohdup.Rows[rowIndex].Cells[3].FindControl("gridmohdup_jen_txi");

                    TextBox box4 = (TextBox)gridmohdup.Rows[rowIndex].Cells[3].FindControl("deb_amount");

                    TextBox box5 = (TextBox)gridmohdup.Rows[rowIndex].Cells[3].FindControl("kre_amount");
                    
                    drCurrentRow = dtCurrentTable2.NewRow();

                    dtCurrentTable2.Rows[i - 1]["kd_akaun"] = box1.SelectedValue;

                    dtCurrentTable2.Rows[i - 1]["ket"] = box2.Text;

                    dtCurrentTable2.Rows[i - 1]["jum_deb"] = double.Parse(box4.Text).ToString("C").Replace("$","").Replace("RM", "");
                     
                    dtCurrentTable2.Rows[i - 1]["jum_kre"] = double.Parse(box5.Text).ToString("C").Replace("$", "").Replace("RM", "");
                     
                    rowIndex++;

                    if (box4.Text != "")
                    {
                        decimal tamt1 = Convert.ToDecimal(box4.Text);
                        total1 += tamt1;
                    }

                    if (box5.Text != "")
                    {
                        decimal tamt2 = Convert.ToDecimal(box5.Text);
                        total2 += tamt2;                        
                    }
                }

                dtCurrentTable2.Rows.Add(drCurrentRow);

                ViewState["CurrentTable2"] = dtCurrentTable2;



                gridmohdup.DataSource = dtCurrentTable2;

                gridmohdup.DataBind();

                tab1();             

            }

        }

        else

        {

            Response.Write("ViewState is null");

        }
        
        SetPreviousData1();

    }

    private void SetPreviousData1()

    {

        int rowIndex = 0;

        if (ViewState["CurrentTable2"] != null)

        {

            DataTable dt = (DataTable)ViewState["CurrentTable2"];

            if (dt.Rows.Count > 0)

            {

                for (int i = 0; i < dt.Rows.Count; i++)

                {


                    DropDownList box1 = (DropDownList)gridmohdup.Rows[rowIndex].Cells[1].FindControl("gridmohdup_ddakaun");

                    TextBox box2 = (TextBox)gridmohdup.Rows[rowIndex].Cells[2].FindControl("ket");

                    //DropDownList box3 = (DropDownList)gridmohdup.Rows[rowIndex].Cells[3].FindControl("gridmohdup_jen_txi");

                    TextBox box4 = (TextBox)gridmohdup.Rows[rowIndex].Cells[3].FindControl("deb_amount");

                    TextBox box5 = (TextBox)gridmohdup.Rows[rowIndex].Cells[3].FindControl("kre_amount");

                    box1.SelectedValue = dt.Rows[i]["kd_akaun"].ToString();

                    box2.Text = dt.Rows[i]["ket"].ToString();
                    if (dt.Rows[i]["jum_deb"].ToString() != "")
                    {
                        box4.Text = double.Parse(dt.Rows[i]["jum_deb"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                    }
                    else
                    {
                        box4.Text = "0.00";
                    }
                    if (dt.Rows[i]["jum_kre"].ToString() != "")
                    {
                        box5.Text = double.Parse(dt.Rows[i]["jum_kre"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                    }
                    else
                    {
                        box5.Text = "0.00";
                    }
                    
                    rowIndex++;
                    
                }

            }

        }

    }


    private void SetRowData()

    {

        int rowIndex = 0;

        decimal total1 = 0;

        decimal total2 = 0;

        decimal total3 = 0;

        decimal total4 = 0;

        if (ViewState["CurrentTable2"] != null)

        {

            DataTable dtCurrentTable2 = (DataTable)ViewState["CurrentTable2"];

            DataRow drCurrentRow = null;

            if (dtCurrentTable2.Rows.Count > 0)

            {

                for (int i = 1; i <= dtCurrentTable2.Rows.Count; i++)
                {


                    DropDownList box1 = (DropDownList)gridmohdup.Rows[rowIndex].Cells[1].FindControl("gridmohdup_ddakaun");

                    TextBox box2 = (TextBox)gridmohdup.Rows[rowIndex].Cells[2].FindControl("ket");

                    //DropDownList box3 = (DropDownList)gridmohdup.Rows[rowIndex].Cells[3].FindControl("gridmohdup_jen_txi");

                    TextBox box4 = (TextBox)gridmohdup.Rows[rowIndex].Cells[3].FindControl("deb_amount");

                    TextBox box5 = (TextBox)gridmohdup.Rows[rowIndex].Cells[3].FindControl("kre_amount");

                    drCurrentRow = dtCurrentTable2.NewRow();

                    dtCurrentTable2.Rows[i - 1]["kd_akaun"] = box1.SelectedValue;

                    dtCurrentTable2.Rows[i - 1]["ket"] = box2.Text;

                    dtCurrentTable2.Rows[i - 1]["jum_deb"] = box4.Text;

                    dtCurrentTable2.Rows[i - 1]["jum_kre"] = box5.Text;

                 
                    rowIndex++;

                    if (box4.Text != "")
                    {
                        decimal tamt1 = Convert.ToDecimal(box4.Text);

                        total1 += tamt1;

                    }
                    if (box5.Text != "")
                    {
                        
                        decimal tamt2 = Convert.ToDecimal(box5.Text);

                        total2 += tamt2;
                    }

                   

                }

                //dtCurrentTable2.Rows.Add(drCurrentRow);

                ViewState["CurrentTable2"] = dtCurrentTable2;



                //grdmohon.DataSource = dtCurrentTable2;

                //grdmohon.DataBind();

                //tab1();

                //grdmohon.FooterRow.Cells[2].Text = "JUMLAH (RM) :";

                //grdmohon.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                //((Label)grdmohon.FooterRow.Cells[7].FindControl("Label2")).Text = total1.ToString("C").Replace("RM", "").Replace("$", "");

                //((Label)grdmohon.FooterRow.Cells[9].FindControl("Label7")).Text = total2.ToString("C").Replace("RM", "").Replace("$", "");

                //((Label)grdmohon.FooterRow.Cells[11].FindControl("Label4")).Text = total3.ToString("C").Replace("RM", "").Replace("$", "");

                //((Label)grdmohon.FooterRow.Cells[11].FindControl("Label9")).Text = total3.ToString("C").Replace("RM", "").Replace("$", "");

                //((Label)grdmohon.FooterRow.Cells[12].FindControl("Label6")).Text = total4.ToString("C").Replace("RM", "").Replace("$", "");



                //if (dtCurrentTable2.Rows.Count == 2)

                //{

                //    grdmohon.FooterRow.Cells[1].Enabled = false;

                //}

                //else

                //{

                //    grdmohon.FooterRow.Cells[1].Enabled = true;

                //}





            }

        }

        else

        {

            Response.Write("ViewState is null");

        }

        //SetPreviousData();

    }



    void akaun()

    {

        DataSet Ds = new DataSet();

        try

        {

            string com = "select kod_syarikat,nama_syarikat from KW_Profile_syarikat where cur_sts='1' and cur_sts='1'";

            SqlDataAdapter adpt = new SqlDataAdapter(com, con);

            DataTable dt = new DataTable();

            adpt.Fill(dt);

            ddakaun.DataSource = dt;

            ddakaun.DataTextField = "nama_syarikat";

            ddakaun.DataValueField = "kod_syarikat";

            ddakaun.DataBind();

            ddakaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));



            string com1 = "select kod_bajet,nama_bajet from KW_Ref_kod_bajet inner join KW_Ref_Bajet s1 on s1.Ref_kod_bajet=kod_bajet where s1.Status='A'";

            SqlDataAdapter adpt1 = new SqlDataAdapter(com1, con);

            DataTable dt1 = new DataTable();

            adpt1.Fill(dt1);

            DropDownList3.DataSource = dt1;

            DropDownList3.DataTextField = "nama_bajet";

            DropDownList3.DataValueField = "kod_bajet";

            DropDownList3.DataBind();

            DropDownList3.Items.Insert(0, new ListItem("--- PILIH ---", ""));



        }

        catch (Exception ex)

        {

            throw ex;

        }

    }


    

    void Bayaran()

    {

        DataSet Ds = new DataSet();

        try

        {

            string com = "select jenis_bayaran_cd,Jenis_bayaran from KW_Jenis_Cara_bayaran";

            SqlDataAdapter adpt = new SqlDataAdapter(com, con);

            DataTable dt = new DataTable();

            adpt.Fill(dt);

            //ddcbaya.DataSource = dt;

            //ddcbaya.DataTextField = "Jenis_bayaran";

            //ddcbaya.DataValueField = "jenis_bayaran_cd";

            //ddcbaya.DataBind();

            //ddcbaya.Items.Insert(0, new ListItem("--- PILIH ---", ""));





        }

        catch (Exception ex)

        {

            throw ex;

        }

    }

    protected void Button3_Click(object sender, EventArgs e)

    {



        Div2.Visible = true;

        Div3.Visible = false;

        kem_Button2.Visible = false;

        gridmohdup.Visible = false;

        grdbind.Visible = false;        

        sts.Visible = false;

        btnprintmoh.Visible = false;

        userid = Session["New"].ToString();

        level = Session["level"].ToString();



        if (level == "6")

        {

            txtid.Text = userid;

            txtid.Attributes.Add("disabled", "disabled");

        }

        else if (level == "5")

        {

            txtApr.Text = userid;

            txtApr.Attributes.Add("disabled", "disabled");

        }

        btnkem.Visible = false;        

        reset();

        tab1();

        GetMohon();

        BindMohon();

        DataTable get_pemo_info = new DataTable();

        get_pemo_info = Dbcon.Ora_Execute_table("SELECT Kk_username, KK_userid, ISNULL(s2.hr_unit_desc,'') as seksion,ISNULL(s1.hr_jaw_desc,'') as jawatan FROM KK_User_Login as table1 LEFT JOIN hr_staff_profile as table2 ON table1. KK_userid = table2.stf_staff_no left join Ref_hr_Jawatan s1 on s1.hr_jaw_Code=table2.stf_curr_post_cd left join Ref_hr_unit s2 on s2.hr_unit_Code = stf_curr_unit_cd where KK_userid='" + Session["New"].ToString() + "'");



        if (get_pemo_info.Rows.Count != 0)

        {

            txtid.Text = get_pemo_info.Rows[0]["Kk_username"].ToString();

            TextBox3.Text = get_pemo_info.Rows[0]["KK_userid"].ToString();

            TextBox4.Text = get_pemo_info.Rows[0]["seksion"].ToString();

            TextBox5.Text = get_pemo_info.Rows[0]["jawatan"].ToString();



        }



        txttarkihmo.Text = DateTime.Now.ToString("dd/MM/yyyy");

    }



    protected void new_entry(object sender, EventArgs e)

    {

        get_dd_value();

        ModalPopupExtender1.Show();



    }



    void get_dd_value()

    {



        if (DropDownList1.SelectedValue == "03")

        {

            tbl_name1 = "KW_Ref_Pelanggan";

            col_name1 = "pel";

            Label16.Text = "Pelenggan";

        }

        else if (DropDownList1.SelectedValue == "02")

        {

            tbl_name1 = "KW_Ref_Pembekal";

            col_name1 = "pem";

            Label16.Text = "Pembekal";

        }

    }

    protected void sel_jenis(object sender, EventArgs e)

    {

        ModalPopupExtender1.Show();

        pel_TextBox10.Text = pel_dd_akaun.SelectedValue;

    }



    protected void pel_katcd_TextChanged(object sender, EventArgs e)

    {

        get_dd_value();

        ModalPopupExtender1.Show();

        DataTable sel_kat = new DataTable();

        sel_kat = DBCon.Ora_Execute_table("select * from " + tbl_name1 + " where Ref_no_syarikat = '" + pel_TextBox5.Text + "'");

        if (sel_kat.Rows.Count != 0)

        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Pendaftaran Syarikat Kod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

            pel_TextBox5.Text = "";

            pel_TextBox5.Focus();

        }

        else

        {

            pel_TextBox1.Focus();

        }



    }





    protected void btncarian_Click(object sender, EventArgs e)

    {

        if (txticno.Text != "")

        {



            DataTable dt = new DataTable();

            dt = Dbcon.Ora_Execute_table("select stf_name,stf_bank_acc_no,stf_bank_cd,R.Bank_Name from  hr_staff_profile S left join Ref_Nama_Bank R on S.stf_bank_cd=R.Bank_Code where stf_staff_no='" + txticno.Text + "'");

            if (dt.Rows.Count > 0)

            {

                txtname.Text = dt.Rows[0][0].ToString();

                txtbname.Text = dt.Rows[0][3].ToString();

                txtbno.Text = dt.Rows[0][1].ToString();

                txtname.Attributes.Add("disabled", "disabled");

                txtbname.Attributes.Add("disabled", "disabled");

                txtbname.Attributes.Add("disabled", "disabled");



            }

        }

        else

        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Masukkan No Kakitangan');", true);



        }





    }

  
    protected void btnkem_Click(object sender, EventArgs e)

    {

        if (txtnoper.Text != "")

        {

            if (TextBox23.Text != "" && ddsts.SelectedValue != "")

            {
                if (DropDownList5.SelectedValue != "")

                {

                    string semsts = string.Empty, Inssql_main = string.Empty;



                    string akan_kod = string.Empty, deb_amt = string.Empty, kre_amt = string.Empty;

                    userid = Session["New"].ToString();

                    DataTable gen_invkd = new DataTable();
                    gen_invkd = Dbcon.Ora_Execute_table("select GL_journal_no from KW_General_Ledger where GL_journal_no='" + TextBox23.Text + "'");
                    if (gen_invkd.Rows.Count > 0)
                    {
                        DataTable dtmb_db1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='23' and Status='A'");
                        if (dtmb_db1.Rows.Count != 0)
                        {
                            if (dtmb_db1.Rows[0]["cfmt"].ToString() == "")
                            {
                                txtnoper_1.Text = dtmb_db1.Rows[0]["fmt"].ToString();
                            }
                            else
                            {
                                int seqno = Convert.ToInt32(dtmb_db1.Rows[0]["lfrmt2"].ToString());
                                int newNumber = seqno + 1;
                                uniqueId = newNumber.ToString(dtmb_db1.Rows[0]["lfrmt1"].ToString() + "0000");
                                txtnoper_1.Text = uniqueId;
                            }

                        }
                        else
                        {
                            DataTable get_doc = new DataTable();
                            get_doc = Dbcon.Ora_Execute_table("select Ref_doc_descript as s1,s1.ws_format as s2 from KW_Ref_Doc_kod left join site_settings s1 on  s1.ID='1' where Ref_doc_code='23'");
                            DataTable dtmb_db2 = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(GL_journal_no,13,3000)),'0') from KW_General_Ledger");
                            if (dtmb_db2.Rows.Count > 0)
                            {
                                int seqno = Convert.ToInt32(dtmb_db2.Rows[0][0].ToString());
                                int newNumber = seqno + 1;
                                uniqueId = newNumber.ToString(get_doc.Rows[0][1].ToString() + "-" + get_doc.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                                txtnoper_1.Text = uniqueId;

                            }
                            else
                            {
                                int newNumber = Convert.ToInt32(uniqueId) + 1;
                                uniqueId = newNumber.ToString(get_doc.Rows[0][1].ToString() + "-" + get_doc.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                                txtnoper_1.Text = uniqueId;
                            }
                        }

                    }
                    else
                    {
                        txtnoper_1.Text = TextBox23.Text;
                    }
                    decimal kreamt = 0;
                    decimal kreamt1 = 0;
                    decimal debamt = 0;
                    decimal debamt1 = 0;
                    int akaun_cnt = 0, j = 1, i = 0;
                    foreach (GridViewRow g1 in gridmohdup.Rows)
                    {
                        string kod_akaun1 = (g1.FindControl("gridmohdup_ddakaun") as DropDownList).SelectedValue;
                        string deb_amn1 = (g1.FindControl("deb_amount") as TextBox).Text;
                        string krd_amn1 = (g1.FindControl("kre_amount") as TextBox).Text;

                        if (kod_akaun1 != "")
                        {
                            akaun_cnt = j;
                            j++;
                        }

                        if (deb_amn1 != "0.00")
                        {
                            debamt1 += decimal.Parse(deb_amn1);
                        }

                        if (krd_amn1 != "0.00")
                        {
                            kreamt1 += decimal.Parse(krd_amn1);
                        }

                    }
                    if (akaun_cnt == gridmohdup.Rows.Count)
                    {
                        if (debamt1 == kreamt1)
                        {
                            foreach (GridViewRow g1 in gridmohdup.Rows)
                            {
                                string kd_akn = (g1.FindControl("gridmohdup_ddakaun") as DropDownList).SelectedValue;

                                string bajet = (g1.FindControl("Label1") as Label).Text;

                                string keterangan = (g1.FindControl("ket") as TextBox).Text;

                                string deb_amn = (g1.FindControl("deb_amount") as TextBox).Text;
                                string krd_amn = (g1.FindControl("kre_amount") as TextBox).Text;

                                string kd_type = (g1.FindControl("Label29") as Label).Text;
                                string kod_akaun = string.Empty;
                               
                                kod_akaun = kd_akn;
                               

                                if (deb_amn != "0.00")
                                {
                                    debamt = decimal.Parse(deb_amn);
                                    kreamt = 0;
                                }
                                if (krd_amn != "0.00")
                                {
                                    kreamt = decimal.Parse(krd_amn);
                                    debamt = 0;
                                }

                                double ss1 = 0;
                                string Inssql = "insert into KW_General_Ledger (GL_journal_no,GL_type,GL_post_dt,ref2,GL_invois_no,kod_akaun,GL_desc1,KW_Debit_amt,KW_kredit_amt,GL_sts,GL_post_note,kod_bajet,crt_id,cr_dt,Gl_jenis_no) values('" + txtnoper_1.Text + "','" + DropDownList5.SelectedValue + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + TextBox16.Text + "','" + TextBox19.Text + "','" + kod_akaun + "','" + keterangan + "','" + debamt + "','" + kreamt + "','" + ddsts.SelectedValue + "','" + jurnal_txt.Value + "','" + bajet + "','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','" + txtname.Text + "')";

                                Status = Dbcon.Ora_Execute_CommamdText(Inssql);
                                string gt1 = string.Empty, gt2 = string.Empty;
                                if (Status == "SUCCESS")
                                {
                                    //if (TextBox31_jcd.Text == "04")
                                    //{
                                    if (i == 0)
                                    {
                                        DataTable get_doc_info = new DataTable();
                                        get_doc_info = Dbcon.Ora_Execute_table("select Ref_doc_code,shadow_bjt,Actual_bjt,bjt_type from KW_Ref_Doc_kod where Ref_doc_code='" + TextBox31_jcd.Text + "' and Status='A'");
                                        if (get_doc_info.Rows.Count != 0)
                                        {
                                            if(get_doc_info.Rows[0]["shadow_bjt"].ToString() == "Y")
                                            {
                                                gt1 = "ref_shadow_bajet";
                                            }
                                            else if (get_doc_info.Rows[0]["bjt_type"].ToString() == "Y")
                                            {
                                                gt1 = "Ref_used_bajet";
                                            }

                                            DataTable get_bjt = new DataTable();
                                            get_bjt = Dbcon.Ora_Execute_table("select a.ubjt,a.ref_bjt_year from (select case when ISNULL("+ gt1 + ",'0.00') ='' then '0.00' else ISNULL(" + gt1 + ",'0.00') end as ubjt,ref_bjt_year from KW_Ref_Bajet where  Ref_kod_bajet='" + bajet + "' and Status='A' ) as a outer apply(select fin_year from KW_financial_Year where Status='1') as b where a.ref_bjt_year=b.fin_year ");

                                            if (get_bjt.Rows.Count != 0)
                                            {

                                                if (deb_amn != "0.00")
                                                {
                                                    if (TextBox31_jcd.Text == "05" || TextBox31_jcd.Text == "10")
                                                    {
                                                        ss1 = double.Parse(deb_amn) - double.Parse(get_bjt.Rows[0]["ubjt"].ToString());
                                                    }
                                                    else
                                                    {
                                                        ss1 = double.Parse(deb_amn) + double.Parse(get_bjt.Rows[0]["ubjt"].ToString());
                                                    }
                                                }
                                                else if (krd_amn != "0.00")
                                                {
                                                    if (TextBox31_jcd.Text == "05" || TextBox31_jcd.Text == "10")
                                                    {
                                                        ss1 = double.Parse(krd_amn) - double.Parse(get_bjt.Rows[0]["ubjt"].ToString());
                                                    }
                                                    else
                                                    {
                                                        ss1 = double.Parse(krd_amn) + double.Parse(get_bjt.Rows[0]["ubjt"].ToString());
                                                    }
                                                }
                                                string upd_bjt = "Update KW_Ref_Bajet SET Ref_used_bajet='" + ss1 + "' where Ref_kod_bajet='" + bajet + "' and ref_bjt_year='" + get_bjt.Rows[0]["ref_bjt_year"].ToString() + "' and Status='A'";
                                                Status1 = Dbcon.Ora_Execute_CommamdText(upd_bjt);
                                            }

                                        }
                                    }
                                    //}
                                }
                                i++;
                            }

                            if (Status == "SUCCESS")
                            {
                                if (ddsts.SelectedValue != "")
                                {
                                    if (TextBox31_jcd.Text == "03")
                                    {
                                        string upd_kew_lulus = "Update KW_Pembayaran_mohon SET kew_appr_gl_cd='"+ ddsts.SelectedValue + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where no_permohonan='" + txtname.Text + "'";
                                        Status = Dbcon.Ora_Execute_CommamdText(upd_kew_lulus);
                                    }
                                    else if (TextBox31_jcd.Text == "02")
                                    {
                                        string upd_kew_lulus = "Update KW_Penerimaan_resit SET gl_sts='" + ddsts.SelectedValue + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where no_resit='" + txtname.Text + "'";
                                        Status = Dbcon.Ora_Execute_CommamdText(upd_kew_lulus);
                                    }
                                    else if (TextBox31_jcd.Text == "01")
                                    {
                                        string upd_kew_lulus = "Update KW_Pembayaran_invois SET kew_appr_gl_cd='" + ddsts.SelectedValue + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where no_invois='" + txtname.Text + "'";
                                        Status = Dbcon.Ora_Execute_CommamdText(upd_kew_lulus);
                                        
                                        
                                    }
                                    else if (TextBox31_jcd.Text == "04")
                                    {
                                        string upd_kew_lulus = "Update KW_Pembayaran_Pay_voucer SET kew_appr_pv_cd='" + ddsts.SelectedValue + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where Pv_no='" + txtname.Text + "'";
                                        Status = Dbcon.Ora_Execute_CommamdText(upd_kew_lulus);
                                    }
                                    else if (TextBox31_jcd.Text == "05")
                                    {
                                        string upd_kew_lulus = "Update KW_Pembayaran_Credit SET kew_appr_gl_cd='" + ddsts.SelectedValue + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where no_Rujukan='" + txtname.Text + "'";
                                        Status = Dbcon.Ora_Execute_CommamdText(upd_kew_lulus);
                                    }
                                    else if (TextBox31_jcd.Text == "06")
                                    {
                                        string upd_kew_lulus = "Update KW_Pembayaran_Dedit SET kew_appr_gl_cd='" + ddsts.SelectedValue + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where no_Rujukan='" + txtname.Text + "'";
                                        Status = Dbcon.Ora_Execute_CommamdText(upd_kew_lulus);
                                    }
                                    else if (TextBox31_jcd.Text == "09")
                                    {
                                        string upd_kew_lulus = "Update KW_Penerimaan_invois SET kew_appr_gl_cd='" + ddsts.SelectedValue + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where no_invois='" + txtname.Text + "'";
                                        Status = Dbcon.Ora_Execute_CommamdText(upd_kew_lulus);
                                    }
                                    else if (TextBox31_jcd.Text == "10")
                                    {
                                        string upd_kew_lulus = "Update KW_Penerimaan_credit SET kew_appr_gl_cd='" + ddsts.SelectedValue + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where no_notakredit='" + txtname.Text + "'";
                                        Status = Dbcon.Ora_Execute_CommamdText(upd_kew_lulus);
                                    }
                                    else if (TextBox31_jcd.Text == "11")
                                    {
                                        string upd_kew_lulus = "Update KW_Pembayaran_Dedit SET kew_appr_gl_cd='" + ddsts.SelectedValue + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where no_notadebit='" + txtname.Text + "'";
                                        Status = Dbcon.Ora_Execute_CommamdText(upd_kew_lulus);
                                    }
                                    else
                                    {
                                        string upd_kew_lulus = "Update KW_jurnal_inter SET kew_appr_gl_cd='" + ddsts.SelectedValue + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where no_permohonan='" + txtname.Text + "'";
                                        Status = Dbcon.Ora_Execute_CommamdText(upd_kew_lulus);
                                    }
                                }

                                DataTable dt_upd_format = new DataTable();
                                dt_upd_format = Dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + txtnoper_1.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='23' and Status = 'A'");
                                BindMohon();

                                Div2.Visible = false;

                                Div3.Visible = true;

                                reset();

                                tab1();

                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Post to GL Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Debit and Kredit Value Not Match.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Kod Akaun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else

                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Jenis Jurnal.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

                }
            }

            else

            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Status Lulus.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

            }
            
        }

        else

        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Permohonan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }

    }



    void reset()

    {

        //tab1
        TextBox31_jcd.Text = "";
        txtnoper_1.Text = "";
        txtnoper.Text = "";

        //mb_tab1.Visible = false;

        DropDownList1.SelectedValue = "";

        DropDownList2.SelectedValue = "";

        DropDownList3.SelectedValue = "";

        ddsts.SelectedValue = "";

        dd_terma.Text = "";

        Button19.Visible = false;

        //mak_dok_shw.Visible = false;

        ddakaun.SelectedIndex = 0;

        ddakaun.Attributes.Remove("disabled");





        txttarkihmo.Text = DateTime.Now.ToString("yyyy-MM-dd");

        //txttarkihmo.Attributes.Remove("disabled");



        ddbkepada.SelectedIndex = 0;

        ddbkepada.Attributes.Remove("disabled");



        txticno.Text = "";

        txticno.Attributes.Remove("disabled");



        txtname.Text = "";

        txtname.Attributes.Remove("disabled");



        txtbname.Text = "";

        txtbname.Attributes.Remove("disabled");



        txtbno.Text = "";

        txtbno.Attributes.Remove("disabled");

        TextBox7.Text = "";
        TextBox17.Text = "";
        TextBox19.Text = "";
        TextBox22.Text = "";
        TextBox23.Text = "";
        TextBox24.Text = "";
        TextBox27.Text = "";
        TextBox29.Value = "";
        txtname.Text = "";
        TextBox26.Text = "";
        txttarkihmo.Text = "";
        dd_terma.Text = "";
        jurnal_txt.Value = "";
        ddsts.SelectedValue = "";
        DropDownList5.SelectedValue = "";


        //txtterma.Text = "";

        //txtterma.Attributes.Remove("disabled");



        txtjenis.Text = "";

        txtjenis.Attributes.Remove("disabled");









        ddstatus.SelectedIndex = 0;

        ddstatus.Attributes.Remove("disabled");



        //txtcatatan.Text = "";

        //txtcatatan.Attributes.Remove("disabled");



        btnprintmoh.Visible = false;



        TextBox17.Text = "";

        Session["dbtno"] = "";

        jurnal_show.Visible = false;

        jurnal_show2.Visible = false;

        TextBox2.Text = "";

        SetInitialRowmoh();

        Session["permohon_no"] = "";

        //tab2
        Session["pvno"] = "";

        ddakaun.SelectedIndex = 0;

        ddakaun.Attributes.Remove("disabled");

        ddakaun.SelectedIndex = 0;

        ddakaun.Attributes.Remove("disabled");
        
        ddakaun.SelectedIndex = 0;

        ddakaun.Attributes.Remove("disabled");        
        
        ddakaun.SelectedIndex = 0;

        ddakaun.Attributes.Remove("disabled");    
       
    }


    
    protected void load_bajet(object sender, EventArgs e)

    {

        lod_bajet();

    }

    void lod_bajet()

    {

        //DataTable dt1 = new DataTable();

        //dt1 = Dbcon.Ora_Execute_table("SELECT nama_bajet, Ref_jumlah_bajet,isnull(cast(Ref_used_bajet as money),'0.00') Ref_used_bajet, sum(isnull(cast(Ref_jumlah_bajet as money),'0.00') - isnull(cast(Ref_used_bajet as money),'0.00')) as baki FROM KW_Ref_kod_bajet as table1 LEFT JOIN  KW_Ref_Bajet as table2 ON table1. kod_bajet = table2.Ref_kod_bajet WHERE kod_bajet='" + DropDownList3.SelectedValue + "' and '" + DateTime.Now.ToString("yyyy-MM-dd") + "' BETWEEN Ref_tk_mula AND Ref_tk_akhir group by nama_bajet, Ref_jumlah_bajet, Ref_used_bajet");

        //Gridview1.DataSource = dt1;

        //Gridview1.DataBind();

    }

    protected void lblSubbind_Click(object sender, EventArgs e)

    {

        
        LinkButton btn = (LinkButton)sender;

        string[] CommadArgument = btn.CommandArgument.Split(',');

        CommandArgument1 = CommadArgument[0];

        Session["jurnal_cmd1"] = CommadArgument[0];

        Session["jurnal_cmd2"] = CommadArgument[1];

        if(CommadArgument[1] == "02" || CommadArgument[1] == "04")
        {
            shw_03.Visible = true;
            shw_02.Visible = false;
            shw_01.Visible = true;
        }
        else
        {
            shw_02.Visible = true;
            shw_03.Visible = false;
            shw_01.Visible = false;
        }

        grd_details1();

    }



    void grd_details1()

    {        

        //kem_Button2.Visible = true;

        //gridmohdup.Enabled = false;

        mb_tab1.Visible = true;

        sts.Visible = true;

        Div2.Visible = true;

        Div3.Visible = false;

        DataTable dt1 = new DataTable();

        DataTable dt = new DataTable();



        if (Session["jurnal_cmd2"].ToString() == "09")

        {

            dt = Dbcon.Ora_Execute_table("select '' cara_bayaran,no_invois  as no_ruj,Format(tarikh_invois,'dd/MM/yyyy') as tm,no_invois as no_inv,Format(tarikh_invois, 'dd/MM/yyyy') as tinv, b.pembekal as pem, project_kod proj, Terma terma, Overall jumlah, perkera perkara,Format(lulus_dt, 'dd/MM/yyyy') lul_kew, kel_nota, s1.Ref_doc_name as jen_desc,'' nama_bank,'' no_cek,'' Bank_Name,'' tk_cek,'09' dc_cd from KW_Penerimaan_invois m1 left   join KW_Ref_Doc_kod s1 on s1.Ref_doc_code = '09' OUTER APPLY (select top(1) case when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s5.stf_name, '') when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s5.stf_name, '') = '') then ISNULL(s4.Ref_nama_syarikat, '') when(ISNULL(s5.stf_name, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s3.Ref_nama_syarikat, '') else '' end as pembekal from KW_Penerimaan_invois_item s2  left  join KW_Ref_Pembekal s3 on s3.Ref_no_syarikat = m1.nama_pelanggan_code left join KW_Ref_Pelanggan s4 on s4.Ref_no_syarikat = m1.nama_pelanggan_code left  join hr_staff_profile s5 on s5.stf_staff_no = m1.nama_pelanggan_code where  s2.Status = 'A' and s2.no_invois = m1.no_invois order by s2.cr_dt asc) as b where no_invois = '" + Session["jurnal_cmd1"].ToString() + "' and m1.Status = 'A'");
        }

        else if (Session["jurnal_cmd2"].ToString() == "02")

        {

            dt = Dbcon.Ora_Execute_table("select '' cara_bayaran,no_resit as no_ruj, Format(tarikh_resit, 'dd/MM/yyyy') as tm, no_invois as no_inv, '' as tinv, b.pembekal as pem, project_kod proj, '' terma, Overall jumlah, perkara,'' lul_kew, '' kel_nota, s1.Ref_doc_name as jen_desc,s10.Ref_nama_bank nama_bank,no_cek,s11.Bank_Name,Format(tarikh_cek,'dd/MM/yyyy') tk_cek,'02' dc_cd from KW_Penerimaan_resit m1 Left join Ref_Nama_Bank s11 on s11.Bank_Code=bank_cod left join KW_Ref_Akaun_bank s10 on s10.Ref_kod_akaun=gl_cd_bank  left  join KW_Ref_Doc_kod s1 on s1.Ref_doc_code = '02' OUTER APPLY (select top(1) case when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s5.stf_name, '') when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s5.stf_name, '') = '') then ISNULL(s4.Ref_nama_syarikat, '') when(ISNULL(s5.stf_name, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s3.Ref_nama_syarikat, '') else '' end as pembekal from KW_Penerimaan_resit_item s2  left  join KW_Ref_Pembekal s3 on s3.Ref_no_syarikat = m1.nama_pelanggan_code left  join KW_Ref_Pelanggan s4 on s4.Ref_no_syarikat = m1.nama_pelanggan_code left  join hr_staff_profile s5 on s5.stf_staff_no = m1.nama_pelanggan_code where  s2.Status = 'A' and s2.no_resit = m1.no_resit order by s2.cr_dt asc) as b where no_resit = '" + Session["jurnal_cmd1"].ToString() + "' and m1.Status = 'A'");



        }

        else if (Session["jurnal_cmd2"].ToString() == "03")

        {

            dt = Dbcon.Ora_Execute_table("select cara_bayaran,no_permohonan  as no_ruj,Format(tarkih_permohonan,'dd/MM/yyyy') as tm,no_ruj as no_inv,Format(tarkih_invois,'dd/MM/yyyy') as tinv,b.pembekal as pem,b.Ref_Projek_name proj,Terma terma,jumlah,perkara,Format(lulus_dt,'dd/MM/yyyy') lul_kew,kel_nota,s1.Ref_doc_name as jen_desc,'' nama_bank,'' no_cek,'' Bank_Name,'' tk_cek,'03' dc_cd from KW_Pembayaran_mohon m1 left join KW_Ref_Doc_kod s1 on s1.Ref_doc_code='03' OUTER APPLY (select top(1) s6.Ref_Projek_name,case when (ISNULL(s3.Ref_nama_syarikat,'') ='' and ISNULL(s4.Ref_nama_syarikat,'') ='') then ISNULL(s5.stf_name,'') when (ISNULL(s3.Ref_nama_syarikat,'') ='' and ISNULL(s5.stf_name,'') ='') then ISNULL(s4.Ref_nama_syarikat,'') when (ISNULL(s5.stf_name,'') ='' and ISNULL(s4.Ref_nama_syarikat,'') ='') then ISNULL(s3.Ref_nama_syarikat,'') else '' end as pembekal from KW_Pembayaran_mohon_item s2 left join KW_Ref_Pembekal s3 on s3.Ref_no_syarikat=s2.mhn_byr_kepada left join KW_Ref_Pelanggan s4 on s4.Ref_no_syarikat=s2.mhn_byr_kepada left join hr_staff_profile s5 on s5.stf_staff_no=s2.mhn_byr_kepada left join KW_Ref_Projek s6 on s6.Ref_Projek_code=s2.mhn_projek_cd where s2.Status='A' and s2.mhn_no_permohonan=m1.no_permohonan order by s2.cr_dt asc) as b where no_permohonan='" + Session["jurnal_cmd1"].ToString() + "' and m1.Status='A'");
            
        }

        else if (Session["jurnal_cmd2"].ToString() == "05")

        {

            dt = Dbcon.Ora_Execute_table("select '' cara_bayaran,no_Rujukan as no_ruj, Format(tarkih_mohon, 'dd/MM/yyyy') as tm, no_invois as no_inv,Format(tarikh_kredit, 'dd/MM/yyyy') as tinv, b.pembekal as pem, project_kod proj, Terma terma, Overall jumlah, perkera perkara, Format(lulus_dt, 'dd/MM/yyyy') lul_kew, kel_nota, s1.Ref_doc_name as jen_desc,'' nama_bank,'' no_cek,'' Bank_Name,''tk_cek,'05' dc_cd  from KW_Pembayaran_Credit m1 left join KW_Ref_Doc_kod s1 on s1.Ref_doc_code='05' OUTER APPLY (select top(1) case when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s5.stf_name, '') when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s5.stf_name, '') = '') then ISNULL(s4.Ref_nama_syarikat, '') when(ISNULL(s5.stf_name, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s3.Ref_nama_syarikat, '') else '' end as pembekal from KW_Pembayaran_Credit_item s2 left join KW_Ref_Pembekal s3 on s3.Ref_no_syarikat=m1.nama_pembekal_code left join KW_Ref_Pelanggan s4 on s4.Ref_no_syarikat = m1.nama_pembekal_code left join hr_staff_profile s5 on s5.stf_staff_no=m1.nama_pembekal_code where s2.Status = 'A' and s2.no_Rujukan = m1.no_Rujukan order by s2.cr_dt asc) as b where no_Rujukan = '" + Session["jurnal_cmd1"].ToString() + "' and m1.Status = 'A'");

            
        }

        else if (Session["jurnal_cmd2"].ToString() == "06")

        {

            dt = Dbcon.Ora_Execute_table("select '' cara_bayaran,no_Rujukan as no_ruj, Format(tarkih_mohon, 'dd/MM/yyyy') as tm, no_invois as no_inv,Format(tarikh_debit, 'dd/MM/yyyy') as tinv, b.pembekal as pem, project_kod proj, Terma terma, Overall jumlah, perkera perkara, Format(lulus_dt, 'dd/MM/yyyy') lul_kew, kel_nota, s1.Ref_doc_name as jen_desc,'' nama_bank,'' no_cek,'' Bank_Name,'' tk_cek,'06' dc_cd from KW_Pembayaran_Dedit m1 left join KW_Ref_Doc_kod s1 on s1.Ref_doc_code='06' OUTER APPLY (select top(1) case when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s5.stf_name, '') when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s5.stf_name, '') = '') then ISNULL(s4.Ref_nama_syarikat, '') when(ISNULL(s5.stf_name, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s3.Ref_nama_syarikat, '') else '' end as pembekal from KW_Pembayaran_Dedit_item s2 left join KW_Ref_Pembekal s3 on s3.Ref_no_syarikat=m1.nama_pembekal_code left join KW_Ref_Pelanggan s4 on s4.Ref_no_syarikat = m1.nama_pembekal_code left join hr_staff_profile s5 on s5.stf_staff_no=m1.nama_pembekal_code where s2.Status = 'A' and s2.no_Rujukan = m1.no_Rujukan order by s2.cr_dt asc) as b where no_Rujukan = '" + Session["jurnal_cmd1"].ToString() + "' and m1.Status = 'A'");
            
        }

        else if (Session["jurnal_cmd2"].ToString() == "01")

        {
            dt = Dbcon.Ora_Execute_table("select cara_bayaran,no_invois as no_ruj, Format(tarkih_mohon, 'dd/MM/yyyy') as tm, no_invois as no_inv,Format(tarikh_invois, 'dd/MM/yyyy') as tinv, b.pembekal as pem, project_kod proj, Terma terma, Overall jumlah, perkara,Format(lulus_dt, 'dd/MM/yyyy') lul_kew, kel_nota, s1.Ref_doc_name as jen_desc,'' nama_bank,'' no_cek,'' Bank_Name,''  tk_cek,'01' dc_cd from KW_Pembayaran_invois m1 left join KW_Ref_Doc_kod s1 on s1.Ref_doc_code='01' OUTER APPLY (select top(1) case when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s5.stf_name, '') when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s5.stf_name, '') = '') then ISNULL(s4.Ref_nama_syarikat, '') when(ISNULL(s5.stf_name, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s3.Ref_nama_syarikat, '') else '' end as pembekal from KW_Pembayaran_invoisBil_item s2 left join KW_Ref_Pembekal s3 on s3.Ref_no_syarikat=m1.Bayar_kepada left join KW_Ref_Pelanggan s4 on s4.Ref_no_syarikat = m1.Bayar_kepada left join hr_staff_profile s5 on s5.stf_staff_no=m1.Bayar_kepada where s2.Status = 'A' and s2.no_invois = m1.no_invois order by s2.cr_dt asc) as b where no_invois = '" + Session["jurnal_cmd1"].ToString() + "' and m1.Status = 'A'");
            
        }

        else if (Session["jurnal_cmd2"].ToString() == "10")

        {

            dt = Dbcon.Ora_Execute_table("select '' cara_bayaran,no_notakredit as no_ruj, Format(tarikh_kredit, 'dd/MM/yyyy') as tm, no_invois as no_inv, '' as tinv, b.pembekal as pem, project_kod proj, Terma terma, Overall jumlah, perkara,Format(lulus_dt, 'dd/MM/yyyy') lul_kew, kel_nota, s1.Ref_doc_name as jen_desc,'' nama_bank,'' no_cek,'' Bank_Name,'' tk_cek,'10' dc_cd from KW_Penerimaan_Credit m1   left join KW_Ref_Doc_kod s1 on s1.Ref_doc_code = '10' OUTER APPLY(select top(1) case when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s5.stf_name, '') when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s5.stf_name, '') = '') then ISNULL(s4.Ref_nama_syarikat, '') when(ISNULL(s5.stf_name, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s3.Ref_nama_syarikat, '') else '' end as pembekal from KW_Penerimaan_Credit_item s2  left  join KW_Ref_Pembekal s3 on s3.Ref_no_syarikat = m1.nama_pelanggan_code left  join KW_Ref_Pelanggan s4 on s4.Ref_no_syarikat = m1.nama_pelanggan_code left  join hr_staff_profile s5 on s5.stf_staff_no = m1.nama_pelanggan_code where  s2.Status = 'A' and s2.no_notakredit = m1.no_notakredit order by s2.cr_dt asc) as b where no_notakredit = '" + Session["jurnal_cmd1"].ToString() + "' and m1.Status = 'A'");



        }

        else if (Session["jurnal_cmd2"].ToString() == "11")

        {

            dt = Dbcon.Ora_Execute_table("select '' cara_bayaran,no_notadebit as no_ruj, Format(tarikh_debit, 'dd/MM/yyyy') as tm, no_invois as no_inv, '' as tinv, b.pembekal as pem, project_kod proj, Terma terma, Overall jumlah, perkara,Format(lulus_dt, 'dd/MM/yyyy') lul_kew, kel_nota, s1.Ref_doc_name as jen_desc,'' nama_bank,'' no_cek,'' Bank_Name,'' tk_cek,'11' dc_cd from KW_Penerimaan_Debit m1 left  join KW_Ref_Doc_kod s1 on s1.Ref_doc_code = '11' OUTER APPLY (select top(1) case when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s5.stf_name, '') when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s5.stf_name, '') = '') then ISNULL(s4.Ref_nama_syarikat, '') when(ISNULL(s5.stf_name, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s3.Ref_nama_syarikat, '') else '' end as pembekal from KW_Penerimaan_Debit_item s2  left join KW_Ref_Pembekal s3 on s3.Ref_no_syarikat = m1.nama_pelanggan_code left  join KW_Ref_Pelanggan s4 on s4.Ref_no_syarikat = m1.nama_pelanggan_code left  join hr_staff_profile s5 on s5.stf_staff_no = m1.nama_pelanggan_code where  s2.Status = 'A' and s2.no_notadebit = m1.no_notadebit order by s2.cr_dt asc) as b where no_notadebit = '" + Session["jurnal_cmd1"].ToString() + "' and m1.Status = 'A'");
            
        }

        else if (Session["jurnal_cmd2"].ToString() == "04")

        {

            dt = Dbcon.Ora_Execute_table("select '' cara_bayaran,Pv_no as no_ruj, Format(tarkih_pv, 'dd/MM/yyyy') as tm, no_invois as no_inv, Format(tarkih_pv, 'dd/MM/yyyy') as tinv, m1.Bayar_kepada as pem, '' proj, Terma terma, Overall jumlah, perkara,'' lul_kew,'' kel_nota, s1.Ref_doc_name as jen_desc,s10.Ref_nama_bank nama_bank,m1.No_cek no_cek,'' Bank_Name,Format(tarikh_cek, 'dd/MM/yyyy') tk_cek,'04' dc_cd from KW_Pembayaran_Pay_voucer m1 left join KW_Ref_Akaun_bank s10 on s10.Ref_kod_akaun=gl_cd_bank  left join KW_Ref_Doc_kod s1 on s1.Ref_doc_code = '04' OUTER APPLY(select top(1) case when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s5.stf_name, '') when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s5.stf_name, '') = '') then ISNULL(s4.Ref_nama_syarikat, '') when(ISNULL(s5.stf_name, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s3.Ref_nama_syarikat, '') else '' end as pembekal from KW_Pembayaran_Pay_voucer s2  left  join KW_Ref_Pembekal s3 on s3.Ref_no_syarikat = s2.Bayar_kepada left join KW_Ref_Pelanggan s4 on s4.Ref_no_syarikat = s2.Bayar_kepada left  join hr_staff_profile s5 on s5.stf_staff_no = s2.Bayar_kepada where  s2.lulus_sts = 'L' and s2.Pv_no = m1.Pv_no order by s2.cr_dt asc) as b where Pv_no = '" + Session["jurnal_cmd1"].ToString() + "' and m1.lulus_sts = 'L'");

        }

        else

        {

            dt = Dbcon.Ora_Execute_table("select '' cara_bayaran,no_permohonan as no_ruj, Format(tarikh_lulus, 'dd/MM/yyyy') as tm, no_Rujukan as no_inv, Format(tarikh_lulus, 'dd/MM/yyyy') as tinv, b.pembekal as pem, '' proj, Terma terma, Overall jumlah, perkara,Format(lulus_dt, 'dd/MM/yyyy') lul_kew, kel_nota, s1.Ref_doc_name as jen_desc,'' nama_bank,'' no_cek,'' Bank_Name,'' tk_cek,'' dc_cd from KW_jurnal_inter m1   left join KW_Ref_Doc_kod s1 on s1.Ref_doc_code = Jenis_permohonan OUTER APPLY(select top(1) case when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s5.stf_name, '') when(ISNULL(s3.Ref_nama_syarikat, '') = '' and ISNULL(s5.stf_name, '') = '') then ISNULL(s4.Ref_nama_syarikat, '') when(ISNULL(s5.stf_name, '') = '' and ISNULL(s4.Ref_nama_syarikat, '') = '') then ISNULL(s3.Ref_nama_syarikat, '') else '' end as pembekal from KW_jurnal_inter_items s2  left  join KW_Ref_Pembekal s3 on s3.Ref_no_syarikat = m1.nama_pelanggan_code left  join KW_Ref_Pelanggan s4 on s4.Ref_no_syarikat = m1.nama_pelanggan_code left  join hr_staff_profile s5 on s5.stf_staff_no = m1.nama_pelanggan_code where  s2.Status = 'A' and s2.no_permohonan = m1.no_permohonan order by s2.cr_dt asc) as b where no_permohonan = '" + Session["jurnal_cmd1"].ToString() + "' and m1.Status = 'A'");



        }

        //tab1();

        if (dt.Rows.Count != 0)

        {

         

            txtnoper.Text = dt.Rows[0]["jen_desc"].ToString();
            TextBox31_jcd.Text = dt.Rows[0]["dc_cd"].ToString();

            TextBox16.Text = Session["jurnal_cmd2"].ToString();           

            kem_Button2.Visible = true;

            TextBox26.Text = dt.Rows[0]["pem"].ToString();

            TextBox22.Text = dt.Rows[0]["proj"].ToString();

            TextBox19.Text = dt.Rows[0]["no_inv"].ToString();

            TextBox24.Text = dt.Rows[0]["tinv"].ToString();

            txttarkihmo.Text = dt.Rows[0]["tm"].ToString();

            TextBox29.Value = dt.Rows[0]["perkara"].ToString();

            TextBox30.Text = dt.Rows[0]["nama_bank"].ToString();

            TextBox7.Text = dt.Rows[0]["lul_kew"].ToString();

            TextBox27.Text = dt.Rows[0]["kel_nota"].ToString();

            txtname.Text = dt.Rows[0]["no_ruj"].ToString();

            dd_terma.Text = dt.Rows[0]["terma"].ToString();

            TextBox14.Text = dt.Rows[0]["no_cek"].ToString();

            DropDownList2.SelectedValue = dt.Rows[0]["cara_bayaran"].ToString();

            TextBox28.Text = dt.Rows[0]["Bank_Name"].ToString();
            if (dt.Rows[0]["tk_cek"].ToString() != "01/01/1900")
            {
                TextBox9.Text = dt.Rows[0]["tk_cek"].ToString();
            }

            TextBox17.Text = double.Parse(dt.Rows[0]["jumlah"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");


            DataTable chk_ledger = new DataTable();
            chk_ledger = DBCon.Ora_Execute_table("select GL_invois_no,GL_sts,GL_post_note,GL_type,GL_journal_no,Gl_jenis_no from KW_General_Ledger where Gl_jenis_no='" + dt.Rows[0]["no_ruj"].ToString() + "' group by GL_invois_no,GL_sts,GL_post_note,GL_type,GL_journal_no,Gl_jenis_no");

            if(chk_ledger.Rows.Count != 0)
            {
                TextBox23.Text = chk_ledger.Rows[0]["GL_journal_no"].ToString();
                ddsts.SelectedValue = chk_ledger.Rows[0]["GL_sts"].ToString();
                DropDownList5.SelectedValue = chk_ledger.Rows[0]["GL_type"].ToString();
                jurnal_txt.Value = chk_ledger.Rows[0]["GL_post_note"].ToString();
                kem_Button2.Attributes.Add("style","pointer-events:None; Opacity :0.5;");
                Gridview1_shw.Visible = true;
                gridmohdup.Visible = false;

                dt1 = Dbcon.Ora_Execute_table("select kod_akaun,kod_bajet,GL_desc1, case when KW_Debit_amt ='0.00' then 'KREDIT' when KW_kredit_amt ='0.00' then 'DEBIT' end as gl_txi,KW_Debit_amt as Amaun_deb,KW_kredit_amt as Amaun_kre from KW_General_Ledger where Gl_jenis_no='" + dt.Rows[0]["no_ruj"].ToString() + "'");

                Gridview1_shw.DataSource = dt1;

                Gridview1_shw.DataBind();

            }
            else
            {
                GetMohon();
                kem_Button2.Attributes.Remove("style");
                Gridview1_shw.Visible = false;
                gridmohdup.Visible = true;
            }
            string gt_pval1 = string.Empty;
           
            gt_pval1 = dt.Rows[0]["no_ruj"].ToString();
            //}

            if (dt.Rows[0]["dc_cd"].ToString() == "01")

            {
                query1 = "select * from (select '1' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,'' kd_akaun,'' kd_bjt, perkara ket, 'K' jen_txi,mhn_amt as jum_kre,'0.00' as jum_deb from KW_Pembayaran_invois a OUTER APPLY (select sum(unit) mhn_amt from KW_Pembayaran_invoisBil_item s2 where s2.Status='A' and s2.no_invois=a.no_invois) as b where a.no_invois='" + gt_pval1 + "'"

                             + " union all select '2' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end jen_txi,case when s1.tc_jenis='PEM' then discount else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then discount else '0.00' end as jum_kre from KW_Pembayaran_invoisBil_item left join KW_Ref_Diskaun s1 on s1.tc_jenis='PEM' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_invois='" + gt_pval1 + "' "

                             + " union all select '3' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, 'K' jen_txi,gstjumlah as jum_kre,'0.00' as jum_deb from KW_Pembayaran_invoisBil_item left join KW_Ref_Tetapan_cukai s1 on s1.Ref_kod_cukai=tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.tc_jenis='PEM' and s2.kod_akaun=s1.Ref_kod_akaun where no_invois='" + gt_pval1 + "' "

                             + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')"

                            + "union all select * from (select '4' sno1,'1' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,(select top(1)* from(select kod_akaun from KW_General_Ledger where GL_invois_no=a.no_invois and ref2='" + dt.Rows[0]["dc_cd"].ToString() + "' group by kod_akaun except select * from (select Ref_kod_akaun from KW_Ref_Pembekal group by Ref_kod_akaun	union all select Ref_kod_akaun from kw_ref_tetapan_cukai group by Ref_kod_akaun	union all select Ref_kod_akaun from KW_Ref_Diskaun group by Ref_kod_akaun ) as a1) as a order by a.kod_akaun desc) kd_akaun,'' kd_bjt, perkara ket, 'D' jen_txi,'0.00' as jum_kre,mhn_amt as jum_deb from KW_Pembayaran_invois a OUTER APPLY (select sum(unit) mhn_amt from KW_Pembayaran_invoisBil_item s2 where s2.Status='A' and s2.no_invois=a.no_invois) as b where a.no_invois='" + gt_pval1 + "'"

                           + " union all select '5' sno1,'2' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end as jen_txi,case when s1.tc_jenis='PEM' then discount else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then discount else '0.00' end as jum_kre from KW_Pembayaran_invoisBil_item left join KW_Ref_Diskaun s1 on s1.tc_jenis='PEM' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_invois='" + gt_pval1 + "' "

                           + " union all select '6' sno1,'3' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end as jen_txi,case when s1.tc_jenis='PEM' then gstjumlah else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then gstjumlah else '0.00' end as jum_kre from KW_Pembayaran_invoisBil_item left join KW_Ref_Tetapan_cukai s1 on s1.tc_jenis='PEM' and s1.Ref_kod_cukai=tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_invois='" + gt_pval1 + "' "

                           + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')";


            }

            else if (dt.Rows[0]["dc_cd"].ToString() == "02")

            {
                query1 = "select * from (select '1' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,'' kd_akaun,'' kd_bjt, perkara ket, 'K' jen_txi,'0.00' as jum_deb,mhn_amt as jum_kre from KW_Penerimaan_resit a OUTER APPLY (select sum(res_unit) mhn_amt from KW_Penerimaan_resit_item s2 where s2.Status='A' and s2.no_resit=a.no_resit) as b where a.no_resit='" + gt_pval1 + "'"

                           + " union all select '2' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end jen_txi,case when s1.tc_jenis='PEM' then res_disk else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then res_disk else '0.00' end as jum_kre from KW_Penerimaan_resit_item left join KW_Ref_Diskaun s1 on s1.Id='1'  and s1.tc_jenis='PEM' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_resit='" + gt_pval1 + "' "

                           + " union all select '3' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, 'K' jen_txi,'0.00' as jum_deb,gstjumlah as jum_kre from KW_Penerimaan_resit_item left join KW_Ref_Tetapan_cukai s1 on s1.Ref_kod_cukai=tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_resit='" + gt_pval1 + "' "

                           + " union all select '4' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,gl_cd_bank kd_akaun,'' kd_bjt, s2.Ref_nama_bank ket, '' jen_txi,Overall as jum_kre,'0.00' as jum_deb from KW_Penerimaan_resit left join KW_Ref_Akaun_bank s2 on s2.Ref_kod_akaun=gl_cd_bank where no_resit='" + gt_pval1 + "' ) as a where (jum_deb != '0.00' or jum_kre != '0.00')";

                           // + "union all select * from (select '5' sno1,'1' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,'' kd_akaun,'' kd_bjt, perkara ket, '' jen_txi,mhn_amt as jum_deb,'0.00' as jum_kre from KW_Penerimaan_resit a OUTER APPLY (select sum(res_unit) mhn_amt from KW_Penerimaan_resit_item s2 where s2.Status='A' and s2.no_resit=a.no_resit) as b where a.no_resit='" + gt_pval1 + "'"

                           //+ " union all select '6' sno1,'2' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end as jen_txi,case when s1.tc_jenis='PEM' then res_disk else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then res_disk else '0.00' end as jum_kre from KW_Penerimaan_resit_item left join KW_Ref_Diskaun s1 on s1.Id='2'  and s1.tc_jenis='PEN' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_resit='" + gt_pval1 + "' "

                           //+ " union all select '7' sno1,'3' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end as jen_txi,case when s1.tc_jenis='PEM' then gstjumlah else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then gstjumlah else '0.00' end as jum_kre from KW_Penerimaan_resit_item left join KW_Ref_Tetapan_cukai s1 on s1.Ref_kod_cukai=tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_resit='" + gt_pval1 + "' "

                           //+ " union all select '8' sno1,'5' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,gl_cd_bank kd_akaun,'' kd_bjt, s2.Ref_nama_bank ket, '' jen_txi,'0.00' as jum_deb,Overall as jum_kre from KW_Penerimaan_resit left join KW_Ref_Akaun_bank s2 on s2.Ref_kod_akaun=gl_cd_bank where no_resit='" + gt_pval1 + "' ) as a where (jum_deb != '0.00' or jum_kre != '0.00')";

            }

            else if (dt.Rows[0]["dc_cd"].ToString() == "03")

            {

                query1 = "select * from (select '1' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,'' kd_akaun,'' kd_bjt, perkara ket, 'K' jen_txi,'0.00' as jum_deb,mhn_amt as jum_kre from KW_Pembayaran_mohon a OUTER APPLY (select sum(mhn_amount) mhn_amt from KW_Pembayaran_mohon_item s2 where s2.Status='A' and s2.mhn_no_permohonan=a.no_permohonan) as b where no_permohonan='" + gt_pval1 + "' "

                            + " union all select '2' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end jen_txi,case when s1.tc_jenis='PEM' then mhn_disc else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then mhn_disc else '0.00' end as jum_kre from KW_Pembayaran_mohon_item left join KW_Ref_Diskaun s1 on s1.Id='1'  and s1.tc_jenis='PEM' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where mhn_no_permohonan='" + gt_pval1 + "' "

                            + " union all select '3' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, 'K' jen_txi,'0.00' as jum_deb,mhn_tax as jum_kre from KW_Pembayaran_mohon_item left join KW_Ref_Tetapan_cukai s1 on s1.Ref_kod_cukai=mhn_jns_tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where mhn_no_permohonan='" + gt_pval1 + "' "

                            + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')"

                            + " union all select * from (select '4' sno1,'1' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,(select top(1)* from(select kod_akaun from KW_General_Ledger where GL_invois_no=a.no_ruj and ref2='" + dt.Rows[0]["dc_cd"].ToString() + "' group by kod_akaun except select * from (select Ref_kod_akaun from KW_Ref_Pembekal group by Ref_kod_akaun	union all select Ref_kod_akaun from kw_ref_tetapan_cukai group by Ref_kod_akaun	union all select Ref_kod_akaun from KW_Ref_Diskaun group by Ref_kod_akaun ) as a1) as a order by a.kod_akaun desc) kd_akaun,'' kd_bjt, perkara ket, 'D' jen_txi,mhn_amt as jum_deb,'0.00' as jum_kre from KW_Pembayaran_mohon a OUTER APPLY (select sum(mhn_amount) mhn_amt from KW_Pembayaran_mohon_item s2 where s2.Status='A' and s2.mhn_no_permohonan=a.no_permohonan) as b where no_permohonan='" + gt_pval1 + "' "

                            + " union all select '5' sno1,'2' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end as jen_txi,case when s1.tc_jenis='PEM' then mhn_disc else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then mhn_disc else '0.00' end as jum_kre from KW_Pembayaran_mohon_item left join KW_Ref_Diskaun s1 on s1.Id='2'  and s1.tc_jenis='PEN' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where mhn_no_permohonan='" + gt_pval1 + "' "

                            + " union all select '6' sno1,'3' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end as jen_txi,case when s1.tc_jenis='PEM' then mhn_tax else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then mhn_tax else '0.00' end as jum_kre from KW_Pembayaran_mohon_item left join KW_Ref_Tetapan_cukai s1 on s1.Ref_kod_cukai=mhn_jns_tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where mhn_no_permohonan='" + gt_pval1 + "' "

                            + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')";

            }
            else if (dt.Rows[0]["dc_cd"].ToString() == "04")

            {

                query1 = "select * from (select '1' sno1,'0' as sno,'03' as jen_typ,'' kd_akaun,'' kd_bjt, ('TERIMAAN '+ perkara) ket, 'D' jen_txi,Overall as jum_deb,'0.00' as jum_kre from KW_Pembayaran_Pay_voucer where Pv_no='" + gt_pval1 + "'"

                            + " union all select '2' sno1,'1' as sno,'03' as jen_typ,gl_cd_bank kd_akaun,'' kd_bjt, ('PEMBAYARAN '+ perkara) ket, 'K' jen_txi,'0.00' as jum_deb,Overall as jum_kre from KW_Pembayaran_Pay_voucer where Pv_no='" + gt_pval1 + "'"

                           + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')";

            }

            else if (dt.Rows[0]["dc_cd"].ToString() == "05")

            {
                query1 = "select * from (select '1' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,'' kd_akaun,'' kd_bjt, perkera ket, 'D' jen_txi,mhn_amt as jum_deb,'0.00' as jum_kre from KW_Pembayaran_Credit a OUTER APPLY (select sum(jumlah) mhn_amt from KW_Pembayaran_Credit_item s2 where s2.Status='A' and s2.no_Rujukan=a.no_Rujukan) as b where no_Rujukan='" + gt_pval1 + "'"

                           + " union all select'2' sno1, '0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end jen_txi,case when s1.tc_jenis='PEM' then discount else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then discount else '0.00' end as jum_kre from KW_Pembayaran_Credit_item left join KW_Ref_Diskaun s1 on s1.Id='1'  and s1.tc_jenis='PEM' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_Rujukan='" + gt_pval1 + "' "

                           + " union all select '3' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, 'K' jen_txi,'0.00' as jum_deb,gstjumlah as jum_kre from KW_Pembayaran_Credit_item left join KW_Ref_Tetapan_cukai s1 on s1.Ref_kod_cukai=tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_Rujukan='" + gt_pval1 + "' "

                           + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')"

                            + "union all select * from (select '4' sno1,'1' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,(select top(1)* from(select kod_akaun from KW_General_Ledger where GL_invois_no=a.no_invois and ref2='" + dt.Rows[0]["dc_cd"].ToString() + "' group by kod_akaun except select * from (select Ref_kod_akaun from KW_Ref_Pembekal group by Ref_kod_akaun	union all select Ref_kod_akaun from kw_ref_tetapan_cukai group by Ref_kod_akaun	union all select Ref_kod_akaun from KW_Ref_Diskaun group by Ref_kod_akaun ) as a1) as a order by a.kod_akaun desc) kd_akaun,'' kd_bjt, perkera ket, '' jen_txi,'0.00' as jum_deb,mhn_amt as jum_kre from KW_Pembayaran_Credit a left join  KW_Pembayaran_Credit_item a1 on a1.no_Rujukan=a.no_Rujukan OUTER APPLY (select sum(jumlah) mhn_amt from KW_Pembayaran_Credit_item s2 where s2.Status='A' and s2.no_Rujukan=a.no_Rujukan) as b where a.no_Rujukan='" + gt_pval1 + "'"

                            + " union all select '5' sno1,'2' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end as jen_txi,case when s1.tc_jenis='PEM' then discount else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then discount else '0.00' end as jum_kre from KW_Pembayaran_Credit_item left join KW_Ref_Diskaun s1 on s1.Id='2'  and s1.tc_jenis='PEN'  and s1.tc_jenis='PEM' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_Rujukan='" + gt_pval1 + "' "

                            + " union all select '6' sno1,'3' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end as jen_txi,case when s1.tc_jenis='PEM' then gstjumlah else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then gstjumlah else '0.00' end as jum_kre from KW_Pembayaran_Credit_item left join KW_Ref_Tetapan_cukai s1 on s1.Ref_kod_cukai=tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_Rujukan='" + gt_pval1 + "' "

                            + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')";

            }

            else if (dt.Rows[0]["dc_cd"].ToString() == "06")

            {

                query1 = "select * from (select '1' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,'' kd_akaun,'' kd_bjt, perkera ket, 'K' jen_txi,'0.00' as jum_deb,mhn_amt as jum_kre from KW_Pembayaran_Dedit a OUTER APPLY (select sum(jumlah) mhn_amt from KW_Pembayaran_Dedit_item s2 where s2.Status='A' and s2.no_Rujukan=a.no_Rujukan) as b where a.no_Rujukan='" + gt_pval1 + "'"

                          + " union all select '2' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end jen_txi,case when s1.tc_jenis='PEM' then discount else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then discount else '0.00' end as jum_kre from KW_Pembayaran_Dedit_item left join KW_Ref_Diskaun s1 on s1.Id='1'  and s1.tc_jenis='PEM' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_Rujukan='" + gt_pval1 + "' "

                          + " union all select '3' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, 'K' jen_txi,'0.00' as jum_deb,gstjumlah as jum_kre from KW_Pembayaran_Dedit_item left join KW_Ref_Tetapan_cukai s1 on s1.Ref_kod_cukai=tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_Rujukan='" + gt_pval1 + "' "

                          + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')"

                            + "union all select * from (select '4' sno1,'1' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,(select top(1)* from(select kod_akaun from KW_General_Ledger where GL_invois_no=a.no_invois and ref2='" + dt.Rows[0]["dc_cd"].ToString() + "' group by kod_akaun except select * from (select Ref_kod_akaun from KW_Ref_Pembekal group by Ref_kod_akaun	union all select Ref_kod_akaun from kw_ref_tetapan_cukai group by Ref_kod_akaun	union all select Ref_kod_akaun from KW_Ref_Diskaun group by Ref_kod_akaun ) as a1) as a order by a.kod_akaun desc) kd_akaun,'' kd_bjt, perkera ket, '' jen_txi,mhn_amt as jum_deb,'0.00' as jum_kre from KW_Pembayaran_Dedit a left join  KW_Pembayaran_Dedit_item a1 on a1.no_Rujukan=a.no_Rujukan OUTER APPLY (select sum(jumlah) mhn_amt from KW_Pembayaran_Dedit_item s2 where s2.Status='A' and s2.no_Rujukan=a.no_Rujukan) as b where a.no_Rujukan='" + gt_pval1 + "'"

                           + " union all select '5' sno1,'2' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end as jen_txi,case when s1.tc_jenis='PEM' then discount else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then discount else '0.00' end as jum_kre from KW_Pembayaran_Dedit_item left join KW_Ref_Diskaun s1 on s1.Id='2'  and s1.tc_jenis='PEN' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_Rujukan='" + gt_pval1 + "' "

                           + " union all select '6' sno1,'3' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end as jen_txi,case when s1.tc_jenis='PEM' then gstjumlah else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then gstjumlah else '0.00' end as jum_kre from KW_Pembayaran_Dedit_item left join KW_Ref_Tetapan_cukai s1 on s1.Ref_kod_cukai=tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_Rujukan='" + gt_pval1 + "' "

                           + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')";

            }

            else if (dt.Rows[0]["dc_cd"].ToString() == "09")

            {
                query1 = "select * from (select '1' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,pel1.Ref_kod_akaun kd_akaun,'' kd_bjt, perkera ket, 'D' jen_txi,mhn_amt as jum_deb,'0.00' as jum_kre from KW_Penerimaan_invois a OUTER APPLY (select sum(unit) mhn_amt from KW_Penerimaan_invois_item s2 where s2.Status='A' and s2.no_invois=a.no_invois) as b left join kw_ref_pelanggan pel1 on pel1.Ref_no_syarikat=a.nama_pelanggan_code and pel1.status='A' where no_invois='" + gt_pval1 + "'"

                            + " union all select '2' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, 'D' jen_txi,case when s1.tc_jenis='PEN' then discount else '0.00' end as jum_deb,case when s1.tc_jenis='PEM' then discount else '0.00' end as jum_kre from KW_Penerimaan_invois_item left join KW_Ref_Diskaun s1 on s1.tc_jenis='PEN' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_invois='" + gt_pval1 + "' "

                            + " union all select '3' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, 'D' jen_txi,gstjumlah as jum_deb,'0.00' as jum_kre from KW_Penerimaan_invois_item left join KW_Ref_Tetapan_cukai s1 on s1.tc_jenis='PEN' and s1.Ref_kod_cukai=tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_invois='" + gt_pval1 + "' "

                            + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')"

                            + "union all select * from (select '4' sno1,'1' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,(select top(1)* from(select kod_akaun from KW_General_Ledger where GL_invois_no=a.no_invois and ref2='" + dt.Rows[0]["dc_cd"].ToString() + "' group by kod_akaun except select * from (select Ref_kod_akaun from KW_Ref_Pembekal group by Ref_kod_akaun	union all select Ref_kod_akaun from kw_ref_tetapan_cukai group by Ref_kod_akaun	union all select Ref_kod_akaun from KW_Ref_Diskaun group by Ref_kod_akaun ) as a1) as a order by a.kod_akaun desc) kd_akaun,'' kd_bjt, perkera ket, '' jen_txi,'0.00' as jum_deb,mhn_amt as jum_kre from KW_Penerimaan_invois a OUTER APPLY (select sum(unit) mhn_amt from KW_Penerimaan_invois_item s2 where s2.Status='A' and s2.no_invois=a.no_invois) as b where no_invois='" + gt_pval1 + "'"

                            + " union all select '5' sno1,'2' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, 'K' as jen_txi,case when s1.tc_jenis='PEM' then discount else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then discount else '0.00' end as jum_kre from KW_Penerimaan_invois_item left join KW_Ref_Diskaun s1 on s1.tc_jenis='PEN' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_invois='" + gt_pval1 + "' "

                            + " union all select '6' sno1,'3' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, 'K' as jen_txi,case when s1.tc_jenis='PEM' then gstjumlah else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then gstjumlah else '0.00' end as jum_kre from KW_Penerimaan_invois_item left join KW_Ref_Tetapan_cukai s1 on s1.tc_jenis='PEN' and s1.Ref_kod_cukai=tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_invois='" + gt_pval1 + "' "

                            + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')";

            }

            else if (dt.Rows[0]["dc_cd"].ToString() == "10")

            {
                query1 = "select * from (select '1' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,'' kd_akaun,'' kd_bjt, perkara ket, 'K' jen_txi,'0.00' as jum_deb,mhn_amt as jum_kre from KW_Penerimaan_Credit a OUTER APPLY (select sum(Unit) mhn_amt from KW_Penerimaan_Credit_item s2 where s2.Status='A' and s2.no_notakredit=a.no_notakredit) as b where a.no_notakredit='" + gt_pval1 + "'"

                            + " union all select '2' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end jen_txi,case when s1.tc_jenis='PEM' then discount else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then discount else '0.00' end as jum_kre from KW_Penerimaan_Credit_item left join KW_Ref_Diskaun s1 on s1.Id='1'  and s1.tc_jenis='PEM' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_notakredit='" + gt_pval1 + "' "

                            + " union all select '3' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, 'K' jen_txi,'0.00' as jum_deb,gstjumlah as jum_kre from KW_Penerimaan_Credit_item left join KW_Ref_Tetapan_cukai s1 on s1.Ref_kod_cukai=tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_notakredit='" + gt_pval1 + "' "

                            + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')"

                            + "union all select * from (select '4' sno1,'1' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,(select top(1)* from(select kod_akaun from KW_General_Ledger where GL_invois_no=a.no_invois and ref2='" + dt.Rows[0]["dc_cd"].ToString() + "' group by kod_akaun except select * from (select Ref_kod_akaun from KW_Ref_Pembekal group by Ref_kod_akaun	union all select Ref_kod_akaun from kw_ref_tetapan_cukai group by Ref_kod_akaun	union all select Ref_kod_akaun from KW_Ref_Diskaun group by Ref_kod_akaun ) as a1) as a order by a.kod_akaun desc) kd_akaun,'' kd_bjt, perkara ket, '' jen_txi,mhn_amt as jum_deb,'0.00' as jum_kre from KW_Penerimaan_Credit a OUTER APPLY (select sum(Unit) mhn_amt from KW_Penerimaan_Credit_item s2 where s2.Status='A' and s2.no_notakredit=a.no_notakredit) as b where a.no_notakredit='" + gt_pval1 + "'"

                            + " union all select '5' sno1,'2' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end as jen_txi,case when s1.tc_jenis='PEM' then discount else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then discount else '0.00' end as jum_kre from KW_Penerimaan_Credit_item left join KW_Ref_Diskaun s1 on s1.Id='2'  and s1.tc_jenis='PEN' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_notakredit='" + gt_pval1 + "' "

                            + " union all select '6' sno1,'3' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end as jen_txi,case when s1.tc_jenis='PEM' then gstjumlah else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then gstjumlah else '0.00' end as jum_kre from KW_Penerimaan_Credit_item left join KW_Ref_Tetapan_cukai s1 on s1.Ref_kod_cukai=tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_notakredit='" + gt_pval1 + "' "

                            + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')";

            }

            else if (dt.Rows[0]["dc_cd"].ToString() == "11")

            {
                query1 = "select * from (select '1' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,'' kd_akaun,'' kd_bjt, perkara ket, 'K' jen_txi,'0.00' as jum_deb,mhn_amt as jum_kre from KW_Penerimaan_Debit a OUTER APPLY (select sum(Unit) mhn_amt from KW_Penerimaan_Debit_item s2 where s2.Status='A' and s2.no_notadebit=a.no_notadebit) as b where no_notadebit='" + gt_pval1 + "'"

                            + " union all select '2' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end jen_txi,case when s1.tc_jenis='PEM' then discount else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then discount else '0.00' end as jum_kre from KW_Penerimaan_Debit_item left join KW_Ref_Diskaun s1 on s1.Id='1'  and s1.tc_jenis='PEM' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_notadebit='" + gt_pval1 + "' "

                            + " union all select '3' sno1,'0' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, 'K' jen_txi,'0.00' as jum_deb,gstjumlah as jum_kre from KW_Penerimaan_Debit_item left join KW_Ref_Tetapan_cukai s1 on s1.Ref_kod_cukai=tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_notadebit='" + gt_pval1 + "' "

                            + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')"

                 + "union all select * from (select '4' sno1,'1' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,(select top(1)* from(select kod_akaun from KW_General_Ledger where GL_invois_no=a.no_invois and ref2='" + dt.Rows[0]["dc_cd"].ToString() + "' group by kod_akaun except select * from (select Ref_kod_akaun from KW_Ref_Pembekal group by Ref_kod_akaun	union all select Ref_kod_akaun from kw_ref_tetapan_cukai group by Ref_kod_akaun	union all select Ref_kod_akaun from KW_Ref_Diskaun group by Ref_kod_akaun ) as a1) as a order by a.kod_akaun desc) kd_akaun,'' kd_bjt, perkara ket, '' jen_txi,mhn_amt as jum_deb,'0.00' as jum_kre from KW_Penerimaan_Debit a OUTER APPLY (select sum(Unit) mhn_amt from KW_Penerimaan_Debit_item s2 where s2.Status='A' and s2.no_notadebit=a.no_notadebit) as b where no_notadebit='" + gt_pval1 + "'"

                            + " union all select '5' sno1,'2' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_diskaun ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end as jen_txi,case when s1.tc_jenis='PEM' then discount else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then discount else '0.00' end as jum_kre from KW_Penerimaan_Debit_item left join KW_Ref_Diskaun s1 on s1.Id='2'  and s1.tc_jenis='PEN' and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_notadebit='" + gt_pval1 + "' "

                            + " union all select '6' sno1,'3' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,s1.Ref_kod_akaun kd_akaun,'' kd_bjt, s1.Ref_nama_cukai ket, case when s1.tc_jenis='PEM' then 'D' when s1.tc_jenis='PEN' then 'K' end as jen_txi,case when s1.tc_jenis='PEM' then gstjumlah else '0.00' end as jum_deb,case when s1.tc_jenis='PEN' then gstjumlah else '0.00' end as jum_kre from KW_Penerimaan_Debit_item left join KW_Ref_Tetapan_cukai s1 on s1.Ref_kod_cukai=tax and s1.Status='A' left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where no_notadebit='" + gt_pval1 + "' "

                            + " ) as a where (jum_deb != '0.00' or jum_kre != '0.00')";

            }

            else

            {

                query1 = "select * from (select '1' sno1,'' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,case when (ISNULL(jur_entry_type,'') = 'K') then ISNULL(jur_cr_gl_cd,'') else ISNULL(jur_dr_gl_cd,'') end as kd_akaun,'' kd_bjt, perkara ket, ISNULL(jur_entry_type,'') jen_txi,case when ISNULL(jur_entry_type,'')='D' then m2.Overall else '0.00' end as jum_deb,case when ISNULL(jur_entry_type,'')='K' then m2.Overall else '0.00' end as jum_kre,m2.cr_dt from KW_jurnal_inter m1 left join KW_jurnal_inter_items m2 on m2.no_permohonan=m1.no_permohonan and m2.keterangan='PENDAPATAN BERSIH' left join KW_ref_jurnal_inter s1 on s1.jur_desc = 'PENDAPATAN BERSIH' and s1.status='A' where m1.no_permohonan='" + gt_pval1 + "'"
                            + " union all select '2' sno1,'' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,case when (ISNULL(jur_entry_type,'') = 'K') then ISNULL(jur_cr_gl_cd,'') else ISNULL(jur_dr_gl_cd,'') end as kd_akaun,kod_bajet kd_bjt, keterangan ket, case when jur_item ='STATUTORI MAJIKAN' then 'K' else ISNULL(s1.jur_entry_type,'') end as jen_txi,case when (ISNULL(jur_entry_type,'')='D' and jur_item !='STATUTORI MAJIKAN' ) then Overall when jur_item ='STATUTORI MAJIKAN' then '0.00' else '0.00' end jum_deb,case when jur_item ='STATUTORI MAJIKAN' then Overall when ISNULL(jur_entry_type,'')='K' then overall else '0.00' end jum_kre, m1.cr_dt from KW_jurnal_inter_items m1 "
                            + " left join KW_ref_jurnal_inter s1 on s1.jur_desc = keterangan and s1.status='A' Left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.jur_dr_gl_cd Left join KW_Kategori_akaun s3 on s3.kat_cd=s2.kat_akaun where m1.keterangan!='PENDAPATAN BERSIH' and no_permohonan='" + gt_pval1 + "'"
                              + " union all select '2' sno1,'' as sno,'" + dt.Rows[0]["dc_cd"].ToString() + "' as jen_typ,case when (ISNULL(jur_entry_type,'') = 'K') then ISNULL(jur_cr_gl_cd,'') else ISNULL(jur_dr_gl_cd,'') end as kd_akaun,kod_bajet kd_bjt, keterangan  ket, case when jur_item ='STATUTORI MAJIKAN' then 'D' else ISNULL(s1.jur_entry_type,'') end as jen_txi,case when jur_item ='STATUTORI MAJIKAN' then Overall else '0.00' end jum_deb,case when jur_item !='STATUTORI MAJIKAN' then Overall else '0.00' end jum_kre,m1.cr_dt from KW_jurnal_inter_items m1 "
                            + " left join KW_ref_jurnal_inter s1 on s1.jur_desc = keterangan and s1.status='A' Left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.jur_dr_gl_cd Left join KW_Kategori_akaun s3 on s3.kat_cd=s2.kat_akaun where jur_item ='STATUTORI MAJIKAN' and m1.keterangan!='PENDAPATAN BERSIH' and no_permohonan='" + gt_pval1 + "'"
                            + "  ) as a where (jum_deb != '0.00' or jum_kre != '0.00') order by cr_dt";

            }
            
            
            dt1 = Dbcon.Ora_Execute_table(query1);

            ViewState["CurrentTable2"] = dt1;

            gridmohdup.DataSource = dt1;

            gridmohdup.DataBind();

            

        }

        tab1();

        BindMohon();

    }

    protected void lblSubItemName_Click(object sender, EventArgs e)

    {



        LinkButton btn = (LinkButton)sender;

        string[] CommadArgument = btn.CommandArgument.Split(',');

        CommandArgument3 = CommadArgument[0];

        //CommandArgument4 = CommadArgument[1];

        ss_frmgrid();



    }



    void ss_frmgrid()

    {

        string ssv2 = string.Empty, ssv3 = string.Empty;

        if (CommandArgument3 != "")

        {

            ssv2 = CommandArgument3;

            ssv3 = txtnoper.Text;

        }

        else

        {

            ssv2 = Session["Invoisno"].ToString();

            ssv3 = Session["permohon_no"].ToString();

        }        

        btnprintmoh.Visible = false;

        gridmohdup.Enabled = false;

        sts.Visible = true;

        Div2.Visible = true;

        Div3.Visible = false;

        DataTable dt = new DataTable();

        dt = Dbcon.Ora_Execute_table("select '' as untuk_akaun,no_permohonan,ID_pemohon,Format(tarkih_permohonan,'dd/MM/yyyy') tarkih_permohonan,'' as  Bayar_kepada,'' as no_kakitangan,'' as nama,'' as nama_bank,'' as no_bank,'' as  tarkih_invois,'' as no_Invois,Terma,Jenis_permohonan, '' as  jumlah,status,Catatan,mohon_kp,jornal_no from KW_Pembayaran_mohon  where no_Invois='" + ssv2 + "' and no_permohonan='" + ssv3 + "' group by untuk_akaun,no_permohonan,ID_pemohon,tarkih_permohonan,Bayar_kepada,no_kakitangan,nama,nama_bank,no_bank,tarkih_invois,no_Invois,Terma,Jenis_permohonan,Overall,status,Catatan,mohon_kp,jornal_no");

        tab1();

        if (dt.Rows.Count > 0)

        {



            if (dt.Rows[0]["Bayar_kepada"].ToString() == "kakitangan")

            {

                jurnal_show2.Visible = false;

            }

            else if (dt.Rows[0]["jornal_no"].ToString() == "Pembekal")

            {

                jurnal_show2.Visible = false;

            }

            else

            {

                jurnal_show2.Visible = true;

                TextBox2.Text = dt.Rows[0]["jornal_no"].ToString();

                TextBox2.Attributes.Add("disabled", "disabled");

            }



            Session["Invoisno"] = ssv2;

            ddakaun.SelectedValue = dt.Rows[0][0].ToString();

            ddakaun.Attributes.Add("disabled", "disabled");



            txtnoper.Text = dt.Rows[0][1].ToString().Trim();

            Session["permohon_no"] = dt.Rows[0][1].ToString().Trim();

            txtnoper.Attributes.Add("disabled", "disabled");



            txtid.Text = dt.Rows[0][2].ToString().Trim();

            txtid.Attributes.Add("disabled", "disabled");



            txttarkihmo.Text = dt.Rows[0][3].ToString().Trim();

            txttarkihmo.Attributes.Add("disabled", "disabled");



            ddbkepada.SelectedValue = dt.Rows[0][4].ToString().Trim();

            ddbkepada.Attributes.Add("disabled", "disabled");



            txticno.Text = dt.Rows[0][5].ToString().Trim();

            txticno.Attributes.Add("disabled", "disabled");



            txtname.Text = dt.Rows[0][6].ToString().Trim();

            txtname.Attributes.Add("disabled", "disabled");



            txtbname.Text = dt.Rows[0][7].ToString().Trim();

            txtbname.Attributes.Add("disabled", "disabled");



            txtbno.Text = dt.Rows[0][8].ToString().Trim();

            txtbno.Attributes.Add("disabled", "disabled");





            //txtterma.Text = dt.Rows[0][11].ToString().Trim();

            //txtterma.Attributes.Add("disabled", "disabled");



            txtjenis.Text = dt.Rows[0][12].ToString().Trim();

            txtjenis.Attributes.Add("disabled", "disabled");







            ddstatus.SelectedValue = dt.Rows[0][14].ToString().Trim();

            ddstatus.Attributes.Add("disabled", "disabled");
            
            userid = Session["New"].ToString();

            level = Session["level"].ToString();
            
            txtApr.Text = userid;

            txtApr.Attributes.Add("disabled", "disabled");

            //sts.Visible = true;

            //Div10.Visible = true;

            if (dt.Rows[0]["status"].ToString().Trim() == "L")

            {

                sts.Visible = false;

                Div10.Visible = false;

            }

            else

            {

                sts.Visible = true;

                Div10.Visible = true;

            }

            DataTable dt1 = new DataTable();            

            gridmohdup.Visible = true;

            dt1 = Dbcon.Ora_Execute_table("select  Format(tarkih_invois,'dd/MM/yyyy')tarkih_invois,No_rujukan,Keteragan,Gjumlah,Gst, overall from KW_Pembayaran_mohon where no_Invois='" + ssv2 + "'");

            gridmohdup.DataSource = dt1;

            gridmohdup.DataBind();

            TextBox17.Text = dt.Rows[0][13].ToString().Trim();

            TextBox17.Attributes.Add("disabled", "disabled");
            
        }

        BindMohon();
    }
   
      protected void btnprintmoh_Click(object sender, EventArgs e)

    {

        if (Session["Invoisno"].ToString() != "")

        {

            CommandArgument3 = "";

            ss_frmgrid();

            Session["Invoisno"] = txtnoper.Text;

            //Response.Redirect("KW_Pembayaran_Mohon_prtview.aspx");

            string url = "KW_Pembayaran_Mohon_prtview.aspx";

            string s = "window.open('" + url + "', 'popup_window', 'width=11000,height=700,left=100,top=100,resizable=yes');";

            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        }

    }



    protected void grd2_reset(object sender, EventArgs e)

    {

        Response.Redirect("../kewengan/kw_Kelulusan_Jurnal.aspx");

    }

    protected void gvSelected_PageIndexChanging_g2(object sender, GridViewPageEventArgs e)

    {

        Gridview2.PageIndex = e.NewPageIndex;

        Gridview2.DataBind();

        BindMohon();

        tab1();

    }

    protected void BindMohon()

    {



        string fmdate = string.Empty, tmdate = string.Empty, condtion = string.Empty;

        if (TextBox15.Text != "")

        {

            DateTime fd = DateTime.ParseExact(TextBox15.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            fmdate = fd.ToString("yyyy-MM-dd");

        }

        if (txttarikhinvois.Text != "")

        {

            DateTime fd1 = DateTime.ParseExact(txttarikhinvois.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            tmdate = fd1.ToString("yyyy-MM-dd");

        }





        if (TextBox15.Text != "" && txttarikhinvois.Text != "" && jenis_trxn.SelectedValue != "" && DropDownList4.SelectedValue != "")

        {

            condtion = "where a.v1 >=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and a.v1<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and a.jen_txi='" + jenis_trxn.SelectedValue + "' and ISNULL(GL_sts,'')='" + DropDownList4.SelectedValue + "'";

        }

        else if (TextBox15.Text != "" && txttarikhinvois.Text != "" && jenis_trxn.SelectedValue == "" && DropDownList4.SelectedValue != "")

        {

            condtion = "where a.v1 >=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and a.v1<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and ISNULL(GL_sts,'')='" + DropDownList4.SelectedValue + "'";

        }

        else if (TextBox15.Text != "" && txttarikhinvois.Text != "" && jenis_trxn.SelectedValue == "" && DropDownList4.SelectedValue == "")

        {

            condtion = "where a.v1 >=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and a.v1<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)";

        }

        else if (TextBox15.Text != "" && txttarikhinvois.Text != "" && jenis_trxn.SelectedValue != "" && DropDownList4.SelectedValue == "")

        {

            condtion = "where a.v1 >=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and a.v1<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)";

        }

        else if (TextBox15.Text == "" && txttarikhinvois.Text == "" && jenis_trxn.SelectedValue != "" && DropDownList4.SelectedValue != "")

        {

            condtion = "where a.jen_txi='" + jenis_trxn.SelectedValue + "' and ISNULL(GL_sts,'')='" + DropDownList4.SelectedValue + "'";

        }

        else if (TextBox15.Text == "" && txttarikhinvois.Text == "" && jenis_trxn.SelectedValue != "" && DropDownList4.SelectedValue == "")

        {

            condtion = "where a.jen_txi='" + jenis_trxn.SelectedValue + "' and ISNULL(GL_sts,'')='" + DropDownList4.SelectedValue + "'";

        }

        else if (TextBox15.Text == "" && txttarikhinvois.Text == "" && jenis_trxn.SelectedValue == "" && DropDownList4.SelectedValue != "")

        {

            condtion = "where ISNULL(GL_sts,'')='" + DropDownList4.SelectedValue + "'";

        }
        else
        {
            condtion = "where ISNULL(GL_sts,'')='" + DropDownList4.SelectedValue + "'";
        }

        
        qry1 = "select *,case when ISNULL(GL_sts,'') = 'L' then 'LULUS' when ISNULL(GL_sts,'') ='T' then 'TIDAK LULUS' else 'PENDING' end as glsts from (select kat_penerima kat,kel_kategory kel_kat,no_permohonan rujukan,no_ruj noinvois,tarkih_permohonan v1,Format(tarkih_permohonan,'dd/MM/yyyy') tk_mohon,'03' as jen_txi,b.mhn_byr_kepada as  Bayar_kepada,a.jumlah as  jumlah,b.mhn_keterangan as keterengan,ISNULL(a.semak_sts,'') sts_cd,CASE ISNULL(a.semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status  from KW_Pembayaran_mohon a OUTER APPLY (select top(1)* from KW_Pembayaran_mohon_item s2 where s2.Status='A' and s2.mhn_no_permohonan=a.no_permohonan order by cr_dt asc) as b  where a.Status='A'"

        + " union all select kat_penerima kat,kel_kategory kel_kat,a.no_invois rujukan,a.no_invois noinvois,tarkih_mohon v1,Format(tarkih_mohon,'dd/MM/yyyy') tk_mohon,'01' as jen_txi,Bayar_kepada as  Bayar_kepada,a.Overall as  jumlah,b.keterangan as keterengan,ISNULL(a.semak_sts,'') sts_cd,CASE ISNULL(a.semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status  from KW_Pembayaran_invois a OUTER APPLY (select top(1)* from KW_Pembayaran_invoisBil_item s2 where s2.Status='A' and s2.no_invois=a.no_invois order by cr_dt asc) as b  where a.Status='A'"

        + " union all select '03' kat,kel_kategory kel_kat,a.no_invois rujukan,a.no_invois noinvois,tarikh_invois v1,Format(tarikh_invois,'dd/MM/yyyy') tk_mohon,'09' as jen_txi,nama_pelanggan_code as  Bayar_kepada,a.Overall as  jumlah,perkera as keterengan,ISNULL(a.semak_sts,'') sts_cd,CASE ISNULL(a.semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status  from KW_Penerimaan_invois a OUTER APPLY (select top(1)* from KW_Penerimaan_invois_item s2 where s2.Status='A' and s2.no_invois=a.no_invois order by cr_dt asc) as b  where a.Status='A'"

        + " union all select '03' kat,'' kel_kat,a.no_resit rujukan,no_invois noinvois,tarikh_resit v1,Format(tarikh_resit,'dd/MM/yyyy') tk_mohon,'02' as jen_txi,nama_pelanggan_code as  Bayar_kepada,a.Overall as  jumlah,perkara as keterengan,ISNULL(a.lulus_sts,'') sts_cd,CASE ISNULL(a.lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status  from KW_Penerimaan_resit a OUTER APPLY (select top(1)* from KW_Penerimaan_resit_item s2 where s2.Status='A' and s2.no_resit=a.no_resit order by cr_dt asc) as b  where a.Status='A'"

        + " union all SELECT '02' kat,kel_kategory kel_kat,no_Rujukan rujukan,no_invois noinvois,tarikh_debit v1, Format(tarikh_debit,'dd/MM/yyyy') tk_mohon,'06' as jen_txi, nama_pembekal_code as Bayar_kepada,Overall as  jumlah,perkera keterangan,ISNULL(semak_sts,'') sts_cd,CASE ISNULL(semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status FROM KW_Pembayaran_Dedit "

        + " union all SELECT '02' kat,kel_kategory kel_kat,no_Rujukan rujukan,no_invois noinvois,tarikh_kredit v1, Format(tarikh_kredit,'dd/MM/yyyy') tk_mohon,'05' as jen_txi, nama_pembekal_code as Bayar_kepada,Overall as  jumlah,perkera keterangan,ISNULL(semak_sts,'') sts_cd,CASE ISNULL(semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status FROM KW_Pembayaran_Credit "

        + " union all SELECT '03' kat,kel_kategory kel_kat,no_notakredit rujukan,no_invois noinvois,tarikh_kredit v1, Format(tarikh_kredit,'dd/MM/yyyy') tk_mohon,'10' as jen_txi, nama_pelanggan_code as Bayar_kepada,Overall as  jumlah,perkara keterangan,ISNULL(semak_sts,'') sts_cd,CASE ISNULL(semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status FROM KW_Penerimaan_Credit "

        + " union all SELECT '03' kat,kel_kategory kel_kat,no_notadebit rujukan,no_invois noinvois,tarikh_debit v1, Format(tarikh_debit,'dd/MM/yyyy') tk_mohon,'11' as jen_txi, nama_pelanggan_code as Bayar_kepada,Overall as  jumlah,perkara keterangan,ISNULL(semak_sts,'') sts_cd,CASE ISNULL(semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status FROM KW_Penerimaan_Debit "

        + " union all SELECT '11' kat,kel_kategory kel_kat,no_permohonan rujukan,no_Rujukan noinvois,tarikh_lulus v1, Format(tarikh_lulus,'dd/MM/yyyy') tk_mohon,Jenis_permohonan as jen_txi, nama_pelanggan_code as Bayar_kepada,Overall as  jumlah,perkara keterangan,ISNULL(semak_sts,'') sts_cd,CASE ISNULL(semak_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status FROM KW_jurnal_inter "

        + " union all SELECT '02' kat,'' kel_kat,Pv_no rujukan,no_invois noinvois,tarkih_pv v1, Format(tarkih_pv,'dd/MM/yyyy') tk_mohon,'04' as jen_txi, Bayar_kepada as Bayar_kepada,Overall as  jumlah,perkara keterangan,ISNULL(lulus_sts,'') sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as status,ISNULL(lulus_sts,'') lul_sts_cd,CASE ISNULL(lulus_sts,'')  WHEN '' THEN 'WAITING'  WHEN 'P' THEN 'PROSES' WHEN 'L' THEN 'LULUS' END  as lul_status FROM KW_Pembayaran_Pay_voucer) as a "

        + " OUTER APPLY (select top(1) GL_sts from KW_General_Ledger s2 where s2.Gl_jenis_no=a.rujukan) as c"

        + " " + condtion + " and lul_sts_cd ='L'  order by a.tk_mohon desc";



        SqlCommand cmd2 = new SqlCommand(qry1, con);

        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);

        DataSet ds2 = new DataSet();

        da2.Fill(ds2);

        if (ds2.Tables[0].Rows.Count == 0)

        {

            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());

            Gridview2.DataSource = ds2;

            Gridview2.DataBind();

            int columncount = Gridview2.Rows[0].Cells.Count;

            Gridview2.Rows[0].Cells.Clear();

            Gridview2.Rows[0].Cells.Add(new TableCell());

            Gridview2.Rows[0].Cells[0].ColumnSpan = columncount;

            Gridview2.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";

        }

        else

        {

            Gridview2.DataSource = ds2;

            Gridview2.DataBind();

        }



    }

    
   
    protected void but_Click(object sender, EventArgs e)

    {
       
        BindMohon();

        tab1();

    }


    
    void tab1()

    {

        p6.Attributes.Add("class", "tab-pane active");

        pp6.Attributes.Add("class", "active");

        hd_txt.Text = "MOHON BAYAR";

    }

    
    protected void Button1_Click(object sender, EventArgs e)

    {

        Div2.Visible = false;

        Div3.Visible = true;

        gridmohdup.Visible = false;

        reset();

        tab1();

        BindMohon();

    }
    
    
    protected void Gridview12_RowDataBound(object sender, GridViewRowEventArgs e)

    {



        if (e.Row.RowType == DataControlRowType.DataRow)

        {

            //Find the DropDownList in the Row

            DropDownList ddlCountries = (e.Row.FindControl("ddkoddupcre") as DropDownList);

            ddlCountries.DataSource = GetData("select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun from KW_ref_carta_akaun where jenis_akaun_type != '1' and Status='A'");

            ddlCountries.DataTextField = "nama_akaun";

            ddlCountries.DataValueField = "kod_akaun";

            ddlCountries.DataBind();



            //Add Default Item in the DropDownList

            ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));

            string country = (e.Row.FindControl("lblCountry") as Label).Text;

            if (country != "0")

            {

                ddlCountries.Items.FindByValue(country).Selected = true;

            }



            //Select the Country of Customer in DropDownList



            DropDownList ddlCountries1 = (e.Row.FindControl("ddgst") as DropDownList);

            ddlCountries1.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='PEM'");

            ddlCountries1.DataTextField = "Ref_nama_cukai";

            ddlCountries1.DataValueField = "Ref_kod_cukai";

            ddlCountries1.DataBind();



            ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));



            //Select the Country of Customer in DropDownList

            string country1 = (e.Row.FindControl("lblgst") as Label).Text;

            if (country1 != "0")

            {

                ddlCountries1.Items.FindByValue(country1).Selected = true;

            }



            DropDownList ddlCountries2 = (e.Row.FindControl("ddgstoth") as DropDownList);

            ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='OTH'");

            ddlCountries2.DataTextField = "Ref_nama_cukai";

            ddlCountries2.DataValueField = "Ref_kod_cukai";

            ddlCountries2.DataBind();



            ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---"));



            //Select the Country of Customer in DropDownList

            string country2 = (e.Row.FindControl("lblgstoth") as Label).Text;

            if (country2 != "0")

            {

                ddlCountries2.Items.FindByValue(country2).Selected = true;

            }

        }



    }



  
    private void GetMohon()

    {

        DataTable dt1 = Dbcon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='23' and Status='A'");

        if (dt1.Rows.Count != 0)

        {

            if (dt1.Rows[0]["cfmt"].ToString() == "")

            {

                TextBox23.Text = dt1.Rows[0]["fmt"].ToString();

                TextBox23.Attributes.Add("disabled", "disabled");

            }

            else

            {

                int seqno = Convert.ToInt32(dt1.Rows[0]["lfrmt2"].ToString());

                int newNumber = seqno + 1;

                uniqueId = newNumber.ToString(dt1.Rows[0]["lfrmt1"].ToString() + "0000");

                TextBox23.Text = uniqueId;

                TextBox23.Attributes.Add("disabled", "disabled");

            }



        }

        else

        {

            DataTable get_doc = new DataTable();

            get_doc = Dbcon.Ora_Execute_table("select Ref_doc_descript as s1,s1.ws_format as s2 from KW_Ref_Doc_kod left join site_settings s1 on  s1.ID='1' where Ref_doc_code='23'");

            DataTable dt = Dbcon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(GL_journal_no,13,3000)),'0') from KW_General_Ledger");

            if (dt.Rows.Count > 0)

            {

                int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());

                int newNumber = seqno + 1;

                uniqueId = newNumber.ToString(get_doc.Rows[0][1].ToString() + "-" + get_doc.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");

                TextBox23.Text = uniqueId;

                TextBox23.Attributes.Add("disabled", "disabled");



            }

            else

            {

                int newNumber = Convert.ToInt32(uniqueId) + 1;

                uniqueId = newNumber.ToString(get_doc.Rows[0][1].ToString() + "-" + get_doc.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");

                TextBox23.Text = uniqueId;

                TextBox23.Attributes.Add("disabled", "disabled");

            }

        }





    }





    
   
    
    protected void grdmohon_RowDataBound(object sender, GridViewRowEventArgs e)

    {

        if (e.Row.RowType == DataControlRowType.DataRow)

        {

            //CheckBox tax = (CheckBox)e.Row.FindControl("CheckBox1");



            //grdmohon.Columns[9].a;



            DropDownList dd_bayar = (e.Row.FindControl("TextBox11") as DropDownList);

            if (DropDownList1.SelectedValue == "01")

            {

                dd_bayar.DataSource = GetData("SELECT stf_name as val1,stf_staff_no as val2 FROM hr_staff_profile s1 left join hr_post_his s2 on s2.pos_staff_no=s1.stf_staff_no and pos_end_dt >='" + DateTime.Now.ToString("yyyy-MM-dd") + "'");

            }

            else if (DropDownList1.SelectedValue == "02")

            {

                dd_bayar.DataSource = GetData("select Ref_nama_syarikat as val1,Ref_no_syarikat as val2 from KW_Ref_Pembekal where Status='A'");

            }

            else if (DropDownList1.SelectedValue == "03")

            {

                dd_bayar.DataSource = GetData("select Ref_nama_syarikat as val1,Ref_no_syarikat as val2 from KW_Ref_Pelanggan where Status='A'");

            }

            dd_bayar.DataTextField = "val1";

            dd_bayar.DataValueField = "val2";

            dd_bayar.DataBind();



            //Add Default Item in the DropDownList

            dd_bayar.Items.Insert(0, new ListItem("--- PILIH ---", ""));



            DropDownList ddlCountries2 = (e.Row.FindControl("ddcukaiinv") as DropDownList);

            ddlCountries2.DataSource = GetData("select Ref_nama_cukai,Ref_kod_cukai from KW_Ref_Tetapan_cukai where tc_jenis='PEM'");

            ddlCountries2.DataTextField = "Ref_nama_cukai";

            ddlCountries2.DataValueField = "Ref_kod_cukai";

            ddlCountries2.DataBind();



            //Add Default Item in the DropDownList

            ddlCountries2.Items.Insert(0, new ListItem("--- PILIH ---", ""));



            DropDownList ddbank = (e.Row.FindControl("dd_bank") as DropDownList);

            ddbank.DataSource = GetData("select Bank_Code,Bank_Name from Ref_Nama_Bank");

            ddbank.DataTextField = "Bank_Name";

            ddbank.DataValueField = "Bank_Code";

            ddbank.DataBind();



            //Add Default Item in the DropDownList

            ddbank.Items.Insert(0, new ListItem("--- PILIH ---", ""));



            DropDownList ddprojek = (e.Row.FindControl("dd_projek") as DropDownList);

            ddprojek.DataSource = GetData("select Ref_Projek_code,Ref_Projek_name from KW_Ref_Projek");

            ddprojek.DataTextField = "Ref_Projek_name";

            ddprojek.DataValueField = "Ref_Projek_code";

            ddprojek.DataBind();



            //Add Default Item in the DropDownList

            ddprojek.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }

    }



    protected void gridmohdup_RowDataBound(object sender, GridViewRowEventArgs e)

    {
     
        if (e.Row.RowType == DataControlRowType.DataRow)

        {

            //DataTable get_query = new DataTable();

            System.Web.UI.WebControls.Label sno = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label22");
            System.Web.UI.WebControls.Label sno1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label27");
            System.Web.UI.WebControls.Label kdakaun = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label28");
            System.Web.UI.WebControls.Label jtype = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label23");
            System.Web.UI.WebControls.Label kd_type = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label29");
            
            System.Web.UI.WebControls.TextBox dbt_amt = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("deb_amount");
            System.Web.UI.WebControls.TextBox krt_amt = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("kre_amount");

            
            TextBox ket = (e.Row.FindControl("ket") as TextBox);

            //Label ref_kod_akaun = (e.Row.FindControl("Label23") as Label);



            LinkButton lnk_shw = (e.Row.FindControl("gridmohdup_lnkView") as LinkButton);


           
            DropDownList ddakaun = (e.Row.FindControl("gridmohdup_ddakaun") as DropDownList);

            //DropDownList ddakaun1 = (e.Row.FindControl("gridmohdup_jen_txi") as DropDownList);

            DataTable get_desc1 = new DataTable();
            DataTable get_desc2 = new DataTable();


            if (sno.Text == "0")

            {
                ddakaun.DataSource = GetData("SELECT kod_akaun as Ref_kod_akaun,(kod_akaun + ' | '+ nama_akaun) as nama_akaun FROM KW_Ref_Carta_Akaun m1 inner join KW_Kategori_akaun s1 on s1.kat_cd=kat_akaun and s1.kat_type='01'  and s1.Status='A' where m1.Status='A' and jenis_akaun_type != '1' and ISNULL(kw_acc_header,'0') = '0'");
                if (Session["jurnal_cmd2"].ToString() == "09" || Session["jurnal_cmd2"].ToString() == "03" || Session["jurnal_cmd2"].ToString() == "04" || Session["jurnal_cmd2"].ToString() == "05" || Session["jurnal_cmd2"].ToString() == "06" || Session["jurnal_cmd2"].ToString() == "14" || Session["jurnal_cmd2"].ToString() == "15" || Session["jurnal_cmd2"].ToString() == "16" || Session["jurnal_cmd2"].ToString() == "18" || Session["jurnal_cmd2"].ToString() == "19" || Session["jurnal_cmd2"].ToString() == "20" || Session["jurnal_cmd2"].ToString() == "21")
                {
                  
                    if (sno1.Text == "1")
                    {
                        get_desc1 = DBCon.Ora_Execute_table("select Ref_no_syarikat Ref_kod_akaun,s2.bal_type,Ref_kod_akaun as kod_akaun from KW_Ref_Pembekal s1 left join KW_Kategori_akaun s2 on s2.kat_cd=s1.Ref_jenis_akaun WHERE s1.Status='A' and Ref_nama_syarikat='" + TextBox26.Text + "'");
                        kd_type.Text = "PEM";
                    }
                    //else if (sno1.Text == "2")
                    //{
                    //    get_desc1 = DBCon.Ora_Execute_table("select ref_kod_diskaun Ref_kod_akaun,'' bal_type,Ref_kod_akaun as kod_akaun from kw_ref_diskaun where status='A' and tc_jenis='PEM'");
                    //}
                    //else if (sno1.Text == "3")
                    //{
                    //    get_desc1 = DBCon.Ora_Execute_table("select ref_kod_cukai Ref_kod_akaun,'' bal_type,Ref_kod_akaun as kod_akaun from kw_ref_tetapan_cukai where status='A' and tc_jenis='PEM'");
                    //}
                    else
                    {
                        kd_type.Text = "";
                    }
                }
                else if (Session["jurnal_cmd2"].ToString() == "01" || Session["jurnal_cmd2"].ToString() == "10" || Session["jurnal_cmd2"].ToString() == "11" || Session["jurnal_cmd2"].ToString() == "12" || Session["jurnal_cmd2"].ToString() == "13" || Session["jurnal_cmd2"].ToString() == "02" || Session["jurnal_cmd2"].ToString() == "04")
                {
                  
                    if (sno1.Text == "1")
                    {
                        get_desc1 = DBCon.Ora_Execute_table("select Ref_no_syarikat Ref_kod_akaun,s2.bal_type,Ref_kod_akaun as kod_akaun from KW_Ref_Pelanggan s1 left join KW_Kategori_akaun s2 on s2.kat_cd=s1.Ref_jenis_akaun WHERE s1.Status='A' and Ref_nama_syarikat='" + TextBox26.Text + "'");
                        kd_type.Text = "PEL";
                    }
                    //else if (sno1.Text == "2")
                    //{
                    //    get_desc1 = DBCon.Ora_Execute_table("select ref_kod_diskaun Ref_kod_akaun,'' bal_type,Ref_kod_akaun as kod_akaun from kw_ref_diskaun where status='A' and tc_jenis='PEN'");
                    //}
                    //else if (sno1.Text == "3")
                    //{
                    //    get_desc1 = DBCon.Ora_Execute_table("select ref_kod_cukai Ref_kod_akaun,'' bal_type,Ref_kod_akaun as kod_akaun from kw_ref_tetapan_cukai where status='A' and tc_jenis='PEN'");                        
                    //}
                    else
                    {
                        kd_type.Text = "";
                    }

                }
                else
                {
                    get_desc1 = DBCon.Ora_Execute_table("select Ref_kod_akaun,s2.bal_type from KW_Ref_Pelanggan s1 left join KW_Kategori_akaun s2 on s2.kat_cd=s1.Ref_jenis_akaun WHERE s1.Status='A' and Ref_nama_syarikat=''");
                    kd_type.Text = "";
                }
                Session["sno1"] = kd_type.Text;

            }
            else if (sno.Text == "1")
            {
                ddakaun.DataSource = GetData("SELECT kod_akaun as Ref_kod_akaun,(kod_akaun + ' | '+ nama_akaun) as nama_akaun FROM KW_Ref_Carta_Akaun m1 inner join KW_Kategori_akaun s1 on s1.kat_cd=kat_akaun and s1.kat_type='01'  and s1.Status='A' where m1.Status='A' and jenis_akaun_type != '1' and ISNULL(kw_acc_header,'0') = '0'");
                get_desc2 = DBCon.Ora_Execute_table("	select top(1)* from(select kod_akaun from KW_General_Ledger where GL_invois_no='"+ TextBox19.Text + "' and ref2='"+ Session["jurnal_cmd2"].ToString() + "' group by kod_akaun except "
                    + " select * from(select Ref_kod_akaun from KW_Ref_Pembekal group by Ref_kod_akaun "
                    + " union all select Ref_kod_akaun from kw_ref_tetapan_cukai group by Ref_kod_akaun "
                    +" union all select Ref_kod_akaun from KW_Ref_Diskaun group by Ref_kod_akaun) as a1) as a order by a.kod_akaun desc");
                if (get_desc2.Rows.Count != 0)
                {
                    kdakaun.Text = get_desc2.Rows[0]["kod_akaun"].ToString();
                }
            }
            else if (sno.Text == "2")
            {
                ddakaun.DataSource = GetData("SELECT kod_akaun as Ref_kod_akaun,(kod_akaun + ' | '+ nama_akaun) as nama_akaun FROM KW_Ref_Carta_Akaun m1 inner join KW_Kategori_akaun s1 on s1.kat_cd=kat_akaun and s1.kat_type='01'  and s1.Status='A' where m1.Status='A' and jenis_akaun_type != '1' and ISNULL(kw_acc_header,'0') = '0'");
            }
            else if (sno.Text == "3")

            {
                ddakaun.DataSource = GetData("SELECT kod_akaun as Ref_kod_akaun,(kod_akaun + ' | '+ nama_akaun) as nama_akaun FROM KW_Ref_Carta_Akaun m1 inner join KW_Kategori_akaun s1 on s1.kat_cd=kat_akaun and s1.kat_type='01'  and s1.Status='A' where m1.Status='A' and jenis_akaun_type != '1' and ISNULL(kw_acc_header,'0') = '0'");
            }
            else
            {
                //if (jtype.Text != "")
                //{
                //    ddakaun.DataSource = GetData("SELECT kod_akaun as Ref_kod_akaun,(kod_akaun + ' | '+ nama_akaun) as nama_akaun FROM KW_Ref_Carta_Akaun m1 inner join KW_Kategori_akaun s1 on s1.kat_cd=kat_akaun and s1.Status='A' where m1.Status='A' and jenis_akaun_type != '1'");
                //}
                //else
                //{
                ddakaun.DataSource = GetData("SELECT kod_akaun as Ref_kod_akaun,(kod_akaun + ' | '+ nama_akaun) as nama_akaun FROM KW_Ref_Carta_Akaun m1 inner join KW_Kategori_akaun s1 on s1.kat_cd=kat_akaun and s1.Status='A' where m1.Status='A' and jenis_akaun_type != '1' and ISNULL(kw_acc_header,'0') = '0'");
                //}
                kd_type.Text = "";
            }



            ddakaun.DataTextField = "nama_akaun";

            ddakaun.DataValueField = "Ref_kod_akaun";

            ddakaun.DataBind();



            //Add Default Item in the DropDownList

            ddakaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            if (sno.Text == "0")

            {

                if (get_desc1.Rows.Count != 0)
                {
                    if (get_desc1.Rows[0]["kod_akaun"].ToString() != "")
                    {
                        if (Session["pem_update"].ToString() == "")
                        {
                            ddakaun.SelectedValue = get_desc1.Rows[0]["kod_akaun"].ToString();
                        }
                        else
                        {
                            ddakaun.SelectedValue = Session["pem_update"].ToString();
                            Session["pem_update"] = "";
                        }

                        //lnk_shw.Visible = false;

                    }
                    else
                    {
                        if (kd_type.Text == "PEM" || kd_type.Text == "PEL")
                        {
                            lnk_shw.Visible = true;
                        }
                        else
                        {
                            lnk_shw.Visible = false;
                        }
                    }
                }
                else
                {
                    if (kdakaun.Text != "")
                    {
                        ddakaun.SelectedValue = kdakaun.Text;
                    }
                }
            }
            else if (sno.Text == "1")

            {
                ddakaun.SelectedValue = kdakaun.Text;
            }
            else if (sno.Text == "2")

            {
                ddakaun.SelectedValue = kdakaun.Text;
            }
            else if (sno.Text == "3")

            {
                ddakaun.SelectedValue = kdakaun.Text;
            }
            else

            {
                ddakaun.SelectedValue = kdakaun.Text;
            }

            //ddakaun1.SelectedValue = jtype.Text;
            if (krt_amt.Text != "0.00" && krt_amt.Text != "")
            {
                tot1 += double.Parse(krt_amt.Text);
            }
            else if (dbt_amt.Text != "0.00" && dbt_amt.Text != "")
            {                
                krt_amt.Text = "0.00";
                tot2 += double.Parse(dbt_amt.Text);
            }
                       

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label jum_deb = (e.Row.FindControl("Label1_deb") as Label);
            Label jum_kre = (e.Row.FindControl("Label2_kre") as Label);
            jum_deb.Text = tot2.ToString("C").Replace("$", "").Replace("RM", "");
            jum_kre.Text = tot1.ToString("C").Replace("$", "").Replace("RM", "");
        }
    }

    protected void RowDataBound_view(object sender, GridViewRowEventArgs e)

    {

        if (e.Row.RowType == DataControlRowType.DataRow)

        {

            //DataTable get_query = new DataTable();

            Label deb = (Label)e.Row.FindControl("amount_deb");
            Label kre = (Label)e.Row.FindControl("amount_kre");
                       
            //ddakaun1.SelectedValue = jtype.Text;
            if (kre.Text != "0.00" && kre.Text != "")
            {
                tot1 += double.Parse(kre.Text);
            }
            else if (deb.Text != "0.00" && deb.Text != "")
            {                
                tot2 += double.Parse(deb.Text);
            }


        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label jum_deb1 = (e.Row.FindControl("Label21_deb") as Label);
            Label jum_kre1 = (e.Row.FindControl("Label21_kre") as Label);
            jum_deb1.Text = tot2.ToString("C").Replace("$", "").Replace("RM", "");
            jum_kre1.Text = tot1.ToString("C").Replace("$", "").Replace("RM", "");
        }
    }
    protected void sel_Akaun_desc(object sender, EventArgs e)
    {
        
        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        DropDownList dd1 = (DropDownList)gridmohdup.Rows[selRowIndex].FindControl("gridmohdup_ddakaun");
        //DropDownList dd2 = (DropDownList)gridmohdup.Rows[selRowIndex].FindControl("gridmohdup_jen_txi");
        TextBox dd_desc = (TextBox)gridmohdup.Rows[selRowIndex].FindControl("ket");
        Label sno = (Label)gridmohdup.Rows[selRowIndex].FindControl("Label22");
        Label kd_type = (Label)gridmohdup.Rows[selRowIndex].FindControl("Label29");
       // LinkButton lnk_shw = (gridmohdup.Rows[selRowIndex].FindControl("gridmohdup_lnkView") as LinkButton);

        if (dd1.SelectedValue != "")
        {
            //if (kd_type.Text != "PEM")
            //{
            if (sno.Text == "1")
            {
                string s1 = dd1.SelectedItem.Text;
                string[] authorsList = s1.Split('|');
                dd_desc.Text = authorsList[1].Trim().ToUpper();
            }
            else if (sno.Text == "2")
            {
                string s1 = dd1.SelectedItem.Text;
                string[] authorsList = s1.Split('|');
                DataTable dt1_gst = new DataTable();
                dt1_gst = Dbcon.Ora_Execute_table("select s2.nama_akaun as val1 from KW_Ref_Diskaun s1 left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where s1.Ref_kod_akaun='" + authorsList[0].Trim().ToUpper() + "'");
                if (dt1_gst.Rows.Count != 0)
                {
                    dd_desc.Text = dt1_gst.Rows[0]["val1"].ToString();
                }
            }
            else if (sno.Text == "3")
            {
                string s1 = dd1.SelectedItem.Text;
                string[] authorsList = s1.Split('|');
                DataTable dt1_gst = new DataTable();
                dt1_gst = Dbcon.Ora_Execute_table("select s2.nama_akaun as val1 from KW_Ref_Tetapan_cukai s1 left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where s1.Ref_kod_akaun='" + authorsList[0].Trim().ToUpper() + "'");
                if (dt1_gst.Rows.Count != 0)
                {
                    dd_desc.Text = dt1_gst.Rows[0]["val1"].ToString();
                }
            }
                //DataTable dt1 = new DataTable();
                //dt1 = Dbcon.Ora_Execute_table("select bal_type from KW_Ref_Carta_Akaun m1 left join KW_Kategori_akaun s1 on s1.kat_cd=m1.kat_akaun and kat_type='01' where kod_akaun='" + dd1.SelectedValue + "'");
                //if (dt1.Rows.Count != 0)
                //{
                //    dd2.SelectedValue = dt1.Rows[0]["bal_type"].ToString();
                //}
                //lnk_shw.Visible = false;
            //}
            //else
            //{
            //    DataTable dt1_pem = new DataTable();
            //    dt1_pem = Dbcon.Ora_Execute_table("select Ref_kod_akaun from KW_Ref_Pembekal where Ref_no_syarikat='" + dd1.SelectedValue + "'");
            //    if (dt1_pem.Rows[0]["Ref_kod_akaun"].ToString() == "")
            //    {
            //        lnk_shw.Visible = true;
            //    }
            //    else
            //    {
            //        lnk_shw.Visible = false;
            //    }
            //}
        }
        GrandTotalt1();
    }
    protected void changed_amt1(object sender, EventArgs e)
    {
        GrandTotalt1();
    }
    private void GrandTotalt1()
    {
        float GTotal = 0f;
        float ITotal = 0f;        
        for (int i = 0; i < gridmohdup.Rows.Count; i++)
        {
            String deb = (gridmohdup.Rows[i].FindControl("deb_amount") as TextBox).Text;
            String kre = (gridmohdup.Rows[i].FindControl("kre_amount") as TextBox).Text;
            Label jum_deb = (Label)gridmohdup.FooterRow.FindControl("Label1_deb");
            Label jum_kre = (Label)gridmohdup.FooterRow.FindControl("Label2_kre");
            decimal totdeb = Convert.ToDecimal(deb);
            decimal totkre = Convert.ToDecimal(kre);

            GTotal += (float)Convert.ToDecimal(totdeb);
            ITotal += (float)Convert.ToDecimal(totkre);
            jum_deb.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");
            jum_kre.Text = ITotal.ToString("C").Replace("RM", "").Replace("$", "");
            
        }

    }
    void bind_jurnal()

    {

        DataSet Ds = new DataSet();

        try

        {

            string com = jurnal_qry;

            SqlDataAdapter adpt = new SqlDataAdapter(com, con);

            DataTable dt = new DataTable();

            adpt.Fill(dt);

            jurnal_no.DataSource = dt;

            jurnal_no.DataTextField = "val2";

            jurnal_no.DataValueField = "val1";

            jurnal_no.DataBind();

            jurnal_no.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }

        catch (Exception ex)

        {

            throw ex;

        }

    }
    
    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)

    {

        if (e.Row.RowType == DataControlRowType.DataRow)

        {



            System.Web.UI.WebControls.Label lblctrl = (System.Web.UI.WebControls.Label)e.Row.FindControl("status");

            System.Web.UI.WebControls.Label lul_sts = (System.Web.UI.WebControls.Label)e.Row.FindControl("kel_status");

            System.Web.UI.WebControls.Label kat = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label17");

            System.Web.UI.WebControls.Label bay_cd = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label18");

            System.Web.UI.WebControls.Label jen_txi = (System.Web.UI.WebControls.Label)e.Row.FindControl("jen_txi");

            System.Web.UI.WebControls.Label bay_desc = (System.Web.UI.WebControls.Label)e.Row.FindControl("Bayar_kepada");

            DataTable ddokdicno = new DataTable();

            DataTable get_ino1 = new DataTable();

            get_ino1 = DBCon.Ora_Execute_table("select Ref_doc_code,Ref_doc_name from KW_Ref_Doc_kod where Ref_doc_code ='" + jen_txi.Text + "'");

            if (get_ino1.Rows.Count != 0)

            {

                jen_txi.Text = get_ino1.Rows[0]["Ref_doc_name"].ToString();

            }

            if (kat.Text == "01")

            {

                ddokdicno = DBCon.Ora_Execute_table("SELECT stf_name as val1,stf_staff_no as val2 FROM hr_staff_profile s1 left join hr_post_his s2 on s2.pos_staff_no=s1.stf_staff_no and pos_end_dt >='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where stf_staff_no='" + bay_cd.Text + "'");

            }

            else if (kat.Text == "02")

            {

                ddokdicno = DBCon.Ora_Execute_table("select Ref_nama_syarikat as val1,Ref_no_syarikat as val2 from KW_Ref_Pembekal where Status='A' and Ref_no_syarikat='" + bay_cd.Text + "'");

            }

            else if (kat.Text == "03")

            {

                ddokdicno = DBCon.Ora_Execute_table("select Ref_nama_syarikat as val1,Ref_no_syarikat as val2 from KW_Ref_Pelanggan where Status='A' and Ref_no_syarikat='" + bay_cd.Text + "'");

            }

            else if (kat.Text == "11")

            {

                ddokdicno = DBCon.Ora_Execute_table("select KK_Skrin_name as val1,KK_Skrin_id as val2 from KK_PID_Skrin where KK_Skrin_id='" + bay_cd.Text + "' and Status='A'");

            }

            if (ddokdicno.Rows.Count != 0)

            {

                bay_desc.Text = ddokdicno.Rows[0]["val1"].ToString();

            }

            if (lblctrl.Text == "WAITING")

            {

                lblctrl.Attributes.Add("Class", "label label-danger Uppercase");

                LinkButton Hlnk = e.Row.FindControl("LinkButton2") as LinkButton;

                Hlnk.Visible = true;

            }

            else

            {

                lblctrl.Attributes.Add("Class", "label label-primary Uppercase");

                LinkButton Hlnk = e.Row.FindControl("LinkButton2") as LinkButton;

                Hlnk.Visible = false;

            }

            if (lul_sts.Text == "PENDING" )

            {

                lul_sts.Attributes.Add("Class", "label label-danger Uppercase");

                LinkButton Hlnk = e.Row.FindControl("LinkButton2") as LinkButton;

                Hlnk.Visible = true;

            }
            else if (lul_sts.Text == "TIDAK LULUS")

            {

                lul_sts.Attributes.Add("Class", "label label-warning Uppercase");

                LinkButton Hlnk = e.Row.FindControl("LinkButton2") as LinkButton;

                Hlnk.Visible = true;

            }

            else if (lul_sts.Text == "LULUS")

            {

                lul_sts.Attributes.Add("Class", "label label-primary Uppercase");

                LinkButton Hlnk = e.Row.FindControl("LinkButton2") as LinkButton;

                Hlnk.Visible = false;

            }

        }

    }



    protected void lnkView_Click(object sender, EventArgs e)

    {

        try

        {

            LinkButton btnButton = sender as LinkButton;

            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;

            System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl_mohon_no");

            string lblid1 = lblTitle.Text;

            DataTable ddokdicno = new DataTable();

            ddokdicno = DBCon.Ora_Execute_table("select * From KW_Pembayaran_mohon where no_permohonan='" + lblTitle.Text + "' and Status='A' ");

            if (ddokdicno.Rows.Count != 0)

            {

                string Inssql_main = "Update KW_Pembayaran_mohon set Status='T' where no_permohonan='" + lblTitle.Text + "' and Status='A'";

                Status = DBCon.Ora_Execute_CommamdText(Inssql_main);

                if (Status == "SUCCESS")

                {

                    string Inssql_item = "Update KW_Pembayaran_mohon_item set Status='T' where mhn_no_permohonan='" + lblTitle.Text + "' and Status='A'";

                    Status = DBCon.Ora_Execute_CommamdText(Inssql_item);

                    BindMohon();

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Daftar Mohon Bayar Dihapuskan Berjaya',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                }

            }

        }

        catch

        {



        }

    }

    protected void gridmohdup_lnkView_Click(object sender, EventArgs e)

    {

        try

        {

            string ss1 = string.Empty;

            LinkButton btnButton = sender as LinkButton;

            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;

            DropDownList kepada_cd = (DropDownList)gvRow.FindControl("gridmohdup_ddakaun");

            DataTable ddokdicno = new DataTable();


            Label16.Text = "Pembekal";

            ss1 = "pem";

            ddokdicno = DBCon.Ora_Execute_table("select * from KW_Ref_Pembekal where Status='A' and Ref_nama_syarikat='" + TextBox26.Text + "'");



            if (ddokdicno.Rows.Count != 0)

            {

                pel_Button4.Text = "Kemaskini";

                pel_ver_id.Text = "1";

                pel_Button1.Visible = false;

                pel_get_id.Text = ddokdicno.Rows[0]["Id"].ToString();

                if (ddokdicno.Rows[0]["" + ss1 + "_pelbagai"].ToString() == "1")

                {

                    chk_pel.Checked = true;

                }

                else

                {

                    chk_pel.Checked = false;

                }

                pel_TextBox1.Text = ddokdicno.Rows[0]["Ref_nama_syarikat"].ToString();

                pel_TextBox2.Text = ddokdicno.Rows[0]["Ref_no_fax"].ToString();

                pel_TextBox4.Text = ddokdicno.Rows[0]["Ref_no_telefone"].ToString();

                pel_TextBox5.Text = ddokdicno.Rows[0]["Ref_no_syarikat"].ToString();

                pel_TextBox3.Text = ddokdicno.Rows[0]["Ref_email"].ToString();

                pel_TextBox6.Text = ddokdicno.Rows[0]["Ref_gst_id"].ToString();

                pel_TextBox7.Text = ddokdicno.Rows[0]["Ref_alamat"].ToString();

                pel_TextBox8.Text = ddokdicno.Rows[0]["Ref_kawalan"].ToString();

                pel_TextBox10.Text = ddokdicno.Rows[0]["Ref_kod_akaun"].ToString();

                pel_dd_akaun.SelectedValue = ddokdicno.Rows[0]["Ref_jenis_akaun"].ToString();

                pel_DropDownList1.SelectedValue = ddokdicno.Rows[0]["" + ss1 + "_bank_cd"].ToString();

                pel_TextBox12.Text = ddokdicno.Rows[0]["" + ss1 + "_bank_accno"].ToString();

                pel_TextBox11.Text = ddokdicno.Rows[0]["ref_alamat_ked"].ToString();

                pel_TextBox9.Text = ddokdicno.Rows[0]["" + ss1 + "_bandar"].ToString();

                pel_ddnegeri.SelectedValue = ddokdicno.Rows[0]["" + ss1 + "_negeri"].ToString();

                pel_TextBox14.Text = ddokdicno.Rows[0]["" + ss1 + "_poskod"].ToString();

                pel_dd_kodind.SelectedValue = ddokdicno.Rows[0]["" + ss1 + "_kod_industry"].ToString();

                if (ddokdicno.Rows[0]["" + ss1 + "_negera"].ToString() != "")

                {

                    pel_dd_negera.SelectedValue = ddokdicno.Rows[0]["" + ss1 + "_negera"].ToString();

                }

                else

                {

                    pel_dd_negera.SelectedValue = "129";

                }

                ModalPopupExtender1.Show();



            }

        }

        catch

        {



        }

    }

}