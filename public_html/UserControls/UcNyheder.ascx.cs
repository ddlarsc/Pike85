using System;
using System.Globalization;
using System.Web.UI;
using App_Code;
using App_Code.Model;

namespace UserControls
{
    public partial class UcNyheder : UserControl
    {
        public NyhedObject NyhedObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            anchor.Name = NyhedObject.Id.ToString(CultureInfo.InvariantCulture);

            lblOverskrift.Text = NyhedObject.Overskrift;
            lblBeskrivelse.Text = NyhedObject.Beskrivelse.Replace("\r\n", "<br>");

            lblLink.Text = NyhedObject.Link;
            refLink.HRef = @"http://" + NyhedObject.Link;
            refLink.Visible = !string.IsNullOrEmpty(NyhedObject.Link);

            imgBilled.ImageUrl = @"~" + NyhedObject.BilledMappe + NyhedObject.Billed1 + @"_thumb.jpg";
            refBilled.HRef = @"~" + NyhedObject.BilledMappe + NyhedObject.Billed1 + @".jpg";
            refBilled.Visible = !string.IsNullOrEmpty(NyhedObject.Billed1);

            lblDato.Text = DateTimeHelper.DateToStringInGuiFormat(NyhedObject.Dato);
        }
    }
}