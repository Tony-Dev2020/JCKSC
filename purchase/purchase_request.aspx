<%@ Page Language="C#" AutoEventWireup="true" CodeFile="purchase_request.aspx.cs" Inherits="purchase_purchase_request" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Orders</title>
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
    <li><a href="#">RQF</a></li>
    </ul>
    </div>  
    <div class="rightinfo">
    <dl class="seachform"> 
    <dd><label>Order No.</label><span class="single-select"><asp:TextBox ID="txtNote_no" runat="server" Width="120" CssClass="scinput"></asp:TextBox></span></dd>
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
            <asp:ListItem Value="" Selected="True">==All==</asp:ListItem>
            <asp:ListItem Value="1">等待回复</asp:ListItem>
            <asp:ListItem Value="2">已回复</asp:ListItem>

          </asp:DropDownList>
    </span>
    </dd>

      <dd class="cx"><asp:Button ID="lbtnSearch" runat="server" CssClass="scbtn" onclick="btnSearch_Click" Text="Search"></asp:Button>   
 </dd>
     <dd class="toolbar1"  style="visibility:hidden">
        <asp:LinkButton ID="btnExport" runat="server" CssClass="save" onclick="btnExport_Click">   <li><span><img src="../images/t04.png" /></span>导出Execl</li></asp:LinkButton>
        </dd>
    </dl>
            <!--列表-->

	  <table class="imgtable">
    	<thead>
    	<tr>
            <th width="40px;">No</th>
            <th width="40px;">Company</th>
		    <th  width="60px;">RQF No</th>
            <th  width="60px;">Line</th>
            <th  width="110px;">Supplier</th>
            <th width="8%">Satsu</th>
	        <th width="100px;">Part No</th>
		    <th width="200px;">Part Desc</th>
            <th width="100px;">Qty</th>
		    <th width="130px;">Order Date</th>
            <th width="90px;">Operate</th>     
        </tr>
        </thead>
        <tbody>
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                    <tr>
                        <td><%# pageSize * page + Container.ItemIndex + 1 - pageSize%></td>
                        <td><%# Eval("Company")%></td>
                        <td><%# Eval("RFQNum")%></td>
                        <td><%# Eval("RFQLine")%></td>
                        <td><%# Eval("VendorName")%></td>
                        <td ><%# Eval("Response").ToString()=="W"? "等待回复" :"已回复"%></td>
                        <td ><%# Eval("PartNum")%></td>
                        <td ><%# Eval("LineDesc")%></td>
                        <td ><%# Eval("QuoteNum")%></td>	
                        <td><%#string.Format("{0:g}",Eval("RFQDate"))%></td>
                        <td><a href="purchase_request_edit.aspx?action=Edit&id=<%#Eval("RFQVendorID")%>&qfqvendorid=<%#Eval("RFQVendorID")%>" class="tablelink"><%# Eval("Response").ToString()=="W" ? "<font color =red>[回复询价单]</font>" : "<font color =blue>[查看回复详情]</font>"%></a> 
                       <%--&nbsp;&nbsp;<asp:LinkButton ID="lbtnDelCa" runat="server" CommandArgument='<%# Eval("id")%>' OnClientClick="return confirm('是否真的要取消该订单吗？')" onclick="lbtnDelCa_Click"><%# Eval("Response").ToString() != "W" ? "<font color =red>[取消订单]</font>" : ""%></asp:LinkButton>--%>
 
        
                        </td>
                    </tr> 		
 	               </ItemTemplate>
            <FooterTemplate>
              <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"8\"><font color=red>暂无记录</font></td></tr>" : ""%>
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
