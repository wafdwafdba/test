using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace GroupThink.CRM.Model.Sys
{
    public class CRM_Type_Model
    {
        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "字典ID")]
        public int ID { get; set; }

        [Display(Name = "类型名称")]
        public string TypeName { get; set; }

        [Display(Name = "父类型ID")]
        public int? TypeID { get; set; }

        [Display(Name = "描述")]
        public string Des { get; set; }

        [Display(Name = "排序号")]
        public int? Sort { get; set; }

        [Display(Name = "是否启用")]
        public bool? Enable { get; set; }

        [Display(Name = "创建人ID")]
        public string CreateUserID { get; set; }

        [Display(Name = "创建时间")]
        public string CreateTime { get; set; }

        [Display(Name = "是否显示")]
        public bool? isShow { get; set; }


    }
}

