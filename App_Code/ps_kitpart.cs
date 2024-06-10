using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// ps_rfq。
/// </summary>
[Serializable]
public partial class ps_kitpart
{
    public ps_kitpart()
    { }
	#region Model
	public int id { set; get; }
	public int? kit_id { set; get; }
	public string partnumber { set; get; }
	public string partdesc { set; get; }
	public string uom { set; get; }
	public decimal? qty { set; get; }
	public decimal? unitprice { set; get; }
	public int? sort_id { set; get; }
	public string remark { set; get; }
	public bool? isCust { set; get; }

	#endregion Model


	#region  Method

	public bool Exists(int id)
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("select count(1) from ps_kitpart");
		strSql.Append(" where id=@id");
		SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
		parameters[0].Value = id;

		return DbHelperSQL.Exists(strSql.ToString(), parameters);
	}


	/// <summary>
	/// 增加一条数据
	/// </summary>
	public int Add(ps_kitpart model)
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("insert into ps_kitpart(");
		strSql.Append("kit_id,partnumber,partdesc,uom,qty,unitprice,sort_id,remark,isCust)");
		strSql.Append(" values (");
		strSql.Append("@kit_id,@partnumber,@partdesc,@uom,@qty,@unitprice,@sort_id,@remark,@isCust)");
		strSql.Append(";select @@IDENTITY");
		SqlParameter[] parameters = {
					new SqlParameter("@kit_id", SqlDbType.Int,4),
					new SqlParameter("@partnumber", SqlDbType.VarChar,200),
					new SqlParameter("@partdesc", SqlDbType.VarChar,500),
					new SqlParameter("@uom", SqlDbType.NVarChar,100),
					new SqlParameter("@qty", SqlDbType.Decimal,5),
					new SqlParameter("@unitprice", SqlDbType.Decimal,5),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,200),
					new SqlParameter("@isCust", SqlDbType.Bit)};
		parameters[0].Value = model.kit_id;
		parameters[1].Value = model.partnumber;
		parameters[2].Value = model.partdesc;
		parameters[3].Value = model.uom;
		parameters[4].Value = model.qty;
		parameters[5].Value = model.unitprice;
		parameters[6].Value = model.sort_id;
		parameters[7].Value = model.remark;
		parameters[8].Value = model.isCust;

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
	public bool Update(ps_kitpart model)
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("update ps_kitpart set ");
		strSql.Append("kit_id=@kit_id,");
		strSql.Append("partnumber=@partnumber,");
		strSql.Append("partdesc=@partdesc,");
		strSql.Append("uom=@uom,");
		strSql.Append("qty=@qty,");
		strSql.Append("unitprice=@unitprice,");
		strSql.Append("sort_id=@sort_id,");
		strSql.Append("remark=@remark,");
		strSql.Append("isCust=@isCust");
		strSql.Append(" where id=@id");
		SqlParameter[] parameters = {
					new SqlParameter("@kit_id", SqlDbType.Int,4),
					new SqlParameter("@partnumber", SqlDbType.VarChar,200),
					new SqlParameter("@partdesc", SqlDbType.VarChar,500),
					new SqlParameter("@uom", SqlDbType.NVarChar,100),
					new SqlParameter("@qty", SqlDbType.Decimal,5),
					new SqlParameter("@unitprice", SqlDbType.Decimal,5),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,200),
					new SqlParameter("@isCust", SqlDbType.Bit),
					new SqlParameter("@id", SqlDbType.Int,4)};
		parameters[0].Value = model.kit_id;
		parameters[1].Value = model.partnumber;
		parameters[2].Value = model.partdesc;
		parameters[3].Value = model.uom;
		parameters[4].Value = model.qty;
		parameters[5].Value = model.unitprice;
		parameters[6].Value = model.sort_id;
		parameters[7].Value = model.remark;
		parameters[8].Value = model.isCust;
		parameters[9].Value = model.id;

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
		strSql.Append("delete from ps_kitpart ");
		strSql.Append(" where id=@id");
		SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
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
	/// 批量删除数据
	/// </summary>
	public bool DeleteList(string idlist)
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("delete from ps_kitpart ");
		strSql.Append(" where id in (" + idlist + ")  ");
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
	/// 批量copy数据
	/// </summary>
	public bool CopyKit(int org_kitid, int new_kitid)
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("insert into ps_kitpart ");
		strSql.Append("(kit_id, partnumber, partdesc, uom, qty, unitprice, sort_id, remark, IsCust) ");
		strSql.Append("select " + new_kitid.ToString()) ;
		strSql.Append(", partnumber, partdesc, uom, qty, unitprice, sort_id, remark, IsCust FROM ps_kitpart where kit_id= " + org_kitid.ToString() + "");
		
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
	/// 得到一个对象实体
	/// </summary>
	public ps_kitpart GetModel(int id)
	{

		StringBuilder strSql = new StringBuilder();
		strSql.Append("select  top 1 id,kit_id,partnumber,partdesc,uom,qty,unitprice,sort_id,remark,isCust from ps_kitpart ");
		strSql.Append(" where id=@id");
		SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
		parameters[0].Value = id;

		ps_kitpart model = new ps_kitpart();
		DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
		if (ds.Tables[0].Rows.Count > 0)
		{
			return DataRowToModel(ds.Tables[0].Rows[0]);
		}
		else
		{
			return null;
		}
	}

	/// <summary>
	/// 得到一个对象实体
	/// </summary>
	public bool GetKitIsCust(string partno)
	{
		bool IsCust = false;
		StringBuilder strSql = new StringBuilder();
		strSql.Append("select  top 1 isCust from ps_kitpart ");
		strSql.Append(" where partnumber=@partno");
		SqlParameter[] parameters = {
					new SqlParameter("@partno", SqlDbType.VarChar, 255)
			};
		parameters[0].Value = partno;
		DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

		if (ds.Tables[0].Rows.Count > 0)
		{
			if (ds.Tables[0].Rows[0]["isCust"] != null)
			{
				if (ds.Tables[0].Rows[0]["isCust"].ToString() != "")
					IsCust = Boolean.Parse(ds.Tables[0].Rows[0]["isCust"].ToString());
				else
					IsCust = false;
			}
		}
		return IsCust;

	}


	/// <summary>
	/// 得到一个对象实体
	/// </summary>
	public ps_kitpart DataRowToModel(DataRow row)
	{
		ps_kitpart model = new ps_kitpart();
		if (row != null)
		{
			if (row["id"] != null && row["id"].ToString() != "")
			{
				model.id = int.Parse(row["id"].ToString());
			}
			if (row["kit_id"] != null && row["kit_id"].ToString() != "")
			{
				model.kit_id = int.Parse(row["kit_id"].ToString());
			}
			if (row["partnumber"] != null)
			{
				model.partnumber = row["partnumber"].ToString();
			}
			if (row["partdesc"] != null)
			{
				model.partdesc = row["partdesc"].ToString();
			}
			if (row["uom"] != null)
			{
				model.uom = row["uom"].ToString();
			}
			if (row["qty"] != null && row["qty"].ToString() != "")
			{
				model.qty = decimal.Parse(row["qty"].ToString());
			}
			if (row["unitprice"] != null && row["unitprice"].ToString() != "")
			{
				model.unitprice = decimal.Parse(row["unitprice"].ToString());
			}
			if (row["sort_id"] != null && row["sort_id"].ToString() != "")
			{
				model.sort_id = int.Parse(row["sort_id"].ToString());
			}
			if (row["remark"] != null)
			{
				model.remark = row["remark"].ToString();
			}
			if (row["isCust"] != null)
			{
				if (row["isCust"].ToString() != "")
					model.isCust = Boolean.Parse(row["isCust"].ToString());
				else
					model.isCust = false;
			}
		}
		return model;
	}

	/// <summary>
	/// 获得数据列表
	/// </summary>
	public DataSet GetList(string strWhere)
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("select * ");
		strSql.Append(" FROM v_Kit ");
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
		strSql.Append(" id,kit_id,partnumber,partdesc,uom,qty,unitprice,sort_id,remark ");
		strSql.Append(" FROM ps_kitpart ");
		if (strWhere.Trim() != "")
		{
			strSql.Append(" where " + strWhere);
		}
		strSql.Append(" order by " + filedOrder);
		return DbHelperSQL.Query(strSql.ToString());
	}

	/// <summary>
	/// 获取记录总数
	/// </summary>
	public int GetRecordCount(string strWhere)
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("select count(1) FROM ps_kitpart ");
		if (strWhere.Trim() != "")
		{
			strSql.Append(" where " + strWhere);
		}
		object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
	/// 分页获取数据列表
	/// </summary>
	public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("SELECT * FROM ( ");
		strSql.Append(" SELECT ROW_NUMBER() OVER (");
		if (!string.IsNullOrEmpty(orderby.Trim()))
		{
			strSql.Append("order by T." + orderby);
		}
		else
		{
			strSql.Append("order by T.id desc");
		}
		strSql.Append(")AS Row, T.*  from ps_kitpart T ");
		if (!string.IsNullOrEmpty(strWhere.Trim()))
		{
			strSql.Append(" WHERE " + strWhere);
		}
		strSql.Append(" ) TT");
		strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
		return DbHelperSQL.Query(strSql.ToString());
	}

	/// <summary>
	/// 获得查询分页数据
	/// </summary>
	public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
	{
		StringBuilder strSql = new StringBuilder();
		strSql.Append("select * FROM  v_Kit");
		if (strWhere.Trim() != "")
		{
			strSql.Append(" where " + strWhere);
		}
		recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
		return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
	}

	/*
	/// <summary>
	/// 分页获取数据列表
	/// </summary>
	public DataSet GetList(int PageSize,int PageIndex,string strWhere)
	{
		SqlParameter[] parameters = {
				new SqlParameter("@tblName", SqlDbType.VarChar, 255),
				new SqlParameter("@fldName", SqlDbType.VarChar, 255),
				new SqlParameter("@PageSize", SqlDbType.Int),
				new SqlParameter("@PageIndex", SqlDbType.Int),
				new SqlParameter("@IsReCount", SqlDbType.Bit),
				new SqlParameter("@OrderType", SqlDbType.Bit),
				new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
				};
		parameters[0].Value = "ps_kitpart";
		parameters[1].Value = "id";
		parameters[2].Value = PageSize;
		parameters[3].Value = PageIndex;
		parameters[4].Value = 0;
		parameters[5].Value = 0;
		parameters[6].Value = strWhere;	
		return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
	}*/

	#endregion  BasicMethod
}




