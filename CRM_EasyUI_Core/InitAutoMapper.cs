using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GroupThink.CRM.Model;
using GroupThink.CRM.Model.Sys;
using AutoMapper;

namespace GroupThink.CRM.Core
{
    /// <summary>
    /// AutoMapper映射
    /// </summary>
    public class InitAutoMapper
    {
        /// <summary>
        /// 初始化映射对象
        /// </summary>
        public static void Init()
        {
            //用户表
            Mapper.CreateMap<CRM_User, CRM_User_Model>();
            Mapper.CreateMap<CRM_User_Model, CRM_User>();

            //部门表
            Mapper.CreateMap<CRM_Dept, CRM_Dept_Model>();
            Mapper.CreateMap<CRM_Dept_Model, CRM_Dept>();

            //模块表
            Mapper.CreateMap<CRM_Module, CRM_Module_Model>();
            Mapper.CreateMap<CRM_Module_Model, CRM_Module>();

            //模块操作表
            Mapper.CreateMap<CRM_ModuleOperate, CRM_ModuleOperate_Model>();
            Mapper.CreateMap<CRM_ModuleOperate_Model, CRM_ModuleOperate>();

            //角色表
            Mapper.CreateMap<CRM_Role, CRM_Role_Model>();
            Mapper.CreateMap<CRM_Role_Model, CRM_Role>();

            //角色用户表
            Mapper.CreateMap<CRM_RoleUser, CRM_RoleUser_Model>();
            Mapper.CreateMap<CRM_RoleUser_Model, CRM_RoleUser>();

            //right表
            Mapper.CreateMap<CRM_Right, CRM_Right_Model>();
            Mapper.CreateMap<CRM_Right_Model, CRM_Right>();

            //right操作表
            Mapper.CreateMap<CRM_RightOperate, CRM_RightOperate_Model>();
            Mapper.CreateMap<CRM_RightOperate_Model, CRM_RightOperate>();

            //客户表
            Mapper.CreateMap<CRM_Customer, CRM_Customer_Model>();
            Mapper.CreateMap<CRM_Customer_Model, CRM_Customer>();

            //客户联系人表
            Mapper.CreateMap<CRM_CustomerConnect, CRM_CustomerConnect_Model>();
            Mapper.CreateMap<CRM_CustomerConnect_Model, CRM_CustomerConnect>();

            //数据字典表
            Mapper.CreateMap<CRM_Type, CRM_Type_Model>();
            Mapper.CreateMap<CRM_Type_Model, CRM_Type>();
        }

    }
}
