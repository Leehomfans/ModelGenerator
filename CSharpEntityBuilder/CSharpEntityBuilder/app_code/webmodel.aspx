<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../css/admin.css" type="text/css" rel="stylesheet" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/include/jquery.js" type="text/javascript"></script>
   <%-- <script type="text/javascript" src="../include/tr.js"></script>--%>
    <script src="../include/pagesou_plugs.js" type="text/javascript"></script>
    <link href="../css/pagesou.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <!--查询条件的定义 start-->
       <div style="background-color: #f5fafe;">内容:<input id="slcon" name="slcon" type="text" /> <input type="submit" value="查询" /></div>
      <!--查询条件的定义 end--> 
      <table width="100%" cellspacing="1" cellpadding="0" border="0" align="center" style="table-layout: fixed"
        class="listTable">
        <tbody id="tblist">
            <tr class="listHeaderTr">
              <%--  <td>
                    客户姓名
                </td>
                <td>
                    手机
                </td>
                <td>
                    省份
                </td>
                <td>
                    城市
                </td>
                <td>
                    订单（状态）
                </td>
                <td width="10%">
                    操作
                </td>--%>
                {$head}
            </tr>
            <%
                int pageSize = 30;
                int currpage = (Request["page"] != null && Request["page"].ToString() != "") ? Convert.ToInt32(Request["page"]) : 1;
                Common.PageControl page = new Common.PageControl(currpage, pageSize);
                  List<Conditions.Condition> whereList = new List<Conditions.Condition>();
                  whereList.Add(new Conditions.Condition("state", "0"));
                string showkey_base = "customer_id,learner_id,truename,mobile";
                //string showkey_detail = "customer_id,sex,mobilecity,mobileprovince,address";
                List<{$namespace}.Model> list ={$namespace}.UiLogic.Query(showkey_base, whereList, "regtime desc", page);
                int totalrows = CCustomerBase.UiLogic.TotalRow;
                //CDictionary.Control Cdic = new CDictionary.Control();
                string listwhere = "<tr class='listTr'>";
                foreach (CCustomerBase.Model model in list)
                {
                    {$body}
                }
                Response.Write(listwhere+"</tr>");
            %>
        </tbody>
        <tfoot>
            <tr class="listFooterTr">
                <td colspan="7">
                    <div id="pagediv" class="cutPage">
                    </div>
                </td>
            </tr>
        </tfoot>
    </table>
    <script type="text/javascript">
        //初始化分页
        window.onload = function () {
            my_page = new open_pager("pagediv");
            my_page.showTotalTip = true;
            my_page.pageSize = <%=pageSize %>; //每页显示数
            my_page.pageFuncName = "LoadPaging"; //回调函数名称
            my_page.currentPage = 1; //当前页
            my_page.totalRows = <%=totalrows %>; //总条数
            //my_page.allowClone = true;
            //my_page.cloneName = "Div_Clone_Page";
            my_page.output();
        }
        //调用
        function LoadPaging(pg) {
            //通过form提交
            var _newurl = my_page.createUrl(pg);
            self.location.href = _newurl;
            /*
            document.getElementById("form1").action = _newurl;
            document.getElementById("form1").method = "post";
            document.getElementById("form1").submit();
            */
        }       
    </script>
    </div>
    </form>
</body>
</html>
