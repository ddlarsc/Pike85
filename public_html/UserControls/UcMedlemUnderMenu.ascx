<%@ Control 
    CodeFile="UcMedlemUnderMenu.ascx.cs" 
    Inherits="UserControls.MedlemUnderMenu" 
    Language="c#" %>
    
<div style="">
    <p><asp:HyperLink ID="HyperLink_SkiftKodeord" NavigateUrl="~/Medlemmer/Skift_Kodeord.aspx" runat="server">Skift kodeord</asp:HyperLink>&nbsp;</p>
    <p><asp:HyperLink ID="HyperLink_NyArt" NavigateUrl="~/Medlemmer/IndtastArt.aspx" runat="server">Art</asp:HyperLink>&nbsp;</p>
    <p><asp:HyperLink ID="HyperLink_IndtastFangstRapport" NavigateUrl="~/Medlemmer/IndtastFangstRapport.aspx" runat="server">Fangster</asp:HyperLink>&nbsp;</p>
    <p><asp:HyperLink ID="HyperLink_IndtastNyhed" NavigateUrl="~/Medlemmer/IndtastNyhed.aspx" runat="server">Nyhed</asp:HyperLink>&nbsp;</p>
    <p><asp:HyperLink ID="HyperLink_Opslag" NavigateUrl="~/Medlemmer/IndtastOpslag.aspx" runat="server">Opslag</asp:HyperLink>&nbsp;</p>
    <p><asp:HyperLink ID="HyperLink_IndtastLink" NavigateUrl="~/Medlemmer/IndtastLink.aspx" runat="server">Link</asp:HyperLink></p>
    <p><asp:HyperLink ID="HyperLink_TilmeldArrangement" NavigateUrl="~/Medlemmer/IndtastArrangement.aspx" runat="server">Tilmeld<br>Arrangement</asp:HyperLink>&nbsp;</p>
    <p><asp:HyperLink ID="HyperLink_NytArrangement" NavigateUrl="~/Medlemmer/Arrangement/Create.aspx" runat="server">Opret<br>Arrangement</asp:HyperLink>&nbsp;</p>
    <%--<p><asp:HyperLink ID="HyperLink_Blogspot" NavigateUrl="~/Medlemmer/IndtastBlogspot.aspx" runat="server">Blogspot</asp:HyperLink>&nbsp;</p>--%> <%--//ændring pr. 170715--%>
    <p><asp:HyperLink ForeColor="Black" ID="HyperLink_RedigerMedlem" NavigateUrl="~/Administration/Medlem.aspx" runat="server" Visible="false">Admin Medlem</asp:HyperLink>&nbsp;</p>    
</div>