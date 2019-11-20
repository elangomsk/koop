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
using System.Security.Cryptography;
using System.Threading;

public partial class kw_bajet : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();

    StudentWebService service = new StudentWebService();
    string level;
    string Status = string.Empty;
    string userid;
    string ref_id;
    string confirmValue, am;
    string qry1 = string.Empty, qry2 = string.Empty;
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty, kod_qry = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(document).ready(function () { $(" + DropDownList1.ClientID + ").SumoSelect({ selectAll: true });$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                    BindData();
                }
                else
                {
                    ver_id.Text = "0";
                }
                bind_akaun();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('741','705','1720','742','110','1721','64','65','1722','824','61','15','77','133')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());  
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());            
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    protected void bind_gview(object sender, EventArgs e)
    {

        bind_akaun();
        string script = " $(document).ready(function () { $(" + DropDownList1.ClientID + ").SumoSelect({ selectAll: true });$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);

    }

    void bind_akaun_list()
    {
        kod_qry = "and kod_akaun NOT IN ('" + TextBox2.Text.Replace(",","','") + "')";
    }
        void bind_akaun()
    {
        var samp = Request.Url.Query;
        if (samp != "")
        {
            bind_akaun_list();
        }
        string get_qry = string.Empty;
        if (DropDownList2.SelectedValue == "0")
        {
            get_qry = "select kod_akaun,(kod_akaun +' | '+ upper(nama_akaun)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A' "+ kod_qry + " order by kod_akaun asc";
        }
        else if (DropDownList2.SelectedValue == "1")
        {
            get_qry = "select kod_akaun,(kod_akaun +' | '+ upper(nama_akaun)) as name from KW_Ref_Carta_Akaun inner join KW_Ref_Pelanggan on Ref_kod_akaun = kod_akaun where jenis_akaun_type != '1' and Status='A' " + kod_qry + " order by kod_akaun asc";
        }
        else if (DropDownList2.SelectedValue == "2")
        {
            get_qry = "select kod_akaun,(kod_akaun +' | '+ upper(nama_akaun)) as name from KW_Ref_Carta_Akaun inner join KW_Ref_Pembekal on Ref_kod_akaun = kod_akaun where jenis_akaun_type != '1' and Status='A' " + kod_qry + " order by kod_akaun asc";
        }
        else
        {

            get_qry = "select kod_akaun,(kod_akaun +' | '+ upper(nama_akaun)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' " + kod_qry + " order by kod_akaun asc";

        }

        DataSet Ds = new DataSet();
        try
        {
            string com = get_qry;
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "name";
            DropDownList1.DataValueField = "kod_akaun";
            DropDownList1.DataBind();
            //DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void view_details()
    {
        Button1.Visible = false;
        Button3.Visible = true;
        try
        {
            string lblid = lbl_name.Text;
            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select *,FORMAT(ISNULL(Ref_tk_mula,''),'dd/MM/yyyy', 'en-us') as tkmula,FORMAT(ISNULL(Ref_tk_akhir,''),'dd/MM/yyyy', 'en-us') as tkakhir From KW_Ref_Bajet where baj_sq_no='" + lblid + "' ");
            if (ddokdicno.Rows.Count != 0)
            {
                string script = " $(document).ready(function () { $(" + DropDownList1.ClientID + ").SumoSelect({ selectAll: true });$('.select2').select2();});";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
                Button4.Text = "Kemaskini";
                ver_id.Text = "1";
                get_id.Text = ddokdicno.Rows[0]["Ref_kat_bajet"].ToString();
                TextBox1.Text = ddokdicno.Rows[0]["Ref_kat_bajet"].ToString();
                TextBox2.Text = ddokdicno.Rows[0]["ref_grp_akaun"].ToString();
                dd_kumpulan.SelectedValue = ddokdicno.Rows[0]["ref_kumpulan"].ToString();
                TextBox1.Attributes.Add("Readonly", "Readonly");
                tk_mula.Text = ddokdicno.Rows[0]["tkmula"].ToString();
                tk_akhir.Text = ddokdicno.Rows[0]["tkakhir"].ToString();
                //TextBox2.Text = ddokdicno.Rows[0]["Ref_tahun_bajet"].ToString();
                TextBox4.Text = double.Parse(ddokdicno.Rows[0]["Ref_jumlah_bajet"].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                DropDownList1.ClearSelection();
                //DropDownList1.Attributes.Add("Readonly", "Readonly");
                //DropDownList1.Attributes.Add("Style", "pointer-events:None;");
                DropDownList2.Attributes.Add("Readonly", "Readonly");
                DropDownList2.Attributes.Add("Style", "pointer-events:None;");
                //string[] thisArray = ddokdicno.Rows[0]["ref_grp_akaun"].ToString().Split(',');
                //List<string> myList = new List<string>();
                //myList.AddRange(thisArray);

                //for (int i = 0; i < DropDownList1.Items.Count; i++)
                //{
                //    if (myList.Contains(DropDownList1.Items[i].Value))
                //    {
                //        DropDownList1.Items[i].Selected = true;
                //    }
                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string GetUniqueKey(int maxSize)
    {
        char[] chars = new char[62];
        chars =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        byte[] data = new byte[1];
        using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
        {
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
        }
        StringBuilder result = new StringBuilder(maxSize);
        foreach (byte b in data)
        {
            result.Append(chars[b % (chars.Length)]);
        }
        return result.ToString();
    }
    protected void clk_submit(object sender, EventArgs e)
    {
        string rcount = string.Empty, rcount1 = string.Empty, str_kod = string.Empty;
        int count = 0, count1 = 0;
        string ss1 = string.Empty;
        foreach (ListItem li in DropDownList1.Items)
        {
            if (li.Selected == true)
            {
                count++;
            }
            rcount = count.ToString();
        }
        string selectedValues = string.Empty;
        string selectedValues1 = string.Empty;
        foreach (ListItem li1 in DropDownList1.Items)
        {
            if (li1.Selected == true)
            {
                if (Int32.Parse(rcount) > (count1 + 1))
                {
                    ss1 = ",";
                }
                else
                {
                    ss1 = "";
                }

                selectedValues1 += li1.Value + ss1;

                count1++;
            }
            rcount1 = count1.ToString();
        }

        if (TextBox1.Text != "" && TextBox4.Text != "" && tk_mula.Text != "" && tk_akhir.Text != "")
        {
            string fmdate = string.Empty, tmdate = string.Empty;
            if (tk_mula.Text != "")
            {
                string fdate = tk_mula.Text;
                DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                fmdate = fd.ToString("yyyy-MM-dd");
            }
            if (tk_akhir.Text != "")
            {
                string tdate = tk_akhir.Text;
                DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tmdate = td.ToString("yyyy-MM-dd");
            }

            if (ver_id.Text == "0")
            {
                string rand_sno = string.Empty;

                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_Bajet where Ref_kat_bajet='" + TextBox1.Text.Replace("'", "''") + "' and '"+ fmdate + "' between Ref_tk_mula and Ref_tk_akhir");
                if (ddokdicno.Rows.Count == 0)
                {
                    rand_sno = GetUniqueKey(6);

                    foreach (ListItem li in DropDownList1.Items)
                    {
                        if (li.Selected == true)
                        {
                            selectedValues = li.Value;
                            string Inssql = "insert into KW_Ref_Bajet(Ref_kat_bajet,Ref_kod_akaun,Ref_jumlah_bajet,Ref_tk_mula,Ref_tk_akhir,Status,crt_id,cr_dt,ref_grp_akaun,ref_kumpulan,baj_sq_no) values ('" + TextBox1.Text.Replace("'", "''") + "','" + selectedValues + "','" + TextBox4.Text.Replace("'", "''") + "','" + fmdate + "','" + tmdate + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + selectedValues1 + "','"+ dd_kumpulan.SelectedValue +"','"+ rand_sno + "')";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql);
                        }
                    }

                    if (Status == "SUCCESS")
                    {
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Session["validate_success"] = "SUCCESS";
                        Response.Redirect("../kewengan/kw_bajet_view.aspx");
                        
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Kategori Sudah Ada.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                if(DropDownList1.SelectedValue == "")
                {
                    if (TextBox2.Text != "")
                    {
                        str_kod = TextBox2.Text;
                    }
                   
                }
                else
                {
                    if (TextBox2.Text != "")
                    {
                        str_kod = TextBox2.Text + "," + selectedValues1;
                    }
                    else
                    {
                        str_kod = selectedValues1;
                    }
                }

                foreach (ListItem li in DropDownList1.Items)
                {
                    if (li.Selected == true)
                    {
                        DataTable ddokdicno1 = new DataTable();
                        ddokdicno1 = DBCon.Ora_Execute_table("select * From KW_Ref_Bajet where Ref_kat_bajet = '" + get_id.Text + "' and Ref_kod_akaun='" + li.Value + "' and baj_sq_no='" + lbl_name.Text + "'");
                        if (ddokdicno1.Rows.Count != 0)
                        {                            
                            string Inssql = "UPDATE KW_Ref_Bajet set ref_kumpulan='"+ dd_kumpulan.SelectedValue +"',Ref_jumlah_bajet='" + TextBox4.Text.Replace("'", "''") + "',Ref_tk_mula='" + fmdate + "',Ref_tk_akhir='" + tmdate + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Ref_kat_bajet = '" + get_id.Text + "' and Ref_kod_akaun='" + li.Value + "' and baj_sq_no='"+ lbl_name.Text + "'";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql);
                        }
                        else
                        {
                            string Inssql = "insert into KW_Ref_Bajet(Ref_kat_bajet,Ref_kod_akaun,Ref_jumlah_bajet,Ref_tk_mula,Ref_tk_akhir,Status,crt_id,cr_dt,ref_grp_akaun,ref_kumpulan,baj_sq_no) values ('" + TextBox1.Text.Replace("'", "''") + "','" + li.Value + "','" + TextBox4.Text.Replace("'", "''") + "','" + fmdate + "','" + tmdate + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + str_kod + "','"+ dd_kumpulan.SelectedValue +"','"+ lbl_name.Text + "')";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql);
                        }
                    }
                    else
                    {
                        Status = "SUCCESS";
                    }
                }

                if(Status == "SUCCESS")
                {
                    if (TextBox2.Text != "")
                    {
                        string[] thisArray = str_kod.Split(',');
                        List<string> myList = new List<string>();
                        myList.AddRange(thisArray);

                        for (int j = 0; j < thisArray.Length; j++)
                        {

                            DataTable ddokdicno1 = new DataTable();
                            ddokdicno1 = DBCon.Ora_Execute_table("select * From KW_Ref_Bajet where Ref_kat_bajet = '" + get_id.Text + "' and Ref_kod_akaun='" + thisArray[j].ToString() + "' and baj_sq_no='" + lbl_name.Text + "'");
                            if (ddokdicno1.Rows.Count != 0)
                            {
                                string Inssql = "UPDATE KW_Ref_Bajet set ref_kumpulan='" + dd_kumpulan.SelectedValue + "',ref_grp_akaun='" + str_kod + "',Ref_jumlah_bajet='" + TextBox4.Text.Replace("'", "''") + "',Ref_tk_mula='" + fmdate + "',Ref_tk_akhir='" + tmdate + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Ref_kat_bajet = '" + get_id.Text + "' and Ref_kod_akaun='" + thisArray[j].ToString() + "' and baj_sq_no='" + lbl_name.Text + "'";
                                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                            }
                        }
                    }
                }

                if (Status == "SUCCESS")
                {
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Session["validate_success"] = "SUCCESS";
                    Response.Redirect("../kewengan/kw_bajet_view.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }
        
    }


    protected void BindData()
    {
        string sqry = string.Empty;
       
        con.Open();
        DataTable ddicno = new DataTable();
        //SqlCommand cmd = new SqlCommand("select s1.id,s1.Ref_kat_bajet,s1.Ref_kod_akaun,s1.Ref_jumlah_bajet,s1.Ref_tk_mula,s1.Ref_tk_akhir,case when s1.Status='A' then 'AKTIF' else 'TIDAK AKTIF' end as sts from KW_Ref_Bajet s1 " + sqry + " order by s1.ID desc", con);
        SqlCommand cmd = new SqlCommand("select s1.baj_sq_no,s1.Ref_kod_akaun,s1.Ref_kat_bajet,ISNULL(s2.nama_akaun,'') nama_akaun from KW_Ref_Bajet s1 left join KW_Ref_Carta_Akaun s2 on s2.kod_akaun=s1.Ref_kod_akaun where s1.Ref_kat_bajet='" + TextBox1.Text + "' and s1.baj_sq_no='"+ lbl_name.Text +"'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gv_refdata.DataSource = ds;
            gv_refdata.DataBind();
            int columncount = gv_refdata.Rows[0].Cells.Count;
            gv_refdata.Rows[0].Cells.Clear();
            gv_refdata.Rows[0].Cells.Add(new TableCell());
            gv_refdata.Rows[0].Cells[0].ColumnSpan = columncount;
            gv_refdata.Rows[0].Cells[0].Text = "<center>Maklumat Carian Tidak Dijumpai</center>";
        }
        else
        {
            gv_refdata.DataSource = ds;
            gv_refdata.DataBind();
        }

        con.Close();
    }

    protected void btn_hups_Click(object sender, EventArgs e)
    {
        
        string rcount = string.Empty;
        int count = 0;
        foreach (GridViewRow gvrow in gv_refdata.Rows)
        {
            var rb = gvrow.FindControl("RadioButton1") as System.Web.UI.WebControls.CheckBox;
            if (rb.Checked)
            {
                count++;
            }
            rcount = count.ToString();
        }
        if (rcount != "0")
        {
            foreach (GridViewRow row in gv_refdata.Rows)
            {
                var rbn = row.FindControl("RadioButton1") as System.Web.UI.WebControls.CheckBox;
                if (rbn.Checked)
                {
                    int RowIndex = row.RowIndex;
                    string varName1 = ((System.Web.UI.WebControls.Label)row.FindControl("Label1")).Text.ToString(); //this store the  value in varName1
                    string varName2 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl2")).Text.ToString(); //this store the  value in varName1
                    string Inssql = "delete from KW_Ref_Bajet where Ref_kat_bajet = '" + varName1 + "' and Ref_kod_akaun='"+ varName2 + "' and baj_sq_no='" + lbl_name.Text + "'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        DataTable ddokdicno1_grp = new DataTable();
                        ddokdicno1_grp = DBCon.Ora_Execute_table("SELECT STUFF ((SELECT ',' + Ref_kod_akaun  FROM KW_Ref_Bajet where Ref_kat_bajet='" + varName1 + "' and baj_sq_no='" + lbl_name.Text + "' FOR XML PATH ('')  ),1,1,'')  as scode");

                        string Inssql1 = "Update KW_Ref_Bajet set ref_grp_akaun='"+ ddokdicno1_grp.Rows[0]["scode"].ToString() + "'  where Ref_kat_bajet = '" + varName1 + "' and baj_sq_no='" + lbl_name.Text + "'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                        TextBox2.Text = ddokdicno1_grp.Rows[0]["scode"].ToString();
                        bind_akaun();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                    }
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Rekod Yang Ingin Dihapuskan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
      
        BindData();
        string script = " $(document).ready(function () { $(" + DropDownList1.ClientID + ").SumoSelect({ selectAll: true });$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_bajet.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_bajet_view.aspx");
    }

    
}