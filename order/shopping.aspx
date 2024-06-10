<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shopping.aspx.cs" Inherits="order_shopping" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>购物车</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<script type="text/javascript" src="../js/base.js"></script>
<script type="text/javascript" src="../js/cart.js"></script>
<script type="text/javascript" src="../js/datepicker/WdatePicker.js"></script>
<script type="text/javascript" charset="utf-8" src="../editor/kindeditor-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../editor/lang/zh_CN.js"></script>
<script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<script type="text/javascript" src="../js/pinyin.js"></script>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
    });

    //购物车数量加减
    function CartComputNum2(obj, webpath, goods_id, num) {
        if (num > 0) {
            var goods_quantity = $(obj).prev("input[name='goods_quantity']");
            $(goods_quantity).val(parseInt($(goods_quantity).val()) + 1);
            //计算购物车金额
            //CartAmountTotal($(goods_quantity), webpath, goods_id);
            CalcAmount($(goods_quantity).val(), goods_id)
        } else if (num < 0) {
            var goods_quantity = $(obj).next("input[name='goods_quantity']");
            if (parseInt($(goods_quantity).val()) > 1) {
                $(goods_quantity).val(parseInt($(goods_quantity).val()) - 1);
                //计算购物车金额
                CalcAmount($(goods_quantity).val(), goods_id)
            }
        }
        else {
            CalcAmount($(obj).val(), goods_id)
        }
    }

    function CalcAmount(qty,goodid) {
        var repeaterId = '<%=rptList.ClientID %>';//Repeater的客户端ID
        var rows = <%=rptList.Items.Count%>;//Repeater的行数
        var totalamount = 0;
        var totalqty = 0;
        var lineamount = 0;
        var lineqty = 0;
        for (var j = 0; j < rows; j++) {
            if (document.getElementById(repeaterId + "_ctl" + getrownumber(j) + "_txtHidID").value == goodid) {
                document.getElementById(repeaterId + "_ctl" + getrownumber(j) + "_txtHidQuantity").value = qty;
            }
        }
        
        for (var i = 0; i < rows; i++) {
           
            if (document.getElementById(repeaterId + "_ctl" + getrownumber(i) + "_txtHidPrice").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i) + "_txtHidPrice").value != '0'
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i) + "_txtHidDiscount").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i) + "_txtHidDiscount").value != '0'
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i) + "_txtHidQuantity").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i) + "_txtHidQuantity").value != '0') {
                lineamount =
                    (document.getElementById(repeaterId + "_ctl" + getrownumber(i) + "_txtHidQuantity").value *
                        document.getElementById(repeaterId + "_ctl" + getrownumber(i) + "_txtHidPrice").value *
                        (Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i) + "_txtHidDiscount").value)) / 100).toFixed(2);
                lineqty = document.getElementById(repeaterId + "_ctl" + getrownumber(i) + "_txtHidQuantity").value;
                totalqty = totalqty + Number(lineqty);
                totalamount = totalamount + Number(lineamount);
                
                if (document.getElementById(repeaterId + "_ctl" + getrownumber(i) + "_txtRealAmount") != null)
                {
                    document.getElementById(repeaterId + "_ctl" + getrownumber(i) + "_txtRealAmount").value = lineamount;
                }
                
               

            }

        }

        document.getElementById("total_quantity").innerHTML = totalqty;
        if (document.getElementById(repeaterId + "_ctl" + getrownumber(0) + "_txtRealAmount") != null) {
            document.getElementById("payable_amount1").innerHTML = totalamount.toFixed(2);
            document.getElementById("payable_amount").innerHTML = totalamount.toFixed(2);
        }

        function getrownumber(i) {
            if (i > 10) {
                return i + 1;
            }
            else {
                return '0' + i;
            }
        }
    }
