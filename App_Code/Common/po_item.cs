using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 购物车实体类
/// </summary>
[Serializable]
public partial class po_items
{
    public po_items()
    { }
    #region Model
    private long _id;
    private string _product_no;
    private string _title;
    private string _dw;
    private string _specification;
    private string _commercialStyle;
    private string _commercialcolor;
    private string _img_url;
    private decimal _price = 0M;
    private int _quantity = 1;
    private int _stock_quantity = 0;
    private int _product_category_id = 0;
    private string _remark;
    private string _po_num;
    private string _po_line;
    private decimal _discount = 0M;
    private bool _is_cust = false;

    /// <summary>
    /// 商品ID
    /// </summary>
    public long id
    {
        set { _id = value; }
        get { return _id; }
    }
    /// <summary>
    /// 商品No
    /// </summary>
    public string product_no
    {
        set { _product_no = value; }
        get { return _product_no; }
    }
    /// <summary>
    /// 商品名称
    /// </summary>
    public string title
    {
        set { _title = value; }
        get { return _title; }
    }

    public string specification
    {
        set { _specification = value; }
        get { return _specification; }
    }

    public string commercialStyle
    {
        set { _commercialStyle = value; }
        get { return _commercialStyle; }
    }

    public string commercialcolor
    {
        set { _commercialcolor = value; }
        get { return _commercialcolor; }
    }

    /// <summary>
    /// 图片路径
    /// </summary>
    public string img_url
    {
        set { _img_url = value; }
        get { return _img_url; }
    }
    /// <summary>
    /// 单位
    /// </summary>
    public string dw
    {
        set { _dw = value; }
        get { return _dw; }
    }
    /// <summary>
    /// 销售单价
    /// </summary>
    public decimal price
    {
        set { _price = value; }
        get { return _price; }
    }

    /// <summary>
    /// 购买数量
    /// </summary>
    public int quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }
    /// <summary>
    /// 库存数量
    /// </summary>
    public int stock_quantity
    {
        set { _stock_quantity = value; }
        get { return _stock_quantity; }
    }
    /// <summary>
    /// 商品种类
    /// </summary>
    public int product_category_id
    {
        set { _product_category_id = value; }
        get { return _product_category_id; }
    }

    /// <summary>
    /// 描述
    /// </summary>
    public string remark
    {
        set { _remark = value; }
        get { return _remark; }
    }

    public string po_num
    {
        set { _po_num = value; }
        get { return _po_num; }
    }

    public string po_line
    {
        set { _po_line = value; }
        get { return _po_line; }
    }

    public decimal discount
    {
        set { _discount = value; }
        get { return _discount; }
    }

    public bool is_cust
    {
        set { _is_cust = value; }
        get { return _is_cust; }
    }



    #endregion
}

/// <summary>
/// 购物车属性类
/// </summary>
[Serializable]
public partial class pocart_total
{
    public pocart_total()
    { }
    #region Model
    private int _total_num = 0;
    private int _total_quantity = 0;
    private decimal _payable_amount = 0M;
    private decimal _real_amount = 0M;

    /// <summary>
    /// 商品种数
    /// </summary>
    public int total_num
    {
        set { _total_num = value; }
        get { return _total_num; }
    }
    /// <summary>
    /// 商品总数量
    /// </summary>
    public int total_quantity
    {
        set { _total_quantity = value; }
        get { return _total_quantity; }
    }
    /// <summary>
    /// 预定商品总金额
    /// </summary>
    public decimal payable_amount
    {
        set { _payable_amount = value; }
        get { return _payable_amount; }
    }
    /// <summary>
    /// 实付商品总金额
    /// </summary>
    public decimal real_amount
    {
        set { _real_amount = value; }
        get { return _real_amount; }
    }

    #endregion
}