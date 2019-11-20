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
using System.Text.RegularExpressions;
using System.Threading;

public partial class KW_jadual_aset : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    decimal total = 0, total1 = 0;
    string qry1 = string.Empty, qry2 = string.Empty;
    DataTable dd_check_peluamt_1 = new DataTable();
    DataTable dd_check_peluamt_2 = new DataTable();
    DataTable dd_check_peluamt_3 = new DataTable();
    string level;
    string uniqueId;
    string Status = string.Empty;
    string userid;
    string get_stt = string.Empty, get_edt = string.Empty;
    string str_sdt1 = string.Empty, end_edt1 = string.Empty, str_year = string.Empty, end_year = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //app_language();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        DataTable sel_gst2 = new DataTable();
        sel_gst2 = DBCon.Ora_Execute_table("select top(1) Format(tarikh_mula,'dd/MM/yyyy') as st_dt,Format(tarikh_akhir,'dd/MM/yyyy') as end_dt from kw_profile_syarikat where cur_sts='1' order by tarikh_akhir desc");

        if (sel_gst2.Rows.Count != 0)
        {
            get_stt = sel_gst2.Rows[0]["st_dt"].ToString();
            get_edt = sel_gst2.Rows[0]["end_dt"].ToString();
            DateTime fd = DateTime.ParseExact(get_stt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime fd1 = DateTime.ParseExact(get_edt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            str_sdt1 = fd.ToString("yyyy-MM-dd");
            end_edt1 = fd1.ToString("yyyy-MM-dd");
            str_year = fd.ToString("yyyy");
            end_year = fd1.ToString("yyyy");


        }
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                TextBox2.Attributes.Add("Readonly", "Readonly");
                TextBox3.Attributes.Add("Readonly", "Readonly");
                //Button5.Attributes.Add("Style", "pointer-events:None;");
                //ver_id.Text = "0";

                bind_ponos();
                GetUniqueInv();
                //gd_month();
                get_kategory();
                BindDropdown();
                BindData();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    private void GetUniqueInv()
    {
        DataTable dt1 = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='08' and Status='A'");
        if (dt1.Rows.Count != 0)
        {
            if (dt1.Rows[0]["cfmt"].ToString() == "")
            {
                txtpvno.Text = dt1.Rows[0]["fmt"].ToString();
                txtpvno.Attributes.Add("disabled", "disabled");
            }
            else
            {
                int seqno = Convert.ToInt32(dt1.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString(dt1.Rows[0]["lfrmt1"].ToString() + "0000");
                txtpvno.Text = uniqueId;
                txtpvno.Attributes.Add("disabled", "disabled");
            }

        }
        else
        {
            DataTable dt = DBCon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(Replace(Pv_no, ' ', ''),13,2000)),'0')  from KW_Pembayaran_Pay_voucer");
            if (dt.Rows.Count > 0)
            {
                int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString("KS-JNO" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                txtpvno.Text = uniqueId;
                txtpvno.Attributes.Add("disabled", "disabled");

            }
            else
            {
                int newNumber = Convert.ToInt32(uniqueId) + 1;
                uniqueId = newNumber.ToString("KS-JNO" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                txtpvno.Text = uniqueId;
                txtpvno.Enabled = false;
                txtpvno.Attributes.Add("disabled", "disabled");
            }
        }



    }
    //void gd_month()
    //{

    //    DataSet Ds = new DataSet();
    //    try
    //    {

    //        string com = "select hr_month_Code,UPPER(hr_month_desc) as hr_month_desc from Ref_hr_month ORDER BY get_sts desc";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DropDownList1.DataSource = dt;
    //        DropDownList1.DataBind();
    //        DropDownList1.DataTextField = "hr_month_desc";
    //        DropDownList1.DataValueField = "hr_month_Code";
    //        DropDownList1.DataBind();
    //        DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    //DropDownList1.SelectedValue = abc.PadLeft(2, '0');
    //}

    void get_kategory()
    {

        DataSet Ds = new DataSet();
        try
        {

            string com = "select kod_akaun,nama_akaun from KW_Ref_Carta_Akaun where jenis_akaun='01' and Status='A'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataBind();
            DropDownList2.DataTextField = "nama_akaun";
            DropDownList2.DataValueField = "kod_akaun";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        //DropDownList1.SelectedValue = abc.PadLeft(2, '0');
    }
    void BindDropdown()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kod_syarikat,nama_syarikat + ' (' + (FORMAT(ISNULL(tarikh_mula,''),'dd/MM/yyyy', 'en-us')) +' - ' + (FORMAT(ISNULL(tarikh_akhir,''),'dd/MM/yyyy', 'en-us')) + ')' as name from KW_Profile_syarikat where cur_sts='1'";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            Tahun_kew.DataSource = dt;
            Tahun_kew.DataTextField = "name";
            Tahun_kew.DataValueField = "kod_syarikat";
            Tahun_kew.DataBind();
            Tahun_kew.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    void bind_ponos()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select UPPER(pur_po_no) as pur_po_no from ast_purchase where pur_mp_sts ='Telah Dibayar' group by pur_po_no except select po_no from KW_Aset_jurnal group by po_no";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_pono.DataSource = dt;
            dd_pono.DataTextField = "pur_po_no";
            dd_pono.DataValueField = "pur_po_no";
            dd_pono.DataBind();
            dd_pono.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void bind_pono_list(object sender, EventArgs e)
    {
        DataTable dd_check_payment = new DataTable();
        dd_check_payment = DBCon.Ora_Execute_table("select ISNULL(SUM(payamt),'') as payamt from KW_Pembayaran_Pay_voucer where no_po like '%" + dd_pono.SelectedValue + "%'");
        if (dd_check_payment.Rows[0]["payamt"].ToString() != "0.00")
        {
            TextBox2.Text = double.Parse(dd_check_payment.Rows[0]["payamt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            //txt_debit_akn.Text = dd_check_payment.Rows[0]["Deb_kod_akaun"].ToString();
            //txt_inv.Text = dd_check_payment.Rows[0]["no_invois"].ToString();
            //txt_pv_dt.Text = dd_check_payment.Rows[0]["tarkih_pv"].ToString();
        }
        else
        {
            TextBox2.Text = "0.00";
        }


        dd_check_peluamt_1 = DBCon.Ora_Execute_table("select ISNULL(s2.dis_pel_jum,'0.00') as amt1 from ast_component s1 inner join ast_dispose s2 on s2.dis_asset_id=s1.com_asset_id where com_po_no='" + dd_pono.SelectedValue + "'");
        if (dd_check_peluamt_1.Rows.Count != 0)
        {
            TextBox3.Text = double.Parse(dd_check_peluamt_1.Rows[0]["amt1"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
        }
        else
        {

            dd_check_peluamt_2 = DBCon.Ora_Execute_table("select ISNULL(s2.dis_pel_jum,'0.00') as amt1 from ast_car s1 inner join ast_dispose s2 on s2.dis_asset_id=s1.car_asset_id where car_po_no='" + dd_pono.SelectedValue + "'");
            if (dd_check_peluamt_2.Rows.Count != 0)
            {
                TextBox3.Text = double.Parse(dd_check_peluamt_2.Rows[0]["amt1"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            }
            else
            {

                dd_check_peluamt_3 = DBCon.Ora_Execute_table("select ISNULL(s2.dis_pel_jum,'0.00') as amt1 from ast_inventory s1 inner join ast_dispose s2 on s2.dis_asset_id=s1.inv_asset_id where inv_po_no='" + dd_pono.SelectedValue + "'");
                if (dd_check_peluamt_3.Rows.Count != 0)
                {
                    TextBox3.Text = double.Parse(dd_check_peluamt_3.Rows[0]["amt1"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                }
                else
                {
                    TextBox3.Text = "0.00";
                }
            }
        }

    }

    protected void sel_tahun(object sender, EventArgs e)
    {
        BindData();
    }


    protected void Button10_Click(object sender, EventArgs e)
    {
        if (Tahun_kew.SelectedValue != "")
        {
            if (dd_pono.SelectedValue != "")
            {

                if (DropDownList2.SelectedValue != "")
                {
                    if (TextBox6.Text != "")
                    {
                        string kat_val = string.Empty, mon_amt = string.Empty;
                        string qry_clmn1 = string.Empty, qry_clmn2 = string.Empty, ssv_qry1 = string.Empty, ssv_qry2 = string.Empty;
                        DataTable dd_check_payment = new DataTable();
                        DataTable get_jurnal_vls = new DataTable();
                        DataTable get_pay_voucher = new DataTable();
                        dd_check_payment = DBCon.Ora_Execute_table("select * from ast_purchase where pur_po_no='" + dd_pono.SelectedValue + "' and pur_del_ind !='D'");


                        DataTable dd_check_rows = new DataTable();
                        dd_check_rows = DBCon.Ora_Execute_table("select *,(FORMAT(ISNULL(tarikh_mula,''),'dd/MM/yyyy', 'en-us')) as sdt,(FORMAT(ISNULL(tarikh_akhir,''),'dd/MM/yyyy', 'en-us')) as edt from KW_Profile_syarikat where kod_syarikat='" + Tahun_kew.SelectedValue + "' and cur_sts='1'");
                        int tot_cnt = 12;
                        string mnth_1 = string.Empty, mnth_1_1 = string.Empty, year_1 = string.Empty, Status1 = string.Empty;
                        for (int k = 0; k < dd_check_payment.Rows.Count; k++)
                        {
                            for (int i = 0; i <= tot_cnt; i++)
                            {

                                if (dd_check_payment.Rows.Count != 0)
                                {

                                    DateTime fd = DateTime.ParseExact(dd_check_rows.Rows[0]["sdt"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    DateTime ed = DateTime.ParseExact(dd_check_rows.Rows[0]["edt"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    if (i <= 1)
                                    {
                                        mnth_1 = fd.ToString("yyyy-MM");
                                    }
                                    else
                                    {

                                        mnth_1 = fd.AddMonths(i - 1).ToString("yyyy-MM");

                                    }

                                    string[] mnth_2 = mnth_1.Split('-');

                                    kat_val = dd_check_payment.Rows[k]["pur_asset_cat_cd"].ToString();
                                    DataTable dd_check_jurnal = new DataTable();
                                    //ssv_qry1 = get_jurnal_vls.Rows[k][qry_clmn1 +"_asset_id"].ToString();
                                    //ssv_qry2 = get_jurnal_vls.Rows[k][qry_clmn1 + "_asset_sub_cat_cd"].ToString();
                                    dd_check_jurnal = DBCon.Ora_Execute_table("select * from KW_Aset_jurnal where profil='" + Tahun_kew.SelectedValue + "' and year='" + mnth_2[0] + "' and month='" + mnth_2[1] + "' and po_no='" + dd_pono.SelectedValue + "' and pv_no='" + dd_check_payment.Rows[k]["pur_pv_no"].ToString() + "'");
                                    if (dd_check_jurnal.Rows.Count == 0)
                                    {
                                        get_pay_voucher = DBCon.Ora_Execute_table("select * from KW_Pembayaran_Pay_voucer where Pv_no='" + dd_check_payment.Rows[k]["pur_pv_no"].ToString() + "'");
                                        if (i == 0)
                                        {
                                            DataTable chk_data = new DataTable();
                                            chk_data = DBCon.Ora_Execute_table("select * from KW_Aset_jurnal where profil='" + Tahun_kew.SelectedValue + "' and kod_akaun='" + DropDownList2.SelectedValue + "' and year='" + mnth_2[0] + "' and pv_no=''");
                                            if (chk_data.Rows.Count == 0)
                                            {

                                                DataTable get_oamt = new DataTable();
                                                get_oamt = DBCon.Ora_Execute_table("select distinct m1.kod_akaun,nama_akaun,ISNULL(op.opening_amt,'0.00') as open_amt from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance op on op.kod_akaun=m1.kod_akaun and start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fd.ToString("yyyy-MM-dd") + "'), 0) and end_dt<=DATEADD(day, DATEDIFF(day, 0, '" + ed.ToString("yyyy-MM-dd") + "'), +1) and set_sts='1'  where  jenis_akaun='01' and m1.kod_akaun='" + DropDownList2.SelectedValue + "'");
                                                for (int o = 1; o <= tot_cnt; o++)
                                                {
                                                    DateTime fd1 = DateTime.ParseExact(dd_check_rows.Rows[0]["sdt"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                    DateTime ed1 = DateTime.ParseExact(dd_check_rows.Rows[0]["edt"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                                    mnth_1_1 = fd.AddMonths(o - 1).ToString("yyyy-MM");
                                                    string[] mnth_2_1 = mnth_1_1.Split('-');
                                                    string Inssql = "Insert into KW_Aset_jurnal ([profil],[year],[month],[kategory],[sub_kategory],[qty],[kod_akaun_debit],[kod_akaun],[po_no],[pv_no],[inv_no],[tarikh_pv],[baki_bersih],[penambatan_amt],[pelubusan_amt],[susut_nilai],[mon_jumlah_amt],[sust_perkumpul_amt],[bersih_samasa_amt],[Status],[jana_sts],[crt_id],[cr_dt]) VALUES('" + Tahun_kew.SelectedValue + "','" + mnth_2_1[0] + "','" + mnth_2_1[1] + "','','','','','" + DropDownList2.SelectedValue + "','baki','','','','" + get_oamt.Rows[0]["open_amt"].ToString() + "','','','" + TextBox6.Text + "','0.00','0.00','0.00','A','0','" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string Inssql = "Insert into KW_Aset_jurnal ([profil],[year],[month],[kategory],[sub_kategory],[qty],[kod_akaun_debit],[kod_akaun],[po_no],[pv_no],[inv_no],[tarikh_pv],[baki_bersih],[penambatan_amt],[pelubusan_amt],[susut_nilai],[mon_jumlah_amt],[sust_perkumpul_amt],[bersih_samasa_amt],[Status],[jana_sts],[crt_id],[cr_dt]) VALUES('" + Tahun_kew.SelectedValue + "','" + mnth_2[0] + "','" + mnth_2[1] + "','" + kat_val + "','" + dd_check_payment.Rows[k]["pur_asset_sub_cat_cd"].ToString() + "','" + dd_check_payment.Rows[k]["pur_verify_qty"].ToString() + "','" + get_pay_voucher.Rows[0]["Deb_kod_akaun"].ToString() + "','" + DropDownList2.SelectedValue + "','" + dd_pono.SelectedValue + "','" + dd_check_payment.Rows[k]["pur_pv_no"].ToString() + "','" + txt_inv.Text + "','" + dd_check_payment.Rows[k]["pur_tarikh_bayaran"].ToString() + "','0.00','" + dd_check_payment.Rows[k]["pur_asset_tot_amt"].ToString() + "','" + TextBox3.Text + "','" + TextBox6.Text + "','0.00','0.00','0.00','A','0','" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                            Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                        }
                                        if (Status == "SUCCESS")
                                        {
                                            string Inssql1 = "delete from KW_Aset_jurnal_temp where profil='" + Tahun_kew.SelectedValue + "' and  year='" + mnth_2[0] + "' and month='" + mnth_2[1] + "' and po_no='" + dd_pono.SelectedValue + "' and pv_no='" + dd_check_payment.Rows[k]["pur_pv_no"].ToString() + "'";
                                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                                            string Inssql2 = "delete from KW_Aset_jurnal_temp where profil='" + Tahun_kew.SelectedValue + "' and  year='" + mnth_2[0] + "' and month='" + mnth_2[1] + "' and po_no='baki' and pv_no=''";
                                            Status1 = DBCon.Ora_Execute_CommamdText(Inssql2);
                                        }
                                    }

                                }
                            }

                        }
                        if (Status == "SUCCESS")
                        {
                            BindData();
                            bind_ponos();
                            DropDownList2.SelectedValue = "";
                            TextBox2.Text = "";
                            TextBox3.Text = "";
                            TextBox6.Text = "";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Susut Nilai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Kategory Aset.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan PO No.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Profil Syarikat.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void temperary_Click(object sender, EventArgs e)
    {
        if (Tahun_kew.SelectedValue != "")
        {
            if (dd_pono.SelectedValue != "")
            {
                if (TextBox6.Text != "")
                {
                    string kat_val = string.Empty, mon_amt = string.Empty;
                    string qry_clmn1 = string.Empty, qry_clmn2 = string.Empty, ssv_qry1 = string.Empty, ssv_qry2 = string.Empty;
                    DataTable dd_check_payment = new DataTable();
                    DataTable get_jurnal_vls = new DataTable();
                    DataTable get_pay_voucher = new DataTable();
                    dd_check_payment = DBCon.Ora_Execute_table("select * from ast_purchase where pur_po_no='" + dd_pono.SelectedValue + "' and pur_del_ind !='D'");


                    DataTable dd_check_rows = new DataTable();
                    dd_check_rows = DBCon.Ora_Execute_table("select *,(FORMAT(ISNULL(tarikh_mula,''),'dd/MM/yyyy', 'en-us')) as sdt,(FORMAT(ISNULL(tarikh_akhir,''),'dd/MM/yyyy', 'en-us')) as edt from KW_Profile_syarikat where kod_syarikat='" + Tahun_kew.SelectedValue + "' and cur_sts='1'");
                    int tot_cnt = 12;
                    string mnth_1 = string.Empty, mnth_1_1 = string.Empty, year_1 = string.Empty, Status1 = string.Empty;
                    for (int k = 0; k < dd_check_payment.Rows.Count; k++)
                    {


                        if (dd_check_payment.Rows.Count != 0)
                        {

                            DataTable dd_check_jurnal = new DataTable();

                            for (int i = 0; i < tot_cnt; i++)
                            {
                                DateTime fd = DateTime.ParseExact(dd_check_rows.Rows[0]["sdt"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                DateTime ed = DateTime.ParseExact(dd_check_rows.Rows[0]["edt"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                mnth_1 = fd.AddMonths(i - 1).ToString("yyyy-MM");

                                string[] mnth_2 = mnth_1.Split('-');

                                kat_val = dd_check_payment.Rows[k]["pur_asset_cat_cd"].ToString();
                                dd_check_jurnal = DBCon.Ora_Execute_table("select * from KW_Aset_jurnal_temp where profil='" + Tahun_kew.SelectedValue + "' and year='" + mnth_2[0] + "' and month='" + mnth_2[1] + "' and po_no='" + dd_pono.SelectedValue + "' and pv_no='" + dd_check_payment.Rows[k]["pur_pv_no"].ToString() + "'");
                                //if (dd_check_jurnal.Rows.Count == 0)
                                //{
                                get_pay_voucher = DBCon.Ora_Execute_table("select * from KW_Pembayaran_Pay_voucer where Pv_no='" + dd_check_payment.Rows[k]["pur_pv_no"].ToString() + "'");



                                if (i == 0)
                                {
                                    DataTable chk_data = new DataTable();
                                    chk_data = DBCon.Ora_Execute_table("select * from KW_Aset_jurnal_temp where profil='" + Tahun_kew.SelectedValue + "' and kod_akaun='" + DropDownList2.SelectedValue + "' and year='" + mnth_2[0] + "' and pv_no=''");


                                    DataTable get_oamt = new DataTable();
                                    get_oamt = DBCon.Ora_Execute_table("select distinct m1.kod_akaun,nama_akaun,ISNULL(op.opening_amt,'0.00') as open_amt from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance op on op.kod_akaun=m1.kod_akaun and start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fd.ToString("yyyy-MM-dd") + "'), 0) and end_dt<=DATEADD(day, DATEDIFF(day, 0, '" + ed.ToString("yyyy-MM-dd") + "'), +1) and set_sts='1'  where  jenis_akaun='01' and m1.kod_akaun='" + DropDownList2.SelectedValue + "'");
                                    for (int o = 1; o <= tot_cnt; o++)
                                    {
                                        DateTime fd1 = DateTime.ParseExact(dd_check_rows.Rows[0]["sdt"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        DateTime ed1 = DateTime.ParseExact(dd_check_rows.Rows[0]["edt"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                        mnth_1_1 = fd.AddMonths(o - 1).ToString("yyyy-MM");
                                        string[] mnth_2_1 = mnth_1_1.Split('-');
                                        string Inssql1 = "delete from KW_Aset_jurnal_temp where profil='" + Tahun_kew.SelectedValue + "' and year='" + mnth_2_1[0] + "' and month='" + mnth_2_1[1] + "' and po_no='baki'";
                                        Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                                        if (Status1 == "SUCCESS")
                                        {
                                            //if (chk_data.Rows.Count == 0)
                                            //{
                                            string Inssql = "Insert into KW_Aset_jurnal_temp ([profil],[year],[month],[kategory],[sub_kategory],[qty],[kod_akaun_debit],[kod_akaun],[po_no],[pv_no],[inv_no],[tarikh_pv],[baki_bersih],[penambatan_amt],[pelubusan_amt],[susut_nilai],[mon_jumlah_amt],[sust_perkumpul_amt],[bersih_samasa_amt],[Status],[jana_sts],[crt_id],[cr_dt]) VALUES('" + Tahun_kew.SelectedValue + "','" + mnth_2_1[0] + "','" + mnth_2_1[1] + "','','','0','','" + DropDownList2.SelectedValue + "','baki','','','','" + get_oamt.Rows[0]["open_amt"].ToString() + "','','','" + TextBox6.Text + "','0.00','0.00','0.00','A','0','" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                            Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                            //}
                                        }
                                    }

                                }
                                else
                                {
                                    string Inssql1 = "delete from KW_Aset_jurnal_temp where profil='" + Tahun_kew.SelectedValue + "' and year='" + mnth_2[0] + "' and month='" + mnth_2[1] + "' and po_no='" + dd_pono.SelectedValue + "' and pv_no='" + dd_check_payment.Rows[k]["pur_pv_no"].ToString() + "'";
                                    Status1 = DBCon.Ora_Execute_CommamdText(Inssql1);
                                    if (Status1 == "SUCCESS")
                                    {
                                        string Inssql = "Insert into KW_Aset_jurnal_temp ([profil],[year],[month],[kategory],[sub_kategory],[qty],[kod_akaun_debit],[kod_akaun],[po_no],[pv_no],[inv_no],[tarikh_pv],[baki_bersih],[penambatan_amt],[pelubusan_amt],[susut_nilai],[mon_jumlah_amt],[sust_perkumpul_amt],[bersih_samasa_amt],[Status],[jana_sts],[crt_id],[cr_dt]) VALUES('" + Tahun_kew.SelectedValue + "','" + mnth_2[0] + "','" + mnth_2[1] + "','" + kat_val + "','" + dd_check_payment.Rows[k]["pur_asset_sub_cat_cd"].ToString() + "','" + dd_check_payment.Rows[k]["pur_verify_qty"].ToString() + "','" + get_pay_voucher.Rows[0]["Deb_kod_akaun"].ToString() + "','" + DropDownList2.SelectedValue + "','" + dd_pono.SelectedValue + "','" + dd_check_payment.Rows[k]["pur_pv_no"].ToString() + "','" + txt_inv.Text + "','" + dd_check_payment.Rows[k]["pur_tarikh_bayaran"].ToString() + "','0.00','" + dd_check_payment.Rows[k]["pur_asset_tot_amt"].ToString() + "','" + TextBox3.Text + "','" + TextBox6.Text + "','0.00','0.00','0.00','A','0','" + userid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                    }
                                }
                                //}
                            }

                        }
                    }
                    if (Status == "SUCCESS")
                    {
                        load_cetak1();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan PO No.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan PO No.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Profil Syarikat.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }


    protected void jana_Click(object sender, EventArgs e)
    {

        string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty, fmdate = string.Empty, todate = string.Empty;
        string set_cnt1_1 = string.Empty, set_cnt1_2 = string.Empty, mcnt1_1 = string.Empty;
        string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
        string sno1_1 = string.Empty, sno2_1 = string.Empty, sno3_1 = string.Empty, cnt_value1_1 = string.Empty;
        DataTable cnt_no = new DataTable();
        DataTable cnt_no1_1 = new DataTable();
        DataTable get_years = new DataTable();
        get_years = DBCon.Ora_Execute_table("select *,(FORMAT(ISNULL(tarikh_mula,''),'dd/MM/yyyy', 'en-us')) as sdt,(FORMAT(ISNULL(tarikh_akhir,''),'dd/MM/yyyy', 'en-us')) as tdt from KW_Profile_syarikat where kod_syarikat='" + Tahun_kew.SelectedValue + "' and cur_sts='1'");
        //string fdate = get_years.Rows[0]["sdt"].ToString();
        //DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //fmdate = fd.ToString("MM-yyyy");
        //string tdate = get_years.Rows[0]["tdt"].ToString();
        //DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //todate = td.ToString("MM-yyyy");
        DataTable dd_check_jurnal = new DataTable();
        dd_check_jurnal = DBCon.Ora_Execute_table("select year,month,kategory,sum(mon_jumlah_amt) as amt1,pv_no from KW_Aset_jurnal where profil='" + Tahun_kew.SelectedValue + "' and pv_no != '' and jana_sts='1' group by year,month,kategory,pv_no");
        if (dd_check_jurnal.Rows.Count != 0)
        {



            DataTable gen_invkd = new DataTable();
            gen_invkd = DBCon.Ora_Execute_table("select jurnal_no from KW_Aset_jurnal where jurnal_no='" + txtpvno.Text + "'");
            if (gen_invkd.Rows.Count > 0)
            {
                DataTable dtpv_1 = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 10) as lfrmt1,SUBSTRING(cur_format, 11, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='08' and Status='A'");
                if (dtpv_1.Rows.Count != 0)
                {
                    if (dtpv_1.Rows[0]["cfmt"].ToString() == "")
                    {
                        txt_jurnal_no.Text = dtpv_1.Rows[0]["fmt"].ToString();
                    }
                    else
                    {
                        int seqno = Convert.ToInt32(dtpv_1.Rows[0]["lfrmt2"].ToString());
                        int newNumber = seqno + 1;
                        uniqueId = newNumber.ToString(dtpv_1.Rows[0]["lfrmt1"].ToString() + "0000");
                        txt_jurnal_no.Text = uniqueId;
                    }

                }
                else
                {
                    DataTable dtpv_2 = DBCon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(Pv_no,13,2000)),'0')  from KW_Pembayaran_Pay_voucer");
                    if (dtpv_2.Rows.Count > 0)
                    {
                        int seqno = Convert.ToInt32(dtpv_2.Rows[0][0].ToString());
                        int newNumber = seqno + 1;
                        uniqueId = newNumber.ToString("KTH-JNO" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                        txt_jurnal_no.Text = uniqueId;
                    }
                    else
                    {
                        int newNumber = Convert.ToInt32(uniqueId) + 1;
                        uniqueId = newNumber.ToString("KTH-JNO" + "-" + DateTime.Now.ToString("yy") + "-" + "0000");
                        txt_jurnal_no.Text = uniqueId;
                    }
                }
            }
            else
            {
                GetUniqueInv();
                txt_jurnal_no.Text = txtpvno.Text;
            }

            for (int k = 0; k < dd_check_jurnal.Rows.Count; k++)
            {
                string kat_name = string.Empty, kat_name1 = string.Empty;




                DataTable gen_invkd_01 = new DataTable();
                gen_invkd_01 = DBCon.Ora_Execute_table("select ast_kategori_code,ast_kategori_desc from Ref_ast_kategori where ast_kategori_code='" + dd_check_jurnal.Rows[k]["kategory"].ToString() + "'");

                kat_name = "susut nilai terkumpul - " + gen_invkd_01.Rows[0]["ast_kategori_desc"].ToString();
                kat_name1 = "susut nilai - " + gen_invkd_01.Rows[0]["ast_kategori_desc"].ToString();

                //carta akaun kredit


                //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
                cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='09'");

                set_cnt1 = cnt_no.Rows[0]["cnt"].ToString();
                mcnt = cnt_no.Rows[0]["rcnt"].ToString();
                set_cnt = "09";



                DataTable cnt_no1 = new DataTable();
                cnt_no1 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_akaun_cnt,'') +1) as mcnt From KW_Ref_Carta_Akaun where jenis_akaun='09' order by kod_akaun desc");

                if (cnt_no1.Rows.Count != 0)
                {
                    cnt_value = cnt_no1.Rows[0]["mcnt"].ToString();
                }
                else
                {
                    cnt_value = "1";
                }
                string chk_role = string.Empty;
                string[] sp_no = set_cnt.Split('.');
                int sp_no1 = sp_no.Length;
                string ss1 = string.Empty;
                for (int i = 0; i <= sp_no1; i++)
                {
                    if (i == (Int32.Parse(mcnt) - 1))
                    {
                        sno1 = cnt_value.PadLeft(2, '0');
                    }
                    else
                    {
                        sno1 = sp_no[i].ToString();
                    }
                    if (i < (sp_no1))
                    {
                        ss1 = ".";
                    }
                    else
                    {
                        ss1 = "";
                    }
                    chk_role += (sno1).PadLeft(2, '0') + "" + ss1;
                }



                //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_akaun='" + get_cd.Text + "'");
                cnt_no1_1 = DBCon.Ora_Execute_table("select Id as cnt,(jenis_akaun_type + 1) as rcnt,kod_akaun,under_jenis,jenis_akaun_type From KW_Ref_Carta_Akaun where kod_akaun='12.57'");

                set_cnt1_2 = cnt_no1_1.Rows[0]["cnt"].ToString();
                mcnt1_1 = cnt_no1_1.Rows[0]["rcnt"].ToString();
                set_cnt1_1 = "12.57";



                DataTable cnt_no2 = new DataTable();
                cnt_no2 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_akaun_cnt,'') +1) as mcnt From KW_Ref_Carta_Akaun where jenis_akaun='12.57' order by kod_akaun desc");

                if (cnt_no2.Rows.Count != 0)
                {
                    cnt_value1_1 = cnt_no2.Rows[0]["mcnt"].ToString();
                }
                else
                {
                    cnt_value1_1 = "1";
                }
                string chk_role2 = string.Empty;
                string[] sp_no2 = set_cnt1_1.Split('.');
                int sp_no1_2 = sp_no2.Length;
                string ss1_2 = string.Empty;
                for (int m = 0; m <= sp_no1_2; m++)
                {
                    if (m == (Int32.Parse(mcnt1_1) - 1))
                    {
                        sno2_1 = cnt_value1_1.PadLeft(2, '0');
                    }
                    else
                    {
                        sno2_1 = sp_no2[m].ToString();
                    }
                    if (m < (sp_no1_2))
                    {
                        ss1_2 = ".";
                    }
                    else
                    {
                        ss1_2 = "";
                    }
                    chk_role2 += (sno2_1).PadLeft(2, '0') + "" + ss1_2;
                }

                //string sso1 = string.Empty, sso2 = string.Empty, sso3 = string.Empty;
                //if (set_cnt1 == "0")
                //{
                //    sso1 = "";
                //}
                //else
                //{
                //    if (cnt_no.Rows[0]["jenis_akaun_type"].ToString() == "1")
                //    {
                //        sso1 = chk_role + "," + cnt_no.Rows[0]["kod_akaun"].ToString();
                //    }
                //    else
                //    {
                //        sso1 = chk_role + "," + cnt_no.Rows[0]["under_jenis"].ToString();
                //    }
                //}

                string kre_kod = string.Empty, deb_kod = string.Empty, kre_kat_kod = string.Empty, deb_kat_kod = string.Empty;
                DataTable get_val1_kat_kredit = new DataTable();
                get_val1_kat_kredit = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kat_akaun='09' and nama_akaun='" + kat_name + "'");

                DataTable get_val1_kat_debit = new DataTable();
                get_val1_kat_debit = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where jenis_akaun='12.57' and nama_akaun='" + kat_name1 + "'");
                //kredit
                if (get_val1_kat_kredit.Rows.Count == 0)
                {
                    DataTable get_val = new DataTable();
                    get_val = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='09'");
                    string Inssql1 = "insert into KW_Ref_Carta_Akaun(kat_akaun,nama_akaun,kod_akaun,jenis_akaun,under_parent,KW_Debit_amt,KW_kredit_amt,Kw_open_amt,jenis_akaun_type,under_jenis,Status,crt_id,cr_dt,Susu_nilai,kod_akaun_cnt,sts_kawalan,ct_kod_industry) values ('" + get_val.Rows[0]["kat_akaun"].ToString() + "','" + kat_name + "','" + chk_role + "','09','" + set_cnt1 + "','0.00','0.00','0.00','" + mcnt + "','','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0','" + cnt_value + "','Y','')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                    kre_kod = chk_role;
                    kre_kat_kod = get_val.Rows[0]["kat_akaun"].ToString();
                }
                else
                {
                    kre_kod = get_val1_kat_kredit.Rows[0]["kod_akaun"].ToString();
                    kre_kat_kod = get_val1_kat_kredit.Rows[0]["kat_akaun"].ToString();
                }
                //Debit
                if (get_val1_kat_debit.Rows.Count == 0)
                {
                    DataTable get_val1 = new DataTable();
                    get_val1 = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='12.57'");
                    string Inssql11 = "insert into KW_Ref_Carta_Akaun(kat_akaun,nama_akaun,kod_akaun,jenis_akaun,under_parent,KW_Debit_amt,KW_kredit_amt,Kw_open_amt,jenis_akaun_type,under_jenis,Status,crt_id,cr_dt,Susu_nilai,kod_akaun_cnt,sts_kawalan,ct_kod_industry) values ('" + get_val1.Rows[0]["kat_akaun"].ToString() + "','" + kat_name1 + "','" + chk_role2 + "','12.57','" + set_cnt1_2 + "','0.00','0.00','0.00','" + mcnt1_1 + "','','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0','" + cnt_value1_1 + "','Y','')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql11);
                    deb_kod = chk_role2;
                    deb_kat_kod = get_val1.Rows[0]["kat_akaun"].ToString();
                }
                else
                {
                    deb_kod = get_val1_kat_debit.Rows[0]["kod_akaun"].ToString();
                    deb_kat_kod = get_val1_kat_debit.Rows[0]["kat_akaun"].ToString();
                }

                //DataTable get_jl = new DataTable();
                //get_jl = DBCon.Ora_Execute_table("select * From KW_Aset_jurnal where kod_akaun='12.57'");

                string ins_debit = "insert into KW_General_Ledger values('" + deb_kod + "','" + dd_check_jurnal.Rows[k]["amt1"].ToString() + "','0.00','" + Session["new"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + deb_kat_kod + "','" + txt_jurnal_no.Text + "','" + dd_check_jurnal.Rows[k]["pv_no"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','SUSUT NILAI - " + dd_check_jurnal.Rows[k]["month"].ToString() + "/" + dd_check_jurnal.Rows[k]["year"].ToString() + "','','','','','A','')";
                Status = DBCon.Ora_Execute_CommamdText(ins_debit);

                // Kredit Query

                string ins_kredit = "insert into KW_General_Ledger values('" + kre_kod + "','0.00','" + dd_check_jurnal.Rows[k]["amt1"].ToString() + "','" + Session["new"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','" + kre_kat_kod + "','" + txt_jurnal_no.Text + "','" + dd_check_jurnal.Rows[k]["pv_no"].ToString() + "','','0.00','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','SUSUT NILAI TERKUMPUL - " + dd_check_jurnal.Rows[k]["month"].ToString() + "/" + dd_check_jurnal.Rows[k]["year"].ToString() + "','','','','','A','')";
                Status = DBCon.Ora_Execute_CommamdText(ins_kredit);

                //update jurnal

                DataTable dt_upd_jurnal = new DataTable();
                dt_upd_jurnal = DBCon.Ora_Execute_table("update KW_Aset_jurnal set jana_sts='2',jurnal_no='" + txt_jurnal_no.Text + "',tarikh_jurnal='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where profil='" + Tahun_kew.SelectedValue + "' and year='" + dd_check_jurnal.Rows[k]["year"].ToString() + "' and month='" + dd_check_jurnal.Rows[k]["month"].ToString() + "' and pv_no = '" + dd_check_jurnal.Rows[k]["pv_no"].ToString() + "'");

            }


            if (Status == "SUCCESS")
            {
                DataTable dt_upd_format = new DataTable();
                dt_upd_format = DBCon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + txt_jurnal_no.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='08' and Status = 'A'");

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }


    }

    protected void kira_Click(object sender, EventArgs e)
    {
        if (Tk_akhir.Text != "")
        {
            decimal pre_amt = 0;
            decimal amt1 = 0;
            decimal mon_amt1 = 0;
            decimal jum_mon_amt1 = 0;
            decimal bers_mon_amt1 = 0;
            string fmdate = string.Empty;
            string fdate1 = Tk_akhir.Text;
            DateTime fd = DateTime.ParseExact(fdate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fmdate = fd.ToString("yyyy-MM-dd");
            DataTable get_years = new DataTable();
            get_years = DBCon.Ora_Execute_table("select *,(FORMAT(ISNULL(tarikh_mula,''),'dd/MM/yyyy', 'en-us')) as sdt,(FORMAT(ISNULL(tarikh_akhir,''),'dd/MM/yyyy', 'en-us')) as tdt from KW_Profile_syarikat where kod_syarikat='" + Tahun_kew.SelectedValue + "' and cur_sts='1'");



            DataTable dd_check_jurnal = new DataTable();
            dd_check_jurnal = DBCon.Ora_Execute_table("select * from KW_Aset_jurnal where profil='" + Tahun_kew.SelectedValue + "' and year='" + fd.ToString("yyyy") + "' and month='" + fd.ToString("MM") + "' union all select * from KW_Aset_jurnal where profil='" + Tahun_kew.SelectedValue + "' and year='" + fd.ToString("yyyy") + "' and kategory ='' and jana_sts='0'");
            if (dd_check_jurnal.Rows.Count != 0)
            {
                string Inssql = string.Empty, Inssql1 = string.Empty;
                for (int k = 0; k < dd_check_jurnal.Rows.Count; k++)
                {
                    pre_amt = decimal.Parse(dd_check_jurnal.Rows[k]["baki_bersih"].ToString());
                    amt1 = decimal.Parse(dd_check_jurnal.Rows[k]["susut_nilai"].ToString());
                    mon_amt1 = ((((pre_amt + decimal.Parse(dd_check_jurnal.Rows[k]["penambatan_amt"].ToString())) * amt1) / 12) / 100);

                    DataTable get_jumlah = new DataTable();
                    get_jumlah = DBCon.Ora_Execute_table("select ISNULL(sum(mon_jumlah_amt),'0.00') as amt1 from KW_Aset_jurnal where profil='" + Tahun_kew.SelectedValue + "' and year='" + fd.ToString("yyyy") + "' and month='" + fd.ToString("MM") + "' and po_no='" + dd_check_jurnal.Rows[k]["po_no"].ToString() + "' and pv_no='" + dd_check_jurnal.Rows[k]["pv_no"].ToString() + "'");
                    if (get_jumlah.Rows.Count != 0)
                    {
                        jum_mon_amt1 = decimal.Parse(get_jumlah.Rows[0]["amt1"].ToString());
                        bers_mon_amt1 = (((pre_amt + decimal.Parse(dd_check_jurnal.Rows[k]["penambatan_amt"].ToString())) - decimal.Parse(dd_check_jurnal.Rows[k]["pelubusan_amt"].ToString())) - jum_mon_amt1);
                    }
                    if (dd_check_jurnal.Rows[k]["pv_no"].ToString() != "")
                    {
                        Inssql = "Update KW_Aset_jurnal set jana_sts='1',[mon_jumlah_amt]='" + mon_amt1 + "',[sust_perkumpul_amt]='" + jum_mon_amt1 + "',[bersih_samasa_amt]='" + bers_mon_amt1 + "',[upd_id]='" + userid + "',[upd_dt]='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where profil='" + Tahun_kew.SelectedValue + "' and year='" + fd.ToString("yyyy") + "' and month='" + fd.ToString("MM") + "'  and po_no='" + dd_check_jurnal.Rows[k]["po_no"].ToString() + "' and pv_no='" + dd_check_jurnal.Rows[k]["pv_no"].ToString() + "' and jana_sts='0'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql);

                        Inssql1 = "Update KW_Aset_jurnal set jana_sts='1',[sust_perkumpul_amt]='" + jum_mon_amt1 + "',[bersih_samasa_amt]='" + bers_mon_amt1 + "',[upd_id]='" + userid + "',[upd_dt]='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where profil='" + Tahun_kew.SelectedValue + "' and year='" + fd.ToString("yyyy") + "' and po_no='" + dd_check_jurnal.Rows[k]["po_no"].ToString() + "' and pv_no='" + dd_check_jurnal.Rows[k]["pv_no"].ToString() + "' and jana_sts='0'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1);

                    }
                    else
                    {
                        Inssql = "Update KW_Aset_jurnal set jana_sts='1',[mon_jumlah_amt]='" + mon_amt1 + "',[sust_perkumpul_amt]='" + jum_mon_amt1 + "',[bersih_samasa_amt]='" + bers_mon_amt1 + "',[upd_id]='" + userid + "',[upd_dt]='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where profil='" + Tahun_kew.SelectedValue + "' and year='" + fd.ToString("yyyy") + "' and kategory ='' and month='" + fd.ToString("MM") + "'  and po_no='baki' and jana_sts='0'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql);

                        Inssql1 = "Update KW_Aset_jurnal set jana_sts='1',[sust_perkumpul_amt]='" + jum_mon_amt1 + "',[bersih_samasa_amt]='" + bers_mon_amt1 + "',[upd_id]='" + userid + "',[upd_dt]='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where profil='" + Tahun_kew.SelectedValue + "' and year='" + fd.ToString("yyyy") + "' and po_no='baki' and kategory ='' and jana_sts='0'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                    }



                }

                if (Status == "SUCCESS")
                {

                    BindData();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Bulan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void sel_pro(object sender, EventArgs e)
    {
        BindData();
    }
    protected void BindData()
    {
        load_cetak();
    }


    void load_cetak()
    {
        if (Tahun_kew.SelectedValue != "")
        {
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty, val6 = string.Empty, pre_day = string.Empty, pre_year = string.Empty, pre_year1 = string.Empty, pre_year2 = string.Empty;

            string fmdate = string.Empty, tmdate = string.Empty;
            //string mnth_name1 = "[01]", mnth_name2 = "[02]", mnth_name3 = "[03]", mnth_name4 = "[04]", mnth_name5 = "[05]", mnth_name6 = "[06]", mnth_name7 = "[07]", mnth_name8 = "[08]", mnth_name9 = "[09]", mnth_name10 = "[10]", mnth_name11 = "[11]", mnth_name12 = "[12]";
            //string get_dt = "[" + sdate.Year.ToString() + "/" + ss2 + "]" + ss1;

            DataTable dd_check_rows = new DataTable();
            dd_check_rows = DBCon.Ora_Execute_table("select *,(FORMAT(ISNULL(tarikh_mula,''),'dd/MM/yyyy', 'en-us')) as sdt,(FORMAT(ISNULL(tarikh_akhir,''),'dd/MM/yyyy', 'en-us')) as tdt from KW_Profile_syarikat where kod_syarikat='" + Tahun_kew.SelectedValue + "' and cur_sts='1'");

            string fdate = dd_check_rows.Rows[0]["sdt"].ToString();
            DateTime sdate = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime sdate1 = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            int month;
            int i = 0;
            string tdate = dd_check_rows.Rows[0]["tdt"].ToString();
            DateTime edate = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime edate1 = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string get_dt = string.Empty, fmt1 = string.Empty, fmt2 = string.Empty, fmt3 = string.Empty, fmt4 = string.Empty, fmt5 = string.Empty;
            string ss1 = string.Empty, ss12 = string.Empty;
            while (sdate <= edate)
            {
                string gtmnth = sdate.Month.ToString().PadLeft(2, '0');
                string nemnth = edate.Month.ToString().PadLeft(2, '0');
                DateTimeFormatInfo d = new DateTimeFormatInfo();
                month = Int32.Parse(gtmnth) - 1;
                if (Int32.Parse(gtmnth) != Int32.Parse(nemnth))
                {
                    ss1 = ",";
                    ss12 = "+";
                }
                else
                {
                    ss1 = "";
                    ss12 = "";
                }
                string ss2 = sdate.Month.ToString().PadLeft(2, '0');
                string ss3 = sdate.ToString("MMM");
                get_dt += "[" + sdate.Year.ToString() + "/" + ss2 + "/01]" + ss1;

                if (i == 0)
                {
                    fmt1 += "ISNULL(a.[" + ss2 + "],'0.00') as " + ss3 + "" + ss1;
                    fmt2 += "(select year as val1,month as val2,kategory,kod_akaun_debit,kod_akaun,po_no,penambatan_amt,pelubusan_amt,susut_nilai,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty,baki_bersih from KW_Aset_jurnal where profil='" + Tahun_kew.SelectedValue + "' and year = '" + sdate.Year.ToString() + "' and month = '" + ss2 + "') as SourceTable PIVOT(AVG([val3])  FOR[val2] IN([" + ss2 + "])) AS a ";
                    fmt3 += "ISNULL(" + ss3 + ",'0.00')" + ss12;

                }
                else
                {
                    if (Int32.Parse(gtmnth) != Int32.Parse(nemnth))
                    {
                        fmt1 += "ISNULL(b" + i + ".[" + ss2 + "],'0.00') as " + ss3 + "" + ss1;
                        fmt2 += "left join (select year as val1,month as val2,kategory,kod_akaun_debit,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where profil='" + Tahun_kew.SelectedValue + "' and year = '" + sdate.Year.ToString() + "' and month = '" + ss2 + "') as SourceTable PIVOT(AVG([val3])  FOR[val2] IN([" + ss2 + "])) AS b" + i + " on b" + i + ".po_no = a.po_no and b" + i + ".pv_no = a.pv_no ";
                        fmt3 += "ISNULL(" + ss3 + ",'0.00')" + ss12;
                    }
                    else
                    {
                        fmt1 += "ISNULL(b" + i + ".[" + ss2 + "],'0.00') as " + ss3 + "" + ss1;
                        fmt2 += "left join (select year as val1,month as val2,kategory,kod_akaun_debit,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where profil='" + Tahun_kew.SelectedValue + "' and year = '" + sdate.Year.ToString() + "' and month = '" + ss2 + "') as SourceTable PIVOT(AVG([val3])  FOR[val2] IN([" + ss2 + "])) AS b" + i + " on b" + i + ".po_no = a.po_no and b" + i + ".pv_no = a.pv_no left join Ref_ast_kategori ss1 on ss1.ast_kategori_code=a.kategory";
                        fmt3 += "ISNULL(" + ss3 + ",'0.00')" + ss12;
                    }

                }
                //drpdate is dropdownlist
                sdate = sdate.AddMonths(1);
                i++;
            }

            if (Tahun_kew.SelectedValue != "")
            {
                //val6 = "select distinct a.val1,a.kategory,ss1.ast_kategori_desc,a.sub_kategory,a.pv_no,a.qty,a.kod_akaun,a.po_no,a.penambatan_amt,a.pelubusan_amt,a.susut_nilai,a.sust_perkumpul_amt,a.bersih_samasa_amt,a.tarkih_pv,a.[01] as JAN,b.[02] as FEB,c.[03] as MAR,d.[04] as APR,e.[05] as MAY,f.[06] as JUN,g.[07] as JUL,h.[08] as AUG,i.[09] as SEP,j.[10] as OCT,k.[11] as NOV,l.[12] as DEC from (select year as val1,month as val2,kategory,kod_akaun,po_no,penambatan_amt,pelubusan_amt,susut_nilai,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='01') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name1 + ")) AS a left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='02') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name2 + ")) AS b on b.po_no=a.po_no and b.pv_no=a.pv_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='03') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name3 + ")) AS c on c.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='04') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name4 + ")) AS d on d.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='05') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name5 + ")) AS e on e.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='06') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name6 + ")) AS f on f.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='07') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name7 + ")) AS g on g.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='08') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name8 + ")) AS h on h.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='09') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name9 + ")) AS i on i.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='10') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name10 + ")) AS j on j.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='11') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name11 + ")) AS k on k.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='12') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name12 + ")) AS l on l.po_no=a.po_no left join Ref_ast_kategori ss1 on ss1.ast_kategori_code=a.kategory";
                val6 = "select *,ISNULL(baki_bersih,'0.00') as baki_bersih_amt,cast(case when po_no='baki' then (case when ISNULL(a1.sus_open_amt,'0.00') = '0.00' then (ISNULL(gl.deb_amt,'0.00') - ISNULL(gl.kre_amt,'0.00')) else ISNULL(a1.sus_open_amt,'0.00') end) else '0.00' end as money) as sus_open_amt1,((ISNULL(baki_bersih,'0.00') + ISNULL(penambatan_amt,'0.00')) - (ISNULL(pelubusan_amt,'0.00') + (" + fmt3 + ") ) + (case when po_no='baki' then (case when ISNULL(a1.sus_open_amt,'0.00') = '0.00' then (ISNULL(gl.deb_amt,'0.00') - ISNULL(gl.kre_amt,'0.00')) else ISNULL(a1.sus_open_amt,'0.00') end) else '0.00' end)) as nbbs from (select distinct m1.kod_akaun,nama_akaun,('susut nilai terkumpul - '+ nama_akaun) as nama_akaun1,ISNULL(op.opening_amt,'0.00') as open_amt from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance op on op.kod_akaun=m1.kod_akaun and start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate1.ToString("yyyy-MM-dd") + "'), 0) and end_dt<=DATEADD(day, DATEDIFF(day, 0, '" + edate1.ToString("yyyy-MM-dd") + "'), +1) and set_sts='1'  where  jenis_akaun='01' ) as a left join (select distinct nama_akaun,ISNULL(op.opening_amt,'0.00') as sus_open_amt from KW_Ref_Carta_Akaun M2 left join KW_Opening_Balance op on op.kod_akaun=m2.kod_akaun and start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate1.ToString("yyyy-MM-dd") + "'), 0) and end_dt<=DATEADD(day, DATEDIFF(day, 0, '" + edate1.ToString("yyyy-MM-dd") + "'), +1) and set_sts='1'  where jenis_akaun='09') as a1 on a1.nama_akaun=a.nama_akaun1 left join(select kod_akaun,sum(KW_Debit_amt) deb_amt,sum(KW_kredit_amt) as kre_amt from KW_General_Ledger where GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate1.AddYears(-1).ToString("yyyy-MM-dd") + "'), 0) and GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + edate.AddYears(-1).ToString("yyyy-MM-dd") + "'), +1) group by kod_akaun) gl on gl.kod_akaun=a.kod_akaun full outer join (select distinct a.val1,a.kategory,ss1.ast_kategori_desc,a.sub_kategory,a.pv_no,a.qty,a.kod_akaun,a.kod_akaun_debit,a.po_no,a.penambatan_amt,a.pelubusan_amt,a.susut_nilai,a.sust_perkumpul_amt,a.bersih_samasa_amt,a.tarkih_pv,a.baki_bersih," + fmt1 + "  from " + fmt2 + ") as b on b.kod_akaun=a.kod_akaun order by a.kod_akaun,ast_kategori_desc";
            }
            else
            {
                val6 = "select *,ISNULL(baki_bersih,'0.00') as baki_bersih_amt,case when po_no='baki' then (case when ISNULL(a1.sus_open_amt,'0.00') = '0.00' then (ISNULL(gl.deb_amt,'0.00') - ISNULL(gl.kre_amt,'0.00')) else ISNULL(a1.sus_open_amt,'0.00') end) else '0.00' end as sus_open_amt1,((ISNULL(baki_bersih,'0.00') + ISNULL(penambatan_amt,'0.00')) - (ISNULL(pelubusan_amt,'0.00') + (" + fmt3 + ") ) + (case when po_no='baki' then (case when ISNULL(a1.sus_open_amt,'0.00') = '0.00' then (ISNULL(gl.deb_amt,'0.00') - ISNULL(gl.kre_amt,'0.00')) else ISNULL(a1.sus_open_amt,'0.00') end) else '0.00' end)) as nbbs from (select distinct m1.kod_akaun,nama_akaun,('susut nilai terkumpul - '+ nama_akaun) as nama_akaun1,ISNULL(op.opening_amt,'0.00') as open_amt from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance op on op.kod_akaun=m1.kod_akaun and start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate1.ToString("yyyy-MM-dd") + "'), 0) and end_dt<=DATEADD(day, DATEDIFF(day, 0, '" + edate1.ToString("yyyy-MM-dd") + "'), +1) and set_sts='1'  where  jenis_akaun='01' ) as a left join (select distinct nama_akaun,ISNULL(op.opening_amt,'0.00') as sus_open_amt from KW_Ref_Carta_Akaun M2 left join KW_Opening_Balance op on op.kod_akaun=m2.kod_akaun and start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate1.ToString("yyyy-MM-dd") + "'), 0) and end_dt<=DATEADD(day, DATEDIFF(day, 0, '" + edate1.ToString("yyyy-MM-dd") + "'), +1) and set_sts='1'  where jenis_akaun='09') as a1 on a1.nama_akaun=a.nama_akaun1 left join(select kod_akaun,sum(KW_Debit_amt) deb_amt,sum(KW_kredit_amt) as kre_amt from KW_General_Ledger where GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate1.AddYears(-1).ToString("yyyy-MM-dd") + "'), 0) and GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + edate.AddYears(-1).ToString("yyyy-MM-dd") + "'), +1) group by kod_akaun) gl on gl.kod_akaun=a.kod_akaun full outer join (select distinct a.val1,a.kategory,ss1.ast_kategori_desc,a.sub_kategory,a.pv_no,a.qty,a.kod_akaun,a.kod_akaun_debit,a.po_no,a.penambatan_amt,a.pelubusan_amt,a.susut_nilai,a.sust_perkumpul_amt,a.bersih_samasa_amt,a.tarkih_pv,a.baki_bersih," + fmt1 + "  from " + fmt2 + ") as b on b.kod_akaun=a.kod_akaun order by a.kod_akaun,ast_kategori_desc";
            }
            dt = DBCon.Ora_Execute_table(val6);
            //dt1 = DBCon.Ora_Execute_table(val5);
            Rptviwerlejar.Reset();
            ds.Tables.Add(dt);
            //ds1.Tables.Add(dt1);

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();



            Rptviwerlejar.LocalReport.DataSources.Clear();
            if (countRow != 0)
            {
                string rdlc_name = string.Empty;
                if (sdate.Month.ToString().PadLeft(2, '0') == "01")
                {
                    rdlc_name = "KW_aset_jurnal1.rdlc";
                }
                else
                {
                    rdlc_name = "KW_aset_jurnal.rdlc";
                }

                Rptviwerlejar.LocalReport.ReportPath = "kewengan/" + rdlc_name;
                ReportDataSource rds = new ReportDataSource("kw_aset_jurn", dt);
                //ReportDataSource rds1 = new ReportDataSource("kwpl1", dt1);
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", ""),
                     new ReportParameter("s2", dd_check_rows.Rows[0]["nama_syarikat"].ToString()),
                     new ReportParameter("s3", dd_check_rows.Rows[0]["sdt"].ToString()),
                     new ReportParameter("s4", dd_check_rows.Rows[0]["tdt"].ToString()),
                     new ReportParameter("S5", "")
                          };

                Rptviwerlejar.LocalReport.SetParameters(rptParams);
                Rptviwerlejar.LocalReport.DataSources.Add(rds);
                //Rptviwerlejar.LocalReport.DataSources.Add(rds1);
                Rptviwerlejar.LocalReport.Refresh();

            }
        }

    }

    void load_cetak1()
    {
        if (Tahun_kew.SelectedValue != "")
        {
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string val1 = string.Empty, val2 = string.Empty, val3 = string.Empty, val4 = string.Empty, val5 = string.Empty, val6 = string.Empty, pre_day = string.Empty, pre_year = string.Empty;

            string fmdate = string.Empty, tmdate = string.Empty;
            //string mnth_name1 = "[01]", mnth_name2 = "[02]", mnth_name3 = "[03]", mnth_name4 = "[04]", mnth_name5 = "[05]", mnth_name6 = "[06]", mnth_name7 = "[07]", mnth_name8 = "[08]", mnth_name9 = "[09]", mnth_name10 = "[10]", mnth_name11 = "[11]", mnth_name12 = "[12]";
            //string get_dt = "[" + sdate.Year.ToString() + "/" + ss2 + "]" + ss1;

            DataTable dd_check_rows = new DataTable();
            dd_check_rows = DBCon.Ora_Execute_table("select *,(FORMAT(ISNULL(tarikh_mula,''),'dd/MM/yyyy', 'en-us')) as sdt,(FORMAT(ISNULL(tarikh_akhir,''),'dd/MM/yyyy', 'en-us')) as tdt from KW_Profile_syarikat where kod_syarikat='" + Tahun_kew.SelectedValue + "' and cur_sts='1'");

            string fdate = dd_check_rows.Rows[0]["sdt"].ToString();
            DateTime sdate = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime sdate1 = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            int month;
            int i = 0;
            string tdate = dd_check_rows.Rows[0]["tdt"].ToString();
            DateTime edate = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime edate1 = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string get_dt = string.Empty, fmt1 = string.Empty, fmt2 = string.Empty, fmt3 = string.Empty, fmt4 = string.Empty, fmt5 = string.Empty;
            string ss1 = string.Empty, ss12 = string.Empty;
            while (sdate <= edate)
            {
                string gtmnth = sdate.Month.ToString().PadLeft(2, '0');
                string nemnth = edate.Month.ToString().PadLeft(2, '0');
                DateTimeFormatInfo d = new DateTimeFormatInfo();
                month = Int32.Parse(gtmnth) - 1;
                if (Int32.Parse(gtmnth) != Int32.Parse(nemnth))
                {
                    ss1 = ",";
                    ss12 = "+";
                }
                else
                {
                    ss1 = "";
                    ss12 = "";
                }
                string ss2 = sdate.Month.ToString().PadLeft(2, '0');
                string ss3 = sdate.ToString("MMM");
                get_dt += "[" + sdate.Year.ToString() + "/" + ss2 + "/01]" + ss1;

                if (i == 0)
                {
                    fmt1 += "ISNULL(a.[" + ss2 + "],'0.00') as " + ss3 + "" + ss1;
                    fmt2 += "(select year as val1,month as val2,kategory,kod_akaun_debit,kod_akaun,po_no,penambatan_amt,pelubusan_amt,susut_nilai,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty,baki_bersih from KW_Aset_jurnal_temp where profil='" + Tahun_kew.SelectedValue + "' and year = '" + sdate.Year.ToString() + "' and month = '" + ss2 + "') as SourceTable PIVOT(AVG([val3])  FOR[val2] IN([" + ss2 + "])) AS a ";
                    fmt3 += "ISNULL(" + ss3 + ",'0.00')" + ss12;
                }
                else
                {
                    if (Int32.Parse(gtmnth) != Int32.Parse(nemnth))
                    {
                        fmt1 += "ISNULL(b" + i + ".[" + ss2 + "],'0.00') as " + ss3 + "" + ss1;
                        fmt2 += "left join (select year as val1,month as val2,kategory,kod_akaun_debit,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal_temp where profil='" + Tahun_kew.SelectedValue + "' and year = '" + sdate.Year.ToString() + "' and month = '" + ss2 + "') as SourceTable PIVOT(AVG([val3])  FOR[val2] IN([" + ss2 + "])) AS b" + i + " on b" + i + ".po_no = a.po_no and b" + i + ".pv_no = a.pv_no ";
                        fmt3 += "ISNULL(" + ss3 + ",'0.00')" + ss12;
                    }
                    else
                    {
                        fmt1 += "ISNULL(b" + i + ".[" + ss2 + "],'0.00') as " + ss3 + "" + ss1;
                        fmt2 += "left join (select year as val1,month as val2,kategory,kod_akaun_debit,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal_temp where profil='" + Tahun_kew.SelectedValue + "' and year = '" + sdate.Year.ToString() + "' and month = '" + ss2 + "') as SourceTable PIVOT(AVG([val3])  FOR[val2] IN([" + ss2 + "])) AS b" + i + " on b" + i + ".po_no = a.po_no and b" + i + ".pv_no = a.pv_no left join Ref_ast_kategori ss1 on ss1.ast_kategori_code=a.kategory";
                        fmt3 += "ISNULL(" + ss3 + ",'0.00')" + ss12;
                    }

                }
                //drpdate is dropdownlist
                sdate = sdate.AddMonths(1);
                i++;
            }

            if (Tahun_kew.SelectedValue != "")
            {
                //val6 = "select distinct a.val1,a.kategory,ss1.ast_kategori_desc,a.sub_kategory,a.pv_no,a.qty,a.kod_akaun,a.po_no,a.penambatan_amt,a.pelubusan_amt,a.susut_nilai,a.sust_perkumpul_amt,a.bersih_samasa_amt,a.tarkih_pv,a.[01] as JAN,b.[02] as FEB,c.[03] as MAR,d.[04] as APR,e.[05] as MAY,f.[06] as JUN,g.[07] as JUL,h.[08] as AUG,i.[09] as SEP,j.[10] as OCT,k.[11] as NOV,l.[12] as DEC from (select year as val1,month as val2,kategory,kod_akaun,po_no,penambatan_amt,pelubusan_amt,susut_nilai,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='01') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name1 + ")) AS a left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='02') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name2 + ")) AS b on b.po_no=a.po_no and b.pv_no=a.pv_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='03') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name3 + ")) AS c on c.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='04') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name4 + ")) AS d on d.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='05') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name5 + ")) AS e on e.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='06') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name6 + ")) AS f on f.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='07') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name7 + ")) AS g on g.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='08') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name8 + ")) AS h on h.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='09') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name9 + ")) AS i on i.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='10') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name10 + ")) AS j on j.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='11') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name11 + ")) AS k on k.po_no=a.po_no left join (select year as val1,month as val2,kategory,kod_akaun,po_no,cast(mon_jumlah_amt as money) as val3,sust_perkumpul_amt,bersih_samasa_amt,format(tarikh_pv,'dd/MM/yyyy') as tarkih_pv,sub_kategory,pv_no,qty from KW_Aset_jurnal where year='" + Tahun_kew.SelectedValue + "' and month='12') as SourceTable PIVOT(AVG([val3])  FOR [val2] IN(" + mnth_name12 + ")) AS l on l.po_no=a.po_no left join Ref_ast_kategori ss1 on ss1.ast_kategori_code=a.kategory";
                val6 = "select *,ISNULL(baki_bersih,'0.00') as baki_bersih_amt,case when po_no='baki' then (case when ISNULL(a1.sus_open_amt,'0.00') = '0.00' then (ISNULL(gl.deb_amt,'0.00') - ISNULL(gl.kre_amt,'0.00')) else ISNULL(a1.sus_open_amt,'0.00') end) else '0.00' end as sus_open_amt1,((ISNULL(baki_bersih,'0.00') + ISNULL(penambatan_amt,'0.00')) - (ISNULL(pelubusan_amt,'0.00') + (" + fmt3 + ") ) + (case when po_no='baki' then (case when ISNULL(a1.sus_open_amt,'0.00') = '0.00' then (ISNULL(gl.deb_amt,'0.00') - ISNULL(gl.kre_amt,'0.00')) else ISNULL(a1.sus_open_amt,'0.00') end) else '0.00' end)) as nbbs from (select distinct m1.kod_akaun,nama_akaun,ISNULL(op.opening_amt,'0.00') as open_amt from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance op on op.kod_akaun=m1.kod_akaun and start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate1.ToString("yyyy-MM-dd") + "'), 0) and end_dt<=DATEADD(day, DATEDIFF(day, 0, '" + edate1.ToString("yyyy-MM-dd") + "'), +1) and set_sts='1'  where  jenis_akaun='01' ) as a left join (select distinct nama_akaun,ISNULL(op.opening_amt,'0.00') as sus_open_amt from KW_Ref_Carta_Akaun M2 left join KW_Opening_Balance op on op.kod_akaun=m2.kod_akaun and start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate1.ToString("yyyy-MM-dd") + "'), 0) and end_dt<=DATEADD(day, DATEDIFF(day, 0, '" + edate1.ToString("yyyy-MM-dd") + "'), +1) and set_sts='1'  where jenis_akaun='09') as a1 on a1.nama_akaun=a.nama_akaun left join(select kod_akaun,sum(KW_Debit_amt) deb_amt,sum(KW_kredit_amt) as kre_amt from KW_General_Ledger where GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate1.AddYears(-1).ToString("yyyy-MM-dd") + "'), 0) and GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + edate.AddYears(-1).ToString("yyyy-MM-dd") + "'), +1) group by kod_akaun) gl on gl.kod_akaun=a.kod_akaun full outer join (select distinct a.val1,a.kategory,ss1.ast_kategori_desc,a.sub_kategory,a.pv_no,a.qty,a.kod_akaun,a.kod_akaun_debit,a.po_no,a.penambatan_amt,a.pelubusan_amt,a.susut_nilai,a.sust_perkumpul_amt,a.bersih_samasa_amt,a.tarkih_pv,a.baki_bersih," + fmt1 + "  from " + fmt2 + ") as b on b.kod_akaun=a.kod_akaun order by a.kod_akaun,ast_kategori_desc";
            }
            else
            {
                val6 = "select *,ISNULL(baki_bersih,'0.00') as baki_bersih_amt,case when po_no='baki' then (case when ISNULL(a1.sus_open_amt,'0.00') = '0.00' then (ISNULL(gl.deb_amt,'0.00') - ISNULL(gl.kre_amt,'0.00')) else ISNULL(a1.sus_open_amt,'0.00') end) else '0.00' end as sus_open_amt1,((ISNULL(baki_bersih,'0.00') + ISNULL(penambatan_amt,'0.00')) - (ISNULL(pelubusan_amt,'0.00') + (" + fmt3 + ") ) + (case when po_no='baki' then (case when ISNULL(a1.sus_open_amt,'0.00') = '0.00' then (ISNULL(gl.deb_amt,'0.00') - ISNULL(gl.kre_amt,'0.00')) else ISNULL(a1.sus_open_amt,'0.00') end) else '0.00' end)) as nbbs from (select distinct m1.kod_akaun,nama_akaun,ISNULL(op.opening_amt,'0.00') as open_amt from KW_Ref_Carta_Akaun m1 left join KW_Opening_Balance op on op.kod_akaun=m1.kod_akaun and start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate1.ToString("yyyy-MM-dd") + "'), 0) and end_dt<=DATEADD(day, DATEDIFF(day, 0, '" + edate1.ToString("yyyy-MM-dd") + "'), +1) and set_sts='1'  where  jenis_akaun='01' ) as a left join (select distinct nama_akaun,ISNULL(op.opening_amt,'0.00') as sus_open_amt from KW_Ref_Carta_Akaun M2 left join KW_Opening_Balance op on op.kod_akaun=m2.kod_akaun and start_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate1.ToString("yyyy-MM-dd") + "'), 0) and end_dt<=DATEADD(day, DATEDIFF(day, 0, '" + edate1.ToString("yyyy-MM-dd") + "'), +1) and set_sts='1'  where jenis_akaun='09') as a1 on a1.nama_akaun=a.nama_akaun left join(select kod_akaun,sum(KW_Debit_amt) deb_amt,sum(KW_kredit_amt) as kre_amt from KW_General_Ledger where GL_process_dt>=DATEADD(day, DATEDIFF(day, 0, '" + sdate1.AddYears(-1).ToString("yyyy-MM-dd") + "'), 0) and GL_process_dt<=DATEADD(day, DATEDIFF(day, 0, '" + edate.AddYears(-1).ToString("yyyy-MM-dd") + "'), +1) group by kod_akaun) gl on gl.kod_akaun=a.kod_akaun full outer join (select distinct a.val1,a.kategory,ss1.ast_kategori_desc,a.sub_kategory,a.pv_no,a.qty,a.kod_akaun,a.kod_akaun_debit,a.po_no,a.penambatan_amt,a.pelubusan_amt,a.susut_nilai,a.sust_perkumpul_amt,a.bersih_samasa_amt,a.tarkih_pv,a.baki_bersih," + fmt1 + "  from " + fmt2 + ") as b on b.kod_akaun=a.kod_akaun order by a.kod_akaun,ast_kategori_desc";
            }
            dt = DBCon.Ora_Execute_table(val6);
            //dt1 = DBCon.Ora_Execute_table(val5);
            Rptviwerlejar.Reset();
            ds.Tables.Add(dt);
            //ds1.Tables.Add(dt1);

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();



            Rptviwerlejar.LocalReport.DataSources.Clear();
            if (countRow != 0)
            {


                string rdlc_name = string.Empty;
                if (sdate.Month.ToString().PadLeft(2, '0') == "01")
                {
                    rdlc_name = "KW_aset_jurnal1.rdlc";
                }
                else
                {
                    rdlc_name = "KW_aset_jurnal.rdlc";
                }

                Rptviwerlejar.LocalReport.ReportPath = "kewengan/" + rdlc_name;
                ReportDataSource rds = new ReportDataSource("kw_aset_jurn", dt);
                //ReportDataSource rds1 = new ReportDataSource("kwpl1", dt1);
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", ""),
                       new ReportParameter("s2", dd_check_rows.Rows[0]["nama_syarikat"].ToString()),
                   new ReportParameter("s3", dd_check_rows.Rows[0]["sdt"].ToString()),
                     new ReportParameter("s4", dd_check_rows.Rows[0]["tdt"].ToString()),
                     new ReportParameter("S5", "")
                          };

                Rptviwerlejar.LocalReport.SetParameters(rptParams);
                Rptviwerlejar.LocalReport.DataSources.Add(rds);
                //Rptviwerlejar.LocalReport.DataSources.Add(rds1);
                Rptviwerlejar.LocalReport.Refresh();

            }
        }

    }




    protected void clk_submit(object sender, EventArgs e)
    {

    }

    void empty_fld()
    {

    }




    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btn_reset1(object sender, EventArgs e)
    {
        Tk_akhir.Text = "";
    }

    protected void btn_reset(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/KW_jadual_aset.aspx");
    }

}