<%@ Page Language="C#" AutoEventWireup="true" CodeFile="purchase_undelivery_edit.aspx.cs" Inherits="purchase_purchase_undelivery_edit" %>
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
    <span><a onclick="DeleteCart(this,'/','0');" href="javascript:;">清空送货信息</a></span>
      建立送货单
    </h1>
    <div class="line20"></div> 
  
    <table width="1200" border="0" align="center" cellpadding="8" cellspacing="0" class="cart_table">
      <tr> 
       <th width="80" align="left">PO单号</th>
       <th width="80" align="left">PO行</th>
       <%--<th width="100" align="left">产品型号</th>--%>
       <th width="120" align="left">物料编码</th>
       <th width="300" align="left">描述</th>
       <th width="90" align="center">订单数量</th>
       <th width="100" align="center">送货数量</th>
       <th width="80" align="center">备注</th>
       <th width="40" align="center">操作</th>
      </tr>
  <asp:Repeater ID="rptList" runat="server">
    <ItemTemplate> 
          <tr>
            <td><%# Eval("PONum")%></td>
            <td><%# Eval("POLine")%></td>
            <td><%# Eval("PARTNUM")%></td>
            <td><%# Eval("LineDesc")%></td>
            <td align="right"><%# String.Format( "{0:N2}", Eval("OrderQty"))%></td>
  
            <td width="60" align="right"><asp:TextBox ID="txtCustSize" runat="server" width="80" Text='<%# string.Format( "{0:N2}", Eval("OrderQty"))%>' CssClass="input date" /></td>
            <td width="60"><asp:TextBox ID="txtGoodsRemarks" runat="server" width="120"  CssClass="input date" /></td>
            <td width="40"><a onclick="DeleteCart(this,'/','<%# Eval("id")%>');" href="javascript:;"><font color="#FF0000" size="2">删除</font></a></td>
          </tr>
       </ItemTemplate>
<FooterTemplate>
  <%#rptList.Items.Count == 0 ? " <tr><td colspan=\"6\"><div class=\"msg_tips\"><div class=\"ico warning\"></div><div class=\"msg\"><strong>购物车没有商品！</strong><p>您的购物车为空，<a href=\"goods_list.aspx\">马上去选购</a>吧！</p></div></div></td></tr>" : ""%>
</FooterTemplate>
</asp:Repeater> 
      <tr>
        <th colspan="13" align="right">
          商品件数：36
            <asp:Literal ID="total_quantity" Visible="false" runat="server"></asp:Literal>件 &nbsp;&nbsp; 
        </th>
      </tr>

            <tr>
             <th colspan="2" align="left">
               <p>送货单号： <asp:TextBox ID="txtOrderDate" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" datatype="/^\s$|^\d{4}\-\d{1,2}\-\d{1,2}$/"  errormsg="请选择正确的日期" sucmsg=" "/>
               <p>送货日期： <asp:TextBox ID="txtNeedDate" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" datatype="/^\s$|^\d{4}\-\d{1,2}\-\d{1,2}$/" errormsg="请选择正确的日期" sucmsg=" " />
            </th>


             <th colspan="12" align="left">
                <div>
                  <div class="left">
                    <h4>备注</span></h4>
                      <asp:TextBox ID="message" class="input" runat="server" Height="34px" 
                          TextMode="MultiLine" Width="500px"></asp:TextBox>
                  </div>
                </th>
               
      </tr>
       <tr>
        <td colspan="13">
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
        <b class="font18">总件数：<font color="#FF0000">36<label id="order_amount"><asp:Literal ID="payable_amount1" runat="server"></asp:Literal></label></font></b>
      </div>
    </div>
     <div class="line20"></div>
    <div class="right">
      <a class="btn green" href="goods_list.aspx">提交保存</a>
    <asp:Button ID="btnSubmit" runat="server" Text="Post到Epicor" CssClass="btn"  onclick="btnSubmit_Click"  />
   
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
