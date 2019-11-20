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
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Threading;

public partial class Kmd_Anggota : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DBConnection Dblog = new DBConnection();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string userid;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;

    SqlCommand strQuery;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();

        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                TextBox2.Attributes.Add("readonly", "readonly");
                kawasan();
                Batch();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('141','1052','907','70','66','22','13','73','71','143','14','15','135','61','883')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;


            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            s_update.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());


        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
        
    }
    void Batch()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select div_batch_name from mem_divident where  div_approve_ind='SA' and div_pay_dt ='1900-01-01 00:00:00.000' and Acc_sts='Y' group by div_batch_name order by div_batch_name";
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

    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0150' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

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

            if (Label1_txt.Text != "")
            {
                Label1_txt.Attributes.Add("Readonly", "Readonly");
                Label1_txt.Attributes.Add("Style", "Pointer-events:None;");
            }
            else
            {
                Label1_txt.Attributes.Remove("Readonly");
                Label1_txt.Attributes.Remove("Style");
            }
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
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,d.div_bank_acc_no,d.div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN '' ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y'  left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cd left join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
            else
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,d.div_bank_acc_no,d.div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN '' ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y'  left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cd left join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and div_eft_batch_name IS NOT null and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
        }
        else if (DropDownList1.SelectedItem.Text != "" && TextBox18.SelectedItem.Text != "--- PILIH ---" && s_icno.Text == "")
        {
            if (s_update.Checked == true)
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,d.div_bank_acc_no,d.div_bank_cd, ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN '' ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y'  left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cd left join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and m.mem_area_cd='" + Check_kaw.Rows[0]["kawasan_code"].ToString() + "' and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
            else
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,d.div_bank_acc_no,d.div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN '' ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y' left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd  left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cdleft join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and m.mem_area_cd='" + Check_kaw.Rows[0]["kawasan_code"].ToString() + "' and div_eft_batch_name IS NOT null and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
        }
        else if (DropDownList1.SelectedItem.Text != "" && TextBox18.SelectedItem.Text == "--- PILIH ---" && s_icno.Text != "")
        {
            if (s_update.Checked == true)
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,d.div_bank_acc_no,d.div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN '' ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y' left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cd left join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and d.div_new_icno='" + s_icno.Text + "' and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
            else
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,d.div_bank_acc_no,d.div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN '' ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y' left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cd left join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and d.div_new_icno='" + s_icno.Text + "' and div_eft_batch_name IS NOT null and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
        }
        else if (DropDownList1.SelectedItem.Text != "" && TextBox18.SelectedItem.Text != "--- PILIH ---" && s_icno.Text != "")
        {
            if (s_update.Checked == true)
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,d.div_bank_acc_no,d.div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN '' ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y'  left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cd left join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and mem_area_cd='" + Check_kaw.Rows[0]["kawasan_code"].ToString() + "' and d.div_new_icno='" + s_icno.Text + "' and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
            }
            else
            {
                strQuery = new SqlCommand("select d.ID,d.div_new_icno,m.mem_name,m.mem_member_no,rb.branch_desc as mem_branch_cd,m.mem_centre,d.div_debit_amt,d.div_bank_acc_no,d.div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, d.div_pay_dt) = '1900-01-01' THEN '' ELSE Convert(varchar(10),CONVERT(date,d.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_member m  left join mem_divident d on m.mem_new_icno=d.div_new_icno and d.Acc_sts ='Y'  left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=d.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=m.mem_region_cd left join ref_branch rb on rb.branch_cd=m.mem_branch_cd where m.Acc_sts ='Y' and d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and mem_area_cd='" + Check_kaw.Rows[0]["kawasan_code"].ToString() + "' and d.div_new_icno='" + s_icno.Text + "' and div_eft_batch_name IS NOT null and d.div_approve_ind='SA' ORDER by rw.Wilayah_Name,rb.branch_desc,m.mem_centre,m.mem_name", con);
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
            shw_cnt1.Visible = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            
        }
        con.Close();

        
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

        if (DropDownList1.SelectedValue != "" && FileUpload1.FileName == "")
        {
            TextBox2.Text = DateTime.Now.ToString("dd/MM/yyy");
            showgrid();
        }
        else if (DropDownList1.SelectedValue == "" && FileUpload1.FileName != "")
        {
            TextBox2.Text = DateTime.Now.ToString("dd/MM/yyy");
            btnok_Click();
        }
        else
        { 
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nama Kelompok Atau Fail.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }


    }


    void btnok_Click()
    {
        try
        {
            if (FileUpload1.FileName != "")
            {
                if (FileUpload1.HasFile)
                {
                    string get_no_item = string.Empty, no_item_type = string.Empty;
                    FileUpload1.SaveAs(Server.MapPath("~/FILES/Div_text/" + FileUpload1.FileName));
                    string path = Server.MapPath("~/FILES/Div_text/" + FileUpload1.FileName).ToString();
                    DataTable table = new DataTable();
                    table.Columns.Add("div_new_icno");
                    table.Columns.Add("mem_name");
                    table.Columns.Add("mem_member_no");
                    table.Columns.Add("ID");
                    table.Columns.Add("mem_branch_cd");
                    table.Columns.Add("mem_centre");
                    table.Columns.Add("div_debit_amt");
                    table.Columns.Add("div_bank_acc_no");
                    table.Columns.Add("div_bank_cd");
                    table.Columns.Add("div_pay_dt");

                    // CONVERT(datetime,LEFT(DTTRX,8) + ' ' + SUBSTRING(DTTRX,9,2) +':'+SUBSTRING(DTTRX,11,2) +':' + RIGHT(DTTRX,2)) as DTTRX
                    using (StreamReader sr = new StreamReader(path))
                    {

                        while (!sr.EndOfStream)
                        {
                            string accno = string.Empty, acccd = string.Empty;
                            string[] parts = sr.ReadLine().Split('|');
                            //String dateString = parts[1].Trim();
                            String paydate = parts[4].Trim();
                            String format = "ddMMyyyy";
                            //DateTime result = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
                            string d1 = parts[1].Trim();
                            accno = parts[2].Trim();
                            acccd = parts[3].Trim();
                            DateTime result1 = DateTime.ParseExact(paydate, format, CultureInfo.InvariantCulture);
                            string d2 = result1.ToString("dd/MM/yyyy");
                            DataTable ddicno = new DataTable();
                            ddicno = Dblog.Ora_Execute_table("select JA.ID,JA.div_new_icno,mm.mem_name,mm.mem_member_no,rb.branch_desc as mem_branch_cd,mm.mem_centre,JA.div_debit_amt,JA.div_bank_acc_no,JA.div_bank_cd,ISNULL(CASE WHEN CONVERT(DATE, JA.div_pay_dt) = '1900-01-01' THEN '' ELSE Convert(varchar(10),CONVERT(date,JA.div_pay_dt,106),103) END, '') AS div_pay_dt,rnb.Bank_Code from mem_divident JA left join mem_member mm on mm.mem_new_icno=JA.div_new_icno and mm.Acc_sts='Y' left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=JA.div_bank_cd left join Ref_Wilayah as rw on rw.Wilayah_Code=mm.mem_region_cd left join ref_branch rb on rb.branch_cd=mm.mem_branch_cd where JA.div_new_icno = '" + parts[0].Trim() + "' and div_batch_name='" + d1 + "' and JA.Acc_sts='Y' and ISNULL(JA.div_pay_dt,'') ='1900-01-01 00:00:00.000'");
                            if (ddicno.Rows.Count != 0)
                            {
                                table.Rows.Add(ddicno.Rows[0]["div_new_icno"].ToString(), ddicno.Rows[0]["mem_name"].ToString(), ddicno.Rows[0]["mem_member_no"].ToString(), ddicno.Rows[0]["ID"].ToString(), ddicno.Rows[0]["mem_branch_cd"].ToString(), ddicno.Rows[0]["mem_centre"].ToString(), double.Parse(ddicno.Rows[0]["div_debit_amt"].ToString()).ToString("C").Replace("$", ""), accno, acccd, d2);
                            }
                            else
                            {
                                no_item_type = "1";
                                get_no_item += parts[0].Trim() + "<br>";
                            }
                        }
                    }
                    GridView1.DataSource = table;
                    GridView1.DataBind();
                    if (table.Rows.Count != 0)
                    {
                        shw_cnt1.Visible = true;                      
                    }
                    else
                    {
                        shw_cnt1.Visible = false;                        
                    }
                    if(no_item_type != "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Miss Matching IC No(S) : <br>"+ get_no_item + "',{'type': 'warning','title': 'Warning'});", true);
                    }

                }
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Fail.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }


        }
        catch (Exception ex)
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + ex.Message + ".',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        showgrid();
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
            string filePath = Server.MapPath("~/FILES/Div_text/sample_div.txt");
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(filePath) + "\"");
            Response.WriteFile(filePath);
            Response.End();
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
                    TextBox box1 = (TextBox)gvrow.FindControl("Label8");
                    //TextBox box2 = (TextBox)gvrow.FindControl("Label9");
                    TextBox box3 = (TextBox)gvrow.FindControl("Label10");
                    string userid = Session["New"].ToString();
                    DropDownList ddl = (DropDownList)gvrow.FindControl("Bank_details");
                    string bdate = box3.Text;
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
                    //refreshdata();

                }

            }
            service.audit_trail("P0150", TextBox2.Text, "Nama Kelompok", DropDownList1.SelectedItem.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);            
            showgrid();
        }
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

}