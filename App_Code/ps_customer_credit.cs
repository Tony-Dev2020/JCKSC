using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// ps_customer_credit 的摘要说明
/// </summary>
public class ps_customer_credit
{
    public ps_customer_credit()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    #region Model
    public string Company { set; get; }
    public string CustID { set; get; }
    public string CustNum { set; get; }
    public string CustName { set; get; }
    public string CreditHold { set; get; }
    public string CreditLimit { set; get; }
    public string TotOrderCredit { set; get; }
    public string TotOpenInvoices { set; get; }
    public string TotOpenOrders { set; get; }
    #endregion

    #region  Method





    /// <summary>
    /// 增加一条数据
    /// </summary>
    public int Add()
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("insert into [ps_customer_credit] (");
        strSql.Append("Company, CustID, CustNum, CustName, CreditHold, CreditLimit, TotOrderCredit, TotOpenInvoices, TotOpenOrders)");
        strSql.Append(" values (");
        strSql.Append("@Company, @CustID, @CustNum, @CustName, @CreditHold, @CreditLimit, @TotOrderCredit, @TotOpenInvoices, @TotOpenOrders)");
        strSql.Append(";select @@IDENTITY");
        SqlParameter[] parameters = {
                    new SqlParameter("@Company", SqlDbType.VarChar,300),
                    new SqlParameter("@CustID", SqlDbType.VarChar,300),
                    new SqlParameter("@CustNum", SqlDbType.VarChar,300),
                    new SqlParameter("@CustName", SqlDbType.VarChar,300),
                    new SqlParameter("@CreditHold", SqlDbType.VarChar,300),
                    new SqlParameter("@CreditLimit", SqlDbType.VarChar,300),
                    new SqlParameter("@TotOrderCredit", SqlDbType.VarChar,300),
                    new SqlParameter("@TotOpenInvoices", SqlDbType.VarChar,300),
                    new SqlParameter("@TotOpenOrders", SqlDbType.VarChar,300)
                    };
        parameters[0].Value = Company;
        parameters[1].Value = CustID;
        parameters[2].Value = CustNum;
        parameters[3].Value = CustName;
        parameters[4].Value = CreditHold;
        parameters[5].Value = CreditLimit;
        parameters[6].Value = TotOrderCredit;
        parameters[7].Value = TotOpenInvoices;
        parameters[8].Value = TotOpenOrders;
       

        object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
        if (obj == null)
        {
            return 0;
        }
        else
        {
            return Convert.ToInt32(obj);
        }
    }
   



    /// <summary>
    /// 修改一列数据
    /// </summary>
    public void UpdateField(int id, string strValue)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("update ps_vendor set " + strValue);
        strSql.Append(" where id=" + id);
        DbHelperSQL.ExecuteSql(strSql.ToString());
    }

    /// <summary>
    /// 删除一条数据
    /// </summary>
    public bool DeleteAll()
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("delete from [ps_customer_credit] ");
      

        int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
        if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 删除一条数据
    /// </summary>
    public bool Delete(string CustID)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("delete from [ps_customer_credit] ");
        strSql.Append(" where CustID=@CustID ");
        SqlParameter[] parameters = {
                    new SqlParameter("@CustID", SqlDbType.VarChar,50)};
        parameters[0].Value = CustID;

        int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }





    /// <summary>
    /// 获得数据列表
    /// </summary>
    public DataSet GetList(string strWhere)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * ");
        strSql.Append(" FROM [v_Customer_Credit] ");
        if (strWhere.Trim() != "")
        {
            strSql.Append(" where " + strWhere);
        }
        return DbHelperSQL.Query(strSql.ToString());
    }



   
    /// <summary>
    /// 获得前几行数据
    /// </summary>
    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select ");
        if (Top > 0)
        {
            strSql.Append(" top " + Top.ToString());
        }
        strSql.Append(" * ");
        strSql.Append(" FROM  [dbo].[v_Customer_Credit]");
        if (strWhere.Trim() != "")
        {
            strSql.Append(" where " + strWhere);
        }
        strSql.Append(" order by " + filedOrder);
        return DbHelperSQL.Query(strSql.ToString());
    }

    /// <summary>
    /// 获得查询分页数据
    /// </summary>
    public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * FROM  [dbo].[v_Customer_Credit]");
        if (strWhere.Trim() != "")
        {
            strSql.Append(" where " + strWhere);
        }
        recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
        return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
    }
    #endregion  Method
}