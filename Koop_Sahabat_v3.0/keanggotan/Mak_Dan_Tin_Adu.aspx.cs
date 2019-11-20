using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Data;
using System.Threading;

public partial class Mak_Dan_Tin_Adu : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string Status = string.Empty;
    DataTable dt = new DataTable();
    DataTable stats = new DataTable();
    DBConnection DBCon = new DBConnection();
    string CommandArgument1;
    static string statename = string.Empty, Resitdate = string.Empty;
    string userid;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                assgn_roles();
                userid = Session["New"].ToString();
                TxtFdate.Attributes.Add("readonly", "readonly");
                TxtTdate.Attributes.Add("readonly", "readonly");
                bBind();
                if (Session["fdate"] != null && Session["tdate"] != null)
                {
                    TxtFdate.Text = Session["fdate"].ToString();
                    TxtTdate.Text = Session["tdate"].ToString();
                    ddadu.SelectedValue = Session["sts"].ToString();
                }
               
                //if (Session["level"].ToString() == "1")
                //{
                //    lev_id.Attributes.Add("style", "display:None;");
                //}
                //else
                //{
                //    lev_id.Attributes.Remove("style");
                //}
                BindData();
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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0133' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();
               


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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('116','1052','1072','1073','64','65','117','13','14','15','1074')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }


    }





        void bBind()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select keterangan,keterangan_code from ref_status_aduan order by keterangan";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddadu.DataSource = dt;
            ddadu.DataBind();
            ddadu.DataTextField = "keterangan";
            ddadu.DataValueField = "keterangan_code";
            ddadu.DataBind();
            ddadu.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void grdView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.BindData();
    }
    protected void grdView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //int index = Convert.ToInt32(e.CommandArgument);    
            //GridViewRow selectedRow = grdView.Rows[index];
            if (e.CommandName == "Remove" || e.CommandName == "Add")
            {

                switch (e.CommandName)
                {
                    case "Add":
                        statename = CommandArgument1;
                        Response.Redirect("~/keanggotan/Mak_Dan_Tin_Adu_pih.aspx?State=" + statename + "", false);
                        if (Session["New"] != null)
                        {
                            Session["BACKURL"] = "~/HomeDashboard.aspx";
                        }
                        break;
                    case "Remove":
                        // Use selectedRow to remove your rows
                        //button.ImageUrl = "~/images/active.gif";
                        //button.CommandName = "Add"0
                        break;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    //protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        //ImageButton button = (ImageButton)e.Row.FindControl("btnEdit");
    //        //button.CommandArgument = e.Row.RowIndex.ToString();

    //    }
    //}


    //protected void grdView_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}


    protected void lblSubItemName_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        string[] CommadArgument = btn.CommandArgument.Split(',');
        CommandArgument1 = CommadArgument[0];
        Session["fdate"] = TxtFdate.Text;
        Session["tdate"] = TxtTdate.Text;
        Session["sts"] = ddadu.SelectedValue;

    }

    void BindData()
    {
        string fdate = string.Empty, frdate = string.Empty, todate = string.Empty, tdate = string.Empty;
        if (TxtFdate.Text != "")
        {
            fdate = TxtFdate.Text;
            DateTime dt = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            frdate = dt.ToString("yyyy-MM-dd");
        }
        if (TxtTdate.Text != "")
        {
            todate = TxtTdate.Text;
            DateTime tod = DateTime.ParseExact(todate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            tdate = tod.ToString("yyyy-MM-dd");
        }
        userid = Session["New"].ToString();
        string Level = Session["level"].ToString();
        if (TxtFdate.Text != "" && TxtTdate.Text != "" && ddadu.SelectedItem.Value != "")
        {

            if (role_1 == "0")
            {
                string query1 = "Select distinct com_new_icno,case when ISNULL(MM.mem_name,'') = '' then ISNULL(cc1.stf_name,'') else ISNULL(MM.mem_name,'') end mem_name,comp_id,com_dt,com_new_icno,com_type_ind,cast(com_remark as char(25)) com_remark ,com_sts_cd,rc.cawangan_name,rsa.KETERANGAN from mem_complaint as mc Left join ref_status_aduan AS rsa ON rsa.KETERANGAN_Code=mc.com_sts_cd Left join mem_member AS mm on mm.mem_new_icno=mc.com_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan AS rc ON rc.Cawangan_code=mm.mem_branch_cd left join hr_staff_profile cc1 on cc1.stf_icno=mc.com_new_icno where mc.Acc_sts ='Y' and mc.com_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + frdate + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), 0) and mc.com_sts_cd='" + ddadu.SelectedItem.Value + "' and com_new_icno='" + userid + "' and com_sts='Y' order by comp_id ";
                SqlCommand com = new SqlCommand(query1);
                GridView1.DataSource = GetData(com);
            }
            else
            {
                if (TextBox1.Text == "")
                {
                    string query = "Select distinct com_new_icno,case when ISNULL(MM.mem_name,'') = '' then ISNULL(cc1.stf_name,'') else ISNULL(MM.mem_name,'') end mem_name,comp_id,com_dt,com_new_icno,com_type_ind,cast(com_remark as char(25)) com_remark ,com_sts_cd,rc.cawangan_name,rsa.KETERANGAN from mem_complaint as mc Left join ref_status_aduan AS rsa ON rsa.KETERANGAN_Code=mc.com_sts_cd Left join mem_member AS mm on mm.mem_new_icno=mc.com_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan AS rc ON rc.Cawangan_code=mm.mem_branch_cd left join hr_staff_profile cc1 on cc1.stf_icno=mc.com_new_icno where mc.Acc_sts ='Y' and mc.com_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + frdate + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), 0) and mc.com_sts_cd='" + ddadu.SelectedItem.Value + "' and com_sts='Y' order by comp_id ";
                    SqlCommand com = new SqlCommand(query);
                    //            Session["level"] = Level;
                    GridView1.DataSource = GetData(com);
                }
                else
                {
                    string query = "Select distinct com_new_icno,case when ISNULL(MM.mem_name,'') = '' then ISNULL(cc1.stf_name,'') else ISNULL(MM.mem_name,'') end mem_name,comp_id,com_dt,com_new_icno,com_type_ind,cast(com_remark as char(25)) com_remark ,com_sts_cd,rc.cawangan_name,rsa.KETERANGAN from mem_complaint as mc Left join ref_status_aduan AS rsa ON rsa.KETERANGAN_Code=mc.com_sts_cd Left join mem_member AS mm on mm.mem_new_icno=mc.com_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan AS rc ON rc.Cawangan_code=mm.mem_branch_cd left join hr_staff_profile cc1 on cc1.stf_icno=mc.com_new_icno where mc.Acc_sts ='Y' and mc.com_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + frdate + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), 0) and mc.com_sts_cd='" + ddadu.SelectedItem.Value + "' and mc.com_new_icno='" + TextBox1.Text + "' and com_sts='Y' order by comp_id ";
                    SqlCommand com = new SqlCommand(query);
                    //            Session["level"] = Level;
                    GridView1.DataSource = GetData(com);
                }

            }
            GridView1.DataBind();
            Session["fdate"] = "";
            Session["tdate"] = "";
            Session["sts"] = "";

        }
        else if (TxtFdate.Text != "" && TxtTdate.Text != "" && ddadu.SelectedItem.Value == "")
        {

            if (role_1 == "0")
            {
                string query1 = "Select distinct com_new_icno,case when ISNULL(MM.mem_name,'') = '' then ISNULL(cc1.stf_name,'') else ISNULL(MM.mem_name,'') end mem_name,comp_id,com_dt,com_new_icno,com_type_ind,cast(com_remark as char(25)) com_remark ,com_sts_cd,rc.cawangan_name,rsa.KETERANGAN from mem_complaint as mc Left join ref_status_aduan AS rsa ON rsa.KETERANGAN_Code=mc.com_sts_cd Left join mem_member AS mm on mm.mem_new_icno=mc.com_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan AS rc ON rc.Cawangan_code=mm.mem_branch_cd left join hr_staff_profile cc1 on cc1.stf_icno=mc.com_new_icno where mc.Acc_sts ='Y' and mc.com_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + frdate + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), 0)  and com_new_icno='" + userid + "' and com_sts='Y' order by comp_id ";
                SqlCommand com = new SqlCommand(query1);
                GridView1.DataSource = GetData(com);
            }
            else
            {
                if (TextBox1.Text == "")
                {
                    string query = "Select distinct com_new_icno,case when ISNULL(MM.mem_name,'') = '' then ISNULL(cc1.stf_name,'') else ISNULL(MM.mem_name,'') end mem_name,comp_id,com_dt,com_new_icno,com_type_ind,cast(com_remark as char(25)) com_remark ,com_sts_cd,rc.cawangan_name,rsa.KETERANGAN from mem_complaint as mc Left join ref_status_aduan AS rsa ON rsa.KETERANGAN_Code=mc.com_sts_cd Left join mem_member AS mm on mm.mem_new_icno=mc.com_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan AS rc ON rc.Cawangan_code=mm.mem_branch_cd left join hr_staff_profile cc1 on cc1.stf_icno=mc.com_new_icno where mc.Acc_sts ='Y' and mc.com_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + frdate + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), 0) and com_sts='Y' order by comp_id ";
                    SqlCommand com = new SqlCommand(query);
                    //            Session["level"] = Level;
                    GridView1.DataSource = GetData(com);
                }
                else
                {
                    string query = "Select distinct com_new_icno,case when ISNULL(MM.mem_name,'') = '' then ISNULL(cc1.stf_name,'') else ISNULL(MM.mem_name,'') end mem_name,comp_id,com_dt,com_new_icno,com_type_ind,cast(com_remark as char(25)) com_remark ,com_sts_cd,rc.cawangan_name,rsa.KETERANGAN from mem_complaint as mc Left join ref_status_aduan AS rsa ON rsa.KETERANGAN_Code=mc.com_sts_cd Left join mem_member AS mm on mm.mem_new_icno=mc.com_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan AS rc ON rc.Cawangan_code=mm.mem_branch_cd left join hr_staff_profile cc1 on cc1.stf_icno=mc.com_new_icno where mc.Acc_sts ='Y' and mc.com_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + frdate + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), 0) and mc.com_new_icno='" + TextBox1.Text + "' and com_sts='Y' order by comp_id ";
                    SqlCommand com = new SqlCommand(query);
                    //            Session["level"] = Level;
                    GridView1.DataSource = GetData(com);
                }

            }
            GridView1.DataBind();
            Session["fdate"] = "";
            Session["tdate"] = "";
            Session["sts"] = "";
        }
        else
        {

            if (role_1 == "0")
            {
                string query1 = "Select distinct com_new_icno,case when ISNULL(MM.mem_name,'') = '' then ISNULL(cc1.stf_name,'') else ISNULL(MM.mem_name,'') end mem_name,comp_id,com_dt,com_new_icno,com_type_ind,cast(com_remark as char(25)) com_remark ,com_sts_cd,rc.cawangan_name,rsa.KETERANGAN from mem_complaint as mc Left join ref_status_aduan AS rsa ON rsa.KETERANGAN_Code=mc.com_sts_cd Left join mem_member AS mm on mm.mem_new_icno=mc.com_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan AS rc ON rc.Cawangan_code=mm.mem_branch_cd left join hr_staff_profile cc1 on cc1.stf_icno=mc.com_new_icno where mc.Acc_sts ='Y' and com_new_icno='' and com_sts='Y' order by comp_id ";
                SqlCommand com = new SqlCommand(query1);
                GridView1.DataSource = GetData(com);
            }
            GridView1.DataBind();
            Session["fdate"] = "";
            Session["tdate"] = "";
            Session["sts"] = "";
        }

    }


    private DataTable GetData(SqlCommand cmd)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    return dt;
                }
            }
        }
    }


    protected void EditCustomer(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        this.BindData();
    }


    protected void btnreset_Click(object sender, EventArgs e)
    {
        Session["fdate"] = "";
        Session["tdate"] = "";
        Session["sts"] = "";
        Response.Redirect(Request.RawUrl);
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (TxtFdate.Text != "" && TxtTdate.Text != "")
        {
            this.BindData();
        }
        else
        { 
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh Mula & Tarikh Akhir.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            return;
        }

    }

    //void instaudt()
    //{
    //    string audcd = "000000";
    //    string auddec = "Carian/Senarai Aduan";
    //    //string usrid = Session["New"].ToString();
    //    string curdt =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //    string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc) values ('" + Session["New"].ToString() + "','" + curdt + "','" + audcd + "','" + auddec + "')";
    //    Status = DBCon.Ora_Execute_CommamdText(Inssql);
    //}

}

