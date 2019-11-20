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
using System.Threading;


public partial class ppp_anggota : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string level, userid;
    string sqry = string.Empty;
    string fdate = string.Empty, tdate = string.Empty, fmdate = string.Empty, todate = string.Empty;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                grid1();
                //TextBox12.Attributes.Add("readonly", "readonly");
                //TextBox2.Attributes.Add("readonly", "readonly");
                TextBox3.Attributes.Add("readonly", "readonly");
                TextBox3.Text = "PS" + DateTime.Now.ToString("yyyyMMdd");
                string Payment_query = "SELECT * FROM Ref_Wilayah ORDER BY Wilayah_Name ASC";
                batchBind();
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(Payment_query))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                ListItem item = new ListItem();
                                item.Text = sdr["Wilayah_Name"].ToString();
                                item.Value = sdr["Wilayah_Code"].ToString();
                                //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                                ddl_wilayah.Items.Add(item);
                            }
                        }
                        con.Close();
                    }


                }
                ddl_wilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));
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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0131' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();
                //if (role_view == "1")
                //{
                //    Button2.Visible = true;
                //}
                //else
                //{
                //    Button2.Visible = false;
                //}
                

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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('103','1052','1069','99','64','65','66','13','23','102','14','15','104','61','883')");


            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;



            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            btnSearch.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }

    }


    void batchBind()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select set_eft_batch_name from mem_settlement where (set_eft_batch_name is not null) and (set_pay_verify_id is null) group by set_eft_batch_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddbat.DataSource = dt;
            ddbat.DataBind();
            ddbat.DataTextField = "set_eft_batch_name";
            ddbat.DataBind();
            ddbat.Items.Insert(0, new ListItem("--- PILIH ---", ""));

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
            //con.Open();
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
            ddl.SelectedValue = ((DataRowView)e.Row.DataItem)["set_bank_cd"].ToString();
            ddl.Items.Insert(0, new ListItem("--- PILIH ---", "0"));

            System.Web.UI.WebControls.TextBox Label1_txt = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("Label8");

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


    protected void BindGridview(object sender, EventArgs e)
    {
        //if (TextBox12.Text != "" && TextBox2.Text != "")
        if (ddbat.SelectedValue != "")
        {
            if (role_add == "1")
            {
                Button1.Visible = true;
            }
            else
            {
                Button1.Visible = false;
            }
            Button3.Visible = false;

            grid1();
        }
        else
        {
            grid1();            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    void sel_qry()
    {

        //if (TextBox12.Text != "" && TextBox2.Text != "")
        //{
        //    fdate = TextBox12.Text;
        //    DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
        //    fmdate = ft.ToString("yyyy-mm-dd");

        //    tdate = TextBox2.Text;
        //    DateTime tt = DateTime.ParseExact(tdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
        //    todate = tt.ToString("yyyy-mm-dd");
        //}
        SqlConnection con = new SqlConnection(cs);

        //SqlCommand cmd = new SqlCommand("select * from mem_settlement where set_txn_dt BETWEEN '" + TextBox12.Text + "' AND '" + TextBox2.Text + "'", con);
        if (ddbat.SelectedValue != "--- PILIH ---" && TextBox4.Text == "" && ddl_wilayah.Text == "")
        {
            sqry = "select ms.ID,mem_new_icno,mem_name,cawangan_name,mem_centre,set_bank_acc_no,set_bank_cd,CASE WHEN FORMAT(ms.set_pay_dt,'dd/MM/yyyy', 'en-us') = '01/01/1900' THEN '' ELSE FORMAT(ms.set_pay_dt,'dd/MM/yyyy', 'en-us') END set_pay_dt from mem_settlement AS ms inner join mem_member AS mm ON mm.mem_new_icno = ms.set_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan as rc ON rc.cawangan_code=mm.mem_branch_cd left join Ref_Nama_Bank AS rnb on rnb.Bank_Code=ms.set_bank_cd where ms.Acc_sts ='Y' and ms.set_eft_batch_name='" + ddbat.SelectedValue + "' and ms.set_apply_sts_ind='A' ORDER by rc.wilayah_name,rc.cawangan_name,mm.mem_centre";
        }
        else if (ddbat.SelectedValue != "--- PILIH ---" && TextBox4.Text != "" && ddl_wilayah.Text == "")
        {
            sqry = "select ms.ID,mem_new_icno,mem_name,cawangan_name,mem_centre,set_bank_acc_no,set_bank_cd,CASE WHEN FORMAT(ms.set_pay_dt,'dd/MM/yyyy', 'en-us') = '01/01/1900' THEN '' ELSE FORMAT(ms.set_pay_dt,'dd/MM/yyyy', 'en-us') END set_pay_dt from mem_settlement AS ms inner join mem_member AS mm ON mm.mem_new_icno = ms.set_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan as rc ON rc.cawangan_code=mm.mem_branch_cd  where ms.Acc_sts ='Y' AND ms.set_eft_batch_name='" + ddbat.SelectedValue + "' AND mm.mem_new_icno='" + TextBox4.Text + "' and ms.set_apply_sts_ind='A' ORDER by rc.wilayah_name,rc.cawangan_name,mm.mem_centre";
        }
        else if (ddbat.SelectedValue != "--- PILIH ---" && TextBox4.Text != "" && ddl_wilayah.Text != "")
        {
            sqry = "select ms.ID,mem_new_icno,mem_name,cawangan_name,mem_centre,set_bank_acc_no,set_bank_cd,CASE WHEN FORMAT(ms.set_pay_dt,'dd/MM/yyyy', 'en-us') = '01/01/1900' THEN '' ELSE FORMAT(ms.set_pay_dt,'dd/MM/yyyy', 'en-us') END set_pay_dt from mem_settlement AS ms inner join mem_member AS mm ON mm.mem_new_icno = ms.set_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan as rc ON rc.cawangan_code=mm.mem_branch_cd  where ms.Acc_sts ='Y' AND ms.set_eft_batch_name='" + ddbat.SelectedValue + "' AND mm.mem_new_icno='" + TextBox4.Text + "' AND mm.mem_region_cd='" + ddl_wilayah.Text + "' and ms.set_apply_sts_ind='A' ORDER by rc.wilayah_name,rc.cawangan_name,mm.mem_centre";
        }
        else if (ddbat.SelectedValue != "--- PILIH ---" && TextBox4.Text == "" && ddl_wilayah.Text != "")
        {
            sqry = "select ms.ID,mem_new_icno,mem_name,cawangan_name,mem_centre,set_bank_acc_no,set_bank_cd,CASE WHEN FORMAT(ms.set_pay_dt,'dd/MM/yyyy', 'en-us') = '01/01/1900' THEN '' ELSE FORMAT(ms.set_pay_dt,'dd/MM/yyyy', 'en-us') END set_pay_dt from mem_settlement AS ms inner join mem_member AS mm ON mm.mem_new_icno = ms.set_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan as rc ON rc.cawangan_code=mm.mem_branch_cd  where ms.Acc_sts ='Y' AND ms.set_eft_batch_name='" + ddbat.SelectedValue + "' AND mm.mem_region_cd='" + ddl_wilayah.Text + "' and ms.set_apply_sts_ind='A' ORDER by rc.wilayah_name,rc.cawangan_name,mm.mem_centre";
        }
     
        else
        {
            sqry = "select ms.ID,mem_new_icno,mem_name,cawangan_name,mem_centre,set_bank_acc_no,set_bank_cd,CASE WHEN FORMAT(ms.set_pay_dt,'dd/MM/yyyy', 'en-us') = '01/01/1900' THEN '' ELSE FORMAT(ms.set_pay_dt,'dd/MM/yyyy', 'en-us') END set_pay_dt from mem_settlement AS ms inner join mem_member AS mm ON mm.mem_new_icno = ms.set_new_icno and mm.Acc_sts ='Y' left join Ref_Cawangan as rc ON rc.cawangan_code=mm.mem_branch_cd  where ms.Acc_sts ='Y' and  ms.set_batch_name='' AND mm.mem_region_cd='' and ms.set_apply_sts_ind='A' ORDER by rc.wilayah_name,rc.cawangan_name,mm.mem_centre";
        }
    }



    void grid1()
    {
        sel_qry();
        con.Open();
        SqlCommand cmd = new SqlCommand("" + sqry + "", con);
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
            gvSelected.Rows[0].Cells[0].Text = "<strong><center>Maklumat Carian Tidak Dijumpai</center></strong>";
            Button1.Visible = false;
        }
        else
        {
            if (role_add == "1")
            {
                Button1.Visible = true;
            }
            else
            {
                Button1.Visible = false;
            }
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
        }
        con.Close();
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid1();
        //BindGridview();
    }


    protected void Save(object sender, EventArgs e)
    {

        using (SqlConnection con = new SqlConnection(cs))
        {

            //cmd.CommandText = "UPDATE mem_settlement SET [set_apply_sts_ind] = @set_apply_sts_ind, [set_pay_verify_id] = @set_pay_verify_id, [set_pay_verify_dt] = @set_pay_verify_dt  WHERE Id=@Id";
            // cmd.Connection = con;
            string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                var checkbox = gvrow.FindControl("chkSelect") as CheckBox;
                if (checkbox.Checked)
                {

                    var lblID = gvrow.FindControl("Label1") as Label;
                    //var bno = gvrow.FindControl("Label6") as TextBox;
                    //var bcd = gvrow.FindControl("Label7") as TextBox;
                    //var paydt = gvrow.FindControl("Label8") as TextBox;
                    TextBox box1 = (TextBox)gvrow.FindControl("Label6");
                    TextBox box2 = (TextBox)gvrow.FindControl("Label7");
                    TextBox box3 = (TextBox)gvrow.FindControl("Label8");

                    string userid = Session["New"].ToString();
                    DropDownList ddl = (DropDownList)gvrow.FindControl("Bank_details");
                    string bdate = string.Empty, up_date = string.Empty;
                    if (box3.Text != "")
                    {
                        bdate = box3.Text;
                        DateTime bd = DateTime.ParseExact(bdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        up_date = bd.ToString("yyyy-MM-dd");
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE mem_settlement SET [set_apply_sts_ind] = @set_apply_sts_ind, [set_pay_verify_id] = @set_pay_verify_id, [set_pay_verify_dt] = @set_pay_verify_dt, [set_bank_acc_no] = @set_bank_acc_no, [set_bank_cd] = @set_bank_cd, [set_pay_dt] = @set_pay_dt WHERE ID='" + lblID.Text + "' and Acc_sts ='Y'", con);
                    cmd.Parameters.AddWithValue("set_apply_sts_ind", "V");
                    cmd.Parameters.AddWithValue("set_pay_verify_id", userid);
                    cmd.Parameters.AddWithValue("set_pay_verify_dt", strDate);
                    cmd.Parameters.AddWithValue("set_bank_acc_no", box1.Text);
                    cmd.Parameters.AddWithValue("set_bank_cd", ddl.SelectedValue);
                    // cmd.Parameters.AddWithValue("set_bank_cd", selectedvalue);
                    cmd.Parameters.AddWithValue("set_pay_dt", up_date);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    //refreshdata();

                }
            }
            grid1();
            service.audit_trail("P0131", ddbat.SelectedItem.Text, "Nama Kelompok Bayaran", TextBox3.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
    }



 
    protected void Reset_btn(object sender, EventArgs e)
    {

        Response.Redirect(Request.RawUrl);

    }

}
