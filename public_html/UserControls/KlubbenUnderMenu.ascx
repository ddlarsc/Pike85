<%@ Control Language="c#" Inherits="UserControls.KlubbenUnderMenu" CodeFile="KlubbenUnderMenu.ascx.cs" %>
<P>
    <asp:HyperLink id="HyperLink_KontaktOs" NavigateUrl="/Klubben/KontaktOs.aspx" runat="server" ToolTip="Kontakt os">Kontakt Os</asp:HyperLink>
</P>
<P>
    <asp:HyperLink id="HyperLink_Medlemmer" NavigateUrl="/Klubben/Medlemmer.aspx" runat="server" ToolTip="Se vores medlemmer">Medlemmer</asp:HyperLink>
</P>
<P>
    <asp:HyperLink id="HyperLink_Vedtaegter" NavigateUrl="/Klubben/Vedtaegter.aspx" runat="server" ToolTip="Læs vores vedtægter">Vedtægter</asp:HyperLink>
</P>
<P>
    <asp:HyperLink id="hlGeneralforsamlingView" NavigateUrl="/Klubben/Medlemmer/Generalforsamling/View.aspx" runat="server" ToolTip="Læs referater fra vores generalforsamlinger">Referater</asp:HyperLink>
</P>
<P>
    <asp:HyperLink id="HyperLink_Fester" runat="server" NavigateUrl="/Klubben/Fester.aspx" Visible="False">Fester</asp:HyperLink>
</P>
<P>
    <asp:HyperLink id="HyperLink_Aktiver" runat="server" NavigateUrl="/Klubben/Aktiver.aspx" Visible="False">Aktiver</asp:HyperLink>
</P>
<P>
    <asp:HyperLink id="hlGeneralforsamlingAdmin" NavigateUrl="/Klubben/Medlemmer/Generalforsamling/Admin.aspx" runat="server" ForeColor="Black" ToolTip="Kun for administratorer">Opret/rediger referat</asp:HyperLink>
</P>

