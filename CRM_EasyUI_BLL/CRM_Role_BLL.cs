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

    public class CRM_Role_BLL : ICRM_Role_BLL
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
        public ICRM_Role_Repository Rep { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        public ICRM_Dept_BLL Dept_Bll { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        public ICRM_Dept_Repository d_Rep { get; set; }
        #endregion

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors errors, CRM_Role_Model model)
        {
            try
            {
                if (this.IsExist(model.ID))
                {
                    errors.Add("主键重复");
                    return false;
                }
                CRM_Role entity = new CRM_Role();
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
        public bool Edit(CRM_Role_Model model)
        {
            try
            {
                if (!IsExist(model.ID))
                {
                    return false;
                }
                CRM_Role entity = new CRM_Role();
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
        public List<CRM_Role_Model> GetList(ref GridPager pager)
        {
            IQueryable<CRM_Role> source = null;
            source = this.Rep.GetList(this.db);
            pager.totalRows = source.Count<CRM_Role>();
            source = LinqHelper.SortingAndPaging<CRM_Role>(source, pager.sort, pager.order, pager.page, pager.rows);
            return this.CreateModelList(ref source);
        }

        /// <summary>
        /// 所有
        /// </summary>
        /// <returns></returns>
        public List<CRM_Role_Model> GetAll()
        {
            IQueryable<CRM_Role> source = this.Rep.GetList(this.db);
            return this.CreateModelList(ref source);
        }


        /// <summary>
        /// 根据Id获得实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CRM_Role_Model GetById(int id)
        {
            CRM_Role_Model model = new CRM_Role_Model();
            if (this.IsExist(id))
            {
                CRM_Role entity = this.Rep.GetById(id);
                Mapper.Map(entity, model);
            }
            return model;
        }

        /// <summary>
        /// 根据Id获得实体详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CRM_Role_Model GetDetailsById(int id)
        {
            CRM_Role_Model model = new CRM_Role_Model();
            if (this.IsExist(id))
            {
                IQueryable<CRM_Role> queryData = Rep.GetList(db).Where(t => t.ID == id);
                model = this.CreateModelList(ref queryData)[0];
            }
            return model;
        }

        /// <summary>
        /// 实体映射
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        private List<CRM_Role_Model> CreateModelList(ref IQueryable<CRM_Role> queryData)
        {
             List<P_Dept__Result> list = GetP_Dept__Result();
            List<CRM_Role_Model> modelList = (from m in
                                                 (from r in queryData
                                                         select new CRM_Role_Model
                                                         {
                                                            ID=r.ID,
                                                            Name=r.Name,
                                                            Des=r.Des,
                                                            DeptId=r.DeptId,
                                                            Enable=r.Enable,
                                                            ParentId=r.ParentId,
                                                            CopyRoleID=r.CopyRoleID,
                                                            Level=r.Level,

                                                         }).ToList()
                                                          join n in list
                                                         on m.DeptId equals n.DeptID
                                                              select new CRM_Role_Model
                                                         {
                                                             ID = m.ID,
                                                             Name = m.Name,
                                                             Des = m.Des,
                                                             DeptId = m.DeptId,
                                                             ParentId = m.ParentId,
                                                             Level = m.Level,
                                                             DeptName = n.Dept,
                                                             Enable = m.Enable
                                                         }).ToList();
            return modelList;

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

        public List<CRM_Role_Model> GetList_(int parentId)
        {
             List<P_Dept__Result> list = GetP_Dept__Result();
            List<CRM_Role_Model> modelList = (from m in GetAll().Where(a => a.Enable == true && a.ParentId == parentId)
                                                         join n in list
                                                         on m.DeptId equals n.DeptID
                                                         select new CRM_Role_Model
                                                         {
                                                             ID = m.ID,
                                                             Name = m.Name,
                                                             Des = m.Des,
                                                             DeptId = m.DeptId,
                                                             ParentId = m.ParentId,
                                                             DeptName = n.Dept,
                                                             Enable = m.Enable
                                                         }).OrderBy(t => t.ID).ToList();

            return modelList;
        }

        public List<P_Dept__Result> GetP_Dept__Result()
        {
            List<P_Dept__Result> list = DataCache.GetCache("P_Dept__Result") as List<P_Dept__Result>;
            if (list == null)
            {
                list = d_Rep.GetListFullName(db);
                DataCache.SetCache("P_Dept__Result", list, DateTime.MaxValue, TimeSpan.FromMinutes(2));
            }
            return list;
        }

        ///// <summary>
        ///// 获取所有的部门信息
        ///// </summary>
        ///// <param name="parentId"></param>
        ///// <returns></returns>
        //public List<CRM_Dept_Model> GetAllDept()
        //{
        //    List<CRM_Dept_Model> depts = Dept_Bll.GetAll();

        //    //if (depts != null)
        //    //{
        //    //    depts.ForEach(a =>
        //    //    {
        //    //        depts.AddRange(Dept_Bll.GetAllByParentId(a.DeptID));
        //    //    });
        //    //}

        //    return depts;
        //}

        public List<CRM_Role_Model> GetList_(int deptID, int parentID)
        {
             List<P_Dept__Result> list = GetP_Dept__Result();
            List<CRM_Role_Model> modelList = (from m in GetAll().Where(a => a.ParentId == parentID && a.DeptId == deptID)
                                                         join n in list
                                                         on m.DeptId equals n.DeptID
                                                          select new CRM_Role_Model
                                                         {
                                                             ID = m.ID,
                                                             Name = m.Name,
                                                             Des = m.Des,
                                                             DeptId = m.DeptId,
                                                             ParentId = m.ParentId,
                                                             DeptName = n.Dept,
                                                             Enable = m.Enable
                                                         }).ToList();

            return modelList;
        }

        public List<CRM_Role_Model> GetList1(int deptID)
        {
             List<P_Dept__Result> list = GetP_Dept__Result();
            List<CRM_Role_Model> modelList = (from m in GetAll().Where(a => a.DeptId == deptID)
                                                         join n in list
                                                         on m.DeptId equals n.DeptID
                                                          select new CRM_Role_Model
                                                         {
                                                             ID = m.ID,
                                                             Name = m.Name,
                                                             Des = m.Des,
                                                             DeptId = m.DeptId,
                                                             ParentId = m.ParentId,
                                                             DeptName = n.DeptName,
                                                             Enable = m.Enable
                                                         }).ToList();

            return modelList;
        }
        //获取部门下的角色
        public List<CRM_Role_Model> GetListByDeptId(int id)
        {
            List<CRM_Role_Model> modelList = (from r in GetAll().Where(t => t.Enable = true && t.DeptId == id)
                                                    select new CRM_Role_Model
                                                         {
                                                             ID = r.ID,
                                                             Name = r.Name,
                                                             Level = (int)r.Level

                                                         }).ToList();
            return modelList;
        }

    }
}


