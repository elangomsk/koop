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
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web.SessionState;
using System.Xml;
using Microsoft.Reporting.WinForms;
using Microsoft.Reporting.WebForms;

public partial class DIV_RINGKASAN : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable wilayah = new DataTable();
    string level, userid;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    string strQuery = string.Empty;
    string strJenisAD = string.Empty;
    string strJenisLap = string.Empty;
    string strArea = string.Empty;
    string strRegion = string.Empty;
    string strBranch = string.Empty;
    string strQry = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        

        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                //wilahBind();
                bindDD_JenisAD();
                bindDD_JenisLap();
                kawasan();
                //sts_anggota();
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
            assgn_roles();
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1095','1052','1096','153','64','65','22','1034','148','14','1042','891')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;




            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            //ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            //ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            //Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
       
    }

    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0145' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();
                if (role_add == "1")
                {
                    Button2.Visible = true;
                }
                else
                {
                    Button2.Visible = false;
                }

            }
        }
    }
    
    private void bindDD_JenisAD()
    {
        DataSet ds = new DataSet();
        try
        {
            string com = "select distinct div_remark from mem_divident where div_approve_ind='SA'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_JenisAD.DataSource = dt;
            DD_JenisAD.DataTextField = "div_remark";
            DD_JenisAD.DataValueField = "div_remark";
            DD_JenisAD.DataBind();
            //DD_JenisAD.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void bindDD_JenisLap()
    {
        DataSet ds = new DataSet();
        try
        {
            ListItem li1 = new ListItem();
            li1.Text = "Semua";
            li1.Value = "0";

            ListItem li2 = new ListItem();
            li2.Text = "Bayar ke Akaun Bank";
            li2.Value = "1";

            ListItem li3 = new ListItem();
            li3.Text = "Kredit ke Akaun Syer";
            li3.Value = "2";

            ListItem li4 = new ListItem();
            li4.Text = "Tidak Lulus";
            li4.Value = "3";

            DD_JenisLap.Items.Add(li1);
            DD_JenisLap.Items.Add(li2);
            DD_JenisLap.Items.Add(li3);
            DD_JenisLap.Items.Add(li4);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void kawasan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select DISTINCT Area_Code,Area_Name from Ref_Kawasan ORDER BY Area_Name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_kaw.DataSource = dt;
            DD_kaw.DataTextField = "Area_Name";
            DD_kaw.DataValueField = "Area_Code";
            DD_kaw.DataBind();
            DD_kaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

  
    protected void DD_kaw_SelectedIndexChanged(object sender, EventArgs e)
    {
        string script1 = "$(function () { $('#GridView1').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); $('.select2').select2() });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
        DataSet Ds = new DataSet();
        try
        {
            string com = "select distinct wilayah_code,wilayah_name from Ref_Cawangan where kavasan_name='" + DD_kaw.SelectedItem.Text + "' order by wilayah_name asc ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_wilayah.DataSource = dt;
            DD_wilayah.DataTextField = "wilayah_name";
            DD_wilayah.DataValueField = "wilayah_code";
            DD_wilayah.DataBind();
            DD_wilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
      
    }

    protected void DD_wilayah_SelectedIndexChanged(object sender, EventArgs e)
    {
        string script1 = "$(function () { $('#GridView1').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); $('.select2').select2() });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
        DataSet Ds = new DataSet();
        try
        {
            string com = "select cawangan_code,cawangan_name From Ref_Cawangan where kavasan_name='" + DD_kaw.SelectedItem.Text + "' and wilayah_name='" + DD_wilayah.SelectedItem.Text + "' order by cawangan_name asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_cawangan.DataSource = dt;
            DD_cawangan.DataTextField = "cawangan_name";
            DD_cawangan.DataValueField = "cawangan_code";
            DD_cawangan.DataBind();
            DD_cawangan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
       
    }

    protected void rst_clk(object sender, EventArgs e)
    {
        Response.Redirect("../keanggotan/DIV_RINGKASAN.aspx");
    }

    void get_filt_details()
    {
        strJenisAD = DD_JenisAD.SelectedValue;
        strJenisLap = DD_JenisLap.SelectedValue;
        strArea = DD_kaw.SelectedValue;
        strRegion = DD_wilayah.SelectedValue;
        strBranch = DD_cawangan.SelectedValue;
        if (strJenisAD != "" && strArea == "" && strRegion == "" && strBranch == "")
        {
            strQry = "and div_remark='"+ strJenisAD + "'";
        }
        else if (strJenisAD != "" && strArea != "" && strRegion == "" && strBranch == "")
        {
            strQry = "and div_remark='" + strJenisAD + "' and s1.kawasan_code='"+ strArea + "'";
        }
        else if (strJenisAD != "" && strArea != "" && strRegion != "" && strBranch == "")
        {
            strQry = "and div_remark='" + strJenisAD + "' and s1.kawasan_code='" + strArea + "' and s1.wilayah_code='"+ strRegion + "'";
        }
        else if (strJenisAD != "" && strArea != "" && strRegion != "" && strBranch != "")
        {
            strQry = "and div_remark='" + strJenisAD + "' and s1.kawasan_code='" + strArea + "' and s1.wilayah_code='" + strRegion + "' and s1.cawangan_code='" + strBranch + "'";
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            if (DD_JenisAD.SelectedValue != "")
            {
                get_filt_details();
                DataTable dtDataLaporan = new DataTable();
                if (strJenisLap == "0") //semua
                {
                    strQuery = "select s1.kavasan_name,s1.wilayah_name,s1.cawangan_name,div_remark,count(div_new_icno) Bil,sum(div_debit_amt) Jum from mem_divident md"
                                    + " left join mem_member mm on mm.mem_new_icno=div_new_icno and mm.Acc_sts='Y'"
                                    + "left join Ref_Cawangan s1 on s1.cawangan_code=mm.mem_branch_cd"
                                    + " where md.Acc_sts='Y' and ISNULL(div_approve_ind,'') = 'SA' " + strQry + " group by s1.kavasan_name,s1.wilayah_name,s1.cawangan_name,div_remark";

                    dtDataLaporan = DBCon.Ora_Execute_table(strQuery);
                }
                else if (strJenisLap == "1")
                {
                    strQuery = "select s1.kavasan_name,s1.wilayah_name,s1.cawangan_name,div_remark,count(div_new_icno) Bil,sum(div_debit_amt) Jum from mem_divident md"
                                     + " left join mem_member mm on mm.mem_new_icno=div_new_icno and mm.Acc_sts='Y'"
                                     + "left join Ref_Cawangan s1 on s1.cawangan_code=mm.mem_branch_cd"
                                     + " where md.Acc_sts='Y' and ISNULL(div_approve_ind,'') = 'SA' and div_bank_cd<>'CR SYER' " + strQry + " group by s1.kavasan_name,s1.wilayah_name,s1.cawangan_name,div_remark";
                    dtDataLaporan = DBCon.Ora_Execute_table(strQuery);
                }
                else if (strJenisLap == "2")
                {
                    strQuery = "select s1.kavasan_name,s1.wilayah_name,s1.cawangan_name,div_remark,count(div_new_icno) Bil,sum(div_debit_amt) Jum from mem_divident md"
                                    + " left join mem_member mm on mm.mem_new_icno=div_new_icno and mm.Acc_sts='Y'"
                                    + "left join Ref_Cawangan s1 on s1.cawangan_code=mm.mem_branch_cd"
                                    + " where md.Acc_sts='Y' and ISNULL(div_approve_ind,'') = 'SA' and div_bank_cd='CR SYER' " + strQry + " group by s1.kavasan_name,s1.wilayah_name,s1.cawangan_name,div_remark";
                    dtDataLaporan = DBCon.Ora_Execute_table(strQuery);
                }
                else if (strJenisLap == "3") //semua
                {
                    strQuery = "select s1.kavasan_name,s1.wilayah_name,s1.cawangan_name,div_remark,count(div_new_icno) Bil,sum(div_debit_amt) Jum from mem_divident md"
                                    + " left join mem_member mm on mm.mem_new_icno=div_new_icno and mm.Acc_sts='Y'"
                                    + "left join Ref_Cawangan s1 on s1.cawangan_code=mm.mem_branch_cd"
                                    + " where md.Acc_sts='Y' and ISNULL(div_approve_ind,'') IN ('','TS') " + strQry + " group by s1.kavasan_name,s1.wilayah_name,s1.cawangan_name,div_remark";

                    dtDataLaporan = DBCon.Ora_Execute_table(strQuery);
                }
                if (dtDataLaporan != null)
                {
                    ds.Tables.Add(dtDataLaporan);
                    //Building an HTML string.
                    StringBuilder htmlTable = new StringBuilder();
                    htmlTable.Append("<table border='1' id='GridView1' cell style='width:100%;' class='table uppercase table-striped'>");
                    htmlTable.Append("<tr style='background-color:#BDC4C7;'> <th> NAMA Kawasan </th><th> NAMA WILLAYA </th><th> NAMA CAWANGAN </th><th> BIL. ANGGOTA </th><th> Jumlah Dividen (RM) </th></tr>");

                    if (!object.Equals(ds.Tables[0], null))
                    {
                        //Building the Data rows.
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                htmlTable.Append("<tr class='text-uppercase'>");                                
                                htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["kavasan_name"] + "</td>");
                                htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["wilayah_name"] + "</td>");
                                htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["cawangan_name"] + "</td>");
                                htmlTable.Append("<td align='center'>" + ds.Tables[0].Rows[i]["Bil"] + "</td>");                                
                                htmlTable.Append("<td align='Right'>" + double.Parse(ds.Tables[0].Rows[i]["Jum"].ToString()).ToString("C").Replace("RM", "").Replace("$", "") + "</td>");
                                htmlTable.Append("</tr>");
                            }                            
                            htmlTable.Append("</table>");
                            PlaceHolder2.Controls.Add(new Literal { Text = htmlTable.ToString() });
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

                        }

                    }
                  
                }
                else
                {                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
            string script1 = "$(function () { $('#GridView1').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); $('.select2').select2() });";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}