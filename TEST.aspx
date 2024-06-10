<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TEST.aspx.cs" Inherits="TEST" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
 <html xmlns="http://www.w3.org/1999/xhtml">
 <head runat="server">
  <title></title>
 <script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/jquery.autocomplete.min.js"></script>
<link rel="Stylesheet" href="css/jquery.autocomplete.css" />
 
  <script type="text/javascript">
      var emails = [
          { name: "Peter Pan", to: "peter@pan.de" },
          { name: "Molly", to: "molly@yahoo.com" },
          { name: "Forneria Marconi", to: "live@japan.jp" },
          { name: "Master <em>Sync</em>", to: "205bw@samsung.com" },
          { name: "Dr. <strong>Tech</strong> de Log", to: "g15@logitech.com" },
          { name: "Don Corleone", to: "don@vegas.com" },
          { name: "Mc Chick", to: "info@donalds.org" },
          { name: "Donnie Darko", to: "dd@timeshift.info" },
          { name: "Quake The Net", to: "webmaster@quakenet.org" },
          { name: "Dr. Write", to: "write@writable.com" },
          { name: "GG Bond", to: "Bond@qq.com" },
          { name: "Zhuzhu Xia", to: "zhuzhu@qq.com" }
      ];

      //$(function () {
      //    $('#txtIput').autocomplete(emails, {
      //        max: 12,    //列表里的条目数
      //        minChars: 0,    //自动完成激活之前填入的最小字符
      //        width: 400,     //提示的宽度，溢出隐藏
      //        scrollHeight: 300,   //提示的高度，溢出显示滚动条
      //        matchContains: true,    //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
      //        autoFill: false,    //自动填充
      //        formatItem: function (row, i, max) {
      //            return i + '/' + max + ':"' + row.name + '"[' + row.to + ']';
      //        },
      //        formatMatch: function (row, i, max) {
      //            return row.name + row.to;
      //        },
      //        formatResult: function (row) {
      //            return row.to;
      //        }
      //    }).result(function (event, row, formatted) {
      //        alert(row.to);
      //    });
      //});
      //$.ajax({
      //    type: "post",
      //    url: sendUrl,
      //    data: postData,
      //    dataType: "json",
      //    error: function (XMLHttpRequest, textStatus, errorThrown) {
      //        $.dialog.alert('尝试发送失败，错误信息：' + errorThrown, function () { }, winObj);
      //    },
      //    success: function (data, textStatus) {
      //        if (data.status == 1) {
      //            winObj.close();
      //            $.dialog.tips(data.msg, 2, '32X32/succ.png', function () { location.reload(); }); //刷新页面
      //        } else {
      //            $.dialog.alert('错误提示：' + data.msg, function () { }, winObj);
      //        }
      //    }
      //});
      //var postData = { "order_no": "123", "edit_type": "order_complete" };
      ////发送AJAX请求
      //sendAjaxUrl(dialog, postData, "../tools/admin_ajax.ashx?action=edit_order_status");
      function sele_id(obj) {
        
          if (obj.value.length >= 2) { 
              var postData = { "searchcontent": obj.value };
              $.ajax({ 
                  type: "post",
                  url: "../tools/search_ajax.ashx?searchtype=part",
                  data: postData,
                  dataType: "json",
                  error: function (XMLHttpRequest, textStatus, errorThrown) {
                      alert('尝试发送失败，错误信息' + errorThrown);
                  },
                  success: function (data, textStatus) {
                      $('#txtIput').autocomplete(data, {
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
                          
                      });
                  }
              });
          }
      }
  </script>  
 </head>
 <body>
  <form id="form1" runat="server">
  <div>
  <center>
   <asp:TextBox ID="txtIput" runat="server" oninput="sele_id(this)" Width="400px"></asp:TextBox>
  </center>
  </div>
  </form>
 </body>
</html>
