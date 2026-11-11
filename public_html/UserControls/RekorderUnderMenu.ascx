<%@ Control Language="c#" Inherits="Pike85.UserControls.RekorderUnderMenu" CodeFile="RekorderUnderMenu.ascx.cs" %>
<U><U>
		<P>
			<asp:HyperLink id="HyperLink_Fangster" runat="server" NavigateUrl="/Fangster/Fangster.aspx"> Fangster</asp:HyperLink></P>
	</U></U>
<P><U>Rekorder</U></P>
<P>
	<asp:HyperLink id="HyperLink_KlubRekorder" NavigateUrl="/Fangster/KlubRekorder.aspx" runat="server"> Klub</asp:HyperLink></P>
<P>
	<asp:HyperLink id="HyperLink_PersonligeRekorder" NavigateUrl="/Fangster/PersonligeRekorder.aspx"
		runat="server" Visible="False" Enabled="False">Personlige</asp:HyperLink></P>
<P>
	<asp:HyperLink id="HyperLink_PersonligTop10" NavigateUrl="/Fangster/PersonligTop10.aspx" runat="server">Personlige Top10</asp:HyperLink></P>
<P>
	<asp:HyperLink id="HyperLink_DanmarksRekorder" runat="server" NavigateUrl="/Fangster/DanmarksRekorder.aspx"
		Visible="False">Danske</asp:HyperLink></P>
<P><U>Toppen</U></P>
<P>
	<asp:HyperLink id="HyperLink_Top10" NavigateUrl="/Fangster/Top10.aspx" runat="server">Top 10</asp:HyperLink></P>
<P>
	<asp:HyperLink id="HyperLink_Top25" runat="server" NavigateUrl="/Fangster/Top25.aspx">Top 25</asp:HyperLink></P>
<P>
	<asp:HyperLink id="HyperLink_Top50" NavigateUrl="/Fangster/Top50.aspx" runat="server">Top 50</asp:HyperLink></P>
<P>
	<asp:HyperLink id="HyperLink_Top100" NavigateUrl="/Fangster/Top100.aspx" runat="server">Top 100</asp:HyperLink></P>
