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
using System.Threading;


public partial class Tambah_Syer_Tunai1 : System.Web.UI.Page
{
    DBConnection dbcon = new DBConnection();
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    StudentWebService service = new StudentWebService();
    string str;
    string lod_pg = string.Empty;
    DBConnection DBCon = new DBConnection();
    SqlCommand com;
    DataTable dt = new DataTable();
    string Status = string.Empty;
    string level, userid;
    string c1 = string.Empty, c2 = string.Empty, c3 = string.Empty;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                grid();
                TextBox7.Text = "TAMBAH SYER TUNAI";
                TextBox6.Attributes.Add("readonly", "readonly");
                TextBox7.Attributes.Add("readonly", "readonly");
                TextArea1.Attributes.Add("readonly", "readonly");
                TextBox9.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //Button1.Visible = false;
                //Button2.Visible = false;

                var samp = Request.Url.Query;
                if (samp != "")
                {
                    TextBox12.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    srch_details();
                    Button4.Visible = false;
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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0211' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

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
                    Button2.Visible = true;
                }
                else
                {
                    Button2.Visible = false;
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

    void app_language()
    {
        if (Session["New"] != null)
        {
            assgn_roles();
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1047','11','1046','124','13','14','16','20','1012','23','24','25','1048','1050','75','53','55','61','35','15','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[19][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            //ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[20][0].ToString().ToLower());
            //ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[18][0].ToString().ToLower());
            ps_lbl15.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl16.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl17.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    
    protected void btnsrch_Click(object sender, EventArgs e)
    {
        srch_details();
    }
    void srch_details()
    {
        con1.Open();
        if (TextBox12.Text != "")
        {
            DataTable ddicno = new DataTable();
            ddicno = DBCon.Ora_Execute_table("select mem_new_icno from mem_member where mem_new_icno='" + TextBox12.Text + "' and Acc_sts ='Y' and mem_sts_cd IN ('SA','FM')");
            if (ddicno.Rows.Count > 0)
            {
                TextBox6.Attributes.Remove("readonly");

                TextArea1.Attributes.Remove("readonly");
                Button1.Visible = true;
                if (role_add == "1")
                {
                    Button2.Visible = true;
                }
                else
                {
                    Button2.Visible = false;
                }
                //Button3.Visible = true;
                //str = "select * from mem_member as mm left join Ref_Cawangan as rc ON rc.cawangan_code = mm.mem_branch_cd left join Ref_Wilayah as rw ON rw.wilayah_code = mm.mem_region_cd where mm.mem_new_icno='" + TextBox12.Text.Trim() + "'";
                str = "select mm.mem_new_icno,mm.mem_name,mm.mem_address,mm.mem_postcd,mm.mem_negri,case(mm.mem_phone_h) when 'NULL' then '' else mm.mem_phone_h END  as mem_phone_h,case(mm.mem_phone_o) when 'NULL' then '' else mm.mem_phone_o END  as mem_phone_o,case(mm.mem_phone_m) when 'NULL' then '' else mm.mem_phone_m END  as mem_phone_m,rc.cawangan_name,rc.Wilayah_Name, case when ISNULL(mm.mem_centre,'') = 'NULL' then '' else ISNULL(mm.mem_centre,'') end mem_centre,mm.mem_member_no,mm.mem_bank_acc_no,mm.mem_bank_cd,CASE WHEN  convert(varchar, mm.mem_register_dt, 103) = '01/01/1900' THEN '' ELSE convert(varchar, mm.mem_register_dt, 103) END mem_register_dt from mem_member as mm Left join Ref_Cawangan as rc on rc.cawangan_code = 	mm.mem_branch_cd Left join Ref_Wilayah as rw on rw.Wilayah_Code = mm.mem_region_cd where mm.mem_new_icno='" + TextBox12.Text.Trim() + "' and mm.Acc_sts ='Y'";
                com = new SqlCommand(str, con1);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    TextBox12.Text = reader["mem_new_icno"].ToString();
                    TextBox1.Text = reader["mem_name"].ToString();
                    TextBox2.Text = reader["mem_member_no"].ToString();
                    string phone_h = reader["mem_phone_h"].ToString();
                    string phone_o = reader["mem_phone_o"].ToString();
                    string phone_m = reader["mem_phone_m"].ToString();

                    if (phone_h != "" && phone_o == "" && phone_m == "")
                    {
                        TextBox14.Text = phone_h;
                    }
                    else if (phone_h == "" && phone_o != "" && phone_m == "")
                    {
                        TextBox14.Text = phone_o;
                    }
                    else if (phone_h == "" && phone_o == "" && phone_m != "")
                    {
                        TextBox14.Text = phone_m;
                    }
                    else if (phone_h != "" && phone_o != "" && phone_m == "")
                    {
                        TextBox14.Text = phone_h;
                    }
                    else if (phone_h == "" && phone_o != "" && phone_m != "")
                    {
                        TextBox14.Text = phone_o;
                    }
                    else if (phone_h != "" && phone_o == "" && phone_m != "")
                    {
                        TextBox14.Text = phone_h;
                    }
                    else if (phone_h != "" && phone_o != "" && phone_m != "")
                    {
                        TextBox14.Text = phone_h;
                    }
                    else if (phone_h == "" && phone_o == "" && phone_m == "")
                    {
                        TextBox14.Text = "";
                    }


                    TextBox3.Text = reader["wilayah_name"].ToString();
                    TextBox4.Text = reader["cawangan_name"].ToString();
                    TextBox5.Text = reader["mem_centre"].ToString();
                    reader.Close();
                    con1.Close();
                }
                grid();
            }
            else if (ddicno.Rows.Count == 0)
            {
                grid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No IC.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }


  

    private DataTable GetData(string sha_new_icno, string sha_txn_dt, string sha_txn_ind, string sha_debit_amt, string sha_item, string sha_reference_ind, string sha_apply_sts_ind, string sha_crt_id, string shrefno, string sha_crt_dt)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ToString();
        using (SqlConnection cn = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("mem_share_Procedure", cn);
            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@sha_new_icno", SqlDbType.VarChar).Value = sha_new_icno;
            DateTime crdt = DateTime.ParseExact(TextBox9.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string crdate = crdt.ToString("yyyy-MM-dd");
            string crdate1 = crdt.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("hh:mm:ss");
            cmd.Parameters.Add("@sha_txn_dt", SqlDbType.DateTime).Value = crdate;
            cmd.Parameters.Add("@sha_txn_ind", SqlDbType.VarChar).Value = sha_txn_ind;
            cmd.Parameters.Add("@sha_debit_amt", SqlDbType.VarChar).Value = sha_debit_amt;
            cmd.Parameters.Add("@sha_item", SqlDbType.VarChar).Value = sha_item;
            cmd.Parameters.Add("@sha_reference_ind", SqlDbType.VarChar).Value = sha_reference_ind;
            cmd.Parameters.Add("@sha_apply_sts_ind", SqlDbType.VarChar).Value = sha_apply_sts_ind;
            cmd.Parameters.Add("@sha_reference_no", SqlDbType.VarChar).Value = shrefno;
            cmd.Parameters.Add("@sha_crt_id", SqlDbType.VarChar).Value = sha_crt_id;
            cmd.Parameters.Add("@sha_crt_dt", SqlDbType.DateTime).Value = crdate1;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
        }        
        return dt;
    }

    void grid()
    {
        DataTable ddicno = new DataTable();
        ddicno = DBCon.Ora_Execute_table("select mem_new_icno from mem_member where mem_new_icno='" + TextBox12.Text + "' and Acc_sts ='Y' and mem_sts_cd IN ('SA','FM')");

        string qry1 = string.Empty;
        if (ddicno.Rows.Count != 0)
        {
            qry1 = "select sha_new_icno,FORMAT(sha_txn_dt,'dd/MM/yyyy', 'en-us') sha_txn_dt,FORMAT(sha_crt_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') sha_crt_dt, sha_debit_amt,sha_item,ISNULL(sha_approve_sts_cd,'') as sha_approve_sts_cd,ISNULL(sha_batch_name,'') as bname from mem_share where sha_new_icno='" + TextBox12.Text + "' and Acc_sts ='Y' order by sha_crt_dt";
        }
        else
        {
            qry1 = "select sha_new_icno,FORMAT(sha_txn_dt,'dd/MM/yyyy', 'en-us') sha_txn_dt,FORMAT(sha_crt_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') sha_crt_dt, sha_debit_amt,sha_item,ISNULL(sha_approve_sts_cd,'') as sha_approve_sts_cd,ISNULL(sha_batch_name,'') as bname from mem_share where sha_new_icno=''";
        }

        SqlCommand cmd2 = new SqlCommand(qry1, con1);
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
            gvSelected.Rows[0].Cells[0].Text = "<strong><center>Maklumat Carian Tidak Dijumpai</center></strong>";
        }
        else
        {
            gvSelected.DataSource = ds2;
            gvSelected.DataBind();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid();
    }

    protected void lblSubItem_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        c1 = commandArgs[0];
        //c2 = commandArgs[1];
        DateTime dt = DateTime.ParseExact(commandArgs[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
        c2 = dt.ToString("yyyy-MM-dd");
        c3 = commandArgs[2];

        show_shadetails();
    }

    void show_shadetails()
    {
        DataTable ddicno_sha = new DataTable();
        ddicno_sha = DBCon.Ora_Execute_table("select * from mem_share where sha_new_icno='" + c1 + "' and Acc_sts ='Y' and sha_txn_dt='" + c2 + "' and sha_crt_dt='" + c3 + "'");

        if (ddicno_sha.Rows.Count != 0)
        {
            Button2.Visible = false;
            if (role_edit == "1")
            {
                Button5.Visible = true;
            }
            else
            {
                Button5.Visible = false;
            }
            TextBox8.Text = c2;
            TextBox6.Text = double.Parse(ddicno_sha.Rows[0]["sha_debit_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            TextArea1.Value = ddicno_sha.Rows[0]["sha_reference_no"].ToString();
            TextBox9.Text = Convert.ToDateTime(ddicno_sha.Rows[0]["sha_txn_dt"]).ToString("dd/MM/yyyy");
            TextBox10.Text = Convert.ToDateTime(ddicno_sha.Rows[0]["sha_crt_dt"]).ToString("yyyy-MM-dd HH:mm:ss");
        }
        //grid();

    }

    protected void btnupdt_Click(object sender, EventArgs e)
    {
        if (TextBox12.Text != "")
        {
            if (TextBox6.Text != "")
            {

                DateTime crdt = DateTime.ParseExact(TextBox9.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string crdate = crdt.ToString("yyyy-MM-dd");
                string crdate1 = crdt.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("hh:mm:ss");

                DataTable qry1 = new DataTable();
                qry1 = DBCon.Ora_Execute_table("select * from mem_share where sha_new_icno='" + TextBox12.Text + "' and sha_txn_dt='" + crdate + "' and Acc_sts ='Y' and sha_crt_dt != '" + TextBox10.Text + "'");
                if (qry1.Rows.Count == 0)
                {
                    string Inssql_updt = "UPDATE mem_share SET sha_txn_dt='" + crdate + "',sha_debit_amt='" + TextBox6.Text + "',sha_reference_no='" + TextArea1.Value + "',sha_upd_id='" + Session["New"].ToString() + "',sha_upd_dt='" + crdate1 + "' where sha_new_icno='" + TextBox12.Text + "' and Acc_sts ='Y' and sha_crt_dt='" + TextBox10.Text + "'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql_updt);
                    if (Status == "SUCCESS")
                    {
                        service.audit_trail("P0211", "Kemaskini","No Kp Baru",TextBox12.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemeskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        clr_reset();
                    }
                    grid();
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Jumlah Pembelian Syer.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No KP Baru.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    void clr_reset()
    {
        if (role_add == "1")
        {
            Button2.Visible = true;
        }
        else
        {
            Button2.Visible = false;
        }
        Button5.Visible = false;
        TextBox8.Text = "";
        TextBox6.Text = "";
        TextArea1.Value = "";
        TextBox9.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }
    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            System.Web.UI.WebControls.LinkButton Label1_txt = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("lblSubItem");
            System.Web.UI.WebControls.Label Label1_lbl = (System.Web.UI.WebControls.Label)e.Row.FindControl("stscd");
            System.Web.UI.WebControls.Label Label1_item = (System.Web.UI.WebControls.Label)e.Row.FindControl("shitem");
            if (Label1_lbl.Text != "" || Label1_item.Text == "ANGGOTA BAHARU")
            {
                //Label1_txt.Attributes.Add("style", "pointer-events:None;");
                LinkButton Hlnk = e.Row.FindControl("lblSubItem") as LinkButton;
                Hlnk.Attributes.Add("style", "pointer-events:None; color:Black;");
            }
            else
            {
                //Label1_txt.Attributes.Remove("Style");
                LinkButton Hlnk = e.Row.FindControl("lblSubItem") as LinkButton;
                Hlnk.Visible = true;
            }
        }

    }

    //insert mem_share
    protected void btnsmmit_Click(object sender, EventArgs e)
    {
        if (TextBox12.Text != "")
        {
            if (TextBox6.Text != "")
            {
                DateTime crdt1 = DateTime.ParseExact(TextBox9.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string crdate1 = crdt1.ToString("yyyy-MM-dd");
                DataTable qry1 = new DataTable();
                qry1 = DBCon.Ora_Execute_table("select * from mem_share where sha_new_icno='" + TextBox12.Text + "' and Acc_sts ='Y' and sha_txn_dt='" + crdate1 + "'");
                if (qry1.Rows.Count == 0)
                {
                    DataTable ddicno_sts = new DataTable();
                    ddicno_sts = DBCon.Ora_Execute_table("select mem_sts_cd from mem_member where mem_new_icno='" + TextBox12.Text + "' and Acc_sts ='Y'");
                    string rmark = string.Empty;
                    if (ddicno_sts.Rows[0]["mem_sts_cd"].ToString() == "FM")
                    {
                        rmark = "ANGGOTA BAHARU";

                        if (Convert.ToInt32(TextBox6.Text) >= 100.00)
                        {
                            DataTable ddicno_fee = new DataTable();
                            ddicno_fee = DBCon.Ora_Execute_table("select fee_refund_ind from mem_fee where fee_new_icno='" + TextBox12.Text + "' and Acc_sts ='Y'");
                            if (ddicno_fee.Rows.Count != 0)
                            {
                                DataTable updt_fee = new DataTable();
                                updt_fee = DBCon.Ora_Execute_table("UPDATE mem_fee set fee_refund_ind='' where fee_new_icno='" + TextBox12.Text + "' and Acc_sts ='Y' and fee_refund_ind != ''");
                            }

                            DataTable updt_mem = new DataTable();
                            updt_mem = DBCon.Ora_Execute_table("UPDATE mem_member set mem_sts_cd='' where mem_new_icno='" + TextBox12.Text + "' and mem_sts_cd='FM' and Acc_sts ='Y'");
                            lod_pg = "window.location ='../keanggotan/Tambah_Syer_Tunai1.aspx';";
                        }
                    }
                    else
                    {
                        rmark = "TAMBAHAN SYER";

                    }

                    string sha_new_icno = TextBox12.Text;
                    string sha_txn_dt =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string sha_txn_ind = "B";
                    string sha_debit_amt = TextBox6.Text;
                    string sha_item = rmark;
                    string sha_reference_ind = "C";
                    string sha_apply_sts_ind = "N";
                    string sha_upd_id = Session["New"].ToString();
                    string shrefno = TextArea1.Value;

                    string sha_upd_dt = DateTime.Now.ToString("yyyy-MM-dd");
                    dt = GetData(sha_new_icno, sha_txn_dt, sha_txn_ind, sha_debit_amt, sha_item, sha_reference_ind, sha_apply_sts_ind, sha_upd_id, shrefno, sha_upd_dt);
                    service.audit_trail("P0211", "Simpan","No Kp Baru",TextBox12.Text);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    clr_reset();
                    grid();
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Jumlah Pembelian Syer.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            grid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No KP Baru.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        clr_reset();
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../keanggotan/Tambah_Syer_Tunai1.aspx");
    }
}