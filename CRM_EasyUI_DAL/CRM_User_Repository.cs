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
    public class CRM_User_Repository : ICRM_User_Repository, IDisposable
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <returns>数据列表</returns>
        public IQueryable<CRM_User> GetList(DBContainer db)
        {
            IQueryable<CRM_User> list = db.CRM_User.AsQueryable();
            return list;
        }

        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">实体</param>
        public int Create(CRM_User entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.CRM_User.AddObject(entity);
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
               // CRM_User entity = db.CRM_User.SingleOrDefault(a => a.ID == id);
                //if (entity != null)
                //{
                   // db.CRM_User.DeleteObject(entity);

                    db.CRM_User.Delete(t => t.ID == id);
                //}
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">实体</param>
        public int Edit(CRM_User entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.CRM_User.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
               // db.CRM_User.Update(
               //t => t.ID == entity.ID,
               //t => new CRM_User { UserName = "wangwu" });
                return db.SaveChanges();
            }
        }
 
        /// <summary>
        /// 获得一个实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>实体</returns>
        public CRM_User GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.CRM_User.SingleOrDefault(a => a.ID == id);
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
                CRM_User entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }

        public void Dispose()
        {

        }

        ///登录
        public CRM_User Login(string username, string pwd)
        {
            using (DBContainer db = new DBContainer())
            {
                CRM_User user = db.CRM_User.SingleOrDefault(t=>t.ID==username&&t.PassWord==pwd);
                return user;
            }
        }
    }
}

