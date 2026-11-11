<%@ Page Language="C#" MasterPageFile="~/Skabelon/Pike85.master" AutoEventWireup="true"
    CodeFile="Opslag.aspx.cs" Inherits="Pike85.Opslag" Title="Opslag" %>

<asp:Content ID="ContentOpslag" ContentPlaceHolderID="Pike85Body" runat="Server">
    <table>
        <tr>
            <td class="indhold" valign="top" style="width: 600; text-align: left">
                <h3 style="text-align: center">Opslag</h3>
                <span id="opslag" runat="server"></span>
            </td>
        </tr>
    </table>
</asp:Content>
