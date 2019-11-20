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


public partial class TKH_Penerimaan_Butiran_TKH : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string userid,batch, level;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                level = Session["level"].ToString();
               
                    Button2.Attributes.Add("disabled", "disabled");
              
                batch = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                
                lblbind();
                BindData();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    protected void RowDataBound(object sender, GridViewRowEventArgs e)

    {

        System.Web.UI.WebControls.CheckBox tick_chk = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("CheckSin");

        System.Web.UI.WebControls.Label lnk2 = (System.Web.UI.WebControls.Label)e.Row.FindControl("lb_4");
        if (e.Row.RowType == DataControlRowType.DataRow )

        {
        
         
            if (level =="5")
            {
                tick_chk.Visible = true;
               
              
            }
            else
            {
                tick_chk.Visible = false;
              
            }
            DataTable get_value = new DataTable();
            get_value = DBCon.Ora_Execute_table("select tkh_chk_vr From tkh_terima_item where tkh_tt_WP4_no='" + batch + "'  and tkh_tt_ic='" + lnk2.Text + "'");
            LinkButton lnk_roe = (LinkButton)e.Row.FindControl("lnkView");
            if (get_value.Rows[0]["tkh_chk_vr"].ToString() == "Y")
            {
                tick_chk.Checked = true;
            }
            else
            {
                tick_chk.Checked = false;
            }
            e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");

            e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");

        }

    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gv_refdata.HeaderRow.FindControl("checkAll");

        bool checkAll = true;

        int currentStartedIndex = (gv_refdata.PageIndex - 1) * gv_refdata.PageCount;
        int CurrentEndIndex = gv_refdata.PageIndex * gv_refdata.PageCount;

        foreach (GridViewRow row in gv_refdata.Rows)
        {
            if ((row.RowIndex >= currentStartedIndex) && (row.RowIndex <= CurrentEndIndex))
            {
                // Only look in data rows, ignore header and footer rows
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("chk");

                    if (ChkBoxRows.Checked == false)
                    {
                        checkAll = false;
                    }

                }
            }
        }
        ChkBoxHeader.Checked = checkAll;
    }

        void lblbind()
     {
        DataTable dt = new DataTable();
        dt = DBCon.Ora_Execute_table("select tkh_tt_batch_no,tkh_tt_sedia,tkh_tt_WP4_dt,tkh_tt_semak,tkh_tt_jana,tkh_tt_sah,tkh_tt_region_cd,tkh_tt_WP4_no,tkh_tt_status_wil,tkh_tt_ulasan_wil,tkh_tt_kemaskini_wil,format(tkh_tt_kemaskini_wil_dt,'dd/MM/yyyy')tkh_tt_kemaskini_wil_dt,tkh_tt_status_hq,tkh_tt_ulasan_hq,tkh_tt_kemaskini_hq,format(tkh_tt_kemaskini_hq_dt,'dd/MM/yyyy')tkh_tt_kemaskini_hq_dt from tkh_terima_item where tkh_tt_batch_no='" + batch + "'");
        if(dt.Rows.Count>0)
        {
            lblbatchview.Text = dt.Rows[0][0].ToString();
            lbldisview.Text = dt.Rows[0][1].ToString();
            lbltardijview.Text= dt.Rows[0][2].ToString();
            lbldisemakview.Text = dt.Rows[0][3].ToString();
            lbldijoleview.Text = dt.Rows[0][4].ToString();
            lbldisahkanview.Text = dt.Rows[0][5].ToString();
            lblwilview.Text = dt.Rows[0][6].ToString();
            if(level == "5")
            {
                H1.Visible = false;
                w1.Visible = true;
                ddsts.SelectedItem.Value = dt.Rows[0][7].ToString();
                txtula.Text = dt.Rows[0][8].ToString();
                lblDol.Text = dt.Rows[0][9].ToString();
                lbltark.Text = dt.Rows[0][10].ToString();
            }
            else
            {
             
                ddsts.SelectedItem.Value = dt.Rows[0][7].ToString();
                txtula.Text = dt.Rows[0][8].ToString();
                lblDol.Text = dt.Rows[0][9].ToString();
                lbltark.Text = dt.Rows[0][10].ToString();
             
                DDSTS1.SelectedItem.Value = dt.Rows[0][11].ToString();
                txtula1.Text = dt.Rows[0][12].ToString();
                lblDol1.Text = dt.Rows[0][13].ToString();
                lbltark1.Text = dt.Rows[0][14].ToString();
            }
            
           
        }
    }

    protected void BindData()
    {
        string sqry = string.Empty;
        string fmdate = string.Empty, fmonth = string.Empty, fyear = string.Empty, stdate = string.Empty, tmdate = string.Empty, tmdate1 = string.Empty;
       
        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select tkh_tt_area_cd,tkh_tt_no_shbt,tkh_tt_name,tkh_tt_ic,thk_tt_age,tkh_tt_produk,tkh_tt_pinjaman_amt,tkh_tt_tempoh,tkh_tt_caj_amt,tkh_tt_lindung_amt,tkh_tt_manfaat_amt,tkh_tt_caruman_amt,format(tkh_tt_httks,'#,##,###.00')tkh_tt_httks,format(tkh_tt_mula_dt,'dd/MM/yyyy') tkh_tt_mula_dt,format(tkh_tt_akhir_dt,'dd/MM/yyyy') tkh_tt_akhir_dt,tkh_wp4_stat,tkh_tt_status_wil,tkh_tt_ulasan_wil,tkh_tt_kemaskini_wil,format(tkh_tt_kemaskini_wil_dt,'dd/MM/yyyy')tkh_tt_kemaskini_wil_dt,tkh_tt_status_hq,tkh_tt_ulasan_hq,format(tkh_tt_kemaskini_hq_dt,'dd/MM/yyyy') tkh_tt_kemaskini_hq_dt,tkh_chk_vr from tkh_terima_item where tkh_tt_batch_no='" + batch + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gv_refdata.DataSource = ds;
            gv_refdata.DataBind();
            int columncount = gv_refdata.Rows[0].Cells.Count;
            gv_refdata.Rows[0].Cells.Clear();
            gv_refdata.Rows[0].Cells.Add(new TableCell());
            gv_refdata.Rows[0].Cells[0].ColumnSpan = columncount;
            gv_refdata.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {
            gv_refdata.DataSource = ds;
            gv_refdata.DataBind();
             
        }

        con.Close();
    }

    protected void btn_simpan(object sender, EventArgs e)
    {
        try
        {
            userid = Session["New"].ToString();
            level = Session["level"].ToString();

            if (level == "5")
            {
                foreach (GridViewRow grow in gv_refdata.Rows)
                {
                    //Searching CheckBox("chkDel") in an individual row of Grid  
                    CheckBox chkdel = (CheckBox)grow.FindControl("CheckSin");
                    //If CheckBox is checked than delete the record with particular empid  
                    if (ddsts.SelectedItem.Text=="Select")
                    {
                        if (chkdel.Checked)
                        {
                            string val2 = ((System.Web.UI.WebControls.Label)grow.FindControl("lb_4")).Text.ToString();
                          
                            DataTable dt = new DataTable();
                            dt = DBCon.Ora_Execute_table("update tkh_terima_item set  tkh_chk_vr='Y',tkh_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where  tkh_tt_ic='" + val2 + "'");
                        }
                    }
                    else
                    {
                        if (chkdel.Checked)
                        {
                            DataTable dt = new DataTable();
                            dt = DBCon.Ora_Execute_table("update tkh_terima_item set  tkh_tt_status_wil='" + ddsts.SelectedItem.Value + "',tkh_tt_ulasan_wil='" + txtula.Text + "',tkh_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',tkh_chk_vr='Y' where  tkh_tt_batch_no='" + batch + "'");
                        }
                    }
                   
                }
            }
            else
            {
                foreach (GridViewRow grow in gv_refdata.Rows)
                {
                    //Searching CheckBox("chkDel") in an individual row of Grid  
                    CheckBox chkdel = (CheckBox)grow.FindControl("CheckSin");
                    //If CheckBox is checked than delete the record with particular empid  
                    if (chkdel.Checked)
                    {
                        DataTable dt = new DataTable();
                        dt = DBCon.Ora_Execute_table("update tkh_terima_item set  tkh_tt_status_hq='" + DDSTS1.SelectedItem.Value + "',tkh_tt_ulasan_hq='" + txtula1.Text + "',tkh_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',tkh_tt_kemaskini_hq='" + lblDol1.Text + "',tkh_tt_kemaskini_hq_dt='" + lbltark1.Text + "' where  tkh_tt_ic='" + grow.Cells[4].Text + "'");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btn_hantar(object sender, EventArgs e)
    {
        try
        {
            if (level == "5")
            {
                DataTable dt = new DataTable();
                dt = DBCon.Ora_Execute_table("update tkh_terima_item set  tkh_tt_status_wil='TUNTUTAN SUMBANGAN DALAM PROSES' where  tkh_tt_batch_no='" + batch + "'");
            }
            else
            {
                DataTable dt = new DataTable();
                dt = DBCon.Ora_Execute_table("update tkh_terima set  tkh_tr_status_wil='TUNTUTAN SUMBANGAN DALAM PROSES' where  tkh_tr_WP4_no='" + batch + "'");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btn_tutub(object sender, EventArgs e)
    {
        Response.Redirect("../KSAIMB_Home.aspx");
    }
}