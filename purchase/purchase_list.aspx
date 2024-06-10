<%@ Page Language="C#" AutoEventWireup="true" CodeFile="purchase_list.aspx.cs" Inherits="purchase_purchase_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>我的订单</title>
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
    <span>Position:</span>
    <ul class="placeul">
    <li><a href="../home.aspx">Home</a></li>
    <li><a href="#">Order</a></li>
    </ul>
    </div>  
    <div class="rightinfo">
    <dl class="seachform"> 
    <dd><label> Order No</label><span class="single-select"><asp:TextBox ID="txtNote_no" runat="server" Width="120" CssClass="scinput"></asp:TextBox></span></dd>
    <dd><label>Company</label>  
    <span class="rule-single-select">
    <asp:DropDownList ID="ddldepot_category_id"  runat="server" AutoPostBack="True" onselectedindexchanged="ddldepot_category_id_SelectedIndexChanged">
    </asp:DropDownList>
    </span>
    </dd>
    <dd><label>Supplier</label>  
    <span class="rule-single-select">
    <asp:DropDownList ID="ddlvendor"  runat="server" AutoPostBack="True" onselectedindexchanged="ddlvendor_SelectedIndexChanged">
    </asp:DropDownList>
    </span>
    </dd>
	<dd><label>Order Date From</label><span class="single-select"><input  type="text" class="timeinput" id="txtstart_time" name="txtstart_time" readonly="readonly" runat="server" /></span></dd>
	<dd><label>To</label><span class="single-select"><input type="text" class="timeinput" id="txtstop_time" name="txtstop_time" readonly="readonly" runat="server"/></span></dd>

     <dd><label>Status</label>  
    <span class="rule-single-select">
        <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" onselectedindexchanged="ddlStatus_SelectedIndexChanged">
            <asp:ListItem Value="0" Selected="True">==All==</asp:ListItem>
            <asp:ListItem Value="1">未确认</asp:ListItem>
            <asp:ListItem Value="2">已确认</asp:ListItem>
          </asp:DropDownList>
    </span>
    </dd>

      <dd class="cx"><asp:Button ID="lbtnSearch" runat="server" CssClass="scbtn" onclick="btnSearch_Click" Text="Search"></asp:Button>   
 </dd>
     <dd class="toolbar1" style="visibility:hidden">
        <asp:LinkButton ID="btnExport" runat="server" CssClass="save" onclick="btnExport_Click">   <li><span><img src="../images/t04.png" /></span>Export Execl</li></asp:LinkButton>
        </dd>
    </dl>
            <!--列表-->

	  <table class="imgtable">
    	<thead>
    	<tr>
            <th width="40px;">Line</th>
            <th width="40px;">Company</th>
		    <th  width="110px;">PO Number</th>
            <th  width="110px;">Supplier</th>
            <th width="130px;">Order Date</th>
            <th width="110px">Receiver</th>
	        <th width="100px;">Receive Address</th>
            <th width="100px;">Status</th>
            <th width="100px;">Remark</th>
            <th width="90px;">Operate</th>     
        </tr>
        </thead>
        <tbody>
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                    <tr>
                        <td><%# pageSize * page + Container.ItemIndex + 1 - pageSize%></td>
                        <td><%# Eval("Company")%></td>
                        <td><%# Eval("PONum")%></td>
                        <td><%# Eval("VendorName")%></td>
                        <td><%#string.Format("{0:g}",Eval("OrderDate"))%></td>
                        <td ><%# Eval("ShipName")%></td>
                        <td ><%# Eval("ShipAddress1")%></td>
                        <td ><%# Eval("CommentText")%></td>	
                        <td ><%# Eval("Approve").ToString()=="True" ? (Eval("Confirmed").ToString()=="True"?"已核准已确认":"已核准未确认" ): "已核准"%></td>
                        <td><a href="purchase_edit.aspx?action=Edit&id=<%#Eval("id")%>" class="tablelink"> <%# Eval("Approve").ToString()=="True" && Eval("Confirmed").ToString()=="False" ? "<font color =red>[确认订单]</font>" : "<font color =blue>[查看详情]</font>"%></a></td>
                       <%--<td><a href="purchase_edit.aspx?action=Edit&id=<%#Eval("id")%>" class="tablelink"> 处理订单</a> --%>
                       <%--&nbsp;&nbsp;<asp:LinkButton ID="lbtnDelCa" runat="server" CommandArgument='<%# Eval("id")%>' OnClientClick="return confirm('是否真的要取消该订单吗？')" onclick="lbtnDelCa_Click"><%# Eval("Response").ToString() != "W" ? "<font color =red>[取消订单]</font>" : ""%></asp:LinkButton>--%>
 
        
                        </td>
                    </tr> 		
 	               </ItemTemplate>
            <FooterTemplate>
              <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"8\"><font color=red>No record</font></td></tr>" : ""%>
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

