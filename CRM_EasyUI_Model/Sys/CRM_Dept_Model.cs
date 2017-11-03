using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace GroupThink.CRM.Model.Sys
{
    public class CRM_Dept_Model
    {
        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "部门ID")]
        public int DeptID { get; set; }

        [Display(Name = "部门名称")]
        [Required(ErrorMessage = "{0}必须填写")]
        public string DeptName { get; set; }

        [Display(Name = "父类ID")]
        public int? ParentID { get; set; }

        [Display(Name = "描述")]
        public string Des { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "是否启用")]
        public bool IsEnable { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "是否删除")]
        public bool IsDelete { get; set; }

        public string state { get; set; }

        [Display(Name = "层级")]
        public int Level { get; set; }
    }
}

