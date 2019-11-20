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

public partial class ModuleMaster : System.Web.UI.MasterPage
{
    DBConnection DBCon = new DBConnection();
    StringBuilder htmlTable = new StringBuilder();
    StringBuilder calendar = new StringBuilder();
    StudentWebService service = new StudentWebService();
    string mod1, mod2, mod3, mod4, mod5, mod6;
    string level = string.Empty;
    string fileName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["New"] != null)
        {
            Notifications();
            DataTable ddicno1 = new DataTable();
            ddicno1 = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid='" + Session["New"].ToString() + "' and status='A'");
            DataTable ddicno1_stf = new DataTable();
            ddicno1_stf = DBCon.Ora_Execute_table("select *,FORMAT(stf_service_start_dt,'dd/MM/yyyy', 'en-us') as st_dt,r1.hr_jaw_desc from hr_staff_profile left join ref_hr_jawatan r1 on r1.hr_jaw_Code=stf_curr_post_cd where stf_staff_no='" + Session["New"].ToString() + "'");
            if (ddicno1.Rows[0]["KK_userid"].ToString() == "")
            {
                lbluname.Text = Session["New"].ToString().ToUpper();
            }
            else
            {
                lbluname.Text = ddicno1.Rows[0]["KK_username"].ToString().ToUpper();
            }

            string checkimage = ddicno1.Rows[0]["user_img"].ToString();
            string checkimage1 = string.Empty;
            if (ddicno1_stf.Rows.Count != 0)
            {
                checkimage1 = ddicno1_stf.Rows[0]["Stf_image"].ToString();
            }

            DataTable dd1 = new DataTable();
            dd1 = DBCon.Ora_Execute_table("select * from site_settings where Id = '1'");
            if (dd1.Rows.Count != 0)
            {

                ft_copy.Text = dd1.Rows[0]["foot_copy"].ToString();

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
        }
        else
        {
            Response.Redirect("KSAIMB_Login.aspx");
        }
    }

    void Notifications()
    {
        DataTable chk_not = new DataTable();
        chk_not = DBCon.Ora_Execute_table("select count(*) cnt from System_Notifications where sys_status='A' and sys_module_cd=''");
        if(chk_not.Rows.Count != 0)
        {
            notify_cnt.Text = chk_not.Rows[0]["cnt"].ToString();
        }
    }


   
}
