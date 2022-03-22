using gjru.Models.Models;
using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI.WebControls;
using TestApp2.Models;

namespace TestApp2.Extensions
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// Хелпер для сортировки
        /// </summary>
        /// <param name="html"></param>
        /// <param name="linkText"></param>
        /// <param name="sortExpression"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static MvcHtmlString SortLink(this HtmlHelper html,
            string linkText,
            string sortExpression,
            string actionName,
            string controllerName,
            RouteValueDictionary routeValues)
        {
            routeValues = routeValues ?? new RouteValueDictionary();
            System.Web.UI.WebControls.SortDirection? sort = null;
            var sortDirectionStr = html.ViewContext.HttpContext.Request["SortDirection"];
            if (!string.IsNullOrEmpty(sortDirectionStr)
                && html.ViewContext.HttpContext.Request["SortExpression"] == sortExpression)
            {
                System.Web.UI.WebControls.SortDirection s;
                if (Enum.TryParse(sortDirectionStr, out s))
                {
                    sort = s;
                }
            }
            routeValues["SortExpression"] = sortExpression;
            routeValues["SortDirection"] = sort.HasValue && sort.Value == SortDirection.Ascending ?
                System.Web.UI.WebControls.SortDirection.Descending :
                System.Web.UI.WebControls.SortDirection.Ascending;
            return html.Partial("SortLink", new SortLinkModel
            {
                ActionName = actionName,
                ControllerName = controllerName,
                SortDirection = sort,
                RouteValues = routeValues,
                LinkText = linkText
            });
        }

        /// <summary>
        /// Метод осуществляет проверку типа файла
        /// </summary>
        /// <param name="html"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsImage(this HtmlHelper html, BinaryFile file)
        {
            if (string.IsNullOrEmpty(file.ContentType))
            {
                return false;
            }
            return file.ContentType.Contains("bmp") ||
                file.ContentType.Contains("jpeg") ||
                file.ContentType.Contains("png");
        }

    }
}
