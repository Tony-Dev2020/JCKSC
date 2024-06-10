<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_edit.aspx.cs" Inherits="select_order_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑订单信息</title>
    <script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<script type="text/javascript" src="../js/pinyin.js"></script>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery.autocomplete.min.js"></script>
<link rel="Stylesheet" href="../css/jquery.autocomplete.css" />
<style type="text/css">
    #loading{position:fixed;_position:absolute;top:50%;left:50%;width:120px;height:120px;overflow:hidden;background:url(../images/loading.gif) no-repeat;z-index:10;display:none;}
  </style>
<script type="text/javascript">
    $(function () {
        $("#btnConfirm").click(function () { OrderConfirm(); });   //确认订单
        $("#btnUpdateStatusToConfirmQuote").click(function () { OrderQuote(); });   //更改订单为等待确认报价
        
        $("#btnComplete").click(function () { OrderComplete(); }); //完成订单
        $("#btnCancel").click(function () { OrderCancel(); });     //取消订单
        $("#btnPrint").click(function () { OrderPrint(); });       //打印订单



        //$("#btnDesign").click(function () { OrderDesign(); });   //设计订单
        //$("#btnConfirmDesign").click(function () { OrderConfirmDesign(); });   //确认设计订单
        //$("#btnQuote").click(function () { OrderQuote(); });   //报价订单
        $("#btnConfirmQuote").click(function () { OrderConfirmQuote(); });   //确认报价订单
        //$("#btnPay").click(function () { OrderPay(); });   //汇款订单
        $("#btnConfirmPay").click(function () { OrderConfirmPay(); });   //确认收款订单
      

        $("#btnEditRemark").click(function () { EditOrderRemark(); });    //修改订单备注
        $("#btnEditPaymentFee").click(function () { EditPaymentFee(); });  //调价

        $("#btnKuJialLeSelect").click(function () {
            var dialog = $.dialog({
                title: '选择酷家乐方案',
                content: 'url:../select/kujiale_select.aspx?selType=sel1',
                min: false,
                max: false,
                lock: true,
                width: 1200,
                top: 200,
            });

        });

    });

    //确认订单
    function OrderConfirm() {
        var dialog = $.dialog.confirm('确认订单后将无法修改金额，确认要继续吗？', function () {
            var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_confirm" };
            //发送AJAX请求
            sendAjaxUrl(dialog, postData, "../tools/admin_ajax.ashx?action=edit_order_status");
            return false;
        });
    }


    //确认订单2
    function OrderConfirm2(order_no) {
        var dialog = $.dialog.confirm('确认订单后将无法修改金额，确认要继续吗？', function () {
            var postData = { "order_no": order_no, "edit_type": "order_confirm" };
            //发送AJAX请求
            sendAjaxUrl(dialog, postData, "../tools/admin_ajax.ashx?action=edit_order_status");
            return false;
        });
    }

    //设计订单
    function OrderDesign() {
        var dialog = $.dialog.confirm('将操作订单标记为已设计，要继续吗？', function () {
            var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_design" };
            //发送AJAX请求
            sendAjaxUrl(dialog, postData, "../tools/admin_ajax.ashx?action=edit_order_status");
            return false;
        });
    } txtPayAmount
   
    //确认设计订单
    function OrderConfirmDesign() {
        var dialog = $.dialog.confirm('将操作订单标记为已确认设计，要继续吗？', function () {
            dialog.hide();
            $("#loading").show();//显示loading
            var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_confirm_design" };
            //发送AJAX请求
            sendAjaxUrl(dialog, postData, "../tools/admin_ajax.ashx?action=edit_order_status");
            //$("#loading").hide();//隐藏loading
            return false;
         
        });
    }

    //报价订单
    function OrderQuote() {
        var dialog = $.dialog.confirm('将操作订单标记为已报价，要继续吗？', function () {
            var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_quote" };
            //发送AJAX请求
            sendAjaxUrl(dialog, postData, "../tools/admin_ajax.ashx?action=edit_order_status");
            return false;
        });
    }

    //确认报价订单
    function OrderConfirmQuote() {
        var dialog = $.dialog.confirm('将操作订单标记为已确认报价，要继续吗？', function () {

            var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_confirm_quote" };
            //发送AJAX请求
            sendAjaxUrl(dialog, postData, "../tools/admin_ajax.ashx?action=edit_order_status");
            return false;
        });
    }

    //汇款订单
    function OrderPay() {
        var dialog = $.dialog.confirm('将操作订单标记为已汇款，要继续吗？', function () {
            var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_pay" };
            //发送AJAX请求
            sendAjaxUrl(dialog, postData, "../tools/admin_ajax.ashx?action=edit_order_status");
            return false;
        });
    }

    //确认报价订单
    function OrderConfirmPay() {
        var dialog = $.dialog.confirm('将操作订单标记为已确认收款并产生Epicor销售单，要继续吗？', function () {
            var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_confirm_pay" };
            //发送AJAX请求
            sendAjaxUrl(dialog, postData, "../tools/admin_ajax.ashx?action=edit_order_status");
            return false;
        });
    }

    //完成订单
    function OrderComplete() {
        var dialog = $.dialog.confirm('订单完成后，说明产品已经送到客户手里，订单完成，确认要继续吗？', function () {
            var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_complete" };
            //发送AJAX请求
            sendAjaxUrl(dialog, postData, "../tools/admin_ajax.ashx?action=edit_order_status");
            return false;
        });
    }
    //取消订单
    function OrderCancel() {
        var dialog = $.dialog({
            title: '取消订单',
            content: '操作提示信息：<br />1、请与下单经销商沟通；<br />2、取消订单后该订单不做财务收入统计；<br />3、请单击相应按钮继续下一步操作！',
            min: false,
            max: false,
            lock: true,
            icon: 'confirm.gif',
            button: [{
                name: '确认取消',
                callback: function () {
                    var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_cancel", "check_revert": 0 };
                    //发送AJAX请求
                    sendAjaxUrl(dialog, postData, "../tools/admin_ajax.ashx?action=edit_order_status");
                    return false;
                }
            }, {
                name: '关闭'
            }]
        });

    } Panel
 
    //打印订单
    function OrderPrint() {
        var dialog = $.dialog({
            title: '打印订单',
            content: 'url:../dialog/dialog_print.aspx?order_no=' + $("#spanOrderNo").text(),
            min: false,
            max: false,
            lock: true,
            width: 1080//,
            //height: 500
        });
    }
   
    //修改订单备注
    function EditOrderRemark() {
        var dialog = $.dialog({
            title: '订单备注',
            content: '<textarea id="txtOrderRemark" name="txtOrderRemark" rows="2" cols="20" class="input">' + $("#divRemark").html() + '</textarea>',
            min: false,
            max: false,
            lock: true,
            ok: function () {
                //var remark2 = $("#txtOrderRemark", parent.document).val();
                var remark = $("#txtOrderRemark").val();
                if (remark == "") {
                    $.dialog.alert('对不起，请输入订单备注内容！' + remark, function () { }, dialog);
                    return false;
                }
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "edit_order_remark", "remark": remark };
                //发送AJAX请求
                sendAjaxUrl(dialog, postData, "../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            },
            cancel: true
        });
    }


    //调价
    function EditPaymentFee() {
        var pop = $.dialog.prompt('请调整产品实际总金额',
            function (val) {
                if (!checkIsMoney(val)) {
                    $.dialog.alert('对不起，请输入正确的产品实际总金额！', function () { }, pop);
                    return false;
                }
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "edit_payment_fee", "payment_fee": val };
                //发送AJAX请求
                sendAjaxUrl(pop, postData, "../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            },
            $("#spanPaymentFeeValue").text()
        );
    }


    //退回
    function OrderBack() {
        var returnflag = false;
        var pop = $.dialog.prompt('请输入退回备注',
            
            function (val) {
                if (val == '') {
                    $.dialog.alert('对不起，请输入退回备注！', function () { }, pop);
                    return false;
                }
                else {
                    alert(val);
                    returnflag =  true;
                }

                
            },
        );
        alert(returnflag);
        return returnflag;
    }
    
    
    function CalcAmount2() {
        var repeaterId = '<%=rptList.ClientID %>';//Repeater的客户端ID
        var rows = <%=rptList.Items.Count%>;//Repeater的行数
        var totalamount = 0;
        var totaldiscountamount = 0;
        var totalrealamount = 0;
        for (var i = 0; i < rows; i++) {
            if (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value != '0'
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscount").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscount").value != '0'
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value != '0') {
                document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtAmount").value =
                    (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                     document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value *
                        (100 - Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscount").value)) / 100).toFixed(2);
                document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscountAmount").value =
                    (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                        document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value *
                        (Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscount").value)) / 100).toFixed(2);
                document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtFullAmount").value =
                    (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                        document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value).toFixed(2);

                totalrealamount = totalrealamount + Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtAmount").value);
                totaldiscountamount = totaldiscountamount + Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscountAmount").value).toFixed(2);
                totalamount = totalamount + Number((document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                    document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value)).toFixed(2);;
            }
            
        }
        document.getElementById("txtTotalAmount").value = totalamount;
        document.getElementById("txtTotalDiscountAmount").value = totaldiscountamount;
        document.getElementById("txtTotalRealAmount").value = totalrealamount;
   
        function getrownumber(i) {
            if (i > 10) {
                return i+1;
            }
            else {
                return '0' + i;
            }
        }
    }

    function CalcAmount() {
        var repeaterId = '<%=rptList.ClientID %>';//Repeater的客户端ID
        var rows = <%=rptList.Items.Count%>;//Repeater的行数
        var totalamount = 0;
        var totaldiscountamount = 0;
        var totalrealamount = 0;
        for (var i = 0; i < rows; i++) {
            if (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value != '0'
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscount").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscount").value != '0'
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value != '0') {
                document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtAmount").value =
                    (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                        document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value *
                        (Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscount").value)) / 100).toFixed(2);
                document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscountAmount").value =
                    (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                        document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value *
                        (100 - Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscount").value)) / 100).toFixed(2);
                document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtFullAmount").value =
                    (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                        document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value).toFixed(2);

                totalrealamount = totalrealamount + Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtAmount").value);
                totaldiscountamount = totaldiscountamount + Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscountAmount").value);
                totalamount = totalamount + Number((document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                    document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value));
            }

        }
        document.getElementById("txtTotalAmount").value = totalamount;
        document.getElementById("txtTotalDiscountAmount").value = totaldiscountamount;
        document.getElementById("txtTotalRealAmount").value = totalrealamount;

        function getrownumber(i) {
            if (i > 10) {
                return i + 1;
            }
            else {
                return '0' + i;
            }
        }
    }


    function CalcDiscount2() {
        var repeaterId = '<%=rptList.ClientID %>';//Repeater的客户端ID
        var rows = <%=rptList.Items.Count%>;//Repeater的行数
        var totalamount = 0;
        var totaldiscountamount = 0;
        var totalrealamount = 0;
        for (var i = 0; i < rows; i++) {
            if (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value != '0'
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtAmount").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtAmount").value != '0'
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value != '0') {
                document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscount").value =
                    ((Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtAmount").value))/
                    (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                        document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value) * 100).toFixed(2);
                         
                document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscountAmount").value =

                    (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                        document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value -
                        Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtAmount").value)).toFixed(2);

                document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtFullAmount").value =
                    (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                        document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value).toFixed(2);

                totalrealamount = totalrealamount + Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtAmount").value);
                totaldiscountamount = totaldiscountamount + Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscountAmount").value);
                totalamount = totalamount + Number((document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                    document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value));
            }

        }
        document.getElementById("txtTotalAmount").value = totalamount;
        document.getElementById("txtTotalDiscountAmount").value = totaldiscountamount;
        document.getElementById("txtTotalRealAmount").value = totalrealamount;

        function getrownumber(i) {
            if (i > 10) {
                return i + 1;
            }
            else {
                return '0' + i;
            }
        }
    }

    function CalcDiscount() {
        var repeaterId = '<%=rptList.ClientID %>';//Repeater的客户端ID
        var rows = <%=rptList.Items.Count%>;//Repeater的行数
        var totalamount = 0;
        var totaldiscountamount = 0;
        var totalrealamount = 0;
        for (var i = 0; i < rows; i++) {
            if (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value != '0'
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscountAmount").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscountAmount").value != '0'
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value != ''
                && document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value != '0') {
                document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscount").value =
                    100- ((Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscountAmount").value)) /
                        (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                            document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value) * 100).toFixed(2);

                document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtAmount").value =
                    (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                        document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value -
                        Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscountAmount").value)).toFixed(2);

                document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtFullAmount").value =
                    (document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                        document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value).toFixed(2);

                totalrealamount = totalrealamount + Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtAmount").value);
                totaldiscountamount = totaldiscountamount + Number(document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtDiscountAmount").value);
                totalamount = totalamount + Number((document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtQuantity").value *
                    document.getElementById(repeaterId + "_ctl" + getrownumber(i + 1) + "_txtPrice").value));
            }

        }
        document.getElementById("txtTotalAmount").value = totalamount;
        document.getElementById("txtTotalDiscountAmount").value = totaldiscountamount;
        document.getElementById("txtTotalRealAmount").value = totalrealamount;

        function getrownumber(i) {
            if (i > 10) {
                return i + 1;
            }
            else {
                return '0' + i;
            }
        }
    }
    
    //=================================工具类的JS函数====================================
    //检查是否货币格式
    function checkIsMoney(val) {
        var regtxt = /^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/;
        if (!regtxt.test(val)) {
            return false;
        }
        return true;
    }
    //发送AJAX请求
    function sendAjaxUrl(winObj, postData, sendUrl) {
        $.ajax({
            type: "post",
            url: sendUrl,
            data: postData,
            dataType: "json",
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.dialog.alert('尝试发送失败，错误信息：' + errorThrown, function () { }, winObj);
            },
            success: function (data, textStatus) {
                if (data.status == 1) {
                    winObj.close();
                    $.dialog.tips(data.msg, 2, '32X32/succ.png', function () { location.reload(); }); //刷新页面
                } else {
                    $.dialog.alert('错误提示：' + data.msg, function () { }, winObj);
                }
            }
        });
    }

  

    function downLoadFile(filepath) {
       window.open(filepath);
    }
</script>
</head>
<body>
<form id="form1" runat="server">
	<div class="place">
    <span>位置：</span>
    <ul class="placeul">
    <li><a href="../home.aspx">首页</a></li>
    <li><a href="order_list.aspx">订单管理</a></li>
    <li><a href="#">确认订单</a></li>
    </ul>
    </div>

    <div class="formbody">   
    <div class="formtitle"><span>订单详细信息</span></div>
    <!--/订单详细信息-->
<div class="tab-content2">
  <dl style="margin-left:20px;">
    <dd style="margin-left:50px;text-align:center;">
      <div class="order-flow" style="width:1200px">
        <%if (model.status < 11 && model.status != 10)
          { %>
        <div class="order-flow-left">
          <a class="order-flow-input">生成</a>
          <span><p class="name">订单已生成</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.add_time)%></p></span>
        </div>
        


<%--        <%if (model.payment_status == 0 && model.status == 1)
          { %>

        <div class="order-flow-wait">
           <a class="order-flow-input">确认</a>
           <span><p class="name">等待确认</p></span>
        </div>
        <%}
          else if (model.payment_status == 0 && model.status > 1  && model.status !=10)
          { %>
        <div class="order-flow-arrive">
          <a class="order-flow-input">确认</a>
          <span><p class="name">已确认</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.confirm_time)%></p></span>
        </div>
        <%} %>--%>
        
        <%if(bllOrderGoods.IsOrerExistCustStyle(Convert.ToInt32(Request.QueryString["id"]==""?"0" : Request.QueryString["id"])))
        { %>
           <%-- <%if (model.payment_status == 0 && model.status < 3)
            { %>
            <div class="order-flow-wait">
               <a class="order-flow-input">设计</a>
               <span><p class="name">等待设计</p></span>
            </div>
            <%}
            else if (model.payment_status == 0 && model.status >= 3 && model.status !=10)
            { %>
            <div class="order-flow-arrive">
              <a class="order-flow-input">设计</a>
              <span><p class="name">已设计</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.designtime)%></p></span>
            </div>
            <%} %>--%>
        
             <%if (model.payment_status == 0 && model.status < 4)
            { %>
            <div class="order-flow-wait">
               <a class="order-flow-input">确认设计</a>
               <span><p class="name">等待确认设计</p></span>
            </div>
            <%}
            else if (model.payment_status == 0 && model.status >= 4 && model.status !=10)
            { %>
            <div class="order-flow-arrive">
              <a class="order-flow-input">确认设计</a>
              <span><p class="name">已确认设计</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.comfirmdesigntime)%></p></span>
            </div>
            <%} %>
        <%} %>
        <%if (model.payment_status == 0 && model.status < 5)
        { %>
        <div class="order-flow-wait">
           <a class="order-flow-input">报价</a>
           <span><p class="name">等待报价</p></span>
        </div>
        <%}
        else if (model.payment_status == 0 && model.status >= 5 && model.status !=10)
        { %>
        <div class="order-flow-arrive">
          <a class="order-flow-input">报价</a>
          <span><p class="name">已报价</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.quotetime)%></p></span>
        </div>
        <%} %>

        <%if (model.payment_status == 0 && model.status < 6)
        { %>
        <div class="order-flow-wait">
           <a class="order-flow-input">确认报价</a>
           <span><p class="name">等待确认报价</p></span>
        </div>
        <%}
        else if (model.payment_status == 0 && model.status >= 6 && model.status !=10)
        { %>
        <div class="order-flow-arrive">
          <a class="order-flow-input">确认报价</a>
          <span><p class="name">已确认报价</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.comfirmquotetime)%></p></span>
        </div>
        <%} %>

         <%if (model.payment_status == 0 && model.status < 7)
        { %>
        <div class="order-flow-wait">
           <a class="order-flow-input">汇款</a>
           <span><p class="name">等待汇款</p></span>
        </div>
        <%}
        else if (model.payment_status == 0 && model.status >= 7 && model.status !=10)
        { %>
        <div class="order-flow-arrive">
          <a class="order-flow-input">汇款</a>
          <span><p class="name">已汇款</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.paytime)%></p></span>
        </div>
        <%} %>

         <%if (model.payment_status == 0 && model.status < 8)
        { %>
        <div class="order-flow-wait">
           <a class="order-flow-input">确认收款</a>
           <span><p class="name">等待确认收款</p></span>
        </div>
        <%}
        else if (model.payment_status == 0 && model.status >= 8 && model.status !=10)
        { %>
        <div class="order-flow-arrive">
          <a class="order-flow-input">确认收款</a>
          <span><p class="name">已确认收款</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.comfirmpaytime)%></p></span>
        </div>
        <%} %>

         <%if (model.status == 9)
           { %>
         <div class="order-flow-right-arrive">
           <a class="order-flow-input">排产</a>
           <span><p class="name">订单已排产</p><p><%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",model.complete_time)%></p></span>
         </div>
         <%}
           else
           { %>
         <div class="order-flow-right-wait">
           <a class="order-flow-input">排产</a>
           <span><p class="name">订单未排产</p></span>
         </div>
         <%} %>
         <%}
          else if (model.status == 10)
          {%>
          <div style="text-align:center;line-height:30px; font-size:20px; color:Red;">该订单已取消</div>
         <%} %>
      </div>
    </dd>
  </dl>
  <dl>
    <dt><%--订单号--%></dt>
    <dd>订单号 ：<span id="spanOrderNo"><%=model.order_no %></span></dd>

    <dt><%--Epicor订单号--%></dt>
    <dd>Epicor订单号 ： <span id="spanEpicorOrderNo"><%=model.epicor_order_no %></span></dd>
  </dl>

  <dl>
    
  </dl>
  <dl id="DesignInfo" runat="server" visible="false">
    <dt></dt>
    <dd>
        <asp:Label  ID="btnFileUplaod" runat="server" Text="设计附档" class="btn"/>
       
        <asp:FileUpload ID="FileUploadDesign" runat="server" accept=".png,.jpg,.xls,.xlsx" CssClass="input date"  /><asp:HiddenField ID="txtDesignFile1Name" runat="server" />
        <asp:FileUpload ID="FileUploadDesign2" runat="server" accept=".png,.jpg,.xls,.xlsx" CssClass="input date"  /><asp:HiddenField ID="txtDesignFile2Name" runat="server" />
        <asp:FileUpload ID="FileUploadDesign3" runat="server" accept=".png,.jpg,.xls,.xlsx" CssClass="input date"  /><asp:HiddenField ID="txtDesignFile3Name" runat="server" />
        <asp:FileUpload ID="FileUploadDesign4" runat="server" accept=".png,.jpg,.xls,.xlsx" CssClass="input date"  /><asp:HiddenField ID="txtDesignFile4Name" runat="server" />
        <asp:FileUpload ID="FileUploadDesign5" runat="server" accept=".png,.jpg,.xls,.xlsx" CssClass="input date"  /><asp:HiddenField ID="txtDesignFile5Name" runat="server" />
        <%--<asp:Button ID="btnKuJialLe"  visible="false"  runat="server" Text="酷家乐方案" CssClass="btn green"  onclick="btnKuJialLe_Click"  />--%>
        <BR>
        
        <asp:Label  ID="lb2" runat="server" Text="设计备注" class="btn"/> 
        <asp:Textbox ID="txtDesignRemarks" runat="server" Width="400"  CssClass="input small" Text=''></asp:Textbox> &nbsp;&nbsp;&nbsp;&nbsp;
        <input id="btnKuJialLeSelect" runat="server"  type="button" class="btn green" value="选择酷家乐方案" />
        <asp:Textbox ID="txtKujiaLe" runat="server" Width="300" CssClass="input small"></asp:Textbox>
        <asp:Button ID="btnKuJialLe"  visible="false"  runat="server" Text="保存酷家乐方案图片" CssClass="btn green"  onclick="btnKuJialLe_Click"  />
    </dd>
  </dl>

   <dl id="DesignInfodisplay" runat="server" visible="false">
    <dt></dt>
    <dd>
        <asp:Label  ID="Label6" runat="server" Text="设计附档" class="btn yellow"/>
        
        <% if (model.designattachment != "") {%> 
           <img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.designattachment%>" ><%=model.designattachment%></a>
        <% } %>
        <% if (model.designattachment2 != "") {%> 
            <img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.designattachment2%>" ><%=model.designattachment2%></a>
        <% } %>
        <% if (model.designattachment3 != "") {%>
            <img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.designattachment3%>" ><%=model.designattachment3%></a>
        <% } %>
        <% if (model.designattachment4 != "") {%>
            <img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.designattachment4%>" ><%=model.designattachment4%></a>
        <% } %>
        <% if (model.designattachment5 != "") {%>
            <img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.designattachment5%>" ><%=model.designattachment5%></a>
        <% } %>
        <asp:Label  ID="Label7" runat="server" Text="设计备注" class="btn yellow"/> <%=model.designremark%>
    </dd>
  </dl>


  <dl id="QuoteInfo" runat="server" visible="false">
    <dt></dt>
    <dd>
        <asp:Label  ID="Label1" runat="server" Text="报价附档" class="btn"/>
        <asp:FileUpload ID="FileUploadQuote" runat="server" CssClass="input date"  /><asp:HiddenField ID="txtQuoteFile1Name" runat="server" />
        
        <asp:Label  ID="Label2" runat="server" Text="报价备注" class="btn"/> <asp:Textbox ID="txtQuoteRemark" runat="server" Width="400"  CssClass="input small" Text=''></asp:Textbox>
    </dd>
  </dl>

   
   <dl id="QuoteInfodisplay" runat="server" visible="false">
    <dt></dt>
    <dd>
        <asp:Label  ID="Label8" runat="server" Text="报价附档" class="btn yellow"/>
        <img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.quoteattachment%>" ><%=model.quoteattachment%></a>
        
        <asp:Label  ID="Label9" runat="server" Text="报价备注" class="btn yellow"/> <%=model.quoteremark%>
    </dd>
  </dl> 

   <dl id="PayInfo" runat="server" visible="false">
    <dt></dt>
    <dd>
        <asp:Label  ID="Label3" runat="server" Text="汇款附档" class="btn"/>
        <asp:FileUpload ID="FileUploadPay" runat="server" CssClass="input date"  /><asp:HiddenField ID="txtPayFile1Name" runat="server" />
        <asp:Label  ID="Label5" runat="server" Text="汇款金额" class="btn"/> <asp:Textbox ID="txtPayAmount" runat="server" onkeyup="clearNoNum(this)"  MaxLength="8" sucmsg="" errormsg="请输入正确的金额" Width="100"  CssClass="input small" Text=''></asp:Textbox>
        <asp:Label  ID="Label15" runat="server" Text="已汇款金额" class="btn yellow"/><%=model.payamount%> 
        <asp:Label  ID="Label4" runat="server" Text="汇款备注" class="btn"/> <asp:Textbox ID="txtPayRemark" runat="server" Width="400"  CssClass="input small" Text=''></asp:Textbox>

    </dd>
  </dl>

   <dl id="PayInfoDisplay" runat="server" visible="false">
    <dt></dt>
    <dd>
        <asp:Label  ID="Label10" runat="server" Text="汇款附档" class="btn yellow"/>
        <img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.payattachment%>" ><%=model.payattachment%></a>
        
        <asp:Label  ID="Label11" runat="server" Text="汇款金额" class="btn yellow"/> <%=model.payamount%>
        <asp:Label  ID="Label12" runat="server" Text="汇款备注" class="btn yellow"/> <%=model.payaremark%>

    </dd>
  </dl> 

  <dl id="BackInfo" runat="server" visible="true">
    <dt></dt>
    <dd>
        <asp:Label  ID="Label13" runat="server" Text="退回备注" class="btn violet"/>
       <asp:Textbox ID="txtBackRemark" runat="server" Width="600"  CssClass="input small" Text=''></asp:Textbox>

    </dd>
  </dl> 

    
  <dl id="BackInfoDisplay" runat="server" visible="false">
    <dt></dt>
    <dd>
        <asp:Label  ID="Label14" runat="server" Text="上级退回备注" class="btn violet"/> <%=model.backremark%>

    </dd>
  </dl> 

  <asp:Repeater ID="rptList" runat="server">
  <HeaderTemplate>
  <dl>
    <dt><%--产品列表--%></dt>
    <dd>
      <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
        <thead>
          <tr>
            <th width="8%">产品编码</th>    
            <%--<th width="8%">产品类别</th>--%>
            <th width="100">产品名称</th>
            <th width="12%">规格型号</th>
            <th width="8%">产品型号</th>
            <th width="8%">颜色</th>
            <th width="4%">售价</th>
            <th width="4%">折扣%</th>  
            <th width="4%">数量</th>
            <th width="8%">定制<BR>规格</th>
            <th width="6%">折扣前<BR>金额</th>
            <th width="6%">实付<BR>金额</th>
            <th width="6%">折扣<BR>金额</th>
            <th width="10%">备注</th>
          </tr>
        </thead>
        <tbody>
        </HeaderTemplate>
        <ItemTemplate>
          <tr class="td_c">
            <td><%# ((model.status !=5 && model.status !=6 ) || this.isSearch=="1")  ? (Eval("new_product_no").ToString()!="" ?  Eval("new_product_no").ToString() +"(新)" :  Eval("product_no").ToString()) :Eval("commercialStyle").ToString() %><asp:TextBox ID="txtProductNo" Width="0" runat="server" Text='<%# Eval("product_no")%>' /></td>    
            <%--<td><%#new ps_product_category().GetTitle(Convert.ToInt32(Eval("product_category_id")))%></td>--%>
            <td style="text-align:left;white-space:normal;" width="100">
                
               <asp:TextBox ID="txtProductDesc"  Width="250"    Visible='<%# model.status ==3 && Eval("custsize").ToString()!="" %>' Enabled='<%# model.status ==3 && Eval("custsize").ToString()!="" %>'  runat="server" Text='<%# Eval("custsize").ToString()=="" ? Eval("product_name") : (model.status ==3 ? Eval("product_name") : Eval("goods_title"))%>' CssClass="input" />
               <asp:Label ID="lbProductDesc" runat="server" Width="200"  Visible='<%# (model.status ==3 && Eval("custsize").ToString()=="") || (model.status !=3) %>'   Text='<%# (model.status ==5 || model.status ==6)  ? Eval("goods_title") : (Eval("custsize").ToString()=="" ? Eval("product_name") : Eval("goods_title")) %>' ></asp:Label>
            </td>
            <td style="text-align:left;white-space:normal;"><%#Eval("specification")%></td>
            <td style="text-align:left;white-space:normal;"><%#Eval("commercialStyle")%></td>
            <td style="text-align:left;white-space:normal;"><%#Eval("commercialcolor")%></td>
            <%--<td><%#Eval("goods_price")%></td>--%>
            <% if (Session["IsDisplayPrice"]==null || Session["IsDisplayPrice"].ToString() == "1") {%>
                <td align="center" width="40"><asp:TextBox ID="txtPrice" Width="40" runat="server" Enabled='<%# (Eval("status").ToString() == "1" || Eval("status").ToString() == "4" ) ? true :false %>'  Text='<%# MyConvert(Eval("real_price"))%>' CssClass="input small" onkeyup="clearNoNum(this)" onblur="CalcAmount();"  MaxLength="8" /></td>
                <td align="center" width="40"><asp:TextBox ID="txtDiscount" Width="30" runat="server"  Enabled='<%# (Eval("status").ToString() == "1" || Eval("status").ToString() == "4" ) ? true :false %>'  Text='<%# MyConvert(Eval("discount"))%>' CssClass="input small" onkeyup="clearNoNum(this)" onblur="CalcAmount();" MaxLength="8" /></td>
             <% }else { %>
                <td>**</td>
                <td>**</td>
            <% }%>
            <td><%#Eval("quantity")%><%# Eval("dw")%><asp:TextBox ID="txtQuantity" Width="0" runat="server" Text='<%# MyConvert(Eval("quantity"))%>' /></td>
            <td><%#Eval("custsize")%></td>
           <% if (Session["IsDisplayPrice"]==null || Session["IsDisplayPrice"].ToString() == "1") {%>
                <%--<td><%#String.Format("{0:N}", Convert.ToDecimal(Eval("real_price")) * Convert.ToDecimal(Eval("quantity")))%></td>--%>
                <td><asp:TextBox ID="txtFullAmount" Width="50" runat="server"  Enabled='false'  Text='<%#String.Format("{0:N}", Convert.ToDecimal(Eval("real_price")) * Convert.ToDecimal(Eval("quantity")))%>'  ></asp:TextBox></td>
                <%--<td><asp:TextBox ID="txtAmount" Width="50" runat="server"  Enabled='<%# (Eval("status").ToString() == "1" || Eval("status").ToString() == "4" ) ? true :false %>'  Text='<%#String.Format("{0:N}", Convert.ToDecimal(Eval("real_price")) * Convert.ToDecimal(Eval("quantity")) * (Convert.ToDecimal(Eval("discount").ToString()=="" ? "0" : Eval("discount")))/100)%>' CssClass="input small" onkeyup="clearNoNum(this)" onblur="CalcDiscount();" ></asp:TextBox></td>
                 <td><asp:TextBox ID="txtDiscountAmount" Width="50" runat="server" Enabled="false" Text='<%#String.Format("{0:N}", Convert.ToDecimal(Eval("real_price")) * Convert.ToDecimal(Eval("quantity")) * (100 -Convert.ToDecimal(Eval("discount").ToString()=="" ? "0" : Eval("discount")))/100)%>'></asp:TextBox></td>--%>
                 <td><asp:TextBox ID="txtAmount" Width="50" runat="server"  Enabled='false'  Text='<%#String.Format("{0:N}", Convert.ToDecimal(Eval("payamount")) )%>'  ></asp:TextBox></td>
                 <td><asp:TextBox ID="txtDiscountAmount" Width="50" runat="server" Enabled='<%# (Eval("status").ToString() == "1" || Eval("status").ToString() == "4" ) ? true :false %>' Text='<%#String.Format("{0:N}", Convert.ToDecimal(Eval("discountamount")) )%>' CssClass="input small" onkeyup="clearNoNum(this)" onblur="CalcDiscount();"></asp:TextBox></td>
                
             <% }else { %>
                <td>**</td>
                <td>**</td>
                <td>**</td>
            <% }%>
            <td><%#Eval("remarks")%></td>
          </tr>
            <asp:Panel runat="server" Visible='<%# Eval("designattachment").ToString()!="" || Eval("designattachment2").ToString()!="" || Eval("designattachment3").ToString()!="" || Eval("designattachment4").ToString()!="" || Eval("designattachment5").ToString()!="" %>'>
            <tr>
                <td colspan="10">
                    <asp:Label  ID="Label6" runat="server" Text="附档" class="btn yellow"/> 
                    <asp:Image runat="server" Visible='<%# Eval("designattachment").ToString()!=""%>' src="../images/attachment.gif" width="20"/><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%# Eval("designattachment").ToString()%>" ><%# Eval("designattachment").ToString()%></a>
                    <asp:Image runat="server" Visible='<%# Eval("designattachment2").ToString()!=""%>' src="../images/attachment.gif" width="20"/><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%# Eval("designattachment2").ToString()%>" ><%# Eval("designattachment2").ToString()%></a>
                    <asp:Image runat="server" Visible='<%# Eval("designattachment3").ToString()!=""%>' src="../images/attachment.gif" width="20"/><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%# Eval("designattachment3").ToString()%>" ><%# Eval("designattachment3").ToString()%></a>
                    <asp:Image runat="server" Visible='<%# Eval("designattachment4").ToString()!=""%>' src="../images/attachment.gif" width="20"/><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%# Eval("designattachment4").ToString()%>" ><%# Eval("designattachment4").ToString()%></a>
                    <asp:Image runat="server" Visible='<%# Eval("designattachment5").ToString()!=""%>' src="../images/attachment.gif" width="20"/><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%# Eval("designattachment5").ToString()%>" ><%# Eval("designattachment5").ToString()%></a>
                    
                </td>
            </tr>
            </asp:Panel>
          </ItemTemplate>
          <FooterTemplate>
        </tbody>
      </table>
    </dd>
  </dl>
  </FooterTemplate>
  </asp:Repeater>
  <dl>
    <dt><%--收货信息--%></dt>
    <dd>
      <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
        <tr>
          <th width="20%">经销商账户</th>
          <td>
            <div class="position">
              <span id="spanAcceptName"><asp:Label ID="user_name" runat="server" /></span>
   
            </div>
          </td>
        </tr>
        
        <tr>
          <th>经销商名称</th>
          <td> <span id="span1"><asp:Label ID="title" runat="server" /></span></td>
        </tr>
        <tr>
          <th>经销商地址</th>
          <td> <span id="spanAddress"><asp:Label ID="contact_address" runat="server" /></span></td>
        </tr>
        <tr>
          <th>联系人姓名</th>
          <td><span id="spanPostCode"><asp:Label ID="contact_name" runat="server" /></span></td>
        </tr>
        <tr>
          <th>联系电话</th>
          <td><span id="spanTelphone"><asp:Label ID="contact_tel" runat="server" /></span></td>
        </tr>
          <tr>
          <th>用户留言</th>
          <td><%=model.message %></td>
        </tr>
        <tr>
          <th>订单备注</th>
          <td>
            <div class="position">
              <div id="divRemark"><%=model.remark %></div>
              <input id="btnEditRemark" runat="server" visible="false" type="button" class="ibtn" value="修改" /> 
            </div>
          </td>
        </tr>
      </table>
    </dd>
  </dl>
  

  <dl>
    <dt><%--订单统计--%></dt>
    <dd>
      <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
        <tr>
          <th width="20%">下单总金额</th>
          <td>
            <div class="position">

               <asp:Textbox ID="txtTotalAmount" Enabled="false" runat="server" Text=''></asp:Textbox>元
               <span id="spanTotalAmountValue" style="visibility:hidden"><%=model.payable_amount%></span>
            </div>
          </td>
        </tr>
         <tr>
          <th width="20%">折扣总金额</th>
          <td>
            <div class="position">
                <asp:Textbox ID="txtTotalDiscountAmount" Enabled="false" runat="server" Text=''></asp:Textbox>元
               <span id="spanDiscountAmountValue" style="visibility:hidden"><%=model.payable_amount%></span>
            </div>
          </td>
        </tr>
        <tr>
          <th width="20%">折扣后总金额</th>
          <td>
            <div class="position">

               <asp:Textbox ID="txtTotalRealAmount" Enabled="false" runat="server" Text=''></asp:Textbox>元
               <span id="spanRealAmountValue" style="visibility:hidden"><%=model.payable_amount%></span>
            </div>
          </td>
        </tr>
        <tr style="visibility:hidden">
          <th>实际总金额</th>
          <td>
            <div class="position">
              <span id="spanPaymentFeeValue"><%=model.order_amount%></span> 元
              <input id="btnEditPaymentFee" runat="server" visible="false" type="button" class="ibtn" value="调价" />
            </div>
          </td>
        </tr>
          <asp:Panel runat="server" ID="palPaymentRation" >
          <tr>
          <th width="20%">付款比例</th>
          <td>
            <div class="position">

               <asp:Textbox ID="txtPaymentRation" Visible="false" Enabled="true" runat="server" Text="100" CssClass="input small" onkeyup="clearNoNum(this)" ></asp:Textbox>
               <asp:Label ID="lbPaymentRation" runat="server"  Text="100" ></asp:Label>%
               <span id="spanPaymentRation" style="visibility:hidden"><%=model.payration%></span>
            </div>
          </td>
        </tr>
        </asp:Panel>
        <tr style="visibility:hidden">
          <th>价格调整金额</th>
          <td><%=model.real_amount%> 元</td>
        </tr>
      </table>
    </dd>
  </dl>

   <dl>
    <dt><%--附件--%></dt>
    <dd>
      <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
        <tr><%--<a href="#" onClick="downLoadFile('../upload/files/<%=model.attachment1%>')"><a href="/upload/files/<%=model.attachment2%>"><%=model.attachment2%></a>--%>
          <td width="20%" align="left" valign="middle"><asp:Panel ID="palAttachment1" Visible="false" runat="server"><img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.attachment1%>" ><%=model.attachment1%></a></asp:Panel></td>
          <td width="20%" align="left" valign="middle"><asp:Panel ID="palAttachment2" Visible="false" runat="server"><img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.attachment2%>" ><%=model.attachment2%></a></asp:Panel></td>
          <td width="20%" align="left" valign="middle"><asp:Panel ID="palAttachment3" Visible="false" runat="server"><img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.attachment3%>" ><%=model.attachment3%></a></asp:Panel></td>
          <td width="20%" align="left" valign="middle"><asp:Panel ID="palAttachment4" Visible="false" runat="server"><img width="20" src="../images/attachment.gif" /><a href="../tools/downloadfile.aspx?fileName=../upload/files/<%=model.attachment4%>" ><%=model.attachment4%></a></asp:Panel></td> 

        </tr>
      </table>
    </dd>
  </dl>

 

</div>
<!--/订单详细信息-->
    </div>

<!--工具栏-->
<div class="page-footer">
  <div class="btn-list">
    <asp:Button ID="btnConfirm2"  visible="false" runat="server" Text="保存订单 " CssClass="btn"  onclick="btnSubmit_Click"  />
    <asp:Button ID="btnPost"  visible="false" runat="server" Text="Post报价单到Epicor " CssClass="btn"  onclick="btnPost_Click"  />
    <input id="btnUpdateStatusToConfirmQuote" runat="server" visible="false" type="button" value="流程转至确认报价" class="btn" />   
      
    <%--<input id="btnConfirm" runat="server" visible="false" type="button" value="确认订单" class="btn" />--%>
    <%--<input id="btnDesign" runat="server" visible="false" type="button" value="设计" class="btn" />--%>
    <asp:Button ID="btnDesign" runat="server" visible="false"  Text="设&nbsp;&nbsp;&nbsp;&nbsp;计" class="btn" onclick="btnDesign_Click" />
     <asp:Button ID="btnConfirmDesign" runat="server" visible="false"  Text="确认设计" class="btn" onclick="btnConfirmDesign_Click" />
      
    
    <%--<input id="btnConfirmDesign" runat="server" visible="false" type="button" value="确认设计"  onclick="btnDesign_Click" class="btn" />--%>
    <%--<input id="btnQuote" runat="server" visible="false" type="button" value="报价" class="btn" />--%>
    <asp:Button ID="btnQuote" runat="server" visible="false"  Text="报&nbsp;&nbsp;&nbsp;&nbsp;价" class="btn" onclick="btnQuote_Click" />
    <input id="btnConfirmQuote" runat="server" visible="false" type="button" value="确认报价" class="btn" />
    <%--<input id="btnPay" runat="server" visible="false" type="button" value="汇款" class="btn" />--%>
    <asp:Button ID="btnPay" runat="server" visible="false" Text="汇&nbsp;&nbsp;&nbsp;&nbsp;款" class="btn" onclick="btnPay_Click" />
    <input id="btnConfirmPay" runat="server" visible="false" type="button" value="确认收款" class="btn" />
    <input id="btnComplete" runat="server" visible="false" type="button" value="排&nbsp;&nbsp;&nbsp;&nbsp;产" class="btn" />
     
    <asp:Button ID="btnBack" runat="server" visible="true"  Text="退&nbsp;&nbsp;&nbsp;&nbsp;回" class="btn violet"  OnClick="btnBack_Click" />
    
    <input id="btnPrint" type="button" value="打印订单" class="btn green" />
    <input id="btnCancel" runat="server" visible="false" type="button" value="取消订单" class="btn violet" />
    <input id="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
  <div class="clear"></div>
  <div id="loading"></div>
</div>
<!--/工具栏-->
    </form>
</body>
</html>
