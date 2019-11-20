using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Threading;


public partial class Pegesahan_Tambah_Syer1 : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection Con = new DBConnection();
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string level, userid;
    string Status = string.Empty;
    string uniqueId, uniqueId2, uniqueId3, unq_id1, unq_id2, unq_id3;
    string jurnal_qry = string.Empty;
    string ref_sts;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                batch();
                TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
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

            SqlConnection con = new SqlConnection(cs);
            string com = "select sha_batch_name from mem_share ms where ISNULL(ms.sha_approve_sts_cd,'') = '' and ms.Acc_sts ='Y' and sha_batch_name !='' and sha_item='TAMBAHAN SYER' group by sha_batch_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            adpt.SelectCommand.CommandTimeout = 180;
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "sha_batch_name";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('81','1052','1054','82','66','71','14','15','1055')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;


            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }


    }

    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0213' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

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

                //if (role_edit == "1")
                //{
                //    Button5.Visible = true;
                //}
                //else
                //{
                //    Button5.Visible = false;
                //}

            }
        }
    }
    protected void BindGridview(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text != "--- PILIH ---")
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

        //Button3.Visible = true;
        //Button4.Visible = true;
        disp_hdr_txt.Visible = true;
        //Button5.Visible = true;
      

            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("select mm.mem_name, mm.mem_new_icno, rc.cawangan_name, mm.mem_centre, mm.mem_staff_ind, ms.sha_reference_ind, sum(ms.sha_debit_amt) as sha_debit_amt,FORMAT(ms.sha_txn_dt,'yyyy-MM-dd', 'en-us') as sha_txn_dt from mem_share AS ms Left join mem_member AS mm ON ms.sha_new_icno = mm.mem_new_icno and mm.Acc_sts ='Y' Left Join Ref_Cawangan as rc on rc.cawangan_code = mm.mem_branch_cd where mm.mem_sts_cd = 'SA' and ms.sha_batch_name='" + DropDownList1.SelectedItem.Text + "' and ISNULL(ms.sha_approve_sts_cd,'') = '' and ms.Acc_sts ='Y' group by mm.mem_name, mm.mem_new_icno, rc.cawangan_name, mm.mem_centre, mm.mem_staff_ind, ms.sha_reference_ind,ms.sha_txn_dt", con);
            DataTable Check_status = new DataTable();
            Con.Execute_CommamdText("Update mem_share SET sha_approve_sts_cd='' WHERE sha_batch_name='" + DropDownList1.SelectedItem.Text + "' and Acc_sts ='Y' AND sha_approve_sts_cd is NULL");
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
                gvSelected.Rows[0].Cells[0].Text = "<center><strong>Rekod Tidak Dijumpai.</strong></center>";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                Button3.Visible = false;
                Button4.Visible = false;
                Button5.Visible = false;
            }
            else
            {
                gvSelected.DataSource = ds;
                gvSelected.DataBind();
            if (role_add == "1")
            {
                Button3.Visible = true;
            }
            else
            {
                Button3.Visible = false;
            }
            //Button4.Visible = true;
        }
            con.Close();
      
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
        //BindGridview();
    }

    protected void submit_button(object sender, EventArgs e)
    {
        String sta, sts;
        using (SqlConnection con = new SqlConnection(cs))
        {

            //string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            string datedari = TextBox1.Text;

            DateTime dt = DateTime.ParseExact(datedari, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datetime = dt.ToString("yyyy-mm-dd");
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                var no_kp = gvrow.FindControl("Label2") as Label;
                var reference_ind = gvrow.FindControl("Label7") as Label;
                var stscd = gvrow.FindControl("lb_stscd") as Label;
                RadioButton chkbox = (RadioButton)gvrow.FindControl("chkSelect_1");
                RadioButton chkbox1 = (RadioButton)gvrow.FindControl("chkSelect_2");
                if (chkbox.Checked == true || chkbox1.Checked == true)
                {

                    String text1 = "A";
                    String text2 = Session["New"].ToString();
                    if (chkbox.Checked == true)
                    {
                        sta = "SA";
                        sts = "N";

                        DataTable DTMEM = new DataTable();
                        DTMEM = DBCon.Ora_Execute_table("SELECT * FROM  aim_pst WHERE pst_withdrawal_type_cd='WSYE' and pst_new_icno= '" + no_kp.Text + "'  and Acc_sts ='Y' and Flag='0' and Flag_set='1'");
                        if (DTMEM.Rows.Count != 0)
                        {
                            DataTable dtpst = new DataTable();
                            dtpst = DBCon.Ora_Execute_table("Update aim_pst SET Flag_set='0',pst_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',pst_upd_id='" + Session["New"].ToString() + "' where pst_withdrawal_type_cd='WSYE' and pst_new_icno='" + no_kp.Text + "' and Acc_sts ='Y' and Flag_set='1' and Flag='0'");
                        }

                    }
                    else
                    {
                        sta = "";// modify 25_03_2019
                        sts = "";
                    }
                    DataTable dtupd1 = new DataTable();
                    dtupd1 = DBCon.Ora_Execute_table("update mem_share set sha_approve_sts_cd='" + sta + "',sha_apply_sts_ind='" + text1 + "',sha_approve_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + text2 + "',sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_refund_ind='" + sts + "'  where sha_new_icno='" + no_kp.Text + "' and Acc_sts ='Y' and sha_txn_dt = '" + stscd.Text + "'");

                }
            }

            // Integration Part
                      
            DataTable ddsha = new DataTable();
            ddsha = DBCon.Ora_Execute_table("select sha_batch_name,Format(sha_approve_dt,'yyyy-MM-dd') app_date,count(sha_new_icno) cnt,sum(cast(sha_debit_amt as money)) syer_amt from mem_share WHERE sha_batch_name='" + DropDownList1.SelectedValue + "' and Format(sha_approve_dt,'yyyy-MM-dd')='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and sha_approve_sts_cd='SA' and Acc_sts ='Y' group by sha_batch_name,Format(sha_approve_dt,'yyyy-MM-dd')");

            if (ddsha.Rows.Count != 0)
            {
                userid = Session["New"].ToString();
                GetUniqueInv();
              
                //MODAL SYER

                DataTable get_inter_info_sh = DBCon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='CARUMAN ANGGOTA' and jur_desc_cd='03'");

                string Inssql_sh = "Insert into KW_jurnal_inter (no_permohonan,no_Rujukan,tarikh_lulus,Terma,Jenis_permohonan,perkara,nama_pelanggan_code,Overall,Status,crt_id,cr_dt) "
                    + " Values ('" + unq_id2 + "','" + ddsha.Rows[0]["sha_batch_name"].ToString() + "','" + ddsha.Rows[0]["app_date"].ToString() + "','30','13', "
                    + " '" + get_inter_info_sh.Rows[0]["jur_desc"].ToString() + "','" + get_inter_info_sh.Rows[0]["jur_module"].ToString() + "','" + ddsha.Rows[0]["syer_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_sh);

                string Inssql_sh_items = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt) "
                    + " Values ('" + unq_id2 + "','" + get_inter_info_sh.Rows[0]["jur_desc"].ToString() + "','" + ddsha.Rows[0]["cnt"].ToString() + "','" + ddsha.Rows[0]["syer_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_sh_items);
                

                DataTable dt_upd_format2 = DBCon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + unq_id2 + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='13' and Status = 'A'");

            }
            grid();
            service.audit_trail("P0213", "Simpan","Nama Kelompok", DropDownList1.SelectedItem.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
    }

    private void GetUniqueInv()
    {
        

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
            DataTable get_inter_info_ms = DBCon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='CARUMAN ANGGOTA' and jur_desc_cd='03'");
            DataTable get_doc1_ms = new DataTable();
            get_doc1_ms = DBCon.Ora_Execute_table("select Ref_doc_descript as s1 from KW_Ref_Doc_kod where Ref_doc_code='13'");
            if (get_inter_info_ms.Rows.Count != 0)
            {
                DataTable dt_ms = DBCon.Ora_Execute_table("select ISNULL(max(SUBSTRING(no_permohonan,13,2000)),'0') from KW_jurnal_inter  where Jenis_permohonan='" + get_inter_info_ms.Rows[0]["jur_item"].ToString() + "' and perkara='" + get_inter_info_ms.Rows[0]["jur_desc_cd"].ToString() + "' and nama_pelanggan_code='" + get_inter_info_ms.Rows[0]["jur_module"].ToString() + "'");
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

        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../keanggotan/Pegesahan_Tambah_Syer1.aspx");

    }
}