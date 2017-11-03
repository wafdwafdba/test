using System;
using System.Linq;
using GroupThink.CRM.IDAL;
using GroupThink.CRM.Model;
using System.Data;
using GroupThink.CRM.Model.Sys;
using System.Collections.Generic;
using EntityFramework.Extended;

namespace GroupThink.CRM.DAL
{
    public class CRM_Dept_Repository : ICRM_Dept_Repository, IDisposable
    {
        public List<P_Dept_Result> GetList1(DBContainer db)
        {
            //IQueryable<CRM_Dept> list = db.CRM_Dept.AsQueryable();
            List<P_Dept_Result> list = db.P_Dept().Where(t => t.IsDelete == false).ToList();
            return list;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <returns>数据列表</returns>
        public IQueryable<CRM_Dept> GetList(DBContainer db)
        {
            IQueryable<CRM_Dept> list = db.CRM_Dept.AsQueryable();
            return list;
        }

        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">实体</param>
        public int Create(CRM_Dept entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.CRM_Dept.AddObject(entity);
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">主键id</param>
        public int Delete(int id)
        {
            using (DBContainer db = new DBContainer())
            {
                    try
                    {
                        CRM_Dept entity = db.CRM_Dept.SingleOrDefault(a => a.DeptID == id);
                        //查询所有的ID

                        if (db.CRM_Role.Where(a => a.DeptId == entity.DeptID).Count() > 0)
                        {
                            List<CRM_Role> list = (from r in db.CRM_Role.Where(a => a.DeptId == entity.DeptID)
                                                   select new CRM_Role
                                                   {
                                                       ID = r.ID
                                                   }
                                                       ).ToList();
                            //将ID存入一个int数组中
                            var roleid = new List<int>();
                            list.ForEach(r =>
                            {
                                roleid.Add(r.ID);
                            });
                            //根据ID删除数据
                            db.CRM_Role.Delete(t => roleid.Contains(t.ID));
                        }
                        db.CRM_Dept.Update(
                            t => t.DeptID == id,
                            t => new CRM_Dept { IsDelete = true });
                        db.SaveChanges();
                        //tran.Commit();
                        return 1;
                    }
                    catch
                    {
                  
                        return 0;
                    }
            }
        }

        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">实体</param>
        public int Edit(CRM_Dept entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.CRM_Dept.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }
 
        /// <summary>
        /// 获得一个实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>实体</returns>
        public CRM_Dept GetById(int id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.CRM_Dept.SingleOrDefault(a => a.DeptID == id);
            }
        }


        /// <summary>
        /// 判断一个实体是否存在
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>是否存在 true or false</returns>
        public bool IsExist(int id)
        {
            using (DBContainer db = new DBContainer())
            {
                CRM_Dept entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }

        public void Dispose()
        {

        }

        public List<P_Dept__Result> GetListFullName(DBContainer db)
        {
            List<P_Dept__Result> list = db.P_Dept_().Where(t => t.IsDelete == false).ToList();
            return list;
        }


        /// <summary>
        /// 根据IDs删除所有相关的
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">主键id</param>
        public int DeleteAll(List<int> ids)
        {
            using (DBContainer db = new DBContainer())
            {
                try
                {
                    //将ID存入一个int数组中
                    var roleid = new List<int>();
                    ids.ForEach(d =>
                    {
                        //查询所有的ID
                        CRM_Dept entity = db.CRM_Dept.SingleOrDefault(a => a.DeptID == d);
                        if (db.CRM_Role.Where(a => a.DeptId == entity.DeptID).Count() > 0)
                        {
                            List<CRM_Role> list = (from r in db.CRM_Role.Where(a => a.DeptId == entity.DeptID)
                                                   select new CRM_Role
                                                   {
                                                       ID = r.ID
                                                   }
                                                       ).ToList();

                            list.ForEach(r =>
                            {
                                roleid.Add(r.ID);
                            });

                        }
                    });
                    //根据ID删除数据
                    if (roleid.Count() > 0)
                    {
                        db.CRM_Role.Delete(t => roleid.Contains(t.ID));
                    }
                    db.CRM_Dept.Update(
                        t => ids.Contains(t.DeptID),
                        t => new CRM_Dept { IsDelete = true });
                    db.SaveChanges();
                    //tran.Commit();
                    return 1;
                }
                catch
                {

                    return 0;
                }
            }
        }
    }
}

