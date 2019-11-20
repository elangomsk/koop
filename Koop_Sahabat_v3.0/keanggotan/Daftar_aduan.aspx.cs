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

public partial class Daftar_aduan : System.Web.UI.Page
{

    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable wilayah = new DataTable();
    DataTable caw = new DataTable();
    DataTable pusat = new DataTable();
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    DataTable ddcom = new DataTable();
    string Status = string.Empty;
    string status;
    DateTime dmula;
    string stscd;
    string scmd;
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
                Txtnokp.Text = Session["New"].ToString();
              
                bind();
                BindData();
                TxtTaradu.Text = DateTime.Now.ToString("dd/MM/yyyy");
                wilahBind();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    //    protected void ImageButtonCalendar_Click(object sender, ImageClickEventArgs e)
    //{
    //    Calendar.Visible = true;


    //}


    //protected void Calendar_SelectionChanged(object sender, EventArgs e)
    //{
    //    DateTime dt = Calendar.SelectedDate;
    //    TxtTaradu.Text = dt.ToString("dd/MM/yyyy");
    //    Calendar.Visible = false;
    //}

    void BindData()
    {

        SqlCommand query1 = new SqlCommand("Select distinct com_new_icno,case when ISNULL(MM.mem_name,'') = '' then ISNULL(cc1.stf_name,'') else ISNULL(MM.mem_name,'') end mem_name,comp_id,com_dt,com_new_icno,com_type_ind,cast(com_remark as char(25)) com_remark ,com_sts_cd,rc.cawangan_name,rsa.KETERANGAN from mem_complaint as mc Left join ref_status_aduan AS rsa ON rsa.KETERANGAN_Code=mc.com_sts_cd Left join mem_member AS mm on mm.mem_new_icno=mc.com_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan AS rc ON rc.Cawangan_code=mm.mem_branch_cd left join hr_staff_profile cc1 on cc1.stf_icno=mc.com_new_icno where mc.Acc_sts ='Y' and com_new_icno='" + Txtnokp.Text + "' and com_sts='Y' order by comp_id ", con);
        SqlDataAdapter da = new SqlDataAdapter(query1);
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
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();           
        }
        con.Close();
    }


    protected void hapus_click2(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            
                var a_no = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label21");
                var a_id = (System.Web.UI.WebControls.Label)gvRow.FindControl("Label1");
              
                string Inssql = "Update mem_complaint set com_sts='N' where com_new_icno='" + a_no.Text + "' and comp_id='" + a_id.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                service.audit_trail("P0132", "Hapus", "No Kp Baru", Txtnokp.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Aduan Dihapuskan Berjaya',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
        BindData();
    }

    protected void grdView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.BindData();
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
    void app_language()
    {
        if (Session["New"] != null)
        {
            assgn_roles();
            DataTable ste_set = new DataTable();
        ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

        DataTable gt_lng = new DataTable();
        gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('106','1052','1070','107','13','16','108','1012','109','110','111','112','113','114','115','61','15','77')");

        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;


        ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
        ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
        ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
        ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
        ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
        ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
        ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
        ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
        ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
        ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
        ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
        RadioButton1.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
        RadioButton2.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
        RadioButton3.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
        ps_lbl15.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
        btnsave.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
        btn_reset.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());

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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0132' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();
                if (role_add == "1")
                {
                    btnsave.Visible = true;
                }
                else
                {
                    btnsave.Visible = false;
                }


            }
        }
    }


    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton1.Checked == true)
        {
            RadioButton2.Checked = false;
            RadioButton3.Checked = false;

        }
        BindData();
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton2.Checked == true)
        {
            RadioButton1.Checked = false;
            RadioButton3.Checked = false;

        }
        BindData();
    }
    protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton3.Checked == true)
        {
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;

        }
        BindData();
    }

    void bind()
    {
        DataTable Dt = new DataTable();

        try
        {


            ddcom = DBCon.Ora_Execute_table("select mem_new_icno,mem_name,case when ISNULL(mem_phone_m,'') = 'NULL' then '' else mem_phone_m end as mem_phone_m,mem_branch_cd from mem_member where mem_new_icno='" + Txtnokp.Text + "' and Acc_sts ='Y'");
            Txtnokp.Text = ddcom.Rows[0][0].ToString();
            Txtnama.Text = ddcom.Rows[0][1].ToString();
            Txtnotel.Text = ddcom.Rows[0][2].ToString();
            wilahBind();

        }
        catch (Exception ex)
        {
            //MessageBox.Show("Record Not Found !!");
        }
    }

    void wilahBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select cawangan_name from Ref_Cawangan where cawangan_code='" + ddcom.Rows[0][3].ToString() + "'");
            if(ste_set.Rows.Count != 0)
            {
                TextBox1.Text = ste_set.Rows[0]["cawangan_name"].ToString();
            }

            //Ds = GetData("select cawangan_name from Ref_Cawangan where cawangan_code='" + ddcom.Rows[0][3].ToString() + "'");
            //ddcaw.DataSource = Ds.Tables[0];
            //ddcaw.DataValueField = "Cawangan_Name";
            //ddcaw.DataTextField = "Cawangan_Name";
            //ddcaw.DataBind();


        }
        catch (Exception ex)
        {
            //MessageBox.Show("Record Not Found !!");
        }
    }

    void Comtype()
    {
        DataSet Ds = new DataSet();
        try
        {

            DataTable ddfee = new DataTable();
            ddfee = DBCon.Ora_Execute_table("select Complaint_Code,Complaint_Type from Ref_Jenis_Aduan where Complaint_Code='" + status + "'");
            stscd = ddfee.Rows[0][0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private DataSet GetData(string query)
    {

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


    protected void Button1_Click(object sender, EventArgs e)
    {

        if (RadioButton1.Checked == true || RadioButton2.Checked == true || RadioButton3.Checked == true)
        {
            if (txtdesc.Value != "")
            {

                if (RadioButton1.Checked == true)
                {
                    status = "SA";
                    Comtype();
                    scmd = "B";
                }
                else if (RadioButton2.Checked == true)
                {
                    status = "CA";
                    Comtype();
                    scmd = "B";
                }
                else
                {
                    status = "LA";
                    Comtype();
                    scmd = "B";
                }
                string userid = Session["New"].ToString();

                SqlConnection con = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Anggota_complan";
                cmd.Parameters.Add("@icno", SqlDbType.VarChar).Value = Txtnokp.Text.Trim();
                cmd.Parameters.Add("@Comdate", SqlDbType.DateTime).Value =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add("@Comtype", SqlDbType.VarChar).Value = status;
                cmd.Parameters.Add("@Comdesc", SqlDbType.VarChar).Value = txtdesc.Value.Trim();
                cmd.Parameters.Add("@Comstcd", SqlDbType.VarChar).Value = scmd;
                cmd.Parameters.Add("@Comcrtdt", SqlDbType.DateTime).Value =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add("@Comcrtid", SqlDbType.VarChar).Value = userid;
                cmd.Parameters.Add("@telno", SqlDbType.VarChar).Value = Txtnotel.Text;
                cmd.Parameters.Add("@kat", SqlDbType.VarChar).Value = dd_kat.SelectedValue;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    clear();
                    service.audit_trail("P0132", "Simpan", "No Kp Baru", Txtnokp.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();

                }
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Catatan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }


        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Jenis Aduan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        BindData();
    }

    void clear()
    {
        //Txtnokp.Text = "";
        Txtnama.Text = "";
        Txtnotel.Text = "";
        TxtTaradu.Text = DateTime.Now.ToString("dd/MM/yyyy");
        RadioButton1.Checked = false;
        RadioButton2.Checked = false;
        RadioButton3.Checked = false;
        txtdesc.Value = "";
        TxtTaradu.Text = "";
        bind();
        BindData();
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../keanggotan/Daftar_aduan.aspx");
    }


}