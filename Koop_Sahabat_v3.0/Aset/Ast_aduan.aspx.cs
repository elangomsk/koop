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

public partial class Ast_aduan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection dbcon = new DBConnection();
    StudentWebService service = new StudentWebService();

    private static int PageSize = 20;
    string qry1 = string.Empty, qry2 = string.Empty;
    string level;
    string Status = string.Empty, Status1 = string.Empty;
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                pg_load();
                BindGrid1();
                TextBox4.Attributes.Add("Readonly", "Readonly");
                TextBox5.Attributes.Add("Readonly", "Readonly");
                TextBox6.Attributes.Add("Readonly", "Readonly");
                TextBox7.Attributes.Add("Readonly", "Readonly");
                TextBox8.Attributes.Add("Readonly", "Readonly");
                TextBox9.Attributes.Add("Readonly", "Readonly");
                if (samp != "")
                {
                    //lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                    //ver_id.Text = "1";

                }
             
                userid = Session["New"].ToString();
               
             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

  
    void view_details()
    {
        //Button1.Visible = false;
        try
        {
           
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void pg_load()
    {
        DataTable ddicno = new DataTable();
        ddicno = dbcon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "' ");
        if (ddicno.Rows.Count != 0)
        {
            string stffno = ddicno.Rows[0]["stf_staff_no"].ToString();
            if (ddicno.Rows.Count > 0)
            {
                DataTable ddbind = new DataTable();
                ddbind = dbcon.Ora_Execute_table("select stf_staff_no,stf_name,org_name,hr_jaba_desc,hr_jaw_desc From hr_staff_profile as SP left join Ref_hr_jabatan as jb on jb.hr_jaba_Code=SP.stf_curr_dept_cd left join Ref_hr_Jawatan as JW on JW.hr_jaw_Code=SP.stf_curr_post_cd left join hr_organization as ho on ho.org_gen_id=SP.str_curr_org_cd  where stf_staff_no='" + stffno + "'");

                TextBox4.Text = ddbind.Rows[0]["stf_staff_no"].ToString();
                TextBox6.Text = ddbind.Rows[0]["stf_name"].ToString();
                TextBox5.Text = ddbind.Rows[0]["org_name"].ToString();
                TextBox7.Text = ddbind.Rows[0]["hr_jaba_desc"].ToString();
                TextBox8.Text = ddbind.Rows[0]["hr_jaw_desc"].ToString();
                TextBox9.Text = DateTime.Now.ToString("dd/MM/yyyy");
                BindGrid();
            }
            else
            {
                BindGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            BindGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Pengguna Tidak Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        BindGrid();
        BindGrid1();
    }

    protected void gv_refdata_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid();
        BindGrid1();
    }


    protected void BindGrid()
    {
        SqlCommand cmd2 = new SqlCommand("select cmp_id,cmp_complaint_dt,acs.cas_asset_desc,cmp_remark,case when cmp_sts_cd = '01'  then 'BARU' when cmp_sts_cd= '02' then 'SEDANG DISELENGGARA' when cmp_sts_cd= '03' then 'SELESAI' when cmp_sts_cd= '04' then 'RUJUK PENYELIA' end as ss1,cmp_asset_id from ast_complaint as ac left join ast_cmn_asset as acs on acs.cas_asset_cd=ac.cmp_asset_name where cmp_staff_no='" + Session["New"].ToString() + "'", con);
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

    protected void BindGrid1()
    {
        SqlCommand cmd2 = new SqlCommand("select ak.ast_kategori_desc,ask.ast_subkateast_desc,ja.ast_jeniaset_desc,ca.cas_asset_desc,sa.sas_asset_id,sa.sas_asset_cat_cd,sa.sas_asset_sub_cat_cd,sa.sas_asset_type_cd,sa.sas_asset_cd from ast_staff_asset as sa left join Ref_ast_kategori as ak on ak.ast_kategori_code=sa.sas_asset_cat_cd left join Ref_ast_sub_kategri_Aset as ask on ask.ast_subkateast_Code=sa.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset as ja on ja.ast_jeniaset_code=sa.sas_asset_type_cd left join ast_cmn_asset as ca on ca.cas_asset_cd=sa.sas_asset_cd where sa.flag_set ='0' and sas_staff_no='" + Session["New"].ToString() + "'", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);

        if (ds2.Tables[0].Rows.Count == 0)
        {
            ds2.Tables[0].Rows.Add(ds2.Tables[0].NewRow());
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
        }
        else
        {
            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }


    }

    protected void insert_values(object sender, EventArgs e)
    {
        try
        {
            if (txt_area.Value != "")
            {
                DataTable ddicno1 = new DataTable();
                ddicno1 = dbcon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "' ");
                if (ddicno1.Rows.Count != 0)
                {
                    DataTable ddicno = new DataTable();
                    ddicno = dbcon.Ora_Execute_table("select count(*) as cnt from ast_complaint");
                    string cc_cnt = string.Empty;
                    //String.Format("{0:0000}", cc_cnt);
                    if (ddicno.Rows[0]["cnt"].ToString() == "0")
                    {
                        cc_cnt = "1";
                    }
                    else
                    {
                        cc_cnt = (double.Parse(ddicno.Rows[0]["cnt"].ToString()) + 1).ToString();
                    }

                    DateTime fd = DateTime.ParseExact(TextBox9.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    String cmp_dt = fd.ToString("yyyy-MM-dd");

                    string rcount = string.Empty;
                    int count1 = 0;
                    foreach (GridViewRow gvrow in GridView1.Rows)
                    {
                        var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.RadioButton;
                        if (rb.Checked)
                        {
                            count1++;
                        }
                        rcount = count1.ToString();
                    }
                    if (rcount != "0")
                    {
                        if (txt_area.Value != "")
                        {
                            foreach (GridViewRow row in GridView1.Rows)
                            {
                                System.Web.UI.WebControls.RadioButton rbn = new System.Web.UI.WebControls.RadioButton();
                                rbn = (System.Web.UI.WebControls.RadioButton)row.FindControl("RadioButton1");
                                if (rbn.Checked)
                                {
                                    string ast_id = ((System.Web.UI.WebControls.Label)row.FindControl("Label8")).Text.ToString(); //this store the  value in varName1
                                    string catid = ((System.Web.UI.WebControls.Label)row.FindControl("Label9")).Text.ToString(); //this store the  value in varName1
                                    string scatid = ((System.Web.UI.WebControls.Label)row.FindControl("Label1")).Text.ToString(); //this store the  value in varName1
                                    string jaid = ((System.Web.UI.WebControls.Label)row.FindControl("Label4")).Text.ToString(); //this store the  value in varName1
                                    string naid = ((System.Web.UI.WebControls.Label)row.FindControl("Label5")).Text.ToString(); //this store the  value in varName1
                                    dbcon.Execute_CommamdText("INSERT INTO ast_complaint (cmp_id,cmp_staff_no,cmp_asset_id,cmp_complaint_dt,cmp_cat_cd,cmp_sub_cat_cd,cmp_type_cd,cmp_asset_name,cmp_remark,cmp_sts_cd,cmp_eft_dt,cmp_crt_id,cmp_crt_dt) VALUES ('" + cc_cnt + "','" + Session["New"].ToString() + "','" + ast_id + "','" + cmp_dt + "','" + catid + "','" + scatid + "','" + jaid + "','" + naid + "','" + txt_area.Value + "','01','1900-01-01','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')");
                                }

                            }
                            txt_area.Value = "";
                            BindGrid1();
                            pg_load();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        }
                        else
                        {
                            BindGrid();
                            //BindGrid1();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Aduan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        BindGrid();
                        BindGrid1();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    BindGrid();
                    BindGrid1();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Pengguna Tidak Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                
                BindGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch
        {
            BindGrid();
            BindGrid1();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Please veryfiy the input values!.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_aduan.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_aduan_view.aspx");
    }

    
}