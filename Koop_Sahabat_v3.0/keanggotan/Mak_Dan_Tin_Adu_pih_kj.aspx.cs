using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Data;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Web.SessionState;

public partial class Mak_Dan_Tin_Adu_pih_kj : System.Web.UI.Page
{
    DBConnection Con = new DBConnection();
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    static string statename = string.Empty, FROMdate = string.Empty, TOdate = string.Empty, branchname = string.Empty, zonid = string.Empty;
    string CommandArgument1;
    DataTable ddcom = new DataTable();
    StudentWebService service = new StudentWebService();
    string level;
    string cid;
    string Status = string.Empty;
    string userid;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                assgn_roles();
                level = Session["level"].ToString();
                txtnama2.Text = Session["New"].ToString();
                userid = Session["New"].ToString();
                memsts();
                lvl1();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }

        }
    }
    void memsts()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select keterangan,keterangan_code from ref_status_aduan order by keterangan";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DDSTS.DataSource = dt;
            DDSTS.DataBind();
            DDSTS.DataTextField = "keterangan";
            DDSTS.DataValueField = "keterangan_code";
            DDSTS.DataBind();
            DDSTS.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            ddsts1.DataSource = dt;
            ddsts1.DataBind();
            ddsts1.DataTextField = "keterangan";
            ddsts1.DataValueField = "keterangan_code";
            ddsts1.DataBind();
            ddsts1.Items.Insert(0, new ListItem("--- PILIH ---", ""));


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = Con.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = Con.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0133' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
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

    void bind()
    {
        DataTable Dt = new DataTable();
        string userid = Session["new"].ToString();

       
            DataTable ddlvl2 = new DataTable();
            ddlvl2 = Con.Ora_Execute_table(" select act_comp_id from mem_complaint_action where act_comp_id='" + Label2.Text + "' and  act_user_lvl_ind='5'");
            if (ddlvl2.Rows.Count > 0)
            {
                DataTable ddsha = new DataTable();
                ddsha = Con.Ora_Execute_table("update mem_complaint_action set act_feedback='" + Txtmak2.Text + "',act_action='" + Txttin2.Text + "',act_sts_cd='" + ddsts1.SelectedValue + "',act_upd_id='" + txtnama2.Text + "',aact_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where act_user_lvl_ind='5' and act_comp_id='" + Label2.Text + "'");
                DataTable ddcom = new DataTable();
                ddcom = Con.Ora_Execute_table("update mem_complaint set com_sts_cd='" + ddsts1.SelectedValue + "' ,com_upd_dt='" + DateTime.UtcNow.ToString() + "',com_upd_id='" + userid + "' where comp_id='" + Label2.Text + "' and Acc_sts ='Y'");
            Button1.Text = "Kemaskini";
            service.audit_trail("P0214", "Kemaskini", "Pengadu", Txtpen.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

        }
            else
            {
                string Inssql = "insert into mem_complaint_action(act_comp_id,act_feedback,act_action,act_sts_cd,act_user_lvl_ind,act_crt_id,act_crt_dt,act_upd_id)values('" + Label2.Text + "','" + Txtmak2.Text + "','" + Txttin2.Text + "','" + ddsts1.SelectedValue + "','5','" + txtnama2.Text + "' ,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','')";
                Status = Con.Ora_Execute_CommamdText(Inssql);
                DataTable ddcom = new DataTable();
                ddcom = Con.Ora_Execute_table("update mem_complaint set com_sts_cd='" + ddsts1.SelectedValue + "' ,com_upd_dt='" + DateTime.UtcNow.ToString() + "',com_upd_id='" + userid + "' where comp_id='" + Label2.Text + "' and Acc_sts ='Y'");
                string audsql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc)values('" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','010521','KEMASKINI MAKLUMBALAS DAN ADUAN(KETUA JABATAN)')";
                Status = Con.Ora_Execute_CommamdText(audsql);
            Button1.Text = "Kemaskini";
            service.audit_trail("P0214", "Simpan", "Pengadu", Txtpen.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
        }

       
       

    }

    void lvl1()
    {
        try
        {

            statename = Request.QueryString["State"];

            ddcom = Con.Ora_Execute_table("Select mc.comp_id,mc.com_dt,rs.KETERANGAN,rt.Complaint_Type,mc.com_remark,mm.mem_branch_cd,mc.com_crt_id,rc.cawangan_name,mc.com_tel_no,mc.com_kaegory from mem_complaint mc LEFT JOIN mem_member AS mm on mm.mem_new_icno=mc.com_new_icno and mm.Acc_sts ='Y' LEFT JOIN Ref_Cawangan AS rc on rc.cawangan_code=mm.mem_branch_cd LEFT JOIN Ref_Jenis_Aduan AS rt on rt.Complaint_Code=mc.com_type_ind LEFT JOIN Ref_Status_Aduan AS rs on rs.KETERANGAN_Code=mc.com_sts_cd where mc.Acc_sts ='Y' and mc.comp_id='" + statename + "'");
            if (ddcom.Rows.Count != 0)
            {
                Button1.Text = "Kemaskini";
                cid = ddcom.Rows[0][0].ToString();
                Label2.Text = cid;
                // Txtdate.Text = ddcom.Rows[0][1].ToString();

                string fmdate = ddcom.Rows[0][1].ToString();
                Txtdate.Text = Convert.ToDateTime(fmdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                Txtdate.ReadOnly = true;
                Txtkat.Text = ddcom.Rows[0][3].ToString();
                Txtkat.ReadOnly = true;
                txtsts.Text = ddcom.Rows[0][2].ToString();
                txtsts.ReadOnly = true;
                txtcaw.Text = ddcom.Rows[0][7].ToString();
                txtcaw.ReadOnly = true;
                Txtpen.Text = ddcom.Rows[0][6].ToString();
                Txtpen.ReadOnly = true;
                txtdesc.Text = ddcom.Rows[0][4].ToString();
                txtdesc.ReadOnly = true;
                string cdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Txtcdate.Text = Convert.ToDateTime(cdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                Txtcdate.ReadOnly = true;

                Txt_telno.Text = ddcom.Rows[0]["com_tel_no"].ToString();
                Txt_telno.Attributes.Add("Readonly", "Readonly");
                dd_kat.SelectedValue = ddcom.Rows[0]["com_kaegory"].ToString();
                dd_kat.Attributes.Add("Readonly", "Readonly");
                //Txtnama1.Text = userid;
                //Txtnama1.ReadOnly = true;

                txtcdate1.Text = Convert.ToDateTime(cdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                txtcdate1.ReadOnly = true;
                txtnama2.Text = Session["New"].ToString();
                txtnama2.ReadOnly = true;
                Txtcdate3.Text = Convert.ToDateTime(cdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                Txtcdate3.ReadOnly = true;
                txtnama3.Text = Session["New"].ToString();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        
            lvl4();
            lvl5();
            Button5.Visible = true;
        
    }


    void lvl3()
    {
        try
        {
            view_lvl4();
            view_lvl5();
            statename = Request.QueryString["State"];

            ddcom = Con.Ora_Execute_table("SELECT MC.act_feedback,MC.act_action,MC.act_sts_cd,RS.keterangan,MC.act_crt_id,MC.act_crt_dt FROM mem_complaint_action as MC left join mem_complaint as mco on mco.comp_id=MC.act_comp_id and mco.Acc_sts ='Y' Left join ref_status_aduan as RS ON MC.act_sts_cd=RS.keterangan_code WHERE MC.act_comp_id='" + statename + "' and MC.act_user_lvl_ind='4'");
            if (ddcom.Rows.Count != 0)
            {
                txtmak1.Text = ddcom.Rows[0][0].ToString();
                txtmak1.Enabled = true;
                Txttin1.Text = ddcom.Rows[0][1].ToString();
                Txttin1.Enabled = true;
                DDSTS.SelectedValue = ddcom.Rows[0][2].ToString();
                DDSTS.Enabled = true;
                string fmdate = ddcom.Rows[0][5].ToString();
                Txtcdate.Text = Convert.ToDateTime(fmdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                Txtcdate.Enabled = true;
                // Txtcdate.Text = ddcom.Rows[0][5].ToString();
                //Txtnama1.Text = ddcom.Rows[0][4].ToString();
                if (level == "4")
                {
                    Txtnama1.Text = Session["New"].ToString();
                }
                else
                {
                    Txtnama1.Text = "";
                }
                //Txtnama1.Enabled = true;
                //txtnama2.Text = Session["New"].ToString();
                //txtnama2.Enabled = true;
                //txtcdate1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //txtcdate1.Enabled = true;
                Txtmak2.ReadOnly = true;
                Txttin2.ReadOnly = true;
                ddsts1.Enabled = false;
                txtcdate1.ReadOnly = true;
                txtnama2.ReadOnly = true;
                Txttin3.ReadOnly = true;
                chkb1.Enabled = false;
                Txtcdate3.ReadOnly = true;
                txtnama3.ReadOnly = true;
                Button4.Visible = false;
                Button1.Text = "Kemaskini";
            }
            else
            {
                Button1.Text = "Simpan";
                string cdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Txtcdate.Text = Convert.ToDateTime(cdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                Txtcdate.ReadOnly = true;
              
                    Txtnama1.Text = Session["New"].ToString();
              
                    Txtnama1.Text = "";
              
                //Txtnama1.ReadOnly = true;

                Txtmak2.ReadOnly = true;
                Txttin2.ReadOnly = true;
                ddsts1.Enabled = false;
                //txtnama3.Enabled = false;
                txtcdate1.ReadOnly = true;
                txtnama2.ReadOnly = true;

                Txttin3.ReadOnly = true;
                chkb1.Enabled = false;
                Txtcdate3.ReadOnly = true;
                txtnama3.ReadOnly = true;

                Button4.Visible = false;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void lvl4()
    {
        try
        {
            view_lvl3();
            DataTable DDTLVL = new DataTable();
            DDTLVL = Con.Ora_Execute_table("SELECT MC.act_feedback,MC.act_action,MC.act_sts_cd,RS.keterangan,MC.act_crt_id,MC.act_crt_dt FROM mem_complaint_action as MC left join mem_complaint as mco on mco.comp_id=MC.act_comp_id and mco.Acc_sts ='Y' Left Join ref_status_aduan as RS ON MC.act_sts_cd=RS.keterangan_code WHERE MC.act_comp_id='" + statename + "' and MC.act_user_lvl_ind='5'");
            if (DDTLVL.Rows.Count != 0)
            {
                Txtmak2.Text = DDTLVL.Rows[0][0].ToString();
                //Txtmak2.Enabled = false;
                Txttin2.Text = DDTLVL.Rows[0][1].ToString();
                //Txttin2.Enabled = false;
                ddsts1.SelectedValue = DDTLVL.Rows[0][2].ToString();
                //ddsts1.Enabled = false;
                string fmdate = DDTLVL.Rows[0][5].ToString();
                Txtcdate.Text = Convert.ToDateTime(fmdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                
                    txtnama2.Text = Session["New"].ToString();
                
                txtcdate1.ReadOnly = true;
                txtnama2.ReadOnly = true;

                //Txttin3.ReadOnly = true;
                //chkb1.Enabled = false;
                txtnama3.Text = Session["New"].ToString();
                txtnama3.ReadOnly = true;
                string cdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Txtcdate3.Text = Convert.ToDateTime(cdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                //Txtcdate3.ReadOnly = true;

                txtmak1.ReadOnly = true;
                Txttin1.ReadOnly = true;
                DDSTS.Enabled = false;
                Txtcdate.ReadOnly = true;
                Txtnama1.ReadOnly = true;

                Button4.Visible = false;

            }
            else
            {
                string cdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                txtcdate1.Text = Convert.ToDateTime(cdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                txtcdate1.ReadOnly = true;
               
                    txtnama2.Text = Session["New"].ToString();
               txtnama2.ReadOnly = true;

                txtmak1.ReadOnly = true;
                Txttin1.ReadOnly = true;
                DDSTS.Enabled = false;
                Txtcdate.ReadOnly = true;
                Txtnama1.ReadOnly = true;

                //Txttin3.ReadOnly = true;
                //chkb1.Enabled = false;
                //Txtcdate3.ReadOnly = true;
                //txtnama3.ReadOnly = true;

                //Button4.Visible = false;

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void lvl5()
    {
        try
        {
            view_lvl3();
            view_lvl4();
            DataTable DDTLVL = new DataTable();
            DDTLVL = Con.Ora_Execute_table("SELECT MC.act_action,MC.act_sts_cd,RS.keterangan,MC.act_crt_id,MC.act_crt_dt FROM mem_complaint_action as MC left join mem_complaint as mco on mco.comp_id=MC.act_comp_id and mco.Acc_sts ='Y' Left Join ref_status_aduan as RS ON MC.act_sts_cd=RS.keterangan_code WHERE MC.act_comp_id='" + statename + "' and MC.act_user_lvl_ind='5'");
            if (DDTLVL.Rows.Count != 0)
            {
                Txttin3.Text = DDTLVL.Rows[0][0].ToString();
                Txttin3.Enabled = true;
                if (DDTLVL.Rows[0][1].ToString() == "S")
                {
                    chkb1.Checked = true;
                }
                //CheckBox1.Checked = DDTLVL.Rows[0][1].ToString();
                string fmdate = DDTLVL.Rows[0][4].ToString();
                Txtcdate3.Text = Convert.ToDateTime(fmdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                Txtcdate3.ReadOnly = true;
               
                    txtnama3.Text = Session["New"].ToString();
               
                txtnama3.ReadOnly = true;

                txtmak1.ReadOnly = true;
                Txttin1.ReadOnly = true;
                DDSTS.Enabled = false;
                Txtcdate.ReadOnly = true;
                Txtnama1.ReadOnly = true;


                //Txtmak2.ReadOnly = true;
                //Txttin2.ReadOnly = true;
                //ddsts1.Enabled = false;

                //txtcdate1.ReadOnly = true;
                //txtnama2.ReadOnly = true;

                //Button4.Visible = false;

            }
            else
            {
                string cdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Txtcdate3.Text = Convert.ToDateTime(cdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                Txtcdate3.ReadOnly = true;
                    txtnama3.Text = Session["New"].ToString();
                    txtnama3.ReadOnly = true;

                txtmak1.ReadOnly = true;
                Txttin1.ReadOnly = true;
                DDSTS.Enabled = false;
                Txtcdate.ReadOnly = true;
                Txtnama1.ReadOnly = true;
                

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    private void gridbind()
    {
        SqlConnection SqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ToString());
        DataTable Dt = new DataTable();

        string UserId = string.Empty;
        try
        {
            if (Session["New"] != null)
            {
                //btnBack.Visible = true;
                //lblHead.Visible = true;

                statename = Request.QueryString["State"];

                //lblHead.Text = " - " + statename;
            }
            else
            {

                //UserId = Session["USER"].ToString();
            }




        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
            if (Txtmak2.Text != "")
            {
                if (Txttin2.Text != "")
                {
                    if (ddsts1.SelectedItem.Text != "Select")
                    {
                        bind();
                    }
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }

    }

    void view_lvl3()
    {
        ddcom = Con.Ora_Execute_table("SELECT MC.act_feedback,MC.act_action,MC.act_sts_cd,RS.keterangan,MC.act_crt_id,MC.act_crt_dt,MC.act_upd_id FROM mem_complaint_action as MC left join mem_complaint as mco on mco.comp_id=MC.act_comp_id and mco.Acc_sts ='Y' Left join ref_status_aduan as RS ON MC.act_sts_cd=RS.keterangan_code WHERE MC.act_comp_id='" + statename + "' and MC.act_user_lvl_ind='4'");
        if (ddcom.Rows.Count != 0)
        {
            txtmak1.Text = ddcom.Rows[0][0].ToString();
            txtmak1.Enabled = true;
            Txttin1.Text = ddcom.Rows[0][1].ToString();
            Txttin1.Enabled = true;
            DDSTS.SelectedValue = ddcom.Rows[0][2].ToString();
            //DDSTS.Enabled = true;
            string fmdate = ddcom.Rows[0][5].ToString();
            Txtcdate.Text = Convert.ToDateTime(fmdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            Txtcdate.Enabled = true;
            // Txtcdate.Text = ddcom.Rows[0][5].ToString();
            if (ddcom.Rows[0]["act_upd_id"].ToString() == "")
            {
                Txtnama1.Text = ddcom.Rows[0][4].ToString();
            }
            else
            {
                Txtnama1.Text = ddcom.Rows[0]["act_upd_id"].ToString();
            }
        }
    }

    void view_lvl4()
    {
        DataTable DDTLVL = new DataTable();
        DDTLVL = Con.Ora_Execute_table("SELECT MC.act_feedback,MC.act_action,MC.act_sts_cd,RS.keterangan,MC.act_crt_id,MC.act_crt_dt,MC.act_upd_id FROM mem_complaint_action as MC left join mem_complaint as mco on mco.comp_id=MC.act_comp_id and mco.Acc_sts ='Y' Left Join ref_status_aduan as RS ON MC.act_sts_cd=RS.keterangan_code WHERE MC.act_comp_id='" + statename + "' and MC.act_user_lvl_ind='5'");
        if (DDTLVL.Rows.Count != 0)
        {
            Txtmak2.Text = DDTLVL.Rows[0][0].ToString();
            //Txtmak2.Enabled = false;
            Txttin2.Text = DDTLVL.Rows[0][1].ToString();
            //Txttin2.Enabled = false;
            ddsts1.SelectedValue = DDTLVL.Rows[0][2].ToString();
            //ddsts1.Enabled = false;
            string fmdate = DDTLVL.Rows[0][5].ToString();
            Txtcdate.Text = Convert.ToDateTime(fmdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (DDTLVL.Rows[0]["act_upd_id"].ToString() == "")
            {
                txtnama2.Text = DDTLVL.Rows[0][4].ToString();
            }
            else
            {
                txtnama2.Text = DDTLVL.Rows[0]["act_upd_id"].ToString();
            }

        }
    }

    void view_lvl5()
    {
        DataTable DDTLVL = new DataTable();
        DDTLVL = Con.Ora_Execute_table("SELECT MC.act_action,MC.act_sts_cd,RS.keterangan,MC.act_crt_id,MC.act_crt_dt,MC.act_upd_id FROM mem_complaint_action as MC left join mem_complaint as mco on mco.comp_id=MC.act_comp_id and mco.Acc_sts ='Y' Left Join ref_status_aduan as RS ON MC.act_sts_cd=RS.keterangan_code WHERE MC.act_comp_id='" + statename + "' and MC.act_user_lvl_ind='5'");
        if (DDTLVL.Rows.Count != 0)
        {
            Txttin3.Text = DDTLVL.Rows[0][0].ToString();
            Txttin3.Enabled = true;
            if (DDTLVL.Rows[0][1].ToString() == "S")
            {
                chkb1.Checked = true;
            }
            //CheckBox1.Checked = DDTLVL.Rows[0][1].ToString();
            string fmdate = DDTLVL.Rows[0][4].ToString();
            Txtcdate3.Text = Convert.ToDateTime(fmdate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            Txtcdate3.ReadOnly = true;
            if (DDTLVL.Rows[0]["act_upd_id"].ToString() == "")
            {
                txtnama3.Text = DDTLVL.Rows[0]["act_crt_id"].ToString();
            }
            else
            {
                txtnama3.Text = DDTLVL.Rows[0]["act_upd_id"].ToString();
            }

            txtnama3.ReadOnly = true;
        }
        else
        {
            txtnama3.Text = Session["New"].ToString();
            txtnama3.ReadOnly = true;
        }
    }


    void clear()
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("../keanggotan/Mak_Dan_Tin_Adu_kj.aspx");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("../keanggotan/Mak_Dan_Tin_Adu_kj.aspx");
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        //Path
        string c1;
        if (chkb1.Checked == true)
        {
            c1 = "Selesai";
        }
        else
        {
            c1 = "";
        }

        ReportDataSource rds = new ReportDataSource();

        RptviwerStudent.LocalReport.DataSources.Add(rds);

        RptviwerStudent.LocalReport.ReportPath = "keanggotan/aduan.rdlc";
        //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

        string kat1 = string.Empty;
        if (dd_kat.SelectedValue != "")
        {
            kat1 = dd_kat.SelectedValue;
        }
        else
        {
            kat1 = "";
        }
        //Parameters
        ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("Txtdate",Txtdate .Text, false )  ,
                      new ReportParameter("Txtkat",Txtkat .Text , false)  ,
                       new ReportParameter("txtsts",txtsts .Text , false)  ,
                        new ReportParameter("txtcaw",txtcaw .Text , false)  ,
                        new ReportParameter("Txtpen",Txtpen .Text , false)  ,
                         new ReportParameter("txtdesc",txtdesc .Text, false )  ,
                          new ReportParameter("txtmak1",txtmak1 .Text, false )  ,
                          new ReportParameter("Txttin1",Txttin1 .Text, false )  ,
                          new ReportParameter("DDSTS",DDSTS.SelectedItem.Text , false)  ,
                          new ReportParameter("Txtcdate",Txtcdate .Text, false )  ,
                          new ReportParameter("Txtnama1",Txtnama1 .Text, false )  ,
                          new ReportParameter("Txtmak2",Txtmak2 .Text, false )  ,
                          new ReportParameter("Txttin2",Txttin2 .Text, false )  ,
                          new ReportParameter("ddsts1",ddsts1.SelectedItem .Text, false )  ,
                          new ReportParameter("txtcdate1",txtcdate1 .Text, false )  ,
                          new ReportParameter("txtnama2",txtnama2 .Text, false )  ,
                            new ReportParameter("Txttin3",Txttin3 .Text,false ),
                              new ReportParameter("chkb1",c1,false ),
                           new ReportParameter("Txtcdate3",Txtcdate3.Text,false ),
                           new ReportParameter("txtnama3",txtnama3.Text,false ),

                            new ReportParameter("ss1",Txt_telno.Text,false ),
                             new ReportParameter("ss2",kat1,false )

                     };


        RptviwerStudent.LocalReport.SetParameters(rptParams);

        //Refresh
        RptviwerStudent.LocalReport.Refresh();
        Warning[] warnings;

        string[] streamids;

        string mimeType;

        string encoding;

        string extension;

        string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
               "  <PageWidth>12.20in</PageWidth>" +
                "  <PageHeight>8.27in</PageHeight>" +
                "  <MarginTop>0.1in</MarginTop>" +
                "  <MarginLeft>0.5in</MarginLeft>" +
                 "  <MarginRight>0in</MarginRight>" +
                 "  <MarginBottom>0in</MarginBottom>" +
               "</DeviceInfo>";

        byte[] bytes = RptviwerStudent.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


        Response.Buffer = true;

        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "inline; filename=myfile." + extension);

        Response.BinaryWrite(bytes);

        //Response.Write("<script>");
        //Response.Write("window.open('', '_newtab');");
        //Response.Write("</script>");
        Response.Flush();

        Response.End();
    }
}