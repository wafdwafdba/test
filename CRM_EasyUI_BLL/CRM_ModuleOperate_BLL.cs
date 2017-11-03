// ----------------------------------------------------------------
// Copyright (C) 2005 武汉群策联动软件有限公司
// 版权所有。 
//         
// 文件功能描述：
//
// 
// 创建标识：xxx------2016/2/15------      
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

    public class CRM_ModuleOperate_BLL : ICRM_ModuleOperate_BLL
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
        public ICRM_ModuleOperate_Repository Rep { get; set; }

        #endregion

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors errors, CRM_ModuleOperate_Model model)
        {
            try
            {
                if (this.IsExist(model.ID))
                {
                    errors.Add("主键重复");
                    return false;
                }
                CRM_ModuleOperate entity = new CRM_ModuleOperate();
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
        public bool Delete(string id)
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
        public bool Edit(CRM_ModuleOperate_Model model)
        {
            try
            {
                if (!IsExist(model.ID))
                {
                    return false;
                }
                CRM_ModuleOperate entity = new CRM_ModuleOperate();
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
        public List<CRM_ModuleOperate_Model> GetList(ref GridPager pager)
        {
            IQueryable<CRM_ModuleOperate> source = null;
            source = this.Rep.GetList(this.db);
            pager.totalRows = source.Count<CRM_ModuleOperate>();
            source = LinqHelper.SortingAndPaging<CRM_ModuleOperate>(source, pager.sort, pager.order, pager.page, pager.rows);
            return this.CreateModelList(ref source);
        }

        /// <summary>
        /// 所有
        /// </summary>
        /// <returns></returns>
        public List<CRM_ModuleOperate_Model> GetAll()
        {
            IQueryable<CRM_ModuleOperate> source = this.Rep.GetList(this.db);
            return this.CreateModelList(ref source);
        }


        /// <summary>
        /// 根据Id获得实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CRM_ModuleOperate_Model GetById(string id)
        {
            CRM_ModuleOperate_Model model = new CRM_ModuleOperate_Model();
            if (this.IsExist(id))
            {
                CRM_ModuleOperate entity = this.Rep.GetById(id);
                Mapper.Map(entity, model);
            }
            return model;
        }

        /// <summary>
        /// 根据Id获得实体详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CRM_ModuleOperate_Model GetDetailsById(string id)
        {
            CRM_ModuleOperate_Model model = new CRM_ModuleOperate_Model();
            if (this.IsExist(id))
            {
                IQueryable<CRM_ModuleOperate> queryData = Rep.GetList(db).Where(t => t.ID == id);
                model = this.CreateModelList(ref queryData)[0];
            }
            return model;
        }

        /// <summary>
        /// 实体映射
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        private List<CRM_ModuleOperate_Model> CreateModelList(ref IQueryable<CRM_ModuleOperate> queryData)
        {
            List<CRM_ModuleOperate_Model> list = (from r in queryData
                                                         select new CRM_ModuleOperate_Model
                                                         {
                                                            ID=r.ID,
                                                            Name=r.Name,
                                                            KeyCode=r.KeyCode,
                                                            ModuleId=r.ModuleId,
                                                            IsValid=r.IsValid,
                                                            Sort=r.Sort,

                                                         }).ToList<CRM_ModuleOperate_Model>();
            return list;

        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsExist(string id)
        {
            return this.Rep.GetById(id) != null;
        }


        public List<CRM_ModuleOperate_Model> GetList(ref GridPager pager, string mid)
        {
            IQueryable<CRM_ModuleOperate> source = GetIQueryable().Where(a => a.ModuleId == mid);
            pager.totalRows = source.Count<CRM_ModuleOperate>();
            source = LinqHelper.SortingAndPaging<CRM_ModuleOperate>(source, pager.sort, pager.order, pager.page, pager.rows);
            return this.CreateModelList(ref source);
        }

        public IQueryable<CRM_ModuleOperate> GetIQueryable()
        {
            IQueryable<CRM_ModuleOperate> query = DataCache.GetCache("CRM_ModuleOperate") as IQueryable<CRM_ModuleOperate>;
            if (query == null)
            {
                query = this.Rep.GetList(db);
                DataCache.SetCache("CRM_ModuleOperate", query);
            }
            return query;
        }
    }
}


