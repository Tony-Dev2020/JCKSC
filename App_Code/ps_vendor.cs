

using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

	/// <summary>
	/// 供应商-类
	/// </summary>
	[Serializable]
public partial class ps_vendor
{
    public ps_vendor()
    { }

    
    #region Model
    public int id { set; get; }
    public string Company { set; get; }
    public string VendorID { set; get; }
    public string Name { set; get; }
    public int VendorNum { set; get; }
    public string Address1 { set; get; }
    public string Address2 { set; get; }
    public string Address3 { set; get; }
    public string City { set; get; }
    public string State { set; get; }
    public string Zip { set; get; }
    public string Country { set; get; }
    public string TaxPayerID { set; get; }
    public string PurPoint { set; get; }
    public string TermsCode { set; get; }
    public string GroupCode { set; get; }
    public int Print1099 { set; get; }
    public int OneCheck { set; get; }
    public int PrintLabels { set; get; }
    public string FaxNum { set; get; }
    public string PhoneNum { set; get; }
    public string Comment { set; get; }
    public int Inactive { set; get; }
    public string CurrencyCode { set; get; }
    public string OrgRegCode { set; get; }
    public string EMailAddress { set; get; }


    //public int id
    //{
    //    set { _id = value; }
    //    get { return _id; }
    //}

    //public string Company
    //{
    //    set { _Company = value; }
    //    get { return _Company; }
    //}

    //public int VendorID
    //{
    //    set { _VendorID = value; }
    //    get { return _VendorID; }
    //}

    //public string Name
    //{
    //    set { _Name = value; }
    //    get { return _Name; }
    //}

    //public int VendorNum
    //{
    //    set { _VendorNum = value; }
    //    get { return _VendorNum; }
    //}

    //public string Address1
    //{
    //    set { _Address1 = value; }
    //    get { return _Address1; }
    //}
    //public string Address2
    //{
    //    set { _Address2 = value; }
    //    get { return _Address2; }
    //}
    //public string Address3
    //{
    //    set { _Address3 = value; }
    //    get { return _Address3; }
    //}

    ////--TaxPayerID, PurPoint, TermsCode, GroupCode, Print1099, OneCheck, PrintLabels, FaxNum, PhoneNum, Comment,Inactive
    //public string City
    //{
    //    set { _City = value; }
    //    get { return _City; }
    //}
    //public string State
    //{
    //    set { _State = value; }
    //    get { return _State; }
    //}
    //public string ZIP
    //{
    //    set { _Zip = value; }
    //    get { return _Zip; }
    //}
    //public string Country
    //{
    //    set { _Country = value; }
    //    get { return _Country; }
    //}
    //public string TaxPayerID
    //{
    //    set { _TaxPayerID = value; }
    //    get { return _TaxPayerID; }
    //}
    //public string PurPoint
    //{
    //    set { _PurPoint = value; }
    //    get { return _PurPoint; }
    //}

    #endregion Model


    #region  Method


    /// <summary>
    /// 是否存在该记录
    /// </summary>
    public bool Exists(int id)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select count(1) from [ps_vendor]");
        strSql.Append(" where id=@id ");

        SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)};
        parameters[0].Value = id;

        return DbHelperSQL.Exists(strSql.ToString(), parameters);
    }

    /// <summary>
    /// 查询名称是否存在
    /// </summary>
    public bool Exists(string Name)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select count(1) from  ps_vendor");
        strSql.Append(" where Name=@Name ");
        SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NVarChar,100)};
        parameters[0].Value = Name;
        return DbHelperSQL.Exists(strSql.ToString(), parameters);
    }

    /// <summary>
    /// 查询排除本身名称是否存在
    /// </summary>
    public bool Exists(string Name, int id)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select count(1) from  ps_vendor");
        strSql.Append(" where id<>@id and  Name=@Name ");
        SqlParameter[] parameters = {
                     new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@Name", SqlDbType.NVarChar,100)};
        parameters[0].Value = id;
        parameters[1].Value = Name;
        return DbHelperSQL.Exists(strSql.ToString(), parameters);
    }

    /// <summary>
    /// 返回地区名称
    /// </summary>
    public string GetName(int id)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select top 1 Name from [ps_vendor]");
        strSql.Append(" where id=" + id);
        string Name = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
        if (string.IsNullOrEmpty(Name))
        {
            return "";
        }
        return Name;
    }




    public string GetVendorID(int id)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select top 1 VendorID from [ps_vendor]");
        strSql.Append(" where id=" + id);
        string Name = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
        if (string.IsNullOrEmpty(Name))
        {
            return "";
        }
        return Name;
    }

    /// <summary>
    /// 增加一条数据
    /// </summary>
    public int Add()
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("insert into [ps_vendor] (");
        strSql.Append("Company, VendorID, Name, VendorNum, Address1, Address2, Address3, City, State, ZIP, Country, TaxPayerID, PurPoint, TermsCode, GroupCode, Print1099, OneCheck, PrintLabels, FaxNum, PhoneNum, Comment,Inactive)");
        strSql.Append(" values (");
        strSql.Append("@Company,@VendorID,@Name,@VendorNum,@Address1,@Address2,@Address3,@City,@State,@ZIP,@Country,@TaxPayerID,@PurPoint,@TermsCode,@GroupCode,@Print1099,@OneCheck,@PrintLabels,@FaxNum,@PhoneNum,@Comment,Inactive)");
        strSql.Append(";select @@IDENTITY");
        SqlParameter[] parameters = {
                    new SqlParameter("@Company", SqlDbType.VarChar,300),
                    new SqlParameter("@VendorID", SqlDbType.Int,4),
                    new SqlParameter("@Name", SqlDbType.VarChar,300),
                    new SqlParameter("@VendorNum", SqlDbType.Int,4),
                    new SqlParameter("@Address1", SqlDbType.VarChar,300),
                    new SqlParameter("@Address2", SqlDbType.VarChar,300),
                    new SqlParameter("@Address3", SqlDbType.VarChar,300),
                    new SqlParameter("@City", SqlDbType.VarChar,300),
                    new SqlParameter("@State", SqlDbType.VarChar,300),
                    new SqlParameter("@ZIP", SqlDbType.VarChar,300),
                    new SqlParameter("@Country", SqlDbType.VarChar,300),
                    new SqlParameter("@VendorNum", SqlDbType.Int,4),
                    new SqlParameter("@TaxPayerID", SqlDbType.Int,4),
                    new SqlParameter("@PurPoint",SqlDbType.Int,4),
                    new SqlParameter("@TermsCode", SqlDbType.VarChar,300),
                    new SqlParameter("@GroupCode", SqlDbType.VarChar,300),
                    new SqlParameter("@Print1099", SqlDbType.TinyInt,1),
                    new SqlParameter("@OneCheck", SqlDbType.Int,4),
                    new SqlParameter("@PrintLabels", SqlDbType.Int,4),
                    new SqlParameter("@FaxNum", SqlDbType.VarChar,300),
                    new SqlParameter("@PhoneNum", SqlDbType.VarChar,300),
                    new SqlParameter("@Comment", SqlDbType.VarChar,300),
                    new SqlParameter("@Inactive", SqlDbType.Int,4)
                    };
                    parameters[0].Value = Company;
                    parameters[1].Value = VendorID;
                    parameters[2].Value = Name;
                    parameters[3].Value = VendorNum;
                    parameters[4].Value = Address1;
                    parameters[5].Value = Address2;
                    parameters[6].Value = Address3;
                    parameters[7].Value = City;
                    parameters[8].Value = State;
                    parameters[9].Value = Zip;
                    parameters[10].Value = Country;
            
                    parameters[11].Value = TaxPayerID;
                    parameters[12].Value = PurPoint;
                    parameters[13].Value = TermsCode;
                    parameters[14].Value = GroupCode;
                    parameters[15].Value = Print1099;
                    parameters[16].Value = OneCheck;
                    parameters[17].Value = PrintLabels;
                    parameters[18].Value = FaxNum;
                    parameters[19].Value = PhoneNum;
                    parameters[20].Value = Comment;
                    parameters[21].Value = Inactive;

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
        strSql.Append("update [ps_vendor] set ");
        strSql.Append("Name=@Name,");
        strSql.Append("Address1=@Address1,");
        strSql.Append("Address2=@Address2,");
        strSql.Append("Address3=@Address3,");
        strSql.Append("City=@City,");
        strSql.Append("State=@State,");
        strSql.Append("ZIP=@ZIP,");
        strSql.Append("Country=@Country,");
        strSql.Append("CurrencyCode=@CurrencyCode,");
        strSql.Append("OrgRegCode=@OrgRegCode,");
        strSql.Append("EMailAddress=@EMailAddress,");
        strSql.Append("TermsCode=@TermsCode,");
        strSql.Append("PhoneNum=@PhoneNum,");
        strSql.Append("FaxNum=@FaxNum");
        strSql.Append(" where id=@id ");
        SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.VarChar,500),
                    new SqlParameter("@Address1", SqlDbType.VarChar,500),
                    new SqlParameter("@Address2", SqlDbType.VarChar,500),
                    new SqlParameter("@Address3", SqlDbType.VarChar,500),
                    new SqlParameter("@City", SqlDbType.VarChar,500),
                    new SqlParameter("@State", SqlDbType.VarChar,500),
                    new SqlParameter("@ZIP", SqlDbType.VarChar,500),
                    new SqlParameter("@Country", SqlDbType.VarChar,500),
                    new SqlParameter("@CurrencyCode", SqlDbType.VarChar,500),
                    new SqlParameter("@OrgRegCode", SqlDbType.VarChar,500),
                    new SqlParameter("@EMailAddress", SqlDbType.VarChar,500),
                    new SqlParameter("@TermsCode", SqlDbType.VarChar,500),
                    new SqlParameter("@PhoneNum", SqlDbType.VarChar,500),
                    new SqlParameter("@FaxNum", SqlDbType.VarChar,500),
                    new SqlParameter("@id", SqlDbType.Int,4)};
        parameters[0].Value = Name;
        parameters[1].Value = Address1;
        parameters[2].Value = Address2;
        parameters[3].Value = Address3;
        parameters[4].Value = City;
        parameters[5].Value = State;
        parameters[6].Value = "";
        parameters[7].Value = Country;
        parameters[8].Value = CurrencyCode;
        parameters[9].Value = OrgRegCode;
        parameters[10].Value = EMailAddress;
        parameters[11].Value = TermsCode;
        parameters[12].Value = PhoneNum;
        parameters[13].Value = FaxNum;
        parameters[14].Value = id;

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
    /// 更新商家地址
    /// </summary>
    public bool UpdateAddress()
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("update [ps_vendor] set ");
        strSql.Append("contact_address=@contact_address");
        strSql.Append(" where id=@id ");
        SqlParameter[] parameters = {
                    new SqlParameter("@contact_address", SqlDbType.VarChar,300),
                    new SqlParameter("@id", SqlDbType.Int,4)};

        parameters[0].Value = Address1;
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
    public bool Delete(int id)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("delete from [ps_vendor] ");
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
        strSql.Append("select id ,Company, VendorID, Name, VendorNum, Address1, Address2, Address3, City, State, ZIP, Country, TaxPayerID, PurPoint, TermsCode, GroupCode, Print1099, OneCheck, PrintLabels, FaxNum, PhoneNum, Comment,Inactive,CurrencyCode,OrgRegCode,EMailAddress");
        strSql.Append(" FROM [ps_vendor] ");
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
            if (ds.Tables[0].Rows[0]["Company"] != null && ds.Tables[0].Rows[0]["Company"].ToString() != "")
            {
                this.Company = ds.Tables[0].Rows[0]["Company"].ToString();
            }
            if (ds.Tables[0].Rows[0]["VendorID"] != null)
            {
                this.VendorID = ds.Tables[0].Rows[0]["VendorID"].ToString();
            }
            if (ds.Tables[0].Rows[0]["Name"] != null && ds.Tables[0].Rows[0]["Name"].ToString() != "")
            {
                this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
            }
            if (ds.Tables[0].Rows[0]["VendorNum"] != null)
            {
                this.VendorNum = int.Parse(ds.Tables[0].Rows[0]["VendorNum"].ToString());
            }
            if (ds.Tables[0].Rows[0]["Address1"] != null)
            {
                this.Address1 = ds.Tables[0].Rows[0]["Address1"].ToString();
            }
            if (ds.Tables[0].Rows[0]["Address2"] != null)
            {
                this.Address2 = ds.Tables[0].Rows[0]["Address2"].ToString();
            }
            if (ds.Tables[0].Rows[0]["Address3"] != null)
            {
                this.Address3 = ds.Tables[0].Rows[0]["Address3"].ToString();
            }
            if (ds.Tables[0].Rows[0]["City"] != null)
            {
                this.City = ds.Tables[0].Rows[0]["City"].ToString();
            }
            if (ds.Tables[0].Rows[0]["State"] != null && ds.Tables[0].Rows[0]["State"].ToString() != "")
            {
                this.State = ds.Tables[0].Rows[0]["status"].ToString();
            }
            if (ds.Tables[0].Rows[0]["ZIP"] != null && ds.Tables[0].Rows[0]["ZIP"].ToString() != "")
            {
                this.Zip = ds.Tables[0].Rows[0]["Zip"].ToString();
            }
            if (ds.Tables[0].Rows[0]["Comment"] != null)
            {
                this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
            }
            if (ds.Tables[0].Rows[0]["CurrencyCode"] != null)
            {
                this.CurrencyCode = ds.Tables[0].Rows[0]["CurrencyCode"].ToString();
            }
            if (ds.Tables[0].Rows[0]["TermsCode"] != null)
            {
                this.TermsCode = ds.Tables[0].Rows[0]["TermsCode"].ToString();
            }
            if (ds.Tables[0].Rows[0]["OrgRegCode"] != null)
            {
                this.OrgRegCode = ds.Tables[0].Rows[0]["OrgRegCode"].ToString();
            }
            if (ds.Tables[0].Rows[0]["EMailAddress"] != null)
            {
                this.EMailAddress = ds.Tables[0].Rows[0]["EMailAddress"].ToString();
            }
            if (ds.Tables[0].Rows[0]["PhoneNum"] != null)
            {
                this.PhoneNum = ds.Tables[0].Rows[0]["PhoneNum"].ToString();
            }
            if (ds.Tables[0].Rows[0]["FaxNum"] != null)
            {
                this.FaxNum = ds.Tables[0].Rows[0]["FaxNum"].ToString();
            }
            
        }
    }

    public void GetModelByVendorID(string vendorid)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select id ,Company, VendorID, Name, VendorNum, Address1, Address2, Address3, City, State, ZIP, Country, TaxPayerID, PurPoint, TermsCode, GroupCode, Print1099, OneCheck, PrintLabels, FaxNum, PhoneNum, Comment,Inactive,CurrencyCode,OrgRegCode,EMailAddress");
        strSql.Append(" FROM [ps_vendor] ");
        strSql.Append(" where VendorID=@VendorID ");
        SqlParameter[] parameters = {
                    new SqlParameter("@VendorID", SqlDbType.VarChar,300)};
        parameters[0].Value = vendorid;

        DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
            {
                this.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            }
            if (ds.Tables[0].Rows[0]["Company"] != null && ds.Tables[0].Rows[0]["Company"].ToString() != "")
            {
                this.Company = ds.Tables[0].Rows[0]["Company"].ToString();
            }
            if (ds.Tables[0].Rows[0]["VendorID"] != null)
            {
                this.VendorID = ds.Tables[0].Rows[0]["VendorID"].ToString();
            }
            if (ds.Tables[0].Rows[0]["Name"] != null && ds.Tables[0].Rows[0]["Name"].ToString() != "")
            {
                this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
            }
            if (ds.Tables[0].Rows[0]["VendorNum"] != null)
            {
                this.VendorNum = int.Parse(ds.Tables[0].Rows[0]["VendorNum"].ToString());
            }
            if (ds.Tables[0].Rows[0]["Address1"] != null)
            {
                this.Address1 = ds.Tables[0].Rows[0]["Address1"].ToString();
            }
            if (ds.Tables[0].Rows[0]["Address2"] != null)
            {
                this.Address2 = ds.Tables[0].Rows[0]["Address2"].ToString();
            }
            if (ds.Tables[0].Rows[0]["Address3"] != null)
            {
                this.Address3 = ds.Tables[0].Rows[0]["Address3"].ToString();
            }
            if (ds.Tables[0].Rows[0]["City"] != null)
            {
                this.City = ds.Tables[0].Rows[0]["City"].ToString();
            }
            if (ds.Tables[0].Rows[0]["State"] != null && ds.Tables[0].Rows[0]["State"].ToString() != "")
            {
                this.State = ds.Tables[0].Rows[0]["status"].ToString();
            }
            if (ds.Tables[0].Rows[0]["ZIP"] != null && ds.Tables[0].Rows[0]["ZIP"].ToString() != "")
            {
                this.Zip = ds.Tables[0].Rows[0]["Zip"].ToString();
            }
            if (ds.Tables[0].Rows[0]["Comment"] != null)
            {
                this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
            }
            if (ds.Tables[0].Rows[0]["CurrencyCode"] != null)
            {
                this.CurrencyCode = ds.Tables[0].Rows[0]["CurrencyCode"].ToString();
            }
            if (ds.Tables[0].Rows[0]["TermsCode"] != null)
            {
                this.TermsCode = ds.Tables[0].Rows[0]["TermsCode"].ToString();
            }
            if (ds.Tables[0].Rows[0]["OrgRegCode"] != null)
            {
                this.OrgRegCode = ds.Tables[0].Rows[0]["OrgRegCode"].ToString();
            }
            if (ds.Tables[0].Rows[0]["EMailAddress"] != null)
            {
                this.EMailAddress = ds.Tables[0].Rows[0]["EMailAddress"].ToString();
            }
            if (ds.Tables[0].Rows[0]["PhoneNum"] != null)
            {
                this.PhoneNum = ds.Tables[0].Rows[0]["PhoneNum"].ToString();
            }
            if (ds.Tables[0].Rows[0]["FaxNum"] != null)
            {
                this.FaxNum = ds.Tables[0].Rows[0]["FaxNum"].ToString();
            }

        }
    }
    /// <summary>
    /// 得到一个对象实体
    /// </summary>
    public void GetModel(string Name)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select id,VendorNum ");
        strSql.Append(" FROM [ps_vendor] ");
        strSql.Append(" where Name=@Name ");
        SqlParameter[] parameters = {
                        new SqlParameter("@Name", SqlDbType.VarChar,50)};
        parameters[0].Value = Name;

        DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
            {
                this.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            }
            if (ds.Tables[0].Rows[0]["VendorNum"] != null && ds.Tables[0].Rows[0]["VendorNum"].ToString() != "")
            {
                this.VendorNum = int.Parse(ds.Tables[0].Rows[0]["VendorNum"].ToString());
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
        strSql.Append(" FROM [ps_vendor] ");
        if (strWhere.Trim() != "")
        {
            strSql.Append(" where " + strWhere);
        }
        return DbHelperSQL.Query(strSql.ToString());
    }

    /// <summary>
    /// 通过id获得对应的信息
    /// </summary>
    public DataTable GetList(int vendor_id)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from ps_vendor");
        if (vendor_id == 0)
        {
            //strSql.Append(" order by sort_id asc,id desc");
            strSql.Append(" order by sort_id asc,name asc");
        }
        else
        {
            //strSql.Append(" where status=1 and category_id=" + category_id + " order by sort_id asc,id desc");
            strSql.Append(" where Inactive=0 and id=" + vendor_id + " order by sort_id asc,name asc");
        }
        DataSet ds = DbHelperSQL.Query(strSql.ToString());
        return ds.Tables[0];
    }

    public DataTable GetVendorCntList(string companycode, string vendornum, string connum)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from ps_VendCnt");
        
        strSql.Append(" where Inactive=0 and company='" + companycode + "'  and vendornum='" + vendornum + "' and connum='" + connum + "' order by vendornum ");
        DataSet ds = DbHelperSQL.Query(strSql.ToString());
        return ds.Tables[0];
    }

    /// <summary>
    /// 获得查询分页数据
    /// </summary>
    public DataSet GetVendorCntList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * FROM  ps_VendCnt");
        if (strWhere.Trim() != "")
        {
            strSql.Append(" where " + strWhere);
        }
        recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
        return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
    }

    /// <summary>
    /// 通过id获得对应的信息
    /// </summary>
    public DataTable GetListByCatID(int vendor_cat_id)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from ps_vendor");
        if (vendor_cat_id == 0)
        {
            //strSql.Append(" order by sort_id asc,id desc");
            strSql.Append(" order by sort_id asc,name asc");
        }
        else
        {
            //strSql.Append(" where status=1 and category_id=" + category_id + " order by sort_id asc,id desc");
            strSql.Append(" where Inactive=0 and catid=" + vendor_cat_id + " order by sort_id asc,name asc");
        }
        DataSet ds = DbHelperSQL.Query(strSql.ToString());
        return ds.Tables[0];
    }

    /// <summary>
    /// 通过id获得对应的信息
    /// </summary>
    public DataTable GetListByCatID(int vendor_cat_id,string strWhere)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from ps_vendor");
        if (strWhere != "")
        {
            strSql.Append(" where " + strWhere);
        }
        else
        {
            strSql.Append(" where 1=1 " );
        }
        if (vendor_cat_id == 0)
        {
            //strSql.Append(" order by sort_id asc,id desc");
            strSql.Append(" order by sort_id asc,name asc");
        }
        else
        {
            //strSql.Append(" where status=1 and category_id=" + category_id + " order by sort_id asc,id desc");
            strSql.Append(" and Inactive=0 and catid=" + vendor_cat_id + " order by sort_id asc,name asc");
        }
        DataSet ds = DbHelperSQL.Query(strSql.ToString());
        return ds.Tables[0];
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
        strSql.Append(" FROM  ps_vendor");
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
        strSql.Append("select * FROM  ps_vendor");
        if (strWhere.Trim() != "")
        {
            strSql.Append(" where " + strWhere);
        }
        recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
        return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
    }
    #endregion  Method
}


