using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class depotmanager_kit_edit : System.Web.UI.Page
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
        int role_id = Convert.ToInt32(Session["RoleID"]);
        int nav_id = 5;
        if (!myrv.QXExists(role_id, nav_id))
        {
            Response.Redirect("../error.html");
            Response.End();
        }

        string _action = AXRequest.GetQueryString("action");
        this.page = AXRequest.GetQueryInt("page", 1);
        if (!string.IsNullOrEmpty(_action) && (_action == "Edit" || _action == "Copy"))
        {
            //this.action = "Edit";//修改类型
            this.action = _action;//修改类型
            if (!int.TryParse(Request.QueryString["id"] as string, out this.id))
            {
                mym.JscriptMsg(this.Page, "传输参数不正确！", "back", "Error");
                return;
            }


        }


        if (!Page.IsPostBack)
        {
            DQBind();
            PSBind();
            RptBind(" kit_id=" + this.id, "id asc");
            Focus myFocus = new Focus();
            myFocus.SetEnterControl(this.txtKitNumber);
            myFocus.SetFocus(txtKitNumber.Page, "txtKitNumber");
            if (action == "Edit" || action == "Copy") //修改
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
        this.ddldepot_category_id.SelectedValue = model1.company.ToString();
        this.ddlproduct_series_id.SelectedValue = model1.product_series_id.ToString();
        this.txtImgUrl.Text = model1.product_url;
        if (model1.product_url != "")
        {
            this.imgPhoto.Visible = true;
            this.imgPhoto.ImageUrl = model1.product_url;//界面显示图片
        }
        else
        {
            this.imgPhoto.Visible = false;
        }
        if(this.action =="Edit")
            this.txtKitNumber.Text = model1.product_no;
        this.txtKitName.Text = model1.product_name;
        this.txtdw.Text = model1.dw;
        this.txtgo_price.Text = "0";
        this.txtsalse_price.Text = MyConvert(model1.salse_price.ToString());
        this.txtproduct_num.Text = model1.product_num.ToString();
        this.dw = model1.dw;
        this.cbIsLock.Checked = model1.is_xs == 0 ? true : false;

        this.txtKitNumber.Enabled = false;
        if (this.action == "Copy")
            this.txtKitNumber.Enabled = true;

        //this.btnInActive.Visible = false;
        //if (this.action == "Edit" && model1.status == 0)
        //    this.btnInActive.Visible = true;
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
    #region 绑定产品系列=================================
    private void PSBind()
    {
        ps_product_series bll = new ps_product_series();
        DataTable dt = bll.GetList("1=1").Tables[0];
        this.ddlproduct_series_id.Items.Clear();
        this.ddlproduct_series_id.Items.Add(new ListItem("==请选择产品系列==", "0"));
        foreach (DataRow dr in dt.Rows)
        {
            string Id = dr["id"].ToString();
            string Title = dr["name"].ToString().Trim();
            this.ddlproduct_series_id.Items.Add(new ListItem(Title, Id));
        }
    }
    #endregion
    #region 增加操作=================================
    private bool DoAdd()
    {
        DateTime now = DateTime.Now;
        string note_no = now.ToString("yy") + now.ToString("MM") + now.ToString("dd") + now.ToString("HH") + now.ToString("mm") + now.ToString("ss");

        ps_here_depot blldepot = new ps_here_depot();
        if (this.txtKitNumber.Text.Trim()=="")
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Error", "alert( '请输入套件编码！');", true);
            return false;
        }
        if (blldepot.IsExistsByProductNo(this.txtKitNumber.Text))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Error", "alert( '套件编码已存在！');", true);
            return false;
        }

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
        //model1.product_url = "/upload/Part.jpg";
        model1.company = ddldepot_category_id.SelectedValue;
        model1.product_category_id = int.Parse(dt.Rows[0]["ID"].ToString());
        model1.product_series_id = int.Parse(ddlproduct_series_id.SelectedValue);
        model1.add_time = DateTime.Now;
        model1.product_name = txtKitName.Text;
        model1.product_no = txtKitNumber.Text;
        model1.product_url = this.txtImgUrl.Text;
        model1.go_price = 0;
        model1.salse_price = Convert.ToDecimal(txtsalse_price.Text);
        model1.user_id = Convert.ToInt32(Session["AID"]);
        model1.product_num = 99999;
        model1.dw = txtdw.Text;
        model1.remark = txtremark.Text;
        model1.is_kit = 1;
        if (FileUpload1.FileName != "")
        {
            string FullName = FileUpload1.PostedFile.FileName;//获取图片物理地址
            FileInfo fi = new FileInfo(FullName);
            string photoname = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fi.Name;//获取图片名称
            string phototype = fi.Extension;//获取图片类型
            if (phototype == ".jpg" || phototype == ".gif" || phototype == ".bmp" || phototype == ".png")
            {
                string SavePath = Server.MapPath("~\\upload\\image");//图片保存到文件夹下
                this.FileUpload1.PostedFile.SaveAs(SavePath + "\\" + photoname);//保存路径
                this.imgPhoto.Visible = true;
                this.imgPhoto.ImageUrl = "~\\upload\\image" + "\\" + photoname;//界面显示图片
            }
            model1.product_url = "\\upload\\image" + "\\" + photoname;
        }

        this.id = model1.Add();
        model.here_depot_id = model1.GetMaxId(Convert.ToInt32(Session["AID"]));
         

        if (model.Add() > 0)
        {
            mym.AddAdminLog("入库", "商品入库，入库单号：" + note_no); //记录日志
            return true;
        }

        return false;
    }
    #endregion

    #region 修改操作=================================
    private bool DoEdit(int _id)
    {
        bool result = false;
        ps_here_depot model = new ps_here_depot();
        model.GetModel(_id);

        model.product_url = this.txtImgUrl.Text;
        //model.product_category_id = int.Parse(ddldepot_category_id.SelectedValue);
        model.company = ddldepot_category_id.SelectedValue;
        model.product_series_id = int.Parse(ddlproduct_series_id.SelectedValue);
        model.product_no = this.txtKitNumber.Text;
        model.product_name =this.txtKitName.Text;
        model.dw = txtdw.Text;
        model.go_price = Convert.ToDecimal(this.txtgo_price.Text);
        model.salse_price = Convert.ToDecimal(this.txtsalse_price.Text);
        model.is_kit = 1;
        model.is_xs = cbIsLock.Checked ? 0 : 1;
        if (FileUpload1.FileName != "")
        {
            string FullName = FileUpload1.PostedFile.FileName;//获取图片物理地址
            FileInfo fi = new FileInfo(FullName);
            string photoname = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fi.Name;//获取图片名称
            string phototype = fi.Extension;//获取图片类型
            if (phototype == ".jpg" || phototype == ".gif" || phototype == ".bmp" || phototype == ".png")
            {
                string SavePath = Server.MapPath("~\\upload\\image");//图片保存到文件夹下
                this.FileUpload1.PostedFile.SaveAs(SavePath + "\\" + photoname);//保存路径
                this.imgPhoto.Visible = true;
                this.imgPhoto.ImageUrl = "~\\upload\\image" + "\\" + photoname;//界面显示图片
            }
            model.product_url = "\\upload\\image" + "\\" + photoname;
        }
        if (model.UpdateALL())
        {
            mym.AddAdminLog("修改", "修改商品:" + txtKitName.Text); //记录日志
            result = true;
        }

        return result;
    }

    private bool DoInActive(int _id)
    {
        bool result = false;
        ps_here_depot model = new ps_here_depot();
        model.GetModel(_id);

       
        model.status = 1;
       
        if (model.UpdateALL())
        {
            mym.AddAdminLog("停用", "停用商品:" + txtKitName.Text); //记录日志
            result = true;
        }

        return result;
    }
    #endregion

    #region 增加组件操作=================================
    private bool DoAddEditKit()
    {
        DateTime now = DateTime.Now;

        ps_here_depot blldepot = new ps_here_depot();
        if (this.txtPartNo.Text != "")
        {
            if (blldepot.IsExistsByProductNo(this.txtPartNo.Text) == false)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Error", "alert( '组件编码不存在！');", true);
                return false;
            }
        }

        ps_kitpart modelkitpart = new ps_kitpart();
        
        modelkitpart.kit_id = this.id;
        modelkitpart.partnumber = this.txtPartNo.Text;
        modelkitpart.partdesc = this.txtPartDesc.Text;
        modelkitpart.uom = "";
        modelkitpart.qty = Convert.ToDecimal(this.txtPartQty.Text);
        modelkitpart.unitprice = Convert.ToDecimal(this.txtPartUnitPrice.Text);
        modelkitpart.sort_id = 0;
        modelkitpart.remark = this.txtPartRemarks.Text;
        modelkitpart.isCust = this.cbIsCust.Checked;

        if (this.txtKitID.Text != "0" && this.txtKitID.Text != "")
        {
            modelkitpart.id = Convert.ToInt32(this.txtKitID.Text);
            if (modelkitpart.Update(modelkitpart))
            {
                mym.AddAdminLog("组件", "更新组件，组件子件：" + this.txtPartNo.Text); //记录日志
                return true;
            }
        }
        else
            this.txtKitID.Text = modelkitpart.Add(modelkitpart).ToString();
        

        if (this.txtKitID.Text !="0" && this.txtKitID.Text!="")
        {
            mym.AddAdminLog("组件", "添加组件，组件子件：" + this.txtPartNo.Text); //记录日志
            return true;
        }

        return false;
    }

    private bool CopyKit(int org_kitid,int new_kitid )
    {
        ps_kitpart modelkitpart = new ps_kitpart();

        if (modelkitpart.CopyKit(org_kitid, new_kitid))
        {
            mym.AddAdminLog("组件", "复制组件，组件子件：" + this.txtPartNo.Text); //记录日志
            return true;
        }
        return false;
    }

    protected void lbtnDelKit_Click(object sender, EventArgs e)
    {
        this.page = AXRequest.GetQueryInt("page", 1);
        // 当前点击的按钮
        LinkButton lb = (LinkButton)sender;
        int kitid = int.Parse(lb.CommandArgument);
        ps_kitpart bll = new ps_kitpart();
        bll.Delete(kitid);
        //mym.JscriptMsg(this.Page, "删除组件成功！", Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()), "Success");
        ScriptManager.RegisterStartupScript(this, typeof(string), "Success", "alert( '删除组件成功！');window.location='" + Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()) + "'", true);
    }

    protected void lbtnUpdateKit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        int kitid = int.Parse(lb.CommandArgument);
        this.kitid = kitid;
        this.txtKitID.Text = kitid.ToString() ;
        ps_kitpart modelkitpart = new ps_kitpart();
        modelkitpart = modelkitpart.GetModel(kitid);
        this.txtPartNo.Text = modelkitpart.partnumber;
        this.txtPartDesc.Text = modelkitpart.partdesc;
        this.txtPartQty.Text = modelkitpart.qty.ToString();
        this.txtPartUnitPrice.Text = modelkitpart.unitprice.ToString();
        this.txtPartRemarks.Text = modelkitpart.remark;
        this.cbIsCust.Checked = modelkitpart.isCust==true ? true :false;

    }
    #endregion

    #region 修改操作=================================
    private bool DoEditKit(int _id)
    {
        bool result = false;
        ps_here_depot model = new ps_here_depot();
        model.GetModel(_id);

        model.product_url = this.txtImgUrl.Text;
        //model.product_category_id = int.Parse(ddldepot_category_id.SelectedValue);
        model.product_no = this.txtKitNumber.Text;
        model.product_name = this.txtKitName.Text;
        model.dw = txtdw.Text;
        model.go_price = Convert.ToDecimal(this.txtgo_price.Text);
        model.salse_price = Convert.ToDecimal(this.txtsalse_price.Text);
        if (model.UpdateALL())
        {
            mym.AddAdminLog("修改", "修改商品:" + txtKitName.Text); //记录日志
            result = true;
        }

        return result;
    }
    #endregion

    //保存
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        if (action == "Edit") //修改
        {
            if (!DoEdit(this.id))
            {
                mym.JscriptMsg(this.Page, "保存过程中发生错误！", "", "Error");
                return;
            }
            DoAddEditKit();
            //mym.JscriptMsg(this.Page, "修改套件信息成功！", Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()), "Success");
            ScriptManager.RegisterStartupScript(this, typeof(string), "Success", "alert( '修改套件信息成功！');window.location='" + Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()) + "'", true);
        }
        else if (action == "Copy") //修改
        {
            int org_kitid = this.id;
            if (!DoAdd())
            {
                mym.JscriptMsg(this.Page, "保存过程中发生错误！", "", "Error");
                return;
            }
            else
            {
                CopyKit(org_kitid, this.id);
            }
            //mym.JscriptMsg(this.Page, "修改套件信息成功！", Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()), "Success");
            ScriptManager.RegisterStartupScript(this, typeof(string), "Success", "alert( '复制套件信息成功！');window.location='" + Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()) + "'", true);
        }
        else
        {
            if (!DoAdd())
            {
                mym.JscriptMsg(this.Page, "保存过程中发生错误！", "", "Error");
                return;
            }
            //mym.JscriptMsg(this.Page, "增加新品成功！", "", "Success");
            //mym.JscriptMsg(this.Page, "增加套件成功！", Utils.CombUrlTxt("kit_edit.aspx","action={0}&id={1}&page={2}","Edit", this.id.ToString(), this.page.ToString()), "Success");
            ScriptManager.RegisterStartupScript(this, typeof(string), "Success", "alert( '增加套件成功！');window.location='" + Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()) + "'", true);
            txtKitName.Text = "";
            txtImgUrl.Text = "";
            txtgo_price.Text = "";
            txtsalse_price.Text = "";
            txtproduct_num.Text = "";
            txtdw.Text = "";
            txtremark.Text = "";
        }
    }


    private void EditKit()
    {
        if (action == "Edit") //修改
        {
            if (!DoEdit(this.id))
            {
                mym.JscriptMsg(this.Page, "保存过程中发生错误！", "", "Error");
                return;
            }
            DoAddEditKit();
            //mym.JscriptMsg(this.Page, "修改套件信息成功！", Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()), "Success");
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Success", "alert( '修改套件信息成功！');window.location='" + Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()) + "'", true);
        }
        else if (action == "Copy") //修改
        {
            int org_kitid = this.id;
            if (!DoAdd())
            {
                mym.JscriptMsg(this.Page, "保存过程中发生错误！", "", "Error");
                return;
            }
            else
            {
                CopyKit(org_kitid, this.id);
            }
            //mym.JscriptMsg(this.Page, "修改套件信息成功！", Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()), "Success");
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Success", "alert( '复制套件信息成功！');window.location='" + Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()) + "'", true);
        }
        else
        {
            if (!DoAdd())
            {
                mym.JscriptMsg(this.Page, "保存过程中发生错误！", "", "Error");
                return;
            }
            //mym.JscriptMsg(this.Page, "增加新品成功！", "", "Success");
            //mym.JscriptMsg(this.Page, "增加套件成功！", Utils.CombUrlTxt("kit_edit.aspx","action={0}&id={1}&page={2}","Edit", this.id.ToString(), this.page.ToString()), "Success");
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Success", "alert( '增加套件成功！');window.location='" + Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()) + "'", true);
            txtKitName.Text = "";
            txtImgUrl.Text = "";
            txtgo_price.Text = "";
            txtsalse_price.Text = "";
            txtproduct_num.Text = "";
            txtdw.Text = "";
            txtremark.Text = "";
        }
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        
        if (action == "Edit") //修改
        {
            if (!DoInActive(this.id))
            {
                mym.JscriptMsg(this.Page, "保存过程中发生错误！", "", "Error");
                return;
            }
            //mym.JscriptMsg(this.Page, "修改套件信息成功！", Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()), "Success");
            ScriptManager.RegisterStartupScript(this, typeof(string), "Success", "alert( '套件已停用！');window.location='" + Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()) + "'", true);
        }
    }
    

    protected void btnAddKit_Click(object sender, EventArgs e)
    {
        EditKit();
        if (!DoAddEditKit())
        {
            //mym.JscriptMsg(this.Page, "保存过程中发生错误！", "", "Error");
            ScriptManager.RegisterStartupScript(this, typeof(string), "Error", "alert( '保存过程中发生错误！');", true);
            return;
        }
        else
        {
            //mym.JscriptMsg(this.Page, "修改组件信息成功！", Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()), "Success");
            ScriptManager.RegisterStartupScript(this, typeof(string), "Success", "alert( '修改组件信息成功！');window.location='"+ Utils.CombUrlTxt("kit_edit.aspx", "action={0}&id={1}&page={2}", "Edit", this.id.ToString(), this.page.ToString()) + "'", true);
        }
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