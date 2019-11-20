using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;



public class DBConnection
{
    public string name;
    string Status = string.Empty;
    string dstat = string.Empty;
    SqlConnection SqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ToString());
    SqlCommand SqlCmd = new SqlCommand();

    //SqlConnection SqlConn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Koopdb"].ToString());
    //SqlCommand SqlCmd1 = new SqlCommand();



    private static  DataTable _DtAddCart;    
    public DataTable DtAddCart
    {
        get
        {
            return _DtAddCart;
        }
        set
        {
           _DtAddCart = value;
        }
    }

    private static DataTable _DtCheckout;
    public DataTable DtCheckout
    {
        get
        {
            return _DtCheckout;
        }
        set
        {
            _DtCheckout = value;
        }
    }
    public void connsting()
    {
       
        //String KoopConnectionString = Encry. GetDecryptedData(ConfigurationManager.ConnectionStrings["KoopConnectionString"].ToString());
        //SqlConn = new SqlConnection(KoopConnectionString);
    }
   
    
     public void Open()
    {
        connsting();
         SqlConn.Open();
    }
   
    public void close()
    {
        connsting();
        if (SqlConn.State != ConnectionState.Closed)
            SqlConn.Close();
    }

    public DataTable Sql_Execute_table(string Sql)
    {
        DataTable Dt = new DataTable();
        try
        {
            SqlDataAdapter Sqlda = new SqlDataAdapter(Sql, SqlConn);
            Sqlda.Fill(Dt);
        }

        catch (Exception ex)
        {
        }
        return Dt;
    }

   

    //public Reportset BankinSlipRpt(string Query)
    //{


    //    SqlCmd.CommandText = Query;
    //    SqlCmd.Connection = SqlConn;
    //    SqlCmd.CommandType = CommandType.Text;

    //    using (SqlDataAdapter sda = new SqlDataAdapter())
    //    {
    //        SqlCmd.Connection = SqlConn;

    //        sda.SelectCommand = SqlCmd;
    //        using (Reportset dsCustomers = new Reportset())
    //        {
    //            sda.Fill(dsCustomers, "Bankslip");
    //            return dsCustomers;
    //        }
    //    }

    //}


    public void Execute_CommamdText(string sqlCmd)
    {
        connsting();
        try
        {
            
            SqlCmd.CommandType = CommandType.Text;
            SqlCmd.CommandText = sqlCmd;
            SqlCmd.CommandTimeout = 120;
            SqlCmd.Connection = SqlConn;
            SqlConn.Open();
            SqlCmd.ExecuteNonQuery();
        }
        finally
        {
            close();
        }

    }
    public void Execute_NonQuery(string spName, SqlParameter[] SqlParams)
    {
        connsting();
        try
        {
            SetupCommand(spName, SqlParams);
            SqlConn.Open();
            SqlCmd.ExecuteNonQuery();
        }
        catch
        { 
        }
        finally
        {
            close();
        }
    }

    public DataTable Execute_DataSet(string spName, SqlParameter[] SqlParams)
    {
        SetupCommand(spName, SqlParams);
        SqlDataAdapter da = new SqlDataAdapter(SqlCmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }


    public string Ora_Execute_CommamdText(string Query)
    {
        string Status = string.Empty;
        string dstat = string.Empty;
        try
        {
            SqlConn.Open();
            SqlCmd.Connection = SqlConn;
            SqlCmd.CommandText = Query;
            SqlCmd.CommandTimeout = 120;
            SqlCmd.ExecuteNonQuery();
            Status = "SUCCESS";
            dstat = "SUCCESS";
        }
        catch (Exception ex)
        {
            Status = "FAIL";
           
        }
        finally
        {
            SqlConn.Close();
        }
        return Status;

    }

    public DataTable Ora_Execute_table(string Sql)
    {
        DataTable Dt = new DataTable();
        try
        {
            SqlDataAdapter Orada = new SqlDataAdapter(Sql, SqlConn);
            Orada.Fill(Dt);
        }

        catch (Exception ex)
        {
        }
        return Dt;
    }


    //public DataTable BowOra_Execute_table(string Sql)
    //{
    //    DataTable Dt = new DataTable();
    //    try
    //    {
    //        SqlDataAdapter Orada = new SqlDataAdapter(Sql, SqlConn1);
    //        Orada.Fill(Dt);
    //    }

    //    catch (Exception ex)
    //    {
    //    }
    //    return Dt;
    //}


    //public string BowOra_Execute_CommamdText(string Query)
    //{

    //    try
    //    {
    //        SqlConn1.Open();
    //        SqlCmd1.Connection = SqlConn1;
    //        SqlCmd1.CommandText = Query;
    //        SqlCmd1.CommandTimeout = 120;
    //        SqlCmd1.ExecuteNonQuery();
    //        Status = "SUCCESS";
    //        dstat = "SUCCESS";
    //    }
    //    catch (Exception ex)
    //    {
    //        Status = "FAIL";

    //    }
    //    finally
    //    {
    //        SqlConn1.Close();
    //    }
    //    return Status;

    //}

    public DataSet Execute_Query(string SQL)
    {
        SqlDataAdapter da = new SqlDataAdapter(SQL, SqlConn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    public DataSet Execute_Query1(string SQL)
    {
        SqlDataAdapter da = new SqlDataAdapter(SQL, SqlConn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        return ds;
    }



    public void Execute_NonQueryAndPersistCon(string spName, SqlParameter[] SqlParams)
    {
        SetupCommand(spName, SqlParams);
        SqlCmd.ExecuteNonQuery();
        SqlCmd.Parameters.Clear();
    }

    public string Execute_Scalar(string spName, SqlParameter[] SqlParams)
    {
        string retStr = string.Empty;
        connsting();
        try
        {
            SetupCommand(spName, SqlParams);
            SqlConn.Open();
            retStr = SqlCmd.ExecuteScalar().ToString();
        }
        finally
        {
            close();
        }

        return retStr;
    }

    public SqlDataReader Execute_DataReader(string spName, SqlParameter[] SqlParams)
    {
        connsting();
        SetupCommand(spName, SqlParams);
        SqlConn.Open();
        SqlDataReader dr = SqlCmd.ExecuteReader();
        return dr;

    }

    public DataSet Execute_DataSet(string spName)
    {

        SqlDataAdapter da = new SqlDataAdapter(SqlCmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    public void AddParametersToCmd(SqlParameter[] SqlParams)
    {
        try
        {
            foreach (SqlParameter SqlParam in SqlParams)
            {
                SqlCmd.Parameters.Add(SqlParam);
            }
        }
        catch(Exception ex)
        {
        }
    }

    public void ClearParameters()
    {
        SqlCmd.Parameters.Clear();
    }

    public SqlDataReader Execute_Reader(string Sql)
    {
        SqlCmd.Connection = SqlConn;
        SqlCmd.CommandType = CommandType.Text;
        SqlCmd.CommandText = Sql;
        if (SqlConn.State == ConnectionState.Closed)
        {
            SqlConn.Open();
        }
        SqlDataReader dr = SqlCmd.ExecuteReader();
        return dr;


    }

    public void SetupCommand(String spName, SqlParameter[] SqlParams)
    {
        connsting();
        SqlCmd.Connection = SqlConn;
        SqlCmd.CommandType = CommandType.StoredProcedure;
        SqlCmd.CommandText = spName;
        SqlCmd.CommandTimeout = 120;
        if (SqlParams != null)
        {
            foreach (SqlParameter SqlParam in SqlParams)
            {
                SqlCmd.Parameters.Add(SqlParam);
            }
        }
    }

    public void AddParameter(String Param, Object Value)
    {
        SqlCmd.Parameters.AddWithValue(Param, Value);
    }

    public string DATE_COVERSITION(string QTY)
    {
        string FAMILY_DOB = QTY;
        int yearno = Convert.ToInt32(FAMILY_DOB.LastIndexOf("-", FAMILY_DOB.Length).ToString()) + 1;
        int monthno = Convert.ToInt32(FAMILY_DOB.IndexOf("-", 0).ToString()) + 1;
        string embdob = FAMILY_DOB.Substring(monthno, yearno - monthno - 1) + "-" + FAMILY_DOB.Substring(0, monthno - 1) + "-" + FAMILY_DOB.Substring(yearno, FAMILY_DOB.Length - yearno);
        return embdob;
    }

    public DataSet VIEW_DATA_METHOD(string QUERY_STR)
    {
        throw new NotImplementedException();
    }

    
   

}

