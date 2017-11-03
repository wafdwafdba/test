using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using GroupThink.CRM.IBLL;
using GroupThink.CRM.Common;
using GroupThink.CRM.Model.Sys;
using CRM_EasyUI_2016.Core;
using System.Transactions;

namespace CRM_EasyUI_2016.Areas.CustomerManager.Controllers
{
    public class CustomerController : BaseController
    {
        #region 接口
        [Dependency]
        public ICRM_Customer_BLL m_BLL { get; set; }
        [Dependency]
        public ICRM_CustomerConnect_BLL cc_BLL { get; set; }
        [Dependency]
        public ICRM_Type_BLL t_BLL { get; set; }
        [Dependency]
        public ICRM_Dept_BLL d_BLL { get; set; }
        [Dependency]
        public ICRM_User_BLL u_BLL { get; set; }
        [Dependency]
        public ICRM_Role_BLL r_BLL { get; set; }
        [Dependency]
        public ICRM_RoleUser_BLL ru_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        #endregion

        #region 查找
        //[SupportFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            //BindDropDownList();
            ViewData["uid"] = this.UserId;

            return View();
        }

        /// <summary>
        /// 获取客户列表（条件查询）
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="customerName">客户名称</param>
        /// <param name="contactName">联系人名称</param>
        /// <param name="userId">业务员</param>
        /// <param name="isCompany">公司/个人</param>
        /// <param name="customerSource">客户来源</param>
        /// <param name="customerBusiness">所属行业</param>
        /// <param name="beginDate1">创建时间起始</param>
        /// <param name="endDate1">创建时间截止</param>
        /// <param name="beginDate2">最后跟进时间起始</param>
        /// <param name="endDate2">最后跟进时间截止</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetList_(GridPager pager
            , string customerName
            , string contactName
            , string userId
            , string isCompany
            , string customerSource
            , string customerBusiness
            , string beginDate1
            , string endDate1
            , string beginDate2
            , string endDate2)
        {
            int _isCompany = Utils.DbInt(isCompany, 0);
            int _customerSource = Utils.DbInt(customerSource, 0);
            int _customerBusiness = Utils.DbInt(customerBusiness, 0);
            DateTime? _beginDate1 = Utils.DbDateTime(beginDate1);
            DateTime? _endDate1 = Utils.DbDateTime(endDate1);
            DateTime? _beginDate2 = Utils.DbDateTime(beginDate2);
            DateTime? _endDate2 = Utils.DbDateTime(endDate2);
            List<string> users = this.Users;

            List<CRM_Customer_Model> list = m_BLL.GetList_(ref pager, customerName, contactName, userId, _isCompany, _customerSource, _customerBusiness, _beginDate1, _endDate1, _beginDate2, _endDate2, users);

            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select r).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 创建
        public ActionResult Create()
        {
           // BindDropDownList();
            ViewData["UserId"] = GetCurrentUserId();
            ViewData["UserName"] = GetUserName();
            return View();
        }

        ///// <summary>
        ///// 选择用户
        ///// </summary>
        ///// <param name="act">动作</param>
        ///// <param name="CustomerId">客户编号</param>
        ///// <param name="uids">业务员编号</param>
        ///// <returns></returns>
        //public ActionResult SelectUser(string act, string CustomerId, string uids)
        //{
        //    ViewData["act"] = act;
        //    ViewData["uid"] = GetCurrentUserId();
        //    ViewData["CustomerId"] = CustomerId;
        //    ViewData["UserId"] = uids;//
        //    ViewData["Dept"] = (from a in d_BLL.GetAll()
        //                        select new SelectListItem
        //                        {
        //                            Text = a.DeptName,
        //                            Value = a.DeptID.ToString()
        //                        });
        //    return View();
        //}


        [HttpPost]
        public JsonResult Create(CRM_Customer_Model model)
        {
         
            int result = 1;
            
            model.ID = Guid.NewGuid();
            model.CreateUserID =model.ModifyUserID= ((CRM_User_Model)Session["Account"]).ID;
            model.CreateTime =model.ModifyTime= DateTime.Now;

            CRM_CustomerConnect_Model model_ = new CRM_CustomerConnect_Model();
            model_.ID = Guid.NewGuid();
            model_.ContactName = model.ContactName;
            model_.CustomerID = model.ID;
            model_.IsDefault = model.IsDefault;
            model_.Tel = model.Tel;
            model_.Phone = model.Phone;
            model_.Email = model.Email;
            model_.Fax = model.Fax;
            model_.QQ = model.QQ;
            model_.CreateUserID = ((CRM_User_Model)Session["Account"]).ID;
            model_.CreateTime = DateTime.Now;
            model_.IsEnable = true;

            if (model.IsDefault == true)
            {
                model.CustomerConnect = model_.ID;
            }
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    if (m_BLL.Create(ref errors, model) && (cc_BLL.Create(ref errors, model_)))
                    {
                        result = 1;
                    }
                    else
                    {
                        throw new Exception(errors.Error);
                    }
                    transaction.Complete();     // 提交事务
                }
                result = 1;
            }
            catch
            {
                result = 0;
            }
            return Json(new RequestResult() { Result = result, Message = result == 1 ? "创建成功" : "创建失败" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //#region 修改
        //public ActionResult Edit(Guid id)
        //{
        //    CRM_Customer_Model entity = m_BLL.GetDetailsById(id);
        //    //BindDropDownList();
        //    return View(entity);
        //}

        //[HttpPost]
        //public JsonResult Edit(CRM_Customer_Model model, string list)
        //{
        //    //判断客户名重复
        //    string _CustomerName = m_BLL.GetById(model.ID).CustomerName;
        //    if (!string.IsNullOrWhiteSpace(model.CustomerName) && model.CustomerName != _CustomerName)
        //    {
        //        int cusNum = m_BLL.GetAll().Where(r => r.CustomerName == model.CustomerName).Count();
        //        if (cusNum > 0)
        //        {
        //            return Json(new RequestResult() { Result = 0, Message = "客户名重复" }, JsonRequestBehavior.AllowGet);
        //        }
        //    }

        //    //判断客户联系人手机号重复
        //    if (m_BLL.GetById(model.ID) != null)
        //    {
        //        Guid CustomerContactID = m_BLL.GetById(model.ID).CustomerContactID;
        //        if (cc_BLL.GetById(CustomerContactID) != null)
        //        {
        //            string _Tel = cc_BLL.GetById(CustomerContactID).Tel;
        //            if (!string.IsNullOrWhiteSpace(model.Tel))
        //            {
        //                int cusNum = cc_BLL.GetAll().Where(r => r.Tel == model.Tel && model.Tel != _Tel).Count();
        //                if (cusNum > 0)
        //                {
        //                    return Json(new RequestResult() { Result = 0, Message = "客户联系人手机号重复" }, JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //        }
        //    }

        //    //判断客户营业执照重复               
        //    string _BusinessLicense = m_BLL.GetById(model.ID).BusinessLicense;
        //    if (!string.IsNullOrWhiteSpace(model.BusinessLicense) && model.BusinessLicense != _BusinessLicense)
        //    {
        //        int cusNum = m_BLL.GetAll().Where(r => r.BusinessLicense == model.BusinessLicense).Count();
        //        if (cusNum > 0)
        //        {
        //            return Json(new RequestResult() { Result = 0, Message = "客户营业执照重复" }, JsonRequestBehavior.AllowGet);
        //        }
        //    }

        //    int result = 1;

        //    model.ModifyUserId = ((AccountModel)Session["Account"]).Id;
        //    model.ModifyTime = DateTime.Now;

        //    try
        //    {

        //        using (TransactionScope transaction = new TransactionScope())
        //        {

        //            CRM_CustomerConnect_Model model_ = cc_BLL.GetById(model.CustomerContactID);
        //            if (model.ContactName == model_.ContactName)
        //            {
        //                model_.IsDefault = 1;
        //                model_.Tel = model.Tel;
        //                model_.Phone = model.Phone;
        //                model_.Email = model.Email;
        //                model_.Fax = model.Fax;
        //                model_.QQ = model.QQ;
        //                model_.Weixin = model.Weixin;
        //                model_.WW = model.WW;
        //                model_.Weibo = model.Weibo;
        //                model_.Des = model.Des_;
        //                model_.BirthDay = model.BirthDay;
        //                model_.IsEnable = 1;
        //                model_.ModifyUserId = this.UserId;
        //                model_.ModifyTime = DateTime.Now;
        //                model_.Address = model.Address;
        //                if (m_BLL.Edit(model) && cc_BLL.Edit(model_))
        //                {
        //                    result = 1;
        //                }
        //                else
        //                {
        //                    throw new Exception(errors.Error);
        //                }
        //            }
        //            else
        //            {
        //                CRM_CustomerContact_Model model__ = new CRM_CustomerContact_Model();
        //                model__.ID = Guid.NewGuid();
        //                model__.ContactName = model.ContactName;
        //                model__.CustomerID = model.ID;
        //                model__.IsDefault = 1;
        //                model__.Tel = model.Tel;
        //                model__.Phone = model.Phone;
        //                model__.Email = model.Email;
        //                model__.Fax = model.Fax;
        //                model__.QQ = model.QQ;
        //                model__.Weixin = model.Weixin;
        //                model__.WW = model.WW;
        //                model__.Weibo = model.Weibo;
        //                model__.Des = model.Des_;
        //                model__.BirthDay = model.BirthDay;
        //                model__.CreateUserId = ((AccountModel)Session["Account"]).Id;
        //                model__.CreateTime = DateTime.Now;
        //                model__.IsEnable = 1;
        //                model__.Address = model.Address;
        //                model.CustomerContactID = model__.ID;

        //                if (m_BLL.Edit(model) && (cc_BLL.Create(ref errors, model__)))
        //                {
        //                    List<CRM_CustomerContact_Model> cclist = cc_BLL.GetAll().Where(t => t.CustomerID == model.ID && t.ID != model.CustomerContactID).ToList();
        //                    foreach (var item in cclist)
        //                    {
        //                        item.IsDefault = 0;
        //                        cc_BLL.Edit(item);
        //                    }
        //                    if (cc_BLL.GetAll().Where(t => t.CustomerID == model.ID && t.ContactName == "-").Count() > 0)
        //                    {
        //                        CRM_CustomerContact_Model cc = cc_BLL.GetAll().SingleOrDefault(t => t.CustomerID == model.ID && t.ContactName == "-");
        //                        cc_BLL.Delete(cc.ID);
        //                    }
        //                    result = 1;
        //                }
        //                else
        //                {
        //                    throw new Exception(errors.Error);
        //                }
        //            }

        //            string listFileId = string.Join(",", list_File.Select(a => a.FileId).ToArray());
        //            listFileId = listFileId.Length == 0 ? new Guid().ToString() : listFileId;
        //            BLL_File.DeleteByParentId("CRM_Customer", model.ID, ResultHelper.StringSqlInFormat(listFileId));

        //            foreach (File_Model item_File in list_File)
        //            {
        //                CRM_File_Model model_File = new CRM_File_Model();

        //                if (!BLL_File.IsExistByNewFileName(item_File.NewFileName))
        //                {
        //                    model_File.FileId = string.IsNullOrWhiteSpace(item_File.FileId) ? Guid.NewGuid() : new Guid(item_File.FileId);
        //                    model_File.RelationTable = "CRM_Customer";
        //                    model_File.RelationId = model.ID;
        //                    model_File.OldFileName = item_File.OldFileName;
        //                    model_File.NewFileName = item_File.NewFileName;
        //                    model_File.FileType = item_File.FileType;
        //                    model_File.FileSize = item_File.FileSize;
        //                    model_File.FilePath = item_File.FilePath;
        //                    model_File.CreateUserId = this.UserId;
        //                    model_File.CreateDate = DateTime.Now;

        //                    if (!BLL_File.Create(ref errors, model_File))
        //                    {
        //                        throw new Exception(errors.Error);
        //                    }
        //                }
        //            }

        //            transaction.Complete();     // 提交事务
        //        }
        //        result = 1;
        //    }
        //    catch
        //    {
        //        result = 0;
        //    }
        //    LogHandler.WriteServiceLog(this.UserId, "Id:" + model.ID + ",Title:" + model.CreateUserId, result == 1 ? "成功" : "失败", "编辑", "客户");
        //    return Json(new RequestResult() { Result = result, Message = result == 1 ? "编辑成功" : "编辑失败" }, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        #region 详细
        //[SupportFilter]
        public ActionResult Details(Guid id)
        {
            CRM_Customer_Model entity = m_BLL.GetDetailsById(id);
            return View(entity);
        }

        public ActionResult Details2(Guid id)
        {
            CRM_Customer_Model entity = m_BLL.GetDetailsById(id);
            return View(entity);
        }
        #endregion

        #region 删除
        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            if (!string.IsNullOrWhiteSpace(id.ToString()))
            {
                if (m_BLL.Delete(id))
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //#region 转移
        //[HttpPost]
        //public JsonResult Move(string CustomerId, string UserId)
        //{

        //    string[] CustomerIds = CustomerId.Split(',');
        //    for (int i = 0; i < CustomerIds.Length; i++)
        //    {
        //        Guid customerID = new Guid(CustomerIds[i]);
        //        CRM_Customer_Model customer = m_BLL.GetById(customerID);
        //        col_BLL.Create(ref  errors, new CRM_CustomerOperateLog_Model()
        //        {
        //            ID = Guid.NewGuid(),
        //            OperateType = "转移",
        //            CustomerID = customerID,
        //            OldMarketer = customer.UserId + (!string.IsNullOrEmpty(customer.ShareUserID) ? "," : "") + customer.ShareUserID,
        //            NewMarketer = UserId + (!string.IsNullOrEmpty(customer.ShareUserID) ? "," : "") + customer.ShareUserID,
        //            OperateUserID = this.GetCurrentUserId(),
        //            OperateDate = DateTime.Now
        //        });
        //    }

        //    if (m_BLL.Move(CustomerId, UserId))
        //    {
        //        return Json(1, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(0, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //#endregion

        //#region 共享
        //[HttpPost]
        //public JsonResult Share(string CustomerId, string UserId)
        //{
        //    string[] CustomerIds = CustomerId.Split(',');
        //    for (int i = 0; i < CustomerIds.Length; i++)
        //    {
        //        Guid customerID = new Guid(CustomerIds[i]);
        //        CRM_Customer_Model customer = m_BLL.GetById(customerID);
        //        col_BLL.Create(ref  errors, new CRM_CustomerOperateLog_Model()
        //        {
        //            ID = Guid.NewGuid(),
        //            OperateType = "共享",
        //            CustomerID = customerID,
        //            OldMarketer = customer.UserId + (!string.IsNullOrEmpty(customer.ShareUserID) ? "," : "") + customer.ShareUserID,
        //            NewMarketer = customer.UserId + "," + UserId,
        //            OperateUserID = this.GetCurrentUserId(),
        //            OperateDate = DateTime.Now
        //        });
        //    }

        //    if (m_BLL.Share(CustomerId, UserId))
        //    {
        //        return Json(1, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(0, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //#endregion

        //#region 取消共享
        //[HttpPost]
        //public JsonResult CancelShare(string CustomerId)
        //{
        //    string[] CustomerIds = CustomerId.Split(',');
        //    for (int i = 0; i < CustomerIds.Length; i++)
        //    {
        //        Guid customerID = new Guid(CustomerIds[i]);
        //        CRM_Customer_Model customer = m_BLL.GetById(customerID);
        //        col_BLL.Create(ref  errors, new CRM_CustomerOperateLog_Model()
        //        {
        //            ID = Guid.NewGuid(),
        //            OperateType = "取消共享",
        //            CustomerID = customerID,
        //            OldMarketer = customer.UserId + (!string.IsNullOrEmpty(customer.ShareUserID) ? "," : "") + customer.ShareUserID,
        //            NewMarketer = customer.UserId,
        //            OperateUserID = this.GetCurrentUserId(),
        //            OperateDate = DateTime.Now
        //        });
        //    }

        //    if (m_BLL.CancelShare(CustomerId))
        //    {
        //        return Json(1, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(0, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //#endregion

        //#region 回归客户池
        //[HttpPost]
        //public JsonResult ReturnSea(Guid CustomerId)
        //{
        //    CRM_Customer_Model model = m_BLL.GetById(CustomerId);
        //    model.IsPublic = 1;
        //    model.IsAssign = 0;
        //    model.UserId = null;
        //    model.ShareUserID = null;
        //    if (m_BLL.Edit(model))
        //    {
        //        return Json(1, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(0, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //#endregion

        //#region 绑定列表
        //private void BindDropDownList()
        //{
        //    ViewData["IsCompany"] = (from a in t_BLL.GetEnableAll().Where(t => t.TypeId == (int)TypeEnum.公司个人).OrderBy(t => t.Sort)
        //                             select new SelectListItem
        //                             {
        //                                 Text = a.TypeName,
        //                                 Value = a.Id.ToString()
        //                             });
        //    ViewData["CustomerLevel"] = (from a in t_BLL.GetEnableAll().Where(t => t.TypeId == (int)TypeEnum.客户级别).OrderBy(t => t.Sort)
        //                                 select new SelectListItem
        //                                 {
        //                                     Text = a.TypeName,
        //                                     Value = a.Id.ToString()
        //                                 });
        //    ViewData["CustomerType"] = (from a in t_BLL.GetEnableAll().Where(t => t.TypeId == (int)TypeEnum.客户类型).OrderBy(t => t.Sort)
        //                                select new SelectListItem
        //                                {
        //                                    Text = a.TypeName,
        //                                    Value = a.Id.ToString()
        //                                });
        //    ViewData["CustomerBusiness"] = (from a in t_BLL.GetEnableAll().Where(t => t.TypeId == (int)TypeEnum.所属行业).OrderBy(t => t.Sort)
        //                                    select new SelectListItem
        //                                    {
        //                                        Text = a.TypeName,
        //                                        Value = a.Id.ToString()
        //                                    });
        //    ViewData["CustomerState"] = (from a in t_BLL.GetEnableAll().Where(t => t.TypeId == (int)TypeEnum.客户状态).OrderBy(t => t.Sort)
        //                                 select new SelectListItem
        //                                 {
        //                                     Text = a.TypeName,
        //                                     Value = a.Id.ToString()
        //                                 });
        //    ViewData["CustomerSource"] = (from a in t_BLL.GetEnableAll().Where(t => t.TypeId == (int)TypeEnum.客户来源).OrderBy(t => t.Sort)
        //                                  select new SelectListItem
        //                                  {
        //                                      Text = a.TypeName,
        //                                      Value = a.Id.ToString()
        //                                  });
        //    ViewData["CompanyNature"] = (from a in t_BLL.GetEnableAll().Where(t => t.TypeId == (int)TypeEnum.公司性质).OrderBy(t => t.Sort)
        //                                 select new SelectListItem
        //                                 {
        //                                     Text = a.TypeName,
        //                                     Value = a.Id.ToString()
        //                                 });
        //    ViewData["EmployeeNum"] = (from a in t_BLL.GetEnableAll().Where(t => t.TypeId == (int)TypeEnum.员工人数).OrderBy(t => t.Sort)
        //                               select new SelectListItem
        //                               {
        //                                   Text = a.TypeName,
        //                                   Value = a.Id.ToString()
        //                               });
        //    ViewData["CustomerProperty"] = (from a in t_BLL.GetEnableAll().Where(t => t.TypeId == (int)TypeEnum.客户属性).OrderBy(t => t.Sort)
        //                                    select new SelectListItem
        //                                    {
        //                                        Text = a.TypeName,
        //                                        Value = a.Id.ToString()
        //                                    });
        //    ViewData["Province"] = (from a in p_BLL.GetList()
        //                            select new SelectListItem
        //                            {
        //                                Text = a.ProvinceName,
        //                                Value = a.ProvinceID.ToString()
        //                            });
        //    ViewData["Tag"] = (from a in g_BLL.GetAll()
        //                       select new SelectListItem
        //                       {
        //                           Text = a.TagName,
        //                           Value = a.ID.ToString()
        //                       });
        //}
        //#endregion

        //#region 导入、导出
        ///// <summary>
        ///// 导出（条件）
        ///// </summary>
        ///// <param name="customerName">客户名称</param>
        ///// <param name="contactName">联系人名称</param>
        ///// <param name="userId">业务员</param>
        ///// <param name="isCompany">公司/个人</param>
        ///// <param name="customerSource">客户来源</param>
        ///// <param name="customerBusiness">所属行业</param>
        ///// <param name="companyNature">公司性质</param>
        ///// <param name="beginDate1">创建时间起始</param>
        ///// <param name="endDate1">创建时间截止</param>
        ///// <param name="beginDate2">最后跟进时间起始</param>
        ///// <param name="endDate2">最后跟进时间截止</param>
        ///// <param name="followCount">跟进数</param>
        ///// <param name="province">省</param>
        ///// <param name="city">市</param>
        ///// <param name="area">区</param>
        ///// <param name="cusLevel">客户级别</param>
        ///// <param name="tag">标签</param>
        ///// <param name="linkType">链接类型</param>
        ///// <param name="linkValue">链接值</param>
        ///// <returns></returns>
        ////[HttpPost]
        ////public FileResult Export(string customerName, string contactName, string userId, string isCompany, string customerSource, string customerBusiness, string companyNature,
        ////       string beginDate1, string endDate1, string beginDate2, string endDate2, string followCount, string province, string city, string area, string cusLevel, string tag, string linkType, string linkValue, string browser)
        ////{
        ////    int _isCompany = String.IsNullOrEmpty(isCompany) ? 0 : int.Parse(isCompany);
        ////    int _customerSource = String.IsNullOrEmpty(customerSource) ? 0 : int.Parse(customerSource);
        ////    int _customerBusiness = String.IsNullOrEmpty(customerBusiness) ? 0 : int.Parse(customerBusiness);
        ////    int _companyNature = String.IsNullOrEmpty(companyNature) ? 0 : int.Parse(companyNature);
        ////    DateTime _beginDate1 = String.IsNullOrEmpty(beginDate1) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(beginDate1);
        ////    DateTime _endDate1 = String.IsNullOrEmpty(endDate1) ? DateTime.Now : Convert.ToDateTime(endDate1);
        ////    DateTime _beginDate2 = String.IsNullOrEmpty(beginDate2) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(beginDate2);
        ////    DateTime _endDate2 = String.IsNullOrEmpty(endDate2) ? DateTime.Now : Convert.ToDateTime(endDate2);
        ////    int _followCount = String.IsNullOrEmpty(followCount) ? 0 : int.Parse(followCount);
        ////    int _province = String.IsNullOrEmpty(province) ? 0 : int.Parse(province);
        ////    int _city = String.IsNullOrEmpty(city) ? 0 : int.Parse(city);
        ////    int _area = String.IsNullOrEmpty(area) ? 0 : int.Parse(area);

        ////    //创建Excel文件的对象
        ////    NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
        ////    //添加一个sheet
        ////    NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
        ////    //获取list数据
        ////    List<string> users = (List<string>)Session["GetUsers"];
        ////    List<CRM_Customer_Model> list = m_BLL.Export(customerName, contactName, userId, _isCompany, _customerSource, _customerBusiness, _companyNature, _beginDate1, _endDate1, _beginDate2, _endDate2, _followCount, _province, _city, _area, cusLevel, tag, linkType, linkValue, users);

        ////    //给sheet1添加第一行的头部标题
        ////    NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
        ////    row1.CreateCell(0).SetCellValue("客户名称");
        ////    row1.CreateCell(1).SetCellValue("客户地址");
        ////    row1.CreateCell(2).SetCellValue("联系人");
        ////    row1.CreateCell(3).SetCellValue("手机号码");
        ////    row1.CreateCell(4).SetCellValue("办公电话");
        ////    row1.CreateCell(5).SetCellValue("电子邮件");
        ////    row1.CreateCell(6).SetCellValue("QQ");
        ////    row1.CreateCell(7).SetCellValue("旺旺");
        ////    row1.CreateCell(8).SetCellValue("微信号");
        ////    row1.CreateCell(9).SetCellValue("联系地址");
        ////    row1.CreateCell(10).SetCellValue("客户级别");
        ////    row1.CreateCell(11).SetCellValue("客户类别");
        ////    row1.CreateCell(12).SetCellValue("客户来源");
        ////    row1.CreateCell(13).SetCellValue("销售员");
        ////    row1.CreateCell(14).SetCellValue("创建时间");
        ////    //将数据逐步写入sheet1各个行
        ////    for (int i = 0; i < list.Count; i++)
        ////    {
        ////        NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
        ////        rowtemp.CreateCell(0).SetCellValue(list[i].CustomerName.ToString());
        ////        if (list[i].CityArea != null)
        ////        {
        ////            string CityArea = "";
        ////            CityArea = list[i].CityArea.ToString().Trim().Replace("<br/>", "-");
        ////            if (list[i].Address != null)
        ////                CityArea += list[i].Address.Trim();
        ////            rowtemp.CreateCell(1).SetCellValue(CityArea);
        ////        }
        ////        if (list[i].ContactName != null)
        ////        {
        ////            rowtemp.CreateCell(2).SetCellValue(list[i].ContactName.ToString());
        ////        }
        ////        if (list[i].Tel != null)
        ////        {
        ////            rowtemp.CreateCell(3).SetCellValue(list[i].Tel.ToString());
        ////        }
        ////        if (list[i].Phone != null)
        ////        {
        ////            rowtemp.CreateCell(4).SetCellValue(list[i].Phone.ToString());
        ////        }
        ////        if (list[i].Email != null)
        ////        {
        ////            rowtemp.CreateCell(5).SetCellValue(list[i].Email.ToString());
        ////        }
        ////        if (list[i].QQ != null)
        ////        {
        ////            rowtemp.CreateCell(6).SetCellValue(list[i].QQ.ToString());
        ////        }
        ////        if (list[i].WW != null)
        ////        {
        ////            rowtemp.CreateCell(7).SetCellValue(list[i].WW.ToString());
        ////        }
        ////        if (list[i].Weixin != null)
        ////        {
        ////            rowtemp.CreateCell(8).SetCellValue(list[i].Weixin.ToString());
        ////        }
        ////        if (list[i].Address != null)
        ////        {
        ////            string CityStr = list[i].CityArea == null ? "" : list[i].CityArea.ToString();
        ////            rowtemp.CreateCell(9).SetCellValue(CityStr + list[i].Address.ToString());
        ////        }
        ////        if (list[i].CustomerLevel_ != null)
        ////        {
        ////            rowtemp.CreateCell(10).SetCellValue(list[i].CustomerLevel_.ToString());
        ////        }
        ////        if (list[i].CustomerType_ != null)
        ////        {
        ////            rowtemp.CreateCell(11).SetCellValue(list[i].CustomerType_.ToString());
        ////        }
        ////        if (list[i].CustomerSource_ != null)
        ////        {
        ////            rowtemp.CreateCell(12).SetCellValue(list[i].CustomerSource_.ToString());
        ////        }
        ////        if (list[i].UserName != null)
        ////        {
        ////            rowtemp.CreateCell(13).SetCellValue(list[i].UserName.ToString());
        ////        }
        ////        if (list[i].CreateTime != null)
        ////        {
        ////            rowtemp.CreateCell(14).SetCellValue(list[i].CreateTime.ToString());
        ////        }
        ////    }
        ////    // 写入到客户端 
        ////    System.IO.MemoryStream ms = new System.IO.MemoryStream();
        ////    book.Write(ms);
        ////    ms.Seek(0, SeekOrigin.Begin);
        ////    if (browser == "Firefox")
        ////    {
        ////        return File(ms, "application/vnd.ms-excel", "客户信息.xls");
        ////    }
        ////    else
        ////    {
        ////        return File(ms, "application/vnd.ms-excel", HttpUtility.UrlEncode("客户信息.xls", System.Text.Encoding.GetEncoding("UTF-8")));
        ////    }
        ////}



        //public FileResult Export(string customerName, string contactName, string userId, string isCompany, string customerSource,
        //    string customerBusiness, string companyNature, string beginDate1, string endDate1, string beginDate2, string endDate2, string followCount,
        //    string province, string city, string area, string cusLevel, string tag, int FilterType, string FilterValue, string UserPhone,
        //    string CustomerProperty, string CustomerState, string CreateDay, string NoFellowDay, string browser)
        //{

        //    CRM_Customer_ModelFilter MFilter = new CRM_Customer_ModelFilter();
        //    MFilter.CustomerName = customerName;
        //    MFilter.ContactName = contactName;
        //    MFilter.UserId = userId;
        //    MFilter.IsCompany = String.IsNullOrEmpty(isCompany) ? 0 : int.Parse(isCompany);
        //    MFilter.CustomerSource = String.IsNullOrEmpty(customerSource) ? 0 : int.Parse(customerSource);
        //    MFilter.CustomerBusiness = String.IsNullOrEmpty(customerBusiness) ? 0 : int.Parse(customerBusiness);
        //    MFilter.CompanyNature = String.IsNullOrEmpty(companyNature) ? 0 : int.Parse(companyNature);
        //    MFilter.CreateBeginDate = String.IsNullOrEmpty(beginDate1) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(beginDate1);
        //    MFilter.CreateEndDate = String.IsNullOrEmpty(endDate1) ? DateTime.Now : Convert.ToDateTime(endDate1);
        //    MFilter.NoFellowBeginDate = String.IsNullOrEmpty(beginDate2) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(beginDate2);
        //    MFilter.NoFellowEndDate = String.IsNullOrEmpty(endDate2) ? DateTime.Now : Convert.ToDateTime(endDate2);
        //    MFilter.FollowCount = String.IsNullOrEmpty(followCount) ? 0 : int.Parse(followCount);
        //    MFilter.CustProvinceID = String.IsNullOrEmpty(province) ? "" : province;
        //    MFilter.CityID = String.IsNullOrEmpty(city) ? 0 : int.Parse(city);
        //    MFilter.AreaID = String.IsNullOrEmpty(area) ? 0 : int.Parse(area);

        //    MFilter.CustLevel = String.IsNullOrEmpty(cusLevel) ? "" : cusLevel;
        //    MFilter.AttendTag = String.IsNullOrEmpty(tag) ? "" : tag;
        //    MFilter.Phone = UserPhone;


        //    MFilter.CustomerState = String.IsNullOrEmpty(CustomerState) ? 0 : int.Parse(CustomerState);
        //    MFilter.CustomerProperty = String.IsNullOrEmpty(CustomerProperty) ? 0 : int.Parse(CustomerProperty);
        //    MFilter.CreateDay = String.IsNullOrEmpty(CreateDay) ? 0 : int.Parse(CreateDay);
        //    MFilter.NoFellowDay = String.IsNullOrEmpty(NoFellowDay) ? 0 : int.Parse(NoFellowDay);
        //    switch (FilterType)
        //    {
        //        case (int)CustomerMenuType.创建时间:
        //            MFilter.CreateDay = Convert.ToInt32(FilterValue);
        //            MFilter.CreateBeginDate = Convert.ToDateTime("1900-01-01");
        //            MFilter.CreateEndDate = DateTime.Now;
        //            break;
        //        case (int)CustomerMenuType.跟进时间:
        //            MFilter.NoFellowDay = Convert.ToInt32(FilterValue);
        //            MFilter.NoFellowBeginDate = Convert.ToDateTime("1900-01-01");
        //            MFilter.NoFellowEndDate = DateTime.Now;
        //            break;
        //        case (int)CustomerMenuType.公司个人:
        //            MFilter.IsCompany = Convert.ToInt32(FilterValue);
        //            break;
        //        case (int)CustomerMenuType.公司性质:
        //            MFilter.CompanyNature = Convert.ToInt32(FilterValue);
        //            break;
        //        case (int)CustomerMenuType.客户级别:
        //            MFilter.CustLevel = FilterValue;
        //            break;
        //        case (int)CustomerMenuType.客户来源:
        //            MFilter.CustomerSource = Convert.ToInt32(FilterValue);
        //            break;
        //        case (int)CustomerMenuType.客户类型:
        //            MFilter.CustomerType = Convert.ToInt32(FilterValue);
        //            break;
        //        case (int)CustomerMenuType.客户属性:
        //            MFilter.CustomerProperty = Convert.ToInt32(FilterValue);
        //            break;
        //        case (int)CustomerMenuType.客户状态:
        //            MFilter.CustomerState = Convert.ToInt32(FilterValue);
        //            break;
        //        case (int)CustomerMenuType.所属行业:
        //            MFilter.CustomerBusiness = Convert.ToInt32(FilterValue);
        //            break;
        //        case (int)CustomerMenuType.客户地区:
        //            MFilter.CustProvinceID = FilterValue;
        //            MFilter.CityID = 0;
        //            MFilter.AreaID = 0;
        //            break;
        //        case (int)CustomerMenuType.共享给我: MFilter.ShareToMeUserID = GetCurrentUserId(); break;
        //        case (int)CustomerMenuType.我共享的: MFilter.IToShareToMeUserID = GetCurrentUserId(); break;
        //    }
        //    List<string> users = (List<string>)Session["GetUsers"];
        //    List<CRM_Customer_Model> list = m_BLL.GetListByModel(MFilter, users);

        //    //创建Excel文件的对象
        //    NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
        //    //添加一个sheet
        //    NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
        //    //获取list数据
        //    //List<string> users = (List<string>)Session["GetUsers"];
        //    //List<CRM_Customer_Model> list = m_BLL.Export(customerName, contactName, userId, _isCompany, _customerSource, _customerBusiness, _companyNature, _beginDate1, _endDate1, _beginDate2, _endDate2, _followCount, _province, _city, _area, cusLevel, tag, linkType, linkValue, users);

        //    //给sheet1添加第一行的头部标题
        //    NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
        //    row1.CreateCell(0).SetCellValue("客户名称");
        //    row1.CreateCell(1).SetCellValue("客户地址");
        //    row1.CreateCell(2).SetCellValue("联系人");
        //    row1.CreateCell(3).SetCellValue("手机号码");
        //    row1.CreateCell(4).SetCellValue("办公电话");
        //    row1.CreateCell(5).SetCellValue("电子邮件");
        //    row1.CreateCell(6).SetCellValue("QQ");
        //    row1.CreateCell(7).SetCellValue("旺旺");
        //    row1.CreateCell(8).SetCellValue("微信号");
        //    row1.CreateCell(9).SetCellValue("联系地址");
        //    row1.CreateCell(10).SetCellValue("客户级别");
        //    row1.CreateCell(11).SetCellValue("客户类别");
        //    row1.CreateCell(12).SetCellValue("客户来源");
        //    row1.CreateCell(13).SetCellValue("销售员");
        //    row1.CreateCell(14).SetCellValue("创建时间");
        //    //将数据逐步写入sheet1各个行
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
        //        rowtemp.CreateCell(0).SetCellValue(list[i].CustomerName.ToString());
        //        if (list[i].CityArea != null)
        //        {
        //            string CityArea = "";
        //            CityArea = list[i].CityArea.ToString().Trim().Replace("<br/>", "-");
        //            if (list[i].Address != null)
        //                CityArea += list[i].Address.Trim();
        //            rowtemp.CreateCell(1).SetCellValue(CityArea);
        //        }
        //        if (list[i].ContactName != null)
        //        {
        //            rowtemp.CreateCell(2).SetCellValue(list[i].ContactName.ToString());
        //        }
        //        if (list[i].Tel != null)
        //        {
        //            rowtemp.CreateCell(3).SetCellValue(list[i].Tel.ToString());
        //        }
        //        if (list[i].Phone != null)
        //        {
        //            rowtemp.CreateCell(4).SetCellValue(list[i].Phone.ToString());
        //        }
        //        if (list[i].Email != null)
        //        {
        //            rowtemp.CreateCell(5).SetCellValue(list[i].Email.ToString());
        //        }
        //        if (list[i].QQ != null)
        //        {
        //            rowtemp.CreateCell(6).SetCellValue(list[i].QQ.ToString());
        //        }
        //        if (list[i].WW != null)
        //        {
        //            rowtemp.CreateCell(7).SetCellValue(list[i].WW.ToString());
        //        }
        //        if (list[i].Weixin != null)
        //        {
        //            rowtemp.CreateCell(8).SetCellValue(list[i].Weixin.ToString());
        //        }
        //        if (list[i].Address != null)
        //        {
        //            string CityStr = list[i].CityArea == null ? "" : list[i].CityArea.ToString();
        //            rowtemp.CreateCell(9).SetCellValue(CityStr + list[i].Address.ToString());
        //        }
        //        if (list[i].CustomerLevel_ != null)
        //        {
        //            rowtemp.CreateCell(10).SetCellValue(list[i].CustomerLevel_.ToString());
        //        }
        //        if (list[i].CustomerType_ != null)
        //        {
        //            rowtemp.CreateCell(11).SetCellValue(list[i].CustomerType_.ToString());
        //        }
        //        if (list[i].CustomerSource_ != null)
        //        {
        //            rowtemp.CreateCell(12).SetCellValue(list[i].CustomerSource_.ToString());
        //        }
        //        if (list[i].UserName != null)
        //        {
        //            rowtemp.CreateCell(13).SetCellValue(list[i].UserName.ToString());
        //        }
        //        if (list[i].CreateTime != null)
        //        {
        //            rowtemp.CreateCell(14).SetCellValue(list[i].CreateTime.ToString());
        //        }
        //    }
        //    // 写入到客户端 
        //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //    book.Write(ms);
        //    ms.Seek(0, SeekOrigin.Begin);
        //    if (browser == "Firefox")
        //    {
        //        return File(ms, "application/vnd.ms-excel", "客户信息.xls");
        //    }
        //    else
        //    {
        //        return File(ms, "application/vnd.ms-excel", HttpUtility.UrlEncode("客户信息.xls", System.Text.Encoding.GetEncoding("UTF-8")));
        //    }
        //}

        ///// <summary>
        ///// 导出（区域客户）
        ///// </summary>
        ///// <param name="customerName">客户名称</param>
        ///// <param name="contactName">联系人名称</param>
        ///// <param name="userId">业务员</param>
        ///// <param name="isCompany">公司/个人</param>
        ///// <param name="customerSource">客户来源</param>
        ///// <param name="customerBusiness">所属行业</param>
        ///// <param name="companyNature">公司性质</param>
        ///// <param name="beginDate1">创建时间起始</param>
        ///// <param name="endDate1">创建时间截止</param>
        ///// <param name="beginDate2">最后跟进时间起始</param>
        ///// <param name="endDate2">最后跟进时间截止</param>
        ///// <param name="followCount">跟进数</param>
        ///// <param name="cusArea">所选子区域ID</param>
        ///// <param name="areaID">区域编号（省编号/市编号）</param>
        ///// <param name="areaType">区域类型（省/市）</param>
        ///// <param name="cusLevel">客户级别</param>
        ///// <param name="tag">标签</param>
        ///// <param name="linkType">链接类型</param>
        ///// <param name="linkValue">链接值</param>
        ///// <returns></returns>
        //public FileResult ExportA(string customerName, string contactName, string userId, string isCompany, string customerSource, string customerBusiness, string companyNature,
        //      string beginDate1, string endDate1, string beginDate2, string endDate2, string followCount, string cusArea, int areaID, string areaType, string cusLevel, string tag, string linkType, string linkValue)
        //{
        //    int _isCompany = isCompany == "" ? 0 : int.Parse(isCompany);
        //    int _customerSource = customerSource == "" ? 0 : int.Parse(customerSource);
        //    int _customerBusiness = customerBusiness == "" ? 0 : int.Parse(customerBusiness);
        //    int _companyNature = companyNature == "" ? 0 : int.Parse(companyNature);
        //    DateTime _beginDate1 = beginDate1 == "" ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(beginDate1);
        //    DateTime _endDate1 = endDate1 == "" ? DateTime.Now : Convert.ToDateTime(endDate1);
        //    DateTime _beginDate2 = beginDate2 == "" ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(beginDate2);
        //    DateTime _endDate2 = endDate2 == "" ? DateTime.Now : Convert.ToDateTime(endDate2);
        //    int _followCount = followCount == "" ? 0 : int.Parse(followCount);

        //    //创建Excel文件的对象
        //    NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
        //    //添加一个sheet
        //    NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
        //    //获取list数据
        //    List<string> users = (List<string>)Session["GetUsers"];
        //    List<CRM_Customer_Model> list = m_BLL.ExportA(customerName, contactName, userId, _isCompany, _customerSource, _customerBusiness, _companyNature, _beginDate1, _endDate1, _beginDate2, _endDate2, _followCount, cusArea, areaID, areaType, cusLevel, tag, linkType, linkValue, users);

        //    //给sheet1添加第一行的头部标题
        //    NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
        //    row1.CreateCell(0).SetCellValue("客户名称");
        //    row1.CreateCell(1).SetCellValue("客户地址");
        //    row1.CreateCell(2).SetCellValue("联系人");
        //    row1.CreateCell(3).SetCellValue("手机号码");
        //    row1.CreateCell(4).SetCellValue("办公电话");
        //    row1.CreateCell(5).SetCellValue("电子邮件");
        //    row1.CreateCell(6).SetCellValue("QQ");
        //    row1.CreateCell(7).SetCellValue("旺旺");
        //    row1.CreateCell(8).SetCellValue("微信号");
        //    row1.CreateCell(9).SetCellValue("联系地址");
        //    row1.CreateCell(10).SetCellValue("客户级别");
        //    row1.CreateCell(11).SetCellValue("客户类别");
        //    row1.CreateCell(12).SetCellValue("客户来源");
        //    row1.CreateCell(13).SetCellValue("销售员");
        //    row1.CreateCell(14).SetCellValue("创建时间");
        //    //将数据逐步写入sheet1各个行
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
        //        rowtemp.CreateCell(0).SetCellValue(list[i].CustomerName.ToString());
        //        if (list[i].CityArea != null)
        //        {
        //            string CityArea = "";
        //            CityArea = list[i].CityArea.ToString().Trim().Replace("<br/>", "-");
        //            if (list[i].Address != null)
        //                CityArea += list[i].Address.Trim();
        //            rowtemp.CreateCell(1).SetCellValue(CityArea);
        //        }
        //        if (list[i].ContactName != null)
        //        {
        //            rowtemp.CreateCell(2).SetCellValue(list[i].ContactName.ToString());
        //        }
        //        if (list[i].Tel != null)
        //        {
        //            rowtemp.CreateCell(3).SetCellValue(list[i].Tel.ToString());
        //        }
        //        if (list[i].Phone != null)
        //        {
        //            rowtemp.CreateCell(4).SetCellValue(list[i].Phone.ToString());
        //        }
        //        if (list[i].Email != null)
        //        {
        //            rowtemp.CreateCell(5).SetCellValue(list[i].Email.ToString());
        //        }
        //        if (list[i].QQ != null)
        //        {
        //            rowtemp.CreateCell(6).SetCellValue(list[i].QQ.ToString());
        //        }
        //        if (list[i].WW != null)
        //        {
        //            rowtemp.CreateCell(7).SetCellValue(list[i].WW.ToString());
        //        }
        //        if (list[i].Weixin != null)
        //        {
        //            rowtemp.CreateCell(8).SetCellValue(list[i].Weixin.ToString());
        //        }
        //        if (list[i].Address != null)
        //        {
        //            rowtemp.CreateCell(9).SetCellValue(list[i].Address.ToString());
        //        }
        //        if (list[i].CustomerLevel_ != null)
        //        {
        //            rowtemp.CreateCell(10).SetCellValue(list[i].CustomerLevel_.ToString());
        //        }
        //        if (list[i].CustomerType_ != null)
        //        {
        //            rowtemp.CreateCell(11).SetCellValue(list[i].CustomerType_.ToString());
        //        }
        //        if (list[i].CustomerSource_ != null)
        //        {
        //            rowtemp.CreateCell(12).SetCellValue(list[i].CustomerSource_.ToString());
        //        }
        //        if (list[i].UserName != null)
        //        {
        //            rowtemp.CreateCell(13).SetCellValue(list[i].UserName.ToString());
        //        }
        //        if (list[i].CreateTime != null)
        //        {
        //            rowtemp.CreateCell(14).SetCellValue(list[i].CreateTime.ToString());
        //        }
        //    }
        //    // 写入到客户端 
        //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //    book.Write(ms);
        //    ms.Seek(0, SeekOrigin.Begin);
        //    return File(ms, "application/vnd.ms-excel", HttpUtility.UrlEncode("区域客户信息.xls", System.Text.Encoding.GetEncoding("UTF-8")));
        //}

        ///// <summary>
        ///// 导入
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult Import()
        //{
        //    return View();
        //}

        ///// <summary>
        ///// 导入
        ///// </summary>
        ///// <returns></returns>
        ////[AcceptVerbs(HttpVerbs.Post)]
        ////[HttpPost]
        ////public ActionResult Import(HttpPostedFileBase file)
        ////{
        ////    //HttpPostedFileBase file = Request.Files[0];     
        ////    string filePath = Server.MapPath("~/UploadFiles/");
        ////    string fileName = Path.GetFileName(file.FileName);
        ////    string fileExtension = Path.GetExtension(fileName);
        ////    if (fileExtension == ".xls" || fileExtension == ".xlsx")
        ////    {
        ////        string saveName = Guid.NewGuid().ToString() + fileExtension;
        ////        file.SaveAs(filePath + saveName);

        ////        IWorkbook workbook;
        ////        //初始化
        ////        try
        ////        {
        ////            //using (FileStream _file = new FileStream(filePath + saveName, FileMode.Open, FileAccess.Read))
        ////            //{
        ////            //    workbook = new HSSFWorkbook(_file);
        ////            //}
        ////            workbook = Common.NPOIHelp.LoadWookBook(filePath + saveName);
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            throw e;
        ////        }

        ////        try
        ////        {
        ////            List<CRM_Customer_Model> list = new List<CRM_Customer_Model>();
        ////            NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(0);
        ////            IRow headerRow = sheet.GetRow(0);
        ////            int cellCount = headerRow.LastCellNum;
        ////            int rowCount = sheet.LastRowNum; //总行数
        ////            int rowCount_ = 0;//非空行数 
        ////            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
        ////            {
        ////                int r = i + 1;
        ////                IRow row = sheet.GetRow(i);
        ////                for (int j = row.FirstCellNum; j < cellCount; j++)
        ////                {
        ////                    int c = j + 1;
        ////                    if (headerRow.GetCell(j).StringCellValue == "客户名称")
        ////                    {
        ////                        if (GetCellValue(row.GetCell(j)) != "" && GetCellValue(row.GetCell(j)) != null)
        ////                        {
        ////                            rowCount_++;
        ////                        }
        ////                    }
        ////                }
        ////            }
        ////            string error = "";
        ////            if (rowCount_ == 0)
        ////            {
        ////                error = "导入数据为空!";
        ////            }
        ////            else
        ////            {
        ////                for (int i = (sheet.FirstRowNum + 1); i <= rowCount_; i++)
        ////                {
        ////                    int r = i + 1;
        ////                    IRow row = sheet.GetRow(i);
        ////                    if (row != null)
        ////                    {
        ////                        CRM_Customer_Model model = new CRM_Customer_Model();
        ////                        for (int j = row.FirstCellNum; j < cellCount; j++)
        ////                        {
        ////                            int c = j + 1;
        ////                            if (headerRow.GetCell(j).StringCellValue == "客户名称")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "" && GetCellValue(row.GetCell(j)) != null)
        ////                                {
        ////                                    if (!m_BLL.IsExists(GetCellValue(row.GetCell(j))))
        ////                                    {
        ////                                        if (System.Text.RegularExpressions.Regex.IsMatch(GetCellValue(row.GetCell(j)), @"^[a-zA-Z0-9\u4e00-\u9fa5]+$"))
        ////                                        {
        ////                                            string val = GetCellValue(row.GetCell(j));
        ////                                            if (list.Where(t => t.CustomerName == val).Count() > 0)
        ////                                            {
        ////                                                error += "第" + r + "行" + "第" + c + "列客户名称重复!\n";
        ////                                            }
        ////                                            else
        ////                                            {
        ////                                                model.CustomerName = GetCellValue(row.GetCell(j));
        ////                                            }
        ////                                        }
        ////                                        else
        ////                                        {
        ////                                            error += "第" + r + "行" + "第" + c + "列客户名称格式不正确!\n";
        ////                                        }
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列客户名称已存在!\n";
        ////                                    }
        ////                                }
        ////                                else
        ////                                {
        ////                                    error += "第" + r + "行" + "第" + c + "列客户名称为空!\n";
        ////                                }
        ////                            }
        ////                            if (headerRow.GetCell(j).StringCellValue == "联系地址")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "")
        ////                                {
        ////                                    if (System.Text.RegularExpressions.Regex.IsMatch(GetCellValue(row.GetCell(j)), @"^[a-zA-Z0-9-\u4e00-\u9fa5]+$"))
        ////                                    {
        ////                                        model.Address = GetCellValue(row.GetCell(j));
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列联系地址格式不正确!\n";
        ////                                    }
        ////                                }
        ////                            }
        ////                            if (headerRow.GetCell(j).StringCellValue == "邮政编码")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "")
        ////                                {
        ////                                    if (System.Text.RegularExpressions.Regex.IsMatch(GetCellValue(row.GetCell(j)), @"^\d{6}$"))
        ////                                    {
        ////                                        model.ZipCode = GetCellValue(row.GetCell(j));
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列邮政编码格式不正确!\n";
        ////                                    }
        ////                                }
        ////                            }
        ////                            if (headerRow.GetCell(j).StringCellValue == "公司网址")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "")
        ////                                {
        ////                                    if (System.Text.RegularExpressions.Regex.IsMatch(GetCellValue(row.GetCell(j)), @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"))
        ////                                    {
        ////                                        model.CustomerWeb = GetCellValue(row.GetCell(j));
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列公司网址格式不正确!\n";
        ////                                    }
        ////                                }
        ////                            }
        ////                            if (headerRow.GetCell(j).StringCellValue == "联系人名称")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "")
        ////                                {
        ////                                    if (System.Text.RegularExpressions.Regex.IsMatch(GetCellValue(row.GetCell(j)), @"^[a-zA-Z0-9\u4e00-\u9fa5]+$"))
        ////                                    {
        ////                                        model.ContactName = GetCellValue(row.GetCell(j));
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列联系人格式不正确!\n";
        ////                                    }
        ////                                }
        ////                            }
        ////                            if (headerRow.GetCell(j).StringCellValue == "手机号码")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "")
        ////                                {
        ////                                    if (System.Text.RegularExpressions.Regex.IsMatch(GetCellValue(row.GetCell(j)), @"1[0-9]{10}"))
        ////                                    {
        ////                                        model.Tel = GetCellValue(row.GetCell(j));
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列手机号码格式不正确!\n";
        ////                                    }
        ////                                }
        ////                            }
        ////                            if (headerRow.GetCell(j).StringCellValue == "办公电话")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "")
        ////                                {
        ////                                    if (System.Text.RegularExpressions.Regex.IsMatch(GetCellValue(row.GetCell(j)), @"^((0\d{2,5}-)|\(0\d{2,5}\))?\d{7,8}(-\d{3,4})?$"))
        ////                                    {
        ////                                        model.Phone = GetCellValue(row.GetCell(j));
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列办公电话格式不正确!\n";
        ////                                    }
        ////                                }
        ////                            }
        ////                            if (headerRow.GetCell(j).StringCellValue == "QQ")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "")
        ////                                {
        ////                                    if (System.Text.RegularExpressions.Regex.IsMatch(GetCellValue(row.GetCell(j)), @"^[1-9]\d*$"))
        ////                                    {
        ////                                        model.QQ = GetCellValue(row.GetCell(j));
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列QQ号码格式不正确!\n";
        ////                                    }
        ////                                }
        ////                            }
        ////                            if (headerRow.GetCell(j).StringCellValue == "邮箱")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "")
        ////                                {
        ////                                    if (System.Text.RegularExpressions.Regex.IsMatch(GetCellValue(row.GetCell(j)), @"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$"))
        ////                                    {
        ////                                        model.Email = GetCellValue(row.GetCell(j));
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列邮箱格式不正确!\n";
        ////                                    }
        ////                                }
        ////                            }
        ////                            if (headerRow.GetCell(j).StringCellValue == "生日")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "")
        ////                                {
        ////                                    if (System.Text.RegularExpressions.Regex.IsMatch(GetCellValue(row.GetCell(j)), @"^((?:19|20)\d\d)-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])$"))
        ////                                    {
        ////                                        model.BirthDay = Convert.ToDateTime(GetCellValue(row.GetCell(j)));
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列生日格式不正确!\n";
        ////                                    }
        ////                                }
        ////                            }
        ////                            if (headerRow.GetCell(j).StringCellValue == "旺旺")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "")
        ////                                {
        ////                                    if (System.Text.RegularExpressions.Regex.IsMatch(GetCellValue(row.GetCell(j)), @"^[a-zA-Z0-9\u4e00-\u9fa5]+$"))
        ////                                    {
        ////                                        model.WW = GetCellValue(row.GetCell(j));
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列旺旺格式不正确!\n";
        ////                                    }
        ////                                }
        ////                            }
        ////                            if (headerRow.GetCell(j).StringCellValue == "业务员编号")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "")
        ////                                {
        ////                                    string val = GetCellValue(row.GetCell(j));
        ////                                    if (u_BLL.GetAll().Where(t => t.Id == val).Count() > 0)
        ////                                    {
        ////                                        model.UserId = val;
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列业务员编号不存在!\n";
        ////                                    }
        ////                                }
        ////                            }
        ////                            if (headerRow.GetCell(j).StringCellValue == "客户级别")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "")
        ////                                {
        ////                                    string val = GetCellValue(row.GetCell(j));
        ////                                    if (t_BLL.GetAll().Where(t => t.TypeId == (int)TypeEnum.客户级别 && t.TypeName == val).Count() > 0)
        ////                                    {
        ////                                        model.CustomerLevel = t_BLL.GetAll().Where(t => t.TypeId == (int)TypeEnum.客户级别 && t.TypeName == val).FirstOrDefault().Id;
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列客户级别不存在!\n";
        ////                                    }
        ////                                }
        ////                            }
        ////                            if (headerRow.GetCell(j).StringCellValue == "客户类型")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "")
        ////                                {
        ////                                    string val = GetCellValue(row.GetCell(j));
        ////                                    if (t_BLL.GetAll().Where(t => t.TypeId == (int)TypeEnum.客户类型 && t.TypeName == val).Count() > 0)
        ////                                    {
        ////                                        model.CustomerType = t_BLL.GetAll().Where(t => t.TypeId == (int)TypeEnum.客户类型 && t.TypeName == val).FirstOrDefault().Id;
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列客户类型不存在!\n";
        ////                                    }
        ////                                }
        ////                            }
        ////                            if (headerRow.GetCell(j).StringCellValue == "客户来源")
        ////                            {
        ////                                if (GetCellValue(row.GetCell(j)) != "")
        ////                                {
        ////                                    string val = GetCellValue(row.GetCell(j));
        ////                                    if (t_BLL.GetAll().Where(t => t.TypeId == (int)TypeEnum.客户来源 && t.TypeName == val).Count() > 0)
        ////                                    {
        ////                                        model.CustomerSource = t_BLL.GetAll().Where(t => t.TypeId == (int)TypeEnum.客户来源 && t.TypeName == val).FirstOrDefault().Id;
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        error += "第" + r + "行" + "第" + c + "列客户来源不存在!\n";
        ////                                    }
        ////                                }
        ////                            }
        ////                        }
        ////                        if (error == "" && model.CustomerName != "")
        ////                        {
        ////                            model.ID = Guid.NewGuid();
        ////                            model.CustomerContactID = Guid.NewGuid();
        ////                            model.CreateUserId = GetCurrentUserId();
        ////                            model.CreateTime = DateTime.Now;
        ////                            list.Add(model);
        ////                        }
        ////                    }
        ////                }
        ////            }

        ////            if (error == "")
        ////            {
        ////                using (DBContainer db = new DBContainer())
        ////                {
        ////                    using (TransactionScope transaction = new TransactionScope())
        ////                    {
        ////                        foreach (var item in list)
        ////                        {
        ////                            CRM_Customer c = new CRM_Customer();
        ////                            if (item.UserId == null || item.UserId == "")
        ////                            {
        ////                                c.IsPublic = 1;
        ////                                c.IsAssign = 0;
        ////                            }
        ////                            else
        ////                            {
        ////                                c.UserId = item.UserId;
        ////                                c.IsPublic = 0;
        ////                                c.IsAssign = 1;
        ////                            }
        ////                            c.ID = item.ID;
        ////                            c.CustomerName = item.CustomerName;
        ////                            c.Address = item.Address;
        ////                            c.ZipCode = item.ZipCode;
        ////                            c.CustomerWeb = item.CustomerWeb;
        ////                            c.IsCompany = 0;
        ////                            c.ProvinceID = 0;
        ////                            c.CityID = 0;
        ////                            c.AreaID = 0;
        ////                            c.CustomerLevel = item.CustomerLevel;
        ////                            c.CustomerType = item.CustomerType;
        ////                            c.CustomerSource = item.CustomerSource;
        ////                            c.CustomerBusiness = 0;
        ////                            c.EmployeeNum = 0;
        ////                            c.CompanyNature = 0;
        ////                            c.CustomerState = 0;
        ////                            c.IsEnable = 1;
        ////                            c.PublicState = 0;
        ////                            c.CustomerProperty = 0;
        ////                            c.IsCareState = 0;
        ////                            c.IsFollowState = 0;
        ////                            c.FollowCount = 0;
        ////                            c.CustomerContactID = item.CustomerContactID;
        ////                            c.CreateUserID = item.CreateUserId;
        ////                            c.CreateTime = item.CreateTime;
        ////                            db.CRM_Customer.AddObject(c);
        ////                            db.SaveChanges();

        ////                            CRM_CustomerContact cc = new CRM_CustomerContact();
        ////                            cc.ID = item.CustomerContactID;
        ////                            cc.CustomerID = item.ID;
        ////                            cc.IsDefault = 1;
        ////                            cc.IsEnable = 1;
        ////                            cc.ContactName = item.ContactName == null ? "-" : item.ContactName;
        ////                            cc.Tel = item.Tel;
        ////                            cc.Phone = item.Phone;
        ////                            cc.QQ = item.QQ;
        ////                            cc.Email = item.Email;
        ////                            cc.BirthDay = item.BirthDay;
        ////                            cc.WW = item.WW;
        ////                            db.CRM_CustomerContact.AddObject(cc);
        ////                            db.SaveChanges();
        ////                        }
        ////                        //提交事务
        ////                        transaction.Complete();
        ////                        return Content("导入成功!", "text/plain");
        ////                    }
        ////                }
        ////            }
        ////            else
        ////            {
        ////                return Content("导入失败!\n" + error, "text/plain");
        ////            }

        ////        }

        ////        catch (Exception ex)
        ////        {
        ////            return Content("导入失败!\n" + ex, "text/plain");
        ////        }
        ////    }
        ////    else
        ////    {
        ////        return Content("导入格式不正确!导入文件必须为Excel文件!", "text/plain");
        ////    }
        ////}

        //[AcceptVerbs(HttpVerbs.Post)]
        //[HttpPost]
        //public JsonResult Import(HttpPostedFileBase file)
        //{
        //    int k = 4;
        //    string filePath = Server.MapPath("~/UploadFiles/");
        //    string fileName = Path.GetFileName(file.FileName);
        //    string fileExtension = Path.GetExtension(fileName);

        //    string strext = GetFilterFileExt(".xls,.xlsx");

        //    if (!strext.Contains(fileExtension))
        //    {
        //        return Json("文件格式不正确!客户模板导入只支持" + strext + "后缀名的文件" + "\r\n", JsonRequestBehavior.AllowGet);
        //    }

        //    string saveName = Guid.NewGuid().ToString() + fileExtension;
        //    file.SaveAs(filePath + saveName);

        //    HSSFWorkbook hssfworkbook;
        //    //初始化
        //    try
        //    {
        //        using (FileStream _file = new FileStream(filePath + saveName, FileMode.Open, FileAccess.Read))
        //        {
        //            hssfworkbook = new HSSFWorkbook(_file);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json("打开文件失败" + "\r\n", JsonRequestBehavior.AllowGet);
        //    }

        //    try
        //    {
        //        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0); //获取excel的第一个sheet
        //        IRow headerRow = sheet.GetRow(k - 1); //获取sheet的首行
        //        int cellCount = headerRow.LastCellNum; //一行最后一个方格的编号 即总的列数 
        //        int rowCount = sheet.LastRowNum;

        //        #region 存入到Hashtable中
        //        Hashtable extTable = new Hashtable();
        //        extTable.Add("0", "客户名称");
        //        extTable.Add("1", "公司个人");
        //        extTable.Add("2", "客户类型");
        //        extTable.Add("3", "客户级别");
        //        extTable.Add("4", "客户属性");
        //        extTable.Add("5", "员工人数");
        //        extTable.Add("6", "所属行业");
        //        extTable.Add("7", "客户状态");
        //        extTable.Add("8", "公司性质");
        //        extTable.Add("9", "客户来源");
        //        extTable.Add("10", "省");
        //        extTable.Add("11", "市");
        //        extTable.Add("12", "区");
        //        extTable.Add("13", "详细地址");
        //        extTable.Add("14", "客户网站");
        //        extTable.Add("15", "邮政编码");
        //        extTable.Add("16", "联系人名称");
        //        extTable.Add("17", "手机号码");
        //        extTable.Add("18", "办公电话");
        //        extTable.Add("19", "QQ");
        //        extTable.Add("20", "电子邮件");
        //        #endregion

        //        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //        for (int i = headerRow.FirstCellNum; i < extTable.Count; i++)
        //        {
        //            if (extTable[i.ToString()].ToString() != headerRow.GetCell(i).StringCellValue)
        //            {
        //                sb.AppendFormat("第一行第{0}列，[{1}]不存在\r\n", i + 1, extTable[i.ToString()]);
        //            }
        //        }
        //        if (sb.ToString().Length > 0)
        //        {
        //            return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
        //        }

        //        List<CRM_Customer_Model> list = new List<CRM_Customer_Model>();//客户列表

        //        List<CRM_CustomerContact_Model> list1 = new List<CRM_CustomerContact_Model>();//联系人列表
        //        for (int i = (sheet.FirstRowNum + k); i <= rowCount; i++)
        //        {
        //            int ProvinceID2 = -1; //省id
        //            int CityID2 = -1;//城市id
        //            IRow row = sheet.GetRow(i);
        //            if (IsNullRow(row, extTable.Count))
        //            {
        //                continue;
        //            }
        //            if (row != null)
        //            {

        //                CRM_Customer_Model model = new CRM_Customer_Model();
        //                CRM_CustomerContact_Model model1 = new CRM_CustomerContact_Model();
        //                for (int j = 0; j < extTable.Count; j++)
        //                {

        //                    #region 循环判断excel的数据是否正确
        //                    if (headerRow.GetCell(j).StringCellValue == "客户名称")
        //                    {
        //                        model.CustomerName = GetCellValue(row.GetCell(j));
        //                        if (string.IsNullOrWhiteSpace(model.CustomerName))
        //                        {
        //                            sb.AppendFormat("第{0}行第{1}列，[{2}]为空\r\n", i + 1, j + 1, "客户名称");
        //                        }
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "公司个人")
        //                    {
        //                        string IsCompany1 = GetCellValue(row.GetCell(j));

        //                        if (!string.IsNullOrEmpty(IsCompany1))
        //                        {
        //                            model.IsCompany = t_BLL.GetEnableAll().FirstOrDefault(t => t.TypeId == (int)TypeEnum.公司个人 && t.TypeName == IsCompany1).Id;
        //                        }
        //                        else
        //                        {
        //                            sb.AppendFormat("第{0}行第{1}列，[{2}]为空\r\n", i + 1, j + 1, "公司个人");
        //                        }


        //                        //model.IsCompany = GetCellValue(row.GetCell(j))=="个人"?0:1;
        //                        //if (model.IsCompany>0)
        //                        //{
        //                        //    sb.AppendFormat("第{0}行第{1}列，[{2}]为空\r\n", i+1, j + 1, "公司个人");
        //                        //}
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "客户类型")
        //                    {
        //                        string cusType = GetCellValue(row.GetCell(j));
        //                        if (!string.IsNullOrEmpty(cusType))
        //                        {
        //                            model.CustomerType = t_BLL.GetEnableAll().FirstOrDefault(t => t.TypeId == (int)TypeEnum.客户类型 && t.TypeName == cusType).Id;
        //                        }
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "客户级别")
        //                    {
        //                        string customerLevel = GetCellValue(row.GetCell(j));

        //                        if (!string.IsNullOrEmpty(customerLevel))
        //                        {
        //                            model.CustomerLevel = t_BLL.GetEnableAll().FirstOrDefault(t => t.TypeId == (int)TypeEnum.客户级别 && t.TypeName == customerLevel).Id;
        //                        }
        //                        else
        //                        {
        //                            sb.AppendFormat("第{0}行第{1}列，[{2}]为空\r\n", i + 1, j + 1, "客户级别");
        //                        }
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "客户属性")
        //                    {
        //                        string CustomerProperty1 = GetCellValue(row.GetCell(j));
        //                        if (!string.IsNullOrEmpty(CustomerProperty1))
        //                        {
        //                            model.CustomerProperty = t_BLL.GetEnableAll().FirstOrDefault(t => t.TypeId == (int)TypeEnum.客户属性 && t.TypeName == CustomerProperty1).Id;
        //                        }
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "员工人数")
        //                    {
        //                        string EmployeeNum1 = GetCellValue(row.GetCell(j));
        //                        if (!string.IsNullOrEmpty(EmployeeNum1))
        //                        {
        //                            model.EmployeeNum = t_BLL.GetEnableAll().FirstOrDefault(t => t.TypeId == (int)TypeEnum.员工人数 && t.TypeName == EmployeeNum1).Id;
        //                        }
        //                        else
        //                        {
        //                            sb.AppendFormat("第{0}行第{1}列，[{2}]为空\r\n", i + 1, j + 1, "员工人数");
        //                        }
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "所属行业")
        //                    {
        //                        string CustomerBusiness1 = GetCellValue(row.GetCell(j));
        //                        if (!string.IsNullOrEmpty(CustomerBusiness1))
        //                        {
        //                            model.CustomerBusiness = t_BLL.GetEnableAll().FirstOrDefault(t => t.TypeId == (int)TypeEnum.所属行业 && t.TypeName == CustomerBusiness1).Id;
        //                        }
        //                        //else
        //                        //{
        //                        //    sb.AppendFormat("第{0}行第{1}列，[{2}]为空\r\n", i + 1, j + 1, "所属行业");
        //                        //}
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "客户状态")
        //                    {
        //                        string CustomerState1 = GetCellValue(row.GetCell(j));
        //                        if (!string.IsNullOrEmpty(CustomerState1))
        //                        {
        //                            model.CustomerState = t_BLL.GetEnableAll().FirstOrDefault(t => t.TypeId == (int)TypeEnum.客户状态 && t.TypeName == CustomerState1).Id;
        //                        }
        //                        else
        //                        {
        //                            sb.AppendFormat("第{0}行第{1}列，[{2}]为空\r\n", i + 1, j + 1, "客户状态");
        //                        }
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "公司性质")
        //                    {
        //                        string CompanyNature1 = GetCellValue(row.GetCell(j));
        //                        if (!string.IsNullOrEmpty(CompanyNature1))
        //                        {
        //                            //去掉查不出来的数据
        //                            CRM_Type_Model t1 = t_BLL.GetEnableAll().FirstOrDefault(t => t.TypeId == (int)TypeEnum.公司性质 && t.TypeName == CompanyNature1);
        //                            if (t1 != null)
        //                            {
        //                                model.CompanyNature = t1.Id;
        //                            }
        //                            else
        //                            {
        //                                sb.AppendFormat("第{0}行第{1}列，[{2}]\r\n", i + 1, j + 1, "公司性质不存在");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            sb.AppendFormat("第{0}行第{1}列，[{2}]为空\r\n", i + 1, j + 1, "公司性质");
        //                        }
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "客户来源")
        //                    {
        //                        string CustomerSource1 = GetCellValue(row.GetCell(j));
        //                        if (!string.IsNullOrEmpty(CustomerSource1))
        //                        {
        //                            model.CustomerSource = t_BLL.GetEnableAll().FirstOrDefault(t => t.TypeId == (int)TypeEnum.客户来源 && t.TypeName == CustomerSource1).Id;
        //                        }
        //                        else
        //                        {
        //                            sb.AppendFormat("第{0}行第{1}列，[{2}]为空\r\n", i + 1, j + 1, "客户来源");
        //                        }
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "省")
        //                    {
        //                        string ProvinceID1 = GetCellValue(row.GetCell(j));
        //                        if (!string.IsNullOrEmpty(ProvinceID1))
        //                        {
        //                            CRM_Province_Model modelProvince = p_BLL.GetList().FirstOrDefault(t => t.ProvinceName == ProvinceID1);
        //                            if (modelProvince != null)
        //                            {
        //                                ProvinceID2 = model.ProvinceID = p_BLL.GetList().FirstOrDefault(t => t.ProvinceName == ProvinceID1).ProvinceID;
        //                            }
        //                            else
        //                            {
        //                                sb.AppendFormat("第{0}行第{1}列，[{2}]\r\n", i + 1, j + 1, "省输入错误");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            sb.AppendFormat("第{0}行第{1}列，[{2}]为空\r\n", i + 1, j + 1, "省");
        //                        }
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "市")
        //                    {
        //                        string CityID1 = GetCellValue(row.GetCell(j));
        //                        if (ProvinceID2 != -1)
        //                        {
        //                            if (!string.IsNullOrEmpty(CityID1))
        //                            {
        //                                CRM_City_Model modelCity = ct_BLL.GetListByParentID(ProvinceID2).FirstOrDefault(t => t.CityName == CityID1);
        //                                if (modelCity != null)
        //                                {
        //                                    CityID2 = model.CityID = ct_BLL.GetListByParentID(ProvinceID2).FirstOrDefault(t => t.CityName == CityID1).CityID;
        //                                }
        //                                else
        //                                {
        //                                    sb.AppendFormat("第{0}行第{1}列，[{2}]\r\n", i + 1, j + 1, "市输入错误");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                sb.AppendFormat("第{0}行第{1}列，[{2}]为空\r\n", i + 1, j + 1, "市");
        //                            }
        //                        }
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "区")
        //                    {
        //                        string AreaID1 = GetCellValue(row.GetCell(j));
        //                        if (CityID2 != -1)
        //                        {
        //                            if (!string.IsNullOrEmpty(AreaID1))
        //                            {
        //                                CRM_District_Model modelDistrict = dc_BLL.GetListByParentID(CityID2).FirstOrDefault(t => t.DistrictName == AreaID1);
        //                                if (modelDistrict != null)
        //                                {
        //                                    model.AreaID = dc_BLL.GetListByParentID(CityID2).FirstOrDefault(t => t.DistrictName == AreaID1).DistrictID;
        //                                }
        //                                else
        //                                {
        //                                    sb.AppendFormat("第{0}行第{1}列，[{2}]\r\n", i + 1, j + 1, "区输入错误");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                sb.AppendFormat("第{0}行第{1}列，[{2}]为空\r\n", i + 1, j + 1, "区");
        //                            }
        //                        }
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "详细地址")
        //                    {
        //                        model.Address = GetCellValue(row.GetCell(j));
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "客户网站")
        //                    {
        //                        model.CustomerWeb = GetCellValue(row.GetCell(j));
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "邮政编码")
        //                    {
        //                        model.ZipCode = GetCellValue(row.GetCell(j));
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "联系人名称")
        //                    {
        //                        model1.ContactName = GetCellValue(row.GetCell(j));
        //                        if (string.IsNullOrWhiteSpace(model1.ContactName))
        //                        {
        //                            sb.AppendFormat("第{0}行第{1}列，[{2}]为空\r\n", i + 1, j + 1, "联系人名称");
        //                        }
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "手机号码")
        //                    {
        //                        model1.Tel = GetCellValue(row.GetCell(j));
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "办公电话")
        //                    {
        //                        model1.Phone = GetCellValue(row.GetCell(j));
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "QQ")
        //                    {
        //                        model1.QQ = GetCellValue(row.GetCell(j));
        //                    }
        //                    else if (headerRow.GetCell(j).StringCellValue == "电子邮件")
        //                    {
        //                        model1.Email = GetCellValue(row.GetCell(j));
        //                    }

        //                    else if (headerRow.GetCell(j).StringCellValue == "生日")
        //                    {
        //                        model1.BirthDay = DateTime.Parse(GetCellValue(row.GetCell(j)));
        //                    }
        //                    #endregion

        //                }

        //                if (sb.ToString().Length == 0)
        //                {

        //                    #region 如果正确，则新增到数据库
        //                    if (!string.IsNullOrWhiteSpace(model.CustomerName)
        //                        && model.IsCompany > 0
        //                        && model.CustomerLevel > 0
        //                        && model.EmployeeNum > 0
        //                        && model.CompanyNature > 0
        //                        && model.CustomerSource > 0
        //                        && model.ProvinceID > 0
        //                        && model.CityID > 0
        //                        && model.AreaID > 0
        //                        && !string.IsNullOrEmpty(model1.ContactName)
        //                        )
        //                    {
        //                        Guid guid1 = Guid.NewGuid();
        //                        model.ID = Guid.NewGuid();
        //                        model.CustomerContactID = guid1;
        //                        model.CreateUserId = model.CreateUserId = GetCurrentUserId();
        //                        model.CreateTime = model.CreateTime = DateTime.Now;
        //                        model.ModifyUserId = model.CreateUserId = GetCurrentUserId();
        //                        model.ModifyTime = model.CreateTime = DateTime.Now;
        //                        model.IsPublic = 1;
        //                        model.IsAssign = 0;
        //                        list.Add(model);
        //                        model1.ID = guid1;
        //                        model1.CustomerID = model.ID;
        //                        model1.IsEnable = 1;
        //                        list1.Add(model1);

        //                    }
        //                    #endregion
        //                }

        //            }
        //        }

        //        if (sb.ToString().Length > 0)
        //        {
        //            return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
        //        }
        //        if (list.Count == 0)
        //        {
        //            return Json("上传的文件中没有数据！请重新选择文件！\r\n", JsonRequestBehavior.AllowGet);
        //        }

        //        if (list1.Count == 0)
        //        {
        //            return Json("上传的文件中没有数据！请重新选择文件！\r\n", JsonRequestBehavior.AllowGet);
        //        }

        //        using (TransactionScope transaction = new TransactionScope())
        //        {
        //            foreach (var item in list)
        //            {
        //                if (!m_BLL.Create(ref errors, item))
        //                {
        //                    throw new Exception("导入时出错：" + errors.Error + "\r\n");
        //                }
        //            }
        //            foreach (var item in list1)
        //            {
        //                if (!cc_BLL.Create(ref errors, item))
        //                {
        //                    throw new Exception("导入时出错：" + errors.Error + "\r\n");
        //                }
        //            }
        //            transaction.Complete();//提交事务
        //        }
        //        return Json("导入成功!", JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("导入失败!" + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
        //    }
        //}

        ////<summary>
        ////获取单元格值
        ////</summary>
        ////<param name="cell"></param>
        ////<returns></returns>
        //private static string GetCellValue(ICell cell)
        //{
        //    if (cell == null)
        //        return string.Empty;
        //    switch (cell.CellType)
        //    {
        //        case CellType.Blank:
        //            return string.Empty;
        //        case CellType.Boolean:
        //            return cell.BooleanCellValue.ToString();
        //        case CellType.Error:
        //            return cell.ErrorCellValue.ToString();
        //        case CellType.Numeric:
        //            short format = cell.CellStyle.DataFormat;
        //            if (format == 22 || format == 20)
        //            {
        //                DateTime date = cell.DateCellValue;
        //                return date.ToString("yyy-MM-dd HH:mm:ss");
        //            }
        //            else
        //            {
        //                return cell.NumericCellValue.ToString();
        //            }
        //        case CellType.Unknown:
        //        default:
        //            return cell.ToString();
        //        case CellType.String:
        //            return cell.StringCellValue;
        //        case CellType.Formula:
        //            try
        //            {
        //                HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
        //                e.EvaluateInCell(cell);
        //                return cell.ToString();
        //            }
        //            catch
        //            {
        //                return cell.NumericCellValue.ToString();
        //            }
        //    }
        //}

        ///// <summary>
        ///// 下载
        ///// </summary>
        ///// <param name="TempName"></param>
        ///// <returns></returns>
        //public ActionResult Download(string TempName)
        //{
        //    string filePath = Server.MapPath("~/UploadFiles/" + TempName);
        //    FileStream fs = new FileStream(filePath, FileMode.Open);
        //    byte[] bytes = new byte[(int)fs.Length];
        //    fs.Read(bytes, 0, bytes.Length);
        //    fs.Close();
        //    Response.Charset = "UTF-8";
        //    Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        //    Response.ContentType = "application/octet-stream";
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(TempName));
        //    Response.BinaryWrite(bytes);
        //    Response.Flush();
        //    Response.End();
        //    return new EmptyResult();

        //}
        //#endregion

        //[HttpGet]
        //public ContentResult GetFollowHistory(string Account, string CustomerID, string page)
        //{
        //    string json = "";
        //    CRM_User_Model user = new CRM_User_Model();
        //    if (!Account.Contains("@"))
        //    {
        //        user = u_BLL.GetById(Account);
        //    }
        //    else
        //    {
        //        user = u_BLL.GetAll().SingleOrDefault(t => t.Email == Account);
        //    }
        //    if (user.Id != null)
        //    {
        //        List<string> users = GetSubUsers(user.Id);
        //        Guid cus = new Guid(CustomerID);
        //        List<CRM_Follow_Model> list = f_BLL.GetAll().Where(t => (users.Contains(t.UserID) || t.ShareUserID == user.Id) && t.CustomerID == cus).ToList();
        //        string FollowType = "";
        //        foreach (var item in list.Skip((int.Parse(page) - 1) * 20).Take(20))
        //        {
        //            if (item.FollowType != null)
        //            {
        //                FollowType = t_BLL.GetById((int)item.FollowType).TypeName;
        //            }
        //            json += "\n{\n" + "UserName:\"" + u_BLL.GetById(item.UserID).UserName + "\",\n"
        //                  + "FollowType:\"" + FollowType + "\",\n"
        //                  + "FollowDate:\"" + item.RealTime + "\",\n"
        //                  + "Content:\"" + item.Contents + "\"\n},";

        //        }
        //        if (json.Length > 1)
        //        {
        //            json = "[" + json.Substring(0, json.Length - 1) + "\n]";
        //        }
        //    }

        //    return Content(json, "text/plain", System.Text.Encoding.UTF8);
        //}

        //private List<string> GetSubUsers(string uid)
        //{
        //    List<int> roles = (from ru in ru_BLL.GetAll().Where(t => t.UserId == uid)
        //                       select (int)ru.RoleId).ToList();
        //    List<int> lists = new List<int>();
        //    foreach (int role in roles)
        //    {
        //        if (r_BLL.GetAll().Where(t => t.ParentId == role).Count() > 0)
        //        {
        //            GetChild(lists, role);
        //        }
        //    }
        //    List<string> u = (from ru in ru_BLL.GetAll()
        //                      where lists.Contains(ru.RoleId)
        //                      select ru.UserId).ToList();
        //    u.Add(uid);
        //    return u;
        //}

        //private List<int> GetChild(List<int> lists, int parent)
        //{
        //    List<int> list = new List<int>();
        //    list = (from r in r_BLL.GetAll().Where(t => t.ParentId == parent)
        //            select r.Id).ToList();
        //    if (list.Count() > 0)
        //    {
        //        foreach (int r in list)
        //        {
        //            lists.Add(r);
        //            GetChild(lists, r);
        //        }
        //    }
        //    return lists;
        //}
        ///// <summary>
        ///// 根据客户ID 获取修改Log集合
        ///// </summary>
        ///// <param name="id">id</param>
        ///// <returns>实体</returns>
        //[HttpPost]
        //public JsonResult GetCustomerLogList(GridPager pager, Guid ID)
        //{
        //    var list = m_BLL.GetCustomerLog(ref pager, ID);
        //    var json = new
        //    {
        //        total = pager.totalRows,
        //        rows = list
        //    };
        //    return Json(json, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// 当前行是否为空行
        ///// </summary>
        ///// <param name="row"></param>
        ///// <param name="cellCount"></param>
        ///// <returns></returns>
        //private static bool IsNullRow(IRow row, int cellCount)
        //{
        //    for (int i = 0; i < cellCount; i++)
        //    {
        //        if (!string.IsNullOrWhiteSpace(GetCellValue(row.GetCell(i))))
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        #region 获取客户简单列表
        public ActionResult Index1()
        {
            return View();
        }

        public JsonResult GetList1(GridPager pager)
        {
            var list = m_BLL.GetList(ref pager);
            var json = new
            {
                total = pager.totalRows,
                rows = list
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
