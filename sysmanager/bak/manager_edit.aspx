<%@ Page Language="C#" AutoEventWireup="true" CodeFile="manager_edit.aspx.cs" Inherits="sysmanager_manager_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>编辑用户</title>
<script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="scripts/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/jquery/jquery.ui.core.js"></script>
    <script type="text/javascript" src="../js/jquery/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="../js/jquery/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="../js/jquery/jquery.ui.button.js"></script>
    <script type="text/javascript" src="../js/jquery/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="../js/jquery/jquery.ui.position.js"></script>
    <script type="text/javascript" src="../js/jquery/jquery.ui.dialog.js"></script>
    <link type="text/css" href="../css/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../js/layout.js"></script>
<script type="text/javascript" src="../js/pinyin.js"></script>
<script type="text/javascript" src="../js/jquery.autocomplete.min.js"></script>
<link rel="Stylesheet" href="../css/jquery.autocomplete.css" />
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
    });

    function SelCus(obj) {

        if (obj.value.length >= 1) {
            var postData = { "searchcontent": obj.value, compamy: $("#ddlCategoryId").val() };
            $.ajax({
                type: "post",
                url: "../tools/search_ajax.ashx?searchtype=cust",
                data: postData,
                dataType: "json",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('尝试发送失败，错误信息' + errorThrown);
                },
  
                success: function (data, textStatus) {
                    $('#txtCustCode').autocomplete(data, {
                        max: 200,    //列表里的条目数
                        minChars: 1,    //自动完成激活之前填入的最小字符
                        width: 400,     //提示的宽度，溢出隐藏
                        scrollHeight: 300,   //提示的高度，溢出显示滚动条
                        matchContains: true,    //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                        autoFill: false,    //自动填充
                        formatItem: function (row, i, max) {
                            return i + '/' + max + ':  "' + row.custcode + '"  [' + row.custname + ']';
                        },
                        formatMatch: function (row, i, max) {
                            return row.custcode + row.custname ;
                        },
                        formatResult: function (row) {
                            return row.custcode;
                        }
                    }).result(function (event, row, formatted) {
                        $('#txtCustName').val(row.custname);
                        $('#txtCustID').val(row.custid);
                    });
                }
            });
        }
    }


    function SelCus2(obj) {

        if (obj.value.length >= 1) {
            var postData = { "searchcontent": obj.value, compamy: $("#ddlCategoryId").val() };
            $.ajax({
                type: "post",
                url: "../tools/search_ajax.ashx?searchtype=cust",
                data: postData,
                dataType: "json",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('尝试发送失败，错误信息' + errorThrown);
                },

                success: function (data, textStatus) {
                    $('#txtCustomerName').autocomplete(data, {
                        max: 200,    //列表里的条目数
                        minChars: 1,    //自动完成激活之前填入的最小字符
                        width: 400,     //提示的宽度，溢出隐藏
                        scrollHeight: 300,   //提示的高度，溢出显示滚动条
                        matchContains: true,    //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                        autoFill: false,    //自动填充
                        formatItem: function (row, i, max) {
                            return i + '/' + max + ':  "' + row.custcode + '"  [' + row.custname + ']';
                        },
                        formatMatch: function (row, i, max) {
                            return row.custcode + row.custname;
                        },
                        formatResult: function (row) {
                            return row.custcode;
                        }
                    }).result(function (event, row, formatted) {
                        $('#txtCustomerName').val(row.custname);
                        $('#txtCustomerID').val(row.custid);
                    });
                }
            });
        }
    }


    function SelVendor(obj) {
        if ($("#ddlCategoryId").val() == '')
            alert('请先选择所属公司.')
        else {
            if (obj.value.length >= 1) {
                var postData = { "searchcontent": obj.value, compamy: $("#ddlCategoryId").val() };
                $.ajax({
                    type: "post",
                    url: "../tools/search_ajax.ashx?searchtype=vendor",
                    data: postData,
                    dataType: "json",
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert('尝试发送失败，错误信息' + errorThrown);
                    },

                    success: function (data, textStatus) {
                        $('#txtVendorName').autocomplete(data, {
                            max: 200,    //列表里的条目数
                            minChars: 1,    //自动完成激活之前填入的最小字符
                            width: 400,     //提示的宽度，溢出隐藏
                            scrollHeight: 300,   //提示的高度，溢出显示滚动条
                            matchContains: true,    //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                            autoFill: false,    //自动填充
                            formatItem: function (row, i, max) {
                                return i + '/' + max + ':  "' + row.vendorcode + '"  [' + row.vendorname + ']';
                            },
                            formatMatch: function (row, i, max) {
                                return row.vendorcode + row.vendorname;
                            },
                            formatResult: function (row) {
                                return row.vendorcode;
                            }
                        }).result(function (event, row, formatted) {
                            $('#txtVendorName').val(row.vendorname);
                            $('#txtVendorID').val(row.vendorid);
                        });
                    }
                });
            }
        }
    }

    function message() {
        alert("Hello, world.");
    }

    $(function () {
        // 初始化
        $("#btn").click(function () {
            $("#dialog-confirm").dialog("open");
        })
            ;


        $("#deleteBtn").click(function () {
            message();
        })
            .hide();


        // 初始化对话框
        $("#dialog-confirm").dialog(
            {
                modal: true,             // 创建模式对话框
                autoOpen: false,
                buttons: {
                    "Ok": function () {
                        //$("#deleteBtn").click();

                        $(this).dialog('close');



                    },
                    "Cancel": function () { $(this).dialog('close'); return false; }
                }
            }
        );
    });
