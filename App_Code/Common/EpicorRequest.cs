using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;   //for HttpWebRequest
using System.Net.Http;  //for HttpClient
using System.Net.Http.Headers;

using System.Security.Permissions;
using System.Web.Script.Serialization;
using System.Text;
using System.IO;

/// <summary>
/// Summary description for EpicorRequest
/// </summary>
public class EpicorRequest
{
    public EpicorRequest()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// 获得数据列表
    /// </summary>
    public DataSet GetList(string strWhere)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * ");
        strSql.Append(" FROM [ps_epicor_config] ");
        if (strWhere.Trim() != "")
        {
            strSql.Append(" where " + strWhere);
        }
        return DbHelperSQL.Query(strSql.ToString());
    }

    //Authorization
    //public static string UserAndPw = "epicor:epicor";    //epicor UserID:Password
    //public static string APIKey = "TTn49xgm6blv6ZW4bxWH2bjczxq3mds921WlzNgb0NtRd";
    //public static string ServerName = "ewin2019s3";
    //public static string AppServerName = "kinetic2022";

    public static string UserAndPw = "";    //epicor UserID:Password
    public static string APIKey = "";
    public static string ServerName = "";
    public static string AppServerName = "";
    public static string AppCompany = "";
    public static string License = "";

    //Post
    public static string RequestURLCompany = (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany;
    public static string RequestURLFormat = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.FAssetSvc/FAssets";
    public static string Company = (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany;
    public static string AssetNum = "F0010";
    public static string AssetDesc = "F0010 xxxx";
    public static string AcquiredDate = "2020-8-20";
    public static string CommissionedDate = "2020-8-20";
    public static string GroupCode = "OF";
    public static string ClassCode = "DESKS";
    public static string ud_date01_c = "";
    public static string ud_number01_c = "0";
    public static string ud_ShortChar01_c = "";

    //Delete
    public static string DelRequestURLCompany = (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany;
    public static string DelRequestURLFormat = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.FAssetSvc/FAssets('{Company}','{AssetNum}')";
    public static string DelFAssetDtlsURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.FAssetSvc/FAssetDtls('{Company}','{AssetNum}','{AssetRegID}')";
    public static string DelCompany = (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany;
    public static string DelAssetNum = "F0010";
    public static string DelAssetRegID = "MAIN";

    //Get
    public static string GetRequestURLCompany = (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany;
    public static string GetRequestURLFormat = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
    //public static string RequestURLFormat = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
    public static string GetCompany = (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany;
    public static string GetAssetNum = "F0010";

    public class PartData
    {
        public string Company { get; set; }
        public string PartNum { get; set; }
        public string SearchWord { get; set; }
        public string PartDescription { get; set; }
    }

    public string GetMaxPartNum(string Calculated_Part)
    {
        try
        {
            string MaxPartNum = "";
            //final RequestURL
            string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/GetMaxPartnumber/Data?%24filter=Calculated_Part%20eq%20'{Calculated_Part}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", "C01");
            RequestURL = RequestURL.Replace("{Calculated_Part}", Calculated_Part);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            List<PartData> lsPartDatas = new List<PartData> { };
            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    for (int i = 0; i < obj.Value.Count; i++)
                    {

                        //partData.Company = obj.Value[i]["Company"];
                        string CalculatedMaxNumber = obj.Value[i]["Calculated_MaxNumber"].ToString();
                        CalculatedMaxNumber = CalculatedMaxNumber.Split('-')[0].ToString();
                        MaxPartNum = "000000" +(Convert.ToInt32(CalculatedMaxNumber) + 1).ToString();
     
                        MaxPartNum = Calculated_Part + MaxPartNum.Substring(MaxPartNum.Length - 6, 6);
                        //partData.PartDescription = obj.Value[i]["PartDescription"];
                        break;
                    }
                }
            }
            return MaxPartNum;

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }

    public string CopyPart(string Company, string PartNum,string NewPartDescription, string CustSize)
    {
        try
        {
            //final RequestURL
            string MaxPartNum = "";
            string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=PartNum %20eq%20'{PartNum}'"; ;
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            RequestURL = RequestURL.Replace("{PartNum}", PartNum);




            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            if (ResponseStatusCode == "200" && ResponseBody != "")
            {
                MaxPartNum = GetMaxPartNum(PartNum.Substring(0,6));
                ResponseBody = ResponseBody.Split('[')[1].ToString();
                ResponseBody = ResponseBody.Substring(0, ResponseBody.Length - 2);
                ResponseBody = ResponseBody.Replace(PartNum, MaxPartNum);

                string PartDescription = "";
                string UD_OldPartNum_c = "";
                string UD_tpluslog_c = "";
                //string GlobalPart = "";
                string Specification = "";

                if (NewPartDescription != "")
                {
                    PartDescription = ResponseBody.Split(new string[] { "\"PartDescription\"" }, StringSplitOptions.None)[1].ToString();
                    PartDescription = PartDescription.Split(new string[] { "\",\"" }, StringSplitOptions.None)[0].ToString();
                    ResponseBody = ResponseBody.Replace("\"PartDescription\"" + PartDescription, "\"PartDescription\":\"" + NewPartDescription);
                }

                UD_OldPartNum_c =  ResponseBody.Split(new string[] { "\"UD_OldPartNum_c\"" }, StringSplitOptions.None)[1].ToString();
                UD_OldPartNum_c = UD_OldPartNum_c.Split(new string[] { "\",\"" }, StringSplitOptions.None)[0].ToString();
                ResponseBody = ResponseBody.Replace("\"UD_OldPartNum_c\""+ UD_OldPartNum_c, "\"UD_OldPartNum_c\":\"");

                UD_tpluslog_c = ResponseBody.Split(new string[] { "\"UD_tpluslog_c\"" }, StringSplitOptions.None)[1].ToString();
                UD_tpluslog_c = UD_tpluslog_c.Split(new string[] { "\",\"" }, StringSplitOptions.None)[0].ToString();
                ResponseBody = ResponseBody.Replace("\"UD_tpluslog_c\"" + UD_tpluslog_c, "\"UD_tpluslog_c\":\"未同步");

                //GlobalPart =  ResponseBody.Split(new string[] { "\"GlobalPart\"" }, StringSplitOptions.None)[1].ToString();
                //GlobalPart = GlobalPart.Split(new string[] { "\",\"" }, StringSplitOptions.None)[0].ToString();
                ResponseBody = ResponseBody.Replace("\"GlobalPart\":true" , "\"GlobalPart\":false");

                ResponseBody = ResponseBody.Replace("\"UD_CanCust_c\":true", "\"UD_CanCust_c\":false");
                ResponseBody = ResponseBody.Replace("\"UD_CanDispPlat_c\":true", "\"UD_CanDispPlat_c\":false");


                Specification = ResponseBody.Split(new string[] { "\"Specification\"" }, StringSplitOptions.None)[1].ToString();
                Specification = Specification.Split(new string[] { "\",\"" }, StringSplitOptions.None)[0].ToString();
                ResponseBody = ResponseBody.Replace("\"Specification\"" + Specification, "\"Specification\":\"" + CustSize);



                ResponseBody = ResponseBody.Replace(Company, "C01");
            }


            isoJson = ResponseBody;

            isoJson = isoJson.Replace("\r\n", "");
            isoJson = isoJson.Replace("\"0001-01-01T00:00:00Z\"", "null");
            isoJson = isoJson.Replace("00:00:00", "00:00:00Z");

            //final RequestURL
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts";
            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", "C01");



            //string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "POST";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);

            string strHTTPStatusCode = ResponseStatusCode;
            string strResponseBody = ResponseBody;
            string strExceptionMsg = ExceptionMsg;
            string strTDeserializeResponseBodyErrorMessage = ErrorMessage;


            if (ResponseStatusCode == "201")
            {
                return MaxPartNum;
            }
            else
                return "";



        }
        catch (AggregateException ex)
        {
            return null;
        }
    }

    public PartData[] GetPartNum(string strWhere, string Title)
    {
        try
        {
            //final RequestURL
            string RequestURL = GetRequestURLFormat;
            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", GetRequestURLCompany);
            RequestURL = RequestURL.Replace("{Company}", GetCompany);
            RequestURL = RequestURL.Replace("{AssetNum}", GetAssetNum);

  

            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            List<PartData> lsPartDatas = new List<PartData> { };
            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    for (int i = 0; i < obj.Value.Count; i++)
                    {
                        PartData partData = new PartData();
                        partData.Company = obj.Value[i]["Company"];
                        partData.PartNum = obj.Value[i]["PartNum"];
                        partData.PartDescription = obj.Value[i]["PartDescription"];
                        lsPartDatas.Add(partData);
                    }
                }
            }
            return lsPartDatas.ToArray();

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }


    




    public class PartPriceRequest
    {
        public string icCustID { get; set; }
        public string icShipToNum { get; set; }
        public string icPartNum { get; set; }
        public string icCustGroupCode { get; set; }
        public string icProductCode { get; set; }
        public double idQuantity { get; set; }
        public string icUOMCode { get; set; }
        public string icWarehouseCode { get; set; }
        public string icCurrencyCode { get; set; }
        public int pageSize { get; set; }
        public int absolutePage { get; set; }
    }

    public class PartPricelist
    {
        public string PartNum { get; set; }
        public double NetPrice { get; set; }
        public double DiscountPercent { get; set; }
    }

    public PartPricelist[] GetPartPricelist(string Company, string CustID, string PartNum, double Quantity, string UOM)
    {
        try
        {
            //final RequestURL
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }

            string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PriceListInquirySvc/GetPriceListInquiry";
            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);

            PartPriceRequest entry = new PartPriceRequest
            {
                icCustID = CustID,
                icShipToNum = "",
                icPartNum = PartNum,
                icCustGroupCode = "",
                icProductCode = "",
                idQuantity = Quantity,
                icUOMCode = UOM,
                icWarehouseCode = "",
                icCurrencyCode = "RMB",
                pageSize = 0,
                absolutePage = 0
            };
            //PartPriceRequest entry = new PartPriceRequest
            //{
            //    icCustID = "TP24000200",
            //    icShipToNum = "",
            //    icPartNum = "P10201000001",
            //    icCustGroupCode = "",
            //    icProductCode = "",
            //    idQuantity = Quantity,
            //    icUOMCode = "PC",
            //    icWarehouseCode = "",
            //    icCurrencyCode = "RMB",
            //    pageSize = 0,
            //    absolutePage = 0
            //};

            string isoJson = JsonConvert.SerializeObject(entry);    //Convert DataEntry to Json string

            isoJson = isoJson.Replace("\r\n", "");
            isoJson = isoJson.Replace("\"0001-01-01T00:00:00Z\"", "null");
            isoJson = isoJson.Replace("00:00:00", "00:00:00Z");


            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "POST";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            string strHTTPStatusCode = ResponseStatusCode;
            string strResponseBody = ResponseBody;
            string strExceptionMsg = ExceptionMsg;
            string strTDeserializeResponseBodyErrorMessage = ErrorMessage;

            if (ResponseStatusCode == "200")
            {

                //JavaScriptSerializer jss = new JavaScriptSerializer();

                dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                if (dyn == null) return null;
                List<PartPricelist> lsPartPricelist = new List<PartPricelist> { };
                foreach (var obj in dyn)
                {
                    if (obj.Name == "returnObj")
                    {
                        //ErrorMessage = Convert.ToString(dyn.Last.Value);
                        //JavaScriptSerializer jss = new JavaScriptSerializer();
                        //反序列化成Part对象
                        //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                        dynamic dynValue = Newtonsoft.Json.JsonConvert.DeserializeObject(obj.First.ToString());
                        foreach (var objdetail in dynValue)
                        {
                            if (objdetail.Name == "PriceListInquiry")
                            {
                                for (int i = 0; i < objdetail.Value.Count; i++)
                                {
                                    PartPricelist partPrice = new PartPricelist();
                                    //partPrice.PartNum = objdetail.Value[i]["PartNum"];
                                    partPrice.NetPrice = objdetail.Value[i]["NetPrice"];
                                    partPrice.DiscountPercent = objdetail.Value[i]["DiscountPercent"];
                                    lsPartPricelist.Add(partPrice);
                                }
                            }
                        }
                    }
                }
                return lsPartPricelist.ToArray();
            }
            else
            {
                return null;
            }
        }
        catch (AggregateException ex)
        {
            return null;
        }
    }
    public class SalesOrder
    {
        public string Company { get; set; }
        public int OrderNum { get; set; }
        public int CustNum { get; set; }
        public string PONum { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime NeedByDate { get; set; }
        //public string FFContact { get; set; }
        //public string FFPhoneNum { get; set; }
        //public string FFAddress1 { get; set; }
        public bool UseOTS { get; set; }
        public string OTSName { get; set; }
        public string OTSAddress1 { get; set; }
        public string OTSContact { get; set; }
        public string OTSPhoneNum { get; set; }
        public string OrderComment { get; set; }
        public string UD_CreateBy_c { get; set; }
        public string UD_CreateName_c { get; set; }
        public List<OrderDtls> OrderDtls { get; set; }
    }

    public class OrderDtls
    {
        public string Company { get; set; }
        public int OrderNum { get; set; }
        public int OrderLine { get; set; }
        public string PartNum { get; set; }
        public string LineDesc { get; set; }
        public double DocDspUnitPrice { get; set; }
        public double UnitPrice { get; set; }
        public double DiscountPercent { get; set; }
        public double DocDspDiscount { get; set; }

        
        public double SellingQuantity { get; set; }
        public string OrderComment { get; set; }
        public string SalesCatID { get; set; }
        public string UD_KitNum_c { get; set; }
        public string UD_KitDesc_c { get; set; }

        
    }
    public string PostSalesOrderToEpicor(string OrgOrderNumber)
    {
        string EpicorOrderNumber = "";
        string Company = "";
        int CustNum = 0;
        string CustName = "";
        ps_orders bllOrder = new ps_orders();
        bllOrder.GetModel(OrgOrderNumber);

        ps_order_goods bllOrderGoods = new ps_order_goods();
        DataSet dsOrderGoods =  bllOrderGoods.GetList(" order_id = "+ bllOrder.id + "");

        ps_depot_category bllCompany = new ps_depot_category();
        Company = bllCompany.GetCompany(bllOrder.depot_category_id == null ? 0 : Convert.ToInt32(bllOrder.depot_category_id));

        ps_depot bllCustomer = new ps_depot();
        DataSet dsCustomer = bllCustomer.GetList(" id = "+ bllOrder.depot_id + "");
        if (dsCustomer.Tables[0].Rows.Count > 0)
        {
            CustNum = Convert.ToInt32(dsCustomer.Tables[0].Rows[0]["CustNum"]);
            CustName = dsCustomer.Tables[0].Rows[0]["CustName"].ToString();
        }


        DataSet ds = GetList("");
        if (ds.Tables[0].Rows.Count > 0)
        {
            UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
            APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
            ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
            AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
            AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            try
            {
                List<OrderDtls> salesOrderDetails = new List<OrderDtls>();
                for(int i = 0;i<dsOrderGoods.Tables[0].Rows.Count;i++)
                {
                    if (dsOrderGoods.Tables[0].Rows[i]["is_kit"].ToString() == "1")
                    {
                        ps_kitpart bllKit = new ps_kitpart();
                        DataSet dsKit = bllKit.GetList(" product_no='" + dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString() + "' ");
                        for (int j = 0; j < dsKit.Tables[0].Rows.Count; j++)
                        {
                            OrderDtls salesOrderDetail = new OrderDtls();
                            salesOrderDetail.Company = Company;
                            salesOrderDetail.OrderNum = 0;
                            salesOrderDetail.OrderLine = 0;
                            salesOrderDetail.LineDesc = dsKit.Tables[0].Rows[j]["partdesc"].ToString(); ;
                            salesOrderDetail.PartNum = dsKit.Tables[0].Rows[j]["partnumber"].ToString();
                            salesOrderDetail.DocDspUnitPrice = Convert.ToDouble(dsKit.Tables[0].Rows[i]["unitprice"].ToString());
                            salesOrderDetail.SellingQuantity = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString()) * Convert.ToDouble(dsKit.Tables[0].Rows[j]["qty"].ToString());
                            salesOrderDetail.OrderComment = dsOrderGoods.Tables[0].Rows[i]["remarks"].ToString();
                            salesOrderDetail.SalesCatID = "20";
                            salesOrderDetail.UD_KitNum_c = dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString();
                            salesOrderDetail.UD_KitDesc_c = dsOrderGoods.Tables[0].Rows[i]["product_name"].ToString();
                            salesOrderDetails.Add(salesOrderDetail);
                        }
                    }
                    else
                    {
                        OrderDtls salesOrderDetail = new OrderDtls();
                        salesOrderDetail.Company = Company;
                        salesOrderDetail.OrderNum = 0;
                        salesOrderDetail.OrderLine = 0;
                        //salesOrderDetail.LineDesc = dsOrderGoods.Tables[0].Rows[i]["product_desc"].ToString(); ;
                        salesOrderDetail.LineDesc = dsOrderGoods.Tables[0].Rows[i]["custsize"].ToString() != "" ? dsOrderGoods.Tables[0].Rows[i]["goods_title"].ToString() : dsOrderGoods.Tables[0].Rows[i]["product_desc"].ToString();
                        salesOrderDetail.PartNum = dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString();
                        salesOrderDetail.DiscountPercent = 100 - Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["discount"].ToString());
                        salesOrderDetail.DocDspDiscount = 100-Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["discount"].ToString());
                        salesOrderDetail.DocDspUnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                        salesOrderDetail.UnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                        salesOrderDetail.SalesCatID = "20";
                        salesOrderDetail.SellingQuantity = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString());
                        salesOrderDetail.OrderComment = dsOrderGoods.Tables[0].Rows[i]["remarks"].ToString();
                        salesOrderDetail.UD_KitNum_c = dsOrderGoods.Tables[0].Rows[i]["kit_num"].ToString();
                        salesOrderDetail.UD_KitDesc_c = dsOrderGoods.Tables[0].Rows[i]["kit_desc"].ToString();
                        salesOrderDetails.Add(salesOrderDetail);
                    }
                }

                SalesOrder entry = new SalesOrder
                {
                    Company = Company,
                    OrderNum = 0,
                    CustNum = CustNum,
                    PONum = OrgOrderNumber,
                    OrderDate = Convert.ToDateTime(bllOrder.order_date),
                    RequestDate = Convert.ToDateTime(bllOrder.ship_date),
                    NeedByDate = Convert.ToDateTime(bllOrder.need_date),
                    OrderComment = bllOrder.message,
                    UseOTS = true,
                    OTSName = CustName,
                    OTSAddress1 = bllOrder.address,
                    OTSContact = bllOrder.contract_name,
                    OTSPhoneNum = bllOrder.contact_number,
                    UD_CreateBy_c = System.Web.HttpContext.Current.Session["AID"].ToString(),
                    UD_CreateName_c = System.Web.HttpContext.Current.Session["AdminName"].ToString(),
        
                OrderDtls = salesOrderDetails,
                };

                string isoJson = JsonConvert.SerializeObject(entry);    //Convert DataEntry to Json string

                isoJson = isoJson.Replace("\r\n", "");
                isoJson = isoJson.Replace("\"0001-01-01T00:00:00Z\"", "null");
                isoJson = isoJson.Replace("00:00:00", "00:00:00Z");

                //final RequestURL
                string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.SalesOrderSVC/SalesOrders";
                RequestURL = RequestURL.Replace("{ServerName}", ServerName);
                RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
                RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);

                

                string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
                HTTPMethods = "POST";
                HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);

                string strHTTPStatusCode = ResponseStatusCode;
                string strResponseBody = ResponseBody;
                string strExceptionMsg = ExceptionMsg;
                string strTDeserializeResponseBodyErrorMessage = ErrorMessage;

                if (ResponseStatusCode == "201")
                {
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //PartData partData = jss.Deserialize<PartData>(ResponseBody);
                    dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                    if (dyn == null) return "Error";
                    List<PartData> lsPartDatas = new List<PartData> { };
                    foreach (var obj in dyn)
                    {
                        if (obj.Name == "OrderNum")
                        {
                            EpicorOrderNumber = obj.Value;
                            break;
                        }
                    }
                }
                ////写入登录日志
                ps_manager_log mylog = new ps_manager_log();
                mylog.user_id = 1;
                mylog.user_name = "API";
                mylog.action_type = "Epcior";
                mylog.add_time = DateTime.Now;
                mylog.remark = "POST订单到Epicor(订单号"+ EpicorOrderNumber + ")";
                mylog.user_ip = AXRequest.GetIP();
                mylog.Add();
                return EpicorOrderNumber;

            }
            catch (AggregateException ex)
            {
                string strExceptionMsg = ex.ToString();
                return "Error:"+ strExceptionMsg;
            }
        }
        else
            return "Error";
    }

    public class QuoteOrder
    {
        public string Company { get; set; }
        public int QuoteNum { get; set; }
        public int CustNum { get; set; }
        public string CustomerCustID { get; set; }
        public int ShipToCustNum { get; set; }
        public int BTCustNum { get; set; }
        public string PONum { get; set; }
        public DateTime DueDate { get; set; }
        public string TerritoryID { get; set; }
        public string SalesRepCode { get; set; }
        public string ShipViaCode { get; set; }
        public string TermsCode { get; set; }
        public string UD_CreateBy_c { get; set; }
        public string UD_CreateName_c { get; set; }

        //public DateTime DateQuoted { get; set; }
        //public DateTime ExpirationDate { get; set; }
        public bool UseOTS { get; set; }
        public string OTSName { get; set; }
        public string OTSAddress1 { get; set; }
        public string OTSContact { get; set; }
        public string OTSPhoneNum { get; set; }
        public string QuoteComment { get; set; }
        public bool QuoteClosed { get; set; }

        public List<QuoteDtls> QuoteDtls { get; set; }
    }
    //{"Company":"C05","QuoteNum":0,"CustNum":12,"ShipToCustNum":12,"BTCustNum":12,"PONum":"23032112475897","DueDate":"2023-03-21T00:00:00Z","SalesRepCode": "IC","QuoteComment":"","QuoteDtls":[{"Company":"C05","QuoteNum":0,"QuoteLine":0,"PartNum":"P10201000004","LineDesc":"PY31910A-P缇派浴室柜盆柜米灰色橡木+白亮光（电商）990*555*480","OrderQty":1.0,"QuoteComment":"","SalesCatID":"20"}]}
    public class QuoteDtls
    {
        public string Company { get; set; }
        public int QuoteNum { get; set; }
        public int QuoteLine { get; set; }
        public string PartNum { get; set; }
        public string LineDesc { get; set; }
        //public double DocDspUnitPrice { get; set; }
        //public double SellingQuantity { get; set; }
        public double OrderQty { get; set; }
        public double SellingExpectedQty { get; set; }
        
        //public double DocDspUnitPrice { get; set; }
        //public double UnitPrice { get; set; }
        public double DocDspExpUnitPrice { get; set; }
        public double DocExpUnitPrice { get; set; }
        
        public double DiscountPercent { get; set; }
        public double DocDspDiscount { get; set; }
        

        //public double SellingQuantity { get; set; }
        public string QuoteComment { get; set; }
        public string SalesCatID { get; set; }
        //public List<QuoteQties> QuoteQties { get; set; }

    }

    public class QuoteQties
    {
        public string Company { get; set; }
        public int QuoteNum { get; set; }
        public int QuoteLine { get; set; }
        //public double QtyNum { get; set; }
        public double SellingQuantity { get; set; }
        public double UnitPrice { get; set; }
        public double DocUnitPrice { get; set; }
    }


    public string CreateNewPartForOrder(string OrgOrderNumber)
    {
        string Company = "";
        ps_orders bllOrder = new ps_orders();
        bllOrder.GetModel(OrgOrderNumber);

        ps_order_goods bllOrderGoods = new ps_order_goods();
        DataSet dsOrderGoods = bllOrderGoods.GetList(" order_id = " + bllOrder.id + "");

        ps_depot_category bllCompany = new ps_depot_category();
        Company = bllCompany.GetCompany(bllOrder.depot_category_id == null ? 0 : Convert.ToInt32(bllOrder.depot_category_id));



        DataSet ds = GetList("");
        if (ds.Tables[0].Rows.Count > 0)
        {
            UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
            APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
            ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
            AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
            AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            try
            {
                List<QuoteDtls> quoteOrderDetails = new List<QuoteDtls>();
                for (int i = 0; i < dsOrderGoods.Tables[0].Rows.Count; i++)
                {
                    if (dsOrderGoods.Tables[0].Rows[i]["is_kit"].ToString() != "1")
                    {
                      
                        string NewPartNumber = "";
                        //if (dsOrderGoods.Tables[0].Rows[i]["iscust"].ToString() == "True")
                        if (dsOrderGoods.Tables[0].Rows[i]["custsize"].ToString().Trim() != "")
                        {
                            NewPartNumber = CopyPart("C01", dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString(), dsOrderGoods.Tables[0].Rows[i]["goods_title"].ToString(), dsOrderGoods.Tables[0].Rows[i]["custsize"].ToString());
                            if (NewPartNumber != "")
                            {
                                bllOrderGoods.GetModel(Convert.ToInt32(dsOrderGoods.Tables[0].Rows[i]["id"]));
                                bllOrderGoods.new_product_no = NewPartNumber;
                                bllOrderGoods.Update();
                            }
                            ////写入登录日志
                            ps_manager_log mylog = new ps_manager_log();
                            mylog.user_id = 1;
                            mylog.user_name = "API";
                            mylog.action_type = "Epcior";
                            mylog.add_time = DateTime.Now;
                            mylog.remark = "Create New Part(Part Number" + dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString() + ".New Part" + NewPartNumber;
                            mylog.user_ip = AXRequest.GetIP();
                            mylog.Add();
                        }
                       
                    }
                }

              
                return "Success";

            }
            catch (AggregateException ex)
            {
                string strExceptionMsg = ex.ToString();
                return "Error:" + strExceptionMsg;
            }
        }
        else
            return "Error";
    }
    public string PostQuoteToEpicor(string OrgOrderNumber)
    {
        string EpicorQuoteOrderNumber = "";
        string Company = "";
        int CustNum = 0;
        string CustName = "";
        string CustID = "";
        string SalesRepCode = "";
        string TerritoryID = "";
        string ShipViaCode = "";
        string TermsCode = "";
        ps_orders bllOrder = new ps_orders();
        bllOrder.GetModel(OrgOrderNumber);

        ps_order_goods bllOrderGoods = new ps_order_goods();
        DataSet dsOrderGoods = bllOrderGoods.GetList(" order_id = " + bllOrder.id + "");

        ps_depot_category bllCompany = new ps_depot_category();
        Company = bllCompany.GetCompany(bllOrder.depot_category_id == null ? 0 : Convert.ToInt32(bllOrder.depot_category_id));

        ps_depot bllCustomer = new ps_depot();
        DataSet dsCustomer = bllCustomer.GetList(" id = " + bllOrder.depot_id + "");
        if (dsCustomer.Tables[0].Rows.Count > 0)
        {
            CustNum = Convert.ToInt32(dsCustomer.Tables[0].Rows[0]["CustNum"]);
            CustName = dsCustomer.Tables[0].Rows[0]["CustName"].ToString();
            CustID = dsCustomer.Tables[0].Rows[0]["CustID"].ToString();
            SalesRepCode = dsCustomer.Tables[0].Rows[0]["SalesRepCode"].ToString();
            TerritoryID = dsCustomer.Tables[0].Rows[0]["TerritoryID"].ToString();
            ShipViaCode = dsCustomer.Tables[0].Rows[0]["ShipViaCode"].ToString();
            TermsCode = dsCustomer.Tables[0].Rows[0]["TermsCode"].ToString();
 
        }


        DataSet ds = GetList("");
        if (ds.Tables[0].Rows.Count > 0)
        {
            UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
            APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
            ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
            AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
            AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            try
            {
                List<QuoteDtls> quoteOrderDetails = new List<QuoteDtls>();
                for (int i = 0; i < dsOrderGoods.Tables[0].Rows.Count; i++)
                {
                    if (dsOrderGoods.Tables[0].Rows[i]["is_kit"].ToString() == "1")
                    {
                        ps_kitpart bllKit = new ps_kitpart();
                        DataSet dsKit = bllKit.GetList(" product_no='" + dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString() + "' ");
                        for (int j = 0; j < dsKit.Tables[0].Rows.Count; j++)
                        {
                            QuoteDtls quoteOrderDetail = new QuoteDtls();
                            quoteOrderDetail.Company = Company;
                            quoteOrderDetail.QuoteNum = 0;
                            quoteOrderDetail.QuoteLine = 0;
                            quoteOrderDetail.LineDesc = dsKit.Tables[0].Rows[j]["partdesc"].ToString(); ;
                            quoteOrderDetail.PartNum = dsKit.Tables[0].Rows[j]["partnumber"].ToString();
                            //quoteOrderDetail.DocDspUnitPrice = Convert.ToDouble(dsKit.Tables[0].Rows[i]["unitprice"].ToString());
                            quoteOrderDetail.OrderQty = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString()) * Convert.ToDouble(dsKit.Tables[0].Rows[j]["qty"].ToString());
                            quoteOrderDetail.SellingExpectedQty = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString()) * Convert.ToDouble(dsKit.Tables[0].Rows[j]["qty"].ToString());
                            
                            quoteOrderDetail.QuoteComment = dsOrderGoods.Tables[0].Rows[i]["remarks"].ToString();
                            quoteOrderDetail.SalesCatID = "20";
                            quoteOrderDetails.Add(quoteOrderDetail);
                        }
                    }
                    else
                    {
                        //List<QuoteQties> quoteOrderQtys = new List<QuoteQties>();
                        //QuoteQties quoteOrderQty = new QuoteQties();
                        //quoteOrderQty.Company = Company;
                        //quoteOrderQty.QuoteNum = 0;
                        //quoteOrderQty.QuoteLine = 0;
                        ////quoteOrderQty.QtyNum = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString()); 
                        //quoteOrderQty.SellingQuantity = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString());
                        //quoteOrderQty.UnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                        //quoteOrderQty.DocUnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                        //quoteOrderQtys.Add(quoteOrderQty);

                        QuoteDtls quoteOrderDetail = new QuoteDtls();
                        quoteOrderDetail.Company = Company;
                        quoteOrderDetail.QuoteNum = 0;
                        quoteOrderDetail.QuoteLine = 0;
                        quoteOrderDetail.LineDesc = dsOrderGoods.Tables[0].Rows[i]["custsize"].ToString()!="" ? dsOrderGoods.Tables[0].Rows[i]["goods_title"].ToString() : dsOrderGoods.Tables[0].Rows[i]["product_desc"].ToString();
                        //string NewPartNumber = CopyPart("C01", dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString());
                        //quoteOrderDetail.PartNum = dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString();
                        string NewPartNumber = "";
                        //if (dsOrderGoods.Tables[0].Rows[i]["iscust"].ToString() == "True")
                        //2023.07.06 Don't create new part
                        //if (dsOrderGoods.Tables[0].Rows[i]["custsize"].ToString().Trim() != "")
                        //{
                        //    NewPartNumber = CopyPart("C01", dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString(), dsOrderGoods.Tables[0].Rows[i]["custsize"].ToString());
                        //    if (NewPartNumber != "")
                        //    {
                        //        bllOrderGoods.GetModel(Convert.ToInt32(dsOrderGoods.Tables[0].Rows[i]["id"]));
                        //        bllOrderGoods.new_product_no = NewPartNumber;
                        //        bllOrderGoods.Update();
                        //    }
                        //}
                        NewPartNumber = dsOrderGoods.Tables[0].Rows[i]["new_product_no"].ToString();
                        quoteOrderDetail.PartNum = NewPartNumber != "" ? NewPartNumber : dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString();
                        //quoteOrderDetail.DocDspUnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["goods_price"].ToString());
                        quoteOrderDetail.SalesCatID = "20";
                        quoteOrderDetail.OrderQty = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString());
                        quoteOrderDetail.SellingExpectedQty = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString());
                        quoteOrderDetail.DiscountPercent = 100-Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["discount"].ToString());
                        quoteOrderDetail.DocDspDiscount = (100 - Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["discount"].ToString()))/100 *
                                                        Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString()) * Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                        quoteOrderDetail.DocDspExpUnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                        quoteOrderDetail.DocExpUnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                        quoteOrderDetail.QuoteComment = dsOrderGoods.Tables[0].Rows[i]["remarks"].ToString();
                        //quoteOrderDetail.QuoteQties = quoteOrderQtys;
                        quoteOrderDetails.Add(quoteOrderDetail);
                    }
                }

                QuoteOrder entry = new QuoteOrder
                {
                    Company = Company,
                    QuoteNum = 0,
                    CustNum = CustNum,
                    CustomerCustID = CustID,
                    ShipToCustNum = CustNum,
                    //BTCustNum = CustNum,
                    PONum = OrgOrderNumber,
                    //DateQuoted = Convert.ToDateTime(bllOrder.order_date),
                    DueDate = Convert.ToDateTime(bllOrder.order_date),
                    //ExpirationDate = Convert.ToDateTime(bllOrder.need_date),
                    QuoteComment = bllOrder.message,
                    //SalesRepCode="IC",
                    //TerritoryID = "IC",
                    SalesRepCode = SalesRepCode=="" ? "IC" : SalesRepCode,
                    TerritoryID = TerritoryID == "" ? "IC" : TerritoryID,
                    ShipViaCode = ShipViaCode,
                    TermsCode = TermsCode ,
                    //UseOTS = true,
                    //OTSName = CustName,
                    //OTSContact = bllOrder.contract_name,
                    //OTSAddress1 = bllOrder.address,
                    //OTSPhoneNum = bllOrder.contact_number,
                    UD_CreateBy_c = System.Web.HttpContext.Current.Session["AID"].ToString(),
                    UD_CreateName_c = System.Web.HttpContext.Current.Session["AdminName"].ToString(),
                    //UseOTS = true,
                    //OTSName = CustName,
                    //OTSAddress1 = bllOrder.address,
                    //OTSContact = bllOrder.contract_name,
                    //OTSPhoneNum = bllOrder.contact_number,


                    QuoteDtls = quoteOrderDetails,
                };

                string isoJson = JsonConvert.SerializeObject(entry);    //Convert DataEntry to Json string

                isoJson = isoJson.Replace("\r\n", "");
                isoJson = isoJson.Replace("\"0001-01-01T00:00:00Z\"", "null");
                isoJson = isoJson.Replace("00:00:00", "00:00:00Z");

                isoJson = isoJson.Replace(",\"QuoteQties\":null", "");

                //final RequestURL
                string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.QuoteSvc/Quotes";
                RequestURL = RequestURL.Replace("{ServerName}", ServerName);
                RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
                RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);



                string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
                HTTPMethods = "POST";
                HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);

                string strHTTPStatusCode = ResponseStatusCode;
                string strResponseBody = ResponseBody;
                string strExceptionMsg = ExceptionMsg;
                string strTDeserializeResponseBodyErrorMessage = ErrorMessage;

                if (ResponseStatusCode == "201")
                {
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //PartData partData = jss.Deserialize<PartData>(ResponseBody);
                    dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                    if( dyn= null ) return "Error";
                    List<PartData> lsPartDatas = new List<PartData> { };
                    foreach (var obj in dyn)
                    {
                        if (obj.Name == "QuoteNum")
                        {
                            EpicorQuoteOrderNumber = obj.Value;
                            if (UpdateQuoteToEpicor(OrgOrderNumber, EpicorQuoteOrderNumber))
                                PostQuoteAttachmentToEpicor(OrgOrderNumber,EpicorQuoteOrderNumber);
                            break;
                        }
                    }
                }
                else
                {
                    EpicorQuoteOrderNumber = "Error:" + strTDeserializeResponseBodyErrorMessage;
                }
                ////写入登录日志
                ps_manager_log mylog = new ps_manager_log();
                mylog.user_id = 1;
                mylog.user_name = "API";
                mylog.action_type = "Epcior";
                mylog.add_time = DateTime.Now;
                mylog.remark = "POST订单到Epicor(报价单号" + EpicorQuoteOrderNumber + ")";
                mylog.user_ip = AXRequest.GetIP();
                mylog.Add();
                return EpicorQuoteOrderNumber;

            }
            catch (AggregateException ex)
            {
                string strExceptionMsg = ex.ToString();
                return "Error:" + strExceptionMsg;
            }
        }
        else
            return "Error";
    }




    public bool UpdateQuoteToEpicor(string OrgOrderNumber, string EpicorOrderNumber)
    {
        string Company = "";
        int CustNum = 0;
        string CustName = "";
        string CustID = "";
        string SalesRepCode = "";
        string TerritoryID = "";
        string ShipViaCode = "";
        string TermsCode = "";
        bool retrunValue = false;
        ps_orders bllOrder = new ps_orders();
        bllOrder.GetModel(OrgOrderNumber);

        ps_order_goods bllOrderGoods = new ps_order_goods();
        DataSet dsOrderGoods = bllOrderGoods.GetList(" order_id = " + bllOrder.id + "");

        ps_depot_category bllCompany = new ps_depot_category();
        Company = bllCompany.GetCompany(bllOrder.depot_category_id == null ? 0 : Convert.ToInt32(bllOrder.depot_category_id));

        ps_depot bllCustomer = new ps_depot();
        DataSet dsCustomer = bllCustomer.GetList(" id = " + bllOrder.depot_id + "");
        if (dsCustomer.Tables[0].Rows.Count > 0)
        {
            CustNum = Convert.ToInt32(dsCustomer.Tables[0].Rows[0]["CustNum"]);
            CustName = dsCustomer.Tables[0].Rows[0]["CustName"].ToString();
            CustID = dsCustomer.Tables[0].Rows[0]["CustID"].ToString();
            SalesRepCode = dsCustomer.Tables[0].Rows[0]["SalesRepCode"].ToString();
            TerritoryID = dsCustomer.Tables[0].Rows[0]["TerritoryID"].ToString();
            ShipViaCode = dsCustomer.Tables[0].Rows[0]["ShipViaCode"].ToString();
            TermsCode = dsCustomer.Tables[0].Rows[0]["TermsCode"].ToString();
        }


        DataSet ds = GetList("");
        if (ds.Tables[0].Rows.Count > 0)
        {
            UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
            APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
            ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
            AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
            AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            try
            {
                List<QuoteDtls> quoteOrderDetails = new List<QuoteDtls>();
                for (int i = 0; i < dsOrderGoods.Tables[0].Rows.Count; i++)
                {
                    if (dsOrderGoods.Tables[0].Rows[i]["is_kit"].ToString() == "1")
                    {
                        ps_kitpart bllKit = new ps_kitpart();
                        DataSet dsKit = bllKit.GetList(" product_no='" + dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString() + "' ");
                        for (int j = 0; j < dsKit.Tables[0].Rows.Count; j++)
                        {
                            QuoteDtls quoteOrderDetail = new QuoteDtls();
                            quoteOrderDetail.Company = Company;
                            quoteOrderDetail.QuoteNum = Convert.ToInt32(EpicorOrderNumber);
                            quoteOrderDetail.QuoteLine = 0;
                            quoteOrderDetail.LineDesc = dsKit.Tables[0].Rows[j]["partdesc"].ToString(); ;
                            quoteOrderDetail.PartNum = dsKit.Tables[0].Rows[j]["partnumber"].ToString();
                            //quoteOrderDetail.DocDspUnitPrice = Convert.ToDouble(dsKit.Tables[0].Rows[i]["unitprice"].ToString());
                            quoteOrderDetail.OrderQty = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString()) * Convert.ToDouble(dsKit.Tables[0].Rows[j]["qty"].ToString());
                            quoteOrderDetail.SellingExpectedQty = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString()) * Convert.ToDouble(dsKit.Tables[0].Rows[j]["qty"].ToString());
                            quoteOrderDetail.QuoteComment = dsOrderGoods.Tables[0].Rows[i]["remarks"].ToString();
                            quoteOrderDetail.SalesCatID = "20";
                            quoteOrderDetails.Add(quoteOrderDetail);
                        }
                    }
                    else
                    {
                        //List<QuoteQties> quoteOrderQtys = new List<QuoteQties>();
                        //QuoteQties quoteOrderQty = new QuoteQties();
                        //quoteOrderQty.Company = Company;
                        //quoteOrderQty.QuoteNum = Convert.ToInt32(EpicorOrderNumber);
                        //quoteOrderQty.QuoteLine = i+1;
                        ////quoteOrderQty.QtyNum = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString());
                        //quoteOrderQty.SellingQuantity = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString());
                        //quoteOrderQty.UnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                        //quoteOrderQty.DocUnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                        //quoteOrderQtys.Add(quoteOrderQty);

                        QuoteDtls quoteOrderDetail = new QuoteDtls();
                        quoteOrderDetail.Company = Company;
                        quoteOrderDetail.QuoteNum = Convert.ToInt32(EpicorOrderNumber); 
                        quoteOrderDetail.QuoteLine = i + 1; 
                        //quoteOrderDetail.LineDesc = dsOrderGoods.Tables[0].Rows[i]["product_desc"].ToString(); ;
                        quoteOrderDetail.LineDesc = dsOrderGoods.Tables[0].Rows[i]["custsize"].ToString()!="" ? dsOrderGoods.Tables[0].Rows[i]["goods_title"].ToString() : dsOrderGoods.Tables[0].Rows[i]["product_desc"].ToString();
                        //quoteOrderDetail.PartNum = dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString();
                        //string NewPartNumber = "";
                        //if (dsOrderGoods.Tables[0].Rows[i]["iscust"].ToString() == "True")
                        //if (dsOrderGoods.Tables[0].Rows[i]["custsize"].ToString().Trim() != "")
                        //{
                        //    NewPartNumber = CopyPart("C01", dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString());
                        //}
                        quoteOrderDetail.PartNum = dsOrderGoods.Tables[0].Rows[i]["new_product_no"].ToString() != "" ? dsOrderGoods.Tables[0].Rows[i]["new_product_no"].ToString() : dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString();
                        //quoteOrderDetail.DocDspUnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["goods_price"].ToString());
                        quoteOrderDetail.SalesCatID = "20";
                        quoteOrderDetail.OrderQty = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString());
                        quoteOrderDetail.SellingExpectedQty = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString());
                        
                        quoteOrderDetail.DiscountPercent = 100-Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["discount"].ToString());
                        quoteOrderDetail.DocDspDiscount = (100-Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["discount"].ToString())) / 100 *
                                                        Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString()) * Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString()); 
                        quoteOrderDetail.DocDspExpUnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                        quoteOrderDetail.DocExpUnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                        quoteOrderDetail.QuoteComment = dsOrderGoods.Tables[0].Rows[i]["remarks"].ToString();
                        //quoteOrderDetail.QuoteQties = quoteOrderQtys;
                        quoteOrderDetails.Add(quoteOrderDetail);
                    }
                }

                QuoteOrder entry = new QuoteOrder
                {
                    Company = Company,
                    QuoteNum = Convert.ToInt32(EpicorOrderNumber),
                    CustNum = CustNum,
                    CustomerCustID = CustID,
                    ShipToCustNum = CustNum,
                    BTCustNum = CustNum,
                    PONum = OrgOrderNumber,
                    //DateQuoted = Convert.ToDateTime(bllOrder.order_date),
                    DueDate = Convert.ToDateTime(bllOrder.order_date),
                    //ExpirationDate = Convert.ToDateTime(bllOrder.need_date),
                    QuoteComment = bllOrder.message,
                    //SalesRepCode = SalesRepCode == "" ? "IC" : SalesRepCode,
                    //TerritoryID = TerritoryID == "" ? "IC" : TerritoryID,
                    SalesRepCode = SalesRepCode == "" ? "IC" : SalesRepCode,
                    TerritoryID = TerritoryID == "" ? "IC" : TerritoryID,
                    ShipViaCode = ShipViaCode ,
                    TermsCode = TermsCode,
                    UseOTS = true,
                    OTSName = CustName,
                    OTSAddress1 = bllOrder.address,
                    OTSContact = bllOrder.contract_name,
                    OTSPhoneNum = bllOrder.contact_number,


                    QuoteDtls = quoteOrderDetails,
                };

                string isoJson = JsonConvert.SerializeObject(entry);    //Convert DataEntry to Json string

                isoJson = isoJson.Replace("\r\n", "");
                isoJson = isoJson.Replace("\"0001-01-01T00:00:00Z\"", "null");
                isoJson = isoJson.Replace("00:00:00", "00:00:00Z");
                isoJson = isoJson.Replace(",\"QuoteQties\":null", "");

                //final RequestURL
                string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.QuoteSvc/Quotes";
                RequestURL = RequestURL.Replace("{ServerName}", ServerName);
                RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
                RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);



                string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
                HTTPMethods = "POST";
                HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);

                string strHTTPStatusCode = ResponseStatusCode;
                string strResponseBody = ResponseBody;
                string strExceptionMsg = ExceptionMsg;
                string strTDeserializeResponseBodyErrorMessage = ErrorMessage;

                if (ResponseStatusCode == "201")
                {
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //PartData partData = jss.Deserialize<PartData>(ResponseBody);
                    dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                    if (dyn == null) return false;
                    List<PartData> lsPartDatas = new List<PartData> { };
                    foreach (var obj in dyn)
                    {
                        if (obj.Name == "QuoteNum")
                        {
                            retrunValue = true;
                        }
                    }
                }
                else
                {
                    retrunValue = false;
                }

            }
            catch (AggregateException ex)
            {
                string strExceptionMsg = ex.ToString();
                retrunValue = false;
            }
        }
        else
            retrunValue =false;

        return retrunValue;
    }


    public bool DeleteQuote(string Company, string QuoteNum)
    {
        try
        {
            //final RequestURL
            string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.QuoteSvc/Quotes('{Company}',{QuoteNum})"; ;
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            RequestURL = RequestURL.Replace("{Company}", Company);
            RequestURL = RequestURL.Replace("{QuoteNum}", QuoteNum);




            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "POST";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            if (ResponseStatusCode == "200" && ResponseBody != "")
            {
                return true;
            }
            else
                return false;


        }
        catch (AggregateException ex)
        {
            return false;
        }
    }

    public bool CancelQuoteToEpicor(string OrgOrderNumber, string EpicorOrderNumber)
    {
        string Company = "";
        int CustNum = 0;
        string CustName = "";
        string CustID = "";
        string SalesRepCode = "";
        string TerritoryID = "";
        string ShipViaCode = "";
        string TermsCode = "";
        bool retrunValue = false;
        ps_orders bllOrder = new ps_orders();
        bllOrder.GetModel(OrgOrderNumber);

        ps_order_goods bllOrderGoods = new ps_order_goods();
        DataSet dsOrderGoods = bllOrderGoods.GetList(" order_id = " + bllOrder.id + "");

        ps_depot_category bllCompany = new ps_depot_category();
        Company = bllCompany.GetCompany(bllOrder.depot_category_id == null ? 0 : Convert.ToInt32(bllOrder.depot_category_id));

        ps_depot bllCustomer = new ps_depot();
        DataSet dsCustomer = bllCustomer.GetList(" id = " + bllOrder.depot_id + "");
        if (dsCustomer.Tables[0].Rows.Count > 0)
        {
            CustNum = Convert.ToInt32(dsCustomer.Tables[0].Rows[0]["CustNum"]);
            CustName = dsCustomer.Tables[0].Rows[0]["CustName"].ToString();
            CustID = dsCustomer.Tables[0].Rows[0]["CustID"].ToString();
            SalesRepCode = dsCustomer.Tables[0].Rows[0]["SalesRepCode"].ToString();
            TerritoryID = dsCustomer.Tables[0].Rows[0]["TerritoryID"].ToString();
            ShipViaCode = dsCustomer.Tables[0].Rows[0]["ShipViaCode"].ToString();
            TermsCode = dsCustomer.Tables[0].Rows[0]["TermsCode"].ToString();
        }


        DataSet ds = GetList("");
        if (ds.Tables[0].Rows.Count > 0)
        {
            UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
            APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
            ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
            AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
            AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            try
            {
                List<QuoteDtls> quoteOrderDetails = new List<QuoteDtls>();
                //for (int i = 0; i < dsOrderGoods.Tables[0].Rows.Count; i++)
                //{
                //    if (dsOrderGoods.Tables[0].Rows[i]["is_kit"].ToString() == "1")
                //    {
                //        ps_kitpart bllKit = new ps_kitpart();
                //        DataSet dsKit = bllKit.GetList(" product_no='" + dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString() + "' ");
                //        for (int j = 0; j < dsKit.Tables[0].Rows.Count; j++)
                //        {
                //            QuoteDtls quoteOrderDetail = new QuoteDtls();
                //            quoteOrderDetail.Company = Company;
                //            quoteOrderDetail.QuoteNum = Convert.ToInt32(EpicorOrderNumber);
                //            quoteOrderDetail.QuoteLine = 0;
                //            quoteOrderDetail.LineDesc = dsKit.Tables[0].Rows[j]["partdesc"].ToString(); ;
                //            quoteOrderDetail.PartNum = dsKit.Tables[0].Rows[j]["partnumber"].ToString();
                //            //quoteOrderDetail.DocDspUnitPrice = Convert.ToDouble(dsKit.Tables[0].Rows[i]["unitprice"].ToString());
                //            quoteOrderDetail.OrderQty = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString()) * Convert.ToDouble(dsKit.Tables[0].Rows[j]["qty"].ToString());
                //            quoteOrderDetail.SellingExpectedQty = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString()) * Convert.ToDouble(dsKit.Tables[0].Rows[j]["qty"].ToString());
                //            quoteOrderDetail.QuoteComment = dsOrderGoods.Tables[0].Rows[i]["remarks"].ToString();
                //            quoteOrderDetail.SalesCatID = "20";
                //            quoteOrderDetails.Add(quoteOrderDetail);
                //        }
                //    }
                //    else
                //    {
                //        //List<QuoteQties> quoteOrderQtys = new List<QuoteQties>();
                //        //QuoteQties quoteOrderQty = new QuoteQties();
                //        //quoteOrderQty.Company = Company;
                //        //quoteOrderQty.QuoteNum = Convert.ToInt32(EpicorOrderNumber);
                //        //quoteOrderQty.QuoteLine = i+1;
                //        ////quoteOrderQty.QtyNum = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString());
                //        //quoteOrderQty.SellingQuantity = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString());
                //        //quoteOrderQty.UnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                //        //quoteOrderQty.DocUnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                //        //quoteOrderQtys.Add(quoteOrderQty);

                //        QuoteDtls quoteOrderDetail = new QuoteDtls();
                //        quoteOrderDetail.Company = Company;
                //        quoteOrderDetail.QuoteNum = Convert.ToInt32(EpicorOrderNumber);
                //        quoteOrderDetail.QuoteLine = i + 1;
                //        quoteOrderDetail.LineDesc = dsOrderGoods.Tables[0].Rows[i]["product_desc"].ToString(); ;
                //        //quoteOrderDetail.PartNum = dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString();
                //        //string NewPartNumber = "";
                //        //if (dsOrderGoods.Tables[0].Rows[i]["iscust"].ToString() == "True")
                //        //if (dsOrderGoods.Tables[0].Rows[i]["custsize"].ToString().Trim() != "")
                //        //{
                //        //    NewPartNumber = CopyPart("C01", dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString());
                //        //}
                //        quoteOrderDetail.PartNum = dsOrderGoods.Tables[0].Rows[i]["new_product_no"].ToString() != "" ? dsOrderGoods.Tables[0].Rows[i]["new_product_no"].ToString() : dsOrderGoods.Tables[0].Rows[i]["product_no"].ToString();
                //        //quoteOrderDetail.DocDspUnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["goods_price"].ToString());
                //        quoteOrderDetail.SalesCatID = "20";
                //        quoteOrderDetail.OrderQty = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString());
                //        quoteOrderDetail.SellingExpectedQty = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString());

                //        quoteOrderDetail.DiscountPercent = 100 - Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["discount"].ToString());
                //        quoteOrderDetail.DocDspDiscount = (100 - Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["discount"].ToString())) / 100 *
                //                                        Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["quantity"].ToString()) * Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                //        quoteOrderDetail.DocDspExpUnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                //        quoteOrderDetail.DocExpUnitPrice = Convert.ToDouble(dsOrderGoods.Tables[0].Rows[i]["real_price"].ToString());
                //        quoteOrderDetail.QuoteComment = dsOrderGoods.Tables[0].Rows[i]["remarks"].ToString();
                //        //quoteOrderDetail.QuoteQties = quoteOrderQtys;
                //        quoteOrderDetails.Add(quoteOrderDetail);
                //    }
                //}

                QuoteOrder entry = new QuoteOrder
                {
                    Company = Company,
                    QuoteNum = Convert.ToInt32(EpicorOrderNumber),
                    CustNum = CustNum,
                    CustomerCustID = CustID,
                    ShipToCustNum = CustNum,
                    BTCustNum = CustNum,
                    //PONum = OrgOrderNumber,
                    ////DateQuoted = Convert.ToDateTime(bllOrder.order_date),
                    //DueDate = Convert.ToDateTime(bllOrder.order_date),
                    ////ExpirationDate = Convert.ToDateTime(bllOrder.need_date),
                    //QuoteComment = bllOrder.message,
                    ////SalesRepCode = SalesRepCode == "" ? "IC" : SalesRepCode,
                    ////TerritoryID = TerritoryID == "" ? "IC" : TerritoryID,
                    //SalesRepCode = SalesRepCode == "" ? "IC" : SalesRepCode,
                    //TerritoryID = TerritoryID == "" ? "IC" : TerritoryID,
                    //ShipViaCode = ShipViaCode,
                    //TermsCode = TermsCode,
                    //UseOTS = true,
                    //OTSName = CustName,
                    //OTSAddress1 = bllOrder.address,
                    //OTSContact = bllOrder.contract_name,
                    //OTSPhoneNum = bllOrder.contact_number,
                    QuoteClosed = true,
                    QuoteDtls = quoteOrderDetails,
                };

                string isoJson = JsonConvert.SerializeObject(entry);    //Convert DataEntry to Json string

                isoJson = isoJson.Replace("\r\n", "");
                isoJson = isoJson.Replace("\"0001-01-01T00:00:00Z\"", "null");
                isoJson = isoJson.Replace("00:00:00", "00:00:00Z");
                isoJson = isoJson.Replace(",\"QuoteQties\":null", "");

                //final RequestURL
                string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.QuoteSvc/Quotes";
                RequestURL = RequestURL.Replace("{ServerName}", ServerName);
                RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
                RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);



                string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
                HTTPMethods = "POST";
                HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);

                string strHTTPStatusCode = ResponseStatusCode;
                string strResponseBody = ResponseBody;
                string strExceptionMsg = ExceptionMsg;
                string strTDeserializeResponseBodyErrorMessage = ErrorMessage;

                if (ResponseStatusCode == "201")
                {
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //PartData partData = jss.Deserialize<PartData>(ResponseBody);
                    dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                    if (dyn == null) return false;
                    List<PartData> lsPartDatas = new List<PartData> { };
                    foreach (var obj in dyn)
                    {
                        if (obj.Name == "QuoteNum")
                        {
                            retrunValue = true;
                        }
                    }
                }
                else
                {
                    retrunValue = false;
                }

            }
            catch (AggregateException ex)
            {
                string strExceptionMsg = ex.ToString();
                retrunValue = false;
            }
        }
        else
            retrunValue = false;

        return retrunValue;
    }

    public static string FileToByteArray(string filePath)
    {
        byte[] byteArray = null;
        if (File.Exists(filePath))
            byteArray = File.ReadAllBytes(filePath);
        string filebase64 = Convert.ToBase64String(byteArray);
        return filebase64;
    }

    public class EpicorFile
    {
        public string docTypeID { get; set; }
        public string parentTable { get; set; }
        public string fileName { get; set; }
        public string data { get; set; }

    }

    public string UploadEpicorFile(string Company, string FileName)
    {
        try
        {
            string EpicorFileName = "";
            //final RequestURL
            if (FileName != "")
            {
                string savePath = System.Web.HttpContext.Current.Server.MapPath("~/upload/files/");
                string data = FileToByteArray(savePath + FileName);
                EpicorFile entry = new EpicorFile
                {
                    docTypeID = "",
                    parentTable = "QuoteDtl",
                    fileName = FileName,
                    data = data,
                };

                string isoJson = JsonConvert.SerializeObject(entry);    //Convert DataEntry to Json string

                isoJson = isoJson.Replace("\r\n", "");
                isoJson = isoJson.Replace("\"0001-01-01T00:00:00Z\"", "null");
                isoJson = isoJson.Replace("00:00:00", "00:00:00Z");

                DataSet ds = GetList("");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                    APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                    ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                    AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                    AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
                }


                //final RequestURL
                string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Ice.BO.AttachmentSvc/UploadFile";
                RequestURL = RequestURL.Replace("{ServerName}", ServerName);
                RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
                RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);


                string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
                HTTPMethods = "POST";
                HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);

                string strHTTPStatusCode = ResponseStatusCode;
                string strResponseBody = ResponseBody;
                string strExceptionMsg = ExceptionMsg;
                string strTDeserializeResponseBodyErrorMessage = ErrorMessage;


                if (ResponseStatusCode == "200")
                {
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    ////反序列化成Part对象
                    //PartData partData = jss.Deserialize<PartData>(ResponseBody);
                    dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                    if (dyn == null) return "";
                    List<PartData> lsPartDatas = new List<PartData> { };
                    foreach (var obj in dyn)
                    {
                        if (obj.Name == "returnObj")
                        {
                            EpicorFileName = obj.Value.ToString();
                            break;
                        }
                    }
                }

            }
            return EpicorFileName;
        }
        catch (AggregateException ex)
        {
            return "";
        }
    }

    public class QuoteHedAttachment
    {
        public string Company { get; set; }
        public int QuoteNum { get; set; }
        public string DrawDesc { get; set; }
        public string FileName { get; set; }
        public string DocTypeID { get; set; }

    }
    public string QuotedHeaderAttachmentSvc(string Company, int QuoteNum,  string FileName)
    {
        try
        {
            string EpicorFileName = "";
            //final RequestURL
            if (FileName != "")
            {
                EpicorFileName = UploadEpicorFile(Company, FileName);
                QuoteHedAttachment entry = new QuoteHedAttachment
                {
                    Company = Company,
                    QuoteNum = QuoteNum,
                    DrawDesc = FileName,
                    FileName = EpicorFileName,
                    DocTypeID = "",
                };


                string isoJson = JsonConvert.SerializeObject(entry);    //Convert DataEntry to Json string

                isoJson = isoJson.Replace("\r\n", "");
                isoJson = isoJson.Replace("\"0001-01-01T00:00:00Z\"", "null");
                isoJson = isoJson.Replace("00:00:00", "00:00:00Z");

                DataSet ds = GetList("");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                    APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                    ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                    AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                    AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
                }

 
                //final RequestURL
                string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.QuoteSvc/QuoteHedAttches";
                RequestURL = RequestURL.Replace("{ServerName}", ServerName);
                RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
                RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);


                string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
                HTTPMethods = "POST";
                HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);

                string strHTTPStatusCode = ResponseStatusCode;
                string strResponseBody = ResponseBody;
                string strExceptionMsg = ExceptionMsg;
                string strTDeserializeResponseBodyErrorMessage = ErrorMessage;


                if (ResponseStatusCode == "201")
                {
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    ////反序列化成Part对象
                    //PartData partData = jss.Deserialize<PartData>(ResponseBody);
                    dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                    if (dyn == null) return "";
                    List<PartData> lsPartDatas = new List<PartData> { };
                    foreach (var obj in dyn)
                    {
                        if (obj.Name == "returnObj")
                        {
                            EpicorFileName = obj.Value.ToString();
                            break;
                        }
                    }
                }

            }
            return EpicorFileName;
        }
        catch (AggregateException ex)
        {
            return "";
        }
    }
    public string QuotedDeatilAttachmentSvc(string Company, int QuoteNum, int QuoteLine, string FileName)
    {
        try
        {
            string EpicorFileName = "";
            //final RequestURL
            if (FileName != "")
            {
                EpicorFileName = UploadEpicorFile(Company,FileName);
                QuoteDtlAttachment entry = new QuoteDtlAttachment
                {
                    Company = Company,
                    QuoteNum = QuoteNum,
                    QuoteLine = QuoteLine,
                    DrawDesc = FileName,
                    FileName = EpicorFileName,
                    DocTypeID="",
                };


                string isoJson = JsonConvert.SerializeObject(entry);    //Convert DataEntry to Json string

                isoJson = isoJson.Replace("\r\n", "");
                isoJson = isoJson.Replace("\"0001-01-01T00:00:00Z\"", "null");
                isoJson = isoJson.Replace("00:00:00", "00:00:00Z");

                DataSet ds = GetList("");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                    APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                    ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                    AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                    AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
                }


                //final RequestURL
                string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.QuoteSvc/QuoteDtlAttches";
                RequestURL = RequestURL.Replace("{ServerName}", ServerName);
                RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
                RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);


                string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
                HTTPMethods = "POST";
                HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);

                string strHTTPStatusCode = ResponseStatusCode;
                string strResponseBody = ResponseBody;
                string strExceptionMsg = ExceptionMsg;
                string strTDeserializeResponseBodyErrorMessage = ErrorMessage;


                if (ResponseStatusCode == "201")
                {
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    ////反序列化成Part对象
                    //PartData partData = jss.Deserialize<PartData>(ResponseBody);
                    dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                    if (dyn == null) return "";
                    List<PartData> lsPartDatas = new List<PartData> { };
                    foreach (var obj in dyn)
                    {
                        if (obj.Name == "returnObj")
                        {
                            EpicorFileName = obj.Value.ToString();
                            break;
                        }
                    }
                }

            }
            return EpicorFileName;
        }
        catch (AggregateException ex)
        {
            return "";
        }
    }



    public class QuoteDtlAttachment
    {
        public string Company { get; set; }
        public int QuoteNum { get; set; }
        public int QuoteLine { get; set; }
        public string DrawDesc { get; set; }
        public string FileName { get; set; }
        public string DocTypeID { get; set; }
        
    }
    public void PostQuoteAttachmentToEpicor(string OrgOrderNumber,string EpicorQuoteOrderNumber)
    {
        ps_orders bllOrder = new ps_orders();
        bllOrder.GetModel(OrgOrderNumber);

        

        ps_order_goods bllOrderGoods = new ps_order_goods();
        DataSet dsOrderGoods = bllOrderGoods.GetList(" order_id = " + bllOrder.id + "");

        ps_depot_category bllCompany = new ps_depot_category();
        Company = bllCompany.GetCompany(bllOrder.depot_category_id == null ? 0 : Convert.ToInt32(bllOrder.depot_category_id));

        if (bllOrder.attachment1.ToString() != "")
        {
            string FileName = bllOrder.attachment1.ToString();
            QuotedHeaderAttachmentSvc(Company, Convert.ToInt32(EpicorQuoteOrderNumber), FileName);
        }
        if (bllOrder.attachment2.ToString() != "")
        {
            string FileName = bllOrder.attachment2.ToString();
            QuotedHeaderAttachmentSvc(Company, Convert.ToInt32(EpicorQuoteOrderNumber), FileName);
        }
        if (bllOrder.attachment3.ToString() != "")
        {
            string FileName = bllOrder.attachment3.ToString();
            QuotedHeaderAttachmentSvc(Company, Convert.ToInt32(EpicorQuoteOrderNumber), FileName);
        }
        if (bllOrder.attachment4.ToString() != "")
        {
            string FileName = bllOrder.attachment4.ToString();
            QuotedHeaderAttachmentSvc(Company, Convert.ToInt32(EpicorQuoteOrderNumber), FileName);
        }

        EpicorQuoteOrderNumber = EpicorQuoteOrderNumber == "" ? bllOrder.epicor_order_no.Split('(')[0].ToString() : EpicorQuoteOrderNumber;
        if (EpicorQuoteOrderNumber != "")
        {
            for (int i = 0; i < dsOrderGoods.Tables[0].Rows.Count; i++)
            {
                if (dsOrderGoods.Tables[0].Rows[i]["designattachment"].ToString() != "")
                {
                    string FileName = dsOrderGoods.Tables[0].Rows[i]["designattachment"].ToString();
                    QuotedDeatilAttachmentSvc(Company, Convert.ToInt32(EpicorQuoteOrderNumber), i + 1, FileName);
                }
                if (dsOrderGoods.Tables[0].Rows[i]["designattachment2"].ToString() != "")
                {
                    string FileName = dsOrderGoods.Tables[0].Rows[i]["designattachment2"].ToString();
                    QuotedDeatilAttachmentSvc(Company, Convert.ToInt32(EpicorQuoteOrderNumber), i + 1, FileName);
                }
                if (dsOrderGoods.Tables[0].Rows[i]["designattachment3"].ToString() != "")
                {
                    string FileName = dsOrderGoods.Tables[0].Rows[i]["designattachment3"].ToString();
                    QuotedDeatilAttachmentSvc(Company, Convert.ToInt32(EpicorQuoteOrderNumber), i + 1, FileName);
                }
                if (dsOrderGoods.Tables[0].Rows[i]["designattachment4"].ToString() != "")
                {
                    string FileName = dsOrderGoods.Tables[0].Rows[i]["designattachment4"].ToString();
                    QuotedDeatilAttachmentSvc(Company, Convert.ToInt32(EpicorQuoteOrderNumber), i + 1, FileName);
                }
                if (dsOrderGoods.Tables[0].Rows[i]["designattachment5"].ToString() != "")
                {
                    string FileName = dsOrderGoods.Tables[0].Rows[i]["designattachment5"].ToString();
                    QuotedDeatilAttachmentSvc(Company, Convert.ToInt32(EpicorQuoteOrderNumber), i + 1, FileName);
                }

            }
        }

    }
          

    private void HttpSendRequest(string HTTPMethods, string RequestURL, string epicorLogin, string XAPIKey, string jsonStr, ref string ResponseStatusCode, ref string ResponseBody, ref string IsSuccessStatusCode, ref string ErrorMessage, ref string ExceptionMsg)
    {
        try
        {
            //Ignore SSL certificates
            var handler = new WebRequestHandler();
            handler.ServerCertificateValidationCallback = delegate { return true; };

            using (HttpClient client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(epicorLogin)));
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-API-Key", XAPIKey);
                string TokenCreateURL = RequestURL;
                var request = new HttpRequestMessage();
                HttpResponseMessage response = null;

                if (HTTPMethods == "POST")
                {
                    request.RequestUri = new Uri(TokenCreateURL);
                    request.Method = HttpMethod.Post;

                    //send user credential
                    request.Content = new StringContent(jsonStr, Encoding.GetEncoding("UTF-8"), "application/json");
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    response = client.SendAsync(request).Result;
                    ResponseStatusCode = Convert.ToString((int)response.StatusCode);
                }

                if (HTTPMethods == "DELETE")
                {
                    response = client.DeleteAsync(TokenCreateURL).Result;
                    ResponseStatusCode = Convert.ToString((int)response.StatusCode);
                }

                if (HTTPMethods == "GET")
                {
                    response = client.GetAsync(TokenCreateURL).Result;
                    ResponseStatusCode = Convert.ToString((int)response.StatusCode);
                }

                //Get response


                if (response.IsSuccessStatusCode)
                {
                    IsSuccessStatusCode = "Yes";
                }
                else
                {
                    IsSuccessStatusCode = "No";
                }

                ResponseBody = response.Content.ReadAsStringAsync().Result;

                //Deserialize ResponseBody and catch "ErrorMessage"
                if (ResponseBody.IndexOf("\"ErrorMessage\"") > 0)
                {
                    dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);

                    foreach (var obj in dyn)
                    {
                        if (obj.Name == "ErrorMessage")
                        {
                            ErrorMessage = Convert.ToString(obj.Value);
                            break;
                        }
                    }
                }
            }
        }
        catch (AggregateException ex)
        {
            ExceptionMsg = ex.ToString();
        }
    }

    //private void HttpSendRequest2(string HTTPMethods, string RequestURL, string epicorLogin, string XAPIKey, string License, string jsonStr, ref string ResponseStatusCode, ref string ResponseBody, ref string IsSuccessStatusCode, ref string ErrorMessage, ref string ExceptionMsg)
    //{
    //    try
    //    {
    //        //Ignore SSL certificates
    //        var handler = new WebRequestHandler();
    //        handler.ServerCertificateValidationCallback = delegate { return true; };

    //        using (HttpClient client = new HttpClient(handler))
    //        {
    //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(epicorLogin)));
    //            // Add an Accept header for JSON format.
    //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //            client.DefaultRequestHeaders.Add("X-API-Key", XAPIKey);
    //            client.DefaultRequestHeaders.Add("License", License);
    //            string TokenCreateURL = RequestURL;
    //            var request = new HttpRequestMessage();
    //            HttpResponseMessage response = null;

    //            if (HTTPMethods == "POST")
    //            {
    //                request.RequestUri = new Uri(TokenCreateURL);
    //                request.Method = HttpMethod.Post;

    //                //send user credential
    //                request.Content = new StringContent(jsonStr, Encoding.GetEncoding("UTF-8"), "application/json");
    //                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

    //                response = client.SendAsync(request).Result;
    //                ResponseStatusCode = Convert.ToString((int)response.StatusCode);
    //            }

    //            if (HTTPMethods == "DELETE")
    //            {
    //                response = client.DeleteAsync(TokenCreateURL).Result;
    //                ResponseStatusCode = Convert.ToString((int)response.StatusCode);
    //            }

    //            if (HTTPMethods == "GET")
    //            {
    //                response = client.GetAsync(TokenCreateURL).Result;
    //                ResponseStatusCode = Convert.ToString((int)response.StatusCode);
    //            }

    //            //Get response


    //            if (response.IsSuccessStatusCode)
    //            {
    //                IsSuccessStatusCode = "Yes";
    //            }
    //            else
    //            {
    //                IsSuccessStatusCode = "No";
    //            }

    //            ResponseBody = response.Content.ReadAsStringAsync().Result;

    //            //Deserialize ResponseBody and catch "ErrorMessage"
    //            if (ResponseBody.IndexOf("\"ErrorMessage\"") > 0)
    //            {
    //                dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);

    //                foreach (var obj in dyn)
    //                {
    //                    if (obj.Name == "ErrorMessage")
    //                    {
    //                        ErrorMessage = Convert.ToString(obj.Value);
    //                        break;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (AggregateException ex)
    //    {
    //        ExceptionMsg = ex.ToString();
    //    }
    //}

    public class CustomerCreditInfo
    {
        public string Customer_Company { get; set; }
        public string Customer_CustID { get; set; }
        public string Customer_CustNum { get; set; }
        public string Customer_Name { get; set; }

        public string Customer_CreditHold { get; set; }
        public string Customer_CreditLimit { get; set; }
        public string Calculated_TotOrderCredit { get; set; }
        public string Calculated_TotOpenInvoices { get; set; }
        public string Calculated_TotOpenOrders { get; set; }
    }

    public CustomerCreditInfo[] GetCustomerCreditInfo(string CustID)
    {
        try
        {
            string MaxPartNum = "";
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/CustomerCredit/Data";
            if (CustID != "")
                RequestURL = RequestURL + "?% 24filter = Customer_CustID % 20eq % 20'{Customer_CustID}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/CustomerCredit/Data?%24filter=Calculated_Part%20eq%20'{Calculated_Part}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", "C01");
            RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            List<CustomerCreditInfo> lsCustomerCreditInfos = new List<CustomerCreditInfo> { };
            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_customer_credit model = new ps_customer_credit();
                    if (obj.Value.Count > 0)
                        model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {
                        CustomerCreditInfo CustomerCreditInfos = new CustomerCreditInfo();
                        CustomerCreditInfos.Customer_Company = obj.Value[i]["Customer_Company"].ToString();
                        CustomerCreditInfos.Customer_CustID = obj.Value[i]["Customer_CustID"].ToString();
                        CustomerCreditInfos.Customer_CustNum = obj.Value[i]["Customer_CustNum"].ToString();
                        CustomerCreditInfos.Customer_Name = obj.Value[i]["Customer_Name"].ToString();
                        CustomerCreditInfos.Customer_CreditHold = obj.Value[i]["Customer_CreditHold"].ToString();
                        CustomerCreditInfos.Customer_CreditLimit = obj.Value[i]["Customer_CreditLimit"].ToString();
                        CustomerCreditInfos.Calculated_TotOrderCredit = obj.Value[i]["Calculated_TotOrderCredit"].ToString();
                        CustomerCreditInfos.Calculated_TotOpenInvoices = obj.Value[i]["Calculated_TotOpenInvoices"].ToString();
                        CustomerCreditInfos.Calculated_TotOpenOrders = obj.Value[i]["Calculated_TotOpenOrders"].ToString();
                        lsCustomerCreditInfos.Add(CustomerCreditInfos);
                        
                        model.Company = obj.Value[i]["Customer_Company"].ToString();
                        model.CustID = obj.Value[i]["Customer_CustID"].ToString();
                        model.CustNum = obj.Value[i]["Customer_CustNum"].ToString();
                        model.CustName = obj.Value[i]["Customer_Name"].ToString();
                        model.CreditHold = obj.Value[i]["Customer_CreditHold"].ToString();
                        model.CreditLimit = obj.Value[i]["Customer_CreditLimit"].ToString();
                        model.TotOrderCredit = obj.Value[i]["Calculated_TotOrderCredit"].ToString();
                        model.TotOpenInvoices = obj.Value[i]["Calculated_TotOpenInvoices"].ToString();
                        model.TotOpenOrders = obj.Value[i]["Calculated_TotOpenOrders"].ToString();
                        model.Add();
                    }
                }
            }
            return lsCustomerCreditInfos.ToArray();

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }


    public class PurchaseOrder
    {
        public string Customer_Company { get; set; }
        public string Customer_CustID { get; set; }
        public string Customer_CustNum { get; set; }
        public string Customer_Name { get; set; }

        public string Customer_CreditHold { get; set; }
        public string Customer_CreditLimit { get; set; }
        public string Calculated_TotOrderCredit { get; set; }
        public string Calculated_TotOpenInvoices { get; set; }
        public string Calculated_TotOpenOrders { get; set; }

      // "POHeader_Company": "C01",
      //"POHeader_OpenOrder": true,
      //"POHeader_VoidOrder": false,
      //"POHeader_PONum": 1000072,
      //"POHeader_EntryPerson": "0000748",
      //"POHeader_OrderDate": "2023-05-17T00:00:00+08:00",
      //"POHeader_FOB": "",
      //"POHeader_ShipViaCode": "20",
      //"POHeader_TermsCode": "D30",
      //"POHeader_BuyerID": "IC",
      //"POHeader_CommentText": "",
      //"POHeader_VendorNum": 128,
      //"POHeader_ApprovedDate": null,
      //"POHeader_ApprovedBy": "",
      //"POHeader_Approve": false,
      //"POHeader_ApprovalStatus": "U",
      //"POHeader_ConfirmReq": false,
      //"POHeader_Confirmed": false,
      //"POHeader_ConfirmVia": "",
      //"POHeader_DocTotalOrder": 20,
      //"POHeader_DocTotalTax": 2.9,
      //"PODetail_OpenLine": true,
      //"PODetail_VoidLine": false,
      //"PODetail_PONUM": 1000072,
      //"PODetail_POLine": 1,
      //"PODetail_LineDesc": "PY32006A-P缇派浴室柜盆柜灰白色石纹（电商）590*470*820",
      //"PODetail_IUM": "PC",
      //"PODetail_UnitCost": 1.71,
      //"PODetail_DocUnitCost": 1.71,
      //"PODetail_OrderQty": 2,
      //"PODetail_XOrderQty": 2,
      //"PODetail_PUM": "PC",
      //"PODetail_PartNum": "P10201000014",
      //"PODetail_CommentText": "",
      //"PODetail_ConfirmDate": null,
      //"PODetail_DueDate": "2023-05-17T00:00:00+08:00",
      //"PODetail_DocInUnitCost": 2,
      //"PODetail_DocExtCost": 3.42,
      //"PODetail_DocTotalTax": 0.58,
      //"PORel_DueDate": "2023-05-17T00:00:00+08:00",
      //"PORel_XRelQty": 2,
      //"PORel_RelQty": 2,
      //"PORel_PromiseDt": null,
      //"PORel_Confirmed": false,
      //"PORel_ConfirmVia": "",
      //"Vendor_VendorID": "10300032",
      //"Vendor_Name": "安平县阳光卫浴有限公司",
      //"Vendor_Inactive": false,
    }

    public CustomerCreditInfo[] GetPurchaseOrder(string CustID)
    {
        try
        {
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/GetPurchaseOrder/Data?%24orderby=POHeader_OrderDate%20desc&%24top=100";

            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", "C01");
            RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            List<CustomerCreditInfo> lsCustomerCreditInfos = new List<CustomerCreditInfo> { };
            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_customer_credit model = new ps_customer_credit();
                    if (obj.Value.Count > 0)
                        model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {
                        CustomerCreditInfo CustomerCreditInfos = new CustomerCreditInfo();
                        CustomerCreditInfos.Customer_Company = obj.Value[i]["Customer_Company"].ToString();
                        CustomerCreditInfos.Customer_CustID = obj.Value[i]["Customer_CustID"].ToString();
                        CustomerCreditInfos.Customer_CustNum = obj.Value[i]["Customer_CustNum"].ToString();
                        CustomerCreditInfos.Customer_Name = obj.Value[i]["Customer_Name"].ToString();
                        CustomerCreditInfos.Customer_CreditHold = obj.Value[i]["Customer_CreditHold"].ToString();
                        CustomerCreditInfos.Customer_CreditLimit = obj.Value[i]["Customer_CreditLimit"].ToString();
                        CustomerCreditInfos.Calculated_TotOrderCredit = obj.Value[i]["Calculated_TotOrderCredit"].ToString();
                        CustomerCreditInfos.Calculated_TotOpenInvoices = obj.Value[i]["Calculated_TotOpenInvoices"].ToString();
                        CustomerCreditInfos.Calculated_TotOpenOrders = obj.Value[i]["Calculated_TotOpenOrders"].ToString();
                        lsCustomerCreditInfos.Add(CustomerCreditInfos);

                        model.Company = obj.Value[i]["Customer_Company"].ToString();
                        model.CustID = obj.Value[i]["Customer_CustID"].ToString();
                        model.CustNum = obj.Value[i]["Customer_CustNum"].ToString();
                        model.CustName = obj.Value[i]["Customer_Name"].ToString();
                        model.CreditHold = obj.Value[i]["Customer_CreditHold"].ToString();
                        model.CreditLimit = obj.Value[i]["Customer_CreditLimit"].ToString();
                        model.TotOrderCredit = obj.Value[i]["Calculated_TotOrderCredit"].ToString();
                        model.TotOpenInvoices = obj.Value[i]["Calculated_TotOpenInvoices"].ToString();
                        model.TotOpenOrders = obj.Value[i]["Calculated_TotOpenOrders"].ToString();
                        model.Add();
                    }
                }
            }
            return lsCustomerCreditInfos.ToArray();

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }


    public ps_epicor_vendor[] GetEpicorVenderList(string vendorid)
    {
        try
        {
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/ud_SupPortalVendReg/Data";
            if(vendorid!="")
                RequestURL  = RequestURL  + "?%24filter=Vendor_VendorID%20eq%20'" + vendorid + "' ";
            //if (CustID != "")
            //    RequestURL = RequestURL + "?% 24filter = Customer_CustID % 20eq % 20'{Customer_CustID}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/CustomerCredit/Data?%24filter=Calculated_Part%20eq%20'{Calculated_Part}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            //RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            List<ps_epicor_vendor> lsEpicorVendor = new List<ps_epicor_vendor> { };
            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_epicor_vendor model = new ps_epicor_vendor();
                    //if (obj.Value.Count > 0)
                    //    model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {
                        ps_epicor_vendor EpicorVendor = new ps_epicor_vendor();
                        EpicorVendor.Vendor_Company = obj.Value[i]["Vendor_Company"].ToString();
                        EpicorVendor.Vendor_VendorNum = obj.Value[i]["Vendor_VendorNum"].ToString();
                        EpicorVendor.Vendor_VendorID = obj.Value[i]["Vendor_VendorID"].ToString();
                        EpicorVendor.Vendor_Name = obj.Value[i]["Vendor_Name"].ToString();
                        EpicorVendor.Vendor_Approved = obj.Value[i]["Vendor_Approved"].ToString();
                        EpicorVendor.Vendor_CurrencyCode = obj.Value[i]["Vendor_CurrencyCode"].ToString();
                        EpicorVendor.Vendor_TermsCode = obj.Value[i]["Vendor_TermsCode"].ToString();
                        EpicorVendor.Vendor_BRNum_c = obj.Value[i]["Vendor_BRNum_c"].ToString();
                        EpicorVendor.Vendor_BRExpiryDate_c = Convert.ToDateTime(obj.Value[i]["Vendor_BRExpiryDate_c"].ToString()=="" ? "9999-12-31" : obj.Value[i]["Vendor_BRExpiryDate_c"].ToString());
                        EpicorVendor.Vendor_OrgRegCode = obj.Value[i]["Vendor_OrgRegCode"].ToString();
                        EpicorVendor.Vendor_PhoneNum = obj.Value[i]["Vendor_PhoneNum"].ToString();
                        EpicorVendor.Vendor_FaxNum = obj.Value[i]["Vendor_FaxNum"].ToString();
                        EpicorVendor.Vendor_EMailAddress = obj.Value[i]["Vendor_EMailAddress"].ToString();
                        EpicorVendor.Vendor_Address1 = obj.Value[i]["Vendor_Address1"].ToString();
                        EpicorVendor.Vendor_Address2 = obj.Value[i]["Vendor_Address2"].ToString();
                        EpicorVendor.Vendor_Address3 = obj.Value[i]["Vendor_Address3"].ToString();
                        EpicorVendor.Vendor_City = obj.Value[i]["Vendor_City"].ToString();
                        EpicorVendor.Vendor_State = obj.Value[i]["Vendor_State"].ToString();
                        EpicorVendor.Vendor_ZIP = obj.Value[i]["Vendor_ZIP"].ToString();
                        EpicorVendor.Vendor_Country = obj.Value[i]["Vendor_Country"].ToString();
                        EpicorVendor.VendCnt_ConNum = obj.Value[i]["VendCnt_ConNum"].ToString();
                        EpicorVendor.VendCnt_PerConID = obj.Value[i]["VendCnt_PerConID"].ToString();
                        EpicorVendor.VendCnt_Name = obj.Value[i]["VendCnt_Name"].ToString();
                        EpicorVendor.VendCnt_Func = obj.Value[i]["VendCnt_Func"].ToString();
                        EpicorVendor.VendCnt_PhoneNum = obj.Value[i]["VendCnt_PhoneNum"].ToString();
                        EpicorVendor.VendCnt_CellPhoneNum = obj.Value[i]["VendCnt_CellPhoneNum"].ToString();
                        EpicorVendor.VendCnt_EmailAddress = obj.Value[i]["VendCnt_EmailAddress"].ToString();
                        EpicorVendor.VendCnt_UD_RFQRecipent_c = obj.Value[i]["VendCnt_UD_RFQRecipent_c"].ToString();
                        EpicorVendor.VendCnt_UD_PORecipient_c = obj.Value[i]["VendCnt_UD_PORecipient_c"].ToString();
                        EpicorVendor.Calculated_PrimaryContact = obj.Value[i]["Calculated_PrimaryContact"].ToString();
                        EpicorVendor.Vendor_PortalRegSubmit_c = obj.Value[i]["Vendor_PortalRegSubmit_c"].ToString();
                        EpicorVendor.Vendor_PortalRegExpiryDate_c = Convert.ToDateTime(obj.Value[i]["Vendor_PortalRegExpiryDate_c"].ToString() == "" ? "9999-12-30" : obj.Value[i]["Vendor_PortalRegExpiryDate_c"].ToString()); ; ;
                        EpicorVendor.Vendor_PortalResetPWTime_c = obj.Value[i]["Vendor_PortalResetPWTime_c"].ToString();
                        EpicorVendor.Vendor_PortalRegTempPW_c = obj.Value[i]["Vendor_PortalRegTempPW_c"].ToString();
                        EpicorVendor.Vendor_PortalRegEmailAddress_c = obj.Value[i]["Vendor_PortalRegEmailAddress_c"]==null ? "" : obj.Value[i]["Vendor_PortalRegEmailAddress_c"].ToString();
                        EpicorVendor.PurTerms_Description = obj.Value[i]["PurTerms_Description"] == null ? "" : obj.Value[i]["PurTerms_Description"].ToString();
                        
                        EpicorVendor.VendorBank_BankID = obj.Value[i]["VendBank_BankID"].ToString();
                        EpicorVendor.VendorBank_BankName = obj.Value[i]["VendBank_BankName"].ToString();
                        EpicorVendor.VendorBank_CountryNum = obj.Value[i]["VendBank_CountryNum"].ToString();
                        EpicorVendor.VendorBank_BankAcctNumber = obj.Value[i]["VendBank_BankAcctNumber"].ToString();
                        EpicorVendor.VendorBank_NameOnAccount = obj.Value[i]["VendBank_NameOnAccount"].ToString();
                        EpicorVendor.VendorBank_PayMethodType = obj.Value[i]["VendBank_PMUID"].ToString(); 
                        EpicorVendor.VendorBank_SwiftNum = obj.Value[i]["VendBank_SwiftNum"].ToString();
                        EpicorVendor.VendorBank_IBANABABSBCode = obj.Value[i]["VendBank_IBANCode"].ToString();


                        lsEpicorVendor.Add(EpicorVendor);


                        EpicorVendor.Add();
                        EpicorVendor.UpdateVendorTempInfoByCompanyAndVerdorID(obj.Value[i]["Vendor_Company"].ToString(), obj.Value[i]["Vendor_VendorID"].ToString());
                        EpicorVendor.UpdateVendorByCompanyAndVerdorID(obj.Value[i]["Vendor_Company"].ToString(), obj.Value[i]["Vendor_VendorID"].ToString(),false);
                    }
                }
            }
            return lsEpicorVendor.ToArray();

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }


    public ps_epicor_vendorbank[] GetEpicorVenderBankList()
    {
        try
        {
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.VendBankSearchSvc/List";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            //RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);
            //strSql.Append("VendorBank_Company,VendorBank_VendorNum,VendorBank_BankID,VendorBank_BankName,VendorBank_CountryNum,VendorBank_BankAcctNumber,VendorBank_NameOnAccount,
            //VendorBank_PayMethodType,VendorBank_SwiftNum,Create_Date)");

            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            List<ps_epicor_vendorbank> lsEpicorVendorBank = new List<ps_epicor_vendorbank> { };
            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_epicor_vendorbank model = new ps_epicor_vendorbank();
                    
                    for (int i = 0; i < obj.Value.Count; i++)
                    {
                        ps_epicor_vendorbank EpicorVendorBank = new ps_epicor_vendorbank();
                        EpicorVendorBank.VendorBank_Company = obj.Value[i]["Company"].ToString();
                        EpicorVendorBank.VendorBank_VendorNum = obj.Value[i]["VendorNum"].ToString();
                        EpicorVendorBank.VendorBank_BankID = obj.Value[i]["BankID"].ToString();
                        EpicorVendorBank.VendorBank_BankName = obj.Value[i]["BankName"].ToString();
                        EpicorVendorBank.VendorBank_CountryNum = obj.Value[i]["CountryNum"].ToString();
                        EpicorVendorBank.VendorBank_BankAcctNumber = obj.Value[i]["BankAcctNumber"].ToString();
                        EpicorVendorBank.VendorBank_NameOnAccount = obj.Value[i]["NameOnAccount"].ToString();
                        EpicorVendorBank.VendorBank_PayMethodType = obj.Value[i]["PayMethodType"].ToString();

                        EpicorVendorBank.VendorBank_SwiftNum = obj.Value[i]["SwiftNum"].ToString();


                        lsEpicorVendorBank.Add(EpicorVendorBank);


                        EpicorVendorBank.Add(EpicorVendorBank);
                    }
                }
            }
            return lsEpicorVendorBank.ToArray();

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }

    public class EpicorVendor
    {
        public string Company { get; set; }
        public string VendorID { get; set; }
        public string Name { get; set; }
        public int VendorNum { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string FaxNum { get; set; }
        public string PhoneNum { get; set; }
        public string EMailAddress { get; set; }
        public string PortalResetPWTime_c { get; set; }

        public string ZIP { get; set; }
        public string OrgRegCode { get; set; }
        public string BRNum_c { get; set; }
        public DateTime BRExpiryDate_c { get; set; }

        public List<VendCntMains> VendCntMains { get; set; }
    }

    public class EpicorVendorMain
    {
        public string Company { get; set; }
        public string VendorID { get; set; }
        public string Name { get; set; }
        public int VendorNum { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string FaxNum { get; set; }
        public string PhoneNum { get; set; }
        public string EMailAddress { get; set; }
        public string PortalResetPWTime_c { get; set; }

        public string ZIP { get; set; }
        public string OrgRegCode { get; set; }
        public string BRNum_c { get; set; }
        public DateTime BRExpiryDate_c { get; set; }

    }

    public class VendCntMains
    {
        public string Company { get; set; }
        public int VendorNum { get; set; }
        public int ConNum { get; set; }
        public string Name { get; set; }
        public string Func { get; set; }
        public string CellPhoneNum { get; set; }
        public string PhoneNum { get; set; }
        public string EmailAddress { get; set; }
        public bool PrimaryContact { get; set; }
        public bool UD_PORecipient_c { get; set; }
        public bool UD_RFQRecipent_c { get; set; }

        


    }    

    public string PostVendorToEpicor(string Company, string VendorID)
    {
        ps_epicor_vendor bllVendor = new ps_epicor_vendor();
        bllVendor.GetModelByVendorID(Company, VendorID);
        string VendorNum = "";


        DataSet ds = GetList("");
        List<VendCntMains> lsVendCnts = new List<VendCntMains>();
        if (ds.Tables[0].Rows.Count > 0)
        {
            UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
            APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
            ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
            AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
            AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            try
            {
                if (bllVendor != null)
                {
                    DataSet dsContact = bllVendor.GetList(" Vendor_Company = '" + bllVendor.Vendor_Company + "' and Vendor_VendorID = '" + bllVendor.Vendor_VendorID + "' and VendCnt_ConNum<>'' ");
                    if (dsContact.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsContact.Tables[0].Rows.Count; i++)
                        {
                            VendCntMains VendCnt = new VendCntMains();
                            VendCnt.Company = dsContact.Tables[0].Rows[i]["Vendor_Company"].ToString();
                            VendCnt.VendorNum = Convert.ToInt32(dsContact.Tables[0].Rows[i]["Vendor_VendorNum"].ToString());
                            VendCnt.ConNum = Convert.ToInt32(dsContact.Tables[0].Rows[i]["VendCnt_ConNum"].ToString());
                            VendCnt.Name = dsContact.Tables[0].Rows[i]["VendCnt_Name"].ToString();
                            VendCnt.Func = dsContact.Tables[0].Rows[i]["VendCnt_Func"].ToString();
                            VendCnt.PhoneNum = dsContact.Tables[0].Rows[i]["VendCnt_PhoneNum"].ToString();
                            VendCnt.CellPhoneNum = dsContact.Tables[0].Rows[i]["VendCnt_CellPhoneNum"].ToString();
                            VendCnt.EmailAddress = dsContact.Tables[0].Rows[i]["VendCnt_EmailAddress"].ToString();
                            VendCnt.PrimaryContact = dsContact.Tables[0].Rows[i]["Calculated_PrimaryContact"].ToString() == "True ? true : false";
                            VendCnt.UD_PORecipient_c = dsContact.Tables[0].Rows[i]["Vendor_Company"].ToString() == "True ? true : false";
                            VendCnt.UD_RFQRecipent_c = dsContact.Tables[0].Rows[i]["Vendor_Company"].ToString() == "True ? true : false";
                            lsVendCnts.Add(VendCnt);
                        }

                    }
                    string isoJson = "";
                    if (lsVendCnts.Count > 0)
                    {
                        EpicorVendor entry = new EpicorVendor
                        {
                            Company = bllVendor.Vendor_Company,
                            VendorID = bllVendor.Vendor_VendorID,
                            Name = bllVendor.Vendor_Name,
                            VendorNum = Convert.ToInt32(bllVendor.Vendor_VendorNum),
                            Address1 = bllVendor.Vendor_Address1,
                            Address2 = bllVendor.Vendor_Address2,
                            Address3 = bllVendor.Vendor_Address3,
                            City = bllVendor.Vendor_City,
                            State = bllVendor.Vendor_State,
                            Country = bllVendor.Vendor_Country,
                            FaxNum = bllVendor.Vendor_FaxNum,
                            PhoneNum = bllVendor.Vendor_PhoneNum,
                            EMailAddress = bllVendor.Vendor_EMailAddress,
                            ZIP = bllVendor.Vendor_ZIP,
                            OrgRegCode = bllVendor.Vendor_OrgRegCode,
                            BRNum_c = bllVendor.Vendor_BRNum_c,
                            BRExpiryDate_c = bllVendor.Vendor_BRExpiryDate_c == null ? new DateTime(9999, 12, 31) : Convert.ToDateTime(bllVendor.Vendor_BRExpiryDate_c),
                        };

                        entry.VendCntMains = lsVendCnts;
                        isoJson = JsonConvert.SerializeObject(entry);
                    }
                    else
                    {
                        EpicorVendorMain entry = new EpicorVendorMain
                        {
                            Company = bllVendor.Vendor_Company,
                            VendorID = bllVendor.Vendor_VendorID,
                            Name = bllVendor.Vendor_Name,
                            VendorNum = Convert.ToInt32(bllVendor.Vendor_VendorNum),
                            Address1 = bllVendor.Vendor_Address1,
                            Address2 = bllVendor.Vendor_Address2,
                            Address3 = bllVendor.Vendor_Address3,
                            City = bllVendor.Vendor_City,
                            State = bllVendor.Vendor_State,
                            Country = bllVendor.Vendor_Country,
                            FaxNum = bllVendor.Vendor_FaxNum,
                            PhoneNum = bllVendor.Vendor_PhoneNum,
                            EMailAddress = bllVendor.Vendor_EMailAddress,
                            //PortalResetPWTime_c = "null",
                            ZIP = bllVendor.Vendor_ZIP,
                            OrgRegCode = bllVendor.Vendor_OrgRegCode,
                            BRNum_c = bllVendor.Vendor_BRNum_c,
                            BRExpiryDate_c = bllVendor.Vendor_BRExpiryDate_c == null ? new DateTime(9999, 12, 31) : Convert.ToDateTime(bllVendor.Vendor_BRExpiryDate_c),


                        };
                        isoJson = JsonConvert.SerializeObject(entry);
                    }

                    //string isoJson = JsonConvert.SerializeObject(entry);    //Convert DataEntry to Json string

                    isoJson = isoJson.Replace("\r\n", "");
                    isoJson = isoJson.Replace("\"0001-01-01T00:00:00Z\"", "null");
                    isoJson = isoJson.Replace("00:00:00", "00:00:00Z");

                    //final RequestURL
                    string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.VendorSvc/Vendors";
                    RequestURL = RequestURL.Replace("{ServerName}", ServerName);
                    RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
                    RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);



                    string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
                    HTTPMethods = "POST";
                    HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);

                    string strHTTPStatusCode = ResponseStatusCode;
                    string strResponseBody = ResponseBody;
                    string strExceptionMsg = ExceptionMsg;
                    string strTDeserializeResponseBodyErrorMessage = ErrorMessage;

                    if (ResponseStatusCode == "201")
                    {
                        //JavaScriptSerializer jss = new JavaScriptSerializer();
                        //反序列化成Part对象
                        //PartData partData = jss.Deserialize<PartData>(ResponseBody);
                        dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                        if (dyn == null) return null;
                        List<PartData> lsPartDatas = new List<PartData> { };
                        foreach (var obj in dyn)
                        {
                            if (obj.Name == "VendorNum")
                            {
                                VendorNum = obj.Value;
                                break;
                            }
                        }
                    }
                    ////写入登录日志
                    ps_manager_log mylog = new ps_manager_log();
                    mylog.user_id = 1;
                    mylog.user_name = "API";
                    mylog.action_type = "Epcior";
                    mylog.add_time = DateTime.Now;
                    mylog.remark = "Update Vendor(Vendor:" + Company + " -" + VendorID + ")";
                    mylog.user_ip = AXRequest.GetIP();
                    mylog.Add();
                }
                return VendorNum;

            }
            catch (AggregateException ex)
            {
                string strExceptionMsg = ex.ToString();
                return "Error:" + strExceptionMsg;
            }
        }
        else
            return "Error";
    }



    public class EpicorVendorStatus
    {
        public string Company { get; set; }
        public string VendorID { get; set; }
        public int VendorNum { get; set; }
        public string ud_SPortalInfoUpdateStatus_c { get; set; }
        public DateTime ud_SPortalUpdateDateTime_c { get; set; }

    }

    public string PostVendorStatusToEpicor(string Company, string VendorID)
    {
        ps_epicor_vendor bllVendor = new ps_epicor_vendor();
        bllVendor.GetModelByVendorID(Company, VendorID);
        string VendorNum = "";


        DataSet ds = GetList("");
        List<VendCntMains> lsVendCnts = new List<VendCntMains>();
        if (ds.Tables[0].Rows.Count > 0)
        {
            UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
            APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
            ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
            AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
            AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            try
            {
                if (bllVendor != null && bllVendor.Vendor_VendorID!="")
                {
                    
                    string isoJson = "";
                    
                    EpicorVendorStatus entry = new EpicorVendorStatus
                    {
                        Company = bllVendor.Vendor_Company,
                        VendorID = bllVendor.Vendor_VendorID,
                        VendorNum = Convert.ToInt32(bllVendor.Vendor_VendorNum),
                        ud_SPortalInfoUpdateStatus_c = "Submitted",
                        ud_SPortalUpdateDateTime_c = System.DateTime.Now,
                    };

                    isoJson = JsonConvert.SerializeObject(entry);
                    

                    //string isoJson = JsonConvert.SerializeObject(entry);    //Convert DataEntry to Json string

                    isoJson = isoJson.Replace("\r\n", "");
                    isoJson = isoJson.Replace("\"0001-01-01T00:00:00Z\"", "null");
                    isoJson = isoJson.Replace("00:00:00", "00:00:00Z");

                    //final RequestURL
                    string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.VendorSvc/Vendors";
                    RequestURL = RequestURL.Replace("{ServerName}", ServerName);
                    RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
                    RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);



                    string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
                    HTTPMethods = "POST";
                    HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);

                    string strHTTPStatusCode = ResponseStatusCode;
                    string strResponseBody = ResponseBody;
                    string strExceptionMsg = ExceptionMsg;
                    string strTDeserializeResponseBodyErrorMessage = ErrorMessage;

                    if (ResponseStatusCode == "201")
                    {
                        //JavaScriptSerializer jss = new JavaScriptSerializer();
                        //反序列化成Part对象
                        //PartData partData = jss.Deserialize<PartData>(ResponseBody);
                        dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                        //Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                        List<PartData> lsPartDatas = new List<PartData> { };
                        foreach (var obj in dyn)
                        {
                            if (obj.Name == "VendorNum")
                            {
                                VendorNum = obj.Value;
                                break;
                            }
                        }
                    }
                    ////写入登录日志
                    ps_manager_log mylog = new ps_manager_log();
                    mylog.user_id = 1;
                    mylog.user_name = "API";
                    mylog.action_type = "Epcior";
                    mylog.add_time = DateTime.Now;
                    mylog.remark = "Submit Vendor(Vendor:" + Company + " -" + VendorID + ")";
                    mylog.user_ip = AXRequest.GetIP();
                    mylog.Add();
                }
                return VendorNum;

            }
            catch (AggregateException ex)
            {
                string strExceptionMsg = ex.ToString();
                return "Error:" + strExceptionMsg;
            }
        }
        else
            return "Error";
    }


    public class EpicorVendor2
    {
        public string Company { get; set; }
        public string VendorID { get; set; }
        public string Name { get; set; }
        public int VendorNum { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string FaxNum { get; set; }
        public string PhoneNum { get; set; }
        public string EMailAddress { get; set; }
        public string PortalResetPWTime_c { get; set; }

        public string ZIP { get; set; }
        public string OrgRegCode { get; set; }
        public string BRNum_c { get; set; }
    }

    public string PostVendorPortalResetPWTimeToEpicor(string Company, string VendorID)
    {
        ps_epicor_vendor bllVendor = new ps_epicor_vendor();
        bllVendor.GetModelByVendorID(Company, VendorID);
        string VendorNum = "";


        DataSet ds = GetList("");
        if (ds.Tables[0].Rows.Count > 0)
        {
            UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
            APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
            ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
            AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
            AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            try
            {
                if (bllVendor != null)
                {
                    EpicorVendor2 entry = new EpicorVendor2
                    {
                        Company = bllVendor.Vendor_Company,

                        VendorID = bllVendor.Vendor_VendorID,
                        Name = bllVendor.Vendor_Name,
                        VendorNum = Convert.ToInt32(bllVendor.Vendor_VendorNum),
                        Address1 = bllVendor.Vendor_Address1,
                        Address2 = bllVendor.Vendor_Address2,
                        Address3 = bllVendor.Vendor_Address3,
                        City = bllVendor.Vendor_City,
                        State = bllVendor.Vendor_State,
                        Country = bllVendor.Vendor_Country,
                        FaxNum = bllVendor.Vendor_FaxNum,
                        PhoneNum = bllVendor.Vendor_PhoneNum,
                        EMailAddress = bllVendor.Vendor_EMailAddress,
                        //PortalResetPWTime_c = System.DateTime.Now.ToLocalTime().ToString(),
                        PortalResetPWTime_c = System.DateTime.Now.GetDateTimeFormats('s')[0].ToString()+ ".835Z",
                        //PortalResetPWTime_c = "2023-11-23T14:33:34.835Z",


                    };

                    string isoJson = JsonConvert.SerializeObject(entry);    //Convert DataEntry to Json string

                    isoJson = isoJson.Replace("\r\n", "");
                    isoJson = isoJson.Replace("\"0001-01-01T00:00:00Z\"", "null");
                    isoJson = isoJson.Replace("00:00:00", "00:00:00Z");

                    //final RequestURL
                    string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.VendorSvc/Vendors";
                    RequestURL = RequestURL.Replace("{ServerName}", ServerName);
                    RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
                    RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);



                    string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
                    HTTPMethods = "POST";
                    HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);

                    string strHTTPStatusCode = ResponseStatusCode;
                    string strResponseBody = ResponseBody;
                    string strExceptionMsg = ExceptionMsg;
                    string strTDeserializeResponseBodyErrorMessage = ErrorMessage;

                    if (ResponseStatusCode == "201")
                    {
                        //JavaScriptSerializer jss = new JavaScriptSerializer();
                        //反序列化成Part对象
                        //PartData partData = jss.Deserialize<PartData>(ResponseBody);
                        dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                        if (dyn == null) return "Error";
                        List<PartData> lsPartDatas = new List<PartData> { };
                        foreach (var obj in dyn)
                        {
                            if (obj.Name == "VendorNum")
                            {
                                VendorNum = obj.Value;
                                break;
                            }
                        }
                    }
                    ////写入登录日志
                    ps_manager_log mylog = new ps_manager_log();
                    mylog.user_id = 1;
                    mylog.user_name = "API";
                    mylog.action_type = "Epcior";
                    mylog.add_time = DateTime.Now;
                    mylog.remark = "Update Vendor(Vendor:" + Company + " -" + VendorID + ")";
                    mylog.user_ip = AXRequest.GetIP();
                    mylog.Add();
                }
                return VendorNum;

            }
            catch (AggregateException ex)
            {
                string strExceptionMsg = ex.ToString();
                return "Error:" + strExceptionMsg;
            }
        }
        else
            return "Error";
    }


    public ps_epicor_country[] GetEpicorCountryList()
    {
        try
        {
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/spFindCountry/Data";
            //if (CustID != "")
            //    RequestURL = RequestURL + "?% 24filter = Customer_CustID % 20eq % 20'{Customer_CustID}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/CustomerCredit/Data?%24filter=Calculated_Part%20eq%20'{Calculated_Part}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            //RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            List<ps_epicor_country> lsEpicorCountry = new List<ps_epicor_country> { };
            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_epicor_country model = new ps_epicor_country();
                    if (obj.Value.Count > 0)
                        model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {
                        ps_epicor_country EpicorCountry = new ps_epicor_country();
                        EpicorCountry.Country_Company = obj.Value[i]["Country_Company"].ToString();
                        EpicorCountry.Country_CountryNum = obj.Value[i]["Country_CountryNum"].ToString();
                        EpicorCountry.Country_Description = obj.Value[i]["Country_Description"].ToString();


                        lsEpicorCountry.Add(EpicorCountry);


                        EpicorCountry.Add();
                    }
                }
            }
            return lsEpicorCountry.ToArray();

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }


    public ps_epicor_currency[] GetEpicorCurrencyList()
    {
        try
        {
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/spFindCcy/Data";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany == "" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            //RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            List<ps_epicor_currency> lsEpicorCurrency = new List<ps_epicor_currency> { };
            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_epicor_currency model = new ps_epicor_currency();
                    if (obj.Value.Count > 0)
                        model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {
                        ps_epicor_currency EpicorCurrency = new ps_epicor_currency();
                        EpicorCurrency.Currency_Company = obj.Value[i]["Currency_Company"].ToString();
                        EpicorCurrency.Currency_CurrencyCode = obj.Value[i]["Currency_CurrencyCode"].ToString();
                        EpicorCurrency.Currency_CurrDesc = obj.Value[i]["Currency_CurrDesc"].ToString();


                        lsEpicorCurrency.Add(EpicorCurrency);


                        EpicorCurrency.Add();
                    }
                }
            }
            return lsEpicorCurrency.ToArray();

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }

    public ps_epicor_paymethod[] GetEpicorPayMethodList()
    {
        try
        {
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/spFindPayMethod/Data";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany == "" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            //RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            List<ps_epicor_paymethod> lsEpicorPayMethod = new List<ps_epicor_paymethod> { };
            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_epicor_paymethod model = new ps_epicor_paymethod();
                    if (obj.Value.Count > 0)
                        model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {
                        ps_epicor_paymethod EpicorPayMethod = new ps_epicor_paymethod();
                        EpicorPayMethod.PayMethod_Company = obj.Value[i]["PayMethod_Company"].ToString();
                        EpicorPayMethod.PayMethod_PMUID = obj.Value[i]["PayMethod_PMUID"].ToString();
                        EpicorPayMethod.PayMethod_Name = obj.Value[i]["PayMethod_Name"].ToString();


                        lsEpicorPayMethod.Add(EpicorPayMethod);


                        EpicorPayMethod.Add();
                    }
                }
            }
            return lsEpicorPayMethod.ToArray();

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }

    public ps_epicor_po[] GetEpicorPOList(string VendorID)
    {
        try
        {
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/ud_SupPortalPO/Data";
            if (VendorID != "")
                RequestURL = RequestURL + "?% 24filter = Vendor_VendorID % 20eq % 20'" + VendorID + "'";
            //if (CustID != "")
            //    RequestURL = RequestURL + "?% 24filter = Customer_CustID % 20eq % 20'{Customer_CustID}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/CustomerCredit/Data?%24filter=Calculated_Part%20eq%20'{Calculated_Part}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            //RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            List<ps_epicor_po> lsEpicorPO = new List<ps_epicor_po> { };
            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_epicor_po model = new ps_epicor_po();
                    //if (obj.Value.Count > 0)
                    //    model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {

                        ps_epicor_po EpicorPO = new ps_epicor_po();
                        EpicorPO.Vendor_Company = obj.Value[i]["Vendor_Company"].ToString();
                        EpicorPO.Vendor_VendorID = obj.Value[i]["Vendor_VendorID"].ToString();
                        EpicorPO.Vendor_Name = obj.Value[i]["Vendor_Name"].ToString();
                        EpicorPO.POHeader_PONum = obj.Value[i]["POHeader_PONum"].ToString();
    
                        EpicorPO.POHeader_OrderDate = Convert.ToDateTime(obj.Value[i]["POHeader_OrderDate"].ToString() == "" ? "9999-12-30" : obj.Value[i]["POHeader_OrderDate"].ToString());
                        EpicorPO.PODetail_OpenLine = obj.Value[i]["PODetail_OpenLine"].ToString();
                        EpicorPO.PODetail_VoidLine = obj.Value[i]["PODetail_VoidLine"].ToString();
                        EpicorPO.PODetail_POLine = obj.Value[i]["PODetail_POLine"].ToString();


                        EpicorPO.PODetail_PartNum = obj.Value[i]["PODetail_PartNum"].ToString();
                        EpicorPO.PODetail_LineDesc = obj.Value[i]["PODetail_LineDesc"].ToString();
                        EpicorPO.PODetail_DocUnitCost = Convert .ToDecimal(obj.Value[i]["PODetail_DocUnitCost"].ToString());
                        EpicorPO.PODetail_OrderQty = Convert.ToDecimal(obj.Value[i]["PODetail_OrderQty"].ToString());
                        EpicorPO.PODetail_PUM = obj.Value[i]["PODetail_PUM"].ToString();
                        EpicorPO.PODetail_DocExtCost = Convert.ToDecimal(obj.Value[i]["PODetail_DocExtCost"].ToString());
                        EpicorPO.POHeader_CurrencyCode = obj.Value[i]["POHeader_CurrencyCode"].ToString();
                        

                        lsEpicorPO.Add(EpicorPO);


                        EpicorPO.Add();
                    }
                }
            }
            return lsEpicorPO.ToArray();

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }

    public ps_epicor_rfq[] GetEpicorRFQList(string VendorID)
    {
        try
        {
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/ud_SupPortalRFQ/Data";
            if (VendorID != "")
                RequestURL = RequestURL + "?% 24filter = Vendor_VendorID % 20eq % 20'" + VendorID + "'";
            //if (CustID != "")
            //    RequestURL = RequestURL + "?% 24filter = Customer_CustID % 20eq % 20'{Customer_CustID}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/CustomerCredit/Data?%24filter=Calculated_Part%20eq%20'{Calculated_Part}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            //RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            List<ps_epicor_rfq> lsEpicorRFQ = new List<ps_epicor_rfq> { };
            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_epicor_rfq model = new ps_epicor_rfq();
                    //if (obj.Value.Count > 0)
                    //    model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {
                        
                        ps_epicor_rfq EpicorRFQ = new ps_epicor_rfq();
                        EpicorRFQ.RFQHead_Company = obj.Value[i]["RFQHead_Company"].ToString();
                        EpicorRFQ.RFQHead_OpenRFQ = obj.Value[i]["RFQHead_OpenRFQ"].ToString();
                        EpicorRFQ.RFQHead_RFQNum = obj.Value[i]["RFQHead_RFQNum"].ToString();
                        EpicorRFQ.RFQHead_RFQDate = Convert.ToDateTime(obj.Value[i]["RFQHead_RFQDate"].ToString() == "" ? "9999-12-30" : obj.Value[i]["RFQHead_RFQDate"].ToString());
                        EpicorRFQ.RFQHead_RFQDueDate = Convert.ToDateTime(obj.Value[i]["RFQHead_RFQDueDate"].ToString() == "" ? "9999-12-30" : obj.Value[i]["RFQHead_RFQDueDate"].ToString());
                        EpicorRFQ.RFQItem_RFQLine = Convert.ToInt32(obj.Value[i]["RFQItem_RFQLine"].ToString());
                        EpicorRFQ.RFQItem_PartNum = obj.Value[i]["RFQItem_PartNum"].ToString();
                        EpicorRFQ.RFQItem_LineDesc = obj.Value[i]["RFQItem_LineDesc"].ToString();
                        EpicorRFQ.RFQItem_PUM = obj.Value[i]["RFQItem_PUM"].ToString();
                        EpicorRFQ.RFQQty_QtyNum = obj.Value[i]["RFQQty_QtyNum"].ToString();
                        EpicorRFQ.RFQQty_Quantity = Convert.ToDecimal(obj.Value[i]["RFQQty_Quantity"].ToString());
                        EpicorRFQ.Vendor_VendorID = obj.Value[i]["Vendor_VendorID"].ToString();
                        EpicorRFQ.Vendor_Name = obj.Value[i]["Vendor_Name"].ToString();
                        EpicorRFQ.RFQHead_PostToSupPortalTime_c = obj.Value[i]["RFQHead_PostToSupPortalTime_c"].ToString();

                        EpicorRFQ.Calculated_DueDateTime = obj.Value[i]["Calculated_DueDateTime"].ToString();
                        EpicorRFQ.RFQVend_ud_SPortalstatus_c = obj.Value[i]["RFQVend_ud_SPortalstatus_c"]==null ? "" : obj.Value[i]["RFQVend_ud_SPortalstatus_c"].ToString();
                        EpicorRFQ.RFQHead_Rptsubject_c = obj.Value[i]["RFQHead_RptSubject_c"] == null ? "" : obj.Value[i]["RFQHead_RptSubject_c"].ToString();
      


                        lsEpicorRFQ.Add(EpicorRFQ);


                        EpicorRFQ.Add(EpicorRFQ);
                        EpicorRFQ.UpdateRFQBySyncTime(EpicorRFQ);
                        
                    }
                }
            }
            return lsEpicorRFQ.ToArray();

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }

    

    public ps_epicor_rfq_head[] GetEpicorRFQRFHead()
    {
        try
        {
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/ud_SP_RFQ_Hed/Data";

            //if (CustID != "")
            //    RequestURL = RequestURL + "?% 24filter = Customer_CustID % 20eq % 20'{Customer_CustID}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/CustomerCredit/Data?%24filter=Calculated_Part%20eq%20'{Calculated_Part}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            //RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            List<ps_epicor_rfq_head> lsEpicorRFQHead = new List<ps_epicor_rfq_head> { };
            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_epicor_rfq_ans model = new ps_epicor_rfq_ans();
                    //if (obj.Value.Count > 0)
                    //    model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {
                        ps_epicor_rfq_head EpicorRFQHead = new ps_epicor_rfq_head();
                        EpicorRFQHead.RFQHead_Company = obj.Value[i]["RFQHead_Company"].ToString();
                        EpicorRFQHead.RFQHead_RFQNum = obj.Value[i]["RFQHead_RFQNum"].ToString();
                        EpicorRFQHead.RFQHead_OpenRFQ = obj.Value[i]["RFQHead_OpenRFQ"].ToString()=="true"?"1":"0";
                        EpicorRFQHead.RFQHead_PostDate = obj.Value[i]["RFQHead_RFQDate"] ==null ? null : Convert.ToDateTime(obj.Value[i]["RFQHead_RFQDate"].ToString());
                        EpicorRFQHead.RFQHead_RFQDueDate = obj.Value[i]["RFQHead_RFQDueDate"] == null ? null : Convert.ToDateTime(obj.Value[i]["RFQHead_RFQDueDate"].ToString());
                        EpicorRFQHead.RFQHead_DueTime_c = obj.Value[i]["Calculated_DueTime"].ToString();
                        EpicorRFQHead.RFQHead_RptSubject_c = obj.Value[i]["RFQHead_RptSubject_c"].ToString();
                        EpicorRFQHead.RFQHead_CommentText = obj.Value[i]["RFQHead_CommentText"].ToString();
                      

                        lsEpicorRFQHead.Add(EpicorRFQHead);


                        EpicorRFQHead.Add(EpicorRFQHead);
                        EpicorRFQHead.Update(EpicorRFQHead);
                        
                    }
                }
            }
            return lsEpicorRFQHead.ToArray();

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }

    public ps_epicor_rfq_ans[] GetEpicorRFQRFQANS(string RFQNumber, string VendorID)
    {
        try
        {
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/ud_SP_RFQ_ANS/Data?%24filter=UD34_Key1%20eq%20'" + RFQNumber + "'&%UD34_Key3%20eq%20'" + VendorID + "'";
            //if (CustID != "")
            //    RequestURL = RequestURL + "?% 24filter = Customer_CustID % 20eq % 20'{Customer_CustID}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/CustomerCredit/Data?%24filter=Calculated_Part%20eq%20'{Calculated_Part}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            //RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            List<ps_epicor_rfq_ans> lsEpicorRFQANS = new List<ps_epicor_rfq_ans> { };
            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_epicor_rfq_ans model = new ps_epicor_rfq_ans();
                    //if (obj.Value.Count > 0)
                    //    model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {

                        ps_epicor_rfq_ans EpicorRFQANS = new ps_epicor_rfq_ans();
                        EpicorRFQANS.UD34_Company = obj.Value[i]["UD34_Company"].ToString();
                        EpicorRFQANS.UD34_Key1 = obj.Value[i]["UD34_Key1"].ToString();
                        EpicorRFQANS.UD34_Key2 = obj.Value[i]["UD34_Key2"].ToString();

                        EpicorRFQANS.UD34_Key3 = obj.Value[i]["UD34_Key3"].ToString();
                        EpicorRFQANS.UD34_Key4 = obj.Value[i]["UD34_Key4"].ToString();
                        EpicorRFQANS.UD34_Key5 = obj.Value[i]["UD34_Key5"].ToString();
                        EpicorRFQANS.UD34_RFQQNA_SeqNum_c = obj.Value[i]["UD34_RFQQNA_SeqNum_c"].ToString();
                        EpicorRFQANS.UD34_RFQQNA_Question_c = obj.Value[i]["UD34_RFQQNA_Question_c"].ToString();
                        EpicorRFQANS.UD34_RFQQNA_Answer_c = obj.Value[i]["UD34_RFQQNA_Answer_c"].ToString();


                        lsEpicorRFQANS.Add(EpicorRFQANS);


                        EpicorRFQANS.Add(EpicorRFQANS);
                    }
                }
            }
            return lsEpicorRFQANS.ToArray();

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }
    
    public ps_epicor_rfq_vendorprice[] GetEpicorRFQVendPrice(string RFQNumber,string VendorID)
    {
        try
        {
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/ud_SP_RFQ_VendPrice/Data?%24filter=UD37_RFQVP_RFQNum_c%20eq%20"+ RFQNumber + "&%20UD37_RFQVP_VendorID_c%20eq%20'" + VendorID + "'"; 
             //if (CustID != "")
             //    RequestURL = RequestURL + "?% 24filter = Customer_CustID % 20eq % 20'{Customer_CustID}'";
             //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/CustomerCredit/Data?%24filter=Calculated_Part%20eq%20'{Calculated_Part}'";
             //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
             DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            //RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            List<ps_epicor_rfq_vendorprice> lsEpicorRFQVendorPrice = new List<ps_epicor_rfq_vendorprice> { };
            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_epicor_rfq model = new ps_epicor_rfq();
                    //if (obj.Value.Count > 0)
                    //    model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {

                        ps_epicor_rfq_vendorprice EpicorRFQVendorPrice = new ps_epicor_rfq_vendorprice();
                        EpicorRFQVendorPrice.UD37_Company = obj.Value[i]["UD37_Company"].ToString();
                        EpicorRFQVendorPrice.UD37_Key1 = obj.Value[i]["UD37_Key1"].ToString();
                        EpicorRFQVendorPrice.UD37_Key2 = obj.Value[i]["UD37_Key2"].ToString();
                        EpicorRFQVendorPrice.UD37_Key3 = obj.Value[i]["UD37_Key3"].ToString();
                        EpicorRFQVendorPrice.UD37_Key4 = obj.Value[i]["UD37_Key4"].ToString();
                        EpicorRFQVendorPrice.UD37_Key5 = obj.Value[i]["UD37_Key5"].ToString();
                        EpicorRFQVendorPrice.UD37_RFQVP_RFQNum_c = obj.Value[i]["UD37_RFQVP_RFQNum_c"].ToString();
                        EpicorRFQVendorPrice.UD37_RFQVP_VendorID_c = obj.Value[i]["UD37_RFQVP_VendorID_c"].ToString();
                        EpicorRFQVendorPrice.UD37_RFQVP_VendorName_c = obj.Value[i]["UD37_RFQVP_VendorName_c"].ToString();
                        EpicorRFQVendorPrice.UD37_RFQVP_RFQLine_c = Convert.ToInt32(obj.Value[i]["UD37_RFQVP_RFQLine_c"].ToString());
                        EpicorRFQVendorPrice.UD37_RFQVP_RFQQtyNum_c = obj.Value[i]["UD37_RFQVP_RFQQtyNum_c"].ToString();

                        EpicorRFQVendorPrice.UD37_RFQVP_PartNum_c = obj.Value[i]["UD37_RFQVP_PartNum_c"].ToString();
                        EpicorRFQVendorPrice.UD37_RFQVP_PartDesc_c = obj.Value[i]["UD37_RFQVP_PartDesc_c"].ToString();

                        EpicorRFQVendorPrice.UD37_RFQVP_Qty_c = Convert.ToDecimal(obj.Value[i]["UD37_RFQVP_Qty_c"].ToString());

                        EpicorRFQVendorPrice.UD37_RFQVP_PartNum_c = obj.Value[i]["UD37_RFQVP_PartNum_c"].ToString();
                        EpicorRFQVendorPrice.UD37_RFQVP_PartDesc_c = obj.Value[i]["UD37_RFQVP_PartDesc_c"].ToString();

                        EpicorRFQVendorPrice.UD37_RFQVP_PUM_c = obj.Value[i]["UD37_RFQVP_PUM_c"].ToString();
                        EpicorRFQVendorPrice.UD37_RFQVP_CurrencyCode_c = obj.Value[i]["UD37_RFQVP_CurrencyCode_c"].ToString();

                        EpicorRFQVendorPrice.UD37_RFQVP_OfferedPrice_c = AesEncryption.Encrypt(obj.Value[i]["UD37_RFQVP_OfferedPrice_c"].ToString());
                        EpicorRFQVendorPrice.UD37_RFQVP_NegotiatedPrice_c = Convert.ToDecimal(obj.Value[i]["UD37_RFQVP_NegotiatedPrice_c"].ToString());


                        EpicorRFQVendorPrice.UD37_RFQVP_ExchangeRate_c = Convert.ToDecimal(obj.Value[i]["UD37_RFQVP_ExchangeRate_c"].ToString());
                        EpicorRFQVendorPrice.UD37_RFQVP_BaseUnitPrice_c = Convert.ToDecimal(obj.Value[i]["UD37_RFQVP_BaseUnitPrice_c"].ToString());
          
              

                        EpicorRFQVendorPrice.PartClass_Description = obj.Value[i]["PartClass_Description"].ToString();
                        EpicorRFQVendorPrice.UD37_RFQVP_Remark_c = obj.Value[i]["UD37_RFQVP_Remark_c"].ToString();

                        EpicorRFQVendorPrice.UD37_RFQVP_Qty_c = Convert.ToDecimal(obj.Value[i]["UD37_RFQVP_Qty_c"].ToString());
                        EpicorRFQVendorPrice.UD37_RFQVP_Status_c = obj.Value[i]["UD37_RFQVP_Status_c"].ToString()==""?"0": obj.Value[i]["UD37_RFQVP_Status_c"].ToString();

                        EpicorRFQVendorPrice.RFQItem_DueDate_c = obj.Value[i]["RFQItem_DueDate_c"].ToString();
                    

                        lsEpicorRFQVendorPrice.Add(EpicorRFQVendorPrice);


                        EpicorRFQVendorPrice.Add(EpicorRFQVendorPrice);
                    }
                }
            }
            return lsEpicorRFQVendorPrice.ToArray();

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }


    public class RFQVend
    {
        public string Company { get; set; }
        public int RFQNum { get; set; }
        public int RFQLine { get; set; }
        public int VendorNum { get; set; }
        public bool ud_VendSubmitted_c { get; set; }
        public string ud_SPortalStatus_c { get; set; }
        public DateTime ud_SPortalStatusUpdateTime_c { get; set; }
    }
    public string UpdateEpicorRFQVendStatus(string RFQNumber,string VendorID,int IsSunit)
    {
        ps_epicor_rfq_vendorprice bll = new ps_epicor_rfq_vendorprice();
        DataSet dsVendorPrice = bll.GetList2(" UD37_RFQVP_RFQNum_c='" + RFQNumber + "' and UD37_RFQVP_VendorID_c='" + VendorID + "' ");
        
        DataSet ds = GetList("");
        if (ds.Tables[0].Rows.Count > 0)
        {
            UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
            APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
            ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
            AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
            AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            try
            {

                for (int i = 0; i < dsVendorPrice.Tables[0].Rows.Count; i++)
                {

                    RFQVend entry = new RFQVend();
                    entry.Company = dsVendorPrice.Tables[0].Rows[i]["UD37_Company"].ToString();
                    entry.RFQNum = Convert.ToInt32(dsVendorPrice.Tables[0].Rows[i]["UD37_RFQVP_RFQNum_c"].ToString());
                    entry.RFQLine = Convert.ToInt32(dsVendorPrice.Tables[0].Rows[i]["UD37_RFQVP_RFQLine_c"].ToString());
                    entry.VendorNum = Convert.ToInt32(dsVendorPrice.Tables[0].Rows[i]["UD37_RFQVP_VendorNum_c"].ToString());
                    if (IsSunit == 1)//submit
                    {
                        entry.ud_VendSubmitted_c = true;
                        entry.ud_SPortalStatus_c = "Submitted";
                    }
                    else//recall
                    {
                        entry.ud_VendSubmitted_c = false;
                        entry.ud_SPortalStatus_c = "Recalled";
                    }
                    entry.ud_SPortalStatusUpdateTime_c = System.DateTime.Now;



                    string isoJson = JsonConvert.SerializeObject(entry);    //Convert DataEntry to Json string

                    isoJson = isoJson.Replace("\r\n", "");
                    isoJson = isoJson.Replace("\"0001-01-01T00:00:00Z\"", "null");
                    isoJson = isoJson.Replace("00:00:00", "00:00:00Z");

                    //final RequestURL
                    string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.RFQVendSvc/RFQVends";
                    RequestURL = RequestURL.Replace("{ServerName}", ServerName);
                    RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
                    RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);



                    string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
                    HTTPMethods = "POST";
                    HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);

                    string strHTTPStatusCode = ResponseStatusCode;
                    string strResponseBody = ResponseBody;
                    string strExceptionMsg = ExceptionMsg;
                    string strTDeserializeResponseBodyErrorMessage = ErrorMessage;

                    if (ResponseStatusCode == "201")
                    {
                        //JavaScriptSerializer jss = new JavaScriptSerializer();
                        //反序列化成Part对象
                        //PartData partData = jss.Deserialize<PartData>(ResponseBody);
                        //dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                        //Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                        //List<PartData> lsPartDatas = new List<PartData> { };
                        //foreach (var obj in dyn)
                        //{
                        //    if (obj.Name == "OrderNum")
                        //    {
                        //        EpicorOrderNumber = obj.Value;
                        //        break;
                        //    }
                        //}
                    }
                    ////写入登录日志
                    ps_manager_log mylog = new ps_manager_log();
                    mylog.user_id = 1;
                    mylog.user_name = "API";
                    mylog.action_type = "Epcior";
                    mylog.add_time = DateTime.Now;
                    mylog.remark = "Update RFQVend(RFQ Num:" + entry.RFQNum  + "-"+ entry.RFQLine +")";
                    mylog.user_ip = AXRequest.GetIP();
                    mylog.Add();
                    
                }
                return RFQNumber;

            }
            catch (AggregateException ex)
            {
                string strExceptionMsg = ex.ToString();
                return "Error:" + strExceptionMsg;
            }
        }
        else
            return "Error";
    }



    public string GetEpicorSupplierNotice(string vendorid)
    {
        try
        {
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/ud_SP_Notice/Data?%24filter=Vendor_VendorID%20eq%20'" + vendorid + "' ";
            //if (vendorid != "")
            //    RequestURL = RequestURL + "?%24filter=Vendor_VendorID%20eq%20'" + vendorid + "' ";
            //if (CustID != "")
            //    RequestURL = RequestURL + "?% 24filter = Customer_CustID % 20eq % 20'{Customer_CustID}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/CustomerCredit/Data?%24filter=Calculated_Part%20eq%20'{Calculated_Part}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            //RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);

 
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return "";
            string strPortalNotice = "";


            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_epicor_vendor model = new ps_epicor_vendor();
                    //if (obj.Value.Count > 0)
                    //    model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {
                        strPortalNotice = obj.Value[i]["Calculated_Global_Notices"].ToString() + "vvvvvvvvvv" + obj.Value[i]["Calculated_Vendor_Notices"].ToString();
                        break;
                    }
                }
            }
            return strPortalNotice;

        }
        catch (AggregateException ex)
        {
            return "";
        }
    }



    public string GetEpicorRFQCount(string vendorid)
    {
        try
        {
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/ud_SP_RFQCount/Data?%24filter=Vendor_VendorID%20eq%20'" + vendorid + "' ";
            //if (vendorid != "")
            //    RequestURL = RequestURL + "?%24filter=Vendor_VendorID%20eq%20'" + vendorid + "' ";
            //if (CustID != "")
            //    RequestURL = RequestURL + "?% 24filter = Customer_CustID % 20eq % 20'{Customer_CustID}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/CustomerCredit/Data?%24filter=Calculated_Part%20eq%20'{Calculated_Part}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            //RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return "";
            string strCalculatedRFQ = "";


            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_epicor_vendor model = new ps_epicor_vendor();
                    //if (obj.Value.Count > 0)
                    //    model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {
                        strCalculatedRFQ = obj.Value[i]["Calculated_RepliedRFQ"].ToString() + "vvvvvvvvvv" + obj.Value[i]["Calculated_WaitForReply"].ToString();
                        break;
                    }
                }
            }
            return strCalculatedRFQ;

        }
        catch (AggregateException ex)
        {
            return "";
        }
    }

    public string GetEpicorPOCount(string vendorid)
    {
        try
        {
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/ud_SP_POCount/Data?%24filter=Vendor_VendorID%20eq%20'" + vendorid + "' ";
            //if (vendorid != "")
            //    RequestURL = RequestURL + "?%24filter=Vendor_VendorID%20eq%20'" + vendorid + "' ";
            //if (CustID != "")
            //    RequestURL = RequestURL + "?% 24filter = Customer_CustID % 20eq % 20'{Customer_CustID}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/CustomerCredit/Data?%24filter=Calculated_Part%20eq%20'{Calculated_Part}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            //RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return "";
            string strCalculatedPO = "";


            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    ps_epicor_vendor model = new ps_epicor_vendor();
                    //if (obj.Value.Count > 0)
                    //    model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {
                        strCalculatedPO = obj.Value[i]["Calculated_OpenPO"].ToString() + "vvvvvvvvvv" + obj.Value[i]["Calculated_ClosedPO"].ToString();
                        break;
                    }
                }
            }
            return strCalculatedPO;

        }
        catch (AggregateException ex)
        {
            return "";
        }
    }


    public string[] GetEpicorRFQAttachment(string RFQNum)
    {
        try
        {
            //final RequestURL
            string RequestURL = "";
            RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/ud_SP_RFQ_Attch/Data";
            if (RFQNum != "")
                RequestURL = RequestURL + "?%24filter=XFileAttch_Key1%20eq%20'" + RFQNum + "' ";
            //if (CustID != "")
            //    RequestURL = RequestURL + "?% 24filter = Customer_CustID % 20eq % 20'{Customer_CustID}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/BaqSvc/CustomerCredit/Data?%24filter=Calculated_Part%20eq%20'{Calculated_Part}'";
            //string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Erp.BO.PartSvc/Parts?$filter=Company%20eq%20'{Company}'";
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
            }


            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);
            //RequestURL = RequestURL.Replace("{Customer_CustID}", CustID);



            string isoJson = "";
            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "GET";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            ////反序列化成Part对象
            //PartData partData = jss.Deserialize<PartData>(ResponseBody);
            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
            if (dyn == null) return null;
            string XFile = "";
            List<string> XFileArr = new List<string>();


            foreach (var obj in dyn)
            {
                if (obj.Name == "value")
                {
                    //ErrorMessage = Convert.ToString(dyn.Last.Value);
                    //JavaScriptSerializer jss = new JavaScriptSerializer();
                    //反序列化成Part对象
                    //ValueItem partData = jss.Deserialize<ValueItem>(obj.Value[10]);
                    
                    //if (obj.Value.Count > 0)
                    //    model.DeleteAll();
                    for (int i = 0; i < obj.Value.Count; i++)
                    {
                        XFile = obj.Value[i]["XFileAttch_Key1"].ToString() + "vvvvvvvvvv" + obj.Value[i]["XFileAttch_XFileRefNum"].ToString() + "vvvvvvvvvv" + obj.Value[i]["XFileRef_XFileName"].ToString();
                        XFileArr.Add(XFile);
                    }
                }
            }
            return XFileArr.ToArray();

        }
        catch (AggregateException ex)
        {
            return null;
        }
    }


    public class FileRefNum
    {
        public string xFileRefNum { get; set; }
    }

    public string[] GetEpicorRFQAttachmentBase64(string xFileRefNum)
    {
        try
        {
            //final RequestURL
            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserAndPw = ds.Tables[0].Rows[0]["UserAndPw"].ToString();
                APIKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                ServerName = ds.Tables[0].Rows[0]["ServerName"].ToString();
                AppServerName = ds.Tables[0].Rows[0]["AppServerName"].ToString();
                AppCompany = ds.Tables[0].Rows[0]["Company"].ToString();
                License = ds.Tables[0].Rows[0]["License"].ToString();
            }
            string RequestURL = "https://{ServerName}/{EpicorAppServerName}/api/v2/odata/{currentCompany}/Ice.BO.AttachmentSvc/DownloadFile";
            RequestURL = RequestURL.Replace("{ServerName}", ServerName);
            RequestURL = RequestURL.Replace("{EpicorAppServerName}", AppServerName);
            RequestURL = RequestURL.Replace("{currentCompany}", (AppCompany=="" || AppCompany == "ALL") ? "EPIC06" : AppCompany);

            FileRefNum entry = new FileRefNum
            {
                xFileRefNum = xFileRefNum,
            };


            string isoJson = JsonConvert.SerializeObject(entry);    //Convert DataEntry to Json string

            isoJson = isoJson.Replace("\r\n", "");
            isoJson = isoJson.Replace("\"0001-01-01T00:00:00Z\"", "null");
            isoJson = isoJson.Replace("00:00:00", "00:00:00Z");


            string HTTPMethods = "", ResponseStatusCode = "", ResponseBody = "", IsSuccessStatusCode = "", ErrorMessage = "", ExceptionMsg = "";
            HTTPMethods = "POST";
            HttpSendRequest(HTTPMethods, RequestURL, UserAndPw, APIKey, isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);
            //HttpSendRequest2(HTTPMethods, RequestURL, UserAndPw, APIKey, License,isoJson, ref ResponseStatusCode, ref ResponseBody, ref IsSuccessStatusCode, ref ErrorMessage, ref ExceptionMsg);


            string strHTTPStatusCode = ResponseStatusCode;
            string strResponseBody = ResponseBody;
            string strExceptionMsg = ExceptionMsg;
            string strTDeserializeResponseBodyErrorMessage = ErrorMessage;

            if (ResponseStatusCode == "200")
            {

                //JavaScriptSerializer jss = new JavaScriptSerializer();

                dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(ResponseBody);
                if (dyn == null) return null;
                List<string> lsBase64list = new List<string> { };
                foreach (var obj in dyn)
                {
                    if (obj.Name == "returnObj")
                    {
                        
                        string base64str = obj.Value;
                        lsBase64list.Add(base64str);
                    }
                }
                return lsBase64list.ToArray();
            }
            else
            {
                return null;
            }
        }
        catch (AggregateException ex)
        {
            return null;
        }
    }

      


}
