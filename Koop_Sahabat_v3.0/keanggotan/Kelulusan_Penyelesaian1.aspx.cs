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


public partial class Kelulusan_Penyelesaian1 : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    DBConnection Con = new DBConnection();
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string userid;
    string Status = string.Empty;
    string uniqueId, uniqueId2, uniqueId3, unq_id1, unq_id2, unq_id3;
    string jurnal_qry = string.Empty;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                //TextBox1.Attributes.Add("readonly", "readonly");
                string Reason = "SELECT Application_name, Application_code FROM Ref_jenis_permohonan WHERE Status = 'A' order by Application_name";
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(Reason))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                Batch();
                                ListItem item = new ListItem();
                                item.Text = sdr["Application_name"].ToString();
                                item.Value = sdr["Application_code"].ToString();
                                //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                                janaan.Items.Add(item);
                            }
                        }
                        con.Close();
                    }
                }
                janaan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

                grid();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }

    }


    void Batch()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(cs);
            string com = "select set_batch_name from mem_settlement where set_approve_sts_cd is null group by set_batch_name order by set_batch_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            adpt.SelectCommand.CommandTimeout = 180;
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "set_batch_name";
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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0129' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('98','1065','905','99','66','100','71','14','15','1066','61','883')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }

    }

    protected void BindGridview(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text != "" && janaan.SelectedValue != "")
        {
            grid();
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Harus Mandatory.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void grid()
    {
        disp_hdr_txt.Visible = true;
        //Button3.Visible = true;
        //Button4.Visible = true;
        SqlConnection con = new SqlConnection(cs);
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from mem_settlement AS ms Left join mem_member AS mm ON ms.set_new_icno = mm.mem_new_icno and mm.Acc_sts ='Y' left join Ref_jenis_permohonan AS rs ON rs.Application_code='" + janaan.SelectedValue + "' Left Join Ref_Cawangan as rc on rc.cawangan_code = mm.mem_branch_cd where ms.Acc_sts ='Y' and ms.set_batch_name='" + DropDownList1.SelectedItem.Text + "' AND ms.set_appl_type_cd='" + janaan.SelectedValue + "' AND ms.set_apply_sts_ind = 'P'", con);
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
            gvSelected.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            Button3.Visible = false;
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
        }
        con.Close();
    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
        gvSelected.DataBind();
    }

    protected void submit_button(object sender, EventArgs e)
    {

        using (SqlConnection con = new SqlConnection(cs))
        {

            string strDate =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string datedari = TextBox1.Text;

            DateTime dt = DateTime.ParseExact(datedari, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datetime = dt.ToString("yyyy-mm-dd");
            foreach (GridViewRow gvrow in gvSelected.Rows)
            {
                var lblID = gvrow.FindControl("Label1") as Label;
                var icno = gvrow.FindControl("Label2") as Label;
                RadioButton chkbox = (RadioButton)gvrow.FindControl("chkSelect_1");
                RadioButton chkbox1 = (RadioButton)gvrow.FindControl("chkSelect_2");
                if (chkbox.Checked == true || chkbox1.Checked == true)
                {
                    //Store Share Information

                    var sha_new_icno = gvrow.FindControl("Label2") as Label;
                    var sha_credit_amt = gvrow.FindControl("Label7") as Label;
                    var sha_reference_no = gvrow.FindControl("Label8") as Label;
                    var sha_batch_name = gvrow.FindControl("Label9") as Label;
                    if (chkbox.Checked == true)
                    {
                        String text1 = Session["New"].ToString();
                        String text2 = "A";
                        SqlCommand up_status = new SqlCommand("UPDATE mem_settlement SET [set_apply_sts_ind] = @set_apply_sts_ind, [set_approve_sts_cd] = @set_approve_sts_cd, [set_appprove_dt] = @set_appprove_dt, [set_upd_id] = @set_upd_id, [set_upd_dt ] = @set_upd_dt  WHERE Id='" + lblID.Text + "' and Acc_sts ='Y'", con);
                        up_status.Parameters.AddWithValue("set_apply_sts_ind", text2);
                        if (chkbox.Checked == true)
                        {
                            String sta = "SA";
                            up_status.Parameters.AddWithValue("set_approve_sts_cd", sta);
                        }
                        else
                        {
                            String sta = "TS";
                            up_status.Parameters.AddWithValue("set_approve_sts_cd", sta);
                        }
                        up_status.Parameters.AddWithValue("set_appprove_dt", datetime);
                        up_status.Parameters.AddWithValue("set_upd_id", text1);
                        up_status.Parameters.AddWithValue("set_upd_dt", DateTime.Now);
                        con.Open();
                        int j = up_status.ExecuteNonQuery();
                        con.Close();

                        //update member table


                       
                        //String ins_text1 = "010305";
                        String ins_text2 = "PENYELESAIAN MODAL SYER";
                        String ins_text3 = userid;
                        SqlCommand ins_share = new SqlCommand("insert into mem_share (sha_new_icno,sha_txn_dt,sha_txn_ind,sha_debit_amt,sha_credit_amt,sha_reference_no,sha_item,sha_batch_name,sha_reference_ind,sha_apply_sts_ind,sha_approve_sts_cd,sha_approve_dt,sha_crt_id,sha_crt_dt,sha_balance_amt,sha_refund_ind,Acc_sts) values (@sha_new_icno,@sha_txn_dt,@sha_txn_ind,@sha_debit_amt,@sha_credit_amt,@sha_reference_no,@sha_item,@sha_batch_name,@sha_reference_ind,@sha_apply_sts_ind,@sha_approve_sts_cd,@sha_approve_dt,@sha_crt_id,@sha_crt_dt,@sha_balance_amt,@sha_refund_ind,@Acc_sts)", con);
                        ins_share.Parameters.AddWithValue("sha_new_icno", sha_new_icno.Text);
                        ins_share.Parameters.AddWithValue("sha_txn_dt", strDate);
                        ins_share.Parameters.AddWithValue("sha_txn_ind", 'J');
                        ins_share.Parameters.AddWithValue("sha_debit_amt", "");
                        ins_share.Parameters.AddWithValue("sha_credit_amt", sha_credit_amt.Text);
                        ins_share.Parameters.AddWithValue("sha_reference_no", sha_reference_no.Text);
                        ins_share.Parameters.AddWithValue("sha_item", janaan.SelectedItem.Text);
                        ins_share.Parameters.AddWithValue("sha_batch_name", sha_batch_name.Text);
                        ins_share.Parameters.AddWithValue("sha_reference_ind", 'C');
                        ins_share.Parameters.AddWithValue("sha_apply_sts_ind", 'A');
                        if (chkbox.Checked == true)
                        {
                            String sta = "SA";
                            ins_share.Parameters.AddWithValue("sha_approve_sts_cd", sta);
                        }
                        else
                        {
                            String sta = "TS";
                            ins_share.Parameters.AddWithValue("sha_approve_sts_cd", sta);
                        }
                        ins_share.Parameters.AddWithValue("sha_approve_dt", datetime);
                        ins_share.Parameters.AddWithValue("sha_crt_id", Session["New"].ToString());
                        ins_share.Parameters.AddWithValue("sha_crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        ins_share.Parameters.AddWithValue("sha_balance_amt", sha_credit_amt.Text);
                        ins_share.Parameters.AddWithValue("sha_refund_ind", "N");
                        ins_share.Parameters.AddWithValue("Acc_sts", "Y");
                        con.Open();
                        int k = ins_share.ExecuteNonQuery();
                        con.Close();

                        if (chkbox.Checked == true)
                        {
                            string Inssql = "Update mem_member SET mem_sts_cd='TS',mem_quit_dt='"+ datetime + "' WHERE mem_new_icno='" + icno.Text + "' and mem_sts_cd='DN'";
                            Status = Con.Ora_Execute_CommamdText(Inssql);
                        }
                        else
                        {
                            Con.Execute_CommamdText("Update mem_member SET mem_sts_cd='SA' WHERE mem_new_icno='" + icno.Text + "'and mem_sts_cd='DN'");
                        }
                    }
                    else if (chkbox1.Checked == true)
                    {

                        DataTable qry1 = new DataTable();
                        qry1 = DBCon.Ora_Execute_table("select * from mem_settlement where set_new_icno='" + icno.Text + "' and set_batch_name = '" + sha_batch_name.Text + "' and Acc_sts ='Y'");
                        if (qry1.Rows.Count != 0)
                        {

                            string Inssql_delete = "delete from mem_settlement where set_new_icno='" + icno.Text + "' and set_batch_name = '" + sha_batch_name.Text + "' and Acc_sts ='Y'";
                            Status = Con.Ora_Execute_CommamdText(Inssql_delete);
                            if (Status == "SUCCESS")
                            {
                                string Inssql_updt = "Update mem_member set mem_sts_cd='SA' where mem_new_icno='" + icno.Text + "' and Acc_sts ='Y'";
                                Status = Con.Ora_Execute_CommamdText(Inssql_updt);                                
                                //grid();
                                //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Dihapuskan Berjaya.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                            }
                        }
                    }
                }
            }
            // Integration Part

            DataTable ddsha = new DataTable();
            ddsha = DBCon.Ora_Execute_table("select set_batch_name,Format(set_appprove_dt,'yyyy-MM-dd') app_date,count(set_new_icno) cnt,sum(cast(set_apply_amt as money)) syer_amt from mem_settlement WHERE set_batch_name='" + DropDownList1.SelectedValue + "' and Format(set_appprove_dt,'yyyy-MM-dd')='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and set_approve_sts_cd='SA' and Acc_sts ='Y' group by set_batch_name,Format(set_appprove_dt,'yyyy-MM-dd')");

            if (ddsha.Rows.Count != 0)
            {
                if (janaan.SelectedValue == "TD")
                {
                    jurnal_qry = "05";
                }
                else if (janaan.SelectedValue == "HN")
                {
                    jurnal_qry = "06";
                }
                else if (janaan.SelectedValue == "PM")
                {
                    jurnal_qry = "07";
                }
                userid = Session["New"].ToString();
                GetUniqueInv();

                //MODAL SYER

                DataTable get_inter_info_sh = DBCon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='PENYELESAIAN ANGGOTA' and jur_desc_cd='" + jurnal_qry + "'");

                string Inssql_sh = "Insert into KW_jurnal_inter (no_permohonan,no_Rujukan,tarikh_lulus,Terma,Jenis_permohonan,perkara,nama_pelanggan_code,Overall,Status,crt_id,cr_dt) "
                    + " Values ('" + unq_id2 + "','" + ddsha.Rows[0]["set_batch_name"].ToString() + "','" + ddsha.Rows[0]["app_date"].ToString() + "','30','14', "
                    + " '" + get_inter_info_sh.Rows[0]["jur_desc"].ToString() + "','" + get_inter_info_sh.Rows[0]["jur_module"].ToString() + "','" + ddsha.Rows[0]["syer_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_sh);

                string Inssql_sh_items = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt) "
                    + " Values ('" + unq_id2 + "','" + get_inter_info_sh.Rows[0]["jur_desc"].ToString() + "','" + ddsha.Rows[0]["cnt"].ToString() + "','" + ddsha.Rows[0]["syer_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                Status = DBCon.Ora_Execute_CommamdText(Inssql_sh_items);


                DataTable dt_upd_format2 = DBCon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + unq_id2 + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='14' and Status = 'A'");

            }
            grid();
            Batch();
            DropDownList1.SelectedValue = "";
            service.audit_trail("P0129", janaan.SelectedItem.Text, "Nama Kelompok", DropDownList1.SelectedItem.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }
    }

    private void GetUniqueInv()
    {


        // PENEBUSAN SYER

        DataTable dt1_ms = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='14' and Status='A'");
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
           

            DataTable get_inter_info_ms = DBCon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0007' and jur_item='PENYELESAIAN ANGGOTA' and jur_desc_cd='"+ jurnal_qry + "'");
            DataTable get_doc1_ms = new DataTable();
            get_doc1_ms = DBCon.Ora_Execute_table("select Ref_doc_descript as s1 from KW_Ref_Doc_kod where Ref_doc_code='14'");
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
        Response.Redirect("../keanggotan/Kelulusan_Penyelesaian1.aspx");

    }
}