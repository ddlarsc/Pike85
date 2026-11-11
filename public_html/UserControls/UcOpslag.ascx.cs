using System;
using App_Code;
using App_Code.Model;

namespace UserControls
{
    public partial class UcOpslag:System.Web.UI.UserControl
    {
        public OpslagObject Opslag { get; set; }

        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender,EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            anchor.Name = Opslag.OpslagId.ToString();

            lblOverskrift.Text = Opslag.Overskrift;
            lblBeskrivelse.Text = Opslag.Beskrivelse.Replace("\r\n", "<br>");

            lblLink.Text = Opslag.Link;
            refLink.HRef = @"http://" + Opslag.Link;
            refLink.Visible = !string.IsNullOrEmpty(Opslag.Link);

            imgBilled.ImageUrl = @"~" + Opslag.BilledMappe + Opslag.Billed + @"_thumb.jpg";
            refBilled.HRef = @"~" + Opslag.BilledMappe + Opslag.Billed + @".jpg";
            refBilled.Visible = !string.IsNullOrEmpty(Opslag.Billed);

            lblDato.Text = DateTimeHelper.DateToStringInGuiFormat(Opslag.Dato);
        }
    }
}
