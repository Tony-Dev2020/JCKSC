<%@ Page Language="C#" AutoEventWireup="true" CodeFile="purchase_request_edit.aspx.cs" Inherits="purchase_purchase_request_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <title>编辑订单信息</title>
<script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<script type="text/javascript" src="../js/pinyin.js"></script>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
    });

</script>
</head>
<body>
    <form id="form1" runat="server">
	<div class="place">
    <span>位置：</span>
    <ul class="placeul">
    <li><a href="../home.aspx">供应商门户</a></li>
    <li><a href="purchase_request.aspx">报价单</a></li>
    <li><a href="#">报价单回复</a></li>
    </ul>
    </div>

    <div class="formbody">   
    <div class="formtitle"><span>报价单回复</span></div>
    <!--/商品信息-->
<div class="tab-content">
   <dl>
    <dt>询价单号</dt>
     <dd>   <asp:Literal ID="litRFQNum" runat="server"></asp:Literal>
     </dd>
   </dl>

   <dl>
    <dt>询价日期</dt>
     <dd>   <asp:Literal ID="litRFQDate" runat="server"></asp:Literal>
     </dd>
   </dl>
   
    <dl>
    <dt>询价信息</dt>
     <dd>   <asp:Literal ID="litRFQcomment" runat="server"></asp:Literal>
     </dd>
   </dl>

   <%--<dl>
    <dt>入库数量</dt>
     <dd>   <asp:TextBox ID="txtproduct_num" runat="server" CssClass="input small" datatype="n" sucmsg=" "></asp:TextBox>&nbsp;&nbsp;<%=dw %>
      <span class="Validform_checktip">*</span></dd>
    </dl>--%>
    <dl>
    <dt>回复单价</dt>
     <dd> <asp:TextBox ID="txtVendorUnitPrice" runat="server" CssClass="input small" onkeyup="clearNoNum(this)"  
         MaxLength="8" sucmsg="" errormsg="请输入正确的金额" ></asp:TextBox>
      </dd>
  </dl>
    <dl>
    <dt>回复内容</dt>
     <dd>
          <asp:TextBox ID="txtVendorRemark" class="input" runat="server" Height="84px" 
                          TextMode="MultiLine" Width="200px"  sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*</span></dd>
  </dl>

    <dl>
    <dt>商品类别</dt>
    <dd> 

   <asp:Literal id="ddlproduct_category_id" runat="server"></asp:Literal>  

     </dd>
  </dl>

    <dl >
    <dt>商品名称</dt>
    <dd><asp:Literal ID="txtproduct_name" runat="server"></asp:Literal>

    </dd>
  </dl>
  <dl>
    <dt>商品图片</dt>
    <dd>
        <asp:Image ID="txtImgUrl" runat="server" Height="150px" Width="150px" />   
    </dd>
  </dl>
  <dl>

    <dl>
    <dt><%--<asp:Label  ID="Label1" runat="server" Text="报价附档" class="btn"/>--%>报价附档</dt>
    <dd>
        <asp:FileUpload ID="FileUploadQuote" runat="server" CssClass="input date"  /><asp:HiddenField ID="txtQuoteFile1Name" runat="server" />
    </dd>
  </dl>
  <dl>

      
            

    <dd>  
        <asp:TextBox ID="txtproduct_num" runat="server" Visible="false" CssClass="input small" datatype="n" sucmsg=" "></asp:TextBox>
        <asp:Literal ID="Litproduct_num" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="txtgo_price" Visible="false" runat="server" ></asp:Literal>
         <asp:Literal ID="txtsalse_price" Visible="false" runat="server"></asp:Literal>
  </dd>
  </dl> 

   
 
</div>
<!--/商品信息-->    
    </div>

    <!--工具栏-->
<div class="page-footer">
  <div class="btn-list">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click"  />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
  <div class="clear"></div>
</div>
<!--/工具栏-->

    </form>
</body>
</html>


