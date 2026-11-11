using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.UI;
using Control;
using Model;

namespace UserControls
{
    public partial class UcBlog : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoadUserControl();
            }
            catch (Exception ex)
            {
                var ctrl = new SysExceptionController();
                ctrl.OnException(ex, 0);
            }
        }

        #region Private Helpers

        private void LoadUserControl()
        {
            var conn = Application["MySQLConnString"].ToString();
            var blogId = Convert.ToInt32(ConfigurationManager.AppSettings.Get("PikeBlogId"));
            var blogEntries = Convert.ToInt32(ConfigurationManager.AppSettings.Get("PikeBlogEntries"));

            var ctrl = new BlogspotController();
            var blog = ctrl.Select(conn, blogId);
            if (blog != null)
            {
                var password = InternalHelper.DecryptString(blog.Password);
                var blogRequests = new List<BlogRequest> { new BlogRequest(blog.Url, blog.Username, password, blogEntries) };

                var sb = new StringBuilder();
                foreach (BlogAtomicEntryContent result in ctrl.GetAtomicFeedEntryContent(blogRequests, blogEntries))
                {
                    if(result.Type.ToUpperInvariant().Trim().Equals("HTML"))
                    {
                        sb.AppendFormat("<P><B>{0}</B></P>{1}<hr />", result.Title, result.Content);
                    }
                }
                sb.AppendLine("<P><A HREF='http://pike85blog.blogspot.com'>Se flere blog nyheder</A></P>");
                bloggerText.InnerHtml = sb.ToString();
            }
        }

        #endregion
    }
}