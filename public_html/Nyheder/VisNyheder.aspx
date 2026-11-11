<%@ Page 
    AutoEventWireup="true" 
    CodeFile="VisNyheder.aspx.cs" 
    Inherits="Nyheder.VisNyheder" 
    Language="C#"
    MasterPageFile="~/Skabelon/Nyheder.master" 
    Title="Nyheder" %>
<%@ Reference Control="~/UserControls/UcNyheder.ascx" %>

<asp:Content ID="ContentNyheder" ContentPlaceHolderID="NyhederBody" runat="Server">
    <table>
        <tr>
            <td class="indhold" style="width: 600; text-align: center; vertical-align: top">
                <h3>Nyheder</h3>
                <asp:PlaceHolder id="phNyheder" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
