
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="kit_view2.aspx.cs" Inherits="depotmanager_kit_view2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>产品明细</title>
<script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../js/swfupload/swfupload.js"></script>
<script type="text/javascript" src="../js/swfupload/swfupload.queue.js"></script>
<script type="text/javascript" src="../js/swfupload/swfupload.handlers.js"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<script type="text/javascript" src="../js/pinyin.js"></script>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery.js"></script>
<script type="text/javascript" src="../js/cart.js"></script>
<script type="text/javascript" src="../js/jquery.autocomplete.min.js"></script>
<link rel="Stylesheet" href="../css/jquery.autocomplete.css" />
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();

        //初始化上传控件
        $(".upload-img").each(function () {
            $(this).InitSWFUpload({ sendurl: "../tools/upload_ajax.ashx", flashurl: "../js/swfupload/swfupload.swf" });
        });
 
        //设置封面图片的样式
        $(".photo-list ul li .img-box img").each(function () {
            if ($(this).attr("src") == $("#hidFocusPhoto").val()) {
                $(this).parent().addClass("selected");
            }
        });

        //设置封面图片的样式
        $(".photo-list ul li .img-box img").each(function () {
            if ($(this).attr("src") == $("#hidFocusPhoto").val()) {
                $(this).parent().addClass("selected");
            }
        });


    });
    function SelPart(obj) {

        if (obj.value.length >= 2) {
            var postData = { "searchcontent": obj.value , compamy: $("#ddldepot_category_id").val() };
            $.ajax({
                type: "post",
                url: "../tools/search_ajax.ashx?searchtype=part",
                data: postData,
                dataType: "json",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('尝试发送失败，错误信息' + errorThrown);
                },
                success: function (data, textStatus) {
                    $('#txtPartNo').autocomplete(data, {
                        max: 12,    //列表里的条目数
                        minChars: 0,    //自动完成激活之前填入的最小字符
                        width: 400,     //提示的宽度，溢出隐藏
                        scrollHeight: 300,   //提示的高度，溢出显示滚动条
                        matchContains: true,    //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                        autoFill: false,    //自动填充
                        formatItem: function (row, i, max) {
                            return i + '/' + max + ':  "' + row.productno + '"  [' + row.productname + ']';
                        },
                        formatMatch: function (row, i, max) {
                            return row.productno + row.productname;
                        },
                        formatResult: function (row) {
                            return row.productno;
                        }
                    }).result(function (event, row, formatted) {
                        $('#txtPartDesc').val(row.productname);
                    });
                }
            });
        }
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="tab-content">

  

    <table  class="cart_table" cellspacing="100" >
        <tr>
            <td width="20%" align="right">产品型号</td>
            <td><asp:TextBox ID="txtKitNumber" runat="server" MaxLength="200" CssClass="input normal" datatype="*"  errormsg=""  Width="300"></asp:TextBox>
            </td>
             <td rowspan="2">
                <asp:Image ID="imgPhoto" runat="server" Height="118px" Width="131px" Visible="false" />
              </td>
            
        </tr> 
        <tr>
            <td width="20%" align="right">产品名称</td>
            <td>
                <asp:TextBox ID="txtKitName" runat="server" MaxLength="200" CssClass="input normal" datatype="*"  errormsg=""  Width="300"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
            <td></td>
             <td rowspan="2">
                <a href="javascript:void(0);" onclick="alert('已成功加入购物车.')" class= "add"></a>
              </td>
        </tr> 
    </table>
    <table width="100%" border="0" align="center" cellpadding="8" cellspacing="0" class="cart_table">
      <tr> 
       <th width="40" align="left">图片</th>
       <th width="100" align="left">组件编码</th>
       <th width="230" align="left">组件描述</th>
       <th width="100" align="left">规格型号</th>
       <th width="80" align="left">颜色</th> 
       <%--<th width="100" align="left">产品型号</th>--%>
       <%--<th width="80" align="left">单价</th>--%>
       <th width="80" align="left">数量</th>
        <th width="80" align="center">可否定制</th>
       
       <th width="80">配置说明</th>
       <th width="100" align="left">备注</th>
       <th width="30">选择</th>
        
      </tr>

      <asp:Repeater ID="rptList" runat="server">
        <ItemTemplate> 
            <tr>
                <td class="imgtd"><img src="<%# Eval("product_url")%>" width="40" height="40" onMouseOut="toolTip()" />
                <td><%# Eval("commercialStyle").ToString()=="" ? Eval("partnumber").ToString() :Eval("commercialStyle").ToString()%></td>	
                <td><%# Eval("UD_ProdName_c").ToString()=="" ? Eval("partdesc").ToString() :Eval("UD_ProdName_c").ToString()%><%--<%# Eval("partdesc")%>--%></td>	

                <td><%# Eval("specification")%></td>
                <td><%# Eval("commercialcolor")%></td>    
                <%--<td><%# Eval("commercialStyle")%></td>--%>	
                <%--<td><%# MyConvert(Eval("unitprice"))%></td>	--%>
                <td><%# MyConvert(Eval("qty"))%></td>
                <td align="center"><%# Eval("iscust").ToString() == "True" ? "是" : "否" %></td>
                	
                <td></td>
                <td><%# Eval("remark")%></td>   
                <%--<td><a href="javascript:void(0);"<%# Eval("part_id").ToString().Trim() != "-2" ? "class=add onclick=\"CartAdd2(this, '/', 0, 'shopping.aspx?action=cart','"+ Eval("part_id")+"','1');\" " : "class=add-over"%> ></a></td>--%>
                 <td><input type="checkbox" class="add" <%# Eval("part_id").ToString().Trim() != "-2" ? "onclick=\"CartAddKit(this, '/', 0, 'shopping.aspx?action=cart','"+ Eval("part_id") +"','1-"+ Eval("part_id") +"');\" ": "class=add-over"%> ></td>
            </tr>
            
        </ItemTemplate>
    </asp:Repeater>
   </table>


</div>
    </form>
</body>
</html>


