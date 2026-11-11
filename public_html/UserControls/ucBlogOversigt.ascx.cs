using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Xml.Linq;
using Control;
using Model;

namespace UserControls
{
    public partial class UcBlogOversigt : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bloggerText.InnerHtml = LoadUserControl();
            }
            catch (Exception ex)
            {
                var ctrl = new SysExceptionController();
                ctrl.OnException(ex, 0);
            }
        }

        #region Private Helpers

        private string LoadUserControl()
        {
            BlogspotController ctrl;
            IEnumerable<BlogRequest> blogRequests = GetBlogRequest(out ctrl);

            var sb = new StringBuilder();
            sb.AppendFormat("<table>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td style='height: 0px; width: 25%' />");
            sb.AppendFormat("<td style='height: 0px; width: 25%' />");
            sb.AppendFormat("<td style='height: 0px; width: 25%' />");
            sb.AppendFormat("<td style='height: 0px; width: 25%' />");
            sb.AppendFormat("</tr>");

            int idx = 0;
            IEnumerable<BlogAtomicEntryContent> blogResults = ctrl.GetAtomicFeedEntryContent(blogRequests, 1);
            foreach (BlogAtomicEntryContent blogResult in blogResults)
            {
                string text;
                string img;
                if (ManipulateBlogResult(blogResult, out text, out img))
                {
                    idx++;
                    sb.AppendFormat("<tr>");
                    if (idx % 2 != 0)
                    {
                        sb.AppendFormat("<td style='text-align: left'>");
                        sb.AppendFormat("<a href='{0}' target='_blank' styel='text-wrap: normal'>", img);
                        sb.AppendFormat("<img src='{0}' style='height: 50px; width: 100%;'", img);
                        sb.AppendFormat("</a>");
                        sb.AppendFormat("</td>");
                        sb.AppendFormat("<td colspan='3' style='text-align: right'>{0}</td>", text);
                    }
                    else
                    {
                        sb.AppendFormat("<td colspan='3' style='text-align: left'>{0}</td>", text);
                        sb.AppendFormat("<td style='text-align: right'>");
                        sb.AppendFormat("<a href='{0}' target='_blank' styel='text-wrap: normal'>", img);
                        sb.AppendFormat("<img src='{0}' style='height: 50px; width: 100%;'", img);
                        sb.AppendFormat("</a>");
                        sb.AppendFormat("</td>");
                    }
                    sb.AppendFormat("</tr>");
                }
            }
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        private IEnumerable<BlogRequest> GetBlogRequest(out BlogspotController ctrl)
        {
            string myConnection = Application["MySQLConnString"].ToString();

            ctrl = new BlogspotController();
            var blogspots = ctrl.Select(myConnection);
            return (
                from x in blogspots
                let password = InternalHelper.DecryptString(x.Password)
                select new BlogRequest(x.Url, x.Username, password, 1)
            ).ToList();
        }

        private bool ManipulateBlogResult(BlogAtomicEntryContent result, out string text, out string img)
        {
            text = string.Empty;
            img = string.Empty;
            var content = string.Empty;
            try
            {
                text = result.Title.Length > 225 ? string.Format("{0}...", result.Title.Substring(0, 225)) : result.Title;
                content = result.Content.Replace("\r<br>", string.Empty);
                //content = result.Content.ToLowerInvariant().Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("<br>", "</br>");
                XDocument xDoc = XDocument.Parse(string.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?><BlogAtomicEntryContent>{0}</BlogAtomicEntryContent>", content), LoadOptions.None);
                foreach (XElement element in xDoc.Descendants("img"))
                {
                    img = element.FirstAttribute.Value;
                    if (!string.IsNullOrEmpty(img))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception firstEx)
            {
                try
                {
                    var message = string.Format("The following HTML string caused an error ...{0}[{1}]{0}{0}Ex.ToString() ...{0}[{2}]{0}{0}Ex.StackTrace ...{0}[{3}]{0}{0}", Environment.NewLine, content, firstEx, firstEx.StackTrace);
                    throw new Exception(message);
                }
                catch (Exception secondEx)
                {
                    var ctrl = new SysExceptionController();
                    ctrl.OnException(secondEx, 0);
                    return false;
                }
            }
        }

        #endregion
    }
}