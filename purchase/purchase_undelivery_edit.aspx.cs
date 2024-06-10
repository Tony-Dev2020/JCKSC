using System;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using LitJson;
using System.IO;

public partial class purchase_purchase_undelivery_edit : System.Web.UI.Page
{
    ManagePage mym = new ManagePage();
    protected int totalCount;
    protected int page;
    protected int pageSize;
    protected void Page_Load(object sender, EventArgs e)
    {

        //判断是否登录
        if (!mym.IsAdminLogin())
        {
            Response.Write("<script>parent.location.href='../index.aspx'</script>");
            Response.End();
        }
        //判断权限
        ps_manager_role_value myrv = new ps_manager_role_value();
        int role_id = Convert.ToInt32(Session["RoleID"]);
        int nav_id = 44;
        if (!myrv.QXExists(role_id, nav_id))
        {
            Response.Redirect("../error.html");
            Response.End();
        }

        if (!Page.IsPostBack)
        {
            int vendor_id = Convert.ToInt32(Session["VendorID"]);
            //获得供应商信息
            if (vendor_id > 0)
            {

                ps_vendor vendor_info = new ps_vendor();
                vendor_info.GetModel(Convert.ToInt32(vendor_id));
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Error", "alert( '此用户没有绑定供应商信息，不能下单！');window.location='purchase_undelivery_list.aspx'", true);
                //return;
            }
            RptBind();
        }
    }


    #region 数据绑定=================================
    private void RptBind()
    {
        //ShopCart bll = new ShopCart();
        //IList<cart_items> ls1 = bll.get_cart_list();
        //decimal totalAmount = 0;
        //for (int i = 0; i < ls1.Count; i++)
        //{
        //    EpicorRequest epicorRequest = new EpicorRequest();
        //    ps_depot bllCustomer = new ps_depot();
        //    if (Session["DepotID"].ToString() != "")
        //    {
        //        bllCustomer.GetModel(Convert.ToInt32(Session["DepotID"]));
        //    }
        //    //ps_depot_category bllCompany = new ps_depot_category();
        //    //if (Session["DepotID"].ToString() != "")
        //    //{
        //    //    bllCustomer.GetModel(Convert.ToInt32(Session["DepotID"]));
        //    //}

        //    EpicorRequest.PartPricelist[] PartPricelist = epicorRequest.GetPartPricelist(bllCustomer.company, bllCustomer.code, ls1[i].product_no, ls1[i].quantity, ls1[i].dw);
        //    if (PartPricelist != null && PartPricelist.Length > 0)
        //    {
        //        ls1[i].price = Convert.ToDecimal(PartPricelist[0].NetPrice);
        //        ls1[i].discount = Convert.ToDecimal(PartPricelist[0].DiscountPercent);
        //    }

        //    totalAmount = totalAmount + (ls1[i].price * ls1[i].quantity * (ls1[i].discount == null ? 100 : (100 - ls1[i].discount))) / 100;
        //    foreach (RepeaterItem rptitem in this.rptList.Items)
        //    {
        //        TextBox txtProductNo = rptitem.FindControl("txtProductNo") as TextBox;
        //        if (ls1[i].product_no == txtProductNo.Text)
        //        {
        //            TextBox txtGoodsRemarks = rptitem.FindControl("txtGoodsRemarks") as TextBox;
        //            ls1[i].remark = txtGoodsRemarks.Text;

        //            TextBox hfKitNum = rptitem.FindControl("kit_num") as TextBox;
        //            TextBox hfKitDesc = rptitem.FindControl("kit_desc") as TextBox;
        //            ls1[i].kit_num = hfKitNum.Text;
        //            ls1[i].kit_desc = hfKitNum.Text;

        //            //TextBox txtPrice = rptitem.FindControl("txtPrice") as TextBox;
        //            //TextBox txtDiscount = rptitem.FindControl("txtDiscount") as TextBox;
        //            //ls1[i].price = Convert.ToDecimal(txtPrice.Text);
        //            //ls1[i].discount = Convert.ToDecimal(txtDiscount.Text);
        //        }
        //    }
        //}
        ps_podetail bll = new ps_podetail();
        this.rptList.DataSource = bll.GetList(10, this.page, " id<5 ", " id ", out this.totalCount);
        this.rptList.DataBind();
        cart_total cartModel = ShopCart.GetTotal();
        total_quantity.Text = cartModel.total_quantity.ToString();
        //payable_amount.Text = cartModel.payable_amount.ToString();
        //payable_amount1.Text = cartModel.payable_amount.ToString();
        //txtOrderDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        txtNeedDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
    }
    #endregion

  


