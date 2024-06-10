<%@ Page Language="C#" AutoEventWireup="true" CodeFile="kit_view.aspx.cs" Inherits="depotmanager_kit_view" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>套件详情</title>
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
  	<div class="place">
    <span>位置：</span>
    <ul class="placeul">
    <li><a href="../home.aspx">首页</a></li>
    <li><a href="#">套件</a></li>
    </ul>
    </div> 
    <div class="formbody">  
    <div id="usual1" class="usual">   
    <div class="itab">
  	<ul> 
    <li><a href="kit_view.aspx" class="selected">套件详情</a></li> 

  	</ul>
    </div>  
 <!--增加新品信息-->
 
<div class="tab-content">

    <dl>
    <dt>公司</dt>
    <dd > 
    <span class="rule-single-select" style="vertical-align:top">
   <asp:DropDownList id="ddldepot_category_id" runat="server" datatype="*" errormsg="请选择公司" sucmsg=" " ></asp:DropDownList>  
    </span>
    <span class="Validform_checktip">*</span>
        <asp:Image ID="imgPhoto" runat="server" Height="118px" Width="131px" Visible="false" /> 
     </dd>
  </dl>
     
    <dl >
    <dt>套件编码</dt>
    <dd><asp:TextBox ID="txtKitNumber" runat="server"  MaxLength="200" CssClass="input normal" datatype="*"  errormsg=""  Width="300"></asp:TextBox>
    <span class="Validform_checktip">*</span><asp:TextBox ID="txtKitID" runat="server" Visible="false"></asp:TextBox>
    </dd>
  </dl>
    <dl >
    <dt>套件名称</dt>
    <dd><asp:TextBox ID="txtKitName" runat="server"  MaxLength="200" CssClass="input normal" datatype="*"  errormsg="" Width="300"></asp:TextBox>
    <span class="Validform_checktip">*</span>
    </dd>
  </dl>
  
  <dl>
    <dt>计量单位</dt>
    <dd>   <asp:TextBox ID="txtdw" runat="server" CssClass="input small"  MaxLength="10" datatype="*" sucmsg=""  ></asp:TextBox>
      <span class="Validform_checktip">*</span></dd>
  </dl> 
    <dl >
        <dt> 销售单价</dt>
         <dd><asp:TextBox ID="txtsalse_price" runat="server" CssClass="input small" onkeyup="clearNoNum(this)"  MaxLength="8" datatype="*" sucmsg="" errormsg="请输入正确的金额"></asp:TextBox>&nbsp;&nbsp;元
          <span class="Validform_checktip">*</span></dd>
      </dl>
    <dl>
        <dt>备注</dt>
            <dd><asp:TextBox ID="txtremark" runat="server" CssClass="input normal" ></asp:TextBox></dd>
      </dl>
    
    <dl>
    
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
               
            </tr>
            
        </ItemTemplate>
    </asp:Repeater>
   </table>
    </dl>

     <dl style="visibility:hidden">
    <dt>进货单价</dt>
    <dd>   <asp:TextBox ID="txtgo_price" runat="server" CssClass="input small" onkeyup="clearNoNum(this)"  MaxLength="8"  sucmsg="" errormsg="请输入正确的金额" ></asp:TextBox>&nbsp;&nbsp;元
   <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input normal upload-path" />
        <asp:TextBox ID="txtproduct_num" runat="server" CssClass="input small"  sucmsg=" "></asp:TextBox>
  </dl> 

</div>

         
<!--/增加新品信息-->    
    </div>
    <!--工具栏-->
<div class="page-footer">
  <div class="btn-list">
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
  <div class="clear"></div>
</div>

        
<!--/工具栏-->

    </form>
</body>
</html>


