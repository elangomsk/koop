using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Globalization;
using System.Threading;

public partial class KSAIMB_NEW : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StringBuilder htmlTable = new StringBuilder();
    StringBuilder htmlTable_mob = new StringBuilder();
    StringBuilder calendar = new StringBuilder();
    StudentWebService service = new StudentWebService();
    DataTable lnk_vew1 = new DataTable();
    DataTable lnk_vew2 = new DataTable();
    DataTable lnk_vew3 = new DataTable();
    string lnk_shw1 = string.Empty, lnk_shw2 = string.Empty, lnk_shw3 = string.Empty, lnk_shw4 = string.Empty;
    string llk1 = string.Empty, llk2 = string.Empty, llk3 = string.Empty, llk4 = string.Empty;
    string mod1, mod2, mod3, mod4, mod5, mod6;
    string level = string.Empty;
    string fileName = string.Empty;
    DataTable chk_menu = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["New"] != null)
        {
            Notifications();
            DropDownList drp = (DropDownList)Page.Master.FindControl("dd_lang");
            drp.SelectedIndexChanged += new EventHandler(drp_SelectedIndexChanged);

            if (!IsPostBack)
            {
                dd_lang.SelectedValue = Session["site_languaage"].ToString();
            }

            DataTable dd1 = new DataTable();
            dd1 = DBCon.Ora_Execute_table("select * from site_settings where Id = '1'");
            if (dd1.Rows.Count != 0)
            {
                
                ft_copy.Text = dd1.Rows[0]["foot_copy"].ToString();

            }

         
            //String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            //strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
            string user = Session["New"].ToString();
            DataTable ddicno1 = new DataTable();
            ddicno1 = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + Session["New"].ToString() + "'");
            DataTable ddicno1_stf = new DataTable();
            ddicno1_stf = DBCon.Ora_Execute_table("select *,FORMAT(stf_service_start_dt,'dd/MM/yyyy', 'en-us') as st_dt,r1.hr_jaw_desc from hr_staff_profile left join ref_hr_jawatan r1 on r1.hr_jaw_Code=stf_curr_post_cd where stf_staff_no='" + Session["New"].ToString() + "'");
            if (ddicno1.Rows[0]["KK_userid"].ToString() == "")
            {
                //lbluname.Text = Session["New"].ToString();
                lbluname1.Text = Session["New"].ToString();
            }
            else
            {
                //lbluname.Text = ddicno1.Rows[0]["KK_username"].ToString();
                lbluname1.Text = ddicno1.Rows[0]["KK_username"].ToString();
            }

            string checkimage = ddicno1.Rows[0]["user_img"].ToString();
            string checkimage1 = string.Empty;
            if (ddicno1_stf.Rows.Count != 0)
            {
                checkimage1 = ddicno1_stf.Rows[0]["Stf_image"].ToString();
            }



            string fileimg1 = Path.GetFileName(checkimage1);
            string fileimg = Path.GetFileName(checkimage);
            if (fileimg1 != "")
            {
                ImgPrv_top_small.Attributes.Add("src", "../Files/user/" + fileimg1);
            }
            else if (fileimg != "")
            {
                ImgPrv_top_small.Attributes.Add("src", "../Files/user/" + fileimg);
            }
            else
            {
                //ImgPrv_top.Attributes.Add("src", "../Files/user/user.gif");
                ImgPrv_top_small.Attributes.Add("src", "../Files/user/user.gif");
            }
            string fullPath = Request.Url.AbsolutePath;
            if (System.IO.Path.GetFileName(fullPath).ToString() != "kw_tutup_akaun.aspx")
            {
                fileName = System.IO.Path.GetFileName(fullPath);
            }
            else
            {
                fileName = "kw_profil_syarikat_view.aspx";
            }
            get_link();
            Load_menus();
            Load_sub_menus();            
          
        }
        else
        {
            Response.Redirect("KSAIMB_Login.aspx");
        }
    }

    void Notifications()
    {
        DataTable chk_not = new DataTable();
        chk_not = DBCon.Ora_Execute_table("select count(*) cnt from System_Notifications where sys_status='A' and sys_module_cd='"+ Session["mnu_id"].ToString() + "'");
        if (chk_not.Rows.Count != 0)
        {
            notify_cnt.Text = chk_not.Rows[0]["cnt"].ToString();
        }
        notifications1();
    }
    void drp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string Inssql = "Update site_settings SET language='" + dd_lang.SelectedValue + "' where Id = '1'";
        //Status = DBCon.Ora_Execute_CommamdText(Inssql);
        Session["site_languaage"] = dd_lang.SelectedValue;
        Response.Redirect(Request.RawUrl);
    }
   
    void chk_level()
    {
              
    }

    public class Protopbaners
    {
        public string mod_val1 { get; set; }
        public string mod_val2 { get; set; }
        public string mod_val3 { get; set; }
        public string mod_image_path { get; set; }
        public string mod_val4 { get; set; }
        public string mod_val5 { get; set; }

        public string mod_val6 { get; set; }
        public string mod_val7 { get; set; }
        public string mod_val8 { get; set; }

    }

   
    protected void Load_menus()
    {
        
        CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
        TextInfo txtinfo = culinfo.TextInfo;
        DataTable ddicno1_stf = new DataTable();
        ddicno1_stf = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + Session["New"].ToString() + "'");
        if (Session["mnu_id"].ToString() == "M0001")
        {
            gt_dshbrd.Attributes.Add("href", "/HR_Dashboard.aspx");
        }
        else if (Session["mnu_id"].ToString() == "M0002")
        {
            gt_dshbrd.Attributes.Add("href", "/Finance_dashboard.aspx");
        }
        else if (Session["mnu_id"].ToString() == "M0004")
        {
            dash_lbl1.Visible = false;
        }
        else if (Session["mnu_id"].ToString() == "M0007" && Session["roles"].ToString() == "R0016")
        {
            dash_lbl1.Visible = false;
        }
        else
        {
            gt_dshbrd.Attributes.Add("href", "/Dashboard.aspx");
        }
        
        if (fileName == "HR_Dashboard.aspx" || fileName == "Dashboard.aspx" || fileName == "Finance_dashboard.aspx" || fileName == "HR_recent_cuti.aspx")
        {
            dash_lbl1.Attributes.Add("class", "active owl-item-active");
        }
        else
        {
            dash_lbl1.Attributes.Add("class", "active");
        }

        DataSet ds = new DataSet();
        DataTable FromTable = new DataTable();
        List<Protopbaners> details1 = new List<Protopbaners>();
        con.Open();
        string cmdstr = "select s1.skrin_id,s1.sub_skrin_id,s2.position,s2.KK_Sskrin_name,s2.KK_skrin_link,s2.subskrin_ikon from KK_Role_skrins s1 left join KK_PID_Sub_Skrin s2 on s2.KK_Skrin_id=s1.skrin_id and s1.sub_skrin_id=s2.KK_Sskrin_id and s2.Status='A' where Role_id IN ('" + ddicno1_stf.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "') and KK_Skrin_id='" + Session["mnu_id"].ToString() + "' group by s1.skrin_id,s1.sub_skrin_id,s2.position,s2.KK_Sskrin_name,s2.KK_skrin_link,s2.subskrin_ikon order by cast(s2.position as int) asc";
        //string cmdstr = "select s1.skrin_id,s2.position,s2.KK_Skrin_name,s2.skrin_ikon from KK_Role_skrins s1 left join KK_PID_Skrin s2 on s2.KK_Skrin_id=s1.skrin_id where Role_id IN ('" + ddicno1_stf.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "') group by s1.skrin_id,s2.position,s2.KK_Skrin_name,s2.skrin_ikon order by cast(s2.position as int) asc";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.ExecuteNonQuery();
        FromTable = ds.Tables[0];
        if (FromTable.Rows.Count != 0)
        {
            foreach (DataRow dtrow in FromTable.Rows)
            {
                Protopbaners user1 = new Protopbaners();

                user1.mod_val1 = dtrow["skrin_id"].ToString();
                user1.mod_val2 = dtrow["sub_skrin_id"].ToString();
                user1.mod_val3 = txtinfo.ToTitleCase(dtrow["KK_Sskrin_name"].ToString().ToLower());
                user1.mod_val4 = dtrow["KK_skrin_link"].ToString();
                user1.mod_val5 = dtrow["subskrin_ikon"].ToString();
                if(dtrow["KK_skrin_link"].ToString() == "#")
                {
                    user1.mod_val6 = "data-toggle='tab' href='#"+ dtrow["sub_skrin_id"].ToString() + "'";
                }
                else
                {
                    user1.mod_val6 = "href='/"+ dtrow["KK_skrin_link"].ToString() + "'";
                }

                if (dtrow["sub_skrin_id"].ToString() == llk2)
                {
                    user1.mod_val7 = "owl-item-active";
                }
                else
                {
                    user1.mod_val7 = "";
                }


                details1.Add(user1);
            }
            ds.Dispose();
            con.Close();
            bnd_mmenus.DataSource = details1.ToArray();
            bnd_mmenus.DataBind();
            //mobile_view
            //bnd_mmenus_mob.DataSource = details1.ToArray();
            //bnd_mmenus_mob.DataBind();
        }
    }

   
    void get_link()
    {
        DataTable ddicno1_stf = new DataTable();
        ddicno1_stf = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + Session["New"].ToString() + "'");

        DataTable bind_menu = new DataTable();
        bind_menu = DBCon.Ora_Execute_table("select s1.skrin_id,s2.position,s2.KK_Skrin_name,s2.skrin_ikon from KK_Role_skrins s1 left join KK_PID_Skrin s2 on s2.KK_Skrin_id=s1.skrin_id where Role_id IN ('" + ddicno1_stf.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "') group by s1.skrin_id,s2.position,s2.KK_Skrin_name,s2.skrin_ikon order by cast(s2.position as int) asc");
        var samp = Request.Url.Query;
       
        if (bind_menu.Rows.Count > 0)
        {
            lnk_vew1 = DBCon.Ora_Execute_table("select * from KK_PID_sub_Skrin where status='A' and (KK_skrin_link like '%" + fileName + "%' OR KK_skrin_link1 like '%" + fileName + "%')");
            if (lnk_vew1.Rows.Count != 0)
            {
                llk1 = lnk_vew1.Rows[0]["KK_Skrin_id"].ToString();
                llk2 = lnk_vew1.Rows[0]["KK_Sskrin_id"].ToString();
                lnk_shw1 = "menu-open dl-subviewopen";
                lnk_shw2 = "menu-open";
                lnk_shw3 = "";
                lnk_shw4 = "";
            }
            else
            {
                if (fileName == "Fin_module.aspx")
                {
                    lnk_vew2 = DBCon.Ora_Execute_table("select * from KK_PID_presub_Skrin where status='A' and ( KK_preskrin_link like '%" + Request.QueryString["edit"] + "%' OR KK_preskrin_link1 like '%" + Request.QueryString["edit"] + "%')");
                }
                else if (fileName == "HR_KM_pen_prestasi2.aspx")
                {
                    lnk_vew2 = DBCon.Ora_Execute_table("select * from KK_PID_presub_Skrin where status='A' and ( KK_preskrin_link like '%HR_KM_pen_prestasi1_view%' OR KK_preskrin_link1 like '%HR_KM_pen_prestasi1_view%')");
                }
                else
                {
                    lnk_vew2 = DBCon.Ora_Execute_table("select * from KK_PID_presub_Skrin where status='A' and ( KK_preskrin_link like '%" + fileName + "%' OR KK_preskrin_link1 like '%" + fileName + "%')");
                }
                if (lnk_vew2.Rows.Count != 0)
                {
                    llk1 = lnk_vew2.Rows[0]["KK_Skrin_id"].ToString();
                    llk2 = lnk_vew2.Rows[0]["KK_Sskrin_id"].ToString();
                    llk3 = lnk_vew2.Rows[0]["KK_Spreskrin_id"].ToString();
                    lnk_shw1 = "menu-open dl-subview";
                    lnk_shw2 = "menu-open dl-subviewopen";
                    lnk_shw3 = "menu-open";
                    lnk_shw4 = "";
                }
                else
                {

                    lnk_vew3 = DBCon.Ora_Execute_table("select * from KK_PID_presub1_Skrin where status='A' and ( KK_preskrin1_link like '%" + fileName + "%' OR KK_preskrin1_link1 like '%" + fileName + "%')");
                    if (lnk_vew3.Rows.Count != 0)
                    {
                        llk1 = lnk_vew3.Rows[0]["KK_Skrin_id"].ToString();
                        llk2 = lnk_vew3.Rows[0]["KK_Sskrin_id"].ToString();
                        llk3 = lnk_vew3.Rows[0]["KK_Spreskrin_id"].ToString();
                        llk4 = lnk_vew3.Rows[0]["KK_Spreskrin1_id"].ToString();
                        lnk_shw1 = "menu-open dl-subview";
                        lnk_shw2 = "menu-open dl-subview";
                        lnk_shw3 = "menu-open dl-subviewopen";
                        lnk_shw4 = "menu-open";
                    }
                }
            }
        }
    }
    protected void Load_sub_menus()
    {
        string main_mm1 = string.Empty, main_mm2 = string.Empty, main_mm3 = string.Empty, main_mm4 = string.Empty;
        //if (Session["site_languaage"].ToString() == "mal") { main_mm1 = "KK_Skrin_name"; main_mm2 = "KK_Sskrin_name"; main_mm3 = "KK_Spreskrin_name"; main_mm4 = "KK_Spreskrin1_name"; } else { main_mm1 = "KK_Skrin_name_en"; main_mm2 = "KK_Sskrin_name_en"; main_mm3 = "KK_Spreskrin_name_en"; main_mm4 = "KK_Spreskrin1_name_en"; }
        CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
        TextInfo txtinfo = culinfo.TextInfo;
        DataTable ddicno1_stf = new DataTable();
        ddicno1_stf = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + Session["New"].ToString() + "'");

        DataSet ds = new DataSet();
        DataTable FromTable = new DataTable();
        List<Protopbaners> details1 = new List<Protopbaners>();
        con.Close();
        con.Open();
        string cmdstr = "select s1.skrin_id,s1.sub_skrin_id,s2.position,s2.KK_Sskrin_name,s2.KK_skrin_link,s2.subskrin_ikon from KK_Role_skrins s1 left join KK_PID_Sub_Skrin s2 on s2.KK_Skrin_id=s1.skrin_id and s1.sub_skrin_id=s2.KK_Sskrin_id and s2.Status='A' where Role_id IN ('" + ddicno1_stf.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "') and KK_Skrin_id='" + Session["mnu_id"].ToString() + "' group by s1.skrin_id,s1.sub_skrin_id,s2.position,s2.KK_Sskrin_name,s2.KK_skrin_link,s2.subskrin_ikon order by cast(s2.position as int) asc";
        //string cmdstr = "select s1.skrin_id,s2.position,s2.KK_Skrin_name,s2.skrin_ikon from KK_Role_skrins s1 left join KK_PID_Skrin s2 on s2.KK_Skrin_id=s1.skrin_id where Role_id IN ('" + ddicno1_stf.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "') group by s1.skrin_id,s2.position,s2.KK_Skrin_name,s2.skrin_ikon order by cast(s2.position as int) asc";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.ExecuteNonQuery();
        FromTable = ds.Tables[0];
        if (FromTable.Rows.Count != 0)
        {
            foreach (DataRow dtrow in FromTable.Rows)
            {
                DataTable bind_presmenu = new DataTable();
                bind_presmenu = DBCon.Ora_Execute_table("select s1.skrin_id,s1.sub_skrin_id,s1.psub_skrin_id,s2.position,s2.KK_Spreskrin_name,s2.KK_preskrin_link from KK_Role_skrins s1 left join KK_PID_presub_Skrin s2 on s2.KK_Skrin_id=s1.skrin_id and s1.sub_skrin_id=s2.KK_Sskrin_id and s1.psub_skrin_id=s2.KK_Spreskrin_id  where Role_id IN ('" + ddicno1_stf.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "') and s1.psub_skrin_id != '' and KK_Skrin_id='" + dtrow["skrin_id"].ToString() + "' and KK_Sskrin_id IN ('" + dtrow["sub_skrin_id"].ToString() + "') and s2.Status='A' group by s1.skrin_id,s1.sub_skrin_id,s1.psub_skrin_id,s2.position,s2.KK_Spreskrin_name,s2.KK_preskrin_link order by cast(s2.position as int) asc");
                Protopbaners user1 = new Protopbaners();

                user1.mod_val1 = dtrow["skrin_id"].ToString();
                user1.mod_val2 = dtrow["sub_skrin_id"].ToString();
                user1.mod_val3 = txtinfo.ToTitleCase(dtrow["KK_Sskrin_name"].ToString().ToLower());
                user1.mod_val4 = dtrow["KK_skrin_link"].ToString();
                user1.mod_val5 = dtrow["subskrin_ikon"].ToString();
                if (llk2 == dtrow["sub_skrin_id"].ToString())
                {
                    main_mm1 = "active";
                }
                else
                {
                    main_mm1 = "";
                }
                htmlTable.Append("<div id='"+ dtrow["sub_skrin_id"].ToString() + "' class='tab-pane kt-tab-menu-bg animated flipInX " + main_mm1 + "'><ul class='kt-main-menu-dropdown'>");
                for (int k = 0; k < bind_presmenu.Rows.Count; k++)
                {
                    if (llk3 == bind_presmenu.Rows[k]["psub_skrin_id"].ToString())
                    {
                        main_mm2 = "owl-item-active";
                    }
                    else
                    {
                        main_mm2 = "";
                    }
                        DataTable bind_presmenu1 = new DataTable();
                    bind_presmenu1 = DBCon.Ora_Execute_table("select s1.skrin_id,s1.sub_skrin_id,s1.psub_skrin_id,s1.psub1_skrin_id,s2.position,s2.KK_Spreskrin1_name,s2.KK_preskrin1_link from KK_Role_skrins s1 left join KK_PID_presub1_Skrin s2 on s2.KK_Skrin_id=s1.skrin_id and s1.sub_skrin_id=s2.KK_Sskrin_id and s1.psub_skrin_id=s2.KK_Spreskrin_id and s1.psub1_skrin_id=s2.KK_Spreskrin1_id where Role_id IN ('" + ddicno1_stf.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "') and s1.psub1_skrin_id != '' and KK_Skrin_id='" + dtrow["skrin_id"].ToString() + "' and KK_Sskrin_id IN ('" + dtrow["sub_skrin_id"].ToString() + "') and KK_Spreskrin_id IN ('" + bind_presmenu.Rows[k]["psub_skrin_id"].ToString() + "') group by s1.skrin_id,s1.sub_skrin_id,s1.psub_skrin_id,s1.psub1_skrin_id,s2.position,s2.KK_Spreskrin1_name,s2.KK_preskrin1_link order by cast(s2.position as int) asc");
                    string sv1 = string.Empty;
                    if (bind_presmenu.Rows[k]["KK_preskrin_link"].ToString() != "#")
                    {
                        sv1 = "";
                    }
                    else
                    {
                        sv1 = "data-toggle='tab'";
                    }


                    htmlTable.Append("<li class='dropdown "+ main_mm2 + "'><a class='dropbtn' "+ sv1 + " href='" + "/" + bind_presmenu.Rows[k]["KK_preskrin_link"].ToString() + "'>" + txtinfo.ToTitleCase(bind_presmenu.Rows[k]["KK_Spreskrin_name"].ToString().ToLower()) + "</a>");
                    if (bind_presmenu1.Rows.Count != 0)
                    {
                        htmlTable.Append("<div class='dropdown-content'>");
                        for (int m = 0; m < bind_presmenu1.Rows.Count; m++)
                        {
                            htmlTable.Append("<a href = '" + "/" + bind_presmenu1.Rows[m]["KK_preskrin1_link"].ToString() + "'> "+ txtinfo.ToTitleCase(bind_presmenu1.Rows[m]["KK_Spreskrin1_name"].ToString().ToLower()) + " </ a >");
                        }
                        htmlTable.Append("</div></li>");
                    }
                }
                    htmlTable.Append("</ul></div>");

                details1.Add(user1);
            }
            plholder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
          
            //mobile_view
            //plholder1_mob.Controls.Add(new Literal { Text = htmlTable.ToString() });
        }
        con.Close();
    }

  
    public class Protopbaners_notify
    {
        public string not_val1 { get; set; }
        public string not_val2 { get; set; }
        public string not_val3 { get; set; }
        public string not_val4 { get; set; }

    }

    protected void notifications1()
    {
        
        DataSet ds = new DataSet();
        DataTable FromTable = new DataTable();
        List<Protopbaners_notify> details2 = new List<Protopbaners_notify>();
        con.Open();
        string cmdstr = "select * from System_Notifications where sys_status='A'";        
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.ExecuteNonQuery();
        FromTable = ds.Tables[0];
        if (FromTable.Rows.Count != 0)
        {
            foreach (DataRow dtrow in FromTable.Rows)
            {
                Protopbaners_notify user2 = new Protopbaners_notify();

                user2.not_val1 = dtrow["sys_header"].ToString();
                user2.not_val2 = dtrow["sys_notifications"].ToString();
                user2.not_val3 = dtrow["sys_not_year"].ToString();
                user2.not_val4 = dtrow["sys_status"].ToString();
                details2.Add(user2);
            }
            ds.Dispose();
            
          
        }
        else
        {
            Protopbaners_notify user2 = new Protopbaners_notify();
            user2.not_val1 = "EMPTY !";
            details2.Add(user2);
        }
        bnd_notifications.DataSource = details2.ToArray();
        bnd_notifications.DataBind();
        con.Close();
    }
    static string GetParentUriString(Uri uri)
    {
        return uri.AbsoluteUri.Remove(uri.AbsoluteUri.Length - uri.Segments.Last().Length);
    }
}
