// ----------------------------------------------------------------
// Copyright (C) 2005 武汉群策联动软件有限公司
// 版权所有。 
//         
// 文件功能描述：
//
// 
// 创建标识：xxx------2016/3/21------      
// ----------------------------------------------------------------

namespace GroupThink.CRM.BLL
{
    using GroupThink.CRM.BLL.Core;
    using GroupThink.CRM.Common;
    using GroupThink.CRM.IBLL;
    using GroupThink.CRM.IDAL;
    using GroupThink.CRM.Model;
    using GroupThink.CRM.Model.Sys;
    using Microsoft.Practices.Unity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using AutoMapper;

    public class CRM_Customer_BLL : ICRM_Customer_BLL
    {

        #region 申明
        /// <summary>
        /// 数据容器
        /// </summary>
        private DBContainer db = new DBContainer();

        /// <summary>
        /// 数据仓库
        /// </summary>
        [Microsoft.Practices.Unity.Dependency]
        public ICRM_Customer_Repository Rep { get; set; }

        #endregion

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors errors, CRM_Customer_Model model)
        {
            try
            {
                if (this.IsExist(model.ID))
                {
                    errors.Add("主键重复");
                    return false;
                }
                CRM_Customer entity = new CRM_Customer();
                Mapper.Map(model, entity);

                if (this.Rep.Create(entity) > 0)
                {
                    return true;
                }
                errors.Add("插入失败");
                return false;
            }
            catch (Exception exception)
            {
                errors.Add(exception.Message);
                ExceptionHander.WriteException(exception);
                return false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            try
            {
                return (this.Rep.Delete(id) == 1);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(CRM_Customer_Model model)
        {
            try
            {
                if (!IsExist(model.ID))
                {
                    return false;
                }
                CRM_Customer entity = new CRM_Customer();
                Mapper.Map(model, entity);
                return (this.Rep.Edit(entity) > 0);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<CRM_Customer_Model> GetList(ref GridPager pager)
        {
            IQueryable<CRM_Customer> source = null;
            source = this.Rep.GetList(this.db);
            pager.totalRows = source.Count<CRM_Customer>();
            source = LinqHelper.SortingAndPaging<CRM_Customer>(source, pager.sort, pager.order, pager.page, pager.rows);
            return this.CreateModelList(ref source);
        }

        /// <summary>
        /// 所有
        /// </summary>
        /// <returns></returns>
        public List<CRM_Customer_Model> GetAll()
        {
            IQueryable<CRM_Customer> source = this.Rep.GetList(this.db);
            return this.CreateModelList(ref source);
        }


        /// <summary>
        /// 根据Id获得实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CRM_Customer_Model GetById(Guid id)
        {
            CRM_Customer_Model model = new CRM_Customer_Model();
            if (this.IsExist(id))
            {
                CRM_Customer entity = this.Rep.GetById(id);
                Mapper.Map(entity, model);
            }
            return model;
        }

        /// <summary>
        /// 根据Id获得实体详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CRM_Customer_Model GetDetailsById(Guid id)
        {
            CRM_Customer_Model model = new CRM_Customer_Model();
            if (this.IsExist(id))
            {
                IQueryable<CRM_Customer> queryData = Rep.GetList(db).Where(t => t.ID == id);
                model = this.CreateModelList(ref queryData)[0];
            }
            return model;
        }

        /// <summary>
        /// 实体映射
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        private List<CRM_Customer_Model> CreateModelList(ref IQueryable<CRM_Customer> queryData)
        {
            List<CRM_Customer_Model> list = (from r in queryData
                                                         select new CRM_Customer_Model
                                                         {
                                                            ID=r.ID,
                                                            CustomerName=r.CustomerName,
                                                            IsCompany=r.IsCompany,
                                                            CustomerConnect=r.CustomerConnect,
                                                            UserID=r.UserID,
                                                            ShareID=r.ShareID,
                                                            Address=r.Address,
                                                            CustomerSource=r.CustomerSource,
                                                            CustomerType=r.CustomerType,
                                                            CustomerBusiness=r.CustomerBusiness,
                                                            CustomerState=r.CustomerState,
                                                            LastFollowTime=r.LastFollowTime,
                                                            FollowTimes=r.FollowTimes,
                                                            IsPublic=r.IsPublic,
                                                            CusProduct=r.CusProduct,
                                                            CreateUserID=r.CreateUserID,
                                                            CreateTime=r.CreateTime,
                                                            ModifyUserID=r.ModifyUserID,
                                                            ModifyTime=r.ModifyTime,
                                                            IsDelete=r.IsDelete,

                                                         }).ToList<CRM_Customer_Model>();
            return list;

        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsExist(Guid id)
        {
            return this.Rep.GetById(id) != null;
        }

        public List<CRM_Customer_Model> GetList_(ref GridPager pager, string customerName, string contactName, string userId, int _isCompany, int _customerSource, int _customerBusiness, DateTime? _beginDate1, DateTime? _endDate1, DateTime? _beginDate2, DateTime? _endDate2, List<string> users)
        {
            IQueryable<CRM_Customer> query = Rep.GetList(db).Where(t => users.Contains(t.UserID));
            if (!string.IsNullOrEmpty(customerName))
            {
                query = query.Where(t => t.CustomerName.Contains(customerName));
            }
            if (!string.IsNullOrEmpty(contactName))
            {
                query = from t1 in query
                        join t2 in db.CRM_CustomerConnect on t1.CustomerConnect equals t2.ID
                        where t2.ContactName.Contains(contactName)
                        select t1;
            }
            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(t => t.UserID == userId);
            }
            if (_isCompany >0)
            {
                query = query.Where(t => t.IsCompany == _isCompany);
            }
            if (_customerSource > 0)
            {
                query = query.Where(t => t.CustomerSource == _customerSource);
            }
            if (_customerBusiness > 0)
            {
                query = query.Where(t => t.CustomerBusiness == _customerBusiness);
            }
            if (_beginDate1 != null)
            {
                query = query.Where(t => t.CreateTime >= _beginDate1);
            }
            if (_endDate1 != null)
            {
                query = query.Where(t => t.CreateTime <= _endDate1);
            }

            pager.totalRows = query.Count<CRM_Customer>();
            query = LinqHelper.SortingAndPaging<CRM_Customer>(query, pager.sort, pager.order, pager.page, pager.rows);
            return this.CreateModelList(ref query);
        }
    }
}


