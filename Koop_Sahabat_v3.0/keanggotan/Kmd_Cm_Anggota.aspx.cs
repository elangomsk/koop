using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Collections;

public partial class Kmd_Cm_Anggota : System.Web.UI.Page
{

    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    //string cmd = string.Empty;
    //string str;
    //SqlCommand com;
    SqlCommand cmd;
    DBConnection Con = new DBConnection();
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string uniqueId, uniqueId2, uniqueId3, unq_id1, unq_id2, unq_id3;
    string jurnal_qry = string.Empty;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    string userid, Status;
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
                //BindData();
                TextBox2.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

    void Batch()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select div_batch_name from mem_divident where ISNULL(div_approve_id,'') ='' and Acc_sts='Y' group by div_batch_name order by div_batch_name";
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('134','1052','1081','70','66','22','71','13','14','1042','135','61')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;


            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0139' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();


            }
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
            TextBox18.DataValueField = "kawasan_code";

            TextBox18.DataBind();
            TextBox18.Items.Insert(0, new ListItem("EMPTY", "EMPTY"));
            TextBox18.Items.Insert(0, new ListItem("--- PILIH ---", ""));


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindGridview(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text != "--- PILIH ---")
        {
            Session["chkditems"] = null;
            grid();

        }
        else
        {
            Session["chkditems"] = null;
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Nama Kelompok.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    void grid()
    {

        //DataTable Check_kaw = new DataTable();
        //Check_kaw = Con.Ora_Execute_table("select DISTINCT kavasan_name,kawasan_code from Ref_Cawangan where kavasan_name='" + TextBox18.SelectedItem.Text + "'");
        string kw_cd = string.Empty;
        if (TextBox18.SelectedValue != "")
        {
            kw_cd = TextBox18.SelectedItem.Text;
        }
        DataTable dt = new DataTable();
        if (DropDownList1.SelectedItem.Text != "--- PILIH ---" && TextBox18.SelectedItem.Text == "--- PILIH ---" && s_icno.Text == "")
        {
            dt = Con.Ora_Execute_table("select mem_new_icno,mem_name,ISNULL(rc.kavasan_name,'EMPTY') kavasan_name,cawangan_name,mem_centre,sum(div_debit_amt) as div_debit_amt,div_bank_acc_no,Bank_Name,sha_amt from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where  d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(d.div_approve_ind,'') ='' group by mem_new_icno,mem_name,kavasan_name,cawangan_name,mem_centre,div_bank_acc_no,Bank_Name,sha_amt order by kavasan_name,cawangan_name,mem_new_icno");
            DataTable dt1 = new DataTable();
            dt1 = Con.Ora_Execute_table("select count(div_bank_acc_no) div_bank_acc_no from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(d.div_approve_ind,'') ='' and div_bank_acc_no='' ");
            if (dt1.Rows.Count > 0)
            {
                Label11.Text = dt1.Rows[0][0].ToString();
            }
            DataTable dt2 = new DataTable();
            dt2 = Con.Ora_Execute_table("select count(div_bank_acc_no) div_bank_acc_no  from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "'  and ISNULL(d.div_approve_ind,'') ='' and div_bank_acc_no!=''");
            if (dt2.Rows.Count > 0)
            {
                Label6.Text = dt2.Rows[0][0].ToString();
            }
            DataTable dt3 = new DataTable();
            dt3 = Con.Ora_Execute_table("select ISNULL(sum(ISNULL(div_debit_amt,'0.00')),'0.00') as div_debit_amt  from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "'  and ISNULL(d.div_approve_ind,'') =''  ");
            if (dt3.Rows.Count > 0)
            {
                decimal s1 = Convert.ToDecimal(dt3.Rows[0][0].ToString());
                Label12.Text = String.Format("{0:N2}", s1);

            }
            DataTable dt4 = new DataTable();
            dt4 = Con.Ora_Execute_table("select count(mem_new_icno) mem_new_icno  from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "'  and ISNULL(d.div_approve_ind,'') =''  ");
            if (dt4.Rows.Count > 0)
            {
                Label1.Text = dt4.Rows[0][0].ToString();
            }
            DataTable dt5 = new DataTable();
            dt5 = Con.Ora_Execute_table("select ISNULL(sum(ISNULL(sha_amt,'0.00')),'0.00') sha_amt  from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "'  and ISNULL(d.div_approve_ind,'') =''  ");
            if (dt5.Rows.Count > 0)
            {
                decimal s1 = Convert.ToDecimal(dt5.Rows[0][0].ToString());
                Label10.Text = String.Format("{0:N2}", s1);

            }
        }
        else if (DropDownList1.SelectedItem.Text != "--- PILIH ---" && TextBox18.SelectedItem.Text != "--- PILIH ---" && s_icno.Text == "")
        {
            dt = Con.Ora_Execute_table("select mem_new_icno,mem_name,ISNULL(rc.kavasan_name,'EMPTY') kavasan_name,cawangan_name,mem_centre,sum(div_debit_amt) as div_debit_amt,div_bank_acc_no,Bank_Name,sha_amt from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where  d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(rc.kavasan_name,'EMPTY') ='" + kw_cd + "' and ISNULL(d.div_approve_ind,'') ='' group by mem_new_icno,mem_name,kavasan_name,cawangan_name,mem_centre,div_bank_acc_no,Bank_Name,sha_amt order by kavasan_name,cawangan_name,mem_new_icno");
            DataTable dt1 = new DataTable();
            dt1 = Con.Ora_Execute_table("select count(div_bank_acc_no) div_bank_acc_no from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(rc.kavasan_name,'EMPTY') ='" + kw_cd + "' and ISNULL(d.div_approve_ind,'') ='' and div_bank_acc_no='' ");
            if (dt1.Rows.Count > 0)
            {
                Label11.Text = dt1.Rows[0][0].ToString();
            }
            DataTable dt2 = new DataTable();
            dt2 = Con.Ora_Execute_table("select count(div_bank_acc_no) div_bank_acc_no from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(rc.kavasan_name,'EMPTY') ='" + kw_cd + "' and ISNULL(d.div_approve_ind,'') ='' and div_bank_acc_no!=''");
            if (dt2.Rows.Count > 0)
            {
                Label6.Text = dt2.Rows[0][0].ToString();
            }
            DataTable dt3 = new DataTable();
            dt3 = Con.Ora_Execute_table("select ISNULL(sum(div_debit_amt),'0.00') as div_debit_amt  from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(rc.kavasan_name,'EMPTY') ='" + kw_cd + "' and ISNULL(d.div_approve_ind,'') =''  ");
            if (dt3.Rows.Count > 0)
            {
                decimal s1 = Convert.ToDecimal(dt3.Rows[0][0].ToString());
                Label12.Text = String.Format("{0:N2}", s1);
            }
            DataTable dt4 = new DataTable();
            dt4 = Con.Ora_Execute_table("select count(mem_new_icno) mem_new_icno  from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(rc.kavasan_name,'EMPTY') ='" + kw_cd + "' and ISNULL(d.div_approve_ind,'') =''  ");
            if (dt4.Rows.Count > 0)
            {
                Label1.Text = dt4.Rows[0][0].ToString();
            }
            DataTable dt5 = new DataTable();
            dt5 = Con.Ora_Execute_table("select ISNULL(sum(sha_amt),'0.00') sha_amt  from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(rc.kavasan_name,'EMPTY') ='" + kw_cd + "' and ISNULL(d.div_approve_ind,'') =''  ");
            if (dt5.Rows.Count > 0)
            {
                decimal s1 = Convert.ToDecimal(dt5.Rows[0][0].ToString());
                Label10.Text = String.Format("{0:N2}", s1);
            }
        }
        else if (DropDownList1.SelectedItem.Text != "--- PILIH ---" && TextBox18.SelectedItem.Text == "--- PILIH ---" && s_icno.Text != "")
        {
            dt = Con.Ora_Execute_table("select mem_new_icno,mem_name,ISNULL(rc.kavasan_name,'EMPTY') kavasan_name,cawangan_name,mem_centre,sum(div_debit_amt) as div_debit_amt,div_bank_acc_no,Bank_Name,sha_amt from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where  d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and d.div_new_icno='" + s_icno.Text + "' and ISNULL(d.div_approve_ind,'') ='' group by mem_new_icno,mem_name,kavasan_name,cawangan_name,mem_centre,div_bank_acc_no,Bank_Name,sha_amt order by kavasan_name,cawangan_name,mem_new_icno");
            DataTable dt1 = new DataTable();
            dt1 = Con.Ora_Execute_table("select count(div_bank_acc_no) div_bank_acc_no from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and d.div_new_icno='" + s_icno.Text + "' and ISNULL(d.div_approve_ind,'') ='' and div_bank_acc_no='' ");
            if (dt1.Rows.Count > 0)
            {
                Label11.Text = dt1.Rows[0][0].ToString();
            }
            DataTable dt2 = new DataTable();
            dt2 = Con.Ora_Execute_table("select count(div_bank_acc_no) div_bank_acc_no from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and d.div_new_icno='" + s_icno.Text + "' and ISNULL(d.div_approve_ind,'') ='' and div_bank_acc_no!=''");
            if (dt2.Rows.Count > 0)
            {
                Label6.Text = dt2.Rows[0][0].ToString();
            }
            DataTable dt3 = new DataTable();
            dt3 = Con.Ora_Execute_table("select ISNULL(sum(div_debit_amt),'0.00') as div_debit_amt  from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and d.div_new_icno='" + s_icno.Text + "' and ISNULL(d.div_approve_ind,'') =''  ");
            if (dt3.Rows.Count > 0)
            {
                decimal s1 = Convert.ToDecimal(dt3.Rows[0][0].ToString());
                Label12.Text = String.Format("{0:N2}", s1);
            }
            DataTable dt4 = new DataTable();
            dt4 = Con.Ora_Execute_table("select count(mem_new_icno) mem_new_icno  from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and d.div_new_icno='" + s_icno.Text + "' and ISNULL(d.div_approve_ind,'') =''  ");
            if (dt4.Rows.Count > 0)
            {
                Label1.Text = dt4.Rows[0][0].ToString();
            }
            DataTable dt5 = new DataTable();
            dt5 = Con.Ora_Execute_table("select ISNULL(sum(sha_amt),'0.00') sha_amt  from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and d.div_new_icno='" + s_icno.Text + "' and ISNULL(d.div_approve_ind,'') =''  ");
            if (dt5.Rows.Count > 0)
            {
                decimal s1 = Convert.ToDecimal(dt5.Rows[0][0].ToString());
                Label10.Text = String.Format("{0:N2}", s1);
            }
        }
        else if (DropDownList1.SelectedItem.Text != "--- PILIH ---" && TextBox18.SelectedItem.Text != "--- PILIH ---" && s_icno.Text != "")
        {
            dt = Con.Ora_Execute_table("select mem_new_icno,mem_name,ISNULL(rc.kavasan_name,'EMPTY') kavasan_name,cawangan_name,mem_centre,sum(div_debit_amt) as div_debit_amt,div_bank_acc_no,Bank_Name,sha_amt from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where  d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(rc.kavasan_name,'EMPTY') ='" + kw_cd + "' and d.div_new_icno='" + s_icno.Text + "' and ISNULL(d.div_approve_ind,'') ='' group by mem_new_icno,mem_name,kavasan_name,cawangan_name,mem_centre,div_bank_acc_no,Bank_Name,sha_amt order by kavasan_name,cawangan_name,mem_new_icno");
            DataTable dt1 = new DataTable();
            dt1 = Con.Ora_Execute_table("select count(div_bank_acc_no) div_bank_acc_no from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(rc.kavasan_name,'EMPTY') ='" + kw_cd + "' and d.div_new_icno='" + s_icno.Text + "' and ISNULL(d.div_approve_ind,'') ='' and div_bank_acc_no='' ");
            if (dt1.Rows.Count > 0)
            {
                Label11.Text = dt1.Rows[0][0].ToString();
            }
            DataTable dt2 = new DataTable();
            dt2 = Con.Ora_Execute_table("select count(div_bank_acc_no) div_bank_acc_no from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(rc.kavasan_name,'EMPTY') ='" + kw_cd + "' and d.div_new_icno='" + s_icno.Text + "' and ISNULL(d.div_approve_ind,'') ='' and div_bank_acc_no!=''");
            if (dt2.Rows.Count > 0)
            {
                Label6.Text = dt2.Rows[0][0].ToString();
            }
            DataTable dt3 = new DataTable();
            dt3 = Con.Ora_Execute_table("select ISNULL(sum(div_debit_amt),'0.00') as div_debit_amt  from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(rc.kavasan_name,'EMPTY') ='" + kw_cd + "' and d.div_new_icno='" + s_icno.Text + "' and ISNULL(d.div_approve_ind,'') =''  ");
            if (dt3.Rows.Count > 0)
            {
                decimal s1 = Convert.ToDecimal(dt3.Rows[0][0].ToString());
                Label12.Text = String.Format("{0:N2}", s1);
            }
            DataTable dt4 = new DataTable();
            dt4 = Con.Ora_Execute_table("select count(mem_new_icno) mem_new_icno  from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(rc.kavasan_name,'EMPTY') ='" + kw_cd + "' and d.div_new_icno='" + s_icno.Text + "' and ISNULL(d.div_approve_ind,'') =''  ");
            if (dt4.Rows.Count > 0)
            {
                Label1.Text = dt4.Rows[0][0].ToString();
            }
            DataTable dt5 = new DataTable();
            dt5 = Con.Ora_Execute_table("select ISNULL(sum(sha_amt),'0.00') sha_amt   from mem_divident as d outer apply(select top(1) * from  mem_member as m where d.div_new_icno=m.mem_new_icno order by mem_crt_dt desc) as m Left Join Ref_Nama_Bank AS rnb ON rnb.Bank_Code=m.mem_bank_cd  left join Ref_Cawangan AS rc ON rc.cawangan_code !='' and rc.cawangan_code=m.mem_branch_cd where d.div_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(rc.kavasan_name,'EMPTY') ='" + kw_cd + "' and d.div_new_icno='" + s_icno.Text + "' and ISNULL(d.div_approve_ind,'') =''  ");
            if (dt5.Rows.Count > 0)
            {
                decimal s1 = Convert.ToDecimal(dt5.Rows[0][0].ToString());
                Label10.Text = String.Format("{0:N2}", s1);
            }
        }

        if (dt.Rows.Count == 0)
        {

            dt.Rows.Add(dt.NewRow());
            gvSelected.DataSource = dt;
            gvSelected.DataBind();
            int columncount = gvSelected.Rows[0].Cells.Count;
            gvSelected.Rows[0].Cells.Clear();
            gvSelected.Rows[0].Cells.Add(new TableCell());
            gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
            gvSelected.Rows[0].Cells[0].Text = "<center>Rekod Tidak Dijumpai.</center>";
            d1.Visible = false;
            d2.Visible = false;
            d3.Visible = false;
            Batch();
            Session["chkditems"] = null;
        }
        else
        {
            gvSelected.DataSource = dt;
            gvSelected.DataBind();
            if (role_add == "1")
            {
                Button1.Visible = true;
            }
            else
            {
                Button1.Visible = false;
            }
            d1.Visible = true;
            d2.Visible = true;
            d3.Visible = true;
            Button1.Visible = true;
            Button3.Visible = true;
        }
    }






    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        savechkdvls();
        gvSelected.PageIndex = e.NewPageIndex;
        this.grid();
        gvSelected.DataBind();
        chkdvaluesp();
    }

    private void chkdvaluesp()
    {

        ArrayList usercontent = (ArrayList)Session["chkditems"];

        if (usercontent != null && usercontent.Count > 0)
        {

            foreach (GridViewRow gvrow in gvSelected.Rows)
            {

                Int64 index = Convert.ToInt64(gvSelected.DataKeys[gvrow.RowIndex].Value);

                if (usercontent.Contains(index))
                {

                    System.Web.UI.WebControls.RadioButton myCheckBox1 = (System.Web.UI.WebControls.RadioButton)gvrow.FindControl("chkSelect_1");

                    System.Web.UI.WebControls.RadioButton myCheckBox2 = (System.Web.UI.WebControls.RadioButton)gvrow.FindControl("chkSelect_2");

                    myCheckBox1.Checked = false;

                    myCheckBox2.Checked = true;

                }

            }

        }

    }

    private void savechkdvls()
    {

        ArrayList usercontent = new ArrayList();

        Int64 index = -1;


        foreach (GridViewRow gvrow in gvSelected.Rows)
        {

            index = Convert.ToInt64(gvSelected.DataKeys[gvrow.RowIndex].Value);

            bool result = ((System.Web.UI.WebControls.RadioButton)gvrow.FindControl("chkSelect_1")).Checked;

            bool result1 = ((System.Web.UI.WebControls.RadioButton)gvrow.FindControl("chkSelect_2")).Checked;



            // Check in the Session

            if (Session["chkditems"] != null)

                usercontent = (ArrayList)Session["chkditems"];

            if (result)
            {

                if (!usercontent.Contains(index))

                    usercontent.Add(index);

            }

            else
            {
                usercontent.Remove(index);
            }

            if (result1)
            {

                if (!usercontent.Contains(index))

                    usercontent.Add(index);

            }

            else
            {
                usercontent.Remove(index);
            }
        }

        if (usercontent != null && usercontent.Count > 0)

            Session["chkditems"] = usercontent;

    }
    protected void submit_button(object sender, EventArgs e)
    {

        using (SqlConnection con = new SqlConnection(cs))
        {

            //string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            string datedari = TextBox2.Text;

            DateTime dt = DateTime.ParseExact(datedari, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datetime = dt.ToString("yyyy-mm-dd");
            if (s_update.Checked == true)
            {
                gvSelected.AllowPaging = false;
                savechkdvls();
                this.grid();
                gvSelected.DataBind();
                chkdvaluesp();
                int[] no = new int[gvSelected.Rows.Count];
                int i = 0;
                foreach (GridViewRow gvrow in gvSelected.Rows)
                {
                    var checkbox = gvrow.FindControl("chkSelect") as CheckBox;
                    if (checkbox.Checked == true)
                    {

                        var no_kp = gvrow.FindControl("Label2") as Label;
                        RadioButton chkbox = (RadioButton)gvrow.FindControl("chkSelect_1");
                        RadioButton chkbox1 = (RadioButton)gvrow.FindControl("chkSelect_2");
                        Label btch_name = (Label)gvrow.FindControl("Label1_btch");
                        //if (chkbox.Checked == true || chkbox1.Checked == true)
                        if (chkbox.Checked == true)
                        {

                            //Update Fee Status

                            //var lblstatus = gvrow.FindControl("lblstatus") as Label;

                            //String text1 = "A";
                            String text2 = Session["New"].ToString();
                            SqlCommand up_status = new SqlCommand("UPDATE mem_divident SET [div_approve_ind] = @div_approve_ind, [div_approve_id] = @div_approve_id, [div_approve_dt] = @div_approve_dt WHERE div_new_icno='" + no_kp.Text + "'  and Acc_sts ='Y'", con);

                            if (chkbox.Checked == true)
                            {
                                String sta = "SA";
                                up_status.Parameters.AddWithValue("div_approve_ind", sta);
                            }
                            else
                            {
                                String sta = "TS";
                                up_status.Parameters.AddWithValue("div_approve_ind", sta);
                            }
                            up_status.Parameters.AddWithValue("div_approve_id", text2);
                            up_status.Parameters.AddWithValue("div_approve_dt", datetime);
                            //up_status.Parameters.AddWithValue("sha_upd_id", text2);
                            //up_status.Parameters.AddWithValue("sha_upd_dt", DateTime.Now);
                            con.Open();
                            int j = up_status.ExecuteNonQuery();
                            con.Close();
                        }


                    }
                }
                gvSelected.AllowPaging = true;
            }
            else
            {
                foreach (GridViewRow gvrow in gvSelected.Rows)
                {
                    var checkbox = gvrow.FindControl("chkSelect") as CheckBox;
                    if (checkbox.Checked == true)
                    {

                        var no_kp = gvrow.FindControl("Label2") as Label;
                        RadioButton chkbox = (RadioButton)gvrow.FindControl("chkSelect_1");
                        RadioButton chkbox1 = (RadioButton)gvrow.FindControl("chkSelect_2");
                        Label btch_name = (Label)gvrow.FindControl("Label1_btch");
                        //if (chkbox.Checked == true || chkbox1.Checked == true)
                        if (chkbox.Checked == true)
                        {

                            //Update Fee Status

                            //var lblstatus = gvrow.FindControl("lblstatus") as Label;

                            //String text1 = "A";
                            String text2 = Session["New"].ToString();
                            SqlCommand up_status = new SqlCommand("UPDATE mem_divident SET [div_approve_ind] = @div_approve_ind, [div_approve_id] = @div_approve_id, [div_approve_dt] = @div_approve_dt WHERE div_new_icno='" + no_kp.Text + "'  and Acc_sts ='Y'", con);

                            if (chkbox.Checked == true)
                            {
                                String sta = "SA";
                                up_status.Parameters.AddWithValue("div_approve_ind", sta);
                            }
                            else
                            {
                                String sta = "TS";
                                up_status.Parameters.AddWithValue("div_approve_ind", sta);
                            }
                            up_status.Parameters.AddWithValue("div_approve_id", text2);
                            up_status.Parameters.AddWithValue("div_approve_dt", datetime);
                            //up_status.Parameters.AddWithValue("sha_upd_id", text2);
                            //up_status.Parameters.AddWithValue("sha_upd_dt", DateTime.Now);
                            con.Open();
                            int j = up_status.ExecuteNonQuery();
                            con.Close();
                        }


                    }
                }
            }

            // Integration Part

            DataTable ddsha = new DataTable();
            ddsha = DBCon.Ora_Execute_table("select div_batch_name,Format(div_approve_dt,'yyyy-MM-dd') app_date,count(div_new_icno) cnt,sum(cast(div_debit_amt as money)) syer_amt from mem_divident WHERE div_batch_name='" + DropDownList1.SelectedValue + "' and Format(div_approve_dt,'yyyy-MM-dd')='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and div_approve_ind='SA' and Acc_sts ='Y' group by div_batch_name,Format(div_approve_dt,'yyyy-MM-dd')");

            if (ddsha.Rows.Count != 0)
            {

                userid = Session["New"].ToString();
                GetUniqueInv();

                //MODAL SYER

                DataTable get_inter_info_sh = DBCon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='DIVIDEN' and jur_desc_cd='08'");

                string Inssql_sh = "Insert into KW_jurnal_inter (no_permohonan,no_Rujukan,tarikh_lulus,Terma,Jenis_permohonan,perkara,nama_pelanggan_code,Overall,Status,crt_id,cr_dt) "
                    + " Values ('" + unq_id2 + "','" + ddsha.Rows[0]["div_batch_name"].ToString() + "','" + ddsha.Rows[0]["app_date"].ToString() + "','30','15', "
                    + " '" + get_inter_info_sh.Rows[0]["jur_desc"].ToString() + "','" + get_inter_info_sh.Rows[0]["jur_module"].ToString() + "','" + ddsha.Rows[0]["syer_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_sh);

                string Inssql_sh_items = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt) "
                    + " Values ('" + unq_id2 + "','" + get_inter_info_sh.Rows[0]["jur_desc"].ToString() + "','" + ddsha.Rows[0]["cnt"].ToString() + "','" + ddsha.Rows[0]["syer_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_sh_items);


                DataTable dt_upd_format2 = DBCon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + unq_id2 + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='15' and Status = 'A'");

            }


            savechkdvls();
            grid();
            chkdvaluesp();
            Session["chkditems"] = null;
            service.audit_trail("P0139", TextBox2.Text, "Nama Kelompok", DropDownList1.SelectedItem.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);


        }
    }


    private void GetUniqueInv()
    {


        // DIVIDEN ANGGOTA

        DataTable dt1_ms = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='15' and Status='A'");
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


            DataTable get_inter_info_ms = DBCon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='DIVIDEN' and jur_desc_cd='08'");
            DataTable get_doc1_ms = new DataTable();
            get_doc1_ms = DBCon.Ora_Execute_table("select Ref_doc_descript as s1 from KW_Ref_Doc_kod where Ref_doc_code='15'");
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
    protected void OnCheckedChanged(object sender, EventArgs e)
    {
        bool isUpdateVisible = false;
        CheckBox chk = (sender as CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in gvSelected.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        CheckBox chkAll = (gvSelected.HeaderRow.FindControl("chkAll") as CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in gvSelected.Rows)
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




    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}