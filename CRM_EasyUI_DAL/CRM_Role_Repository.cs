using System;
using System.Linq;
using GroupThink.CRM.IDAL;
using GroupThink.CRM.Model;
using System.Data;
using GroupThink.CRM.Model.Sys;
using System.Collections.Generic;

namespace GroupThink.CRM.DAL
{
    public class CRM_Role_Repository : ICRM_Role_Repository, IDisposable
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <returns>数据列表</returns>
        public IQueryable<CRM_Role> GetList(DBContainer db)
        {
            IQueryable<CRM_Role> list = db.CRM_Role.AsQueryable();
            return list;
        }

        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">实体</param>
        public int Create(CRM_Role entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.CRM_Role.AddObject(entity);
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
                CRM_Role entity = db.CRM_Role.SingleOrDefault(a => a.ID == id);
                if (entity != null)
                {
                    db.CRM_Role.DeleteObject(entity);
                }
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">实体</param>
        public int Edit(CRM_Role entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.CRM_Role.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }
 
        /// <summary>
        /// 获得一个实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>实体</returns>
        public CRM_Role GetById(int id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.CRM_Role.SingleOrDefault(a => a.ID == id);
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
                CRM_Role entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }

        public void Dispose()
        {

        }

       
    }
}

