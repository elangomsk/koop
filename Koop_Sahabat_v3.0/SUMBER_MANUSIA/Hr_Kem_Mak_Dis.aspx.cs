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
using System.Threading;

public partial class Hr_Kem_Mak_Dis : System.Web.UI.Page
{
    DBConnection dbcon = new DBConnection();
    DBConnection DBCon = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    StudentWebService service = new StudentWebService();
    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string gt_val1 = string.Empty, gt_val2 = string.Empty;

    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        assgn_roles();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                useid = Session["New"].ToString();
                GP_date.Text = DateTime.Now.ToString("dd/MM/yyyy");                
               
                if (samp != "")
                {
                    Kaki_no.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    Applcn_no1.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                }
                DIS();
                grid_1();
                BindGrid();

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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1973','448','505','484','77','1565','513','1491','1954','1288','190','1974','415','1338','1528','1975','61','35')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[16][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());      
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[15][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[14][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[17][0].ToString().ToLower());       
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl12.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl15.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl16.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            btnUpload.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button7.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
           

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void assgn_roles()
    {
        if (Session["New"] != null)
        {
            DataTable ddokdicno = new DataTable();
            ddokdicno = dbcon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

            if (ddokdicno.Rows.Count != 0)
            {
                DataTable ddokdicno_1 = new DataTable();
                ddokdicno_1 = dbcon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0045' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

                if (ddokdicno_1.Rows.Count != 0)
                {

                    gt_val1 = ddokdicno_1.Rows[0]["Edit_chk"].ToString();
                    
                    if (gt_val1 == "1")
                    {
                        Button5.Visible = true;
                    }
                    else
                    {
                        Button5.Visible = false;
                    }
                }
            }
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    void view_details()
    {
        try
        {
            if (Kaki_no.Text != "")
            {
                DataTable dd1 = new DataTable();
                dd1 = dbcon.Ora_Execute_table("select stf_staff_no from hr_staff_profile where '" + Kaki_no.Text + "' IN (stf_staff_no,stf_name)");
                if (dd1.Rows.Count > 0)
                {
                    Applcn_no1.Text = dd1.Rows[0]["stf_staff_no"].ToString();
                    bindchl();
                    grid_1();
                }
                else
                {
                    grid_1();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                grid_1();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {

        using (SqlConnection con = new SqlConnection())
        {

            con.ConnectionString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
            DBConnection dbcon1 = new DBConnection();
            DataTable qry1 = new DataTable();
            qry1 = dbcon1.Ora_Execute_table("select stf_staff_no from hr_staff_profile where stf_staff_no LIKE '%" + prefixText + "%'");
            if (qry1.Rows.Count != 0)
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "select stf_staff_no from hr_staff_profile where stf_staff_no LIKE '%' + @Search + '%'";
                    com.Parameters.AddWithValue("@Search", prefixText);
                    com.Connection = con;
                    con.Open();
                    List<string> countryNames = new List<string>();
                    using (SqlDataReader sdr = com.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["stf_staff_no"].ToString());

                        }
                    }

                    con.Close();
                    return countryNames;
                }
            }
            else
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "select stf_staff_no,stf_name from hr_staff_profile where stf_name LIKE '%' + @Search + '%'";
                    com.Parameters.AddWithValue("@Search", prefixText);
                    com.Connection = con;
                    con.Open();
                    List<string> countryNames = new List<string>();
                    using (SqlDataReader sdr = com.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["stf_name"].ToString());

                        }
                    }

                    con.Close();
                    return countryNames;
                }
            }
        }
    }

    void DIS()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select hr_discipline_Code,hr_discipline_desc  from Ref_hr_discipline";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_sebab.DataSource = dt;
            dd_sebab.DataBind();
            dd_sebab.DataTextField = "hr_discipline_desc";
            dd_sebab.DataValueField = "hr_discipline_Code";
            dd_sebab.DataBind();
            dd_sebab.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void bindchl()
    {
        DataTable ddjab1 = new DataTable();
        ddjab1 = dbcon.Ora_Execute_table("select stf_name,pos_post_cd,pos_grade_cd,pos_dept_cd from hr_staff_profile hsp inner join hr_post_his hph on hph.pos_staff_no=hsp.stf_staff_no where hsp.stf_staff_no='" + Applcn_no1.Text + "' AND pos_end_dt = '9999-12-31'");
        if (ddjab1.Rows.Count != 0)
        {
            SqlConnection conn = new SqlConnection(cs);

            string query2 = "select stf_name,pos_post_cd,pos_grade_cd,pos_dept_cd,ho.org_name,o1.op_perg_name from hr_staff_profile hsp left join hr_post_his hph on hph.pos_staff_no=hsp.stf_staff_no left join hr_organization ho on ho.org_gen_id=hsp.str_curr_org_cd left join hr_organization_pern o1 on o1.op_perg_code=hsp.stf_cur_sub_org where hsp.stf_staff_no='" + Applcn_no1.Text + "' AND pos_end_dt = '9999-12-31'";
            conn.Open();
            var sqlCommand3 = new SqlCommand(query2, conn);
            var sqlReader3 = sqlCommand3.ExecuteReader();

            while (sqlReader3.Read())
            {
                s_nama.Text = (string)sqlReader3["stf_name"].ToString().Trim();
                txt_org.Text = (string)sqlReader3["org_name"].ToString().ToUpper();
                string jaw = (string)sqlReader3["pos_post_cd"].ToString().Trim();
                TextBox2.Text = (string)sqlReader3["op_perg_name"].ToString();
                DataTable ddjaw = new DataTable();
                ddjaw = dbcon.Ora_Execute_table("select hr_jaw_Code,hr_jaw_desc from ref_hr_jawatan where  hr_jaw_Code='" + jaw + "'");
                if (ddjaw.Rows.Count != 0)
                {
                    s_jaw.Text = ddjaw.Rows[0][1].ToString();
                }
                string jab = (string)sqlReader3["pos_dept_cd"].ToString().Trim();
                DataTable ddjab = new DataTable();
                ddjab = dbcon.Ora_Execute_table("select hr_jaba_Code,hr_jaba_desc from Ref_hr_jabatan where hr_jaba_Code='" + jab + "'");
                if (ddjab.Rows.Count != 0)
                {
                    s_jab.Text = ddjab.Rows[0][1].ToString();
                }
                string gred = (string)sqlReader3["pos_grade_cd"].ToString().Trim();
                DataTable ddgrd = new DataTable();
                ddgrd = dbcon.Ora_Execute_table("select hr_gred_code,hr_gred_desc from ref_hr_gred where hr_gred_code='" + gred + "'");
                if (ddgrd.Rows.Count != 0)
                {
                    s_gred.Text = ddgrd.Rows[0][1].ToString();
                }

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }


    private void BindGrid()
    {
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select Id, Name from Displin  where td_staff_no='" + Applcn_no1.Text + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView2.DataSource = ds;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }
    }
    protected void Upload(object sender, EventArgs e)
    {
        if (Kaki_no.Text != "")
        {
            if (GP_date.Text != "")
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if ( filename!="")
                { 

                string contentType = FileUpload1.PostedFile.ContentType;
                useid = Session["New"].ToString();
                using (Stream fs = FileUpload1.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            string query = "insert into Displin values (@sno,@type,@sdt,@Name, @ContentType, @Data,@cid,@cdt)";
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@sno", Applcn_no1.Text);
                                cmd.Parameters.AddWithValue("@type", dd_sebab.SelectedValue);
                                cmd.Parameters.AddWithValue("@sdt", GP_date.Text);
                                cmd.Parameters.AddWithValue("@Name", filename);
                                cmd.Parameters.AddWithValue("@ContentType", contentType);
                                cmd.Parameters.AddWithValue("@Data", bytes);
                                cmd.Parameters.AddWithValue("@cid", useid);
                                cmd.Parameters.AddWithValue("@cdt", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        BindGrid();
                        grid_1();
                    }

                }

            }
                else
                {
                    grid_1();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }

            }

            else
            {
                grid_1();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            grid_1();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
        //Response.Redirect(Request.Url.AbsoluteUri);
        BindGrid();
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            string fileName, contentType;
            string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Name, Data, ContentType from Displin where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Data"];
                        contentType = sdr["ContentType"].ToString();
                        fileName = sdr["Name"].ToString();
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        
    }
  
    protected void DeleteFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);

        DataTable dt = new DataTable();

        dt = dbcon.Ora_Execute_table("delete from Displin where id='" + id + "' ");

        clear();
        BindGrid();
        grid_1();
    }

    void grid_1()
    {
        SqlCommand cmd2 = new SqlCommand("select dis_discipline_type_cd,RHD.hr_discipline_desc,format(dis_eff_dt,'dd/MM/yyyy') dis_eff_dt,format(dis_exp_dt,'dd/MM/yyyy')dis_exp_dt,dis_staff_no,dis_file,dis_catatan  from hr_discipline HD INNER JOIN Ref_hr_discipline RHD ON  HD.dis_discipline_type_cd=RHD.hr_discipline_Code where dis_staff_no ='" + Applcn_no1.Text + "'", con);
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
        BindGrid();
    }
    protected void lblSubItemName_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        CommandArgument1 = commandArgs[0];
        CommandArgument2 = commandArgs[1];
        CommandArgument3 = commandArgs[2];
        oid = CommandArgument2;
        sdd = CommandArgument3;
        dd_sebab.SelectedValue = CommandArgument1;
        TextBox1.Text = commandArgs[4];
        GP_date.Text = oid;
        //GP_amaun.Text = sdd;
        lab_fname.Text = commandArgs[3];
        GP_date.Attributes.Add("style", "pointer-events:None;");
        Button5.Visible = false;        

        if (gt_val1 == "1")
        {
            Button7.Visible = true;            
        }
        else
        {
            Button7.Visible = false;            
        }

        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select Id, Name from Displin  where td_staff_no='" + Applcn_no1.Text + "' and td_start_dt='" + oid + "' and td_dis_type_ce='" + CommandArgument1 + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView2.DataSource = ds;
            GridView2.DataBind();
            int columncount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView2.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }
        con.Close();
    }


  
    void clear()
    {
        //Kaki_no.Text = "";
        //s_nama.Text = "";
        //s_jaw.Text = "";
        //s_jab.Text = "";
        //s_gred.Text = "";        
        if (gt_val1 == "1")
        {
            Button5.Visible = true;
        }
        else
        {
            Button5.Visible = false;
        }
        Button7.Visible = false;
        dd_sebab.SelectedValue = "";
        GP_date.Attributes.Remove("style");
        GP_date.Text = "";
        TextBox1.Text = "";
        //GP_amaun.Text = "";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        if (Kaki_no.Text != "" && GP_date.Text != "")
        {
            if (Kaki_no.Text != "" && GP_date.Text != "" && dd_sebab.SelectedValue != "")
            {
                DateTime dt4 = DateTime.ParseExact(GP_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string sdt = dt4.ToString("yyyy/MM/dd");
                string edt = string.Empty;
              
                dt = dbcon.Ora_Execute_table("select dis_discipline_type_cd,dis_eff_dt,dis_exp_dt  from hr_discipline where dis_staff_no ='" + Kaki_no.Text + "' and dis_eff_dt='" + sdt + "'");
                if (dt.Rows.Count == 0)
                {
                    useid = Session["New"].ToString();

                    string Inssql = "insert into hr_discipline(dis_staff_no,dis_discipline_type_cd,dis_eff_dt,dis_exp_dt,dis_crt_id,dis_crt_dt,dis_catatan) values('" + Kaki_no.Text + "','" + dd_sebab.SelectedValue + "','" + sdt + "','" + edt + "','" + useid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','"+ TextBox1.Text + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql);
                    clear();
                    grid_1();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    grid_1();                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {
                grid_1();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }
        }
        else
        {
            grid_1();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No Kakitangan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }


    protected void DownloadFile1(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~/FILES/Attendance/" + (sender as LinkButton).CommandArgument);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();

      
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelected.PageIndex = e.NewPageIndex;
        grid_1();
    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        if (Kaki_no.Text != "")
        {
            useid = Session["New"].ToString();
            DateTime dt4 = DateTime.ParseExact(GP_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string sdt = dt4.ToString("yyyy/MM/dd");
            //DateTime dt5 = DateTime.ParseExact(GP_amaun.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //string edt = dt5.ToString("yyyy/MM/dd");
            dt = dbcon.Ora_Execute_table("select dis_discipline_type_cd,dis_eff_dt,dis_exp_dt,dis_file,dis_catatan  from hr_discipline where dis_staff_no ='" + Kaki_no.Text + "' and dis_eff_dt='" + sdt + "'");
            if (dt.Rows.Count > 0)
            {
                int contentLength = FileUpload1.PostedFile.ContentLength;//You may need it for validation
                string contentType = FileUpload1.PostedFile.ContentType;//You may need it for validation
                string fileName = FileUpload1.PostedFile.FileName;
                string fname = string.Empty;
                if (fileName != "")
                {
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/Attendance/" + fileName));//Or code to save in the DataBase.
                    fname = fileName;
                }
                else
                {
                    fname = dt.Rows[0]["dis_file"].ToString();
                }

                string upssql = "update hr_discipline set dis_catatan='"+ TextBox1.Text +"',dis_discipline_type_cd='" + dd_sebab.SelectedValue + "',dis_eff_dt ='" + sdt + "',dis_file ='" + fname + "',dis_upd_id ='" + useid + "',dis_upd_dt ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where dis_staff_no ='" + Kaki_no.Text + "' and dis_eff_dt ='" + sdt + "'";
                Status = dbcon.Ora_Execute_CommamdText(upssql);
                clear();
                GP_date.Attributes.Remove("style");                
                if (gt_val1 == "1")
                {
                    Button5.Visible = true;
                }
                else
                {
                    Button5.Visible = false;
                }
                Button7.Visible = false;
                grid_1();                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }
        else
        {
            grid_1();            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan No Kakitangan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../SUMBER_MANUSIA/Hr_Kem_Mak_Dis_view.aspx");
    }
}