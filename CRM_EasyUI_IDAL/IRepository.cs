using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GroupThink.CRM.Model;

namespace GroupThink.CRM.IDAL
{
    /// <summary>
    /// 数据仓库接口
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    /// <typeparam name="IdType">主键类型</typeparam>
    public interface IRepository<T, IdType>
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Create(T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete(IdType id);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Edit(T entity);

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        IQueryable<T> GetList(DBContainer db);

        /// <summary>
        /// 根据Id获得实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(IdType id);

    }
}
