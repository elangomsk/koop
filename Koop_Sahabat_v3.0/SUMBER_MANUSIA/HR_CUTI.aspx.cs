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
using System.Threading;

public partial class HR_CUTI : System.Web.UI.Page
{

    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string level, userid;
    string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty;
    string get_ngri = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                hm.Attributes.Add("style", "display:none");
                cl.Attributes.Add("style", "display:none");
                M_title.Text = "&nbsp;";
                NegriBind();
                OrgBind();
                JcutiBind();
                chs_year();
                grid();
                if (samp != "")
                {
                    view_details();

                }
                else
                {

                }
                userid = Session["New"].ToString();


            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void app_language()
    {
        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('472','448','1538','1491','473','29','1539','1080','61','15','883','36')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;

            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());

            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());

            h3_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());

            lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            lbl2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            lbl3_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            lbl4_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());

            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void view_details()
    {

    }



    void NegriBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select DISTINCT hr_negeri_Code, UPPER(hr_negeri_desc) as hr_negeri_desc from Ref_hr_negeri  WHERE Status = 'A' order by hr_negeri_desc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_NegriBind1.DataSource = dt;
            DD_NegriBind1.DataTextField = "hr_negeri_desc";
            DD_NegriBind1.DataValueField = "hr_negeri_Code";
            DD_NegriBind1.DataBind();
            DD_NegriBind1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void OrgBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select DISTINCT org_gen_id, UPPER(org_name) as org_name from hr_organization  order by org_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_org.DataSource = dt;
            dd_org.DataTextField = "org_name";
            dd_org.DataValueField = "org_gen_id";
            dd_org.DataBind();
            dd_org.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void JcutiBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select hr_jenis_Code,UPPER(hr_jenis_desc) as hr_jenis_desc from Ref_hr_jenis_cuti WHERE Status = 'A' and hr_jenis_Code IN ('13','16') ORDER BY hr_jenis_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_jcuti.DataSource = dt;
            dd_jcuti.DataTextField = "hr_jenis_desc";
            dd_jcuti.DataValueField = "hr_jenis_Code";
            dd_jcuti.DataBind();
            dd_jcuti.Items.Insert(0, new ListItem("--- PILIH ---", ""));


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void chs_year()
    {
        DataSet Ds = new DataSet();
        try
        {
            var currentYear = DateTime.Today.Year;
            for (int i = 0; i <= 1; i++)
            {
                // Now just add an entry that's the current year minus the counter
                dd_tahun.Items.Add((currentYear + i).ToString());
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void sel_negeri(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {
            if (dd_org.SelectedValue != "")
            {
               
                DataTable sel_organ = new DataTable();
                sel_organ = DBCon.Ora_Execute_table("SELECT STUFF ((SELECT ',' + op_state_cd  FROM hr_organization_pern where op_org_id='" + dd_org.SelectedValue + "' FOR XML PATH ('')  ),1,1,'')  as scode");

                string com = "select DISTINCT hr_negeri_Code, UPPER(hr_negeri_desc) as hr_negeri_desc from Ref_hr_negeri  WHERE Status = 'A' and hr_negeri_Code IN (" + sel_organ.Rows[0]["scode"] + ") order by hr_negeri_desc";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                DD_NegriBind1.DataSource = dt;
                DD_NegriBind1.DataTextField = "hr_negeri_desc";
                DD_NegriBind1.DataValueField = "hr_negeri_Code";
                DD_NegriBind1.DataBind();
                DD_NegriBind1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
                if (dd_jcuti.SelectedValue != "")
                {
                    sow_cnt.Attributes.Remove("Style");
                }
                grid();
            }
            else
            {
                grid();
                NegriBind();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void clk_paper(object sender, EventArgs e)
    {
        try
        {
            if (dd_org.SelectedItem.Value != "" || dd_jcuti.SelectedItem.Value != "" || DD_NegriBind1.SelectedItem.Value != "")
            {
                grid();
            }
            else
            {
                grid();                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Maklumat Cuti.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void grid()
    {
        sel_dd();

        if (dd_jcuti.SelectedValue != "")
        {
            sow_cnt.Attributes.Remove("Style");
            cl_simp.Attributes.Remove("Style");
        }

        SqlCommand cmd2 = new SqlCommand("select hol_gen_id,hol_org_id,hol_state_cd,hol_holiday_cd,FORMAT(hol_dt,'dd/MM/yyyy', 'en-us') as hol_dt,hol_remark,hro.org_name,rn.hr_negeri_desc,jc.hr_jenis_desc from hr_holiday as hrh left join hr_organization as hro on hro.org_gen_ID= hrh.hol_gen_id left join Ref_hr_jenis_cuti as jc on jc.hr_jenis_Code=hrh.hol_holiday_cd left join Ref_hr_negeri as rn on rn.hr_negeri_Code=hrh.hol_state_cd " + val1 + " " + val2 + " " + val3 + " order by hol_org_id,hol_state_cd,cast(hol_dt as datetime)", con);
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
            Button2.Visible = false;
            Button5.Visible = false;
        }
        else
        {
            Button2.Visible = true;
            Button5.Visible = true;
            GridView1.DataSource = ds2;
            GridView1.DataBind();
        }
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();

    }

    void sel_dd()
    {
        if (dd_org.SelectedValue != "" && dd_jcuti.SelectedValue == "" && DD_NegriBind1.SelectedValue == "")
        {
            val1 = " where hol_cancel_ind='N' and hol_gen_id='" + dd_org.SelectedValue + "'";
            val2 = "";
            val3 = "";
        }
        else if (dd_org.SelectedValue == "" && dd_jcuti.SelectedValue != "" && DD_NegriBind1.SelectedValue == "")
        {
            val2 = " where hol_cancel_ind='N' and hol_holiday_cd='" + dd_jcuti.SelectedValue + "'";
            val1 = "";
            val3 = "";

        }
        else if (dd_org.SelectedValue == "" && dd_jcuti.SelectedValue == "" && DD_NegriBind1.SelectedValue != "")
        {
            val3 = " where hol_cancel_ind='N' and hol_state_cd='" + DD_NegriBind1.SelectedValue + "'";
            val2 = "";
            val1 = "";

        }
        else if (dd_org.SelectedValue != "" && dd_jcuti.SelectedValue != "" && DD_NegriBind1.SelectedValue == "")
        {
            val1 = " where hol_cancel_ind='N' and hol_gen_id='" + dd_org.SelectedValue + "'";
            val2 = " and hol_holiday_cd='" + dd_jcuti.SelectedValue + "'";
            val3 = "";

        }
        else if (dd_org.SelectedValue == "" && dd_jcuti.SelectedValue != "" && DD_NegriBind1.SelectedValue != "")
        {
            val2 = " where hol_cancel_ind='N' and hol_holiday_cd='" + dd_jcuti.SelectedValue + "'";
            val3 = "and hol_state_cd='" + DD_NegriBind1.SelectedValue + "'";
            val1 = "";

        }
        else if (dd_org.SelectedValue != "" && dd_jcuti.SelectedValue == "" && DD_NegriBind1.SelectedValue != "")
        {
            val1 = " where hol_cancel_ind='N' and hol_gen_id='" + dd_org.SelectedValue + "'";
            val3 = "and hol_state_cd='" + DD_NegriBind1.SelectedValue + "'";
            val2 = "";

        }
        else if (dd_org.SelectedValue != "" && dd_jcuti.SelectedValue != "" && DD_NegriBind1.SelectedValue != "")
        {
            val1 = " where hol_cancel_ind='N' and hol_gen_id='" + dd_org.SelectedValue + "'";
            val2 = "and hol_holiday_cd='" + dd_jcuti.SelectedValue + "'";
            val3 = "and hol_state_cd='" + DD_NegriBind1.SelectedValue + "'";

        }
        else
        {
            val1 = " where hol_cancel_ind='' and hol_gen_id=''";
        }
    }

    protected void clk_insert(object sender, EventArgs e)
    {
        try
        {
            if (dd_jcuti.SelectedValue != "")
            {
                if (dd_org.SelectedItem.Value != "" || dd_jcuti.SelectedItem.Value != "" || DD_NegriBind1.SelectedItem.Value != "")
                {
                    if (ver_id.Text == "1")
                    {
                        if (td_date.Text != "" && ts_date.Text != "")
                        {
                            string month = string.Empty;
                            string year = string.Empty;
                            string v1_1 = td_date.Text;
                            DateTime dt_1 = DateTime.ParseExact(v1_1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string fdate = dt_1.ToString("MM/dd/yyyy");

                            string v1_2 = ts_date.Text;
                            DateTime dt_2 = DateTime.ParseExact(v1_2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //string tdate = dt_2.ToString("dd/MM/yyyy");
                            double dcount = (dt_2 - dt_1).TotalDays;

                            if (check_sn.Checked == false)
                            {

                                for (int i = 0; i <= dcount; i++)
                                {
                                    DateTime mm = Convert.ToDateTime(fdate);
                                    DateTime mm1 = mm.AddDays(i);
                                    string pdt_month = mm1.ToString("yyyy-MM-dd");
                                    DataTable dtt = new DataTable();

                                    DataTable sel_organ1 = new DataTable();
                                    sel_organ1 = DBCon.Ora_Execute_table("select * from hr_organization where org_gen_id='" + dd_org.SelectedValue + "'");
                                    for (int k = 0; k <= sel_organ1.Rows.Count - 1; k++)
                                    {
                                        if (DD_NegriBind1.SelectedValue != "")
                                        {
                                            get_ngri = DD_NegriBind1.SelectedValue;
                                        }
                                        else
                                        {
                                            get_ngri = sel_organ1.Rows[k]["org_state_cd"].ToString();
                                        }
                                        dtt = DBCon.Ora_Execute_table("select org_gen_ID from hr_organization where org_id='" + sel_organ1.Rows[k]["org_id"].ToString() + "' and org_name='" + sel_organ1.Rows[k]["org_name"].ToString() + "'");
                                        DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ1.Rows[k]["org_id"].ToString() + "','" + get_ngri + "','" + dd_jcuti.SelectedItem.Value + "','" + pdt_month + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtt.Rows[0][0].ToString() + "')");
                                    }
                                }
                                service.audit_trail("P0109", "Simpan", "Nama Organisasi", dd_org.SelectedItem.Text);
                                dd_jcuti.SelectedValue = "";
                                DD_NegriBind1.SelectedValue = "";
                                clr_hist();
                                check_sn.Checked = false;
                                Button3.Visible = false;
                                hm.Attributes.Add("style", "display:none");
                                cl_simp.Attributes.Add("style", "display:none");
                                cl.Attributes.Add("style", "display:none");
                                M_title.Text = "&nbsp;";
                                grid();
                              
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                            }
                            else
                            {
                                DataTable sel_organ = new DataTable();
                                sel_organ = DBCon.Ora_Execute_table("select  s1.org_id,s1.org_gen_id,op_state_cd as org_state_cd from hr_organization_pern left join hr_organization s1 on s1.org_gen_id=op_org_id group by s1.org_id,s1.org_gen_id,op_state_cd");
                                for (int k = 0; k <= sel_organ.Rows.Count - 1; k++)
                                {
                                    for (int i = 0; i <= dcount; i++)
                                    {
                                        DateTime mm = Convert.ToDateTime(fdate);
                                        DateTime mm1 = mm.AddDays(i);
                                        string pdt_month = mm1.ToString("yyyy-MM-dd");
                                        DataTable dtt = new DataTable();
                                        dtt = DBCon.Ora_Execute_table("select org_gen_ID from hr_organization where org_id='" + sel_organ.Rows[k]["org_id"].ToString() + "' and org_gen_ID='" + sel_organ.Rows[k]["org_gen_ID"].ToString() + "'");
                                        DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ.Rows[k]["org_id"].ToString() + "','" + sel_organ.Rows[k]["org_state_cd"].ToString() + "','" + dd_jcuti.SelectedItem.Value + "','" + pdt_month + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtt.Rows[0][0].ToString() + "')");
                                    }

                                }
                                service.audit_trail("P0109", "Simpan", "Nama Organisasi", dd_org.SelectedItem.Text);
                                dd_jcuti.SelectedValue = "";
                                DD_NegriBind1.SelectedValue = "";
                                clr_hist();
                                check_sn.Checked = false;
                                Button3.Visible = false;
                                hm.Attributes.Add("style", "display:none");
                                cl_simp.Attributes.Add("style", "display:none");
                                cl.Attributes.Add("style", "display:none");
                                M_title.Text = "&nbsp;";
                                grid();
                                
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                            }

                        }
                        else
                        {
                            if (dd_jcuti.SelectedValue != "")
                            {
                                sow_cnt.Attributes.Remove("Style");
                            }
                            grid();                            
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Maklumat Cuti.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                        }
                    }

                    if (ver_id.Text == "0")
                    {
                        //string ct1 = string.Empty, ct2 = string.Empty, ct3 = string.Empty, ct4 = string.Empty, ct5 = string.Empty, ct6 = string.Empty, ct7 = string.Empty, ct8 = string.Empty;
                        int ct1, ct2, ct3, ct4, ct5, ct6, ct7;

                        if (CheckBox3.Checked == true || CheckBox4.Checked == true || CheckBox6.Checked == true || CheckBox8.Checked == true || CheckBox2.Checked == true || CheckBox5.Checked == true || CheckBox7.Checked == true)
                        {
                            //DataTable dtt1 = new DataTable();
                            //dtt1 = DBCon.Ora_Execute_table("select org_gen_ID from hr_organization where org_id='" + sel_organ.Rows[k]["org_id"].ToString() + "' and org_gen_ID='" + dd_org.SelectedValue + "'");
                            if (CheckBox3.Checked == true)
                            {
                                ct1 = 1;
                            }
                            else
                            {
                                ct1 = 0;
                            }

                            if (CheckBox4.Checked == true)
                            {
                                ct2 = 1;
                            }
                            else
                            {
                                ct2 = 0;
                            }

                            if (CheckBox6.Checked == true)
                            {
                                ct3 = 1;
                            }
                            else
                            {
                                ct3 = 0;
                            }

                            if (CheckBox8.Checked == true)
                            {
                                ct4 = 1;
                            }
                            else
                            {
                                ct4 = 0;
                            }

                            if (CheckBox2.Checked == true)
                            {
                                ct5 = 1;
                            }
                            else
                            {
                                ct5 = 0;
                            }

                            if (CheckBox5.Checked == true)
                            {
                                ct6 = 1;
                            }
                            else
                            {
                                ct6 = 0;
                            }

                            if (CheckBox7.Checked == true)
                            {
                                ct7 = 1;
                            }
                            else
                            {
                                ct7 = 0;
                            }


                            if (ct1 == 1)
                            {

                                List<DateTime> dates = new List<DateTime>();
                                int year = Int32.Parse(dd_tahun.SelectedItem.Value);

                                for (int k = 1; k <= 12; k++)
                                {
                                    int month = k;
                                    DayOfWeek day = DayOfWeek.Sunday;
                                    //System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                                    for (int i = 1; i <= CultureInfo.InvariantCulture.Calendar.GetDaysInMonth(year, month); i++)
                                    {
                                        DateTime d = new DateTime(year, month, i);
                                        if (d.DayOfWeek == day)
                                        {
                                            dates.Add(d);
                                            string fd1 = d.ToString("yyyy-MM-dd");
                                            if (check_sn.Checked == false)
                                            {
                                                DataTable sel_organ1 = new DataTable();
                                                sel_organ1 = DBCon.Ora_Execute_table("select * from hr_organization where org_gen_id='" + dd_org.SelectedValue + "'");
                                                for (int k1 = 0; k1 <= sel_organ1.Rows.Count - 1; k1++)
                                                {
                                                    if (DD_NegriBind1.SelectedValue != "")
                                                    {
                                                        get_ngri = DD_NegriBind1.SelectedValue;
                                                    }
                                                    else
                                                    {
                                                        get_ngri = sel_organ1.Rows[k1]["org_state_cd"].ToString();
                                                    }
                                                    DataTable dtt1 = new DataTable();
                                                    dtt1 = DBCon.Ora_Execute_table("select org_gen_id from hr_organization where org_id='" + sel_organ1.Rows[k1]["org_id"].ToString() + "' and org_name='" + sel_organ1.Rows[k1]["org_name"].ToString() + "'");
                                                    DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ1.Rows[k1]["org_id"].ToString() + "','" + get_ngri + "','" + dd_jcuti.SelectedItem.Value + "','" + fd1 + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtt1.Rows[0]["org_gen_id"].ToString() + "')");
                                                }
                                            }
                                            else
                                            {
                                                DataTable sel_organ = new DataTable();
                                                sel_organ = DBCon.Ora_Execute_table("select  s1.org_id,s1.org_gen_id,op_state_cd as org_state_cd from hr_organization_pern left join hr_organization s1 on s1.org_gen_id=op_org_id group by s1.org_id,s1.org_gen_id,op_state_cd");
                                                for (int j = 0; j <= sel_organ.Rows.Count - 1; j++)
                                                {
                                                    DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ.Rows[j]["org_id"].ToString() + "','" + sel_organ.Rows[j]["org_state_cd"].ToString() + "','" + dd_jcuti.SelectedItem.Value + "','" + fd1 + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + sel_organ.Rows[j]["org_gen_id"].ToString() + "')");
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (ct2 == 1)
                            {

                                List<DateTime> dates = new List<DateTime>();
                                int year = Int32.Parse(dd_tahun.SelectedItem.Value);

                                for (int k = 1; k <= 12; k++)
                                {
                                    int month = k;
                                    DayOfWeek day = DayOfWeek.Monday;
                                    //System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                                    for (int i = 1; i <= CultureInfo.InvariantCulture.Calendar.GetDaysInMonth(year, month); i++)
                                    {
                                        DateTime d = new DateTime(year, month, i);
                                        if (d.DayOfWeek == day)
                                        {
                                            dates.Add(d);
                                            string fd1 = d.ToString("yyyy-MM-dd");
                                            if (check_sn.Checked == false)
                                            {
                                                DataTable sel_organ1 = new DataTable();
                                                sel_organ1 = DBCon.Ora_Execute_table("select * from hr_organization where org_gen_id='" + dd_org.SelectedValue + "'");
                                                for (int k1 = 0; k1 <= sel_organ1.Rows.Count - 1; k1++)
                                                {
                                                    if (DD_NegriBind1.SelectedValue != "")
                                                    {
                                                        get_ngri = DD_NegriBind1.SelectedValue;
                                                    }
                                                    else
                                                    {
                                                        get_ngri = sel_organ1.Rows[k1]["org_state_cd"].ToString();
                                                    }
                                                    DataTable dtt1 = new DataTable();
                                                    dtt1 = DBCon.Ora_Execute_table("select org_gen_id from hr_organization where org_id='" + sel_organ1.Rows[k1]["org_id"].ToString() + "' and org_name='" + sel_organ1.Rows[k1]["org_name"].ToString() + "'");
                                                    DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ1.Rows[k1]["org_id"].ToString() + "','" + get_ngri + "','" + dd_jcuti.SelectedItem.Value + "','" + fd1 + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtt1.Rows[0]["org_gen_id"].ToString() + "')");
                                                }
                                            }
                                            else
                                            {
                                                DataTable sel_organ = new DataTable();
                                                sel_organ = DBCon.Ora_Execute_table("select  s1.org_id,s1.org_gen_id,op_state_cd as org_state_cd from hr_organization_pern left join hr_organization s1 on s1.org_gen_id=op_org_id group by s1.org_id,s1.org_gen_id,op_state_cd");
                                                for (int j = 0; j <= sel_organ.Rows.Count - 1; j++)
                                                {
                                                    DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ.Rows[j]["org_id"].ToString() + "','" + sel_organ.Rows[j]["org_state_cd"].ToString() + "','" + dd_jcuti.SelectedItem.Value + "','" + fd1 + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + sel_organ.Rows[j]["org_gen_id"].ToString() + "')");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (ct3 == 1)
                            {

                                List<DateTime> dates = new List<DateTime>();
                                int year = Int32.Parse(dd_tahun.SelectedItem.Value);

                                for (int k = 1; k <= 12; k++)
                                {
                                    int month = k;
                                    DayOfWeek day = DayOfWeek.Tuesday;
                                    //System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                                    for (int i = 1; i <= CultureInfo.InvariantCulture.Calendar.GetDaysInMonth(year, month); i++)
                                    {
                                        DateTime d = new DateTime(year, month, i);
                                        if (d.DayOfWeek == day)
                                        {
                                            dates.Add(d);
                                            string fd1 = d.ToString("yyyy-MM-dd");
                                            if (check_sn.Checked == false)
                                            {
                                                DataTable sel_organ1 = new DataTable();
                                                sel_organ1 = DBCon.Ora_Execute_table("select * from hr_organization where org_gen_id='" + dd_org.SelectedValue + "'");
                                                for (int k1 = 0; k1 <= sel_organ1.Rows.Count - 1; k1++)
                                                {
                                                    if (DD_NegriBind1.SelectedValue != "")
                                                    {
                                                        get_ngri = DD_NegriBind1.SelectedValue;
                                                    }
                                                    else
                                                    {
                                                        get_ngri = sel_organ1.Rows[k1]["org_state_cd"].ToString();
                                                    }
                                                    DataTable dtt1 = new DataTable();
                                                    dtt1 = DBCon.Ora_Execute_table("select org_gen_id from hr_organization where org_id='" + sel_organ1.Rows[k1]["org_id"].ToString() + "' and org_name='" + sel_organ1.Rows[k1]["org_name"].ToString() + "'");
                                                    DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ1.Rows[k1]["org_id"].ToString() + "','" + get_ngri + "','" + dd_jcuti.SelectedItem.Value + "','" + fd1 + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtt1.Rows[0]["org_gen_id"].ToString() + "')");
                                                }
                                            }
                                            else
                                            {
                                                DataTable sel_organ = new DataTable();
                                                sel_organ = DBCon.Ora_Execute_table("select  s1.org_id,s1.org_gen_id,op_state_cd as org_state_cd from hr_organization_pern left join hr_organization s1 on s1.org_gen_id=op_org_id group by s1.org_id,s1.org_gen_id,op_state_cd");
                                                for (int j = 0; j <= sel_organ.Rows.Count - 1; j++)
                                                {
                                                    DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ.Rows[j]["org_id"].ToString() + "','" + sel_organ.Rows[j]["org_state_cd"].ToString() + "','" + dd_jcuti.SelectedItem.Value + "','" + fd1 + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + sel_organ.Rows[j]["org_gen_id"].ToString() + "')");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (ct4 == 1)
                            {

                                List<DateTime> dates = new List<DateTime>();
                                int year = Int32.Parse(dd_tahun.SelectedItem.Value);

                                for (int k = 1; k <= 12; k++)
                                {
                                    int month = k;
                                    DayOfWeek day = DayOfWeek.Wednesday;
                                    //System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                                    for (int i = 1; i <= CultureInfo.InvariantCulture.Calendar.GetDaysInMonth(year, month); i++)
                                    {
                                        DateTime d = new DateTime(year, month, i);
                                        if (d.DayOfWeek == day)
                                        {
                                            dates.Add(d);
                                            string fd1 = d.ToString("yyyy-MM-dd");
                                            if (check_sn.Checked == false)
                                            {
                                                DataTable sel_organ1 = new DataTable();
                                                sel_organ1 = DBCon.Ora_Execute_table("select * from hr_organization where org_gen_id='" + dd_org.SelectedValue + "'");
                                                for (int k1 = 0; k1 <= sel_organ1.Rows.Count - 1; k1++)
                                                {
                                                    if (DD_NegriBind1.SelectedValue != "")
                                                    {
                                                        get_ngri = DD_NegriBind1.SelectedValue;
                                                    }
                                                    else
                                                    {
                                                        get_ngri = sel_organ1.Rows[k1]["org_state_cd"].ToString();
                                                    }
                                                    DataTable dtt1 = new DataTable();
                                                    dtt1 = DBCon.Ora_Execute_table("select org_gen_id from hr_organization where org_id='" + sel_organ1.Rows[k1]["org_id"].ToString() + "' and org_name='" + sel_organ1.Rows[k1]["org_name"].ToString() + "'");
                                                    DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ1.Rows[k1]["org_id"].ToString() + "','" + get_ngri + "','" + dd_jcuti.SelectedItem.Value + "','" + fd1 + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtt1.Rows[0]["org_gen_id"].ToString() + "')");
                                                }
                                            }
                                            else
                                            {
                                                DataTable sel_organ = new DataTable();
                                                sel_organ = DBCon.Ora_Execute_table("select  s1.org_id,s1.org_gen_id,op_state_cd as org_state_cd from hr_organization_pern left join hr_organization s1 on s1.org_gen_id=op_org_id group by s1.org_id,s1.org_gen_id,op_state_cd");
                                                for (int j = 0; j <= sel_organ.Rows.Count - 1; j++)
                                                {
                                                    DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ.Rows[j]["org_id"].ToString() + "','" + sel_organ.Rows[j]["org_state_cd"].ToString() + "','" + dd_jcuti.SelectedItem.Value + "','" + fd1 + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + sel_organ.Rows[j]["org_gen_id"].ToString() + "')");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (ct5 == 1)
                            {

                                List<DateTime> dates = new List<DateTime>();
                                int year = Int32.Parse(dd_tahun.SelectedItem.Value);

                                for (int k = 1; k <= 12; k++)
                                {
                                    int month = k;
                                    DayOfWeek day = DayOfWeek.Thursday;
                                    //System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                                    for (int i = 1; i <= CultureInfo.InvariantCulture.Calendar.GetDaysInMonth(year, month); i++)
                                    {
                                        DateTime d = new DateTime(year, month, i);
                                        if (d.DayOfWeek == day)
                                        {
                                            dates.Add(d);
                                            string fd1 = d.ToString("yyyy-MM-dd");
                                            if (check_sn.Checked == false)
                                            {
                                                DataTable sel_organ1 = new DataTable();
                                                sel_organ1 = DBCon.Ora_Execute_table("select * from hr_organization where org_gen_id='" + dd_org.SelectedValue + "'");
                                                for (int k1 = 0; k1 <= sel_organ1.Rows.Count - 1; k1++)
                                                {
                                                    if (DD_NegriBind1.SelectedValue != "")
                                                    {
                                                        get_ngri = DD_NegriBind1.SelectedValue;
                                                    }
                                                    else
                                                    {
                                                        get_ngri = sel_organ1.Rows[k1]["org_state_cd"].ToString();
                                                    }
                                                    DataTable dtt1 = new DataTable();
                                                    dtt1 = DBCon.Ora_Execute_table("select org_gen_id from hr_organization where org_id='" + sel_organ1.Rows[k1]["org_id"].ToString() + "' and org_name='" + sel_organ1.Rows[k1]["org_name"].ToString() + "'");
                                                    DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ1.Rows[k1]["org_id"].ToString() + "','" + get_ngri + "','" + dd_jcuti.SelectedItem.Value + "','" + fd1 + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtt1.Rows[0]["org_gen_id"].ToString() + "')");
                                                }
                                            }
                                            else
                                            {
                                                DataTable sel_organ = new DataTable();
                                                sel_organ = DBCon.Ora_Execute_table("select  s1.org_id,s1.org_gen_id,op_state_cd as org_state_cd from hr_organization_pern left join hr_organization s1 on s1.org_gen_id=op_org_id group by s1.org_id,s1.org_gen_id,op_state_cd");
                                                for (int j = 0; j <= sel_organ.Rows.Count - 1; j++)
                                                {
                                                    DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ.Rows[j]["org_id"].ToString() + "','" + sel_organ.Rows[j]["org_state_cd"].ToString() + "','" + dd_jcuti.SelectedItem.Value + "','" + fd1 + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + sel_organ.Rows[j]["org_gen_id"].ToString() + "')");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (ct6 == 1)
                            {

                                List<DateTime> dates = new List<DateTime>();
                                int year = Int32.Parse(dd_tahun.SelectedItem.Value);

                                for (int k = 1; k <= 12; k++)
                                {
                                    int month = k;
                                    DayOfWeek day = DayOfWeek.Friday;
                                    //System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                                    for (int i = 1; i <= CultureInfo.InvariantCulture.Calendar.GetDaysInMonth(year, month); i++)
                                    {
                                        DateTime d = new DateTime(year, month, i);
                                        if (d.DayOfWeek == day)
                                        {
                                            dates.Add(d);
                                            string fd1 = d.ToString("yyyy-MM-dd");
                                            if (check_sn.Checked == false)
                                            {
                                                DataTable sel_organ1 = new DataTable();
                                                sel_organ1 = DBCon.Ora_Execute_table("select * from hr_organization where org_gen_id='" + dd_org.SelectedValue + "'");
                                                for (int k1 = 0; k1 <= sel_organ1.Rows.Count - 1; k1++)
                                                {
                                                    if (DD_NegriBind1.SelectedValue != "")
                                                    {
                                                        get_ngri = DD_NegriBind1.SelectedValue;
                                                    }
                                                    else
                                                    {
                                                        get_ngri = sel_organ1.Rows[k1]["org_state_cd"].ToString();
                                                    }
                                                    DataTable dtt1 = new DataTable();
                                                    dtt1 = DBCon.Ora_Execute_table("select org_gen_id from hr_organization where org_id='" + sel_organ1.Rows[k1]["org_id"].ToString() + "' and org_name='" + sel_organ1.Rows[k1]["org_name"].ToString() + "'");
                                                    DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ1.Rows[k1]["org_id"].ToString() + "','" + get_ngri + "','" + dd_jcuti.SelectedItem.Value + "','" + fd1 + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtt1.Rows[0]["org_gen_id"].ToString() + "')");
                                                }
                                            }
                                            else
                                            {
                                                DataTable sel_organ = new DataTable();
                                                sel_organ = DBCon.Ora_Execute_table("select  s1.org_id,s1.org_gen_id,op_state_cd as org_state_cd from hr_organization_pern left join hr_organization s1 on s1.org_gen_id=op_org_id group by s1.org_id,s1.org_gen_id,op_state_cd");
                                                for (int j = 0; j <= sel_organ.Rows.Count - 1; j++)
                                                {
                                                    DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ.Rows[j]["org_id"].ToString() + "','" + sel_organ.Rows[j]["org_state_cd"].ToString() + "','" + dd_jcuti.SelectedItem.Value + "','" + fd1 + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + sel_organ.Rows[j]["org_gen_id"].ToString() + "')");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (ct7 == 1)
                            {

                                List<DateTime> dates = new List<DateTime>();
                                int year = Int32.Parse(dd_tahun.SelectedItem.Value);

                                for (int k = 1; k <= 12; k++)
                                {
                                    int month = k;
                                    DayOfWeek day = DayOfWeek.Saturday;
                                    //System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                                    for (int i = 1; i <= CultureInfo.InvariantCulture.Calendar.GetDaysInMonth(year, month); i++)
                                    {
                                        DateTime d = new DateTime(year, month, i);
                                        if (d.DayOfWeek == day)
                                        {
                                            dates.Add(d);
                                            string fd1 = d.ToString("yyyy-MM-dd");
                                            if (check_sn.Checked == false)
                                            {
                                                DataTable sel_organ1 = new DataTable();
                                                sel_organ1 = DBCon.Ora_Execute_table("select * from hr_organization where org_gen_id='" + dd_org.SelectedValue + "'");
                                                for (int k1 = 0; k1 <= sel_organ1.Rows.Count - 1; k1++)
                                                {
                                                    
                                                    if(DD_NegriBind1.SelectedValue != "")
                                                    {
                                                        get_ngri = DD_NegriBind1.SelectedValue;
                                                    }
                                                    else
                                                    {
                                                        get_ngri = sel_organ1.Rows[k1]["org_state_cd"].ToString();
                                                    }
                                                    DataTable dtt1 = new DataTable();
                                                    dtt1 = DBCon.Ora_Execute_table("select org_gen_id from hr_organization where org_id='" + sel_organ1.Rows[k1]["org_id"].ToString() + "' and org_name='" + sel_organ1.Rows[k1]["org_name"].ToString() + "'");
                                                    DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ1.Rows[k1]["org_id"].ToString() + "','" + get_ngri + "','" + dd_jcuti.SelectedItem.Value + "','" + fd1 + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + dtt1.Rows[0]["org_gen_id"].ToString() + "')");
                                                }
                                            }
                                            else
                                            {
                                                DataTable sel_organ = new DataTable();
                                                sel_organ = DBCon.Ora_Execute_table("select  s1.org_id,s1.org_gen_id,op_state_cd as org_state_cd from hr_organization_pern left join hr_organization s1 on s1.org_gen_id=op_org_id group by s1.org_id,s1.org_gen_id,op_state_cd");
                                                for (int j = 0; j <= sel_organ.Rows.Count - 1; j++)
                                                {
                                                    DBCon.Ora_Execute_table("INSERT INTO hr_holiday (hol_org_id,hol_state_cd,hol_holiday_cd,hol_dt,hol_remark,hol_cancel_ind,hol_crt_id,hol_crt_dt,hol_gen_id) VALUES ('" + sel_organ.Rows[j]["org_id"].ToString() + "','" + sel_organ.Rows[j]["org_state_cd"].ToString() + "','" + dd_jcuti.SelectedItem.Value + "','" + fd1 + "','" + TextBox7.Value + "','N','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + sel_organ.Rows[j]["org_gen_id"].ToString() + "')");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            service.audit_trail("P0109", "Simpan", "Nama Organisasi", dd_org.SelectedItem.Text);
                            dd_jcuti.SelectedValue = "";
                            DD_NegriBind1.SelectedValue = "";
                            clr_hist();
                            check_sn.Checked = false;
                            Button3.Visible = false;
                            hm.Attributes.Add("style", "display:none");
                            cl_simp.Attributes.Add("style", "display:none");
                            cl.Attributes.Add("style", "display:none");
                            M_title.Text = "&nbsp;";
                            grid();                            
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                        }
                        else
                        {
                            if (dd_jcuti.SelectedValue != "")
                            {
                                sow_cnt.Attributes.Remove("Style");
                            }
                            grid();                            
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Maklumat Cuti.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                        }

                    }

                }
                else
                {
                    grid();                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Maklumat Cuti.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                grid();                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Jenis Cuti.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void batal_Click(object sender, EventArgs e)
    {

        using (SqlConnection con = new SqlConnection(cs))
        {
            string strDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string rcount = string.Empty;
            int count = 0;
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                var rb = gvrow.FindControl("chkStatus") as System.Web.UI.WebControls.CheckBox;
                if (rb.Checked == true)
                {
                    count++;
                }
                rcount = count.ToString();
            }
            if (rcount != "0")
            {
                foreach (GridViewRow gvrow in GridView1.Rows)
                {
                    var checkbox = gvrow.FindControl("chkStatus") as System.Web.UI.WebControls.CheckBox;
                    if (checkbox.Checked == true)
                    {
                        var o_id = gvrow.FindControl("oid") as System.Web.UI.WebControls.Label;
                        var h_id = gvrow.FindControl("hid") as System.Web.UI.WebControls.Label;
                        var s_id = gvrow.FindControl("sid") as System.Web.UI.WebControls.Label;
                        var hdate = gvrow.FindControl("Label5") as System.Web.UI.WebControls.Label;
                        var oid1 = gvrow.FindControl("Label21") as System.Web.UI.WebControls.Label;
                        String up_date;
                        DateTime bd = DateTime.ParseExact(hdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        up_date = bd.ToString("yyyy-MM-dd");

                        string userid = Session["New"].ToString();
                        DBCon.Ora_Execute_table("Update hr_holiday set hol_cancel_ind='Y',hol_upd_id='" + userid + "',hol_upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where hol_org_id='" + o_id.Text + "' and hol_state_cd='" + s_id.Text + "' and hol_gen_id='" + oid1.Text + "' and hol_dt='" + up_date + "'");
                    }

                }
                grid();
                service.audit_trail("P0109", "BATAL", "Nama Organisasi", dd_org.SelectedItem.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                grid();                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Yang Hendak Dibatalkan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
    }

    protected void dd_jeniscuti(object sender, EventArgs e)

    {
        CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
        TextInfo txtinfo = culinfo.TextInfo;
        if (dd_jcuti.SelectedValue == "16")
        {
            Button3.Visible = true;
            M_title.Text = txtinfo.ToTitleCase(dd_jcuti.SelectedItem.Text.ToLower());
            sow_cnt.Attributes.Remove("Style");
            hm.Attributes.Remove("style");
            cl_simp.Attributes.Remove("style");
            cl.Attributes.Add("style", "display:none");
            ver_id.Text = "0";
            grid();
        }
        else if (dd_jcuti.SelectedValue == "13")
        {
            Button3.Visible = true;
            M_title.Text = txtinfo.ToTitleCase(dd_jcuti.SelectedItem.Text.ToLower());
            sow_cnt.Attributes.Remove("Style");
            cl.Attributes.Remove("style");
            cl_simp.Attributes.Remove("style");
            hm.Attributes.Add("style", "display:none");
            ver_id.Text = "1";
            grid();
        }
        else
        {
            Button3.Visible = false;
            hm.Attributes.Add("style", "display:none");
            cl_simp.Attributes.Add("style", "display:none");
            cl.Attributes.Add("style", "display:none");
            M_title.Text = "&nbsp;";
            grid();
        }
    }

    //protected void rset_click(object sender, EventArgs e)
    //{
    //    Response.Redirect("HR_CUTI.aspx");
    //}

    protected void click_pdf(object sender, EventArgs e)
    {
        {
            try
            {
                sel_dd();
                //Path
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = DBCon.Ora_Execute_table("select hol_gen_id,hol_org_id,hol_state_cd,hol_holiday_cd,FORMAT(hol_dt,'dd/MM/yyyy', 'en-us') as hol_dt,hol_remark,hro.org_name,rn.hr_negeri_desc,jc.hr_jenis_desc from hr_holiday as hrh left join hr_organization as hro on hro.org_gen_ID= hrh.hol_gen_id left join Ref_hr_jenis_cuti as jc on jc.hr_jenis_Code=hrh.hol_holiday_cd left join Ref_hr_negeri as rn on rn.hr_negeri_Code=hrh.hol_state_cd " + val1 + " " + val2 + " " + val3 + " order by hol_org_id,hol_state_cd,cast(hol_dt as datetime)");

                ds.Tables.Add(dt);

                Rptviwer_kelulusan.Reset();
                Rptviwer_kelulusan.LocalReport.Refresh();
                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

                string jname = string.Empty, oname = string.Empty;
                if (dd_jcuti.SelectedValue != "")
                {
                    jname = dd_jcuti.SelectedItem.Text;
                }
                else
                {
                    jname = "SEMUA";
                }
                if (dd_org.SelectedValue != "")
                {
                    oname = dd_org.SelectedItem.Text;
                }
                else
                {
                    oname = "SEMUA";
                }

                if (countRow != 0)
                {
                    Rptviwer_kelulusan.LocalReport.DataSources.Clear();

                    Rptviwer_kelulusan.LocalReport.ReportPath = "SUMBER_MANUSIA/hr_cuti.rdlc";
                    ReportDataSource rds = new ReportDataSource("hrcuti", dt);
                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("ogname",oname.ToUpper() ),
                     new ReportParameter("jename",jname.ToUpper() )

                     };
                    Rptviwer_kelulusan.LocalReport.SetParameters(rptParams);
                    Rptviwer_kelulusan.LocalReport.DataSources.Add(rds);

                    //Refresh
                    Rptviwer_kelulusan.LocalReport.Refresh();
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

                    byte[] bytes = Rptviwer_kelulusan.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                    Response.Buffer = true;

                    Response.Clear();

                    Response.ClearHeaders();

                    Response.ClearContent();

                    Response.ContentType = "application/pdf";


                    Response.AddHeader("content-disposition", "attachment; filename= SELENGGARA_KALENDAR_CUTI_" + DateTime.Now.ToString("dd_MM_yyyy") + "." + extension);

                    Response.BinaryWrite(bytes);

                    //Response.Write("<script>");
                    //Response.Write("window.open('', '_newtab');");
                    //Response.Write("</script>");
                    Response.Flush();

                    Response.End();
                }
                else
                {
                    grid();                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    void clr_hist()
    {
        
        dd_tahun.SelectedValue = DateTime.Now.Year.ToString();
        CheckBox3.Checked = false;
        CheckBox4.Checked = false;
        CheckBox6.Checked = false;
        CheckBox8.Checked = false;
        CheckBox2.Checked = false;
        CheckBox5.Checked = false;
        CheckBox7.Checked = false;
        td_date.Text = "";
        ts_date.Text = "";
        TextBox7.Value = "";

    }

    protected void rset_click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_CUTI.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Session["pro_id"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_CUTI_view.aspx");
    }


}