</script>
</head>
<body>
    <form id="form1" runat="server">

	<div class="place">
    <span>位置：</span>
    <ul class="placeul">
    <li><a href="../home.aspx">首页</a></li>
    <li><a href="manager_list.aspx">用户管理</a></li>
    <li><a href="#">编辑用户</a></li>
    </ul>
    </div>
    
    <div class="formbody">   
    <div class="formtitle"><span>用户信息</span></div>
    <!--用户信息-->
<div class="tab-content">
   <dl id="role" runat="server" visible="true">
    <dt>管理角色</dt>
    <dd>
      <span class="rule-single-select" style="position:absoulte;z-index:5555; ">
        <asp:DropDownList id="ddlRoleId" runat="server" datatype="*" errormsg="请选择管理员角色" sucmsg=" "  AutoPostBack="True" onselectedindexchanged="ddlRoleId_SelectedIndexChanged"></asp:DropDownList>
      </span>
    </dd>
  </dl>
     <dl id="bm" runat="server" visible="false">
    <dt>所属公司</dt>
    <dd>
      <span class="rule-single-select" style="position:absoulte;z-index:5554; ">
        <asp:DropDownList id="ddlCategoryId" runat="server" datatype="*" errormsg="请选择所属公司" sucmsg=" " AutoPostBack="True" onselectedindexchanged="ddlCategoryId_SelectedIndexChanged"></asp:DropDownList>
      </span>
    </dd>
  </dl>
    <dl id="md" runat="server" visible="false">
    <dt>所属经销商</dt>
    <dd>
        <asp:TextBox ID="txtCustomerName"  CssClass="input normal" oninput="SelCus2(this)" onkeyup="SelCus2(this)"   runat="server" Width="300" datatype="*" errormsg="请选择所属经销商"  sucmsg=" " ></asp:TextBox>
        <asp:TextBox ID="txtCustomerID"  CssClass="input normal"   runat="server" Width="0" style="visibility:hidden " ></asp:TextBox>
      <span class="rule-single-select" style="position:absoulte;z-index:5553;visibility:hidden ">
        <asp:DropDownList id="ddlDepotId" width="200" runat="server" Visible="false"  datatype="*" errormsg="请选择所属经销商"  sucmsg=" "></asp:DropDownList>
      </span>
    </dd>
  </dl>
   <dl >
    <dt>是否显示价格</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="ckIsDisplayPrice" runat="server" Checked="True" />
      </div>
      
    </dd>
  </dl>
  <dl id="vd" runat="server" visible="false">
    <dt>所属供应商</dt>
    <dd>
      
      <span class="rule-single-select" style="position:absoulte;z-index:5553;">
        <asp:DropDownList id="ddlVendorId" width="250"  runat="server" datatype="*" errormsg="请选择所属供应商" sucmsg=" "></asp:DropDownList>
      </span>
        <asp:TextBox ID="txtVendorKeyWord"  CssClass="input normal"   runat="server" Width="100"  ></asp:TextBox>
       <asp:Button ID="btnSearchVendor" runat="server" Text="搜索" CssClass="btn green" onclick="btnSearchVendor_Click"  />
       <asp:TextBox ID="txtVendorName"  CssClass="input normal" oninput="SelVendor(this)" runat="server" Width="300" datatype="*" errormsg="请选择所属供应商" Visible="false"  sucmsg=" " ></asp:TextBox>
      <asp:TextBox ID="txtVendorID"  CssClass="input normal"   runat="server" Width="0" style="visibility:hidden " ></asp:TextBox>
    </dd>
  </dl>
