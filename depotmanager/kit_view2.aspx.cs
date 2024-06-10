using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class depotmanager_kit_view2 : System.Web.UI.Page
{
    ManagePage mym = new ManagePage();
    protected int page;
    private string action = ""; //操作类型
    protected string dw = ""; //计量单位
    private int id = 0;
    private int kitid = 0;
    protected int totalCount;
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


        string _action = AXRequest.GetQueryString("action");
        this.page = AXRequest.GetQueryInt("page", 1);
        if (!string.IsNullOrEmpty(_action) && _action == "Edit")
        {
            this.action = "Edit";//修改类型
            if (!int.TryParse(Request.QueryString["id"] as string, out this.id))
            {
                mym.JscriptMsg(this.Page, "传输参数不正确！", "back", "Error");
                return;
            }

        }


        if (!Page.IsPostBack)
        {
            RptBind(" kit_id=" + this.id, "id asc");
            Focus myFocus = new Focus();
            myFocus.SetEnterControl(this.txtKitNumber);
            myFocus.SetFocus(txtKitNumber.Page, "txtKitNumber");
            if (action == "Edit") //修改
            {
                ShowInfo(this.id);
            }
        }
    }
    #region 赋值操作=================================
    private void ShowInfo(int _id)
    {
        ps_here_depot model1 = new ps_here_depot();
        model1.GetModel(_id);

        this.dw = new ps_product_category().GetDW(Convert.ToInt32(model1.product_category_id));
        

       
        this.txtKitNumber.Text = model1.product_no;
        this.txtKitName.Text = model1.product_name;
        
        this.dw = model1.dw;

        this.txtKitNumber.Enabled = false;
        this.txtKitName.Enabled = false;
        if (model1.product_url != "")
        {
            this.imgPhoto.Visible = true;
            this.imgPhoto.ImageUrl = model1.product_url;//界面显示图片
        }
        else
        {
            this.imgPhoto.Visible = false;
        }


    }
    #endregion

    #region 数据绑定=================================
    private void RptBind(string _strWhere, string _orderby)
    {
        ps_kitpart bll = new ps_kitpart();
        this.rptList.DataSource = bll.GetList(100, this.page, _strWhere, _orderby, out this.totalCount);
        this.rptList.DataBind();
    }
    #endregion








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