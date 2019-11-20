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
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;


public partial class site_language : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();

    string level;
    string Status = string.Empty;
    string userid;
    string ref_id;
    string confirmValue, am;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                Session["validate_success"] = "";
                Session["alrt_msg"] = "";
                Session["pro_id"] = "";
                userid = Session["New"].ToString();
                BindData();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindData();
    }


    protected void BindData()
    {
        string sqry = string.Empty;
        if (txtSearch.Text == "")
        {
            sqry = "";
        }
        else
        {
            sqry = "where eng LIKE'%" + txtSearch.Text + "%' OR mal LIKE'%" + txtSearch.Text + "%'";
        }

        DataSet ds = new DataSet();
        DataTable FromTable = new DataTable();
        conn.Open();
        string cmdstr = "select * From Ref_language s " + sqry + " order by s.ID";
        SqlCommand cmd = new SqlCommand(cmdstr, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.ExecuteNonQuery();
        FromTable = ds.Tables[0];
        if (FromTable.Rows.Count > 0)
        {
            gv_refdata.DataSource = ds;
            gv_refdata.DataBind();
        }
        else
        {
            FromTable.Rows.Add(FromTable.NewRow());
            gv_refdata.DataSource = ds;
            gv_refdata.DataBind();
            int TotalColumns = gv_refdata.Rows[0].Cells.Count;
            gv_refdata.Rows[0].Cells.Clear();
            gv_refdata.Rows[0].Cells.Add(new TableCell());
            gv_refdata.Rows[0].Cells[0].ColumnSpan = TotalColumns;
            gv_refdata.Rows[0].Cells[0].Text = "<center>Rekod Tidak Dijumpai. Sila Lakukan Semula Carian</center>";
        }
        ds.Dispose();
        conn.Close();
    }


    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindData();
        gv_refdata.PageIndex = e.NewPageIndex;
        gv_refdata.DataBind();
    }


    protected void gv_refdata_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            System.Web.UI.WebControls.TextBox Id = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
            conn.Open();
            string cmdstr = "delete from Ref_language where Id=@Id";
            SqlCommand cmd = new SqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@Id", Id.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            BindData();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dipadamkan',{'type': 'confirmation','title': 'Success','auto_close': 2000}); ", true);
        }
    }

    protected void gv_refdata_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ADD"))
        {

            System.Web.UI.WebControls.TextBox txtAddCawangan = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddCawangan");
            DropDownList ddlStatus = (DropDownList)gv_refdata.FooterRow.FindControl("ddlStatus");
            System.Web.UI.WebControls.TextBox lbladdwilayah = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("lbladdwilayah");
            System.Web.UI.WebControls.TextBox txtAddcode = (System.Web.UI.WebControls.TextBox)gv_refdata.FooterRow.FindControl("txtAddcode");
            if (txtAddCawangan.Text != "")
            {
                //DataTable dtcenter = new DataTable();
                //dtcenter = Dblog.Ora_Execute_table("select * from Ref_language where eng='" + txtAddCawangan.Text + "'");
                //if (dtcenter.Rows.Count == 0)
                //{
                //    DataTable dtcenter1 = new DataTable();
                //    dtcenter1 = Dblog.Ora_Execute_table("select * from Ref_language where mal='" + txtAddcode.Text + "'");
                //    if (dtcenter1.Rows.Count == 0)
                //    {
                        DataTable dt_upd_format = new DataTable();
                        dt_upd_format = DBCon.Ora_Execute_table("insert into Ref_language values ('" + lbladdwilayah.Text + "','"+ txtAddCawangan.Text + "','"+ txtAddcode.Text + "','"+ ddlStatus.SelectedValue + "','','','','')");
                        BindData();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Berjaya Masukkan Rekod',{'type': 'confirmation','title': 'Success','auto_close': 2000}); ", true);
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Kod Sudah wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
                //    }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
                //}

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Nilai',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
                BindData();
            }
        }

    }

    protected void gv_refdata_RowUpdating(object sender, GridViewUpdateEventArgs e)

    {

        System.Web.UI.WebControls.TextBox Id = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
        System.Web.UI.WebControls.Label lbleditwilayah = (System.Web.UI.WebControls.Label)gv_refdata.Rows[e.RowIndex].FindControl("lbleditwilayah");
        System.Web.UI.WebControls.TextBox txtEditCawangan = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditCawangan");
        DropDownList editddlStatus = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("editddlStatus");
        System.Web.UI.WebControls.TextBox txtEditcode = (System.Web.UI.WebControls.TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditcode");

        //DataTable dtcenter = new DataTable();
        //dtcenter = Dblog.Ora_Execute_table("select * from Ref_language where Id != '" + Id.Text + "'");
        //if (dtcenter.Rows.Count == 0)
        //{
            //DataTable dtcenter1 = new DataTable();
            //dtcenter1 = Dblog.Ora_Execute_table("select * from Ref_language where mal='" + txtEditcode.Text + "' AND Id != '" + Id.Text + "'");
            //if (dtcenter1.Rows.Count == 0)
            //{
                DataTable dt_upd_format = new DataTable();
                dt_upd_format = DBCon.Ora_Execute_table("update Ref_language set eng ='"+ txtEditCawangan.Text + "',mal='"+ txtEditcode.Text + "' where Id='"+ Id.Text + "'");
                gv_refdata.EditIndex = -1;
                BindData();

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dimuatnaik',{'type': 'confirmation','title': 'Success','auto_close': 2000}); ", true);

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Kod Sudah wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
            //}
        //}
        //else
        //{

        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
        //}

    }

    protected void gv_refdata_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_refdata.EditIndex = -1;
        BindData();
    }

    protected void gv_refdata_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_refdata.EditIndex = e.NewEditIndex;
        BindData();
    }
}