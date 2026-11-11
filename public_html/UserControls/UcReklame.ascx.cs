using System;
using System.Web.UI;

namespace UserControls
{
    /// <summary>
	///		Summary description for Reklame.
	/// </summary>
	public partial class UcReklame : UserControl
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			// Put user code to initialize the page here
		}

        protected void GoogleSoegClick(object sender, EventArgs e)
        {
            // http://www.pike85.dk/GoogleSearchResult.aspx?
            // client=pub-9313326140797505&
            // forid=1&
            // channel=2341985710&
            // ie=ISO-8859-1&
            // oe=ISO-8859-1&
            // flav=0000&
            // sig=8hrehOgq7BWTTflg&
            // cof=GALT%3A%23008000%3BGL%3A1%3BDIV%3A%23336699%3BVLC%3A663399%3BAH%3Acenter%3BBGC%3AFFFFFF%3BLBGC%3A336699%3BALC%3A0000FF%3BLC%3A0000FF%3BT%3A000000%3BGFNT%3A0000FF%3BGIMP%3A0000FF%3BFORID%3A11&
            // hl=da&
            // q=sandart

            const string url = "client=pub-9313326140797505&";
            //url += "forid=1&";
            //url += "channel=2341985710&";
            //url += "ie=ISO-8859-1&";
            //url += "oe=ISO-8859-1&";
            //url += "flav=0000&";
            //url += "sig=8hrehOgq7BWTTflg&";
            //url += "cof=" + Server.UrlEncode("GALT:#008000;GL:1;DIV:#336699;VLC:663399;AH:center;BGC:FFFFFF;LBGC:336699;ALC:0000FF;LC:0000FF;T:000000;GFNT:0000FF;GIMP:0000FF;FORID:11") +"&";
            //url += "hl=da&";
            //url += "q=" + Server.UrlEncode(TextBox_GoogleSoeg.Text);
            
            Response.Redirect("/GoogleSearchResult.aspx?" + url);
        }
	}
}
