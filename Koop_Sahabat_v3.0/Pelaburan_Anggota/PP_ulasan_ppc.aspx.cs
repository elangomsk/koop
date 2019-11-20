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


public partial class PP_ulasan_ppc : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string str;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid, radiobtn1, radiobtn2;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                
                txtkooptarik.Attributes.Add("readonly", "readonly");
                txtkooptarik.Attributes.Add("style", "background-color:white");
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
              
                dropkoop();
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    DataTable ddokdicno = new DataTable();
                    ddokdicno = DBCon.Ora_Execute_table("select app_new_icno,app_applcn_no from jpa_application where app_applcn_no='" + service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"])) + "'");
                   
                    Txtnokp.Text = ddokdicno.Rows[0]["app_new_icno"].ToString();
                    load_permohon();
                    dd_permohon.SelectedValue = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch_rfd();
                    
                    Button4.Visible = false;
                    //Button6.Visible = false;
                    Button2.Visible = false;
                    Button1.Visible = false;
                    Button5.Visible = true;
                    
                }
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    void load_permohon()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select app_new_icno,app_applcn_no from jpa_application where app_new_icno='" + Txtnokp.Text + "' order by Created_date ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con1);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_permohon.DataSource = dt;
            dd_permohon.DataTextField = "app_applcn_no";
            dd_permohon.DataValueField = "app_applcn_no";
            dd_permohon.DataBind();
            //dd_permohon.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
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
                com.CommandText = "select app_new_icno from jpa_application where app_new_icno like '%' + @Search + '%' group by app_new_icno";
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
                            countryNames.Add(sdr["app_new_icno"].ToString());

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
    protected void btnupd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Txtnokp.Text != "")
            {
               
                    if (txtkoopul.Value != "" && txtkoopnama.Text != "" && ddlkoopjawa.SelectedValue != "" && txtkooptarik.Text != "")
                    {
                        DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select app_new_icno,app_applcn_no from jpa_application where app_applcn_no='" + dd_permohon.SelectedValue + "'");
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PPUlasan";
                    cmd.Parameters.Add("@avi_new_icno", SqlDbType.Char).Value = ddokdicno.Rows[0]["app_new_icno"].ToString();
                    cmd.Parameters.Add("@avi_applcn_no", SqlDbType.Char).Value = ddokdicno.Rows[0]["app_applcn_no"].ToString();
                
                radiobtn1 = "";

                radiobtn2 = "";
                
                //string nametxt1 = txtaimnama.Text;
                //DateTime dt1 = DateTime.ParseExact(txttarik.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime dt2 = DateTime.ParseExact(txtkooptarik.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DBCon.Execute_CommamdText("UPDATE jpa_application_visit SET avi_biz_own_type_ind='" + radiobtn1 + "',avi_biz_progress_ind='" + radiobtn2 + "',avi_income_amt='" + txtrbb.Text + "',avi_aim_remark='" + txtulusan.Value + "',avi_aim_name='" + nametxt1 + "',avi_aim_post='" + ddlaimjawa.SelectedValue + "',avi_aim_dt='" + dt1.ToString("yyyy-MM-dd") + "',avi_koop_remark='" + txtkoopul.Value + "',avi_koop_name='" + txtkoopnama.Text + "',avi_koop_post='" + ddlkoopjawa.SelectedValue + "',avi_koop_dt='" + dt2.ToString("yyyy-MM-dd") + "'  WHERE avi_applcn_no='" + ddokdicno.Rows[0]["app_applcn_no"].ToString() + "'");
                cmd.Parameters.Add("@avi_biz_own_type_ind", SqlDbType.Char).Value = radiobtn1;
                cmd.Parameters.AddWithValue("@avi_biz_progress_ind", SqlDbType.Char).Value = radiobtn2;
                cmd.Parameters.Add("@avi_income_amt ", SqlDbType.VarChar).Value = "";
                string userid = Session["New"].ToString();
                cmd.Parameters.AddWithValue("@avi_crt_id", SqlDbType.VarChar).Value = userid;
                cmd.Parameters.AddWithValue("@avi_crt_dt", SqlDbType.VarChar).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                cmd.Parameters.AddWithValue("@avi_aim_remark", SqlDbType.VarChar).Value = "";
                cmd.Parameters.AddWithValue("@avi_aim_name", SqlDbType.VarChar).Value ="";
                cmd.Parameters.AddWithValue("@avi_aim_post", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.AddWithValue("@avi_aim_dt", SqlDbType.DateTime).Value = "";
                    //string datedari = "";

                    //DateTime dt = DateTime.ParseExact(datedari, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    //String datetime = dt.ToString("yyyy-mm-dd");

                    //cmd.Parameters.AddWithValue("@avi_aim_dt", SqlDbType.VarChar).Value = datetime;


                    cmd.Parameters.AddWithValue("@avi_koop_remark", SqlDbType.VarChar).Value = txtkoopul.Value;
                cmd.Parameters.AddWithValue("@avi_koop_name", SqlDbType.VarChar).Value = txtkoopnama.Text;
                cmd.Parameters.AddWithValue("@avi_koop_post", SqlDbType.VarChar).Value = ddlkoopjawa.SelectedItem.Value;

                string datedari1 = txtkooptarik.Text;

                DateTime dt1 = DateTime.ParseExact(datedari1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                String datetime1 = dt1.ToString("yyyy-mm-dd");

                cmd.Parameters.AddWithValue("@avi_koop_dt", SqlDbType.VarChar).Value = datetime1;

                cmd.Parameters.AddWithValue("@updid", SqlDbType.VarChar).Value = "0";


               
                cmd.Connection = con;
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            Session["validate_success"] = "SUCCESS";
                            Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                            Response.Redirect("../Pelaburan_Anggota/PP_ulasan_ppc_view.aspx");
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            con.Close();
                            con.Dispose();
                            //clear();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Ulasan Pegawai (Koperasi).',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void bind_permohon(object sender, EventArgs e)
    {
        if (dd_permohon.SelectedValue != "")
        {
            srch_rfd();
        }
    }

    void srch_rfd()
    {
        if (Txtnokp.Text != "")
        {
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select app_name,app_applcn_no,wilayah_name,cawangan_name from jpa_application as ja left join ref_cawangan as rc on rc.cawangan_code=ja.app_branch_cd and rc.wilayah_code=ja.app_region_cd where app_applcn_no='" + dd_permohon.SelectedValue + "'");
            if (ddokdicno.Rows.Count != 0)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Permohonan Telah Diluluskan. Pindaan Maklumat Permohonan Tidak Dibenarkan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                txtnama.Text = ddokdicno.Rows[0]["app_name"].ToString();
               
                DataTable ddokdicno_vis = new DataTable();
                ddokdicno_vis = DBCon.Ora_Execute_table("select * from jpa_application_visit where avi_applcn_no='" + dd_permohon.SelectedValue + "'");
                if (ddokdicno_vis.Rows.Count != 0)
                {
                  
                    txtkoopul.Value = ddokdicno_vis.Rows[0]["avi_koop_remark"].ToString();
                    txtkoopnama.Text = ddokdicno_vis.Rows[0]["avi_koop_name"].ToString();
                    ddlkoopjawa.SelectedValue = ddokdicno_vis.Rows[0]["avi_koop_post"].ToString();
                    txtkooptarik.Text = DateTime.Parse(ddokdicno_vis.Rows[0]["avi_koop_dt"].ToString()).ToString("dd/MM/yyyy");
                    Button2.Visible = false;
                    Button5.Visible = true;
                }
                else
                {
                    txtkoopul.Value = "";
                    txtkoopnama.Text = "";
                    ddlkoopjawa.SelectedValue = "";
                    txtkooptarik.Text = "";
                    Button2.Visible = true;
                    Button5.Visible = false;

                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
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
            load_permohon();
            srch_rfd();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    //void dropaim()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        SqlConnection con = new SqlConnection(cs);
    //        string com = "select jawatan_code,jawatan_desc From Ref_Jawatan where status='A' order by Jawatan_desc ";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        ddlaimjawa.DataSource = dt;
    //        ddlaimjawa.DataBind();
    //        ddlaimjawa.DataTextField = "jawatan_desc";
    //        ddlaimjawa.DataValueField = "jawatan_code";
    //        ddlaimjawa.DataBind();
    //        ddlaimjawa.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    void dropkoop()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string com = "select jawatan_code,jawatan_desc From Ref_Jawatan where status='A' order by Jawatan_desc ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlkoopjawa.DataSource = dt;
            ddlkoopjawa.DataBind();
            ddlkoopjawa.DataTextField = "jawatan_desc";
            ddlkoopjawa.DataValueField = "jawatan_code";
            ddlkoopjawa.DataBind();
            ddlkoopjawa.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void btnsmmit_Click(object sender, EventArgs e)
     {
        if (Txtnokp.Text != "")
        {
           
                if (txtkoopul.Value != "" && txtkoopnama.Text != "" && ddlkoopjawa.SelectedValue != "" && txtkooptarik.Text != "")
                {
                    DataTable Dt_ic = new DataTable();
                    Dt_ic = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application where app_applcn_no='" + dd_permohon.SelectedValue + "'");
                    DataTable Dt = new DataTable();
                    Dt = DBCon.Ora_Execute_table("select avi_applcn_no from jpa_application_visit where avi_applcn_no='" + Dt_ic.Rows[0]["app_applcn_no"] + "'");
                    string appno = Dt_ic.Rows[0]["app_applcn_no"].ToString();
                    if (Dt.Rows.Count == 0)
                    {
                        SqlConnection con = new SqlConnection(cs);
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.CommandText = "PPUlasan";
                    cmd1.Parameters.Add("@avi_new_icno", SqlDbType.Char).Value = Txtnokp.Text;
                    cmd1.Parameters.Add("@avi_applcn_no", SqlDbType.Char).Value = appno;

                       
                        radiobtn1 = "";
                        
                        cmd1.Parameters.Add("@avi_biz_own_type_ind", SqlDbType.Char).Value = radiobtn1;

                        radiobtn2 = "";
                       

                        cmd1.Parameters.AddWithValue("@avi_biz_progress_ind", SqlDbType.Char).Value = radiobtn2;
                        cmd1.Parameters.Add("@avi_income_amt ", SqlDbType.VarChar).Value = "";
                        string userid = Session["New"].ToString();
                        cmd1.Parameters.AddWithValue("@avi_crt_id", SqlDbType.VarChar).Value = userid;
                        cmd1.Parameters.AddWithValue("@avi_crt_dt", SqlDbType.VarChar).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                        cmd1.Parameters.AddWithValue("@avi_aim_remark", SqlDbType.VarChar).Value ="";
                        cmd1.Parameters.AddWithValue("@avi_aim_name", SqlDbType.VarChar).Value = "";
                        cmd1.Parameters.AddWithValue("@avi_aim_post", SqlDbType.VarChar).Value = "";
                    cmd1.Parameters.AddWithValue("@avi_aim_dt", SqlDbType.DateTime).Value = "";
                    cmd1.Parameters.AddWithValue("@avi_koop_remark", SqlDbType.VarChar).Value = txtkoopul.Value;
                        cmd1.Parameters.AddWithValue("@avi_koop_name", SqlDbType.VarChar).Value = txtkoopnama.Text;
                        cmd1.Parameters.AddWithValue("@avi_koop_post", SqlDbType.VarChar).Value = ddlkoopjawa.SelectedItem.Value;
                        string datedari1 = txtkooptarik.Text;
                        DateTime dt1 = DateTime.ParseExact(datedari1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        String datetime1 = dt1.ToString("yyyy-mm-dd");
                        cmd1.Parameters.AddWithValue("@avi_koop_dt", SqlDbType.VarChar).Value = datetime1;
                        cmd1.Parameters.AddWithValue("@updid", SqlDbType.VarChar).Value = "1";
                        cmd1.Connection = con;
                        try
                        {
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            Session["validate_success"] = "SUCCESS";
                            Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                            Response.Redirect("../Pelaburan_Anggota/PP_ulasan_ppc_view.aspx");
                            

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            con.Close();
                            con.Dispose();
                            //clear();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Ic No Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Ulasan Pegawai (Koperasi).',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
           
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No KP Baru.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("PP_ulasan_ppc.aspx");
    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_ulasan_ppc_view.aspx");
    }
}