
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="customer_credit.aspx.cs" Inherits="sysmanager_customer_credit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>经销商账户状态</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>
</head>
<body>
    <form id="form2" runat="server">
	<div class="place">
    <span>位置：</span>
    <ul class="placeul">
    <li><a href="../home.aspx">首页</a></li>
    <li><a href="#">经销商账户状态</a></li>
    </ul>
    </div>  
    <div class="rightinfo">    
    
    <dl class="seachform"> 
    <dd><label>查询关键字</label><span class="single-select"><asp:TextBox ID="txtKeywords" runat="server" CssClass="scinput"></asp:TextBox></span></dd>
    <dd><label>所属公司</label>  
    <span class="rule-single-select">
    <asp:DropDownList ID="ddlCategoryId"  runat="server" AutoPostBack="True" onselectedindexchanged="ddlCategoryId_SelectedIndexChanged">
    </asp:DropDownList>
    </span>
    </dd>

      <dd class="cx"> <asp:Button ID="lbtnSearch" runat="server" CssClass="scbtn" onclick="btnSearch_Click" Text="查询"></asp:Button></dd> 

      <dd class="cx"> <asp:Button ID="lbtnUpdate" runat="server" CssClass="scbtn" onclick="btnUpdate_Click" Text="更新信用信息"></asp:Button></dd> 
    </dl>
    
    <!--列表-->
<asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
        <table class="tablelist">
    	    <thead>
    	    <tr>
            <th width="50px;">序号</th>
             <%--<th>公司</th>--%>
            <th>经销商编码</th>
            <th>经销商名称</th>
            <th>信用冻结</th>
            <th>信用额度</th>
            <th>订单总计金额</th>
            <th>未结发票金额</th>
            <th>未结订单金额</th>
            </tr>
            </thead>
       <tbody>
    </HeaderTemplate>
    <ItemTemplate> 
        <tr>
		<td><%# pageSize * page + Container.ItemIndex + 1 - pageSize%><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /></td>	
       <%-- <td><%# Eval("Company")%></td>--%>
        <td><%# Eval("CustID")%></td>
        <td><%# Eval("CustName")%></td>
        <td><%# Eval("CreditHold")%></td>
        <% if (Session["IsDisplayPrice"]==null || Session["IsDisplayPrice"].ToString() == "1") {%>
            <td align="right"><%# String.Format("{0:N}",Eval("CreditLimit"))%></td>
            <td align="right"><%# String.Format("{0:N}",Eval("TotOrderCredit"))%></td>
            <td align="right"><%# String.Format("{0:N}",Eval("TotOpenInvoices"))%></td>
		    <td align="right"><%# String.Format("{0:N}",Eval("TotOpenOrders"))%></td>
        <% }else { %>
            <td>**</td>
            <td>**</td>
            <td>**</td>
            <td>**</td>
        <% }%>
        </tr> 
     </ItemTemplate>
    <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"10\"><font color=red>暂无记录</font></td></tr>" : ""%>
       </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>  
   <!--列表-->
<div class="pagelist">
  <div class="l-btns">
    <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" ontextchanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
  </div>
  <div id="PageContent" runat="server" class="default"></div>
</div>   
    </div>
    </form>
</body>
</html>
