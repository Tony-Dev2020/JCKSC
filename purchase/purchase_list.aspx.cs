using System;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class purchase_purchase_list : System.Web.UI.Page
{
    protected int totalCount;
    protected int page;
    protected int pageSize;

    protected int depot_category_id;
    protected string vendor_id;
    protected int status=-1;
    protected string note_no = string.Empty;
    protected string start_time = string.Empty;
    protected string stop_time = string.Empty;

    ManagePage mym = new ManagePage();
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断是否登录
        if (!mym.IsAdminLogin())
        {
            Response.Write("<script>parent.location.href='../index.aspx'</script>");
            Response.End();
        }
        //判断权限
        ps_manager_role_value myrv = new ps_manager_role_value();
        int role_id = Convert.ToInt32(Session["RoleID"]);
        int nav_id = 43;
        if (!myrv.QXExists(role_id, nav_id))
        {
            Response.Redirect("../error.html");
            Response.End();
        }
        if (Session["VendorID"].ToString() !="")
        {
            this.depot_category_id = Convert.ToInt32(Session["DepotCatID"]);
            this.vendor_id = Session["VendorID"].ToString();
        }
        else
        {
            this.depot_category_id = AXRequest.GetQueryInt("depot_category_id");
            this.vendor_id = AXRequest.GetQueryString("vendor_id");
        }
        this.status = AXRequest.GetQueryInt("status");
        this.note_no = AXRequest.GetQueryString("note_no");
        if (AXRequest.GetQueryString("start_time") == "")
        {
            //this.start_time = DateTime.Now.ToString("yyyy-MM-01");
            this.start_time = DateTime.Now.AddYears(-2).ToString("yyyy-MM-01");
        }
        else
        {
            this.start_time = AXRequest.GetQueryString("start_time");
        }
        if (AXRequest.GetQueryString("stop_time") == "")
        {
            this.stop_time = DateTime.Now.ToString("yyyy-MM-dd");
        }
        else
        {
            this.stop_time = AXRequest.GetQueryString("stop_time");
        }

        this.pageSize = GetPageSize(10); //每页数量

        if (!Page.IsPostBack)
        {
            DQBind(depot_category_id); //绑定商家地区
            VendorBind(ddldepot_category_id.SelectedValue=="" ?  0 : Convert.ToInt32(ddldepot_category_id.SelectedValue)); //绑定下单商家
            RptBind("id>0 " + CombSqlTxt(this.depot_category_id, this.vendor_id, this.status, this.note_no, this.start_time, this.stop_time), "Company,PONum desc ");

        }
        if (Session["VendorID"].ToString() != "")
        {
            ddldepot_category_id.Enabled = false;
            ddlvendor.Enabled = false;
        }
    }

    #region 数据绑定=================================
    private void RptBind(string _strWhere, string _orderby)
    {
        this.page = AXRequest.GetQueryInt("page", 1);

        if (this.status >= 0)
        {
            this.ddlStatus.SelectedValue = this.status.ToString();
        }
        if (this.depot_category_id > 0)
        {
            this.ddldepot_category_id.SelectedValue = this.depot_category_id.ToString();
        }
        if (this.vendor_id !="")
        {
            this.ddlvendor.SelectedValue = this.vendor_id.ToString();
        }
        txtNote_no.Text = this.note_no;
        txtstart_time.Value = this.start_time;
        txtstop_time.Value = this.stop_time;

        ps_po bll = new ps_po();
        this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
        this.rptList.DataBind();
        //绑定页码
        txtPageNum.Text = this.pageSize.ToString();
        string pageUrl = Utils.CombUrlTxt("purchase_list.aspx", "depot_category_id={0}&vendor_id={1}&status={2}&start_time={3}&stop_time={4}&note_no={5}&page={6}", this.depot_category_id.ToString(), this.vendor_id.ToString(), this.status.ToString(), this.txtstart_time.Value, this.txtstop_time.Value, txtNote_no.Text, "__id__");
        PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
    }
    #endregion

    #region 组合SQL查询语句==========================
    protected string CombSqlTxt(int _depot_category_id, string _vendor_id, int _status, string _note_no, string _start_time, string _stop_time)
    {
        StringBuilder strTemp = new StringBuilder();

        //if (_status == 1)
        //{
        //    strTemp.Append(" and Approve=" + _status);
        //}
        strTemp.Append(" and Approve=1");
        if (_status == 1 || _status == 2)
        {
            strTemp.Append(" and Confirmed=" + (_status-1));
        }


        if (_depot_category_id > 0)
        {
            strTemp.Append(" and CatID=" + _depot_category_id);
        }
        if (_vendor_id != "")
        {
            strTemp.Append(" and VendorID='" + _vendor_id +"'");
        }

        if (string.IsNullOrEmpty(_start_time))
        {
            _start_time = "1900-01-01";
        }
        if (string.IsNullOrEmpty(_stop_time))
        {
            _stop_time = "2099-01-01";
        }
        strTemp.Append(" and OrderDate between  '" + DateTime.Parse(_start_time) + "' and '" + DateTime.Parse(_stop_time + " 23:59:59") + "'");

        _note_no = _note_no.Replace("'", "");
        if (!string.IsNullOrEmpty(_note_no))
        {
            strTemp.Append(" and PONum like  '%" + _note_no + "%' ");
        }
        return strTemp.ToString();
    }
    #endregion

    #region 返回每页数量=============================
    private int GetPageSize(int _default_size)
    {
        int _pagesize;
        if (int.TryParse(Utils.GetCookie("order_page_size"), out _pagesize))
        {
            if (_pagesize > 0)
            {
                return _pagesize;
            }
        }
        return _default_size;
    }
    #endregion

    #region 返回订单状态=============================
    protected string GetOrderStatus(int _id)
    {
        string _title = string.Empty;

        switch (_id)
        {
            case 1:
                _title = "已生成";
                break;
            case 2:
                _title = "已确认";
                break;
            case 3:
                _title = "交易完成";
                break;
            case 4:
                _title = "<font color=red>已取消</font>";
                break;
        }

        return _title;
    }
    #endregion

    //查询
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect(Utils.CombUrlTxt("purchase_list.aspx", "depot_category_id={0}&vendor_id={1}&status={2}&start_time={3}&stop_time={4}&note_no={5}", this.depot_category_id.ToString(), this.vendor_id.ToString(), this.status.ToString(), this.txtstart_time.Value, this.txtstop_time.Value, txtNote_no.Text));
    }


    //筛选商家地区
    protected void ddldepot_category_id_SelectedIndexChanged(object sender, EventArgs e)
    {
        VendorBind(Convert.ToInt32(ddldepot_category_id.SelectedValue));
        Response.Redirect(Utils.CombUrlTxt("purchase_list.aspx", "depot_category_id={0}&_vendor_id={1}&status={2}&note_no={3}", this.ddldepot_category_id.SelectedValue, this.ddlvendor.ToString(), this.status.ToString(), txtNote_no.Text));
    }

    //筛选下单商家
    protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect(Utils.CombUrlTxt("purchase_list.aspx", "depot_category_id={0}&vendor_id={1}&status={2}&note_no={3}", this.depot_category_id.ToString(), this.ddlvendor.SelectedValue, this.status.ToString(), txtNote_no.Text));
    }

    private void DQBind(int _category_id)
    {
        ps_depot_category bll = new ps_depot_category();
        DataTable dt = bll.GetList(0);
        this.ddldepot_category_id.Items.Clear();
        this.ddldepot_category_id.Items.Add(new ListItem("==全部==", "0"));
        foreach (DataRow dr in dt.Rows)
        {
            string Id = dr["id"].ToString();
            string Title = dr["name"].ToString().Trim();
            this.ddldepot_category_id.Items.Add(new ListItem(Title, Id));
        }
        if (this.depot_category_id > 0)
        {
            this.ddldepot_category_id.SelectedValue = this.depot_category_id.ToString();
            VendorBind(depot_category_id);
        }
    }

    private void VendorBind(int _category_id)
    {
        ps_vendor bll = new ps_vendor();
        DataTable dt = bll.GetList("CatID=" + _category_id + "and inactive=0 ").Tables[0];
        this.ddlvendor.Items.Clear();
        this.ddlvendor.Items.Add(new ListItem("==全部==", ""));
        foreach (DataRow dr in dt.Rows)
        {
            string VendorNum = dr["VendorNum"].ToString();
            string Name = dr["Name"].ToString().Trim();
            this.ddlvendor.Items.Add(new ListItem(Name, VendorNum));
        }
        if (this.vendor_id !="")
        {
            this.ddlvendor.SelectedValue = this.vendor_id.ToString();
        }
    }

    //订单状态
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect(Utils.CombUrlTxt("purchase_list.aspx", "depot_category_id={0}&depot_id={1}&status={2}&start_time={3}&stop_time={4}&note_no={5}", this.depot_category_id.ToString(), this.vendor_id.ToString(), this.ddlStatus.SelectedValue, this.txtstart_time.Value, this.txtstop_time.Value, txtNote_no.Text));

    }

    //设置分页数量
    protected void txtPageNum_TextChanged(object sender, EventArgs e)
    {
        int _pagesize;
        if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
        {
            if (_pagesize > 0)
            {
                Utils.WriteCookie("order_page_size", _pagesize.ToString(), 14400);
            }
        }
        Response.Redirect(Utils.CombUrlTxt("purchase_list.aspx", "depot_category_id={0}&depot_id={1}&status={2}&start_time={3}&stop_time={4}&note_no={5}", this.depot_category_id.ToString(), this.vendor_id.ToString(), this.status.ToString(), this.txtstart_time.Value, this.txtstop_time.Value, txtNote_no.Text));

    }
    //导出报表
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Redirect(Utils.CombUrlTxt("purchase_request_rep.aspx", "depot_category_id={0}&depot_id={1}&status={2}&start_time={3}&stop_time={4}&note_no={5}", this.depot_category_id.ToString(), this.vendor_id.ToString(), this.status.ToString(), this.txtstart_time.Value, this.txtstop_time.Value, txtNote_no.Text));
    }

    // 取消订单
    protected void lbtnDelCa_Click(object sender, EventArgs e)
    {
        this.page = AXRequest.GetQueryInt("page", 1);
        // 当前点击的按钮
        LinkButton lb = (LinkButton)sender;
        int caId = int.Parse(lb.CommandArgument);
        ps_orders bll = new ps_orders();
        bll.CancelOrder(caId);
        bll.GetModel(caId);

        //写入登录日志
        ps_manager_log mylog = new ps_manager_log();
        mylog.user_id = Convert.ToInt32(Session["AID"].ToString());
        mylog.user_name = Session["AdminName"].ToString();
        mylog.action_type = "订单";
        mylog.add_time = DateTime.Now;
        mylog.remark = "取消订单(订单号" + bll.order_no + ")";
        mylog.user_ip = AXRequest.GetIP();
        mylog.Add();
        //bll.Delete(caId);
        //ps_order_goods bllg = new ps_order_goods();
        //bllg.DeleteOid(caId);

        mym.JscriptMsg(this.Page, " 取消订单成功！", Utils.CombUrlTxt("purchase_list.aspx", "depot_category_id={0}&vendor_id={1}&status={2}&start_time={3}&stop_time={4}&note_no={5}&page={6}", this.depot_category_id.ToString(), this.vendor_id.ToString(), this.status.ToString(), this.txtstart_time.Value, this.txtstop_time.Value, txtNote_no.Text, this.page.ToString()), "Success");
    }
    //小数位是0的不显示
    public string MyConvert(object d)
    {
        string myNum = d.ToString();
        string[] strs = d.ToString().Split('.');
        if (strs.Length > 1)
        {
            if (Convert.ToInt32(strs[1]) == 0)
            {
                myNum = strs[0];
            }
        }
        return myNum;
    }
}