<dl>
    <dt>登录账号</dt>
    <dd><asp:TextBox ID="txtUserName" runat="server" CssClass="input normal" datatype="/^[a-zA-Z0-9\-\_]{2,50}$/" sucmsg=" " ajaxurl="../tools/admin_ajax.ashx?action=manager_validate"></asp:TextBox> <span class="Validform_checktip">*字母、下划线、数字</span></dd>
  </dl> 
<dl>
    <dt>登录密码</dt>
    <dd><asp:TextBox ID="txtPassword" runat="server" CssClass="input normal" TextMode="Password" datatype="*6-20" nullmsg="请设置密码" errormsg="密码范围在6-20位之间" sucmsg=" " ></asp:TextBox> <span class="Validform_checktip">*</span></dd>
  </dl>
  <dl>
    <dt>确认密码</dt>
    <dd><asp:TextBox ID="txtPassword1" runat="server" CssClass="input normal" TextMode="Password" datatype="*" recheck="txtPassword" nullmsg="请再输入一次密码" errormsg="两次输入的密码不一致" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*</span></dd>
  </dl>
  
  <dl>
    <dt>姓名</dt>
    <dd><asp:TextBox ID="txtRealName" runat="server" CssClass="input normal" nullmsg="请输入姓名"></asp:TextBox>
  </dl>
  <dl>
    <dt>联系电话</dt>
    <dd><asp:TextBox ID="txtmobile" runat="server" CssClass="input normal"></asp:TextBox></dd>
  </dl>
   <dl>
    <dt>邮件</dt>
    <dd><asp:TextBox ID="txtEmailAddress" runat="server" CssClass="input normal"></asp:TextBox></dd>
  </dl>


  <dl >
    <dt>是否启用</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsLock" runat="server" Checked="True" />
      </div>
      <span class="Validform_checktip">*不启用该账户将无法登录使用本系统</span>
    </dd>
  </dl>

     <dl id="cu" runat="server" visible="false">
    <dt>绑定经销商</dt>
     <dd>       
    <table width="400" border="0" align="center" cellpadding="9" cellspacing="0" class="cart_table">
     
      <tr> 
       <th width="150" align="left">经销商编码</th>
       <th width="300" align="left">经销商名称</th>
        <th width="100" align="left">操作</th>
      </tr>
       <tr> 
       <th width="150" align="left"><asp:TextBox ID="txtCustCode"  CssClass="input normal" oninput="SelCus(this)"   runat="server" Width="100" ></asp:TextBox></th>
       <th width="300" align="left"><asp:TextBox ID="txtCustName"  CssClass="input normal" runat="server" Width="300" ></asp:TextBox><asp:TextBox ID="txtCustID" runat="server" Width="0" ></asp:TextBox></th>
       <th width="100" align="center" ><asp:Button ID="btnAddCust" runat="server" Text="添加经销商 " class="btn"   onclick="btnAddCust_Click" Width="100" /></th>  
      </tr>
      <asp:Repeater ID="rptList" runat="server">
        <ItemTemplate> 
            <tr>
                <td width="150" ><%# Eval("custcode")%></td>	
                <td width="300"><%# Eval("custname")%></td>	
                <td ><asp:LinkButton ID="lbtnDelCust" runat="server" class="btn"  Width="30"  CommandArgument='<%# Eval("id")%>'  OnClientClick="return confirm('是否真的要删除？')"   onclick="lbtnDelCust_Click" ><font color ="red">删除</font></asp:LinkButton></td>
            </tr>
           </ItemTemplate>
        </asp:Repeater>
   </table>
    </dd>
    </dl>

  <dl>
    <dt>备注</dt>
        <dd><asp:TextBox ID="txtremark" runat="server" CssClass="input normal" ></asp:TextBox></dd>
  </dl>
</div>
<!--/用户信息-->    
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

  <a href="#" id="deleteBtn">Click Me!</a>
    <input type="button" value="删除" id="btn" />
    <div id="dialog-confirm" title="Empty the recycle bin?" style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            These items will be permanently deleted and cannot be recovered. Are you sure?</p>
    </div>  
        
    </form>
</body>
</html>
