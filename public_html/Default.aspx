<%@ Page 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    EnableViewState="true" 
    Inherits="Default" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Pike85.master" 
    Title="Untitled Page" 
    ViewStateMode="Enabled" %>

<%@ Reference Control="~/UserControls/UcDefaultPokaler.ascx" %>
<%@ Reference Control="~/UserControls/UcDefaultFangster.ascx" %>
<%@ Reference Control="~/UserControls/UcBlog.ascx" %>
<%@ Reference Control="~/UserControls/UcBlogOversigt.ascx" %>
<%@ Reference Control="~/UserControls/UcDefaultNyheder.ascx" %>
<%@ Reference Control="~/UserControls/UcDefaultOpslag.ascx" %>

<asp:Content ContentPlaceHolderID="Pike85Body" ID="Content1" Runat="Server">
    <table>
        <tr>
            <td style="border-right: #758dc7 thin solid; width: 300px; vertical-align: top" rowspan="2">
                <asp:PlaceHolder id="phPokalOversigt" runat="server" EnableViewState="True" ViewStateMode="Enabled"/>
                <asp:PlaceHolder id="phFangster" runat="server" EnableViewState="True" ViewStateMode="Enabled"/>
            </td>
            <td style="border-right: #758dc7 thin solid; width: 300px; text-align: center; vertical-align: top;" rowspan="2">
                <asp:PlaceHolder ID="phBlogger" runat="server" />
            </td>
            <td style="width: 300px; text-align: left; padding: 2px; vertical-align: top;" rowspan="2">
                <%--<asp:PlaceHolder id="phBlogOversigt" runat="server" EnableViewState="True" ViewStateMode="Enabled"/>--%>  <%--//ændring pr. 170715 --%>
                <asp:PlaceHolder id="phNyheder" runat="server" EnableViewState="True" ViewStateMode="Enabled"/>
                <asp:PlaceHolder id="phOpslag" runat="server" EnableViewState="True" ViewStateMode="Enabled"/>
            </td>
        </tr>
    </table>
</asp:Content>

