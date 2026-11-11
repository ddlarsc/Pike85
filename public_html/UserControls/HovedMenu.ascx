<%@ Control 
    CodeFile="HovedMenu.ascx.cs" 
    Inherits="UserControls.HovedMenu" 
    Language="c#" %>
    
<asp:HyperLink id="HyperLink_Pike85" NavigateUrl="/Default.aspx" runat="server" Target="_self"> Startside</asp:HyperLink>
<span id="spanPike85" runat="server"/>
<asp:hyperlink id="Hyperlink_Klubben" NavigateUrl="/Klubben/KontaktOs.aspx" runat="server" tabIndex="1">Klubben</asp:hyperlink>
<span id="spanklubben" runat="server"/>
<asp:hyperlink id="HyperLink_Nyheder" NavigateUrl="/Nyheder/VisNyheder.aspx" runat="server" tabIndex="2">Nyheder</asp:hyperlink>
<span id="spannyheder" runat="server"/>
<asp:hyperlink id="Hyperlink_Fangst" NavigateUrl="/Fangster/FindFangster.aspx" runat="server" tabIndex="5">Fangster</asp:hyperlink>
<span id="spanfangst" runat="server"/>
<asp:hyperlink id="Hyperlink_Links" NavigateUrl="/Links/Fiskesider.aspx" runat="server" tabIndex="7">Links</asp:hyperlink>
<span id="spanlinks" runat="server"/>
<asp:hyperlink id="HyperLink_Medlemmer" NavigateUrl="/Medlemmer/Default.aspx" runat="server" tabIndex="8" Visible="False">Kun for Medlemmer</asp:hyperlink>
<span id="spanmedlemmer" runat="server"/>

