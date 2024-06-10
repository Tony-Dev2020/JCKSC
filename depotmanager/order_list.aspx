<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_list.aspx.cs" Inherits="depotmanager_order_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>未处理订单</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<script type="text/javascript" src="../js/lhgcore.min.js"></script>
<script type="text/javascript" src="../js/lhgcalendar.min.js"></script>
<script type="text/javascript">
    J(function () {
        J('#txtstart_time').calendar({ btnBar: true });
        J('#txtstop_time').calendar({ btnBar: true });
    });
    function opdg(s_type, s_url) {
        var t_width, t_height, t_title, t_url, t_id;
        t_id = 'w_1';
        switch (s_type) {
            case 'info':
                t_width = 1080;
                t_height = 460;
                t_title = '查看订单详情';
                t_url = s_url;
                break;
        }
        $.dialog({
            width: t_width,
            height: t_height,
            title: t_title,
            max: false,
            content: 'url:' + t_url
        });
    } 
</script> 
</head>
<body>
    <form id="form1" runat="server">
	<div class="place">
    <span>位置:</span>
    <ul class="placeul">
    <li><a href="../home.aspx">首页</a></li>
    <li><a href="#"><asp:Label ID="lbTile" runat="server"></asp:Label></a></li>
    </ul>
    </div>  
    <div class="rightinfo">
    <dl class="seachform"> 
    <dd><label> 关键字</label><span class="single-select"><asp:TextBox ID="txtNote_no" runat="server" Width="120" CssClass="scinput"></asp:TextBox></span></dd>
	<dd><label>下单日期</label><span class="single-select"><input  type="text" class="timeinput" id="txtstart_time" name="txtstart_time" readonly="readonly" runat="server" /></span></dd>
	<dd><label>到</label><span class="single-select"><input type="text" class="timeinput" id="txtstop_time" name="txtstop_time" readonly="readonly" runat="server"/></span></dd>


     <dd><label>公司</label>  
    <span class="rule-single-select">
    <asp:DropDownList ID="ddldepot_category_id"  runat="server" AutoPostBack="True" onselectedindexchanged="ddldepot_category_id_SelectedIndexChanged">
    </asp:DropDownList>
    </span>
    </dd>
    <dd><label>下单经销商</label>  
    <span class="rule-single-select">
    <asp:DropDownList ID="ddldepot_id"  runat="server" AutoPostBack="True" onselectedindexchanged="ddldepot_id_SelectedIndexChanged">
    </asp:DropDownList>
    </span>
    </dd>


      <dd class="cx"><asp:Button ID="lbtnSearch" runat="server" CssClass="scbtn" onclick="btnSearch_Click" Text="查询"></asp:Button>   
 </dd>
 
    </dl>
            <!--列表-->
<asp:Repeater ID="rptList" runat="server">
<HeaderTemplate>
	  <table class="tablelist">
    	<thead>
    	<tr>
        <th width="40px;">序号</th>
		<th width="110px;">订单号</th>
        <%# (this.status != 5 && this.status != 6)? "<th width=\"120px;\">Epicor订单号</th>": "" %>
		<th width="60px;">公司</th>
        <th width="8%" align="center" width="200px;">下单经销商</th>
		<th  width="8%" align="center">下单账号</th>
        <th width="8%">订单状态</th>
        <th width="80px;">折扣前金额</th>
        <th width="80px;">折扣金额</th>
		<th width="80px;">实付金额</th>
		<th width="150px;">下单时间</th>
         <th width="90px;">操作</th>     
        </tr>
        </thead>
        <tbody>
 </HeaderTemplate>
<ItemTemplate>
        <tr>
            <td><%# pageSize * page + Container.ItemIndex + 1 - pageSize%></td>
            <td><%# Eval("order_no")%></td>
            <%# (this.status != 5 && this.status != 6)?"<td>" + Eval("epicor_order_no") +"</td>": "" %>
            <td><%# Convert.ToInt32(Eval("depot_category_id")) == 0 ? "<font color=red>公司操作</font>" : new ps_depot_category().GetTitle(Convert.ToInt32(Eval("depot_category_id")))%></td>
            <td><%# Convert.ToInt32(Eval("depot_id")) == 0  ? "<font color=red>公司操作</font>" : new ps_depot().GetTitle(Convert.ToInt32(Eval("depot_id")))%></td>
            <td><%#Eval("user_name").ToString() == "" ? "匿名用户" : Eval("user_name").ToString()%></td>
            <td ><%#GetOrderStatus(Convert.ToInt32(Eval("status")))%></td>
            <% if (Session["IsDisplayPrice"]==null || Session["IsDisplayPrice"].ToString() == "1") {%>
                <td align="right"><%# String.Format("{0:N}",Eval("order_amount"))%></td>	
                <td align="right"><%# String.Format("{0:N}",Eval("discount_amount"))%></td>	
                <td align="right"><%# String.Format("{0:N}",Eval("payable_amount"))%></td>		
             <% }else { %>
                <td>**</td>
                <td>**</td>
                <td>**</td>
            <% }%>
            <td><%#string.Format("{0:yyyy-MM-dd HH:mm:ss}",Eval("add_time"))%></td>
            <td><a href="../select/order_edit.aspx?action=Edit&id=<%#Eval("id")%>&status=<%#this.status%>" class="tablelink"> 
                <%#this.status==1 ? "确认" : (this.status==2 ? "设计" :(this.status==3 ? "确认设计" :(this.status==4 ? "报价" :(this.status==5 ? "确认报价" :(this.status==6 ? "汇款" :(this.status==7 ? "确认汇款" :"")))))) %>
                </a>
            </td>
        </tr> 		
 	   </ItemTemplate>
    <FooterTemplate>
  <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"12\"><font color=red>暂无记录</font></td></tr>" : ""%>
   </tbody>
    </table>
</FooterTemplate>
</asp:Repeater> 

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
