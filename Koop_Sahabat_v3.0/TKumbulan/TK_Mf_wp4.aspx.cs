using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Text;
using System.Collections;
using System.Windows.Forms;

public partial class TKumbulan_TK_Mf_wp4 : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;

    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DBConnection Dblog = new DBConnection();
    Hashtable passvalue = new Hashtable();
    StudentWebService service = new StudentWebService();
    string userid;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    string uniqueId, uniqueId2, uniqueId3, unq_id1, unq_id2, unq_id3;
    string Status = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                bind();


            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }

        }

    }

    void bind()
    {
        con1.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select * from aim_st_loc where st_file_type='03' and st_file_sts_cd='A'", con1);
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
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        con1.Close();
    }

    protected void lnkView_Click11(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl1");
        string lblid = lblTitle.Text;
        string filePath = Server.MapPath("~/FILES/WP4/" + lblid.Trim());
        Response.ContentType = ContentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(filePath) + "\"");
        Response.WriteFile(filePath);
        Response.End();
    }

    protected void lnkView_Click12(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label lblTitle = (System.Web.UI.WebControls.Label)gvRow.FindControl("lbl1");
        string lblid = lblTitle.Text;
        string filePath = Server.MapPath("~/FILES/WP4/" + lblTitle.Text.Trim());
        DataTable delete_file = new DataTable();
        DataTable chk_file = new DataTable();
        chk_file = Dblog.Ora_Execute_table("select * from aim_st_loc where st_file_type='03' and st_file_nm='" + lblTitle.Text + "'");
        if (chk_file.Rows.Count != 0)
        {
            string Inssql_item = "delete from aim_st_loc where st_file_type='03' and st_file_nm='" + lblTitle.Text + "'";
            Status = Dblog.Ora_Execute_CommamdText(Inssql_item);
            if (Status == "SUCCESS")
            {
                File.Delete(filePath);
                bind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }

    }
    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = Dblog.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = Dblog.Ora_Execute_table("select m1.* from KK_Role_skrins m1   where sub_skrin_id='S0018' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {

                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();

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

    protected void UploadFile(object sender, EventArgs e)
    {
        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Files/WP4/") + fileName);
        Response.Redirect(Request.Url.AbsoluteUri);
    }

  
    void UPLOAD()
    {
        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Files/WP4/") + fileName);

    }


    protected void btnok_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable chk_file = new DataTable();
            chk_file = Dblog.Ora_Execute_table("select * from aim_st_loc where st_file_type='03' and st_file_nm='" + Path.GetFileName(FileUpload1.PostedFile.FileName) + "'");
            if (chk_file.Rows.Count == 0)
            {
                if (FileUpload1.HasFile)
                {
                    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string excelPath = Server.MapPath("~/Files/PST/") + fileName;
                    FileUpload1.SaveAs(Server.MapPath("~/Files/WP4/" + FileUpload1.FileName));
                    string path = Server.MapPath("~/Files/WP4/" + FileUpload1.FileName).ToString();
                 
                    //TextBox1.Text = path;
                    if (!string.IsNullOrEmpty(path))
                    {
                       
                            string[] readText = System.IO.File.ReadAllLines(path);
                        //File.ReadAllLines(path);
                        StringBuilder strbuild = new StringBuilder();
                        string b_name = "";
                        string eft_no = "";
                        string amt = "";
                        string d_acc = "";
                        string b_cd = "";
                        string bop = "";
                        string ben_name = "";
                        string ben_acc_no = "";
                        string ben_new_icno = "";
                        string p_name = "";
                        string branch_cd = "";
                        string produk_cd = "";
                        string p_details = "";
                        string ben_old_icno = "";
                        string biz_reg_no = "";
                        string ppt_no = "";
                        string Inssql = string.Empty;
                        //string count_val = "";
                        foreach (string s in readText)
                        {
                            
                                int count_val = s.Count();
                            if (count_val != 906)
                            {
                                b_name = s.Substring(0, 10).Trim();
                                eft_no = s.Substring(11, 15).Trim();
                                amt = s.Substring(59, 13).Trim();
                                d_acc = s.Substring(73, 19).Trim();
                                b_cd = s.Substring(148, 11).Trim();
                                bop = s.Substring(159, 4).Trim();
                                ben_name = s.Substring(180, 120).Trim();
                                ben_acc_no = s.Substring(300, 20).Trim();
                                ben_new_icno = s.Substring(320, 15).Trim();
                                p_name = s.Substring(336, 39).Trim();
                                produk_cd = s.Substring(382, 14).Trim();
                                branch_cd = s.Substring(397, 3).Trim();
                                ben_old_icno = s.Substring(704, 14).Trim();
                                biz_reg_no = s.Substring(719, 14).Trim();
                                ppt_no = s.Substring(734, 14).Trim();
                                DataTable chk_wp4_table = new DataTable();
                                chk_wp4_table = Dblog.Ora_Execute_table("select * from aim_wp4 where wp4_batch_name='" + b_name + "'");
                                if (chk_wp4_table.Rows.Count == 0)
                                {
                                    Inssql = "insert into aim_wp4 (wp4_batch_name,wp4_eft_no,wp4_amt,wp4_debit_acc,wp4_bank_cd,wp4_bop,wp4_bene_name,wp4_bene_acc_no,wp4_bene_new_icno,wp4_payer_name,wp4_pay_detail,wp4_bene_old_icno,wp4_biz_reg_no,wp4_ppt_no,wp4_crt_id,wp4_crt_dt,wp4_product_name,wp4_branch_cd,wp4_withdrawal_type_cd) values('" + b_name + "','" + eft_no + "','" + amt + "','" + d_acc + "','" + b_cd + "','" + bop + "','" + ben_name + "','" + ben_acc_no + "','" + ben_new_icno + "','" + p_name + "','" + p_details + "','" + ben_old_icno + "','" + biz_reg_no + "','" + ppt_no + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','" + produk_cd + "','" + branch_cd + "','')";
                                    Status = Dblog.Ora_Execute_CommamdText(Inssql);
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Format fail WP4 Tidak Memenuhi Kriteria. Sila Semak Semula',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                            }
                        }

                        if (Status == "SUCCESS")
                        {
                           
                            string Inssql_file = "insert into aim_st_loc (st_file_type,st_file_nm,st_file_loc,st_file_sts_cd,st_file_create_id,st_file_create_dte) values('03','" + fileName + "','" + excelPath + "','A','" + Session["new"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            string Status_file = Dblog.Ora_Execute_CommamdText(Inssql_file);
                            if (Status_file == "SUCCESS")
                            {

                                // Integration Part

                                DataTable ddsha = new DataTable();
                                ddsha = DBCon.Ora_Execute_table("select wp4_batch_name,Format(wp4_pay_txn_dt,'yyyy-MM-dd') app_date,count(wp4_bene_new_icno) cnt,sum(ISNULL(wp4_amt,'0.00')) syer_amt from aim_wp4 where wp4_batch_name='"+ b_name +"' group by wp4_batch_name,wp4_pay_txn_dt");

                                if (ddsha.Rows.Count != 0)
                                {
                                    userid = Session["New"].ToString();
                                    GetUniqueInv();

                                    //WP4

                                    DataTable get_inter_info_sh = DBCon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0005' and jur_item='WP4' and jur_desc_cd='24'");

                                    string Inssql_sh = "Insert into KW_jurnal_inter (no_permohonan,no_Rujukan,tarikh_lulus,Terma,Jenis_permohonan,perkara,nama_pelanggan_code,Overall,Status,crt_id,cr_dt) "
                                        + " Values ('" + unq_id2 + "','" + ddsha.Rows[0]["wp4_batch_name"].ToString() + "','" + ddsha.Rows[0]["app_date"].ToString() + "','30','24', "
                                        + " '" + get_inter_info_sh.Rows[0]["jur_desc"].ToString() + "','" + get_inter_info_sh.Rows[0]["jur_module"].ToString() + "','" + ddsha.Rows[0]["syer_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    Status = DBCon.Ora_Execute_CommamdText(Inssql_sh);

                                    string Inssql_sh_items = "Insert into KW_jurnal_inter_items (no_permohonan,keterangan,jumlah,Overall,Status,crt_id,cr_dt) "
                                        + " Values ('" + unq_id2 + "','" + get_inter_info_sh.Rows[0]["jur_desc"].ToString() + "','" + ddsha.Rows[0]["cnt"].ToString() + "','" + ddsha.Rows[0]["syer_amt"].ToString() + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    Status = DBCon.Ora_Execute_CommamdText(Inssql_sh_items);

                                    DataTable dt_upd_format2 = DBCon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + unq_id2 + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='24' and Status = 'A'");

                                }
                               
                            }
                            service.audit_trail("S0058", b_name, "Muatnaik Fail PST", fileName);
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dimuatnaik.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Fail Berkenaan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Nama Telah Didaftar, Sila Semak.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + ex.Message + ".',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
        bind();
    }

    private void GetUniqueInv()
    {


        // WP4

        DataTable dt1_ms = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='24' and Status='A'");
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


            DataTable get_inter_info_ms = DBCon.Ora_Execute_table("select jur_module,jur_item,jur_desc,jur_desc_cd from KW_ref_jurnal_inter where jur_module='M0005' and jur_item='WP4' and jur_desc_cd='24'");
            DataTable get_doc1_ms = new DataTable();
            get_doc1_ms = DBCon.Ora_Execute_table("select Ref_doc_descript as s1 from KW_Ref_Doc_kod where Ref_doc_code='24'");
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

}