<%@ Page Language="C#" AutoEventWireup="true" CodeFile="kit_edit.aspx.cs" Inherits="depotmanager_kit_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>增加新品</title>
<script type="text/javascript" src="../js/swfupload/swfupload.js"></script>
<script type="text/javascript" src="../js/swfupload/swfupload.queue.js"></script>
<script type="text/javascript" src="../js/swfupload/swfupload.handlers.js"></script>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery.js"></script>
<link rel="Stylesheet" href="../css/jquery.autocomplete.css" />
<script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<script type="text/javascript" src="../js/pinyin.js"></script>
<script type="text/javascript" src="../js/jquery.autocomplete.min.js"></script>


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

        $("#btnSelPart").click(function () {
            var dialog = $.dialog({
                title: '选择组件',
                content: 'url:../select/part_select.aspx?Company=' + $("#ddldepot_category_id").val() + '',
                min: false,
                max: false,
                lock: true,
                width: 1200,
                top: 200,
            });

        });

    });
    function SelPart(obj) {

        if (obj.value.length >= 1) {
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
                        max: 200,    //列表里的条目数
                        minChars: 1,    //自动完成激活之前填入的最小字符
                        width: 400,     //提示的宽度，溢出隐藏
                        scrollHeight: 300,   //提示的高度，溢出显示滚动条
                        matchContains: true,    //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                        autoFill: false,    //自动填充
                        formatItem: function (row, i, max) {
                            return i + '/' + max + ':  "' + row.productno + '"  [' + row.productname + ']' + ' [' + row.productdesc + ']';
                        },
                        formatMatch: function (row, i, max) {
                            return row.productno + row.productname + row.productdesc + row.commercialstyle;
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
    <li><a href="kit_add.aspx" class="selected">套件</a></li> 

  	</ul>
    </div>  
 <!--增加新品信息-->
 
<div class="tab-content">

    <dl>
    <dt>公司</dt>
    <dd> 
    <span class="rule-single-select">
    <asp:DropDownList id="ddldepot_category_id" runat="server" datatype="*" errormsg="请选择公司" sucmsg=" " ></asp:DropDownList>  
    </span>
    <span class="Validform_checktip">*</span>
     </dd>
  </dl>

    <dl>
    <dt>产品系列</dt>
    <dd> 
    <span class="rule-single-select">
    <asp:DropDownList id="ddlproduct_series_id" runat="server" datatype="*" errormsg="请选择公司" sucmsg=" " ></asp:DropDownList>  
    </span>
    <span class="Validform_checktip">*</span>
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

   <dl >
    <dt>上传商品图片</dt>
    <dd style="vertical-align:top"><asp:Image ID="imgPhoto" runat="server" Height="118px" Width="131px" Visible="false" /> </dd> 
      <dd style="vertical-align:top"> <asp:FileUpload ID="FileUpload1"  CssClass="input normal upload-path" accept=".jpg,.gif,.png,.bmp"  runat="server" />
    </dd>
   
  </dl>

  <dl >
    <dt>网络图片</dt>
    <dd><asp:TextBox ID="txtImgUrl" runat="server"  CssClass="input normal upload-path" /></dd>
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

   <dl >
    <dt>是否启用</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsLock" runat="server" Checked="True" />
      </div>
      <span class="Validform_checktip">*不启用该套件将无法销售</span>
    </dd>
  </dl>

    <dl>
        <dt>备注</dt>
            <dd><asp:TextBox ID="txtremark" runat="server" CssClass="input normal" ></asp:TextBox></dd>
      </dl>

 

    <dl>
    
    <table width="1150" border="0" align="center" cellpadding="9" cellspacing="0" class="cart_table">
     
      <tr> 
       <th width="250" align="left">组件编码</th>
       <th width="300" align="left">组件描述</th>
       <th width="60" align="center">单价</th>
       <th width="60" align="center">数量</th>
       <th width="80" align="left">可否定制</th>
       <th width="200" align="center">备注</th>
       <th width="60" colspan="2" align="center">操作</th>
      </tr>

       <tr> 
       <%--<th width="150" align="left"><asp:TextBox ID="txtPartNo"  CssClass="input normal" oninput="SelPart(this)"   runat="server" Width="100" ></asp:TextBox></th>--%>
       <th width="250" align="left"><asp:TextBox ID="txtPartNo"  CssClass="input normal" runat="server" Width="80" ></asp:TextBox><input id="btnSelPart" runat="server"  type="button" class="btn green"   value="选择" /> </th> 
       <th width="300" align="left"><asp:TextBox ID="txtPartDesc"  CssClass="input normal" runat="server" Width="300" ></asp:TextBox></th>
       <th width="60" align="center"><asp:TextBox ID="txtPartUnitPrice"  CssClass="input small" onkeyup="clearNoNum(this)" Text="0" MaxLength="8" sucmsg="" errormsg="请输入正确的金额" runat="server" Width="60" ></asp:TextBox></th>
       <th width="60" align="center"><asp:TextBox ID="txtPartQty" CssClass="input small" onkeyup="clearNoNum(this)" Text="1"  MaxLength="8" sucmsg="" errormsg="请输入正确的金额" runat="server" Width="60" ></asp:TextBox></th>
       <th width="80" align="center"><asp:CheckBox ID="cbIsCust" Width="60" runat="server"></asp:CheckBox></th>
       <th width="200" align="center"><asp:TextBox ID="txtPartRemarks" CssClass="input normal" runat="server" Width="200" ></asp:TextBox></th>
       <th width="30" align="center" colspan="2"><asp:Button ID="btnAddKit" runat="server" Text="保存组件 " onclick="btnAddKit_Click" class="btn"  Width="80" /></th>  
      </tr>
      <asp:Repeater ID="rptList" runat="server">
        <ItemTemplate> 
            <tr>
                <td width="250"><%# Eval("partnumber")%></td>	
                <td width="300"><%# Eval("partdesc")%></td>	
                <td width="60"><%# MyConvert(Eval("unitprice"))%></td>	
                <td width="60"><%# MyConvert(Eval("qty"))%></td>
                <td width="80" align="center"><%# Eval("isCust").ToString()=="True" ? "<input type=\"checkbox\" name=\"cbIsCustDetail\" checked >" :"<input type=\"checkbox\" name=\"cbIsCustDetail\" >" %></td>
                <td width="200"><%# Eval("remark")%></td>
                <td ><asp:LinkButton ID="lbtnUpdateKit" runat="server"  class="btn yellow" Text="更新" Width="30" CommandArgument='<%# Eval("id")%>'  onclick="lbtnUpdateKit_Click" ></asp:LinkButton></td>	
                <td ><asp:LinkButton ID="lbtnDelKit" runat="server" class="btn"  Width="30" CommandArgument='<%# Eval("id")%>'  OnClientClick="return confirm('是否真的要删除？')" onclick="lbtnDelKit_Click" ><font color ="red">删除</font></asp:LinkButton></td>
            </tr>
           </ItemTemplate>
        </asp:Repeater>
   </table>
    </dl>

     <dl style="visibility:hidden">
    <dt>进货单价</dt>
    <dd>   <asp:TextBox ID="txtgo_price" runat="server" CssClass="input small" onkeyup="clearNoNum(this)"  MaxLength="8"  sucmsg="" errormsg="请输入正确的金额" ></asp:TextBox>&nbsp;&nbsp;元
            <asp:TextBox ID="txtImgUrl2" runat="server" CssClass="input normal upload-path" />
        <asp:TextBox ID="txtproduct_num" runat="server" CssClass="input small"  sucmsg=" "></asp:TextBox>
  </dl> 


         
<!--/增加新品信息-->    
    </div>
    <!--工具栏-->
<div class="page-footer">
  <div class="btn-list">
    <asp:Button ID="btnSubmit" runat="server" Text="保存套件" CssClass="btn" onclick="btnSubmit_Click"  />
    <asp:Button ID="btnInActive" runat="server" Text="停用套件" CssClass="btn violet" Visible="false" OnClientClick="return confirm('是否要设置为停用？设置后客户不可购买！')" onclick="btnInActive_Click"  />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
  <div class="clear"></div>
</div>

        
<!--/工具栏-->

    </form>
</body>
</html>

