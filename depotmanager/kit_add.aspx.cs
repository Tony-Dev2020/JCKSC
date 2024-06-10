using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class depotmanager_kit_add : System.Web.UI.Page
{
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
        int nav_id = 5;
        if (!myrv.QXExists(role_id, nav_id))
        {
            Response.Redirect("../error.html");
            Response.End();
        }

        if (!Page.IsPostBack)
        {
            DQBind();
            Focus myFocus = new Focus();
            myFocus.SetEnterControl(this.txtKitNumber);
            myFocus.SetFocus(txtKitNumber.Page, "txtKitNumber");
        }
    }

    #region 绑定商品类别=================================
    private void DQBind()
    {
        ps_depot_category bll = new ps_depot_category();
        DataTable dt = bll.GetList(0);
        this.ddldepot_category_id.Items.Clear();
        foreach (DataRow dr in dt.Rows)
        {
            string Id = dr["title"].ToString();
            string Title = dr["name"].ToString().Trim();
            this.ddldepot_category_id.Items.Add(new ListItem(Title, Id));
        }
    }
    #endregion

    #region 增加操作=================================
    private bool DoAdd()
    {
        DateTime now = DateTime.Now;
        string note_no = now.ToString("yy") + now.ToString("MM") + now.ToString("dd") + now.ToString("HH") + now.ToString("mm") + now.ToString("ss");

        ps_join_depot model = new ps_join_depot();
        ps_product_category bll = new ps_product_category();
        DataTable dt = bll.GetList("1=1 AND title='T001-套件'").Tables[0];

        //model.product_category_id = int.Parse(ddldepot_category_id.SelectedValue);
        model.product_category_id = int.Parse(dt.Rows[0]["ID"].ToString());
        model.note_no = note_no;
        model.add_time = DateTime.Now;
        model.product_name = txtKitName.Text;
        model.product_code_state = "入库";
        model.go_price = 0;
        model.salse_price = Convert.ToDecimal(txtsalse_price.Text);
        model.user_id = Convert.ToInt32(Session["AID"]);
        model.product_num = 99999;
        model.dw = txtdw.Text;

        ps_here_depot model1 = new ps_here_depot();
        model1.product_url = txtImgUrl.Text;
        model1.product_category_id = int.Parse(dt.Rows[0]["ID"].ToString());
        model1.add_time = DateTime.Now;
        model1.product_name = txtKitName.Text;
        model1.go_price = 0;
        model1.salse_price = Convert.ToDecimal(txtsalse_price.Text);
        model1.user_id = Convert.ToInt32(Session["AID"]);
        model1.product_num = 99999;
        model1.dw = txtdw.Text;
        model1.remark = txtremark.Text;
        model1.Add();
        model.here_depot_id = model1.GetMaxId(Convert.ToInt32(Session["AID"]));

        if (model.Add() > 0)
        {
            mym.AddAdminLog("入库", "商品入库，入库单号：" + note_no); //记录日志
            return true;
        }

        return false;
    }
    #endregion

    //保存
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!DoAdd())
        {
            mym.JscriptMsg(this.Page, "保存过程中发生错误！", "", "Error");
            return;
        }
        mym.JscriptMsg(this.Page, "增加新品成功！", "", "Success");
        txtKitName.Text = "";
        txtImgUrl.Text = "";
        txtgo_price.Text = "";
        txtsalse_price.Text = "";
        txtproduct_num.Text = "";
        txtdw.Text = "";
        txtremark.Text = "";
    }

}