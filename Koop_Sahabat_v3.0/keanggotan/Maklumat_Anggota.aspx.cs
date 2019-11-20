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
using System.Text.RegularExpressions;

public partial class Maklumat_Anggota : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection Con = new DBConnection();
    string level, userid;
    string bcode;
    string wcode;
    string kcode;
    string status;
    string refind;
    string role_1 = string.Empty, role_2 = string.Empty;

    decimal total = 0M;
    DataTable wilayah = new DataTable();
    DataTable caw = new DataTable();
    DataTable pusat = new DataTable();
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
     
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);

        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                negriBind();
                negriBind1();
                negriBind2();
                //TextBox2.Attributes.Add("readonly", "readonly");
                TextBox7.Attributes.Add("readonly", "readonly");
                ddbangsa.Attributes.Add("readonly", "readonly");
                TextBox27.Attributes.Add("readonly", "readonly");
                Txtsa.Attributes.Add("readonly", "readonly");
                TextBox14.Attributes.Add("readonly", "readonly");
                TextBox25.Attributes.Add("readonly", "readonly");
                TextBox24.Attributes.Add("readonly", "readonly");
                Txtsa.Attributes.Add("READONLY", "READONLY");
                TextBox4.Attributes.Add("readonly", "readonly");
                TextBox6.Attributes.Add("readonly", "readonly");
                style.Visible = false;
                string Hubungan_query = "SELECT * FROM Ref_Hubungan ORDER BY Id ASC";
                string cawangan_query = "SELECT * FROM Ref_Cawangan ORDER BY Id ASC";
                string wilayah_query = "select Wilayah_Name,Wilayah_Code from Ref_Wilayah  group by Wilayah_Name,Wilayah_Code order by Wilayah_Name";
                //string kawasan_query = "select kavasan_name from Ref_Cawangan  group by kavasan_name order by kavasan_name asc";

                string bank_det = "SELECT Bank_Name, Bank_Code FROM Ref_Nama_Bank ORDER BY Id ASC";
                string status_ang = "SELECT PERKARA,PERKARA_CODE FROM Ref_Status_Keahlian ORDER BY id ASC";

                using (SqlConnection con = new SqlConnection(cs))
                {


                    using (SqlCommand cmd = new SqlCommand(Hubungan_query))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                ListItem item = new ListItem();
                                item.Text = sdr["Contact_Name"].ToString();
                                item.Value = sdr["Contact_Code"].ToString();
                                //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                                DropDownList2.Items.Add(item);
                                DropDownList1.Items.Add(item);
                            }
                        }
                        con.Close();
                    }

                    using (SqlCommand cmd1 = new SqlCommand(cawangan_query))
                    {
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd1.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                ListItem item = new ListItem();
                                item.Text = sdr["cawangan_name"].ToString();
                                item.Value = sdr["cawangan_code"].ToString();
                                //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                                TextBox9.Items.Add(item);

                            }
                        }
                        con.Close();
                    }
                    using (SqlCommand cmd3 = new SqlCommand(wilayah_query))
                    {
                        cmd3.CommandType = CommandType.Text;
                        cmd3.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd3.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                ListItem item = new ListItem();
                                item.Text = sdr["wilayah_name"].ToString();
                                item.Value = sdr["wilayah_code"].ToString();
                                //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                                TextBox8.Items.Add(item);

                            }
                        }
                        con.Close();
                    }
                    using (SqlCommand cmd4 = new SqlCommand(bank_det))
                    {
                        cmd4.CommandType = CommandType.Text;
                        cmd4.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd4.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                ListItem item = new ListItem();
                                item.Text = sdr["Bank_Name"].ToString();
                                item.Value = sdr["Bank_Code"].ToString();
                                //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                                Bank_details.Items.Add(item);
                            }
                        }
                        con.Close();
                    }

                }

                if (role_1 == "0")
                {
                    TextBox1.Text = userid;
                    TextBox1.Attributes.Add("Readonly", "Readonly");
                    TextBox4.Attributes.Add("Readonly", "Readonly");
                    TextBox4.Attributes.Add("Style", "pointer-events:None;");
                    TextBox6.Attributes.Add("Readonly", "Readonly");
                    TextBox6.Attributes.Add("Style", "pointer-events:None;");
                    kawasan();
                    dash();
                    Button2.Visible = false;
                    Button1.Visible = true;
                    //wilahkawasan();
                }
                else
                {
                    //wilahkawasan();
                    TextBox1.Attributes.Remove("Readonly");
                    //TextBox4.Attributes.Remove("Readonly");
                    //TextBox4.Attributes.Remove("Style");
                    //TextBox6.Attributes.Remove("Readonly");
                    //TextBox6.Attributes.Remove("Style");
                    kawasan();
                }

                Bank_details.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                TextBox9.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                TextBox8.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }

    }

    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where sub_skrin_id='S0047' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_2 = ddokdicno_1.Rows[0]["edit_chk"].ToString();
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
                com.CommandText = "select mem_new_icno from mem_member where Acc_sts='Y' and mem_new_icno like '%' + @Search + '%'";
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
                            countryNames.Add(sdr["mem_new_icno"].ToString());

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
    void app_language()
    {
        if (Session["New"] != null)
        {
            assgn_roles();
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1004','12','13','14','15','16','17','18','19','20','21','22','23','24','25','26','914','915','27','28','29','30','916','31','32','1009', '37', '1006', '34', '1012', '1015', '1017', '1002', '1023', '1006', '1025', '1026', '1027', '1028', '1029', '35', '36', '1022', '1020', '1024')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[27][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[25][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl15.Text = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
            ps_lbl16.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl17.Text = txtinfo.ToTitleCase(gt_lng.Rows[26][0].ToString().ToLower());
            ps_lbl18.Text = txtinfo.ToTitleCase(gt_lng.Rows[39][0].ToString().ToLower());
            ps_lbl19.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl20.Text = txtinfo.ToTitleCase(gt_lng.Rows[34][0].ToString().ToLower());
            ps_lbl21.Text = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower());
            ps_lbl22.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            ps_lbl23.Text = txtinfo.ToTitleCase(gt_lng.Rows[38][0].ToString().ToLower());
            ps_lbl24.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl25.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl26.Text = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower());
            ps_lbl27.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
            ps_lbl28.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl29.Text = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower());
            ps_lbl30.Text = txtinfo.ToTitleCase(gt_lng.Rows[40][0].ToString().ToLower());
            ps_lbl31.Text = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower());
            ps_lbl32.Text = txtinfo.ToTitleCase(gt_lng.Rows[30][0].ToString().ToLower());
            ps_lbl33.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl34.Text = txtinfo.ToTitleCase(gt_lng.Rows[42][0].ToString().ToLower());
            ps_lbl35.Text = txtinfo.ToTitleCase(gt_lng.Rows[37][0].ToString().ToLower());
            ps_lbl36.Text = txtinfo.ToTitleCase(gt_lng.Rows[36][0].ToString().ToLower());
            ps_lbl37.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl38.Text = txtinfo.ToTitleCase(gt_lng.Rows[41][0].ToString().ToLower());
            ps_lbl39.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());
            ps_lbl40.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
            ps_lbl41.Text = txtinfo.ToTitleCase(gt_lng.Rows[35][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    void kawasan()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select kawasan_code,kavasan_name from Ref_Cawangan  group by kawasan_code,kavasan_name order by kavasan_name asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            TextBox18.DataSource = dt;
            TextBox18.DataBind();
            TextBox18.DataTextField = "kavasan_name";

            TextBox18.DataBind();
            TextBox18.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void negriBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string com = "select Decription,Decription_Code from Ref_Negeri  group by Decription,Decription_Code order by Decription,Decription_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlnegri.DataSource = dt;
            ddlnegri.DataBind();
            ddlnegri.DataTextField = "Decription";
            ddlnegri.DataValueField = "Decription_Code";
            ddlnegri.DataBind();
            ddlnegri.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
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
            ddlnegri1.DataSource = dt;
            ddlnegri1.DataBind();
            ddlnegri1.DataTextField = "Decription";
            ddlnegri1.DataValueField = "Decription_Code";
            ddlnegri1.DataBind();
            ddlnegri1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void negriBind2()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string com = "select Decription,Decription_Code from Ref_Negeri  group by Decription,Decription_Code order by Decription,Decription_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlnegri2.DataSource = dt;
            ddlnegri2.DataBind();
            ddlnegri2.DataTextField = "Decription";
            ddlnegri2.DataValueField = "Decription_Code";
            ddlnegri2.DataBind();
            ddlnegri2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void wilahkawasan()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select kavasan_name,kawasan_code from Ref_Cawangan where  kawasan_code='" + kcode + "' group by kavasan_name,kawasan_code ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            TextBox18.DataSource = dt;
            TextBox18.DataTextField = "kavasan_name";
            TextBox18.DataValueField = "kawasan_code";
            TextBox18.DataBind();


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //protected void ddkaw_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if (TextBox18.SelectedItem.Text == "SEMUA WILAYAH")
    //    {
    //        wilayah.Rows.Clear();
    //        TextBox8.Items.Clear();
    //        TextBox8.Items.Insert(0, new ListItem("SEMUA WILAYAH", ""));
    //    }
    //    //-Pusat---------------------------------------------------------------------------------
    //    string cmd6 = "select distinct wilayah_code,wilayah_name from  Ref_Cawangan where kavasan_name='" + TextBox18.SelectedItem.Text + "'";
    //    wilayah.Rows.Clear();
    //    TextBox8.Items.Clear();

    //    con.Open();
    //    SqlDataAdapter adapterP = new SqlDataAdapter(cmd6, con);
    //    adapterP.Fill(wilayah);

    //    TextBox8.DataSource = wilayah;
    //    TextBox8.DataTextField = "wilayah_name";
    //    TextBox8.DataValueField = "wilayah_code";
    //    TextBox8.DataBind();
    //    //ddPusat.Items.RemoveAt(0); //remove 'Semua Wilayah'
    //    con.Close();

    //    TextBox8.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //}

    //protected void ddwil_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (TextBox18.SelectedItem.Text == "SEMUA KAWASAN")
    //    {
    //        pusat.Rows.Clear();
    //        TextBox18.Items.Clear();
    //        TextBox18.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    //-Pusat---------------------------------------------------------------------------------
    //    string cmd5 = "select cawangan_name,cawangan_code from Ref_Cawangan where kavasan_name='" + TextBox18.SelectedItem.Text + "' and wilayah_name='" + TextBox8.SelectedItem.Text + "' ";
    //    pusat.Rows.Clear();
    //    TextBox9.Items.Clear();

    //    con.Open();
    //    SqlDataAdapter adapterP = new SqlDataAdapter(cmd5, con);
    //    adapterP.Fill(pusat);

    //    TextBox9.DataSource = pusat;
    //    TextBox9.DataTextField = "cawangan_name";
    //    TextBox9.DataValueField = "cawangan_code";
    //    TextBox9.DataBind();
    //    //ddPusat.Items.RemoveAt(0); //remove 'Semua Wilayah'
    //    con.Close();

    //    TextBox9.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //}
    void kawasan1()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select kavasan_name,kawasan_code from Ref_Cawangan where  cawangan_code='" + TextBox9.SelectedItem.Value + "' and wilayah_code='" + TextBox8.SelectedItem.Value + "' group by kavasan_name,kawasan_code ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            TextBox18.DataSource = dt;
            TextBox18.DataTextField = "kavasan_name";
            TextBox18.DataValueField = "kawasan_code";
            TextBox18.DataBind();
            //TextBox18.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void kawasan2()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select kavasan_name,kawasan_code from Ref_Cawangan where  wilayah_code='" + TextBox8.SelectedItem.Value + "' group by kavasan_name,kawasan_code ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            TextBox18.DataSource = dt;
            TextBox18.DataTextField = "kavasan_name";
            TextBox18.DataValueField = "kawasan_code";
            TextBox18.DataBind();
            //TextBox18.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Searchbtn_Click(object sender, EventArgs e)
    {
        try
        {
            //style.Attributes.Remove("style", "style");
            //Button3.Visible = true;
            if (TextBox1.Text != "")
            {
                //Button5.Visible = true;
                wilahBind();

                negriBind();
                bangsa();
                style.Visible = true;

                SqlConnection conn = new SqlConnection(cs);
                string query = "    ";
                conn.Open();
                var sqlCommand = new SqlCommand(query, conn);
                var sqlReader = sqlCommand.ExecuteReader();

                if (sqlReader.Read() == true)
                {
                    if (role_2 == "1")
                    {

                        //if ((string)sqlReader["mem_new_icno"] == Session["New"].ToString() && (string)sqlReader["mem_sts_cd"] == "SA")
                        //{
                            lev2.Attributes.Remove("Style");
                            Button3.Attributes.Remove("Style");
                        //}
                        //else
                        //{
                        //    lev2.Attributes.Add("Style", "pointer-events:None; opacity: 0.8;");
                        //    Button3.Attributes.Add("Style", "pointer-events:None; opacity: 0.5;");
                        //}
                    }
                    else
                    {
                        lev2.Attributes.Add("Style", "pointer-events:None; opacity: 0.8;");
                        Button3.Attributes.Add("Style", "pointer-events:None; opacity: 0.5;");
                    }


                    if ((string)sqlReader["mem_nationality_ind"] == "W")
                    {
                        status = "Warganegara";
                    }
                    else if ((string)sqlReader["mem_nationality_ind"] == "N")
                    {
                        status = "Bukan Warganegara";
                    }
                    else
                    {
                        status = "Pemastautin Tetap";
                    }

                    TextBox2.Text = (string)sqlReader["mem_name"];
                    TextBox7.Text = (string)sqlReader["mem_old_icno"];
                    //dd_status.SelectedValue = (string)sqlReader["mem_sts_cd"];
                    TextBox14.Text = status;
                    //TextBox16.Text = Convert.ToDateTime(sqlReader["mem_register_dt"]).ToString("dd/MM/yyyy");
                    if (sqlReader["mem_race_cd"].ToString() != "")
                    {
                        ddbangsa.SelectedValue = sqlReader["mem_race_cd"].ToString();
                    }
                    else
                    {
                        ddbangsa.SelectedValue = "--- PILIH ---";
                    }
                    if ((string)sqlReader["mem_region_cd"] != "")
                    {
                        TextBox8.SelectedValue = (string)sqlReader["mem_region_cd"];
                        wcode = (string)sqlReader["mem_region_cd"];
                    }
                    else
                    {
                        TextBox8.SelectedValue = "";
                        wcode = "";
                    }
                    if ((string)sqlReader["mem_branch_cd"] != "")
                    {
                        DataTable Check_brach = new DataTable();
                        Check_brach = Con.Ora_Execute_table("select * from Ref_Cawangan where cawangan_code='" + sqlReader["mem_branch_cd"].ToString() + "'");
                        if (Check_brach.Rows.Count != 0)
                        {
                            bcode = (string)sqlReader["mem_branch_cd"];
                            TextBox9.SelectedValue = (string)sqlReader["mem_branch_cd"].ToString();
                        }
                    }
                    else
                    {
                        TextBox9.SelectedValue = "";
                        bcode = "";
                    }

                    

                    if (sqlReader["mem_area_cd"].ToString() != "")
                    {
                        DataTable Check = new DataTable();
                        Check = Con.Ora_Execute_table("select * from Ref_Cawangan where kawasan_code='" + sqlReader["mem_area_cd"].ToString() + "'");
                        if (Check.Rows.Count != 0)
                        {
                            TextBox18.SelectedItem.Text = Check.Rows[0]["kavasan_name"].ToString();
                        }
                        else
                        {
                            TextBox18.SelectedItem.Text = "--- PILIH ---";
                        }
                        //kcode = (string)sqlReader["mem_area_cd"];
                        //wilahkawasan();
                    }
                    else if ((string)sqlReader["mem_area_cd"] == "" && TextBox9.SelectedValue != "" && (string)sqlReader["mem_branch_cd"] != "")
                    {
                        //TextBox18.SelectedValue = "";

                        kawasan1();
                    }
                    else if ((string)sqlReader["mem_area_cd"] == "" && TextBox9.SelectedValue == "" && (string)sqlReader["mem_branch_cd"] != "")
                    {
                        //TextBox18.SelectedValue = "";

                        kawasan2();
                    }
                    else
                    {
                        kawasan();
                    }
                    TextBox19.Text = (string)sqlReader["mem_centre"];
                    TextBox5.Text = (string)sqlReader["memphoneh"];
                    TextBox20.Text = (string)sqlReader["memphoneo"];
                    TextBox22.Text = (string)sqlReader["memphonem"];
                    //TextArea1.Value = sqlReader["mem_address"].ToString();

                    //TextArea1.Value = Regex.Replace(sqlReader["mem_address"].ToString(), @"\\r\\r\\n", "");
                    TextArea1.Value = Regex.Replace(sqlReader["mem_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                    string scode = (string)sqlReader["mem_sts_cd"];

                    if (scode == "SA")
                    {
                        Txtsa.Text = "ANGGOTA SAH";
                    }
                    else if (scode == "TS")
                    {
                        Txtsa.Text = "ANGGOTA TIDAK SAH";

                    }
                    else if (scode == "FM")
                    {
                        Txtsa.Text = "FI MASUK";
                    }
                    else
                    {
                        Txtsa.Text = "DALAM NOTIS";
                    }
                    //string addrs = TextArea1.Value.Replace("\r\n", "<br />");
                    //addrs = (string)sqlReader["mem_address"];
                    TextBox3.Text = (string)sqlReader["mem_bank_acc_no"];
                    Bank_details.SelectedValue = (string)sqlReader["mem_bank_cd"];

                    TextBox13.Text = (string)sqlReader["mempospcd"];

                    //if (sqlReader["memnegri"].ToString().Length == 2)
                    //{
                    ddlnegri.SelectedValue = (string)sqlReader["memnegri"].ToString().Trim();
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Your Record is wrong.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    //}
                    if (sqlReader["fee_approval_dt"].ToString() != "")
                    {

                        var feedt = Convert.ToDateTime(sqlReader["fee_approval_dt"]).ToString("dd/MM/yyyy");
                        if (feedt != "01/01/1900")
                        {
                            TextBox4.Text = feedt;
                        }

                    }
                    else
                    {
                        TextBox4.Text = "";
                    }
                    //TextBox6.Text = Convert.ToDateTime(sqlReader["mem_quit_dt"]).ToString("yyyy/MM/dd");
                    //TextBox6.Text = Convert.ToDateTime(sqlReader["mem_quit_dt"]).ToString("dd/MM/yyyy");
                    //string up_date = Convert.ToDateTime(sqlReader["mem_quit_dt"]).ToString("dd/MM/yyyy");
                    //if (up_date == "01/01/1900")
                    //{
                    //    TextBox6.Text = "";
                    //}
                    //else
                    //{
                    //    TextBox6.Text = Convert.ToDateTime(sqlReader["mem_quit_dt"]).ToString("dd/MM/yyyy");
                    //}
                    TextBox27.Text = (string)sqlReader["mem_member_no"];
                    //wilahkawasan();




                    sqlReader.Close();

                    string query1 = "select mw.was_icno,mw.was_name,mw.was_relation_cd,mw.was_phone_no,mw.was_address,mw.was_postcd,mw.was_negri  from mem_member as mm Left join mem_wasi AS mw ON mw.was_new_icno = mm.mem_new_icno and mw.Acc_sts ='Y' where mem_new_icno='" + TextBox1.Text + "' and mm.Acc_sts ='Y' AND mw.was_seqno='1'";
                    var sqlCommand1 = new SqlCommand(query1, conn);
                    var sqlReader1 = sqlCommand1.ExecuteReader();
                    while (sqlReader1.Read())
                    {
                        TextBox11.Text = (string)sqlReader1["was_icno"];
                        TextBox10.Text = (string)sqlReader1["was_name"];
                        DropDownList2.SelectedValue = (string)sqlReader1["was_relation_cd"];
                        TextBox12.Text = (string)sqlReader1["was_phone_no"];
                        //TextArea2.Value = (string)sqlReader1["was_address"];
                        TextArea2.Value = Regex.Replace(sqlReader1["was_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                        TextBox21.Text = (string)sqlReader1["was_postcd"];
                        ddlnegri1.SelectedValue = (string)sqlReader1["was_negri"];
                    }
                    sqlReader1.Close();
                    //string query4 = "select  ISNULL(CASE WHEN CONVERT(DATE, div_approve_dt) = '1900-01-01' THEN '' ELSE CONVERT(CHAR(10), div_approve_dt, 103) END, '') AS div_approve_dt from mem_divident where div_new_icno='" + TextBox1.Text + "'";
                    //var sqlCommand4 = new SqlCommand(query4, conn);
                    //var sqlReader4 = sqlCommand4.ExecuteReader();
                    //while (sqlReader4.Read())
                    //{
                    //    if (sqlReader4["div_approve_dt"].ToString() != "")
                    //    {
                    //        TextBox6.Text = Convert.ToDateTime(sqlReader4["div_approve_dt"]).ToString("dd/MM/yyyy");

                    //    }
                    //    else
                    //    {
                    //        TextBox6.Text = "";
                    //    }
                    //}
                    //sqlReader4.Close();
                    string query4 = "select  ISNULL(CASE WHEN CONVERT(DATE, set_appprove_dt) = '1900-01-01' THEN '' ELSE CONVERT(CHAR(10), set_appprove_dt, 103) END, '') AS set_appprove_dt from mem_settlement where set_new_icno='" + TextBox1.Text + "' and Acc_sts ='Y'";
                    var sqlCommand4 = new SqlCommand(query4, conn);
                    var sqlReader4 = sqlCommand4.ExecuteReader();
                    while (sqlReader4.Read())
                    {
                        if (sqlReader4["set_appprove_dt"].ToString() != "")
                        {
                            TextBox6.Text = (sqlReader4["set_appprove_dt"]).ToString();

                        }
                        else
                        {
                            TextBox6.Text = "";
                        }
                    }
                    sqlReader4.Close();
                    string query5 = "select  CONVERT(varchar(12), ast_st_balance_amt, 1) as ast_st_balance_amt,ast_end_date from aim_st where ast_new_icno='" + TextBox1.Text + "'";
                    var sqlCommand5 = new SqlCommand(query5, conn);
                    var sqlReader5 = sqlCommand5.ExecuteReader();
                    while (sqlReader5.Read())
                    {
                        if (sqlReader5["ast_st_balance_amt"].ToString() != "")
                        {
                            TextBox25.Text = Convert.ToDateTime(sqlReader5["ast_end_date"]).ToString("dd/MM/yyyy");
                            TextBox24.Text = sqlReader5["ast_st_balance_amt"].ToString();
                        }
                        else
                        {
                            TextBox25.Text = "";
                            TextBox24.Text = "0.00";
                        }
                    }
                    sqlReader5.Close();
                    string query2 = "select mw.was_icno,mw.was_name,mw.was_relation_cd,mw.was_phone_no,mw.was_address,mw.was_postcd,mw.was_negri from mem_member as mm Left join mem_wasi AS mw ON mw.was_new_icno = mm.mem_new_icno and mw.Acc_sts ='Y' where mem_new_icno='" + TextBox1.Text + "' and mm.Acc_sts ='Y' AND mw.was_seqno='2'";
                    var sqlCommand2 = new SqlCommand(query2, conn);
                    var sqlReader2 = sqlCommand2.ExecuteReader();
                    while (sqlReader2.Read())
                    {
                        TextBox15.Text = (string)sqlReader2["was_icno"];
                        TextBox17.Text = (string)sqlReader2["was_name"];
                        DropDownList1.SelectedValue = (string)sqlReader2["was_relation_cd"];
                        TextBox23.Text = (string)sqlReader2["was_phone_no"];
                        TextArea3.Value = Regex.Replace(sqlReader2["was_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                        TextBox26.Text = (string)sqlReader2["was_postcd"];
                        ddlnegri2.SelectedValue = (string)sqlReader2["was_negri"];
                    }
                    sqlReader2.Close();
                    //string query3 = "select * from mem_member as mm left join mem_share AS ms ON ms.sha_new_icno=mm.mem_new_icno Left join aim_pst AS ap ON ap.pst_new_icno = mm.mem_new_icno where mm.mem_new_icno='" + TextBox1.Text + "'";
                    //var sqlCommand3 = new SqlCommand(query3, conn);
                    //var sqlReader3 = sqlCommand3.ExecuteReader();
                    //if (sqlReader3.Read() == false)
                    //{
                    //    while (sqlReader3.Read())
                    //    {
                    //        TextBox25.Text = Convert.ToDateTime(sqlReader3["pst_post_dt"]).ToString("dd/MM/yyyy");
                    //        TextBox24.Text = (string)sqlReader3["sha_debit_amt"];


                    //    }

                    //    sqlReader3.Close();
                    //}
                    //else
                    //{
                    //    TextBox25.Text = "";
                    //    TextBox24.Text = "";
                    //    sqlReader3.Close();
                    //}
                }
                else
                {
                    sqlCommand.Dispose();
                    conn.Close();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

                //DataTable dt = new DataTable();
                //dt = DBCon.Ora_Execute_table("select sha_reference_ind from mem_share where sha_new_icno='" + TextBox1.Text + "'");
                SqlCommand cmd;
                //if (dt.Rows.Count == 1)
                //{
                //    if (dt.Rows[0][0].ToString() == "T")
                //    {

                //         cmd = new SqlCommand("select sum(sha_debit_amt) as snet,'0.00' as anet ,sum(sha_debit_amt)  as jumlah, fe.fee_payment_type_cd,ms.sha_reference_ind from mem_share ms inner join aim_pst ap on  ap.pst_new_icno= ms.sha_new_icno  inner join mem_fee fe on fe.fee_new_icno=ap.pst_new_icno where   sha_new_icno='" + TextBox1.Text + "' and sha_reference_ind ='T'   group by fe.fee_payment_type_cd,ms.sha_reference_ind", conn);
                //    }
                //    else
                //    {
                //         cmd = new SqlCommand("select '0.00'  as snet, sum(sha_debit_amt) as anet,sum(sha_debit_amt)  as jumlah, fe.fee_payment_type_cd,ms.sha_reference_ind from mem_share ms inner join aim_pst ap on  ap.pst_new_icno= ms.sha_new_icno  inner join mem_fee fe on fe.fee_new_icno=ap.pst_new_icno where   sha_new_icno='" + TextBox1.Text + "' and sha_reference_ind ='P'   group by fe.fee_payment_type_cd,ms.sha_reference_ind", conn);
                //    }
                //}
                //else
                //{
                //cmd = new SqlCommand("select  b.ftunai,b.fpst, b.PST as snet,b.tunai as anet  , b.PST +b.tunai as jumlah,b.fee_payment_type_cd,b.sha_reference_ind from (select  isnull(a.f1,'0.00') as ftunai,isnull(a.f2,'0.00') as fpst, isnull(a.p1,'0.00') as PST,isnull(a.t1,'0.00') as tunai,fee_payment_type_cd,sha_reference_ind  from (select case (sha_reference_ind) when 'T' then isnull(sum(sha_debit_amt),'0.00')     end t1 ,case (sha_reference_ind) when 'P' then isnull(sum(sha_debit_amt),'0.00')     end p1,fe.fee_payment_type_cd,ms.sha_reference_ind,case (fee_payment_type_cd) when 'C' then isnull(sum(fe.fee_amount),'0.00')     end f1 ,case (fee_payment_type_cd) when 'P' then isnull(sum(fe.fee_amount),'0.00')     end f2 from mem_share ms inner join mem_fee fe on fe.fee_new_icno=ms.sha_new_icno where ms.sha_new_icno='" + TextBox1.Text + "'  group by fe.fee_payment_type_cd,ms.sha_reference_ind) a)b", conn);
                //cmd = new SqlCommand("select b.FTUNAI,b.FPST,a.STUNAI,a.SPST, a.STUNAI + a.SPST as Jumlah  from (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_debit_amt) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + TextBox1.Text + "' and sha_refund_ind='N'  group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final )a full outer join (select * from (select isnull([FTUNAI],'') as FTUNAI,isnull([FPST],'') as FPST,fee_new_icno from (select  SUM(fee_amount) as Tran_count, case (fee_payment_type_cd) WHEN 'C' THEN 'FTUNAI' WHEN 'P' THEN 'FPST' END MONTHNAME,fee_new_icno from mem_fee where fee_new_icno='" + TextBox1.Text + "' and fee_refund_ind='N'  group by fee_payment_type_cd,fee_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([FTUNAI], [FPST]))AS PivotTable) as final )b on b.fee_new_icno=a.sha_new_icno and b.fee_new_icno='" + TextBox1.Text + "'", conn);
                //cmd = new SqlCommand("select b.FTUNAI,b.FPST,(a.STUNAI) - (c.STUNAI) as STUNAI,(a.SPST) - (c.SPST) as SPST, (a.STUNAI + a.SPST) - (c.STUNAI + c.SPST) as Jumlah  from (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_debit_amt) as Tran_count,SUM(sha_credit_amt) as camt, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + TextBox1.Text + "' and sha_refund_ind='N' group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final )a full outer join (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_credit_amt) as Tran_count,SUM(sha_credit_amt) as camt, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + TextBox1.Text + "' and sha_refund_ind='N' group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final ) c on c.sha_new_icno=a.sha_new_icno full outer join (select * from (select isnull([FTUNAI],'') as FTUNAI,isnull([FPST],'') as FPST,fee_new_icno from (select  SUM(fee_amount) as Tran_count, case (fee_payment_type_cd) WHEN 'C' THEN 'FTUNAI' WHEN 'P' THEN 'FPST' END MONTHNAME,fee_new_icno from mem_fee where fee_new_icno='" + TextBox1.Text + "' and fee_refund_ind='N'  group by fee_payment_type_cd,fee_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([FTUNAI], [FPST]))AS PivotTable) as final )b on b.fee_new_icno=c.sha_new_icno and b.fee_new_icno='" + TextBox1.Text + "'", conn);// 13_03_2017
                cmd = new SqlCommand("select b.FTUNAI,b.FPST,(a.STUNAI) - (c.STUNAI) as STUNAI,(a.SPST) - (c.SPST) as SPST, (a.STUNAI + a.SPST) - (c.STUNAI + c.SPST) as Jumlah  from (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_debit_amt) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + TextBox1.Text + "' and Acc_sts ='Y' and sha_refund_ind='N' group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final )a full outer join (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_credit_amt) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + TextBox1.Text + "' and Acc_sts ='Y' and sha_refund_ind='N' group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final ) c on c.sha_new_icno=a.sha_new_icno full outer join (select * from (select isnull([FTUNAI],'') as FTUNAI,isnull([FPST],'') as FPST,fee_new_icno from (select  SUM(fee_amount) as Tran_count, case (fee_payment_type_cd) WHEN 'C' THEN 'FTUNAI' WHEN 'P' THEN 'FPST' END MONTHNAME,fee_new_icno from mem_fee where fee_new_icno='" + TextBox1.Text + "' and Acc_sts ='Y' and fee_refund_ind='N'  group by fee_payment_type_cd,fee_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([FTUNAI], [FPST]))AS PivotTable) as final )b on b.fee_new_icno=c.sha_new_icno and b.fee_new_icno='" + TextBox1.Text + "'", conn);
                //}

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
                    gvSelected.Rows[0].Cells[0].Text = "MAKLUMAT CARIAN TIDAK DIJUMPAI";

                }
                else
                {
                    gvSelected.DataSource = ds;
                    gvSelected.DataBind();
                }


                //SqlCommand cmd1 = new SqlCommand("select ISNULL(CASE WHEN CONVERT(DATE, sha_approve_Dt) = '1900-01-01' THEN '' ELSE CONVERT(CHAR(10), sha_approve_Dt, 103) END, '') AS sha_approve_Dt, UPPER(sha_item) as sha_item,case(sha_reference_ind) when 'C' then 'TUNAI'  when 'P' then 'PST' end as sha_reference_ind,sha_debit_amt,sha_credit_amt,Jumla=(sum(sha_debit_amt)-Sum(sha_credit_amt)) from mem_member AS mm left join Ref_Nama_Bank as bn ON mm.mem_bank_cd=bn.Bank_Code Left join mem_share AS ms ON ms.sha_new_icno = mm.mem_new_icno where ms.sha_new_icno = '" + TextBox1.Text + "' and sha_refund_ind='N'  group by sha_approve_Dt,sha_item,sha_reference_ind,sha_debit_amt,sha_credit_amt order by sha_approve_Dt,sha_item,sha_reference_ind,sha_debit_amt,sha_credit_amt ", conn);

                SqlCommand cmd1 = new SqlCommand("select ISNULL(CASE WHEN CONVERT(DATE, sha_txn_dt) = '1900-01-01' THEN '' ELSE CONVERT(CHAR(10), sha_txn_dt, 103) END, '') AS sha_approve_Dt, UPPER(sha_item) as sha_item,case(sha_reference_ind) when 'C' then 'TUNAI'  when 'P' then 'PST' end as sha_reference_ind,sha_debit_amt,isnull(sha_credit_amt,'0.00') sha_credit_amt ,sha_debit_amt - isnull(sha_credit_amt,'0.00') as Jumla from mem_share ms inner join mem_member mm  on ms.sha_new_icno=mm.mem_new_icno and mm.Acc_sts ='Y' where sha_new_icno='" + TextBox1.Text + "' and ms.Acc_sts ='Y' and sha_refund_ind='N' ", conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                using (DataTable dt = new DataTable())
                {
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    //da1.Fill(dt);
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
                        GridView2.DataSource = ds1;
                        GridView2.DataBind();

                        int columncount = GridView2.Rows[0].Cells.Count;
                        GridView2.Rows[0].Cells.Clear();
                        GridView2.Rows[0].Cells.Add(new TableCell());
                        GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
                        GridView2.Rows[0].Cells[0].Text = "<center>MAKLUMAT CARIAN TIDAK DIJUMPAI</center>";

                        //Calculate Sum and display in Footer Row

                    }
                    else
                    {
                        GridView2.DataSource = ds1;
                        GridView2.DataBind();
                        GridView2.FooterRow.Cells[4].Text = "<strong>JUMLAH</strong>";
                        GridView2.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                        //e.Row.Cells[4].Text = String.Format((e.Row.Cells[4].Text), "#,##");

                    }
                }
                //DataSet ds1 = new DataSet();
                //da1.Fill(ds1);
                //if (ds1.Tables[0].Rows.Count == 0)
                //{

                //    ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
                //    GridView2.DataSource = ds1;
                //    GridView2.DataBind();

                //    decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Price"));
                //    GridView1.FooterRow.Cells[1].Text = "Total";
                //    GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                //    GridView1.FooterRow.Cells[2].Text = total.ToString("N2");


                //    int columncount = GridView2.Rows[0].Cells.Count;
                //    GridView2.Rows[0].Cells.Clear();
                //    GridView2.Rows[0].Cells.Add(new TableCell());
                //    GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
                //    GridView2.Rows[0].Cells[0].Text = "Maklumat Carian Tidak Dijumpai";


                //}
                //else
                //{
                //    GridView2.DataSource = ds1;
                //    GridView2.DataBind();
                //}


                SqlCommand cmd2 = new SqlCommand("select * from mem_member AS mm Left join mem_settlement AS ms ON ms.set_new_icno = mm.mem_new_icno and ms.Acc_sts ='Y' left join Ref_Sebab as sb on sb.DESCRRIPTION_CODE=ms.set_reason_cd left join Ref_jenis_permohonan as jp on jp.Application_code=ms.set_appl_type_cd  left join Ref_Kaedah_Pembayaran as kp on kp.Payment_Code=ms.set_pay_method_cd where ms.set_new_icno = '" + TextBox1.Text + "' and mm.Acc_sts ='Y' and set_approve_sts_cd='SA' ORDER BY ms.set_txn_dt ASC ", conn);
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
                    GridView1.Rows[0].Cells[0].Text = "MAKLUMAT CARIAN TIDAK DIJUMPAI";
                }
                else
                {
                    GridView1.DataSource = ds2;
                    GridView1.DataBind();
                }


                //SqlCommand cmd3 = new SqlCommand("select * from mem_member AS mm left join Ref_Nama_Bank as bn ON mm.mem_bank_cd=bn.Bank_Code Left join mem_divident AS md ON md.div_new_icno = mm.mem_new_icno where md.div_new_icno = '" + TextBox1.Text + "' and md.div_approve_ind='L' ORDER BY md.div_remark ASC ", conn);
                SqlCommand cmd3 = new SqlCommand("select * from mem_member AS mm  Left join mem_divident AS md ON md.div_new_icno = mm.mem_new_icno and md.Acc_sts ='Y' left join Ref_Nama_Bank as bn ON md.div_bank_cd=bn.Bank_Code where md.div_new_icno = '" + TextBox1.Text + "' and mm.Acc_sts ='Y' and md.div_approve_ind='SA' ORDER BY md.div_remark ASC ", conn);
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                if (ds3.Tables[0].Rows.Count == 0)
                {

                    ds3.Tables[0].Rows.Add(ds3.Tables[0].NewRow());
                    GridView3.DataSource = ds3;
                    GridView3.DataBind();
                    int columncount = GridView3.Rows[0].Cells.Count;
                    GridView3.Rows[0].Cells.Clear();
                    GridView3.Rows[0].Cells.Add(new TableCell());
                    GridView3.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView3.Rows[0].Cells[0].Text = "MAKLUMAT CARIAN TIDAK DIJUMPAI";
                }
                else
                {
                    GridView3.DataSource = ds3;
                    GridView3.DataBind();
                }


                conn.Close();
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch
        {

            //Response.Redirect("Maklumat_Anggota.aspx");
            //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Record is Wrong',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod adalah salah',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    public void dash()
    {
        //style.Attributes.Remove("style", "style");
        //Button3.Visible = true;
        if (TextBox1.Text != "")
        {
            //Button5.Visible = true;
            wilahBind();
            //kawasan();
            negriBind();
            bangsa();
            style.Visible = true;

            SqlConnection conn = new SqlConnection(cs);
            string query = "select *,ISNULL(CASE WHEN CONVERT(DATE, fee_approval_dt) = '1900-01-01' THEN '' ELSE CONVERT(CHAR(10), fee_approval_dt, 103) END, '') AS fee_approval_dt, ISNULL(mem_postcd, '') as mempospcd,ISNULL(mem_negri, '') as memnegri,case(mem_phone_o) when 'NULL' then'' else mem_phone_o end as memphoneo,case(mem_phone_h) when 'NULL' then'' else mem_phone_h end as memphoneh,case(mem_phone_m) when 'NULL' then'' else mem_phone_m end as memphonem,Format(mem_quit_dt, 'dd/MM/yyyy') as qt_dt from mem_member as mm Left join mem_fee AS mf ON mf.fee_new_icno = mm.mem_new_icno and mf.Acc_sts ='Y' Left join  Ref_Wilayah AS rw ON mm.mem_region_cd = rw.Wilayah_Code Left join Ref_Cawangan AS rc ON mm.mem_branch_cd = rc.cawangan_Code and mm.mem_area_cd=rc.kawasan_code left join mem_share ms on ms.sha_new_icno=mm.mem_new_icno and ms.Acc_sts ='Y' where mem_new_icno='" + TextBox1.Text + "' and mm.Acc_sts ='Y'";
            conn.Open();
            var sqlCommand = new SqlCommand(query, conn);
            var sqlReader = sqlCommand.ExecuteReader();

            if (sqlReader.Read() == true)
            {

                if (role_2 == "1")
                {
                    lev2.Attributes.Remove("Style");
                    Button3.Attributes.Remove("Style");
                }
                else
                {
                    lev2.Attributes.Add("Style", "pointer-events:None; opacity: 0.8;");
                    Button3.Attributes.Add("Style", "pointer-events:None; opacity: 0.5;");
                }

                if ((string)sqlReader["mem_nationality_ind"] == "W")
                {
                    status = "Warganegara";
                }
                else if ((string)sqlReader["mem_nationality_ind"] == "N")
                {
                    status = "Bukan Warganegara";
                }
                else
                {
                    status = "Pemustautin Tetap";
                }

                TextBox2.Text = (string)sqlReader["mem_name"];
                TextBox7.Text = (string)sqlReader["mem_old_icno"];
                //dd_status.SelectedValue = (string)sqlReader["mem_sts_cd"];
                TextBox14.Text = status;
                //TextBox16.Text = Convert.ToDateTime(sqlReader["mem_register_dt"]).ToString("dd/MM/yyyy");
                if (sqlReader["mem_race_cd"].ToString() != "")
                {
                    ddbangsa.SelectedValue = sqlReader["mem_race_cd"].ToString();
                }
                else
                {
                    ddbangsa.SelectedValue = "--- PILIH ---";
                }
                if ((string)sqlReader["mem_region_cd"] != "")
                {
                    TextBox8.SelectedValue = (string)sqlReader["mem_region_cd"];
                    wcode = (string)sqlReader["mem_region_cd"];
                }
                else
                {
                    TextBox8.SelectedValue = "";
                    wcode = "";
                }
                if ((string)sqlReader["mem_branch_cd"] != "")
                {
                    bcode = (string)sqlReader["mem_branch_cd"];
                    TextBox9.SelectedValue = (string)sqlReader["mem_branch_cd"].ToString();
                }
                else
                {
                    TextBox9.SelectedValue = "";
                    bcode = "";
                }

                if (sqlReader["mem_area_cd"].ToString() != "")
                {
                    DataTable Check = new DataTable();
                    Check = Con.Ora_Execute_table("select * from Ref_Kawasan where Area_Code='" + sqlReader["mem_area_cd"].ToString() + "'");
                    TextBox18.SelectedItem.Text = Check.Rows[0]["Area_name"].ToString();
                    //kcode = (string)sqlReader["mem_area_cd"];
                    //wilahkawasan();
                }
                else if ((string)sqlReader["mem_area_cd"] == "" && (string)sqlReader["mem_region_cd"] != "" && (string)sqlReader["mem_branch_cd"] != "")
                {
                    //TextBox18.SelectedValue = "";

                    kawasan1();
                }
                else
                {
                    kawasan();
                }
                TextBox19.Text = (string)sqlReader["mem_centre"];
                TextBox5.Text = (string)sqlReader["memphoneh"];
                TextBox20.Text = (string)sqlReader["memphoneo"];
                TextBox22.Text = (string)sqlReader["memphonem"];
                TextArea1.Value = Regex.Replace(sqlReader["mem_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                string scode = (string)sqlReader["mem_sts_cd"];

                if (scode == "SA")
                {
                    Txtsa.Text = "ANGGOTA SAH";
                }
                else if (scode == "TS")
                {
                    Txtsa.Text = "ANGGOTA TIDAK SAH";

                }
                else if (scode == "FM")
                {
                    Txtsa.Text = "FI MASUK";
                }
                else
                {
                    Txtsa.Text = "DALAM NOTIS";
                }
                //string addrs = TextArea1.Value.Replace("\r\n", "<br />");
                //addrs = (string)sqlReader["mem_address"];
                TextBox3.Text = (string)sqlReader["mem_bank_acc_no"];
                Bank_details.SelectedValue = (string)sqlReader["mem_bank_cd"];

                TextBox13.Text = (string)sqlReader["mempospcd"];


                ddlnegri.SelectedValue = (string)sqlReader["memnegri"];
                if (sqlReader["fee_approval_dt"].ToString() != "")
                {
                    var feedt = Convert.ToDateTime(sqlReader["fee_approval_dt"]).ToString("dd/MM/yyyy");
                    if (feedt != "01/01/1900")
                    {
                        TextBox4.Text = feedt;
                    }
                }
                else
                {
                    TextBox4.Text = "";
                }
                //TextBox6.Text = Convert.ToDateTime(sqlReader["mem_quit_dt"]).ToString("yyyy/MM/dd");
                //TextBox6.Text = Convert.ToDateTime(sqlReader["mem_quit_dt"]).ToString("dd/MM/yyyy");
                //string up_date = Convert.ToDateTime(sqlReader["mem_quit_dt"]).ToString("dd/MM/yyyy");
                //if (up_date == "01/01/1900")
                //{
                //    TextBox6.Text = "";
                //}
                //else
                //{
                //    TextBox6.Text = Convert.ToDateTime(sqlReader["mem_quit_dt"]).ToString("dd/MM/yyyy");
                //}
                TextBox27.Text = (string)sqlReader["mem_member_no"];
                //wilahkawasan();




                sqlReader.Close();

                string query1 = "select mw.was_icno,mw.was_name,mw.was_relation_cd,mw.was_phone_no,mw.was_address,mw.was_postcd,mw.was_negri  from mem_member as mm Left join mem_wasi AS mw ON mw.was_new_icno = mm.mem_new_icno and mw.Acc_sts ='Y' where mem_new_icno='" + TextBox1.Text + "' AND mw.was_seqno='1' and mm.Acc_sts ='Y'";
                var sqlCommand1 = new SqlCommand(query1, conn);
                var sqlReader1 = sqlCommand1.ExecuteReader();
                while (sqlReader1.Read())
                {
                    TextBox11.Text = (string)sqlReader1["was_icno"];
                    TextBox10.Text = (string)sqlReader1["was_name"];
                    DropDownList2.SelectedValue = (string)sqlReader1["was_relation_cd"];
                    TextBox12.Text = (string)sqlReader1["was_phone_no"];
                    TextArea2.Value = Regex.Replace(sqlReader1["was_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                    TextBox21.Text = (string)sqlReader1["was_postcd"];
                    ddlnegri1.SelectedValue = (string)sqlReader1["was_negri"];
                }
                sqlReader1.Close();
                //string query4 = "select  ISNULL(CASE WHEN CONVERT(DATE, div_approve_dt) = '1900-01-01' THEN '' ELSE CONVERT(CHAR(10), div_approve_dt, 103) END, '') AS div_approve_dt from mem_divident where div_new_icno='" + TextBox1.Text + "'";
                //var sqlCommand4 = new SqlCommand(query4, conn);
                //var sqlReader4 = sqlCommand4.ExecuteReader();
                //while (sqlReader4.Read())
                //{
                //    if (sqlReader4["div_approve_dt"].ToString() != "")
                //    {
                //        TextBox6.Text = Convert.ToDateTime(sqlReader4["div_approve_dt"]).ToString("dd/MM/yyyy");

                //    }
                //    else
                //    {
                //        TextBox6.Text = "";
                //    }
                //}
                //sqlReader4.Close();
                string query4 = "select  ISNULL(CASE WHEN CONVERT(DATE, set_appprove_dt) = '1900-01-01' THEN '' ELSE CONVERT(CHAR(10), set_appprove_dt, 103) END, '') AS set_appprove_dt from mem_settlement where set_new_icno='" + TextBox1.Text + "' and Acc_sts ='Y'";
                var sqlCommand4 = new SqlCommand(query4, conn);
                var sqlReader4 = sqlCommand4.ExecuteReader();
                while (sqlReader4.Read())
                {
                    if (sqlReader4["set_appprove_dt"].ToString() != "")
                    {
                        TextBox6.Text = (sqlReader4["set_appprove_dt"]).ToString();

                    }
                    else
                    {
                        TextBox6.Text = "";
                    }
                }
                sqlReader4.Close();
                string query5 = "select  CONVERT(varchar(12), ast_st_balance_amt, 1) as ast_st_balance_amt,ast_end_date from aim_st where ast_new_icno='" + TextBox1.Text + "'";
                var sqlCommand5 = new SqlCommand(query5, conn);
                var sqlReader5 = sqlCommand5.ExecuteReader();
                while (sqlReader5.Read())
                {
                    if (sqlReader5["ast_st_balance_amt"].ToString() != "")
                    {
                        TextBox25.Text = Convert.ToDateTime(sqlReader5["ast_end_date"]).ToString("dd/MM/yyyy");
                        TextBox24.Text = sqlReader5["ast_st_balance_amt"].ToString();
                    }
                    else
                    {
                        TextBox25.Text = "";
                        TextBox24.Text = "0.00";
                    }
                }
                sqlReader5.Close();
                string query2 = "select mw.was_icno,mw.was_name,mw.was_relation_cd,mw.was_phone_no,mw.was_address,mw.was_postcd,mw.was_negri from mem_member as mm Left join mem_wasi AS mw ON mw.was_new_icno = mm.mem_new_icno and mw.Acc_sts ='Y' where mem_new_icno='" + TextBox1.Text + "' AND mw.was_seqno='2' and mm.Acc_sts ='Y'";
                var sqlCommand2 = new SqlCommand(query2, conn);
                var sqlReader2 = sqlCommand2.ExecuteReader();
                while (sqlReader2.Read())
                {
                    TextBox15.Text = (string)sqlReader2["was_icno"];
                    TextBox17.Text = (string)sqlReader2["was_name"];
                    DropDownList1.SelectedValue = (string)sqlReader2["was_relation_cd"];
                    TextBox23.Text = (string)sqlReader2["was_phone_no"];
                    TextArea3.Value = Regex.Replace(sqlReader2["was_address"].ToString().Replace(System.Environment.NewLine, ",\n"), @"\\r\\r\\n", ",\n");
                    TextBox26.Text = (string)sqlReader2["was_postcd"];
                    ddlnegri2.SelectedValue = (string)sqlReader2["was_negri"];
                }
                sqlReader2.Close();
                //string query3 = "select * from mem_member as mm left join mem_share AS ms ON ms.sha_new_icno=mm.mem_new_icno Left join aim_pst AS ap ON ap.pst_new_icno = mm.mem_new_icno where mm.mem_new_icno='" + TextBox1.Text + "'";
                //var sqlCommand3 = new SqlCommand(query3, conn);
                //var sqlReader3 = sqlCommand3.ExecuteReader();
                //if (sqlReader3.Read() == false)
                //{
                //    while (sqlReader3.Read())
                //    {
                //        TextBox25.Text = Convert.ToDateTime(sqlReader3["pst_post_dt"]).ToString("dd/MM/yyyy");
                //        TextBox24.Text = (string)sqlReader3["sha_debit_amt"];


                //    }

                //    sqlReader3.Close();
                //}
                //else
                //{
                //    TextBox25.Text = "";
                //    TextBox24.Text = "";
                //    sqlReader3.Close();
                //}
            }
            else
            {
                sqlCommand.Dispose();
                conn.Close();
                Button3.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

            //DataTable dt = new DataTable();
            //dt = DBCon.Ora_Execute_table("select sha_reference_ind from mem_share where sha_new_icno='" + TextBox1.Text + "'");
            SqlCommand cmd;
            //if (dt.Rows.Count == 1)
            //{
            //    if (dt.Rows[0][0].ToString() == "T")
            //    {

            //         cmd = new SqlCommand("select sum(sha_debit_amt) as snet,'0.00' as anet ,sum(sha_debit_amt)  as jumlah, fe.fee_payment_type_cd,ms.sha_reference_ind from mem_share ms inner join aim_pst ap on  ap.pst_new_icno= ms.sha_new_icno  inner join mem_fee fe on fe.fee_new_icno=ap.pst_new_icno where   sha_new_icno='" + TextBox1.Text + "' and sha_reference_ind ='T'   group by fe.fee_payment_type_cd,ms.sha_reference_ind", conn);
            //    }
            //    else
            //    {
            //         cmd = new SqlCommand("select '0.00'  as snet, sum(sha_debit_amt) as anet,sum(sha_debit_amt)  as jumlah, fe.fee_payment_type_cd,ms.sha_reference_ind from mem_share ms inner join aim_pst ap on  ap.pst_new_icno= ms.sha_new_icno  inner join mem_fee fe on fe.fee_new_icno=ap.pst_new_icno where   sha_new_icno='" + TextBox1.Text + "' and sha_reference_ind ='P'   group by fe.fee_payment_type_cd,ms.sha_reference_ind", conn);
            //    }
            //}
            //else
            //{
            //cmd = new SqlCommand("select  b.ftunai,b.fpst, b.PST as snet,b.tunai as anet  , b.PST +b.tunai as jumlah,b.fee_payment_type_cd,b.sha_reference_ind from (select  isnull(a.f1,'0.00') as ftunai,isnull(a.f2,'0.00') as fpst, isnull(a.p1,'0.00') as PST,isnull(a.t1,'0.00') as tunai,fee_payment_type_cd,sha_reference_ind  from (select case (sha_reference_ind) when 'T' then isnull(sum(sha_debit_amt),'0.00')     end t1 ,case (sha_reference_ind) when 'P' then isnull(sum(sha_debit_amt),'0.00')     end p1,fe.fee_payment_type_cd,ms.sha_reference_ind,case (fee_payment_type_cd) when 'C' then isnull(sum(fe.fee_amount),'0.00')     end f1 ,case (fee_payment_type_cd) when 'P' then isnull(sum(fe.fee_amount),'0.00')     end f2 from mem_share ms inner join mem_fee fe on fe.fee_new_icno=ms.sha_new_icno where ms.sha_new_icno='" + TextBox1.Text + "'  group by fe.fee_payment_type_cd,ms.sha_reference_ind) a)b", conn);
            //cmd = new SqlCommand("select b.FTUNAI,b.FPST,a.STUNAI,a.SPST, a.STUNAI + a.SPST as Jumlah  from (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_debit_amt) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + TextBox1.Text + "' and sha_refund_ind='N'  group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final )a full outer join (select * from (select isnull([FTUNAI],'') as FTUNAI,isnull([FPST],'') as FPST,fee_new_icno from (select  SUM(fee_amount) as Tran_count, case (fee_payment_type_cd) WHEN 'C' THEN 'FTUNAI' WHEN 'P' THEN 'FPST' END MONTHNAME,fee_new_icno from mem_fee where fee_new_icno='" + TextBox1.Text + "' and fee_refund_ind='N'  group by fee_payment_type_cd,fee_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([FTUNAI], [FPST]))AS PivotTable) as final )b on b.fee_new_icno=a.sha_new_icno and b.fee_new_icno='" + TextBox1.Text + "'", conn);
            cmd = new SqlCommand("select b.FTUNAI,b.FPST,(a.STUNAI) - (c.STUNAI) as STUNAI,(a.SPST) - (c.SPST) as SPST, (a.STUNAI + a.SPST) - (c.STUNAI + c.SPST) as Jumlah  from (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_debit_amt) as Tran_count,SUM(sha_credit_amt) as camt, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + TextBox1.Text + "' and Acc_sts ='Y' and sha_refund_ind='N' group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final )a full outer join (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_credit_amt) as Tran_count,SUM(sha_credit_amt) as camt, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + TextBox1.Text + "' and Acc_sts ='Y' and sha_refund_ind='N' group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final ) c on c.sha_new_icno=a.sha_new_icno full outer join (select * from (select isnull([FTUNAI],'') as FTUNAI,isnull([FPST],'') as FPST,fee_new_icno from (select  SUM(fee_amount) as Tran_count, case (fee_payment_type_cd) WHEN 'C' THEN 'FTUNAI' WHEN 'P' THEN 'FPST' END MONTHNAME,fee_new_icno from mem_fee where fee_new_icno='" + TextBox1.Text + "' and Acc_sts ='Y' and fee_refund_ind='N'  group by fee_payment_type_cd,fee_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([FTUNAI], [FPST]))AS PivotTable) as final )b on b.fee_new_icno=c.sha_new_icno and b.fee_new_icno='" + TextBox1.Text + "'", conn);
            //}

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
                gvSelected.Rows[0].Cells[0].Text = "<center><strong> MAKLUMAT CARIAN TIDAK DIJUMPAI</strong></center>";

            }
            else
            {
                gvSelected.Visible = true;
                gvSelected.DataSource = ds;
                gvSelected.DataBind();
            }


            SqlCommand cmd1 = new SqlCommand("select ISNULL(CASE WHEN CONVERT(DATE, sha_txn_dt) = '1900-01-01' THEN '' ELSE CONVERT(CHAR(10), sha_txn_dt, 103) END, '') AS sha_approve_Dt, UPPER(sha_item) as sha_item,case(sha_reference_ind) when 'C' then 'TUNAI'  when 'P' then 'PST' end as sha_reference_ind,sha_debit_amt,sha_credit_amt ,sha_debit_amt - sha_credit_amt as Jumla from mem_share ms inner join mem_member mm  on ms.sha_new_icno=mm.mem_new_icno and mm.Acc_sts ='Y' where sha_new_icno='" + TextBox1.Text + "' and ms.Acc_sts ='Y' and sha_refund_ind='N' order by sha_approve_Dt ", conn);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

            using (DataTable dt = new DataTable())
            {
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                //da1.Fill(dt);
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
                    GridView2.DataSource = ds1;
                    GridView2.DataBind();

                    int columncount = GridView2.Rows[0].Cells.Count;
                    GridView2.Rows[0].Cells.Clear();
                    GridView2.Rows[0].Cells.Add(new TableCell());
                    GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView2.Rows[0].Cells[0].Text = "<center><strong>MAKLUMAT CARIAN TIDAK DIJUMPAI</strong></center>";

                    //Calculate Sum and display in Footer Row

                }
                else
                {
                    GridView2.DataSource = ds1;
                    GridView2.DataBind();
                    GridView2.FooterRow.Cells[4].Text = "<strong>TOTAL</strong>";
                    GridView2.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                }
            }
            //DataSet ds1 = new DataSet();
            //da1.Fill(ds1);
            //if (ds1.Tables[0].Rows.Count == 0)
            //{

            //    ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
            //    GridView2.DataSource = ds1;
            //    GridView2.DataBind();

            //    decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Price"));
            //    GridView1.FooterRow.Cells[1].Text = "Total";
            //    GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            //    GridView1.FooterRow.Cells[2].Text = total.ToString("N2");


            //    int columncount = GridView2.Rows[0].Cells.Count;
            //    GridView2.Rows[0].Cells.Clear();
            //    GridView2.Rows[0].Cells.Add(new TableCell());
            //    GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            //    GridView2.Rows[0].Cells[0].Text = "Maklumat Carian Tidak Dijumpai";


            //}
            //else
            //{
            //    GridView2.DataSource = ds1;
            //    GridView2.DataBind();
            //}


            SqlCommand cmd2 = new SqlCommand("select * from mem_member AS mm Left join mem_settlement AS ms ON ms.set_new_icno = mm.mem_new_icno and ms.Acc_sts ='Y' left join Ref_Sebab as sb on sb.DESCRRIPTION_CODE=ms.set_reason_cd left join Ref_jenis_permohonan as jp on jp.Application_code=ms.set_appl_type_cd  left join Ref_Kaedah_Pembayaran as kp on kp.Payment_Code=ms.set_pay_method_cd where mm.Acc_sts ='Y' and ms.set_new_icno = '" + TextBox1.Text + "' and set_approve_sts_cd='SA' ORDER BY ms.set_txn_dt ASC ", conn);
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
                GridView1.Rows[0].Cells[0].Text = "<center><strong>MAKLUMAT CARIAN TIDAK DIJUMPAI</strong></center>";
            }
            else
            {
                GridView1.DataSource = ds2;
                GridView1.DataBind();
            }


            SqlCommand cmd3 = new SqlCommand("select * from mem_member AS mm  Left join mem_divident AS md ON md.div_new_icno = mm.mem_new_icno and md.Acc_sts ='Y' left join Ref_Nama_Bank as bn ON md.div_bank_cd=bn.Bank_Code where md.div_new_icno = '" + TextBox1.Text + "' and mm.Acc_sts ='Y' and md.div_approve_ind='SA' ORDER BY md.div_pay_dt ASC ", conn);
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            DataSet ds3 = new DataSet();
            da3.Fill(ds3);
            if (ds3.Tables[0].Rows.Count == 0)
            {

                ds3.Tables[0].Rows.Add(ds3.Tables[0].NewRow());
                GridView3.DataSource = ds3;
                GridView3.DataBind();
                int columncount = GridView3.Rows[0].Cells.Count;
                GridView3.Rows[0].Cells.Clear();
                GridView3.Rows[0].Cells.Add(new TableCell());
                GridView3.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView3.Rows[0].Cells[0].Text = "<center><strong>MAKLUMAT CARIAN TIDAK DIJUMPAI</strong></center>";
            }
            else
            {
                GridView3.DataSource = ds3;
                GridView3.DataBind();
            }


            conn.Close();
        }
        else
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }


    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "Jumla") != DBNull.Value)
            {
                System.Web.UI.WebControls.Label lblamount1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Jumla");
                //total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Jumla"));
                decimal lblamt = decimal.Parse(lblamount1.Text);
                total += lblamt;
            }

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            System.Web.UI.WebControls.Label lblamount = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal");
            lblamount.Text = total.ToString("#,##0.00");
        }
    }





    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        //BindGridview();
    }



    protected void Reset_btn(object sender, EventArgs e)
    {        
        Response.Redirect("../keanggotan/Maklumat_Anggota.aspx");
    }

    protected void update(object sender, EventArgs e)
    {
        DataTable Check_mem = new DataTable();
        Check_mem = Con.Ora_Execute_table("select * from mem_member where mem_new_icno='" + TextBox1.Text + "' and Acc_sts ='Y'");

        if (Check_mem.Rows.Count != 0)
        {
            string fdate = string.Empty, fmdate = string.Empty;
            if (TextBox4.Text != "")
            {
                fdate = TextBox4.Text;
                DateTime ft = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                fmdate = ft.ToString("yyyy-MM-dd");
            }


            DataTable Check_kaw = new DataTable();
            Check_kaw = Con.Ora_Execute_table("select kawasan_code from Ref_Cawangan where wilayah_code='" + TextBox8.SelectedValue + "' and cawangan_code='" + TextBox9.SelectedValue + "'");
            SqlConnection con = new SqlConnection(cs);
            //insert Audit Trail
            SqlCommand cmd2 = new SqlCommand("UPDATE mem_member set [mem_name] = @mem_name, [mem_phone_h] = @mem_phone_h, [mem_phone_o] = @mem_phone_o, [mem_phone_m] = @mem_phone_m, [mem_address] = @mem_address,[mem_postcd] = @mem_postcd,[mem_negri] = @mem_negri, [mem_bank_acc_no] = @mem_bank_acc_no, [mem_bank_cd] = @mem_bank_cd,[mem_region_cd] =@mem_region_cd,[mem_branch_cd]=@mem_branch_cd,[mem_centre]=@mem_centre,[mem_area_cd]=@mem_area_cd WHERE mem_new_icno ='" + TextBox1.Text + "' and Acc_sts ='Y'", con);

            cmd2.Parameters.AddWithValue("mem_name", TextBox2.Text.Replace("''", "'"));
            cmd2.Parameters.AddWithValue("mem_phone_h", TextBox5.Text);
            cmd2.Parameters.AddWithValue("mem_phone_o", TextBox20.Text);
            cmd2.Parameters.AddWithValue("mem_phone_m", TextBox22.Text);
            //string addrs3 = TextArea1.Value.Replace("\r\n", "<br />");
            cmd2.Parameters.AddWithValue("mem_address", TextArea1.Value.Replace(",", ""));
            cmd2.Parameters.AddWithValue("mem_postcd", TextBox13.Text);
            cmd2.Parameters.AddWithValue("mem_negri", ddlnegri.SelectedValue);
            cmd2.Parameters.AddWithValue("mem_bank_acc_no", TextBox3.Text);
            cmd2.Parameters.AddWithValue("mem_bank_cd", Bank_details.SelectedValue);
            cmd2.Parameters.AddWithValue("mem_region_cd", TextBox8.SelectedValue);
            cmd2.Parameters.AddWithValue("mem_branch_cd", TextBox9.SelectedValue);
            cmd2.Parameters.AddWithValue("mem_centre", TextBox19.Text);
            cmd2.Parameters.AddWithValue("mem_area_cd", Check_kaw.Rows[0]["kawasan_code"].ToString());

            con.Open();
            int k = cmd2.ExecuteNonQuery();
            con.Close();

            if (TextBox11.Text != "")
            {
                DataTable dtt1 = DBCon.Ora_Execute_table("select was_seqno from mem_wasi where was_new_icno='" + TextBox1.Text + "' and Acc_sts ='Y' and was_seqno='1'");
                if (dtt1.Rows.Count == 0)
                {
                    DataTable dw1 = DBCon.Ora_Execute_table("insert into mem_wasi(was_icno,was_name,was_relation_cd,was_phone_no,was_address,was_negri,was_postcd,was_seqno,was_new_icno,Acc_sts)values('" + TextBox11.Text + "','" + TextBox10.Text + "','" + DropDownList2.SelectedItem.Value + "','" + TextBox12.Text + "','" + TextArea2.Value.Replace(",", "") + "','" + ddlnegri1.SelectedItem.Value + "','" + TextBox21.Text + "','1','" + TextBox1.Text + "','Y')");
                }
                else
                {

                    DataTable dw1 = DBCon.Ora_Execute_table("update mem_wasi set was_icno='" + TextBox11.Text + "',was_name='" + TextBox10.Text + "',was_relation_cd='" + DropDownList2.SelectedItem.Value + "',was_phone_no='" + TextBox12.Text + "',was_address='" + TextArea2.Value.Replace(",", "") + "',was_negri='" + ddlnegri1.SelectedItem.Value + "',was_postcd='" + TextBox21.Text + "' where was_new_icno='" + TextBox1.Text + "' and was_seqno='1' and Acc_sts ='Y'");
                }
            }
            if (TextBox15.Text != "")
            {
                DataTable dtt2 = DBCon.Ora_Execute_table("select was_seqno from mem_wasi where was_new_icno='" + TextBox1.Text + "' and was_seqno='2' and Acc_sts ='Y'");
                if (dtt2.Rows.Count == 0)
                {
                    DataTable dw1 = DBCon.Ora_Execute_table("insert into mem_wasi(was_icno,was_name,was_relation_cd,was_phone_no,was_address,was_negri,was_postcd,was_seqno,was_new_icno,Acc_sts)values('" + TextBox15.Text + "','" + TextBox17.Text + "','" + DropDownList1.SelectedItem.Value + "','" + TextBox23.Text + "','" + TextArea3.Value.Replace(",", "") + "','" + ddlnegri2.SelectedItem.Value + "','" + TextBox26.Text + "','2','" + TextBox1.Text + "','Y')");
                }
                else
                {
                    DataTable dw2 = DBCon.Ora_Execute_table("update mem_wasi set was_icno='" + TextBox15.Text + "',was_name='" + TextBox17.Text + "',was_relation_cd='" + DropDownList1.SelectedItem.Value + "',was_phone_no='" + TextBox23.Text + "',was_address='" + TextArea3.Value.Replace(",", "") + "',was_negri='" + ddlnegri2.SelectedItem.Value + "',was_postcd='" + TextBox26.Text + "' where was_new_icno='" + TextBox1.Text + "' and was_seqno='2' and Acc_sts ='Y'");
                }
            }
            dash();
            service.audit_trail("S0047", "Kemaskini","No KP Baru",TextBox1.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Kemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }



    void wilahBind()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select cawangan_name,cawangan_code from Ref_Cawangan";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            TextBox9.DataSource = dt;
            TextBox9.DataBind();
            TextBox9.DataTextField = "cawangan_name";
            TextBox9.DataValueField = "cawangan_code";
            TextBox9.DataBind();
            //TextBox9.Items.Insert(0, "--- PILIH ---");
            TextBox9.Items.Insert(0, new ListItem("--- PILIH ---", ""));

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

            SqlConnection con = new SqlConnection(cs);
            string com = "SELECT Bangsa_Name,Bangsa_Code FROM ref_bangsa ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddbangsa.DataSource = dt;
            ddbangsa.DataBind();
            ddbangsa.DataTextField = "Bangsa_Name";
            ddbangsa.DataValueField = "Bangsa_Code";
            ddbangsa.DataBind();
            ddbangsa.Items.Insert(0, "--- PILIH ---");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void bBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string com = "select kavasan_name,kawasan_code from Ref_Cawangan where cawangan_code='" + bcode + "'  ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            TextBox18.DataSource = dt;
            TextBox18.DataBind();
            TextBox18.DataTextField = "kavasan_name";
            TextBox18.DataValueField = "kawasan_code";
            TextBox18.DataBind();
            TextBox18.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //void bBind()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {

    //        SqlConnection con = new SqlConnection(cs);
    //        string com = "select kavasan_name,kawasan_code from Ref_Cawangan where  cawangan_name='" + TextBox9.SelectedItem.Text + "' and wilayah_name='" + TextBox8.SelectedItem.Text + "'   group by kavasan_name,kawasan_code order by kavasan_name asc";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        TextBox18.DataSource = dt;

    //        TextBox18.DataTextField = "kavasan_name";
    //        TextBox18.DataValueField = "kawasan_code";
    //        TextBox18.DataBind();
    //        //ddkaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    protected void ddcaw_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(cs);
        if (TextBox18.SelectedItem.Text == "Semua Kawasan")
        {
            pusat.Rows.Clear();
            TextBox18.Items.Clear();
            TextBox18.Items.Insert(0, new ListItem("Semua kawasan", ""));
        }
        //-Pusat---------------------------------------------------------------------------------
        string cmd5 = "select distinct kawasan_code,kavasan_name from  Ref_Cawangan where cawangan_code='" + TextBox9.SelectedItem.Value + "'";
        pusat.Rows.Clear();
        TextBox18.Items.Clear();

        con.Open();
        SqlDataAdapter adapterP = new SqlDataAdapter(cmd5, con);
        adapterP.Fill(pusat);

        TextBox18.DataSource = pusat;
        TextBox18.DataTextField = "kavasan_name";
        TextBox18.DataValueField = "kawasan_code";
        TextBox18.DataBind();
        //ddPusat.Items.RemoveAt(0); //remove 'Semua Wilayah'
        con.Close();

        TextBox18.Items.Insert(0, new ListItem("Semua kawasan", ""));

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            if (TextBox1.Text != "")
            {
                //Path
                DataSet ds = new DataSet();
                //DataSet ds1 = new DataSet();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                //dt = DBCon.Ora_Execute_table("SELECT DISTINCT  A.mem_member_no, A.mem_name, A.mem_address, A.mem_new_icno, A.mem_phone_m, A.gender_desc, A.Bangsa_Name, A.Wilayah_Name, A.mem_centre, A.cawangan_name,  A.mem_fee_amount, CONVERT(VARCHAR(10),GETDATE(),105) as cdate, B.ftunai,B.fpst,B.SPST,B.STUNAI,B.jumlah,c.sha_approve_Dt, C.sha_item,C. sha_reference_ind,C. sha_debit_amt,C. sha_credit_amt,c.Jumla,d.div_pay_dt,d.div_remark,d.Bank_Name as bname,d.div_bank_acc_no,d.div_debit_amt,a.Applicant_Name,e.ast_end_date,e.ast_monthly_collect_amt,e.ast_st_balance_amt FROM ((select mem_member_no,mem_name,mem_address,mem_new_icno,mem_phone_m,rg.gender_desc,rb.Bangsa_Name,rw.Wilayah_Name,mm.mem_centre,mem_fee_amount,ra.Applicant_Name,br.branch_desc as cawangan_name  from mem_member as mm Left join  Ref_Wilayah AS rw ON mm.mem_region_cd = rw.Wilayah_Code Left join Ref_Cawangan AS rc ON mm.mem_area_cd=rc.kawasan_code inner join Ref_Bangsa rb on rb.Bangsa_Code=mm.mem_race_cd inner join ref_gender rg on rg.gender_cd=mm.mem_gender_cd  inner join Ref_Applicant_Category ra on ra.Applicant_Code=mm.mem_applicant_type_cd left join ref_branch br on br.branch_cd=mm.mem_branch_cd  ) a FULL OUTER JOIN (select b.FTUNAI,b.FPST,a.STUNAI,a.SPST, a.STUNAI + a.SPST as Jumlah,a.sha_new_icno  from (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_debit_amt) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + TextBox1.Text + "' and sha_refund_ind='N'  group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final )a full outer join (select * from (select isnull([FTUNAI],'') as FTUNAI,isnull([FPST],'') as FPST,fee_new_icno from (select  SUM(fee_amount) as Tran_count, case (fee_payment_type_cd) WHEN 'C' THEN 'FTUNAI' WHEN 'P' THEN 'FPST' END MONTHNAME,fee_new_icno from mem_fee where fee_new_icno='" + TextBox1.Text + "' and fee_refund_ind='N' group by fee_payment_type_cd,fee_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([FTUNAI], [FPST]))AS PivotTable) as final )b on b.fee_new_icno=a.sha_new_icno)b  on A.mem_new_icno= '" + TextBox1.Text + "' FULL OUTER JOIN  (select ISNULL(CASE WHEN CONVERT(DATE, sha_approve_Dt) = '1900-01-01' THEN '' ELSE CONVERT(CHAR(10), sha_approve_Dt, 105) END, '') AS sha_approve_Dt, UPPER(sha_item) as sha_item,case(sha_reference_ind) when 'C' then 'TUNAI'  when 'P' then 'PST' end as sha_reference_ind,sha_debit_amt,sha_credit_amt,Jumla=(sum(sha_debit_amt)-Sum(sha_credit_amt)),ms.sha_new_icno from mem_member AS mm left join Ref_Nama_Bank as bn ON mm.mem_bank_cd=bn.Bank_Code Left join mem_share AS ms ON ms.sha_new_icno = mm.mem_new_icno and ms.sha_refund_ind='N'   group by sha_approve_Dt,sha_item,sha_reference_ind ,sha_debit_amt,sha_credit_amt,sha_new_icno  )c on c.sha_new_icno=a.mem_new_icno FULL OUTER JOIN (select  Convert(CHAR(10), div_pay_dt, 105) as div_pay_dt,div_remark,Bank_Name,div_bank_acc_no,div_debit_amt,div_new_icno from mem_member AS mm left join Ref_Nama_Bank as bn ON mm.mem_bank_cd=bn.Bank_Code Left join mem_divident AS md ON md.div_new_icno = mm.mem_new_icno ) d on d.div_new_icno=a.mem_new_icno FULL OUTER JOIN (select Convert(char(10),ast_end_date,105) as ast_end_date,ast_st_balance_amt,ast_monthly_collect_amt,ast_new_icno from aim_st ) e on e.ast_new_icno=a.mem_new_icno) where a.mem_new_icno='" + TextBox1.Text + "'");
                //dt = DBCon.Ora_Execute_table("SELECT A.mem_member_no,A.mmjob as mem_job, A.mem_name, A.mem_address, A.mem_new_icno, A.mem_phone_m, A.gender_desc, A.Bangsa_Name, A.Wilayah_Name,a.Area_Name, A.mem_centre, ISNULL(A.cawangan_name,'') as cawangan_name,  A.mem_fee_amount, FORMAT(GETDATE(),'dd/MM/yyyy', 'en-us') as cdate, B.ftunai,B.fpst,B.SPST,B.STUNAI,B.jumlah,c.sha_approve_Dt, C.sha_item,C. sha_reference_ind,C. sha_debit_amt,C. sha_credit_amt,c.Jumla,a.Applicant_Name,e.ast_end_date,e.ast_monthly_collect_amt,e.ast_st_balance_amt FROM ((select mem_member_no,mem_name,mem_address,mem_new_icno,case(mem_phone_m) when 'NULL' then '' else mem_phone_m END  as mem_phone_m,rg.gender_desc,rb.Bangsa_Name,rw.Wilayah_Name,rk.Area_Name,mm.mem_centre,mem_fee_amount,ra.Applicant_Name,br.branch_desc as cawangan_name,case when mem_job = 'NULL' then '' else ISNULL(mem_job,'') end as mmjob from mem_member as mm Left join  Ref_Wilayah AS rw ON mm.mem_region_cd = rw.Wilayah_Code left join Ref_Kawasan as rk on rk.Area_Code=mm.mem_area_cd Left join Ref_Cawangan AS rc ON mm.mem_area_cd=rc.kawasan_code inner join Ref_Bangsa rb on rb.Bangsa_Code=mm.mem_race_cd inner join ref_gender rg on rg.gender_cd=mm.mem_gender_cd  inner join Ref_Applicant_Category ra on ra.Applicant_Code=mm.mem_applicant_type_cd left join ref_branch br on br.branch_cd=mm.mem_branch_cd  ) a FULL OUTER JOIN (select b.FTUNAI,b.FPST,a.STUNAI,a.SPST, a.STUNAI + a.SPST as Jumlah,a.sha_new_icno  from (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_debit_amt) - sum(sha_credit_amt) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + TextBox1.Text + "' and sha_refund_ind='N'  group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final )a full outer join (select * from (select isnull([FTUNAI],'') as FTUNAI,isnull([FPST],'') as FPST,fee_new_icno from (select  SUM(fee_amount) as Tran_count, case (fee_payment_type_cd) WHEN 'C' THEN 'FTUNAI' WHEN 'P' THEN 'FPST' END MONTHNAME,fee_new_icno from mem_fee where fee_new_icno='" + TextBox1.Text + "' and fee_refund_ind='N' group by fee_payment_type_cd,fee_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([FTUNAI], [FPST]))AS PivotTable) as final )b on b.fee_new_icno=a.sha_new_icno)b  on A.mem_new_icno= '" + TextBox1.Text + "' FULL OUTER JOIN  (select sha_txn_Dt,ISNULL(CASE WHEN sha_txn_dt = '1900-01-01 00:00:00.000' THEN '' ELSE FORMAT(sha_txn_dt,'dd/MM/yyyy', 'en-us') END, '') AS sha_approve_Dt, UPPER(sha_item) as sha_item,case(sha_reference_ind) when 'C' then 'TUNAI'  when 'P' then 'PST' end as sha_reference_ind,sha_debit_amt,sha_credit_amt,Jumla=(sum(sha_debit_amt)-Sum(sha_credit_amt)),ms.sha_new_icno from mem_member AS mm left join Ref_Nama_Bank as bn ON mm.mem_bank_cd=bn.Bank_Code Left join mem_share AS ms ON ms.sha_new_icno = mm.mem_new_icno and ms.sha_refund_ind='N' group by sha_txn_Dt,sha_item,sha_reference_ind ,sha_debit_amt,sha_credit_amt,sha_new_icno  )c on c.sha_new_icno=a.mem_new_icno  FULL OUTER JOIN (select FORMAT(ast_end_date,'dd/MM/yyyy', 'en-us') as ast_end_date,ast_st_balance_amt,ast_monthly_collect_amt,ast_new_icno from aim_st ) e on e.ast_new_icno=a.mem_new_icno) where a.mem_new_icno='" + TextBox1.Text + "' order by c.sha_txn_Dt ASC");
                dt = DBCon.Ora_Execute_table("SELECT A.mem_member_no,A.mmjob as mem_job, A.mem_name, A.mem_address, A.mem_new_icno, A.mem_phone_m, ISNULL(A.gender_desc,'') gender_desc, ISNULL(A.Bangsa_Name,'') Bangsa_Name,ISNULL(A.Wilayah_Name,'') Wilayah_Name,ISNULL(a.Area_Name,'') Area_Name, A.mem_centre, ISNULL(A.cawangan_name,'') as cawangan_name,  A.mem_fee_amount, FORMAT(GETDATE(),'dd/MM/yyyy', 'en-us') as cdate, B.ftunai,B.fpst,B.SPST,B.STUNAI,B.jumlah,c.sha_approve_Dt, C.sha_item,C. sha_reference_ind,C. sha_debit_amt,C. sha_credit_amt,c.Jumla,a.Applicant_Name,e.ast_end_date,e.ast_monthly_collect_amt,e.ast_st_balance_amt FROM ((select mem_member_no,mem_name,mem_address,mem_new_icno,case(mem_phone_m) when 'NULL' then '' else mem_phone_m END  as mem_phone_m,rg.gender_desc,rb.Bangsa_Name,rw.Wilayah_Name,rk.Area_Name,mm.mem_centre,mem_fee_amount,ra.Applicant_Name,br.branch_desc as cawangan_name,case when mem_job = 'NULL' then '' else ISNULL(mem_job,'') end as mmjob from mem_member as mm Left join  Ref_Wilayah AS rw ON mm.mem_region_cd = rw.Wilayah_Code left join Ref_Kawasan as rk on rk.Area_Code=mm.mem_area_cd and rk.cawangan_cd=mm.mem_branch_cd Left join Ref_Cawangan AS rc ON mm.mem_area_cd=rc.kawasan_code and mm.mem_branch_cd= rc.cawangan_code Left join Ref_Bangsa rb on rb.Bangsa_Code=mm.mem_race_cd Left join ref_gender rg on rg.gender_cd=mm.mem_gender_cd  Left join Ref_Applicant_Category ra on ra.Applicant_Code=mm.mem_applicant_type_cd left join ref_branch br on br.branch_cd=mm.mem_branch_cd where mm.Acc_sts='Y') a FULL OUTER JOIN (select b.FTUNAI,b.FPST,a.STUNAI,a.SPST, a.STUNAI + a.SPST as Jumlah,a.sha_new_icno  from (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_debit_amt) - sum(sha_credit_amt) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + TextBox1.Text + "' and Acc_sts ='Y' and sha_refund_ind='N'  group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final )a full outer join (select * from (select isnull([FTUNAI],'') as FTUNAI,isnull([FPST],'') as FPST,fee_new_icno from (select  SUM(fee_amount) as Tran_count, case (fee_payment_type_cd) WHEN 'C' THEN 'FTUNAI' WHEN 'P' THEN 'FPST' END MONTHNAME,fee_new_icno from mem_fee where fee_new_icno='" + TextBox1.Text + "' and Acc_sts ='Y' and fee_refund_ind='N' group by fee_payment_type_cd,fee_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([FTUNAI], [FPST]))AS PivotTable) as final )b on b.fee_new_icno=a.sha_new_icno)b  on A.mem_new_icno= '" + TextBox1.Text + "' FULL OUTER JOIN  (select sha_txn_Dt,ISNULL(CASE WHEN sha_txn_dt = '1900-01-01 00:00:00.000' THEN '' ELSE FORMAT(sha_txn_dt,'dd/MM/yyyy', 'en-us') END, '') AS sha_approve_Dt, UPPER(sha_item) as sha_item,case(sha_reference_ind) when 'C' then 'TUNAI'  when 'P' then 'PST' end as sha_reference_ind,sha_debit_amt,sha_credit_amt,Jumla=(sum(sha_debit_amt)-Sum(sha_credit_amt)),ms.sha_new_icno from mem_member AS mm left join Ref_Nama_Bank as bn ON mm.mem_bank_cd=bn.Bank_Code Left join mem_share AS ms ON ms.sha_new_icno = mm.mem_new_icno and ms.Acc_sts ='Y' and ms.sha_refund_ind='N'  where mm.Acc_sts='Y' group by sha_txn_Dt,sha_item,sha_reference_ind ,sha_debit_amt,sha_credit_amt,sha_new_icno  )c on c.sha_new_icno=a.mem_new_icno  FULL OUTER JOIN (select FORMAT(ast_end_date,'dd/MM/yyyy', 'en-us') as ast_end_date,ast_st_balance_amt,ast_monthly_collect_amt,ast_new_icno from aim_st ) e on e.ast_new_icno=a.mem_new_icno) where a.mem_new_icno='" + TextBox1.Text + "' order by c.sha_txn_Dt ASC");
                dt1 = DBCon.Ora_Execute_table("select case when FORMAT( md.div_pay_dt,'dd/MM/yyyy', 'en-us') = '01/01/1900' then '' else FORMAT( md.div_pay_dt,'dd/MM/yyyy', 'en-us') end as div_pay_dt,md.div_remark,ISNULL(bn.Bank_Name,'') as Bank_Name,md.div_bank_acc_no,md.div_debit_amt from mem_member AS mm Left join mem_divident AS md ON md.div_new_icno = mm.mem_new_icno and md.Acc_sts ='Y' left join Ref_Nama_Bank as bn ON md.div_bank_cd=bn.Bank_Code where md.div_new_icno = '" + TextBox1.Text + "' and mm.Acc_sts ='Y' and md.div_approve_ind='SA' ORDER BY md.div_remark ASC ");

                ds.Tables.Add(dt);
                //ds1.Tables.Add(dt1);
                RptviwerStudent.LocalReport.DataSources.Clear();
                RptviwerStudent.Reset();
                ReportDataSource RDS1 = new ReportDataSource("BANKSLIP", dt1);
                //ReportViewer1.ProcessingMode = ProcessingMode.Local;

                RptviwerStudent.LocalReport.ReportPath = "keanggotan/Dash_report.rdlc";
                //ReportViewer1.LocalReport.DataSources.Add(RDS1);
                ReportDataSource rds = new ReportDataSource("list", dt);
                ReportDataSource rds1 = new ReportDataSource("list1", dt1);


                string ss1 = string.Empty, ss2 = string.Empty, ss3 = string.Empty, ss4 = string.Empty;
                if (TextBox18.SelectedValue != "")
                {
                    ss1 = TextBox18.SelectedItem.Text;
                }


                string cyr = DateTime.Now.ToString("yyyy");

                ss2 = (double.Parse(cyr) - 1).ToString();

                if (ddlnegri.SelectedValue != "")
                {
                    ss3 = ddlnegri.SelectedItem.Text;
                }



                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("tarik_sah", TextBox4.Text),
                     new ReportParameter("kawasan", dt.Rows[0]["Area_Name"].ToString()),
                     new ReportParameter("pyear", ss2),
                     new ReportParameter("negri", ss3),
                     new ReportParameter("pscd", TextBox13.Text),
                     new ReportParameter("mmjob", dt.Rows[0]["mem_job"].ToString()),
                     new ReportParameter("sts", Txtsa.Text)

                     };


                RptviwerStudent.LocalReport.SetParameters(rptParams);

                RptviwerStudent.LocalReport.DataSources.Add(rds);
                RptviwerStudent.LocalReport.DataSources.Add(rds1);
                //RptviwerStudent.ProcessingMode = ProcessingMode.Local;


                //Refresh
                RptviwerStudent.LocalReport.Refresh();
                Warning[] warnings;

                string[] streamids;

                string mimeType;

                string encoding;

                string extension;

                string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
                       "  <PageWidth>8.0in</PageWidth>" +
                        "  <PageHeight>11in</PageHeight>" +
                        "  <MarginTop>0.1in</MarginTop>" +
                        "  <MarginLeft>0.1in</MarginLeft>" +
                         "  <MarginRight>0.0in</MarginRight>" +
                         "  <MarginBottom>0.1in</MarginBottom>" +
                       "</DeviceInfo>";

                byte[] bytes = RptviwerStudent.LocalReport.Render("PDF", devinfo, out mimeType, out encoding, out extension, out streamids, out warnings);


                Response.Buffer = true;

                Response.Clear();

                Response.ContentType = mimeType;

                Response.AddHeader("content-disposition", "inline; filename=MAKLUMAT_PERIBADI_ANGGOTA." + extension);

                Response.BinaryWrite(bytes);

                //Response.Write("<script>");
                //Response.Write("window.open('', '_newtab');");
                //Response.Write("</script>");
                Response.Flush();

                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Issue')", true);
        }
    }


    protected void ddkaw_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TextBox18.SelectedItem.Text == "SEMUA WILAYAH")
        {
            wilayah.Rows.Clear();
            TextBox8.Items.Clear();
            TextBox8.Items.Insert(0, new ListItem("SEMUA WILAYAH", ""));
        }
        //-Pusat---------------------------------------------------------------------------------
        string cmd6 = "select distinct wilayah_code,wilayah_name from  Ref_Cawangan where kavasan_name='" + TextBox18.SelectedItem.Text + "'";
        wilayah.Rows.Clear();
        TextBox8.Items.Clear();

        con.Open();
        SqlDataAdapter adapterP = new SqlDataAdapter(cmd6, con);
        adapterP.Fill(wilayah);

        TextBox8.DataSource = wilayah;
        TextBox8.DataTextField = "wilayah_name";
        TextBox8.DataValueField = "wilayah_code";
        TextBox8.DataBind();
        //ddPusat.Items.RemoveAt(0); //remove 'Semua Wilayah'
        con.Close();

        TextBox8.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }

    protected void ddwil_SelectedIndexChanged(object sender, EventArgs e)
    {

        //-Pusat---------------------------------------------------------------------------------
        string cmd5 = "select cawangan_name,cawangan_code from Ref_Cawangan where kavasan_name='" + TextBox18.SelectedItem.Text + "' and wilayah_name='" + TextBox8.SelectedItem.Text + "' ";

        con.Open();
        SqlDataAdapter adapterP = new SqlDataAdapter(cmd5, con);
        adapterP.Fill(pusat);

        TextBox9.DataSource = pusat;
        TextBox9.DataTextField = "cawangan_name";
        TextBox9.DataValueField = "cawangan_code";
        TextBox9.DataBind();
        //ddPusat.Items.RemoveAt(0); //remove 'Semua Wilayah'
        con.Close();

        TextBox9.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    }


}