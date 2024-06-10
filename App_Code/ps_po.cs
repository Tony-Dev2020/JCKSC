using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// ps_rfq。
/// </summary>
[Serializable]
public partial class ps_po
{
    public ps_po()
    { }
    #region Model
    private int _id;
    private string _company;
    private bool _openorder;
    private bool _voidorder;
    private int _ponum;
    private string _entryperson;
    private DateTime? _orderdate;
    private string _fob;
    private string _shipviacode;
    private string _termscode;
    private string _shipname;
    private string _shipaddress1;
    private string _shipaddress2;
    private string _shipaddress3;
    private string _shipcity;
    private string _shipstate;
    private string _shipzip;
    private string _shipcountry;
    private string _buyerid;
    private bool _freightpp;
    private int _prcconnum;
    private int _vendornum;
    private string _purpoint;
    private string _commenttext;
    private bool _orderheld;
    private string _shiptoconname;
    private bool _readytoprint;
    private string _printas;
    private string _currencycode;
    private decimal _exchangerate;
    private bool _lockrate;
    private int _shipcountrynum;
    private bool _logchanges;
    private DateTime? _approveddate;
    private string _approvedby;
    private bool _approve;
    private string _approvalstatus;
    private decimal _approvedamount;
    private bool _posttoweb;
    private DateTime? _postdate;
    private string _vendorrefnum;
    private bool _confirmreq;
    private bool _confirmed;
    private string _confirmvia;
    private int _ordernum;
    private string _legalnumber;
    private int? _catid;
    private DateTime? _createdate = DateTime.Now;
    private DateTime? _confirmdate;
    /// <summary>
    /// 
    /// </summary>
    public int ID
    {
        set { _id = value; }
        get { return _id; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string Company
    {
        set { _company = value; }
        get { return _company; }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool OpenOrder
    {
        set { _openorder = value; }
        get { return _openorder; }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool VoidOrder
    {
        set { _voidorder = value; }
        get { return _voidorder; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int PONum
    {
        set { _ponum = value; }
        get { return _ponum; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string EntryPerson
    {
        set { _entryperson = value; }
        get { return _entryperson; }
    }
    /// <summary>
    /// 
    /// </summary>
    public DateTime? OrderDate
    {
        set { _orderdate = value; }
        get { return _orderdate; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string FOB
    {
        set { _fob = value; }
        get { return _fob; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ShipViaCode
    {
        set { _shipviacode = value; }
        get { return _shipviacode; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string TermsCode
    {
        set { _termscode = value; }
        get { return _termscode; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ShipName
    {
        set { _shipname = value; }
        get { return _shipname; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ShipAddress1
    {
        set { _shipaddress1 = value; }
        get { return _shipaddress1; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ShipAddress2
    {
        set { _shipaddress2 = value; }
        get { return _shipaddress2; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ShipAddress3
    {
        set { _shipaddress3 = value; }
        get { return _shipaddress3; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ShipCity
    {
        set { _shipcity = value; }
        get { return _shipcity; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ShipState
    {
        set { _shipstate = value; }
        get { return _shipstate; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ShipZIP
    {
        set { _shipzip = value; }
        get { return _shipzip; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ShipCountry
    {
        set { _shipcountry = value; }
        get { return _shipcountry; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string BuyerID
    {
        set { _buyerid = value; }
        get { return _buyerid; }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool FreightPP
    {
        set { _freightpp = value; }
        get { return _freightpp; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int PrcConNum
    {
        set { _prcconnum = value; }
        get { return _prcconnum; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int VendorNum
    {
        set { _vendornum = value; }
        get { return _vendornum; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string PurPoint
    {
        set { _purpoint = value; }
        get { return _purpoint; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string CommentText
    {
        set { _commenttext = value; }
        get { return _commenttext; }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool OrderHeld
    {
        set { _orderheld = value; }
        get { return _orderheld; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ShipToConName
    {
        set { _shiptoconname = value; }
        get { return _shiptoconname; }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool ReadyToPrint
    {
        set { _readytoprint = value; }
        get { return _readytoprint; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string PrintAs
    {
        set { _printas = value; }
        get { return _printas; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string CurrencyCode
    {
        set { _currencycode = value; }
        get { return _currencycode; }
    }
    /// <summary>
    /// 
    /// </summary>
    public decimal ExchangeRate
    {
        set { _exchangerate = value; }
        get { return _exchangerate; }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool LockRate
    {
        set { _lockrate = value; }
        get { return _lockrate; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int ShipCountryNum
    {
        set { _shipcountrynum = value; }
        get { return _shipcountrynum; }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool LogChanges
    {
        set { _logchanges = value; }
        get { return _logchanges; }
    }
    /// <summary>
    /// 
    /// </summary>
    public DateTime? ApprovedDate
    {
        set { _approveddate = value; }
        get { return _approveddate; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ApprovedBy
    {
        set { _approvedby = value; }
        get { return _approvedby; }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool Approve
    {
        set { _approve = value; }
        get { return _approve; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ApprovalStatus
    {
        set { _approvalstatus = value; }
        get { return _approvalstatus; }
    }
    /// <summary>
    /// 
    /// </summary>
    public decimal ApprovedAmount
    {
        set { _approvedamount = value; }
        get { return _approvedamount; }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool PostToWeb
    {
        set { _posttoweb = value; }
        get { return _posttoweb; }
    }
    /// <summary>
    /// 
    /// </summary>
    public DateTime? PostDate
    {
        set { _postdate = value; }
        get { return _postdate; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string VendorRefNum
    {
        set { _vendorrefnum = value; }
        get { return _vendorrefnum; }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool ConfirmReq
    {
        set { _confirmreq = value; }
        get { return _confirmreq; }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool Confirmed
    {
        set { _confirmed = value; }
        get { return _confirmed; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ConfirmVia
    {
        set { _confirmvia = value; }
        get { return _confirmvia; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int OrderNum
    {
        set { _ordernum = value; }
        get { return _ordernum; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string LegalNumber
    {
        set { _legalnumber = value; }
        get { return _legalnumber; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int? CatID
    {
        set { _catid = value; }
        get { return _catid; }
    }
    /// <summary>
    /// 
    /// </summary>
    public DateTime? CreateDate
    {
        set { _createdate = value; }
        get { return _createdate; }
    }
    /// <summary>
    /// 
    /// </summary>
    public DateTime? ConfirmDate
    {
        set { _confirmdate = value; }
        get { return _confirmdate; }
    }


    #endregion Model


    #region  Method


    /// <summary>
    /// 增加一条数据
    /// </summary>
    public int Add(ps_po model)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("insert into ps_POHeader(");
        strSql.Append("Company,OpenOrder,VoidOrder,PONum,EntryPerson,OrderDate,FOB,ShipViaCode,TermsCode,ShipName,ShipAddress1,ShipAddress2,ShipAddress3,ShipCity,ShipState,ShipZIP,ShipCountry,BuyerID,FreightPP,PrcConNum,VendorNum,PurPoint,CommentText,OrderHeld,ShipToConName,ReadyToPrint,PrintAs,CurrencyCode,ExchangeRate,LockRate,ShipCountryNum,LogChanges,ApprovedDate,ApprovedBy,Approve,ApprovalStatus,ApprovedAmount,PostToWeb,PostDate,VendorRefNum,ConfirmReq,Confirmed,ConfirmVia,OrderNum,LegalNumber,CatID,CreateDate,ConfirmDate)");
        strSql.Append(" values (");
        strSql.Append("@Company,@OpenOrder,@VoidOrder,@PONum,@EntryPerson,@OrderDate,@FOB,@ShipViaCode,@TermsCode,@ShipName,@ShipAddress1,@ShipAddress2,@ShipAddress3,@ShipCity,@ShipState,@ShipZIP,@ShipCountry,@BuyerID,@FreightPP,@PrcConNum,@VendorNum,@PurPoint,@CommentText,@OrderHeld,@ShipToConName,@ReadyToPrint,@PrintAs,@CurrencyCode,@ExchangeRate,@LockRate,@ShipCountryNum,@LogChanges,@ApprovedDate,@ApprovedBy,@Approve,@ApprovalStatus,@ApprovedAmount,@PostToWeb,@PostDate,@VendorRefNum,@ConfirmReq,@Confirmed,@ConfirmVia,@OrderNum,@LegalNumber,@CatID,@CreateDate,@ConfirmDate)");
        strSql.Append(";select @@IDENTITY");
        SqlParameter[] parameters = {
                    new SqlParameter("@Company", SqlDbType.NVarChar,8),
                    new SqlParameter("@OpenOrder", SqlDbType.Bit,1),
                    new SqlParameter("@VoidOrder", SqlDbType.Bit,1),
                    new SqlParameter("@PONum", SqlDbType.Int,4),
                    new SqlParameter("@EntryPerson", SqlDbType.NVarChar,75),
                    new SqlParameter("@OrderDate", SqlDbType.Date,3),
                    new SqlParameter("@FOB", SqlDbType.NVarChar,15),
                    new SqlParameter("@ShipViaCode", SqlDbType.NVarChar,4),
                    new SqlParameter("@TermsCode", SqlDbType.NVarChar,4),
                    new SqlParameter("@ShipName", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipAddress1", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipAddress2", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipAddress3", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipCity", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipState", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipZIP", SqlDbType.NVarChar,10),
                    new SqlParameter("@ShipCountry", SqlDbType.NVarChar,50),
                    new SqlParameter("@BuyerID", SqlDbType.NVarChar,8),
                    new SqlParameter("@FreightPP", SqlDbType.Bit,1),
                    new SqlParameter("@PrcConNum", SqlDbType.Int,4),
                    new SqlParameter("@VendorNum", SqlDbType.Int,4),
                    new SqlParameter("@PurPoint", SqlDbType.NVarChar,4),
                    new SqlParameter("@CommentText", SqlDbType.NVarChar,-1),
                    new SqlParameter("@OrderHeld", SqlDbType.Bit,1),
                    new SqlParameter("@ShipToConName", SqlDbType.NVarChar,50),
                    new SqlParameter("@ReadyToPrint", SqlDbType.Bit,1),
                    new SqlParameter("@PrintAs", SqlDbType.NVarChar,2),
                    new SqlParameter("@CurrencyCode", SqlDbType.NVarChar,4),
                    new SqlParameter("@ExchangeRate", SqlDbType.Decimal,13),
                    new SqlParameter("@LockRate", SqlDbType.Bit,1),
                    new SqlParameter("@ShipCountryNum", SqlDbType.Int,4),
                    new SqlParameter("@LogChanges", SqlDbType.Bit,1),
                    new SqlParameter("@ApprovedDate", SqlDbType.Date,3),
                    new SqlParameter("@ApprovedBy", SqlDbType.NVarChar,75),
                    new SqlParameter("@Approve", SqlDbType.Bit,1),
                    new SqlParameter("@ApprovalStatus", SqlDbType.NVarChar,2),
                    new SqlParameter("@ApprovedAmount", SqlDbType.Decimal,9),
                    new SqlParameter("@PostToWeb", SqlDbType.Bit,1),
                    new SqlParameter("@PostDate", SqlDbType.Date,3),
                    new SqlParameter("@VendorRefNum", SqlDbType.NVarChar,30),
                    new SqlParameter("@ConfirmReq", SqlDbType.Bit,1),
                    new SqlParameter("@Confirmed", SqlDbType.Bit,1),
                    new SqlParameter("@ConfirmVia", SqlDbType.NVarChar,8),
                    new SqlParameter("@OrderNum", SqlDbType.Int,4),
                    new SqlParameter("@LegalNumber", SqlDbType.NVarChar,30),
                    new SqlParameter("@CatID", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@ConfirmDate", SqlDbType.DateTime)};
        parameters[0].Value = model.Company;
        parameters[1].Value = model.OpenOrder;
        parameters[2].Value = model.VoidOrder;
        parameters[3].Value = model.PONum;
        parameters[4].Value = model.EntryPerson;
        parameters[5].Value = model.OrderDate;
        parameters[6].Value = model.FOB;
        parameters[7].Value = model.ShipViaCode;
        parameters[8].Value = model.TermsCode;
        parameters[9].Value = model.ShipName;
        parameters[10].Value = model.ShipAddress1;
        parameters[11].Value = model.ShipAddress2;
        parameters[12].Value = model.ShipAddress3;
        parameters[13].Value = model.ShipCity;
        parameters[14].Value = model.ShipState;
        parameters[15].Value = model.ShipZIP;
        parameters[16].Value = model.ShipCountry;
        parameters[17].Value = model.BuyerID;
        parameters[18].Value = model.FreightPP;
        parameters[19].Value = model.PrcConNum;
        parameters[20].Value = model.VendorNum;
        parameters[21].Value = model.PurPoint;
        parameters[22].Value = model.CommentText;
        parameters[23].Value = model.OrderHeld;
        parameters[24].Value = model.ShipToConName;
        parameters[25].Value = model.ReadyToPrint;
        parameters[26].Value = model.PrintAs;
        parameters[27].Value = model.CurrencyCode;
        parameters[28].Value = model.ExchangeRate;
        parameters[29].Value = model.LockRate;
        parameters[30].Value = model.ShipCountryNum;
        parameters[31].Value = model.LogChanges;
        parameters[32].Value = model.ApprovedDate;
        parameters[33].Value = model.ApprovedBy;
        parameters[34].Value = model.Approve;
        parameters[35].Value = model.ApprovalStatus;
        parameters[36].Value = model.ApprovedAmount;
        parameters[37].Value = model.PostToWeb;
        parameters[38].Value = model.PostDate;
        parameters[39].Value = model.VendorRefNum;
        parameters[40].Value = model.ConfirmReq;
        parameters[41].Value = model.Confirmed;
        parameters[42].Value = model.ConfirmVia;
        parameters[43].Value = model.OrderNum;
        parameters[44].Value = model.LegalNumber;
        parameters[45].Value = model.CatID;
        parameters[46].Value = model.CreateDate;
        parameters[47].Value = model.ConfirmDate;

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
    public bool Update(ps_po model)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("update ps_POHeader set ");
        strSql.Append("Company=@Company,");
        strSql.Append("OpenOrder=@OpenOrder,");
        strSql.Append("VoidOrder=@VoidOrder,");
        strSql.Append("PONum=@PONum,");
        strSql.Append("EntryPerson=@EntryPerson,");
        strSql.Append("OrderDate=@OrderDate,");
        strSql.Append("FOB=@FOB,");
        strSql.Append("ShipViaCode=@ShipViaCode,");
        strSql.Append("TermsCode=@TermsCode,");
        strSql.Append("ShipName=@ShipName,");
        strSql.Append("ShipAddress1=@ShipAddress1,");
        strSql.Append("ShipAddress2=@ShipAddress2,");
        strSql.Append("ShipAddress3=@ShipAddress3,");
        strSql.Append("ShipCity=@ShipCity,");
        strSql.Append("ShipState=@ShipState,");
        strSql.Append("ShipZIP=@ShipZIP,");
        strSql.Append("ShipCountry=@ShipCountry,");
        strSql.Append("BuyerID=@BuyerID,");
        strSql.Append("FreightPP=@FreightPP,");
        strSql.Append("PrcConNum=@PrcConNum,");
        strSql.Append("VendorNum=@VendorNum,");
        strSql.Append("PurPoint=@PurPoint,");
        strSql.Append("CommentText=@CommentText,");
        strSql.Append("OrderHeld=@OrderHeld,");
        strSql.Append("ShipToConName=@ShipToConName,");
        strSql.Append("ReadyToPrint=@ReadyToPrint,");
        strSql.Append("PrintAs=@PrintAs,");
        strSql.Append("CurrencyCode=@CurrencyCode,");
        strSql.Append("ExchangeRate=@ExchangeRate,");
        strSql.Append("LockRate=@LockRate,");
        strSql.Append("ShipCountryNum=@ShipCountryNum,");
        strSql.Append("LogChanges=@LogChanges,");
        strSql.Append("ApprovedDate=@ApprovedDate,");
        strSql.Append("ApprovedBy=@ApprovedBy,");
        strSql.Append("Approve=@Approve,");
        strSql.Append("ApprovalStatus=@ApprovalStatus,");
        strSql.Append("ApprovedAmount=@ApprovedAmount,");
        strSql.Append("PostToWeb=@PostToWeb,");
        strSql.Append("PostDate=@PostDate,");
        strSql.Append("VendorRefNum=@VendorRefNum,");
        strSql.Append("ConfirmReq=@ConfirmReq,");
        strSql.Append("Confirmed=@Confirmed,");
        strSql.Append("ConfirmVia=@ConfirmVia,");
        strSql.Append("OrderNum=@OrderNum,");
        strSql.Append("LegalNumber=@LegalNumber,");
        strSql.Append("CatID=@CatID,");
        strSql.Append("CreateDate=@CreateDate,");
        strSql.Append("ConfirmDate=@ConfirmDate");
        strSql.Append(" where ID=@ID");
        SqlParameter[] parameters = {
                    new SqlParameter("@Company", SqlDbType.NVarChar,8),
                    new SqlParameter("@OpenOrder", SqlDbType.Bit,1),
                    new SqlParameter("@VoidOrder", SqlDbType.Bit,1),
                    new SqlParameter("@PONum", SqlDbType.Int,4),
                    new SqlParameter("@EntryPerson", SqlDbType.NVarChar,75),
                    new SqlParameter("@OrderDate", SqlDbType.Date,3),
                    new SqlParameter("@FOB", SqlDbType.NVarChar,15),
                    new SqlParameter("@ShipViaCode", SqlDbType.NVarChar,4),
                    new SqlParameter("@TermsCode", SqlDbType.NVarChar,4),
                    new SqlParameter("@ShipName", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipAddress1", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipAddress2", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipAddress3", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipCity", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipState", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipZIP", SqlDbType.NVarChar,10),
                    new SqlParameter("@ShipCountry", SqlDbType.NVarChar,50),
                    new SqlParameter("@BuyerID", SqlDbType.NVarChar,8),
                    new SqlParameter("@FreightPP", SqlDbType.Bit,1),
                    new SqlParameter("@PrcConNum", SqlDbType.Int,4),
                    new SqlParameter("@VendorNum", SqlDbType.Int,4),
                    new SqlParameter("@PurPoint", SqlDbType.NVarChar,4),
                    new SqlParameter("@CommentText", SqlDbType.NVarChar,-1),
                    new SqlParameter("@OrderHeld", SqlDbType.Bit,1),
                    new SqlParameter("@ShipToConName", SqlDbType.NVarChar,50),
                    new SqlParameter("@ReadyToPrint", SqlDbType.Bit,1),
                    new SqlParameter("@PrintAs", SqlDbType.NVarChar,2),
                    new SqlParameter("@CurrencyCode", SqlDbType.NVarChar,4),
                    new SqlParameter("@ExchangeRate", SqlDbType.Decimal,13),
                    new SqlParameter("@LockRate", SqlDbType.Bit,1),
                    new SqlParameter("@ShipCountryNum", SqlDbType.Int,4),
                    new SqlParameter("@LogChanges", SqlDbType.Bit,1),
                    new SqlParameter("@ApprovedDate", SqlDbType.Date,3),
                    new SqlParameter("@ApprovedBy", SqlDbType.NVarChar,75),
                    new SqlParameter("@Approve", SqlDbType.Bit,1),
                    new SqlParameter("@ApprovalStatus", SqlDbType.NVarChar,2),
                    new SqlParameter("@ApprovedAmount", SqlDbType.Decimal,9),
                    new SqlParameter("@PostToWeb", SqlDbType.Bit,1),
                    new SqlParameter("@PostDate", SqlDbType.Date,3),
                    new SqlParameter("@VendorRefNum", SqlDbType.NVarChar,30),
                    new SqlParameter("@ConfirmReq", SqlDbType.Bit,1),
                    new SqlParameter("@Confirmed", SqlDbType.Bit,1),
                    new SqlParameter("@ConfirmVia", SqlDbType.NVarChar,8),
                    new SqlParameter("@OrderNum", SqlDbType.Int,4),
                    new SqlParameter("@LegalNumber", SqlDbType.NVarChar,30),
                    new SqlParameter("@CatID", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@ConfirmDate", SqlDbType.DateTime),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
        parameters[0].Value = model.Company;
        parameters[1].Value = model.OpenOrder;
        parameters[2].Value = model.VoidOrder;
        parameters[3].Value = model.PONum;
        parameters[4].Value = model.EntryPerson;
        parameters[5].Value = model.OrderDate;
        parameters[6].Value = model.FOB;
        parameters[7].Value = model.ShipViaCode;
        parameters[8].Value = model.TermsCode;
        parameters[9].Value = model.ShipName;
        parameters[10].Value = model.ShipAddress1;
        parameters[11].Value = model.ShipAddress2;
        parameters[12].Value = model.ShipAddress3;
        parameters[13].Value = model.ShipCity;
        parameters[14].Value = model.ShipState;
        parameters[15].Value = model.ShipZIP;
        parameters[16].Value = model.ShipCountry;
        parameters[17].Value = model.BuyerID;
        parameters[18].Value = model.FreightPP;
        parameters[19].Value = model.PrcConNum;
        parameters[20].Value = model.VendorNum;
        parameters[21].Value = model.PurPoint;
        parameters[22].Value = model.CommentText;
        parameters[23].Value = model.OrderHeld;
        parameters[24].Value = model.ShipToConName;
        parameters[25].Value = model.ReadyToPrint;
        parameters[26].Value = model.PrintAs;
        parameters[27].Value = model.CurrencyCode;
        parameters[28].Value = model.ExchangeRate;
        parameters[29].Value = model.LockRate;
        parameters[30].Value = model.ShipCountryNum;
        parameters[31].Value = model.LogChanges;
        parameters[32].Value = model.ApprovedDate;
        parameters[33].Value = model.ApprovedBy;
        parameters[34].Value = model.Approve;
        parameters[35].Value = model.ApprovalStatus;
        parameters[36].Value = model.ApprovedAmount;
        parameters[37].Value = model.PostToWeb;
        parameters[38].Value = model.PostDate;
        parameters[39].Value = model.VendorRefNum;
        parameters[40].Value = model.ConfirmReq;
        parameters[41].Value = model.Confirmed;
        parameters[42].Value = model.ConfirmVia;
        parameters[43].Value = model.OrderNum;
        parameters[44].Value = model.LegalNumber;
        parameters[45].Value = model.CatID;
        parameters[46].Value = model.CreateDate;
        parameters[47].Value = model.ConfirmDate;
        parameters[48].Value = model.ID;

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
    public bool Delete(int ID)
    {

        StringBuilder strSql = new StringBuilder();
        strSql.Append("delete from ps_POHeader ");
        strSql.Append(" where ID=@ID");
        SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
        parameters[0].Value = ID;

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
    public bool DeleteList(string IDlist)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("delete from ps_POHeader ");
        strSql.Append(" where ID in (" + IDlist + ")  ");
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
    public ps_po GetModel(int ID)
    {

        StringBuilder strSql = new StringBuilder();
        strSql.Append("select  top 1 ID,Company,OpenOrder,VoidOrder,PONum,EntryPerson,OrderDate,FOB,ShipViaCode,TermsCode,ShipName,ShipAddress1,ShipAddress2,ShipAddress3,ShipCity,ShipState,ShipZIP,ShipCountry,BuyerID,FreightPP,PrcConNum,VendorNum,PurPoint,CommentText,OrderHeld,ShipToConName,ReadyToPrint,PrintAs,CurrencyCode,ExchangeRate,LockRate,ShipCountryNum,LogChanges,ApprovedDate,ApprovedBy,Approve,ApprovalStatus,ApprovedAmount,PostToWeb,PostDate,VendorRefNum,ConfirmReq,Confirmed,ConfirmVia,OrderNum,LegalNumber,CatID,CreateDate,ConfirmDate from ps_POHeader ");
        strSql.Append(" where ID=@ID");
        SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
        parameters[0].Value = ID;

        ps_po model = new ps_po();
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
    public ps_po DataRowToModel(DataRow row)
    {
        ps_po model = new ps_po();
        if (row != null)
        {
            if (row["ID"] != null && row["ID"].ToString() != "")
            {
                model.ID = int.Parse(row["ID"].ToString());
            }
            if (row["Company"] != null)
            {
                model.Company = row["Company"].ToString();
            }
            if (row["OpenOrder"] != null && row["OpenOrder"].ToString() != "")
            {
                if ((row["OpenOrder"].ToString() == "1") || (row["OpenOrder"].ToString().ToLower() == "true"))
                {
                    model.OpenOrder = true;
                }
                else
                {
                    model.OpenOrder = false;
                }
            }
            if (row["VoidOrder"] != null && row["VoidOrder"].ToString() != "")
            {
                if ((row["VoidOrder"].ToString() == "1") || (row["VoidOrder"].ToString().ToLower() == "true"))
                {
                    model.VoidOrder = true;
                }
                else
                {
                    model.VoidOrder = false;
                }
            }
            if (row["PONum"] != null && row["PONum"].ToString() != "")
            {
                model.PONum = int.Parse(row["PONum"].ToString());
            }
            if (row["EntryPerson"] != null)
            {
                model.EntryPerson = row["EntryPerson"].ToString();
            }
            if (row["OrderDate"] != null && row["OrderDate"].ToString() != "")
            {
                model.OrderDate = DateTime.Parse(row["OrderDate"].ToString());
            }
            if (row["FOB"] != null)
            {
                model.FOB = row["FOB"].ToString();
            }
            if (row["ShipViaCode"] != null)
            {
                model.ShipViaCode = row["ShipViaCode"].ToString();
            }
            if (row["TermsCode"] != null)
            {
                model.TermsCode = row["TermsCode"].ToString();
            }
            if (row["ShipName"] != null)
            {
                model.ShipName = row["ShipName"].ToString();
            }
            if (row["ShipAddress1"] != null)
            {
                model.ShipAddress1 = row["ShipAddress1"].ToString();
            }
            if (row["ShipAddress2"] != null)
            {
                model.ShipAddress2 = row["ShipAddress2"].ToString();
            }
            if (row["ShipAddress3"] != null)
            {
                model.ShipAddress3 = row["ShipAddress3"].ToString();
            }
            if (row["ShipCity"] != null)
            {
                model.ShipCity = row["ShipCity"].ToString();
            }
            if (row["ShipState"] != null)
            {
                model.ShipState = row["ShipState"].ToString();
            }
            if (row["ShipZIP"] != null)
            {
                model.ShipZIP = row["ShipZIP"].ToString();
            }
            if (row["ShipCountry"] != null)
            {
                model.ShipCountry = row["ShipCountry"].ToString();
            }
            if (row["BuyerID"] != null)
            {
                model.BuyerID = row["BuyerID"].ToString();
            }
            if (row["FreightPP"] != null && row["FreightPP"].ToString() != "")
            {
                if ((row["FreightPP"].ToString() == "1") || (row["FreightPP"].ToString().ToLower() == "true"))
                {
                    model.FreightPP = true;
                }
                else
                {
                    model.FreightPP = false;
                }
            }
            if (row["PrcConNum"] != null && row["PrcConNum"].ToString() != "")
            {
                model.PrcConNum = int.Parse(row["PrcConNum"].ToString());
            }
            if (row["VendorNum"] != null && row["VendorNum"].ToString() != "")
            {
                model.VendorNum = int.Parse(row["VendorNum"].ToString());
            }
            if (row["PurPoint"] != null)
            {
                model.PurPoint = row["PurPoint"].ToString();
            }
            if (row["CommentText"] != null)
            {
                model.CommentText = row["CommentText"].ToString();
            }
            if (row["OrderHeld"] != null && row["OrderHeld"].ToString() != "")
            {
                if ((row["OrderHeld"].ToString() == "1") || (row["OrderHeld"].ToString().ToLower() == "true"))
                {
                    model.OrderHeld = true;
                }
                else
                {
                    model.OrderHeld = false;
                }
            }
            if (row["ShipToConName"] != null)
            {
                model.ShipToConName = row["ShipToConName"].ToString();
            }
            if (row["ReadyToPrint"] != null && row["ReadyToPrint"].ToString() != "")
            {
                if ((row["ReadyToPrint"].ToString() == "1") || (row["ReadyToPrint"].ToString().ToLower() == "true"))
                {
                    model.ReadyToPrint = true;
                }
                else
                {
                    model.ReadyToPrint = false;
                }
            }
            if (row["PrintAs"] != null)
            {
                model.PrintAs = row["PrintAs"].ToString();
            }
            if (row["CurrencyCode"] != null)
            {
                model.CurrencyCode = row["CurrencyCode"].ToString();
            }
            if (row["ExchangeRate"] != null && row["ExchangeRate"].ToString() != "")
            {
                model.ExchangeRate = decimal.Parse(row["ExchangeRate"].ToString());
            }
            if (row["LockRate"] != null && row["LockRate"].ToString() != "")
            {
                if ((row["LockRate"].ToString() == "1") || (row["LockRate"].ToString().ToLower() == "true"))
                {
                    model.LockRate = true;
                }
                else
                {
                    model.LockRate = false;
                }
            }
            if (row["ShipCountryNum"] != null && row["ShipCountryNum"].ToString() != "")
            {
                model.ShipCountryNum = int.Parse(row["ShipCountryNum"].ToString());
            }
            if (row["LogChanges"] != null && row["LogChanges"].ToString() != "")
            {
                if ((row["LogChanges"].ToString() == "1") || (row["LogChanges"].ToString().ToLower() == "true"))
                {
                    model.LogChanges = true;
                }
                else
                {
                    model.LogChanges = false;
                }
            }
            if (row["ApprovedDate"] != null && row["ApprovedDate"].ToString() != "")
            {
                model.ApprovedDate = DateTime.Parse(row["ApprovedDate"].ToString());
            }
            if (row["ApprovedBy"] != null)
            {
                model.ApprovedBy = row["ApprovedBy"].ToString();
            }
            if (row["Approve"] != null && row["Approve"].ToString() != "")
            {
                if ((row["Approve"].ToString() == "1") || (row["Approve"].ToString().ToLower() == "true"))
                {
                    model.Approve = true;
                }
                else
                {
                    model.Approve = false;
                }
            }
            if (row["ApprovalStatus"] != null)
            {
                model.ApprovalStatus = row["ApprovalStatus"].ToString();
            }
            if (row["ApprovedAmount"] != null && row["ApprovedAmount"].ToString() != "")
            {
                model.ApprovedAmount = decimal.Parse(row["ApprovedAmount"].ToString());
            }
            if (row["PostToWeb"] != null && row["PostToWeb"].ToString() != "")
            {
                if ((row["PostToWeb"].ToString() == "1") || (row["PostToWeb"].ToString().ToLower() == "true"))
                {
                    model.PostToWeb = true;
                }
                else
                {
                    model.PostToWeb = false;
                }
            }
            if (row["PostDate"] != null && row["PostDate"].ToString() != "")
            {
                model.PostDate = DateTime.Parse(row["PostDate"].ToString());
            }
            if (row["VendorRefNum"] != null)
            {
                model.VendorRefNum = row["VendorRefNum"].ToString();
            }
            if (row["ConfirmReq"] != null && row["ConfirmReq"].ToString() != "")
            {
                if ((row["ConfirmReq"].ToString() == "1") || (row["ConfirmReq"].ToString().ToLower() == "true"))
                {
                    model.ConfirmReq = true;
                }
                else
                {
                    model.ConfirmReq = false;
                }
            }
            if (row["Confirmed"] != null && row["Confirmed"].ToString() != "")
            {
                if ((row["Confirmed"].ToString() == "1") || (row["Confirmed"].ToString().ToLower() == "true"))
                {
                    model.Confirmed = true;
                }
                else
                {
                    model.Confirmed = false;
                }
            }
            if (row["ConfirmVia"] != null)
            {
                model.ConfirmVia = row["ConfirmVia"].ToString();
            }
            if (row["OrderNum"] != null && row["OrderNum"].ToString() != "")
            {
                model.OrderNum = int.Parse(row["OrderNum"].ToString());
            }
            if (row["LegalNumber"] != null)
            {
                model.LegalNumber = row["LegalNumber"].ToString();
            }
            if (row["CatID"] != null && row["CatID"].ToString() != "")
            {
                model.CatID = int.Parse(row["CatID"].ToString());
            }
            if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
            {
                model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
            }
            if (row["ConfirmDate"] != null && row["ConfirmDate"].ToString() != "")
            {
                model.ConfirmDate = DateTime.Parse(row["ConfirmDate"].ToString());
            }
        }
        return model;
    }




    public bool ComfirmPO(int ID, DateTime ConfirmDate,string VendorRemark)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("Update ps_POHeader set Confirmed=1,PromiseDate = @ConfirmDate,ConfirmDate=GETDATE(),VendorRemark = @VendorRemark ");
        strSql.Append(" where ID=@ID");
        SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@ConfirmDate", SqlDbType.Date),
                    new SqlParameter("@VendorRemark", SqlDbType.VarChar,500)
        };
        parameters[0].Value = ID;
        parameters[1].Value = ConfirmDate;
        parameters[2].Value = VendorRemark;

        int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        if (rows > 0)
        {
            ComfirmEpicorPO(ID, ConfirmDate);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ComfirmEpicorPO(int ID,DateTime ConfirmDate)
    {
        string ErpDB = System.Configuration.ConfigurationManager.AppSettings["ErpDB"];
        StringBuilder strSql = new StringBuilder();
        strSql.Append("update A set A.Confirmed = 1, PromiseDate = @ConfirmDate from " + ErpDB + ".Erp.POHeader A inner join ps_POHeader B on A.Company = B.Company AND A.PONum = B.PONum ");
        strSql.Append(" where B.ID=@ID");
        SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@ConfirmDate", SqlDbType.Date)
            };
        parameters[0].Value = ID;
        parameters[1].Value = ConfirmDate;

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
    /// 获得查询分页数据
    /// </summary>
    public DataSet GetListByID(int id, out int recordCount)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * FROM  ps_POHeader");
        if (id > 0)
        {
            strSql.Append(" where id=" + id);
        }
        recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
        return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, 1, 1, strSql.ToString(), " id"));
    }

    /// <summary>
    /// 获得查询分页数据
    /// </summary>
    public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * FROM  v_POHeader ");
        if (strWhere.Trim() != "")
        {
            strSql.Append(" where " + strWhere);
        }
        recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
        return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
    }

    #endregion  Method
}