</script>
</head>
<body>
<form id="form1" runat="server">
<div id="main">
<div  style="height:10px;"></div>
<div class="boxwrap">
  <div class="cart_box">

    <!--购物车-->
    <h1 class="main_tit">
    <span><a onclick="DeleteCart(this,'/','0');" href="javascript:;">清空购物车</a></span>
      我的购物车
    </h1>
    <div class="line20"></div> 
  
    <table width="1300" border="0" align="center" cellpadding="8" cellspacing="0" class="cart_table">
      <tr> 
       <th width="150" align="left">编码</th>
       <th width="300" align="left">商品描述</th>
       <%--<th width="100" align="left">产品型号</th>--%>
       <%--<th width="60" align="left">类别</th>--%>
       <th width="60" align="left">规格型号</th>
       <th width="90" align="center">颜色</th>
       <th width="100" align="center">单价</th>
       <th width="80" align="center">数量</th>
       <th width="50" align="center">折扣</th>
       <th width="80" align="center">金额小计</th>
       
       <th width="80" align="center">配置说明</th> 
       <th width="30">定制</th>
       <th width="80" align="left">备注</th>
       <th width="40" align="center">操作</th>
      </tr>
  <asp:Repeater ID="rptList" runat="server">
    <ItemTemplate> 
          <tr>
            <td width="150"><%# Eval("commercialStyle").ToString()=="" ? Eval("product_no").ToString() :Eval("commercialStyle").ToString()%>
                <asp:TextBox ID="txtProductNo" runat="server" Visible="false" Text='<%# Eval("product_no")%>' ></asp:TextBox>
                <asp:TextBox ID="kit_num" runat="server" Visible="false" Text='<%# Eval("kit_num")%>' ></asp:TextBox>
                <asp:TextBox ID="kit_desc" runat="server" Visible="false" Text='<%# Eval("kit_desc")%>' ></asp:TextBox>
            </td> <%--<a target="_blank" href=""><img src="<%# Eval("img_url")%>" class="img" />--%></td>
            <td width="200">
                <%# Eval("title")%> 
            </td>  
            <%--<td ><%# Eval("commercialStyle")%></td>--%>
            <%--<td width="100"><%#new ps_product_category().GetTitle(Convert.ToInt32(new ps_here_depot().GetTPid(Convert.ToInt32(Eval("id")))))%></td>--%>
            <td ><%# Eval("specification")%></td> 
            <td ><%# Eval("commercialcolor")%></td>
            <td align="center" width="120">
                <asp:HiddenField ID="txtHidPrice" runat="server"  Value='<%# MyConvert(Eval("price"))%>' />
            <% if (Session["IsDisplayPrice"]==null || Session["IsDisplayPrice"].ToString() == "1") {%>
                <asp:TextBox ID="txtPrice" Enabled="false" Width="28" runat="server"  Text='<%# MyConvert(Eval("price"))%>' CssClass="input" />&nbsp;&nbsp;元/<%# Eval("dw")%><input name="goods_price" type="hidden" value="<%# MyConvert(Eval("price"))%>" />
            <% }else { %>
               **
             <% }%>
            </td>
            <td width="120" align="center">
              <asp:HiddenField ID="txtHidID" runat="server"  Value='<%# Eval("id")%>' />
              <asp:HiddenField ID="txtHidQuantity" runat="server"  Value='<%# Eval("quantity")%>' /><%--CartAmountTotal--%>
              <a href="javascript:;" class="reduce" title="减一" onclick="CartComputNum2(this, '/', '<%# Eval("id")%>', -1);">减一</a>
              <input type="text"  name="goods_quantity" class="input" style="width:30px;text-align:center;ime-mode:Disabled;" value="<%# Eval("quantity")%>" onblur="CartComputNum2(this, '/', '<%# Eval("id")%>',0);" onkeypress="return (/[\d]/.test(String.fromCharCode(event.keyCode)))" />
              <a href="javascript:;" class="subjoin" title="加一" onclick="CartComputNum2(this,'/', '<%# Eval("id")%>', 1);">加一</a>
            </td>
            <td align="center" width="80">
            <asp:HiddenField ID="txtHidDiscount" runat="server"  Value='<%# MyConvert(Eval("discount"))%>' />
            <% if (Session["IsDisplayPrice"]==null || Session["IsDisplayPrice"].ToString() == "1") {%>
                <asp:TextBox ID="txtDiscount" Enabled="false" Width="28" runat="server"  Text='<%# MyConvert(Eval("discount"))%>'  CssClass="input" />%
            <% }else { %>
               **
             <% }%>
            </td>
            <% if (Session["IsDisplayPrice"]==null || Session["IsDisplayPrice"].ToString() == "1") {%>
            <td align="center" width="100"><font color="Blue" size="2">￥
                <asp:TextBox ID="txtRealAmount" Enabled="false" Width="45" runat="server"  Text='<%# Convert.ToDecimal(Eval("price")) * Convert.ToDecimal(Eval("quantity"))%>'  CssClass="input" />
                <%--<label name="real_amount"><%# Convert.ToDecimal(Eval("price")) * Convert.ToDecimal(Eval("quantity"))%></label>--%></font></td>      
            <% }else { %>
               <td align="center" width="100">**</td>
             <% }%>
            <td width="60"></td>
            <td width="60" align="center"><asp:CheckBox Width="50"  ID="ckIsCust"  Visible='<%# Eval("is_cust")%>' runat="server" />
                <%--<asp:TextBox ID="txtCustSize" Visible='<%# Eval("is_cust")%>' runat="server" width="80" Text='<%# Eval("remark")%>' CssClass="input date" /><%# Eval("is_cust").ToString()=="True" ? "":"不可定制"%>--%>
                <asp:TextBox ID="txtIsCust" Visible='false' runat="server" Text='<%# Eval("is_cust")%>'  />
            </td>
            <td width="60"><asp:TextBox ID="txtGoodsRemarks" runat="server" width="80" Text='<%# Eval("remark")%>' CssClass="input date" /></td>
            <td width="40"><a onclick="DeleteCart(this,'/','<%# Eval("id")%>');" href="javascript:;"><font color="#FF0000" size="2">删除</font></a></td>
          </tr>

           <asp:Panel runat="server" Visible='<%# Eval("is_cust")%>'>
          <tr >
              <td colspan="12">
                  <asp:Label  ID="btnFileUplaod" runat="server" Text="附档" class="btn"/>
                <asp:FileUpload ID="FileUploadDesign" runat="server" accept=".png,.jpg,.xls,.xlsx" CssClass="input date"  /><asp:HiddenField ID="txtDesignFile1Name" runat="server" />
                <asp:FileUpload ID="FileUploadDesign2" runat="server" accept=".png,.jpg,.xls,.xlsx" CssClass="input date"  /><asp:HiddenField ID="txtDesignFile2Name" runat="server" />
                <asp:FileUpload ID="FileUploadDesign3" runat="server" accept=".png,.jpg,.xls,.xlsx" CssClass="input date"  /><asp:HiddenField ID="txtDesignFile3Name" runat="server" />
                <asp:FileUpload ID="FileUploadDesign4" runat="server" accept=".png,.jpg,.xls,.xlsx" CssClass="input date"  /><asp:HiddenField ID="txtDesignFile4Name" runat="server" />
                <asp:FileUpload ID="FileUploadDesign5" runat="server" accept=".png,.jpg,.xls,.xlsx" CssClass="input date"  /><asp:HiddenField ID="txtDesignFile5Name" runat="server" />
            </td>
          </tr>
        </asp:Panel> 

       </ItemTemplate>
