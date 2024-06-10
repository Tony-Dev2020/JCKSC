using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class order_my_order_info : System.Web.UI.Page
{
    protected int page;
    private string action = ""; //操作类型
    private int id = 0;
    ManagePage mym = new ManagePage();
    protected ps_orders model = new ps_orders();
    protected ps_order_goods bllOrderGoods = new ps_order_goods();

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
        int nav_id = 22;
        if (!myrv.QXExists(role_id, nav_id))
        {
            Response.Redirect("../error.html");
            Response.End();
        }
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
            if (action == "Edit") //修改
            {
                ShowInfo(this.id);
            }
        }

    }

    #region 赋值操作=================================
    private void ShowInfo(int _id)
    {
        double totalAmount = 0;
        double totalRealAmount = 0;
        double totalDiscountAmount = 0;
        model.GetModel(_id);
        //绑定商品列表
        ps_order_goods bll = new ps_order_goods();
        string sql = " order_id =" + _id;

        DataTable dt = bll.GetList(sql).Tables[0];
        this.rptList.DataSource = dt;
        this.rptList.DataBind();

        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    totalAmount = totalAmount + Convert.ToDouble(dt.Rows[i]["real_price"]) * (100 - Convert.ToDouble(dt.Rows[i]["quantity"]) * Convert.ToDouble(dt.Rows[i]["discount"].ToString() == "" ? "0" : dt.Rows[i]["discount"].ToString())) / 100;
        //}

        if (Session["IsDisplayPrice"] == null || Session["IsDisplayPrice"].ToString() == "1")
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                totalAmount = totalAmount + Convert.ToDouble(dt.Rows[i]["real_price"]) * Convert.ToDouble(dt.Rows[i]["quantity"]);
                totalRealAmount = totalRealAmount + Convert.ToDouble(dt.Rows[i]["real_price"]) * (Convert.ToDouble(dt.Rows[i]["quantity"]) * (Convert.ToDouble(dt.Rows[i]["discount"].ToString() == "" ? "0" : dt.Rows[i]["discount"].ToString()))) / 100;
                totalDiscountAmount = totalDiscountAmount + Convert.ToDouble(dt.Rows[i]["real_price"]) * (Convert.ToDouble(dt.Rows[i]["quantity"]) * (100 - Convert.ToDouble(dt.Rows[i]["discount"].ToString() == "" ? "0" : dt.Rows[i]["discount"].ToString()))) / 100;
            }

            if (txtTotalAmount.Text != "" && txtTotalAmount.Text != "0")
            {
                txtTotalAmount.Text = String.Format("{0:N}", MyConvert(totalAmount));
            }
            if (txtTotalDiscountAmount.Text != "" && txtTotalDiscountAmount.Text != "0")
            {
                txtTotalDiscountAmount.Text = String.Format("{0:N}", MyConvert(totalDiscountAmount));
            }
            if (txtTotalRealAmount.Text != "" && txtTotalRealAmount.Text != "0")
            {
                txtTotalRealAmount.Text = String.Format("{0:N}", MyConvert(totalRealAmount));
            }
        }
        else
        {
            txtTotalAmount.Text = "**";
            txtTotalDiscountAmount.Text = "**";
            txtTotalRealAmount.Text = "**";
        }

        //获得商家信息
        if (model.depot_id > 0)
        {
            ps_depot user_info = new ps_depot();
            user_info.GetModel(Convert.ToInt32(model.depot_id));
            user_name.Text = model.user_name;
            title.Text = user_info.title;
            contact_address.Text = model.address!="" ? model.address  : user_info.contact_address;
            contact_name.Text = model.contract_name != "" ? model.contract_name : user_info.contact_name;
            contact_tel.Text = model.contact_number != "" ? model.contact_number : user_info.contact_mobile;
        }
        if (model.attachment1 != "")
            palAttachment1.Visible = true;
        if (model.attachment2 != "")
            palAttachment2.Visible = true;
        if (model.attachment3 != "")
            palAttachment3.Visible = true;


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