<%@ Page Language="C#" MasterPageFile="~/Skabelon/Nyheder.master" AutoEventWireup="true"
    CodeFile="Nyheder.aspx.cs" Inherits="Pike85.Nyheder" Title="Nyheder" %>
<asp:Content ID="ContentNyheder" ContentPlaceHolderID="NyhederBody" runat="Server">
    <table>
        <tr>
            <td class="indhold" valign="top" style="width: 600; text-align: left">
                <h3 style="text-align: center">Nyheder</h3>
                <span id="nyheder" runat="server"></span>
            </td>
        </tr>
    </table>
</asp:Content>

