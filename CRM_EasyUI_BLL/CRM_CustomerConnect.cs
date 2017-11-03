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

    public class CRM_CustomerConnect_BLL : ICRM_CustomerConnect_BLL
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
        public ICRM_CustomerConnect_Repository Rep { get; set; }

        #endregion

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors errors, CRM_CustomerConnect_Model model)
        {
            try
            {
                if (this.IsExist((Guid)model.ID))
                {
                    errors.Add("主键重复");
                    return false;
                }
                CRM_CustomerConnect entity = new CRM_CustomerConnect();
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
        public bool Edit(CRM_CustomerConnect_Model model)
        {
            try
            {
                if (!IsExist((Guid)model.ID))
                {
                    return false;
                }
                CRM_CustomerConnect entity = new CRM_CustomerConnect();
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
        public List<CRM_CustomerConnect_Model> GetList(ref GridPager pager)
        {
            IQueryable<CRM_CustomerConnect> source = null;
            source = this.Rep.GetList(this.db);
            pager.totalRows = source.Count<CRM_CustomerConnect>();
            source = LinqHelper.SortingAndPaging<CRM_CustomerConnect>(source, pager.sort, pager.order, pager.page, pager.rows);
            return this.CreateModelList(ref source);
        }

        /// <summary>
        /// 所有
        /// </summary>
        /// <returns></returns>
        public List<CRM_CustomerConnect_Model> GetAll()
        {
            IQueryable<CRM_CustomerConnect> source = this.Rep.GetList(this.db);
            return this.CreateModelList(ref source);
        }


        /// <summary>
        /// 根据Id获得实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CRM_CustomerConnect_Model GetById(Guid id)
        {
            CRM_CustomerConnect_Model model = new CRM_CustomerConnect_Model();
            if (this.IsExist(id))
            {
                CRM_CustomerConnect entity = this.Rep.GetById(id);
                Mapper.Map(entity, model);
            }
            return model;
        }

        /// <summary>
        /// 根据Id获得实体详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CRM_CustomerConnect_Model GetDetailsById(Guid id)
        {
            CRM_CustomerConnect_Model model = new CRM_CustomerConnect_Model();
            if (this.IsExist(id))
            {
                IQueryable<CRM_CustomerConnect> queryData = Rep.GetList(db).Where(t => t.ID == id);
                model = this.CreateModelList(ref queryData)[0];
            }
            return model;
        }

        /// <summary>
        /// 实体映射
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        private List<CRM_CustomerConnect_Model> CreateModelList(ref IQueryable<CRM_CustomerConnect> queryData)
        {
            List<CRM_CustomerConnect_Model> list = (from r in queryData
                                                         select new CRM_CustomerConnect_Model
                                                         {
                                                            ID=r.ID,
                                                            ContactName=r.ContactName,
                                                            Sex=r.Sex,
                                                            CustomerID=r.CustomerID,
                                                            IsDefault=r.IsDefault,
                                                            Tel=r.Tel,
                                                            Phone=r.Phone,
                                                            Fax=r.Fax,
                                                            Email=r.Email,
                                                            QQ=r.QQ,
                                                            Address=r.Address,
                                                            CreateUserID=r.CreateUserID,
                                                            CreateTime=r.CreateTime,
                                                            IsEnable=r.IsEnable,

                                                         }).ToList<CRM_CustomerConnect_Model>();
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

    }
}


