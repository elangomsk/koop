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

public partial class Tambah_Syer_PST1 : System.Web.UI.Page
{
    DBConnection dbcon = new DBConnection();
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string level, userid;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty, fmdate = string.Empty, todate = string.Empty;
    
    String count_text;
    int incNumber = 1;
    int incNumber1 = 1;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {                
                userid = Session["New"].ToString();
                grid();
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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0210' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

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
                if (role_add == "1")
                {
                    Button1.Visible = true;
                }
                else
                {
                    Button1.Visible = false;
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('72','11','1045','70','64','73','65','14','15')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            s_update.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    protected void BindGridview(object sender, EventArgs e)
    {
        if (TextBox12.Text != "" && TextBox2.Text != "")
        {
            grid();
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void grid()
    {
        show_cnt1.Visible = true;
        //Button3.Visible = true;
        if (TextBox12.Text != "")
        {
            string fdate = TextBox12.Text;
            DateTime ft = DateTime.ParseExact(fdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            fmdate = ft.ToString("yyyy-mm-dd");
        }

        if (TextBox2.Text != "")
        {
            string tdate = TextBox2.Text;
            DateTime tt = DateTime.ParseExact(tdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            todate = tt.ToString("yyyy-mm-dd");
        }

        string chk_qry = string.Empty;
        if (s_update.Checked == false)
        {
            chk_qry = "select mm.mem_sts_cd,rc.cawangan_name,mm.mem_new_icno,mm.mem_centre,mm.mem_name,mm.mem_member_no,ap.pst_wp4_no,mm.mem_sahabat_no,ap.pst_post_dt,ap.Flag_set,ap.Flag,SUM(ap.pst_nett_amt) as pst_balance_amt from mem_member AS mm Left join aim_pst AS ap ON ap.pst_new_icno = mm.mem_new_icno and ap.Acc_sts ='Y' left join Ref_Cawangan AS rc ON rc.cawangan_code=mm.mem_branch_cd left join mem_fee as mf on mf.fee_new_icno=mm.mem_new_icno and mf.Acc_sts ='Y' where mm.mem_sts_cd IN ('SA','FM') and ap.pst_withdrawal_type_cd='WSYE' and mm.Acc_sts ='Y' and ap.pst_post_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + todate + "'), 0) AND ap.pst_nett_amt >= 130.00 and ISNULL(pst_txn_ind,'') = ''  and ap.Flag='1'  and ap.Acc_sts='Y' group by mm.mem_sts_cd,rc.cawangan_name,mm.mem_new_icno,mm.mem_centre,mm.mem_name,mm.mem_member_no,ap.pst_wp4_no,mm.mem_sahabat_no,ap.pst_post_dt,ap.Flag_set,ap.Flag order by Flag,pst_post_dt";
        }
        else
        {
            chk_qry = "select mm.mem_sts_cd,rc.cawangan_name,mm.mem_new_icno,mm.mem_centre,mm.mem_name,mm.mem_member_no,ap.pst_wp4_no,mm.mem_sahabat_no,ap.pst_post_dt,ap.Flag_set,ap.Flag,SUM(ap.pst_nett_amt) as pst_balance_amt from mem_member AS mm Left join aim_pst AS ap ON ap.pst_new_icno = mm.mem_new_icno and ap.Acc_sts ='Y' left join Ref_Cawangan AS rc ON rc.cawangan_code=mm.mem_branch_cd left join mem_fee as mf on mf.fee_new_icno=mm.mem_new_icno and mf.Acc_sts ='Y' where mm.mem_sts_cd IN ('SA','FM','') and ap.pst_withdrawal_type_cd='WSYE' and mm.Acc_sts ='Y' and ap.pst_post_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + todate + "'), 0) AND ap.pst_nett_amt >= 130.00  and ap.Flag IN ('1','0') and ap.Acc_sts='Y' group by mm.mem_sts_cd,rc.cawangan_name,mm.mem_new_icno,mm.mem_centre,mm.mem_name,mm.mem_member_no,ap.pst_wp4_no,mm.mem_sahabat_no,ap.pst_post_dt,ap.Flag_set,ap.Flag order by Flag,pst_post_dt";
        }

        SqlConnection con = new SqlConnection(conString);
        con.Open();
        //SqlCommand cmd = new SqlCommand("select * from aim_pst AS ap Left join mem_member AS mm ON ap.pst_new_icno = mm.mem_new_icno where  mm.mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + TextBox12.Text + "'), 0)   and  mm.mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + TextBox2.Text + "'), 0)  AND ap.pst_balance_amt > 130.00 ", con);
        SqlCommand cmd = new SqlCommand("" + chk_qry + "", con);
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
            gvSelected.Rows[0].Cells[0].Text = "<center><strong>Tiada Rekod Dijumpai</strong></center>";
            Button1.Visible = false;
        }
        else
        {
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            if (role_add == "1")
            {
                Button1.Visible = true;
            }
            else
            {
                Button1.Visible = false;
            }
            System.Threading.Thread.Sleep(1);
        }
        con.Close();
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
                    row.Cells[9].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        CheckBox chkAll = (gvSelected.HeaderRow.FindControl("chkAll") as CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in gvSelected.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[9].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
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

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
        //BindGridview();
    }


    protected void gvUserInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            System.Web.UI.WebControls.Label Label1_txt = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_id");
            System.Web.UI.WebControls.Label Label1_cd = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_cd");
            System.Web.UI.WebControls.CheckBox ck_box = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chkSelect");

            if (Label1_txt.Text == "0")
            {

                ck_box.Attributes.Add("style", "display:none;");
            }
            else
            {

                ck_box.Attributes.Remove("style");
            }
        }
    }

    protected void Save(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(conString))
            {

                //cmd.CommandText = "UPDATE mem_settlement SET [set_apply_sts_ind] = @set_apply_sts_ind, [set_pay_verify_id] = @set_pay_verify_id, [set_pay_verify_dt] = @set_pay_verify_dt  WHERE Id=@Id";
                // cmd.Connection = con;
                string strDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                String text3 = userid;
                string rcount = string.Empty;
                int count = 0;
                foreach (GridViewRow gvrow in gvSelected.Rows)
                {
                    var rb = gvrow.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
                    if (rb.Checked)
                    {
                        count++;
                    }
                    rcount = count.ToString();
                }
                if (rcount != "0")
                {
                    foreach (GridViewRow gvrow in gvSelected.Rows)
                    {
                        var checkbox = gvrow.FindControl("chkSelect") as CheckBox;
                        if (checkbox.Checked)
                        {
                            var lblID = gvrow.FindControl("Label2") as Label;
                            var B_amount = gvrow.FindControl("Label6") as Label;
                            var lblCity = gvrow.FindControl("Label10") as Label;
                            var pst_dt = gvrow.FindControl("pst_date") as Label;

                            DateTime dt = Convert.ToDateTime(pst_dt.Text);
                            string txtdt = dt.ToString("yyyy-MM-dd");

                            DataTable chk_pst_tbl = new DataTable();
                            chk_pst_tbl = dbcon.Ora_Execute_table("select * from aim_pst where pst_new_icno='" + lblID.Text + "' and ISNULL(pst_txn_ind,'') !='1' and Flag='1' and Acc_sts ='Y'");

                            if (chk_pst_tbl.Rows.Count != 0)
                            {
                                DataTable ddicno_sts = new DataTable();
                                ddicno_sts = dbcon.Ora_Execute_table("select mem_sts_cd from mem_member where mem_new_icno='" + lblID.Text + "' and Acc_sts ='Y'");
                                string rmark = string.Empty;
                                if (ddicno_sts.Rows[0]["mem_sts_cd"].ToString() == "FM")
                                {
                                    rmark = "ANGGOTA BAHARU";
                                    DataTable updt_mem = new DataTable();
                                    updt_mem = dbcon.Ora_Execute_table("UPDATE mem_member set mem_sts_cd='' where mem_new_icno='" + lblID.Text + "' and mem_sts_cd='FM' and Acc_sts ='Y'");

                                    DataTable ddicno_fee = new DataTable();
                                    ddicno_fee = dbcon.Ora_Execute_table("select fee_refund_ind from mem_fee where fee_new_icno='" + lblID.Text + "' and Acc_sts ='Y'");
                                    if (ddicno_fee.Rows.Count != 0)
                                    {
                                        DataTable updt_fee = new DataTable();
                                        updt_fee = dbcon.Ora_Execute_table("UPDATE mem_fee set fee_refund_ind='' where fee_new_icno='" + lblID.Text + "' and Acc_sts ='Y' and fee_refund_ind != ''");
                                    }
                                }
                                else
                                {
                                    rmark = "TAMBAHAN SYER";

                                }

                                //float amt1 = (float.Parse(B_amount.Text) - 30);

                                float amt1 = (float.Parse(B_amount.Text));

                                SqlCommand cmd = new SqlCommand("insert into mem_share (sha_new_icno,sha_txn_dt,sha_txn_ind,sha_debit_amt,sha_item,sha_reference_ind,sha_apply_sts_ind,sha_crt_id,sha_crt_dt,Acc_sts) values (@sha_new_icno,@sha_txn_dt,@sha_txn_ind,@sha_debit_amt,@sha_item,@sha_reference_ind,@sha_apply_sts_ind,@sha_crt_id,@sha_crt_dt,@Acc_sts)", con);
                                cmd.Parameters.AddWithValue("sha_new_icno", lblID.Text);
                                cmd.Parameters.AddWithValue("sha_txn_dt", txtdt);
                                cmd.Parameters.AddWithValue("sha_txn_ind", "B");
                                cmd.Parameters.AddWithValue("sha_debit_amt", amt1);
                                cmd.Parameters.AddWithValue("sha_item", rmark);
                                cmd.Parameters.AddWithValue("sha_reference_ind", "P");
                                cmd.Parameters.AddWithValue("sha_apply_sts_ind", "N");
                                cmd.Parameters.AddWithValue("sha_crt_id", Session["New"].ToString());
                                cmd.Parameters.AddWithValue("sha_crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                cmd.Parameters.AddWithValue("Acc_sts", "Y");
                                con.Open();
                                int i = cmd.ExecuteNonQuery();
                                con.Close();
                                DataTable fdt = new DataTable();
                                fdt = dbcon.Ora_Execute_table("update aim_pst set pst_txn_ind='1',Flag='0',Flag_set_remark='" + rmark + "',pst_upd_id='" + Session["New"].ToString() + "',pst_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where pst_withdrawal_type_cd='WSYE' and pst_new_icno='" + lblID.Text + "' and ISNULL(pst_txn_ind,'') !='1'  and Flag='1' and Acc_sts ='Y'");
                            }
                            //refreshdata();
                        }
                    }
                    service.audit_trail("P0210", "Simpan","Tarikh", TextBox12.Text + " to " + TextBox2.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success'});window.location ='../keanggotan/Tambah_Syer_PST1.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Issue.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }


    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect("../keanggotan/Tambah_Syer_PST1.aspx");
    }

}