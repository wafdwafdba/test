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

    public class CRM_Dept_BLL : ICRM_Dept_BLL
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
        public ICRM_Dept_Repository Rep { get; set; }

        #endregion

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors errors, CRM_Dept_Model model)
        {
            try
            {
                if (this.IsExist(model.DeptID))
                {
                    errors.Add("主键重复");
                    return false;
                }
                CRM_Dept entity = new CRM_Dept();
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
        public bool Delete(int id)
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
        public bool Edit(CRM_Dept_Model model)
        {
            try
            {
                if (!IsExist(model.DeptID))
                {
                    return false;
                }
                CRM_Dept entity = new CRM_Dept();
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
        public List<CRM_Dept_Model> GetList(ref GridPager pager)
        {
            IQueryable<CRM_Dept> source = null;
            source = this.Rep.GetList(this.db);
            pager.totalRows = source.Count<CRM_Dept>();
            source = LinqHelper.SortingAndPaging<CRM_Dept>(source, pager.sort, pager.order, pager.page, pager.rows);
            return this.CreateModelList(ref source);
        }

        /// <summary>
        /// 所有
        /// </summary>
        /// <returns></returns>
        public List<CRM_Dept_Model> GetAll()
        {
            //List<CRM_Dept_Model> list = DataCache.GetCache("CRM_Dept") as List<CRM_Dept_Model>;
            //if (list == null)
            //{
            //    list = (from r in Rep.GetList1(db).Where(t => t.IsDelete == false)
            //            select new CRM_Dept_Model { DeptID = r.DeptID, DeptName = r.DeptName, Des = r.Des, IsEnable = r.IsEnable, ParentID = r.ParentID, IsDelete = r.IsDelete, Level = r.Level}).ToList<CRM_Dept_Model>();
            //    DataCache.SetCache("CRM_Dept", list);
            //}
            List<CRM_Dept_Model> list = new List<CRM_Dept_Model>();
                list = (from r in Rep.GetList1(db).Where(t => t.IsDelete == false)
                        select new CRM_Dept_Model { DeptID = r.DeptID, DeptName = r.DeptName, Des = r.Des, IsEnable = r.IsEnable, ParentID = r.ParentID, IsDelete = r.IsDelete, Level = r.Level }).ToList<CRM_Dept_Model>();
            return list;
        }


        /// <summary>
        /// 根据Id获得实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CRM_Dept_Model GetById(int id)
        {
            CRM_Dept_Model model = new CRM_Dept_Model();
            if (this.IsExist(id))
            {
                CRM_Dept entity = this.Rep.GetById(id);
                Mapper.Map(entity, model);
            }
            return model;
        }

        /// <summary>
        /// 根据Id获得实体详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CRM_Dept_Model GetDetailsById(int id)
        {
            CRM_Dept_Model model = new CRM_Dept_Model();
            if (this.IsExist(id))
            {
                IQueryable<CRM_Dept> queryData = Rep.GetList(db).Where(t => t.DeptID == id);
                model = this.CreateModelList(ref queryData)[0];
            }
            return model;
        }

        /// <summary>
        /// 实体映射
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        private List<CRM_Dept_Model> CreateModelList(ref IQueryable<CRM_Dept> queryData)
        {
            List<CRM_Dept_Model> list = (from r in queryData
                                                         select new CRM_Dept_Model
                                                         {
                                                            DeptID=r.DeptID,
                                                            DeptName=r.DeptName,
                                                            ParentID=r.ParentID,
                                                            Des=r.Des,
                                                            IsEnable=r.IsEnable,
                                                            IsDelete=r.IsDelete
                                                         }).ToList<CRM_Dept_Model>();
            return list;

        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsExist(int id)
        {
            return this.Rep.GetById(id) != null;
        }


        #region 根据父级ID获取列表
        public List<CRM_Dept_Model> GetList_(int parentId)
        {
            return GetAll().Where(t => t.IsDelete == false && t.ParentID == parentId).OrderBy(t => t.DeptID).ToList();
        }
        #endregion

        /// <summary>
        /// 根据部门父ID递归出下级部门
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<CRM_Dept_Model> GetAllByParentId(int parentId)
        {
            List<CRM_Dept_Model> depts = GetList_(parentId);

            if (depts != null)
            {
                depts.ForEach(a =>
                {
                    depts.AddRange(GetAllByParentId(a.DeptID));
                });
            }

            return depts;
        }

        /// <summary>
        /// 根据父级Id获取列表
        /// </summary>
        /// <param name="parentId">父级Id</param>          
        /// <returns>模型列表</returns>
        public List<CRM_Dept_Model> GetListByParentId(int parentId)
        {
            IQueryable<CRM_Dept> queryData = Rep.GetList(db);
            queryData = queryData.Where(a => a.ParentID == parentId && a.IsEnable == true && a.IsDelete == false);

            List<CRM_Dept_Model> modelList = (from r in queryData
                                                         join sr in
                                                         (from ss in db.CRM_Dept.Where(a => a.IsEnable == true && a.IsDelete == false)
                                                              group ss by ss.ParentID into g
                                                              select new
                                                              {
                                                                  ParentId = g.Select(p => p.ParentID).FirstOrDefault(),
                                                                  nums = g.Count()
                                                              }
                                                             )
                                                         on r.DeptID equals sr.ParentId into x
                                                         from dN in x.DefaultIfEmpty()
                                                         select new CRM_Dept_Model
                                                         {
                                                             DeptID = r.DeptID,
                                                             DeptName = r.DeptName,
                                                             Des = r.Des,
                                                             IsEnable = (bool)r.IsEnable,
                                                             ParentID = (int)r.ParentID,
                                                             IsDelete = (bool)r.IsDelete
                                                         }).ToList();
            return modelList;
        }


        /// <summary>
        /// 删除所有
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteAll(List<int> id)
        {
            try
            {
                return (this.Rep.DeleteAll(id) == 1);
            }
            catch
            {
                return false;
            }
        }
    }
}