    //小数位是0的不显示
    public string MyConvert(object d)
    {
        string myNum = d.ToString();
        string[] strs = d.ToString().Split('.');
        if (strs.Length > 1)
        {
            if (Convert.ToInt32(strs[1]) == 0)
            {
                myNum = strs[0];
            }
        }
        return myNum;
    }

    //负数红色显示
    public string MyZF(object d)
    {
        string myNum = d.ToString();
        if (Convert.ToInt32(d.ToString()) <= 0)
        {
            myNum = "<font color=red> " + d.ToString() + "</font>";
        }
        return myNum;
    }

    //提交订单
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int user_id = 0;
        int depot_category_id = 0;
        int depot_id = 0;
        string user_name = string.Empty;

        user_id = Convert.ToInt32(Session["AID"]);
        depot_category_id = Convert.ToInt32(Session["DepotCatID"]);
        depot_id = Convert.ToInt32(Session["DepotID"]);
        user_name = Session["AdminName"].ToString();
        FileUplaod();



        //检查购物车商品
        IList<cart_items> iList = ShopCart.GetList();
        if (iList == null)
        {
            mym.JscriptMsg(this.Page, "对不起，购物车为空，无法结算！", "", "Error");
            return;
        }

        //统计购物车
        cart_total cartModel = ShopCart.GetTotal();
        //判断是否有商品
        if (cartModel.total_quantity == 0)
        {
            mym.JscriptMsg(this.Page, "对不起，购物车为空，无法结算！", "", "Error");
            return;
        }

        //保存订单=======================================================================
        ps_orders model = new ps_orders();
        model.order_no = Utils.GetOrderNumber(); //订单号B开头为商品订单
        model.user_id = user_id;
        model.depot_category_id = depot_category_id;
        model.depot_id = depot_id;
        model.user_name = user_name;
        model.payment_id = 1;//未支付
        model.message = message.Text;
        model.payable_amount = cartModel.payable_amount;
        model.real_amount = 0;
        model.order_date = Convert.ToDateTime(txtOrderDate.Text);
        model.need_date = Convert.ToDateTime(txtNeedDate.Text);
        //model.provice = ddlProvice.SelectedItem.Text;
        //model.city = ddlCity.SelectedItem.Text;
        //model.area = ddlArea.SelectedItem.Text;
        model.provice = "";
        model.city = "";
        model.area = "";
        model.attachment1 = txtFile1Name.Value;
        model.attachment2 = txtFile2Name.Value;
        model.attachment3 = txtFile3Name.Value;
        model.attachment4 = txtFile4Name.Value;




        //订单总金额=实付商品金额
        model.order_amount = cartModel.payable_amount;
        model.add_time = DateTime.Now;


        if (model.Add() == 0)
        {
            mym.JscriptMsg(this.Page, "订单保存过程中发生错误，请重新提交！", "", "Error");
            return;
        }
        //商品详细列表
        ps_order_goods gls = new ps_order_goods();
        ps_here_depot my = new ps_here_depot();
        foreach (cart_items item in iList)
        {
            my.GetModel(item.id);
            gls.order_id = model.GetMaxId();
            gls.goods_id = item.id;
            gls.product_no = item.product_no;
            gls.goods_title = item.title;
            gls.goods_price = my.go_price;
            gls.real_price = item.price;
            gls.quantity = item.quantity;
            gls.product_category_id = item.product_category_id;
            gls.dw = item.dw;
            foreach (RepeaterItem rptitem in this.rptList.Items)
            {
                TextBox txtProductNo = rptitem.FindControl("txtProductNo") as TextBox;
                if (item.product_no == txtProductNo.Text)
                {
                    TextBox txtGoodsRemarks = rptitem.FindControl("txtGoodsRemarks") as TextBox;
                    gls.remarks = txtGoodsRemarks.Text;

                    TextBox hfKitNum = rptitem.FindControl("kit_num") as TextBox;
                    TextBox hfKitDesc = rptitem.FindControl("kit_desc") as TextBox;
                    gls.kit_num = hfKitNum.Text;
                    gls.kit_desc = hfKitNum.Text;

                    TextBox txtCustSize = rptitem.FindControl("txtCustSize") as TextBox;
                    gls.custsize = txtCustSize.Text;

                    TextBox txtPrice = rptitem.FindControl("txtPrice") as TextBox;
                    TextBox txtDiscount = rptitem.FindControl("txtDiscount") as TextBox;
                    gls.real_price = Convert.ToDecimal(txtPrice.Text);
                    gls.goods_price = Convert.ToDecimal(txtPrice.Text);
                    gls.discount = Convert.ToDecimal(txtDiscount.Text);
                }
            }


            gls.Add();
        }
        model.UpdateOrderAmount(-1);
        //清空购物车
        ShopCart.Clear("0");

