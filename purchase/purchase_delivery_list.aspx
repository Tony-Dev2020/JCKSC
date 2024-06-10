<%@ Page Language="C#" AutoEventWireup="true" CodeFile="purchase_delivery_list.aspx.cs" Inherits="purchase_purchase_delivery_list" %>
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
    <span>位置:</span>
    <ul class="placeul">
    <li><a href="../home.aspx">首页</a></li>
    <li><a href="#">已送货采购订单</a></li>
    </ul>
    </div>  
    <div class="rightinfo">
    <dl class="seachform"> 
    <dd><label>关键字</label><span class="single-select"><asp:TextBox ID="txtNote_no" runat="server" Width="220" CssClass="scinput"></asp:TextBox></span></dd>   
    <dd><label>产品种类</label>  
    <span class="rule-single-select">
      <asp:DropDownList ID="ddlproduct_category_id"  runat="server" AutoPostBack="True" onselectedindexchanged="ddlproduct_category_id_SelectedIndexChanged">
          </asp:DropDownList>
    </span>
    </dd>
     <dd><label>产品系列</label>  
    <span class="rule-single-select">
      <asp:DropDownList ID="ddlproduct_series_id"  runat="server" AutoPostBack="True" onselectedindexchanged="ddlproduct_series_id_SelectedIndexChanged">
          </asp:DropDownList>
    </span>
    </dd>

       <dd class="cx"><asp:Button ID="lbtnSearch" runat="server" CssClass="scbtn" onclick="btnSearch_Click" Text="查询"></asp:Button>   
    </dd>
     <dd class="toolbar1">
     <li><span><img src="../images/t04.png" /></span><a href="shopping.aspx">建立送货单&nbsp;<%--<font color=red><script type="text/javascript" src="../tools/submit_ajax.ashx?action=view_cart_count"></script></font>&nbsp;种--%></a></li>
        </dd>
</dl>
            <!--列表-->

	  <table class="imgtable">
    	<thead>
    	<tr>
            <th width="50px;">序号</th>
            <th width="80px;">公司</th>
            <th width="100px;">供应商</th>
		    <th  width="80px;">PO号</th>
            <th width="80px;">PO行号</th>
            <th width="120px">物料编码</th>
	        <th width="150px;">物料描述</th>
            <th width="100px;">订单数量</th>
            <th width="100px;">到期日</th>
            <th width="100px;" >已送货数量</th> 
        </tr>
        </thead>
        <tbody>
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%# pageSize * page + Container.ItemIndex + 1 - pageSize%></td>
                    <td><%# Eval("Company")%></td>
                    <td><%# Eval("VendorName")%></td>
                    <td><%# Eval("PONum")%></td>
                    <td><%# Eval("POLine")%></td>
                    <td><%# Eval("PARTNUM")%></td>
                    <td><%# Eval("LineDesc")%></td>
                    <td align="right"><%# String.Format( "{0:N2}", Eval("OrderQty"))%></td>
                    <td><%#string.Format("{0:d}",Eval("DueDate"))%></td>
                    <td align="right"><%# String.Format( "{0:N2}", Eval("OrderQty"))%></td>
                    <%--<td><a href="purchase_edit.aspx?action=Edit&id=<%#Eval("id")%>" class="tablelink"> <%# Eval("Approve").ToString()=="True" && Eval("Confirmed").ToString()=="False" ? "<font color =red>[确认订单]</font>" : "<font color =blue>[查看详情]</font>"%></a></td>--%>
                        
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

