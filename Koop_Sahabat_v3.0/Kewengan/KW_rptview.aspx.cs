using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Net;
using System.Collections;
using System.Web.SessionState;
using System.Threading;
using System.Reflection;

public partial class KW_rptview : System.Web.UI.Page
{
    DBConnection Dbcon = new DBConnection();
    string query1 = string.Empty, query2 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                string nameshbt = Session["dbtno"].ToString();
                if (nameshbt != "")
                {
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();
                    if (Session["rpt_type"].ToString() == "KREDIT")
                    {
                        query1 = "select tb1.no_notakredit vv1,tb3.syar_logo v1, tb3.nama_syarikat v2,tb3.kod_syarikat v3, tb3.alamat_syarikat v4, tb3.syar_nombo_teleph v5,tb3.syar_nombo_fax v6, "
                              + " (tb4.Ref_nama_syarikat +' & '+ tb4.Ref_no_syarikat) v7,Ref_alamat v8, Ref_no_telefone v9, Ref_no_fax v10, Ref_kawalan v11,tb1.no_notakredit v12,  "
                              + " Format(tb6.tarikh_invois,'dd/MM/yyyy') v13, tb1.no_invois v14,Format(tb1.tarikh_kredit,'dd/MM/yyyy') v15,tb1.Terma v16,tb1.perkara v17,tb5.Bank_Name v18,tb1.inv_noacc_bank v19,tb1.Overall v20, "
                              + " tb2.keterangan v21,tb2.Unit v22,tb2.quantiti v23,tb2.jumlah v24,tb2.gstjumlah v25,tb2.overall v26,tb2.discount v27 "
                              + "from KW_Penerimaan_Credit tb1 left join KW_Penerimaan_Credit_item tb2 on tb2.no_notakredit=tb1.no_notakredit "
                              + "left join KW_Penerimaan_invois tb6 on tb6.no_invois=tb1.no_invois and tb6.Status ='A' "
                              + "  left join KW_Profile_syarikat tb3 on tb3.Status='A' and cur_sts='1' "
                              + " left join KW_Ref_Pelanggan tb4 on tb4.Ref_no_syarikat=tb1.nama_pelanggan_code "
                              + " left join Ref_Nama_Bank tb5 on tb5.Bank_Code=tb1.inv_bank  where tb1.Status = 'A' and tb1.no_notakredit ='" + nameshbt + "'";
                    }
                    else if (Session["rpt_type"].ToString() == "DEBIT")
                    {
                        query1 = "select tb1.no_notadebit vv1,tb3.syar_logo v1, tb3.nama_syarikat v2,tb3.kod_syarikat v3, tb3.alamat_syarikat v4, tb3.syar_nombo_teleph v5,tb3.syar_nombo_fax v6, "
                              + " (tb4.Ref_nama_syarikat +' & '+ tb4.Ref_no_syarikat) v7,Ref_alamat v8, Ref_no_telefone v9, Ref_no_fax v10, Ref_kawalan v11,tb1.no_notadebit v12,  "
                              + " Format(tb6.tarikh_invois,'dd/MM/yyyy') v13, tb1.no_invois v14,Format(tb1.tarikh_debit,'dd/MM/yyyy') v15,tb1.Terma v16,tb1.perkara v17,tb5.Bank_Name v18,tb1.inv_noacc_bank v19,tb1.Overall v20, "
                              + " tb2.keterangan v21,tb2.Unit v22,tb2.quantiti v23,tb2.jumlah v24,tb2.gstjumlah v25,tb2.overall v26,tb2.discount v27 "
                              + "from KW_Penerimaan_Debit tb1 left join KW_Penerimaan_Debit_item tb2 on tb2.no_notadebit=tb1.no_notadebit "
                              + "left join KW_Penerimaan_invois tb6 on tb6.no_invois=tb1.no_invois and tb6.Status ='A' "
                              + "  left join KW_Profile_syarikat tb3 on tb3.Status='A' and cur_sts='1' "
                              + " left join KW_Ref_Pelanggan tb4 on tb4.Ref_no_syarikat=tb1.nama_pelanggan_code "
                              + " left join Ref_Nama_Bank tb5 on tb5.Bank_Code=tb1.inv_bank  where tb1.Status = 'A' and tb1.no_notadebit ='" + nameshbt + "'";
                    }
                    else if (Session["rpt_type"].ToString() == "INVOIS")
                    {
                        query1 = "select tb1.no_invois vv1,tb3.syar_logo v1, tb3.nama_syarikat v2,tb3.kod_syarikat v3, tb3.alamat_syarikat v4, tb3.syar_nombo_teleph v5,tb3.syar_nombo_fax v6, "
                              + " (tb4.Ref_nama_syarikat +' & '+ tb4.Ref_no_syarikat) v7,Ref_alamat v8, Ref_no_telefone v9, Ref_no_fax v10, Ref_kawalan v11,tb1.no_invois v12,  "
                              + " tb1.DO_no v13, tb1.PO_no v14,Format(tb1.tarikh_invois,'dd/MM/yyyy') v15,tb1.Terma v16,tb1.perkera v17,tb5.Bank_Name v18,tb1.inv_noacc_bank v19,tb1.Overall v20, "
                              + " tb2.keterangan v21,tb2.Unit v22,tb2.quantiti v23,tb2.jumlah v24,tb2.gstjumlah v25,tb2.overall v26,tb2.discount v27 "
                              + "from KW_Penerimaan_invois tb1 left join KW_Penerimaan_invois_item tb2 on tb2.no_invois=tb1.no_invois "
                              + "  left join KW_Profile_syarikat tb3 on tb3.Status='A' and cur_sts='1' "
                              + " left join KW_Ref_Pelanggan tb4 on tb4.Ref_no_syarikat=tb1.nama_pelanggan_code "
                              + " left join Ref_Nama_Bank tb5 on tb5.Bank_Code=tb1.inv_bank  where tb1.Status = 'A' and tb1.no_invois ='" + nameshbt + "'";
                    }
                    else if (Session["rpt_type"].ToString() == "payment_voucher")
                    {
                        query1 = "select kwp.Bayar_kepada,kwp.Pv_no,kwp.Bayar_kepada as kbk,kwp.no_invois,kwp.perkara,ISNULL(ab.Bank_Name,'')Bank_Name,pem_bank_accno, CONVERT(date, GETDATE()) as date, krp.Ref_nama_syarikat,krp.Ref_alamat,krp.Ref_no_telefone,krp.Ref_no_fax, kgl.kod_bajet,kgl.KW_Debit_amt,KW_kredit_amt,kps.syar_logo as v1,kps.kod_syarikat,kps.nama_syarikat,kps.nama_syarikat as kp,kps.alamat_syarikat,kps.syar_nombo_teleph,kps.syar_nombo_fax ,sum(kpi.discount) as dicount,sum(kpi.overall) as overall,sum(kpi.gstjumlah) as gst,sum(kpi.jumlah) as juml from KW_Pembayaran_Pay_voucer as kwp  left join KW_Ref_Pembekal as krp on kwp.Bayar_kepada = krp.Ref_nama_syarikat left join  KW_General_Ledger as kgl on kwp.Pv_no = kgl.GL_invois_no left join KW_Pembayaran_Credit as kpc on kwp.no_invois = kpc.no_invois left join  KW_Pembayaran_Credit_item as kpi on kpc.no_Rujukan = kpi.no_Rujukan  left join   KW_Profile_syarikat as kps on kps.cur_sts = '1' and kps.Status='A' left join Ref_Nama_Bank ab on ab.Bank_Code=pem_bank_cd where pv_no='" + nameshbt + "' group by kwp.Bayar_kepada,kwp.Pv_no,kwp.no_invois,kwp.perkara,  krp.Ref_nama_syarikat,krp.Ref_alamat,krp.Ref_no_telefone,krp.Ref_no_fax, kgl.kod_bajet,kgl.KW_Debit_amt,KW_kredit_amt,kps.syar_logo,kps.kod_syarikat,kps.nama_syarikat,kps.alamat_syarikat,kps.syar_nombo_teleph,kps.syar_nombo_fax,ab.Bank_Name,pem_bank_accno";

                        if (Session["cmd_arg1"].ToString() == "01")

                        {

                            query2 = "select * from (select s1.kod_bajet,s1.Id,m1.Bayar_kepada as kepada,UPPER(s1.keterangan) as Keteragan,s2.Ref_Projek_name as projek,s3.Bank_Name as bank,s1.nv_noacc_bank as acc_no, s1.Overall as jumlah, discount as Disk  , "
                                       + " UPPER(ISNULL(s4.Ref_nama_cukai,'')) as jen_cukai, s1.gstjumlah as cukai_amt,jumlah as amount,jumlah as unit,m1.kat_penerima as kat,'1' as qty   from KW_Pembayaran_invoisBil_item s1  "
                                       + " left join KW_Pembayaran_invois m1 on m1.no_invois=s1.no_invois left join KW_Ref_Projek s2 on s2.Ref_Projek_code=m1.project_kod  "
                                       + " left join Ref_Nama_Bank s3 on s3.Bank_Code=s1.inv_bank  left join KW_Ref_Tetapan_cukai s4 on s4.Ref_kod_cukai=s1.tax where s1.no_invois='" + Session["cmd_arg3"].ToString() + "' and s1.Status='A') as a outer apply( select Ref_nama_syarikat s1,Ref_alamat s2, ref_no_telefone s3,Ref_no_fax s4 from KW_Ref_Pembekal s4 where s4.Ref_no_syarikat=a.kepada  union all  select Ref_nama_syarikat s1,Ref_alamat s2, ref_no_telefone s3,Ref_no_fax s4 from  KW_Ref_Pelanggan s4_1 where s4_1.Ref_no_syarikat=a.kepada  union all  select stf_name s1,stf_permanent_address s2, stf_phone_h s3, stf_phone_m s4 from  hr_staff_profile s4_2 where s4_2.stf_staff_no = a.kepada) as b"
                                       + " outer apply(select Format(GL_post_dt,'dd/MM/yyyy') post_dt,kod_akaun,kod_bajet bajet,GL_desc1,KW_Debit_amt,KW_kredit_amt from KW_General_Ledger where gl_jenis_no='" + nameshbt + "') as c "
                                       + " outer apply(select No_cek from KW_Pembayaran_Pay_voucer where  no_invois='" + Session["cmd_arg3"].ToString() + "' and lulus_sts='L' and Pv_no='" + nameshbt + "') as d ";

                        }

                        else if (Session["cmd_arg1"].ToString() == "03")

                        {

                            query2 = "select * from (select s1.kod_bajet,s1.Id,mhn_byr_kepada as kepada,UPPER(mhn_keterangan) as Keteragan,s2.Ref_Projek_name as projek,s3.Bank_Name as bank,s1.mhn_noacc_bank as acc_no,mhn_amount_bsr as jumlah, mhn_disc as Disk "
                                      + " ,UPPER(ISNULL(s4.Ref_nama_cukai,'')) as jen_cukai, s1.mhn_tax as cukai_amt, (ISNULL(s1.mhn_amount,'0.00') - ISNULL(mhn_disc,'0.00')) as amount,mhn_amount as unit,m1.kat_penerima as kat,'1' as qty "
                                      + " from KW_Pembayaran_mohon_item s1 left join KW_Pembayaran_mohon m1 on m1.no_permohonan =s1.mhn_no_permohonan left join KW_Ref_Projek s2 on s2.Ref_Projek_code=s1.mhn_projek_cd "
                                      + " left join Ref_Nama_Bank s3 on s3.Bank_Code=s1.mhn_bank left join KW_Ref_Tetapan_cukai s4 on s4.Ref_kod_cukai=s1.mhn_jns_tax  where mhn_no_permohonan='" + Session["cmd_arg3"].ToString() + "' and s1.Status='A' ) as a outer apply( select Ref_nama_syarikat s1,Ref_alamat s2, ref_no_telefone s3,Ref_no_fax s4 from KW_Ref_Pembekal s4 where s4.Ref_no_syarikat=a.kepada  union all  select Ref_nama_syarikat s1,Ref_alamat s2, ref_no_telefone s3,Ref_no_fax s4 from  KW_Ref_Pelanggan s4_1 where s4_1.Ref_no_syarikat=a.kepada  union all  select stf_name s1,stf_permanent_address s2, stf_phone_h s3, stf_phone_m s4 from  hr_staff_profile s4_2 where s4_2.stf_staff_no = a.kepada) as b"
                                      + " outer apply(select Format(GL_post_dt,'dd/MM/yyyy') post_dt,kod_akaun,kod_bajet bajet,GL_desc1,KW_Debit_amt,KW_kredit_amt from KW_General_Ledger where gl_jenis_no='" + nameshbt + "') as c "
                                      + " outer apply(select No_cek from KW_Pembayaran_Pay_voucer where  no_invois='" + Session["cmd_arg3"].ToString() + "' and lulus_sts='L' and Pv_no='" + nameshbt + "') as d ";

                        }

                        else if (Session["cmd_arg1"].ToString() == "05")

                        {

                            query2 = "select * from (select s1.kod_bajet,s1.Id,m1.nama_pembekal_code as kepada,UPPER(perkera) as Keteragan,s2.Ref_Projek_name as projek,'' as bank,'' as acc_no,s1.overall as jumlah, discount as Disk "
                                      + " ,UPPER(ISNULL(s4.Ref_nama_cukai,'')) as jen_cukai, s1.gstjumlah cukai_amt, s1.jumlah as amount,s1.jumlah as unit,'02' as kat,'1' as qty "
                                      + " from KW_Pembayaran_Credit_item s1 left join KW_Pembayaran_Credit m1 on m1.no_Rujukan =s1.no_Rujukan left join KW_Ref_Projek s2 on s2.Ref_Projek_code=m1.project_kod "
                                      + " left join KW_Ref_Tetapan_cukai s4 on s4.Ref_kod_cukai=s1.tax  where m1.no_Rujukan='" + Session["cmd_arg3"].ToString() + "' and s1.Status='A') as a outer apply( select Ref_nama_syarikat s1,Ref_alamat s2, ref_no_telefone s3,Ref_no_fax s4 from KW_Ref_Pembekal s4 where s4.Ref_no_syarikat=a.kepada  union all  select Ref_nama_syarikat s1,Ref_alamat s2, ref_no_telefone s3,Ref_no_fax s4 from  KW_Ref_Pelanggan s4_1 where s4_1.Ref_no_syarikat=a.kepada  union all  select stf_name s1,stf_permanent_address s2, stf_phone_h s3, stf_phone_m s4 from  hr_staff_profile s4_2 where s4_2.stf_staff_no = a.kepada) as b"
                                      + " outer apply(select Format(GL_post_dt,'dd/MM/yyyy') post_dt,kod_akaun,kod_bajet bajet,GL_desc1,KW_Debit_amt,KW_kredit_amt from KW_General_Ledger where gl_jenis_no='" + nameshbt + "') as c "
                                      + " outer apply(select No_cek from KW_Pembayaran_Pay_voucer where  no_invois='" + Session["cmd_arg3"].ToString() + "' and lulus_sts='L' and Pv_no='" + nameshbt + "') as d ";

                        }

                        else if (Session["cmd_arg1"].ToString() == "06")

                        {

                            query2 = "select * from (select s1.kod_bajet,s1.Id,m1.nama_pembekal_code as kepada,UPPER(perkera) as Keteragan,s2.Ref_Projek_name as projek,'' as bank,'' as acc_no,s1.overall as jumlah, discount as Disk "
                                      + " ,UPPER(ISNULL(s4.Ref_nama_cukai,'')) as jen_cukai, s1.gstjumlah cukai_amt,s1.jumlah  as amount,s1.jumlah as unit,'02' as kat,'1' as qty "
                                      + " from KW_Pembayaran_Dedit_item s1 left join KW_Pembayaran_Dedit m1 on m1.no_Rujukan =s1.no_Rujukan left join KW_Ref_Projek s2 on s2.Ref_Projek_code=m1.project_kod "
                                      + " left join KW_Ref_Tetapan_cukai s4 on s4.Ref_kod_cukai=s1.tax  where m1.no_Rujukan='" + Session["cmd_arg3"].ToString() + "' and s1.Status='A') as a outer apply( select Ref_nama_syarikat s1,Ref_alamat s2, ref_no_telefone s3,Ref_no_fax s4 from KW_Ref_Pembekal s4 where s4.Ref_no_syarikat=a.kepada  union all  select Ref_nama_syarikat s1,Ref_alamat s2, ref_no_telefone s3,Ref_no_fax s4 from  KW_Ref_Pelanggan s4_1 where s4_1.Ref_no_syarikat=a.kepada  union all  select stf_name s1,stf_permanent_address s2, stf_phone_h s3, stf_phone_m s4 from  hr_staff_profile s4_2 where s4_2.stf_staff_no = a.kepada) as b"
                                      + " outer apply(select Format(GL_post_dt,'dd/MM/yyyy') post_dt,kod_akaun,kod_bajet bajet,GL_desc1,KW_Debit_amt,KW_kredit_amt from KW_General_Ledger where gl_jenis_no='" + nameshbt + "') as c "
                                      + " outer apply(select No_cek from KW_Pembayaran_Pay_voucer where  no_invois='" + Session["cmd_arg3"].ToString() + "' and lulus_sts='L' and Pv_no='" + nameshbt + "') as d ";

                        }
                        else
                        {

                            query2 = "select * from (select * from (select no_permohonan,'' kod_bajet,'' Id,'' kepada,(a.desc1 +' '+replace(b.gl_invois_no,'HR','')) as Keteragan,'' as projek,''bank,'' acc_no,a.Overall as jumlah,'0.00' Disk,'' jen_cukai,'0.00' cukai_amt,a.Overall amount,a.Overall unit,'' kat,'1' qty  from  "
                                      + " ( select pv_item,sb1.no_permohonan,sb1.desc1,sum(sb1.Overall) Overall,sb1.jp_desc,sb1.tarkih_permohonan,sb1.nama_pelanggan_code,sb1.status,sb1.jp_cd,sb1.no_inv,sb1.semak_sts,sb1.semakdt,sb1.tk_invois,sb1.tk_mohon,sb1.kat_penerima,sb1.Terma,sb1.pv_no,sb1.perkara,sb1.pcod,sb1.semak_id,sb1.lulus_sts from ( select pv_item,m1.no_permohonan as no_permohonan,case when pv_item = '1' then 'Gaji Kakitangan' when pv_item = '2' then 'STATUTORI MAJIKAN (KWSP)' when pv_item = '3' then 'STATUTORI MAJIKAN (PERKESO)' "
                                        + " when pv_item = '4' then 'STATUTORI MAJIKAN (LHDN)' when pv_item = '5' then 'STATUTORI MAJIKAN (SIP)' else m1.keterangan end as desc1, "
                                        + " sum(m1.Overall) as Overall,s2.kk_skrin_name as jp_desc,Format(tarikh_lulus, 'dd/MM/yyyy') as tarkih_permohonan ,nama_pelanggan_code,case when ISNULL(kew_appr_gl_cd,'') = 'L' then 'LULUS' "
                                        + " else 'WAITING' end as status,Jenis_permohonan as jp_cd,no_Rujukan no_inv, semak_sts, Format(semak_dt,'dd/MM/yyyy') semakdt  ,'' as tk_invois,Format(tarikh_lulus, 'dd/MM/yyyy') as tk_mohon, "
                                        + " '03' kat_penerima,m2.Terma,ISNULL(p1.Pv_no, '') pv_no,m2.perkara,'' pcod,semak_id,ISNULL(p1.lulus_sts, '') lulus_sts from KW_jurnal_inter_items m1   inner join KW_jurnal_inter m2 on "
                                        + " m2.no_permohonan = m1.no_permohonan left join KW_Pembayaran_Pay_voucer p1 on p1.no_invois = m1.no_permohonan "
                                        + " left join kk_pid_skrin s2 on s2.KK_skrin_id = nama_pelanggan_code and s2.status = 'A'  where m1.pv_item != '0' and m1.overall != '0.00' "
                                        + " and m2.kew_appr_gl_cd = 'L'  and ISNULL(m2.kew_appr_pv_cd, '') = ''  group by  m1.no_permohonan,pv_item,kk_skrin_name,tarikh_lulus,nama_pelanggan_code,ISNULL(kew_appr_gl_cd, ''), "
                                        + " Jenis_permohonan,no_Rujukan ,semak_sts,semak_dt,m2.Terma,Pv_no,m2.perkara,semak_id,p1.lulus_sts,keterangan "
                                        + "  ) as sb1 group by pv_item,sb1.no_permohonan,sb1.desc1,sb1.jp_desc,sb1.tarkih_permohonan,sb1.nama_pelanggan_code,sb1.status,sb1.jp_cd,sb1.no_inv,sb1.semak_sts,sb1.semakdt,sb1.tk_invois,sb1.tk_mohon,sb1.kat_penerima,sb1.Terma,sb1.pv_no,sb1.perkara,sb1.pcod,sb1.semak_id,sb1.lulus_sts "
                                        + " ) as a outer apply(select gl_invois_no from KW_General_Ledger where gl_jenis_no=a.no_permohonan group by gl_invois_no) as b) as a where  no_permohonan ='" + Session["cmd_arg3"].ToString() + "' and Keteragan='" + Session["cmd_arg2"].ToString() + "' ) as a outer apply( select Ref_nama_syarikat s1,Ref_alamat s2, ref_no_telefone s3,Ref_no_fax s4 from KW_Ref_Pembekal s4 where s4.Ref_no_syarikat=a.kepada  union all  select Ref_nama_syarikat s1,Ref_alamat s2, ref_no_telefone s3,Ref_no_fax s4 from  KW_Ref_Pelanggan s4_1 where s4_1.Ref_no_syarikat=a.kepada  union all  select stf_name s1,stf_permanent_address s2, stf_phone_h s3, stf_phone_m s4 from  hr_staff_profile s4_2 where s4_2.stf_staff_no = a.kepada) as b outer apply(select Format(GL_post_dt,'dd/MM/yyyy') post_dt,kod_akaun,kod_bajet bajet,GL_desc1,KW_Debit_amt,KW_kredit_amt from KW_General_Ledger where gl_jenis_no='" + nameshbt + "') as c"
                                        + " outer apply(select No_cek from KW_Pembayaran_Pay_voucer where  no_invois='"+ Session["cmd_arg3"].ToString() + "' and lulus_sts='L' and Pv_no='" + nameshbt + "') as d ";

                        }


                    }
                    dt = Dbcon.Ora_Execute_table(query1);

                    rptcredit.Reset();

                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();

                    if (countRow != 0)
                    {

                        string imagePath = string.Empty;
                        if (
                            dt.Rows[0]["v1"].ToString() != "")
                        {
                            imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/" + dt.Rows[0]["v1"].ToString() + "")).AbsoluteUri;

                        }
                        else
                        {
                            imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/user.png")).AbsoluteUri;
                        }

                        txtError.Text = "";
                        //Display Report
                        //rptcredit.LocalReport.DataSources.Clear();
                        string gt_datast = string.Empty;




                        //Path
                        if (Session["rpt_type"].ToString() == "KREDIT")
                        {
                            gt_datast = "kwkredit";
                            if (Session["rpt_lang"].ToString() == "mal")
                            {
                                rptcredit.LocalReport.ReportPath = "kewengan/KW_Kredit_Note.rdlc";
                            }
                            else
                            {
                                rptcredit.LocalReport.ReportPath = "kewengan/KW_Kredit_Note_eng.rdlc";
                            }
                        }
                        else if (Session["rpt_type"].ToString() == "DEBIT")
                        {
                            gt_datast = "kwkredit";
                            if (Session["rpt_lang"].ToString() == "mal")
                            {
                                rptcredit.LocalReport.ReportPath = "kewengan/KW_Debit_Note_ma.rdlc";
                            }
                            else
                            {
                                rptcredit.LocalReport.ReportPath = "kewengan/KW_Debit_Note_eng.rdlc";
                            }
                        }
                        else if (Session["rpt_type"].ToString() == "INVOIS")
                        {
                            gt_datast = "kwkredit";
                            if (Session["rpt_lang"].ToString() == "mal")
                            {
                                rptcredit.LocalReport.ReportPath = "kewengan/KW_Invois_Note_ma.rdlc";
                            }
                            else
                            {
                                rptcredit.LocalReport.ReportPath = "kewengan/KW_Invois_Note_eng.rdlc";
                            }
                        }
                        else if (Session["rpt_type"].ToString() == "payment_voucher")
                        {
                            dt1 = Dbcon.Ora_Execute_table(query2);
                            gt_datast = "Payment_vou";
                            if (Session["rpt_lang"].ToString() == "mal")
                            {
                                rptcredit.LocalReport.ReportPath = "kewengan/Kw_payment_note.rdlc";
                            }
                            else
                            {
                                rptcredit.LocalReport.ReportPath = "kewengan/KW_payment_eng.rdlc";
                            }
                            ReportDataSource rds1 = new ReportDataSource("Payment_vou1", dt1);
                            rptcredit.LocalReport.DataSources.Add(rds1);
                        }
                        rptcredit.LocalReport.EnableExternalImages = true;
                        ReportDataSource rds = new ReportDataSource(gt_datast, dt);
                       

                        //Parameters
                        ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("v1",imagePath)
                   
                     };


                        rptcredit.LocalReport.SetParameters(rptParams);
                        rptcredit.LocalReport.DataSources.Add(rds);
                        
                        //ReportViewer1.LocalReport.ExecuteReportInCurrentAppDomain(AppDomain.CurrentDomain.Evidence);
                        //Refresh
                        rptcredit.LocalReport.Refresh();

                        Warning[] warnings;

                        string[] streamids;

                        string mimeType;

                        string encoding;

                        string extension;

                        //string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
                        //       "  <PageWidth>12.20in</PageWidth>" +
                        //        "  <PageHeight>8.27in</PageHeight>" +
                        //        "  <MarginTop>0.1in</MarginTop>" +
                        //        "  <MarginLeft>0.5in</MarginLeft>" +
                        //         "  <MarginRight>0in</MarginRight>" +
                        //         "  <MarginBottom>0in</MarginBottom>" +
                        //       "</DeviceInfo>";

                        byte[] bytes = rptcredit.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = mimeType;
                        Response.AddHeader("content-disposition", "inline; filename=myfile." + extension);
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                txtError.Text = ex.ToString();

            }

        }
    }
}