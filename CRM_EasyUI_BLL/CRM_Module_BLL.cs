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

    public class CRM_Module_BLL : ICRM_Module_BLL
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
        public ICRM_Module_Repository Rep { get; set; }

        #endregion

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors errors, CRM_Module_Model model)
        {
            try
            {
                if (this.IsExist(model.ID))
                {
                    errors.Add("主键重复");
                    return false;
                }
                CRM_Module entity = new CRM_Module();
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
        public bool Edit(CRM_Module_Model model)
        {
            try
            {
                if (!IsExist(model.ID))
                {
                    return false;
                }
                CRM_Module entity = new CRM_Module();
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
        public List<CRM_Module_Model> GetList(ref GridPager pager)
        {
            IQueryable<CRM_Module> source = null;
            source = this.Rep.GetList(this.db);
            pager.totalRows = source.Count<CRM_Module>();
            source = LinqHelper.SortingAndPaging<CRM_Module>(source, pager.sort, pager.order, pager.page, pager.rows);
            return this.CreateModelList(ref source);
        }

        /// <summary>
        /// 所有
        /// </summary>
        /// <returns></returns>
        public List<CRM_Module_Model> GetAll()
        {
            IQueryable<CRM_Module> source = this.Rep.GetList(this.db);
            return this.CreateModelList(ref source);
        }


        /// <summary>
        /// 根据Id获得实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CRM_Module_Model GetById(string id)
        {
            CRM_Module_Model model = new CRM_Module_Model();
            if (this.IsExist(id))
            {
                CRM_Module entity = this.Rep.GetById(id);
                Mapper.Map(entity, model);
            }
            return model;
        }

        /// <summary>
        /// 根据Id获得实体详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CRM_Module_Model GetDetailsById(string id)
        {
            CRM_Module_Model model = new CRM_Module_Model();
            if (this.IsExist(id))
            {
                IQueryable<CRM_Module> queryData = Rep.GetList(db).Where(t => t.ID == id);
                model = this.CreateModelList(ref queryData)[0];
            }
            return model;
        }

        /// <summary>
        /// 实体映射
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        private List<CRM_Module_Model> CreateModelList(ref IQueryable<CRM_Module> queryData)
        {
            List<CRM_Module_Model> list = (from r in queryData
                                                         select new CRM_Module_Model
                                                         {
                                                            ID=r.ID,
                                                            Name=r.Name,
                                                            ParentID=r.ParentID,
                                                            Url=r.Url,
                                                            Sort=r.Sort,
                                                            CreateUserID=r.CreateUserID,
                                                            CreateTime=r.CreateTime,
                                                            IsLast=(bool)r.IsLast,
                                                            Enable=r.Enable
                                                         }).ToList<CRM_Module_Model>();
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

        public List<CRM_Module_Model> GetList(string parentId)
        {
            IQueryable<CRM_Module> queryData = GetIQueryable().Where(a => a.ParentID == parentId);
            List<CRM_Module_Model> list = this.CreateModelList(ref queryData);
            return list;
        }

        /// <summary>
        /// 根据父级Id和角色ID获取模块
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<CRM_Module_Model> GetList(string parentId, int RoleID)
        {
            List<CRM_Module_Model> list = (from r in db.CRM_Module
                        join t1 in db.CRM_Right on r.ID equals t1.ModulID
                        where t1.RoleID == RoleID && r.ParentID == parentId
                        select new CRM_Module_Model
                        {
                            ID = r.ID,
                            Name = r.Name,
                            ParentID = r.ParentID,
                            Url = r.Url,
                            Sort = r.Sort,
                            CreateUserID = r.CreateUserID,
                            CreateTime = r.CreateTime,
                            IsLast = (bool)r.IsLast,
                            Enable = r.Enable
                        }).ToList<CRM_Module_Model>();
            return list;
        }


        private IQueryable<CRM_Module> GetIQueryable()
        {
            IQueryable<CRM_Module> query = DataCache.GetCache("CRM_Module") as IQueryable<CRM_Module>;
            if (query == null)
            {
                query = from a in this.Rep.GetList(db)
                        orderby a.Sort
                        select a;
            }
            return query;
        }



        /// <summary>
        /// 根绝角色ID获取相关的模块
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public List<CRM_Module_Model> GetListByRoleID()
        {
            List<CRM_Module_Model> list= (from r in db.CRM_Module
                                        join t1 in db.CRM_User on r.CreateUserID equals t1.ID
                                        where r.ParentID=="0"
                                        select new CRM_Module_Model
                                        {
                                            ID = r.ID,
                                            Name = r.Name,
                                            ParentID = r.ParentID,
                                            Url = r.Url,
                                            Sort = r.Sort,
                                            CreateUserID = r.CreateUserID,
                                            CreateTime = r.CreateTime,
                                            IsLast = (bool)r.IsLast,
                                            Enable = r.Enable
                                        }).ToList<CRM_Module_Model>();
          
            return list;
        
        }

    }
}


