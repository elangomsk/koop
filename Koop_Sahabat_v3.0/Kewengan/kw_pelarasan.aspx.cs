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

public partial class kw_pelarasan : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    float total = 0, total1 = 0;
    string level, userid;
    string Status = string.Empty;
    string qry1 = string.Empty, qry2 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()});";
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
                }
                else
                {
                    ver_id.Text = "0";
                    FirstGridViewRow();
                    AddNewRow();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('818','705','1803','1772','67','1338','61','15','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Button4.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());       
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
           
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    private void FirstGridViewRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Col1", typeof(string)));
        dt.Columns.Add(new DataColumn("Col2", typeof(string)));
        dt.Columns.Add(new DataColumn("Col3", typeof(string)));
        dt.Columns.Add(new DataColumn("Col4", typeof(string)));
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Col1"] = string.Empty;
        dr["Col2"] = string.Empty;
        dr["Col3"] = string.Empty;
        dr["Col4"] = string.Empty;
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;

        grvStudentDetails.DataSource = dt;
        grvStudentDetails.DataBind();
        grvStudentDetails.FooterRow.Cells[2].Text = "JUMLAH (RM) :";
        grvStudentDetails.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRow1();
    }


    private void AddNewRow1()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            //if (dtCurrentTable.Rows.Count < 2)
            //{
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList TextBoxName =
                          (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col1");
                        System.Web.UI.WebControls.TextBox TextBoxAge =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col2");
                        System.Web.UI.WebControls.TextBox TextBoxAddress =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("Col3");
                        System.Web.UI.WebControls.TextBox RBLGender =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("Col4");
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Col1"] = TextBoxName.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col2"] = TextBoxAge.Text;
                        dtCurrentTable.Rows[i - 1]["Col3"] = TextBoxAddress.Text;
                        dtCurrentTable.Rows[i - 1]["Col4"] = RBLGender.Text;
                        rowIndex++;
                        if (TextBoxAddress.Text != "")
                        {
                            decimal amt1 = Convert.ToDecimal(TextBoxAddress.Text);
                            total += (float)amt1;
                        }
                        if (RBLGender.Text != "")
                        {
                            decimal amt2 = Convert.ToDecimal(RBLGender.Text);
                            total1 += (float)amt2;
                        }
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grvStudentDetails.DataSource = dtCurrentTable;
                    grvStudentDetails.DataBind();
                    grvStudentDetails.FooterRow.Cells[2].Text = "JUMLAH (RM) :";
                    grvStudentDetails.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    ((System.Web.UI.WebControls.TextBox)grvStudentDetails.FooterRow.Cells[3].FindControl("lblTotal1")).Text = total.ToString("C").Replace("RM", "").Replace("$", "");
                    ((System.Web.UI.WebControls.TextBox)grvStudentDetails.FooterRow.Cells[4].FindControl("lblTotal2")).Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
                    //if (dtCurrentTable.Rows.Count == 2)
                    //{
                    //    grvStudentDetails.FooterRow.Cells[1].Enabled = false;
                    //}
                    //else
                    //{
                    //    grvStudentDetails.FooterRow.Cells[1].Enabled = true;
                    //}
                }
            //}
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData();
    }
    private void AddNewRow()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count < 2)
            {
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList TextBoxName =
                          (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col1");
                        System.Web.UI.WebControls.TextBox TextBoxAge =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col2");
                        System.Web.UI.WebControls.TextBox TextBoxAddress =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("Col3");
                        System.Web.UI.WebControls.TextBox RBLGender =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("Col4");
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Col1"] = TextBoxName.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col2"] = TextBoxAge.Text;
                        dtCurrentTable.Rows[i - 1]["Col3"] = TextBoxAddress.Text;
                        dtCurrentTable.Rows[i - 1]["Col4"] = RBLGender.Text;
                        rowIndex++;
                        if (TextBoxAddress.Text != "")
                        {
                            decimal amt1 = Convert.ToDecimal(TextBoxAddress.Text);
                            total += (float)amt1;
                        }
                        if (RBLGender.Text != "")
                        {
                            decimal amt2 = Convert.ToDecimal(RBLGender.Text);
                            total1 += (float)amt2;
                        }
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grvStudentDetails.DataSource = dtCurrentTable;
                    grvStudentDetails.DataBind();
                    grvStudentDetails.FooterRow.Cells[2].Text = "JUMLAH (RM) :";
                    grvStudentDetails.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    ((System.Web.UI.WebControls.TextBox)grvStudentDetails.FooterRow.Cells[3].FindControl("lblTotal1")).Text = total.ToString("C").Replace("RM", "").Replace("$", "");
                    ((System.Web.UI.WebControls.TextBox)grvStudentDetails.FooterRow.Cells[4].FindControl("lblTotal2")).Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
                    //if (dtCurrentTable.Rows.Count == 2)
                    //{
                    //    grvStudentDetails.FooterRow.Cells[1].Enabled = false;
                    //}
                    //else
                    //{
                    //    grvStudentDetails.FooterRow.Cells[1].Enabled = true;
                    //}
                }
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData();
    }



    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList TextBoxName =
                       (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col1");
                    System.Web.UI.WebControls.TextBox TextBoxAge =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col2");
                    System.Web.UI.WebControls.TextBox TextBoxAddress =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("Col3");
                    System.Web.UI.WebControls.TextBox RBLGender =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("Col4");

                    TextBoxName.SelectedValue = dt.Rows[i]["Col1"].ToString();
                    TextBoxAge.Text = dt.Rows[i]["Col2"].ToString();
                    TextBoxAddress.Text = dt.Rows[i]["Col3"].ToString();
                    RBLGender.Text = dt.Rows[i]["Col4"].ToString();
                    rowIndex++;
                    if (TextBoxAddress.Text != "")
                    {
                        decimal amt1 = Convert.ToDecimal(TextBoxAddress.Text);
                        total += (float)amt1;
                    }
                    if (RBLGender.Text != "")
                    {
                        decimal amt2 = Convert.ToDecimal(RBLGender.Text);
                        total1 += (float)amt2;
                    }
                }
            }
        }
    }

    protected void grvStudentDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                grvStudentDetails.DataSource = dt;
                grvStudentDetails.DataBind();

                for (int i = 0; i < grvStudentDetails.Rows.Count - 1; i++)
                {
                    grvStudentDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousData();
                grvStudentDetails.FooterRow.Cells[2].Text = "JUMLAH (RM) :";
                grvStudentDetails.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                ((System.Web.UI.WebControls.TextBox)grvStudentDetails.FooterRow.Cells[3].FindControl("lblTotal1")).Text = total.ToString("C").Replace("RM", "").Replace("$", "");
                ((System.Web.UI.WebControls.TextBox)grvStudentDetails.FooterRow.Cells[4].FindControl("lblTotal2")).Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
            }
        }
    }

    private void SetRowData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList TextBoxName =
                        (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col1");
                    System.Web.UI.WebControls.TextBox TextBoxAge =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col2");
                    System.Web.UI.WebControls.TextBox TextBoxAddress =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("Col3");
                    System.Web.UI.WebControls.TextBox RBLGender =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("Col4");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Col1"] = TextBoxName.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = TextBoxAge.Text;
                    dtCurrentTable.Rows[i - 1]["Col3"] = TextBoxAddress.Text;
                    dtCurrentTable.Rows[i - 1]["Col4"] = RBLGender.Text;
                    rowIndex++;

                }

                ViewState["CurrentTable"] = dtCurrentTable;
                //grvStudentDetails.DataSource = dtCurrentTable;
                //grvStudentDetails.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //SetPreviousData();
    }

    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            var ddl = (DropDownList)e.Row.FindControl("Col1");
            //int CountryId = Convert.ToInt32(e.Row.Cells[0].Text);
            SqlCommand cmd = new SqlCommand("select kod_akaun,(kod_akaun + ' | ' + nama_akaun) as name from KW_Ref_Carta_Akaun where jenis_akaun_type !='1' and Status='A' order by kod_akaun asc", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddl.DataSource = ds;
            ddl.DataTextField = "name";
            ddl.DataValueField = "kod_akaun";
            ddl.DataBind();
            ddl.SelectedValue = ((DataRowView)e.Row.DataItem)["Col1"].ToString();
            ddl.Items.Insert(0, new ListItem("--- PILIH ---", ""));


            var samp = Request.Url.Query;
            if (samp != "")
            {
                System.Web.UI.WebControls.TextBox txt = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("Col3");
                System.Web.UI.WebControls.TextBox txt1 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("Col4");
                float sval1 = 0, sval2 = 0;
                if (txt.Text == "")
                {
                    sval1 = 0;
                }
                else
                {
                    sval1 = (float)Convert.ToDecimal(txt.Text);
                }

                if (txt1.Text == "")
                {
                    sval2 = 0;
                }
                else
                {
                    sval2 = (float)Convert.ToDecimal(txt1.Text);
                }

                total += sval1;
                total1 += sval2;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            System.Web.UI.WebControls.TextBox lblamount1 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("lblTotal1");
            System.Web.UI.WebControls.TextBox lblamount2 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("lblTotal2");
            lblamount1.Text = total.ToString("C").Replace("RM", "").Replace("$", "");
            lblamount2.Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
        }
    }

      
    protected void clk_submit(object sender, EventArgs e)
    {
        if (TextBox1.Text != "" && TextBox2.Text != "")
        {
            string tk_m = string.Empty, tk_a = string.Empty;
            string rcount = string.Empty;
            int count = 0;
            if (TextBox2.Text != "")
            {
                DateTime dt_1 = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tk_m = dt_1.ToString("yyyy-MM-dd");
            }

            if (ver_id.Text == "0")
            {

                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select * From KW_Pelarasan where no_rujukan='" + TextBox1.Text + "'");
                if (ddokdicno.Rows.Count == 0)
                {
                    foreach (GridViewRow row in grvStudentDetails.Rows)
                    {
                        count++;
                        rcount = count.ToString();
                        string val1 = ((DropDownList)row.FindControl("Col1")).SelectedValue;
                        string val2 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col2")).Text.ToString();
                        string val3 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col3")).Text.ToString();
                        string val4 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col4")).Text.ToString();
                        DataTable ddokdicno1 = new DataTable();
                        ddokdicno1 = DBCon.Ora_Execute_table("select * From KW_Ref_Carta_Akaun where kod_akaun='" + val1 + "'");
                        string Inssql = "insert into KW_Pelarasan(no_rujukan,keterangan,tarikh,kod_akaun,akaun_keterangan,debit_amt,kredit_amt,seq_no,Status,crt_id,cr_dt) values ('" + TextBox1.Text.Replace("'", "''") + "','" + TextBox4.Text.Replace("'", "''") + "','" + tk_m + "','" + val1 + "','" + val2 + "','" + val3 + "','" + val4 + "','" + rcount + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql);
                        if (Status == "SUCCESS")
                        {
                            string Inssql1 = "insert into KW_General_Ledger(kod_akaun,KW_Debit_amt,KW_kredit_amt,kat_akaun,GL_invois_no,ref2,GL_process_dt,GL_desc1,crt_id,cr_dt,GL_sts) values ('" + val1 + "','" + val3 + "','" + val4 + "','" + ddokdicno1.Rows[0]["kat_akaun"].ToString() + "','" + TextBox1.Text.Replace("'", "''") + "','" + TextBox4.Text.Replace("'", "''") + "','" + tk_m + "','" + val2 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','A')";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                        }
                    }
                    if (Status == "SUCCESS")
                    {
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Session["validate_success"] = "SUCCESS";
                        Response.Redirect("../kewengan/kw_pelarasan_view.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                foreach (GridViewRow row in grvStudentDetails.Rows)
                {
                    string rno = ((System.Web.UI.WebControls.Label)row.FindControl("RowNumber")).Text.ToString();
                    string val1 = ((DropDownList)row.FindControl("Col1")).SelectedValue;
                    string val2 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col2")).Text.ToString();
                    string val3 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col3")).Text.ToString();
                    string val4 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col4")).Text.ToString();
                    string Inssql = "UPDATE KW_Pelarasan SET keterangan='" + TextBox4.Text.Replace("'", "''") + "',tarikh='" + tk_m + "',kod_akaun='" + val1 + "',akaun_keterangan='" + val2 + "',debit_amt='" + val3 + "',kredit_amt='" + val4 + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where seq_no='" + rno + "' and no_rujukan='" + TextBox1.Text.Replace("'", "''") + "'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                }
                if (Status == "SUCCESS")
                {

                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Session["validate_success"] = "SUCCESS";
                    Response.Redirect("../kewengan/kw_pelarasan_view.aspx");
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

    
    protected void QtyChanged_deb(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;

        GridViewRow row = ((GridViewRow)((System.Web.UI.WebControls.TextBox)sender).NamingContainer);
        System.Web.UI.WebControls.TextBox debit = (System.Web.UI.WebControls.TextBox)row.FindControl("col3");
        System.Web.UI.WebControls.TextBox kredit = (System.Web.UI.WebControls.TextBox)row.FindControl("col4");
        if (debit.Text != "0.00")
        {
            kredit.Attributes.Add("Readonly", "Readonly");
            //kredit.Attributes.Add("style", "pointer-events:None;");
            debit.Attributes.Remove("Readonly");
            //debit.Attributes.Remove("style");
        }
        else
        {
            kredit.Attributes.Remove("Readonly");
            //kredit.Attributes.Remove("style");
            debit.Attributes.Add("Readonly", "Readonly");
            //debit.Attributes.Add("style", "pointer-events:None;");
        }

        decimal GTotal = 0;
        decimal GTotal1 = 0;
        decimal ITotal = 0;
        decimal Gst = 0;
        for (int i = 0; i < grvStudentDetails.Rows.Count; i++)
        {
            String total = (grvStudentDetails.Rows[i].FindControl("col3") as System.Web.UI.WebControls.TextBox).Text;

            System.Web.UI.WebControls.TextBox jumI = (System.Web.UI.WebControls.TextBox)grvStudentDetails.FooterRow.FindControl("lblTotal1");
            if (total != "")
            {
                GTotal1 = (decimal)Convert.ToDecimal(total);
            }
            else
            {
                GTotal1 = 0;
            }
            GTotal += (decimal)Convert.ToDecimal(GTotal1);

            jumI.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");

        }


    }

    protected void QtyChanged_kre(object sender, EventArgs eventArgs)
    {
        decimal numTotal = 0;

        GridViewRow row = ((GridViewRow)((System.Web.UI.WebControls.TextBox)sender).NamingContainer);
        System.Web.UI.WebControls.TextBox debit = (System.Web.UI.WebControls.TextBox)row.FindControl("col3");
        System.Web.UI.WebControls.TextBox kredit = (System.Web.UI.WebControls.TextBox)row.FindControl("col4");
        if (kredit.Text != "0.00")
        {
            debit.Attributes.Add("Readonly", "Readonly");
            //debit.Attributes.Add("style", "pointer-events:None;");
            kredit.Attributes.Remove("Readonly");
            //kredit.Attributes.Remove("style");
        }
        else
        {
            debit.Attributes.Remove("Readonly");
            kredit.Attributes.Add("Readonly", "Readonly");
            //kredit.Attributes.Add("style", "pointer-events:None;");
            //debit.Attributes.Remove("style");
        }

        decimal GTotal = 0;
        decimal GTotal1 = 0;
        decimal ITotal = 0;
        decimal Gst = 0;
        for (int i = 0; i < grvStudentDetails.Rows.Count; i++)
        {
            String total = (grvStudentDetails.Rows[i].FindControl("col4") as System.Web.UI.WebControls.TextBox).Text;

            System.Web.UI.WebControls.TextBox jumI = (System.Web.UI.WebControls.TextBox)grvStudentDetails.FooterRow.FindControl("lblTotal2");
            if (total != "")
            {
                GTotal1 = (decimal)Convert.ToDecimal(total);
            }
            else
            {
                GTotal1 = 0;
            }
            GTotal += (decimal)Convert.ToDecimal(GTotal1);

            jumI.Text = GTotal.ToString("C").Replace("RM", "").Replace("$", "");

        }

    }
    protected void BindData_show()
    {

        con.Open();
        DataTable ddicno = new DataTable();
        SqlCommand cmd = new SqlCommand("select s1.seq_no as RowNumber,s1.kod_akaun as col1,s1.akaun_keterangan as col2,s1.debit_amt as col3,s1.kredit_amt as col4 from KW_Pelarasan s1 where s1.no_rujukan='" + get_id.Text + "' order by s1.ID asc", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            FirstGridViewRow();
            AddNewRow();
        }
        else
        {
            grvStudentDetails.DataSource = ds;
            grvStudentDetails.DataBind();
            grvStudentDetails.FooterRow.Cells[2].Text = "JUMLAH (RM) :";
            grvStudentDetails.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            
        }

        con.Close();
    }
    void view_details()
    {
        Button1.Visible = false;
        string lblid = lbl_name.Text;
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select s1.no_rujukan,s1.keterangan,FORMAT(s1.tarikh,'dd/MM/yyyy', 'en-us') tarikh,s1.kod_akaun,s1.akaun_keterangan,s1.debit_amt,s1.kredit_amt,s1.seq_no,s1.Status from KW_Pelarasan s1 where s1.no_rujukan='" + lblid + "' ");
        if (ddokdicno.Rows.Count != 0)
        {
            Button4.Text = "Kemaskini";
            Button4.Visible = false;
            TextBox1.Attributes.Add("Readonly", "Readonly");
            ver_id.Text = "1";
            get_id.Text = lblid;
            TextBox1.Text = ddokdicno.Rows[0]["no_rujukan"].ToString();
            TextBox2.Text = ddokdicno.Rows[0]["tarikh"].ToString();
            TextBox4.Text = ddokdicno.Rows[0]["keterangan"].ToString();
            BindData_show();

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_pelarasan.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_pelarasan_view.aspx");
    }

    
}