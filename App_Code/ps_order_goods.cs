using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

	/// <summary>
	/// 类ps_order_goods。
	/// </summary>
	[Serializable]
	public partial class ps_order_goods
	{
		public ps_order_goods()
		{}
        #region Model
        private long _id;
        private int? _order_id;
        private long? _goods_id;
        private string _product_no;
        private int? _product_category_id = 0;
        private string _goods_title;
        private decimal? _goods_price = 0M;
        private decimal? _real_price = 0M;
        private decimal? _discount = 0M;
        private decimal? _payamount = 0M;
        private int? _quantity = 0;
        private string _dw = "";
        private string _remarks = "";
        private string _kit_num = "";
        private string _kit_desc = "";
        private string _iscust = "";
        private string _custsize = "";
        private string _new_product_no;
        private string _designattachment = "";
        private string _designattachment2 = "";
        private string _designattachment3 = "";
        private string _designattachment4 = "";
        private string _designattachment5 = "";


        /// <summary>
        /// 自增ID
        /// </summary>
        public long id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 订单ID
        /// </summary>
        public int? order_id
        {
            set { _order_id = value; }
            get { return _order_id; }
        }
        /// <summary>
        /// 商品ID
        /// </summary>
        public long? goods_id
        {
            set { _goods_id = value; }
            get { return _goods_id; }
        }
        public string product_no
        {
            set { _product_no = value; }
            get { return _product_no; }
        }
    
        /// <summary>
        /// 产品种类
        /// </summary>
        public int? product_category_id
        {
            set { _product_category_id = value; }
            get { return _product_category_id; }
        }
        /// <summary>
        /// 商品标题
        /// </summary>
        public string goods_title
        {
            set { _goods_title = value; }
            get { return _goods_title; }
        }
        /// <summary>
        /// 进货价格
        /// </summary>
        public decimal? goods_price
        {
            set { _goods_price = value; }
            get { return _goods_price; }
        }
        /// <summary>
        /// 实际价格
        /// </summary>
        public decimal? real_price
        {
            set { _real_price = value; }
            get { return _real_price; }
        }

        public decimal? discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 订购数量
        /// </summary>
        public int? quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }

        public decimal? payamount
        {
            set { _payamount = value; }
            get { return _payamount; }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string dw
        {
            set { _dw = value; }
            get { return _dw; }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }

        public string kit_num
        {
            set { _kit_num = value; }
            get { return _kit_num; }
        }

        public string kit_desc
        {
            set { _kit_desc = value; }
            get { return _kit_desc; }
        }

        public string custsize
        {
            set { _custsize = value; }
            get { return _custsize; }
        }

        public string iscust
        {
            set { _iscust = value; }
            get { return _iscust; }
        }


        public string new_product_no
        {
            set { _new_product_no = value; }
            get { return _new_product_no; }
        }

        public string designattachment
        {
            set { _designattachment = value; }
            get { return _designattachment; }
        }
        public string designattachment2
        {
            set { _designattachment2 = value; }
            get { return _designattachment2; }
        }
        public string designattachment3
        {
            set { _designattachment3 = value; }
            get { return _designattachment3; }
        }
        public string designattachment4
        {
            set { _designattachment4 = value; }
            get { return _designattachment4; }
        }

        public string designattachment5
        {
            set { _designattachment5 = value; }
            get { return _designattachment5; }
        }





        #endregion Model


        #region  Method


    /// <summary>
    /// 得到最大ID
    /// </summary>
        public int GetMaxId()
		{

		return DbHelperSQL.GetMaxID("id", "ps_order_goods"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from [ps_order_goods]");
			strSql.Append(" where id=@id ");

			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [ps_order_goods] (");
            strSql.Append("order_id,goods_id,product_category_id,goods_title,goods_price,real_price,quantity,dw,remarks,product_no,kit_num,kit_desc,custsize,iscust,discount,designattachment,designattachment2,designattachment3,designattachment4,designattachment5)");
            strSql.Append(" values (");
            strSql.Append("@order_id,@goods_id,@product_category_id,@goods_title,@goods_price,@real_price,@quantity,@dw,@remarks,@product_no,@kit_num,@kit_desc,@custsize,@iscust,@discount,@designattachment,@designattachment2,@designattachment3,@designattachment4,@designattachment5)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@order_id", SqlDbType.Int,4),
					new SqlParameter("@goods_id", SqlDbType.BigInt,8),
					new SqlParameter("@product_category_id", SqlDbType.Int,4),
					new SqlParameter("@goods_title", SqlDbType.NVarChar,100),
					new SqlParameter("@goods_price", SqlDbType.Decimal,5),
					new SqlParameter("@real_price", SqlDbType.Decimal,5),
					new SqlParameter("@quantity", SqlDbType.Int,4),
					new SqlParameter("@dw", SqlDbType.VarChar,50),
                    new SqlParameter("@remarks", SqlDbType.VarChar,200),
                    new SqlParameter("@product_no", SqlDbType.VarChar,200),
                    new SqlParameter("@kit_num", SqlDbType.VarChar,200),
                    new SqlParameter("@kit_desc", SqlDbType.VarChar,200),
                    new SqlParameter("@custsize", SqlDbType.VarChar,200),
                    new SqlParameter("@iscust", SqlDbType.VarChar,200),
                    new SqlParameter("@discount", SqlDbType.Int,4),
                    new SqlParameter("@designattachment", SqlDbType.VarChar,200),
                    new SqlParameter("@designattachment2", SqlDbType.VarChar,200),
                    new SqlParameter("@designattachment3", SqlDbType.VarChar,200),
                    new SqlParameter("@designattachment4", SqlDbType.VarChar,200),
                    new SqlParameter("@designattachment5", SqlDbType.VarChar,200),
                    };
            parameters[0].Value = order_id;
            parameters[1].Value = goods_id;
            parameters[2].Value = product_category_id;
            parameters[3].Value = goods_title;
            parameters[4].Value = goods_price;
            parameters[5].Value = real_price;
            parameters[6].Value = quantity;
            parameters[7].Value = dw;
            parameters[8].Value = remarks;
            parameters[9].Value = product_no;
            parameters[10].Value = kit_num;
            parameters[11].Value = kit_desc;
            parameters[12].Value = custsize;
            parameters[13].Value = iscust;
            parameters[14].Value = discount;
            parameters[15].Value = designattachment;
            parameters[16].Value = designattachment2;
            parameters[17].Value = designattachment3;
            parameters[18].Value = designattachment4;
            parameters[19].Value = designattachment5;

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
            strSql.Append("update [ps_order_goods] set ");
            strSql.Append("order_id=@order_id,");
            strSql.Append("goods_id=@goods_id,");
            strSql.Append("product_category_id=@product_category_id,");
            strSql.Append("goods_title=@goods_title,");
            strSql.Append("goods_price=@goods_price,");
            strSql.Append("real_price=@real_price,");
            strSql.Append("quantity=@quantity,");
            strSql.Append("dw=@dw,");
            strSql.Append("product_no=@product_no,");
            strSql.Append("new_product_no=@new_product_no,");
            strSql.Append("payamount=@payamount ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@order_id", SqlDbType.Int,4),
					new SqlParameter("@goods_id", SqlDbType.BigInt,8),
					new SqlParameter("@product_category_id", SqlDbType.Int,4),
					new SqlParameter("@goods_title", SqlDbType.NVarChar,100),
					new SqlParameter("@goods_price", SqlDbType.Decimal,5),
					new SqlParameter("@real_price", SqlDbType.Decimal,5),
					new SqlParameter("@quantity", SqlDbType.Int,4),
					new SqlParameter("@dw", SqlDbType.VarChar,50),
                    new SqlParameter("@remarks", SqlDbType.VarChar,200),
                    new SqlParameter("@product_no", SqlDbType.VarChar,200),
                    new SqlParameter("@new_product_no", SqlDbType.VarChar,200),
                    new SqlParameter("@payamount", SqlDbType.Decimal,5),
                    new SqlParameter("@id", SqlDbType.BigInt,8)};
            parameters[0].Value = order_id;
            parameters[1].Value = goods_id;
            parameters[2].Value = product_category_id;
            parameters[3].Value = goods_title;
            parameters[4].Value = goods_price;
            parameters[5].Value = real_price;
            parameters[6].Value = quantity;
            parameters[7].Value = dw;
            parameters[8].Value = remarks;
            parameters[9].Value = product_no;
            parameters[10].Value = new_product_no;
            parameters[11].Value = payamount;
            parameters[12].Value = id;

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
    

    public bool UpdateOrderPriceAndDiscount()
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("update [ps_order_goods] set ");
        strSql.Append("real_price=@real_price,");
        strSql.Append("discount=@discount,");
        strSql.Append("payamount=@payamount,");
        strSql.Append("goods_title=@goods_title");
        strSql.Append(" where id=@id ");
        SqlParameter[] parameters = {
                    new SqlParameter("@real_price", SqlDbType.Decimal,5),
                    new SqlParameter("@discount", SqlDbType.Decimal,5),
                    new SqlParameter("@payamount", SqlDbType.Decimal,5),
                    new SqlParameter("@goods_title", SqlDbType.VarChar,200),
                    new SqlParameter("@id", SqlDbType.BigInt,8)};
       
        parameters[0].Value = real_price;
        parameters[1].Value = discount;
        parameters[2].Value = payamount;
        parameters[3].Value = goods_title;
        parameters[4].Value = id;

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

    public bool UpdateGoodsProdeuctDesc()
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("update [ps_order_goods] set ");
        strSql.Append("goods_title=@goods_title");
        strSql.Append(" where id=@id ");
        SqlParameter[] parameters = {
                    new SqlParameter("@goods_title", SqlDbType.VarChar,200),
                    new SqlParameter("@id", SqlDbType.BigInt,8)};

        parameters[0].Value = goods_title;
        parameters[1].Value = id;

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
    public bool Delete(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [ps_order_goods] ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt)};
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
    public void GetModel(long id)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select id,order_id,goods_id,product_category_id,goods_title,goods_price,real_price,quantity,dw,remarks,product_no,custsize,iscust, payamount,new_product_no,designattachment,designattachment2,designattachment3,designattachment4,designattachment5 ");
        strSql.Append(" FROM [ps_order_goods] ");
        strSql.Append(" where id=@id ");
        SqlParameter[] parameters = {
				new SqlParameter("@id", SqlDbType.BigInt)};
        parameters[0].Value = id;

        DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
            {
                this.id = long.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            }
            if (ds.Tables[0].Rows[0]["order_id"] != null && ds.Tables[0].Rows[0]["order_id"].ToString() != "")
            {
                this.order_id = int.Parse(ds.Tables[0].Rows[0]["order_id"].ToString());
            }
            if (ds.Tables[0].Rows[0]["goods_id"] != null && ds.Tables[0].Rows[0]["goods_id"].ToString() != "")
            {
                this.goods_id = long.Parse(ds.Tables[0].Rows[0]["goods_id"].ToString());
            }
            if (ds.Tables[0].Rows[0]["product_category_id"] != null && ds.Tables[0].Rows[0]["product_category_id"].ToString() != "")
            {
                this.product_category_id = int.Parse(ds.Tables[0].Rows[0]["product_category_id"].ToString());
            }
            if (ds.Tables[0].Rows[0]["goods_title"] != null)
            {
                this.goods_title = ds.Tables[0].Rows[0]["goods_title"].ToString();
            }
            if (ds.Tables[0].Rows[0]["goods_price"] != null && ds.Tables[0].Rows[0]["goods_price"].ToString() != "")
            {
                this.goods_price = decimal.Parse(ds.Tables[0].Rows[0]["goods_price"].ToString());
            }
            if (ds.Tables[0].Rows[0]["real_price"] != null && ds.Tables[0].Rows[0]["real_price"].ToString() != "")
            {
                this.real_price = decimal.Parse(ds.Tables[0].Rows[0]["real_price"].ToString());
            }
            if (ds.Tables[0].Rows[0]["quantity"] != null && ds.Tables[0].Rows[0]["quantity"].ToString() != "")
            {
                this.quantity = int.Parse(ds.Tables[0].Rows[0]["quantity"].ToString());
            }
            if (ds.Tables[0].Rows[0]["dw"] != null)
            {
                this.dw = ds.Tables[0].Rows[0]["dw"].ToString();
            }
            if (ds.Tables[0].Rows[0]["remarks"] != null)
            {
                this.remarks = ds.Tables[0].Rows[0]["remarks"].ToString();
            }
            if (ds.Tables[0].Rows[0]["product_no"] != null)
            {
                this.product_no = ds.Tables[0].Rows[0]["product_no"].ToString();
            }
            if (ds.Tables[0].Rows[0]["custsize"] != null)
            {
                this.custsize = ds.Tables[0].Rows[0]["custsize"].ToString();
            }
            if (ds.Tables[0].Rows[0]["iscust"] != null)
            {
                this.iscust = ds.Tables[0].Rows[0]["iscust"].ToString();
            }
            
            if (ds.Tables[0].Rows[0]["payamount"] != null && ds.Tables[0].Rows[0]["payamount"].ToString() != "")
            {
                this.payamount = decimal.Parse(ds.Tables[0].Rows[0]["payamount"].ToString());
            }
            if (ds.Tables[0].Rows[0]["new_product_no"] != null && ds.Tables[0].Rows[0]["new_product_no"].ToString() != "")
            {
                this.new_product_no = ds.Tables[0].Rows[0]["new_product_no"].ToString();
            }
            if (ds.Tables[0].Rows[0]["designattachment"] != null && ds.Tables[0].Rows[0]["designattachment"].ToString() != "")
            {
                this.designattachment = ds.Tables[0].Rows[0]["designattachment"].ToString();
            }
            if (ds.Tables[0].Rows[0]["designattachment2"] != null && ds.Tables[0].Rows[0]["designattachment2"].ToString() != "")
            {
                this.designattachment2 = ds.Tables[0].Rows[0]["designattachment2"].ToString();
            }
            if (ds.Tables[0].Rows[0]["designattachment3"] != null && ds.Tables[0].Rows[0]["designattachment3"].ToString() != "")
            {
                this.designattachment3 = ds.Tables[0].Rows[0]["designattachment3"].ToString();
            }
            if (ds.Tables[0].Rows[0]["designattachment4"] != null && ds.Tables[0].Rows[0]["designattachment4"].ToString() != "")
            {
                this.designattachment4 = ds.Tables[0].Rows[0]["designattachment4"].ToString();
            }
            if (ds.Tables[0].Rows[0]["designattachment5"] != null && ds.Tables[0].Rows[0]["designattachment5"].ToString() != "")
            {
                this.designattachment5 = ds.Tables[0].Rows[0]["designattachment5"].ToString();
            }

        }
    }
    

    /// <summary>
    /// 订单号id删除数据
    /// </summary>
    public bool DeleteOid(int order_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [ps_order_goods] ");
            strSql.Append(" where order_id=@order_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@order_id", SqlDbType.Int,4)};
            parameters[0].Value = order_id;

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
        /// 通过order_id获得对应的信息
        /// </summary>
        public DataTable GetList(int order_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ps_order_goods");
            if (order_id == 0)
            {
                strSql.Append(" order by id desc");
            }
            else
            {
                strSql.Append(" where order_id=" + order_id + " order by id desc");
            }
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return ds.Tables[0];
        }

    public bool IsOrerExistCustStyle(string order_no)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select top 1 order_no from v_OrderGoods");
        
        strSql.Append(" where order_no=" + order_no + " and isnull(custsize,'')<>''");
        DataSet ds = DbHelperSQL.Query(strSql.ToString());
        if (ds.Tables[0].Rows.Count > 0)
            return true;
        else
            return false;
    }

    public bool IsOrerExistCustStyle(int order_id)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select top 1 order_no from v_OrderGoods");

        strSql.Append(" where order_id=" + order_id + " and isnull(custsize,'')<>''");
        DataSet ds = DbHelperSQL.Query(strSql.ToString());
        if (ds.Tables[0].Rows.Count > 0)
            return true;
        else
            return false;
    }
    /// <summary>
    /// 获得数据列表
    /// </summary>
    public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM [v_OrderGoods] ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by id ");
            return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListRep(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  goods_title,goods_id,product_category_id ,dw,sum(quantity) as zongquantity  ");
            strSql.Append(" FROM [ps_order_goods] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere + " group by  goods_title,goods_id,product_category_id,dw order by product_category_id desc");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得分组后查询分页数据
        /// </summary>
        public DataSet GetListGroup( int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  goods_title,goods_id,product_category_id ,product_no,dw, commercialStyle  ,sum(quantity) as zongquantity,specification FROM[v_OrderGoods] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere + " group by  goods_title,goods_id,product_category_id,product_no,dw ,specification, commercialStyle");
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
		#endregion  Method
	}

