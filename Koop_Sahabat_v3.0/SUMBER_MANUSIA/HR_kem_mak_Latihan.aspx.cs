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

public partial class HR_kem_mak_Latihan : System.Web.UI.Page
{
    DBConnection Dblog = new DBConnection();
    string Status = string.Empty;
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    DBConnection dbcon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string userid;
    string ddate1 = string.Empty, ddate2 = string.Empty, ddate3 = string.Empty, ddate4 = string.Empty, ddate5 = string.Empty;
    string tout1 = string.Empty, tout2 = string.Empty, tout3 = string.Empty, tout4 = string.Empty, tout5 = string.Empty, tout6 = string.Empty, tout7 = string.Empty, tout8 = string.Empty, tout9 = string.Empty, tout10 = string.Empty, tout11 = string.Empty, tout12 = string.Empty, tout13 = string.Empty, tout14 = string.Empty, tout15 = string.Empty, tout16 = string.Empty, tout17 = string.Empty, tout18 = string.Empty, tout19 = string.Empty, tout20 = string.Empty, tout21 = string.Empty, tout22 = string.Empty, tout23 = string.Empty, tout24 = string.Empty, tout25 = string.Empty, tout26 = string.Empty, tout27 = string.Empty, tout28 = string.Empty, tout29 = string.Empty, tout30 = string.Empty, tout31 = string.Empty;
    string stout1 = string.Empty, stout2 = string.Empty, stout3 = string.Empty, stout4 = string.Empty, stout5 = string.Empty, stout6 = string.Empty, stout7 = string.Empty, stout8 = string.Empty, stout9 = string.Empty, stout10 = string.Empty, stout11 = string.Empty, stout12 = string.Empty, stout13 = string.Empty, stout14 = string.Empty, stout15 = string.Empty, stout16 = string.Empty, stout17 = string.Empty, stout18 = string.Empty, stout19 = string.Empty, stout20 = string.Empty, stout21 = string.Empty, stout22 = string.Empty, stout23 = string.Empty, stout24 = string.Empty, stout25 = string.Empty, stout26 = string.Empty, stout27 = string.Empty, stout28 = string.Empty, stout29 = string.Empty, stout30 = string.Empty, stout31 = string.Empty;
    string ttout1 = string.Empty, ttout2 = string.Empty, ttout3 = string.Empty, ttout4 = string.Empty, ttout5 = string.Empty, ttout6 = string.Empty, ttout7 = string.Empty, ttout8 = string.Empty, ttout9 = string.Empty, ttout10 = string.Empty, ttout11 = string.Empty, ttout12 = string.Empty, ttout13 = string.Empty, ttout14 = string.Empty, ttout15 = string.Empty, ttout16 = string.Empty, ttout17 = string.Empty, ttout18 = string.Empty, ttout19 = string.Empty, ttout20 = string.Empty, ttout21 = string.Empty, ttout22 = string.Empty, ttout23 = string.Empty, ttout24 = string.Empty, ttout25 = string.Empty, ttout26 = string.Empty, ttout27 = string.Empty, ttout28 = string.Empty, ttout29 = string.Empty, ttout30 = string.Empty, ttout31 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/FILES/ST/"));
                List<ListItem> files = new List<ListItem>();
                foreach (string filePath in filePaths)
                {
                    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                }
                //GridView1.DataSource = files;
                //GridView1.DataBind();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }
    static DataTable GetSchemaTable(string connectionString)
    {
        using (OleDbConnection connection = new
                   OleDbConnection(connectionString))
        {
            connection.Open();
            DataTable schemaTable = connection.GetOleDbSchemaTable(
                OleDbSchemaGuid.Tables,
                new object[] { null, null, null, "TABLE" });
            return schemaTable;
        }
    }

    void app_language()
    {
        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = dbcon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = dbcon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1487','448','143','1422','77')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentUICulture;
            TextInfo txtinfo = culinfo.TextInfo;

            h1_tag.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            bb1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            bb2_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());

