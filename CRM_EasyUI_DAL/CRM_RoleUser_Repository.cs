using System;
using System.Linq;
using GroupThink.CRM.IDAL;
using GroupThink.CRM.Model;
using System.Data;
using GroupThink.CRM.Model.Sys;
using System.Collections.Generic;

namespace GroupThink.CRM.DAL
{
    public class CRM_RoleUser_Repository : ICRM_RoleUser_Repository, IDisposable
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <returns>数据列表</returns>
        public IQueryable<CRM_RoleUser> GetList(DBContainer db)
        {
            IQueryable<CRM_RoleUser> list = db.CRM_RoleUser.AsQueryable();
            return list;
        }

        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">实体</param>
        public int Create(CRM_RoleUser entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.CRM_RoleUser.AddObject(entity);
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">主键id</param>
        public int Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                CRM_RoleUser entity = db.CRM_RoleUser.SingleOrDefault(a => a.UserId == id);
                if (entity != null)
                {
                    db.CRM_RoleUser.DeleteObject(entity);
                }
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">实体</param>
        public int Edit(CRM_RoleUser entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.CRM_RoleUser.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }
 
        /// <summary>
        /// 获得一个实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>实体</returns>
        public CRM_RoleUser GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.CRM_RoleUser.SingleOrDefault(a => a.UserId == id);
            }
        }


        /// <summary>
        /// 判断一个实体是否存在
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>是否存在 true or false</returns>
        public bool IsExist(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                CRM_RoleUser entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }

        public int Delete(int roleId, string userId)
        {
            using (DBContainer container = new DBContainer())
            {
                CRM_RoleUser entity = container.CRM_RoleUser.SingleOrDefault<CRM_RoleUser>(t => t.RoleId == roleId && t.UserId == userId);
                if (entity != null)
                {
                    container.CRM_RoleUser.DeleteObject(entity);
                }
                return container.SaveChanges();
            }
        }


        public void Dispose()
        {

        }
    }
}