        //写入登录日志
        ps_manager_log mylog = new ps_manager_log();
        mylog.user_id = user_id;
        mylog.user_name = user_name;
        mylog.action_type = "订单";
        mylog.add_time = DateTime.Now;
        mylog.remark = "新增订单(订单号:" + model.order_no + ")";
        mylog.user_ip = AXRequest.GetIP();
        mylog.Add();

        //提交成功，返回URL
        //mym.JscriptMsg(this.Page, "恭喜您，订单已成功提交！", "my_order.aspx", "Success");
        ScriptManager.RegisterStartupScript(this, typeof(string), "Success", "alert( '恭喜您，订单已成功提交！');window.location='my_order.aspx'", true);
        return;
    }


    #region///上传，文件名称添加数据库，文件保存相应路径
    /// <summary>
    /// 添加附件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FileUplaod()
    {
        string fileName1 = FileUpload1.FileName;//获取要导入的文件名 
        if (fileName1 != null && fileName1 != "")
        {
            string savePath = Server.MapPath("~/upload/files/");
            string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            FileOperatpr(fileName1, savePath);
            string url = savePath + formatdate + "-" + fileName1;
            txtFile1Name.Value = formatdate + "-" + fileName1;
            FileUpload1.SaveAs(url);
        }
        string fileName2 = FileUpload2.FileName;//获取要导入的文件名 
        if (fileName2 != null && fileName2 != "")
        {
            string savePath = Server.MapPath("~/upload/files/");
            string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            FileOperatpr(fileName2, savePath);
            string url = savePath + formatdate + "-" + fileName2;
            txtFile2Name.Value = formatdate + "-" + fileName2;
            FileUpload2.SaveAs(url);
        }
        string fileName3 = FileUpload3.FileName;//获取要导入的文件名 
        if (fileName3 != null && fileName3 != "")
        {
            string savePath = Server.MapPath("~/upload/files/");
            string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            FileOperatpr(fileName3, savePath);
            string url = savePath + formatdate + "-" + fileName3;
            txtFile3Name.Value = formatdate + "-" + fileName3;
            FileUpload3.SaveAs(url);
        }
        string fileName4 = FileUpload4.FileName;//获取要导入的文件名 
        if (fileName4 != null && fileName4 != "")
        {
            string savePath = Server.MapPath("~/upload/files/");
            string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            FileOperatpr(fileName4, savePath);
            string url = savePath + formatdate + "-" + fileName4;
            txtFile4Name.Value = formatdate + "-" + fileName4;
            FileUpload4.SaveAs(url);
        }
    }
    #region 辅助功能
    /// <summary>
    /// 获取上传文件的后缀名
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private string getFileEND(string fileName)
    {
        if (fileName.IndexOf(".") == -1)
            return "";
        string[] temp = fileName.Split('.');
        return temp[temp.Length - 1].ToLower();
    }
    /// <summary>
    /// //获取上传文件的名称
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private string getFileExt(string fileName)
    {
        if (fileName.IndexOf(".") == -1)
            return "";
        string file = "";
        string[] temp = fileName.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
        file = temp[0].ToLower();
        return file.ToLower();
    }


    private void FileOperatpr(string fileName, string savePath)
    {
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        if (File.Exists(savePath + fileName))
        {
            File.Delete(savePath + fileName);
        }
    }

    #endregion
    #endregion
}