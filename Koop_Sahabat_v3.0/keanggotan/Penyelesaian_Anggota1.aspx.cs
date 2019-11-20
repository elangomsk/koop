

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


public partial class Penyelesaian_Anggota1 : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection Con = new DBConnection();
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string level, userid;
    DBConnection dbcon = new DBConnection();
    string Status = string.Empty;
    string c1 = string.Empty, c2 = string.Empty;
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
                textbox12.Text = DateTime.Now.ToString("dd/MM/yyyy");
                grid();
                data_grid1();
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    textbox1.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch_rcd();
                    Button1.Visible = false;
                    Button4.Visible = false;
                    Button7.Visible = true;
                }
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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0127' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

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
        gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('83','1052','895','124','13','14','15','16','915','108','23','25','20','1056', '86', '87', '88', '89', '90', '91', '1057', '55', '93', '37', '1024', '1012', '27', '28', '29', '1037', '52', '94', '61', '1042', '77', '35')");

        CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
        TextInfo txtinfo = culinfo.TextInfo;


        ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[28][0].ToString().ToLower());
        ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[30][0].ToString().ToLower());
        ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
        ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[22][0].ToString().ToLower());
        ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
        Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[24][0].ToString().ToLower());
        Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
        ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[33][0].ToString().ToLower());
        ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
        ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
        ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
        ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
        ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[31][0].ToString().ToLower());
        ps_lbl15.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
        ps_lbl16.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
        ps_lbl17.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
        ps_lbl18.Text = txtinfo.ToTitleCase(gt_lng.Rows[26][0].ToString().ToLower());
        ps_lbl19.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
        ps_lbl20.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
        ps_lbl21.Text = txtinfo.ToTitleCase(gt_lng.Rows[32][0].ToString().ToLower());
        ps_lbl22.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
        ps_lbl23.Text = txtinfo.ToTitleCase(gt_lng.Rows[21][0].ToString().ToLower());
        ps_lbl24.Text = txtinfo.ToTitleCase(gt_lng.Rows[23][0].ToString().ToLower());
        ps_lbl25.Text = txtinfo.ToTitleCase(gt_lng.Rows[27][0].ToString().ToLower());
        ps_lbl26.Text = txtinfo.ToTitleCase(gt_lng.Rows[34][0].ToString().ToLower());
        ps_lbl27.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
        ps_lbl28.Text = txtinfo.ToTitleCase(gt_lng.Rows[25][0].ToString().ToLower());
        ps_lbl29.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
        ps_lbl30.Text = txtinfo.ToTitleCase(gt_lng.Rows[35][0].ToString().ToLower());
        ps_lbl31.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
        ps_lbl32.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
        Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
        Button6.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
        Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[29][0].ToString().ToLower());
        Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }

    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        if (textbox9.Text != "")
        {
            if (Convert.ToDecimal(textbox9.Text) >= Convert.ToDecimal(textbox14.Text))
            {

            }
            else
            {
                textbox14.Text = textbox9.Text;                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Jumlah Pengeluaran Dipohon Lebih Besar dari Syer, Sila Semak!',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }
        }
        grid();
    }

    void data_grid1()
    {
        string Payment_query = "SELECT Payment_Type, Payment_Code FROM Ref_Kaedah_Pembayaran WHERE Status = 'A' order by Payment_Type";
        string Application_query = "SELECT Application_name, Application_code FROM Ref_jenis_permohonan WHERE Status = 'A' order by Application_name";
        string Reason_query = "SELECT DESCRRIPTION, DESCRRIPTION_CODE FROM Ref_Sebab WHERE Status = 'A' order by DESCRRIPTION";
        string bank_det = "SELECT Bank_Name, Bank_Code FROM Ref_Nama_Bank WHERE Status = 'A' ORDER BY Bank_Name";
        string jenis_bay = "SELECT KETERANGAN, KETERANGAN_Code FROM Ref_Jenis_Bayaran WHERE Status = 'A' ORDER BY Id";
        negriBind1();
        hubungan();
        using (SqlConnection con = new SqlConnection(cs))
        {
            using (SqlCommand cmd1 = new SqlCommand(Payment_query))
            {
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd1.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ListItem item = new ListItem();
                        item.Text = sdr["Payment_Type"].ToString();
                        item.Value = sdr["Payment_Code"].ToString();
                        //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                        textbox13.Items.Add(item);

                    }
                }
                con.Close();
            }
            using (SqlCommand cmd2 = new SqlCommand(Application_query))
            {
                cmd2.CommandType = CommandType.Text;
                cmd2.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ListItem item = new ListItem();
                        item.Text = sdr["Application_name"].ToString();
                        item.Value = sdr["Application_code"].ToString();
                        //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                        textbox10.Items.Add(item);
                    }
                }
                con.Close();
            }

            using (SqlCommand cmd3 = new SqlCommand(Reason_query))
            {
                cmd3.CommandType = CommandType.Text;
                cmd3.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd3.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ListItem item = new ListItem();
                        item.Text = sdr["DESCRRIPTION"].ToString();
                        item.Value = sdr["DESCRRIPTION_CODE"].ToString();
                        //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                        textbox11.Items.Add(item);
                    }
                }
                con.Close();
            }
            using (SqlCommand cmd4 = new SqlCommand(bank_det))
            {
                cmd4.CommandType = CommandType.Text;
                cmd4.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd4.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ListItem item = new ListItem();
                        item.Text = sdr["Bank_Name"].ToString();
                        item.Value = sdr["Bank_Code"].ToString();
                        //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                        Bank_details.Items.Add(item);
                    }
                }
                con.Close();
            }

            using (SqlCommand cmd5 = new SqlCommand(jenis_bay))
            {
                cmd5.CommandType = CommandType.Text;
                cmd5.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd5.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ListItem item = new ListItem();
                        item.Text = sdr["KETERANGAN"].ToString();
                        item.Value = sdr["KETERANGAN_Code"].ToString();
                        //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                        DropDownList1.Items.Add(item);
                    }
                }
                con.Close();
            }
        }
        Bank_details.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        textbox13.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        textbox10.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        textbox11.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }
    void negriBind1()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string com = "select Decription,Decription_Code from Ref_Negeri WHERE Status = 'A'  group by Decription,Decription_Code order by Decription,Decription_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlnegri.DataSource = dt;
            ddlnegri.DataBind();
            ddlnegri.DataTextField = "Decription";
            ddlnegri.DataValueField = "Decription_Code";
            ddlnegri.DataBind();
            ddlnegri.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void hubungan()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(cs);
            string com = "select * from Ref_Hubungan WHERE Status = 'A'  order by Contact_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataBind();
            DropDownList2.DataTextField = "Contact_name";
            DropDownList2.DataValueField = "Contact_Code";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (textbox1.Text != "")
        {
            if (textbox10.SelectedValue != "" && textbox11.SelectedValue != "" && textbox13.SelectedValue != "" && DropDownList1.SelectedValue != "")
            {
                if (textbox14.Text != "")
                {

                    SqlConnection con = new SqlConnection(cs);
                    DateTime crdt = DateTime.ParseExact(textbox12.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string crdate = crdt.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("hh:mm:ss");
                    //string crdate = crdt.ToString("yyyy-MM-dd");
                    //string Check_data = "select set_new_icno FROM mem_settlement WHERE set_new_icno='" + textbox1.Text + "' and set_txn_dt='" + crdate + "'";
                    string Check_data = "select set_new_icno FROM mem_settlement WHERE set_new_icno='" + textbox1.Text + "' and Acc_sts ='Y' and ISNULL(set_approve_sts_cd,'')=''";
                    SqlCommand cmd_Check_data = new SqlCommand(Check_data, con);
                    con.Open();
                    string strDate = DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataReader dr_Check_data = cmd_Check_data.ExecuteReader();
                    if (dr_Check_data.Read() != true)
                    {
                        con.Close();
                        SqlCommand cmd = new SqlCommand("insert into mem_settlement(set_new_icno,set_txn_dt,set_appl_type_cd,set_reason_cd,set_pay_method_cd,set_pay_type_cd,set_apply_amt,set_bank_acc_no,set_bank_cd,set_wasi_icno,set_wasi_name,set_wasi_phone_no,set_wasi_address,set_apply_sts_ind,set_crt_id,set_crt_dt,set_negri,set_postcd,set_wasi_relation_cd,Acc_sts,set_member_no) values(@set_new_icno,@set_txn_dt,Upper(@set_appl_type_cd),Upper(@set_reason_cd),@set_pay_method_cd,@set_pay_type_cd,@set_apply_amt,@set_bank_acc_no,@set_bank_cd,@set_wasi_icno,Upper(@set_wasi_name),@set_wasi_phone_no,Upper(@set_wasi_address),@set_apply_sts_ind,@set_crt_id,@set_crt_dt,@set_negri,@set_postcd,@set_wasi_relation_cd,@Acc_sts,@set_member_no)", con);
                        Con.Execute_CommamdText("update mem_member set mem_bank_acc_no='" + textbox15.Text + "',mem_bank_cd='" + Bank_details.SelectedValue + "' where mem_new_icno='" + textbox1.Text + "'");
                        //Con.Execute_CommamdText("UPDATE mem_wasi SET was_icno='" + textbox17.Text + "',WAS_NAME='" + textbox18.Text + "',WAS_RELATION_CD='" + DropDownList2.SelectedValue + "',was_phone_no='" + textbox19.Text + "',was_address='" + textbox20.Value + "',was_negri='" + ddlnegri.SelectedValue + "',was_postcd='" + txtpostcd.Text + "' where WAS_NEW_ICNO='" + textbox1.Text + "' AND was_seqno='1'");
                        //Con.Execute_CommamdText("UPDATE mem_wasi SET was_icno='" + textbox17.Text + "',WAS_NAME='" + textbox18.Text + "',WAS_RELATION_CD='" + DropDownList2.SelectedValue + "',was_phone_no='" + textbox19.Text + "',was_address='" + textbox20.Value + "',was_negri='" + ddlnegri.SelectedValue + "',was_postcd='" + txtpostcd.Text + "' where WAS_NEW_ICNO='" + textbox1.Text + "' AND was_seqno='2'");

                        cmd.Parameters.AddWithValue("@set_new_icno", textbox1.Text);

                        cmd.Parameters.AddWithValue("@set_txn_dt", crdate);
                        cmd.Parameters.AddWithValue("@set_appl_type_cd", textbox10.Text);
                        cmd.Parameters.AddWithValue("@set_reason_cd", textbox11.Text);
                        cmd.Parameters.AddWithValue("@set_pay_method_cd", textbox13.Text);
                        cmd.Parameters.AddWithValue("@set_pay_type_cd", DropDownList1.SelectedValue);
                        cmd.Parameters.AddWithValue("@set_apply_amt", textbox14.Text);
                        cmd.Parameters.AddWithValue("@set_bank_acc_no", textbox15.Text);
                        cmd.Parameters.AddWithValue("@set_bank_cd", Bank_details.SelectedValue);
                        cmd.Parameters.AddWithValue("@set_wasi_icno", textbox17.Text);
                        cmd.Parameters.AddWithValue("@set_wasi_name", textbox18.Text);
                        cmd.Parameters.AddWithValue("@set_wasi_phone_no", textbox19.Text);
                        //string addrs = textbox20.Value.Replace("\r\n", "<br />");
                        cmd.Parameters.AddWithValue("@set_wasi_address", textbox20.Value);
                        cmd.Parameters.AddWithValue("@set_apply_sts_ind", textbox24.Text);
                        cmd.Parameters.AddWithValue("@set_crt_id", Session["New"].ToString());

                        cmd.Parameters.AddWithValue("@set_crt_dt", crdate);
                        cmd.Parameters.AddWithValue("@set_negri", ddlnegri.SelectedValue);
                        cmd.Parameters.AddWithValue("@set_postcd", txtpostcd.Text);
                        cmd.Parameters.AddWithValue("@set_wasi_relation_cd", DropDownList2.SelectedValue);
                        cmd.Parameters.AddWithValue("@Acc_sts", "Y");
                        cmd.Parameters.AddWithValue("@set_member_no", TextBox21.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        if (textbox10.SelectedValue == "TD" || textbox10.SelectedValue == "HN")
                        {
                            string Inssql = "Update mem_member SET mem_sts_cd='DN',mem_upd_id='" + Session["New"].ToString() + "',mem_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE mem_new_icno='" + textbox1.Text + "' and Acc_sts ='Y'";
                            Status = dbcon.Ora_Execute_CommamdText(Inssql);
                        }
                        clr_data();
                        grid();
                        service.audit_trail("P0127", "Simpan", "No KP Baru", textbox1.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    }
                    else
                    {
                        con.Close();
                        grid();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('ID Penggunna Telah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

                    }
                }
                else
                {
                    grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Pengeluaran Dipohon (RM).',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

                }
            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Diperlukan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No KP Baru.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }

    }

    protected void Searchbtn_Click(object sender, EventArgs e)
    {
        srch_rcd();
    }

    void srch_rcd()
    {
        SqlConnection conn = new SqlConnection(cs);
        if (textbox1.Text != "")
        {
            //string query = "select top 1 * from mem_member as mm left join mem_share as ms ON ms.sha_new_icno=mm.mem_new_icno Left join Ref_Cawangan as rc on rc.cawangan_code = mm.mem_branch_cd Left join Ref_Wilayah as rw on rw.Wilayah_Code = mm.mem_region_cd where mem_new_icno='" + textbox1.Text + "' order by sha_txn_dt desc ";
            string query = "select mm.mem_name,mm.mem_address,mm.mem_postcd,mm.mem_negri,case(mm.mem_phone_h) when 'NULL' then '' else mm.mem_phone_h END  as mem_phone_h,case(mm.mem_phone_o) when 'NULL' then '' else mm.mem_phone_o END  as mem_phone_o,case(mm.mem_phone_m) when 'NULL' then '' else mm.mem_phone_m END  as mem_phone_m,rc.cawangan_name,rc.Wilayah_Name,case when ISNULL(mm.mem_centre,'') = 'NULL' then '' else ISNULL(mm.mem_centre,'') end mem_centre,mm.mem_member_no,mm.mem_bank_acc_no,mm.mem_bank_cd, CASE WHEN FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') = '01/01/1900' THEN '' ELSE FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') END mem_register_dt,isnull(sum(ms.sha_debit_amt),'0.00') as sha_debit_amt,isnull(sum(ms.sha_credit_amt),'0.00') as sha_credit_amt,mem_sts_cd from mem_member as mm left join mem_share as ms ON ms.sha_new_icno=mm.mem_new_icno and ms.Acc_sts ='Y' and ms.sha_approve_sts_cd='SA' Left join Ref_Cawangan as rc on rc.cawangan_code = 	mm.mem_branch_cd where mm.mem_new_icno='" + textbox1.Text + "' and mm.Acc_sts ='Y' and mm.mem_sts_cd IN ('SA','DN','TS')  group by mm.mem_name,mm.mem_address,mm.mem_postcd,mm.mem_negri,mm.mem_phone_h,mm.mem_phone_o,mm.mem_phone_m,rc.cawangan_name,rc.Wilayah_Name,mm.mem_centre,mm.mem_member_no,mm.mem_bank_acc_no,mm.mem_bank_cd,mm.mem_register_dt,mem_sts_cd "; //and ms.sha_refund_ind='N' REMOBED FROM CONDITION
            conn.Open();
            var sqlCommand = new SqlCommand(query, conn);
            var sqlReader = sqlCommand.ExecuteReader();

            DataTable Check_status = new DataTable();
            Con.Execute_CommamdText("Update mem_share SET sha_credit_amt='0.00' WHERE sha_new_icno='" + textbox1.Text + "' and Acc_sts ='Y' AND sha_credit_amt is NULL or sha_credit_amt=''");

            //SqlCommand cmd_Check_data = new SqlCommand(query, conn);
            // SqlDataReader dr_Check_data = cmd_Check_data.ExecuteReader();
            if (sqlReader.Read() == true)
            {

                textbox2.Text = (string)sqlReader["mem_name"].ToString();

                if (sqlReader["mem_phone_h"].ToString() == "")
                {
                    textbox3.Text = (string)sqlReader["mem_phone_o"].ToString();
                    if (sqlReader["mem_phone_o"].ToString() == "")
                    {
                        textbox3.Text = (string)sqlReader["mem_phone_m"].ToString();
                    }
                }
                else
                {
                    textbox3.Text = (string)sqlReader["mem_phone_h"].ToString();
                }
                textbox4.Text = (string)sqlReader["cawangan_name"].ToString();
                textbox5.Text = (string)sqlReader["Wilayah_Name"].ToString();
                textbox6.Text = (string)sqlReader["mem_centre"].ToString();
                textbox7.Text = (string)sqlReader["mem_member_no"].ToString();
                textbox15.Text = (string)sqlReader["mem_bank_acc_no"].ToString();
                Bank_details.Enabled = true;
                Bank_details.SelectedValue = (string)sqlReader["mem_bank_cd"].ToString();
                textbox8.Text = sqlReader["mem_register_dt"].ToString();
                TextBox21.Text = sqlReader["mem_member_no"].ToString();

                string t1 = sqlReader["sha_debit_amt"].ToString();

                string credit = sqlReader["sha_credit_amt"].ToString();
                double tot = (double.Parse(t1) - double.Parse(credit));
                textbox9.Text = tot.ToString("C").Replace("$", "").Replace("RM", "");

                if(sqlReader["mem_sts_cd"].ToString() == "TS")
                {
                    Button3.Visible = false;
                }
                else
                {
                    Button3.Visible = true;
                }

                //wasi

                textbox17.Text = textbox1.Text;
                textbox18.Text = sqlReader["mem_name"].ToString();
                textbox19.Text = textbox3.Text;
                textbox20.Value = sqlReader["mem_address"].ToString();
                txtpostcd.Text = sqlReader["mem_postcd"].ToString();
                ddlnegri.SelectedValue = sqlReader["mem_negri"].ToString();
                grid();
                sqlReader.Close();


               

            }
            else
            {
                sqlCommand.Dispose();
                conn.Close();
                grid();
                // negriBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    void grid()
    {

        SqlCommand cmd2 = new SqlCommand("select set_new_icno,FORMAT(set_txn_dt,'dd/MM/yyyy', 'en-us') set_txn_dt,FORMAT(set_txn_dt,'dd/MM/yyyy HH:mm:ss.fff', 'en-us') set_txn_dt1,Application_name,set_apply_amt from mem_settlement left join Ref_jenis_permohonan jp on jp.Application_code=set_appl_type_cd where set_new_icno='" + textbox1.Text + "' and Acc_sts ='Y'", con1);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            gvSelected.DataSource = ds2;
            gvSelected.DataBind();
            int columncount = gvSelected.Rows[0].Cells.Count;
            gvSelected.Rows[0].Cells.Clear();
            gvSelected.Rows[0].Cells.Add(new TableCell());
            gvSelected.Rows[0].Cells[0].ColumnSpan = columncount;
            gvSelected.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            gvSelected.DataSource = ds2;
            gvSelected.DataBind();
        }
    }

    protected void lblSubItem_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        c1 = commandArgs[0];
        //c2 = commandArgs[1];
        DateTime dt = DateTime.ParseExact(commandArgs[1], "dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
        c2 = dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
        show_shadetails();
    }

    void show_shadetails()
    {
        DataTable ddicno_sha = new DataTable();
        ddicno_sha = Con.Ora_Execute_table("select s1.*,mm.mem_sts_cd,mm.mem_member_no from mem_settlement s1 left join mem_member mm on mm.mem_new_icno=set_new_icno and mm.Acc_sts='Y' where set_new_icno='" + c1 + "' and set_txn_dt='" + c2 + "' and s1.Acc_sts ='Y'");

        if (ddicno_sha.Rows.Count != 0)
        {
            Button3.Visible = false;
            if (role_edit == "1")
            {
                Button6.Visible = true;
            }
            else
            {
                Button6.Visible = false;
            }

            textbox10.Text = ddicno_sha.Rows[0]["set_appl_type_cd"].ToString();
            textbox11.SelectedValue = ddicno_sha.Rows[0]["set_reason_cd"].ToString();
            textbox13.SelectedValue = ddicno_sha.Rows[0]["set_pay_method_cd"].ToString();
            DropDownList1.SelectedValue = ddicno_sha.Rows[0]["set_pay_type_cd"].ToString();
            string PD = ddicno_sha.Rows[0]["set_apply_amt"].ToString();
            textbox14.Text =  double.Parse(PD).ToString("C").Replace("$", "").Replace("RM", "");
            txn_dt.Text = Convert.ToDateTime(ddicno_sha.Rows[0]["set_txn_dt"]).ToString("yyyy-MM-dd");
            textbox12.Text = Convert.ToDateTime(ddicno_sha.Rows[0]["set_txn_dt"]).ToString("dd/MM/yyyy");
            textbox15.Text = ddicno_sha.Rows[0]["set_bank_acc_no"].ToString();
            Bank_details.SelectedValue = ddicno_sha.Rows[0]["set_bank_cd"].ToString();
            TextBox16.Text = ddicno_sha.Rows[0]["ID"].ToString();
            TextBox21.Text = ddicno_sha.Rows[0]["mem_member_no"].ToString();

            //wasi

            textbox17.Text = ddicno_sha.Rows[0]["set_wasi_icno"].ToString();
            textbox18.Text = ddicno_sha.Rows[0]["set_wasi_name"].ToString();
            textbox19.Text = ddicno_sha.Rows[0]["set_wasi_phone_no"].ToString();
            textbox20.Value = ddicno_sha.Rows[0]["set_wasi_address"].ToString();
            txtpostcd.Text = ddicno_sha.Rows[0]["set_postcd"].ToString();
            ddlnegri.SelectedValue = ddicno_sha.Rows[0]["set_negri"].ToString();

            DropDownList2.SelectedValue = ddicno_sha.Rows[0]["set_wasi_relation_cd"].ToString();
            if(ddicno_sha.Rows[0]["mem_sts_cd"].ToString() == "TS")
            {
                Button6.Visible = false;
                Button8.Visible = true;
            }
            else
            {
                Button8.Visible = false;
                Button6.Visible = true;
            }
        }
        //grid();

    }

    protected void Btl_Click(object sender, EventArgs e)
    {
        if (textbox1.Text != "")
        {
           
                    DateTime crdt = DateTime.ParseExact(textbox12.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string crdate = crdt.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("hh:mm:ss");
                    //string crdate = crdt.ToString("yyyy-MM-dd");
                    DataTable qry1 = new DataTable();
                    //qry1 = dbcon.Ora_Execute_table("select * from mem_settlement where set_new_icno='" + textbox1.Text + "' and set_txn_dt='" + crdate + "' and ID != '" + TextBox16.Text + "'");
                    qry1 = dbcon.Ora_Execute_table("select * from mem_settlement where set_new_icno='" + textbox1.Text + "' and ID = '" + TextBox16.Text + "' and Acc_sts ='Y'");
                    if (qry1.Rows.Count != 0)
                    {

                        string Inssql_delete = "delete from mem_settlement where set_new_icno='" + textbox1.Text + "' and ID = '" + TextBox16.Text + "' and Acc_sts ='Y'";
                        Status = Con.Ora_Execute_CommamdText(Inssql_delete);
                        if (Status == "SUCCESS")
                        {
                            string Inssql_updt = "Update mem_member set mem_sts_cd='SA' where mem_new_icno='" + textbox1.Text + "' and Acc_sts ='Y'";
                            Status = Con.Ora_Execute_CommamdText(Inssql_updt);
                            service.audit_trail("P0127", "Batal Permohonan", "No KP Baru", textbox1.Text);
                            clr_data();
                            grid();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Dihapuskan Berjaya.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                        }
                    }
                    else
                    {
                        grid();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('ID Penggunna Telah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

                    }
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void btnupdt_Click(object sender, EventArgs e)
    {
        if (textbox1.Text != "")
        {
            if (textbox10.SelectedValue != "" && textbox11.SelectedValue != "" && textbox13.SelectedValue != "" && DropDownList1.SelectedValue != "")
            {
                if (textbox14.Text != "")
                {
                    DateTime crdt = DateTime.ParseExact(textbox12.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string crdate = crdt.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("hh:mm:ss");
                    //string crdate = crdt.ToString("yyyy-MM-dd");
                    DataTable qry1 = new DataTable();
                    //qry1 = dbcon.Ora_Execute_table("select * from mem_settlement where set_new_icno='" + textbox1.Text + "' and set_txn_dt='" + crdate + "' and ID != '" + TextBox16.Text + "'");
                    qry1 = dbcon.Ora_Execute_table("select * from mem_settlement where set_new_icno='" + textbox1.Text + "' and Acc_sts ='Y'");
                    if (qry1.Rows.Count > 0)
                    {


                        string Inssql_updt = "Update mem_settlement set set_member_no='" + TextBox21.Text +"',set_txn_dt='" + crdate + "',set_bank_acc_no='" + textbox15.Text + "',set_bank_cd='" + Bank_details.SelectedValue + "',set_wasi_icno='" + textbox17.Text + "',set_wasi_name='" + textbox18.Text + "',set_wasi_phone_no='" + textbox19.Text + "',set_wasi_address='" + textbox20.Value.Replace("'", "''") + "',set_negri='" + ddlnegri.SelectedValue + "',set_postcd='" + txtpostcd.Text + "',set_wasi_relation_cd='" + DropDownList2.SelectedValue + "',set_appl_type_cd='" + textbox10.SelectedValue + "',set_reason_cd='" + textbox11.SelectedValue + "',set_pay_method_cd='" + textbox13.SelectedValue + "',set_pay_type_cd='" + DropDownList1.SelectedValue + "',set_apply_amt='" + textbox14.Text + "',set_apply_sts_ind='" + textbox24.Text + "',set_upd_id='" + Session["New"].ToString() + "',set_upd_dt='" + crdate + "' where set_new_icno='" + textbox1.Text + "' and ID = '" + TextBox16.Text + "' and Acc_sts ='Y'";
                        Status = Con.Ora_Execute_CommamdText(Inssql_updt);
                        if (Status == "SUCCESS")
                        {
                            service.audit_trail("P0127", "Kemaskini","No KP Baru", textbox1.Text);
                            clr_data();
                            grid();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                        }
                    }
                    else
                    {
                        grid();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('ID Penggunna Telah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

                    }
                }
                else
                {
                    grid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Pengeluaran Dipohon (RM).',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

                }
            }
            else
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Red Mark Field Diperlukan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    void clr_data()
    {
        if (role_add == "1")
        {
            Button3.Visible = true;
        }
        else
        {
            Button3.Visible = false;
        }
        if (textbox1.Text != "")
        {
            srch_rcd();
        }
        
        Button6.Visible = false;
        Button8.Visible = false;
        textbox10.SelectedValue = "";
        textbox11.SelectedValue = "";
        textbox13.SelectedValue = "";
        DropDownList1.SelectedValue = "";
        textbox14.Text = "";
        textbox12.Text = DateTime.Now.ToString("dd/MM/yyyy");
        
        grid();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../keanggotan/Penyelesaian_Anggota1.aspx");
    }

    protected void Reset_btn1(object sender, EventArgs e)
    {
        clr_data();
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../keanggotan/Penyelesaian_Anggota1.aspx");
    }
}
