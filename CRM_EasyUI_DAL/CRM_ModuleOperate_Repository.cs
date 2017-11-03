using System;
using System.Linq;
using GroupThink.CRM.IDAL;
using GroupThink.CRM.Model;
using System.Data;
using GroupThink.CRM.Model.Sys;
using System.Collections.Generic;

namespace GroupThink.CRM.DAL
{
    public class CRM_ModuleOperate_Repository : ICRM_ModuleOperate_Repository, IDisposable
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <returns>数据列表</returns>
        public IQueryable<CRM_ModuleOperate> GetList(DBContainer db)
        {
            IQueryable<CRM_ModuleOperate> list = db.CRM_ModuleOperate.AsQueryable();
            return list;
        }

        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">实体</param>
        public int Create(CRM_ModuleOperate entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.CRM_ModuleOperate.AddObject(entity);
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
                CRM_ModuleOperate entity = db.CRM_ModuleOperate.SingleOrDefault(a => a.ID == id);
                if (entity != null)
                {
                    db.CRM_ModuleOperate.DeleteObject(entity);
                }
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">实体</param>
        public int Edit(CRM_ModuleOperate entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.CRM_ModuleOperate.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }
 
        /// <summary>
        /// 获得一个实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>实体</returns>
        public CRM_ModuleOperate GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.CRM_ModuleOperate.SingleOrDefault(a => a.ID == id);
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
                CRM_ModuleOperate entity = GetById(id);
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

