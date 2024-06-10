<%@ Page Language="C#" AutoEventWireup="true" CodeFile="my_order_info.aspx.cs" Inherits="order_my_order_info" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单信息</title>
    <script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<script type="text/javascript" src="../js/pinyin.js"></script>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
	<div class="place">
    <span>位置：</span>
    <ul class="placeul">
    <li><a href="../home.aspx">首页</a></li>
    <li><a href="my_order.aspx">我的订单</a></li>
    <li><a href="#">订单信息</a></li>
    </ul>
    </div>

    <div class="formbody">   
    <div class="formtitle"><span>订单详细信息</span></div>
    <!--/订单详细信息-->
<div class="tab-content2">
  <dl style="margin-left:20px;">
    <dd style="margin-left:50px;text-align:center;">
      <div class="order-flow" style="width:1200px">
        <%if (model.status < 11 && model.status != 10)
          { %>
        <div class="order-flow-left">
          <a class="order-flow-input">生成</a>
          <span><p class="name">订单已生成</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.add_time)%></p></span>
        </div>
        


<%--        <%if (model.payment_status == 0 && model.status == 1)
          { %>

        <div class="order-flow-wait">
           <a class="order-flow-input">确认</a>
           <span><p class="name">等待确认</p></span>
        </div>
        <%}
          else if (model.payment_status == 0 && model.status > 1  && model.status !=10)
          { %>
        <div class="order-flow-arrive">
          <a class="order-flow-input">确认</a>
          <span><p class="name">已确认</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.confirm_time)%></p></span>
        </div>
        <%} %>--%>
        
        <%if(bllOrderGoods.IsOrerExistCustStyle(Convert.ToInt32(Request.QueryString["id"]==""?"0" : Request.QueryString["id"])))
        { %>
           <%-- <%if (model.payment_status == 0 && model.status < 3)
            { %>
            <div class="order-flow-wait">
               <a class="order-flow-input">设计</a>
               <span><p class="name">等待设计</p></span>
            </div>
            <%}
            else if (model.payment_status == 0 && model.status >= 3 && model.status !=10)
            { %>
            <div class="order-flow-arrive">
              <a class="order-flow-input">设计</a>
              <span><p class="name">已设计</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.designtime)%></p></span>
            </div>
            <%} %>--%>
        
             <%if (model.payment_status == 0 && model.status < 4)
            { %>
            <div class="order-flow-wait">
               <a class="order-flow-input">确认设计</a>
               <span><p class="name">等待确认设计</p></span>
            </div>
            <%}
            else if (model.payment_status == 0 && model.status >= 4 && model.status !=10)
            { %>
            <div class="order-flow-arrive">
              <a class="order-flow-input">确认设计</a>
              <span><p class="name">已确认设计</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.comfirmdesigntime)%></p></span>
            </div>
            <%} %>
        <%} %>
        <%if (model.payment_status == 0 && model.status < 5)
        { %>
        <div class="order-flow-wait">
           <a class="order-flow-input">报价</a>
           <span><p class="name">等待报价</p></span>
        </div>
        <%}
        else if (model.payment_status == 0 && model.status >= 5 && model.status !=10)
        { %>
        <div class="order-flow-arrive">
          <a class="order-flow-input">报价</a>
          <span><p class="name">已报价</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.quotetime)%></p></span>
        </div>
        <%} %>

        <%if (model.payment_status == 0 && model.status < 6)
        { %>
        <div class="order-flow-wait">
           <a class="order-flow-input">确认报价</a>
           <span><p class="name">等待确认报价</p></span>
        </div>
        <%}
        else if (model.payment_status == 0 && model.status >= 6 && model.status !=10)
        { %>
        <div class="order-flow-arrive">
          <a class="order-flow-input">确认报价</a>
          <span><p class="name">已确认报价</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.comfirmquotetime)%></p></span>
        </div>
        <%} %>

         <%if (model.payment_status == 0 && model.status < 7)
        { %>
        <div class="order-flow-wait">
           <a class="order-flow-input">汇款</a>
           <span><p class="name">等待汇款</p></span>
        </div>
        <%}
        else if (model.payment_status == 0 && model.status >= 7 && model.status !=10)
        { %>
        <div class="order-flow-arrive">
          <a class="order-flow-input">汇款</a>
          <span><p class="name">已汇款</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.paytime)%></p></span>
        </div>
        <%} %>

         <%if (model.payment_status == 0 && model.status < 8)
        { %>
        <div class="order-flow-wait">
           <a class="order-flow-input">确认收款</a>
           <span><p class="name">等待确认收款</p></span>
        </div>
        <%}
        else if (model.payment_status == 0 && model.status >= 8 && model.status !=10)
        { %>
        <div class="order-flow-arrive">
          <a class="order-flow-input">确认收款</a>
          <span><p class="name">已确认收款</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.comfirmpaytime)%></p></span>
        </div>
        <%} %>

         <%if (model.status == 9)
           { %>
         <div class="order-flow-right-arrive">
           <a class="order-flow-input">排产</a>
           <span><p class="name">订单已排产</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.complete_time)%></p></span>
         </div>
         <%}
           else
           { %>
         <div class="order-flow-right-wait">
           <a class="order-flow-input">排产</a>
           <span><p class="name">订单未排产</p></span>
         </div>
         <%} %>
         <%}
          else if (model.status == 10)
          {%>
          <div style="text-align:center;line-height:30px; font-size:20px; color:Red;">该订单已取消</div>
         <%} %>
      </div>
    </dd>
  </dl>
  <dl>
    <dt><%--订单号--%></dt>
    <dd>订单号 ：<span id="spanOrderNo"><%=model.order_no %></span></dd>
  </dl>

  <dl>
    
  </dl>
  <asp:Repeater ID="rptList" runat="server">
  <HeaderTemplate>
  <dl>
    <dt><%--产品列表--%></dt>
    <dd>
      <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
        <thead>
          <tr>
            <th width="8%">产品编码</th>    
            <%--<th width="8%">产品类别</th>--%>
            <th width="10%">产品名称</th>
            <th width="12%">产品型号</th>
            <th width="8%">颜色</th>
            <th width="4%">售价</th>
            <th width="4%">折扣%</th>  
            <th width="4%">数量</th>
            <th width="8%">定制<BR>规格</th>
            <th width="4%">折扣前<BR>金额</th>
            <th width="4%">实付<BR>金额</th>
            <th width="4%">折扣<BR>金额</th>
            <th width="10%">备注</th>
          </tr>
        </thead>
        <tbody>
        </HeaderTemplate>
        <ItemTemplate>
          <tr class="td_c">
            <%--<td><%# Eval("commercialStyle").ToString()=="" ? Eval("product_no").ToString() :Eval("commercialStyle").ToString()%></td>
            <td><%#new ps_product_category().GetTitle(Convert.ToInt32(Eval("product_category_id")))%></td>
            <td style="text-align:left;white-space:normal;"><%#Eval("goods_title")%></td>
            <td style="text-align:left;white-space:normal;"><%#Eval("specification")%></td>
            <%--<td style="text-align:left;white-space:normal;"><%#Eval("commercialStyle")%></td>
            <%--<td style="text-align:left;white-space:normal;"><%#Eval("commercialcolor")%></td>
            <td><%#Eval("real_price")%></td>
            <td><%#Eval("quantity")%>&nbsp;&nbsp;<%# Eval("dw")%></td>
            <td><%#Eval("discount")%></td>
            <td><%#Eval("custsize")%></td>
            <td><%#Convert.ToDecimal(Eval("real_price")) * Convert.ToDecimal(Eval("quantity")) * (100 -Convert.ToDecimal(Eval("discount").ToString()=="" ? "0" : Eval("discount")))/100%></td>
            <td><%#Eval("remarks")%></td>--%>

             <td><%# Eval("commercialStyle").ToString()=="" ? Eval("product_no").ToString() :Eval("commercialStyle").ToString()%></td>    
            <%--<td><%#new ps_product_category().GetTitle(Convert.ToInt32(Eval("product_category_id")))%></td>--%>
            <td style="text-align:left;white-space:normal;"><%#Eval("goods_title")%></td>
            <td style="text-align:left;white-space:normal;"><%#Eval("specification")%></td>
      
            <td style="text-align:left;white-space:normal;"><%#Eval("commercialcolor")%></td>
            <% if (Session["IsDisplayPrice"]==null || Session["IsDisplayPrice"].ToString() == "1") {%>
            <td align="center" width="40"><%# MyConvert(Eval("real_price"))%></td>
            <td align="center" width="40"><%# MyConvert(Eval("discount"))%></td>
            <% }else { %>
                <td>**</td>
                <td>**</td>
            <% }%>
            <td><%#Eval("quantity")%><%# Eval("dw")%></td>
            <td><%#Eval("custsize")%></td>
            <% if (Session["IsDisplayPrice"]==null || Session["IsDisplayPrice"].ToString() == "1") {%>
                <td><%#String.Format("{0:N}", Convert.ToDecimal(Eval("real_price")) * Convert.ToDecimal(Eval("quantity")))%></td>
                <td><%#String.Format("{0:N}", Convert.ToDecimal(Eval("real_price")) * Convert.ToDecimal(Eval("quantity")) * (Convert.ToDecimal(Eval("discount").ToString()=="" ? "0" : Eval("discount")))/100)%></td>
                <td><%#String.Format("{0:N}", Convert.ToDecimal(Eval("real_price")) * Convert.ToDecimal(Eval("quantity")) * (100 -Convert.ToDecimal(Eval("discount").ToString()=="" ? "0" : Eval("discount")))/100)%></td>
              <% }else { %>
                <td>**</td>
                <td>**</td>
                <td>**</td>
            <% }%>
            <td><%#Eval("remarks")%></td>
          </tr>
          </ItemTemplate>
          <FooterTemplate>
        </tbody>
      </table>
    </dd>
  </dl>
  </FooterTemplate>
  </asp:Repeater>
  <dl>
    <dt><%--收货信息--%></dt>
    <dd>
      <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
        <tr>
          <th width="20%">经销商账户</th>
          <td>
            <div class="position">
              <span id="spanAcceptName"><asp:Label ID="user_name" runat="server" /></span>
   
            </div>
          </td>
        </tr>
        
        <tr>
          <th>经销商名称</th>
          <td> <span id="span1"><asp:Label ID="title" runat="server" /></span></td>
        </tr>
        <tr>
          <th>经销商地址</th>
          <td> <span id="spanAddress"><asp:Label ID="contact_address" runat="server" /></span></td>
        </tr>
        <tr>
          <th>联系人姓名</th>
          <td><span id="spanPostCode"><asp:Label ID="contact_name" runat="server" /></span></td>
        </tr>
        <tr>
          <th>联系电话</th>
          <td><span id="spanTelphone"><asp:Label ID="contact_tel" runat="server" /></span></td>
        </tr>
          <tr>
          <th>用户留言</th>
          <td><%=model.message %></td>
        </tr>
        <tr>
          <th>订单备注</th>
          <td>
            <div class="position">
              <div id="divRemark"><%=model.remark %></div>
              <input id="btnEditRemark" runat="server" visible="false" type="button" class="ibtn" value="修改" />
            </div>
          </td>
        </tr>
      </table>
    </dd>
  </dl>
  

  <dl>
    <dt><%--订单统计--%></dt>
    <dd>
      <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
        <tr>
          <th width="20%">下单总金额</th>
          <td>
            <div class="position">

               <asp:Textbox ID="txtTotalAmount" Enabled="false" runat="server" Text='<%=model.payable_amount%>'></asp:Textbox>元
               <span id="spanTotalAmountValue" style="visibility:hidden"><%=model.payable_amount%></span>
            </div>
          </td>
        </tr>
         <tr>
          <th width="20%">折扣总金额</th>
          <td>
            <div class="position">
                <asp:Textbox ID="txtTotalDiscountAmount" Enabled="false" runat="server" Text='<%=model.payable_amount%>'></asp:Textbox>元
               <span id="spanDiscountAmountValue" style="visibility:hidden"><%=model.payable_amount%></span>
            </div>
          </td>
        </tr>
        <tr>
          <th width="20%">折扣后总金额</th>
          <td>
            <div class="position">

               <asp:Textbox ID="txtTotalRealAmount" Enabled="false" runat="server" Text='<%=model.payable_amount%>'></asp:Textbox>元
               <span id="spanRealAmountValue" style="visibility:hidden"><%=model.payable_amount%></span>
            </div>
          </td>
        </tr>
        <tr style="visibility:hidden">
          <th>实际总金额</th>
          <td>
            <div class="position">
              <span id="spanPaymentFeeValue"><%=model.order_amount%></span> 元
              <input id="btnEditPaymentFee" runat="server" visible="false" type="button" class="ibtn" value="调价" />
            </div>
          </td>
        </tr>
        <tr style="visibility:hidden">
          <th>价格调整金额</th>
          <td><%=model.real_amount%> 元</td>
        </tr>
      </table>
    </dd>
  </dl>

   <dl>
    <dt><%--附件--%></dt>
    <dd>
      <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
        <tr><%--<a href="#" onClick="downLoadFile('../upload/files/<%=model.attachment1%>')"><a href="/upload/files/<%=model.attachment2%>"><%=model.attachment2%></a>--%>
          <td width="20%" align="left" valign="middle"><asp:Panel ID="palAttachment1" Visible="false" runat="server"><img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.attachment1%>" ><%=model.attachment1%></a></asp:Panel></td>
          <td width="20%" align="left" valign="middle"><asp:Panel ID="palAttachment2" Visible="false" runat="server"><img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.attachment2%>" ><%=model.attachment2%></a></asp:Panel></td>
          <td width="20%" align="left" valign="middle"><asp:Panel ID="palAttachment3" Visible="false" runat="server"><img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.attachment3%>" ><%=model.attachment3%></a></asp:Panel></td>
          <td width="20%" align="left" valign="middle"><asp:Panel ID="palAttachment4" Visible="false" runat="server"><img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.attachment4%>" ><%=model.attachment4%></a></asp:Panel></td>
        </tr>
      </table>
    </dd>
  </dl>
</div>
<!--/订单详细信息-->
    </div>



    </form>
</body>
</html>
