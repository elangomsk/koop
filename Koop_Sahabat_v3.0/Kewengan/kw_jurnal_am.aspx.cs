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

public partial class kw_jurnal_am : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
   
    string level, userid;
    string Status = string.Empty;
    string uniqueId;
    string qry1 = string.Empty, qry2 = string.Empty;
    float total = 0, total1 = 0;
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
                    GetMohon();
                    Get_GL_no();
                    TextBox2.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

    private void GetMohon()
    {
        DataTable dt1 = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='22' and Status='A'");
        if (dt1.Rows.Count != 0)
        {
            if (dt1.Rows[0]["cfmt"].ToString() == "")
            {
                TextBox1.Text = dt1.Rows[0]["fmt"].ToString();
                TextBox1.Attributes.Add("disabled", "disabled");
            }
            else
            {
                int seqno = Convert.ToInt32(dt1.Rows[0]["lfrmt2"].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString(dt1.Rows[0]["lfrmt1"].ToString() + "0000");
                TextBox1.Text = uniqueId;
                TextBox1.Attributes.Add("disabled", "disabled");
            }

        }
        else
        {
            DataTable get_doc = new DataTable();
            get_doc = DBCon.Ora_Execute_table("select Ref_doc_descript as s1,s1.ws_format as s2 from KW_Ref_Doc_kod left join site_settings s1 on  s1.ID='1' where Ref_doc_code='22'");
            DataTable dt = DBCon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_rujukan,13,3000)),'0') from KW_Pelarasan");
            if (dt.Rows.Count > 0)
            {
                int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());
                int newNumber = seqno + 1;
                uniqueId = newNumber.ToString(get_doc.Rows[0][1].ToString() + "-" + get_doc.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                TextBox1.Text = uniqueId;
                TextBox1.Attributes.Add("disabled", "disabled");

            }
            else
            {
                int newNumber = Convert.ToInt32(uniqueId) + 1;
                uniqueId = newNumber.ToString(get_doc.Rows[0][1].ToString() + "-" + get_doc.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");
                TextBox1.Text = uniqueId;
                TextBox1.Attributes.Add("disabled", "disabled");
            }
        }


    }

    private void Get_GL_no()

    {

        DataTable dt1 = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='23' and Status='A'");

        if (dt1.Rows.Count != 0)

        {

            if (dt1.Rows[0]["cfmt"].ToString() == "")

            {

                TextBox5.Text = dt1.Rows[0]["fmt"].ToString();

                TextBox5.Attributes.Add("disabled", "disabled");

            }

            else

            {

                int seqno = Convert.ToInt32(dt1.Rows[0]["lfrmt2"].ToString());

                int newNumber = seqno + 1;

                uniqueId = newNumber.ToString(dt1.Rows[0]["lfrmt1"].ToString() + "0000");

                TextBox5.Text = uniqueId;

                TextBox5.Attributes.Add("disabled", "disabled");

            }



        }

        else

        {

            DataTable get_doc = new DataTable();

            get_doc = DBCon.Ora_Execute_table("select Ref_doc_descript as s1,s1.ws_format as s2 from KW_Ref_Doc_kod left join site_settings s1 on  s1.ID='1' where Ref_doc_code='23'");

            DataTable dt = DBCon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(GL_journal_no,13,3000)),'0') from KW_General_Ledger");

            if (dt.Rows.Count > 0)

            {

                int seqno = Convert.ToInt32(dt.Rows[0][0].ToString());

                int newNumber = seqno + 1;

                uniqueId = newNumber.ToString(get_doc.Rows[0][1].ToString() + "-" + get_doc.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");

                TextBox5.Text = uniqueId;

                TextBox5.Attributes.Add("disabled", "disabled");



            }

            else

            {

                int newNumber = Convert.ToInt32(uniqueId) + 1;

                uniqueId = newNumber.ToString(get_doc.Rows[0][1].ToString() + "-" + get_doc.Rows[0]["s1"].ToString() + "-" + DateTime.Now.ToString("yyyy") + "-" + "0000");

                TextBox5.Text = uniqueId;

                TextBox5.Attributes.Add("disabled", "disabled");

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
        //dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dr = dt.NewRow();
        //dr["RowNumber"] = 1;
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
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
        float total = 0, total1 = 0;
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
                          (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[0].FindControl("Col1");
                        System.Web.UI.WebControls.TextBox TextBoxAge =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col2");
                    DropDownList col5 =
                         (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col3");
                    System.Web.UI.WebControls.TextBox TextBoxAddress =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("Col4");
                        System.Web.UI.WebControls.TextBox RBLGender =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("Col5");
                        drCurrentRow = dtCurrentTable.NewRow();
                        //drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["column1"] = TextBoxName.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["column2"] = TextBoxAge.Text;
                    dtCurrentTable.Rows[i - 1]["column3"] = col5.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["column4"] = TextBoxAddress.Text;
                        dtCurrentTable.Rows[i - 1]["column5"] = RBLGender.Text;
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
        float total = 0, total1 = 0;
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
                          (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[0].FindControl("Col1");
                        System.Web.UI.WebControls.TextBox TextBoxAge =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col2");
                        DropDownList col5 =
                        (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col3");
                        System.Web.UI.WebControls.TextBox TextBoxAddress =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("Col4");
                        System.Web.UI.WebControls.TextBox RBLGender =
                          (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("Col5");
                        drCurrentRow = dtCurrentTable.NewRow();
                        //drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["column1"] = TextBoxName.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["column2"] = TextBoxAge.Text;
                        dtCurrentTable.Rows[i - 1]["column3"] = col5.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["column4"] = TextBoxAddress.Text;
                        dtCurrentTable.Rows[i - 1]["column5"] = RBLGender.Text;
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
        float total = 0, total1 = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList TextBoxName =
                       (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[0].FindControl("Col1");
                    System.Web.UI.WebControls.TextBox TextBoxAge =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("Col2");
                    DropDownList col5 =
                       (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("Col3");
                    System.Web.UI.WebControls.TextBox TextBoxAddress =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("Col4");
                    System.Web.UI.WebControls.TextBox RBLGender =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("Col5");

                    TextBoxName.SelectedValue = dt.Rows[i]["column1"].ToString();
                    TextBoxAge.Text = dt.Rows[i]["column2"].ToString();
                    col5.SelectedValue = dt.Rows[i]["column3"].ToString();
                    TextBoxAddress.Text = dt.Rows[i]["column4"].ToString();
                    RBLGender.Text = dt.Rows[i]["column5"].ToString();
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
        float total = 0, total1 = 0;
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
                    DropDownList col5 =
                        (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("Col3");
                    System.Web.UI.WebControls.TextBox TextBoxAddress =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("Col4");
                    System.Web.UI.WebControls.TextBox RBLGender =
                      (System.Web.UI.WebControls.TextBox)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("Col5");
                    drCurrentRow = dtCurrentTable.NewRow();
                    //drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Col1"] = TextBoxName.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = TextBoxAge.Text;
                    dtCurrentTable.Rows[i - 1]["Col3"] = col5.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col4"] = TextBoxAddress.Text;
                    dtCurrentTable.Rows[i - 1]["Col5"] = RBLGender.Text;
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
            SqlCommand cmd = new SqlCommand("select kod_akaun,(kod_akaun + ' | ' + nama_akaun) as name from KW_Ref_Carta_Akaun where jenis_akaun_type !='1' and ISNULL(kw_acc_header,'0')='0' and Status='A' order by kod_akaun asc", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddl.DataSource = ds;
            ddl.DataTextField = "name";
            ddl.DataValueField = "kod_akaun";
            ddl.DataBind();
            ddl.SelectedValue = ((DataRowView)e.Row.DataItem)["column1"].ToString();
            ddl.Items.Insert(0, new ListItem("--- PILIH ---", ""));


            DropDownList ddprojek = (e.Row.FindControl("Col3") as DropDownList);
            ddprojek.DataSource = GetData("SELECT Ref_Projek_code,Ref_Projek_name FROM KW_Ref_Projek where status='A'");
            ddprojek.DataTextField = "Ref_Projek_name";
            ddprojek.DataValueField = "Ref_Projek_code";
            ddprojek.DataBind();
            ddprojek.SelectedValue = ((DataRowView)e.Row.DataItem)["column3"].ToString();
            //Add Default Item in the DropDownList
            ddprojek.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            

           

            System.Web.UI.WebControls.TextBox txt = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("Col4");
            System.Web.UI.WebControls.TextBox txt1 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("Col5");
            //var samp = Request.Url.Query;
            //if (samp != "")
            //{
          
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

                if(sval1 == 0 && sval2 != 0)
            {
                txt.Attributes.Add("Readonly", "Readonly");
                txt1.Attributes.Remove("Readonly");
            }
                else if (sval2 == 0 && sval1 != 0)
            {
                txt.Attributes.Remove("Readonly");
                txt1.Attributes.Add("Readonly", "Readonly");
            }
            else
            {
                txt.Attributes.Remove("Readonly");
                txt1.Attributes.Remove("Readonly");
            }

            

            total += sval1;
                total1 += sval2;
            //}
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            System.Web.UI.WebControls.TextBox lblamount1 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("lblTotal1");
            System.Web.UI.WebControls.TextBox lblamount2 = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("lblTotal2");
            lblamount1.Text = total.ToString("C").Replace("RM", "").Replace("$", "");
            lblamount2.Text = total1.ToString("C").Replace("RM", "").Replace("$", "");
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

                DataTable gen_invkd = new DataTable();
                gen_invkd = DBCon.Ora_Execute_table("select * From KW_Pelarasan where no_rujukan='" + TextBox1.Text + "'");
                
                    if (gen_invkd.Rows.Count > 0)
                {
                    DataTable dtmb_db1 = DBCon.Ora_Execute_table("select doc_type_cd,format as fmt,SUBSTRING(cur_format, 1, 12) as lfrmt1,SUBSTRING(cur_format, 13, 4) as lfrmt2,cur_format as cfmt from KW_Format_Nombor_rujukan where doc_type_cd='22' and Status='A'");
                    if (dtmb_db1.Rows.Count != 0)
                    {
                        if (dtmb_db1.Rows[0]["cfmt"].ToString() == "")
                        {
                            TextBox3.Text = dtmb_db1.Rows[0]["fmt"].ToString();
                        }
                        else
                        {
                            int seqno = Convert.ToInt32(dtmb_db1.Rows[0]["lfrmt2"].ToString());
                            int newNumber = seqno + 1;
                            uniqueId = newNumber.ToString(dtmb_db1.Rows[0]["lfrmt1"].ToString() + "0000");
                            TextBox3.Text = uniqueId;
                        }

                    }
                    else
                    {

                        DataTable get_doc = new DataTable();
                        get_doc = DBCon.Ora_Execute_table("select Ref_doc_descript as s1 from KW_Ref_Doc_kod where Ref_doc_code='22'");

                        DataTable dtmb_db2 = DBCon.Ora_Execute_table("select   ISNULL(max(SUBSTRING(no_rujukan,13,3000)),'0') from KW_Pelarasan");
                        if (dtmb_db2.Rows.Count > 0)
                        {
                            int seqno = Convert.ToInt32(dtmb_db2.Rows[0][0].ToString());
                            int newNumber = seqno + 1;
                            uniqueId = newNumber.ToString(get_doc.Rows[0]["s1"].ToString() + " - " + DateTime.Now.ToString("yyyy") + " - " + "0000");
                            TextBox3.Text = uniqueId;

                        }
                        else
                        {
                            int newNumber = Convert.ToInt32(uniqueId) + 1;
                            uniqueId = newNumber.ToString(get_doc.Rows[0]["s1"].ToString() + " - " + DateTime.Now.ToString("yyyy") + " - " + "0000");
                            TextBox3.Text = uniqueId;
                        }
                    }

                }
                else
                {
                    TextBox3.Text = TextBox1.Text;
                }
                if (gen_invkd.Rows.Count == 0)
                {

                    decimal kreamt = 0;
                    decimal kreamt1 = 0;
                    decimal debamt = 0;
                    decimal debamt1 = 0;
                    int akaun_cnt = 0, j = 1, i = 0;
                    foreach (GridViewRow g1 in grvStudentDetails.Rows)
                    {
                        string val1 = ((DropDownList)g1.FindControl("Col1")).SelectedValue;
                        string val4 = ((System.Web.UI.WebControls.TextBox)g1.FindControl("Col4")).Text.ToString();
                        string val5 = ((System.Web.UI.WebControls.TextBox)g1.FindControl("Col5")).Text.ToString();


                        if (val1 != "")
                        {
                            akaun_cnt = j;
                            j++;
                        }

                        if (val4 != "0.00" && val4 != "")
                        {
                            debamt1 += decimal.Parse(val4);
                        }

                        if (val5 != "0.00" && val5 != "")
                        {
                            kreamt1 += decimal.Parse(val5);
                        }

                    }
                    if (akaun_cnt == grvStudentDetails.Rows.Count)
                    {
                        if (debamt1 == kreamt1)
                        {
                            string Inssql1 = "insert into KW_Pelarasan(no_rujukan,tarikh,pel_perkara,debit_amt,kredit_amt,pel_lulus_sts,Status,crt_id,cr_dt) values ('" + TextBox3.Text + "','" + tk_m + "','" + TextBox4.Text.Replace("'", "''") + "','0.00','0.00','L','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                            if (Status == "SUCCESS")
                            {
                                foreach (GridViewRow row in grvStudentDetails.Rows)
                                {

                                    string val1 = ((DropDownList)row.FindControl("Col1")).SelectedValue;
                                    string val2 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col2")).Text.ToString();
                                    string val3 = ((DropDownList)row.FindControl("Col3")).SelectedValue;
                                    string val4 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col4")).Text.ToString();
                                    string val5 = ((System.Web.UI.WebControls.TextBox)row.FindControl("Col5")).Text.ToString();

                                    System.Web.UI.WebControls.TextBox jumdeb = (System.Web.UI.WebControls.TextBox)grvStudentDetails.FooterRow.FindControl("lblTotal1");
                                    System.Web.UI.WebControls.TextBox jumkre = (System.Web.UI.WebControls.TextBox)grvStudentDetails.FooterRow.FindControl("lblTotal2");

                                    string Inssql = "insert into KW_Pelarasan_item(pel_no_jurnal,pel_gl_cd,pel_keterangan,pel_projek,pel_debit_amt,pel_kredit_amt,Status,crt_id,cr_dt) values ('" + TextBox3.Text + "','" + val1 + "','" + val2 + "','" + val3 + "','" + val4 + "','" + val5 + "','A','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                                    string upsql = "Update KW_Pelarasan set debit_amt='" + jumdeb.Text + "',kredit_amt='" + jumkre.Text + "' where no_rujukan='" + TextBox3.Text + "' and Status='A'";
                                    Status = DBCon.Ora_Execute_CommamdText(upsql);

                                    //GL

                                    string Ins_GL = "insert into kw_general_ledger(kod_akaun,KW_Debit_amt,KW_kredit_amt,GL_invois_no,ref2,GL_desc1,GL_type,GL_sts,GL_journal_no,GL_post_dt,GL_post_note,kod_bajet,Gl_jenis_no,project_kod,crt_id,cr_dt) values ('" + val1 + "','" + val4 + "','" + val5 + "','','22','" + TextBox4.Text + "','','L','" + TextBox5.Text + "','" + tk_m + "','" + val2 + "','','" + TextBox1.Text + "','" + val3 + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    Status = DBCon.Ora_Execute_CommamdText(Ins_GL);

                                }
                                if (Status == "SUCCESS")
                                {
                                    DataTable dt_upd_format = new DataTable();
                                    dt_upd_format = DBCon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + TextBox3.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='22' and Status = 'A'");
                                    DataTable dt_upd_format_gl = new DataTable();
                                    dt_upd_format_gl = DBCon.Ora_Execute_table("update KW_Format_Nombor_rujukan set cur_format='" + TextBox5.Text + "',upd_id='" + userid + "',upd_dt='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where doc_type_cd='23' and Status = 'A'");
                                    Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                                    Session["validate_success"] = "SUCCESS";
                                    Response.Redirect("../kewengan/kw_jurnal_am_view.aspx");
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Debit and Kredit Value Not Match.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Pilih Kod Akaun.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                string Inssql = "UPDATE KW_Pelarasan SET pel_perkara='" + TextBox4.Text.Replace("'", "''") + "',tarikh='" + tk_m + "',upd_id='" + Session["New"].ToString() + "',upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where no_rujukan='" + TextBox1.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {

                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Session["validate_success"] = "SUCCESS";
                    Response.Redirect("../kewengan/kw_jurnal_am_view.aspx");
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
        System.Web.UI.WebControls.TextBox debit = (System.Web.UI.WebControls.TextBox)row.FindControl("col4");
        System.Web.UI.WebControls.TextBox kredit = (System.Web.UI.WebControls.TextBox)row.FindControl("col5");
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
            String total = (grvStudentDetails.Rows[i].FindControl("col4") as System.Web.UI.WebControls.TextBox).Text;

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
        System.Web.UI.WebControls.TextBox debit = (System.Web.UI.WebControls.TextBox)row.FindControl("col4");
        System.Web.UI.WebControls.TextBox kredit = (System.Web.UI.WebControls.TextBox)row.FindControl("col5");
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
            String total = (grvStudentDetails.Rows[i].FindControl("col5") as System.Web.UI.WebControls.TextBox).Text;

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
        SqlCommand cmd = new SqlCommand("select s1.Id as RowNumber,s1.pel_gl_cd as column1,s1.pel_keterangan as column2,s1.pel_projek as column3,s1.pel_debit_amt as column4,s1.pel_kredit_amt as column5 from KW_Pelarasan_item s1 where s1.pel_no_jurnal='" + get_id.Text + "' order by s1.ID asc", con);
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
        ddokdicno = DBCon.Ora_Execute_table("select s1.no_rujukan,s1.pel_perkara,FORMAT(s1.tarikh,'dd/MM/yyyy', 'en-us') tarikh,s1.Status,b.GL_journal_no  from KW_Pelarasan s1 outer apply(select GL_journal_no from kw_general_ledger where Gl_jenis_no=s1.no_rujukan group by GL_journal_no) as b where s1.no_rujukan='" + lblid + "' ");
        if (ddokdicno.Rows.Count != 0)
        {
            Button4.Text = "Kemaskini";
            //Button4.Visible = false;
            TextBox1.Attributes.Add("Readonly", "Readonly");
            ver_id.Text = "1";
            get_id.Text = lblid;
            TextBox1.Text = ddokdicno.Rows[0]["no_rujukan"].ToString();
            TextBox2.Text = ddokdicno.Rows[0]["tarikh"].ToString();
            TextBox4.Text = ddokdicno.Rows[0]["pel_perkara"].ToString();
            TextBox5.Text = ddokdicno.Rows[0]["GL_journal_no"].ToString();
            BindData_show();

        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        TextBox4.Text = "";
        TextBox2.Text = DateTime.Now.ToString("dd/MM/yyyy");
        ver_id.Text = "0";
        FirstGridViewRow();
        AddNewRow();
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../kewengan/kw_jurnal_am_view.aspx");
    }

    
}