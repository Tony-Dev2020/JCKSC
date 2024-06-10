
using System;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class select_kujiale_select : System.Web.UI.Page
{

    protected int totalCount;
    protected int page;
    protected int pageSize;
    protected int status;
    protected int category_id;
    protected string selType;
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
        this.keywords = AXRequest.GetQueryString("keywords");
        this.pageSize = GetPageSize(10); //每页数量
        this.page = AXRequest.GetQueryInt("page", 1);

        if (!Page.IsPostBack)
        {
            RptBind("id>0" + CombSqlTxt(this.keywords), "id desc");

        }
    }



    #region 数据绑定=================================
    private void RptBind(string _strWhere, string _orderby)
    {
        this.page = AXRequest.GetQueryInt("page", 1);
        
        txtKeywords.Text = this.keywords;
        ps_depot bll = new ps_depot();
        this.rptList.DataSource = bll.GetKuJiaLeList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
        this.rptList.DataBind();

        //绑定页码
        txtPageNum.Text = this.pageSize.ToString();
        string pageUrl = Utils.CombUrlTxt("kujiale_select.aspx", "keywords={0}", this.txtKeywords.Text.ToString(), "__id__");
        PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
    }
    #endregion

    #region 组合SQL查询语句==========================
    protected string CombSqlTxt(string _keywords)
    {
        StringBuilder strTemp = new StringBuilder();
        
        _keywords = _keywords.Replace("'", "");
        if (!string.IsNullOrEmpty(_keywords))
        {
            strTemp.Append(" and (name like  '%" + _keywords + "%' or description like '%" + _keywords + "%' )");
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
        Response.Redirect(Utils.CombUrlTxt("kujiale_select.aspx", "keywords={0}", this.txtKeywords.Text.ToString()));
    }
    
    //筛选地区
    protected void ddlCategoryId_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect(Utils.CombUrlTxt("kujiale_select.aspx", "keywords={0}", this.txtKeywords.Text.ToString()));
    }
    //筛选状态
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect(Utils.CombUrlTxt("kujiale_select.aspx", "keywords={0}", this.txtKeywords.Text.ToString()));
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
        Response.Redirect(Utils.CombUrlTxt("kujiale_select.aspx", "keywords={0}", this.keywords.ToString()));
    }

    
}
