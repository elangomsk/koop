using System;
using System.Collections.Generic;
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
public partial class kw_role : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string level;
    string Status = string.Empty, Status1 = string.Empty, Status2 = string.Empty;
    string userid;
    string ref_id;
    string confirmValue, am;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);

        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                 //PopulateTreeview();
                userid = Session["New"].ToString();
                bind_kategory();
                //btb_kmes.Visible = false;
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                    edit_id.Text = "1";
                    //rol_set.Attributes.Remove("style");
                    submit_btn.Text = "Kemaskini";
                    shw_cnt1.Visible = true;
                    BindData_jenis();
                    BindData_jenis1();
                }
                else
                {
                    //rol_set.Attributes.Add("style", "Pointer-events:none; opacity: 0.6;");
                    shw_cnt1.Visible = false;
                    submit_btn.Text = "Simpan";
                    edit_id.Text = "0";
                }
                
            }
            else
            {                
                Response.Redirect("~/KSAIMB_Login.aspx");
            }
        }
    }

    void bind_kategory()
    {
        string com2 = "select * from kw_kategory_kelulusan where status='A'";
        SqlDataAdapter adpt2 = new SqlDataAdapter(com2, con);
        DataTable dt2 = new DataTable();
        adpt2.Fill(dt2);
        Dropdownlist2.DataSource = dt2;
        Dropdownlist2.DataTextField = "kat_name";
        Dropdownlist2.DataValueField = "kat_cod";
        Dropdownlist2.DataBind();
        Dropdownlist2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }
    protected void BindData_jenis()
    {
        con.Open();
        string sqry = string.Empty;
        if (txtSearch.Text == "")
        {
            sqry = "";
        }
        else
        {
            sqry = "where a.KK_Skrin_name LIKE'%" + txtSearch.Text + "%' OR b.KK_Sskrin_name LIKE'%" + txtSearch.Text + "%' OR c.KK_Spreskrin_name LIKE'%" + txtSearch.Text + "%' OR d.KK_Spreskrin1_name LIKE'%" + txtSearch.Text + "%'";
        }

        string cmdstr = string.Empty;

        if (Session["roles"].ToString() == "R0001")
        {
            cmdstr = "select * from (select a.KK_Skrin_id,a.KK_Skrin_name,b.KK_Sskrin_id,b.KK_Sskrin_name,ISNULL(c.KK_Spreskrin_id,'') KK_Spreskrin_id,ISNULL(c.KK_Spreskrin_name,'') KK_Spreskrin_name,ISNULL(d.KK_Spreskrin1_id,'') KK_Spreskrin1_id,ISNULL(d.KK_Spreskrin1_name,'') KK_Spreskrin1_name,a.position as pos1,b.position as pos2,c.position as pos3,d.position  as pos4 from (select * from KK_PID_Skrin m1 where Status='A') as a left join (select * from KK_PID_Sub_Skrin where Status='A' ) as b on b.KK_Skrin_id=a.KK_Skrin_id left join (select * from KK_PID_presub_skrin where Status='A') as c on c.KK_Skrin_id=B.KK_Skrin_id and c.KK_Sskrin_id=b.KK_Sskrin_id left join (select * from KK_PID_presub1_skrin where Status='A') as d on d.KK_Skrin_id=c.KK_Skrin_id and d.KK_Sskrin_id=c.KK_Sskrin_id and d.KK_Spreskrin_id=c.KK_Spreskrin_id " + sqry + ") as a except (select a.KK_Skrin_id,a.KK_Skrin_name,b.KK_Sskrin_id,b.KK_Sskrin_name,ISNULL(c.KK_Spreskrin_id,'') KK_Spreskrin_id,ISNULL(c.KK_Spreskrin_name,'') KK_Spreskrin_name,ISNULL(d.KK_Spreskrin1_id,'') KK_Spreskrin1_id,ISNULL(d.KK_Spreskrin1_name,'') KK_Spreskrin1_name,a.position as pos1,b.position as pos2,c.position as pos3,d.position  as pos4 from KK_Role_skrins left join KK_PID_Skrin a on a.KK_Skrin_id=skrin_id left join KK_PID_Sub_Skrin b on b.KK_Skrin_id=skrin_id and b.KK_Sskrin_id=sub_skrin_id left join KK_PID_presub_skrin c on c.KK_Skrin_id=skrin_id and c.KK_Sskrin_id=sub_skrin_id and c.KK_Spreskrin_id=psub_skrin_id left join KK_PID_presub1_skrin d on d.KK_Skrin_id=skrin_id and d.KK_Sskrin_id=sub_skrin_id and d.KK_Spreskrin_id=psub_skrin_id and d.KK_Spreskrin1_id=psub1_skrin_id where Role_id='" + TextBox1.Text +"')   order by pos1,pos2,pos3,pos4";
        }
        else
        {
            //cmdstr = "select KK_Skrin_id,KK_Sskrin_id,UPPER(KK_Sskrin_name) as KK_Sskrin_name from KK_PID_Sub_Skrin where status='A' and KK_Sskrin_id NOT IN ('S0012','S0013','S0014','S0015') order by cast(position as int)";
            cmdstr = "select * from (select a.KK_Skrin_id,a.KK_Skrin_name,b.KK_Sskrin_id,b.KK_Sskrin_name,ISNULL(c.KK_Spreskrin_id,'') KK_Spreskrin_id,ISNULL(c.KK_Spreskrin_name,'') KK_Spreskrin_name,ISNULL(d.KK_Spreskrin1_id,'') KK_Spreskrin1_id,ISNULL(d.KK_Spreskrin1_name,'') KK_Spreskrin1_name,a.position as pos1,b.position as pos2,c.position as pos3,d.position  as pos4 from (select * from KK_PID_Skrin m1 where Status='A') as a left join (select * from KK_PID_Sub_Skrin where Status='A' and KK_Sskrin_id NOT IN ('S0012','S0013','S0014','S0015','S0016','S0017')) as b on b.KK_Skrin_id=a.KK_Skrin_id left join (select * from KK_PID_presub_skrin where Status='A') as c on c.KK_Skrin_id=B.KK_Skrin_id and c.KK_Sskrin_id=b.KK_Sskrin_id left join (select * from KK_PID_presub1_skrin where Status='A') as d on d.KK_Skrin_id=c.KK_Skrin_id and d.KK_Sskrin_id=c.KK_Sskrin_id and d.KK_Spreskrin_id=c.KK_Spreskrin_id " + sqry + ") as a except (select a.KK_Skrin_id,a.KK_Skrin_name,b.KK_Sskrin_id,b.KK_Sskrin_name,ISNULL(c.KK_Spreskrin_id,'') KK_Spreskrin_id,ISNULL(c.KK_Spreskrin_name,'') KK_Spreskrin_name,ISNULL(d.KK_Spreskrin1_id,'') KK_Spreskrin1_id,ISNULL(d.KK_Spreskrin1_name,'') KK_Spreskrin1_name,a.position as pos1,b.position as pos2,c.position as pos3,d.position  as pos4 from KK_Role_skrins left join KK_PID_Skrin a on a.KK_Skrin_id=skrin_id left join KK_PID_Sub_Skrin b on b.KK_Skrin_id=skrin_id and b.KK_Sskrin_id=sub_skrin_id and KK_Sskrin_id NOT IN ('S0012','S0013','S0014','S0015','S0017','S0016') left join KK_PID_presub_skrin c on c.KK_Skrin_id=skrin_id and c.KK_Sskrin_id=sub_skrin_id and c.KK_Spreskrin_id=psub_skrin_id  left join KK_PID_presub1_skrin d on d.KK_Skrin_id=skrin_id and d.KK_Sskrin_id=sub_skrin_id and d.KK_Spreskrin_id=psub_skrin_id and d.KK_Spreskrin1_id=psub1_skrin_id where Role_id='" + TextBox1.Text + "') order by pos1,pos2,pos3,pos4";
        }

        
        SqlCommand cmd = new SqlCommand("" + cmdstr + "", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
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
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        con.Close();
    }


    protected void BindData_jenis1()
    {
        con.Open();
        string cmdstr = string.Empty;
        
            //cmdstr = "select KK_Skrin_id,KK_Sskrin_id,UPPER(KK_Sskrin_name) as KK_Sskrin_name from KK_PID_Sub_Skrin where status='A' and KK_Sskrin_id NOT IN ('S0012','S0013','S0014','S0015') order by cast(position as int)";
            cmdstr = "select m1.Id,a.KK_Skrin_id,a.KK_Skrin_name,b.KK_Sskrin_id,b.KK_Sskrin_name,ISNULL(c.KK_Spreskrin_id,'') KK_Spreskrin_id,ISNULL(c.KK_Spreskrin_name,'') KK_Spreskrin_name,ISNULL(d.KK_Spreskrin1_id,'') KK_Spreskrin1_id,ISNULL(d.KK_Spreskrin1_name,'') KK_Spreskrin1_name from KK_Role_skrins m1 left join KK_PID_Skrin a on a.KK_Skrin_id=skrin_id left join KK_PID_Sub_Skrin b on b.KK_Skrin_id=skrin_id and b.KK_Sskrin_id=sub_skrin_id left join KK_PID_presub_skrin c on c.KK_Skrin_id=skrin_id and c.KK_Sskrin_id=sub_skrin_id and c.KK_Spreskrin_id=psub_skrin_id left join KK_PID_presub1_skrin d on d.KK_Skrin_id=skrin_id and d.KK_Sskrin_id=sub_skrin_id and d.KK_Spreskrin_id=psub_skrin_id and d.KK_Spreskrin1_id=psub1_skrin_id where Role_id='" + TextBox1.Text + "' order by a.position,b.position,c.position,d.position";
        


        SqlCommand cmd = new SqlCommand("" + cmdstr + "", con);
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
            //btn_hups.Visible = false;
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }
        con.Close();
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        BindData_jenis();
    }


    protected void gvSelected_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
        BindData_jenis1();
    }


    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

        BindData_jenis();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindData_jenis();
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
      

        System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)sender;
        decimal val1 = 0;
        //if (TextBox4.Text != "")
        //{
       

        if (chk.Checked == true)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                System.Web.UI.WebControls.CheckBox rbn = (System.Web.UI.WebControls.CheckBox)row.FindControl("chk1");
                if (rbn.Checked == true)
                {
                    try
                    {
                        int RowIndex = row.RowIndex;
                        string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("vv1")).Text.ToString();
                        string varName2 = ((System.Web.UI.WebControls.Label)row.FindControl("vv2")).Text.ToString();
                        string varName3 = ((System.Web.UI.WebControls.Label)row.FindControl("vv3")).Text.ToString();
                        string varName4 = ((System.Web.UI.WebControls.Label)row.FindControl("vv4")).Text.ToString();

                        System.Web.UI.WebControls.CheckBox chk1 = (System.Web.UI.WebControls.CheckBox)row.FindControl("chk1");
                      
                        string sv1 = string.Empty, sv2 = string.Empty, sv3 = string.Empty, sv4 = string.Empty;
                        if (chk1.Checked == true)
                        {
                            sv1 = "1";
                        }
                        else
                        {
                            sv1 = "0";
                        }

                                          
                        string Inssql2 = "Insert into KK_Role_skrins (Role_id,skrin_id,sub_skrin_id,psub_skrin_id,psub1_skrin_id,permisssion,view_chk,Add_chk,Edit_chk,crt_id,crt_dt) values ('" + TextBox1.Text + "','" + varName1 + "','" + varName2 + "','" + varName3 + "','" + varName4 + "','" + sv1 + "','1','1','1','" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql2);
                        if(Status == "SUCCESS")
                        {
                            BindData_jenis();
                            BindData_jenis1();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Skrin yang Ditugaskan Berjaya.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        }
                      


                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
        }
    
       
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        

      
    }

    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {


        System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)sender;
        decimal val1 = 0;
      
        if (chk.Checked == false)
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                System.Web.UI.WebControls.CheckBox rbn = (System.Web.UI.WebControls.CheckBox)row.FindControl("chk1");
                if (rbn.Checked == false)
                {
                    try
                    {
                        int RowIndex = row.RowIndex;
                        string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("get_id")).Text.ToString();
                     
                        string Inssql2 = "delete from KK_Role_skrins where ID ='"+ varName1 + "'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql2);
                        if (Status == "SUCCESS")
                        {
                            BindData_jenis();
                            BindData_jenis1();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Skrin Dikeluarkan Dengan Berjaya.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
        }
    }


    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
         
            System.Web.UI.WebControls.CheckBox CheckBox1 = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chk1");
            System.Web.UI.WebControls.CheckBox CheckBox2 = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chk2");
            System.Web.UI.WebControls.CheckBox CheckBox3 = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chk3");
            System.Web.UI.WebControls.CheckBox CheckBox4 = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chk4");
            string vv1 = ((System.Web.UI.WebControls.Label)e.Row.FindControl("vv1")).Text.ToString();
            string vv2 = ((System.Web.UI.WebControls.Label)e.Row.FindControl("vv2")).Text.ToString();
            string vv3 = ((System.Web.UI.WebControls.Label)e.Row.FindControl("vv3")).Text.ToString();
            string vv4 = ((System.Web.UI.WebControls.Label)e.Row.FindControl("vv4")).Text.ToString();

            DataTable dd2 = new DataTable();
            dd2 = DBCon.Ora_Execute_table("select * from KK_Role_skrins where Role_id='" + TextBox1.Text + "' and skrin_id='" + vv1 + "' and sub_skrin_id='" + vv2 + "' and psub_skrin_id='" + vv3 + "' and psub1_skrin_id='" + vv4 + "'");
            if (dd2.Rows.Count != 0)
            {
                if (dd2.Rows[0]["permisssion"].ToString() == "1")
                {
                    CheckBox1.Checked = true;
                }

                if (dd2.Rows[0]["view_chk"].ToString() == "1")
                {
                    CheckBox2.Checked = true;
                }
                if (dd2.Rows[0]["Add_chk"].ToString() == "1")
                {
                    CheckBox3.Checked = true;
                }
                if (dd2.Rows[0]["Edit_chk"].ToString() == "1")
                {
                    CheckBox4.Checked = true;
                }
            }
            else
            {


                if (CheckBox1.Checked == false)
                {
                    CheckBox2.Attributes.Add("style", "pointer-events:none;");
                    CheckBox3.Attributes.Add("style", "pointer-events:none;");
                    CheckBox4.Attributes.Add("style", "pointer-events:none;");
                    CheckBox2.Checked = false;
                    CheckBox3.Checked = false;
                    CheckBox4.Checked = false;
                }
                else
                {
                    CheckBox2.Checked = true;
                    CheckBox3.Checked = true;
                    CheckBox4.Checked = true;
                    CheckBox2.Attributes.Remove("style");
                    CheckBox3.Attributes.Remove("style");
                    CheckBox4.Attributes.Remove("style");
                }
            }

        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            System.Web.UI.WebControls.CheckBox chkAll = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chkAll");
            chkAll.Checked = true;
        }
    }
    void view_details()
    {
        try
        {
            Button1.Visible = false;
             DataTable dd1 = new DataTable();
            dd1 = DBCon.Ora_Execute_table("select * from KK_PID_Kumpulan where ID='" + lbl_name.Text + "'");
            if (dd1.Rows.Count != 0)
            {
                txtFee.Text = dd1.Rows[0]["KK_Kumpulan_name"].ToString();
                TextBox1.Text = dd1.Rows[0]["KK_Kumpulan_id"].ToString();
                dd_sts.SelectedValue = dd1.Rows[0]["Status"].ToString();
                Dropdownlist1.SelectedValue = dd1.Rows[0]["ctrl_type"].ToString();
                Dropdownlist2.SelectedValue = dd1.Rows[0]["kel_kategory"].ToString();

                string[] k = dd1.Rows[0]["KK_kumpulan_screen"].ToString().Split(',');

              

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Kakitangan Tidak Berdaftar',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   


    protected void clk_submit(object sender, EventArgs e)
    {
        if (txtFee.Text != "")
        {
            if (edit_id.Text == "0")
            {
                DataTable dd2 = new DataTable();
                dd2 = DBCon.Ora_Execute_table("select top(1) (RIGHT(KK_kumpulan_id, 4) + 1) cnt from KK_PID_Kumpulan order by KK_kumpulan_id desc");
                string ss2 = string.Empty;
                ss2 = dd2.Rows[0]["cnt"].ToString();
                var count = "R" + ss2.PadLeft(4, '0');

                string Inssql = "INSERT INTO KK_PID_Kumpulan (KK_kumpulan_id,KK_kumpulan_name,Status,KK_kumpulan_screen,crt_id,crt_dt,ctrl_type,kel_kategory) VALUES ('" + count + "','" + txtFee.Text + "','" + dd_sts.SelectedValue + "','','','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+ Dropdownlist1.SelectedValue + "','" + Dropdownlist2.SelectedValue + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    service.audit_trail("S0011", "Peranan Masukkan", "Nama Peranan", txtFee.Text);
                    Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                    Session["validate_success"] = "SUCCESS";
                    Response.Redirect("../Pentadbiran/kw_role_view.aspx");
                    //tvTables.CheckedNodes.Clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert',{'type': 'error','title': 'Error','auto_close': 2000}); ", true);
                }
            }
            else
            {

                string Inssql1 = "UPDATE KK_PID_Kumpulan SET kel_kategory='" + Dropdownlist2.SelectedValue + "',KK_kumpulan_name = '" + txtFee.Text + "',ctrl_type='"+ Dropdownlist1.SelectedValue +"', Status= '" + dd_sts.SelectedValue + "', upd_id = '',upd_dt = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE ID = '" + lbl_name.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                if (Status == "SUCCESS")
                {
                    //DataTable dd2 = new DataTable();
                    //dd2 = DBCon.Ora_Execute_table("select * from KK_Role_skrins where Role_id='"+ TextBox1.Text + "'");
                    //if (dd2.Rows.Count != 0)
                    //{
                    //    string Inssql3 = "Delete from KK_Role_skrins where Role_id='" + TextBox1.Text + "'";
                    //    Status1 = DBCon.Ora_Execute_CommamdText(Inssql3);
                    //}
                    //else
                    //{
                    //    Status1 = "SUCCESS";
                    //}
                    //if (Status1 == "SUCCESS")
                    //{
                    foreach (GridViewRow row in GridView2.Rows)
                    {
                        System.Web.UI.WebControls.CheckBox rbn = (System.Web.UI.WebControls.CheckBox)row.FindControl("chk1");
                        if (rbn.Checked == true)
                        {
                            try
                            {
                                int RowIndex = row.RowIndex;
                                string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("vv1")).Text.ToString();
                                string varName2 = ((System.Web.UI.WebControls.Label)row.FindControl("vv2")).Text.ToString();
                                string varName3 = ((System.Web.UI.WebControls.Label)row.FindControl("vv3")).Text.ToString();
                                string varName4 = ((System.Web.UI.WebControls.Label)row.FindControl("vv4")).Text.ToString();

                                System.Web.UI.WebControls.CheckBox chk1 = (System.Web.UI.WebControls.CheckBox)row.FindControl("chk1");
                                System.Web.UI.WebControls.CheckBox chk2 = (System.Web.UI.WebControls.CheckBox)row.FindControl("chk2");
                                System.Web.UI.WebControls.CheckBox chk3 = (System.Web.UI.WebControls.CheckBox)row.FindControl("chk3");
                                System.Web.UI.WebControls.CheckBox chk4 = (System.Web.UI.WebControls.CheckBox)row.FindControl("chk4");

                                string sv1 = string.Empty, sv2 = string.Empty, sv3 = string.Empty, sv4 = string.Empty;
                                if (chk1.Checked == true)
                                {
                                    sv1 = "1";
                                }
                                else
                                {
                                    sv1 = "0";
                                }

                                if (chk2.Checked == true)
                                {
                                    sv2 = "1";
                                }
                                else
                                {
                                    sv2 = "0";
                                }

                                if (chk3.Checked == true)
                                {
                                    sv3 = "1";
                                }
                                else
                                {
                                    sv3 = "0";
                                }

                                if (chk4.Checked == true)
                                {
                                    sv4 = "1";
                                }
                                else
                                {
                                    sv4 = "0";
                                }

                                DataTable dd2 = new DataTable();
                                dd2 = DBCon.Ora_Execute_table("select * from KK_Role_skrins where Role_id='" + TextBox1.Text + "' and skrin_id='" + varName1 + "' and sub_skrin_id='" + varName2 + "' and psub_skrin_id='" + varName3 + "' and psub1_skrin_id='" + varName4 + "'");
                                if (dd2.Rows.Count != 0)
                                {
                                    string Inssql2 = "Update KK_Role_skrins set kel_kategory='" + Dropdownlist2.SelectedValue + "',view_chk='" + sv2 + "',Add_chk='" + sv3 + "',Edit_chk='" + sv4 + "',upd_id='"+ userid +"',upd_dt='"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Role_id='" + TextBox1.Text + "' and skrin_id='" + varName1 + "' and sub_skrin_id='" + varName2 + "' and psub_skrin_id='" + varName3 + "' and psub1_skrin_id='" + varName4 + "'";
                                Status = DBCon.Ora_Execute_CommamdText(Inssql2);
                                }


                            }
                            catch (Exception ex)
                            {

                            }
                        }

                    }
                    service.audit_trail("S0011", "Peranan Kemaskini", "Nama Peranan", txtFee.Text);
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                        Session["validate_success"] = "SUCCESS";
                        Response.Redirect("../Pentadbiran/kw_role_view.aspx");
                        //tvTables.CheckedNodes.Clear();
                    }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert',{'type': 'error','title': 'Error','auto_close': 2000}); ", true);
                //}

            }
        }
        else
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Nama Kumpulan',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
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
                    row.Cells[5].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                   
                }
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    System.Web.UI.WebControls.CheckBox rbn = (System.Web.UI.WebControls.CheckBox)row.FindControl("chk1");
                    if (rbn.Checked == true)
                    {
                        try
                        {
                            int RowIndex = row.RowIndex;
                            string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("vv1")).Text.ToString();
                            string varName2 = ((System.Web.UI.WebControls.Label)row.FindControl("vv2")).Text.ToString();
                            string varName3 = ((System.Web.UI.WebControls.Label)row.FindControl("vv3")).Text.ToString();
                            string varName4 = ((System.Web.UI.WebControls.Label)row.FindControl("vv4")).Text.ToString();

                            System.Web.UI.WebControls.CheckBox chk1 = (System.Web.UI.WebControls.CheckBox)row.FindControl("chk1");

                            string sv1 = string.Empty, sv2 = string.Empty, sv3 = string.Empty, sv4 = string.Empty;
                            if (chk1.Checked == true)
                            {
                                sv1 = "1";
                            }
                            else
                            {
                                sv1 = "0";
                            }


                            string Inssql2 = "Insert into KK_Role_skrins (Role_id,skrin_id,sub_skrin_id,psub_skrin_id,psub1_skrin_id,permisssion,view_chk,Add_chk,Edit_chk,crt_id,crt_dt) values ('" + TextBox1.Text + "','" + varName1 + "','" + varName2 + "','" + varName3 + "','" + varName4 + "','" + sv1 + "','1','1','1','" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql2);
                            if (Status == "SUCCESS")
                            {
                                service.audit_trail("S0011", "Peranan Masukkan", "Peranan Id", TextBox1.Text);
                                BindData_jenis();
                                BindData_jenis1();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Skrin yang Ditugaskan Berjaya.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                            }



                        }
                        catch (Exception ex)
                        {

                        }
                    }


                }
            }
        }
      
    }

    protected void OnCheckedChanged1(object sender, EventArgs e)
    {
        bool isUpdateVisible = false;
        CheckBox chk = (sender as CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[5].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                    row.Cells[6].Controls.OfType<CheckBox>().FirstOrDefault().Checked = true;
                    row.Cells[7].Controls.OfType<CheckBox>().FirstOrDefault().Checked = true;
                    row.Cells[8].Controls.OfType<CheckBox>().FirstOrDefault().Checked = true;

                }
            }
        }
        CheckBox chkAll = (GridView2.HeaderRow.FindControl("chkAll") as CheckBox);

        chkAll.Checked = true;
        foreach (GridViewRow row in GridView2.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[5].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    if (isChecked && !isUpdateVisible)
                    {
                        isUpdateVisible = true;
                    }
                    if (!isChecked)
                    {
                        chkAll.Checked = false;
                        row.Cells[6].Controls.OfType<CheckBox>().FirstOrDefault().Checked = false;
                        row.Cells[7].Controls.OfType<CheckBox>().FirstOrDefault().Checked = false;
                        row.Cells[8].Controls.OfType<CheckBox>().FirstOrDefault().Checked = false;
                    }
                }
            }
        }

        //System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)sender;
        decimal val1 = 0;

        if (chk.Checked == false)
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                System.Web.UI.WebControls.CheckBox rbn = (System.Web.UI.WebControls.CheckBox)row.FindControl("chk1");
                if (rbn.Checked == false)
                {
                    try
                    {
                        int RowIndex = row.RowIndex;
                        string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("get_id")).Text.ToString();

                        string Inssql2 = "delete from KK_Role_skrins where ID ='" + varName1 + "'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql2);
                        if (Status == "SUCCESS")
                        {
                            service.audit_trail("S0011", "Peranan Hapus", "","");
                            BindData_jenis();
                            BindData_jenis1();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Skrin Dikeluarkan Dengan Berjaya.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
        }
        //btnUpdate.Visible = isUpdateVisible;
    }



    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pentadbiran/kw_role.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pentadbiran/kw_role_view.aspx");
    }

    
}