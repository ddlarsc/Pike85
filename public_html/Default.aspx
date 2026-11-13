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
<%@ Reference Control="~/UserControls/UcDefaultGalleri.ascx" %>

<asp:Content ContentPlaceHolderID="Pike85Body" ID="Content1" Runat="Server">
    <table style="width: 100%; table-layout: auto">
        <tr>
            <td style="border-right: #758dc7 thin solid; width: 33.33%; vertical-align: top" rowspan="2">
                <asp:PlaceHolder id="phPokalOversigt" runat="server" EnableViewState="True" ViewStateMode="Enabled"/>
                <asp:PlaceHolder id="phFangster" runat="server" EnableViewState="True" ViewStateMode="Enabled"/>
            </td>
            <td style="width: 33.33%; text-align: center; vertical-align: top;" rowspan="2">
                <asp:PlaceHolder ID="phBlogger" runat="server" />
            </td>
            <td style="border-left: #758dc7 thin solid; width: 33.33%; text-align: center; vertical-align: top;" rowspan="2">
                <asp:PlaceHolder ID="phGalleri" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

