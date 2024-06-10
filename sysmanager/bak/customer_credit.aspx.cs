using System;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sysmanager_customer_credit : System.Web.UI.Page
{

    protected int totalCount;
    protected int page;
    protected int pageSize;
    protected int status;
    protected int category_id;
    protected string keywords = string.Empty;

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
        int nav_id = 57;
        if (!myrv.QXExists(role_id, nav_id))
        {
            Response.Redirect("../error.html");
            Response.End();
        }
        this.status = AXRequest.GetQueryInt("status");
        this.category_id = AXRequest.GetQueryInt("category_id");
        this.keywords = AXRequest.GetQueryString("keywords");
        this.pageSize = GetPageSize(20); //每页数量
        this.page = AXRequest.GetQueryInt("page", 1);

        if (!Page.IsPostBack)
        {
            
            TreeBind(Convert.ToInt32(Session["DepotCatID"])); //绑定地区
            if (Convert.ToInt32(Session["DepotID"]) == 0 && Convert.ToInt32(Session["DepotCatID"]) == 0)
            {
                RptBind("id>0" + CombSqlTxt(this.status, this.category_id, this.keywords), " id desc");
            }
            else if ( Convert.ToInt32(Session["DepotCatID"]) > 0)
            {
                this.ddlCategoryId.SelectedValue = Session["DepotCatID"].ToString();
                RptBind("category_id=" + Convert.ToInt32(Session["DepotCatID"]) + CombSqlTxt(this.status, this.category_id, this.keywords), " id desc");
            }

        }
    }

    #region 绑定地区=================================
    private void TreeBind(int _category_id)
    {
        ps_depot_category bll = new ps_depot_category();
        DataTable dt = bll.GetList(_category_id);
        this.ddlCategoryId.Items.Clear();
        this.ddlCategoryId.Items.Add(new ListItem("==全部==", ""));
        foreach (DataRow dr in dt.Rows)
        {
            string Id = dr["id"].ToString();
            string Title = dr["name"].ToString().Trim();
            this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
        }
        if (Session["DepotCatID"].ToString() != "" && Session["DepotCatID"].ToString() != "0")
        {
            this.ddlCategoryId.SelectedValue = Session["DepotCatID"].ToString();
            this.ddlCategoryId.Enabled = false;
        }

    }
    #endregion

    #region 数据绑定=================================
    private void RptBind(string _strWhere, string _orderby)
    {
        this.page = AXRequest.GetQueryInt("page", 1);
      
        if (this.category_id > 0)
        {
            this.ddlCategoryId.SelectedValue = this.category_id.ToString();
        }
        
        txtKeywords.Text = this.keywords;

        ps_customer_credit bll = new ps_customer_credit();
        this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
        this.rptList.DataBind();

        //绑定页码
        txtPageNum.Text = this.pageSize.ToString();
        string pageUrl = Utils.CombUrlTxt("customer_credit.aspx", "status={0}&category_id={1}&keywords={2}&page={3}", this.status.ToString(), this.category_id.ToString(), this.keywords, "__id__");
        PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
    }
    #endregion

    #region 组合SQL查询语句==========================
    protected string CombSqlTxt(int _status, int _category_id, string _keywords)
    {
        StringBuilder strTemp = new StringBuilder();
        if (_status > 0)
        {
            strTemp.Append(" and status=" + _status);
        }
        if (_category_id > 0)
        {
            strTemp.Append(" and category_id=" + _category_id);
        }
        if (Session["DepotID"].ToString() != "" && Session["DepotID"].ToString() != "0")
        {
            strTemp.Append(" and depot_id='" + Session["DepotID"].ToString() + "' ");
        }
        _keywords = _keywords.Replace("'", "");
        if (!string.IsNullOrEmpty(_keywords))
        {
            strTemp.Append(" and (CustID like  '%" + _keywords + "%' or CustNum like '%" + _keywords + "%' or  CustName like '%" + _keywords + "%' )");
        }
        return strTemp.ToString();
    }
    #endregion

    #region 返回每页数量=============================
    private int GetPageSize(int _default_size)
    {
        int _pagesize;
        if (int.TryParse(Utils.GetCookie("depot_page_size"), out _pagesize))
        {
            if (_pagesize > 0)
            {
                return _pagesize;
            }
        }
        return _default_size;
    }
    #endregion

    //关健字查询
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect(Utils.CombUrlTxt("customer_credit.aspx", "status={0}&category_id={1}&keywords={2}", this.status.ToString(), this.category_id.ToString(), txtKeywords.Text));
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        (new EpicorRequest()).GetCustomerCreditInfo("");
        btnSearch_Click(sender,e);
    }
    

    //筛选地区
    protected void ddlCategoryId_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect(Utils.CombUrlTxt("customer_credit.aspx", "status={0}&category_id={1}&keywords={2}",
            this.status.ToString(), ddlCategoryId.SelectedValue, this.keywords));
    }

    //设置分页数量
    protected void txtPageNum_TextChanged(object sender, EventArgs e)
    {
        int _pagesize;
        if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
        {
            if (_pagesize > 0)
            {
                Utils.WriteCookie("depot_page_size", _pagesize.ToString(), 14400);
            }
        }
        Response.Redirect(Utils.CombUrlTxt("customer_credit.aspx", "status={0}&category_id={1}&keywords={2}", this.status.ToString(), this.category_id.ToString(), this.keywords));
    }

    //导出报表
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Redirect(Utils.CombUrlTxt("depot_rep.aspx", "status={0}&category_id={1}&keywords={2}", this.status.ToString(), this.category_id.ToString(), this.keywords));
    }
}
