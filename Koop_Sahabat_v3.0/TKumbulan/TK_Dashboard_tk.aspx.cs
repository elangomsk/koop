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

public partial class TKumbulan_TK_Dashboard_tk : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    DBConnection DBCon = new DBConnection();
    DataTable ddcom = new DataTable();
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    string str;
    string str1;
    SqlCommand com;
    string status = string.Empty;
    string userid;
    // decimal moneyvalue = 1921.39m;

    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                assgn_roles();
                userid = Session["New"].ToString();
                dropdownName();
                bind();
            }
            else
            {
                Response.Redirect("KSAIMB_Login.aspx");
            }
            dropdownName();
            //bind();
            //cawBind();
            //userid = Session["New"].ToString();
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

                if (role_add == "1")
                {
                    btnsrch.Visible = true;
                }
                else
                {
                    btnsrch.Visible = false;
                }

            }
        }
    }

    private void dropdownName()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string com = "select branch_desc,branch_cd from Ref_branch order by branch_cd ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlcaw.DataSource = dt;
            ddlcaw.DataBind();
            ddlcaw.DataTextField = "branch_desc";
            ddlcaw.DataValueField = "branch_cd";
            ddlcaw.DataBind();
            ddlcaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnrst_Click(object sender, EventArgs e)
    {
        Response.Redirect("TK_Dashboard_tk.aspx");
    }

    private void cawBind()
    {
        SqlConnection con = new SqlConnection(cs);
        DataTable ddicno = new DataTable();
        ddicno = DBCon.Ora_Execute_table("select mem_new_icno from mem_member where mem_new_icno='" + txticno.Text + "'");
        if (ddicno.Rows.Count > 0)
        {
            str = "select mem_centre,mem_branch_cd from mem_member where mem_new_icno='" + txticno.Text + "'";
            com = new SqlCommand(str, con1);
            con1.Open();
            SqlDataReader reader = com.ExecuteReader();
            if (reader.Read())
            {
                txtbname.Text = reader["mem_centre"].ToString();
                //ddlcaw.SelectedItem.Value = reader["mem_branch_cd"].ToString();
                reader.Close();
                con1.Close();
            }
        }
        else if (ddicno.Rows.Count == 0)
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    private void bind()
    {
        SqlCommand cmd = new SqlCommand("select ps.pst_iq_dt,ps.pst_reason,ps.pst_apply_amt,sum(ps.pst_apply_amt) as total,ps.pst_post_dt,pk1.Product_Code from aim_pst ps Left join mem_member AS mm ON mm.mem_new_icno = ps.pst_new_icno left join Ref_Produk_TK as pk1 on pk1.Product_Description=ps.pst_withdrawal_type_cd where ps.pst_new_icno = '" + txticno.Text + "'  group by ps.pst_iq_dt,ps.pst_reason,ps.pst_apply_amt,ps.pst_post_dt,pk1.Product_Code  order by ps.pst_iq_dt,ps.pst_reason,ps.pst_apply_amt,ps.pst_post_dt", con1);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView2.DataSource = ds;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }
        SqlCommand cmd1 = new SqlCommand("SELECT WP.wp4_batch_name,WP.wp4_pay_txn_dt,WP.wp4_pay_detail,WP.wp4_amt,WP.wp4_amt AS TOTAL FROM aim_wp4 WP LEFT JOIN mem_member MM ON MM.mem_new_icno=WP.wp4_bene_new_icno WHERE WP.wp4_bene_new_icno='" + txticno.Text + "' GROUP BY WP.wp4_batch_name,WP.wp4_pay_txn_dt,WP.wp4_pay_detail,WP.wp4_amt ORDER BY WP.wp4_pay_txn_dt ASC", con1);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count == 0)
        {
            ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
            GridView1.DataSource = ds1;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            GridView1.DataSource = ds1;
            GridView1.DataBind();
        }
        //instaudt();



    }
    protected void btnsrch_Click(object sender, EventArgs e)
    {
        if (txticno.Text != "")
        {
            SqlConnection con = new SqlConnection(cs);
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select ast_new_icno from aim_st where ast_new_icno='" + txticno.Text + "'");
            if (ddicno.Rows.Count > 0)
            {
                str = "select DISTINCT ast.ast_no_sahabat,ast.ast_new_icno,ast.ast_name,ast.ast_centre_name,ast.ast_branch_name,ast.ast_branch_cd,ast.ast_region_name,ast.ast_st_balance_amt,ast.ast_end_date from aim_st as ast where ast.ast_new_icno='" + txticno.Text.Trim() + "'";
                //str = "select DISTINCT mem_member_no,mem_new_icno,mem_name,mem_centre,mem_region_cd,mem_branch_cd,mem_centre,at.ast_st_balance_amt,at.ast_end_date,rc.cawangan_name,rc.wilayah_name from mem_member mm left join Ref_Cawangan as rc ON rc.cawangan_code=mm.mem_branch_cd left join Ref_Wilayah as rw ON rw.wilayah_code=mm.mem_region_cd left join aim_st as at ON mm.mem_new_icno=at.ast_new_icno where mm.mem_new_icno='" + txticno.Text.Trim() + "'";
                com = new SqlCommand(str, con1);
                con1.Open();
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {

                    if ((string)reader["ast_branch_cd"].ToString() != "")
                    {

                        ddlcaw.SelectedValue = (string)reader["ast_branch_cd"].ToString();
                    }
                    else
                    {
                        ddlcaw.SelectedValue = "";

                    }

                    txtsahabt.Text = reader["ast_no_sahabat"].ToString();
                    txtnokb.Text = reader["ast_new_icno"].ToString();
                    txtname.Text = reader["ast_name"].ToString();
                    txtbname.Text = reader["ast_centre_name"].ToString();
                    txtwila.Text = reader["ast_region_name"].ToString();
                    txtcaw.Text = reader["ast_branch_name"].ToString();
                    txtpust.Text = reader["ast_centre_name"].ToString();

                    string fmdate = reader["ast_end_date"].ToString();
                    if (fmdate != "")
                    {
                        txttaric.Text = Convert.ToDateTime(fmdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        txttaric.Text = "";
                    }
                    string amt = reader["ast_st_balance_amt"].ToString();
                    if (amt != "")
                    {
                        txtjum.Text = Convert.ToDecimal(amt).ToString("0.00");
                    }
                    else
                    {
                        txtjum.Text = "";
                    }
                    //ddlcaw.Text = reader["mem_branch_cd"].ToString();
                    reader.Close();
                    con1.Close();
                }


            }
            else if (ddicno.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No KP Baru.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        bind();
    }

    void instaudt()
    {
        string audcd = "020501";
        string auddec = "DASHBOARD TABUNG KUMPULAN";
        string usrid = Session["New"].ToString();
        string curdt =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc) values ('" + usrid + "','" + curdt + "','" + audcd + "','" + auddec + "')";
        status = DBCon.Ora_Execute_CommamdText(Inssql);
    }
}