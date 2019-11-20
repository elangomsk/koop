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
using System.Data.OleDb;
using System.IO;
using System.Net;

public partial class Ast_pak_aset : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection dbcon = new DBConnection();
    StudentWebService service = new StudentWebService();

    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string qr1 = string.Empty;
    string cde = string.Empty, qry1 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
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

                TextBox9.Attributes.Add("Readonly", "Readonly");
                TextBox10.Attributes.Add("Readonly", "Readonly");
                TextBox11.Attributes.Add("Readonly", "Readonly");
                TextBox12.Attributes.Add("Readonly", "Readonly");
                //TextBox8.Attributes.Add("Readonly", "Readonly");
                //TextBox9.Attributes.Add("Readonly", "Readonly");
                TextBox1.Attributes.Add("Readonly", "Readonly");
                TextBox5.Text = DateTime.Now.ToString("hh:mm:ss");
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                    get_id.Text = "1";

                }
                else
                {
                    get_id.Text = "0";
                }
                useid = Session["New"].ToString();
               
             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

   

    void view_details()
    {
        try
        {
            DataTable ddicno_astid = new DataTable();
            ddicno_astid = dbcon.Ora_Execute_table("select lst_cmplnt_id,case when FORMAT(lst_cmplnt_dt,'dd/MM/yyyy', 'en-us') = '01/01/1900' then '' else FORMAT(lst_cmplnt_dt,'dd/MM/yyyy', 'en-us') end as lst_cmplnt_dt,case when FORMAT(lst_incident_dt,'dd/MM/yyyy', 'en-us') = '01/01/1900' then '' else FORMAT(lst_incident_dt,'dd/MM/yyyy', 'en-us') end as lst_incident_dt,lst_incident_time,lst_incident_loc,lst_police_rpt_no,case when FORMAT(lst_police_rpt_dt,'dd/MM/yyyy', 'en-us') = '01/01/1900' then '' else FORMAT(lst_police_rpt_dt,'dd/MM/yyyy', 'en-us') end as lst_police_rpt_dt,lst_remark,lst_suspect,lst_police_rpt_doc from ast_lost where lst_cmplnt_id='" + lbl_name.Text + "'");
            if (ddicno_astid.Rows.Count != 0)
            {
                Button4.Text = "Kemaskini";
                TextBox1.Text = ddicno_astid.Rows[0]["lst_cmplnt_id"].ToString();
                TextBox2.Text = ddicno_astid.Rows[0]["lst_cmplnt_dt"].ToString();
                TextBox3.Text = ddicno_astid.Rows[0]["lst_incident_dt"].ToString();
                TextBox5.Text = ddicno_astid.Rows[0]["lst_incident_time"].ToString();
                TextBox4.Text = ddicno_astid.Rows[0]["lst_incident_loc"].ToString();
                TextBox6.Text = ddicno_astid.Rows[0]["lst_police_rpt_no"].ToString();
                txt_ar1.Value = ddicno_astid.Rows[0]["lst_remark"].ToString();
                TextBox8.Text = ddicno_astid.Rows[0]["lst_police_rpt_dt"].ToString();
                TextBox7.Text = ddicno_astid.Rows[0]["lst_suspect"].ToString();
                TextBox13.Text = ddicno_astid.Rows[0]["lst_police_rpt_doc"].ToString();
            }
        }
        catch
        {

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
                ddbind = dbcon.Ora_Execute_table("select hs.stf_staff_no,hs.stf_name,rj.jawatan_desc,ho.org_name,rl.loc_desc from hr_staff_profile as hs left join ast_staff_asset as asa on asa.sas_staff_no=hs.stf_staff_no left join ref_location as rl on rl.loc_cd = asa.sas_location_cd left join hr_organization as ho on ho.org_gen_id=hs.str_curr_org_cd left join ref_jawatan as rj on rj.Jawatan_Code=hs.stf_curr_post_cd where hs.stf_staff_no='" + stffno + "'");

                TextBox9.Text = ddbind.Rows[0]["stf_name"].ToString();
                TextBox10.Text = ddbind.Rows[0]["jawatan_desc"].ToString();
                TextBox11.Text = ddbind.Rows[0]["org_name"].ToString();
                TextBox12.Text = ddbind.Rows[0]["loc_desc"].ToString();
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

    protected void Group1_CheckedChanged(Object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvSelected.Rows)
        {
            System.Web.UI.WebControls.RadioButton rbn = new System.Web.UI.WebControls.RadioButton();
            rbn = (System.Web.UI.WebControls.RadioButton)row.FindControl("RadioButton1");
            if (rbn.Checked)
            {
                string ast_id = ((System.Web.UI.WebControls.Label)row.FindControl("Label8")).Text.ToString(); //this store the  value in varName1
                DataTable ddicno_astid = new DataTable();
                ddicno_astid = dbcon.Ora_Execute_table("select lst_cmplnt_id,case when FORMAT(lst_cmplnt_dt,'dd/MM/yyyy', 'en-us') = '01/01/1900' then '' else FORMAT(lst_cmplnt_dt,'dd/MM/yyyy', 'en-us') end as lst_cmplnt_dt,case when FORMAT(lst_incident_dt,'dd/MM/yyyy', 'en-us') = '01/01/1900' then '' else FORMAT(lst_incident_dt,'dd/MM/yyyy', 'en-us') end as lst_incident_dt,lst_incident_time,lst_incident_loc,lst_police_rpt_no,case when FORMAT(lst_police_rpt_dt,'dd/MM/yyyy', 'en-us') = '01/01/1900' then '' else FORMAT(lst_police_rpt_dt,'dd/MM/yyyy', 'en-us') end as lst_police_rpt_dt,lst_remark,lst_suspect,lst_police_rpt_doc from ast_lost where lst_staff_no='" + Session["New"].ToString() + "' and lst_asset_id='" + ast_id + "'");
                if (ddicno_astid.Rows.Count != 0)
                {
                    Button4.Text = "Kemaskini";
                    TextBox1.Text = ddicno_astid.Rows[0]["lst_cmplnt_id"].ToString();
                    TextBox2.Text = ddicno_astid.Rows[0]["lst_cmplnt_dt"].ToString();
                    TextBox3.Text = ddicno_astid.Rows[0]["lst_incident_dt"].ToString();
                    TextBox5.Text = ddicno_astid.Rows[0]["lst_incident_time"].ToString();
                    TextBox4.Text = ddicno_astid.Rows[0]["lst_incident_loc"].ToString();
                    TextBox6.Text = ddicno_astid.Rows[0]["lst_police_rpt_no"].ToString();
                    txt_ar1.Value = ddicno_astid.Rows[0]["lst_remark"].ToString();
                    TextBox8.Text = ddicno_astid.Rows[0]["lst_police_rpt_dt"].ToString();
                    TextBox7.Text = ddicno_astid.Rows[0]["lst_suspect"].ToString();
                    TextBox13.Text = ddicno_astid.Rows[0]["lst_police_rpt_doc"].ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    Button4.Text = "Simpan";
                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox5.Text = DateTime.Now.ToString("hh:mm:ss");
                    TextBox4.Text = "";
                    TextBox6.Text = "";
                    txt_ar1.Value = "";
                    TextBox8.Text = "";
                    TextBox7.Text = "";
                }
            }
        }
    }


    protected void BindGrid()
    {
        SqlCommand cmd2 = new SqlCommand("select ak.ast_kategori_desc,ask.ast_subkateast_desc,ja.ast_jeniaset_desc,ca.cas_asset_desc,sa.sas_asset_id from ast_staff_asset as sa left join Ref_ast_kategori as ak on ak.ast_kategori_code=sa.sas_asset_cat_cd left join Ref_ast_sub_kategri_Aset as ask on ask.ast_subkateast_Code=sa.sas_asset_sub_cat_cd left join Ref_ast_jenis_aset as ja on ja.ast_jeniaset_code=sa.sas_asset_type_cd left join ast_cmn_asset as ca on ca.cas_asset_cd=sa.sas_asset_cd where sa.flag_set ='0' and sas_staff_no='" + Session["New"].ToString() + "'", con);
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

    protected void gv_refdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        BindGrid();
    }

 
    protected void insert_values(object sender, EventArgs e)
    {
       
        string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty;
        string c_dt = string.Empty, i_dt = string.Empty, p_dt = string.Empty;
        DataTable ddicno1 = new DataTable();
        ddicno1 = dbcon.Ora_Execute_table("select stf_staff_no From hr_staff_profile where stf_staff_no='" + Session["New"].ToString() + "' ");
        if (ddicno1.Rows.Count != 0)
        {
            DataTable ddicno = new DataTable();
            ddicno = dbcon.Ora_Execute_table("select count(*) as cnt from ast_lost");
            string cc_cnt = string.Empty;

            if (ddicno.Rows[0]["cnt"].ToString() == "0")
            {
                cc_cnt = "00001";
            }
            else
            {
                string count = (double.Parse(ddicno.Rows[0]["cnt"].ToString()) + 1).ToString();
                cc_cnt = count.PadLeft(5, '0');
            }
            if (TextBox2.Text != "")
            {
                DateTime cd = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                c_dt = cd.ToString("yyyy-MM-dd");
            }
            else
            {
                c_dt = "";
            }
            if (TextBox3.Text != "")
            {
                DateTime td = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                i_dt = td.ToString("yyyy-MM-dd");
            }
            else
            {
                i_dt = "";
            }

            if (TextBox8.Text != "")
            {
                DateTime pd = DateTime.ParseExact(TextBox8.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                p_dt = pd.ToString("yyyy-MM-dd");
            }
            else
            {
                p_dt = "";
            }

            if (get_id.Text == "0")
            {

                string rcount = string.Empty;
                int count1 = 0;
                foreach (GridViewRow gvrow in gvSelected.Rows)
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
                    int contentLength = FileUpload1.PostedFile.ContentLength;//You may need it for validation
                    string contentType = FileUpload1.PostedFile.ContentType;//You may need it for validation
                    string fileName = FileUpload1.PostedFile.FileName;

                    string fname = string.Empty;
                    if (fileName != "")
                    {
                        string[] commandArgs = fileName.ToString().Split(new char[] { '.' });
                        CommandArgument1 = commandArgs[0];
                        CommandArgument2 = commandArgs[1];
                        string Oname = CommandArgument1 + "_" + DateTime.Now.ToString("HHmmss") + "." + CommandArgument2;
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/AST_polis/" + Oname));//Or code to save in the DataBase.
                        fname = Oname;
                    }
                    foreach (GridViewRow row in gvSelected.Rows)
                    {
                        System.Web.UI.WebControls.RadioButton rbn = new System.Web.UI.WebControls.RadioButton();
                        rbn = (System.Web.UI.WebControls.RadioButton)row.FindControl("RadioButton1");
                        if (rbn.Checked)
                        {
                            string ast_id = ((System.Web.UI.WebControls.Label)row.FindControl("Label8")).Text.ToString(); //this store the  value in varName1
                            DataTable ddicno_astid = new DataTable();
                            ddicno_astid = dbcon.Ora_Execute_table("select lst_cmplnt_id,lst_police_rpt_doc From ast_lost where lst_cmplnt_id='" + TextBox1.Text + "'");
                            if (ddicno_astid.Rows.Count == 0)
                            {

                                // insrt Query
                                string Inssql1 = "INSERT INTO ast_lost (lst_cmplnt_id,lst_staff_no,lst_asset_id,lst_cmplnt_dt,lst_incident_dt,lst_incident_time,lst_incident_loc,lst_police_rpt_no,lst_police_rpt_dt,lst_remark,lst_suspect,lst_verify_ind,lst_crt_id,lst_crt_dt,lst_police_rpt_doc) VALUES ('" + cc_cnt.ToString() + "','" + Session["New"].ToString() + "','" + ast_id.ToString() + "','" + c_dt.ToString() + "','" + i_dt.ToString() + "','" + TextBox5.Text + "','" + TextBox4.Text + "','" + TextBox6.Text + "','" + p_dt + "','" + txt_ar1.Value.Replace("'", "''") + "','" + TextBox7.Text + "','N','" + Session["New"].ToString() + "','" + DateTime.Now + "','" + fname + "')";
                                Status = dbcon.Ora_Execute_CommamdText(Inssql1);
                                if (Status == "SUCCESS")
                                {
                                    Session["validate_success"] = "SUCCESS";
                                    Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                                    Response.Redirect("../Aset/Ast_pak_aset_view.aspx");
                                    //BindGrid();
                                }
                            }
                            else
                            {
                                string ssnme = string.Empty;
                                if (fname == "")
                                {
                                    ssnme = ddicno_astid.Rows[0]["lst_police_rpt_doc"].ToString();
                                }
                                else
                                {
                                    ssnme = fname;
                                }

                                string Inssql1 = "UPDATE ast_lost set lst_staff_no='" + Session["New"].ToString() + "',lst_cmplnt_dt='" + c_dt.ToString() + "',lst_incident_dt='" + i_dt.ToString() + "',lst_incident_time='" + DateTime.Now.ToString("hh:mm:ss") + "',lst_incident_loc='" + TextBox4.Text + "',lst_police_rpt_no='" + TextBox6.Text + "',lst_police_rpt_dt='" + p_dt + "',lst_remark='" + txt_ar1.Value.Replace("'", "''") + "',lst_suspect='" + TextBox7.Text + "',lst_verify_ind='N',lst_upd_id='" + Session["New"].ToString() + "',lst_upd_dt='" + DateTime.Now + "',lst_police_rpt_doc='" + ssnme + "' where lst_cmplnt_id='" + TextBox1.Text + "'";
                                Status = dbcon.Ora_Execute_CommamdText(Inssql1);
                                if (Status == "SUCCESS")
                                {
                                    Session["validate_success"] = "SUCCESS";
                                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                                    Response.Redirect("../Aset/Ast_pak_aset_view.aspx");
                                    //BindGrid();
                                }
                            }
                        }

                    }


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                DataTable ddicno_astid = new DataTable();
                ddicno_astid = dbcon.Ora_Execute_table("select lst_cmplnt_id,lst_police_rpt_doc From ast_lost where lst_cmplnt_id='" + lbl_name.Text + "'");
                int contentLength = FileUpload1.PostedFile.ContentLength;//You may need it for validation
                string contentType = FileUpload1.PostedFile.ContentType;//You may need it for validation
                string fileName = FileUpload1.PostedFile.FileName;

                string fname = string.Empty;
                if (fileName != "")
                {
                    string[] commandArgs = fileName.ToString().Split(new char[] { '.' });
                    CommandArgument1 = commandArgs[0];
                    CommandArgument2 = commandArgs[1];
                    string Oname = CommandArgument1 + "_" + DateTime.Now.ToString("HHmmss") + "." + CommandArgument2;
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/AST_polis/" + Oname));//Or code to save in the DataBase.
                    fname = Oname;
                }
                string ssnme = string.Empty;
                if (fname == "")
                {
                    ssnme = ddicno_astid.Rows[0]["lst_police_rpt_doc"].ToString();
                }
                else
                {
                    ssnme = fname;
                }

                string Inssql1 = "UPDATE ast_lost set lst_staff_no='" + Session["New"].ToString() + "',lst_cmplnt_dt='" + c_dt.ToString() + "',lst_incident_dt='" + i_dt.ToString() + "',lst_incident_time='" + DateTime.Now.ToString("hh:mm:ss") + "',lst_incident_loc='" + TextBox4.Text + "',lst_police_rpt_no='" + TextBox6.Text + "',lst_police_rpt_dt='" + p_dt + "',lst_remark='" + txt_ar1.Value.Replace("'", "''") + "',lst_suspect='" + TextBox7.Text + "',lst_verify_ind='N',lst_upd_id='" + Session["New"].ToString() + "',lst_upd_dt='" + DateTime.Now + "',lst_police_rpt_doc='" + ssnme + "' where lst_cmplnt_id='" + TextBox1.Text + "'";
                Status = dbcon.Ora_Execute_CommamdText(Inssql1);
                if (Status == "SUCCESS")
                {
                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("../Aset/Ast_pak_aset_view.aspx");
                    //BindGrid();
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Pengguna Tidak Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }



    protected void DownloadFile(object sender, EventArgs e)
    {
        if (TextBox13.Text != "")
        {
            string filePath = Server.MapPath("~/FILES/AST_polis/" + TextBox13.Text);

            string[] commandArgs = TextBox13.Text.ToString().Split(new char[] { '.' });

            string val1 = commandArgs[0];
            string val2 = commandArgs[1];
            string ext = string.Empty;
            if (val2 == "pdf")
            {
                ext = "Application/pdf";
            }
            else if (val2 == "doc" || val2 == "docx" || val2 == "dotx" || val2 == "dot" || val2 == "dotm")
            {
                ext = "Application/msword";
            }
            else if (val2 == "txt" || val2 == "rtf")
            {
                ext = "Text/Plain";
            }
            //Response.ContentType = "application/doc";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            //Response.WriteFile(filePath);
            //Response.End();

            byte[] bytesPDF = System.IO.File.ReadAllBytes(@"" + filePath + "");

            if (bytesPDF != null)
            {

                Response.AddHeader("content-disposition", "attachment;filename= " + TextBox13.Text + "");
                Response.ContentType = "" + ext + "";
                Response.BinaryWrite(bytesPDF);
                Response.End();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Fail Tidak Turun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_pak_aset.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_pak_aset_view.aspx");
    }

    
}