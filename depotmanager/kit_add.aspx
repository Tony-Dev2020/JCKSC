<%@ Page Language="C#" AutoEventWireup="true" CodeFile="kit_add.aspx.cs" Inherits="depotmanager_kit_add" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>增加新品</title>
<script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../js/swfupload/swfupload.js"></script>
<script type="text/javascript" src="../js/swfupload/swfupload.queue.js"></script>
<script type="text/javascript" src="../js/swfupload/swfupload.handlers.js"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<script type="text/javascript" src="../js/pinyin.js"></script>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
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

</script>
</head>
<body>
    <form id="form1" runat="server">
  	<div class="place">
    <span>位置：</span>
    <ul class="placeul">
    <li><a href="../home.aspx">首页</a></li>
    <li><a href="#">新增套件</a></li>
    </ul>
    </div> 
    <div class="formbody">  
    <div id="usual1" class="usual">   
    <div class="itab">
  	<ul> 
    <li><a href="kit_add.aspx" class="selected">新增套件</a></li> 

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
     
    <dl >
    <dt>套件编码</dt>
    <dd><asp:TextBox ID="txtKitNumber" runat="server"  MaxLength="200" CssClass="input normal" datatype="*"  errormsg="" ajaxurl="../tools/admin_ajax.ashx?action=code_validate" Width="300"></asp:TextBox>
    <span class="Validform_checktip">*</span>
    </dd>
  </dl>
    <dl >
    <dt>套件名称</dt>
    <dd><asp:TextBox ID="txtKitName" runat="server"  MaxLength="200" CssClass="input normal" datatype="*"  errormsg="" ajaxurl="../tools/admin_ajax.ashx?action=code_validate" Width="300"></asp:TextBox>
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
    
    <table width="938" border="0" align="center" cellpadding="8" cellspacing="0" class="cart_table">
      <tr> 
       <th width="100" align="left">组件编码</th>
       <th width="300" align="left">组件描述</th>
       <th width="60" align="center">单价</th>
       <th width="60" align="center">数量</th>
       <th width="200" align="center">备注</th>
       <th width="100" align="center">操作</th>
      </tr>
       <tr> 
       <th width="300" align="left"><asp:TextBox ID="txtPartNo"  CssClass="input normal"  runat="server" Width="100" ></asp:TextBox></th>
       <th width="300" align="left"><asp:TextBox ID="txtPartDesc"  CssClass="input normal" runat="server" Width="300" ></asp:TextBox></th>
       <th width="60" align="center"><asp:TextBox ID="txtPartQty"  CssClass="input small" onkeyup="clearNoNum(this)"  MaxLength="8" sucmsg="" errormsg="请输入正确的金额" runat="server" Width="60" ></asp:TextBox></th>
       <th width="60" align="center"><asp:TextBox ID="txtPartUnitPrice" CssClass="input small" onkeyup="clearNoNum(this)"  MaxLength="8" sucmsg="" errormsg="请输入正确的金额" runat="server" Width="60" ></asp:TextBox></th>
       <th width="200" align="center"><asp:TextBox ID="txtPartRemarks" CssClass="input normal" runat="server" Width="200" ></asp:TextBox></th>
       <th width="100" align="center"><asp:Button ID="btnAdd" runat="server" Text="保存组件 " CssClass="btn"  /></th>
      </tr>
      <asp:Repeater ID="rptList" runat="server">
        <ItemTemplate> 
                <tr>
                <td align="center">123</a></td>
                    <td align="center">234</a></td>
                    <td align="center">456</a></td>
                    <td align="center">789</a></td>
                    <td align="center">000</a></td>
                    <td align="center">000</a></td>
                    <td align="center">000</a></td>
                    <td><font color ="green">[修改]</font></a>  &nbsp;&nbsp;<asp:LinkButton ID="lbtnDelCa" runat="server" CssClass="btn"   OnClientClick="return confirm('是否真的要删除？')" ><font color ="red">[删除]</font></asp:LinkButton></td>
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
    <asp:Button ID="btnSubmit" runat="server" Text="保存套件" CssClass="btn" onclick="btnSubmit_Click"  />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
  <div class="clear"></div>
</div>

        
<!--/工具栏-->

    </form>
</body>
</html>

