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
public partial class kw_user_registration : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
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
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
      
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                 //PopulateTreeview();
                Type_Kumpulan();
                userid = Session["New"].ToString();
                //btb_kmes.Visible = false;
                ImgPrv.Attributes.Add("src", "../Files/user/user.gif");
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    lbl_name.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    view_details();
                    edit_id.Text = "1";
                    //rol_set.Attributes.Remove("style");
                    Button4.Attributes.Remove("style");
                    submit_btn.Text = "Kemaskini";
                    //shw_cnt1.Visible = true;
                    BindData_jenis();
                }
                else
                {
                    //rol_set.Attributes.Add("style", "Pointer-events:none; opacity: 0.6;");
                    Button4.Attributes.Add("style", "Pointer-events:none; opacity: 0.6;");
                    submit_btn.Text = "Simpan";
                    //shw_cnt1.Visible = false;
                    edit_id.Text = "0";
                    
                }
                
            }
            else
            {                
                Response.Redirect("~/KSAIMB_Login.aspx");
            }
        }
    }

    protected void BindData_jenis()
    {
        con.Open();
        string sqry = string.Empty;
        if (txtSearch.Text == "")
        {
            sqry = "";
        }
        else
        {
            sqry = "where a.KK_Skrin_name LIKE'%" + txtSearch.Text + "%' OR b.KK_Sskrin_name LIKE'%" + txtSearch.Text + "%' OR c.KK_Spreskrin_name LIKE'%" + txtSearch.Text + "%' OR d.KK_Spreskrin1_name LIKE'%" + txtSearch.Text + "%'";
        }

        qry1 = "select a.KK_Skrin_id,a.KK_Skrin_name,b.KK_Sskrin_id,b.KK_Sskrin_name,ISNULL(c.KK_Spreskrin_id,'') KK_Spreskrin_id,ISNULL(c.KK_Spreskrin_name,'') KK_Spreskrin_name,ISNULL(d.KK_Spreskrin1_id,'') KK_Spreskrin1_id,ISNULL(d.KK_Spreskrin1_name,'') KK_Spreskrin1_name from (select * from KK_PID_Skrin m1 where Status='A') as a left join (select * from KK_PID_Sub_Skrin where Status='A') as b on b.KK_Skrin_id=a.KK_Skrin_id left join (select * from KK_PID_presub_skrin where Status='A') as c on c.KK_Skrin_id=B.KK_Skrin_id and c.KK_Sskrin_id=b.KK_Sskrin_id left join (select * from KK_PID_presub1_skrin where Status='A') as d on d.KK_Skrin_id=c.KK_Skrin_id and d.KK_Sskrin_id=c.KK_Sskrin_id and d.KK_Spreskrin_id=c.KK_Spreskrin_id " + sqry + " order by a.position,b.position,c.position,d.position";
        SqlCommand cmd = new SqlCommand("" + qry1 + "", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
            //btn_hups.Visible = false;
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        con.Close();
    }

    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        BindData_jenis();
    }


    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

        BindData_jenis();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindData_jenis();
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {


        System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)sender;
        decimal val1 = 0;
        //if (TextBox4.Text != "")
        //{


        if (chk.Checked == true)
        {
            GridViewRow gvr = (GridViewRow)chk.NamingContainer;
            System.Web.UI.WebControls.CheckBox chk1 = (System.Web.UI.WebControls.CheckBox)gvr.FindControl("chk2");
            System.Web.UI.WebControls.CheckBox chk2 = (System.Web.UI.WebControls.CheckBox)gvr.FindControl("chk3");
            System.Web.UI.WebControls.CheckBox chk3 = (System.Web.UI.WebControls.CheckBox)gvr.FindControl("chk4");
            chk1.Attributes.Remove("style");
            chk2.Attributes.Remove("style");
            chk3.Attributes.Remove("style");
            chk1.Checked = true;
            chk2.Checked = true;
            chk3.Checked = true;
        }
        else
        {
            GridViewRow gvr = (GridViewRow)chk.NamingContainer;
            System.Web.UI.WebControls.CheckBox chk1 = (System.Web.UI.WebControls.CheckBox)gvr.FindControl("chk2");
            System.Web.UI.WebControls.CheckBox chk2 = (System.Web.UI.WebControls.CheckBox)gvr.FindControl("chk3");
            System.Web.UI.WebControls.CheckBox chk3 = (System.Web.UI.WebControls.CheckBox)gvr.FindControl("chk4");
            chk1.Attributes.Add("style", "pointer-events:None;");
            chk2.Attributes.Add("style", "pointer-events:None;");
            chk3.Attributes.Add("style", "pointer-events:None;");
            chk1.Checked = false;
            chk2.Checked = false;
            chk3.Checked = false;
        }

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.CheckBox CheckBox1 = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chk1");
            System.Web.UI.WebControls.CheckBox CheckBox2 = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chk2");
            System.Web.UI.WebControls.CheckBox CheckBox3 = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chk3");
            System.Web.UI.WebControls.CheckBox CheckBox4 = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chk4");
            string vv1 = ((System.Web.UI.WebControls.Label)e.Row.FindControl("vv1")).Text.ToString();
            string vv2 = ((System.Web.UI.WebControls.Label)e.Row.FindControl("vv2")).Text.ToString();
            string vv3 = ((System.Web.UI.WebControls.Label)e.Row.FindControl("vv3")).Text.ToString();
            string vv4 = ((System.Web.UI.WebControls.Label)e.Row.FindControl("vv4")).Text.ToString();

            DataTable dd2 = new DataTable();
            dd2 = DBCon.Ora_Execute_table("select * from KK_Role_skrins where Role_id='" + TextBox1.Text + "' and skrin_id='" + vv1 + "' and sub_skrin_id='" + vv2 + "' and psub_skrin_id='" + vv3 + "' and psub1_skrin_id='" + vv4 + "'");
            if (dd2.Rows.Count != 0)
            {
                if (dd2.Rows[0]["permisssion"].ToString() == "1")
                {
                    CheckBox1.Checked = true;
                }

                if (dd2.Rows[0]["view_chk"].ToString() == "1")
                {
                    CheckBox2.Checked = true;
                }
                if (dd2.Rows[0]["Add_chk"].ToString() == "1")
                {
                    CheckBox3.Checked = true;
                }
                if (dd2.Rows[0]["Edit_chk"].ToString() == "1")
                {
                    CheckBox4.Checked = true;
                }
            }
            else
            {


                if (CheckBox1.Checked == false)
                {
                    CheckBox2.Attributes.Add("style", "pointer-events:none;");
                    CheckBox3.Attributes.Add("style", "pointer-events:none;");
                    CheckBox4.Attributes.Add("style", "pointer-events:none;");
                    CheckBox2.Checked = false;
                    CheckBox3.Checked = false;
                    CheckBox4.Checked = false;
                }
                else
                {
                    CheckBox2.Checked = true;
                    CheckBox3.Checked = true;
                    CheckBox4.Checked = true;
                    CheckBox2.Attributes.Remove("style");
                    CheckBox3.Attributes.Remove("style");
                    CheckBox4.Attributes.Remove("style");
                }
            }

        }
    }
    void view_details()
    {
        try
        {
            Button1.Visible = false;
             DataTable dd1 = new DataTable();
            dd1 = DBCon.Ora_Execute_table("select *,FORMAT(KK_tarikh_daftar,'dd/MM/yyyy', 'en-us') as daft_dt from kk_User_Login where '" + lbl_name.Text + "' IN (cast(KK_userid as varchar),cast(Id as varchar))");
            if (dd1.Rows.Count != 0)
            {
                uname.Attributes.Add("Readonly", "Readonly");
                uname.Text = dd1.Rows[0]["KK_userid"].ToString();
                pname.Text = dd1.Rows[0]["KK_username"].ToString();
                email.Text = dd1.Rows[0]["KK_email"].ToString();
                sts.SelectedValue = dd1.Rows[0]["Status"].ToString();

                string checkimage = dd1.Rows[0]["user_img"].ToString();

                string fileName = Path.GetFileName(checkimage);
                if (fileName != "")
                {
                    ImgPrv.Attributes.Add("src", "../Files/user/" + fileName);
                }
                else
                {
                    ImgPrv.Attributes.Add("src", "../Files/user/user.gif");
                }
                TextBox2.Text = dd1.Rows[0]["KK_skrins"].ToString();
                TextBox3.Text = dd1.Rows[0]["KK_roles"].ToString();
                string[] k1 = dd1.Rows[0]["KK_roles"].ToString().Split(',');
                for (int m = 0; m <= k1.Length - 1; m++)
                {
                    for (int i = 0; i <= skrin_list.Items.Count - 1; i++)
                    {
                        if (skrin_list.Items[i].Value == k1[m])
                        {
                            skrin_list.Items[i].Selected = true;
                        }
                    }
                }

                //string[] k = dd1.Rows[0]["KK_skrins"].ToString().Split(',');

                //foreach (TreeNode parent in tvTables.Nodes)
                //{
                //    for (int j = 0; j < k.Length; j++)
                //    {
                //        if (parent.Value.Trim() == k[j].ToString().Trim())
                //        {
                //            parent.Checked = true;
                //            break;
                //        }

                //    }
                //    foreach (TreeNode child in parent.ChildNodes)
                //    {

                //        for (int j = 0; j < k.Length; j++)
                //        {
                //            if (child.Value.Trim() == k[j].ToString().Trim())
                //            {
                //                child.Checked = true;
                //                break;
                //            }

                //        }
                //        foreach (TreeNode subchild in child.ChildNodes)
                //        {
                //            for (int j = 0; j < k.Length; j++)
                //            {
                //                if (subchild.Value.Trim() == k[j].ToString().Trim())
                //                {
                //                    subchild.Checked = true;
                //                    break;
                //                }

                //            }
                //            foreach (TreeNode presubchild in subchild.ChildNodes)
                //            {
                //                for (int j = 0; j < k.Length; j++)
                //                {
                //                    if (presubchild.Value.Trim() == k[j].ToString().Trim())
                //                    {
                //                        presubchild.Checked = true;
                //                        break;
                //                    }

                //                }
                //            }
                //        }

                //    }
                //}

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Kakitangan Tidak Berdaftar',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void Type_Kumpulan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select *,KK_kumpulan_id +' '+KK_kumpulan_name as ss1 from KK_PID_Kumpulan where Status = 'A' order by KK_kumpulan_id";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con1);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            skrin_list.DataSource = dt;
            skrin_list.DataTextField = "ss1";
            skrin_list.DataValueField = "KK_kumpulan_id";
            skrin_list.DataBind();



        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void chg_cpassword(object sender, EventArgs e)
    {
        if(password.Text != "")
        {
            if (password.Text != cpassword.Text)
            {
                cpassword.Text = "";
                cpassword.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Password Mis Match.',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
            }
        }
    }

    protected void clk_submit(object sender, EventArgs e)
    {
        string reg_date = string.Empty, chk_role = string.Empty, ss1 = string.Empty;
        if (uname.Text != "" && pname.Text != "")
        {
            string spwd = string.Empty;
           

            DataTable dd1 = new DataTable();
            dd1 = DBCon.Ora_Execute_table("select * from kk_User_Login where KK_userid = '" + uname.Text + "'");
            if (cpassword.Text != "")
            {
                spwd = cpassword.Text;
            }
            else
            {
                if (dd1.Rows.Count == 0)
                {
                    spwd = "12345";
                }
                else
                {
                    spwd = dd1.Rows[0]["KK_password"].ToString();
                }
            }
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            if (fileName != "")
            {
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Files/user/" + fileName));//Or code to save in the DataBase.
                    System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                    decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
            }
            else
            {
                if(dd1.Rows.Count != 0)
                {
                    fileName = dd1.Rows[0]["user_img"].ToString();
                }

            }
            if (edit_id.Text == "0")
            {
                
                if (dd1.Rows.Count == 0)
                {
                    string Inssql = "Insert into kk_User_Login (KK_userid,KK_password,Kk_username,KK_email,KK_roles,Status,Kk_crt_id,KK_crt_dt,KK_skrins,user_img,KK_user_type) Values('" + uname.Text + "','"+ spwd + "','" + pname.Text.Replace("''", "'") + "','" + email.Text + "','','" + sts.SelectedValue + "','"+ Session["new"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','','"+ fileName + "','N')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        service.audit_trail("S0009", "Pengurusan Pengguna Masukkan","User Id", uname.Text);
                        Session["validate_success"] = "SUCCESS";
                        Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        Response.Redirect("../Pentadbiran/kw_user_registration_view.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert',{'type': 'error','title': 'Error','auto_close': 2000});", true);
                    }
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('No Kakitangan Already Exist',{'type': 'warning','title': 'Warning','auto_close': 2000}); ", true);
                }
            }
            else
            {
                string Inssql = "Update kk_User_Login SET KK_userid='" + uname.Text.Replace("''", "'") + "',KK_password='"+ spwd +"',Kk_username='" + pname.Text.Replace("''", "'") + "',user_img='"+ fileName +"',KK_email='" + email.Text + "',KK_roles='" + TextBox3.Text + "',KK_skrins='" + TextBox2.Text + "',Status='" + sts.SelectedValue + "',Kk_upd_id='"+ Session["new"].ToString() + "',KK_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where '"+ lbl_name.Text + "' IN (cast(KK_userid as varchar),cast(Id as varchar))";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    service.audit_trail("S0009", "Pengurusan Pengguna Kemaskini", "User Id", uname.Text);
                    Session["validate_success"] = "SUCCESS";
                    Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    Response.Redirect("../Pentadbiran/kw_user_registration_view.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Not Insert',{'type': 'error','title': 'Error','auto_close': 2000});", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Kakitangan',{'type': 'warning','title': 'Warning'}); ", true);
        }
    }

    //Remove Hyperlinks
    //System.Web.UI.WebControls.TreeView RemoveHyperLinks(System.Web.UI.WebControls.TreeView treeView, TreeNodeCollection treeNodes)
    //{
    //    foreach (TreeNode node in treeNodes)
    //    {
    //        node.SelectAction = TreeNodeSelectAction.None;//here the link is removed
    //        if (node.ChildNodes != null && node.ChildNodes.Count > 0)
    //        {
    //            treeView = RemoveHyperLinks(treeView, node.ChildNodes);
    //        }
    //    }
    //    return treeView;
    //}

    //private void PopulateTreeview()

    //{

    //    try

    //    {

    //        DataSet ds = new DataSet();

    //        DataTable dtparent = new DataTable();

    //        DataTable dtchild = new DataTable();
    //        DataTable dtsubchild = new DataTable();
    //        DataTable dtpresubchild = new DataTable();

    //        dtparent = FillParentTable();

    //        dtparent.TableName = "A";

    //        dtchild = FillChildTable();

    //        dtchild.TableName = "B";

    //        dtsubchild = FillsubChildTable();

    //        dtsubchild.TableName = "C";

    //        dtpresubchild = FillpresubChildTable();

    //        dtpresubchild.TableName = "D";

    //        ds.Tables.Add(dtparent);

    //        ds.Tables.Add(dtchild);

    //        ds.Tables.Add(dtsubchild);
    //        ds.Tables.Add(dtpresubchild);

    //        ds.Relations.Add("children", dtparent.Columns["KK_Skrin_id"],

    //                                      dtchild.Columns["KK_Skrin_id"]);

    //        ds.Relations.Add("subchildren", dtchild.Columns["KK_Sskrin_id"],

    //                                    dtsubchild.Columns["KK_Sskrin_id"]);
    //        ds.Relations.Add("presubchildren", dtsubchild.Columns["KK_Spreskrin_id"],

    //                                  dtpresubchild.Columns["KK_Spreskrin_id"]);

    //        if (ds.Tables[0].Rows.Count > 0)

    //        {

    //            tvTables.Nodes.Clear();

    //            foreach (DataRow masterRow in ds.Tables[0].Rows)

    //            {

    //                TreeNode masterNode = new TreeNode((string)masterRow["KK_Skrin_name"],

    //                                      Convert.ToString(masterRow["KK_Skrin_name"]));

    //                tvTables.Nodes.Add(masterNode);
    //                RemoveHyperLinks(tvTables, tvTables.Nodes);
    //                masterNode.Value = Convert.ToString(masterRow["KK_Skrin_id"]);

    //                //tvTables.CollapseAll();

    //                foreach (DataRow childRow in masterRow.GetChildRows("Children"))

    //                {

    //                    TreeNode childNode = new TreeNode((string)childRow["KK_Sskrin_name"],

    //                                          Convert.ToString(childRow["KK_Sskrin_name"]));

    //                    masterNode.ChildNodes.Add(childNode);
    //                    childNode.Value = Convert.ToString(childRow["KK_Sskrin_id"]);
    //                    //masterNode.CollapseAll();
    //                    foreach (DataRow subchildRow in childRow.GetChildRows("subchildren"))

    //                    {

    //                        TreeNode subchildNode = new TreeNode((string)subchildRow["KK_Spreskrin_name"],

    //                                              Convert.ToString(subchildRow["KK_Spreskrin_name"]));

    //                        childNode.ChildNodes.Add(subchildNode);

    //                        subchildNode.Value = Convert.ToString(subchildRow["KK_Spreskrin_id"]);
    //                        foreach (DataRow presubchildRow in subchildRow.GetChildRows("presubchildren"))

    //                        {

    //                            TreeNode presubchildNode = new TreeNode((string)presubchildRow["KK_Spreskrin1_name"],

    //                                                  Convert.ToString(presubchildRow["KK_Spreskrin1_name"]));

    //                            subchildNode.ChildNodes.Add(presubchildNode);

    //                            presubchildNode.Value = Convert.ToString(presubchildRow["KK_Spreskrin1_id"]);

    //                        }

    //                    }
    //                    //childNode.Value = Convert.ToString(childRow["KK_Sskrin_id"]);

    //                }

    //            }

    //        }

    //    }

    //    catch (Exception ex)

    //    {

    //        throw new Exception("Unable to populate treeview" + ex.Message);

    //    }

    //}



    //private DataTable FillParentTable()

    //{

    //    DataTable dt = new DataTable();

    //    con1.Open();

    //    string cmdstr = "select KK_Skrin_id, UPPER(KK_Skrin_name) as KK_Skrin_name from KK_PID_Skrin where status='A' order by cast(position as int)";

    //    SqlCommand cmd = new SqlCommand(cmdstr, con1);

    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);

    //    adp.Fill(dt);

    //    con1.Close();

    //    return dt;

    //}



    //private DataTable FillChildTable()

    //{

    //    DataTable dt = new DataTable();

    //    con1.Open();
    //    string cmdstr = string.Empty;
    //    if (Session["New"].ToString() == "it_000011" || Session["New"].ToString() == "0000000018")
    //    {
    //        cmdstr = "select KK_Skrin_id,KK_Sskrin_id,UPPER(KK_Sskrin_name) as KK_Sskrin_name from KK_PID_Sub_Skrin where status='A' order by cast(position as int)";
    //    }
    //    else
    //    {
    //        cmdstr = "select KK_Skrin_id,KK_Sskrin_id,UPPER(KK_Sskrin_name) as KK_Sskrin_name from KK_PID_Sub_Skrin where status='A' and KK_Sskrin_id NOT IN ('S0012','S0013','S0014','S0015') order by cast(position as int)";
    //    }

    //    SqlCommand cmd = new SqlCommand(cmdstr, con1);

    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);

    //    adp.Fill(dt);

    //    con1.Close();

    //    return dt;

    //}

    //private DataTable FillsubChildTable()

    //{

    //    DataTable dt = new DataTable();

    //    con1.Open();

    //    string cmdstr = "select KK_Skrin_id,KK_Sskrin_id,KK_Spreskrin_id,UPPER(KK_Spreskrin_name) as KK_Spreskrin_name from KK_PID_presub_Skrin where Status='A' order by cast(position as int)";

    //    SqlCommand cmd = new SqlCommand(cmdstr, con1);

    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);

    //    adp.Fill(dt);

    //    con1.Close();

    //    return dt;

    //}
    //private DataTable FillpresubChildTable()

    //{

    //    DataTable dt = new DataTable();

    //    con1.Open();

    //    string cmdstr = "select KK_Skrin_id,KK_Sskrin_id,KK_Spreskrin_id,KK_Spreskrin1_id,UPPER(KK_Spreskrin1_name) as KK_Spreskrin1_name from KK_PID_presub1_Skrin where Status='A' order by cast(position as int)";

    //    SqlCommand cmd = new SqlCommand(cmdstr, con1);

    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);

    //    adp.Fill(dt);

    //    con1.Close();

    //    return dt;

    //}
    //protected void btnGetNode_Click(object sender, EventArgs e)

    //{

    //    string chk_role = string.Empty;
    //    TreeNodeCollection tn = tvTables.CheckedNodes;


    //    for (int i = 0; i < tn.Count; i++)
    //    {
    //        //if (tn[i].Value.Length == 5)
    //        //{
    //            string ss1 = string.Empty;
    //            if (i < tn.Count - 1)
    //            {
    //                ss1 = ",";
    //            }
    //            else
    //            {
    //                ss1 = "";
    //            }
    //            chk_role += tn[i].Value.ToString() + "" + ss1;
    //        //}

    //    }
    //    TextBox2.Text = chk_role;
    //}


    protected void btnGetNode_Click1(object sender, EventArgs e)

    {
        string chk_role1 = string.Empty, ss2 = string.Empty;
        for (int i = 0; i < skrin_list.Items.Count; i++)
        {
            if (skrin_list.Items[i].Selected)//changed 1 to i 
            {
                if (i != (skrin_list.Items.Count - 1))
                {
                    //if (skrin_list.Items[i + 1].Selected)
                    if (chk_role1 != "")
                    {
                        ss2 = ",";
                    }
                    else
                    {
                        ss2 = "";
                    }
                }
                else
                {
                    //ss2 = "";
                    if (chk_role1 != "")
                    {
                        ss2 = ",";
                    }
                    else
                    {
                        ss2 = "";
                    }
                }
                chk_role1 += ss2 + "" + skrin_list.Items[i].Value.ToString(); //changed 1 to i
            }
        }
        TextBox3.Text = chk_role1;

        //DataTable dd1_rle = new DataTable();
        //dd1_rle = DBCon.Ora_Execute_table("SELECT STUFF ((SELECT ',' + KK_kumpulan_screen  FROM KK_PID_Kumpulan where KK_kumpulan_id IN ('" + TextBox3.Text.Replace(",", "','") + "') FOR XML PATH ('')  ),1,1,'')  as scode");

        //string[] k = dd1_rle.Rows[0]["scode"].ToString().Split(',');

        //foreach (TreeNode parent in tvTables.Nodes)
        //{
        //    foreach (TreeNode child in parent.ChildNodes)
        //    {

        //        for (int j = 0; j < k.Length; j++)
        //        {
        //            if (child.Value.Trim() == k[j].ToString().Trim())
        //            {
        //                child.Checked = true;
        //                parent.Checked = true;
        //                break;
        //            }
        //            else
        //            {
        //                child.Checked = false;
        //                parent.Checked = false;
        //            }
        //        }

        //        foreach (TreeNode subchild in child.ChildNodes)
        //        {
        //            for (int j = 0; j < k.Length; j++)
        //            {
        //                if (subchild.Value.Trim() == k[j].ToString().Trim())
        //                {
        //                    subchild.Checked = true;
        //                    break;
        //                }

        //            }
        //            foreach (TreeNode presubchild in subchild.ChildNodes)
        //            {
        //                for (int j = 0; j < k.Length; j++)
        //                {
        //                    if (presubchild.Value.Trim() == k[j].ToString().Trim())
        //                    {
        //                        presubchild.Checked = true;
        //                        break;
        //                    }

        //                }
        //            }
        //        }
        //    }
        //}
        //TextBox2.Text = dd1_rle.Rows[0]["scode"].ToString();
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pentadbiran/kw_user_registration.aspx");
    }

    protected void Click_bck(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pentadbiran/kw_user_registration_view.aspx");
    }

    
}