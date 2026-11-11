using System;
using System.Collections.Generic;
using App_Code.Model;
using Control;
using UserControls;

namespace Klubben.Medlemmer
{
    public partial class Generalforsamling : SkabelonPage
    {
        private string _connection;
        
        protected void Page_Load(object sender , EventArgs e)
        {
            _connection =  Application["MySQLConnString"].ToString();
            Getobjects();
        }

        private void Getobjects()
        {
            try
            {
                var ctrl = new GeneralforsamlingController();
                IEnumerable<GeneralforsamlingObject> models = ctrl.SelectTop5(_connection);
                foreach (GeneralforsamlingObject model in models)
                {
                    var uc = (UcGeneralforsamling)Page.LoadControl("~/UserControls/UcGeneralforsamling.ascx");
                    uc.Generalforsamling = model;
                    phUcGeneralforsamling.Controls.Add(uc);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}