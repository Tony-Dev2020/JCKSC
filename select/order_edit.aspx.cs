using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class select_order_edit : System.Web.UI.Page
{
    protected int page;
    private string action = ""; //操作类型
    public string isSearch = ""; //操作类型
    public string status = ""; 
    private int id = 0;
    private int orderstatus = 0;
    ManagePage mym = new ManagePage();
    protected ps_orders model = new ps_orders();
    protected ps_order_goods bllOrderGoods = new ps_order_goods();

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
        string _action = AXRequest.GetQueryString("action");
        string _isSearch = AXRequest.GetQueryString("IsSearch");
        this.status = AXRequest.GetQueryString("status");
        this.page = AXRequest.GetQueryInt("page", 1);
        this.isSearch = _isSearch;
        int role_id = Convert.ToInt32(Session["RoleID"]);
        int nav_id = 16;
        if (this.status == "2")
            nav_id = 49;
        if (this.status == "3")
            nav_id = 50;
        if (this.status == "4")
            nav_id = 51;
        if (this.status == "5")
            nav_id = 52;
        if (this.status == "6")
            nav_id = 53;
        if (this.status == "7")
            nav_id = 54;
        if (_isSearch != "1")
        {
            if (!myrv.QXExists(role_id, nav_id))
            {
                Response.Redirect("../error.html");
                Response.End();
            }
        }

        if (!string.IsNullOrEmpty(_action) && _action == "Edit")
        {
            this.action = "Edit";//修改类型
            if (!int.TryParse(Request.QueryString["id"] as string, out this.id))
            {
                mym.JscriptMsg(this.Page, "传输参数不正确！", "back", "Error");
                return;
            }

        }
        if (!Page.IsPostBack)
        {
            if (action == "Edit") //修改
            {
                ShowInfo(this.id);
            }
        }

    }

    #region 赋值操作=================================
    private void ShowInfo(int _id)
    {
        double totalAmount = 0;
        double totalRealAmount = 0;
        double totalDiscountAmount = 0;
        
        model.GetModel(_id);
        orderstatus = model.status==1 ? 1 : 0;
        //绑定商品列表
        ps_order_goods bll = new ps_order_goods();
        string sql = " order_id =" + _id;

        DataTable dt = bll.GetList(sql).Tables[0];
        this.rptList.DataSource = dt;
        this.rptList.DataBind();
        
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            totalAmount = totalAmount + Convert.ToDouble(dt.Rows[i]["real_price"]) * Convert.ToDouble(dt.Rows[i]["quantity"]);
            //totalRealAmount = totalRealAmount + Convert.ToDouble(dt.Rows[i]["real_price"]) * (Convert.ToDouble(dt.Rows[i]["quantity"]) * ( Convert.ToDouble(dt.Rows[i]["discount"].ToString() == "" ? "0" : dt.Rows[i]["discount"].ToString()))) / 100;
            //totalDiscountAmount = totalDiscountAmount + Convert.ToDouble(dt.Rows[i]["real_price"]) * (Convert.ToDouble(dt.Rows[i]["quantity"]) * (100 - Convert.ToDouble(dt.Rows[i]["discount"].ToString() == "" ? "0" : dt.Rows[i]["discount"].ToString()))) / 100;
            totalRealAmount = totalRealAmount + Convert.ToDouble(dt.Rows[i]["payamount"]);
            totalDiscountAmount = totalDiscountAmount + Convert.ToDouble(dt.Rows[i]["discountamount"]);
        }

        //if (txtTotalAmount.Text != "" && txtTotalAmount.Text != "0")
        //{
        //    txtTotalAmount.Text = String.Format("{0:N}", MyConvert(totalAmount));
        //}
        //if (txtTotalDiscountAmount.Text != "" && txtTotalDiscountAmount.Text != "0")
        //{
        //    txtTotalDiscountAmount.Text = String.Format("{0:N}", MyConvert(totalDiscountAmount));
        //}
        //if (txtTotalRealAmount.Text != "" && txtTotalRealAmount.Text != "0")
        //{
        //    txtTotalRealAmount.Text = String.Format("{0:N}", MyConvert(totalRealAmount));
        //}
        this.txtPaymentRation.Text = model.payration==0 ? "100": model.payration.ToString();
        this.txtPaymentRation.Enabled = this.txtPaymentRation.Visible = model.status == 1 || model.status == 4;

        this.lbPaymentRation.Text = model.payration == 0 ? "100" : model.payration.ToString();
        this.lbPaymentRation.Visible = model.status != 1 && model.status != 4;


        if (Session["IsDisplayPrice"] == null || Session["IsDisplayPrice"].ToString() == "1")
        {
            txtTotalAmount.Text = String.Format("{0:N}", MyConvert(totalAmount));
            txtTotalDiscountAmount.Text = String.Format("{0:N}", MyConvert(totalDiscountAmount));
            txtTotalRealAmount.Text = String.Format("{0:N}", MyConvert(totalRealAmount));
        }
        else
        {
            txtTotalAmount.Text = "**";
            txtTotalDiscountAmount.Text = "**";
            txtTotalRealAmount.Text = "**";
        }

        //获得商家信息
        if (model.depot_id > 0)
        {              
            ps_depot user_info = new ps_depot();
            user_info.GetModel(Convert.ToInt32(model.depot_id));
            user_name.Text = model.user_name;
            title.Text = user_info.title;
            
            contact_address.Text = model.address == "" ? user_info.contact_address : model.address;
            contact_name.Text = model.contract_name == "" ? user_info.contact_name : model.contract_name;
            contact_tel.Text = model.contact_number == "" ? user_info.contact_mobile : model.contact_number;
        }
        if (model.attachment1 != "")
            palAttachment1.Visible = true;
        if (model.attachment2 != "")
            palAttachment2.Visible = true;
        if (model.attachment3 != "")
            palAttachment3.Visible = true;
        if (model.attachment4 != "")
            palAttachment4.Visible = true;
        //根据订单状态，显示各类操作按钮
        switch (model.status)
        {
            case 1: //订单为已生成状态     
                    //确认订单、取消订单显示-+
                //修改订单备注、调价按钮显示
                btnEditRemark.Visible = btnEditPaymentFee.Visible = true;
                btnDesign.Visible = btnConfirmDesign.Visible = btnQuote.Visible = btnConfirmQuote.Visible = btnPay.Visible = btnConfirmPay.Visible = false;
                btnBack.Visible = BackInfo.Visible = false;
                
                if (this.status == model.status.ToString())
                {
                    btnConfirm2.Visible = btnCancel.Visible = true;
                    btnPost.Visible = btnCancel.Visible = true;
                    //btnUpdateStatusToConfirmQuote.Visible = true;
                }
                break;
            case 2: //如果订单为已确认状态
                //完成设计
                btnComplete.Visible = false;    
                btnEditRemark.Visible = false;
                btnConfirm2.Visible = btnCancel.Visible = false;
                btnPost.Visible = btnCancel.Visible = false;
                btnDesign.Visible = btnConfirmDesign.Visible = btnKuJialLe.Visible = btnQuote.Visible = btnConfirmQuote.Visible = btnPay.Visible = btnConfirmPay.Visible = false;
                if (this.status == model.status.ToString())
                {
                    btnDesign.Visible = true;
                    DesignInfo.Visible = true;
                    
                }
                break;
            case 3: //如果订单为已设计状态
                //完成确认设计
                btnComplete.Visible = false;
                btnEditRemark.Visible = false;
                btnConfirm2.Visible = btnCancel.Visible = false;
                btnPost.Visible = btnCancel.Visible = false;
                btnDesign.Visible = btnConfirmDesign.Visible = btnKuJialLe.Visible = btnQuote.Visible = btnConfirmQuote.Visible = btnPay.Visible = btnConfirmPay.Visible = false;
                btnBack.Visible = false;
                BackInfo.Visible = false;
                
                if (this.status == model.status.ToString())
                {
                    btnConfirmDesign.Visible = true;
                    btnKuJialLe.Visible = true;
                    DesignInfo.Visible = true;
                    if (model.designattachment != "" || model.designattachment2 != "" || model.designattachment3 != "" || model.designattachment4 != "" || model.designattachment5 != "")
                        DesignInfodisplay.Visible = true;
                }
                break;
            case 4: //如果订单为已确认设计状态
                //完成报价
                btnComplete.Visible = false;
                btnEditRemark.Visible = false;
                btnConfirm2.Visible = btnCancel.Visible = false;
                btnPost.Visible = btnCancel.Visible = false;
                btnDesign.Visible = btnConfirmDesign.Visible = btnKuJialLe.Visible = btnQuote.Visible = btnConfirmQuote.Visible = btnPay.Visible = btnConfirmPay.Visible = false;
                if (this.status == model.status.ToString())
                {
                    //btnQuote.Visible = true;
                    //if(model.quoteattachment!="" || model.remark != "")
                    DesignInfodisplay.Visible = true;
                    QuoteInfo.Visible = true;
                    btnConfirm2.Visible = true;
                    btnPost.Visible = true;
                }
                break;
            case 5: //如果订单为已报价状态
                //完成确认报价
                btnComplete.Visible = false;
                btnEditRemark.Visible = false;
                btnConfirm2.Visible = btnCancel.Visible = false;
                btnPost.Visible = btnCancel.Visible = false;
                btnDesign.Visible = btnConfirmDesign.Visible = btnKuJialLe.Visible = btnQuote.Visible = btnConfirmQuote.Visible = btnPay.Visible = btnConfirmPay.Visible = false;
                if (this.status == model.status.ToString())
                {
                    btnConfirmQuote.Visible = true;
                    QuoteInfodisplay.Visible = true;
                }
                break;
            case 6: //如果订单为已确认报价状态
                //完成付款
                this.txtPayAmount.Text = (Convert.ToDecimal(model.payable_amount==null ? 0 : model.payable_amount) - Convert.ToDecimal(model.payamount == null ? 0 : model.payamount)).ToString();
                btnComplete.Visible = false;
                btnEditRemark.Visible = false;
                btnConfirm2.Visible = btnCancel.Visible = false;
                btnPost.Visible = btnCancel.Visible = false;
                btnDesign.Visible = btnConfirmDesign.Visible = btnKuJialLe.Visible = btnQuote.Visible = btnConfirmQuote.Visible = btnPay.Visible = btnConfirmPay.Visible = false;
                if (this.status == model.status.ToString())
                {
                    btnPay.Visible = true;
                    PayInfo.Visible = true;
                }
                break;
            case 7: //如果订单为已付款状态
                //完成确认付款
                btnComplete.Visible = false;
                btnEditRemark.Visible = false;
                btnConfirm2.Visible = btnCancel.Visible = false;
                btnPost.Visible = btnCancel.Visible = false;
                btnDesign.Visible = btnConfirmDesign.Visible = btnKuJialLe.Visible = btnQuote.Visible = btnConfirmQuote.Visible = btnPay.Visible = btnConfirmPay.Visible = false;
                if (this.status == model.status.ToString())
                {
                    btnConfirmPay.Visible = true;
                    PayInfoDisplay.Visible = true;
                }
                break;
            case 8: //如果订单为已设计状态
                //完成确认设计
                //修改订单备注按钮可见
                btnEditRemark.Visible = false;
                btnConfirm2.Visible = btnCancel.Visible = false;
                btnPost.Visible = btnCancel.Visible = false;
                btnDesign.Visible = btnConfirmDesign.Visible = btnKuJialLe.Visible = btnQuote.Visible = btnConfirmQuote.Visible = btnPay.Visible = btnConfirmPay.Visible = false;
                if(this.status == model.status.ToString())
                    btnComplete.Visible = true;
                break;
            default:
                btnBack.Visible = BackInfo.Visible = false;
                break;
        }
        if(model.status<8)
            btnCancel.Visible = true;

        
        if (this.isSearch=="1" || this.status!=model.status.ToString())
            btnBack.Visible = BackInfo.Visible = btnCancel.Visible = false;

        if (model.status<9 && model.backremark!="")
            BackInfoDisplay.Visible = true;

    }
    #endregion

    #region save img
    public string SaveImg()
    {
        //string imgurl = "http://qhyxpic.oss.kujiale.com/fpimg/2014/07/20/U8vLw0NvY1konQBBAAAA_800x800.jpg";//图片地址
        string imgurl = txtKujiaLe.Text;
        try
        {
            if (txtKujiaLe.Text != "")
            {
                string savePath = Server.MapPath("~/upload/files/");
                string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string imgfile = savePath + "kujiale_" + formatdate + ".jpg";
                WriteBytesToFile(imgfile, GetBytesFromUrl(imgurl));
                return "kujiale_" + formatdate + ".jpg";
            }
            else
                return "";
        }
        catch ( Exception ex)
        {
            return "";
        }
    }

    /// <summary>
    /// 将数据流转化为图片保存到本地
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="content"></param>
    static public void WriteBytesToFile(string fileName, byte[] content)
    {
        FileStream fs = new FileStream(fileName, FileMode.Create);
        BinaryWriter w = new BinaryWriter(fs);
        try
        {
            w.Write(content);
        }
        finally
        {
            fs.Close();
            w.Close();
        }
    }

    /// <summary>
    /// 根据url将图片转化为数据流
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    static public byte[] GetBytesFromUrl(string url)
    {
        byte[] b;
        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
        WebResponse myResp = myReq.GetResponse();
        Stream stream = myResp.GetResponseStream();
        using (BinaryReader br = new BinaryReader(stream))
        {
            b = br.ReadBytes(500000);
            br.Close();
        }
        myResp.Close();
        return b;

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


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Session["IsDisplayPrice"] == null || Session["IsDisplayPrice"].ToString() == "1")
        {
            model.GetModel(this.id);
            ps_order_goods bll = new ps_order_goods();
            string sql = " order_id =" + this.id;
            DataTable dt = bll.GetList(sql).Tables[0];

            ps_order_goods gls = new ps_order_goods();
            ps_here_depot my = new ps_here_depot();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gls.GetModel(Convert.ToInt32(dt.Rows[i]["ID"]));
                    foreach (RepeaterItem rptitem in this.rptList.Items)
                    {
                        TextBox txtProductNo = rptitem.FindControl("txtProductNo") as TextBox;
                        if (txtProductNo.Text == dt.Rows[i]["product_no"].ToString())
                        {

                            TextBox txtPrice = rptitem.FindControl("txtPrice") as TextBox;
                            TextBox txtDiscount = rptitem.FindControl("txtDiscount") as TextBox;
                            TextBox txtDiscountAmount = rptitem.FindControl("txtDiscountAmount") as TextBox;
                            gls.real_price = Convert.ToDecimal(txtPrice.Text);
                            gls.discount = Convert.ToDecimal(txtDiscount.Text == "" ? "100" : txtDiscount.Text);
                            gls.payamount = Convert.ToDecimal(txtDiscountAmount.Text == "" ? (gls.quantity * gls.real_price * gls.discount).ToString() : (gls.quantity * gls.real_price - Convert.ToDecimal(txtDiscountAmount.Text)).ToString());

                        }
                    }
                    gls.UpdateOrderPriceAndDiscount();
                }
                (new ps_orders()).UpdateOrderAmount(model.id);
                if (model.status == 1 || model.status == 4)
                {
                    decimal PaymentRation = this.txtPaymentRation.Text == "" ? 100 : Convert.ToDecimal(this.txtPaymentRation.Text);
                    if (PaymentRation < 0 || PaymentRation > 100)
                    {
                        //ScriptManager.RegisterStartupScript(this, typeof(string), "error", "$.dialog.alert( '付款比例必须在0-100之间！');", true);
                        mym.JscriptMsg(this.Page, "付款比例必须在0-100之间！", "back", "Error");
                        return;
                    }
                    else
                    {
                        model.payration = this.txtPaymentRation.Text == "" ? 100 : Convert.ToDecimal(this.txtPaymentRation.Text);
                        model.Update();
                    }
                }
                
                //if (model.epicor_order_no==""|| model.epicor_order_no == "未生成")
                //{
                //    ScriptManager.RegisterStartupScript(this, typeof(string), "", "OrderConfirm2(" + dt.Rows[0]["order_no"] + ")", true);
                //    return;
                //}

            }
            //ScriptManager.RegisterStartupScript(this, typeof(string), "error", "$.dialog.alert( '订单已成功保存！');", true);
            mym.JscriptMsg(this.Page, "订单已成功保存！", "back", "Success");
        }
        else
            ScriptManager.RegisterStartupScript(this, typeof(string), "error", "$.dialog.alert( '订单已成功保存！');", true);
            //mym.JscriptMsg(this.Page, "订单已成功保存！", "back", "Success");

    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        model.GetModel(this.id);
        ps_order_goods bll = new ps_order_goods();
        string sql = " order_id =" + this.id;
        DataTable dt = bll.GetList(sql).Tables[0];
        if (Session["IsDisplayPrice"] == null || Session["IsDisplayPrice"].ToString() == "1")
        {
            ps_order_goods gls = new ps_order_goods();
            ps_here_depot my = new ps_here_depot();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gls.GetModel(Convert.ToInt32(dt.Rows[i]["ID"]));
                    foreach (RepeaterItem rptitem in this.rptList.Items)
                    {
                        TextBox txtProductNo = rptitem.FindControl("txtProductNo") as TextBox;
                        if (txtProductNo.Text == dt.Rows[i]["product_no"].ToString())
                        {
                            TextBox txtPrice = rptitem.FindControl("txtPrice") as TextBox;
                            TextBox txtDiscount = rptitem.FindControl("txtDiscount") as TextBox;
                            TextBox txtDiscountAmount = rptitem.FindControl("txtDiscountAmount") as TextBox;
                            gls.real_price = Convert.ToDecimal(txtPrice.Text);
                            gls.discount = Convert.ToDecimal(txtDiscount.Text == "" ? "100" : txtDiscount.Text);
                            gls.payamount = Convert.ToDecimal(txtDiscountAmount.Text == "" ? (gls.quantity * gls.real_price * gls.discount).ToString() : (gls.quantity * gls.real_price - Convert.ToDecimal(txtDiscountAmount.Text)).ToString());


                        }
                    }
                    gls.UpdateOrderPriceAndDiscount();
                }
            }
            (new ps_orders()).UpdateOrderAmount(model.id);
            if (model.status == 4)
            {
                string fileNameQuote = FileUploadQuote.FileName;//获取要导入的文件名 
                if (fileNameQuote != null && fileNameQuote != "")
                {
                    string savePath = Server.MapPath("~/upload/files/");
                    string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    FileOperatpr(fileNameQuote, savePath);
                    string url = savePath + formatdate + "-" + fileNameQuote;
                    txtQuoteFile1Name.Value = formatdate + "-" + fileNameQuote;
                    FileUploadQuote.SaveAs(url);
                    model.quoteattachment = txtQuoteFile1Name.Value;
                }
                model.quoteremark = txtQuoteRemark.Text.Trim();
                model.Update();
            }
        }

        if (model.status == 1 || model.status == 4)
        {
            decimal PaymentRation = this.txtPaymentRation.Text == "" ? 100 : Convert.ToDecimal(this.txtPaymentRation.Text);
            if (PaymentRation < 0 || PaymentRation > 100)
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '付款比例必须在0-100之间！');", true);
                mym.JscriptMsg(this.Page, "付款比例必须在0-100之间！", "back", "Error");
                return;
            }
            else
            {
                model.payration = this.txtPaymentRation.Text == "" ? 100 : Convert.ToDecimal(this.txtPaymentRation.Text);
                model.Update();
            }
        }


        if (model.epicor_order_no == "" || model.epicor_order_no == "未生成" || model.epicor_order_no.Contains("报价单已关闭") || model.epicor_order_no.Contains("销售单已关闭"))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "", "OrderConfirm2(" + dt.Rows[0]["order_no"] + ")", true);
            return;
        }
        else
        {
            btnConfirm2.Visible = btnPost.Visible = btnCancel.Visible = btnUpdateStatusToConfirmQuote.Visible= false;
            this.isSearch = "1";
            ShowInfo(this.id);
        }

       

    }

    protected void btnDesign_Click(object sender, EventArgs e)
    {
        model.GetModel(this.id);
        if (model.status == 2)
        {
            #region file1 upload
            bool fileOk = false;
            string fileNameDesign = FileUploadDesign.FileName;//获取要导入的文件名 
            if (fileNameDesign != null && fileNameDesign != "")
            {
                string savePath = Server.MapPath("~/upload/files/");
                string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                FileOperatpr(fileNameDesign, savePath);
                string url = savePath + formatdate + "-" + fileNameDesign;
                txtDesignFile1Name.Value = formatdate + "-" + fileNameDesign;

                //取得文件的扩展名,并转换成小写
                string fileExtension = System.IO.Path.GetExtension(FileUploadDesign.FileName).ToLower();
                //限定只能上传jpg和gif图片
                string[] allowExtension = { ".jpg", ".png", ".xls", ".xlsx" };
                //对上传的文件的类型进行一个个匹对
                for (int i = 0; i < allowExtension.Length; i++)
                {
                    if (fileExtension == allowExtension[i])
                    {
                        fileOk = true;
                        break;
                    }
                }
                if (fileOk==false)
                {
                    //mym.JscriptMsg(this.Page, "附件1文件类型不支持！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件1文件类型不支持！');", true);
                    return;
                }

                //对上传文件的大小进行检测，限定文件最大不超过50M
                if (FileUploadDesign.PostedFile.ContentLength > 1024000 * 50)
                {
                    fileOk = false;
                }
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1超过50M！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件1超过50M！');", true);
                    return;
                }
                else
                //if (fileOk)
                {
                    FileUploadDesign.SaveAs(url);
                    model.designattachment = txtDesignFile1Name.Value;
                }
            }
            #endregion
            #region file2 upload
            fileOk = false;
            string fileNameDesign2 = FileUploadDesign2.FileName;//获取要导入的文件名 
            if (fileNameDesign2 != null && fileNameDesign2 != "")
            {
                string savePath = Server.MapPath("~/upload/files/");
                string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                FileOperatpr(fileNameDesign2, savePath);
                string url = savePath + formatdate + "-" + fileNameDesign2;
                txtDesignFile2Name.Value = formatdate + "-" + fileNameDesign2;

                //取得文件的扩展名,并转换成小写
                string fileExtension = System.IO.Path.GetExtension(FileUploadDesign2.FileName).ToLower();
                //限定只能上传jpg和gif图片
                string[] allowExtension = { ".jpg", ".png", ".xls", ".xlsx" };
                //对上传的文件的类型进行一个个匹对
                for (int i = 0; i < allowExtension.Length; i++)
                {
                    if (fileExtension == allowExtension[i])
                    {
                        fileOk = true;
                        break;
                    }
                }
                //
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1文件类型不支持！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件2文件类型不支持！');", true);
                    return;
                }

                //对上传文件的大小进行检测，限定文件最大不超过50M
                if (FileUploadDesign.PostedFile.ContentLength > 1024000 * 50)
                {
                    fileOk = false;
                }
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1超过50M！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件2超过50M！');", true);
                    return;
                }
                else
                //if (fileOk)
                {
                    FileUploadDesign2.SaveAs(url);
                    model.designattachment2 = txtDesignFile2Name.Value;
                }
            }
            #endregion
            #region file3 upload
            fileOk = false;
            string fileNameDesign3 = FileUploadDesign3.FileName;//获取要导入的文件名 
            if (fileNameDesign3 != null && fileNameDesign3 != "")
            {
                string savePath = Server.MapPath("~/upload/files/");
                string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                FileOperatpr(fileNameDesign3, savePath);
                string url = savePath + formatdate + "-" + fileNameDesign3;
                txtDesignFile3Name.Value = formatdate + "-" + fileNameDesign3;

                //取得文件的扩展名,并转换成小写
                string fileExtension = System.IO.Path.GetExtension(FileUploadDesign3.FileName).ToLower();
                //限定只能上传jpg和gif图片
                string[] allowExtension = { ".jpg", ".png", ".xls", ".xlsx" };
                //对上传的文件的类型进行一个个匹对
                for (int i = 0; i < allowExtension.Length; i++)
                {
                    if (fileExtension == allowExtension[i])
                    {
                        fileOk = true;
                        break;
                    }
                }
                //
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1文件类型不支持！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件3文件类型不支持！');", true);
                    return;
                }

                //对上传文件的大小进行检测，限定文件最大不超过50M
                if (FileUploadDesign.PostedFile.ContentLength > 1024000 * 50)
                {
                    fileOk = false;
                }
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1超过50M！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件3超过50M！');", true);
                    return;
                }
                else
                //if (fileOk)
                {
                    FileUploadDesign3.SaveAs(url);
                    model.designattachment3 = txtDesignFile3Name.Value;
                }
            }
            #endregion
            #region file4 upload
            fileOk = false;
            string fileNameDesign4 = FileUploadDesign4.FileName;//获取要导入的文件名 
            if (fileNameDesign4 != null && fileNameDesign4 != "")
            {
                string savePath = Server.MapPath("~/upload/files/");
                string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                FileOperatpr(fileNameDesign4, savePath);
                string url = savePath + formatdate + "-" + fileNameDesign4;
                txtDesignFile4Name.Value = formatdate + "-" + fileNameDesign4;

                //取得文件的扩展名,并转换成小写
                string fileExtension = System.IO.Path.GetExtension(FileUploadDesign4.FileName).ToLower();
                //限定只能上传jpg和gif图片
                string[] allowExtension = { ".jpg", ".png", ".xls", ".xlsx" };
                //对上传的文件的类型进行一个个匹对
                for (int i = 0; i < allowExtension.Length; i++)
                {
                    if (fileExtension == allowExtension[i])
                    {
                        fileOk = true;
                        break;
                    }
                }
                //
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1文件类型不支持！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件4文件类型不支持！');", true);
                    return;
                }

                //对上传文件的大小进行检测，限定文件最大不超过50M
                if (FileUploadDesign.PostedFile.ContentLength > 1024000 * 50)
                {
                    fileOk = false;
                }
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1超过50M！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件4超过50M！');", true);
                    return;
                }
                else
                //if (fileOk)
                {
                    FileUploadDesign4.SaveAs(url);
                    model.designattachment4 = txtDesignFile4Name.Value;
                }
            }
            #endregion
            #region file5 upload
            fileOk = false;
            string fileNameDesign5 = FileUploadDesign5.FileName;//获取要导入的文件名 
            if (fileNameDesign5 != null && fileNameDesign5 != "")
            {
                string savePath = Server.MapPath("~/upload/files/");
                string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                FileOperatpr(fileNameDesign5, savePath);
                string url = savePath + formatdate + "-" + fileNameDesign5;
                txtDesignFile3Name.Value = formatdate + "-" + fileNameDesign5;

                //取得文件的扩展名,并转换成小写
                string fileExtension = System.IO.Path.GetExtension(FileUploadDesign5.FileName).ToLower();
                //限定只能上传jpg和gif图片
                string[] allowExtension = { ".jpg", ".png", ".xls", ".xlsx" };
                //对上传的文件的类型进行一个个匹对
                for (int i = 0; i < allowExtension.Length; i++)
                {
                    if (fileExtension == allowExtension[i])
                    {
                        fileOk = true;
                        break;
                    }
                }
                //
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1文件类型不支持！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件5文件类型不支持！');", true);
                    return;
                }

                //对上传文件的大小进行检测，限定文件最大不超过50M
                if (FileUploadDesign.PostedFile.ContentLength > 1024000 * 50)
                {
                    fileOk = false;
                }
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1超过50M！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件5超过50M！');", true);
                    return;
                }
                else
                //if (fileOk)
                {
                    FileUploadDesign5.SaveAs(url);
                    model.designattachment5 = txtDesignFile5Name.Value;
                }
            }
            #endregion

            model.designremark = txtDesignRemarks.Text.Trim();

            if (model.Update())
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "", "OrderDesign()", true);

            }
        }
        else
        {
            this.btnDesign.Visible = false;
            this.DesignInfo.Visible = false;
            this.DesignInfodisplay.Visible = true;
        }


    }

    protected void btnConfirmDesign_Click(object sender, EventArgs e)
    {
        model.GetModel(this.id);

        if (model.status == 3)
        {
            ps_order_goods bll = new ps_order_goods();
            string sql = " order_id =" + this.id;
            DataTable dt = bll.GetList(sql).Tables[0];

            ps_order_goods gls = new ps_order_goods();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gls.GetModel(Convert.ToInt32(dt.Rows[i]["ID"]));
                    if (dt.Rows[i]["custsize"].ToString() != "")
                    {
                        foreach (RepeaterItem rptitem in this.rptList.Items)
                        {
                            TextBox txtProductNo = rptitem.FindControl("txtProductNo") as TextBox;
                            if (txtProductNo.Text == dt.Rows[i]["product_no"].ToString())
                            {
                                TextBox txtProductDesc = rptitem.FindControl("txtProductDesc") as TextBox;
                                gls.goods_title = txtProductDesc.Text;
                            }
                        }
                        gls.UpdateGoodsProdeuctDesc();
                    }
                }
            }

            #region file1 upload
                bool fileOk = false;
            string fileNameDesign = FileUploadDesign.FileName;//获取要导入的文件名 
            if (fileNameDesign != null && fileNameDesign != "")
            {
                string savePath = Server.MapPath("~/upload/files/");
                string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                FileOperatpr(fileNameDesign, savePath);
                string url = savePath + formatdate + "-" + fileNameDesign;
                txtDesignFile1Name.Value = formatdate + "-" + fileNameDesign;

                //取得文件的扩展名,并转换成小写
                string fileExtension = System.IO.Path.GetExtension(FileUploadDesign.FileName).ToLower();
                //限定只能上传jpg和gif图片
                string[] allowExtension = { ".jpg", ".png", ".xls", ".xlsx" };
                //对上传的文件的类型进行一个个匹对
                for (int i = 0; i < allowExtension.Length; i++)
                {
                    if (fileExtension == allowExtension[i])
                    {
                        fileOk = true;
                        break;
                    }
                }
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1文件类型不支持！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件1文件类型不支持！');", true);
                    return;
                }

                //对上传文件的大小进行检测，限定文件最大不超过50M
                if (FileUploadDesign.PostedFile.ContentLength > 1024000 * 50)
                {
                    fileOk = false;
                }
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1超过50M！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件1超过50M！');", true);
                    return;
                }
                else
                //if (fileOk)
                {
                    FileUploadDesign.SaveAs(url);
                    model.designattachment = txtDesignFile1Name.Value;
                }
            }
            #endregion
            #region file2 upload
            fileOk = false;
            string fileNameDesign2 = FileUploadDesign2.FileName;//获取要导入的文件名 
            if (fileNameDesign2 != null && fileNameDesign2 != "")
            {
                string savePath = Server.MapPath("~/upload/files/");
                string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                FileOperatpr(fileNameDesign2, savePath);
                string url = savePath + formatdate + "-" + fileNameDesign2;
                txtDesignFile2Name.Value = formatdate + "-" + fileNameDesign2;

                //取得文件的扩展名,并转换成小写
                string fileExtension = System.IO.Path.GetExtension(FileUploadDesign2.FileName).ToLower();
                //限定只能上传jpg和gif图片
                string[] allowExtension = { ".jpg", ".png", ".xls", ".xlsx" };
                //对上传的文件的类型进行一个个匹对
                for (int i = 0; i < allowExtension.Length; i++)
                {
                    if (fileExtension == allowExtension[i])
                    {
                        fileOk = true;
                        break;
                    }
                }
                //
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1文件类型不支持！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件2文件类型不支持！');", true);
                    return;
                }

                //对上传文件的大小进行检测，限定文件最大不超过50M
                if (FileUploadDesign.PostedFile.ContentLength > 1024000 * 50)
                {
                    fileOk = false;
                }
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1超过50M！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件2超过50M！');", true);
                    return;
                }
                else
                //if (fileOk)
                {
                    FileUploadDesign2.SaveAs(url);
                    model.designattachment2 = txtDesignFile2Name.Value;
                }
            }
            #endregion
            #region file3 upload
            fileOk = false;
            string fileNameDesign3 = FileUploadDesign3.FileName;//获取要导入的文件名 
            if (fileNameDesign3 != null && fileNameDesign3 != "")
            {
                string savePath = Server.MapPath("~/upload/files/");
                string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                FileOperatpr(fileNameDesign3, savePath);
                string url = savePath + formatdate + "-" + fileNameDesign3;
                txtDesignFile3Name.Value = formatdate + "-" + fileNameDesign3;

                //取得文件的扩展名,并转换成小写
                string fileExtension = System.IO.Path.GetExtension(FileUploadDesign3.FileName).ToLower();
                //限定只能上传jpg和gif图片
                string[] allowExtension = { ".jpg", ".png", ".xls", ".xlsx" };
                //对上传的文件的类型进行一个个匹对
                for (int i = 0; i < allowExtension.Length; i++)
                {
                    if (fileExtension == allowExtension[i])
                    {
                        fileOk = true;
                        break;
                    }
                }
                //
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1文件类型不支持！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件3文件类型不支持！');", true);
                    return;
                }

                //对上传文件的大小进行检测，限定文件最大不超过50M
                if (FileUploadDesign.PostedFile.ContentLength > 1024000 * 50)
                {
                    fileOk = false;
                }
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1超过50M！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件3超过50M！');", true);
                    return;
                }
                else
                //if (fileOk)
                {
                    FileUploadDesign3.SaveAs(url);
                    model.designattachment3 = txtDesignFile3Name.Value;
                }
            }
            #endregion
            #region file4 upload
            fileOk = false;
            string fileNameDesign4 = FileUploadDesign4.FileName;//获取要导入的文件名 
            if (fileNameDesign4 != null && fileNameDesign4 != "")
            {
                string savePath = Server.MapPath("~/upload/files/");
                string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                FileOperatpr(fileNameDesign4, savePath);
                string url = savePath + formatdate + "-" + fileNameDesign4;
                txtDesignFile4Name.Value = formatdate + "-" + fileNameDesign4;

                //取得文件的扩展名,并转换成小写
                string fileExtension = System.IO.Path.GetExtension(FileUploadDesign4.FileName).ToLower();
                //限定只能上传jpg和gif图片
                string[] allowExtension = { ".jpg", ".png", ".xls", ".xlsx" };
                //对上传的文件的类型进行一个个匹对
                for (int i = 0; i < allowExtension.Length; i++)
                {
                    if (fileExtension == allowExtension[i])
                    {
                        fileOk = true;
                        break;
                    }
                }
                //
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1文件类型不支持！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件4文件类型不支持！');", true);
                    return;
                }

                //对上传文件的大小进行检测，限定文件最大不超过50M
                if (FileUploadDesign.PostedFile.ContentLength > 1024000 * 50)
                {
                    fileOk = false;
                }
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1超过50M！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件4超过50M！');", true);
                    return;
                }
                else
                //if (fileOk)
                {
                    FileUploadDesign4.SaveAs(url);
                    model.designattachment4 = txtDesignFile4Name.Value;
                }
            }
            #endregion
            #region file5 upload
            fileOk = false;
            string fileNameDesign5 = FileUploadDesign5.FileName;//获取要导入的文件名 
            if (fileNameDesign5 != null && fileNameDesign5 != "")
            {
                string savePath = Server.MapPath("~/upload/files/");
                string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                FileOperatpr(fileNameDesign5, savePath);
                string url = savePath + formatdate + "-" + fileNameDesign5;
                txtDesignFile3Name.Value = formatdate + "-" + fileNameDesign5;

                //取得文件的扩展名,并转换成小写
                string fileExtension = System.IO.Path.GetExtension(FileUploadDesign5.FileName).ToLower();
                //限定只能上传jpg和gif图片
                string[] allowExtension = { ".jpg", ".png", ".xls", ".xlsx" };
                //对上传的文件的类型进行一个个匹对
                for (int i = 0; i < allowExtension.Length; i++)
                {
                    if (fileExtension == allowExtension[i])
                    {
                        fileOk = true;
                        break;
                    }
                }
                //
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1文件类型不支持！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件5文件类型不支持！');", true);
                    return;
                }

                //对上传文件的大小进行检测，限定文件最大不超过50M
                if (FileUploadDesign.PostedFile.ContentLength > 1024000 * 50)
                {
                    fileOk = false;
                }
                if (fileOk == false)
                {
                    //mym.JscriptMsg(this.Page, "附件1超过50M！", "back", "Error");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '附件5超过50M！');", true);
                    return;
                }
                else
                //if (fileOk)
                {
                    FileUploadDesign5.SaveAs(url);
                    model.designattachment5 = txtDesignFile5Name.Value;
                }
            }
            #endregion
            
            model.designremark = txtDesignRemarks.Text.Trim();

            if (model.Update())
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "", "OrderConfirmDesign()", true);
                
            }
        }
        else
        {
            this.btnConfirmDesign.Visible = false;
            this.DesignInfo.Visible = false;
            this.btnCancel.Visible = false;
            this.DesignInfodisplay.Visible = true;
            this.isSearch = "1";
            ShowInfo(this.id);
        }


    }

    protected void btnKuJialLe_Click(object sender, EventArgs e)
    {
        model.GetModel(this.id);

        if (model.status == 3)
        {
            string strKuJiaLeImageFile = SaveImg();
            if (strKuJiaLeImageFile != "")
            {
                if (model.designattachment5 == "")
                    model.designattachment5 = strKuJiaLeImageFile;
                else if (model.designattachment4 == "")
                    model.designattachment4 = strKuJiaLeImageFile;
                else if (model.designattachment3 == "")
                    model.designattachment3 = strKuJiaLeImageFile;
                else if (model.designattachment2 == "")
                    model.designattachment2 = strKuJiaLeImageFile;
                else if (model.designattachment == "")
                    model.designattachment = strKuJiaLeImageFile;
                model.Update();
                if (model.designattachment != "" || model.designattachment2 != "" || model.designattachment3 != "" || model.designattachment4 != "" || model.designattachment5 != "")
                    DesignInfodisplay.Visible = true;
            }
        }
           
    }
    
    protected void btnQuote_Click(object sender, EventArgs e)
    {
        model.GetModel(this.id);
        if (model.status == 4)
        {
            ps_order_goods bll = new ps_order_goods();
            string sql = " order_id =" + this.id;
            DataTable dt = bll.GetList(sql).Tables[0];
            if (Session["IsDisplayPrice"] == null || Session["IsDisplayPrice"].ToString() == "1")
            {
                ps_order_goods gls = new ps_order_goods();
                ps_here_depot my = new ps_here_depot();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gls.GetModel(Convert.ToInt32(dt.Rows[i]["ID"]));
                        foreach (RepeaterItem rptitem in this.rptList.Items)
                        {
                            TextBox txtProductNo = rptitem.FindControl("txtProductNo") as TextBox;
                            if (txtProductNo.Text == dt.Rows[i]["product_no"].ToString())
                            {

                                TextBox txtPrice = rptitem.FindControl("txtPrice") as TextBox;
                                TextBox txtDiscount = rptitem.FindControl("txtDiscount") as TextBox;
                                TextBox txtDiscountAmount = rptitem.FindControl("txtDiscountAmount") as TextBox;
                                gls.real_price = Convert.ToDecimal(txtPrice.Text);
                                gls.discount = Convert.ToDecimal(txtDiscount.Text == "" ? "100" : txtDiscount.Text);
                                gls.payamount = Convert.ToDecimal(txtDiscountAmount.Text == "" ? (gls.quantity * gls.real_price * gls.discount).ToString() : (gls.quantity * gls.real_price - Convert.ToDecimal(txtDiscountAmount.Text)).ToString());


                            }
                        }
                        gls.UpdateOrderPriceAndDiscount();
                    }
                    (new ps_orders()).UpdateOrderAmount(model.id);
                }
            }
            string fileNameQuote = FileUploadQuote.FileName;//获取要导入的文件名 
            if (fileNameQuote != null && fileNameQuote != "")
            {
                string savePath = Server.MapPath("~/upload/files/");
                string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                FileOperatpr(fileNameQuote, savePath);
                string url = savePath + formatdate + "-" + fileNameQuote;
                txtQuoteFile1Name.Value = formatdate + "-" + fileNameQuote;
                FileUploadQuote.SaveAs(url);
                model.quoteattachment = txtQuoteFile1Name.Value;
            }
            model.quoteremark = txtQuoteRemark.Text.Trim();

            if (model.Update())
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "", "OrderQuote()", true);
                return;
            }
        }
        else
        {
            this.btnQuote.Visible = false;
            this.QuoteInfo.Visible = false;
            this.QuoteInfodisplay.Visible = true;
        }

    }

    protected void btnPay_Click(object sender, EventArgs e)
    {
        model.GetModel(this.id);
        if (model.status == 6)
        {
            string fileNamePay = FileUploadPay.FileName;//获取要导入的文件名 
            if (fileNamePay != null && fileNamePay != "")
            {
                string savePath = Server.MapPath("~/upload/files/");
                string formatdate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                FileOperatpr(fileNamePay, savePath);
                string url = savePath + formatdate + "-" + fileNamePay;
                txtPayFile1Name.Value = formatdate + "-" + fileNamePay;
                FileUploadPay.SaveAs(url);
                model.payattachment = txtPayFile1Name.Value;
            }
            model.payaremark = txtPayRemark.Text.Trim();
            model.payamount = model.payamount + Convert.ToDecimal(txtPayAmount.Text.Trim()=="" ? "0": txtPayAmount.Text.Trim())  ;

            

            if (model.Update() )
            {
                if (Convert.ToDecimal(txtTotalRealAmount.Text.Trim())* Convert.ToDecimal(model.payration)/100 <= model.payamount)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "", "OrderPay()", true);
                    return;
                }
                else
                {
                    //mym.JscriptMsg(this.Page, "付款金额已更新，付款金额小于应付金额，状态还是未汇款状态！", "back", "Success");
                    //return;
                    //ScriptManager.RegisterStartupScript(this, typeof(string), "Error", "$.message({message: '成功提示', type: 'success' });", true);
                    //ScriptManager.RegisterStartupScript(this, typeof(string), "Success", "$.dialog.alert( '付款金额已更新，付款金额小于应付金额的30%，状态还是未汇款状态！');", true);
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Success", "$.dialog.alert( '付款金额已更新，付款金额小于应付金额的付款比例，状态还是未汇款状态！');", true);
                    //ScriptManager.RegisterStartupScript(this, typeof(string), "Error", "$.dialog.alert( '付款金额已更新，付款金额小于应付金额，状态还是未汇款状态！',function () {window.location='order_edit.aspx?action=Edit&id=" + model.id + "&IsSearch=1' });", true);
                }
            }
        }
        else
        {
            this.btnPay.Visible = false;
            this.PayInfo.Visible = false;
            this.PayInfoDisplay.Visible = true;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (txtBackRemark.Text == "")
        {
            //mym.JscriptMsg(this.Page, "请输入退回备注！", "back", "Error");
            ScriptManager.RegisterStartupScript(this, typeof(string), "success", "$.dialog.alert( '请输入退回备注！');", true);
            ShowInfo(this.id);
            return;
        }

        model.GetModel(this.id);
        if (model.status >1)
        {
            if (model.status == 2)
                model.UpdateEpicorOrderNo(model.order_no, "");
            

            ps_order_goods bllOrderGoods = new ps_order_goods();
            if (model.status == 5)
            {
                if (bllOrderGoods.IsOrerExistCustStyle(model.order_no) == false)
                {
                    model.status = 1;
                    model.UpdateEpicorOrderNo(model.order_no, "");
                }
                else
                {
                    model.status = model.status - 1;
                    if (model.epicor_order_no != "") 
                    {
                        if(model.epicor_order_no.Contains("(报价单)"))
                            model.CancelEpicorQuote(Convert.ToInt32(model.epicor_order_no.Replace("(报价单)", "").Replace("(报价单已关闭)", "")));
                        model.UpdateEpicorOrderNo(model.order_no, model.epicor_order_no.Replace("(报价单)", "(报价单已关闭)"));
                    }
                }
            }
            else if (model.status == 6)
            {
                model.status = model.status - 1;
                model.payamount = 0;
            }
            else if(model.status == 8)
            {
                model.status = model.status - 1;
                if (model.epicor_order_no != "")
                {
                    if (model.epicor_order_no.Contains("(销售单)"))
                        model.CancelEpicorOrder(Convert.ToInt32(model.epicor_order_no.Replace("(销售单)", "").Replace("(销售单已关闭)", "")));
                    model.UpdateEpicorOrderNo(model.order_no, model.epicor_order_no.Replace("(销售单)", "(销售单已关闭)"));
                }
            }
            else
                model.status = model.status - 1;
            model.backremark = txtBackRemark.Text;



            if (model.Update())
            {
                //mym.JscriptMsg(this.Page, "订单已退回上一流程！", "back", "Success");
                //this.btnBack.Visible = false;
                //this.txtBackRemark.Enabled = false;
                ScriptManager.RegisterStartupScript(this, typeof(string), "Error", "alert( '订单已退回上一流程！');window.location='order_edit.aspx?action=Edit&id="+model.id+"&IsSearch=1'", true);
                return;
            }
            else
            {
                mym.JscriptMsg(this.Page, "订单退回上一流程失败！", "back", "Success");
            }
        }
        else
        {
            this.btnPay.Visible = false;
            this.PayInfo.Visible = false;
            this.PayInfoDisplay.Visible = true;
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

}