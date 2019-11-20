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

public partial class TKumbulan_TK_Km_Bayaran : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable pusat = new DataTable();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    string userid;

    protected void Page_Load(object sender, EventArgs e)
    {
        string script = "  $().ready(function () {  $('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                assgn_roles();
                userid = Session["New"].ToString();
                dropdownName();
                showgrid();
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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.* from KK_Role_skrins m1   where sub_skrin_id='S0046' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {

                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();

                //if (role_add == "1")
                //{
                //    btnupdt.Visible = true;
                //}
                //else
                //{
                //    btnupdt.Visible = false;
                //}

            }
        }
    }

    private void dropdownName()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string com = "select wp4_batch_name from aim_wp4 where ISNULL(wp4_batch_name,'')!='' and ISNULL(wp4_pay_dt,'') = ''  group by wp4_batch_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddljp.DataSource = dt;
            ddljp.DataBind();
            ddljp.DataTextField = "wp4_batch_name";
            ddljp.DataValueField = "wp4_batch_name";
            ddljp.DataBind();
            ddljp.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
        //btnUpdate.Visible = isUpdateVisible;
    }



    protected void gvUserInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            var ddl = (DropDownList)e.Row.FindControl("Bank_details");
            //int CountryId = Convert.ToInt32(e.Row.Cells[0].Text);
            SqlCommand cmd = new SqlCommand("select * from Ref_Nama_Bank ORDER BY Id ASC", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            
            ddl.DataSource = ds;
            ddl.DataTextField = "Bank_Name";
            ddl.DataValueField = "Bank_Code";
            ddl.DataBind();
            ddl.SelectedValue = ((DataRowView)e.Row.DataItem)["wp4_bank_cd"].ToString();
            ddl.Items.Insert(0, new ListItem("--- PILIH ---", "0"));
        }
    }

    public void showgrid()
    {
       
            assgn_roles();
            DataTable ddicno = new DataTable();
            string txtgt = ddljp.SelectedValue;
        con.Open();
        SqlCommand cmd = new SqlCommand("select distinct m.mem_new_icno,m.mem_name,m.mem_member_no,m.mem_centre,a.wp4_amt,a.wp4_bene_acc_no,a.wp4_bank_cd,a.wp4_batch_name,rc.cawangan_name from mem_member m left Join aim_wp4 a on m.mem_new_icno=a.wp4_bene_new_icno left join Ref_Cawangan as rc ON rc.cawangan_code=m.mem_branch_cd where wp4_batch_name='" + txtgt + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            btnupdt.Visible = false;
            Button2.Visible = false;
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {
            if (role_add == "1")
            {
                btnupdt.Visible = true;
                Button2.Visible = true;
            }
            else
            {
                btnupdt.Visible = false;
                Button2.Visible = false;
            }
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }

        con.Close();


    }
    protected void clk_reset(object sender, EventArgs e)
    {
        ddljp.SelectedValue = "";
        showgrid();
    }
    protected void btnupdt_Click1(object sender, EventArgs e)
    {

        using (SqlConnection con = new SqlConnection(cs))
        {
            string strDate =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                var checkbox = gvrow.FindControl("chkSelect") as CheckBox;
                if (checkbox.Checked == true)
                {

                    string usrid = Session["New"].ToString();
                    var lblID = gvrow.FindControl("Label2") as Label;
                    //var bno = gvrow.FindControl("Label6") as TextBox;
                    //var bcd = gvrow.FindControl("Label7") as TextBox;
                    //var paydt = gvrow.FindControl("Label8") as TextBox;
                    TextBox box1 = (TextBox)gvrow.FindControl("Label8");
                    TextBox box2 = (TextBox)gvrow.FindControl("Label9");
                    TextBox box3 = (TextBox)gvrow.FindControl("TextBox2");
                    DropDownList ddl = (DropDownList)gvrow.FindControl("Bank_details");

                    string bdate = box3.Text;
                    DateTime bd = DateTime.ParseExact(bdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    String up_date = bd.ToString("yyyy-mm-dd");

                    SqlCommand cmd = new SqlCommand("UPDATE aim_wp4 SET [wp4_pay_id] = @wp4_pay_id, [wp4_pay_txn_dt] = @wp4_pay_txn_dt, [wp4_bene_acc_no] = @wp4_bene_acc_no, [wp4_bank_cd] = @wp4_bank_cd, [wp4_pay_dt] = @wp4_pay_dt,[wp4_upd_dt] = @wp4_upd_dt,[wp4_upd_id] = @wp4_upd_id WHERE wp4_bene_new_icno='" + lblID.Text + "' and wp4_batch_name='"+ ddljp.SelectedValue +"'", con);
                    //cmd.Parameters.AddWithValue("set_apply_sts_ind", "V");
                    cmd.Parameters.AddWithValue("wp4_pay_id", usrid);
                    cmd.Parameters.AddWithValue("wp4_pay_txn_dt", up_date);
                    cmd.Parameters.AddWithValue("wp4_bene_acc_no", box1.Text);
                    cmd.Parameters.AddWithValue("wp4_bank_cd", ddl.SelectedValue);
                    cmd.Parameters.AddWithValue("wp4_pay_dt", strDate);
                    cmd.Parameters.AddWithValue("wp4_upd_dt", DateTime.Today);
                    cmd.Parameters.AddWithValue("wp4_upd_id", Session["New"].ToString());

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            service.audit_trail("S0046", "", "Kemaskini Maklumat Bayaran", ddljp.SelectedValue);
            ddljp.SelectedValue = "";            
            showgrid();            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
    }

    protected void BindGridview(object sender, EventArgs e)
    {
        if (ddljp.SelectedValue != "")
        {
            showgrid();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Nama Kelompok',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        showgrid();
        GridView1.DataBind();
    }
}