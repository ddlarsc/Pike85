<%@ Control Language="c#" Inherits="UserControls.UcKlubbenUnderMenu" CodeFile="UcKlubbenUnderMenu.ascx.cs" %>
<P><asp:HyperLink id="HyperLink_KontaktOs" NavigateUrl="~/Klubben/KontaktOs.aspx" runat="server" ToolTip="Kontakt os">Kontakt Os</asp:HyperLink></P>
<P><asp:HyperLink id="HyperLink_Medlemmer" NavigateUrl="~/Klubben/Medlemmer.aspx" runat="server" ToolTip="Se vores medlemmer">Medlemmer</asp:HyperLink></P>
<P><asp:HyperLink id="HyperLink_Vedtaegter" NavigateUrl="~/Klubben/Vedtaegter.aspx" runat="server" ToolTip="Læs vores vedtægter">Vedtægter</asp:HyperLink></P>
<P><asp:HyperLink id="hlGeneralforsamlingView" NavigateUrl="~/Klubben/Medlemmer/Generalforsamling.aspx" runat="server" ToolTip="Læs referater fra vores generalforsamlinger">Referater</asp:HyperLink></P>
<P><asp:HyperLink id="hlGeneralforsamlingAdmin" NavigateUrl="~/Administration/Generalforsamling.aspx" runat="server" ForeColor="Black" ToolTip="Kun for administratorer">Admin Referater</asp:HyperLink></P>

