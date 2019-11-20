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

public partial class TLAB_SENARI : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable wilayah = new DataTable();
    string level, userid;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.Button2);

        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                kawasan();
                //wilahBind();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('158','1052','1100','156','64','65','22','1034','148','25','157','14','1042','1101')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;
            
            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());

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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0147' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

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
            DD_kaw.DataValueField = "Area_Name";
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
        DataSet Ds = new DataSet();
        try
        {
            string com = "select distinct wilayah_code,wilayah_name from Ref_Cawangan where kavasan_name='" + DD_kaw.SelectedValue + "' order by wilayah_name asc ";
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
        DataSet Ds = new DataSet();
        try
        {
            string com = "select cawangan_code,cawangan_name From Ref_Cawangan where  kavasan_name='" + DD_kaw.SelectedValue + "' and wilayah_name='" + DD_wilayah.SelectedItem.Text + "' order by cawangan_name asc";
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
        Response.Redirect("../keanggotan/TLAB_SENARI.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (f_date.Text != "" && t_date.Text != "")
            {

                //Path
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                string fdate = f_date.Text;
                DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                String fmdate = fd.ToString("yyyy-MM-dd");

                string tdate = t_date.Text;
                DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                String tmdate = td.ToString("yyyy-MM-dd");

                disp_sts.Text = DD_STS_ANGGO.SelectedItem.Text;

                string skav_no = string.Empty;
                if (DD_kaw.SelectedValue != "")
                {
                    DataTable ddokdicno_pro = new DataTable();
                    ddokdicno_pro = DBCon.Ora_Execute_table("select DISTINCT Area_Code,Area_Name from Ref_Kawasan where Area_Name='" + DD_kaw.SelectedValue + "'");

                    skav_no = ddokdicno_pro.Rows[0]["Area_Code"].ToString();
                }
                //if (DD_STS_ANGGO.SelectedValue == "TL")
                //{
                //    if (DD_kaw.SelectedValue == "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                //    {
                //        dt = DBCon.Ora_Execute_table("select ISNULL(rk.Area_Name,'') s1,ISNULL(wilayah_name,'') s2,ISNULL(cawangan_name,'') s3,ISNULL(mem_centre,'') s4,ISNULL(mem_new_icno,'') s5,ISNULL(mem_name,'') s6,ISNULL(set_apply_amt,'') s7,FORMAT(set_appprove_dt,'dd/MM/yyyy') s8 From mem_settlement st inner join mem_member mm on mm.mem_new_icno=st.set_new_icno left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' group by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt order by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt ");
                //        ds.Tables.Add(dt);
                //    }
                //    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                //    {
                //        dt = DBCon.Ora_Execute_table("select ISNULL(rk.Area_Name,'') s1,ISNULL(wilayah_name,'') s2,ISNULL(cawangan_name,'') s3,ISNULL(mem_centre,'') s4,ISNULL(mem_new_icno,'') s5,ISNULL(mem_name,'') s6,ISNULL(set_apply_amt,'') s7,FORMAT(set_appprove_dt,'dd/MM/yyyy') s8 From mem_settlement st inner join mem_member mm on mm.mem_new_icno=st.set_new_icno left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and kawasan_code='" + skav_no + "' and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' group by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt order by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt ");
                //        ds.Tables.Add(dt);
                //    }
                //    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                //    {
                //        dt = DBCon.Ora_Execute_table("select ISNULL(rk.Area_Name,'') s1,ISNULL(wilayah_name,'') s2,ISNULL(cawangan_name,'') s3,ISNULL(mem_centre,'') s4,ISNULL(mem_new_icno,'') s5,ISNULL(mem_name,'') s6,ISNULL(set_apply_amt,'') s7,FORMAT(set_appprove_dt,'dd/MM/yyyy') s8 From mem_settlement st inner join mem_member mm on mm.mem_new_icno=st.set_new_icno left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and kawasan_code='" + skav_no + "' and wilayah_cd='" + DD_wilayah.SelectedValue + "' and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' group by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt order by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt ");
                //        ds.Tables.Add(dt);
                //    }
                //    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue != "" && DD_STS_ANGGO.SelectedValue != "")
                //    {
                //        dt = DBCon.Ora_Execute_table("select ISNULL(rk.Area_Name,'') s1,ISNULL(wilayah_name,'') s2,ISNULL(cawangan_name,'') s3,ISNULL(mem_centre,'') s4,ISNULL(mem_new_icno,'') s5,ISNULL(mem_name,'') s6,ISNULL(set_apply_amt,'') s7,FORMAT(set_appprove_dt,'dd/MM/yyyy') s8 From mem_settlement st inner join mem_member mm on mm.mem_new_icno=st.set_new_icno left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and kawasan_code='" + skav_no + "' and wilayah_cd='" + DD_wilayah.SelectedValue + "' and cawangan_cd='" + DD_cawangan.SelectedValue + "' and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' group by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt order by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt ");
                //        ds.Tables.Add(dt);
                //    }
                //    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue != "" && DD_STS_ANGGO.SelectedValue != "")
                //    {
                //        dt = DBCon.Ora_Execute_table("select ISNULL(rk.Area_Name,'') s1,ISNULL(wilayah_name,'') s2,ISNULL(cawangan_name,'') s3,ISNULL(mem_centre,'') s4,ISNULL(mem_new_icno,'') s5,ISNULL(mem_name,'') s6,ISNULL(set_apply_amt,'') s7,FORMAT(set_appprove_dt,'dd/MM/yyyy') s8 From mem_settlement st inner join mem_member mm on mm.mem_new_icno=st.set_new_icno left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and kawasan_code='" + skav_no + "' and wilayah_cd='" + DD_wilayah.SelectedValue + "' and cawangan_cd='" + DD_cawangan.SelectedValue + "' and mem_centre like ('%" + txt_pusat.Text + "%') and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' group by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt order by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt ");
                //        ds.Tables.Add(dt);
                //    }
                //    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                //    {
                //        dt = DBCon.Ora_Execute_table("select ISNULL(rk.Area_Name,'') s1,ISNULL(wilayah_name,'') s2,ISNULL(cawangan_name,'') s3,ISNULL(mem_centre,'') s4,ISNULL(mem_new_icno,'') s5,ISNULL(mem_name,'') s6,ISNULL(set_apply_amt,'') s7,FORMAT(set_appprove_dt,'dd/MM/yyyy') s8 From mem_settlement st inner join mem_member mm on mm.mem_new_icno=st.set_new_icno left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and kawasan_code='" + skav_no + "' and mem_centre like ('%" + txt_pusat.Text + "%') and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' group by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt order by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt ");
                //        ds.Tables.Add(dt);
                //    }
                //    else if (DD_kaw.SelectedValue == "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                //    {
                //        dt = DBCon.Ora_Execute_table("select ISNULL(rk.Area_Name,'') s1,ISNULL(wilayah_name,'') s2,ISNULL(cawangan_name,'') s3,ISNULL(mem_centre,'') s4,ISNULL(mem_new_icno,'') s5,ISNULL(mem_name,'') s6,ISNULL(set_apply_amt,'') s7,FORMAT(set_appprove_dt,'dd/MM/yyyy') s8 From mem_settlement st inner join mem_member mm on mm.mem_new_icno=st.set_new_icno left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_centre like ('%" + txt_pusat.Text + "%') and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' group by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt order by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt ");
                //        ds.Tables.Add(dt);
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Rekod Tidak Dijumpai.');", true);//
                //    }
                //}
                //else if (DD_STS_ANGGO.SelectedValue == "L")
                //{
                if (DD_kaw.SelectedValue == "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                {
                    //dt = DBCon.Ora_Execute_table("select ISNULL(rk.Area_Name,'') s1,ISNULL(wilayah_name,'') s2,ISNULL(cawangan_name,'') s3,ISNULL(mem_centre,'') s4,ISNULL(mem_new_icno,'') s5,ISNULL(mem_name,'') s6,ISNULL(set_apply_amt,'') s7,FORMAT(set_appprove_dt,'dd/MM/yyyy') s8 From mem_settlement st inner join mem_member mm on mm.mem_new_icno=st.set_new_icno left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' group by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt order by Area_Name,wilayah_name,cawangan_name,mem_centre,mem_new_icno,mem_name,set_apply_amt,set_appprove_dt ");
                    dt = DBCon.Ora_Execute_table("select ISNULL(asb.cawangan_name,'') s1,asb.mem_centre s2,COUNT(ISNULL(asb.mem_new_icno,'')) s3,SUM(asb.set_apply_amt) s4 from (select mem_new_icno,set_apply_amt,cawangan_name,mem_centre from mem_settlement st left join mem_member mm on mm.mem_new_icno=st.set_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where st.Acc_sts ='Y' and set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)  and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' group by mem_new_icno,set_apply_amt,cawangan_name,mem_centre) asb group by asb.cawangan_name,asb.mem_centre order by asb.cawangan_name,asb.mem_centre");
                    ds.Tables.Add(dt);
                    if (dt.Rows.Count % 20 != 0)
                    {
                        int addCount = 20 - dt.Rows.Count % 20;
                        for (int i = 0; i < addCount; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = "";
                            dt.Rows.Add(dr);
                        }
                    }
                }
                else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                {
                    dt = DBCon.Ora_Execute_table("select ISNULL(asb.cawangan_name,'') s1,asb.mem_centre s2,COUNT(ISNULL(asb.mem_new_icno,'')) s3,SUM(asb.set_apply_amt) s4 from (select mem_new_icno,set_apply_amt,cawangan_name,mem_centre from mem_settlement st left join mem_member mm on mm.mem_new_icno=st.set_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where st.Acc_sts ='Y' and set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and kawasan_code='" + skav_no + "' and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "'  group by mem_new_icno,set_apply_amt,cawangan_name,mem_centre) asb group by asb.cawangan_name,asb.mem_centre order by asb.cawangan_name,asb.mem_centre");
                    ds.Tables.Add(dt);
                    if (dt.Rows.Count % 20 != 0)
                    {
                        int addCount = 20 - dt.Rows.Count % 20;
                        for (int i = 0; i < addCount; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = "";
                            dt.Rows.Add(dr);
                        }
                    }
                    //
                }
                else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                {
                    dt = DBCon.Ora_Execute_table("select ISNULL(asb.cawangan_name,'') s1,asb.mem_centre s2,COUNT(ISNULL(asb.mem_new_icno,'')) s3,SUM(asb.set_apply_amt) s4 from (select mem_new_icno,set_apply_amt,cawangan_name,mem_centre from mem_settlement st left join mem_member mm on mm.mem_new_icno=st.set_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where st.Acc_sts ='Y' and set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and kawasan_code='" + skav_no + "' and wilayah_cd='" + DD_wilayah.SelectedValue + "' and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "'  group by mem_new_icno,set_apply_amt,cawangan_name,mem_centre) asb group by asb.cawangan_name,asb.mem_centre order by asb.cawangan_name,asb.mem_centre");
                    ds.Tables.Add(dt);
                    if (dt.Rows.Count % 20 != 0)
                    {
                        int addCount = 20 - dt.Rows.Count % 20;
                        for (int i = 0; i < addCount; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = "";
                            dt.Rows.Add(dr);
                        }
                    }
                }
                else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue != "" && DD_STS_ANGGO.SelectedValue != "")
                {
                    dt = DBCon.Ora_Execute_table("select ISNULL(asb.cawangan_name,'') s1,asb.mem_centre s2,COUNT(ISNULL(asb.mem_new_icno,'')) s3,SUM(asb.set_apply_amt) s4 from (select mem_new_icno,set_apply_amt,cawangan_name,mem_centre from mem_settlement st left join mem_member mm on mm.mem_new_icno=st.set_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where st.Acc_sts ='Y' and set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and kawasan_code='" + skav_no + "' and wilayah_cd='" + DD_wilayah.SelectedValue + "' and cawangan_cd='" + DD_cawangan.SelectedValue + "' and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "'  group by mem_new_icno,set_apply_amt,cawangan_name,mem_centre) asb group by asb.cawangan_name,asb.mem_centre order by asb.cawangan_name,asb.mem_centre");
                    ds.Tables.Add(dt);
                    if (dt.Rows.Count % 20 != 0)
                    {
                        int addCount = 20 - dt.Rows.Count % 20;
                        for (int i = 0; i < addCount; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = "";
                            dt.Rows.Add(dr);
                        }
                    }
                }
                else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue != "" && DD_STS_ANGGO.SelectedValue != "")
                {
                    dt = DBCon.Ora_Execute_table("select ISNULL(asb.cawangan_name,'') s1,asb.mem_centre s2,COUNT(ISNULL(asb.mem_new_icno,'')) s3,SUM(asb.set_apply_amt) s4 from (select mem_new_icno,set_apply_amt,cawangan_name,mem_centre from mem_settlement st left join mem_member mm on mm.mem_new_icno=st.set_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where st.Acc_sts ='Y' and set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and kawasan_code='" + skav_no + "' and wilayah_cd='" + DD_wilayah.SelectedValue + "' and cawangan_cd='" + DD_cawangan.SelectedValue + "' and mem_centre like ('%" + txt_pusat.Text + "%') and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "'  group by mem_new_icno,set_apply_amt,cawangan_name,mem_centre) asb group by asb.cawangan_name,asb.mem_centre order by asb.cawangan_name,asb.mem_centre");
                    ds.Tables.Add(dt);
                    if (dt.Rows.Count % 20 != 0)
                    {
                        int addCount = 20 - dt.Rows.Count % 20;
                        for (int i = 0; i < addCount; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = "";
                            dt.Rows.Add(dr);
                        }
                    }
                }
                else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                {
                    dt = DBCon.Ora_Execute_table("select ISNULL(asb.cawangan_name,'') s1,asb.mem_centre s2,COUNT(ISNULL(asb.mem_new_icno,'')) s3,SUM(asb.set_apply_amt) s4 from (select mem_new_icno,set_apply_amt,cawangan_name,mem_centre from mem_settlement st left join mem_member mm on mm.mem_new_icno=st.set_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where st.Acc_sts ='Y' and set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and kawasan_code='" + skav_no + "' and mem_centre like ('%" + txt_pusat.Text + "%') and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "'  group by mem_new_icno,set_apply_amt,cawangan_name,mem_centre) asb group by asb.cawangan_name,asb.mem_centre order by asb.cawangan_name,asb.mem_centre");
                    ds.Tables.Add(dt);
                    if (dt.Rows.Count % 20 != 0)
                    {
                        int addCount = 20 - dt.Rows.Count % 20;
                        for (int i = 0; i < addCount; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = "";
                            dt.Rows.Add(dr);
                        }
                    }
                }
                else if (DD_kaw.SelectedValue == "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                {
                    dt = DBCon.Ora_Execute_table("select ISNULL(asb.cawangan_name,'') s1,asb.mem_centre s2,COUNT(ISNULL(asb.mem_new_icno,'')) s3,SUM(asb.set_apply_amt) s4 from (select mem_new_icno,set_apply_amt,cawangan_name,mem_centre from mem_settlement st left join mem_member mm on mm.mem_new_icno=st.set_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Kawasan rk on rk.Area_Code=MM.mem_area_cd where st.Acc_sts ='Y' and set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_centre like ('%" + txt_pusat.Text + "%') and set_approve_sts_cd='" + DD_STS_ANGGO.SelectedValue + "'  group by mem_new_icno,set_apply_amt,cawangan_name,mem_centre) asb group by asb.cawangan_name,asb.mem_centre order by asb.cawangan_name,asb.mem_centre");
                    ds.Tables.Add(dt);
                    if (dt.Rows.Count % 20 != 0)
                    {
                        int addCount = 20 - dt.Rows.Count % 20;
                        for (int i = 0; i < addCount; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = "";
                            dt.Rows.Add(dr);
                        }
                    }
                }

                else
                {
                  
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);//


                }

                //}

                string ss1 = string.Empty, ss2 = string.Empty, ss3 = string.Empty, ss4 = string.Empty, ss5 = string.Empty, ss6 = string.Empty, ss7 = string.Empty;


                if (DD_kaw.SelectedValue != "")
                {
                    ss1 = DD_kaw.SelectedItem.Text;
                }
                else
                {
                    ss1 = "SEMUA";
                }
                if (DD_wilayah.SelectedValue != "")
                {
                    ss2 = DD_wilayah.SelectedItem.Text;
                }
                else
                {
                    ss2 = "SEMUA";
                }
                if (DD_cawangan.SelectedValue != "")
                {
                    ss3 = DD_cawangan.SelectedItem.Text;
                }
                else
                {
                    ss3 = "SEMUA";
                }
                if (txt_pusat.Text != "")
                {
                    ss4 = txt_pusat.Text;
                }
                else
                {
                    ss4 = "SEMUA";
                }



                RptviwerLKSENARI.Reset();
                RptviwerLKSENARI.LocalReport.Refresh();
                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();
                string disp = string.Empty;
                if (countRow != 0)
                {
                    ss1_stap1.Visible = true;
                    RptviwerLKSENARI.LocalReport.DataSources.Clear();

                    //if (DD_STS_ANGGO.SelectedValue == "TL")
                    //{
                    //    RptviwerLKSENARI.LocalReport.ReportPath = "TLAB_SENARI.rdlc";
                    //    ReportDataSource rds = new ReportDataSource("TLAB_SENARI", dt);
                    //    RptviwerLKSENARI.LocalReport.DataSources.Add(rds);
                    //    disp = "TIDAK_LULUS_ANGGOTA_BAHARU_SENERAI_" + DateTime.Now.ToString("ddMMyyyy");
                    //}
                    //else if (DD_STS_ANGGO.SelectedValue=="L")
                    //{
                    RptviwerLKSENARI.LocalReport.ReportPath = "keanggotan/TLAB_RINGASAN.rdlc";
                    ReportDataSource rds = new ReportDataSource("TLAB_RINGASAN", dt);
                    RptviwerLKSENARI.LocalReport.DataSources.Add(rds);
                    disp = "LAPORAN " + disp_sts.Text + " ANGGOTA BAHARU (RINGKASAN)_" + DateTime.Now.ToString("ddMMyyyy");
                    //}



                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("p1", f_date.Text),
                     new ReportParameter("p2", t_date.Text),
                     new ReportParameter("p3", ss1),
                     new ReportParameter("p4", ss2),
                     new ReportParameter("p5", ss3),
                     new ReportParameter("p6", ss4),
                     new ReportParameter("p7", disp_sts.Text)
                     };

                    RptviwerLKSENARI.LocalReport.SetParameters(rptParams);
                    RptviwerLKSENARI.LocalReport.DisplayName = disp;

                    //Refresh

                    RptviwerLKSENARI.LocalReport.Refresh();
                    System.Threading.Thread.Sleep(1);

                }
                else
                {
                    ss1_stap1.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);


                }

            }
            else
            {
                ss1_stap1.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Response.Redirect("LK_SENARI.aspx");
        }
    }

}