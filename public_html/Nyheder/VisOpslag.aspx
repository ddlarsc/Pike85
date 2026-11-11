<%@ Page 
    AutoEventWireup="true" 
    CodeFile="VisOpslag.aspx.cs" 
    Inherits="Nyheder.VisOpslag" 
    Language="C#" 
    MasterPageFile="~/Skabelon/Nyheder.master" 
    Title="Opslag" %>
    
<%@ Reference Control="~/UserControls/UcOpslag.ascx" %>

<asp:Content ID="ContentOpslag" ContentPlaceHolderID="NyhederBody" runat="Server">
    <table>
        <tr>
            <td class="indhold" valign="top" style="width: 600; text-align: center">
                <h3>Opslag</h3>
                <asp:PlaceHolder id="phOpslag" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>
