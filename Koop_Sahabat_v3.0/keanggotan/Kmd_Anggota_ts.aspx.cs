using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Net;
using System.Threading;

public partial class Kmd_Anggota_ts : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty, Status1 = string.Empty;
    string userid;
    
    string uniqueId, uniqueId2, uniqueId3, unq_id1, unq_id2, unq_id3;
    string jurnal_qry = string.Empty;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;

    SqlCommand strQuery;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                TextBox2.Attributes.Add("readonly", "readonly");
                TextBox2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                kawasan();
                Batch();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    void Batch()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select div_batch_name from mem_divident where  div_approve_ind='SA' and div_pay_dt ='1900-01-01 00:00:00.000' group by div_batch_name order by div_batch_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            adpt.SelectCommand.CommandTimeout = 180;
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "div_batch_name";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--- PILIH ---", ""));

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
            assgn_roles();
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('144','1052','1082','70','66','22','37','73','71','14','1083','1042','1084','61','883')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            s_update.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
           // Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());

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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0151' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();
                if (role_add == "1")
                {
                    Button1.Visible = true;
                }
                else
                {
                    Button1.Visible = false;
                }

            }
        }
    }



    void kawasan()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select kavasan_name from Ref_Cawangan  group by kavasan_name order by kavasan_name asc";
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

    protected void gvUserInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            con.Open();
            var ddl = (DropDownList)e.Row.FindControl("Bank_details");
            //int CountryId = Convert.ToInt32(e.Row.Cells[0].Text);
            SqlCommand cmd = new SqlCommand("select * from Ref_Nama_Bank ORDER BY Id ASC", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddl.DataSource = ds;
            ddl.DataTextField = "Bank_Name";
            ddl.DataValueField = "Bank_Code";
            ddl.DataBind();
            ddl.SelectedValue = ((DataRowView)e.Row.DataItem)["div_bank_cd"].ToString();
            ddl.Items.Insert(0, new ListItem("--- PILIH ---", "0"));

            System.Web.UI.WebControls.TextBox Label1_txt = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("Label10");

            //if (Label1_txt.Text != "")
            //{
            //    Label1_txt.Attributes.Add("Readonly", "Readonly");
            //    Label1_txt.Attributes.Add("Style", "Pointer-events:None;");
            //}
            //else
            //{
            //    Label1_txt.Attributes.Remove("Readonly");
            //    Label1_txt.Attributes.Remove("Style");
            //}
        }
    }


    public void showgrid()
    {
        DataTable dt = new DataTable();
        con1 = new SqlConnection(cs);
        DataTable Check_kaw = new DataTable();
        Check_kaw = DBCon.Ora_Execute_table("select DISTINCT kavasan_name,kawasan_code from Ref_Cawangan where kavasan_name='" + TextBox18.SelectedItem.Text + "'");
        con1.Open();
        SqlDataAdapter sda = new SqlDataAdapter();
        //string strQuery = "select d.div_new_icno,m.mem_name,m.mem_member_no,m.mem_branch_cd,m.mem_centre,d.div_debit_amt,d.div_bank_acc_no,d.div_bank_cd,d.div_pay_dt,rnb.Bank_Code from mem_member m ,mem_divident d left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd where m.mem_new_icno=d.div_new_icno and d.div_batch_name='" + TextBox1.Text + "' and d.div_approve_ind='L'";

        if (DropDownList1.SelectedItem.Text != "" && TextBox18.SelectedItem.Text == "--- PILIH ---" && s_icno.Text == "")
        {
            if (s_update.Checked == true)
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,case when d.div_bank_acc_no = '' then m.mem_member_no else d.div_bank_acc_no end as div_bank_acc_no,case when d.div_bank_cd = '' then 'CR SYER' else d.div_bank_cd end as div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN Convert(varchar(10),CONVERT(date, CAST(GETDATE() AS DATE),106),103) ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y'  left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cd left join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
            else
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,case when d.div_bank_acc_no = '' then m.mem_member_no else d.div_bank_acc_no end as div_bank_acc_no,case when d.div_bank_cd = '' then 'CR SYER' else d.div_bank_cd end as div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN Convert(varchar(10),CONVERT(date, CAST(GETDATE() AS DATE),106),103) ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y'  left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cd left join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and d.div_approve_ind='SA' and ISNULL(d.div_pay_dt,'') = '1900-01-01' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
        }
        else if (DropDownList1.SelectedItem.Text != "" && TextBox18.SelectedItem.Text != "--- PILIH ---" && s_icno.Text == "")
        {
            if (s_update.Checked == true)
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,case when d.div_bank_acc_no = '' then m.mem_member_no else d.div_bank_acc_no end as div_bank_acc_no,case when d.div_bank_cd = '' then 'CR SYER' else d.div_bank_cd end as div_bank_cd, ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN Convert(varchar(10),CONVERT(date, CAST(GETDATE() AS DATE),106),103) ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y'  left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cd left join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and m.mem_area_cd='" + Check_kaw.Rows[0]["kawasan_code"].ToString() + "' and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
            else
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,case when d.div_bank_acc_no = '' then m.mem_member_no else d.div_bank_acc_no end as div_bank_acc_no,case when d.div_bank_cd = '' then 'CR SYER' else d.div_bank_cd end as div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN Convert(varchar(10),CONVERT(date, CAST(GETDATE() AS DATE),106),103) ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y' left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd  left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cdleft join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and m.mem_area_cd='" + Check_kaw.Rows[0]["kawasan_code"].ToString() + "' and ISNULL(d.div_pay_dt,'') = '1900-01-01' and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
        }
        else if (DropDownList1.SelectedItem.Text != "" && TextBox18.SelectedItem.Text == "--- PILIH ---" && s_icno.Text != "")
        {
            if (s_update.Checked == true)
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,case when d.div_bank_acc_no = '' then m.mem_member_no else d.div_bank_acc_no end as div_bank_acc_no,case when d.div_bank_cd = '' then 'CR SYER' else d.div_bank_cd end as div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN Convert(varchar(10),CONVERT(date, CAST(GETDATE() AS DATE),106),103) ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y' left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cd left join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and d.div_new_icno='" + s_icno.Text + "' and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
            else
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,case when d.div_bank_acc_no = '' then m.mem_member_no else d.div_bank_acc_no end as div_bank_acc_no,case when d.div_bank_cd = '' then 'CR SYER' else d.div_bank_cd end as div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN Convert(varchar(10),CONVERT(date, CAST(GETDATE() AS DATE),106),103) ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y' left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cd left join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and d.div_new_icno='" + s_icno.Text + "' and ISNULL(d.div_pay_dt,'') = '1900-01-01' and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
        }
        else if (DropDownList1.SelectedItem.Text != "" && TextBox18.SelectedItem.Text != "--- PILIH ---" && s_icno.Text != "")
        {
            if (s_update.Checked == true)
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,case when d.div_bank_acc_no = '' then m.mem_member_no else d.div_bank_acc_no end as div_bank_acc_no,case when d.div_bank_cd = '' then 'CR SYER' else d.div_bank_cd end as div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN Convert(varchar(10),CONVERT(date, CAST(GETDATE() AS DATE),106),103) ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y'  left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cd left join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and mem_area_cd='" + Check_kaw.Rows[0]["kawasan_code"].ToString() + "' and d.div_new_icno='" + s_icno.Text + "' and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
            else
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,case when d.div_bank_acc_no = '' then m.mem_member_no else d.div_bank_acc_no end as div_bank_acc_no,case when d.div_bank_cd = '' then 'CR SYER' else d.div_bank_cd end as div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN Convert(varchar(10),CONVERT(date, CAST(GETDATE() AS DATE),106),103) ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y'  left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cd left join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and mem_area_cd='" + Check_kaw.Rows[0]["kawasan_code"].ToString() + "' and d.div_new_icno='" + s_icno.Text + "' and ISNULL(d.div_pay_dt,'') = '1900-01-01' and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
        }


        SqlDataAdapter da = new SqlDataAdapter(strQuery);
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
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Rekod Tidak Dijumpai.</strong></center>";
            shw_cnt1.Visible = false;
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
            shw_cnt1.Visible = true;
        }
        con.Close();

        //Button1.Visible = true;
        //Button3.Visible = true;
    }

    protected void OnCheckedChanged(object sender, EventArgs e)
    {
        bool isUpdateVisible = false;
        CheckBox chk = (sender as CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        CheckBox chkAll = (GridView1.HeaderRow.FindControl("chkAll") as CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
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

    
    protected void btnserch_Click(object sender, EventArgs e)
    {

        if (DropDownList1.SelectedItem.Text != "")
        {
          
            showgrid();
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nama Kelompok.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    //protected void btnprcess_Click(object sender, EventArgs e)
    //{
    //    if (DropDownList1.SelectedItem.Text != "")
    //    {
    //        using (SqlConnection con = new SqlConnection(cs))
    //        {
    //            string strDate =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    //            foreach (GridViewRow gvrow in GridView1.Rows)
    //            {

    //                var checkbox = gvrow.FindControl("chkSelect") as CheckBox;
    //                if (checkbox.Checked == true)
    //                {

    //                    var lblID = gvrow.FindControl("Label2") as Label;
    //                    var Ran_ID = gvrow.FindControl("Label1") as Label;
    //                    var deb_amt = gvrow.FindControl("Label7") as Label;
    //                    TextBox box1 = (TextBox)gvrow.FindControl("Label8");
    //                    TextBox box3 = (TextBox)gvrow.FindControl("Label10");
    //                    string userid = Session["New"].ToString();
    //                    DropDownList ddl = (DropDownList)gvrow.FindControl("Bank_details");
    //                    string bdate = box3.Text;
    //                    String up_date;
    //                    if (bdate != "")
    //                    {
    //                        DateTime bd = DateTime.ParseExact(bdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
    //                        up_date = bd.ToString("yyyy-mm-dd");
    //                    }
    //                    else
    //                    {
    //                        up_date = "";
    //                    }
    //                    DataTable det_noanggota = new DataTable();
    //                    det_noanggota = DBCon.Ora_Execute_table("select div_new_icno,ISNULL(CASE WHEN CONVERT(DATE, div_pay_dt) = '1900-01-01' THEN '' ELSE Convert(varchar(10),CONVERT(date,div_pay_dt,106),103) END, '') AS div_pay_dt from mem_divident WHERE div_new_icno='" + lblID.Text + "' and ID='" + Ran_ID.Text + "' and Acc_sts ='Y'");
    //                    if (det_noanggota.Rows[0]["div_pay_dt"].ToString() == "")
    //                    {


    //                        string Inssql1 = "Update mem_divident SET div_bank_acc_no = '" + box1.Text + "', div_bank_cd = '" + ddl.SelectedValue + "',div_pay_id='" + userid + "', div_pay_dt = '" + up_date + "', div_upd_id='" + userid + "', div_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE div_new_icno='" + lblID.Text + "' and ID='" + Ran_ID.Text + "' and Acc_sts ='Y'";
    //                        Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);

    //                        if (Status1 == "SUCCESS")
    //                        {
    //                            string Inssql = "insert into mem_share (sha_new_icno,sha_txn_dt,sha_batch_name,sha_txn_ind,sha_debit_amt,sha_reference_no,sha_item,sha_reference_ind,sha_apply_sts_ind,sha_approve_sts_cd,sha_approve_dt,sha_crt_id,sha_crt_dt,Acc_sts,sha_refund_ind) values ('" + lblID.Text + "','" + up_date + "','" + DropDownList1.SelectedItem.Text + "','B','" + deb_amt.Text + "','CR.SYER','DIPINDAHKAN SEBAGAI SYER ANGGOTA','C','A','SA','" + up_date + "','" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Y','N')";
    //                            Status = DBCon.Ora_Execute_CommamdText(Inssql);
    //                        }
    //                    }

    //                }

    //            }
    //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
    //            s_update.Checked = true;
    //            showgrid();
    //        }
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nama Kelompok.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
    //    }
    //}

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        showgrid();
    }

   
    protected void Button1_Click(object sender, EventArgs e)
    {

        using (SqlConnection con = new SqlConnection(cs))
        {
            string strDate =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                var checkbox = gvrow.FindControl("chkSelect") as CheckBox;
                if (checkbox.Checked == true)
                {


                    var lblID = gvrow.FindControl("Label2") as Label;
                    var Ran_ID = gvrow.FindControl("Label1") as Label;
                    var deb_amt = gvrow.FindControl("Label7") as Label;
                    TextBox box1 = (TextBox)gvrow.FindControl("Label8");
                    //TextBox box2 = (TextBox)gvrow.FindControl("Label9");
                   
                    string userid = Session["New"].ToString();
                    DropDownList ddl = (DropDownList)gvrow.FindControl("Bank_details");
                    string bdate = TextBox2.Text;
                    String up_date;
                    if (bdate != "")
                    {
                        DateTime bd = DateTime.ParseExact(bdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        up_date = bd.ToString("yyyy-mm-dd");
                    }
                    else
                    {
                        up_date = "";
                    }

                    SqlCommand cmd = new SqlCommand("Update mem_divident SET div_bank_acc_no = @div_bank_acc_no, div_bank_cd = @div_bank_cd,div_pay_id=@div_pay_id, div_pay_dt = @div_pay_dt, div_upd_id=@div_upd_id, div_upd_dt=@div_upd_dt WHERE div_new_icno='" + lblID.Text + "' and ID='" + Ran_ID.Text + "' and Acc_sts ='Y'", con);
                    cmd.Parameters.AddWithValue("div_bank_acc_no", box1.Text);
                    cmd.Parameters.AddWithValue("div_bank_cd", ddl.SelectedValue);
                    cmd.Parameters.AddWithValue("div_pay_id", userid);
                    cmd.Parameters.AddWithValue("div_pay_dt", up_date);
                    cmd.Parameters.AddWithValue("div_upd_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("div_upd_id", userid);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (up_date != "")
                    {
                        string Inssql = "insert into mem_share (sha_new_icno,sha_txn_dt,sha_batch_name,sha_txn_ind,sha_debit_amt,sha_reference_no,sha_item,sha_reference_ind,sha_apply_sts_ind,sha_approve_sts_cd,sha_approve_dt,sha_crt_id,sha_crt_dt,Acc_sts,sha_refund_ind) values ('" + lblID.Text + "','" + up_date + "','" + DropDownList1.SelectedItem.Text + "','B','" + deb_amt.Text + "','CR.SYER','DIPINDAHKAN SEBAGAI SYER ANGGOTA','C','A','SA','" + up_date + "','" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Y','N')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    }

                    //refreshdata();

                }

            }
            // Integration Part

            DataTable ddsha = new DataTable();
            ddsha = DBCon.Ora_Execute_table("select sha_batch_name,Format(sha_approve_dt,'yyyy-MM-dd') app_date,count(sha_new_icno) cnt,sum(cast(sha_debit_amt as money)) syer_amt from mem_share WHERE sha_batch_name='" + DropDownList1.SelectedValue + "' and Format(sha_approve_dt,'yyyy-MM-dd')='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and sha_approve_sts_cd='SA' and Acc_sts ='Y' group by sha_batch_name,Format(sha_approve_dt,'yyyy-MM-dd')");

            if (ddsha.Rows.Count != 0)
            {
                userid = Session["New"].ToString();
                GetUniqueInv();

                //MODAL SYER

                DataTable get_inter_info_sh = DBCon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='CARUMAN ANGGOTA' and jur_desc_cd='04'");

                string Inssql_sh = "Insert into KW_jurnal_inter (no_permohonan,no_Rujukan,tarikh_lulus,Terma,Jenis_permohonan,perkara,nama_pelanggan_code,Overall,Status,crt_id,cr_dt) "
                    + " Values ('" + unq_id2 + "','" + ddsha.Rows[0]["sha_batch_name"].ToString() + "','" + ddsha.Rows[0]["app_date"].ToString() + "','30','" + get_inter_info_sh.Rows[0]["jur_item"].ToString() + "', "
                    + " '" + get_inter_info_sh.Rows[0]["jur_desc_cd"].ToString() + "','" + get_inter_info_sh.Rows[0]["jur_module"].ToString() + "','" + ddsha.Rows[0]["syer_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_sh);

                string Inssql_sh_items = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt) "
                    + " Values ('" + unq_id2 + "','" + get_inter_info_sh.Rows[0]["jur_desc"].ToString() + "','" + ddsha.Rows[0]["cnt"].ToString() + "','" + ddsha.Rows[0]["syer_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_sh_items);


                DataTable dt_upd_format2 = DBCon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + unq_id2 + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='13' and Status = 'A'");

            }
            showgrid();
            service.audit_trail("P0151", TextBox2.Text, "Nama Kelompok", DropDownList1.SelectedItem.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

        }
    }

    private void GetUniqueInv()
    {


        // MODAL SYER

        DataTable dt1_ms = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='13' and Status='A'");
        if (dt1_ms.Rows.Count != 0)
        {
            if (dt1_ms.Rows[0]["cfmt"].ToString() == "")
            {
                unq_id2 = dt1_ms.Rows[0]["fmt"].ToString();
            }
            else
            {
                int seqno = Convert.ToInt32(dt1_ms.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId2 = newNumber.ToString(dt1_ms.Rows[0]["lfrmt1"].ToString() + "0000");
                unq_id2 = uniqueId2;
            }

        }
        else
        {
            DataTable get_inter_info_ms = DBCon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='CARUMAN ANGGOTA' and jur_desc_cd='04'");
            DataTable get_doc1_ms = new DataTable();
            get_doc1_ms = DBCon.Ora_Execute_table("select Ref_doc_descript as s1 from KW_Ref_Doc_kod where Ref_doc_code='13'");
            if (get_inter_info_ms.Rows.Count != 0)
            {
                DataTable dt_ms = DBCon.Ora_Execute_table("select ISNULL(max(SUBSTRING(no_permohonan,13,2000)),'0') from KW_jurnal_inter  where Jenis_permohonan='" + get_inter_info_ms.Rows[0]["jur_item"].ToString() + "' and perkara='" + get_inter_info_ms.Rows[0]["jur_desc_cd"].ToString() + "' and nama_pelanggan_code='" + get_inter_info_ms.Rows[0]["jur_module"].ToString() + "'");
                if (dt_ms.Rows.Count > 0)
                {
                    int seqno = Convert.ToInt32(dt_ms.Rows[0][0].ToString());
                    int newNumber = seqno + 1;
                    uniqueId2 = newNumber.ToString(get_doc1_ms.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                    unq_id2 = uniqueId2;

                }
                else
                {
                    int newNumber = Convert.ToInt32(uniqueId2) + 1;
                    uniqueId2 = newNumber.ToString(get_doc1_ms.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                    unq_id2 = uniqueId2;
                }
            }
        }


    }
    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

}