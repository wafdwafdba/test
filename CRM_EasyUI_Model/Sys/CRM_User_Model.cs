using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace GroupThink.CRM.Model.Sys
{
    public class CRM_User_Model
    {
        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "用户名")]
        public string ID { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "姓名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "密码")]
        public string PassWord { get; set; }

        [Display(Name = "性别")]
        public int? Sex { get; set; }

        [Display(Name = "电话")]
        public string Tel { get; set; }

        [Display(Name = "角色")]
        public int? RoleID { get; set; }

        [Display(Name = "用户状态")]
        public int? UserState { get; set; }

        [Display(Name = "用户类型")]
        public int? UserType { get; set; }

        [Display(Name = "创建人")]
        public string CreateUserID { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }

        [Display(Name = "是否选择")]
        public bool IsSelect { get; set; }

        //用户类型名称
        public string UserTypeName { get; set; }

        //用户状态名称
        public string UserStateName { get; set; }

        /// <summary>
        /// 部门角色
        /// </summary>
        public string DeptRole { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [Display(Name = "部门")]
        public int? deptID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string roleName { get; set; }
    }
}

