using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GroupThink.CRM.Common;

namespace GroupThink.CRM.IBLL
{
    /// <summary>
    /// BLL接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="IdType"></typeparam>
    public interface I_BLL<T, IdType>
    {

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Create(ref ValidationErrors errors, T model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(IdType id);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Edit(T model);

        /// <summary>
        /// 根据Id获得实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(IdType id);

        /// <summary>
        /// 根据Id获得实体详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetDetailsById(IdType id);

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        List<T> GetList(ref GridPager pager);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsExist(IdType id);
    }
}
