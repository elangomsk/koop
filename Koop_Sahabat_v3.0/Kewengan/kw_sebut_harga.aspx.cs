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

public partial class kw_sebut_harga : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string uniqueId;
    float total = 0, total1 = 0;
    string level, userid;
    string Status = string.Empty;
    string qry1 = string.Empty, qry2 = string.Empty;
    string CommandArgument1;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        if (set_hingga.Checked == true)
        {
            if (txtdate.Text != "")
            {
                string fdate = txtdate.Text;
                DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string s1_dt = "" + fd.ToString("yyyy") + ", " + (double.Parse(fd.ToString("MM")) - 1) + ", " + fd.ToString("dd") + "";
                string s2_dt = "" + fd.AddDays(14).ToString("yyyy") + ", " + (double.Parse(fd.AddDays(14).ToString("MM")) - 1) + ", " + fd.AddDays(14).ToString("dd") + "";
                txtdate1.Attributes.Remove("Readonly");
                txtdate1.Attributes.Remove("style");
                TextBox1.Text = s1_dt;
                TextBox6.Text = s2_dt;
            }
        }
        else
        {
            TextBox1.Text = "";
            TextBox6.Text = "";
            //txtdate1.Text = "";
            txtdate1.Attributes.Add("Readonly", "Readonly");
            txtdate1.Attributes.Add("style", "pointer-events: none;");
        }

        string script = " $(function () { var today = new Date();   var preYear = today.getFullYear() - 1; var curYear = today.getFullYear() - 0; $('.datepicker2').datepicker({ format: 'dd/mm/yyyy',autoclose: true,inline: true,startDate: new Date(" + TextBox1.Text + "),endDate: new Date(" + TextBox6.Text + ")}).on('changeDate', function(ev) {(ev.viewMode == 'days') ? $(this).datepicker('hide') : '';}); $('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);

        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                SetInitialRow();
                pelanggan();
                txtdate1.Attributes.Add("Readonly", "Readonly");
                userid = Session["New"].ToString();
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                }
                else
                {
                    GetUniquesebut();
                }


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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('784','705','1803','1750','1804','1338','785','1805','1755','61','35','15','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());        
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            Button8.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button9.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
           
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    private void GetUniquesebut()
    {
        DataTable dt1 = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='07' and Status='A'");
        if (dt1.Rows.Count != 0)
        {
            if (dt1.Rows[0]["cfmt"].ToString() == "")
            {
                txtnsh.Text = dt1.Rows[0]["fmt"].ToString();
                txtnsh.Attributes.Add("disabled", "disabled");
            }
            else
            {
                int seqno = Convert.ToInt32(dt1.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString(dt1.Rows[0]["lfrmt1"].ToString() + "0000");
                txtnsh.Text = uniqueId;
                txtnsh.Attributes.Add("disabled", "disabled");
            }

        }
        else
        {
            DataTable dt = DBCon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_Rujukan,13,2000)),'0') from KW_Penerimaan_Debit_item ");
            if (dt.Rows.Count > 0)
            {
                int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString("KTH-QUO" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                txtnsh.Text = uniqueId;

                txtnsh.Attributes.Add("disabled", "disabled");
            }
            else
            {
                int newNumber = Convert.ToInt32(uniqueId) + 1;
                uniqueId = newNumber.ToString("KTH-QUO" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                txtnsh.Text = uniqueId;

                txtnsh.Attributes.Add("disabled", "disabled");
            }
        }

    }
    void view_details()
    {
        CommandArgument1 = lbl_name.Text;
        Button1.Visible = true;
        Button8.Visible = false;
        DataTable dt = new DataTable();
        dt = DBCon.Ora_Execute_table("select  no_baucer,nama_sebut, FORMAT(tarkih_mula,'dd/MM/yyyy') tarkih_mula,FORMAT(tarkih_akhir,'dd/MM/yyyy') tarkih_akhir,tajuk,produk,deskripsi,quantiti,harga_amt harga_amt,diskaun,akaun,cukai,jumlah_amt as jumlah_amt,jumlah_keseluruhan,tot_cukai,tot_jumlah_amt from KW_Sebut_harga where no_baucer='" + CommandArgument1 + "'");
        
        if (dt.Rows.Count > 0)
        {

            txtnsh.Text = dt.Rows[0][0].ToString().Trim();
            txtnsh.Attributes.Add("Readonly", "Readonly");
            ddpela.SelectedValue = dt.Rows[0][1].ToString().Trim();
            txtdate.Text = dt.Rows[0][2].ToString().Trim();
            txtdate1.Text = dt.Rows[0][3].ToString().Trim();
            if (dt.Rows[0][3].ToString().Trim() != "")
            {
                set_hingga.Checked = true;
                //txtdate1.Attributes.Remove("Readonly");
                //txtdate1.Attributes.Remove("style");
            }
            txttajuk.Text = dt.Rows[0][4].ToString().Trim();
            TextBox14.Text = double.Parse(dt.Rows[0][13].ToString().Trim()).ToString("C").Replace("$","").Replace("RM", "");
            foreach (GridViewRow g1 in Gridview2.Rows)
            {
                (g1.FindControl("ddkod1") as DropDownList).SelectedValue = dt.Rows[0][5].ToString().Trim();
                (g1.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text = dt.Rows[0][6].ToString().Trim();
                (g1.FindControl("TextBox3") as System.Web.UI.WebControls.TextBox).Text = dt.Rows[0][7].ToString().Trim();
                (g1.FindControl("TextBox4") as System.Web.UI.WebControls.TextBox).Text = double.Parse(dt.Rows[0][8].ToString()).ToString("C").Replace("RM", "").Replace("$", "");
                (g1.FindControl("TextBox5") as System.Web.UI.WebControls.TextBox).Text = dt.Rows[0][9].ToString().Trim();
                (g1.FindControl("ddkod") as DropDownList).SelectedValue = dt.Rows[0][10].ToString().Trim();
                (g1.FindControl("TextBox7") as System.Web.UI.WebControls.TextBox).Text = dt.Rows[0][11].ToString().Trim();
                System.Web.UI.WebControls.TextBox fttax_jum = (System.Web.UI.WebControls.TextBox)Gridview2.FooterRow.FindControl("txtTotalr1");
                fttax_jum.Text = double.Parse(dt.Rows[0]["tot_cukai"].ToString()).ToString("C").Replace("RM","").Replace("$", "");
                System.Web.UI.WebControls.TextBox fttax_jum1 = (System.Web.UI.WebControls.TextBox)Gridview2.FooterRow.FindControl("txtTotal1");
                fttax_jum1.Text = double.Parse(dt.Rows[0]["tot_jumlah_amt"].ToString().Trim()).ToString("C").Replace("RM", "").Replace("$", "");
                (g1.FindControl("TextBox8") as System.Web.UI.WebControls.TextBox).Text = double.Parse(dt.Rows[0]["tot_jumlah_amt"].ToString().Trim()).ToString("C").Replace("RM", "").Replace("$", "");


            }
        }
    }
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dt.Columns.Add(new DataColumn("Column6", typeof(string)));
        dt.Columns.Add(new DataColumn("Column7", typeof(string)));
        dt.Columns.Add(new DataColumn("Column8", typeof(string)));
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dr["Column6"] = string.Empty;
        dr["Column7"] = string.Empty;
        dr["Column8"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        Gridview2.DataSource = dt;
        Gridview2.DataBind();

    }

    //protected void sel_chk_tarikh(object sender, EventArgs e)
    //{
    //    if (set_hingga.Checked == true)
    //    {
    //        string fdate = txtdate.Text;
    //        DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //        string s1_dt = "" + fd.ToString("yyyy") + ", " + (double.Parse(fd.ToString("MM")) -1) + ", " + fd.ToString("dd") + "";
    //        string s2_dt = "" + fd.AddDays(14).ToString("yyyy") + ", " + (double.Parse(fd.AddDays(14).ToString("MM")) - 1) + ", " + fd.AddDays(14).ToString("dd") + "";
    //        txtdate1.Attributes.Remove("Readonly");
    //        TextBox1.Text = s1_dt;
    //        TextBox6.Text = s2_dt;
    //        string script1 = " $(function () { var today = new Date();   var preYear = today.getFullYear() - 1; var curYear = today.getFullYear() - 0; $('.datepicker2').datepicker({ format: 'dd/mm/yyyy',autoclose: true,inline: true,startDate: new Date(" + TextBox1.Text + "),endDate: new Date(" + TextBox6.Text + ")}).on('changeDate', function(ev) {(ev.viewMode == 'days') ? $(this).datepicker('hide') : '';}); $('.select2').select2()});";
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    //    }
    //    else
    //    {
    //        TextBox1.Text = "";
    //        TextBox6.Text = "";
    //        txtdate1.Attributes.Add("Readonly", "Readonly");
    //    }
    //}
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (ddpela.SelectedItem.Text != "--- PILIH ---" && txtnsh.Text != "" && txtdate.Text != "" && txtdate1.Text != "" && txttajuk.Text != "")
        {
            
            foreach (GridViewRow g1 in Gridview2.Rows)
            {
                string Produk = (g1.FindControl("ddkod1") as System.Web.UI.WebControls.DropDownList).SelectedValue;
                string Deskripsi = (g1.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text;
                string qty = (g1.FindControl("TextBox3") as System.Web.UI.WebControls.TextBox).Text;
                string Harga = (g1.FindControl("TextBox4") as System.Web.UI.WebControls.TextBox).Text;
                string Dis = (g1.FindControl("TextBox5") as System.Web.UI.WebControls.TextBox).Text;
                string DD = (g1.FindControl("ddkod") as System.Web.UI.WebControls.DropDownList).SelectedValue;
                string Cukai = (g1.FindControl("TextBox7") as System.Web.UI.WebControls.TextBox).Text;
                string total = (g1.FindControl("TextBox8") as System.Web.UI.WebControls.TextBox).Text;
                string totcukai = string.Empty;
                System.Web.UI.WebControls.TextBox txtName = Gridview2.FooterRow.FindControl("txtTotalr1") as System.Web.UI.WebControls.TextBox;
                if (txtName.Text != "")
                {
                    totcukai = txtName.Text;
                }
                else
                {
                    totcukai = "0.00";
                }
                System.Web.UI.WebControls.TextBox txtName1 = Gridview2.FooterRow.FindControl("txtTotal1") as System.Web.UI.WebControls.TextBox;
                string tottot = txtName1.Text;

                userid = Session["New"].ToString();
                DataTable dt = new DataTable();
                //DateTime tDate = DateTime.Parse(txtdate.Text);
                //DateTime tDate1 = DateTime.Parse(txtdate1.Text);
                string fmdate = string.Empty, tmdate = string.Empty;
                if (txtdate.Text != "")
                {
                    string fdate = txtdate.Text;
                    DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    fmdate = fd.ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (txtdate1.Text != "")
                {
                    string tdate = txtdate1.Text;
                    DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    tmdate = td.ToString("yyyy-MM-dd HH:mm:ss");
                }
                decimal tgst = 0;
                decimal ttotal = 0;
                if (Cukai != "")
                {
                    tgst = Convert.ToDecimal(total) * Convert.ToDecimal(Cukai) / 100;
                    ttotal = Convert.ToDecimal(total) + tgst;
                }
                else
                {
                    tgst = (Convert.ToDecimal(total) * 0) / 100;
                    ttotal = Convert.ToDecimal(total) + tgst;
                }

                dt = DBCon.Ora_Execute_table("insert into KW_Sebut_harga values ('" + ddpela.SelectedValue + "','" + txtnsh.Text + "','" + fmdate + "','" + tmdate + "','" + txttajuk.Text + "','" + Produk + "','" + Deskripsi + "','" + qty + "','" + Harga + "','" + Dis + "','" + DD + "','" + Cukai + "','" + ttotal + "','" + totcukai + "','" + tottot + "','" + TextBox14.Text + "','B','" + userid + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','','')");
                DataTable dt_upd_format = new DataTable();
                dt_upd_format = DBCon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + txtnsh.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='07' and Status = 'A'");
            }
            Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
            Session["validate_success"] = "SUCCESS";
            Response.Redirect("../kewengan/kw_sebut_harga_view.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
                   
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddpela.SelectedItem.Text != "--- PILIH ---" && txtnsh.Text != "" && txtdate.Text != "" && txtdate1.Text != "" && txttajuk.Text != "")
        {

            foreach (GridViewRow g1 in Gridview2.Rows)
            {
                string Produk = (g1.FindControl("ddkod1") as DropDownList).SelectedValue;
                string Deskripsi = (g1.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text;
                string qty = (g1.FindControl("TextBox3") as System.Web.UI.WebControls.TextBox).Text;
                string Harga = (g1.FindControl("TextBox4") as System.Web.UI.WebControls.TextBox).Text;
                string Dis = (g1.FindControl("TextBox5") as System.Web.UI.WebControls.TextBox).Text;
                string Akaun = (g1.FindControl("ddkod") as DropDownList).SelectedValue;
                string Cukai = (g1.FindControl("TextBox7") as System.Web.UI.WebControls.TextBox).Text;
                string total = (g1.FindControl("TextBox8") as System.Web.UI.WebControls.TextBox).Text;
                DataTable dt = new DataTable();
                //DateTime tDate = DateTime.Parse(txtdate.Text);
                //DateTime tDate1 = DateTime.Parse(txtdate1.Text);
                System.Web.UI.WebControls.TextBox txtName = Gridview2.FooterRow.FindControl("txtTotalr1") as System.Web.UI.WebControls.TextBox;
                string totcukai = txtName.Text;
                System.Web.UI.WebControls.TextBox txtName1 = Gridview2.FooterRow.FindControl("txtTotal1") as System.Web.UI.WebControls.TextBox;
                string tottot = txtName1.Text;

                string fmdate = string.Empty, tmdate = string.Empty;
                if (txtdate.Text != "")
                {
                    string fdate = txtdate.Text;
                    DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    fmdate = fd.ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (txtdate1.Text != "")
                {
                    string tdate = txtdate1.Text;
                    DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    tmdate = td.ToString("yyyy-MM-dd HH:mm:ss");
                }
                dt = DBCon.Ora_Execute_table("update KW_Sebut_harga set no_baucer='" + txtnsh.Text + "',nama_sebut='" + ddpela.SelectedItem.Value + "', tarkih_mula='" + fmdate + "',tarkih_akhir='" + tmdate + "',tajuk='" + txttajuk.Text + "',produk='" + Produk + "',deskripsi='" + Deskripsi + "',quantiti='" + qty + "',harga_amt='" + Harga + "',diskaun='" + Dis + "',akaun='" + Akaun + "',cukai='" + Cukai + "',jumlah_amt='" + total + "',tot_cukai='"+ totcukai + "',tot_jumlah_amt='"+ tottot +"',jumlah_keseluruhan='"+ TextBox14.Text +"' where no_baucer='" + txtnsh.Text + "'");

            }
            Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
            Session["validate_success"] = "SUCCESS";
            Response.Redirect("../kewengan/kw_sebut_harga_view.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    void pelanggan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Ref_no_syarikat,Ref_nama_syarikat from  KW_Ref_Pelanggan";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddpela.DataSource = dt;
            ddpela.DataTextField = "Ref_nama_syarikat";
            ddpela.DataValueField = "Ref_no_syarikat";
            ddpela.DataBind();
            ddpela.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddgstdeboth_SelectedIndexChanged(object sender, EventArgs e)
    {

        int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        DropDownList c1 = (DropDownList)Gridview2.Rows[selRowIndex].FindControl("ddkod1");
        System.Web.UI.WebControls.TextBox c2 = (System.Web.UI.WebControls.TextBox)Gridview2.Rows[selRowIndex].FindControl("TextBox4");

        decimal tgst;
        //decimal tgst1;
        //if (tk_mula.Text != "")
        //{
            DataTable sel_gst = new DataTable();
            sel_gst = DBCon.Ora_Execute_table("select kod_barang,nama_barang,unit From KW_INVENTORI_BARANG where kod_barang='" + c1.SelectedValue + "'");
            if (sel_gst.Rows.Count != 0)
            {
                tgst = Convert.ToDecimal(sel_gst.Rows[0]["unit"].ToString());
            }
            else
            {
                tgst = 0;
            }
            c2.Text = tgst.ToString("C").Replace("$", "").Replace("RM", "");
        //}
        //else
        //{
        //    c1.SelectedValue = "";
        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Tarikh DO.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        //}
    }
    protected void Gridview2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlCountries = (e.Row.FindControl("ddkod") as DropDownList);
            ddlCountries.DataSource = GetData("select (kod_akaun + ' | '+ nama_akaun) nama_akaun,kod_akaun from KW_ref_carta_akaun where jenis_akaun_type != '1' and Status='A'");
            ddlCountries.DataTextField = "nama_akaun";
            ddlCountries.DataValueField = "kod_akaun";
            ddlCountries.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries.Items.Insert(0, new ListItem("--- PILIH ---"));


            //Find the DropDownList in the Row
            DropDownList ddlCountries1 = (e.Row.FindControl("ddkod1") as DropDownList);
            ddlCountries1.DataSource = GetData("select kod_barang,nama_barang,unit from KW_INVENTORI_BARANG");
            ddlCountries1.DataTextField = "nama_barang";
            ddlCountries1.DataValueField = "kod_barang";
            ddlCountries1.DataBind();

            //Add Default Item in the DropDownList
            ddlCountries1.Items.Insert(0, new ListItem("--- PILIH ---"));

            //Select the Country of Customer in DropDownList

        }
    }

    private DataSet GetData(string query)
    {
        string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
        SqlCommand cmd = new SqlCommand(query);
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataSet ds = new DataSet())
                {
                    sda.Fill(ds);
                    return ds;
                }
            }
        }
    }

    protected void QtyChanged(object sender, EventArgs eventArgs)
    {
        double numTotal = 0;
        double unit1 = 0;
        string fmdate = string.Empty, fmdate1 = string.Empty, fmdate2 = string.Empty;
        GridViewRow row = ((GridViewRow)((System.Web.UI.WebControls.TextBox)sender).NamingContainer);

        DropDownList kod_bor = (DropDownList)row.FindControl("Col1");
        System.Web.UI.WebControls.TextBox qty = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox3");
        System.Web.UI.WebControls.TextBox unit = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox4");
        System.Web.UI.WebControls.TextBox disc = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox5");
        System.Web.UI.WebControls.TextBox tax = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox7");
        System.Web.UI.WebControls.TextBox jum = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox8");
        System.Web.UI.WebControls.TextBox ft_jum = (System.Web.UI.WebControls.TextBox)Gridview2.FooterRow.FindControl("txtTotal1");

        if (unit.Text != "")
        {
            unit1 =  double.Parse(qty.Text) * double.Parse(unit.Text);
        }
        jum.Text = unit1.ToString("C").Replace("RM","").Replace("$", "");
        ft_jum.Text = jum.Text;
        TextBox14.Text = ft_jum.Text;
    }

    protected void QtyChanged1(object sender, EventArgs eventArgs)
    {
        double numTotal = 0;
        double unit2 = 0;
        string fmdate = string.Empty, fmdate1 = string.Empty, fmdate2 = string.Empty;
        GridViewRow row = ((GridViewRow)((System.Web.UI.WebControls.TextBox)sender).NamingContainer);

        //DropDownList kod_bor = (DropDownList)row.FindControl("Col1");
        System.Web.UI.WebControls.TextBox qty = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox3");
        System.Web.UI.WebControls.TextBox unit = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox4");
        //System.Web.UI.WebControls.TextBox disc = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox5");
        System.Web.UI.WebControls.TextBox tax = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox7");
        System.Web.UI.WebControls.TextBox jum = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox8");
        System.Web.UI.WebControls.TextBox fttax_jum = (System.Web.UI.WebControls.TextBox)Gridview2.FooterRow.FindControl("txtTotalr1");

        if (unit.Text != "" && qty.Text != "")
        {
            unit2 = ((double.Parse(jum.Text) * double.Parse(tax.Text)) / 100);
        }
        //jum.Text = unit2.ToString();
       fttax_jum.Text = unit2.ToString("C").Replace("RM", "").Replace("$", "");
        numTotal = double.Parse(jum.Text) + double.Parse(fttax_jum.Text);
        TextBox14.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
    }

    protected void QtyChanged2(object sender, EventArgs eventArgs)
    {
        double numTotal = 0;
        double unit2 = 0;
        string fmdate = string.Empty, fmdate1 = string.Empty, fmdate2 = string.Empty;
        GridViewRow row = ((GridViewRow)((System.Web.UI.WebControls.TextBox)sender).NamingContainer);

        //DropDownList kod_bor = (DropDownList)row.FindControl("Col1");
        System.Web.UI.WebControls.TextBox qty = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox3");
        System.Web.UI.WebControls.TextBox unit = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox4");
        System.Web.UI.WebControls.TextBox disc = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox5");
        System.Web.UI.WebControls.TextBox tax = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox7");
        System.Web.UI.WebControls.TextBox jum = (System.Web.UI.WebControls.TextBox)row.FindControl("TextBox8");
        System.Web.UI.WebControls.TextBox fttax_jum = (System.Web.UI.WebControls.TextBox)Gridview2.FooterRow.FindControl("txtTotalr1");
        System.Web.UI.WebControls.TextBox ft_jum = (System.Web.UI.WebControls.TextBox)Gridview2.FooterRow.FindControl("txtTotal1");

        if (jum.Text != "")
        {
            if (disc.Text != "")
            {
                unit2 = ((double.Parse(qty.Text) * double.Parse(unit.Text)) - (((double.Parse(qty.Text) * double.Parse(unit.Text)) * double.Parse(disc.Text)) / 100));
            }
            else
            {
                unit2 = ((double.Parse(qty.Text) * double.Parse(unit.Text)));
            }
        }
        jum.Text =  unit2.ToString("C").Replace("RM", "").Replace("$", "");
        ft_jum.Text = unit2.ToString("C").Replace("RM", "").Replace("$", "");
        numTotal = double.Parse(jum.Text);
        TextBox14.Text = numTotal.ToString("C").Replace("RM", "").Replace("$", "");
        //var samp = Request.Url.Query;
        //if (samp != "")
        //{
        //    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
        //    view_details();
        //}
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_sebut_harga.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_sebut_harga_view.aspx");
    }

    
}