            lbl1_text.InnerText = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }

    void filepath()
    {

        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        string excelPath = Server.MapPath("~/FILES/") + fileName;
        string directoryPath = Path.GetDirectoryName(excelPath);

        FileUpload1.SaveAs(excelPath);
        string conString = string.Empty;
        string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

        switch (extension)
        {
            case ".xls": //Excel 97-03
                conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07 or higher
                conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                break;

        }
        cs = string.Format(conString, excelPath);


    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }

    protected void UploadFile(object sender, EventArgs e)
    {
        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/ST/") + fileName);
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void DeleteFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        File.Delete(filePath);
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    void UPLOAD()
    {
        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FILES/ST/") + fileName);
        //Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (FileUpload1.PostedFile.FileName != "")
        {
            string excelPath = Server.MapPath("~/Files/Attendance/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(excelPath);


            string conString = string.Empty;
            string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                    break;

            }
            conString = string.Format(conString, excelPath);
            DataSet ds = new DataSet();

            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = conString;

            DataTable sheets = GetSchemaTable(conString);


            string FolderPath = @"C:\Source\";
            //Provide the table name in which you want to load the data
            string TableName = "Customer";
            //Provide the schema of table 
            string SchemaName = "dbo";
            //Provide the starting column for read actul records
            string StartingColumn = "A";
            //Provide the end column till which you want to read
            string EndingColumn = "AU";

            //Provide the row number from which you like to start reading
            string StartReadingFromRow1 = "4";
            string StartReadingFromRow = "3";
            //Provide the Database Name in which table or view exists
            string DatabaseName = "TechbrothersIT";
            //Upload and save the file


            foreach (DataRow r in sheets.Rows)
            {
                connection.Open();
                DataTable dtExcelData = new DataTable();
                string query = "SELECT * FROM [" + r[2].ToString() + StartingColumn + StartReadingFromRow + ":" + EndingColumn + "]";
                ds.Clear();
                OleDbDataAdapter data = new OleDbDataAdapter(query, connection);
                data.Fill(dtExcelData);
                connection.Close();
                if (dtExcelData.Columns.Count == 47)
                {
                    //first date
                    string atdate = dtExcelData.Rows[1][1].ToString();
                    string ddt = atdate.Substring(0, 7);

                    string atdate1 = dtExcelData.Rows[9][0].ToString();
                    string ddt1 = atdate1.Substring(0, 2);
                    string atedate1 = ddt + "-" + ddt1;

                    string atdate2 = dtExcelData.Rows[10][0].ToString();
                    string ddt2 = atdate2.Substring(0, 2);
                    string atedate2 = ddt + "-" + ddt2;

                    string atdate3 = dtExcelData.Rows[11][0].ToString();
                    string ddt3 = atdate3.Substring(0, 2);
                    string atedate3 = ddt + "-" + ddt3;

                    string atdate4 = dtExcelData.Rows[12][0].ToString();
                    string ddt4 = atdate4.Substring(0, 2);
                    string atedate4 = ddt + "-" + ddt4;

                    string atdate5 = dtExcelData.Rows[13][0].ToString();
                    string ddt5 = atdate5.Substring(0, 2);
                    string atedate5 = ddt + "-" + ddt5;

                    string atdate6 = dtExcelData.Rows[14][0].ToString();
                    string ddt6 = atdate6.Substring(0, 2);
                    string atedate6 = ddt + "-" + ddt6;

                    string atdate7 = dtExcelData.Rows[15][0].ToString();
                    string ddt7 = atdate7.Substring(0, 2);
                    string atedate7 = ddt + "-" + ddt7;

                    string atdate8 = dtExcelData.Rows[16][0].ToString();
                    string ddt8 = atdate8.Substring(0, 2);
                    string atedate8 = ddt + "-" + ddt8;

                    string atdate9 = dtExcelData.Rows[17][0].ToString();
                    string ddt9 = atdate9.Substring(0, 2);
                    string atedate9 = ddt + "-" + ddt9;

                    string atdate10 = dtExcelData.Rows[18][0].ToString();
                    string ddt10 = atdate10.Substring(0, 2);
                    string atedate10 = ddt + "-" + ddt10;

                    string atdate11 = dtExcelData.Rows[19][0].ToString();
                    string ddt11 = atdate11.Substring(0, 2);
                    string atedate11 = ddt + "-" + ddt11;

                    string atdate12 = dtExcelData.Rows[20][0].ToString();
                    string ddt12 = atdate12.Substring(0, 2);
                    string atedate12 = ddt + "-" + ddt12;

                    string atdate13 = dtExcelData.Rows[21][0].ToString();
                    string ddt13 = atdate13.Substring(0, 2);
                    string atedate13 = ddt + "-" + ddt13;

                    string atdate14 = dtExcelData.Rows[22][0].ToString();
                    string ddt14 = atdate14.Substring(0, 2);
                    string atedate14 = ddt + "-" + ddt14;

                    string atdate15 = dtExcelData.Rows[23][0].ToString();
                    string ddt15 = atdate15.Substring(0, 2);
                    string atedate15 = ddt + "-" + ddt15;

                    string atdate16 = dtExcelData.Rows[24][0].ToString();
                    string ddt16 = atdate16.Substring(0, 2);
                    string atedate16 = ddt + "-" + ddt16;


                    string atdate17 = dtExcelData.Rows[25][0].ToString();
                    string ddt17 = atdate17.Substring(0, 2);
                    string atedate17 = ddt + "-" + ddt17;

                    string atdate18 = dtExcelData.Rows[26][0].ToString();
                    string ddt18 = atdate18.Substring(0, 2);
                    string atedate18 = ddt + "-" + ddt18;

                    string atdate19 = dtExcelData.Rows[27][0].ToString();
                    string ddt19 = atdate19.Substring(0, 2);
                    string atedate19 = ddt + "-" + ddt19;

                    string atdate20 = dtExcelData.Rows[28][0].ToString();
                    string ddt20 = atdate20.Substring(0, 2);
                    string atedate20 = ddt + "-" + ddt20;

                    string atdate21 = dtExcelData.Rows[29][0].ToString();
                    string ddt21 = atdate21.Substring(0, 2);
                    string atedate21 = ddt + "-" + ddt21;

                    string atdate22 = dtExcelData.Rows[30][0].ToString();
                    string ddt22 = atdate22.Substring(0, 2);
                    string atedate22 = ddt + "-" + ddt22;

                    string atdate23 = dtExcelData.Rows[31][0].ToString();
                    string ddt23 = atdate23.Substring(0, 2);
                    string atedate23 = ddt + "-" + ddt23;

                    string atdate24 = dtExcelData.Rows[32][0].ToString();
                    string ddt24 = atdate24.Substring(0, 2);
                    string atedate24 = ddt + "-" + ddt24;

                    string atdate25 = dtExcelData.Rows[33][0].ToString();
                    string ddt25 = atdate25.Substring(0, 2);
                    string atedate25 = ddt + "-" + ddt25;

                    string atdate26 = dtExcelData.Rows[34][0].ToString();
                    string ddt26 = atdate26.Substring(0, 2);
                    string atedate26 = ddt + "-" + ddt26;

                    string atdate27 = dtExcelData.Rows[35][0].ToString();
                    string ddt27 = atdate27.Substring(0, 2);
                    string atedate27 = ddt + "-" + ddt27;

                    string atdate28 = dtExcelData.Rows[36][0].ToString();
                    string ddt28 = atdate28.Substring(0, 2);
                    string atedate28 = ddt + "-" + ddt28;
                    string atdate31 = string.Empty, ddt31 = string.Empty, atedate31 = string.Empty, atdate29 = string.Empty, ddt29 = string.Empty, atedate29 = string.Empty, atdate30 = string.Empty, ddt30 = string.Empty, atedate30 = string.Empty;
                    if (dtExcelData.Rows.Count > 37)
                    {
                        atdate29 = dtExcelData.Rows[37][0].ToString();
                        ddt29 = atdate29.Substring(0, 2);
                        atedate29 = ddt + "-" + ddt29;

                        atdate30 = dtExcelData.Rows[38][0].ToString();
                        ddt30 = atdate30.Substring(0, 2);
                        atedate30 = ddt + "-" + ddt30;


                        if (dtExcelData.Rows.Count > 39)
                        {
                            atdate31 = dtExcelData.Rows[39][0].ToString();
                            ddt31 = atdate31.Substring(0, 2);
                            atedate31 = ddt + "-" + ddt31;
                        }
                    }

                    //second date
                    string satdate = dtExcelData.Rows[1][17].ToString();
                    string sddt = atdate.Substring(0, 7);

                    string satdate1 = dtExcelData.Rows[9][16].ToString();
                    string sddt1 = satdate1.Substring(0, 2);
                    string satedate1 = sddt + "-" + sddt1;

                    string satdate2 = dtExcelData.Rows[10][16].ToString();
                    string sddt2 = satdate2.Substring(0, 2);
                    string satedate2 = sddt + "-" + sddt2;

                    string satdate3 = dtExcelData.Rows[11][16].ToString();
                    string sddt3 = satdate3.Substring(0, 2);
                    string satedate3 = sddt + "-" + sddt3;

                    string satdate4 = dtExcelData.Rows[12][16].ToString();
                    string sddt4 = satdate4.Substring(0, 2);
                    string satedate4 = sddt + "-" + sddt4;

                    string satdate5 = dtExcelData.Rows[13][16].ToString();
                    string sddt5 = satdate5.Substring(0, 2);
                    string satedate5 = sddt + "-" + sddt5;

                    string satdate6 = dtExcelData.Rows[14][16].ToString();
                    string sddt6 = satdate6.Substring(0, 2);
                    string satedate6 = sddt + "-" + sddt6;

                    string satdate7 = dtExcelData.Rows[15][16].ToString();
                    string sddt7 = satdate7.Substring(0, 2);
                    string satedate7 = sddt + "-" + sddt7;

                    string satdate8 = dtExcelData.Rows[16][16].ToString();
                    string sddt8 = satdate8.Substring(0, 2);
                    string satedate8 = sddt + "-" + sddt8;

                    string satdate9 = dtExcelData.Rows[17][16].ToString();
                    string sddt9 = satdate9.Substring(0, 2);
                    string satedate9 = sddt + "-" + sddt9;

                    string satdate10 = dtExcelData.Rows[18][16].ToString();
                    string sddt10 = satdate10.Substring(0, 2);
                    string satedate10 = sddt + "-" + sddt10;

                    string satdate11 = dtExcelData.Rows[19][16].ToString();
                    string sddt11 = satdate11.Substring(0, 2);
                    string satedate11 = sddt + "-" + sddt11;

                    string satdate12 = dtExcelData.Rows[20][16].ToString();
                    string sddt12 = satdate12.Substring(0, 2);
                    string satedate12 = sddt + "-" + sddt12;

                    string satdate13 = dtExcelData.Rows[21][16].ToString();
                    string sddt13 = satdate13.Substring(0, 2);
                    string satedate13 = sddt + "-" + sddt13;

                    string satdate14 = dtExcelData.Rows[22][16].ToString();
                    string sddt14 = satdate14.Substring(0, 2);
                    string satedate14 = sddt + "-" + sddt14;

                    string satdate15 = dtExcelData.Rows[23][16].ToString();
                    string sddt15 = satdate15.Substring(0, 2);
                    string satedate15 = sddt + "-" + sddt15;

                    string satdate16 = dtExcelData.Rows[24][16].ToString();
                    string sddt16 = satdate16.Substring(0, 2);
                    string satedate16 = sddt + "-" + sddt16;


                    string satdate17 = dtExcelData.Rows[25][16].ToString();
                    string sddt17 = satdate17.Substring(0, 2);
                    string satedate17 = sddt + "-" + sddt17;

                    string satdate18 = dtExcelData.Rows[26][16].ToString();
                    string sddt18 = satdate18.Substring(0, 2);
                    string satedate18 = sddt + "-" + sddt18;

                    string satdate19 = dtExcelData.Rows[27][16].ToString();
                    string sddt19 = satdate19.Substring(0, 2);
                    string satedate19 = sddt + "-" + sddt19;

                    string satdate20 = dtExcelData.Rows[28][16].ToString();
                    string sddt20 = satdate20.Substring(0, 2);
                    string satedate20 = sddt + "-" + sddt20;

                    string satdate21 = dtExcelData.Rows[29][16].ToString();
                    string sddt21 = satdate21.Substring(0, 2);
                    string satedate21 = sddt + "-" + sddt21;

                    string satdate22 = dtExcelData.Rows[30][16].ToString();
                    string sddt22 = satdate22.Substring(0, 2);
                    string satedate22 = sddt + "-" + sddt22;

                    string satdate23 = dtExcelData.Rows[31][16].ToString();
                    string sddt23 = satdate23.Substring(0, 2);
                    string satedate23 = sddt + "-" + sddt23;

                    string satdate24 = dtExcelData.Rows[32][16].ToString();
                    string sddt24 = satdate24.Substring(0, 2);
                    string satedate24 = sddt + "-" + sddt24;

                    string satdate25 = dtExcelData.Rows[33][16].ToString();
                    string sddt25 = satdate25.Substring(0, 2);
                    string satedate25 = sddt + "-" + sddt25;

                    string satdate26 = dtExcelData.Rows[34][16].ToString();
                    string sddt26 = satdate26.Substring(0, 2);
                    string satedate26 = sddt + "-" + sddt26;

                    string satdate27 = dtExcelData.Rows[35][16].ToString();
                    string sddt27 = satdate27.Substring(0, 2);
                    string satedate27 = sddt + "-" + sddt27;

                    string satdate28 = dtExcelData.Rows[36][16].ToString();
                    string sddt28 = satdate28.Substring(0, 2);
                    string satedate28 = sddt + "-" + sddt28;
                    string satdate31 = string.Empty, sddt31 = string.Empty, satedate31 = string.Empty, satdate29 = string.Empty, sddt29 = string.Empty, satedate29 = string.Empty, satdate30 = string.Empty, sddt30 = string.Empty, satedate30 = string.Empty;
                    if (dtExcelData.Rows.Count > 37)
                    {
                        satdate29 = dtExcelData.Rows[37][16].ToString();
                        sddt29 = satdate29.Substring(0, 2);
                        satedate29 = sddt + "-" + sddt29;

                        satdate30 = dtExcelData.Rows[38][16].ToString();
                        sddt30 = satdate30.Substring(0, 2);
                        satedate30 = sddt + "-" + sddt30;

                        if (dtExcelData.Rows.Count > 39)
                        {
                            satdate31 = dtExcelData.Rows[39][16].ToString();
                            sddt31 = satdate31.Substring(0, 2);
                            satedate31 = sddt + "-" + sddt31;
                        }
                    }
                    // third date
                    string tatdate = dtExcelData.Rows[1][33].ToString();
                    string tddt = tatdate.Substring(0, 7);

                    string tatdate1 = dtExcelData.Rows[9][16].ToString();
                    string tddt1 = tatdate1.Substring(0, 2);
                    string tatedate1 = tddt + "-" + tddt1;

                    string tatdate2 = dtExcelData.Rows[10][16].ToString();
                    string tddt2 = tatdate2.Substring(0, 2);
                    string tatedate2 = tddt + "-" + tddt2;

                    string tatdate3 = dtExcelData.Rows[11][16].ToString();
                    string tddt3 = tatdate3.Substring(0, 2);
                    string tatedate3 = tddt + "-" + tddt3;

                    string tatdate4 = dtExcelData.Rows[12][16].ToString();
                    string tddt4 = tatdate4.Substring(0, 2);
                    string tatedate4 = tddt + "-" + tddt4;

                    string tatdate5 = dtExcelData.Rows[13][16].ToString();
                    string tddt5 = tatdate5.Substring(0, 2);
                    string tatedate5 = tddt + "-" + tddt5;

                    string tatdate6 = dtExcelData.Rows[14][16].ToString();
                    string tddt6 = tatdate6.Substring(0, 2);
                    string tatedate6 = tddt + "-" + tddt6;

                    string tatdate7 = dtExcelData.Rows[15][16].ToString();
                    string tddt7 = tatdate7.Substring(0, 2);
                    string tatedate7 = tddt + "-" + tddt7;

                    string tatdate8 = dtExcelData.Rows[16][16].ToString();
                    string tddt8 = tatdate8.Substring(0, 2);
                    string tatedate8 = tddt + "-" + tddt8;

                    string tatdate9 = dtExcelData.Rows[17][16].ToString();
                    string tddt9 = tatdate9.Substring(0, 2);
                    string tatedate9 = tddt + "-" + tddt9;

                    string tatdate10 = dtExcelData.Rows[18][16].ToString();
                    string tddt10 = tatdate10.Substring(0, 2);
                    string tatedate10 = tddt + "-" + tddt10;

                    string tatdate11 = dtExcelData.Rows[19][16].ToString();
                    string tddt11 = tatdate11.Substring(0, 2);
                    string tatedate11 = tddt + "-" + tddt11;

                    string tatdate12 = dtExcelData.Rows[20][16].ToString();
                    string tddt12 = tatdate12.Substring(0, 2);
                    string tatedate12 = tddt + "-" + tddt12;

                    string tatdate13 = dtExcelData.Rows[21][16].ToString();
                    string tddt13 = tatdate13.Substring(0, 2);
                    string tatedate13 = tddt + "-" + tddt13;

                    string tatdate14 = dtExcelData.Rows[22][16].ToString();
                    string tddt14 = tatdate14.Substring(0, 2);
                    string tatedate14 = tddt + "-" + tddt14;

                    string tatdate15 = dtExcelData.Rows[23][16].ToString();
                    string tddt15 = tatdate15.Substring(0, 2);
                    string tatedate15 = tddt + "-" + tddt15;

                    string tatdate16 = dtExcelData.Rows[24][16].ToString();
                    string tddt16 = tatdate16.Substring(0, 2);
                    string tatedate16 = tddt + "-" + tddt16;


                    string tatdate17 = dtExcelData.Rows[25][16].ToString();
                    string tddt17 = tatdate17.Substring(0, 2);
                    string tatedate17 = tddt + "-" + tddt17;

                    string tatdate18 = dtExcelData.Rows[26][16].ToString();
                    string tddt18 = tatdate18.Substring(0, 2);
                    string tatedate18 = tddt + "-" + tddt18;

                    string tatdate19 = dtExcelData.Rows[27][16].ToString();
                    string tddt19 = tatdate19.Substring(0, 2);
                    string tatedate19 = tddt + "-" + tddt19;

                    string tatdate20 = dtExcelData.Rows[28][16].ToString();
                    string tddt20 = tatdate20.Substring(0, 2);
                    string tatedate20 = tddt + "-" + tddt20;

                    string tatdate21 = dtExcelData.Rows[29][16].ToString();
                    string tddt21 = tatdate21.Substring(0, 2);
                    string tatedate21 = tddt + "-" + tddt21;

                    string tatdate22 = dtExcelData.Rows[30][16].ToString();
                    string tddt22 = tatdate22.Substring(0, 2);
                    string tatedate22 = tddt + "-" + tddt22;

                    string tatdate23 = dtExcelData.Rows[31][16].ToString();
                    string tddt23 = tatdate23.Substring(0, 2);
                    string tatedate23 = tddt + "-" + tddt23;

                    string tatdate24 = dtExcelData.Rows[32][16].ToString();
                    string tddt24 = tatdate24.Substring(0, 2);
                    string tatedate24 = tddt + "-" + tddt24;

                    string tatdate25 = dtExcelData.Rows[33][16].ToString();
                    string tddt25 = tatdate25.Substring(0, 2);
                    string tatedate25 = tddt + "-" + tddt25;

                    string tatdate26 = dtExcelData.Rows[34][16].ToString();
                    string tddt26 = tatdate26.Substring(0, 2);
                    string tatedate26 = tddt + "-" + tddt26;

                    string tatdate27 = dtExcelData.Rows[35][16].ToString();
                    string tddt27 = tatdate27.Substring(0, 2);
                    string tatedate27 = tddt + "-" + tddt27;

                    string tatdate28 = dtExcelData.Rows[36][16].ToString();
                    string tddt28 = tatdate28.Substring(0, 2);
                    string tatedate28 = tddt + "-" + tddt28;
                    string tatdate31 = string.Empty, tddt31 = string.Empty, tatedate31 = string.Empty, tatdate29 = string.Empty, tddt29 = string.Empty, tatedate29 = string.Empty, tatdate30 = string.Empty, tddt30 = string.Empty, tatedate30 = string.Empty;
                    if (dtExcelData.Rows.Count > 37)
                    {
                        tatdate29 = dtExcelData.Rows[37][16].ToString();
                        tddt29 = tatdate29.Substring(0, 2);
                        tatedate29 = tddt + "-" + tddt29;

                        tatdate30 = dtExcelData.Rows[38][16].ToString();
                        tddt30 = tatdate30.Substring(0, 2);
                        tatedate30 = tddt + "-" + tddt30;


                        if (dtExcelData.Rows.Count > 39)
                        {
                            tatdate31 = dtExcelData.Rows[39][16].ToString();
                            tddt31 = tatdate31.Substring(0, 2);
                            tatedate31 = tddt + "-" + tddt31;
                        }
                    }


                    if (dtExcelData.Rows[9][8].ToString() != "" || dtExcelData.Rows[9][24].ToString() != "" || dtExcelData.Rows[9][40].ToString() != "")
                    {
                        tout1 = dtExcelData.Rows[9][8].ToString();
                        stout1 = dtExcelData.Rows[9][24].ToString();
                        ttout1 = dtExcelData.Rows[9][40].ToString();
                    }
                    else if (dtExcelData.Rows[9][11].ToString() != "" || dtExcelData.Rows[9][27].ToString() != "" || dtExcelData.Rows[9][43].ToString() != "")
                    {
                        tout1 = dtExcelData.Rows[9][11].ToString();
                        stout1 = dtExcelData.Rows[9][27].ToString();
                        ttout1 = dtExcelData.Rows[9][43].ToString();
                    }
                    else if (dtExcelData.Rows[9][13].ToString() != "" || dtExcelData.Rows[9][29].ToString() != "" || dtExcelData.Rows[9][45].ToString() != "")
                    {
                        tout1 = dtExcelData.Rows[9][13].ToString();
                        stout1 = dtExcelData.Rows[9][29].ToString();
                        ttout1 = dtExcelData.Rows[9][45].ToString();
                    }


                    if (dtExcelData.Rows[10][8].ToString() != "" || dtExcelData.Rows[10][24].ToString() != "" || dtExcelData.Rows[10][40].ToString() != "")
                    {
                        tout2 = dtExcelData.Rows[10][8].ToString();
                        stout2 = dtExcelData.Rows[10][24].ToString();
                        ttout2 = dtExcelData.Rows[10][40].ToString();
                    }
                    else if (dtExcelData.Rows[10][11].ToString() != "" || dtExcelData.Rows[10][27].ToString() != "" || dtExcelData.Rows[10][43].ToString() != "")
                    {
                        tout2 = dtExcelData.Rows[10][11].ToString();
                        stout2 = dtExcelData.Rows[10][27].ToString();
                        ttout2 = dtExcelData.Rows[10][43].ToString();
                    }
                    else if (dtExcelData.Rows[10][13].ToString() != "" || dtExcelData.Rows[10][29].ToString() != "" || dtExcelData.Rows[10][45].ToString() != "")
                    {
                        tout2 = dtExcelData.Rows[10][13].ToString();
                        stout2 = dtExcelData.Rows[10][29].ToString();
                        ttout2 = dtExcelData.Rows[10][45].ToString();
                    }


                    if (dtExcelData.Rows[11][8].ToString() != "" || dtExcelData.Rows[11][24].ToString() != "" || dtExcelData.Rows[11][40].ToString() != "")
                    {
                        tout3 = dtExcelData.Rows[11][8].ToString();
                        stout3 = dtExcelData.Rows[11][24].ToString();
                        ttout3 = dtExcelData.Rows[11][40].ToString();
                    }
                    else if (dtExcelData.Rows[11][11].ToString() != "" || dtExcelData.Rows[11][27].ToString() != "" || dtExcelData.Rows[11][43].ToString() != "")
                    {
                        tout3 = dtExcelData.Rows[11][11].ToString();
                        stout3 = dtExcelData.Rows[11][27].ToString();
                        ttout3 = dtExcelData.Rows[11][43].ToString();
                    }
                    else if (dtExcelData.Rows[11][13].ToString() != "" || dtExcelData.Rows[11][29].ToString() != "" || dtExcelData.Rows[11][45].ToString() != "")
                    {
                        tout3 = dtExcelData.Rows[11][13].ToString();
                        stout3 = dtExcelData.Rows[11][29].ToString();
                        ttout3 = dtExcelData.Rows[11][45].ToString();
                    }


                    if (dtExcelData.Rows[12][8].ToString() != "" || dtExcelData.Rows[12][24].ToString() != "" || dtExcelData.Rows[12][40].ToString() != "")
                    {
                        tout4 = dtExcelData.Rows[12][8].ToString();
                        stout4 = dtExcelData.Rows[12][24].ToString();
                        ttout4 = dtExcelData.Rows[12][40].ToString();
                    }
                    else if (dtExcelData.Rows[12][11].ToString() != "" || dtExcelData.Rows[12][27].ToString() != "" || dtExcelData.Rows[12][43].ToString() != "")
                    {
                        tout4 = dtExcelData.Rows[12][11].ToString();
                        stout4 = dtExcelData.Rows[12][27].ToString();
                        ttout4 = dtExcelData.Rows[12][43].ToString();
                    }
                    else if (dtExcelData.Rows[12][13].ToString() != "" || dtExcelData.Rows[12][29].ToString() != "" || dtExcelData.Rows[12][45].ToString() != "")
                    {
                        tout4 = dtExcelData.Rows[12][13].ToString();
                        stout4 = dtExcelData.Rows[12][29].ToString();
                        ttout4 = dtExcelData.Rows[12][45].ToString();
                    }


                    if (dtExcelData.Rows[13][8].ToString() != "" || dtExcelData.Rows[13][24].ToString() != "" || dtExcelData.Rows[13][40].ToString() != "")
                    {
                        tout5 = dtExcelData.Rows[13][8].ToString();
                        stout5 = dtExcelData.Rows[13][24].ToString();
                        ttout5 = dtExcelData.Rows[13][40].ToString();
                    }
                    else if (dtExcelData.Rows[13][11].ToString() != "" || dtExcelData.Rows[13][27].ToString() != "" || dtExcelData.Rows[13][43].ToString() != "")
                    {
                        tout5 = dtExcelData.Rows[13][11].ToString();
                        stout5 = dtExcelData.Rows[13][24].ToString();
                        ttout5 = dtExcelData.Rows[13][43].ToString();
                    }
                    else if (dtExcelData.Rows[13][13].ToString() != "" || dtExcelData.Rows[13][29].ToString() != "" || dtExcelData.Rows[13][45].ToString() != "")
                    {
                        tout5 = dtExcelData.Rows[13][13].ToString();
                        stout5 = dtExcelData.Rows[13][29].ToString();
                        ttout5 = dtExcelData.Rows[13][45].ToString();
                    }


                    if (dtExcelData.Rows[14][8].ToString() != "" || dtExcelData.Rows[14][24].ToString() != "" || dtExcelData.Rows[14][40].ToString() != "")
                    {
                        tout6 = dtExcelData.Rows[14][8].ToString();
                        stout6 = dtExcelData.Rows[14][24].ToString();
                        ttout6 = dtExcelData.Rows[14][40].ToString();
                    }
                    else if (dtExcelData.Rows[14][11].ToString() != "" || dtExcelData.Rows[14][27].ToString() != "" || dtExcelData.Rows[14][43].ToString() != "")
                    {
                        tout6 = dtExcelData.Rows[14][11].ToString();
                        stout6 = dtExcelData.Rows[14][27].ToString();
                        ttout6 = dtExcelData.Rows[14][43].ToString();
                    }
                    else if (dtExcelData.Rows[14][13].ToString() != "" || dtExcelData.Rows[14][29].ToString() != "" || dtExcelData.Rows[14][45].ToString() != "")
                    {
                        tout6 = dtExcelData.Rows[14][13].ToString();
                        stout6 = dtExcelData.Rows[14][29].ToString();
                        ttout6 = dtExcelData.Rows[14][45].ToString();
                    }


                    if (dtExcelData.Rows[15][8].ToString() != "" || dtExcelData.Rows[15][24].ToString() != "" || dtExcelData.Rows[15][40].ToString() != "")
                    {
                        tout7 = dtExcelData.Rows[15][8].ToString();
                        stout7 = dtExcelData.Rows[15][24].ToString();
                        ttout7 = dtExcelData.Rows[15][40].ToString();
                    }
                    else if (dtExcelData.Rows[15][11].ToString() != "" || dtExcelData.Rows[15][27].ToString() != "" || dtExcelData.Rows[15][43].ToString() != "")
                    {
                        tout7 = dtExcelData.Rows[15][11].ToString();
                        stout7 = dtExcelData.Rows[15][27].ToString();
                        ttout7 = dtExcelData.Rows[15][43].ToString();
                    }
                    else if (dtExcelData.Rows[15][13].ToString() != "" || dtExcelData.Rows[15][29].ToString() != "" || dtExcelData.Rows[15][45].ToString() != "")
                    {
                        tout7 = dtExcelData.Rows[15][13].ToString();
                        stout7 = dtExcelData.Rows[15][29].ToString();
                        ttout7 = dtExcelData.Rows[15][45].ToString();
                    }


                    if (dtExcelData.Rows[16][8].ToString() != "" || dtExcelData.Rows[16][24].ToString() != "" || dtExcelData.Rows[16][40].ToString() != "")
                    {
                        tout8 = dtExcelData.Rows[16][8].ToString();
                        stout8 = dtExcelData.Rows[16][24].ToString();
                        ttout8 = dtExcelData.Rows[16][40].ToString();
                    }
                    else if (dtExcelData.Rows[16][11].ToString() != "" || dtExcelData.Rows[16][27].ToString() != "" || dtExcelData.Rows[16][43].ToString() != "")
                    {
                        tout8 = dtExcelData.Rows[16][11].ToString();
                        stout8 = dtExcelData.Rows[16][27].ToString();
                        ttout8 = dtExcelData.Rows[16][43].ToString();
                    }
                    else if (dtExcelData.Rows[16][13].ToString() != "" || dtExcelData.Rows[16][29].ToString() != "" || dtExcelData.Rows[16][45].ToString() != "")
                    {
                        tout8 = dtExcelData.Rows[16][13].ToString();
                        stout8 = dtExcelData.Rows[16][29].ToString();
                        ttout8 = dtExcelData.Rows[16][45].ToString();
                    }


                    if (dtExcelData.Rows[17][8].ToString() != "" || dtExcelData.Rows[17][24].ToString() != "" || dtExcelData.Rows[17][40].ToString() != "")
                    {
                        tout9 = dtExcelData.Rows[17][8].ToString();
                        stout9 = dtExcelData.Rows[17][24].ToString();
                        ttout9 = dtExcelData.Rows[17][40].ToString();
                    }
                    else if (dtExcelData.Rows[17][11].ToString() != "" || dtExcelData.Rows[17][27].ToString() != "" || dtExcelData.Rows[17][43].ToString() != "")
                    {
                        tout9 = dtExcelData.Rows[17][11].ToString();
                        stout9 = dtExcelData.Rows[17][27].ToString();
                        ttout9 = dtExcelData.Rows[17][43].ToString();
                    }
                    else if (dtExcelData.Rows[17][13].ToString() != "" || dtExcelData.Rows[17][29].ToString() != "" || dtExcelData.Rows[17][45].ToString() != "")
                    {
                        tout9 = dtExcelData.Rows[17][13].ToString();
                        stout9 = dtExcelData.Rows[17][29].ToString();
                        ttout9 = dtExcelData.Rows[17][45].ToString();
                    }


                    if (dtExcelData.Rows[18][8].ToString() != "" || dtExcelData.Rows[18][24].ToString() != "" || dtExcelData.Rows[18][40].ToString() != "")
                    {
                        tout10 = dtExcelData.Rows[18][8].ToString();
                        stout10 = dtExcelData.Rows[18][24].ToString();
                        ttout10 = dtExcelData.Rows[18][40].ToString();
                    }
                    else if (dtExcelData.Rows[18][11].ToString() != "" || dtExcelData.Rows[18][27].ToString() != "" || dtExcelData.Rows[18][43].ToString() != "")
                    {
                        tout10 = dtExcelData.Rows[18][11].ToString();
                        stout10 = dtExcelData.Rows[18][27].ToString();
                        ttout10 = dtExcelData.Rows[18][43].ToString();
                    }
                    else if (dtExcelData.Rows[18][13].ToString() != "" || dtExcelData.Rows[18][29].ToString() != "" || dtExcelData.Rows[18][45].ToString() != "")
                    {
                        tout10 = dtExcelData.Rows[18][13].ToString();
                        stout10 = dtExcelData.Rows[18][29].ToString();
                        ttout10 = dtExcelData.Rows[18][45].ToString();
                    }


                    if (dtExcelData.Rows[19][8].ToString() != "" || dtExcelData.Rows[19][24].ToString() != "" || dtExcelData.Rows[19][40].ToString() != "")
                    {
                        tout11 = dtExcelData.Rows[19][8].ToString();
                        stout11 = dtExcelData.Rows[19][24].ToString();
                        ttout11 = dtExcelData.Rows[19][40].ToString();
                    }
                    else if (dtExcelData.Rows[19][11].ToString() != "" || dtExcelData.Rows[19][27].ToString() != "" || dtExcelData.Rows[19][43].ToString() != "")
                    {
                        tout11 = dtExcelData.Rows[19][11].ToString();
                        stout11 = dtExcelData.Rows[19][27].ToString();
                        ttout11 = dtExcelData.Rows[19][43].ToString();
                    }
                    else if (dtExcelData.Rows[19][13].ToString() != "" || dtExcelData.Rows[19][29].ToString() != "" || dtExcelData.Rows[19][45].ToString() != "")
                    {
                        tout11 = dtExcelData.Rows[19][13].ToString();
                        stout11 = dtExcelData.Rows[19][29].ToString();
                        ttout11 = dtExcelData.Rows[19][45].ToString();
                    }


                    if (dtExcelData.Rows[20][8].ToString() != "" || dtExcelData.Rows[20][24].ToString() != "" || dtExcelData.Rows[20][40].ToString() != "")
                    {
                        tout12 = dtExcelData.Rows[20][8].ToString();
                        stout12 = dtExcelData.Rows[20][24].ToString();
                        ttout12 = dtExcelData.Rows[20][40].ToString();
                    }
                    else if (dtExcelData.Rows[20][11].ToString() != "" || dtExcelData.Rows[20][27].ToString() != "" || dtExcelData.Rows[20][43].ToString() != "")
                    {
                        tout12 = dtExcelData.Rows[20][11].ToString();
                        stout12 = dtExcelData.Rows[20][27].ToString();
                        ttout12 = dtExcelData.Rows[20][43].ToString();
                    }
                    else if (dtExcelData.Rows[20][13].ToString() != "" || dtExcelData.Rows[20][29].ToString() != "" || dtExcelData.Rows[20][45].ToString() != "")
                    {
                        tout12 = dtExcelData.Rows[20][13].ToString();
                        stout12 = dtExcelData.Rows[20][29].ToString();
                        ttout12 = dtExcelData.Rows[20][45].ToString();
                    }


                    if (dtExcelData.Rows[21][8].ToString() != "" || dtExcelData.Rows[21][24].ToString() != "" || dtExcelData.Rows[21][40].ToString() != "")
                    {
                        tout13 = dtExcelData.Rows[21][8].ToString();
                        stout13 = dtExcelData.Rows[21][24].ToString();
                        ttout13 = dtExcelData.Rows[21][40].ToString();
                    }
                    else if (dtExcelData.Rows[21][11].ToString() != "" || dtExcelData.Rows[21][27].ToString() != "" || dtExcelData.Rows[21][43].ToString() != "")
                    {
                        tout13 = dtExcelData.Rows[21][11].ToString();
                        stout13 = dtExcelData.Rows[21][27].ToString();
                        ttout13 = dtExcelData.Rows[21][43].ToString();
                    }
                    else if (dtExcelData.Rows[21][13].ToString() != "" || dtExcelData.Rows[21][29].ToString() != "" || dtExcelData.Rows[21][45].ToString() != "")
                    {
                        tout13 = dtExcelData.Rows[21][13].ToString();
                        stout13 = dtExcelData.Rows[21][29].ToString();
                        ttout13 = dtExcelData.Rows[21][45].ToString();
                    }


                    if (dtExcelData.Rows[22][8].ToString() != "" || dtExcelData.Rows[22][24].ToString() != "" || dtExcelData.Rows[22][40].ToString() != "")
                    {
                        tout14 = dtExcelData.Rows[22][8].ToString();
                        stout14 = dtExcelData.Rows[22][24].ToString();
                        ttout14 = dtExcelData.Rows[22][40].ToString();
                    }
                    else if (dtExcelData.Rows[22][11].ToString() != "" || dtExcelData.Rows[22][27].ToString() != "" || dtExcelData.Rows[22][43].ToString() != "")
                    {
                        tout14 = dtExcelData.Rows[22][11].ToString();
                        stout14 = dtExcelData.Rows[22][27].ToString();
                        ttout14 = dtExcelData.Rows[22][43].ToString();
                    }
                    else if (dtExcelData.Rows[22][13].ToString() != "" || dtExcelData.Rows[22][29].ToString() != "" || dtExcelData.Rows[22][45].ToString() != "")
                    {
                        tout14 = dtExcelData.Rows[22][13].ToString();
                        stout14 = dtExcelData.Rows[22][29].ToString();
                        ttout14 = dtExcelData.Rows[22][45].ToString();
                    }


                    if (dtExcelData.Rows[23][8].ToString() != "" || dtExcelData.Rows[23][24].ToString() != "" || dtExcelData.Rows[23][40].ToString() != "")
                    {
                        tout15 = dtExcelData.Rows[23][8].ToString();
                        stout15 = dtExcelData.Rows[23][24].ToString();
                        ttout15 = dtExcelData.Rows[23][40].ToString();
                    }
                    else if (dtExcelData.Rows[23][11].ToString() != "" || dtExcelData.Rows[23][27].ToString() != "" || dtExcelData.Rows[23][43].ToString() != "")
                    {
                        tout15 = dtExcelData.Rows[23][11].ToString();
                        stout15 = dtExcelData.Rows[23][27].ToString();
                        ttout15 = dtExcelData.Rows[23][43].ToString();
                    }
                    else if (dtExcelData.Rows[23][13].ToString() != "" || dtExcelData.Rows[23][29].ToString() != "" || dtExcelData.Rows[23][45].ToString() != "")
                    {
                        tout15 = dtExcelData.Rows[23][13].ToString();
                        stout15 = dtExcelData.Rows[23][29].ToString();
                        ttout15 = dtExcelData.Rows[23][45].ToString();
                    }


                    if (dtExcelData.Rows[24][8].ToString() != "" || dtExcelData.Rows[24][24].ToString() != "" || dtExcelData.Rows[24][40].ToString() != "")
                    {
                        tout16 = dtExcelData.Rows[24][8].ToString();
                        stout16 = dtExcelData.Rows[24][24].ToString();
                        ttout16 = dtExcelData.Rows[24][40].ToString();
                    }
                    else if (dtExcelData.Rows[24][11].ToString() != "" || dtExcelData.Rows[24][27].ToString() != "" || dtExcelData.Rows[24][43].ToString() != "")
                    {
                        tout16 = dtExcelData.Rows[24][11].ToString();
                        stout16 = dtExcelData.Rows[24][27].ToString();
                        ttout16 = dtExcelData.Rows[24][43].ToString();
                    }
                    else if (dtExcelData.Rows[24][13].ToString() != "" || dtExcelData.Rows[24][29].ToString() != "" || dtExcelData.Rows[24][45].ToString() != "")
                    {
                        tout16 = dtExcelData.Rows[24][13].ToString();
                        stout16 = dtExcelData.Rows[24][29].ToString();
                        ttout16 = dtExcelData.Rows[24][45].ToString();
                    }


                    if (dtExcelData.Rows[25][8].ToString() != "" || dtExcelData.Rows[25][24].ToString() != "" || dtExcelData.Rows[25][40].ToString() != "")
                    {
                        tout17 = dtExcelData.Rows[25][8].ToString();
                        stout17 = dtExcelData.Rows[25][24].ToString();
                        ttout17 = dtExcelData.Rows[25][40].ToString();
                    }
                    else if (dtExcelData.Rows[25][11].ToString() != "" || dtExcelData.Rows[25][27].ToString() != "" || dtExcelData.Rows[25][43].ToString() != "")
                    {
                        tout17 = dtExcelData.Rows[25][11].ToString();
                        stout17 = dtExcelData.Rows[25][27].ToString();
                        ttout17 = dtExcelData.Rows[25][43].ToString();
                    }
                    else if (dtExcelData.Rows[25][13].ToString() != "" || dtExcelData.Rows[25][29].ToString() != "" || dtExcelData.Rows[25][45].ToString() != "")
                    {
                        tout17 = dtExcelData.Rows[25][13].ToString();
                        stout17 = dtExcelData.Rows[25][29].ToString();
                        ttout17 = dtExcelData.Rows[25][45].ToString();
                    }


                    if (dtExcelData.Rows[26][8].ToString() != "" || dtExcelData.Rows[26][24].ToString() != "" || dtExcelData.Rows[26][40].ToString() != "")
                    {
                        tout18 = dtExcelData.Rows[26][8].ToString();
                        stout18 = dtExcelData.Rows[26][24].ToString();
                        ttout18 = dtExcelData.Rows[26][40].ToString();
                    }
                    else if (dtExcelData.Rows[26][11].ToString() != "" || dtExcelData.Rows[26][27].ToString() != "" || dtExcelData.Rows[26][43].ToString() != "")
                    {
                        tout18 = dtExcelData.Rows[26][11].ToString();
                        stout18 = dtExcelData.Rows[26][27].ToString();
                        ttout18 = dtExcelData.Rows[26][43].ToString();
                    }
                    else if (dtExcelData.Rows[26][13].ToString() != "" || dtExcelData.Rows[26][29].ToString() != "" || dtExcelData.Rows[26][45].ToString() != "")
                    {
                        tout18 = dtExcelData.Rows[26][13].ToString();
                        stout18 = dtExcelData.Rows[26][29].ToString();
                        ttout18 = dtExcelData.Rows[26][45].ToString();
                    }


                    if (dtExcelData.Rows[27][8].ToString() != "" || dtExcelData.Rows[27][24].ToString() != "" || dtExcelData.Rows[27][40].ToString() != "")
                    {
                        tout19 = dtExcelData.Rows[27][8].ToString();
                        stout19 = dtExcelData.Rows[27][24].ToString();
                        ttout19 = dtExcelData.Rows[27][40].ToString();
                    }
                    else if (dtExcelData.Rows[27][11].ToString() != "" || dtExcelData.Rows[27][27].ToString() != "" || dtExcelData.Rows[27][43].ToString() != "")
                    {
                        tout19 = dtExcelData.Rows[27][11].ToString();
                        stout19 = dtExcelData.Rows[27][27].ToString();
                        ttout19 = dtExcelData.Rows[27][43].ToString();
                    }
                    else if (dtExcelData.Rows[27][13].ToString() != "" || dtExcelData.Rows[27][29].ToString() != "" || dtExcelData.Rows[27][45].ToString() != "")
                    {
                        tout19 = dtExcelData.Rows[27][13].ToString();
                        stout19 = dtExcelData.Rows[27][29].ToString();
                        ttout19 = dtExcelData.Rows[27][45].ToString();
                    }


                    if (dtExcelData.Rows[28][8].ToString() != "" || dtExcelData.Rows[28][24].ToString() != "" || dtExcelData.Rows[28][40].ToString() != "")
                    {
                        tout20 = dtExcelData.Rows[28][8].ToString();
                        stout20 = dtExcelData.Rows[28][24].ToString();
                        ttout20 = dtExcelData.Rows[28][40].ToString();
                    }
                    else if (dtExcelData.Rows[28][11].ToString() != "" || dtExcelData.Rows[28][27].ToString() != "" || dtExcelData.Rows[28][43].ToString() != "")
                    {
                        tout20 = dtExcelData.Rows[28][11].ToString();
                        stout20 = dtExcelData.Rows[28][27].ToString();
                        ttout20 = dtExcelData.Rows[28][43].ToString();
                    }
                    else if (dtExcelData.Rows[28][13].ToString() != "" || dtExcelData.Rows[28][29].ToString() != "" || dtExcelData.Rows[28][45].ToString() != "")
                    {
                        tout20 = dtExcelData.Rows[28][13].ToString();
                        stout20 = dtExcelData.Rows[28][29].ToString();
                        ttout20 = dtExcelData.Rows[28][45].ToString();
                    }


                    if (dtExcelData.Rows[29][8].ToString() != "" || dtExcelData.Rows[29][24].ToString() != "" || dtExcelData.Rows[29][40].ToString() != "")
                    {
                        tout21 = dtExcelData.Rows[29][8].ToString();
                        stout21 = dtExcelData.Rows[29][24].ToString();
                        ttout21 = dtExcelData.Rows[29][40].ToString();
                    }
                    else if (dtExcelData.Rows[29][11].ToString() != "" || dtExcelData.Rows[29][27].ToString() != "" || dtExcelData.Rows[29][43].ToString() != "")
                    {
                        tout21 = dtExcelData.Rows[29][11].ToString();
                        stout21 = dtExcelData.Rows[29][27].ToString();
                        ttout21 = dtExcelData.Rows[29][43].ToString();
                    }
                    else if (dtExcelData.Rows[29][13].ToString() != "" || dtExcelData.Rows[29][29].ToString() != "" || dtExcelData.Rows[29][45].ToString() != "")
                    {
                        tout21 = dtExcelData.Rows[29][13].ToString();
                        stout21 = dtExcelData.Rows[29][29].ToString();
                        ttout21 = dtExcelData.Rows[29][45].ToString();
                    }


                    if (dtExcelData.Rows[30][8].ToString() != "" || dtExcelData.Rows[30][24].ToString() != "" || dtExcelData.Rows[30][40].ToString() != "")
                    {
                        tout22 = dtExcelData.Rows[30][8].ToString();
                        stout22 = dtExcelData.Rows[30][24].ToString();
                        ttout22 = dtExcelData.Rows[30][40].ToString();
                    }
                    else if (dtExcelData.Rows[30][11].ToString() != "" || dtExcelData.Rows[30][27].ToString() != "" || dtExcelData.Rows[30][43].ToString() != "")
                    {
                        tout22 = dtExcelData.Rows[30][11].ToString();
                        stout22 = dtExcelData.Rows[30][27].ToString();
                        ttout22 = dtExcelData.Rows[30][43].ToString();
                    }
                    else if (dtExcelData.Rows[30][13].ToString() != "" || dtExcelData.Rows[30][29].ToString() != "" || dtExcelData.Rows[30][45].ToString() != "")
                    {
                        tout22 = dtExcelData.Rows[30][13].ToString();
                        stout22 = dtExcelData.Rows[30][29].ToString();
                        ttout22 = dtExcelData.Rows[30][45].ToString();
                    }


                    if (dtExcelData.Rows[31][8].ToString() != "" || dtExcelData.Rows[31][24].ToString() != "" || dtExcelData.Rows[31][40].ToString() != "")
                    {
                        tout23 = dtExcelData.Rows[31][8].ToString();
                        stout23 = dtExcelData.Rows[17][24].ToString();
                        ttout23 = dtExcelData.Rows[31][40].ToString();
                    }
                    else if (dtExcelData.Rows[31][11].ToString() != "" || dtExcelData.Rows[31][27].ToString() != "" || dtExcelData.Rows[31][43].ToString() != "")
                    {
                        tout23 = dtExcelData.Rows[31][11].ToString();
                        stout23 = dtExcelData.Rows[31][27].ToString();
                        ttout23 = dtExcelData.Rows[31][43].ToString();
                    }
                    else if (dtExcelData.Rows[31][13].ToString() != "" || dtExcelData.Rows[31][29].ToString() != "" || dtExcelData.Rows[31][45].ToString() != "")
                    {
                        tout23 = dtExcelData.Rows[31][13].ToString();
                        stout23 = dtExcelData.Rows[31][29].ToString();
                        ttout23 = dtExcelData.Rows[31][45].ToString();
                    }


                    if (dtExcelData.Rows[32][8].ToString() != "" || dtExcelData.Rows[32][24].ToString() != "" || dtExcelData.Rows[32][40].ToString() != "")
                    {
                        tout24 = dtExcelData.Rows[32][8].ToString();
                        stout24 = dtExcelData.Rows[32][24].ToString();
                        ttout24 = dtExcelData.Rows[32][40].ToString();
                    }
                    else if (dtExcelData.Rows[32][11].ToString() != "" || dtExcelData.Rows[32][27].ToString() != "" || dtExcelData.Rows[32][43].ToString() != "")
                    {
                        tout24 = dtExcelData.Rows[32][11].ToString();
                        stout24 = dtExcelData.Rows[32][27].ToString();
                        ttout24 = dtExcelData.Rows[32][43].ToString();
                    }
                    else if (dtExcelData.Rows[32][13].ToString() != "" || dtExcelData.Rows[32][29].ToString() != "" || dtExcelData.Rows[32][45].ToString() != "")
                    {
                        tout24 = dtExcelData.Rows[32][13].ToString();
                        stout24 = dtExcelData.Rows[32][29].ToString();
                        ttout24 = dtExcelData.Rows[25][45].ToString();
                    }

                    if (dtExcelData.Rows[33][8].ToString() != "" || dtExcelData.Rows[33][24].ToString() != "" || dtExcelData.Rows[33][40].ToString() != "")
                    {
                        tout25 = dtExcelData.Rows[33][8].ToString();
                        stout25 = dtExcelData.Rows[33][24].ToString();
                        ttout25 = dtExcelData.Rows[33][40].ToString();
                    }
                    else if (dtExcelData.Rows[33][11].ToString() != "" || dtExcelData.Rows[33][27].ToString() != "" || dtExcelData.Rows[33][43].ToString() != "")
                    {
                        tout25 = dtExcelData.Rows[33][11].ToString();
                        stout25 = dtExcelData.Rows[33][27].ToString();
                        ttout25 = dtExcelData.Rows[33][43].ToString();
                    }
                    else if (dtExcelData.Rows[33][13].ToString() != "" || dtExcelData.Rows[33][29].ToString() != "" || dtExcelData.Rows[33][45].ToString() != "")
                    {
                        tout25 = dtExcelData.Rows[33][13].ToString();
                        stout25 = dtExcelData.Rows[33][29].ToString();
                        ttout25 = dtExcelData.Rows[33][45].ToString();
                    }

                    if (dtExcelData.Rows[34][8].ToString() != "" || dtExcelData.Rows[34][24].ToString() != "" || dtExcelData.Rows[34][40].ToString() != "")
                    {
                        tout26 = dtExcelData.Rows[34][8].ToString();
                        stout26 = dtExcelData.Rows[34][24].ToString();
                        ttout26 = dtExcelData.Rows[34][40].ToString();
                    }
                    else if (dtExcelData.Rows[34][11].ToString() != "" || dtExcelData.Rows[34][27].ToString() != "" || dtExcelData.Rows[34][43].ToString() != "")
                    {
                        tout26 = dtExcelData.Rows[34][11].ToString();
                        stout26 = dtExcelData.Rows[34][27].ToString();
                        ttout26 = dtExcelData.Rows[34][43].ToString();
                    }
                    else if (dtExcelData.Rows[34][13].ToString() != "" || dtExcelData.Rows[34][29].ToString() != "" || dtExcelData.Rows[34][45].ToString() != "")
                    {
                        tout26 = dtExcelData.Rows[34][13].ToString();
                        stout26 = dtExcelData.Rows[34][29].ToString();
                        ttout26 = dtExcelData.Rows[34][45].ToString();
                    }

                    if (dtExcelData.Rows[35][8].ToString() != "" || dtExcelData.Rows[35][24].ToString() != "" || dtExcelData.Rows[35][40].ToString() != "")
                    {
                        tout27 = dtExcelData.Rows[35][8].ToString();
                        stout27 = dtExcelData.Rows[35][24].ToString();
                        ttout27 = dtExcelData.Rows[35][40].ToString();
                    }
                    else if (dtExcelData.Rows[35][11].ToString() != "" || dtExcelData.Rows[35][27].ToString() != "" || dtExcelData.Rows[35][43].ToString() != "")
                    {
                        tout27 = dtExcelData.Rows[35][11].ToString();
                        stout27 = dtExcelData.Rows[35][27].ToString();
                        ttout27 = dtExcelData.Rows[35][43].ToString();
                    }
                    else if (dtExcelData.Rows[35][13].ToString() != "" || dtExcelData.Rows[35][29].ToString() != "" || dtExcelData.Rows[35][45].ToString() != "")
                    {
                        tout27 = dtExcelData.Rows[35][13].ToString();
                        stout27 = dtExcelData.Rows[35][29].ToString();
                        ttout27 = dtExcelData.Rows[35][45].ToString();
                    }

                    if (dtExcelData.Rows[36][8].ToString() != "" || dtExcelData.Rows[36][24].ToString() != "" || dtExcelData.Rows[36][40].ToString() != "")
                    {
                        tout28 = dtExcelData.Rows[36][8].ToString();
                        stout28 = dtExcelData.Rows[36][24].ToString();
                        ttout28 = dtExcelData.Rows[36][40].ToString();
                    }
                    else if (dtExcelData.Rows[36][11].ToString() != "" || dtExcelData.Rows[36][27].ToString() != "" || dtExcelData.Rows[36][43].ToString() != "")
                    {
                        tout28 = dtExcelData.Rows[36][11].ToString();
                        stout28 = dtExcelData.Rows[36][27].ToString();
                        ttout28 = dtExcelData.Rows[36][43].ToString();
                    }
                    else if (dtExcelData.Rows[36][13].ToString() != "" || dtExcelData.Rows[36][29].ToString() != "" || dtExcelData.Rows[36][45].ToString() != "")
                    {
                        tout28 = dtExcelData.Rows[36][13].ToString();
                        stout28 = dtExcelData.Rows[36][29].ToString();
                        ttout28 = dtExcelData.Rows[36][45].ToString();
                    }
                    if (dtExcelData.Rows.Count > 37)
                    {
                        if (dtExcelData.Rows[37][8].ToString() != "" || dtExcelData.Rows[37][24].ToString() != "" || dtExcelData.Rows[37][40].ToString() != "")
                        {
                            tout29 = dtExcelData.Rows[37][8].ToString();
                            stout29 = dtExcelData.Rows[37][24].ToString();
                            ttout29 = dtExcelData.Rows[37][40].ToString();
                        }
                        else if (dtExcelData.Rows[37][11].ToString() != "" || dtExcelData.Rows[37][27].ToString() != "" || dtExcelData.Rows[37][43].ToString() != "")
                        {
                            tout29 = dtExcelData.Rows[37][11].ToString();
                            stout29 = dtExcelData.Rows[37][27].ToString();
                            ttout29 = dtExcelData.Rows[37][43].ToString();
                        }
                        else if (dtExcelData.Rows[37][13].ToString() != "" || dtExcelData.Rows[37][29].ToString() != "" || dtExcelData.Rows[37][45].ToString() != "")
                        {
                            tout29 = dtExcelData.Rows[37][13].ToString();
                            stout29 = dtExcelData.Rows[37][29].ToString();
                            ttout29 = dtExcelData.Rows[37][45].ToString();
                        }

                        if (dtExcelData.Rows[38][8].ToString() != "" || dtExcelData.Rows[38][24].ToString() != "" || dtExcelData.Rows[38][40].ToString() != "")
                        {
                            tout30 = dtExcelData.Rows[38][8].ToString();
                            stout30 = dtExcelData.Rows[38][24].ToString();
                            ttout30 = dtExcelData.Rows[38][40].ToString();
                        }
                        else if (dtExcelData.Rows[38][11].ToString() != "" || dtExcelData.Rows[38][27].ToString() != "" || dtExcelData.Rows[38][43].ToString() != "")
                        {
                            tout30 = dtExcelData.Rows[38][11].ToString();
                            stout30 = dtExcelData.Rows[38][27].ToString();
                            ttout30 = dtExcelData.Rows[38][43].ToString();
                        }
                        else if (dtExcelData.Rows[38][13].ToString() != "" || dtExcelData.Rows[38][29].ToString() != "" || dtExcelData.Rows[38][45].ToString() != "")
                        {
                            tout30 = dtExcelData.Rows[38][13].ToString();
                            stout30 = dtExcelData.Rows[38][29].ToString();
                            ttout30 = dtExcelData.Rows[38][45].ToString();
                        }
                        if (dtExcelData.Rows.Count > 39)
                        {
                            if (dtExcelData.Rows[39][8].ToString() != "" || dtExcelData.Rows[39][24].ToString() != "" || dtExcelData.Rows[39][40].ToString() != "")
                            {
                                tout31 = dtExcelData.Rows[39][8].ToString();
                                stout31 = dtExcelData.Rows[39][24].ToString();
                                ttout31 = dtExcelData.Rows[39][40].ToString();
                            }
                            else if (dtExcelData.Rows[39][11].ToString() != "" || dtExcelData.Rows[39][27].ToString() != "" || dtExcelData.Rows[39][43].ToString() != "")
                            {
                                tout31 = dtExcelData.Rows[39][11].ToString();
                                stout31 = dtExcelData.Rows[39][27].ToString();
                                ttout31 = dtExcelData.Rows[39][43].ToString();
                            }
                            else if (dtExcelData.Rows[39][13].ToString() != "" || dtExcelData.Rows[39][29].ToString() != "" || dtExcelData.Rows[39][45].ToString() != "")
                            {
                                tout31 = dtExcelData.Rows[39][13].ToString();
                                stout31 = dtExcelData.Rows[39][29].ToString();
                                ttout31 = dtExcelData.Rows[39][45].ToString();
                            }
                        }
                    }
                    //first
                    string ind1 = string.Empty;
                    if (dtExcelData.Rows[9][1].ToString() == "" && tout1 == "")
                    {
                        ind1 = "H";
                    }

                    string ind2 = string.Empty;
                    if (dtExcelData.Rows[10][1].ToString() == "" && tout2 == "")
                    {
                        ind2 = "H";
                    }

                    string ind3 = string.Empty;
                    if (dtExcelData.Rows[11][1].ToString() == "" && tout3 == "")
                    {
                        ind3 = "H";
                    }

                    string ind4 = string.Empty;
                    if (dtExcelData.Rows[12][1].ToString() == "" && tout4 == "")
                    {
                        ind4 = "H";
                    }

                    string ind5 = string.Empty;
                    if (dtExcelData.Rows[13][1].ToString() == "" && tout5 == "")
                    {
                        ind5 = "H";
                    }
                    string ind6 = string.Empty;
                    if (dtExcelData.Rows[14][1].ToString() == "" && tout6 == "")
                    {
                        ind6 = "H";
                    }
                    string ind7 = string.Empty;
                    if (dtExcelData.Rows[15][1].ToString() == "" && tout7 == "")
                    {
                        ind7 = "H";
                    }
                    string ind8 = string.Empty;
                    if (dtExcelData.Rows[16][1].ToString() == "" && tout8 == "")
                    {
                        ind8 = "H";
                    }

                    string ind9 = string.Empty;
                    if (dtExcelData.Rows[17][1].ToString() == "" && tout9 == "")
                    {
                        ind9 = "H";
                    }

                    string ind10 = string.Empty;
                    if (dtExcelData.Rows[18][1].ToString() == "" && tout10 == "")
                    {
                        ind10 = "H";
                    }

                    string ind11 = string.Empty;
                    if (dtExcelData.Rows[19][1].ToString() == "" && tout11 == "")
                    {
                        ind11 = "H";
                    }

                    string ind12 = string.Empty;
                    if (dtExcelData.Rows[20][1].ToString() == "" && tout12 == "")
                    {
                        ind12 = "H";
                    }

                    string ind13 = string.Empty;
                    if (dtExcelData.Rows[21][1].ToString() == "" && tout13 == "")
                    {
                        ind13 = "H";
                    }

                    string ind14 = string.Empty;
                    if (dtExcelData.Rows[22][1].ToString() == "" && tout14 == "")
                    {
                        ind14 = "H";
                    }

                    string ind15 = string.Empty;
                    if (dtExcelData.Rows[23][1].ToString() == "" && tout15 == "")
                    {
                        ind15 = "H";
                    }

                    string ind16 = string.Empty;
                    if (dtExcelData.Rows[24][1].ToString() == "" && tout16 == "")
                    {
                        ind16 = "H";
                    }

                    string ind17 = string.Empty;
                    if (dtExcelData.Rows[25][1].ToString() == "" && tout17 == "")
                    {
                        ind17 = "H";
                    }

                    string ind18 = string.Empty;
                    if (dtExcelData.Rows[26][1].ToString() == "" && tout18 == "")
                    {
                        ind18 = "H";
                    }

                    string ind19 = string.Empty;
                    if (dtExcelData.Rows[27][1].ToString() == "" && tout19 == "")
                    {
                        ind19 = "H";
                    }

                    string ind20 = string.Empty;
                    if (dtExcelData.Rows[28][1].ToString() == "" && tout20 == "")
                    {
                        ind20 = "H";
                    }
                    string ind21 = string.Empty;
                    if (dtExcelData.Rows[29][1].ToString() == "" && tout21 == "")
                    {
                        ind21 = "H";
                    }
                    string ind22 = string.Empty;
                    if (dtExcelData.Rows[30][1].ToString() == "" && tout22 == "")
                    {
                        ind22 = "H";
                    }
                    string ind23 = string.Empty;
                    if (dtExcelData.Rows[31][1].ToString() == "" && tout23 == "")
                    {
                        ind23 = "H";
                    }
                    string ind24 = string.Empty;
                    if (dtExcelData.Rows[32][1].ToString() == "" && tout24 == "")
                    {
                        ind24 = "H";
                    }
                    string ind25 = string.Empty;
                    if (dtExcelData.Rows[33][1].ToString() == "" && tout25 == "")
                    {
                        ind25 = "H";
                    }
                    string ind26 = string.Empty;
                    if (dtExcelData.Rows[34][1].ToString() == "" && tout26 == "")
                    {
                        ind26 = "H";
                    }
                    string ind27 = string.Empty;
                    if (dtExcelData.Rows[35][1].ToString() == "" && tout27 == "")
                    {
                        ind27 = "H";
                    }

                    string ind28 = string.Empty, ind29 = string.Empty, ind30 = string.Empty, ind31 = string.Empty;
                    if (dtExcelData.Rows[36][1].ToString() == "" && tout28 == "")
                    {
                        ind28 = "H";
                    }
                    if (dtExcelData.Rows.Count > 37)
                    {

                        if (dtExcelData.Rows[37][1].ToString() == "" && tout29 == "")
                        {
                            ind29 = "H";
                        }

                        if (dtExcelData.Rows[38][1].ToString() == "" && tout30 == "")
                        {
                            ind30 = "H";
                        }
                        if (dtExcelData.Rows.Count > 38)
                        {

                            if (dtExcelData.Rows[39][1].ToString() == "" && tout31 == "")
                            {
                                ind31 = "H";
                            }
                        }
                    }

                    string sind1 = string.Empty;
                    if (dtExcelData.Rows[9][17].ToString() == "" && stout1 == "")
                    {
                        sind1 = "H";
                    }

                    string sind2 = string.Empty;
                    if (dtExcelData.Rows[10][17].ToString() == "" && stout2 == "")
                    {
                        sind2 = "H";
                    }

                    string sind3 = string.Empty;
                    if (dtExcelData.Rows[11][17].ToString() == "" && stout3 == "")
                    {
                        sind3 = "H";
                    }

                    string sind4 = string.Empty;
                    if (dtExcelData.Rows[12][17].ToString() == "" && stout4 == "")
                    {
                        sind4 = "H";
                    }

                    string sind5 = string.Empty;
                    if (dtExcelData.Rows[13][17].ToString() == "" && stout5 == "")
                    {
                        sind5 = "H";
                    }
                    string sind6 = string.Empty;
                    if (dtExcelData.Rows[14][17].ToString() == "" && stout6 == "")
                    {
                        sind6 = "H";
                    }
                    string sind7 = string.Empty;
                    if (dtExcelData.Rows[15][17].ToString() == "" && stout7 == "")
                    {
                        sind7 = "H";
                    }
                    string sind8 = string.Empty;
                    if (dtExcelData.Rows[16][17].ToString() == "" && stout8 == "")
                    {
                        sind8 = "H";
                    }

                    string sind9 = string.Empty;
                    if (dtExcelData.Rows[17][17].ToString() == "" && stout9 == "")
                    {
                        sind9 = "H";
                    }

                    string sind10 = string.Empty;
                    if (dtExcelData.Rows[18][17].ToString() == "" && stout10 == "")
                    {
                        sind10 = "H";
                    }

                    string sind11 = string.Empty;
                    if (dtExcelData.Rows[19][17].ToString() == "" && stout11 == "")
                    {
                        sind11 = "H";
                    }

                    string sind12 = string.Empty;
                    if (dtExcelData.Rows[20][17].ToString() == "" && stout12 == "")
                    {
                        sind12 = "H";
                    }

                    string sind13 = string.Empty;
                    if (dtExcelData.Rows[21][17].ToString() == "" && stout13 == "")
                    {
                        sind13 = "H";
                    }

                    string sind14 = string.Empty;
                    if (dtExcelData.Rows[22][17].ToString() == "" && stout14 == "")
                    {
                        sind14 = "H";
                    }

                    string sind15 = string.Empty;
                    if (dtExcelData.Rows[23][17].ToString() == "" && stout15 == "")
                    {
                        sind15 = "H";
                    }

                    string sind16 = string.Empty;
                    if (dtExcelData.Rows[24][17].ToString() == "" && stout16 == "")
                    {
                        sind16 = "H";
                    }

                    string sind17 = string.Empty;
                    if (dtExcelData.Rows[25][17].ToString() == "" && stout17 == "")
                    {
                        sind17 = "H";
                    }

                    string sind18 = string.Empty;
                    if (dtExcelData.Rows[26][17].ToString() == "" && stout18 == "")
                    {
                        sind18 = "H";
                    }

                    string sind19 = string.Empty;
                    if (dtExcelData.Rows[27][17].ToString() == "" && stout19 == "")
                    {
                        sind19 = "H";
                    }

                    string sind20 = string.Empty;
                    if (dtExcelData.Rows[28][17].ToString() == "" && stout20 == "")
                    {
                        sind20 = "H";
                    }
                    string sind21 = string.Empty;
                    if (dtExcelData.Rows[29][17].ToString() == "" && stout21 == "")
                    {
                        sind21 = "H";
                    }
                    string sind22 = string.Empty;
                    if (dtExcelData.Rows[30][17].ToString() == "" && stout22 == "")
                    {
                        sind22 = "H";
                    }
                    string sind23 = string.Empty;
                    if (dtExcelData.Rows[31][17].ToString() == "" && stout23 == "")
                    {
                        sind23 = "H";
                    }
                    string sind24 = string.Empty;
                    if (dtExcelData.Rows[32][17].ToString() == "" && stout24 == "")
                    {
                        sind24 = "H";
                    }
                    string sind25 = string.Empty;
                    if (dtExcelData.Rows[33][17].ToString() == "" && stout25 == "")
                    {
                        sind25 = "H";
                    }
                    string sind26 = string.Empty;
                    if (dtExcelData.Rows[34][17].ToString() == "" && stout26 == "")
                    {
                        sind26 = "H";
                    }
                    string sind27 = string.Empty;
                    if (dtExcelData.Rows[35][17].ToString() == "" && stout27 == "")
                    {
                        sind27 = "H";
                    }

                    string sind28 = string.Empty, sind29 = string.Empty, sind30 = string.Empty, sind31 = string.Empty;
                    if (dtExcelData.Rows[36][17].ToString() == "" && stout28 == "")
                    {
                        sind28 = "H";
                    }
                    if (dtExcelData.Rows.Count > 37)
                    {

                        if (dtExcelData.Rows[37][17].ToString() == "" && stout29 == "")
                        {
                            sind29 = "H";
                        }

                        if (dtExcelData.Rows[38][17].ToString() == "" && stout30 == "")
                        {
                            sind30 = "H";
                        }
                        if (dtExcelData.Rows.Count > 38)
                        {

                            if (dtExcelData.Rows[39][17].ToString() == "" && stout31 == "")
                            {
                                sind31 = "H";
                            }
                        }
                    }

                    string tind1 = string.Empty;
                    if (dtExcelData.Rows[9][33].ToString() == "" && ttout1 == "")
                    {
                        tind1 = "H";
                    }

                    string tind2 = string.Empty;
                    if (dtExcelData.Rows[10][33].ToString() == "" && ttout2 == "")
                    {
                        tind2 = "H";
                    }

                    string tind3 = string.Empty;
                    if (dtExcelData.Rows[11][33].ToString() == "" && ttout3 == "")
                    {
                        tind3 = "H";
                    }

                    string tind4 = string.Empty;
                    if (dtExcelData.Rows[12][33].ToString() == "" && ttout4 == "")
                    {
                        tind4 = "H";
                    }

                    string tind5 = string.Empty;
                    if (dtExcelData.Rows[13][33].ToString() == "" && ttout5 == "")
                    {
                        tind5 = "H";
                    }
                    string tind6 = string.Empty;
                    if (dtExcelData.Rows[14][33].ToString() == "" && ttout6 == "")
                    {
                        tind6 = "H";
                    }
                    string tind7 = string.Empty;
                    if (dtExcelData.Rows[15][33].ToString() == "" && ttout7 == "")
                    {
                        tind7 = "H";
                    }
                    string tind8 = string.Empty;
                    if (dtExcelData.Rows[16][33].ToString() == "" && ttout8 == "")
                    {
                        tind8 = "H";
                    }

                    string tind9 = string.Empty;
                    if (dtExcelData.Rows[17][33].ToString() == "" && ttout9 == "")
                    {
                        tind9 = "H";
                    }

                    string tind10 = string.Empty;
                    if (dtExcelData.Rows[18][33].ToString() == "" && ttout10 == "")
                    {
                        tind10 = "H";
                    }

                    string tind11 = string.Empty;
                    if (dtExcelData.Rows[19][33].ToString() == "" && ttout11 == "")
                    {
                        tind11 = "H";
                    }

                    string tind12 = string.Empty;
                    if (dtExcelData.Rows[20][33].ToString() == "" && ttout12 == "")
                    {
                        tind12 = "H";
                    }

                    string tind13 = string.Empty;
                    if (dtExcelData.Rows[21][33].ToString() == "" && ttout13 == "")
                    {
                        tind13 = "H";
                    }

                    string tind14 = string.Empty;
                    if (dtExcelData.Rows[22][33].ToString() == "" && ttout14 == "")
                    {
                        tind14 = "H";
                    }

                    string tind15 = string.Empty;
                    if (dtExcelData.Rows[23][33].ToString() == "" && ttout15 == "")
                    {
                        tind15 = "H";
                    }

                    string tind16 = string.Empty;
                    if (dtExcelData.Rows[24][33].ToString() == "" && ttout16 == "")
                    {
                        tind16 = "H";
                    }

                    string tind17 = string.Empty;
                    if (dtExcelData.Rows[25][33].ToString() == "" && ttout17 == "")
                    {
                        tind17 = "H";
                    }

                    string tind18 = string.Empty;
                    if (dtExcelData.Rows[26][33].ToString() == "" && ttout18 == "")
                    {
                        tind18 = "H";
                    }

                    string tind19 = string.Empty;
                    if (dtExcelData.Rows[27][33].ToString() == "" && ttout19 == "")
                    {
                        tind19 = "H";
                    }

                    string tind20 = string.Empty;
                    if (dtExcelData.Rows[28][33].ToString() == "" && ttout20 == "")
                    {
                        tind20 = "H";
                    }
                    string tind21 = string.Empty;
                    if (dtExcelData.Rows[29][33].ToString() == "" && ttout21 == "")
                    {
                        tind21 = "H";
                    }
                    string tind22 = string.Empty;
                    if (dtExcelData.Rows[30][33].ToString() == "" && ttout22 == "")
                    {
                        tind22 = "H";
                    }
                    string tind23 = string.Empty;
                    if (dtExcelData.Rows[31][33].ToString() == "" && ttout23 == "")
                    {
                        tind23 = "H";
                    }
                    string tind24 = string.Empty;
                    if (dtExcelData.Rows[32][33].ToString() == "" && ttout24 == "")
                    {
                        tind24 = "H";
                    }
                    string tind25 = string.Empty;
                    if (dtExcelData.Rows[33][33].ToString() == "" && ttout25 == "")
                    {
                        tind25 = "H";
                    }
                    string tind26 = string.Empty;
                    if (dtExcelData.Rows[34][33].ToString() == "" && ttout26 == "")
                    {
                        tind26 = "H";
                    }
                    string tind27 = string.Empty;
                    if (dtExcelData.Rows[35][33].ToString() == "" && ttout27 == "")
                    {
                        tind27 = "H";
                    }

                    string tind28 = string.Empty, tind29 = string.Empty, tind30 = string.Empty, tind31 = string.Empty;
                    if (dtExcelData.Rows[36][33].ToString() == "" && ttout28 == "")
                    {
                        tind28 = "H";
                    }
                    if (dtExcelData.Rows.Count > 37)
                    {

                        if (dtExcelData.Rows[37][33].ToString() == "" && ttout29 == "")
                        {
                            tind29 = "H";
                        }

                        if (dtExcelData.Rows[38][33].ToString() == "" && ttout30 == "")
                        {
                            tind30 = "H";
                        }
                        if (dtExcelData.Rows.Count > 38)
                        {

                            if (dtExcelData.Rows[39][33].ToString() == "" && ttout31 == "")
                            {
                                tind31 = "H";
                            }
                        }
                    }

                    string Inssql1 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate1 + "','" + dtExcelData.Rows[9][1].ToString() + "','" + tout1 + "','" + ind1 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql1);

                    string Inssql2 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate2 + "','" + dtExcelData.Rows[10][1].ToString() + "','" + tout2 + "','" + ind2 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql2);

                    string Inssql3 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate3 + "','" + dtExcelData.Rows[11][1].ToString() + "','" + tout3 + "','" + ind3 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql3);

                    string Inssql4 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate4 + "','" + dtExcelData.Rows[12][1].ToString() + "','" + tout4 + "','" + ind4 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql4);

                    string Inssql5 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate5 + "','" + dtExcelData.Rows[13][1].ToString() + "','" + tout5 + "','" + ind5 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql5);

                    string Inssql6 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate6 + "','" + dtExcelData.Rows[14][1].ToString() + "','" + tout6 + "','" + ind6 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql6);

                    string Inssql7 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate7 + "','" + dtExcelData.Rows[15][1].ToString() + "','" + tout7 + "','" + ind7 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql7);

                    string Inssql8 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate8 + "','" + dtExcelData.Rows[16][1].ToString() + "','" + tout8 + "','" + ind8 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql8);

                    string Inssql9 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate9 + "','" + dtExcelData.Rows[17][1].ToString() + "','" + tout9 + "','" + ind9 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql9);

                    string Inssql10 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate10 + "','" + dtExcelData.Rows[18][1].ToString() + "','" + tout10 + "','" + ind10 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql10);

                    string Inssql11 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate11 + "','" + dtExcelData.Rows[19][1].ToString() + "','" + tout11 + "','" + ind11 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql11);

                    string Inssql12 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate12 + "','" + dtExcelData.Rows[20][1].ToString() + "','" + tout12 + "','" + ind12 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql12);

                    string Inssql13 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate13 + "','" + dtExcelData.Rows[21][1].ToString() + "','" + tout13 + "','" + ind13 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql13);

                    string Inssql14 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate14 + "','" + dtExcelData.Rows[22][1].ToString() + "','" + tout14 + "','" + ind14 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql14);

                    string Inssql15 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate15 + "','" + dtExcelData.Rows[23][1].ToString() + "','" + tout15 + "','" + ind15 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql15);

                    string Inssql16 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate16 + "','" + dtExcelData.Rows[24][1].ToString() + "','" + tout16 + "','" + ind16 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql16);

                    string Inssql17 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate17 + "','" + dtExcelData.Rows[25][1].ToString() + "','" + tout17 + "','" + ind17 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql17);

                    string Inssql18 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate18 + "','" + dtExcelData.Rows[26][1].ToString() + "','" + tout18 + "','" + ind18 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql18);

                    string Inssql19 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate19 + "','" + dtExcelData.Rows[27][1].ToString() + "','" + tout19 + "','" + ind19 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql19);

                    string Inssql20 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate20 + "','" + dtExcelData.Rows[28][1].ToString() + "','" + tout20 + "','" + ind20 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql20);

                    string Inssql21 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate21 + "','" + dtExcelData.Rows[29][1].ToString() + "','" + tout21 + "','" + ind21 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql21);

                    string Inssql22 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate22 + "','" + dtExcelData.Rows[30][1].ToString() + "','" + tout22 + "','" + ind22 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql22);

                    string Inssql23 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate23 + "','" + dtExcelData.Rows[31][1].ToString() + "','" + tout23 + "','" + ind23 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql23);

                    string Inssql24 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate24 + "','" + dtExcelData.Rows[32][1].ToString() + "','" + tout24 + "','" + ind24 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql24);

                    string Inssql25 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate25 + "','" + dtExcelData.Rows[33][1].ToString() + "','" + tout25 + "','" + ind25 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql25);

                    string Inssql26 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate26 + "','" + dtExcelData.Rows[34][1].ToString() + "','" + tout26 + "','" + ind26 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql26);

                    string Inssql27 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate27 + "','" + dtExcelData.Rows[35][1].ToString() + "','" + tout27 + "','" + ind27 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql27);

                    string Inssql28 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate28 + "','" + dtExcelData.Rows[36][1].ToString() + "','" + tout28 + "','" + ind28 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(Inssql28);
                    if (dtExcelData.Rows.Count > 37)
                    {
                        string Inssql29 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate29 + "','" + dtExcelData.Rows[37][1].ToString() + "','" + tout29 + "','" + ind29 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                        Status = dbcon.Ora_Execute_CommamdText(Inssql29);

                        string Inssql30 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate30 + "','" + dtExcelData.Rows[38][1].ToString() + "','" + tout30 + "','" + ind30 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                        Status = dbcon.Ora_Execute_CommamdText(Inssql30);
                        if (dtExcelData.Rows.Count > 39)
                        {
                            string Inssql31 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][10].ToString() + "','" + atedate31 + "','" + dtExcelData.Rows[39][1].ToString() + "','" + tout31 + "','" + ind31 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                            Status = dbcon.Ora_Execute_CommamdText(Inssql31);
                        }
                    }
                    //second

                    string sInssql1 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate1 + "','" + dtExcelData.Rows[9][17].ToString() + "','" + stout1 + "','" + sind1 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql1);

                    string sInssql2 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate2 + "','" + dtExcelData.Rows[10][17].ToString() + "','" + stout2 + "','" + sind2 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql2);

                    string sInssql3 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate3 + "','" + dtExcelData.Rows[11][17].ToString() + "','" + stout3 + "','" + sind3 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql3);

                    string sInssql4 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate4 + "','" + dtExcelData.Rows[12][17].ToString() + "','" + stout4 + "','" + sind4 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql4);

                    string sInssql5 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate5 + "','" + dtExcelData.Rows[13][17].ToString() + "','" + stout5 + "','" + sind5 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql5);

                    string sInssql6 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate6 + "','" + dtExcelData.Rows[14][17].ToString() + "','" + stout6 + "','" + sind6 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql6);

                    string sInssql7 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate7 + "','" + dtExcelData.Rows[15][17].ToString() + "','" + stout7 + "','" + sind7 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql7);

                    string sInssql8 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate8 + "','" + dtExcelData.Rows[16][17].ToString() + "','" + stout8 + "','" + sind8 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql8);

                    string sInssql9 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate9 + "','" + dtExcelData.Rows[17][17].ToString() + "','" + stout9 + "','" + sind9 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql9);

                    string sInssql10 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate10 + "','" + dtExcelData.Rows[18][1].ToString() + "','" + stout10 + "','" + sind10 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql10);

                    string sInssql11 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate11 + "','" + dtExcelData.Rows[19][17].ToString() + "','" + stout11 + "','" + sind11 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql11);

                    string sInssql12 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate12 + "','" + dtExcelData.Rows[20][17].ToString() + "','" + stout12 + "','" + sind12 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql12);

                    string sInssql13 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate13 + "','" + dtExcelData.Rows[21][17].ToString() + "','" + stout13 + "','" + sind13 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql13);

                    string sInssql14 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate14 + "','" + dtExcelData.Rows[22][17].ToString() + "','" + stout14 + "','" + sind14 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql14);

                    string sInssql15 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate15 + "','" + dtExcelData.Rows[23][17].ToString() + "','" + stout15 + "','" + sind15 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql15);

                    string sInssql16 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate16 + "','" + dtExcelData.Rows[24][17].ToString() + "','" + stout16 + "','" + sind16 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql16);

                    string sInssql17 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate17 + "','" + dtExcelData.Rows[25][17].ToString() + "','" + stout17 + "','" + sind17 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql17);

                    string sInssql18 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate18 + "','" + dtExcelData.Rows[26][17].ToString() + "','" + stout18 + "','" + sind18 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql18);

                    string sInssql19 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate19 + "','" + dtExcelData.Rows[27][17].ToString() + "','" + stout19 + "','" + sind19 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql19);

                    string sInssql20 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate20 + "','" + dtExcelData.Rows[28][17].ToString() + "','" + stout20 + "','" + sind20 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql20);

                    string sInssql21 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate21 + "','" + dtExcelData.Rows[29][17].ToString() + "','" + stout21 + "','" + sind21 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql21);

                    string sInssql22 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate22 + "','" + dtExcelData.Rows[30][17].ToString() + "','" + stout22 + "','" + sind22 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql22);

                    string sInssql23 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate23 + "','" + dtExcelData.Rows[31][17].ToString() + "','" + stout23 + "','" + sind23 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql23);

                    string sInssql24 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate24 + "','" + dtExcelData.Rows[32][17].ToString() + "','" + stout24 + "','" + sind24 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql24);

                    string sInssql25 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate25 + "','" + dtExcelData.Rows[33][17].ToString() + "','" + stout25 + "','" + sind25 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql25);

                    string sInssql26 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate26 + "','" + dtExcelData.Rows[34][17].ToString() + "','" + stout26 + "','" + sind26 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql26);

                    string sInssql27 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate27 + "','" + dtExcelData.Rows[35][17].ToString() + "','" + stout27 + "','" + sind27 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql27);

                    string sInssql28 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate28 + "','" + dtExcelData.Rows[36][17].ToString() + "','" + stout28 + "','" + sind28 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(sInssql28);
                    if (dtExcelData.Rows.Count > 37)
                    {
                        string sInssql29 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate29 + "','" + dtExcelData.Rows[37][17].ToString() + "','" + stout29 + "','" + sind29 + "v','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                        Status = dbcon.Ora_Execute_CommamdText(sInssql29);

                        string sInssql30 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate30 + "','" + dtExcelData.Rows[38][17].ToString() + "','" + stout30 + "','" + sind30 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                        Status = dbcon.Ora_Execute_CommamdText(sInssql30);
                        if (dtExcelData.Rows.Count > 39)
                        {
                            string sInssql31 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][26].ToString() + "','" + satedate31 + "','" + dtExcelData.Rows[39][17].ToString() + "','" + stout31 + "','" + sind31 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                            Status = dbcon.Ora_Execute_CommamdText(sInssql31);
                        }
                    }
                    //third
                    string tInssql1 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate1 + "','" + dtExcelData.Rows[9][33].ToString() + "','" + ttout1 + "','" + tind1 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql1);

                    string tInssql2 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate2 + "','" + dtExcelData.Rows[10][33].ToString() + "','" + ttout2 + "','" + tind2 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql2);

                    string tInssql3 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate3 + "','" + dtExcelData.Rows[11][33].ToString() + "','" + ttout3 + "','" + tind3 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql3);

                    string tInssql4 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate4 + "','" + dtExcelData.Rows[12][33].ToString() + "','" + ttout4 + "','" + tind4 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql4);

                    string tInssql5 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate5 + "','" + dtExcelData.Rows[13][33].ToString() + "','" + ttout5 + "','" + tind5 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql5);

                    string tInssql6 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate6 + "','" + dtExcelData.Rows[14][33].ToString() + "','" + ttout6 + "','" + tind6 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql6);

                    string tInssql7 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate7 + "','" + dtExcelData.Rows[15][33].ToString() + "','" + ttout7 + "','" + tind7 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql7);

                    string tInssql8 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate8 + "','" + dtExcelData.Rows[16][33].ToString() + "','" + ttout8 + "','" + tind8 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql8);

                    string tInssql9 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate9 + "','" + dtExcelData.Rows[17][33].ToString() + "','" + ttout9 + "','" + tind9 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql9);

                    string tInssql10 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate10 + "','" + dtExcelData.Rows[18][33].ToString() + "','" + ttout10 + "','" + tind10 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql10);

                    string tInssql11 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate11 + "','" + dtExcelData.Rows[19][33].ToString() + "','" + ttout11 + "','" + tind11 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql11);

                    string tInssql12 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate12 + "','" + dtExcelData.Rows[20][33].ToString() + "','" + ttout12 + "','" + tind12 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql12);

                    string tInssql13 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate13 + "','" + dtExcelData.Rows[21][33].ToString() + "','" + ttout13 + "','" + tind13 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql13);

                    string tInssql14 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate14 + "','" + dtExcelData.Rows[22][33].ToString() + "','" + ttout14 + "','" + tind14 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql14);

                    string tInssql15 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate15 + "','" + dtExcelData.Rows[23][33].ToString() + "','" + ttout15 + "','" + tind15 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql15);

                    string tInssql16 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate16 + "','" + dtExcelData.Rows[24][33].ToString() + "','" + ttout16 + "','" + tind16 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql16);

                    string tInssql17 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate17 + "','" + dtExcelData.Rows[25][33].ToString() + "','" + ttout17 + "','" + tind17 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql17);

                    string tInssql18 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate18 + "','" + dtExcelData.Rows[26][33].ToString() + "','" + ttout18 + "','" + tind18 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql18);

                    string tInssql19 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate19 + "','" + dtExcelData.Rows[27][33].ToString() + "','" + ttout19 + "','" + tind19 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql19);

                    string tInssql20 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate20 + "','" + dtExcelData.Rows[28][33].ToString() + "','" + ttout20 + "','" + tind20 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql20);

                    string tInssql21 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate21 + "','" + dtExcelData.Rows[29][33].ToString() + "','" + ttout21 + "','" + tind21 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql21);

                    string tInssql22 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate22 + "','" + dtExcelData.Rows[30][33].ToString() + "','" + stout22 + "','" + tind22 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql22);

                    string tInssql23 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate23 + "','" + dtExcelData.Rows[31][33].ToString() + "','" + ttout23 + "','" + tind23 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql23);

                    string tInssql24 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate24 + "','" + dtExcelData.Rows[32][33].ToString() + "','" + ttout24 + "','" + tind24 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql24);

                    string tInssql25 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate25 + "','" + dtExcelData.Rows[33][33].ToString() + "','" + ttout25 + "','" + tind25 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql25);

                    string tInssql26 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate26 + "','" + dtExcelData.Rows[34][33].ToString() + "','" + ttout26 + "','" + tind26 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql26);

                    string tInssql27 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate27 + "','" + dtExcelData.Rows[35][33].ToString() + "','" + ttout27 + "','" + tind27 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql27);

                    string tInssql28 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate28 + "','" + dtExcelData.Rows[36][33].ToString() + "','" + ttout28 + "','" + tind28 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                    Status = dbcon.Ora_Execute_CommamdText(tInssql28);
                    if (dtExcelData.Rows.Count > 37)
                    {
                        string tInssql29 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate29 + "','" + dtExcelData.Rows[37][33].ToString() + "','" + ttout29 + "','" + tind29 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                        Status = dbcon.Ora_Execute_CommamdText(tInssql29);

                        string tInssql30 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate30 + "','" + dtExcelData.Rows[38][33].ToString() + "','" + ttout30 + "','" + tind30 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                        Status = dbcon.Ora_Execute_CommamdText(tInssql30);
                        if (dtExcelData.Rows.Count > 39)
                        {
                            string tInssql31 = "insert into hr_attendance(atd_staff_no,atd_date,atd_clock_in,atd_clock_out,atd_hol_late_ind,atd_remark,atd_crt_id,atd_crt_dt)values ('" + dtExcelData.Rows[1][42].ToString() + "','" + tatedate31 + "','" + dtExcelData.Rows[39][33].ToString() + "','" + ttout31 + "','" + tind31 + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
                            Status = dbcon.Ora_Execute_CommamdText(tInssql31);
                        }
                    }

                }
                else
                {
                   

                    Page.ClientScript.RegisterStartupScript(typeof(Page), "alertMessage", "$.Zebra_Dialog('File Format Tidak Menyokong.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }

            }
            service.audit_trail("P0046", "Muatnaik","Filename", FileUpload1.PostedFile.FileName);
            Page.ClientScript.RegisterStartupScript(typeof(Page), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

        }
        else
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "alertMessage", "$.Zebra_Dialog('Sila Pilih fail.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
        }
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../SUMBER_MANUSIA/HR_kem_mak_Latihan_view.aspx");
    }


}


