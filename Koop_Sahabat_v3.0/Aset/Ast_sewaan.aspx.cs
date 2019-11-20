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

public partial class Ast_sewaan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string qr1 = string.Empty;
    string cde = string.Empty, qry1 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;

                Negri();
                grid1();
                Btn_Simpan.Visible = true;
                Button2.Visible = false;
                TextBox15.Text = "0";
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                 
                }
                useid = Session["New"].ToString();
               
             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lb1 = (System.Web.UI.WebControls.Label)gvRow.FindControl("gd_4");
        System.Web.UI.WebControls.Label lb2 = (System.Web.UI.WebControls.Label)gvRow.FindControl("gd_2");

        DataTable ddokdicno1 = new DataTable();
        ddokdicno1 = DBCon.Ora_Execute_table("select * from ast_rental where Id ='" + lb1.Text + "'");
        if (ddokdicno1.Rows.Count != 0)
        {
            TextBox15.Text = "1";
            TextBox1.Text = ddokdicno1.Rows[0]["ren_comp_reg_no"].ToString().Trim();
            TextBox12.Text = ddokdicno1.Rows[0]["ren_comp_name"].ToString().Trim();
            TextBox2.Text = ddokdicno1.Rows[0]["ren_ctc_name"].ToString().Trim();
            TextBox3.Text = ddokdicno1.Rows[0]["ren_ctc_icno"].ToString().Trim();
            TextBox4.Text = ddokdicno1.Rows[0]["ren_ctc_phone_no"].ToString().Trim();
            txt_area.Value = ddokdicno1.Rows[0]["ren_ctc_address"].ToString().Trim();
            TextBox8.Text = double.Parse(ddokdicno1.Rows[0]["ren_rental_amt"].ToString()).ToString("C").Replace("$", "");
            if (Convert.ToDateTime(ddokdicno1.Rows[0]["ren_start_dt"]).ToString("dd/MM/yyyy").Trim() != "01/01/1900")
            {
                TextBox5.Text = Convert.ToDateTime(ddokdicno1.Rows[0]["ren_start_dt"]).ToString("dd/MM/yyyy").Trim();
            }
            else
            {
                TextBox5.Text = "";
            }
            if (Convert.ToDateTime(ddokdicno1.Rows[0]["ren_end_dt"]).ToString("dd/MM/yyyy").Trim() != "01/01/1900")
            {
                TextBox7.Text = Convert.ToDateTime(ddokdicno1.Rows[0]["ren_end_dt"]).ToString("dd/MM/yyyy").Trim();
            }
            else
            {
                TextBox7.Text = "";
            }
            TextBox6.Text = double.Parse(ddokdicno1.Rows[0]["ren_deposit_amt"].ToString()).ToString("C").Replace("$", "");
            TextBox9.Text = double.Parse(ddokdicno1.Rows[0]["ren_deposit_utility_amt"].ToString()).ToString("C").Replace("$", "");
            TextBox11.Text = ddokdicno1.Rows[0]["ren_level"].ToString().Trim();
            TextBox10.Text = ddokdicno1.Rows[0]["ren_rental_lot"].ToString().Trim();
            TextBox14.Text = lb2.Text;
            lbl_name.Text = lb1.Text;
            TextBox1.Attributes.Add("readonly", "readonly");
            TextBox10.Attributes.Add("readonly", "readonly");
            Btn_Simpan.Visible = false;
            Button2.Visible = true;
        }
        else
        {
            grid1();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }

    void view_details()
    {
        //try
        //{
        //    Button3.Visible = false;
        //    DataTable ddokdicno1 = new DataTable();
        //    ddokdicno1 = DBCon.Ora_Execute_table("select * from ast_rental where Id='" + lbl_name.Text + "'");
        //    if (ddokdicno1.Rows.Count != 0)
        //    {
        //        TextBox15.Text = "1";
        //        TextBox1.Text = ddokdicno1.Rows[0]["ren_comp_reg_no"].ToString().Trim();
        //        TextBox12.Text = ddokdicno1.Rows[0]["ren_comp_name"].ToString().Trim();
        //        TextBox2.Text = ddokdicno1.Rows[0]["ren_ctc_name"].ToString().Trim();
        //        TextBox3.Text = ddokdicno1.Rows[0]["ren_ctc_icno"].ToString().Trim();
        //        TextBox4.Text = ddokdicno1.Rows[0]["ren_ctc_phone_no"].ToString().Trim();
        //        txt_area.Value = ddokdicno1.Rows[0]["ren_ctc_address"].ToString().Trim();
        //        TextBox8.Text = double.Parse(ddokdicno1.Rows[0]["ren_rental_amt"].ToString()).ToString("C").Replace("$", "");
        //        if (Convert.ToDateTime(ddokdicno1.Rows[0]["ren_start_dt"]).ToString("dd/MM/yyyy").Trim() != "01/01/1900")
        //        {
        //            TextBox5.Text = Convert.ToDateTime(ddokdicno1.Rows[0]["ren_start_dt"]).ToString("dd/MM/yyyy").Trim();
        //        }
        //        else
        //        {
        //            TextBox5.Text = "";
        //        }
        //        if (Convert.ToDateTime(ddokdicno1.Rows[0]["ren_end_dt"]).ToString("dd/MM/yyyy").Trim() != "01/01/1900")
        //        {
        //            TextBox7.Text = Convert.ToDateTime(ddokdicno1.Rows[0]["ren_end_dt"]).ToString("dd/MM/yyyy").Trim();
        //        }
        //        else
        //        {
        //            TextBox7.Text = "";
        //        }
        //        TextBox6.Text = double.Parse(ddokdicno1.Rows[0]["ren_deposit_amt"].ToString()).ToString("C").Replace("$", "");
        //        TextBox9.Text = double.Parse(ddokdicno1.Rows[0]["ren_deposit_utility_amt"].ToString()).ToString("C").Replace("$", "");
        //        TextBox11.Text = ddokdicno1.Rows[0]["ren_level"].ToString().Trim();
        //        TextBox10.Text = ddokdicno1.Rows[0]["ren_rental_lot"].ToString().Trim();
        //        TextBox14.Text = lbl_name.Text;
        //        TextBox1.Attributes.Add("readonly", "readonly");
        //        TextBox10.Attributes.Add("readonly", "readonly");
        //        Btn_Simpan.Visible = false;
        //        Button2.Visible = true;
        //    }
        //    else
        //    {
        //        grid1();
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
        //    }

        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }

    void Negri()
    {
        DataSet Ds = new DataSet();
        try
        {
            DataTable dd_negri = new DataTable();
            dd_negri = DBCon.Ora_Execute_table("SELECT STUFF ((SELECT ',' + pro_state_cd  FROM ast_property FOR XML PATH ('')  ),1,1,'')  as scode");

            string ss1 = string.Empty;
            if (dd_negri.Rows[0]["scode"].ToString() != "")
            {
                ss1 = dd_negri.Rows[0]["scode"].ToString();
            }
            else
            {
                ss1 = "0";
            }

            string com = "select hr_negeri_Code,UPPER(hr_negeri_desc) as hr_negeri_desc from Ref_hr_negeri where hr_negeri_Code IN (" + ss1 + ") and Status='A' group by hr_negeri_Code,hr_negeri_desc order by hr_negeri_desc ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ss_dd.DataSource = dt;
            ss_dd.DataTextField = "hr_negeri_desc";
            ss_dd.DataValueField = "hr_negeri_Code";
            ss_dd.DataBind();
            ss_dd.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   
    protected void Carian_Click(object sender, EventArgs e)
    {
        if (ss_dd.SelectedValue != "")
        {
            con.Open();
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select pro_state_cd From ast_property where pro_state_cd='" + ss_dd.SelectedValue + "' ");
            if (ddicno.Rows.Count > 0)
            {
                grid1();
            }
            else
            {
                grid1();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
            }
        }
        else
        {
            grid1();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila Pilih Negeri.');", true);
        }
    }

  
    protected void gv_refdata_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid1();
    }

    void grid1()
    {

        SqlCommand cmd = new SqlCommand("select distinct UPPER(hr_negeri_desc) hr_negeri_desc,pro_asset_id,UPPER(pro_district) pro_district,UPPER(pro_city) pro_city,ISNULL(UPPER(pro_address),'') pro_address,ISNULL(pro_ownership_no,'') pro_ownership_no,pro_lot_no,pro_pt from ast_property as P left join Ref_hr_negeri as N on N.hr_negeri_Code=P.pro_state_cd where pro_state_cd='" + ss_dd.SelectedValue + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            int columncount = gvSelected.Rows[0].Cells.Count;
            gvSelected.Rows[0].Cells.Clear();
            gvSelected.Rows[0].Cells.Add(new TableCell());
            gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
            gvSelected.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
        }
        con.Close();
    }

    //void grid2()
    //{
    //    con.Open();
    //    //DataTable ddicno = new DataTable();
    //    //ddicno = DBCon.Ora_Execute_table("select pro_state_cd From ast_property where pro_state_cd='" + ss_dd.SelectedValue + "' ");
    //    //string nopo = ddicno.Rows[0]["pro_state_cd"].ToString();
    //    if (TextBox13.Text != "" && ss_dd.SelectedValue != "")
    //    {
    //        qry1 = "select ren_comp_reg_no,ren_ownership_no,ren_state_cd,ren_rental_lot,UPPER(ren_ctc_address) ren_ctc_address,ren_level,ren_rental_lot,UPPER(ren_comp_name) ren_comp_name From ast_rental as R where ren_state_cd='" + ss_dd.SelectedValue + "' and ren_ownership_no='" + TextBox13.Text + "'";
    //    }
    //    else if (TextBox13.Text == "" && ss_dd.SelectedValue != "")
    //    {
    //        qry1 = "select ren_comp_reg_no,ren_ownership_no,ren_state_cd,ren_rental_lot,UPPER(ren_ctc_address) ren_ctc_address,ren_level,ren_rental_lot,UPPER(ren_comp_name) ren_comp_name From ast_rental as R where ren_state_cd='" + ss_dd.SelectedValue + "'";
    //    }
    //    else
    //    {
    //        qry1 = "select ren_comp_reg_no,ren_ownership_no,ren_state_cd,ren_rental_lot,UPPER(ren_ctc_address) ren_ctc_address,ren_level,ren_rental_lot,UPPER(ren_comp_name) ren_comp_name From ast_rental as R where ren_state_cd=''";
    //    }
    //    SqlCommand cmd = new SqlCommand("" + qry1 + "", con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count == 0)
    //    {
    //        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
    //        GridView1.DataSource = ds;
    //        GridView1.DataBind();
    //        int columncount = GridView1.Rows[0].Cells.Count;
    //        GridView1.Rows[0].Cells.Clear();
    //        GridView1.Rows[0].Cells.Add(new TableCell());
    //        GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
    //        GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
    //        //btn_hups.Visible = false;
    //    }
    //    else
    //    {
    //        GridView1.DataSource = ds;
    //        GridView1.DataBind();
    //    }
    //    con.Close();
    //}
    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void Btn_Simpan_Click(object sender, EventArgs e)
    {
        if (ss_dd.SelectedValue != "")
        {

            DataTable ddokdicno1 = new DataTable();
            ddokdicno1 = DBCon.Ora_Execute_table("select ren_state_cd,ren_ownership_no,ren_comp_reg_no,ren_comp_name,ren_ctc_name,ren_ctc_icno,ren_ctc_phone_no,ren_ctc_address,ren_rental_amt,ren_start_dt,ren_end_dt,ren_deposit_amt,ren_deposit_utility_amt,ren_level,ren_rental_lot from ast_rental where ren_ownership_no='" + TextBox14.Text + "' and ren_state_cd='" + ss_dd.SelectedValue + "' and ren_comp_reg_no='" + TextBox1.Text + "' and ren_rental_lot='" + TextBox10.Text + "'");

            if (TextBox15.Text == "0")
            {
                try
                {
                    string rcount = string.Empty;
                    int count = 0;
                    foreach (GridViewRow gvrow in gvSelected.Rows)
                    {
                        var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.RadioButton;
                        if (rb.Checked)
                        {
                            count++;
                        }
                        rcount = count.ToString();
                    }
                    if (rcount != "0")
                    {
                        if (TextBox1.Text != "")
                        {
                            if (TextBox10.Text != "")
                            {

                                SqlCommand ins_prof = new SqlCommand("insert into ast_rental (ren_ownership_no,ren_comp_reg_no,ren_comp_name,ren_ctc_name,ren_ctc_icno,ren_ctc_phone_no,ren_ctc_address,ren_state_cd,ren_rental_amt,ren_start_dt,ren_end_dt,ren_deposit_amt,ren_deposit_utility_amt,ren_level,ren_rental_lot ) values(@ren_ownership_no,@ren_comp_reg_no,@ren_comp_name,@ren_ctc_name,@ren_ctc_icno,@ren_ctc_phone_no,@ren_ctc_address,@ren_state_cd,@ren_rental_amt,@ren_start_dt,@ren_end_dt,@ren_deposit_amt,@ren_deposit_utility_amt,@ren_level,@ren_rental_lot)", con);

                                ins_prof.Parameters.AddWithValue("ren_ownership_no", TextBox13.Text);
                                ins_prof.Parameters.AddWithValue("ren_comp_reg_no", TextBox1.Text);
                                ins_prof.Parameters.AddWithValue("ren_comp_name", TextBox12.Text);
                                ins_prof.Parameters.AddWithValue("ren_ctc_name", TextBox2.Text);
                                ins_prof.Parameters.AddWithValue("ren_ctc_icno", TextBox3.Text);
                                ins_prof.Parameters.AddWithValue("ren_ctc_phone_no", TextBox4.Text);
                                ins_prof.Parameters.AddWithValue("ren_ctc_address", txt_area.Value);
                                ins_prof.Parameters.AddWithValue("ren_state_cd", ss_dd.SelectedValue);

                                //ins_prof.Parameters.AddWithValue("ren_ctc_phone_no", TextBox4.Text);
                                ins_prof.Parameters.AddWithValue("ren_rental_amt", TextBox8.Text);

                                string datedari = string.Empty, datedari1 = string.Empty, fromdate = string.Empty, todate = string.Empty;

                                if (TextBox5.Text != "")
                                {
                                    datedari = TextBox5.Text;
                                    DateTime dt = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    fromdate = dt.ToString("yyyy-MM-dd");
                                }
                                ins_prof.Parameters.AddWithValue("ren_start_dt", fromdate);
                                if (TextBox7.Text != "")
                                {
                                    datedari1 = TextBox7.Text;
                                    DateTime dt1 = DateTime.ParseExact(datedari1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    todate = dt1.ToString("yyyy-MM-dd");
                                }
                                ins_prof.Parameters.AddWithValue("ren_end_dt", todate);

                                ins_prof.Parameters.AddWithValue("ren_deposit_amt", TextBox6.Text);
                                ins_prof.Parameters.AddWithValue("ren_deposit_utility_amt", TextBox9.Text);
                                ins_prof.Parameters.AddWithValue("ren_level", TextBox11.Text);
                                ins_prof.Parameters.AddWithValue("ren_rental_lot", TextBox10.Text);
                                ins_prof.Parameters.AddWithValue("ren_crt_id", Session["New"].ToString());
                                ins_prof.Parameters.AddWithValue("ren_crt_dt", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                                con.Open();
                                int i = ins_prof.ExecuteNonQuery();
                                con.Close();
                                //Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                                //Session["validate_success"] = "SUCCESS";
                                //Response.Redirect("../Aset/Ast_sewaan_view.aspx");
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                                grid2();
                                clr_flds1();
                            }
                            else
                            {
                                grid1();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Lat Sewaan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                            }
                        }
                        else
                        {
                            grid1();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No Daftar Syarikat.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        grid1();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Issue.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    con.Close();
                }
            }
            else
            {
                try
                {
                    string datedari = string.Empty, datedari1 = string.Empty, fromdate = string.Empty, todate = string.Empty;
                    if (TextBox5.Text != "")
                    {
                        datedari = TextBox5.Text;
                        DateTime dt = DateTime.ParseExact(datedari, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        fromdate = dt.ToString("yyyy-MM-dd");
                    }

                    if (TextBox7.Text != "")
                    {
                        datedari1 = TextBox7.Text;
                        DateTime dt1 = DateTime.ParseExact(datedari1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        todate = dt1.ToString("yyyy-MM-dd");
                    }

                    
                    string Inssql = "update ast_rental set ren_comp_name='" + TextBox12.Text + "',ren_ctc_name='" + TextBox2.Text + "',ren_ctc_icno='" + TextBox3.Text + "',ren_ctc_phone_no='" + TextBox4.Text + "',ren_ctc_address='" + txt_area.Value.Replace("'", "''") + "',ren_rental_amt='" + TextBox8.Text + "',ren_start_dt='" + fromdate + "',ren_end_dt='" + todate + "',ren_deposit_amt='" + TextBox6.Text + "',ren_deposit_utility_amt='" + TextBox9.Text + "',ren_level='" + TextBox11.Text + "',ren_upd_id='" + Session["New"].ToString() + "',ren_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'  where Id='"+ lbl_name.Text +"'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        TextBox15.Text = "0";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        TextBox1.Attributes.Remove("readonly");
                        TextBox10.Attributes.Remove("readonly");
                        Btn_Simpan.Visible = true;
                        Button2.Visible = false;
                        clr_flds1();
                        //GridView1.Visible = false;
                        grid1();
                        grid2();

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Update Issue.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    con.Close();
                }
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
            }
        }
        else
        {
            grid1();

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Negeri.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }


    }

    void clr_flds1()
    {
        TextBox1.Text = "";
        TextBox12.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        txt_area.Value = "";
        TextBox8.Text = "";
        TextBox5.Text = "";
        TextBox7.Text = "";
        TextBox6.Text = "";
        TextBox9.Text = "";
        TextBox11.Text = "";
        TextBox10.Text = "";


    }

    void clr_flds2()
    {
        TextBox1.Text = "";
        TextBox12.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        txt_area.Value = "";
        TextBox8.Text = "";
        TextBox5.Text = "";
        TextBox7.Text = "";
        TextBox6.Text = "";
        TextBox9.Text = "";
        TextBox11.Text = "";
        TextBox10.Text = "";
        Btn_Simpan.Visible = true;
        Button2.Visible = false;
        TextBox1.Attributes.Remove("readonly");
        TextBox10.Attributes.Remove("readonly");
    }

    protected void rbName_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvSelected.Rows)
        {
            var rb = row.FindControl("RadioButton1") as System.Web.UI.WebControls.RadioButton;
            if (rb.Checked)
            {
                string varName1 = ((Label)row.FindControl("Label1")).Text.ToString();
                TextBox13.Text = varName1;
                clr_flds2();
                grid2();
            }
        }
    }

    void grid2()
    {
        con.Open();
        //DataTable ddicno = new DataTable();
        //ddicno = DBCon.Ora_Execute_table("select pro_state_cd From ast_property where pro_state_cd='" + ss_dd.SelectedValue + "' ");
        //string nopo = ddicno.Rows[0]["pro_state_cd"].ToString();
        if (TextBox13.Text != "" && ss_dd.SelectedValue != "")
        {
            qry1 = "select Id,ren_comp_reg_no,ren_ownership_no,ren_state_cd,ren_rental_lot,UPPER(ren_ctc_address) ren_ctc_address,ren_level,ren_rental_lot,UPPER(ren_comp_name) ren_comp_name From ast_rental as R where ren_state_cd='" + ss_dd.SelectedValue + "' and ren_ownership_no='" + TextBox13.Text + "'";
        }
        else if (TextBox13.Text == "" && ss_dd.SelectedValue != "")
        {
            qry1 = "select Id,ren_comp_reg_no,ren_ownership_no,ren_state_cd,ren_rental_lot,UPPER(ren_ctc_address) ren_ctc_address,ren_level,ren_rental_lot,UPPER(ren_comp_name) ren_comp_name From ast_rental as R where ren_state_cd='" + ss_dd.SelectedValue + "'";
        }
        else
        {
            qry1 = "select Id,ren_comp_reg_no,ren_ownership_no,ren_state_cd,ren_rental_lot,UPPER(ren_ctc_address) ren_ctc_address,ren_level,ren_rental_lot,UPPER(ren_comp_name) ren_comp_name From ast_rental as R where ren_state_cd=''";
        }
        SqlCommand cmd = new SqlCommand("" + qry1 + "", con);
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
            GridView1.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        con.Close();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_sewaan.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_sewaan_view.aspx");
    }

    
}