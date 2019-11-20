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


public partial class PP_Guaman_cagaran : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);

    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid, stscd, abc1, abc2;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp_req = Request.UrlReferrer;
                if (samp_req != null)
                {
                    ViewState["ReferrerUrl"] = Request.UrlReferrer.ToString();
                }
                else
                {
                    ViewState["ReferrerUrl"] = "";
                }
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                txtname.Attributes.Add("Readonly", "Readonly");
                txtnokp.Attributes.Add("Readonly", "Readonly");
                //txtwila.Attributes.Add("Readonly", "Readonly");
                //txtcawa.Attributes.Add("Readonly", "Readonly");
                txtamaun.Attributes.Add("Readonly", "Readonly");
                txttemp.Attributes.Add("Readonly", "Readonly");

                txttn16d.Attributes.Add("Readonly", "Readonly");
                txttn16g.Attributes.Add("Readonly", "Readonly");
                txttp.Attributes.Add("Readonly", "Readonly");
                txttpt.Attributes.Add("Readonly", "Readonly");
                txttl2.Attributes.Add("Readonly", "Readonly");
                txttlg1.Attributes.Add("Readonly", "Readonly");
                txttlg2.Attributes.Add("Readonly", "Readonly");


                negriBind1();
                hubungan();
                pengenalan();
                Keputusan();
                Button7.Visible = false;
                var samp = Request.Url.Query;

                if (samp != "")
                {
                    if (service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"])) != "")
                    {

                        txtappno.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    }
                    srch();
                }
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
    void negriBind1()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string com = "select Decription,Decription_Code from Ref_Negeri  group by Decription,Decription_Code order by Decription,Decription_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlneg.DataSource = dt;
            ddlneg.DataBind();
            ddlneg.DataTextField = "Decription";
            ddlneg.DataValueField = "Decription_Code";
            ddlneg.DataBind();
            ddlneg.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            ddl1.DataSource = dt;
            ddl1.DataBind();
            ddl1.DataTextField = "Decription";
            ddl1.DataValueField = "Decription_Code";
            ddl1.DataBind();
            ddl1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Keputusan()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "SELECT keputusan_cd,keputusan_desc FROM ref_keputusan_lelongan WHERE Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlkl.DataSource = dt;
            ddlkl.DataBind();
            ddlkl.DataTextField = "keputusan_desc";
            ddlkl.DataValueField = "keputusan_cd";
            ddlkl.DataBind();
            ddlkl.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void hubungan()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string com = "select Contact_Code,Contact_Name from ref_hubungan where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlhubu.DataSource = dt;
            ddlhubu.DataBind();
            ddlhubu.DataTextField = "Contact_Name";
            ddlhubu.DataValueField = "Contact_Code";
            ddlhubu.DataBind();
            ddlhubu.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void pengenalan()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string com = "select Description,Description_Code from Ref_Jenis_Pengenalan where status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddljp.DataSource = dt;
            ddljp.DataBind();
            ddljp.DataTextField = "Description";
            ddljp.DataValueField = "Description_Code";
            ddljp.DataBind();
            ddljp.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void btnsrch_Click(object sender, EventArgs e)
    {
        srch();
        grid();
        grid1();
    }

    protected void srch()
    {
        try
        {
            if (txtappno.Text != "")
            {

                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select ja.app_name,ja.app_new_icno,rc.branch_desc,rw.Wilayah_Name,ja.app_loan_amt,ja.appl_loan_dur,ja.app_applcn_no from jpa_application as ja left join jpa_jkkpa_approval as jk on jk.jkk_applcn_no=ja.app_applcn_no left join ref_branch as rc on rc.branch_cd=ja.app_branch_cd left join ref_wilayah as rw on rw.Wilayah_Code=ja.app_region_cd  where app_applcn_no='" + txtappno.Text + "' and jkk_result_ind='L'");
                if (ddokdicno.Rows.Count != 0)
                {
                    string appno;
                    txtname.Text = ddokdicno.Rows[0][0].ToString();
                    txtnokp.Text = ddokdicno.Rows[0][1].ToString();
                    //txtcawa.Text = ddokdicno.Rows[0][2].ToString();
                    //txtwila.Text = ddokdicno.Rows[0][3].ToString();
                    decimal amt1 = decimal.Parse(ddokdicno.Rows[0][4].ToString());
                    txtamaun.Text = amt1.ToString("C").Replace("RM", "").Replace("$", "");
                    txttemp.Text = ddokdicno.Rows[0][5].ToString();
                    appno = ddokdicno.Rows[0][6].ToString();
                    DataTable ddokdicno1 = new DataTable();
                    ddokdicno1 = DBCon.Ora_Execute_table("select cag_preserve_ind,cag_grant_no,cag_grant_own_type_cd,cag_property_value,cag_lawyer_name,cag_apply_dt,cag_trial_dt,cag_trial_result,cag_notice_16d_dt,cag_notice_16g_dt,cag_property_address,cag_property_postcode,cag_property_state_cd,cag_approve_auction_dt,cag_release_auction_dt from jpa_preserve as jp left join jpa_application as ja on ja.app_applcn_no=jp.cag_applcn_no where cag_applcn_no='" + txtappno.Text + "'");
                    if (ddokdicno1.Rows.Count != 0)
                    {
                        string zzz;
                        zzz = Convert.ToDecimal(ddokdicno1.Rows[0][0]).ToString();
                        if (zzz == "0")
                        {
                            rb1.Checked = true;
                        }
                        else if (zzz == "1")
                        {
                            rb2.Checked = true;
                        }
                        else
                        {
                            //rb1.Checked = false;
                            //rb2.Checked = false;
                        }

                        string xyz;
                        xyz = Convert.ToDecimal(ddokdicno1.Rows[0][2]).ToString();
                        if (xyz == "0")
                        {
                            rbjmg1.Checked = true;
                        }
                        else if (xyz == "1")
                        {
                            rbjmg2.Checked = true;
                        }
                        else
                        {
                            //rbjmg1.Checked = false;
                            //rbjmg2.Checked = false;
                        }

                        txtng.Text = ddokdicno1.Rows[0][1].ToString();
                        //ddljmg.SelectedValue = ddokdicno1.Rows[0][2].ToString(); // add theis to sql query
                        decimal amt1A = decimal.Parse(ddokdicno1.Rows[0][3].ToString());
                        txtnh.Text = amt1A.ToString("C").Replace("RM", "").Replace("$", "");

                        txtpp.Text = ddokdicno1.Rows[0][4].ToString();
                        txttpt.Text = Convert.ToDateTime(ddokdicno1.Rows[0][5]).ToString("dd/MM/yyyy");
                        txttp.Text = Convert.ToDateTime(ddokdicno1.Rows[0][6]).ToString("dd/MM/yyyy");
                        txtareakp.Value = ddokdicno1.Rows[0][7].ToString();
                        txttn16d.Text = Convert.ToDateTime(ddokdicno1.Rows[0][8]).ToString("dd/MM/yyyy");
                        txttn16g.Text = Convert.ToDateTime(ddokdicno1.Rows[0][9]).ToString("dd/MM/yyyy");
                        txtareaalmt.Value = ddokdicno1.Rows[0][10].ToString();
                        ddl1.SelectedValue = ddokdicno1.Rows[0][12].ToString();
                        txttlg1.Text = Convert.ToDateTime(ddokdicno1.Rows[0][13]).ToString("dd/MM/yyyy");
                        txttlg2.Text = Convert.ToDateTime(ddokdicno1.Rows[0][14]).ToString("dd/MM/yyyy");
                        txtpk.Text = ddokdicno1.Rows[0][11].ToString();
                        con1.Open();
                        SqlCommand cmd2 = new SqlCommand("select pro_name,pro_icno,rp.Description,pro_address,pro_postcode,ng.Decription,pro_phone,rh.Contact_Name from jpa_property_auction as jpa left join Ref_Negeri as ng on ng.Decription_Code=jpa.pro_state_cd left join ref_hubungan as rh on rh.Contact_Code=jpa.pro_relation_cd left join Ref_Jenis_Pengenalan as rp on rp.Description_Code=jpa.pro_ic_type_cd where pro_applcn_no='" + txtappno.Text + "'", con1);
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
                        con1.Close();
                        con1.Open();
                        SqlCommand cmd3 = new SqlCommand("select auc_dt,auc_seq_no,auc_venue,auc_value_amt,rkl.keputusan_desc from jpa_auction as ja left join ref_keputusan_lelongan as rkl on rkl.keputusan_cd=ja.auc_result_ind where auc_applcn_no='" + txtappno.Text + "'", con1);
                        SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                        DataSet ds3 = new DataSet();
                        da3.Fill(ds3);
                        if (ds3.Tables[0].Rows.Count == 0)
                        {

                            ds3.Tables[0].Rows.Add(ds3.Tables[0].NewRow());
                            GridView2.DataSource = ds3;
                            GridView2.DataBind();
                            int columncount1 = GridView2.Rows[0].Cells.Count;
                            GridView2.Rows[0].Cells.Clear();
                            GridView2.Rows[0].Cells.Add(new TableCell());
                            GridView2.Rows[0].Cells[0].ColumnSpan = columncount1;
                            GridView2.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
                        }
                        else
                        {
                            GridView2.DataSource = ds3;
                            GridView2.DataBind();
                        }
                        con1.Close();




                    }
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
                    //}
                }
                else
                {
                   
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

                }
            }
            else
            {
               
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No IC.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PP_Guaman_cagaran.aspx");
    }

    void grid()
    {
        con1.Open();
        SqlCommand cmd = new SqlCommand("select pro_icno,RJP.Description,pro_name,pro_phone,RH.Contact_Name From jpa_property_auction as JA  inner join Ref_Hubungan as RH on RH.Contact_Code=JA.pro_relation_cd inner join Ref_Jenis_Pengenalan as RJP on RJP.Description_Code=JA.pro_ic_type_cd  where pro_applcn_no='" + txtappno.Text + "' order by pro_crt_dt desc", con1);
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
        }
        else
        {

            GridView1.DataSource = ds;
            GridView1.DataBind();

        }
        con1.Close();
    }

    void grid1()
    {
        con1.Open();
        SqlCommand cmd = new SqlCommand("select auc_dt,auc_seq_no,auc_venue,auc_value_amt,keputusan_desc  From jpa_auction as JA inner join ref_keputusan_lelongan as RKL on RKL.keputusan_cd=JA.auc_result_ind where auc_applcn_no='" + txtappno.Text + "' ORDER BY auc_seq_no DESC", con1);
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
        }
        else
        {

            GridView2.DataSource = ds;
            GridView2.DataBind();

        }
        con1.Close();
    }

    protected void btntmpa_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtappno.Text != "" && txtnama1.Text != "" && txtnokp1.Text != "" && txttp1.Text != "")
            {
                DataTable ddokdicno2 = new DataTable();
                ddokdicno2 = DBCon.Ora_Execute_table("select * from jpa_property_auction  where pro_applcn_no='" + txtappno.Text + "' and pro_icno='" + txtnokp1.Text + "'");
                if (ddokdicno2.Rows.Count == 0)
                {
                    DataTable ddokdicno3 = new DataTable();
                    string mpval = txtnama1.Text;
                    string mval = mpval.Replace("'", "''");
                    string mpval1 = txtareaalamt.Value;
                    string mval1 = mpval1.Replace("'", "''");
                    ddokdicno3 = DBCon.Ora_Execute_table("insert into jpa_property_auction(pro_applcn_no,pro_icno,pro_ic_type_cd,pro_name,pro_address,pro_postcode,pro_state_cd,pro_phone,pro_relation_cd,pro_crt_id,pro_crt_dt) values('" + txtappno.Text + "','" + txtnokp1.Text + "','" + ddljp.SelectedValue + "','" + mval + "','" + mval1 + "','" + txtpk1.Text + "','" + ddlneg.SelectedValue + "','" + txttp1.Text + "','" + ddlhubu.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                    DataTable ddaudit = new DataTable();
                    GridView1.DataBind();
                   
                    txtnama1.Text = "";
                    txtnokp1.Text = "";
                    txtareaalamt.Value = "";
                    ddljp.SelectedValue = "";
                    txtpk1.Text = "";
                    txttp1.Text = "";
                    ddlneg.SelectedValue = "";
                    ddlhubu.SelectedValue = "";
                   // Button5.Visible = true;
                    Button7.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                   

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('sila masukkan bidang marked merah.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }

        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
        grid();
        grid1();
    }

    protected void btntmpa1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtappno.Text != "")
            {
                if (txttl1.Text != "" && txttl2.Text != "" && txtal1.Text != "" && txtal2.Text != "" && ddlkl.SelectedValue != "")
                {
                    DataTable ddokdicno2 = new DataTable();
                    ddokdicno2 = DBCon.Ora_Execute_table("select * from jpa_auction  where auc_applcn_no='" + txtappno.Text + "'");
                    if (ddokdicno2.Rows.Count != 0)
                    {

                        DataTable ddokdicno4 = new DataTable();
                        ddokdicno4 = DBCon.Ora_Execute_table("select MAX(auc_seq_no) from jpa_auction  where auc_applcn_no='" + txtappno.Text + "' group by auc_seq_no order by auc_seq_no desc");
                        string maxno;
                        maxno = ddokdicno4.Rows[0][0].ToString();

                        DataTable ddokdicno3 = new DataTable();
                        string fdate = txttl2.Text;
                        DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        String fmdate = ft.ToString("mm/dd/yyyy");

                        int incNumber = Convert.ToInt32(maxno);
                        incNumber++;
                        string nyNumber = incNumber.ToString("0");


                        ddokdicno3 = DBCon.Ora_Execute_table("insert into jpa_auction (auc_applcn_no,auc_seq_no,auc_venue,auc_dt,auc_agency_name,auc_value_amt,auc_beat_amt,auc_result_ind,auc_crt_dt,auc_crt_id) values('" + txtappno.Text + "','" + incNumber + "','" + txttl1.Text + "','" + fmdate + "','" + txtal1.Text + "','" + txtnl1.Text + "','" + txtal2.Text + "','" + ddlkl.SelectedValue + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Session["New"].ToString() + "')");
                        DataTable ddaudit = new DataTable();
                        GridView2.DataBind();
                       
                        txttl1.Text = "";
                        txttl2.Text = "";
                        txtal1.Text = "";
                        txtnl1.Text = "";
                        txtal2.Text = "";
                        ddlkl.SelectedValue = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                    }
                    else
                    {
                        DataTable ddokdicno3 = new DataTable();
                        string fdate = txttl2.Text;
                        DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        String fmdate = ft.ToString("mm/dd/yyyy");

                        int incNumber = 0;
                        string nyNumber = incNumber.ToString("0");
                        incNumber++;

                        ddokdicno3 = DBCon.Ora_Execute_table("insert into jpa_auction (auc_applcn_no,auc_seq_no,auc_venue,auc_dt,auc_agency_name,auc_value_amt,auc_beat_amt,auc_result_ind,auc_crt_dt,auc_crt_id) values('" + txtappno.Text + "','" + nyNumber + "','" + txttl1.Text + "','" + fmdate + "','" + txtal1.Text + "','" + txtnl1.Text + "','" + txtal2.Text + "','" + ddlkl.SelectedValue + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Session["New"].ToString() + "')");
                        DataTable ddaudit = new DataTable();
                        GridView2.DataBind();
                    
                        txttl1.Text = "";
                        txttl2.Text = "";
                        txtal1.Text = "";
                        txtnl1.Text = "";
                        txtal2.Text = "";
                        ddlkl.SelectedValue = "";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod Sudah Wujud')", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('sila masukkan bidang Marked merah.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No IC.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
        grid();
        grid1();
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label3");
        string abc = lblTitle.Text;

        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select leg_applcn_no,leg_plaintif_cat_cd from jpa_legal_action where leg_applcn_no='" + txtappno.Text + "'");
        stscd = ddokdicno.Rows[0][1].ToString();
        if (ddokdicno.Rows.Count != 0)
        {
            string appno;
            appno = ddokdicno.Rows[0][0].ToString();
            DataTable ddokdicno1 = new DataTable();
            ddokdicno1 = DBCon.Ora_Execute_table("select pro_name,pro_icno,pro_address,pro_ic_type_cd,pro_postcode,pro_phone,pro_state_cd,pro_relation_cd from jpa_property_auction where pro_icno='" + lblTitle.Text + "'");
            if (ddokdicno1.Rows.Count != 0)
            {
                txtnama1.Text = ddokdicno1.Rows[0][0].ToString();
                txtnokp1.Text = ddokdicno1.Rows[0][1].ToString();
                txtareaalamt.Value = ddokdicno1.Rows[0][2].ToString();
                ddljp.SelectedValue = ddokdicno1.Rows[0][3].ToString();
                txtpk1.Text = ddokdicno1.Rows[0][4].ToString();
                txttp1.Text = ddokdicno1.Rows[0][5].ToString();
                ddlneg.SelectedValue = ddokdicno1.Rows[0][6].ToString();
                ddlhubu.SelectedValue = ddokdicno1.Rows[0][7].ToString();
                Button5.Visible = false;
                Button7.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
      
        grid();
        grid1();
    }


    protected void lnkView_Click2(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        //System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label3");
        System.Web.UI.WebControls.Label sqno = (System.Web.UI.WebControls.Label)gvRow.FindControl("seqno");
        //string abc = lblTitle.Text;

        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from jpa_auction where auc_applcn_no='" + txtappno.Text + "' and auc_seq_no ='" + sqno.Text + "'");
        //stscd = ddokdicno.Rows[0][1].ToString();
        if (ddokdicno.Rows.Count != 0)
        {
            string appno;
            appno = ddokdicno.Rows[0][0].ToString();
            DataTable ddokdicno1 = new DataTable();
            ddokdicno1 = DBCon.Ora_Execute_table("select auc_applcn_no,auc_seq_no,auc_venue,auc_agency_name,auc_value_amt,auc_beat_amt,auc_result_ind,FORMAT(auc_dt,'dd/MM/yyyy', 'en-us') as auc_dt from jpa_auction where auc_applcn_no='" + txtappno.Text + "' and auc_seq_no ='" + sqno.Text + "'");
            if (ddokdicno1.Rows.Count != 0)
            {
                txttl1.Text = ddokdicno1.Rows[0]["auc_venue"].ToString();

                txttl2.Text = ddokdicno1.Rows[0]["auc_dt"].ToString();
                txtal1.Text = ddokdicno1.Rows[0]["auc_agency_name"].ToString();
                decimal amt1 = decimal.Parse(ddokdicno1.Rows[0]["auc_value_amt"].ToString());
                txtnl1.Text = amt1.ToString("C").Replace("RM", "").Replace("$", "");
                decimal amt2 = decimal.Parse(ddokdicno1.Rows[0]["auc_beat_amt"].ToString());
                txtal2.Text = amt2.ToString("C").Replace("RM", "").Replace("$", "");
                ddlkl.SelectedValue = ddokdicno1.Rows[0]["auc_result_ind"].ToString();
                sqno1.Text = ddokdicno1.Rows[0]["auc_seq_no"].ToString();
                Button6.Visible = false;
                Button8.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        grid();
        grid1();
    }

    protected void btnkem_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtappno.Text != "")
            {
                DataTable ddokdicno2 = new DataTable();
                ddokdicno2 = DBCon.Ora_Execute_table("select * from jpa_auction  where auc_applcn_no='" + txtappno.Text + "'");
                if (ddokdicno2.Rows.Count != 0)
                {
                    DataTable ddokdicno4 = new DataTable();
                    ddokdicno4 = DBCon.Ora_Execute_table("select * from jpa_auction  where auc_applcn_no='" + txtappno.Text + "' and auc_seq_no='" + sqno1.Text + "'");

                    if (ddokdicno4.Rows.Count != 0)
                    {
                        string fdate1 = txttl2.Text;
                        DateTime ft1 = DateTime.ParseExact(fdate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        String fmdate1 = ft1.ToString("yyyy-MM-dd");
                        DataTable ddokdicno3 = new DataTable();
                        ddokdicno3 = DBCon.Ora_Execute_table("UPDATE jpa_auction SET auc_venue='" + txttl1.Text + "',auc_dt='" + fmdate1 + "',auc_agency_name='" + txtal1.Text + "',auc_value_amt='" + txtnl1.Text + "',auc_beat_amt='" + txtal2.Text + "',auc_result_ind='" + ddlkl.SelectedValue + "',auc_upd_id='" + Session["New"].ToString() + "',auc_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE  auc_applcn_no='" + txtappno.Text + "' and auc_seq_no='" + sqno1.Text + "'");
                        DataTable ddaudit = new DataTable();
                        GridView2.DataBind();
                        grid1();
                        txttl1.Text = "";
                        txttl2.Text = "";
                        txtal1.Text = "";
                        txtnl1.Text = "";
                        txtal2.Text = "";
                        ddlkl.SelectedValue = "";
                        Button6.Visible = true;
                        Button8.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No IC.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('ISSUE.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
        grid();
        grid1();
    }



    protected void btnkems1_Click(object sender, EventArgs e)
    {
        DataTable ddokdicno2 = new DataTable();
        ddokdicno2 = DBCon.Ora_Execute_table("select pro_name,pro_icno,pro_address,pro_ic_type_cd,pro_postcode,pro_phone,pro_state_cd,pro_relation_cd from jpa_property_auction where pro_icno='" + txtnokp1.Text + "' ");
        if (ddokdicno2.Rows.Count != 0)
        {
            DataTable ddokdicno3 = new DataTable();

            string mpval = txtnama1.Text;
            string mval = mpval.Replace("'", "''");
            string mpval1 = txtareaalamt.Value;
            string mval1 = mpval1.Replace("'", "''");

            ddokdicno3 = DBCon.Ora_Execute_table("update jpa_property_auction set pro_name='" + mval + "',pro_address='" + mval1 + "',pro_ic_type_cd='" + ddljp.SelectedValue + "',pro_postcode='" + txtpk1.Text + "',pro_phone='" + txttp1.Text + "',pro_state_cd='" + ddlneg.SelectedValue + "',pro_relation_cd='" + ddlhubu.SelectedValue + "',pro_upd_id='" + Session["New"].ToString() + "',pro_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where pro_icno='" + txtnokp1.Text + "'");
            txtnama1.Text = "";
            txtnokp1.Text = "";
            txtareaalamt.Value = "";
            ddljp.SelectedValue = "";
            txtpk1.Text = "";
            txttp1.Text = "";
            ddlneg.SelectedValue = "";
            ddlhubu.SelectedValue = "";
           Button5.Visible = true;
            Button7.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);


            DataTable ddaudit = new DataTable();

        }
        grid();
        grid1();

    }
    protected void click_batal(object sender, EventArgs e)
    {
        Session["sess_no"] = txtappno.Text;
        object referrer = ViewState["ReferrerUrl"];
        if (Session["sess_no"] != "")
            Response.Redirect(referrer.ToString());


    }
    protected void btnsmmit_Click(object sender, EventArgs e)
    {

        DataTable ddokdicno21 = new DataTable();
        ddokdicno21 = DBCon.Ora_Execute_table("select ISNULL(auc_applcn_no ,'') as auc_applcn_no,ISNULL(pro_applcn_no ,'') as pro_applcn_no From jpa_auction as auc inner join jpa_property_auction as pro on pro.pro_applcn_no=auc.auc_applcn_no where auc_applcn_no='" + txtappno.Text + "' group by auc_applcn_no,pro_applcn_no ");



        if (ddokdicno21.Rows.Count != 0)
        {

            if (txttlg1.Text != "" && txttlg2.Text != "" && txtpp.Text != "" && txttn16d.Text != "" && txttn16g.Text != "" && txtareaalmt.Value != "" && txtareakp.Value != "" && txtng.Text != "" && txtnh.Text != "" && txttpt.Text != "" && txttp.Text != "" && txtpk.Text != "" && ddl1.SelectedValue != "")
            {
                string bca;
                if (rb1.Checked == true)
                {
                    bca = "0";
                }
                else if (rb2.Checked == true)
                {
                    bca = "1";
                }
                else
                {
                    bca = "";
                }
                abc1 = bca;

                string bca1;
                if (rbjmg1.Checked == true)
                {
                    bca1 = "0";
                }
                else if (rbjmg2.Checked == true)
                {
                    bca1 = "1";
                }
                else
                {
                    bca1 = "";
                }
                abc2 = bca1;
                //string fd1 = txttlg2.Text;
                DateTime ft1 = DateTime.ParseExact(txttlg2.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                DateTime ft2 = DateTime.ParseExact(txttlg1.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                DateTime ft3 = DateTime.ParseExact(txttp.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                DateTime ft4 = DateTime.ParseExact(txttpt.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                DateTime ft5 = DateTime.ParseExact(txttn16g.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                DateTime ft6 = DateTime.ParseExact(txttn16d.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);

                DataTable ddokdicno2 = new DataTable();
                ddokdicno2 = DBCon.Ora_Execute_table("select * from jpa_preserve  where cag_applcn_no='" + txtappno.Text + "'");
                if (ddokdicno2.Rows.Count == 0)
                {
                    DataTable ddokdicno3 = new DataTable();
                    ddokdicno3 = DBCon.Ora_Execute_table("insert into jpa_preserve (cag_applcn_no,cag_preserve_ind,cag_grant_no,cag_grant_own_type_cd,cag_property_value,cag_property_address,cag_property_postcode,cag_property_state_cd,cag_lawyer_name,cag_notice_16d_dt,cag_notice_16g_dt,cag_apply_dt,cag_trial_dt,cag_trial_result,cag_approve_auction_dt,cag_release_auction_dt,cag_crt_id,cag_crt_dt) values('" + txtappno.Text + "','" + abc1 + "','" + txtng.Text + "','" + abc2 + "','" + txtnh.Text + "','" + txtareaalmt.Value + "','" + txtpk.Text + "','" + ddl1.SelectedValue + "','" + txtpp.Text + "','" + ft6.ToString("yyyy-MM-dd") + "','" + ft5.ToString("yyyy-MM-dd") + "','" + ft4.ToString("yyyy-MM-dd") + "','" + ft3.ToString("yyyy-MM-dd") + "','" + txtareakp.Value + "','" + ft2.ToString("yyyy-MM-dd") + "','" + ft1.ToString("yyyy-MM-dd") + "','" + Session["new"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                    DataTable ddaudit = new DataTable();                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    DataTable ddokdicno3 = new DataTable();
                    ddokdicno3 = DBCon.Ora_Execute_table("update jpa_preserve set cag_preserve_ind='" + abc1 + "',cag_grant_no='" + txtng.Text + "',cag_grant_own_type_cd='" + abc2 + "',cag_property_value='" + txtnh.Text + "',cag_property_address='" + txtareaalmt.Value + "',cag_property_postcode='" + txtpk.Text + "',cag_property_state_cd='" + ddl1.SelectedValue + "',cag_lawyer_name='" + txtpp.Text + "',cag_notice_16d_dt='" + ft6.ToString("yyyy-MM-dd") + "',cag_notice_16g_dt='" + ft5.ToString("yyyy-MM-dd") + "',cag_apply_dt='" + ft4.ToString("yyyy-MM-dd") + "',cag_trial_dt='" + ft3.ToString("yyyy-MM-dd") + "',cag_trial_result='" + txtareakp.Value + "',cag_approve_auction_dt='" + ft2.ToString("yyyy-MM-dd") + "',cag_release_auction_dt='" + ft1.ToString("yyyy-MM-dd") + "',cag_upd_id='" + Session["new"].ToString() + "',cag_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where cag_applcn_no='" + txtappno.Text + "'");
                    DataTable ddaudit = new DataTable();                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('sila masukkan bidang Marked merah.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Pastikan MAKLUMAT KETUANPUNYAAN GERAN dan MAKLUMAT LELONGAN mempunyai atleast Satu Rekod.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

        grid();
        grid1();
    }

  
}