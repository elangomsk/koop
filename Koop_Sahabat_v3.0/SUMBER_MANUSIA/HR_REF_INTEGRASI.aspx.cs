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


public partial class HR_REF_INTEGRASI : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    string level;
    string Status = string.Empty, Status1 = string.Empty, Status2 = string.Empty, Status_del = string.Empty;
    string userid;
    string ref_id;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty, tc5 = string.Empty, tc6 = string.Empty, tc7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                if (Session["New"] != null)
                {
                    level = Session["level"].ToString();
                    userid = Session["New"].ToString();
                    BindData();
                }
                else
                {
                    Response.Redirect("../KSAIMB_Login.aspx");
                }
            }
        }
    }

    protected void dd_SelectedIndexChanged(object sender, EventArgs e)
    {
        sel_val();
    }
 

    void sel_val()
    {
      
        BindData();
    }


    protected void gv_refdata_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddList = (DropDownList)e.Row.FindControl("lbleditkodID");
                DropDownList ddList1 = (DropDownList)e.Row.FindControl("lbleditkodID1");

                //return DataTable havinf department data
                DataTable dt = load_department();

                ddList.DataSource = dt;
                ddList.DataTextField = "nama_akaun";
                ddList.DataValueField = "kod_akaun";
                ddList.DataBind();
                ddList.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DataRowView dr = e.Row.DataItem as DataRowView;
                ddList.SelectedItem.Text = dr["C2"].ToString();

                ddList1.DataSource = dt;
                ddList1.DataTextField = "nama_akaun";
                ddList1.DataValueField = "kod_akaun";
                ddList1.DataBind();
                ddList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                DataRowView dr1 = e.Row.DataItem as DataRowView;
                ddList1.SelectedItem.Text = dr1["C3"].ToString();

            }

        }

        //if (e.Row.RowType == DataControlRowType.Footer)
        //{

        //    DropDownList lbladdwilayah = (DropDownList)e.Row.FindControl("lbladdkodID");
        //    DropDownList lbladdwilayah1 = (DropDownList)e.Row.FindControl("lbladdkodID1");

        //    DataTable dt = load_department();

        //    lbladdwilayah.DataSource = dt;

        //    lbladdwilayah.DataTextField = "nama_akaun";

        //    lbladdwilayah.DataValueField = "kod_akaun";

        //    lbladdwilayah.DataBind();

        //    lbladdwilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));



        //    lbladdwilayah1.DataSource = dt;

        //    lbladdwilayah1.DataTextField = "nama_akaun";

        //    lbladdwilayah1.DataValueField = "kod_akaun";

        //    lbladdwilayah1.DataBind();

        //    lbladdwilayah1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        //    conn.Close();

        //}
    }

    public DataTable load_department()
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(cs);
        sqlcon.Open();
        string sql = "select kod_akaun,(kod_akaun +' | ' + nama_akaun) as nama_akaun from KW_Ref_Carta_Akaun where Status='A'";
        SqlCommand cmd = new SqlCommand(sql);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = sqlcon;
        SqlDataAdapter sd = new SqlDataAdapter(cmd);
        sd.Fill(dt);
        return dt;
    }


    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        BindData();

        gv_refdata.PageIndex = e.NewPageIndex;

        gv_refdata.DataBind();

    }

    protected void BindData()
    {
        if (sel_rt.SelectedValue == "01")
        {

            qry1 = "SELECT A.c1 AS C1,A.c7 AS C2,B.c7 AS C3,A.c4 AS C4,A.C5 FROM (Select  hr_ite_desc as c1,hr_ite_Maji_jenis_akaun as c2,s2.hr_ite_Per_jenis_akaun as c3,s2.Status as c4,s2.Id as c5,(s1.kod_akaun + ' | ' +s1.nama_akaun) as c7   from Ref_hr_Itegrasi s2 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=s2.hr_ite_Maji_jenis_akaun   ) A FULL OUTER JOIN (Select  hr_ite_desc as c1,hr_ite_Maji_jenis_akaun as c2,s2.hr_ite_Per_jenis_akaun as c3,s2.Status as c4,s2.Id as c5,(s1.kod_akaun + ' | ' +s1.nama_akaun) as c7   from Ref_hr_Itegrasi s2 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=s2.hr_ite_Per_jenis_akaun   ) B ON A.c1=B.c1 ORDER BY A.c3 ASC";


            SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count == 0)
            {
                ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
                gv_refdata.DataSource = ds2;
                gv_refdata.DataBind();
                int columncount = gv_refdata.Rows[0].Cells.Count;
                gv_refdata.Rows[0].Cells.Clear();
                gv_refdata.Rows[0].Cells.Add(new TableCell());
                gv_refdata.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_refdata.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            }
            else
            {
                gv_refdata.DataSource = ds2;
                gv_refdata.DataBind();
            }
        }
        else if (sel_rt.SelectedValue == "02")
        {
            qry1 = "SELECT A.c1 AS C1,A.c7 AS C2,B.c7 AS C3,A.c4 AS C4,A.C5 FROM (Select  hr_elau_desc as c1,elau_kod_akaun as c2,s2.elau_jenis_akaun as c3,s2.Status as c4,s2.Id as c5,(s1.kod_akaun + ' | ' +s1.nama_akaun) as c7   from Ref_hr_jenis_elaun s2 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=s2.elau_kod_akaun   ) A FULL OUTER JOIN (Select  hr_elau_desc as c1,elau_kod_akaun as c2,s2.elau_jenis_akaun as c3,s2.Status as c4,s2.Id as c5,(s1.kod_akaun + ' | ' +s1.nama_akaun) as c7   from Ref_hr_jenis_elaun s2 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=s2.elau_jenis_akaun   ) B ON A.c1=B.c1 ORDER BY A.c3 ASC";


            SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count == 0)
            {
                ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
                gv_refdata.DataSource = ds2;
                gv_refdata.DataBind();
                int columncount = gv_refdata.Rows[0].Cells.Count;
                gv_refdata.Rows[0].Cells.Clear();
                gv_refdata.Rows[0].Cells.Add(new TableCell());
                gv_refdata.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_refdata.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            }
            else
            {
                gv_refdata.DataSource = ds2;
                gv_refdata.DataBind();
            }
        }
        else if (sel_rt.SelectedValue == "03")
        {
            qry1 = "SELECT A.c1 AS C1,A.c7 AS C2,B.c7 AS C3,A.c4 AS C4,A.C5 FROM (Select  hr_poto_desc as c1,elau_kod_akaun as c2,s2.elau_jenis_akaun as c3,s2.Status as c4,s2.Id as c5,(s1.kod_akaun + ' | ' +s1.nama_akaun) as c7   from Ref_hr_potongan s2 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=s2.elau_kod_akaun   ) A FULL OUTER JOIN (Select  hr_poto_desc as c1,elau_kod_akaun as c2,s2.elau_jenis_akaun as c3,s2.Status as c4,s2.Id as c5,(s1.kod_akaun + ' | ' +s1.nama_akaun) as c7   from Ref_hr_potongan s2 left join KW_Ref_Carta_Akaun s1 on s1.kod_akaun=s2.elau_jenis_akaun   ) B ON A.c1=B.c1 ORDER BY A.c3 ASC";


            SqlCommand cmd2 = new SqlCommand("" + qry1 + "", con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count == 0)
            {
                ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
                gv_refdata.DataSource = ds2;
                gv_refdata.DataBind();
                int columncount = gv_refdata.Rows[0].Cells.Count;
                gv_refdata.Rows[0].Cells.Clear();
                gv_refdata.Rows[0].Cells.Add(new TableCell());
                gv_refdata.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_refdata.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            }
            else
            {
                gv_refdata.DataSource = ds2;
                gv_refdata.DataBind();
            }
        }
    }

    protected void gv_refdata_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
            TextBox Id = (TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
            conn.Open();
        if (sel_rt.SelectedValue == "01")
        {
            string cmdstr = "delete from Ref_hr_Itegrasi where Id =@Id";
            SqlCommand cmd = new SqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@Id", Id.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            sel_val();
        }
        else if (sel_rt.SelectedValue == "02")
        {
            string cmdstr = "delete from Ref_hr_jenis_elaun where Id =@Id";
            SqlCommand cmd = new SqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@Id", Id.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            sel_val();
        }
        else if (sel_rt.SelectedValue == "03")
        {
            string cmdstr = "delete from Ref_hr_potongan where Id =@Id";
            SqlCommand cmd = new SqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@Id", Id.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            sel_val();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dipadamkan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
       
    }

    protected void gv_refdata_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ADD"))
        {
            TextBox txtAddName = (TextBox)gv_refdata.FooterRow.FindControl("txtAddName");
        
            DropDownList lbladdkodID = (DropDownList)gv_refdata.FooterRow.FindControl("lbladdkodID");
            DropDownList lbladdkodID1 = (DropDownList)gv_refdata.FooterRow.FindControl("lbladdkodID1");
            DropDownList ddlStatus = (DropDownList)gv_refdata.FooterRow.FindControl("ddlStatus");
            if (txtAddName.Text != "" )
            {

                string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
                string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
                DataTable cnt_no = new DataTable();
                if (lbladdkodID.SelectedValue != "")
                {

                    //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
                    cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='" + lbladdkodID.SelectedValue + "'");

                    set_cnt1 = cnt_no.Rows[0]["cnt"].ToString();
                    mcnt = cnt_no.Rows[0]["rcnt"].ToString();
                    set_cnt = lbladdkodID.SelectedValue;
                    //set_cnt1 = "1";
                }
                else
                {
                    set_cnt1 = "0";
                    mcnt = "1";
                    set_cnt = "00";
                }


                DataTable cnt_no1 = new DataTable();
                cnt_no1 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_akaun_cnt,'') +1) as mcnt From KW_Ref_Carta_Akaun where jenis_akaun='" + lbladdkodID.SelectedValue + "' order by kod_akaun desc");

                if (cnt_no1.Rows.Count != 0)
                {
                    cnt_value = cnt_no1.Rows[0]["mcnt"].ToString();
                }
                else
                {
                    cnt_value = "1";
                }
                string chk_role = string.Empty;
                string[] sp_no = set_cnt.Split('.');
                int sp_no1 = sp_no.Length;
                string ss1 = string.Empty;
                for (int i = 0; i <= sp_no1; i++)
                {
                    if (i == (Int32.Parse(mcnt) - 1))
                    {
                        sno1 = cnt_value.PadLeft(2, '0');
                    }
                    else
                    {
                        sno1 = sp_no[i].ToString();
                    }
                    if (i < (sp_no1))
                    {
                        ss1 = ".";
                    }
                    else
                    {
                        ss1 = "";
                    }
                    chk_role += (sno1).PadLeft(2, '0') + "" + ss1;
                }

                string sso1 = string.Empty, sso2 = string.Empty, sso3 = string.Empty;
                if (set_cnt1 == "0")
                {
                    sso1 = "";
                }
                else
                {
                    if (cnt_no.Rows[0]["jenis_akaun_type"].ToString() == "1")
                    {
                        sso1 = chk_role + "," + cnt_no.Rows[0]["kod_akaun"].ToString();
                    }
                    else
                    {
                        sso1 = chk_role + "," + cnt_no.Rows[0]["under_jenis"].ToString();
                    }
                }

                string set_cnt_1 = string.Empty, set_cnt11 = string.Empty, mcnt1 = string.Empty;
                string sno11 = string.Empty, sno21 = string.Empty, sno31 = string.Empty, cnt_value1 = string.Empty;
                DataTable cnt_no11 = new DataTable();
                if (lbladdkodID1.SelectedValue != "")
                {

                    //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
                    cnt_no11 = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='" + lbladdkodID1.SelectedValue + "'");

                    set_cnt11 = cnt_no11.Rows[0]["cnt"].ToString();
                    mcnt1 = cnt_no11.Rows[0]["rcnt"].ToString();
                    set_cnt_1 = lbladdkodID1.SelectedValue;
                    //set_c_nt1 = "1";
                }
                else
                {
                    set_cnt11 = "0";
                    mcnt1 = "1";
                    set_cnt_1 = "00";
                }


                DataTable cnt_no12 = new DataTable();
                cnt_no12 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_akaun_cnt,'') +1) as mcnt From KW_Ref_Carta_Akaun where jenis_akaun='" + lbladdkodID1.SelectedValue + "' order by kod_akaun desc");

                if (cnt_no12.Rows.Count != 0)
                {
                    cnt_value1 = cnt_no12.Rows[0]["mcnt"].ToString();
                }
                else
                {
                    cnt_value = "1";
                }
                string chk_role1= string.Empty;
                string[] sp_no21 = set_cnt_1.Split('.');
                int sp_no11 = sp_no.Length;
                string ss11 = string.Empty;
                for (int i = 0; i <= sp_no11; i++)
                {
                    if (i == (Int32.Parse(mcnt1) - 1))
                    {
                        sno11 = cnt_value1.PadLeft(2, '0');
                    }
                    else
                    {
                        sno11 = sp_no21[i].ToString();
                    }
                    if (i < (sp_no11))
                    {
                        ss11 = ".";
                    }
                    else
                    {
                        ss11 = "";
                    }
                    chk_role1 += (sno1).PadLeft(2, '0') + "" + ss1;
                }

                string sso11 = string.Empty, sso21 = string.Empty, sso31= string.Empty;
                if (set_cnt11 == "0")
                {
                    sso11 = "";
                }
                else
                {
                    if (cnt_no11.Rows[0]["jenis_akaun_type"].ToString() == "1")
                    {
                        sso11 = chk_role + "," + cnt_no11.Rows[0]["kod_akaun"].ToString();
                    }
                    else
                    {
                        sso11 = chk_role + "," + cnt_no11.Rows[0]["under_jenis"].ToString();
                    }
                }




                DataTable dtcenter = new DataTable();
             
                dtcenter = Dblog.Ora_Execute_table("select * from " + tn1 + " where " + tc2 + "='" + txtAddName.Text + "'");

                if (dtcenter.Rows.Count == 0)
                {

                    string Inssql = "INSERT INTO Ref_hr_Itegrasi(hr_ite_Maji_kod_akaun,hr_ite_Maji_jenis_akaun,hr_ite_Per_kod_akaun,hr_ite_Per_jenis_akaun,hr_ite_desc,Status,ite_crt_id,ite_crt_dt)VALUES('" + chk_role + "','" + lbladdkodID.SelectedValue + "','" + chk_role1 + "','" + lbladdkodID1.SelectedValue + "','" + txtAddName.Text + "','" + ddlStatus.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    BindData();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Simpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Nilai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                sel_val();
            }
        }
    }

    protected void gv_refdata_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //sel_val();
        TextBox Id = (TextBox)gv_refdata.Rows[e.RowIndex].FindControl("Id");
        TextBox lblEditName = (TextBox)gv_refdata.Rows[e.RowIndex].FindControl("lblEditName");
        TextBox txtEditcode = (TextBox)gv_refdata.Rows[e.RowIndex].FindControl("txtEditcode");
        DropDownList lbleditkodID = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbleditkodID");
        DropDownList lbleditkodID1 = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("lbleditkodID1");
        DropDownList editddlStatus = (DropDownList)gv_refdata.Rows[e.RowIndex].FindControl("editddlStatus");
        DataTable dtcenter = new DataTable();
        if (sel_rt.SelectedValue == "01")
        {
            dtcenter = Dblog.Ora_Execute_table("select * from Ref_hr_Itegrasi where hr_ite_desc='" + lblEditName.Text + "' AND Id != '" + Id.Text + "'");
            if (dtcenter.Rows.Count == 0)
            {
                string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
                string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
                DataTable cnt_no = new DataTable();
                if (lbleditkodID.SelectedValue != "")
                {

                    //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
                    cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='" + lbleditkodID.SelectedValue + "'");

                    set_cnt1 = cnt_no.Rows[0]["cnt"].ToString();
                    mcnt = cnt_no.Rows[0]["rcnt"].ToString();
                    set_cnt = lbleditkodID.SelectedValue;
                    //set_cnt1 = "1";
                }
                else
                {
                    set_cnt1 = "0";
                    mcnt = "1";
                    set_cnt = "00";
                }




                DataTable cnt_no1 = new DataTable();
                cnt_no1 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_akaun_cnt,'') +1) as mcnt From KW_Ref_Carta_Akaun where jenis_akaun='" + lbleditkodID.SelectedValue + "' order by kod_akaun desc");

                if (cnt_no1.Rows.Count != 0)
                {
                    cnt_value = cnt_no1.Rows[0]["mcnt"].ToString();
                }
                else
                {
                    cnt_value = "1";
                }
                string chk_role = string.Empty;
                string[] sp_no = set_cnt.Split('.');
                int sp_no1 = sp_no.Length;
                string ss1 = string.Empty;
                for (int i = 0; i <= sp_no1; i++)
                {
                    if (i == (Int32.Parse(mcnt) - 1))
                    {
                        sno1 = cnt_value.PadLeft(2, '0');
                    }
                    else
                    {
                        sno1 = sp_no[i].ToString();
                    }
                    if (i < (sp_no1))
                    {
                        ss1 = ".";
                    }
                    else
                    {
                        ss1 = "";
                    }
                    chk_role += (sno1).PadLeft(2, '0') + "" + ss1;
                }

                string sso1 = string.Empty, sso2 = string.Empty, sso3 = string.Empty;
                if (set_cnt1 == "0")
                {
                    sso1 = "";
                }
                else
                {
                    if (cnt_no.Rows[0]["jenis_akaun_type"].ToString() == "1")
                    {
                        sso1 = chk_role + "," + cnt_no.Rows[0]["kod_akaun"].ToString();
                    }
                    else
                    {
                        sso1 = chk_role + "," + cnt_no.Rows[0]["under_jenis"].ToString();
                    }
                }



                string set_cnt_1 = string.Empty, set_cnt11 = string.Empty, mcnt1 = string.Empty;
                string sno11 = string.Empty, sno21 = string.Empty, sno31 = string.Empty, cnt_value1 = string.Empty;
                DataTable cnt_no11 = new DataTable();
                if (lbleditkodID1.SelectedValue != "")
                {

                    //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
                    cnt_no11 = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='" + lbleditkodID1.SelectedValue + "'");

                    set_cnt11 = cnt_no11.Rows[0]["cnt"].ToString();
                    mcnt1 = cnt_no11.Rows[0]["rcnt"].ToString();
                    set_cnt_1 = lbleditkodID1.SelectedValue;
                    //set_c_nt1 = "1";
                }
                else
                {
                    set_cnt11 = "0";
                    mcnt1 = "1";
                    set_cnt_1 = "00";
                }


                DataTable cnt_no12 = new DataTable();
                cnt_no12 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_akaun_cnt,'') +1) as mcnt From KW_Ref_Carta_Akaun where jenis_akaun='" + lbleditkodID1.SelectedValue + "' order by kod_akaun desc");

                if (cnt_no12.Rows.Count != 0)
                {
                    cnt_value1 = cnt_no12.Rows[0]["mcnt"].ToString();
                }
                else
                {
                    cnt_value = "1";
                }
                string chk_role1 = string.Empty;
                string[] sp_no21 = set_cnt_1.Split('.');
                int sp_no11 = sp_no.Length;
                string ss11 = string.Empty;
                for (int i = 0; i <= sp_no11; i++)
                {
                    if (i == (Int32.Parse(mcnt1) - 1))
                    {
                        sno11 = cnt_value1.PadLeft(2, '0');
                    }
                    else
                    {
                        sno11 = sp_no21[i].ToString();
                    }
                    if (i < (sp_no11))
                    {
                        ss11 = ".";
                    }
                    else
                    {
                        ss11 = "";
                    }
                    chk_role1 += (sno1).PadLeft(2, '0') + "" + ss1;
                }

                string sso11 = string.Empty, sso21 = string.Empty, sso31 = string.Empty;
                if (set_cnt11 == "0")
                {
                    sso11 = "";
                }
                else
                {
                    if (cnt_no11.Rows[0]["jenis_akaun_type"].ToString() == "1")
                    {
                        sso11 = chk_role + "," + cnt_no11.Rows[0]["kod_akaun"].ToString();
                    }
                    else
                    {
                        sso11 = chk_role + "," + cnt_no11.Rows[0]["under_jenis"].ToString();
                    }
                }



                string Inssql = "update Ref_hr_Itegrasi set hr_ite_desc='" + lblEditName.Text + "',Status='" + editddlStatus.SelectedValue + "',hr_ite_Maji_kod_akaun='" + chk_role + "',hr_ite_Maji_jenis_akaun='" + lbleditkodID.SelectedValue + "',hr_ite_Per_kod_akaun='" + chk_role1 + "',hr_ite_Per_jenis_akaun='" + lbleditkodID1.SelectedValue + "' where id='" + Id.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    gv_refdata.EditIndex = -1;
                    sel_val();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }
            
        }
        else if (sel_rt.SelectedValue == "02")
        {
            dtcenter = Dblog.Ora_Execute_table("select * from Ref_hr_jenis_elaun where hr_elau_desc='" + lblEditName.Text + "' AND Id != '" + Id.Text + "'");
            if (dtcenter.Rows.Count == 0)
            {
                string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
                string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
                DataTable cnt_no = new DataTable();
                if (lbleditkodID.SelectedValue != "")
                {

                    //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
                    cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='" + lbleditkodID.SelectedValue + "'");

                    set_cnt1 = cnt_no.Rows[0]["cnt"].ToString();
                    mcnt = cnt_no.Rows[0]["rcnt"].ToString();
                    set_cnt = lbleditkodID.SelectedValue;
                    //set_cnt1 = "1";
                }
                else
                {
                    set_cnt1 = "0";
                    mcnt = "1";
                    set_cnt = "00";
                }




                DataTable cnt_no1 = new DataTable();
                cnt_no1 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_akaun_cnt,'') +1) as mcnt From KW_Ref_Carta_Akaun where jenis_akaun='" + lbleditkodID.SelectedValue + "' order by kod_akaun desc");

                if (cnt_no1.Rows.Count != 0)
                {
                    cnt_value = cnt_no1.Rows[0]["mcnt"].ToString();
                }
                else
                {
                    cnt_value = "1";
                }
                string chk_role = string.Empty;
                string[] sp_no = set_cnt.Split('.');
                int sp_no1 = sp_no.Length;
                string ss1 = string.Empty;
                for (int i = 0; i <= sp_no1; i++)
                {
                    if (i == (Int32.Parse(mcnt) - 1))
                    {
                        sno1 = cnt_value.PadLeft(2, '0');
                    }
                    else
                    {
                        sno1 = sp_no[i].ToString();
                    }
                    if (i < (sp_no1))
                    {
                        ss1 = ".";
                    }
                    else
                    {
                        ss1 = "";
                    }
                    chk_role += (sno1).PadLeft(2, '0') + "" + ss1;
                }

                string sso1 = string.Empty, sso2 = string.Empty, sso3 = string.Empty;
                if (set_cnt1 == "0")
                {
                    sso1 = "";
                }
                else
                {
                    if (cnt_no.Rows[0]["jenis_akaun_type"].ToString() == "1")
                    {
                        sso1 = chk_role + "," + cnt_no.Rows[0]["kod_akaun"].ToString();
                    }
                    else
                    {
                        sso1 = chk_role + "," + cnt_no.Rows[0]["under_jenis"].ToString();
                    }
                }



                string set_cnt_1 = string.Empty, set_cnt11 = string.Empty, mcnt1 = string.Empty;
                string sno11 = string.Empty, sno21 = string.Empty, sno31 = string.Empty, cnt_value1 = string.Empty;
                DataTable cnt_no11 = new DataTable();
                if (lbleditkodID1.SelectedValue != "")
                {

                    //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
                    cnt_no11 = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='" + lbleditkodID1.SelectedValue + "'");

                    set_cnt11 = cnt_no11.Rows[0]["cnt"].ToString();
                    mcnt1 = cnt_no11.Rows[0]["rcnt"].ToString();
                    set_cnt_1 = lbleditkodID1.SelectedValue;
                    //set_c_nt1 = "1";
                }
                else
                {
                    set_cnt11 = "0";
                    mcnt1 = "1";
                    set_cnt_1 = "00";
                }


                DataTable cnt_no12 = new DataTable();
                cnt_no12 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_akaun_cnt,'') +1) as mcnt From KW_Ref_Carta_Akaun where jenis_akaun='" + lbleditkodID1.SelectedValue + "' order by kod_akaun desc");

                if (cnt_no12.Rows.Count != 0)
                {
                    cnt_value1 = cnt_no12.Rows[0]["mcnt"].ToString();
                }
                else
                {
                    cnt_value = "1";
                }
                string chk_role1 = string.Empty;
                string[] sp_no21 = set_cnt_1.Split('.');
                int sp_no11 = sp_no.Length;
                string ss11 = string.Empty;
                for (int i = 0; i <= sp_no11; i++)
                {
                    if (i == (Int32.Parse(mcnt1) - 1))
                    {
                        sno11 = cnt_value1.PadLeft(2, '0');
                    }
                    else
                    {
                        sno11 = sp_no21[i].ToString();
                    }
                    if (i < (sp_no11))
                    {
                        ss11 = ".";
                    }
                    else
                    {
                        ss11 = "";
                    }
                    chk_role1 += (sno1).PadLeft(2, '0') + "" + ss1;
                }

                string sso11 = string.Empty, sso21 = string.Empty, sso31 = string.Empty;
                if (set_cnt11 == "0")
                {
                    sso11 = "";
                }
                else
                {
                    if (cnt_no11.Rows[0]["jenis_akaun_type"].ToString() == "1")
                    {
                        sso11 = chk_role + "," + cnt_no11.Rows[0]["kod_akaun"].ToString();
                    }
                    else
                    {
                        sso11 = chk_role + "," + cnt_no11.Rows[0]["under_jenis"].ToString();
                    }
                }



                string Inssql = "update Ref_hr_jenis_elaun set hr_elau_desc='" + lblEditName.Text + "',Status='" + editddlStatus.SelectedValue + "',elau_kod_akaun='" + lbleditkodID.SelectedValue + "',elau_jenis_akaun='" + lbleditkodID1.SelectedValue + "' where id='" + Id.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    gv_refdata.EditIndex = -1;
                    sel_val();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }

        }
        else if (sel_rt.SelectedValue == "03")
        {
            dtcenter = Dblog.Ora_Execute_table("select * from Ref_hr_potongan where hr_poto_desc='" + lblEditName.Text + "' AND Id != '" + Id.Text + "'");
            if (dtcenter.Rows.Count == 0)
            {
                string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
                string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
                DataTable cnt_no = new DataTable();
                if (lbleditkodID.SelectedValue != "")
                {

                    //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
                    cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='" + lbleditkodID.SelectedValue + "'");

                    set_cnt1 = cnt_no.Rows[0]["cnt"].ToString();
                    mcnt = cnt_no.Rows[0]["rcnt"].ToString();
                    set_cnt = lbleditkodID.SelectedValue;
                    //set_cnt1 = "1";
                }
                else
                {
                    set_cnt1 = "0";
                    mcnt = "1";
                    set_cnt = "00";
                }




                DataTable cnt_no1 = new DataTable();
                cnt_no1 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_akaun_cnt,'') +1) as mcnt From KW_Ref_Carta_Akaun where jenis_akaun='" + lbleditkodID.SelectedValue + "' order by kod_akaun desc");

                if (cnt_no1.Rows.Count != 0)
                {
                    cnt_value = cnt_no1.Rows[0]["mcnt"].ToString();
                }
                else
                {
                    cnt_value = "1";
                }
                string chk_role = string.Empty;
                string[] sp_no = set_cnt.Split('.');
                int sp_no1 = sp_no.Length;
                string ss1 = string.Empty;
                for (int i = 0; i <= sp_no1; i++)
                {
                    if (i == (Int32.Parse(mcnt) - 1))
                    {
                        sno1 = cnt_value.PadLeft(2, '0');
                    }
                    else
                    {
                        sno1 = sp_no[i].ToString();
                    }
                    if (i < (sp_no1))
                    {
                        ss1 = ".";
                    }
                    else
                    {
                        ss1 = "";
                    }
                    chk_role += (sno1).PadLeft(2, '0') + "" + ss1;
                }

                string sso1 = string.Empty, sso2 = string.Empty, sso3 = string.Empty;
                if (set_cnt1 == "0")
                {
                    sso1 = "";
                }
                else
                {
                    if (cnt_no.Rows[0]["jenis_akaun_type"].ToString() == "1")
                    {
                        sso1 = chk_role + "," + cnt_no.Rows[0]["kod_akaun"].ToString();
                    }
                    else
                    {
                        sso1 = chk_role + "," + cnt_no.Rows[0]["under_jenis"].ToString();
                    }
                }



                string set_cnt_1 = string.Empty, set_cnt11 = string.Empty, mcnt1 = string.Empty;
                string sno11 = string.Empty, sno21 = string.Empty, sno31 = string.Empty, cnt_value1 = string.Empty;
                DataTable cnt_no11 = new DataTable();
                if (lbleditkodID1.SelectedValue != "")
                {

                    //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
                    cnt_no11 = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='" + lbleditkodID1.SelectedValue + "'");

                    set_cnt11 = cnt_no11.Rows[0]["cnt"].ToString();
                    mcnt1 = cnt_no11.Rows[0]["rcnt"].ToString();
                    set_cnt_1 = lbleditkodID1.SelectedValue;
                    //set_c_nt1 = "1";
                }
                else
                {
                    set_cnt11 = "0";
                    mcnt1 = "1";
                    set_cnt_1 = "00";
                }


                DataTable cnt_no12 = new DataTable();
                cnt_no12 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_akaun_cnt,'') +1) as mcnt From KW_Ref_Carta_Akaun where jenis_akaun='" + lbleditkodID1.SelectedValue + "' order by kod_akaun desc");

                if (cnt_no12.Rows.Count != 0)
                {
                    cnt_value1 = cnt_no12.Rows[0]["mcnt"].ToString();
                }
                else
                {
                    cnt_value = "1";
                }
                string chk_role1 = string.Empty;
                string[] sp_no21 = set_cnt_1.Split('.');
                int sp_no11 = sp_no.Length;
                string ss11 = string.Empty;
                for (int i = 0; i <= sp_no11; i++)
                {
                    if (i == (Int32.Parse(mcnt1) - 1))
                    {
                        sno11 = cnt_value1.PadLeft(2, '0');
                    }
                    else
                    {
                        sno11 = sp_no21[i].ToString();
                    }
                    if (i < (sp_no11))
                    {
                        ss11 = ".";
                    }
                    else
                    {
                        ss11 = "";
                    }
                    chk_role1 += (sno1).PadLeft(2, '0') + "" + ss1;
                }

                string sso11 = string.Empty, sso21 = string.Empty, sso31 = string.Empty;
                if (set_cnt11 == "0")
                {
                    sso11 = "";
                }
                else
                {
                    if (cnt_no11.Rows[0]["jenis_akaun_type"].ToString() == "1")
                    {
                        sso11 = chk_role + "," + cnt_no11.Rows[0]["kod_akaun"].ToString();
                    }
                    else
                    {
                        sso11 = chk_role + "," + cnt_no11.Rows[0]["under_jenis"].ToString();
                    }
                }



                string Inssql = "update Ref_hr_potongan set hr_poto_desc='" + lblEditName.Text + "',Status='" + editddlStatus.SelectedValue + "',elau_kod_akaun='" + lbleditkodID.SelectedValue + "',elau_jenis_akaun='" + lbleditkodID1.SelectedValue + "' where id='" + Id.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    gv_refdata.EditIndex = -1;
                    sel_val();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
            }

        }
        else
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Sudah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void gv_refdata_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_refdata.EditIndex = -1;
        sel_val();
    }

    protected void gv_refdata_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_refdata.EditIndex = e.NewEditIndex;
        sel_val();
    }
}