<FooterTemplate>
  <%#rptList.Items.Count == 0 ? " <tr><td colspan=\"6\"><div class=\"msg_tips\"><div class=\"ico warning\"></div><div class=\"msg\"><strong>购物车没有商品！</strong><p>您的购物车为空，<a href=\"goods_list.aspx\">马上去选购</a>吧！</p></div></div></td></tr>" : ""%>
</FooterTemplate>
</asp:Repeater> 
      <tr>
        <th colspan="12" align="right">
          商品件数：
            <asp:label ID="total_quantity" runat="server"></asp:label>件 &nbsp;&nbsp; 商品总金额：<font color="#FF0000" size="2">￥<asp:label ID="payable_amount" runat="server"></asp:label></font>元 
        </th>
      </tr>

            <tr>
             <th colspan="2" align="left">
               <p>订单日期： <asp:TextBox ID="txtOrderDate" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" datatype="/^\s$|^\d{4}\-\d{1,2}\-\d{1,2}$/"  errormsg="请选择正确的日期" sucmsg=" "/>
               <p>需求日期： <asp:TextBox ID="txtNeedDate" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" datatype="/^\s$|^\d{4}\-\d{1,2}\-\d{1,2}$/" errormsg="请选择正确的日期" sucmsg=" " />
               <p>出货日期： <asp:TextBox ID="txtShipDate" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" datatype="/^\s$|^\d{4}\-\d{1,2}\-\d{1,2}$/" errormsg="请选择正确的日期" sucmsg=" " />
            </th>

            <th colspan="8" align="left">
                 联&nbsp;系&nbsp;人&nbsp;：<asp:TextBox ID="txtContactName" class="input" runat="server" Width="350px"  ></asp:TextBox> <span class="Validform_checktip">*</span>
                 <BR>
                 联系电话：<asp:TextBox ID="txtContactNumber" class="input" runat="server" Width="350px"></asp:TextBox> <span class="Validform_checktip">*</span>
                <BR>
                送货地址：<asp:TextBox ID="txtAddress" class="input" runat="server" Height="34px" TextMode="MultiLine" Width="350px"  ></asp:TextBox>
                 <%--<asp:DropDownList ID="ddlProvice" runat="server" Visible="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlProvice_SelectedIndexChanged">
                    <asp:ListItem Text="-请选择省份-" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlCity" runat="server" Visible="false"  AutoPostBack="true" onselectedindexchanged="ddlCity_SelectedIndexChanged">
                    <asp:ListItem Text="-请选择城市-" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlArea" Visible="false"  runat="server">
                    <asp:ListItem Text="-请选择县区-" Value=""></asp:ListItem>
                </asp:DropDownList>--%>
                 
                
            </th>

             <th colspan="2" align="left">
                <div>
                  <div class="left">
                    <h4>订单留言<span>字数控制在100个字符内</span></h4>
                      <asp:TextBox ID="message" class="input" runat="server" Height="84px" 
                          TextMode="MultiLine" Width="200px"></asp:TextBox>
                  </div>
                </th>
               
      </tr>
       <tr>
        <td colspan="12">
            <asp:Label  ID="btnFileUplaod" runat="server" Text="附件" class="btn yellow"/>
            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="input date"  /><asp:HiddenField ID="txtFile1Name" runat="server" />
            <asp:FileUpload ID="FileUpload2" runat="server" CssClass="input date" /><asp:HiddenField ID="txtFile2Name" runat="server" />
            <asp:FileUpload ID="FileUpload3" runat="server" CssClass="input date" /><asp:HiddenField ID="txtFile3Name" runat="server" /> 
            <asp:FileUpload ID="FileUpload4" runat="server" CssClass="input date" /><asp:HiddenField ID="txtFile4Name" runat="server" /> 
        </td>
      </tr>
    <tr>
    <th colspan="13" align="left">

    <div>
      <div class="left">
<%--        <h4>订单留言<span>字数控制在100个字符内</span></h4>
          <asp:TextBox ID="message" class="input" runat="server" Height="84px" 
              TextMode="MultiLine" Width="307px"></asp:TextBox>--%>
      </div>
      
      <div class="right" style="text-align:right;line-height:40px;">
        <b class="font18">应付总金额：<font color="#FF0000">￥<label id="order_amount"><asp:label ID="payable_amount1" runat="server"></asp:label></label></font></b>
      </div>
    </div>
     <div class="line20"></div>
    <div class="right">
      <a class="btn green" href="goods_list.aspx">继续购物</a>
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存 " CssClass="btn"  onclick="btnSubmit_Click"  />
   
    </div>
    </th>
      </tr>
    	</table>
   
    <div class="clear"></div>
    <!--/购物车-->

  </div>
</div>

<div class="clear"></div>
</div>

    </form>
</body>
</html>
