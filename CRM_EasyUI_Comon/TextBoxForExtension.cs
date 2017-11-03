
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Web.Routing;
namespace System.Web.Mvc.Html
{
    public static class TextBoxForExtension
    {
        public static MvcHtmlString BsTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
             Expression<Func<TModel, TProperty>> expression, string css = "", string placeholder = "", string Readonly = "", string value = "")
        {
            TagBuilder tagBuilder = new TagBuilder("input");
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            //tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(ExpressionHelper.GetExpressionText(expression), metadata));
            string name = ExpressionHelper.GetExpressionText(expression);
            //  var vas = htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata);
            htmlHelper.ValidateFor(expression);
            tagBuilder.MergeAttribute("name", name);
            if (!string.IsNullOrEmpty(placeholder))
                tagBuilder.MergeAttribute("placeholder", placeholder);
            tagBuilder.GenerateId(name);
            tagBuilder.MergeAttribute("type", "text");//核心代码，直接通过这个方法可以获取属性上的验证信息，如：“data-val= ....”。有一点要注意，在一个属性上，调用了这个方法完成后。mvc底层代码会自动释放这个验证对象。也就是说，一个属性的输入文本框只会第一个上边会生成相关的验证。 var vas = htmlHelper.GetUnobtrusiveValidationAttributes(name,metadata); if (!string.IsNullOrEmpty(placeholder)) { tagBuilder.MergeAttribute("placeholder",placeholder); }　　　　　　　//string类型，看有没长度限制，如果有，增加maxlength，minlength
            if (!string.IsNullOrEmpty(value))
                tagBuilder.MergeAttribute("value", value.Trim());
            if (!string.IsNullOrEmpty(Readonly) && Readonly == "Y")
                tagBuilder.MergeAttribute("readonly", Readonly);
            tagBuilder.AddCssClass("form-control");
            if (!string.IsNullOrEmpty(css))
            {
                tagBuilder.AddCssClass(css);
            }　　　　　　　//约定，属性名以day或者date结束的属性为日体，为其增加日期选择功能。
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }
            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));
          return new MvcHtmlString(tagBuilder.ToString());
        }

        public static MvcHtmlString BsTextBox(this HtmlHelper htmlHelper, string name = "",
            string css = "", string placeholder = "", string Readonly = "", string value = "")
        {
            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", "text");
            tagBuilder.MergeAttribute("name", name, true);

            tagBuilder.GenerateId(name);
            if (!string.IsNullOrEmpty(placeholder))
                tagBuilder.MergeAttribute("placeholder", placeholder);
            if (!string.IsNullOrEmpty(value))
                tagBuilder.MergeAttribute("value", value.Trim());
            if (!string.IsNullOrEmpty(Readonly) && Readonly == "Y")
                tagBuilder.MergeAttribute("readonly", Readonly);
            tagBuilder.AddCssClass("form-control");
            if (!string.IsNullOrEmpty(css))
            {
                tagBuilder.AddCssClass(css);
            }　　　　　　　//约定，属性名以day或者date结束的属性为日体，为其增加日期选择功能。
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }
            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, null));
            return new MvcHtmlString(tagBuilder.ToString());
        }
        /// <summary>
        /// 针对开窗的控件
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="placeholder"></param>
        /// <param name="icon"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="Readonly"></param>
        /// <param name="value"></param>
        /// <param name="isvalid"></param>
        /// <returns></returns>
        public static MvcHtmlString BsTextBoxIcon(this HtmlHelper htmlHelper, string name = "",
        string icon = "", string placeholder = "", string type="s",string width="140",string height="28", string Readonly = "",string isvalid="")
        {
            string widthText = (int.Parse(width) - 18).ToString() + "px";
            string heightText = height + "px";
            width = width + "px";
            StringBuilder strButton = new StringBuilder();
            strButton.AppendFormat("<div class=\"serach-text\" style=\"width:{0};height:{1}\">", width, heightText);
             strButton.AppendFormat("<input type='text' id='{0}' class='text-css' name='{1}' style='width:{2};height:{3};' placeholder='{4}'", name, name, widthText, heightText, placeholder);
            if(isvalid=="Y")
                strButton.AppendFormat("data-val-required='{0}' data-val='true'",placeholder);
            if (Readonly=="Y")
                strButton.AppendFormat("readonly='readonly'");
            strButton.Append("/>");
            strButton.AppendFormat(" <span>");
            if(type=="s")
            strButton.AppendFormat(" <span id='btn{0}' class='icon {1} iconcss'></span></span></div>", name,icon);
            else
                strButton.AppendFormat(" <span id='btn{0}' class='{1}'></span></span></div>", name, icon);
            return new MvcHtmlString(strButton.ToString());
         
        }
    }
    public static class LabelExtensions
    {
        public static MvcHtmlString BsLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText)
        {
            string resolvedLabelText = labelText;
            if (String.IsNullOrEmpty(resolvedLabelText))
            {
                return MvcHtmlString.Empty;
            }
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            TagBuilder tag = new TagBuilder("label");
            tag.AddCssClass("control-label");
            tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
            tag.SetInnerText(resolvedLabelText);
            //tag.MergeAttributes(htmlAttributes, replaceExisting: true);
            return new MvcHtmlString(tag.ToString());
        }
        public static MvcHtmlString BsLabel(this HtmlHelper html, string labelText)
        {
            string resolvedLabelText = labelText;
            if (String.IsNullOrEmpty(resolvedLabelText))
            {
                return MvcHtmlString.Empty;
            }
            TagBuilder tag = new TagBuilder("label");
            tag.AddCssClass("control-label");
            tag.SetInnerText(resolvedLabelText);
            //tag.MergeAttributes(htmlAttributes, replaceExisting: true);
            return new MvcHtmlString(tag.ToString());
        }
    }

    public static class ButtonExtensions
    {
        /// <summary>
        /// 主要是一些功能按钮比如 新增 修改，导出，删除之类的
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">button的ID</param>
        /// <param name="value">button显示的中文</param>
        /// <param name="cssClass">这个是按钮的样式</param>
        /// <param name="icon">字体图标的样式</param>
        /// <returns></returns>
        public static MvcHtmlString BsButtonO(this HtmlHelper htmlHelper, string name, string value, string cssClass, string icon)
        {

            StringBuilder strButton = new StringBuilder();
            strButton.AppendFormat("<button  class='btn {0}'", cssClass);
            strButton.AppendFormat(" id='{0}'", name);
            strButton.AppendFormat(" style='width: auto; height: 28px; padding:2px 8px; margin:2px;'>");
            strButton.AppendFormat(" <span class='icon {0}' style='font-size:14px;'><lable style='margin-left:2px;'>{1}</lable></span>", icon, value);
            strButton.Append("</button>");
            return new MvcHtmlString(strButton.ToString());
        }
        public static MvcHtmlString BsButtonT(this HtmlHelper htmlHelper, string inputType, string name, string value, string cssClass = "", string htmlAttributes = "")
        {
            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", inputType);
            tagBuilder.MergeAttribute("name", name, true);
            tagBuilder.MergeAttribute("value", value);
            tagBuilder.MergeAttribute("style", "width:80px;height:35px;" + htmlAttributes);
            //if (htmlAttributes != null)
            //    tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tagBuilder.GenerateId(name);
            if (!string.IsNullOrEmpty(cssClass))
                tagBuilder.AddCssClass(cssClass);
            return new MvcHtmlString(tagBuilder.ToString());
        }
    }

}



