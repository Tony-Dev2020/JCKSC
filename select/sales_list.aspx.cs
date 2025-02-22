﻿using System;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class select_sales_list : System.Web.UI.Page
{
    protected int totalCount;
    protected int page;
    protected int pageSize;

    protected int depot_category_id;
    protected int depot_id;

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
        int nav_id = 17;
        if (!myrv.QXExists(role_id, nav_id))
        {
            Response.Redirect("../error.html");
            Response.End();
        }

        this.depot_category_id = AXRequest.GetQueryInt("depot_category_id");
        this.depot_id = AXRequest.GetQueryInt("depot_id");

        this.note_no = AXRequest.GetQueryString("note_no");
        if (AXRequest.GetQueryString("start_time") == "")
        {
            this.start_time = DateTime.Now.ToString("yyyy-MM-01");
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
            DQBind(Convert.ToInt32(Session["DepotCatID"])); //绑定商家地区
            SJBind(depot_category_id); //绑定下单商家
            RptBind("id>0" + CombSqlTxt(this.depot_category_id, this.depot_id, this.note_no, this.start_time, this.stop_time), "add_time desc,id desc");
         
        }
    }

    #region 绑定商家地区=================================
    private void DQBind(int _category_id)
    {
        ps_depot_category bll = new ps_depot_category();
        DataTable dt = bll.GetList(_category_id);
        this.ddldepot_category_id.Items.Clear();
        this.ddldepot_category_id.Items.Add(new ListItem("==全部==", "0"));
        foreach (DataRow dr in dt.Rows)
        {
            string Id = dr["id"].ToString();
            string Title = dr["name"].ToString().Trim();
            this.ddldepot_category_id.Items.Add(new ListItem(Title, Id));
        }
    }
    #endregion

    #region 绑定下单商家=================================
    private void SJBind(int _category_id)
    {
        ps_depot bll = new ps_depot();
        DataTable dt = bll.GetList("category_id=" + _category_id + "and status=1").Tables[0];
        this.ddldepot_id.Items.Clear();
        this.ddldepot_id.Items.Add(new ListItem("==全部==", ""));
        foreach (DataRow dr in dt.Rows)
        {
            string Id = dr["id"].ToString();
            string Title = dr["title"].ToString().Trim();
            this.ddldepot_id.Items.Add(new ListItem(Title, Id));
        }
    }
    #endregion

    #region 数据绑定=================================
    private void RptBind(string _strWhere, string _orderby)
    {
        this.page = AXRequest.GetQueryInt("page", 1);

        if (this.depot_category_id > 0)
        {
            this.ddldepot_category_id.SelectedValue = this.depot_category_id.ToString();
        }
        if (this.depot_id > 0)
        {
            this.ddldepot_id.SelectedValue = this.depot_id.ToString();
        }

        txtNote_no.Text = this.note_no;
        txtstart_time.Value = this.start_time;
        txtstop_time.Value = this.stop_time;

        ps_salse_depot bll = new ps_salse_depot();
        this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
        this.rptList.DataBind();

        //this.Literal_go_price.Text = MyConvert(bll.GetTitleSum(_strWhere, "sum(goods_price)"));
        //this.Literal_zongprice.Text = MyConvert(bll.GetTitleSum(_strWhere, "sum(real_price)"));
        //this.Literal_zongprice_sj.Text = MyConvert(bll.GetTitleSum(_strWhere, "sum(quantity)"));
        this.Literal_lrprice.Text = MyConvert(Convert.ToDecimal(bll.GetTitleSum(_strWhere, " sum(real_price*quantity)")) - Convert.ToDecimal(bll.GetTitleSum(_strWhere, "sum(goods_price*quantity)")));
        this.Literal_hj.Text = MyConvert(Convert.ToDecimal(bll.GetTitleSum(_strWhere, "sum(real_price*quantity)")));
        
        //绑定页码
        txtPageNum.Text = this.pageSize.ToString();
        string pageUrl = Utils.CombUrlTxt("sales_list.aspx", "depot_category_id={0}&depot_id={1}&start_time={2}&stop_time={3}&note_no={4}&page={5}", this.depot_category_id.ToString(), this.depot_id.ToString(), this.txtstart_time.Value, this.txtstop_time.Value, txtNote_no.Text, "__id__");
        PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
    }
    #endregion

    #region 组合SQL查询语句==========================
    protected string CombSqlTxt(int _depot_category_id, int _depot_id,string _note_no, string _start_time, string _stop_time)
    {
        StringBuilder strTemp = new StringBuilder();

        if (_depot_category_id > 0)
        {
            strTemp.Append(" and depot_category_id=" + _depot_category_id);
        }
        if (_depot_id > 0)
        {
            strTemp.Append(" and depot_id=" + _depot_id);
        }

        if (string.IsNullOrEmpty(_start_time))
        {
            _start_time = "1900-01-01";
        }
        if (string.IsNullOrEmpty(_stop_time))
        {
            _stop_time = "2099-01-01";
        }
        strTemp.Append(" and add_time between  '" + DateTime.Parse(_start_time) + "' and '" + DateTime.Parse(_stop_time + " 23:59:59") + "'");

        _note_no = _note_no.Replace("'", "");
        if (!string.IsNullOrEmpty(_note_no))
        {
            strTemp.Append(" and note_no like  '%" + _note_no + "%' ");
        }
        return strTemp.ToString();
    }
    #endregion

    #region 返回每页数量=============================
    private int GetPageSize(int _default_size)
    {
        int _pagesize;
        if (int.TryParse(Utils.GetCookie("sales_page_size"), out _pagesize))
        {
            if (_pagesize > 0)
            {
                return _pagesize;
            }
        }
        return _default_size;
    }
    #endregion


    //查询
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect(Utils.CombUrlTxt("sales_list.aspx", "depot_category_id={0}&depot_id={1}&start_time={2}&stop_time={3}&note_no={4}", this.depot_category_id.ToString(), this.depot_id.ToString(), this.txtstart_time.Value, this.txtstop_time.Value,  txtNote_no.Text));
    }

    //筛选商家地区
    protected void ddldepot_category_id_SelectedIndexChanged(object sender, EventArgs e)
    {
        SJBind(Convert.ToInt32(ddldepot_category_id.SelectedValue));
        Response.Redirect(Utils.CombUrlTxt("sales_list.aspx", "depot_category_id={0}&depot_id={1}&start_time={2}&stop_time={3}&note_no={4}", this.ddldepot_category_id.SelectedValue, "", this.txtstart_time.Value, this.txtstop_time.Value, txtNote_no.Text));
    }

    //筛选下单商家
    protected void ddldepot_id_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect(Utils.CombUrlTxt("sales_list.aspx", "depot_category_id={0}&depot_id={1}&start_time={2}&stop_time={3}&note_no={4}", this.depot_category_id.ToString(), this.ddldepot_id.SelectedValue, this.txtstart_time.Value, this.txtstop_time.Value, txtNote_no.Text));
    }


    //设置分页数量
    protected void txtPageNum_TextChanged(object sender, EventArgs e)
    {
        int _pagesize;
        if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
        {
            if (_pagesize > 0)
            {
                Utils.WriteCookie("sales_page_size", _pagesize.ToString(), 14400);
            }
        }
        Response.Redirect(Utils.CombUrlTxt("sales_list.aspx", "depot_category_id={0}&depot_id={1}&start_time={2}&stop_time={3}&note_no={4}", this.depot_category_id.ToString(), this.depot_id.ToString(), this.txtstart_time.Value, this.txtstop_time.Value, txtNote_no.Text));
    }

    //导出报表
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Redirect(Utils.CombUrlTxt("sales_rep.aspx", "depot_category_id={0}&depot_id={1}&start_time={2}&stop_time={3}&note_no={4}", this.depot_category_id.ToString(), this.depot_id.ToString(), this.txtstart_time.Value, this.txtstop_time.Value, txtNote_no.Text));

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
