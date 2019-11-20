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


public partial class kw_sel_kod_bajet : System.Web.UI.Page
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
    string tn1 = string.Empty, tc1 = string.Empty, tc2 = string.Empty, tc3 = string.Empty, tc4 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(document).ready(function () { $(" + dd_akaun.ClientID + ").SumoSelect({ selectAll: true }); $('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        app_language();
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button5);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                if (Session["validate_success"].ToString() == "SUCCESS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('" + Session["alrt_msg"].ToString() + "',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                    Session["validate_success"] = "";
                    Session["alrt_msg"] = "";
                    Session["pro_type"] = "";
                }
                //Button2.Attributes.Add("style", "Pointer-events:none; opacity: 0.5;");
                //Button3.Attributes.Add("style", "Pointer-events:none; opacity: 0.5;");
                userid = Session["New"].ToString();
                //Button1.Visible = false;
                bind_kat_akaun();
                bind_kod_akaun();
                bind_kod_industry();
                BindData();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void bind_kod_akaun()
    {

        DataSet Ds = new DataSet();
        try
        {
            string get_qry = string.Empty;

            if (dd_kodind.SelectedValue == "1")
            {
                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A' order by kod_akaun asc";
            }
            else if (dd_kodind.SelectedValue == "2")
            {
                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun s1 inner join KW_Ref_Pelanggan on Ref_kod_akaun = kod_akaun where jenis_akaun_type != '1' and s1.Status='A' order by kod_akaun asc";
            }
            else if (dd_kodind.SelectedValue == "3")
            {
                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun s1 inner join KW_Ref_Pembekal on Ref_kod_akaun = kod_akaun where jenis_akaun_type != '1' and s1.Status='A' order by kod_akaun asc";
            }
            else if (dd_kodind.SelectedValue == "4")
            {
                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and jenis_akaun='12.01' order by kod_akaun asc";
            }
            else
            {

                get_qry = "select kod_akaun,upper((kod_akaun + ' | ' + case when LEN(nama_akaun) >= '50' then SUBSTRING ( nama_akaun ,1 , 50)+ ' ...'  else  nama_akaun end)) as name from KW_Ref_Carta_Akaun where jenis_akaun_type != '1' and Status='A' order by kod_akaun asc";

            }

            string com = get_qry;
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_akaun.DataSource = dt;
            dd_akaun.DataTextField = "name";
            dd_akaun.DataValueField = "kod_akaun";
            dd_akaun.DataBind();            

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void bind_gview(object sender, EventArgs e)
    {
        BindGrid();
        bind_kod_akaun();
        ModalPopupExtender1.Show();
        string script = " $(document).ready(function () { $(" + dd_akaun.ClientID + ").SumoSelect({ selectAll: true });$('.select2').select2();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);

    }

    void bind_kod_industry()
    {
        //DataSet Ds = new DataSet();
        //try
        //{
        //    string com = "select DISTINCT kod_industry,(kod_industry +' | ' + msic_desc) as name from Kw_kod_industry left join KW_Ref_Kod_Industri on msic_kod=kod_industry where kod_industry != ''";
        //    SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        //    DataTable dt = new DataTable();
        //    adpt.Fill(dt);
        //    dd_kodind.DataSource = dt;
        //    dd_kodind.DataTextField = "name";
        //    dd_kodind.DataValueField = "kod_industry";
        //    dd_kodind.DataBind();
        //    dd_kodind.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
    void bind_kat_akaun()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select kat_cd,UPPER(kat_akuan) kat_akuan from KW_Kategori_akaun where kat_type='02' order by kat_cd asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            kat_akaun.DataSource = dt;
            kat_akaun.DataTextField = "kat_akuan";
            kat_akaun.DataValueField = "kat_cd";
            kat_akaun.DataBind();
            kat_akaun.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void app_language()

    {
        if (Session["New"] != null)
        {
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('731','705','36','1646','1647','133')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());               
            //Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            //Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            //Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
           
        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
    }
    protected void BindData()
    {
        //PopulateTreeview();
        BindGrid();
    }

    //public class HierarchyTrees : List<HierarchyTrees.HTree>
    //{
    //    public class HTree
    //    {
    //        private string m_NodeDescription;
    //        private int m_UnderParent;
    //        private int m_LevelDepth;
    //        private int m_NodeID;

    //        public int NodeID
    //        {
    //            get { return m_NodeID; }
    //            set { m_NodeID = value; }
    //        }

    //        public string NodeDescription
    //        {
    //            get { return m_NodeDescription; }
    //            set { m_NodeDescription = value; }
    //        }
    //        public int UnderParent
    //        {
    //            get { return m_UnderParent; }
    //            set { m_UnderParent = value; }
    //        }


    //        public int LevelDepth
    //        {
    //            get { return m_LevelDepth; }
    //            set { m_LevelDepth = value; }
    //        }
    //    }
    //}
//    private void PopulateTreeview()
//    {
//        this.tvHierarchyView.Nodes.Clear();
//        HierarchyTrees hierarchyTrees = new HierarchyTrees();
//        HierarchyTrees.HTree objHTree = null;
//        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString))
//        {
//            connection.Open();
//            using (SqlCommand command =
//            new SqlCommand("SSP_GET_HIERARCHY", connection))
//            {
//                command.CommandType = System.Data.CommandType.StoredProcedure;
//                SqlDataReader reader = command.ExecuteReader
//                (System.Data.CommandBehavior.CloseConnection);
               
//                while (reader.Read())
//                {
//                    objHTree = new HierarchyTrees.HTree();
//                    //objHTree.NodeDescription = "<table cellpadding='3' cellspacing='3' width='80%' class='col-md-12 table-responsive><tr><td style='width: 50%; text-decoration:underline; color:black; text-transform:uppercase;'>Nama Akaun</td><td style='width: 30%;text-align:Right; color:black;'>Kod Akaun</td></tr></table>";
//                    objHTree.LevelDepth = int.Parse(reader["LEVEL_DEPTH"].ToString());
//                    objHTree.NodeID = int.Parse(reader["Id"].ToString());
//                    objHTree.UnderParent = int.Parse(reader["bjt_under_parent"].ToString());
//                    string kodname = string.Empty, get_cs = string.Empty, get_cs1 = string.Empty;
//                    if (reader["jenis_bajet_type"].ToString() != "1") { kodname = reader["kod_bajet"].ToString(); }
//                    if (reader["jenis_bajet"].ToString() == "12.01" || reader["jenis_bajet"].ToString() == "12.01") { get_cs = "cursor: not-allowed;"; get_cs1 = "<i class='fa fa-lock'></i>"; } // hr integration
//                    objHTree.NodeDescription = "<table cellpadding='3' cellspacing='3' width='100%' class='col-md-12 table-responsive' style='" + get_cs + "'><tr><td style='width: 20%; color:black; text-transform:uppercase;'>" + get_cs1 + " " + reader["kod_bajet"].ToString() + "</td><td style='width: 60%; color:black; text-transform:uppercase;'>" + reader["nama_bajet"].ToString() + "</td><td style='width: 30%;text-align:Right; color:black;'><asp:LinkButton OnClick='clk_new' runat='server' title='Add'><i class='fa fa-plus-square'></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton OnClick='' runat='server' title='Edit'><i class='fa fa-edit'></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton OnClick='' runat='server' title='Delete'><i class='fa fa-trash'></i></asp:LinkButton></td></tr></table>";
//                    hierarchyTrees.Add(objHTree);

//                }
//            }
//        }
//        //Iterate through Collections.
//        foreach (HierarchyTrees.HTree hTree in hierarchyTrees)
//        {
//            //Filter the collection HierarchyTrees based on 
//            //Iteration as per object Htree Parent ID 
//            HierarchyTrees.HTree parentNode = hierarchyTrees.Find
//            (delegate (HierarchyTrees.HTree emp)
//            { return emp.NodeID == hTree.UnderParent; });
//            //If parent node has child then populate the leaf node.
//            if (parentNode != null)
//            {
//                foreach (TreeNode tn in tvHierarchyView.Nodes)
//                {
//                    //If single child then match Node ID with Parent ID
//                    if (tn.Value == parentNode.NodeID.ToString())
//                    {
//                        tn.ChildNodes.Add(new TreeNode
//                        (hTree.NodeDescription.ToString(), hTree.NodeID.ToString()));
//                    }

//                    //If Node has multiple child ,
//                    //recursively traverse through end child or leaf node.
//                    if (tn.ChildNodes.Count > 0)
//                    {
//                        foreach (TreeNode ctn in tn.ChildNodes)
//                        {
//                            RecursiveChild(ctn, parentNode.NodeID.ToString(), hTree);
//                        }
//                    }
//                }
//            }
//            //Else add all Node at first level 
//            else
//            {
//                tvHierarchyView.Nodes.Add(new TreeNode
//                (hTree.NodeDescription, hTree.NodeID.ToString()));
//            }
//        }
//        //tvHierarchyView.CollapseAll();
//        tvHierarchyView.ExpandAll();

//    }

//    public void RecursiveChild
//(TreeNode tn, string searchValue, HierarchyTrees.HTree hTree)
//    {
//        if (tn.Value == searchValue)
//        {
//            tn.ChildNodes.Add(new TreeNode
//            (hTree.NodeDescription.ToString(), hTree.NodeID.ToString()));
//        }
//        if (tn.ChildNodes.Count > 0)
//        {
//            foreach (TreeNode ctn in tn.ChildNodes)
//            {
//                RecursiveChild(ctn, searchValue, hTree);
//            }
//        }
//    }

//    protected void MenuTree_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
//    {
//        try
//        {
//            int key_id = Convert.ToInt32(e.Node.Value);

//            if (e.Node.Depth == 0)
//            {
//                TreeView t = (TreeView)sender;

//                for (int i = 0; i < t.Nodes.Count; i++)
//                {
//                    if (t.Nodes[i] != e.Node)
//                    {
//                        t.Nodes[i].ExpandAll();
//                    }
//                }
//            }
//        }
//        catch
//        {
//        }
//    }



    //protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    TreeView tv = (TreeView)sender;
    //    tv.SelectedNodeStyle.ForeColor = System.Drawing.Color.MidnightBlue;
    //    tv.SelectedNodeStyle.BackColor = System.Drawing.Color.PowderBlue;
    //    tv.SelectedNodeStyle.Font.Bold = true;

    //    if (ViewState["PrevSelectedNode"] != null)
    //        tvHierarchyView.FindNode(ViewState["PrevSelectedNode"].ToString()).Text = ViewState["PrevSelectedText"].ToString();

    //    TreeNode SelectedNode = tvHierarchyView.SelectedNode;
    //    //TextBox3.Text = SelectedNode.Value;

    //    DataTable get_value = new DataTable();
    //    get_value = DBCon.Ora_Execute_table("select * From KW_Ref_kod_bajet where Id='" + SelectedNode.Value + "' and jenis_bajet NOT IN ('12.01')");
    //    if (get_value.Rows.Count != 0)
    //    {

    //        DataTable chk_kategory = new DataTable();
    //        chk_kategory = DBCon.Ora_Execute_table("select * From KW_Kategori_akaun where kat_cd='" + get_value.Rows[0]["kat_bajet"].ToString() + "'");
    //        if (chk_kategory.Rows[0]["Status"].ToString() != "T")
    //        {
    //            if (get_value.Rows[0]["jenis_bajet_type"].ToString() == "10")
    //            {
    //                get_id.Text = SelectedNode.Value;
    //                //Button2.Attributes.Add("style", "pointer-events:None; opacity: 0.5;");
    //                Button3.Attributes.Remove("style");
    //            }
    //            if (get_value.Rows[0]["jenis_bajet_type"].ToString() == "1")
    //            {
    //                get_id.Text = SelectedNode.Value;
    //                if (get_value.Rows[0]["Status"].ToString() == "A")
    //                {
    //                    //Button2.Attributes.Remove("style");
    //                }
    //                else
    //                {
    //                    //Button2.Attributes.Add("style", "pointer-events:None; opacity: 0.5;");
    //                }
    //                Button3.Attributes.Remove("style");

    //            }
    //            else
    //            {
    //                get_id.Text = SelectedNode.Value;
    //                Button3.Attributes.Remove("style");
    //                if (get_value.Rows[0]["Status"].ToString() == "A")
    //                {
    //                    //Button2.Attributes.Remove("style");
    //                }
    //                else
    //                {
    //                    //Button2.Attributes.Add("style", "pointer-events:None; opacity: 0.5;");
    //                }
    //            }


    //            DataTable ddokdicno = new DataTable();
    //            ddokdicno = DBCon.Ora_Execute_table("select * from KW_Ref_kod_bajet where jenis_bajet='" + get_value.Rows[0]["kod_bajet"].ToString() + "'");
    //            //if (ddokdicno.Rows.Count == 0 && get_value.Rows[0]["jenis_bajet_type"].ToString() != "1")
    //            if (ddokdicno.Rows.Count == 0)
    //            {
    //                if (get_value.Rows[0]["bjt_sts_kawalan"].ToString() != "Y")
    //                {
    //                    Button1.Visible = true;
    //                }
    //                else
    //                {
    //                    Button1.Visible = false;
    //                }
    //            }
    //            else
    //            {
    //                Button1.Visible = false;
    //            }

    //            if (ddokdicno.Rows.Count == 0 && get_value.Rows[0]["jenis_bajet_type"].ToString() == "1" && get_value.Rows[0]["bjt_sts_kawalan"].ToString() != "Y")
    //            {
    //                Button1.Visible = true;
    //            }
    //            else if (ddokdicno.Rows.Count == 0 && get_value.Rows[0]["jenis_bajet_type"].ToString() != "1" && get_value.Rows[0]["bjt_sts_kawalan"].ToString() != "Y")
    //            {
    //                Button1.Visible = true;
    //            }
    //            else if (ddokdicno.Rows.Count != 0 && get_value.Rows[0]["jenis_bajet_type"].ToString() != "1" && get_value.Rows[0]["bjt_sts_kawalan"].ToString() != "Y")
    //            {
    //                Button1.Visible = false;
    //            }
    //            else if (ddokdicno.Rows.Count != 0 && get_value.Rows[0]["jenis_bajet_type"].ToString() != "1" && get_value.Rows[0]["bjt_sts_kawalan"].ToString() == "Y")
    //            {
    //                Button1.Visible = false;
    //            }
    //        }
    //        else
    //        {
    //            get_id.Text = SelectedNode.Value;
    //            Button3.Attributes.Remove("style");
    //            //Button2.Attributes.Add("style", "pointer-events:None; opacity: 0.5;");
    //        }
    //    }
    //    else
    //    {
    //        Button1.Visible = false;
    //        //Button2.Attributes.Add("style", "Pointer-events:none; opacity: 0.5;");
    //        Button3.Attributes.Add("style", "Pointer-events:none; opacity: 0.5;");
    //    }


    //}

    //protected void clk_Hapus(object sender, EventArgs e)
    //{

    //    DataTable clk_hps = new DataTable();
    //    clk_hps = DBCon.Ora_Execute_table("select * From KW_Ref_kod_bajet where Id='" + get_id.Text + "'");
    //    if (clk_hps.Rows.Count != 0)
    //    {
    //        string Inssql = "Delete from KW_Ref_kod_bajet where Id = '" + get_id.Text + "'";
    //        Status = DBCon.Ora_Execute_CommamdText(Inssql);
    //        if (Status == "SUCCESS")
    //        {
    //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Dihapus Tidak Mungkin.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
    //        }
    //    }
    //    BindData();
    //}
    protected void BindGrid()
    {
        string sqry = string.Empty;
        //if (txtSearch.Text == "")
        //{
        //    sqry = "";
        //}
        //else
        //{
        //    sqry = "where m1.nama_bajet LIKE'%" + txtSearch.Text + "%' OR m1.kod_bajet LIKE'%" + txtSearch.Text + "%'";
        //}

        //string query = "select * from KW_Ref_kod_bajet " + sqry + "";

        string query = "select m1.bjt_Id,m1.jenis_bajet_type,m1.kat_bajet,m1.nama_bajet,m1.kod_bajet,m1.jenis_bajet,m1.bjt_under_parent,case when s1.opening_amt > 0 then opening_amt else '0.00' end as KW_Debit_amt,case when s1.opening_amt < 0 then opening_amt else '0.00' end as KW_kredit_amt,isHeader from KW_Ref_kod_bajet m1 left join KW_Opening_Balance s1 on s1.kod_Akaun=m1.kod_bajet and s1.kat_akaun=m1.kat_bajet and s1.set_sts='1' " + sqry + " order by kat_bajet";

        //string query = "BEGIN  with  ITERATE_NODES_RECURSIVE AS (select a1.kat_bajet,a1.kod_bajet,a1.nama_bajet,a1.Id,a1.bjt_under_parent, case when a1.bjt_under_parent = '0' then a1.nama_bajet else a2.nama_bajet end as jenis_name, 0 as LEVEL_DEPTH,a1.KW_Debit_amt,a1.KW_kredit_amt,a1.Kw_open_amt,a1.bjt_under_jenis,a1.jenis_bajet_type from dbo.KW_Ref_kod_bajet a1 left join dbo.KW_Ref_kod_bajet as a2 on a2.kod_bajet=a1.jenis_bajet where (a1.Id IN (select Id from dbo.KW_Ref_kod_bajet where (bjt_under_parent = '0')))UNION ALL select super.kat_bajet,super.kod_bajet,super.nama_bajet,super.Id,super.bjt_under_parent,case when super.bjt_under_parent = '0' then super.nama_bajet else sub.nama_bajet end as jenis_name, sub.LEVEL_DEPTH + 1  as LEVEL_DEPTH,super.KW_Debit_amt,super.KW_kredit_amt,super.Kw_open_amt,super.bjt_under_jenis,super.jenis_bajet_type from dbo.KW_Ref_kod_bajet as super INNER JOIN ITERATE_NODES_RECURSIVE as sub on sub.id=super.bjt_under_parent ) select m1.Id,m1.jenis_bajet_type,m1.kat_bajet,m1.nama_bajet,m1.kod_bajet,m1.bjt_under_parent,case when s1.opening_amt > 0 then opening_amt else '0.00' end as KW_Debit_amt,case when s1.opening_amt < 0 then opening_amt else '0.00' end as KW_kredit_amt from ITERATE_NODES_RECURSIVE m1 left join KW_Opening_Balance s1 on s1.kod_bajet=m1.kod_bajet and s1.kat_bajet=m1.kat_bajet and s1.set_sts='1' " + sqry + " order by m1.kat_bajet,m1.Id,m1.LEVEL_DEPTH; END ";

        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            BindGrid1();
                            //Calculate Sum and display in Footer Row
                            //decimal debit = dt.AsEnumerable().Sum(row => row.Field<decimal>("KW_Debit_amt"));
                            //decimal kredit = dt.AsEnumerable().Sum(row => row.Field<decimal>("KW_Kredit_amt"));
                            //GridView1.FooterRow.Cells[2].Text = "<strong>JUMLAH (RM)</strong>";
                            //GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                            //GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                            //GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                            //GridView1.FooterRow.Cells[3].Text = debit.ToString("C").Replace("RM", "").Replace("$", "");
                            //GridView1.FooterRow.Cells[4].Text = kredit.ToString("C").Replace("RM", "").Replace("$", "");
                        }

                    }
                }
            }
        }

    }
    protected void gvSelected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_refdata.PageIndex = e.NewPageIndex;
        gv_refdata.DataBind();
        BindData();
    }

    protected void BindGrid1()
    {
        //SqlCommand cmd2 = new SqlCommand("select ISNULL(ho.org_name,'') as org_name,ISNULL(dd.dis_dispose_type_cd,'') as dis_dispose_type_cd,rk.ast_kategori_desc,rja.ast_jeniaset_desc,aca.cas_asset_desc,a.sas_asset_id,a.sas_curr_price_amt, a.sas_asset_cat_cd,a.sas_asset_sub_cat_cd,a.sas_asset_type_cd,a.sas_asset_cd,a.sas_org_id, case a.sas_asset_cat_cd when '01' then (select FORMAT(com_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd) when '02' then (select FORMAT(car_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select FORMAT(inv_reg_dt,'dd/MM/yyyy', 'en-us') as reg_dt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a1, case a.sas_asset_cat_cd when '01' then (select DATEDIFF(day,com_reg_dt,GETDATE()) as u_dt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '02' then (select DATEDIFF(day,car_reg_dt,GETDATE()) as u_dt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select  DATEDIFF(day,inv_reg_dt,GETDATE()) as u_dt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a2, case a.sas_asset_cat_cd when '01' then (select com_price_amt from ast_component where com_asset_cat_cd=a.sas_asset_cat_cd and com_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and com_asset_type_cd=a.sas_asset_type_cd and com_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '02' then (select car_price_amt from ast_car where car_asset_cat_cd=a.sas_asset_cat_cd and car_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and car_asset_type_cd=a.sas_asset_type_cd and car_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') when '03' then (select inv_price_amt from ast_inventory where inv_asset_cat_cd=a.sas_asset_cat_cd and inv_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and inv_asset_type_cd=a.sas_asset_type_cd and inv_asset_cd=a.sas_asset_cd and sas_staff_no='" + Session["New"].ToString() + "') end as a3 from (select * from ast_staff_asset  where sas_cond_sts_cd = '03' and ISNULL(sas_dispose_cfm_ind,'' ) !='Y' and sas_staff_no='" + Session["New"].ToString() + "') as a left join Ref_ast_kategori as rk on rk.ast_kategori_code=a.sas_asset_cat_cd left join Ref_ast_jenis_aset as rja on rja.ast_jeniaset_Code=a.sas_asset_type_cd left join ast_cmn_asset as aca on aca.cas_asset_cd=a.sas_asset_cd and aca.cas_asset_cat_cd=a.sas_asset_cat_cd and aca.cas_asset_sub_cat_cd=a.sas_asset_sub_cat_cd and aca.cas_asset_type_cd=a.sas_asset_type_cd left join hr_organization as ho on ho.org_gen_id=a.sas_org_id left join ast_dispose as dd on dd.dis_asset_id=a.sas_asset_id", con);
        SqlCommand cmd2 = new SqlCommand("select *,case when jenis_bajet_type='1' then kat_bajet else kod_bajet end as kod_akaun1 from KW_Ref_kod_bajet", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        gv_refdata.DataSource = ds2;
        gv_refdata.DataBind();

    }

    protected void Add_profile(object sender, EventArgs e)
    {
        Response.Redirect("../kewengan/kw_carta_akaun.aspx");
    }

   
    protected void ctk_values(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt = DBCon.Ora_Execute_table("select *,case when jenis_bajet_type='1' then kat_bajet else kod_bajet end as kod_akaun1 from KW_Ref_kod_bajet");
        Rptviwer_baki.Reset();
        ds.Tables.Add(dt);

        List<DataRow> listResult = dt.AsEnumerable().ToList();
        listResult.Count();
        int countRow = 0;
        countRow = listResult.Count();

        Rptviwer_baki.LocalReport.DataSources.Clear();
        if (countRow != 0)
        {

            StringBuilder builder = new StringBuilder();
            string strFileName = string.Format("{0}.{1}", "BAJET_" + DateTime.Now.ToString("yyyyMMdd") + "", "csv");
            builder.Append("NAMA BAJET ,KOD BAJET" + Environment.NewLine);
            string oamt = string.Empty;
            foreach (GridViewRow row in gv_refdata.Rows)
            {
                string kodakaun = ((Label)row.FindControl("lbl1")).Text.ToString();
                string akaunname = ((Label)row.FindControl("lbl2")).Text.ToString();
                string openamt = ((Label)row.FindControl("lbl3")).Text.ToString();
                string type = ((Label)row.FindControl("lbl4")).Text.ToString();
                if (type == "1")
                {
                    oamt = "";
                }
                else
                {
                    oamt = openamt;
                }

                builder.Append(akaunname.Replace(",", "").ToUpper() + "," + kodakaun + Environment.NewLine);
            }
            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
            Response.Write(builder.ToString());
            Response.End();

        }
        else if (countRow == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Rekod tidak dijumpai. Sila Pastikan Semua Maklumat Dimasukkan Dengan Betul');", true);
        }

    }

    protected void clk_new(object sender, EventArgs e)
    {
        try
        {
            
            LinkButton btnButton = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
            System.Web.UI.WebControls.Label og_genid = (System.Web.UI.WebControls.Label)gvRow.FindControl("og_genid");
            string ogid = og_genid.Text;

            DataTable get_value1_1 = new DataTable();
            get_value1_1 = DBCon.Ora_Execute_table("select * From KW_Ref_kod_bajet where bjt_Id='" + og_genid.Text + "'");

            DataTable ddokdicno = new DataTable();
            ddokdicno = DBCon.Ora_Execute_table("select * from KW_Ref_kod_bajet where jenis_bajet='" + get_value1_1.Rows[0]["kod_bajet"].ToString() + "'");
            //if (ddokdicno.Rows.Count == 0 && get_value.Rows[0]["jenis_bajet_type"].ToString() != "1")
            BindGrid();
            string script = " $(function () {$('.select2').select2()})";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
            ModalPopupExtender1.Show();

            if (get_value1_1.Rows[0]["jenis_bajet_type"].ToString() != "1")
            {
                if (ddokdicno.Rows.Count == 0)
                {
                    if (get_value1_1.Rows[0]["bjt_sts_kawalan"].ToString() != "Y")
                    {
                        dd_list_sts.Attributes.Remove("Readonly");
                        dd_list_sts.Attributes.Remove("Style");

                    }
                    else
                    {
                        dd_list_sts.Attributes.Add("Readonly", "Readonly");
                        dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
                    }
                }
                else
                {
                    dd_list_sts.Attributes.Add("Readonly", "Readonly");
                    dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
                }

                if (ddokdicno.Rows.Count == 0 && get_value1_1.Rows[0]["jenis_bajet_type"].ToString() == "1" && get_value1_1.Rows[0]["bjt_sts_kawalan"].ToString() != "Y")
                {
                    dd_list_sts.Attributes.Remove("Readonly");
                    dd_list_sts.Attributes.Remove("Style");
                }
                else if (ddokdicno.Rows.Count == 0 && get_value1_1.Rows[0]["jenis_bajet_type"].ToString() != "1" && get_value1_1.Rows[0]["bjt_sts_kawalan"].ToString() != "Y")
                {
                    dd_list_sts.Attributes.Remove("Readonly");
                    dd_list_sts.Attributes.Remove("Style");
                }
                else if (ddokdicno.Rows.Count != 0 && get_value1_1.Rows[0]["jenis_bajet_type"].ToString() != "1" && get_value1_1.Rows[0]["bjt_sts_kawalan"].ToString() != "Y")
                {
                    dd_list_sts.Attributes.Add("Readonly", "Readonly");
                    dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
                }
                else if (ddokdicno.Rows.Count != 0 && get_value1_1.Rows[0]["jenis_bajet_type"].ToString() != "1" && get_value1_1.Rows[0]["bjt_sts_kawalan"].ToString() == "Y")
                {
                    dd_list_sts.Attributes.Add("Readonly", "Readonly");
                    dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
                }
            }
            else
            {
                dd_list_sts.Attributes.Add("Readonly", "Readonly");
                dd_list_sts.Attributes.Add("Style", "pointer-events:None;");
            }
            hdr_txt.Text = "New Kod Bajet";
            DataTable get_value1 = new DataTable();
            get_value1 = DBCon.Ora_Execute_table("select * From KW_Ref_kod_bajet where bjt_Id='" + og_genid.Text + "'");
            if (get_value1.Rows.Count != 0)
            {
                
                kat_akaun.SelectedValue = get_value1.Rows[0]["kat_bajet"].ToString();
                TextBox3.Text = get_value1.Rows[0]["kod_bajet"].ToString();
                TextBox2.Text = get_value1.Rows[0]["nama_bajet"].ToString();
                get_cd.Text = get_value1.Rows[0]["kod_bajet"].ToString();
                //dd_kodind.SelectedValue = get_value1.Rows[0]["bjt_ct_kod_industry"].ToString();
                dd_list_sts.SelectedValue = get_value1.Rows[0]["bjt_Status"].ToString();

                if (get_value1.Rows[0]["bjt_kkk_rep"].ToString() == "1")
                {
                    CheckBox1.Checked = true;
                }
                else
                {
                    CheckBox1.Checked = false;
                }

                if (get_value1.Rows[0]["bjt_PAL_rep"].ToString() == "1")
                {
                    CheckBox2.Checked = true;
                }
                else
                {
                    CheckBox2.Checked = false;
                }

                if (get_value1.Rows[0]["bjt_AT_rep"].ToString() == "1")
                {
                    CheckBox3.Checked = true;
                }
                else
                {
                    CheckBox3.Checked = false;
                }

                if (get_value1.Rows[0]["bjt_AP_rep"].ToString() == "1")
                {
                    CheckBox4.Checked = true;
                }
                else
                {
                    CheckBox4.Checked = false;
                }

                if (get_value1.Rows[0]["bjt_COGS_rep"].ToString() == "1")
                {
                    CheckBox5.Checked = true;
                }
                else
                {
                    CheckBox5.Checked = false;
                }

                if (get_value1.Rows[0]["jenis_bajet_type"].ToString() != "1")
                {
                    ss1.Visible = true;
                }
                else
                {
                    ss1.Visible = false;
                }
            }
            TextBox1.Text = "";
            TextBox1.Focus();
            ver_id.Text = "0";
            show_ddvalue();
            get_id.Text = og_genid.Text;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    //protected void clk_yes(object sender, EventArgs e)
    //{
    //    BindGrid();
    //    ModalPopupExtender1.Show();
    //    show_ddvalue();
    //}

  
    void show_ddvalue()
    {
        //if (RadioButton1.Checked == true)
        //{
        //    set_kakaun.Visible = true;
        //}
        //else
        //{
        //    set_kakaun.Visible = false;
        //}
    }

   
    protected void clk_update(object sender, EventArgs e)
    {
        LinkButton btnButton = sender as LinkButton;
        GridViewRow gvRow = (GridViewRow)btnButton.NamingContainer;
        System.Web.UI.WebControls.Label og_genid = (System.Web.UI.WebControls.Label)gvRow.FindControl("og_genid");
        string ogid = og_genid.Text;
        DataTable get_value = new DataTable();
        get_value = DBCon.Ora_Execute_table("select * From KW_Ref_kod_bajet where bjt_Id='" + og_genid.Text + "'");
        if (get_value.Rows.Count != 0)
        {
            hdr_txt.Text = "Update Kod Bajet";
            BindGrid();
            ModalPopupExtender1.Show();
            Button4.Text = "Kemaskini";

            ver_id.Text = "1";

            //Button6.Visible = true;

            DataTable get_value2 = new DataTable();
            get_value2 = DBCon.Ora_Execute_table("select * From KW_Ref_kod_bajet where kod_bajet='" + get_value.Rows[0]["jenis_bajet"].ToString() + "'");
            if (get_value2.Rows.Count != 0)
            {
                TextBox2.Text = get_value2.Rows[0]["nama_bajet"].ToString();
                TextBox3.Text = get_value2.Rows[0]["kod_bajet"].ToString();
            }

            kat_akaun.SelectedValue = get_value.Rows[0]["kat_bajet"].ToString();
            TextBox1.Text = get_value.Rows[0]["nama_bajet"].ToString();
            //dd_akaun.SelectedValue = get_value.Rows[0]["bjt_under_jenis"].ToString();
            //dd_kodind.SelectedValue = get_value.Rows[0]["bjt_ct_kod_industry"].ToString();

            string[] thisArray = get_value.Rows[0]["kod_akaun"].ToString().Split(',');
            List<string> myList = new List<string>();
            myList.AddRange(thisArray);

            for (int i = 0; i < dd_akaun.Items.Count; i++)
            {
                if (myList.Contains(dd_akaun.Items[i].Value))
                {
                    dd_akaun.Items[i].Selected = true;
                }
            }

            dd_list_sts.SelectedValue = get_value.Rows[0]["bjt_Status"].ToString();
            if (get_value.Rows[0]["bjt_Susu_nilai"].ToString() == "1")
            {
                RadioButton1.Checked = true;
            }
            else
            {
                RadioButton1.Checked = false;
            }

            if (get_value.Rows[0]["bjt_kkk_rep"].ToString() == "1")
            {
                CheckBox1.Checked = true;
            }
            else
            {
                CheckBox1.Checked = false;
            }

            if (get_value.Rows[0]["bjt_PAL_rep"].ToString() == "1")
            {
                CheckBox2.Checked = true;
            }
            else
            {
                CheckBox2.Checked = false;
            }

            if (get_value.Rows[0]["bjt_AT_rep"].ToString() == "1")
            {
                CheckBox3.Checked = true;
            }
            else
            {
                CheckBox3.Checked = false;
            }

            if (get_value.Rows[0]["bjt_AP_rep"].ToString() == "1")
            {
                CheckBox4.Checked = true;
            }
            else
            {
                CheckBox4.Checked = false;
            }
            if (get_value.Rows[0]["bjt_COGS_rep"].ToString() == "1")
            {
                CheckBox5.Checked = true;
            }
            else
            {
                CheckBox5.Checked = false;
            }

            if (get_value.Rows[0]["jenis_bajet_type"].ToString() == "1")
            {
                TextBox1.Attributes.Add("Readonly", "Readonly");
                ss1.Visible = false;
            }
            else
            {
                TextBox1.Attributes.Remove("Readonly");
                ss1.Visible = true;
            }
            get_id.Text = og_genid.Text;
            TextBox1.Focus();
            show_ddvalue();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        System.Web.UI.WebControls.Label lbl1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label1");
        System.Web.UI.WebControls.Label clmn1 = (System.Web.UI.WebControls.Label)e.Row.FindControl("bal_type");
        System.Web.UI.WebControls.Label clmn2 = (System.Web.UI.WebControls.Label)e.Row.FindControl("kat_cd");
        System.Web.UI.WebControls.Label clmn3 = (System.Web.UI.WebControls.Label)e.Row.FindControl("og_genid");
        System.Web.UI.WebControls.Label clmn4 = (System.Web.UI.WebControls.Label)e.Row.FindControl("Label2");
        LinkButton Button3 = e.Row.FindControl("LinkButton1") as LinkButton;
        LinkButton Button1 = e.Row.FindControl("LinkButton2") as LinkButton;
        LinkButton Button2 = e.Row.FindControl("lnkView") as LinkButton;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if(lbl1.Text == "1")
            {
                e.Row.BackColor = Color.FromName("#D4D4D4");
            }
            else if (lbl1.Text == "2")
            {
                e.Row.BackColor = Color.FromName("#D2D7D3");
                clmn1.Attributes.Add("style", "padding-left:10px;");
                clmn2.Attributes.Add("style", "padding-left:10px;");
            }
            else if (lbl1.Text == "3")
            {
                e.Row.BackColor = Color.FromName("#E7E7E7");
                clmn1.Attributes.Add("style", "padding-left:25px;");
                clmn2.Attributes.Add("style", "padding-left:25px;");
            }
            else if (lbl1.Text == "4")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:40px;");
                clmn2.Attributes.Add("style", "padding-left:40px;");
            }
            else if (lbl1.Text == "5")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:55px;");
                clmn2.Attributes.Add("style", "padding-left:55px;");
            }
            else if (lbl1.Text == "6")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:70px;");
                clmn2.Attributes.Add("style", "padding-left:70px;");
            }
            else if (lbl1.Text == "7")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:85px;");
                clmn2.Attributes.Add("style", "padding-left:85px;");
            }
            else if (lbl1.Text == "8")
            {
                e.Row.BackColor = Color.FromName("#F5F5F5");
                clmn1.Attributes.Add("style", "padding-left:100px;");
                clmn2.Attributes.Add("style", "padding-left:100px;");
            }

            if (clmn4.Text == "1")
            {
                e.Row.BackColor = Color.FromName("#FFFFFF");
            }
           

            string get_id = string.Empty;

            DataTable get_value = new DataTable();
            get_value = DBCon.Ora_Execute_table("select * From KW_Ref_kod_bajet where bjt_Id='" + clmn3.Text + "' and jenis_bajet NOT IN ('12.01')");
            if (get_value.Rows.Count != 0)
            {

                DataTable chk_kategory = new DataTable();
                chk_kategory = DBCon.Ora_Execute_table("select * From KW_Kategori_akaun where kat_cd='" + get_value.Rows[0]["kat_bajet"].ToString() + "'");
                if (chk_kategory.Rows[0]["Status"].ToString() != "T")
                {
                    if (get_value.Rows[0]["jenis_bajet_type"].ToString() == "10")
                    {
                        get_id = clmn3.Text;
                        Button2.Attributes.Add("style", "pointer-events:None; opacity: 0.5;");
                        Button3.Attributes.Remove("style");
                    }
                    if (get_value.Rows[0]["jenis_bajet_type"].ToString() == "1")
                    {
                        get_id = clmn3.Text;
                        if (get_value.Rows[0]["bjt_Status"].ToString() == "A")
                        {
                            Button2.Attributes.Remove("style");
                        }
                        else
                        {
                            Button2.Attributes.Add("style", "pointer-events:None; opacity: 0.5;");
                        }
                        Button3.Attributes.Remove("style");

                    }
                    else
                    {
                        get_id = clmn3.Text;
                        Button3.Attributes.Remove("style");
                        if (get_value.Rows[0]["bjt_Status"].ToString() == "A")
                        {
                            Button2.Attributes.Remove("style");
                        }
                        else
                        {
                            Button2.Attributes.Add("style", "pointer-events:None; opacity: 0.5;");
                        }
                    }


                    DataTable ddokdicno = new DataTable();
                    ddokdicno = DBCon.Ora_Execute_table("select * from KW_Ref_kod_bajet where jenis_bajet='" + get_value.Rows[0]["kod_bajet"].ToString() + "'");
                    //if (ddokdicno.Rows.Count == 0 && get_value.Rows[0]["jenis_bajet_type"].ToString() != "1")
                    if (ddokdicno.Rows.Count == 0)
                    {
                        if (get_value.Rows[0]["bjt_sts_kawalan"].ToString() != "Y")
                        {
                            Button1.Visible = true;
                        }
                        else
                        {
                            Button1.Visible = false;
                        }
                    }
                    else
                    {
                        Button1.Visible = false;
                    }

                    if (ddokdicno.Rows.Count == 0 && get_value.Rows[0]["jenis_bajet_type"].ToString() == "1" && get_value.Rows[0]["bjt_sts_kawalan"].ToString() != "Y")
                    {
                        Button1.Visible = true;
                    }
                    else if (ddokdicno.Rows.Count == 0 && get_value.Rows[0]["jenis_bajet_type"].ToString() != "1" && get_value.Rows[0]["bjt_sts_kawalan"].ToString() != "Y")
                    {
                        Button1.Visible = true;
                    }
                    else if (ddokdicno.Rows.Count != 0 && get_value.Rows[0]["jenis_bajet_type"].ToString() != "1" && get_value.Rows[0]["bjt_sts_kawalan"].ToString() != "Y")
                    {
                        Button1.Visible = false;
                    }
                    else if (ddokdicno.Rows.Count != 0 && get_value.Rows[0]["jenis_bajet_type"].ToString() != "1" && get_value.Rows[0]["bjt_sts_kawalan"].ToString() == "Y")
                    {
                        Button1.Visible = false;
                    }
                }
                else
                {
                    get_id = clmn3.Text;
                    Button3.Attributes.Remove("style");
                    Button2.Attributes.Add("style", "pointer-events:None; opacity: 0.5;");
                }
            }
            else
            {
                Button1.Visible = false;
                Button2.Attributes.Add("style", "Pointer-events:none; opacity: 0.5;");
                Button3.Attributes.Add("style", "Pointer-events:none; opacity: 0.5;");
            }


        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        clr_txt();
    }

    void clr_txt()
    {
        bind_kod_akaun();
        BindGrid();
        Button4.Text = "Simpan";
        hdr_txt.Text = "";
        kat_akaun.SelectedValue = "";
        TextBox1.Attributes.Remove("Readonly");
        TextBox2.Text = "";
        TextBox1.Text = "";
        dd_kodind.SelectedValue = "0";
        dd_akaun.ClearSelection();
        RadioButton1.Checked = false;
        //set_kakaun.Visible = false;
        ss1.Visible = false;
        CheckBox1.Checked = false;
        CheckBox2.Checked = false;
        CheckBox3.Checked = false;
        CheckBox4.Checked = false;
        CheckBox5.Checked = false;
        dd_list_sts.SelectedValue = "A";
    }
    protected void clk_submit(object sender, EventArgs e)
    {
        if (kat_akaun.SelectedValue != "" && TextBox1.Text != "")
        {
            string set_cnt = string.Empty, set_cnt1 = string.Empty, mcnt = string.Empty;
            DataTable cnt_no = new DataTable();

            string rcount = string.Empty, rcount1 = string.Empty;
            int count = 0, count1 = 1, pyr = 0, prdt = 0;
            string s_s1 = string.Empty;
            foreach (ListItem li in dd_akaun.Items)
            {
                if (li.Selected == true)
                {
                    count++;
                }
                rcount = count.ToString();
            }
            string selectedValues = string.Empty;
            foreach (ListItem li in dd_akaun.Items)
            {
                if (li.Selected == true)
                {
                    if (Int32.Parse(rcount) > count1)
                    {
                        s_s1 = ",";
                    }
                    else
                    {
                        s_s1 = "";
                    }

                    selectedValues += li.Value + s_s1;

                    count1++;
                }
                rcount1 = count1.ToString();
            }

            string susnilai = string.Empty, ckbox1 = string.Empty, ckbox2 = string.Empty, ckbox3 = string.Empty, ckbox4 = string.Empty, ckbox5 = string.Empty, ckbox6 = string.Empty;
            if (RadioButton1.Checked == true)
            {
                susnilai = "1";
            }
            else
            {
                susnilai = "0";
            }

            if (CheckBox1.Checked == true)
            {
                ckbox1 = "1";
            }
            else
            {
                ckbox1 = "0";
            }

            if (CheckBox2.Checked == true)
            {
                ckbox2 = "1";
            }
            else
            {
                ckbox2 = "0";
            }

            if (CheckBox3.Checked == true)
            {
                ckbox3 = "1";
            }
            else
            {
                ckbox3 = "0";
            }

            if (CheckBox4.Checked == true)
            {
                ckbox4 = "1";
            }
            else
            {
                ckbox4 = "0";
            }

            if (CheckBox5.Checked == true)
            {
                ckbox5 = "1";
            }
            else
            {
                ckbox5 = "0";
            }

            if (ver_id.Text == "0")
            {
                if (get_cd.Text != "")
                {

                    //cnt_no = DBCon.Ora_Execute_table("select Id as cnt,(jenis_bajet_type + 1) as rcnt,kod_bajet,bjt_under_jenis,jenis_bajet_type From KW_Ref_kod_bajet where kat_akaun='" + kat_akaun.SelectedValue + "' and kod_bajet='" + get_cd.Text + "'");
                    cnt_no = DBCon.Ora_Execute_table("select bjt_Id as cnt,(jenis_bajet_type + 1) as rcnt,kod_bajet,bjt_under_jenis,jenis_bajet_type From KW_Ref_kod_bajet where kod_bajet='" + get_cd.Text + "'");

                    set_cnt1 = cnt_no.Rows[0]["cnt"].ToString();
                    mcnt = cnt_no.Rows[0]["rcnt"].ToString();
                    set_cnt = get_cd.Text;
                    //set_cnt1 = "1";
                }
                else
                {
                    set_cnt1 = "0";
                    mcnt = "1";
                    set_cnt = "00";
                }

                string sno1 = string.Empty, sno2 = string.Empty, sno3 = string.Empty, cnt_value = string.Empty;
                DataTable cnt_no1 = new DataTable();
                cnt_no1 = DBCon.Ora_Execute_table("select top(1) (ISNULL(kod_bajet_cnt,'') +1) as mcnt From KW_Ref_kod_bajet where jenis_bajet='" + get_cd.Text + "' order by cast(kod_bajet_cnt as int) desc");

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

                string sso1 = string.Empty, sso2 = string.Empty, sso3 = string.Empty;
                if (set_cnt1 == "0")
                {
                    sso1 = "";
                }
                else
                {
                    if (cnt_no.Rows[0]["jenis_bajet_type"].ToString() == "1")
                    {
                        sso1 = chk_role + "," + cnt_no.Rows[0]["kod_bajet"].ToString();
                    }
                    else
                    {
                        sso1 = chk_role + "," + cnt_no.Rows[0]["bjt_under_jenis"].ToString();
                    }
                }

                DataTable ddokdicno = new DataTable();
                ddokdicno = DBCon.Ora_Execute_table("select * From KW_Ref_kod_bajet where kat_bajet='" + kat_akaun.SelectedValue + "' and kod_bajet='" + TextBox1.Text + "'");
                if (ddokdicno.Rows.Count == 0)
                {


                    string Inssql = "insert into KW_Ref_kod_bajet(kat_bajet,nama_bajet,kod_bajet,jenis_bajet,bjt_under_parent,bjt_Debit_amt,bjt_kredit_amt,bjt_open_amt,jenis_bajet_type,bjt_under_jenis,bjt_Status,bjt_crt_id,bjt_cr_dt,bjt_Susu_nilai,kod_bajet_cnt,bjt_kkk_rep,bjt_PAL_rep,bjt_AT_rep,bjt_AP_rep,bjt_COGS_rep,bjt_ct_kod_industry,Kod_akaun,isHeader) values ('" + kat_akaun.SelectedValue + "','" + TextBox1.Text.Replace("'", "''") + "','" + chk_role + "','" + get_cd.Text + "','" + set_cnt1 + "','0.00','0.00','0.00','" + mcnt + "','" + TextBox3.Text + "','" + dd_list_sts.SelectedValue + "','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + susnilai + "','" + cnt_value + "','" + ckbox1 + "','" + ckbox2 + "','" + ckbox3 + "','" + ckbox4 + "','" + ckbox5 + "','" + dd_kodind.SelectedValue + "','"+ selectedValues + "','"+ susnilai + "')";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql);
                    if (Status == "SUCCESS")
                    {
                        //Session["alrt_msg"] = "Rekod Berjaya Disimpan.";
                        //Session["validate_success"] = "SUCCESS";
                        //Response.Redirect("../kewengan/kw_carta_akaun_view.aspx");
                        clr_txt();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan.',{'type': 'confirmation','title': 'Success'});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Memasukkan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Telah Wujud.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                string Inssql = "UPDATE KW_Ref_kod_bajet set Kod_akaun='"+ selectedValues + "',isHeader='"+ susnilai +"',bjt_ct_kod_industry='" + dd_kodind.SelectedValue + "',kat_bajet='" + kat_akaun.SelectedValue + "',bjt_under_jenis='" + TextBox3.Text + "',nama_bajet='" + TextBox1.Text.Replace("'", "''") + "',bjt_Susu_nilai='" + susnilai + "',bjt_kkk_rep='" + ckbox1 + "',bjt_PAL_rep='" + ckbox2 + "',bjt_AT_rep='" + ckbox3 + "',bjt_AP_rep='" + ckbox4 + "',bjt_COGS_rep='" + ckbox5 + "',bjt_Status='" + dd_list_sts.SelectedValue + "',bjt_upd_id='" + userid + "',bjt_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where bjt_Id = '" + get_id.Text + "'";
                Status = DBCon.Ora_Execute_CommamdText(Inssql);
                if (Status == "SUCCESS")
                {
                    cnt_no = DBCon.Ora_Execute_table("select * From KW_Ref_kod_bajet where bjt_Id = '" + get_id.Text + "'");
                    string Inssql2 = "UPDATE KW_Ref_kod_bajet set bjt_ct_kod_industry='" + dd_kodind.SelectedValue + "',bjt_kkk_rep='" + ckbox1 + "',bjt_PAL_rep='" + ckbox2 + "',bjt_AT_rep='" + ckbox3 + "',bjt_AP_rep='" + ckbox4 + "',bjt_COGS_rep='" + ckbox5 + "',bjt_upd_id='" + userid + "',bjt_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where jenis_bajet = '" + cnt_no.Rows[0]["kod_bajet"].ToString() + "'";
                    Status = DBCon.Ora_Execute_CommamdText(Inssql2);

                    //Session["alrt_msg"] = "Rekod Berjaya Dikemaskini.";
                    //Session["validate_success"] = "SUCCESS";
                    //Response.Redirect("../kewengan/kw_carta_akaun_view.aspx");
                    clr_txt();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini.',{'type': 'confirmation','title': 'Success'});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Memasukkan.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);

        }


    }
    protected void btn_hups_Click(object sender, EventArgs e)
    {
        DataTable clk_hps = new DataTable();
        clk_hps = DBCon.Ora_Execute_table("select * From KW_Ref_kod_bajet where bjt_Id='" + get_id.Text + "'");
        if (clk_hps.Rows.Count != 0)
        {
            string Inssql = "Delete from KW_Ref_kod_bajet where bjt_Id = '" + get_id.Text + "'";
            Status = DBCon.Ora_Execute_CommamdText(Inssql);
            if (Status == "SUCCESS")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dihapuskan.',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Dihapus Tidak Mungkin.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }
        BindData();
    }
}