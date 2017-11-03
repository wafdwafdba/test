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

    public class CRM_User_BLL : ICRM_User_BLL
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
        public ICRM_User_Repository Rep { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        public ICRM_Role_BLL r_BLL { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        public ICRM_Dept_BLL d_BLL { get; set; }
        #endregion

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors errors, CRM_User_Model model)
        {
            try
            {
                if (this.IsExist(model.ID))
                {
                    errors.Add("主键重复");
                    return false;
                }
                CRM_User entity = new CRM_User();
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
        public bool Edit(CRM_User_Model model)
        {
            try
            {
                if (!IsExist(model.ID))
                {
                    return false;
                }
                CRM_User entity = new CRM_User();
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
        public List<CRM_User_Model> GetList(ref GridPager pager)
        {
            IQueryable<CRM_User> source = null;
            source = this.Rep.GetList(this.db);
            pager.totalRows = source.Count<CRM_User>();
            source = LinqHelper.SortingAndPaging<CRM_User>(source, pager.sort, pager.order, pager.page, pager.rows);
            return this.CreateModelList(ref source);
        }

        /// <summary>
        /// 所有
        /// </summary>
        /// <returns></returns>
        public List<CRM_User_Model> GetAll()
        {
            IQueryable<CRM_User> source = this.Rep.GetList(this.db);
            return this.CreateModelList(ref source);
        }


        /// <summary>
        /// 根据Id获得实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CRM_User_Model GetById(string id)
        {
            CRM_User_Model model = new CRM_User_Model();
            if (this.IsExist(id))
            {
                CRM_User entity = this.Rep.GetById(id);
                Mapper.Map(entity, model);
            }
            return model;
        }

        /// <summary>
        /// 根据Id获得实体详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CRM_User_Model GetDetailsById(string id)
        {
            CRM_User_Model model = new CRM_User_Model();
            if (this.IsExist(id))
            {
                IQueryable<CRM_User> queryData = Rep.GetList(db).Where(t => t.ID == id);
                model = this.CreateModelList(ref queryData)[0];
            }
            return model;
        }

        /// <summary>
        /// 实体映射
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        private List<CRM_User_Model> CreateModelList(ref IQueryable<CRM_User> queryData)
        {
            List<CRM_User_Model> list = (from r in queryData
                                         join t2 in db.V_DeptUser on r.ID equals t2.ID
                                                         select new CRM_User_Model
                                                         {
                                                            ID=r.ID,
                                                            UserName=r.UserName,
                                                            PassWord=r.PassWord,
                                                            Sex=r.Sex,
                                                            Tel=r.Tel,
                                                            RoleID=r.RoleID,
                                                            UserState=r.UserState,
                                                            UserType=r.UserType,
                                                            CreateUserID=r.CreateUserID,
                                                            CreateTime=r.CreateTime,
                                                            deptID=t2.DeptId,
                                                            roleName=t2.RoleName
                                                         }).ToList<CRM_User_Model>();
            list.ForEach(t=>{
                if (r_BLL.GetP_Dept__Result().FirstOrDefault(m => m.DeptID == t.deptID) != null)
                {
                    string deptName = r_BLL.GetP_Dept__Result().FirstOrDefault(m => m.DeptID == t.deptID).DeptName;
                    t.DeptName = deptName;
                    t.DeptRole = deptName + "-" + t.roleName;
                }
                if (t.UserType >= -1)
                {
                    t.UserTypeName = Enum.GetName(typeof(UserTypeEnum), t.UserType);
                }
                else
                {
                    t.UserType = -1;
                }
            });
            
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

        #region 登录
        public CRM_User Login(string username, string pwd)
        {
            return Rep.Login(username, pwd);
        }
        #endregion

        #region 根据用户获取列表
        public List<CRM_User_Model> GetListQuery(ref GridPager pager, string userName)
        {
            IQueryable<CRM_User> queryData = Rep.GetList(db);
            if (!string.IsNullOrWhiteSpace(userName))
            {
                queryData = Rep.GetList(db).Where(a => a.UserName.Contains(userName));
            }

            //过滤已离职的
            queryData = queryData.Where(t => t.UserState != 4);

            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
        #endregion

        #region 用户列表，以及用户查询条件
        public List<CRM_User_Model> GetList(ref GridPager pager, List<string> users, string UserName, int DeptId, int RoleId, int UserType, int UserState)
        {

            IQueryable<CRM_User> queryData = Rep.GetList(db).Where(t=>users.Contains(t.CreateUserID));
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                queryData = Rep.GetList(db).Where(a => a.ID.Contains(UserName));
            }

            if (DeptId != -1)
            {
                queryData = (from t1 in Rep.GetList(db)
                                 join t2 in db.V_DeptUser on t1.ID equals t2.ID
                                 where t2.DeptId == DeptId
                                 select t1);
            }

            if (RoleId != -1)
            {
                queryData = Rep.GetList(db).Where(a => a.RoleID==RoleId);
            }

            if (UserType != -1)
            {
                queryData = Rep.GetList(db).Where(a => a.UserType==UserType);
            }

            if (UserState != -1)
            {
                queryData = Rep.GetList(db).Where(a => a.UserState== UserState);
            }

            //过滤已离职的
            queryData = queryData.Where(t => t.UserState != 4);

            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
        #endregion


        /// <summary>
        /// 根据部门Id得到用户列表（递归）
        /// </summary>
        /// <param name="DeptId"></param>
        /// <returns></returns>
        public List<CRM_User_Model> GetAllByDeptId(int DeptId)
        {
            List<CRM_User_Model> ulist = new List<CRM_User_Model>();
            if (DeptId > 0)
            {
                (
                         from a in db.V_DeptUser
                         where a.DeptId == DeptId
                         select a
                    ).Distinct().ToList().ForEach(a =>
                    {
                        ulist.Add(new CRM_User_Model() { ID = a.ID, UserName = a.UserName });
                    });


                //下级部门用户
                List<CRM_Dept_Model> depts = d_BLL.GetList_(DeptId);
                depts.ForEach(b =>
                {
                    ulist.AddRange(GetAllByDeptId(b.DeptID));
                });
            }
            return ulist;
        }
    }

}


