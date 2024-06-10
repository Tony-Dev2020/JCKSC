
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// 类ps_manager_customer。
/// </summary>
[Serializable]
public partial class ps_manager_customer
{
	public ps_manager_customer()
	{ }
	#region Model
	private int _id;
	private int? _user_id = 0;
	private int? _cust_id = 0;
	/// <summary>
	/// 角色栏目对应关系id
	/// </summary>
	public int id
	{
		set { _id = value; }
		get { return _id; }
	}
	/// <summary>
	/// 角色id
	/// </summary>
	public int? user_id
	{
		set { _user_id = value; }
		get { return _user_id; }
	}
	/// <summary>
	/// 栏目id
	/// </summary>
	public int? cust_id
	{
		set { _cust_id = value; }
		get { return _cust_id; }
	}
	#endregion Model


	#region  Method


	/// <summary>
	/// 是否存在该记录
	/// </summary>
	public bool Exists()
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("select count(1) from [ps_manager_customer]");
		strSql.Append(" where id=@id ");

		SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
		parameters[0].Value = id;

		return DbHelperSQL.Exists(strSql.ToString(), parameters);
	}

	/// <summary>
	/// 是否有访问该网页的权限
	/// </summary>
	public bool CustExists(int _user_id, int _cust_id)
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("select count(1) from ps_manager_customer where user_id=" + _user_id + " and cust_id=" + _cust_id + "");
		return DbHelperSQL.Exists(strSql.ToString());
	}
	/// <summary>
	/// 增加一条数据
	/// </summary>
	public int Add()
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("insert into [ps_manager_customer] (");
		strSql.Append("user_id,cust_id)");
		strSql.Append(" values (");
		strSql.Append("@user_id,@cust_id)");
		strSql.Append(";select @@IDENTITY");
		SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@cust_id", SqlDbType.Int,4)};
		parameters[0].Value = user_id;
		parameters[1].Value = cust_id;

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
	/// 更新一条数据
	/// </summary>
	public bool Update()
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("update [ps_manager_customer] set ");
		strSql.Append("user_id=@user_id,");
		strSql.Append("cust_id=@cust_id");
		strSql.Append(" where id=@id ");
		SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@cust_id", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
		parameters[0].Value = user_id;
		parameters[1].Value = cust_id;
		parameters[2].Value = id;

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
	/// 删除一条数据
	/// </summary>
	public bool Delete(int id)
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("delete from [ps_manager_customer] ");
		strSql.Append(" where id=@id ");
		SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
		parameters[0].Value = id;

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
	/// 得到一个对象实体
	/// </summary>
	public void GetModel(int id)
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("select id,user_id,cust_id ");
		strSql.Append(" FROM [ps_manager_customer] ");
		strSql.Append(" where id=@id ");
		SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
		parameters[0].Value = id;

		DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
		if (ds.Tables[0].Rows.Count > 0)
		{
			if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
			{
				this.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
			}
			if (ds.Tables[0].Rows[0]["user_id"] != null && ds.Tables[0].Rows[0]["user_id"].ToString() != "")
			{
				this.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
			}
			if (ds.Tables[0].Rows[0]["cust_id"] != null && ds.Tables[0].Rows[0]["cust_id"].ToString() != "")
			{
				this.cust_id = int.Parse(ds.Tables[0].Rows[0]["cust_id"].ToString());
			}
		}
	}

	/// <summary>
	/// 获得数据列表
	/// </summary>
	public DataSet GetList(string strWhere)
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("select * ");
		strSql.Append(" FROM [v_manager_customer] ");
		if (strWhere.Trim() != "")
		{
			strSql.Append(" where " + strWhere);
		}
		return DbHelperSQL.Query(strSql.ToString());
	}
	public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("select * FROM  [v_manager_customer]");
		if (strWhere.Trim() != "")
		{
			strSql.Append(" where " + strWhere);
		}
		recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
		return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
	}
	#endregion  Method
}


