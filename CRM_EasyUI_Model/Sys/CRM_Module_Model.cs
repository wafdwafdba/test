using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace GroupThink.CRM.Model.Sys
{
    public class CRM_Module_Model
    {
        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "ID")]
        public string ID { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "父类ID")]
        public string ParentID { get; set; }

        [Display(Name = "链接")]
        public string Url { get; set; }

        [Display(Name = "排序号")]
        public int? Sort { get; set; }

        [Display(Name = "创建人")]
        public string CreateUserID { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }

        [Display(Name = "是否最后一项")]
        public bool IsLast { get; set; }

        [Display(Name = "状态")]
        public bool Enable { get; set; }

        public string state { get; set; }
    }
}

