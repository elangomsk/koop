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
using System.Threading;
using System.Collections;

public partial class Pengesahan_Anggota1 : System.Web.UI.Page
{
    DBConnection dbcon = new DBConnection();
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string level, userid;
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string uniqueId, uniqueId2, uniqueId3, unq_id1, unq_id2, unq_id3;
    string jurnal_qry = string.Empty;
    String count_text;
    int incNumber = 1;
    int incNumber1 = 1;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {

                userid = Session["New"].ToString();
                TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                batch();
                grid();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void batch()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select mem_batch_name from mem_member where mem_sts_cd='' and Acc_sts='Y'   group by mem_batch_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            adpt.SelectCommand.CommandTimeout = 180;
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "mem_batch_name";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0122' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

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
                    Button3.Visible = true;
                }
                else
                {
                    Button3.Visible = false;
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('69','70','66','71','14','15')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;
            
            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    protected void BindGridview(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text != "--- PILIH ---")
        {
            //show_cnt1.Visible = true;
            Session["chkditems"] = null;
            grid();
        }
        else
        {
            //show_cnt1.Visible = false; 
            Session["chkditems"] = null;
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Nama Kelompok.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
    protected void grid()
    {
        if (role_add == "1")
        {
            Button3.Visible = true;
        }
        else
        {
            Button3.Visible = false;
        }
        //Button4.Visible = true;
        SqlConnection con = new SqlConnection(conString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select distinct ROW_NUMBER() OVER (Order by mm.mem_new_icno) AS RowNumber,mm.mem_name,mm.mem_new_icno,rc.cawangan_name,mm.mem_centre,mm.mem_staff_ind,FORMAT(ms.sha_txn_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as sha_txn_dt,ms.sha_debit_amt,mf.fee_amount from mem_fee AS mf Left join mem_member AS mm ON mf.fee_new_icno = mm.mem_new_icno and mm.Acc_sts ='Y' Left join mem_share AS ms ON ms.sha_new_icno = mf.fee_new_icno and ms.Acc_sts ='Y' and ms.sha_batch_name=mf.fee_batch_name Left Join Ref_Cawangan as rc ON rc.cawangan_code = mm.mem_branch_cd where mf.fee_batch_name='" + DropDownList1.SelectedItem.Text  + "' AND mm.mem_sts_cd = '' and mf.Acc_sts ='Y'", con);
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
            gvSelected.Rows[0].Cells[0].Text = "<center><strong>Rekod Tidak Dijumpai. Sila Lakukan Semula Carian</strong></center>";
            Button3.Visible = false;
            //Button4.Visible = false;
            batch();
            Session["chkditems"] = null;
            
        }
        else
        {
            gvSelected.DataSource = ds;
            gvSelected.DataBind();
            System.Threading.Thread.Sleep(1);

        }
        con.Close();
    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        savechkdvls();
        gvSelected.PageIndex = e.NewPageIndex;
        //grid();
        this.grid();
        gvSelected.DataBind();
        chkdvaluesp();
    }

    private void chkdvaluesp()
    {

        ArrayList usercontent = (ArrayList)Session["chkditems"];

        if (usercontent != null && usercontent.Count > 0)
        {

            foreach (GridViewRow gvrow in gvSelected.Rows)
            {

                Int64 index = Convert.ToInt64(gvSelected.DataKeys[gvrow.RowIndex].Value);

                if (usercontent.Contains(index))
                {

                    System.Web.UI.WebControls.RadioButton myCheckBox1 = (System.Web.UI.WebControls.RadioButton)gvrow.FindControl("chkSelect_1");

                    System.Web.UI.WebControls.RadioButton myCheckBox2 = (System.Web.UI.WebControls.RadioButton)gvrow.FindControl("chkSelect_2");

                    myCheckBox1.Checked = false;

                    myCheckBox2.Checked = true;

                }

            }

        }

    }

    private void savechkdvls()
    {

        ArrayList usercontent = new ArrayList();

        Int64 index = -1;
        

        foreach (GridViewRow gvrow in gvSelected.Rows)
        {

            index = Convert.ToInt64(gvSelected.DataKeys[gvrow.RowIndex].Value);

            bool result = ((System.Web.UI.WebControls.RadioButton)gvrow.FindControl("chkSelect_1")).Checked;

            bool result1 = ((System.Web.UI.WebControls.RadioButton)gvrow.FindControl("chkSelect_2")).Checked;



            // Check in the Session

            if (Session["chkditems"] != null)

                usercontent = (ArrayList)Session["chkditems"];

            if (result)
            {

                if (!usercontent.Contains(index))

                    usercontent.Add(index);

            }

            else
            {
                usercontent.Remove(index);
            }

            if (result1)
            {

                if (!usercontent.Contains(index))

                    usercontent.Add(index);

            }

            else
            {
                usercontent.Remove(index);
            }
        }

        if (usercontent != null && usercontent.Count > 0)

            Session["chkditems"] = usercontent;

    }



protected void submit_button(object sender, EventArgs e)
    {

        using (SqlConnection con = new SqlConnection(conString))
        {

            //string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            string datedari = TextBox1.Text;

            DateTime dt = DateTime.ParseExact(datedari, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datetime = dt.ToString("yyyy-mm-dd");
            if (s_update.Checked == true)
            {                
                gvSelected.AllowPaging = false;
                savechkdvls();
                this.grid();
                gvSelected.DataBind();
                chkdvaluesp();
                int[] no = new int[gvSelected.Rows.Count];
                int i = 0;
                
                foreach (GridViewRow gvrow in gvSelected.Rows)
                    {
                        var no_kp = gvrow.FindControl("Label2") as System.Web.UI.WebControls.Label;
                        var txn_dt = gvrow.FindControl("Label1_dt") as System.Web.UI.WebControls.Label;
                        var caw = gvrow.FindControl("Label4") as System.Web.UI.WebControls.Label;
                        var pus = gvrow.FindControl("Label5") as System.Web.UI.WebControls.Label;
                        System.Web.UI.WebControls.RadioButton chkbox = (System.Web.UI.WebControls.RadioButton)gvrow.FindControl("chkSelect_1");
                        System.Web.UI.WebControls.RadioButton chkbox1 = (System.Web.UI.WebControls.RadioButton)gvrow.FindControl("chkSelect_2");
                    //if(chkbox1.Checked == true)
                    //{
                    //    chkbox.Checked = false;
                    //}
                        if (chkbox.Checked == true || chkbox1.Checked == true)
                        {

                            //Update Fee Status

                            string text1 = string.Empty, text2 = string.Empty;
                            if (chkbox.Checked == true)
                            {
                                text1 = "SA";
                                text2 = "N";
                            }
                            else
                            {
                                text1 = "";
                                text2 = "";
                            }
                            SqlCommand up_status = new SqlCommand("UPDATE mem_fee SET [fee_refund_ind]= @fee_refund_ind ,[fee_sts_cd] = @fee_sts_cd, [fee_approval_dt] = @fee_approval_dt WHERE fee_new_icno='" + no_kp.Text + "' and Acc_sts ='Y'", con);
                            up_status.Parameters.AddWithValue("fee_refund_ind", text2);
                            up_status.Parameters.AddWithValue("fee_sts_cd", text1);
                            up_status.Parameters.AddWithValue("fee_approval_dt", datetime);
                            con.Open();
                            int j = up_status.ExecuteNonQuery();
                            con.Close();

                            //Update share Status

                            SqlCommand SA_status = new SqlCommand("UPDATE mem_share SET [sha_refund_ind]=@sha_refund_ind,[sha_approve_sts_cd] = @sha_approve_sts_cd, [sha_approve_dt] = @sha_approve_dt WHERE sha_new_icno='" + no_kp.Text + "' and sha_txn_dt='" + txn_dt.Text + "' and Acc_sts ='Y'", con);
                            SA_status.Parameters.AddWithValue("sha_refund_ind", text2);
                            SA_status.Parameters.AddWithValue("sha_approve_sts_cd", text1);
                            SA_status.Parameters.AddWithValue("sha_approve_dt", datetime);
                            SA_status.Parameters.AddWithValue("sha_upd_id", Session["New"].ToString());
                            SA_status.Parameters.AddWithValue("sha_upd_dt", datetime);
                            con.Open();
                            int k = SA_status.ExecuteNonQuery();
                            con.Close();

                            //update pst table set flag '0'

                            if (chkbox1.Checked == true)
                            {
                                DataTable dtpst = new DataTable();
                                dtpst = DBCon.Ora_Execute_table("Update aim_pst SET Flag_set='0',pst_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',pst_upd_id='" + Session["New"].ToString() + "' where pst_withdrawal_type_cd='WSYE' and pst_new_icno='" + no_kp.Text + "' and Acc_sts ='Y' and Flag_set='1'");
                            }

                            //Update Membership Status & Membership No

                            var s_id = gvrow.FindControl("Label6") as System.Web.UI.WebControls.Label;
                            DataTable DTMEM = new DataTable();
                            DTMEM = DBCon.Ora_Execute_table("SELECT mem_applicant_type_cd,mem_name FROM  mem_member WHERE mem_staff_ind= '" + s_id.Text + "' and mem_new_icno='" + no_kp.Text + "' and Acc_sts ='Y'");

                            if (chkbox.Checked == true)
                            {
                                if (DTMEM.Rows[0][0].ToString() == "SH")
                                {
                                    DataTable dtgen = new DataTable();
                                    dtgen = DBCon.Ora_Execute_table("select max(mem_member_no) mem_member_no  from mem_member where mem_applicant_type_cd='SH'");
                                    if (dtgen.Rows[0][0].ToString() != "")
                                    {
                                        string nyNumber = dtgen.Rows[0][0].ToString();
                                        string mm = nyNumber.Substring(1, nyNumber.Length - 1);
                                        int number = Convert.ToInt32(mm) + 1;
                                        string value2 = "S" + (number.ToString()).PadLeft(6, '0');

                                        count_text = value2;
                                    }
                                    else
                                    {
                                        count_text = "S000001";
                                    }
                                }
                                else
                                {
                                    DataTable dtgen = new DataTable();
                                    dtgen = DBCon.Ora_Execute_table("select max(mem_member_no) mem_member_no  from mem_member where mem_applicant_type_cd in ('SA','BD','SK')");
                                    if (dtgen.Rows[0][0].ToString() != "")
                                    {
                                        string nyNumber = dtgen.Rows[0][0].ToString();
                                        string mm = nyNumber.Substring(1, nyNumber.Length - 1);
                                        int number = Convert.ToInt32(mm) + 1;
                                        string value2 = "K" + (number.ToString()).PadLeft(6, '0');
                                        count_text = value2;
                                    }
                                    else
                                    {
                                        count_text = "K000001";
                                    }
                                }
                            }
                            else if (chkbox1.Checked == true)
                            {
                                count_text = "";

                            }
                            else
                            {
                                object count = "";
                                count_text = count.ToString();
                            }


                            String mem_text1 = Session["New"].ToString();

                            SqlCommand up_mem = new SqlCommand("UPDATE mem_member SET [mem_member_no] = @mem_member_no, [mem_sts_cd] = @mem_sts_cd, [mem_register_dt] = @mem_register_dt, [mem_upd_id] = @mem_upd_id, [mem_upd_dt] = @mem_upd_dt WHERE mem_new_icno='" + no_kp.Text + "' and Acc_sts ='Y'", con);
                            up_mem.Parameters.AddWithValue("mem_member_no", count_text);
                            con.Close();
                            up_mem.Parameters.AddWithValue("mem_sts_cd", text1);
                            up_mem.Parameters.AddWithValue("mem_register_dt", datetime);
                            up_mem.Parameters.AddWithValue("mem_upd_id", mem_text1);
                            up_mem.Parameters.AddWithValue("mem_upd_dt", DateTime.Now);
                            con.Open();
                            int s = up_mem.ExecuteNonQuery();
                            con.Close();


                            // create member login

                            if (chkbox.Checked == true)
                            {
                                DataTable ddlvl2 = new DataTable();
                                ddlvl2 = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + no_kp.Text + "'");
                                if (ddlvl2.Rows.Count == 0)
                                {
                                    DataTable ddlvl2_role = new DataTable();
                                    ddlvl2_role = DBCon.Ora_Execute_table("select * from KK_PID_Kumpulan where KK_kumpulan_id='R0025'");

                                    string Inssql = "insert into KK_User_Login(KK_userid,KK_password,KK_username,KK_roles,KK_skrins,Status,KK_crt_id,KK_crt_dt) values ('" + no_kp.Text + "','12345','" + DTMEM.Rows[0]["mem_name"].ToString() + "','" + ddlvl2_role.Rows[0]["KK_kumpulan_id"].ToString() + "','" + ddlvl2_role.Rows[0]["KK_kumpulan_screen"].ToString() + "','A','" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    Status = DBCon.Ora_Execute_CommamdText(Inssql);

                                }
                              

                            }

                        }

                    }
                gvSelected.AllowPaging = true;
                
                //gvSelected.DataBind();
                //}
                //gvSelected.SetPageIndex(a);

            }
            else
            {
                    foreach (GridViewRow gvrow in gvSelected.Rows)
                    {
                        var no_kp = gvrow.FindControl("Label2") as System.Web.UI.WebControls.Label;
                        var txn_dt = gvrow.FindControl("Label1_dt") as System.Web.UI.WebControls.Label;
                        var caw = gvrow.FindControl("Label4") as System.Web.UI.WebControls.Label;
                        var pus = gvrow.FindControl("Label5") as System.Web.UI.WebControls.Label;
                        System.Web.UI.WebControls.RadioButton chkbox = (System.Web.UI.WebControls.RadioButton)gvrow.FindControl("chkSelect_1");
                        System.Web.UI.WebControls.RadioButton chkbox1 = (System.Web.UI.WebControls.RadioButton)gvrow.FindControl("chkSelect_2");
                        if (chkbox.Checked == true || chkbox1.Checked == true)
                        {

                            //Update Fee Status

                            string text1 = string.Empty, text2 = string.Empty;
                            if (chkbox.Checked == true)
                            {
                                text1 = "SA";
                                text2 = "N";
                            }
                            else
                            {
                                text1 = "";
                                text2 = "";
                            }
                            SqlCommand up_status = new SqlCommand("UPDATE mem_fee SET [fee_refund_ind]= @fee_refund_ind ,[fee_sts_cd] = @fee_sts_cd, [fee_approval_dt] = @fee_approval_dt WHERE fee_new_icno='" + no_kp.Text + "' and Acc_sts ='Y'", con);
                            up_status.Parameters.AddWithValue("fee_refund_ind", text2);
                            up_status.Parameters.AddWithValue("fee_sts_cd", text1);
                            up_status.Parameters.AddWithValue("fee_approval_dt", datetime);
                            con.Open();
                            int j = up_status.ExecuteNonQuery();
                            con.Close();

                            //Update share Status

                            SqlCommand SA_status = new SqlCommand("UPDATE mem_share SET [sha_refund_ind]=@sha_refund_ind,[sha_approve_sts_cd] = @sha_approve_sts_cd, [sha_approve_dt] = @sha_approve_dt WHERE sha_new_icno='" + no_kp.Text + "' and sha_txn_dt='" + txn_dt.Text + "' and Acc_sts ='Y'", con);
                            SA_status.Parameters.AddWithValue("sha_refund_ind", text2);
                            SA_status.Parameters.AddWithValue("sha_approve_sts_cd", text1);
                            SA_status.Parameters.AddWithValue("sha_approve_dt", datetime);
                            SA_status.Parameters.AddWithValue("sha_upd_id", Session["New"].ToString());
                            SA_status.Parameters.AddWithValue("sha_upd_dt", datetime);
                            con.Open();
                            int k = SA_status.ExecuteNonQuery();
                            con.Close();

                            //update pst table set flag '0'

                            if (chkbox1.Checked == true)
                            {
                                DataTable dtpst = new DataTable();
                                dtpst = DBCon.Ora_Execute_table("Update aim_pst SET Flag_set='0',pst_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',pst_upd_id='" + Session["New"].ToString() + "' where pst_withdrawal_type_cd='WSYE' and pst_new_icno='" + no_kp.Text + "' and Acc_sts ='Y' and Flag_set='1'");
                            }

                            //Update Membership Status & Membership No

                            var s_id = gvrow.FindControl("Label6") as System.Web.UI.WebControls.Label;
                            DataTable DTMEM = new DataTable();
                            DTMEM = DBCon.Ora_Execute_table("SELECT mem_applicant_type_cd,mem_name FROM  mem_member WHERE mem_staff_ind= '" + s_id.Text + "' and mem_new_icno='" + no_kp.Text + "' and Acc_sts ='Y'");

                            if (chkbox.Checked == true)
                            {
                                if (DTMEM.Rows[0][0].ToString() == "SH")
                                {
                                    DataTable dtgen = new DataTable();
                                    dtgen = DBCon.Ora_Execute_table("select max(mem_member_no) mem_member_no  from mem_member where mem_applicant_type_cd='SH'");
                                    if (dtgen.Rows[0][0].ToString() != "")
                                    {
                                        string nyNumber = dtgen.Rows[0][0].ToString();
                                        string mm = nyNumber.Substring(1, nyNumber.Length - 1);
                                        int number = Convert.ToInt32(mm) + 1;
                                        string value2 = "S" + (number.ToString()).PadLeft(6, '0');

                                        count_text = value2;
                                    }
                                    else
                                    {
                                        count_text = "S000001";
                                    }
                                }
                                else
                                {
                                    DataTable dtgen = new DataTable();
                                    dtgen = DBCon.Ora_Execute_table("select max(mem_member_no) mem_member_no  from mem_member where mem_applicant_type_cd in ('SA','BD','SK')");
                                    if (dtgen.Rows[0][0].ToString() != "")
                                    {
                                        string nyNumber = dtgen.Rows[0][0].ToString();
                                        string mm = nyNumber.Substring(1, nyNumber.Length - 1);
                                        int number = Convert.ToInt32(mm) + 1;
                                        string value2 = "K" + (number.ToString()).PadLeft(6, '0');
                                        count_text = value2;
                                    }
                                    else
                                    {
                                        count_text = "K000001";
                                    }
                                }
                            }
                            else if (chkbox1.Checked == true)
                            {
                                count_text = "";

                            }
                            else
                            {
                                object count = "";
                                count_text = count.ToString();
                            }


                            String mem_text1 = Session["New"].ToString();

                            SqlCommand up_mem = new SqlCommand("UPDATE mem_member SET [mem_member_no] = @mem_member_no, [mem_sts_cd] = @mem_sts_cd, [mem_register_dt] = @mem_register_dt, [mem_upd_id] = @mem_upd_id, [mem_upd_dt] = @mem_upd_dt WHERE mem_new_icno='" + no_kp.Text + "' and Acc_sts ='Y'", con);
                            up_mem.Parameters.AddWithValue("mem_member_no", count_text);
                            con.Close();
                            up_mem.Parameters.AddWithValue("mem_sts_cd", text1);
                            up_mem.Parameters.AddWithValue("mem_register_dt", datetime);
                            up_mem.Parameters.AddWithValue("mem_upd_id", mem_text1);
                            up_mem.Parameters.AddWithValue("mem_upd_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            con.Open();
                            int s = up_mem.ExecuteNonQuery();
                            con.Close();


                            // create member login

                            if (chkbox.Checked == true)
                            {
                                DataTable ddlvl2 = new DataTable();
                                ddlvl2 = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + no_kp.Text + "'");
                                if (ddlvl2.Rows.Count == 0)
                                {
                                    DataTable ddlvl2_role = new DataTable();
                                    ddlvl2_role = DBCon.Ora_Execute_table("select * from KK_PID_Kumpulan where KK_kumpulan_id='R0016'");

                                    string Inssql = "insert into KK_User_Login(KK_userid,KK_password,KK_username,KK_roles,KK_skrins,Status,KK_crt_id,KK_crt_dt,KK_user_type) values ('" + no_kp.Text + "','12345','" + DTMEM.Rows[0]["mem_name"].ToString() + "','" + ddlvl2_role.Rows[0]["KK_kumpulan_id"].ToString() + "','" + ddlvl2_role.Rows[0]["KK_kumpulan_screen"].ToString() + "','A','" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Y')";
                                    Status = DBCon.Ora_Execute_CommamdText(Inssql);

                                }
                             

                            }

                        }

                    }
                
            }

            // Integration Part

            
            DataTable ddmem = new DataTable();
            ddmem = DBCon.Ora_Execute_table("select mem_batch_name,Format(mem_upd_dt,'yyyy-MM-dd') mem_upd_dt,count(mem_new_icno) cnt,sum(cast(mem_fee_amount as money)) fi_amt from mem_member WHERE mem_batch_name='" + DropDownList1.SelectedValue + "' and mem_sts_cd='SA' and Format(mem_upd_dt,'yyyy-MM-dd')='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and Acc_sts ='Y' group by mem_batch_name,Format(mem_upd_dt,'yyyy-MM-dd')");
            DataTable ddsha = new DataTable();
            ddsha = DBCon.Ora_Execute_table("select sha_batch_name,count(sha_new_icno) cnt,sum(cast(sha_debit_amt as money)) syer_amt from mem_share WHERE sha_batch_name='" + DropDownList1.SelectedValue + "' and sha_approve_sts_cd='SA' and Acc_sts ='Y' group by sha_batch_name");

            if (ddmem.Rows.Count != 0)
            {
                userid = Session["New"].ToString();
                GetUniqueInv();
                //fi masuk

                DataTable get_inter_info_fm = dbcon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='CARUMAN ANGGOTA' and jur_desc_cd='01'");

                string Inssql_fm = "Insert into KW_jurnal_inter (no_permohonan,no_Rujukan,tarikh_lulus,Terma,Jenis_permohonan,perkara,nama_pelanggan_code,Overall,Status,crt_id,cr_dt) "
                    + " Values ('" + unq_id1 + "','" + ddmem.Rows[0]["mem_batch_name"].ToString() + "','" + ddmem.Rows[0]["mem_upd_dt"].ToString() + "','30','12', "
                    + " '" + get_inter_info_fm.Rows[0]["jur_desc"].ToString() + "','" + get_inter_info_fm.Rows[0]["jur_module"].ToString() + "','" + ddmem.Rows[0]["fi_amt"].ToString() + "','A','"+ Session["New"].ToString() +"','"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_fm);

                string Inssql_fm_items = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt) "
                    + " Values ('" + unq_id1 + "','" + get_inter_info_fm.Rows[0]["jur_desc"].ToString() + "','" + ddmem.Rows[0]["cnt"].ToString() + "','" + ddmem.Rows[0]["fi_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_fm_items);

                //MODAL SYER

                DataTable get_inter_info_sh = dbcon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='CARUMAN ANGGOTA' and jur_desc_cd='02'");

                string Inssql_sh = "Insert into KW_jurnal_inter (no_permohonan,no_Rujukan,tarikh_lulus,Terma,Jenis_permohonan,perkara,nama_pelanggan_code,Overall,Status,crt_id,cr_dt) "
                    + " Values ('" + unq_id2 + "','" + ddmem.Rows[0]["mem_batch_name"].ToString() + "','" + ddmem.Rows[0]["mem_upd_dt"].ToString() + "','30','13', "
                    + " '" + get_inter_info_sh.Rows[0]["jur_desc"].ToString() + "','" + get_inter_info_sh.Rows[0]["jur_module"].ToString() + "','" + ddsha.Rows[0]["syer_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_sh);

                string Inssql_sh_items = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt) "
                    + " Values ('" + unq_id2 + "','" + get_inter_info_sh.Rows[0]["jur_desc"].ToString() + "','" + ddsha.Rows[0]["cnt"].ToString() + "','" + ddsha.Rows[0]["syer_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_sh_items);

                DataTable dt_upd_format1 = dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + unq_id1 + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='12' and Status = 'A'");

                DataTable dt_upd_format2 = dbcon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + unq_id2 + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='13' and Status = 'A'");

            }
            
            savechkdvls();
            grid();
            chkdvaluesp();
            Session["chkditems"] = null;
            service.audit_trail("P0122", "Simpan","Nama Kelompok",DropDownList1.SelectedItem.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.Maklumat Login Pengguna Berjaya Diwujudkan.',{'type': 'confirmation','title': 'Success'});", true);
        }
    }

    private void GetUniqueInv()
    {

        // FI KEANGGOTAAN

        DataTable dt1 = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='12' and Status='A'");
        if (dt1.Rows.Count != 0)
        {
            if (dt1.Rows[0]["cfmt"].ToString() == "")
            {
                unq_id1 = dt1.Rows[0]["fmt"].ToString();
            }
            else
            {
                int seqno = Convert.ToInt32(dt1.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString(dt1.Rows[0]["lfrmt1"].ToString() + "0000");
                unq_id1 = uniqueId;
            }

        }
        else
        {
            DataTable get_inter_info = dbcon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='CARUMAN ANGGOTA' and jur_desc_cd='01'");
            DataTable get_doc1 = new DataTable();
            get_doc1 = dbcon.Ora_Execute_table("select Ref_doc_descript as s1 from KW_Ref_Doc_kod where Ref_doc_code='12'");
            if (get_inter_info.Rows.Count != 0)
            {
                DataTable dt = dbcon.Ora_Execute_table("select ISNULL(max(SUBSTRING(no_permohonan,13,2000)),'0') from KW_jurnal_inter  where Jenis_permohonan='"+ get_inter_info.Rows[0]["jur_item"].ToString() + "' and perkara='" + get_inter_info.Rows[0]["jur_desc_cd"].ToString() + "' and nama_pelanggan_code='" + get_inter_info.Rows[0]["jur_module"].ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());
                    int newNumber = seqno + 1;
                    uniqueId = newNumber.ToString(get_doc1.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                    unq_id1 = uniqueId;

                }
                else
                {
                    int newNumber = Convert.ToInt32(uniqueId) + 1;
                    uniqueId = newNumber.ToString(get_doc1.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                    unq_id1 = uniqueId;
                }
            }
        }


        // MODAL SYER

        DataTable dt1_ms = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='13' and Status='A'");
        if (dt1_ms.Rows.Count != 0)
        {
            if (dt1_ms.Rows[0]["cfmt"].ToString() == "")
            {
                unq_id2 = dt1_ms.Rows[0]["fmt"].ToString();
            }
            else
            {
                int seqno = Convert.ToInt32(dt1_ms.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId2 = newNumber.ToString(dt1_ms.Rows[0]["lfrmt1"].ToString() + "0000");
                unq_id2 = uniqueId2;
            }

        }
        else
        {
            DataTable get_inter_info_ms = dbcon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='CARUMAN ANGGOTA' and jur_desc_cd='02'");
            DataTable get_doc1_ms = new DataTable();
            get_doc1_ms = dbcon.Ora_Execute_table("select Ref_doc_descript as s1 from KW_Ref_Doc_kod where Ref_doc_code='13'");
            if (get_inter_info_ms.Rows.Count != 0)
            {
                DataTable dt_ms = dbcon.Ora_Execute_table("select ISNULL(max(SUBSTRING(no_permohonan,13,2000)),'0') from KW_jurnal_inter  where Jenis_permohonan='" + get_inter_info_ms.Rows[0]["jur_item"].ToString() + "' and perkara='" + get_inter_info_ms.Rows[0]["jur_desc_cd"].ToString() + "' and nama_pelanggan_code='" + get_inter_info_ms.Rows[0]["jur_module"].ToString() + "'");
                if (dt_ms.Rows.Count > 0)
                {
                    int seqno = Convert.ToInt32(dt_ms.Rows[0][0].ToString());
                    int newNumber = seqno + 1;
                    uniqueId2 = newNumber.ToString(get_doc1_ms.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                    unq_id2 = uniqueId2;

                }
                else
                {
                    int newNumber = Convert.ToInt32(uniqueId2) + 1;
                    uniqueId2 = newNumber.ToString(get_doc1_ms.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                    unq_id2 = uniqueId2;
                }
            }
        }


    }
    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect("../keanggotan/Pengesahan_Anggota1.aspx");
    }
}