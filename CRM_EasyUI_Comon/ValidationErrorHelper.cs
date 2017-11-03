using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupThink.CRM.Common
{
    public class ValidationError
    {
        public ValidationError() { }
        public string ErrorMessage { get; set; }
    }

    public class ValidationErrors : List<ValidationError>
    {
        /// <summary>
        /// 添加错误
        /// </summary>
        /// <param name="errorMessage">信息描述</param>
        public void Add(string errorMessage)
        {
            base.Add(new ValidationError { ErrorMessage = errorMessage });
        }

        public string Error { 
            get{
                string error = "";
                this.All(a =>
                {
                    error += a.ErrorMessage;
                    return true;
                });
                return error;
            }
        }
    }
}
